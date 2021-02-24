using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Float2"/>
    public partial struct Float2
    {
        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Float2"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref float this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}[int]");
        }

        /// <summary>
        /// Gets a <see cref="Float2"/> value with the values <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Float2 XX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(XX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Float2"/> value with the values <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Float2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Float2"/> value with the values <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Float2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets a <see cref="Float2"/> value with the values <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Float2 YY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(YY)}");
        }

        /// <summary>
        /// Gets a <see cref="Float2"/> value with the values <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Float2 RR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(RR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Float2"/> value with the values <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Float2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Float2"/> value with the values <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Float2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets a <see cref="Float2"/> value with the values <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Float2 GG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Float2)}.{nameof(GG)}");
        }

        /// <summary>
        /// Negates a <see cref="Float2"/> value.
        /// </summary>
        /// <param name="xy">The <see cref="Float2"/> value to negate.</param>
        public static Float2 operator -(Float2 xy) => throw new InvalidExecutionContextException($"{nameof(Float2)}.-");

        /// <summary>
        /// Sums two <see cref="Float2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Float2"/> value to sum.</param>
        public static Float2 operator +(Float2 left, Float2 right) => throw new InvalidExecutionContextException($"{nameof(Float2)}.+");

        /// <summary>
        /// Divides two <see cref="Float2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Float2"/> value to divide.</param>
        public static Float2 operator /(Float2 left, Float2 right) => throw new InvalidExecutionContextException($"{nameof(Float2)}./");

        /// <summary>
        /// Multiplies two <see cref="Float2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Float2"/> value to multiply.</param>
        public static Float2 operator *(Float2 left, Float2 right) => throw new InvalidExecutionContextException($"{nameof(Float2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Float2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Float2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Float2"/> value to subtract.</param>
        public static Float2 operator -(Float2 left, Float2 right) => throw new InvalidExecutionContextException($"{nameof(Float2)}.-");
    }
}
