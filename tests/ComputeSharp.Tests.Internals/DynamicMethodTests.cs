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
                // Due to the instance method trick, the first parameter is at offset 1.
                // See the docs on DynamicMethod<T>.New for more info on how this works.
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Dup);
                il.Emit(OpCodes.Mul);
                il.Emit(OpCodes.Ret);
            });

            int result = square(2); // result = 2 * 2

            Assert.IsTrue(result == 4);
        }
    }
}
