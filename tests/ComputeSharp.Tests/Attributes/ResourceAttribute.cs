using System;

namespace ComputeSharp.Tests.Attributes
{
    /// <summary>
    /// An attribute to use with <see cref="CombinatorialAttribute"/> targeting a specific resource type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ResourceAttribute : Attribute
    {
        public ResourceAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}
