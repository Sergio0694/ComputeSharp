using System;
using System.Reflection.Emit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Internals
{
    [TestClass]
    [TestCategory("DynamicMethod")]
    public class DynamicMethodTests
    {
        [TestMethod]
        public void TestFunc()
        {
            Func<int, int> square = DynamicMethod<Func<int, int>>.New(il =>
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Mul);
                il.Emit(OpCodes.Ret);
            });

            int result = square(2); // result = 2 * 2

            Assert.IsTrue(result == 4);
        }

        public delegate int Square(int x);

        [TestMethod]
        public void TestSquare()
        {
            Square square = DynamicMethod<Square>.New(il =>
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Mul);
                il.Emit(OpCodes.Ret);
            });

            int result = square(2); // result = 2 * 2

            Assert.IsTrue(result == 4);
        }
    }
}
