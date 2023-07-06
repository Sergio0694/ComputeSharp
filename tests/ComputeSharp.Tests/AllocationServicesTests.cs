#if USE_D3D12MA

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using CommunityToolkit.HighPerformance.Helpers;
using ComputeSharp.D3D12MemoryAllocator;
using ComputeSharp.Interop;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Win32;
using HRESULT = Win32.HResult;

namespace ComputeSharp.Tests;

[TestClass]
[TestCategory("AllocationServices")]
public unsafe class AllocationServicesTests
{
    [TestMethod]
    public void ConfigureAllocationFactory_CorrectLogic()
    {
        D3D12MemoryAllocatorFactory allocatorFactory = new();
        Guid uuidOfID3D12MemoryAllocatorFactory = new("CC1E74A7-786D-40F4-8AE2-F8B7A255587E");

        using ComPtr<IUnknown> d3D12MemoryAllocatorFactory = default;

        CustomQueryInterfaceResult result = ((ICustomQueryInterface)allocatorFactory).GetInterface(
            iid: ref uuidOfID3D12MemoryAllocatorFactory,
            ppv: out *(IntPtr*)d3D12MemoryAllocatorFactory.GetAddressOf());

        Assert.AreEqual(CustomQueryInterfaceResult.Handled, result);
        Assert.IsTrue(d3D12MemoryAllocatorFactory.Get() is not null);

        // There's two references: one from the managed object, one from the local ComPtr<T>
        Assert.AreEqual(2u, d3D12MemoryAllocatorFactory.GetReferenceCount());

        try
        {
            // Try to configure the allocator (this should fail)
            AllocationServices.ConfigureAllocatorFactory(allocatorFactory);
        }
        catch (InvalidOperationException)
        {
            // This is expected
        }
        catch
        {
            Assert.Fail();
        }

        // No additional references should've been added
        Assert.AreEqual(2u, d3D12MemoryAllocatorFactory.GetReferenceCount());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConfigureAllocationFactory_ValidatesArgument()
    {
        AllocationServices.ConfigureAllocatorFactory(null!);
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ConstantBuffer<>))]
    [Resource(typeof(ReadOnlyBuffer<>))]
    [Resource(typeof(ReadWriteBuffer<>))]
    public void ValidateAllocatorFunctionalityWithBuffer(Device device, Type bufferType)
    {
        ValidateAllocationState(device.Get().AllocateBuffer<float>(bufferType, 128));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture2D<>))]
    [Resource(typeof(ReadWriteTexture2D<>))]
    public void ValidateAllocatorFunctionalityWithTexture2D(Device device, Type bufferType)
    {
        ValidateAllocationState(device.Get().AllocateTexture2D<float>(bufferType, 16, 16));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(ReadOnlyTexture3D<>))]
    [Resource(typeof(ReadWriteTexture3D<>))]
    public void ValidateAllocatorFunctionalityWithTexture3D(Device device, Type bufferType)
    {
        ValidateAllocationState(device.Get().AllocateTexture3D<float>(bufferType, 16, 16, 4));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadBuffer<>))]
    [Resource(typeof(ReadBackBuffer<>))]
    public void ValidateAllocatorFunctionalityWithTransferBuffer(Device device, Type bufferType)
    {
        ValidateAllocationState(device.Get().AllocateTransferBuffer<float>(bufferType, 128));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadTexture2D<>))]
    [Resource(typeof(ReadBackTexture2D<>))]
    public void ValidateAllocatorFunctionalityWithTransferTexture2D(Device device, Type bufferType)
    {
        ValidateAllocationState(device.Get().AllocateTransferTexture2D<float>(bufferType, 16, 16));
    }

    [CombinatorialTestMethod]
    [AllDevices]
    [Resource(typeof(UploadTexture3D<>))]
    [Resource(typeof(ReadBackTexture3D<>))]
    public void ValidateAllocatorFunctionalityWithTransferTexture3D(Device device, Type bufferType)
    {
        ValidateAllocationState(device.Get().AllocateTransferTexture3D<float>(bufferType, 16, 16, 4));
    }

    private static void ValidateAllocationState(IGraphicsResource resource)
    {
        using ComPtr<IUnknown> allocationPtr = default;

        Type? instanceType = resource.GetType();
        FieldInfo? allocationFieldInfo = null;
        FieldInfo? d3D12ResourceFieldInfo = null;

        // Get the allocation field (might be in a base type)
        do
        {
            allocationFieldInfo = instanceType?.GetField("allocation", BindingFlags.Instance | BindingFlags.NonPublic);
            d3D12ResourceFieldInfo = instanceType?.GetField("d3D12Resource", BindingFlags.Instance | BindingFlags.NonPublic);

            instanceType = instanceType?.BaseType;
        }
        while (allocationFieldInfo is null && instanceType is not null);

        Assert.IsNotNull(allocationFieldInfo);
        Assert.IsNotNull(d3D12ResourceFieldInfo);

        // Get the allocation and D3D12 resource pointers (we use reflection as they're private fields)
        object allocation = allocationFieldInfo.GetValue(resource)!;
        object d3D12Resource = d3D12ResourceFieldInfo.GetValue(resource)!;

        *&allocationPtr = new ComPtr<IUnknown>((IUnknown*)ObjectMarshal.DangerousGetObjectDataReferenceAt<nint>(allocation, default));

        using ComPtr<IUnknown> d3D12ResourcePtr = new((IUnknown*)ObjectMarshal.DangerousGetObjectDataReferenceAt<nint>(d3D12Resource, default));

        Assert.AreNotEqual(0, (nint)allocationPtr.Get());
        Assert.AreNotEqual(0, (nint)d3D12ResourcePtr.Get());

        using ComPtr<IUnknown> wrappedD3D12ResourcePtr = default;

        // Invoke ID3D12Allocation::GetD3D12Resource (manually, as the type definition is internal)
        HRESULT hresult = ((delegate* unmanaged[Stdcall]<IUnknown*, IUnknown**, int>)(*(void***)allocationPtr.Get())[3])(
            allocationPtr.Get(),
            wrappedD3D12ResourcePtr.GetAddressOf());

        Assert.IsTrue(hresult.Success);

        using ComPtr<IUnknown> d3D12ResourceUnknown = default;
        using ComPtr<IUnknown> wrappedD3D12ResourceUnknown = default;

        // Compare the identity of the D3D12 resource from the managed resource, and the one from the allocation
        Assert.IsTrue(d3D12ResourcePtr.CopyTo<IUnknown>(d3D12ResourceUnknown.GetAddressOf()).Success);
        Assert.IsTrue(wrappedD3D12ResourcePtr.CopyTo<IUnknown>(wrappedD3D12ResourceUnknown.GetAddressOf()).Success);
        Assert.AreEqual((nint)d3D12ResourceUnknown.Get(), (nint)wrappedD3D12ResourceUnknown.Get());

        // The allocation is kept alive by the resource at this point
        Assert.IsTrue(allocationPtr.GetReferenceCount() > 1);

        ((IDisposable)resource).Dispose();

        // The local reference is the only thing keeping the allocation alive at this point
        Assert.AreEqual(1u, allocationPtr.GetReferenceCount());
    }
}

#endif