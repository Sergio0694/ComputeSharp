using System;
using Vortice.Direct3D12;

namespace ComputeSharp.Graphics.Commands.Abstract
{
    /// <summary>
    /// A base <see langword="class"/> for an instance that deals with GPU commands in some way
    /// </summary>
    internal abstract class CommandController : IDisposable
    {
        /// <summary>
        /// Creates a new <see cref="CommandAllocatorPool"/> instance with the specified parameters
        /// </summary>
        /// <param name="device">The input <see cref="Graphics.GraphicsDevice"/> to use to perform requested operations</param>
        /// <param name="commandListType">The type of command list to use for the current instance</param>
        protected CommandController(GraphicsDevice device, CommandListType commandListType)
        {
            GraphicsDevice = device;
            CommandListType = commandListType;
        }

        /// <summary>
        /// Gets the <see cref="Graphics.GraphicsDevice"/> object targeted by the current instance
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; }

        /// <summary>
        /// Gets the command list type being used by the current instance
        /// </summary>
        public CommandListType CommandListType { get; }

        /// <inheritdoc/>
        public abstract void Dispose();
    }
}
