using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Double2"/>
    public partial struct Double2
    {
        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Double2"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref double this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double2)}[int]");
        }

        /// <summary>
        /// Gets a <see cref="Double2"/> value with the values <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Double2 XX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(XX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Double2"/> value with the values <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Double2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Double2"/> value with the values <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Double2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets a <see cref="Double2"/> value with the values <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Double2 YY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(YY)}");
        }

        /// <summary>
        /// Gets a <see cref="Double2"/> value with the values <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Double2 RR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(RR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Double2"/> value with the values <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Double2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Double2"/> value with the values <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Double2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets a <see cref="Double2"/> value with the values <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Double2 GG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double2)}.{nameof(GG)}");
        }

        /// <summary>
        /// Negates a <see cref="Double2"/> value.
        /// </summary>
        /// <param name="xy">The <see cref="Double2"/> value to negate.</param>
        public static Double2 operator -(Double2 xy) => throw new InvalidExecutionContextException($"{nameof(Double2)}.-");

        /// <summary>
        /// Sums two <see cref="Double2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Double2"/> value to sum.</param>
        public static Double2 operator +(Double2 left, Double2 right) => throw new InvalidExecutionContextException($"{nameof(Double2)}.+");

        /// <summary>
        /// Divides two <see cref="Double2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Double2"/> value to divide.</param>
        public static Double2 operator /(Double2 left, Double2 right) => throw new InvalidExecutionContextException($"{nameof(Double2)}./");

        /// <summary>
        /// Multiplies two <see cref="Double2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Double2"/> value to multiply.</param>
        public static Double2 operator *(Double2 left, Double2 right) => throw new InvalidExecutionContextException($"{nameof(Double2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Double2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Double2"/> value to subtract.</param>
        public static Double2 operator -(Double2 left, Double2 right) => throw new InvalidExecutionContextException($"{nameof(Double2)}.-");
    }
}
