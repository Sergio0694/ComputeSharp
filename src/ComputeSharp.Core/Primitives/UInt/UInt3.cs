using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="uint3"/> HLSL type
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    [StructLayout(LayoutKind.Sequential, Size = sizeof(uint) * 3)]
    public struct UInt3
    {
        /// <summary>
        /// Gets an <see cref="UInt3"/> value with all components set to 0
        /// </summary>
        public static UInt3 Zero => 0;

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with all components set to 1
        /// </summary>
        public static UInt3 One => 1;

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/> component set to 1, and the others to 0
        /// </summary>
        public static UInt3 UnitX => new UInt3(1, 0, 0);

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/> component set to 1, and the others to 0
        /// </summary>
        public static UInt3 UnitY => new UInt3(0, 1, 0);

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/> component set to 1, and the others to 0
        /// </summary>
        public static UInt3 UnitZ => new UInt3(0, 0, 1);

        /// <summary>
        /// Creates a new <see cref="UInt3"/> instance with the specified parameters
        /// </summary>
        /// <param name="x">The value to assign to the first vector component</param>
        /// <param name="y">The value to assign to the second vector component</param>
        /// <param name="z">The value to assign to the third vector component</param>
        public UInt3(uint x, uint y, uint z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Gets or sets the value of the first vector component
        /// </summary>
        public uint X { get; set; }

        /// <summary>
        /// Gets or sets the value of the second vector component
        /// </summary>
        public uint Y { get; set; }

        /// <summary>
        /// Gets or sets the value of the third vector component
        /// </summary>
        public uint Z { get; set; }

        /// <summary>
        /// Gets or sets the value of the first color component
        /// </summary>
        public uint R
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component
        /// </summary>
        public uint G
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component
        /// </summary>
        public uint B
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(B)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="UInt3"/> instance
        /// </summary>
        /// <param name="i">The index of the component to access</param>
        public uint this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}[int]");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}[int]");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public UInt2 XX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 XZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public UInt2 YY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 YZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 ZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 ZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="Z"/> value for all components
        /// </summary>
        public UInt2 ZZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public UInt2 RR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 RB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public UInt2 GG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 GB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 BR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 BG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="B"/> value for all components
        /// </summary>
        public UInt2 BB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BB)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public UInt2 XXX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XXX)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 XXY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XXY)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 XXZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 XYX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XYX)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 XYY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XYY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 XYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XYZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 XZX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XZX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 XZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XZY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 XZZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 YXX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YXX)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 YXY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YXY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 YXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YXZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 YYX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YYX)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public UInt2 YYY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YYY)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 YYZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 YZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YZX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 YZY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YZY)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 YZZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 ZXX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 ZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZXY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 ZXZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 ZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZYX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 ZYY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 ZYZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 ZZX => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 ZZY => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/> value for all components
        /// </summary>
        public UInt2 ZZZ => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public UInt2 RRR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RRR)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 RRG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RRG)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 RRB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RRB)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 RGR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RGR)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 RGG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RGG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 RGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RGB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 RBR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RBR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 RBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RBG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RBG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 RBB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(RBB)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 GRR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GRR)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 GRG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GRG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 GRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GRB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GRB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 GGR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GGR)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public UInt2 GGG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GGG)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 GGB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GGB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 GBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GBR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GBR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 GBG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GBG)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 GBB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(GBB)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 BRR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BRR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 BRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BRG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BRG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 BRB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BRB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 BGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BGR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 BGG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BGG)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 BGB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BGB)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 BBR => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BBR)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 BBG => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BBG)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/> value for all components
        /// </summary>
        public UInt2 BBB => throw new InvalidExecutionContextException($"{nameof(UInt3)}.{nameof(BBB)}");

        /// <summary>
        /// Creates a new <see cref="UInt3"/> value with the same value for all its components
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt3"/> instance</param>
        public static implicit operator UInt3(uint x) => new UInt3(x, x, x);

        /// <summary>
        /// Sums two <see cref="UInt3"/> values
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to sum</param>
        /// <param name="right">The second <see cref="UInt3"/> value to sum</param>
        public static UInt3 operator +(UInt3 left, UInt3 right) => throw new InvalidExecutionContextException($"{nameof(UInt3)}.+");

        /// <summary>
        /// Divides two <see cref="UInt3"/> values
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to divide</param>
        /// <param name="right">The second <see cref="UInt3"/> value to divide</param>
        public static UInt3 operator /(UInt3 left, UInt3 right) => throw new InvalidExecutionContextException($"{nameof(UInt3)}./");

        /// <summary>
        /// Multiplies two <see cref="UInt3"/> values
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to multiply</param>
        /// <param name="right">The second <see cref="UInt3"/> value to multiply</param>
        public static UInt3 operator *(UInt3 left, UInt3 right) => throw new InvalidExecutionContextException($"{nameof(UInt3)}.*");

        /// <summary>
        /// Subtracts two <see cref="UInt3"/> values
        /// </summary>
        /// <param name="left">The first <see cref="UInt3"/> value to subtract</param>
        /// <param name="right">The second <see cref="UInt3"/> value to subtract</param>
        public static UInt3 operator -(UInt3 left, UInt3 right) => throw new InvalidExecutionContextException($"{nameof(UInt3)}.-");
    }
}
