using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Int2"/>
    public partial struct Int2
    {
        /// <summary>
        /// Gets a <see cref="Int2"/> value with the values <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Int2 XX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(XX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Int2"/> value with the values <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Int2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Int2"/> value with the values <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Int2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets a <see cref="Int2"/> value with the values <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Int2 YY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(YY)}");
        }

        /// <summary>
        /// Gets a <see cref="Int2"/> value with the values <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Int2 RR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(RR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Int2"/> value with the values <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Int2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Int2"/> value with the values <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Int2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets a <see cref="Int2"/> value with the values <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Int2 GG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int2)}.{nameof(GG)}");
        }

        /// <summary>
        /// Negates a <see cref="Int2"/> value.
        /// </summary>
        /// <param name="xy">The <see cref="Int2"/> value to negate.</param>
        public static Int2 operator -(Int2 xy) => throw new InvalidExecutionContextException($"{nameof(Int2)}.-");

        /// <summary>
        /// Sums two <see cref="Int2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to sum.</param>
        /// <param name="right">The second <see cref="Int2"/> value to sum.</param>
        public static Int2 operator +(Int2 left, Int2 right) => throw new InvalidExecutionContextException($"{nameof(Int2)}.+");

        /// <summary>
        /// Divides two <see cref="Int2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to divide.</param>
        /// <param name="right">The second <see cref="Int2"/> value to divide.</param>
        public static Int2 operator /(Int2 left, Int2 right) => throw new InvalidExecutionContextException($"{nameof(Int2)}./");

        /// <summary>
        /// Multiplies two <see cref="Int2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to multiply.</param>
        /// <param name="right">The second <see cref="Int2"/> value to multiply.</param>
        public static Int2 operator *(Int2 left, Int2 right) => throw new InvalidExecutionContextException($"{nameof(Int2)}.*");

        /// <summary>
        /// Subtracts two <see cref="Int2"/> values.
        /// </summary>
        /// <param name="left">The first <see cref="Int2"/> value to subtract.</param>
        /// <param name="right">The second <see cref="Int2"/> value to subtract.</param>
        public static Int2 operator -(Int2 left, Int2 right) => throw new InvalidExecutionContextException($"{nameof(Int2)}.-");
    }
}
