using System.Diagnostics;
using System.Runtime.InteropServices;
using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that maps the <see langword="double4"/> HLSL type
    /// </summary>
    [DebuggerDisplay("({X}, {Y}, {Z}, {W})")]
    [StructLayout(LayoutKind.Sequential, Size = sizeof(double) * 4)]
    public struct Double4
    {
        /// <summary>
        /// Gets an <see cref="Double4"/> value with all components set to 0
        /// </summary>
        public static Double4 Zero => 0;

        /// <summary>
        /// Gets an <see cref="Double4"/> value with all components set to 1
        /// </summary>
        public static Double4 One => 1;

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/> component set to 1, and the others to 0
        /// </summary>
        public static Double4 UnitX => new Double4(1, 0, 0, 0);

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/> component set to 1, and the others to 0
        /// </summary>
        public static Double4 UnitY => new Double4(0, 1, 0, 0);

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/> component set to 1, and the others to 0
        /// </summary>
        public static Double4 UnitZ => new Double4(0, 0, 1, 0);

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/> component set to 1, and the others to 0
        /// </summary>
        public static Double4 UnitW => new Double4(0, 0, 0, 1);

        /// <summary>
        /// Creates a new <see cref="Double4"/> instance with the specified parameters
        /// </summary>
        /// <param name="x">The value to assign to the first vector component</param>
        /// <param name="y">The value to assign to the second vector component</param>
        /// <param name="z">The value to assign to the third vector component</param>
        /// <param name="w">The value to assign to the fourth vector component</param>
        public Double4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Gets or sets the value of the first vector component
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the value of the second vector component
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the value of the third vector component
        /// </summary>
        public double Z { get; set; }

        /// <summary>
        /// Gets or sets the value of the fourth vector component
        /// </summary>
        public double W { get; set; }

        /// <summary>
        /// Gets or sets the value of the first color component
        /// </summary>
        public double R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component
        /// </summary>
        public double G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component
        /// </summary>
        public double B
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(B)}");
        }

        /// <summary>
        /// Gets or sets the value of the fourth color component
        /// </summary>
        public double A
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(A)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(A)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Double4"/> instance
        /// </summary>
        /// <param name="i">The index of the component to access</param>
        public double this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}[int]");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}[int]");
        }

        /// <summary>
        /// Gets an <see cref="Double2"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public Double2 XX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double2 XZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double2 XW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets an <see cref="Double2"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public Double2 YY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double2 YZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double2 YW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double2 ZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double2 ZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZY)}");
        }

        /// <summary>
        /// Gets an <see cref="Double2"/> value with the <see cref="Z"/> value for all components
        /// </summary>
        public Double2 ZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double2 ZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double2 WX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double2 WY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double2 WZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Double2"/> value with the <see cref="W"/> value for all components
        /// </summary>
        public Double2 WW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WW)}");

        /// <summary>
        /// Gets an <see cref="Double2"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public Double2 RR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double2 RB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double2 RA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets an <see cref="Double2"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public Double2 GG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double2 GB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double2 GA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double2 BR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double2 BG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BG)}");
        }

        /// <summary>
        /// Gets an <see cref="Double2"/> value with the <see cref="B"/> value for all components
        /// </summary>
        public Double2 BB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double2 BA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double2 AR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double2 AG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double2"/> value with the <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double2 AB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AB)}");
        }

        /// <summary>
        /// Gets an <see cref="Double2"/> value with the <see cref="A"/> value for all components
        /// </summary>
        public Double2 AA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AA)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public Double3 XXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXX)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 XXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXY)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 XXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double3 XXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXW)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double3 XYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYX)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 XYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 XYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double3 XYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYW)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double3 XZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 XZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZY)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 XZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double3 XZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZW)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double3 XWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 XWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 XWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="X"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double3 XWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWW)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public Double3 YXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXX)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 YXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 YXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double3 YXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXW)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double3 YYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYX)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public Double3 YYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYY)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 YYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double3 YYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYW)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double3 YZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZX)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 YZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZY)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 YZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double3 YZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double3 YWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWX)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 YWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 YWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double3 YWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWW)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 ZXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 ZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXY)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 ZXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double3 ZXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double3 ZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYX)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 ZYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 ZYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double3 ZYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYW)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double3 ZZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 ZZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Z"/> value for all components
        /// </summary>
        public Double3 ZZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double3 ZZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZW)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double3 ZWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 ZWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWY)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 ZWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWZ)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="Z"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double3 ZWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWW)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 WXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 WXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 WXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double3 WXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXW)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double3 WYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYX)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 WYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 WYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double3 WYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYW)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double3 WZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 WZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZY)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 WZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZZ)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double3 WZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZW)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double3 WWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWX)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double3 WWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWY)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="W"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double3 WWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWZ)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="W"/> value for all components
        /// </summary>
        public Double3 WWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWW)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public Double3 RRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRR)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double3 RRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRG)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double3 RRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRB)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double3 RRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRA)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double3 RGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGR)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double3 RGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double3 RGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double3 RGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGA)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double3 RBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double3 RBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBG)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double3 RBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double3 RBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBA)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double3 RAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double3 RAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double3 RAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAB)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="R"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double3 RAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAA)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public Double3 GRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRR)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double3 GRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double3 GRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double3 GRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRA)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double3 GGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGR)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public Double3 GGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGG)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double3 GGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGB)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double3 GGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGA)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double3 GBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBR)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double3 GBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBG)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double3 GBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double3 GBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double3 GAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAR)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double3 GAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double3 GAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAB)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double3 GAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAA)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double3 BRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double3 BRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRG)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double3 BRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double3 BRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double3 BGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGR)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double3 BGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGG)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double3 BGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double3 BGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGA)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double3 BBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBR)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double3 BBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBG)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="B"/> value for all components
        /// </summary>
        public Double3 BBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBB)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double3 BBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBA)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double3 BAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double3 BAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAG)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double3 BAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAB)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="B"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double3 BAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAA)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double3 ARR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double3 ARG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double3 ARB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARB)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double3 ARA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARA)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double3 AGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGR)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double3 AGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double3 AGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGB)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double3 AGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGA)}");

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double3 ABR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double3 ABG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABG)}");
        }

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double3 ABB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABB)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double3 ABA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABA)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double3 AAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAR)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double3 AAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAG)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="A"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double3 AAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAB)}");

        /// <summary>
        /// Gets an <see cref="Double3"/> value with the <see cref="A"/> value for all components
        /// </summary>
        public Double3 AAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public Double4 XXXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XXXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXXY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XXXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXXZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XXXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXXW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XXYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XXYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XXYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XXYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXYW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XXZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXZX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XXZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XXZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XXZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXZW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XXWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXWX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XXWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXWY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XXWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="X"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XXWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XXWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XYXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XYXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYXY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XYXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYXZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XYXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYXW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XYYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XYYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XYYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYYZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XYYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYYW)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XYZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYZX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XYZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XYZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XYZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYZW)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XYWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYWX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XYWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYWY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XYWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYWZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYWZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XYWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XYWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XZXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XZXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZXY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XZXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZXZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XZXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZXW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XZYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XZYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XZYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XZYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZYW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZYW)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XZZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZZX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XZZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XZZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XZZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZZW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XZWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZWX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XZWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZWY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZWY)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XZWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XZWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XZWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XWXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XWXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWXY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XWXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWXZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XWXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWXW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XWYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XWYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWYY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XWYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWYZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XWYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWYW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XWZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWZX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XWZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWZY)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XWZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 XWZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWZW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 XWWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWWX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 XWWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWWY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 XWWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="X"/>, <see cref="W"/> value for all components
        /// </summary>
        public Double4 XWWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(XWWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YXXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YXXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXXY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YXXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXXZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YXXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXXW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YXYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YXYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YXYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXYZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YXYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXYW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YXZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXZX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YXZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YXZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YXZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXZW)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YXWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXWX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YXWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXWY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YXWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXWZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXWZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="X"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YXWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YXWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YYXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YYXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYXY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YYXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YYXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYXW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YYYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public Double4 YYYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YYYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYYZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YYYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYYW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YYZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYZX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YYZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YYZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YYZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYZW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YYWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYWX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YYWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYWY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YYWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YYWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YYWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YZXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YZXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZXY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YZXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YZXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZXW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZXW)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YZYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YZYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YZYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZYZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YZYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZYW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YZZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZZX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YZZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/> value for all components
        /// </summary>
        public Double4 YZZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YZZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZZW)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YZWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZWX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZWX)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YZWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZWY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YZWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YZWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YZWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YWXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YWXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWXY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YWXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWXZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YWXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWXW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YWYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YWYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YWYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWYZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YWYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWYW)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YWZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWZX)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YWZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YWZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YWZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWZW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 YWWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWWX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 YWWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWWY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 YWWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Y"/>, <see cref="W"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 YWWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(YWWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZXXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZXXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXXY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZXXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXXZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZXXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXXW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZXYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZXYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZXYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZXYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXYW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXYW)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZXZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXZX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZXZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZXZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZXZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXZW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZXWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXWX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZXWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXWY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXWY)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZXWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="X"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZXWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZXWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZYXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZYXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYXY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZYXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZYXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYXW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYXW)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZYYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZYYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZYYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYYZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZYYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYYW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZYZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYZX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZYZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZYZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZYZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYZW)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZYWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYWX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYWX)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZYWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYWY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZYWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZYWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZYWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZZXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZZXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZXY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZZXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZXZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZZXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZXW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZZYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZZYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZZYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZYZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZZYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZYW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZZZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZZX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZZZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/> value for all components
        /// </summary>
        public Double4 ZZZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZZZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZZW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZZWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZWX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZZWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZWY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZZWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZZWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZZWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZWXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWXX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZWXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWXY)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZWXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWXZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZWXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWXW)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZWYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWYX)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZWYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZWYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWYZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZWYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWYW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZWZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWZX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZWZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZWZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZWZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWZW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 ZWWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWWX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 ZWWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWWY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 ZWWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="Z"/>, <see cref="W"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 ZWWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ZWWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WXXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WXXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXXY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WXXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXXZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WXXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXXW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WXYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WXYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXYY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WXYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXYZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WXYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXYW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WXZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXZX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WXZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXZY)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WXZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WXZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXZW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WXWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXWX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WXWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXWY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WXWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="X"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WXWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WXWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WYXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WYXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYXY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WYXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYXZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WYXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYXW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WYYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/> value for all components
        /// </summary>
        public Double4 WYYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WYYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYYZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WYYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYYW)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WYZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYZX)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WYZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WYZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WYZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYZW)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WYWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYWX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WYWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYWY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WYWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Y"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WYWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WYWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WZXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZXX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZXY)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WZXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZXZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WZXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZXW)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZYX)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WZYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WZYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZYZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WZYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZYW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WZZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZZX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WZZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/> value for all components
        /// </summary>
        public Double4 WZZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WZZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZZW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WZWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZWX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WZWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZWY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WZWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="Z"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WZWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WZWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WWXX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWXX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WWXY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWXY)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WWXZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWXZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WWXW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWXW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WWYX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWYX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WWYY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWYY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WWYZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWYZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WWYW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWYW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WWZX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWZX)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WWZY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWZY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WWZZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWZZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Double4 WWZW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWZW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Double4 WWWX => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWWX)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Double4 WWWY => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWWY)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/>, <see cref="W"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Double4 WWWZ => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWWZ)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="W"/> value for all components
        /// </summary>
        public Double4 WWWW => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(WWWW)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public Double4 RRRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRRR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RRRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRRG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RRRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRRB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RRRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRRA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RRGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RRGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RRGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRGB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RRGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRGA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RRBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRBR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RRBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRBG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RRBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRBB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RRBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRBA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RRAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRAR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RRAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRAG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RRAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="R"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RRAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RRAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RGRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGRR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RGRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGRG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RGRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGRB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RGRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGRA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RGGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RGGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RGGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGGB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RGGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGGA)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RGBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGBR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RGBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGBG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RGBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGBB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RGBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGBA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGBA)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RGAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGAR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RGAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGAG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RGAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGAB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGAB)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="G"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RGAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RGAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RBRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBRR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RBRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBRG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RBRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBRB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RBRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBRA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RBGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RBGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RBGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBGB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RBGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBGA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBGA)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RBBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBBR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RBBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBBG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RBBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBBB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RBBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBBA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RBAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBAR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RBAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBAG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBAG)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RBAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="B"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RBAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RBAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RARR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RARR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RARG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RARG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RARB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RARB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RARA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RARA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RAGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RAGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAGG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RAGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAGB)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RAGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAGA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RABR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RABR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RABG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RABG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RABG)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RABB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RABB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 RABA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RABA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 RAAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAAR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 RAAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAAG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 RAAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="R"/>, <see cref="A"/> value for all components
        /// </summary>
        public Double4 RAAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(RAAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GRRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRRR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GRRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRRG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GRRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRRB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GRRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRRA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GRGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GRGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GRGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRGB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GRGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRGA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GRBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRBR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GRBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRBG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GRBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRBB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GRBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRBA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRBA)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GRAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRAR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GRAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRAG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GRAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRAB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRAB)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="R"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GRAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GRAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GGRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGRR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GGRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGRG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GGRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGRB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GGRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGRA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GGGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public Double4 GGGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GGGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGGB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GGGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGGA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GGBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGBR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GGBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGBG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GGBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGBB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GGBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGBA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GGAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGAR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GGAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGAG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GGAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="G"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GGAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GGAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GBRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBRR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GBRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBRG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GBRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBRB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GBRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBRA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBRA)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GBGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GBGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GBGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBGB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GBGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBGA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GBBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBBR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GBBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBBG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/> value for all components
        /// </summary>
        public Double4 GBBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBBB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GBBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBBA)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GBAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBAR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBAR)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GBAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBAG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GBAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="B"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GBAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GBAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GARR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GARR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GARG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GARG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GARB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GARB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GARB)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GARA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GARA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GAGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GAGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GAGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAGB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GAGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAGA)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GABR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GABR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GABR)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GABG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GABG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GABB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GABB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GABA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GABA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 GAAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAAR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 GAAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAAG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 GAAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="G"/>, <see cref="A"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 GAAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(GAAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BRRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRRR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BRRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRRG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BRRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRRB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BRRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRRA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BRGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BRGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BRGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRGB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BRGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRGA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRGA)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BRBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRBR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BRBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRBG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BRBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRBB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BRBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRBA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BRAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRAR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BRAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRAG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRAG)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BRAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="R"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BRAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BRAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BGRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGRR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BGRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGRG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BGRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGRB)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BGRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGRA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGRA)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BGGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BGGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BGGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGGB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BGGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGGA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BGBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGBR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BGBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGBG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BGBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGBB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BGBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGBA)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BGAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGAR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGAR)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BGAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGAG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BGAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="G"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BGAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BGAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BBRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBRR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BBRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBRG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BBRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBRB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BBRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBRA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BBGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BBGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BBGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBGB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BBGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBGA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BBBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBBR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BBBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBBG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/> value for all components
        /// </summary>
        public Double4 BBBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBBB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BBBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBBA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BBAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBAR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BBAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBAG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BBAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="B"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BBAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BBAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BARR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BARR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BARG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BARG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BARG)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BARB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BARB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BARA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BARA)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BAGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAGR)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BAGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BAGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAGB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BAGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAGA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BABR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BABR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BABG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BABG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BABB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BABB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BABA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BABA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 BAAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAAR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 BAAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAAG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 BAAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="B"/>, <see cref="A"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 BAAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(BAAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 ARRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARRR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 ARRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARRG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 ARRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARRB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 ARRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARRA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 ARGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 ARGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARGG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 ARGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARGB)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 ARGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARGA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 ARBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARBR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 ARBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARBG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARBG)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 ARBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARBB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 ARBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARBA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 ARAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARAR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 ARAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARAG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 ARAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="R"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 ARAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ARAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public Double4 AGRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGRR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 AGRG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGRG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 AGRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGRB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGRB)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 AGRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGRA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 AGGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/> value for all components
        /// </summary>
        public Double4 AGGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 AGGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGGB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 AGGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGGA)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 AGBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGBR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGBR)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 AGBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGBG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 AGBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGBB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 AGBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGBA)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 AGAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGAR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 AGAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGAG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 AGAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="G"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 AGAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AGAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 ABRR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABRR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 ABRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABRG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABRG)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 ABRB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABRB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 ABRA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABRA)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 ABGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABGR)}");
        }

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 ABGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 ABGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABGB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 ABGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABGA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 ABBR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABBR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 ABBG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABBG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/> value for all components
        /// </summary>
        public Double4 ABBB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABBB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 ABBA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABBA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 ABAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABAR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 ABAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABAG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 ABAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="B"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Double4 ABAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(ABAA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 AARR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AARR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Double4 AARG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AARG)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Double4 AARB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AARB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Double4 AARA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AARA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Double4 AAGR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAGR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Double4 AAGG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAGG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Double4 AAGB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAGB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Double4 AAGA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAGA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Double4 AABR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AABR)}");

        /// <summary>
        /// Gets or sets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Double4 AABG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AABG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Double4 AABB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AABB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Double4 AABA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AABA)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Double4 AAAR => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAAR)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Double4 AAAG => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAAG)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/>, <see cref="A"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Double4 AAAB => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAAB)}");

        /// <summary>
        /// Gets an <see cref="Double4"/> value with the <see cref="A"/> value for all components
        /// </summary>
        public Double4 AAAA => throw new InvalidExecutionContextException($"{nameof(Double4)}.{nameof(AAAA)}");

        /// <summary>
        /// Creates a new <see cref="Double4"/> value with the same value for all its components
        /// </summary>
        /// <param name="x">The value to use for the components of the new <see cref="Double4"/> instance</param>
        public static implicit operator Double4(double x) => new Double4(x, x, x, x);

        /// <summary>
        /// Sums two <see cref="Double4"/> values
        /// </summary>
        /// <param name="left">The first <see cref="Double4"/> value to sum</param>
        /// <param name="right">The second <see cref="Double4"/> value to sum</param>
        public static Double4 operator +(Double4 left, Double4 right) => throw new InvalidExecutionContextException($"{nameof(Double4)}.+");

        /// <summary>
        /// Divides two <see cref="Double4"/> values
        /// </summary>
        /// <param name="left">The first <see cref="Double4"/> value to divide</param>
        /// <param name="right">The second <see cref="Double4"/> value to divide</param>
        public static Double4 operator /(Double4 left, Double4 right) => throw new InvalidExecutionContextException($"{nameof(Double4)}./");

        /// <summary>
        /// Multiplies two <see cref="Double4"/> values
        /// </summary>
        /// <param name="left">The first <see cref="Double4"/> value to multiply</param>
        /// <param name="right">The second <see cref="Double4"/> value to multiply</param>
        public static Double4 operator *(Double4 left, Double4 right) => throw new InvalidExecutionContextException($"{nameof(Double4)}.*");

        /// <summary>
        /// Subtracts two <see cref="Double4"/> values
        /// </summary>
        /// <param name="left">The first <see cref="Double4"/> value to subtract</param>
        /// <param name="right">The second <see cref="Double4"/> value to subtract</param>
        public static Double4 operator -(Double4 left, Double4 right) => throw new InvalidExecutionContextException($"{nameof(Double4)}.-");
    }
}
