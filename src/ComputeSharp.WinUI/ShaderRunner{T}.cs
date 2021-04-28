using System;

namespace ComputeSharp.WinUI
{
    /// <summary>
    /// An <see cref="IShaderRunner"/> implementation powered by a supplied shader type.
    /// </summary>
    /// <typeparam name="T">The type of shader to use to render frames.</typeparam>
    public sealed class ShaderRunner<T> : IShaderRunner
        where T : struct, IComputeShader
    {
        /// <summary>
        /// The <see cref="Func{T1, T2, TResult}"/> instance used to create shaders to run.
        /// </summary>
        private readonly Func<IReadWriteTexture2D<Float4>, TimeSpan, T> shaderFactory;

        /// <summary>
        /// Creates a new <see cref="ShaderRunner{T}"/> instance.
        /// </summary>
        /// <param name="shaderFactory">The <see cref="Func{T1, T2, TResult}"/> instance used to create shaders to run.</param>
        public ShaderRunner(Func<IReadWriteTexture2D<Float4>, TimeSpan, T> shaderFactory)
        {
            this.shaderFactory = shaderFactory;
        }

        /// <inheritdoc/>
        public void Execute(IReadWriteTexture2D<Float4> texture, TimeSpan time)
        {
            Gpu.Default.For(texture.Width, texture.Height, this.shaderFactory(texture, time));
        }
    }
}
