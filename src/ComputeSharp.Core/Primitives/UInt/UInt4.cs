using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="uint4"/> HLSL type
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z}, {W})")]
    [StructLayout(LayoutKind.Sequential, Size = sizeof(uint) * 4)]
    public struct UInt4
    {
        /// <summary>
        /// Gets an <see cref="UInt4"/> value with all components set to 0
        /// </summary>
        public static UInt4 Zero => 0;

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with all components set to 1
        /// </summary>
        public static UInt4 One => 1;

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/> component set to 1, and the others to 0
        /// </summary>
        public static UInt4 UnitX => new UInt4(1, 0, 0, 0);

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/> component set to 1, and the others to 0
        /// </summary>
        public static UInt4 UnitY => new UInt4(0, 1, 0, 0);

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/> component set to 1, and the others to 0
        /// </summary>
        public static UInt4 UnitZ => new UInt4(0, 0, 1, 0);

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/> component set to 1, and the others to 0
        /// </summary>
        public static UInt4 UnitW => new UInt4(0, 0, 0, 1);

        /// <summary>
        /// Creates a new <see cref="UInt4"/> instance with the specified parameters
        /// </summary>
        /// <param name="x">The value to assign to the first vector component</param>
        /// <param name="y">The value to assign to the second vector component</param>
        /// <param name="z">The value to assign to the third vector component</param>
        /// <param name="w">The value to assign to the fourth vector component</param>
        public UInt4(uint x, uint y, uint z, uint w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
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
        /// Gets or sets the value of the fourth vector component
        /// </summary>
        public uint W { get; set; }

        /// <summary>
        /// Gets or sets the value of the first color component
        /// </summary>
        public uint R
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component
        /// </summary>
        public uint G
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component
        /// </summary>
        public uint B
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(B)}");
        }

        /// <summary>
        /// Gets or sets the value of the fourth color component
        /// </summary>
        public uint A
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(A)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(A)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="UInt4"/> instance
        /// </summary>
        /// <param name="i">The index of the component to access</param>
        public uint this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}[uint]");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}[uint]");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public UInt2 XX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 XZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt2 XW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public UInt2 YY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 YZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt2 YW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 ZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 ZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="Z"/> value for all components
        /// </summary>
        public UInt2 ZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt2 ZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt2 WX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt2 WY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt2 WZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="W"/> value for all components
        /// </summary>
        public UInt2 WW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WW)}");

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public UInt2 RR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 RB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt2 RA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public UInt2 GG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 GB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt2 GA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 BR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 BG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="B"/> value for all components
        /// </summary>
        public UInt2 BB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt2 BA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt2 AR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt2 AG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt2"/> value with the <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt2 AB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt2"/> value with the <see cref="A"/> value for all components
        /// </summary>
        public UInt2 AA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AA)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public UInt3 XXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXX)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 XXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXY)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 XXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 XXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXW)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 XYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYX)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 XYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 XYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 XYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYW)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 XZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 XZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 XZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 XZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZW)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 XWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 XWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 XWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="X"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 XWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWW)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 YXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXX)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 YXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 YXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 YXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXW)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 YYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYX)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public UInt3 YYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYY)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 YYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 YYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYW)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 YZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 YZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZY)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 YZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 YZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 YWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 YWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 YWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 YWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWW)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 ZXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 ZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 ZXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 ZXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 ZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 ZYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 ZYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 ZYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYW)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 ZZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 ZZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/> value for all components
        /// </summary>
        public UInt3 ZZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 ZZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZW)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 ZWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 ZWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 ZWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="Z"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 ZWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWW)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 WXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 WXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 WXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 WXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXW)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 WYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 WYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 WYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 WYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYW)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 WZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 WZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 WZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt3 WZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZW)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt3 WWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWX)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt3 WWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWY)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="W"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt3 WWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="W"/> value for all components
        /// </summary>
        public UInt3 WWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWW)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public UInt3 RRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRR)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 RRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRG)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 RRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRB)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 RRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRA)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 RGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGR)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 RGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 RGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 RGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGA)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 RBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 RBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 RBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 RBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBA)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 RAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 RAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 RAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="R"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 RAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAA)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 GRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRR)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 GRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 GRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 GRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRA)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 GGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGR)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public UInt3 GGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGG)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 GGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGB)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 GGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGA)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 GBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 GBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBG)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 GBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 GBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 GAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 GAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 GAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 GAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAA)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 BRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 BRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 BRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 BRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 BGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 BGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGG)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 BGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 BGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGA)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 BBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBR)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 BBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBG)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/> value for all components
        /// </summary>
        public UInt3 BBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBB)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 BBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBA)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 BAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 BAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 BAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAB)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="B"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 BAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAA)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 ARR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 ARG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 ARB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 ARA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARA)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 AGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 AGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 AGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 AGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGA)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 ABR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 ABG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 ABB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABB)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt3 ABA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABA)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt3 AAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAR)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt3 AAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAG)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="A"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt3 AAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAB)}");

        /// <summary>
        /// Gets an <see cref="UInt3"/> value with the <see cref="A"/> value for all components
        /// </summary>
        public UInt3 AAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public UInt4 XXXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XXXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXXY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XXXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XXXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXXW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XXYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XXYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XXYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XXYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXYW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XXZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXZX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XXZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XXZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XXZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXZW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XXWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXWX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XXWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXWY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XXWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XXWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XXWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XYXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XYXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYXY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XYXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XYXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYXW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XYYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XYYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XYYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XYYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYYW)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XYZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYZX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XYZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XYZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XYZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYZW)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XYWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYWX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XYWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYWY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XYWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYWZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYWZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XYWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XYWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XZXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XZXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZXY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XZXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XZXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZXW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XZYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XZYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XZYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XZYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZYW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZYW)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XZZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZZX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XZZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XZZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XZZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZZW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XZWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZWX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XZWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZWY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZWY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XZWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XZWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XZWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XWXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XWXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWXY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XWXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XWXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWXW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XWYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XWYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWYY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XWYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWYZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XWYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWYW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XWZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWZX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XWZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWZY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XWZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 XWZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWZW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 XWWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWWX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 XWWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWWY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 XWWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="X"/>, <see cref="W"/> value for all components
        /// </summary>
        public UInt4 XWWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(XWWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YXXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YXXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXXY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YXXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YXXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXXW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YXYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YXYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YXYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YXYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXYW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YXZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXZX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YXZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YXZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YXZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXZW)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YXWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXWX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YXWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXWY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YXWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXWZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXWZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YXWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YXWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YYXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YYXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYXY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YYXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YYXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYXW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YYYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public UInt4 YYYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YYYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YYYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYYW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YYZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYZX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YYZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YYZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YYZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYZW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YYWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYWX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YYWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYWY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YYWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YYWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YYWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YZXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YZXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZXY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YZXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YZXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZXW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZXW)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YZYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YZYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YZYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YZYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZYW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YZZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZZX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YZZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/> value for all components
        /// </summary>
        public UInt4 YZZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YZZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZZW)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YZWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZWX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZWX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YZWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZWY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YZWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YZWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YZWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YWXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YWXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWXY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YWXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWXZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YWXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWXW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YWYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YWYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YWYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YWYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWYW)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YWZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWZX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YWZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YWZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YWZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWZW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 YWWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWWX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 YWWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWWY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 YWWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 YWWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(YWWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZXXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZXXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXXY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZXXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZXXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXXW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZXYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZXYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZXYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZXYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXYW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXYW)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZXZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXZX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZXZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZXZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZXZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXZW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZXWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXWX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZXWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXWY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXWY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZXWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZXWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZXWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZYXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZYXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYXY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZYXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZYXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYXW)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYXW)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZYYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZYYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZYYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZYYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYYW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZYZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYZX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZYZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZYZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZYZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYZW)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZYWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYWX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYWX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZYWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYWY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZYWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZYWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZYWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZZXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZZXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZXY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZZXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZZXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZXW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZZYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZZYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZZYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZZYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZYW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZZZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZZX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZZZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/> value for all components
        /// </summary>
        public UInt4 ZZZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZZZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZZW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZZWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZWX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZZWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZWY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZZWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZZWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZZWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZWXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWXX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZWXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWXY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZWXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZWXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWXW)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZWYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWYX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZWYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZWYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZWYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWYW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZWZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWZX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZWZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZWZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZWZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWZW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 ZWWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWWX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 ZWWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWWY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 ZWWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 ZWWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ZWWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WXXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WXXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXXY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WXXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WXXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXXW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WXYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WXYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXYY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WXYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXYZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WXYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXYW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WXZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXZX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WXZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXZY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WXZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WXZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXZW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WXWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXWX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WXWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXWY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WXWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WXWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WXWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WYXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WYXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYXY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WYXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYXZ)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WYXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYXW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WYYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/> value for all components
        /// </summary>
        public UInt4 WYYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WYYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WYYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYYW)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WYZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYZX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WYZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WYZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WYZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYZW)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WYWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYWX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WYWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYWY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WYWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WYWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WYWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WZXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZXX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZXY)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WZXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WZXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZXW)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZYX)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WZYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WZYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WZYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZYW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WZZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZZX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WZZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/> value for all components
        /// </summary>
        public UInt4 WZZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WZZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZZW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WZWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZWX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WZWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZWY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WZWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WZWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WZWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WWXX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWXX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WWXY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWXY)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WWXZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWXZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WWXW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWXW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WWYX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWYX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WWYY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWYY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WWYZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWYZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WWYW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWYW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WWZX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWZX)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WWZY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWZY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WWZZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWZZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public UInt4 WWZW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWZW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public UInt4 WWWX => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWWX)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public UInt4 WWWY => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWWY)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public UInt4 WWWZ => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWWZ)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="W"/> value for all components
        /// </summary>
        public UInt4 WWWW => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(WWWW)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public UInt4 RRRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRRR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RRRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRRG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RRRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRRB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RRRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRRA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RRGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RRGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RRGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRGB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RRGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRGA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RRBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRBR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RRBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRBG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RRBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRBB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RRBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRBA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RRAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRAR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RRAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRAG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RRAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RRAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RRAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RGRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGRR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RGRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGRG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RGRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGRB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RGRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGRA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RGGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RGGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RGGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGGB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RGGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGGA)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RGBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGBR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RGBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGBG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RGBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGBB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RGBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGBA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGBA)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RGAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGAR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RGAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGAG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RGAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGAB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGAB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RGAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RGAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RBRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBRR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RBRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBRG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RBRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBRB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RBRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBRA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RBGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RBGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RBGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBGB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RBGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBGA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBGA)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RBBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBBR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RBBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBBG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RBBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBBB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RBBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBBA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RBAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBAR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RBAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBAG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBAG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RBAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RBAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RBAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RARR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RARR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RARG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RARG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RARB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RARB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RARA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RARA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RAGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RAGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAGG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RAGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAGB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RAGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAGA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RABR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RABR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RABG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RABG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RABG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RABB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RABB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 RABA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RABA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 RAAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAAR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 RAAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAAG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 RAAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="R"/>, <see cref="A"/> value for all components
        /// </summary>
        public UInt4 RAAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(RAAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GRRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRRR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GRRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRRG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GRRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRRB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GRRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRRA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GRGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GRGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GRGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRGB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GRGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRGA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GRBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRBR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GRBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRBG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GRBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRBB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GRBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRBA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRBA)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GRAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRAR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GRAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRAG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GRAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRAB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRAB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GRAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GRAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GGRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGRR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GGRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGRG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GGRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGRB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GGRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGRA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GGGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public UInt4 GGGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GGGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGGB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GGGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGGA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GGBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGBR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GGBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGBG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GGBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGBB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GGBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGBA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GGAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGAR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GGAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGAG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GGAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GGAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GGAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GBRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBRR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GBRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBRG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GBRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBRB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GBRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBRA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBRA)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GBGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GBGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GBGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBGB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GBGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBGA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GBBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBBR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GBBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBBG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/> value for all components
        /// </summary>
        public UInt4 GBBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBBB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GBBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBBA)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GBAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBAR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBAR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GBAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBAG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GBAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GBAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GBAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GARR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GARR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GARG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GARG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GARB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GARB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GARB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GARA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GARA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GAGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GAGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GAGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAGB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GAGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAGA)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GABR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GABR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GABR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GABG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GABG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GABB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GABB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GABA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GABA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 GAAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAAR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 GAAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAAG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 GAAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 GAAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(GAAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BRRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRRR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BRRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRRG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BRRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRRB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BRRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRRA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BRGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BRGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BRGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRGB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BRGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRGA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRGA)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BRBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRBR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BRBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRBG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BRBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRBB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BRBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRBA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BRAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRAR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BRAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRAG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRAG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BRAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BRAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BRAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BGRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGRR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BGRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGRG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BGRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGRB)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BGRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGRA)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGRA)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BGGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BGGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BGGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGGB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BGGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGGA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BGBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGBR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BGBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGBG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BGBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGBB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BGBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGBA)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BGAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGAR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGAR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BGAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGAG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BGAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BGAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BGAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BBRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBRR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BBRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBRG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BBRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBRB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BBRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBRA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BBGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BBGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BBGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBGB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BBGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBGA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BBBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBBR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BBBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBBG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/> value for all components
        /// </summary>
        public UInt4 BBBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBBB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BBBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBBA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BBAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBAR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BBAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBAG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BBAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BBAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BBAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BARR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BARR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BARG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BARG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BARG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BARB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BARB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BARA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BARA)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BAGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAGR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BAGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BAGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAGB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BAGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAGA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BABR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BABR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BABG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BABG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BABB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BABB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BABA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BABA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 BAAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAAR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 BAAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAAG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 BAAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 BAAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(BAAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 ARRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARRR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 ARRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARRG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 ARRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARRB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 ARRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARRA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 ARGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 ARGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARGG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 ARGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARGB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 ARGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARGA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 ARBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARBR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 ARBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARBG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARBG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 ARBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARBB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 ARBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARBA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 ARAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARAR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 ARAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARAG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 ARAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 ARAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ARAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 AGRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGRR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 AGRG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGRG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 AGRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGRB)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGRB)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 AGRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGRA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 AGGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/> value for all components
        /// </summary>
        public UInt4 AGGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 AGGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGGB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 AGGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGGA)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 AGBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGBR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGBR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 AGBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGBG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 AGBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGBB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 AGBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGBA)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 AGAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGAR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 AGAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGAG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 AGAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 AGAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AGAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 ABRR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABRR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 ABRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABRG)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABRG)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 ABRB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABRB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 ABRA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABRA)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 ABGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABGR)}");
        }

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 ABGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 ABGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABGB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 ABGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABGA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 ABBR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABBR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 ABBG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABBG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/> value for all components
        /// </summary>
        public UInt4 ABBB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABBB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 ABBA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABBA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 ABAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABAR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 ABAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABAG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 ABAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 ABAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(ABAA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 AARR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AARR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 AARG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AARG)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 AARB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AARB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 AARA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AARA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 AAGR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAGR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 AAGG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAGG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 AAGB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAGB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 AAGA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAGA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 AABR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AABR)}");

        /// <summary>
        /// Gets or sets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 AABG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AABG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 AABB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AABB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public UInt4 AABA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AABA)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public UInt4 AAAR => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAAR)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public UInt4 AAAG => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAAG)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public UInt4 AAAB => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAAB)}");

        /// <summary>
        /// Gets an <see cref="UInt4"/> value with the <see cref="A"/> value for all components
        /// </summary>
        public UInt4 AAAA => throw new InvalidExecutionContextException($"{nameof(UInt4)}.{nameof(AAAA)}");

        /// <summary>
        /// Creates a new <see cref="UInt4"/> value with the same value for all its components
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="UInt4"/> instance</param>
        public static implicit operator UInt4(uint x) => new UInt4(x, x, x, x);

        /// <summary>
        /// Sums two <see cref="UInt4"/> values
        /// </summary>
        /// <param name="left">The first <see cref="UInt4"/> value to sum</param>
        /// <param name="right">The second <see cref="UInt4"/> value to sum</param>
        public static UInt4 operator +(UInt4 left, UInt4 right) => throw new InvalidExecutionContextException($"{nameof(UInt4)}.+");

        /// <summary>
        /// Divides two <see cref="UInt4"/> values
        /// </summary>
        /// <param name="left">The first <see cref="UInt4"/> value to divide</param>
        /// <param name="right">The second <see cref="UInt4"/> value to divide</param>
        public static UInt4 operator /(UInt4 left, UInt4 right) => throw new InvalidExecutionContextException($"{nameof(UInt4)}./");

        /// <summary>
        /// Multiplies two <see cref="UInt4"/> values
        /// </summary>
        /// <param name="left">The first <see cref="UInt4"/> value to multiply</param>
        /// <param name="right">The second <see cref="UInt4"/> value to multiply</param>
        public static UInt4 operator *(UInt4 left, UInt4 right) => throw new InvalidExecutionContextException($"{nameof(UInt4)}.*");

        /// <summary>
        /// Subtracts two <see cref="UInt4"/> values
        /// </summary>
        /// <param name="left">The first <see cref="UInt4"/> value to subtract</param>
        /// <param name="right">The second <see cref="UInt4"/> value to subtract</param>
        public static UInt4 operator -(UInt4 left, UInt4 right) => throw new InvalidExecutionContextException($"{nameof(UInt4)}.-");
    }
}
