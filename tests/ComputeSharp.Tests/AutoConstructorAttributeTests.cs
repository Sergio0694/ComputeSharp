using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests
{
    [TestClass]
    [TestCategory("AutoConstructorAttribute")]
    public unsafe partial class AutoConstructorAttributeTests
    {
        [AutoConstructor]
        public partial struct StructWithFixedField
        {
            public int a;
            public float b;
            public byte* c;
            public fixed byte d[16];
        }

        [TestMethod]
        public void GenerateConstructorWithInheritedUnsafeContextAndIgnoredFixedField()
        {
            byte c = 123;

            StructWithFixedField instance = new(42, 3.14f, &c);

            Assert.AreEqual(instance.a, 42);
            Assert.AreEqual(instance.b, 3.14f);
            Assert.IsTrue(instance.c == &c);
        }
    }
}
