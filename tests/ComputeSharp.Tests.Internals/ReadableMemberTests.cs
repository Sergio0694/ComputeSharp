using ComputeSharp.Shaders.Translation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Internals
{
    [TestClass]
    [TestCategory("ReadableMembers")]
    public class ReadableMemberTests
    {
        [TestMethod]
        public void PropertyGetter()
        {
            string text = "Hello world";

            ReadableMember member = typeof(string).GetProperty(nameof(string.Length));

            object value = member.GetValue(text);

            Assert.IsTrue((int)value == text.Length);
        }
    }
}
