using ComputeSharp.Shaders.Translation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using System.Reflection;

namespace ComputeSharp.Tests.Internals
{
    /// <summary>
    /// A container <see langword="class"/> for static fields and properties to test
    /// </summary>
    public sealed class StaticMembersContainer
    {
        /// <summary>
        /// A <see langword="static"/> <see langword="readonly"/> <see langword="int"/> field
        /// </summary>
        public static readonly int Field = 7;

        /// <summary>
        /// A <see langword="static"/> <see langword="int"/> property
        /// </summary>
        public static int Property { get; } = 44;
    }

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

        [TestMethod]
        public void FieldGetter()
        {
            Vector4 vector = new Vector4(7, 0, 0, 0);

            ReadableMember member = typeof(Vector4).GetField(nameof(Vector4.X));

            object value = member.GetValue(vector);

            Assert.IsTrue((int)(float)value == (int)vector.X);
        }

        [TestMethod]
        public void StaticFieldGetter()
        {
            ReadableMember member = typeof(StaticMembersContainer).GetField(nameof(StaticMembersContainer.Field), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            object value = member.GetValue(null);

            Assert.IsTrue((int)value == StaticMembersContainer.Field);
        }

        [TestMethod]
        public void StaticPropertyGetter()
        {
            ReadableMember member = typeof(StaticMembersContainer).GetProperty(nameof(StaticMembersContainer.Property), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            object value = member.GetValue(null);

            Assert.IsTrue((int)value == StaticMembersContainer.Property);
        }
    }
}
