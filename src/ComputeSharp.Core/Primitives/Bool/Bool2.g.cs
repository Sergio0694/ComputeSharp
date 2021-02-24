using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Bool2"/>
    public partial struct Bool2
    {
        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Bool2"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref bool this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}[int]");
        }

        /// <summary>
        /// Gets a <see cref="Bool2"/> value with the values <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool2 XX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(XX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool2"/> value with the values <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool2"/> value with the values <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool2"/> value with the values <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool2 YY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(YY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool2"/> value with the values <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool2 RR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(RR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool2"/> value with the values <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool2"/> value with the values <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool2"/> value with the values <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool2 GG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(GG)}");
        }
    }
}
