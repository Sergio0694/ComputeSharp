using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="bool3"/> HLSL type.
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    [StructLayout(LayoutKind.Explicit, Size = sizeof(int) * 3)]
    public struct Bool3
    {
        /// <summary>
        /// Gets an <see cref="Bool3"/> value with all components set to <see langword="false"/>.
        /// </summary>
        public static Bool3 False => false;

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with all components set to <see langword="true"/>.
        /// </summary>
        public static Bool3 True => true;

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="X"/> component set to <see langword="true"/>, and the others to <see langword="false"/>.
        /// </summary>
        public static Bool3 TrueX => new(true, false, false);

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Y"/> component set to <see langword="true"/>, and the others to <see langword="false"/>.
        /// </summary>
        public static Bool3 TrueY => new(false, true, false);

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Z"/> component set to <see langword="true"/>, and the others to <see langword="false"/>.
        /// </summary>
        public static Bool3 TrueZ => new(false, false, true);

        /// <summary>
        /// Creates a new <see cref="Bool3"/> instance with the specified parameters.
        /// </summary>
        /// <param name="x">The value to assign to the first vector component.</param>
        /// <param name="y">The value to assign to the second vector component.</param>
        /// <param name="z">The value to assign to the third vector component.</param>
        public Bool3(bool x, bool y, bool z)
        {
            _X = x;
            _Y = y;
            _Z = z;
        }

        [FieldOffset(0)]
        private bool _X;

        /// <summary>
        /// Gets or sets the value of the first vector component.
        /// </summary>
        public bool X
        {
            get => _X;
            set => _X = value;
        }

        [FieldOffset(4)]
        private bool _Y;

        /// <summary>
        /// Gets or sets the value of the second vector component.
        /// </summary>
        public bool Y
        {
            get => _Y;
            set => _Y = value;
        }

        [FieldOffset(8)]
        private bool _Z;

        /// <summary>
        /// Gets or sets the value of the third vector component.
        /// </summary>
        public bool Z
        {
            get => _Z;
            set => _Z = value;
        }

        /// <summary>
        /// Gets or sets the value of the first color component.
        /// </summary>
        public bool R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component.
        /// </summary>
        public bool G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component.
        /// </summary>
        public bool B
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(B)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Bool3"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public bool this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}[int]");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}[int]");
        }

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="X"/> value for all components.
        /// </summary>
        public Bool2 XX => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XX)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
        public Bool2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="X"/> and <see cref="Z"/> values.
        /// </summary>
        public Bool2 XZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="Y"/> and <see cref="X"/> values.
        /// </summary>
        public Bool2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="Y"/> value for all components.
        /// </summary>
        public Bool2 YY => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YY)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="Y"/> and <see cref="Z"/> values.
        /// </summary>
        public Bool2 YZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="Z"/> and <see cref="X"/> values.
        /// </summary>
        public Bool2 ZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="Z"/> and <see cref="Y"/> values.
        /// </summary>
        public Bool2 ZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZY)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="Z"/> value for all components.
        /// </summary>
        public Bool2 ZZ => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="R"/> value for all components.
        /// </summary>
        public Bool2 RR => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RR)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="R"/> and <see cref="G"/> values.
        /// </summary>
        public Bool2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="R"/> and <see cref="B"/> values.
        /// </summary>
        public Bool2 RB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="G"/> and <see cref="R"/> values.
        /// </summary>
        public Bool2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="G"/> value for all components.
        /// </summary>
        public Bool2 GG => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GG)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="G"/> and <see cref="B"/> values.
        /// </summary>
        public Bool2 GB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="B"/> and <see cref="R"/> values.
        /// </summary>
        public Bool2 BR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Bool2"/> value with the <see cref="B"/> and <see cref="G"/> values.
        /// </summary>
        public Bool2 BG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BG)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool2"/> value with the <see cref="B"/> value for all components.
        /// </summary>
        public Bool2 BB => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BB)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="X"/> value for all components.
        /// </summary>
        public Bool3 XXX => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XXX)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
        public Bool3 XXY => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XXY)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values.
        /// </summary>
        public Bool3 XXZ => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values.
        /// </summary>
        public Bool3 XYX => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XYX)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values.
        /// </summary>
        public Bool3 XYY => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XYY)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values.
        /// </summary>
        public Bool3 XYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XYZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values.
        /// </summary>
        public Bool3 XZX => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XZX)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values.
        /// </summary>
        public Bool3 XZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XZY)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values.
        /// </summary>
        public Bool3 XZZ => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values.
        /// </summary>
        public Bool3 YXX => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YXX)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
        public Bool3 YXY => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YXY)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values.
        /// </summary>
        public Bool3 YXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YXZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values.
        /// </summary>
        public Bool3 YYX => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YYX)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Y"/> value for all components.
        /// </summary>
        public Bool3 YYY => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YYY)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values.
        /// </summary>
        public Bool3 YYZ => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values.
        /// </summary>
        public Bool3 YZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YZX)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values.
        /// </summary>
        public Bool3 YZY => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YZY)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values.
        /// </summary>
        public Bool3 YZZ => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values.
        /// </summary>
        public Bool3 ZXX => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
        public Bool3 ZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZXY)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values.
        /// </summary>
        public Bool3 ZXZ => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values.
        /// </summary>
        public Bool3 ZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZYX)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values.
        /// </summary>
        public Bool3 ZYY => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values.
        /// </summary>
        public Bool3 ZYZ => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values.
        /// </summary>
        public Bool3 ZZX => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values.
        /// </summary>
        public Bool3 ZZY => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="Z"/> value for all components.
        /// </summary>
        public Bool3 ZZZ => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="R"/> value for all components.
        /// </summary>
        public Bool3 RRR => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RRR)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values.
        /// </summary>
        public Bool3 RRG => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RRG)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values.
        /// </summary>
        public Bool3 RRB => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RRB)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values.
        /// </summary>
        public Bool3 RGR => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RGR)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values.
        /// </summary>
        public Bool3 RGG => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RGG)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values.
        /// </summary>
        public Bool3 RGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RGB)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values.
        /// </summary>
        public Bool3 RBR => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RBR)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values.
        /// </summary>
        public Bool3 RBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RBG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RBG)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values.
        /// </summary>
        public Bool3 RBB => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(RBB)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values.
        /// </summary>
        public Bool3 GRR => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GRR)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values.
        /// </summary>
        public Bool3 GRG => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GRG)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values.
        /// </summary>
        public Bool3 GRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GRB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GRB)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values.
        /// </summary>
        public Bool3 GGR => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GGR)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="G"/> value for all components.
        /// </summary>
        public Bool3 GGG => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GGG)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values.
        /// </summary>
        public Bool3 GGB => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GGB)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values.
        /// </summary>
        public Bool3 GBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GBR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GBR)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values.
        /// </summary>
        public Bool3 GBG => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GBG)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values.
        /// </summary>
        public Bool3 GBB => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(GBB)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values.
        /// </summary>
        public Bool3 BRR => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BRR)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values.
        /// </summary>
        public Bool3 BRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BRG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BRG)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values.
        /// </summary>
        public Bool3 BRB => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BRB)}");

        /// <summary>
        /// Gets or sets an <see cref="Bool3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values.
        /// </summary>
        public Bool3 BGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BGR)}");
        }

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values.
        /// </summary>
        public Bool3 BGG => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BGG)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values.
        /// </summary>
        public Bool3 BGB => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BGB)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values.
        /// </summary>
        public Bool3 BBR => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BBR)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values.
        /// </summary>
        public Bool3 BBG => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BBG)}");

        /// <summary>
        /// Gets an <see cref="Bool3"/> value with the <see cref="B"/> value for all components.
        /// </summary>
        public Bool3 BBB => throw new InvalidExecutionContextException($"{nameof(Bool3)}.{nameof(BBB)}");

        /// <summary>
        /// Creates a new <see cref="Bool3"/> value with the same value for all its components.
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Bool3"/> instance.</param>
        public static implicit operator Bool3(bool x) => new(x, x, x);
    }
}
