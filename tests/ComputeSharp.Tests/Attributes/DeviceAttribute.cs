using System;
using ComputeSharp.Tests.Extensions;

namespace ComputeSharp.Tests.Attributes
{
    /// <summary>
    /// An attribute to use with <see cref="CombinatorialAttribute"/> adding a specific <see cref="Extensions.Device"/> input.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class DeviceAttribute : Attribute
    {
        public DeviceAttribute(Device device)
        {
            Device = device;
        }

        /// <summary>
        /// Gets the <see cref="Extensions.Device"/> target to use.
        /// </summary>
        public Device Device { get; }
    }
}
