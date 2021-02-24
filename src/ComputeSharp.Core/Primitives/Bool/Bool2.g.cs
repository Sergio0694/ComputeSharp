using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Bool2"/>
    public partial struct Bool2
    {
        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Bool2"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref bool this[int i] => throw new InvalidExecutionContextException($"{nameof(Bool2)}[int]");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Bool2 XX => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Bool2 XY => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Bool2 YX => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Bool2 YY => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(YY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Bool2 RR => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Bool2 RG => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Bool2 GR => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Bool2 GG => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(GG)}");
    }
}
