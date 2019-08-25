using System.Runtime.InteropServices;
using ComputeSharp.Graphics.Exceptions;

namespace ComputeSharp.Shaders.Primitives.Int
{
    /// <summary>
    /// A <see langword="struct"/> that maps the int4 HLSL type
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = sizeof(int) * 4)]
    public struct Int4
    {
        /// <summary>
        /// Creates a new <see cref="Int4"/> instance with the specified parameters
        /// </summary>
        /// <param name="x">The value to assign to the first vector component</param>
        /// <param name="y">The value to assign to the second vector component</param>
        /// <param name="z">The value to assign to the third vector component</param>
        /// <param name="w">The value to assign to the fourth vector component</param>
        public Int4(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Gets or sets the value of the first vector component
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the value of the second vector component
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the value of the third vector component
        /// </summary>
        public int Z { get; set; }

        /// <summary>
        /// Gets or sets the value of the fourth vector component
        /// </summary>
        public int W { get; set; }

        /// <summary>
        /// Gets or sets the value of the first color component
        /// </summary>
        public float R
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(R)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(R)}");
        }

        /// <summary>
        /// Gets or sets the value of the second color component
        /// </summary>
        public float G
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(G)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(G)}");
        }

        /// <summary>
        /// Gets or sets the value of the third color component
        /// </summary>
        public float B
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(B)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(B)}");
        }

        /// <summary>
        /// Gets or sets the value of the fourth color component
        /// </summary>
        public float A
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(A)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(A)}");
        }

        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Int4"/> instance
        /// </summary>
        /// <param name="i">The index of the component to access</param>
        public float this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}[int]");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}[int]");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public Int2 XX => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 XZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Int2 XW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Int2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public Int2 YY => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YY)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 YZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Int2 YW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Int2 ZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 ZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZY)}");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="Z"/> value for all components
        /// </summary>
        public Int2 ZZ => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Int2 ZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Int2 WX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Int2 WY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Int2 WZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="W"/> value for all components
        /// </summary>
        public Int2 WW => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WW)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public Int2 RR => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RR)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Int2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Int2 RB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Int2 RA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Int2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public Int2 GG => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GG)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Int2 GB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Int2 GA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Int2 BR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Int2 BG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BG)}");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="B"/> value for all components
        /// </summary>
        public Int2 BB => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BB)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Int2 BA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Int2 AR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Int2 AG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Int2 AB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AB)}");
        }

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="A"/> value for all components
        /// </summary>
        public Int2 AA => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AA)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/> value for all components
        /// </summary>
        public Int3 XXX => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XXX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 XXY => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XXY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 XXZ => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Int3 XXW => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XXW)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Int3 XYX => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XYX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 XYY => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XYY)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 XYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XYZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Int3 XYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XYW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XYW)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Int3 XZX => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XZX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 XZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XZY)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 XZZ => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Int3 XZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XZW)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Int3 XWX => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XWX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 XWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XWY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XWY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 XWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XWZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XWZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Int3 XWW => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(XWW)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values
        /// </summary>
        public Int3 YXX => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YXX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 YXY => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YXY)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 YXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YXZ)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Int3 YXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YXW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YXW)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Int3 YYX => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YYX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/> value for all components
        /// </summary>
        public Int3 YYY => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YYY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 YYZ => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Int3 YYW => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YYW)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Int3 YZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YZX)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 YZY => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YZY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 YZZ => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Int3 YZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YZW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Int3 YWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YWX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YWX)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 YWY => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YWY)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 YWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YWZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YWZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Int3 YWW => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(YWW)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 ZXX => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 ZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZXY)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 ZXZ => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Int3 ZXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZXW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZXW)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Int3 ZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZYX)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 ZYY => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 ZYZ => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Int3 ZYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZYW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZYW)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Int3 ZZX => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 ZZY => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/> value for all components
        /// </summary>
        public Int3 ZZZ => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZZZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Int3 ZZW => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZZW)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Int3 ZWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZWX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZWX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 ZWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZWY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZWY)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 ZWZ => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZWZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="W"/> and <see cref="W"/> values
        /// </summary>
        public Int3 ZWW => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ZWW)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 WXX => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WXX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="X"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 WXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WXY)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="X"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 WXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WXZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="X"/> and <see cref="W"/> values
        /// </summary>
        public Int3 WXW => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WXW)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="Y"/> and <see cref="X"/> values
        /// </summary>
        public Int3 WYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WYX)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="Y"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 WYY => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WYY)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="Y"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 WYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WYZ)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="Y"/> and <see cref="W"/> values
        /// </summary>
        public Int3 WYW => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WYW)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="X"/> values
        /// </summary>
        public Int3 WZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WZX)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 WZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WZY)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 WZZ => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WZZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="Z"/> and <see cref="W"/> values
        /// </summary>
        public Int3 WZW => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WZW)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="W"/> and <see cref="X"/> values
        /// </summary>
        public Int3 WWX => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WWX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="W"/> and <see cref="Y"/> values
        /// </summary>
        public Int3 WWY => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WWY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="W"/>, <see cref="W"/> and <see cref="Z"/> values
        /// </summary>
        public Int3 WWZ => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WWZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="W"/> value for all components
        /// </summary>
        public Int3 WWW => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(WWW)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="R"/> value for all components
        /// </summary>
        public Int3 RRR => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RRR)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Int3 RRG => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RRG)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Int3 RRB => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RRB)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Int3 RRA => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RRA)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Int3 RGR => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RGR)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Int3 RGG => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RGG)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Int3 RGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RGB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Int3 RGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RGA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RGA)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Int3 RBR => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RBR)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Int3 RBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RBG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RBG)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Int3 RBB => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RBB)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Int3 RBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RBA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RBA)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Int3 RAR => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RAR)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Int3 RAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RAG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RAG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Int3 RAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RAB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RAB)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="R"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Int3 RAA => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(RAA)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="R"/> values
        /// </summary>
        public Int3 GRR => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GRR)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Int3 GRG => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GRG)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Int3 GRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GRB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GRB)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Int3 GRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GRA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GRA)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Int3 GGR => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GGR)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="G"/> value for all components
        /// </summary>
        public Int3 GGG => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GGG)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Int3 GGB => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GGB)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Int3 GGA => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GGA)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Int3 GBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GBR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GBR)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Int3 GBG => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GBG)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Int3 GBB => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GBB)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Int3 GBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GBA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GBA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Int3 GAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GAR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GAR)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Int3 GAG => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GAG)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Int3 GAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GAB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GAB)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="G"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Int3 GAA => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(GAA)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Int3 BRR => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BRR)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Int3 BRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BRG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BRG)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Int3 BRB => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BRB)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Int3 BRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BRA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BRA)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Int3 BGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BGR)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Int3 BGG => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BGG)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Int3 BGB => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BGB)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Int3 BGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BGA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BGA)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Int3 BBR => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BBR)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Int3 BBG => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BBG)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="B"/> value for all components
        /// </summary>
        public Int3 BBB => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BBB)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Int3 BBA => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BBA)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Int3 BAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BAR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BAR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Int3 BAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BAG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BAG)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Int3 BAB => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BAB)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="B"/>, <see cref="A"/> and <see cref="A"/> values
        /// </summary>
        public Int3 BAA => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(BAA)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Int3 ARR => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ARR)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="R"/> and <see cref="G"/> values
        /// </summary>
        public Int3 ARG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ARG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ARG)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="R"/> and <see cref="B"/> values
        /// </summary>
        public Int3 ARB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ARB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ARB)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="R"/> and <see cref="A"/> values
        /// </summary>
        public Int3 ARA => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ARA)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="G"/> and <see cref="R"/> values
        /// </summary>
        public Int3 AGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AGR)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="G"/> and <see cref="G"/> values
        /// </summary>
        public Int3 AGG => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AGG)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="G"/> and <see cref="B"/> values
        /// </summary>
        public Int3 AGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AGB)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="G"/> and <see cref="A"/> values
        /// </summary>
        public Int3 AGA => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AGA)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="R"/> values
        /// </summary>
        public Int3 ABR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ABR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ABR)}");
        }

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="G"/> values
        /// </summary>
        public Int3 ABG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ABG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ABG)}");
        }

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="B"/> values
        /// </summary>
        public Int3 ABB => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ABB)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="B"/> and <see cref="A"/> values
        /// </summary>
        public Int3 ABA => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(ABA)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="A"/> and <see cref="R"/> values
        /// </summary>
        public Int3 AAR => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AAR)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="A"/> and <see cref="G"/> values
        /// </summary>
        public Int3 AAG => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AAG)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="A"/>, <see cref="A"/> and <see cref="B"/> values
        /// </summary>
        public Int3 AAB => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AAB)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="A"/> value for all components
        /// </summary>
        public Int3 AAA => throw new InvalidExecutionContextException($"{nameof(Int4)}.{nameof(AAA)}");
    }
}
