using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.NetCore.Tests
{
    [TestClass]
    [TestCategory("Misc")]
    public class MiscTests
    {
        [TestMethod]
        public void CheckIsSupportedAndGetDefaultDevice()
        {
            if (Gpu.IsSupported) Assert.IsNotNull(Gpu.Default);
            else Assert.ThrowsException<NotSupportedException>(() => Gpu.Default);
        }
    }
}
