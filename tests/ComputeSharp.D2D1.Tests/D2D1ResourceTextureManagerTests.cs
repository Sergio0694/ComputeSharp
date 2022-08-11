using System;
using ComputeSharp.D2D1.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop.Windows;

namespace ComputeSharp.D2D1.Tests;

[TestClass]
[TestCategory("D2D1ResourceTextureManager")]
public class D2D1ResourceTextureManagerTests
{
    [TestMethod]
    public unsafe void VerifyInterfaces()
    {
        using ComPtr<IUnknown> resourceTextureManager = default;

        D2D1ResourceTextureManager.Create((void**)resourceTextureManager.GetAddressOf());

        Assert.IsTrue(resourceTextureManager.Get() is not null);

        _ = resourceTextureManager.Get()->AddRef();

        Assert.AreEqual(1u, resourceTextureManager.Get()->Release());

        using ComPtr<IUnknown> unknown = default;
        using ComPtr<IUnknown> resourceTextureManager2 = default;
        using ComPtr<IUnknown> resourceTextureManagerInternal = default;

        Guid uuidOfIUnknown = typeof(IUnknown).GUID;
        Guid uuidOfResourceTextureManager = new("3C4FC7E4-A419-46CA-B5F6-66EB4FF18D64");
        Guid uuidOfResourceTextureManagerInternal = new("5CBB1024-8EA1-4689-81BF-8AD190B5EF5D");

        // The object implements IUnknown and the two resource texture manager interfaces
        Assert.AreEqual(0, (int)resourceTextureManager.AsIID(&uuidOfIUnknown, &unknown));
        Assert.AreEqual(0, (int)resourceTextureManager.AsIID(&uuidOfResourceTextureManager, &resourceTextureManager2));
        Assert.AreEqual(0, (int)resourceTextureManager.AsIID(&uuidOfResourceTextureManagerInternal, &resourceTextureManagerInternal));

        Assert.IsTrue(unknown.Get() is not null);
        Assert.IsTrue(resourceTextureManager2.Get() is not null);
        Assert.IsTrue(resourceTextureManagerInternal.Get() is not null);

        using ComPtr<IUnknown> garbage = default;

        Guid uuidOfGarbage = Guid.NewGuid();

        // Any other random QueryInterface should fail
        Assert.AreEqual(E.E_NOINTERFACE, (int)resourceTextureManager.AsIID(&uuidOfGarbage, &garbage));

        Assert.IsTrue(garbage.Get() is null);
    }
}