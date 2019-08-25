using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Exceptions;

namespace ComputeSharp.Shaders.Primitives.Float
{
    /// <summary>
    /// A <see langword="struct"/> that maps the float2 HLSL type
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = sizeof(float) * 2)]
    public struct Float2
    {
        /// <summary>
        /// Creates a new <see cref="Float2"/> instance with the specified parameters
        /// </summary>
        /// <param name="x">The value to assign to the first vector component</param>
        /// <param name="y">The value to assign to the second vector component</param>
        public Float2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets or sets the value of the first vector component
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the value of the second vector component
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Gets or sets the value of the first color component
        /// </summary>
        public float R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component
        /// </summary>
        public float G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Float2"/> instance
        /// </summary>
        /// <param name="i">The index of the component to access</param>
        public float this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}[int]");
            set => throw new InvalidExecutionContextException($"{nameof(Float2)}[int]");
        }

        /// <summary>
        /// Gets an <see cref="Float2"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public Float2 XX => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(XX)}");

        /// <summary>
        /// Gets or sets an <see cref="Float2"/> value with the <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Float2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets an <see cref="Float2"/> value with the <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Float2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets an <see cref="Float2"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public Float2 YY => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(YY)}");

        /// <summary>
        /// Gets an <see cref="Float2"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public Float2 RR => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(RR)}");

        /// <summary>
        /// Gets or sets an <see cref="Float2"/> value with the <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Float2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets an <see cref="Float2"/> value with the <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Float2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets an <see cref="Float2"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public Float2 GG => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(GG)}");

        /// <summary>
        /// Creates a new <see cref="Float2"/> value with the same value for all its components
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Float2"/> instance</param>
        public static implicit operator Float2(float x) => new Float2(x, x);
    }
}
