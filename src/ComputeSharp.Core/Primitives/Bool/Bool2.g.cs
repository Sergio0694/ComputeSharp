using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Bool2"/>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public partial struct Bool2
    {
        [FieldOffset(0)]
        private bool x;

        [FieldOffset(4)]
        private bool y;

        /// <summary>
        /// Gets a reference to a specific component in the current <see cref="Bool2"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref bool this[int i] => throw new InvalidExecutionContextException($"{nameof(Bool2)}[int]");

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>X</c> component.
        /// </summary>
        public ref bool X => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(X)}");

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>Y</c> component.
        /// </summary>
        public ref bool Y => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(Y)}");

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>Z</c> component.
        /// </summary>
        public ref bool Z => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(Z)}");

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>W</c> component.
        /// </summary>
        public ref bool W => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(W)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public ref readonly Bool2 XX => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(XX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public ref Bool2 XY => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(XY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public ref Bool2 XZ => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(XZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public ref Bool2 XW => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(XW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public ref Bool2 YX => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(YX)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public ref readonly Bool2 YY => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(YY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public ref Bool2 YZ => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(YZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public ref Bool2 YW => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(YW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public ref Bool2 ZX => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(ZX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public ref Bool2 ZY => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(ZY)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public ref readonly Bool2 ZZ => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public ref Bool2 ZW => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(ZW)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public ref Bool2 WX => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(WX)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public ref Bool2 WY => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(WY)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public ref Bool2 WZ => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(WZ)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public ref readonly Bool2 WW => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(WW)}");

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>R</c> component.
        /// </summary>
        public ref bool R => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(R)}");

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>G</c> component.
        /// </summary>
        public ref bool G => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(G)}");

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>B</c> component.
        /// </summary>
        public ref bool B => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(B)}");

        /// <summary>
        /// Gets a reference to the <see cref="bool"/> value representing the <c>A</c> component.
        /// </summary>
        public ref bool A => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(A)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public ref readonly Bool2 RR => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(RR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public ref Bool2 RG => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(RG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public ref Bool2 RB => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(RB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public ref Bool2 RA => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(RA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public ref Bool2 GR => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(GR)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public ref readonly Bool2 GG => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(GG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public ref Bool2 GB => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(GB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public ref Bool2 GA => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(GA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public ref Bool2 BR => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(BR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public ref Bool2 BG => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(BG)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public ref readonly Bool2 BB => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(BB)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public ref Bool2 BA => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(BA)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public ref Bool2 AR => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(AR)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public ref Bool2 AG => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(AG)}");

        /// <summary>
        /// Gets a reference to the <see cref="Bool2"/> value with the components <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public ref Bool2 AB => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(AB)}");

        /// <summary>
        /// Gets a readonly reference to the <see cref="Bool2"/> value with the components <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public ref readonly Bool2 AA => throw new InvalidExecutionContextException($"{nameof(Bool2)}.{nameof(AA)}");
    }
}
