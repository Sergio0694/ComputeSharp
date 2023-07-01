#if USE_D3D12MA

using System;
using System.Runtime.InteropServices;
using ComputeSharp.D3D12MemoryAllocator;
using ComputeSharp.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Win32;

namespace ComputeSharp.Tests;

[TestClass]
[TestCategory("AllocationServices")]
public class AllocationServicesTests
{
    [TestMethod]
    public unsafe void ConfigureAllocationFactory_CorrectLogic()
    {
#if !USE_D3D12MA
        // Ensure a device has been created already (if D3D12MA is set, this isn't needed, because
        // the global assembly initializer will have always configured D3D12MA for the process).
        _ = GraphicsDevice.GetDefault();
#endif

        D3D12MemoryAllocatorFactory allocatorFactory = new();
        Guid uuidOfID3D12MemoryAllocatorFactory = new("CC1E74A7-786D-40F4-8AE2-F8B7A255587E");

        using ComPtr<IUnknown> d3D12MemoryAllocatorFactory = default;

        CustomQueryInterfaceResult result = ((ICustomQueryInterface)allocatorFactory).GetInterface(
            iid: ref uuidOfID3D12MemoryAllocatorFactory,
            ppv: out *(IntPtr*)d3D12MemoryAllocatorFactory.GetAddressOf());

        Assert.AreEqual(CustomQueryInterfaceResult.Handled, result);
        Assert.IsTrue(d3D12MemoryAllocatorFactory.Get() is not null);

        _ = d3D12MemoryAllocatorFactory.Get()->AddRef();

        uint referenceCount = d3D12MemoryAllocatorFactory.Get()->Release();

        // There's two references: one from the managed object, one from the local ComPtr<T>
        Assert.AreEqual(2u, referenceCount);

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

        _ = d3D12MemoryAllocatorFactory.Get()->AddRef();

        referenceCount = d3D12MemoryAllocatorFactory.Get()->Release();

        // No additional references should've been added
        Assert.AreEqual(2u, referenceCount);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConfigureAllocationFactory_ValidatesArgument()
    {
        AllocationServices.ConfigureAllocatorFactory(null!);
    }
}

#endif