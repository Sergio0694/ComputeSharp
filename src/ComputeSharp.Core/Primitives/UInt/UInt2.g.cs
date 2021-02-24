using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="UInt2"/>
    public partial struct UInt2
    {
        /// <summary>
        /// Gets or sets a specific component in the current <see cref="UInt2"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref uint this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}[int]");
        }

        /// <summary>
        /// Gets a <see cref="UInt2"/> value with the values <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public UInt2 XX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(XX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="UInt2"/> value with the values <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public UInt2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="UInt2"/> value with the values <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public UInt2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets a <see cref="UInt2"/> value with the values <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public UInt2 YY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(YY)}");
        }

        /// <summary>
        /// Gets a <see cref="UInt2"/> value with the values <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public UInt2 RR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(RR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="UInt2"/> value with the values <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public UInt2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="UInt2"/> value with the values <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public UInt2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets a <see cref="UInt2"/> value with the values <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public UInt2 GG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt2)}.{nameof(GG)}");
        }

        /// <summary>
        /// Sums two <see cref="UInt2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2"/> value to sum.</param>
        /// <param name="right">The second <see cref="UInt2"/> value to sum.</param>
        public static UInt2 operator +(UInt2 left, UInt2 right) => throw new InvalidExecutionContextException($"{nameof(UInt2)}.+");

        /// <summary>
        /// Divides two <see cref="UInt2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2"/> value to divide.</param>
        /// <param name="right">The second <see cref="UInt2"/> value to divide.</param>
        public static UInt2 operator /(UInt2 left, UInt2 right) => throw new InvalidExecutionContextException($"{nameof(UInt2)}./");

        /// <summary>
        /// Multiplies two <see cref="UInt2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="UInt2"/> value to multiply.</param>
        public static UInt2 operator *(UInt2 left, UInt2 right) => throw new InvalidExecutionContextException($"{nameof(UInt2)}.*");

        /// <summary>
        /// Subtracts two <see cref="UInt2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="UInt2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="UInt2"/> value to subtract.</param>
        public static UInt2 operator -(UInt2 left, UInt2 right) => throw new InvalidExecutionContextException($"{nameof(UInt2)}.-");
    }
}
