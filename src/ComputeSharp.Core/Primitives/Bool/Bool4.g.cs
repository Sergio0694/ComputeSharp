using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="Bool4"/>
    public partial struct Bool4
    {
        /// <summary>
        /// Gets or sets a specific component in the current <see cref="Bool4"/> instance.
        /// </summary>
        /// <param name="i">The index of the component to access.</param>
        public ref bool this[int i]
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}[int]");
        }

        /// <summary>
        /// Gets a <see cref="Bool2"/> value with the values <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool2 XX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool2"/> value with the values <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool2 XY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool2"/> value with the values <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool2 YX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool2"/> value with the values <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool2 YY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool2"/> value with the values <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool2 RR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool2"/> value with the values <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool2 RG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool2"/> value with the values <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool2 GR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool2"/> value with the values <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool2 GG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool3 XXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool3 XXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool3 XXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool3 XYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool3 XYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool3"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool3 XYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool3 XZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool3"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool3 XZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool3 XZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool3 YXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool3 YXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool3"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool3 YXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool3 YYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool3 YYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool3 YYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYZ)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool3"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool3 YZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool3 YZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool3 YZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool3 ZXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool3"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool3 ZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool3 ZXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXZ)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool3"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool3 ZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool3 ZYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool3 ZYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool3 ZZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool3 ZZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool3 ZZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool3 RRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool3 RRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool3 RRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool3 RGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool3 RGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool3"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool3 RGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool3 RBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool3"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool3 RBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool3 RBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool3 GRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool3 GRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool3"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool3 GRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool3 GGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool3 GGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool3 GGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGB)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool3"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool3 GBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool3 GBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool3 GBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool3 BRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool3"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool3 BRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool3 BRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRB)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool3"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool3 BGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool3 BGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool3 BGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool3 BBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool3 BBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool3"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool3 BBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XXXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XXXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XXXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XXXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XXYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XXYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XXYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XXYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XXZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XXZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XXZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XXZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XXWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XXWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XXWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XXWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XXWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XYXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XYXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XYXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XYXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XYYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XYYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XYYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XYYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XYZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XYZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XYZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYZZ)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XYZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XYWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XYWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYWY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XYWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYWZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XYWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XYWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XZXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XZXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XZXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XZYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XZYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZYZ)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XZYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZYW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XZZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XZZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XZZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XZZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XZWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZWX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XZWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZWY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XZWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XZWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XZWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XWXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XWXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XWXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XWXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XWYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XWYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWYY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XWYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XWYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XWZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWZX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XWZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XWZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XWZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 XWWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 XWWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 XWWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="X"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 XWWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(XWWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YXXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YXXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YXXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YXXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YXYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YXYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YXYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YXYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YXZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YXZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YXZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXZZ)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YXZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXZW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YXWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YXWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXWY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YXWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXWZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YXWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YXWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YYXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YYXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YYXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YYXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YYYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YYYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YYYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YYYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YYZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YYZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YYZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YYZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YYWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YYWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YYWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YYWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YYWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YZXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YZXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZXZ)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YZXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZXW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YZYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YZYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YZYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YZZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YZZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YZZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YZZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZZW)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YZWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZWX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YZWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YZWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YZWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YZWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YWXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YWXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWXY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YWXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YWXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YWYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YWYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YWYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YWYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWYW)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YWZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YWZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YWZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YWZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 YWWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 YWWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 YWWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 YWWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(YWWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZXXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZXXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZXXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZXXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZXYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZXYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZXYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXYZ)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZXYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXYW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZXZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZXZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZXZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZXZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZXWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXWX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZXWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXWY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZXWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZXWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZXWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZYXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZYXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZYXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYXZ)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZYXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYXW)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZYYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZYYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZYYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZYYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZYZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZYZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZYZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZYZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYZW)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZYWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYWX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZYWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZYWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZYWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZYWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZZXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZZXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZZXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZZYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZZYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZZYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZZZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZZZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZZZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZZZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZZWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZZWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZZWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZZWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZZWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZWXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWXX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZWXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZWXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZWXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWXW)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZWYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZWYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZWYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZWYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZWZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZWZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZWZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZWZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 ZWWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 ZWWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 ZWWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 ZWWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ZWWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WXXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WXXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WXXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WXXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WXYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WXYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXYY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WXYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXYZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WXYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WXZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXZX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WXZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXZY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WXZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WXZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WXWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WXWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WXWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="X"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WXWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WXWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WYXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WYXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYXY)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WYXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYXZ)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WYXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WYYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WYYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WYYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WYYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYYW)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WYZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYZX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WYZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WYZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WYZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WYWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WYWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WYWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WYWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WYWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WZXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZXX)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WZXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZXY)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WZXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WZXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZXW)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WZYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZYX)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WZYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WZYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WZYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WZZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WZZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WZZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WZZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WZWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WZWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WZWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WZWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WZWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WWXX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWXX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WWXY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWXY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WWXZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWXZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="X"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WWXW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWXW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WWYX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWYX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WWYY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWYY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WWYZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWYZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WWYW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWYW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WWZX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWZX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WWZY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWZY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WWZZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWZZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WWZW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWZW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="X"/>.
        /// </summary>
        public Bool4 WWWX
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWWX)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Y"/>.
        /// </summary>
        public Bool4 WWWY
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWWY)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="Z"/>.
        /// </summary>
        public Bool4 WWWZ
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWWZ)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="W"/>, <see cref="W"/>, <see cref="W"/>, <see cref="W"/>.
        /// </summary>
        public Bool4 WWWW
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(WWWW)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RRRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RRRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RRRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRRB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RRRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRRA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RRGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RRGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RRGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RRGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RRBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RRBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RRBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RRBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRBA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RRAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RRAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RRAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RRAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RRAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RGRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RGRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RGRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGRB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RGRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGRA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RGGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RGGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RGGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RGGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RGBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RGBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RGBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGBB)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RGBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGBA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGBA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RGAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RGAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGAG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RGAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGAB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RGAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RGAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RBRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RBRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RBRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBRB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RBRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBRA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RBGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RBGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RBGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBGB)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RBGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBGA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RBBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RBBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RBBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RBBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBBA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RBAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBAR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RBAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBAG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RBAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RBAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RBAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RARR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RARR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RARG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RARG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RARB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RARB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RARA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RARA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RAGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RAGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RAGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RAGG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RAGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RAGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RAGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RAGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RAGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RABR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RABR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RABG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RABG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RABG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RABB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RABB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RABA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RABA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 RAAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RAAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 RAAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RAAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 RAAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RAAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="R"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 RAAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(RAAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GRRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GRRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GRRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRRB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GRRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRRA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GRGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GRGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GRGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GRGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GRBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GRBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GRBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRBB)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GRBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRBA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRBA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GRAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GRAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRAG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GRAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRAB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GRAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GRAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GGRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GGRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GGRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGRB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GGRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGRA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GGGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GGGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GGGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GGGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GGBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GGBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GGBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GGBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGBA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GGAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GGAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GGAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GGAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GGAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GBRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GBRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GBRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBRB)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GBRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBRA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBRA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GBGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GBGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GBGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GBGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GBBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GBBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GBBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GBBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBBA)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GBAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBAR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GBAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GBAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GBAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GBAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GARR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GARR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GARG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GARG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GARB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GARB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GARB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GARA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GARA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GAGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GAGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GAGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GAGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GAGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GAGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GAGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GAGA)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GABR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GABR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GABR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GABG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GABG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GABB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GABB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GABA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GABA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 GAAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GAAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 GAAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GAAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 GAAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GAAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="G"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 GAAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(GAAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BRRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BRRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BRRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRRB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BRRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRRA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BRGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BRGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BRGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRGB)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BRGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRGA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BRBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BRBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BRBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BRBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRBA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BRAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRAR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BRAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRAG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BRAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BRAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BRAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BGRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BGRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BGRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGRB)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BGRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGRA)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGRA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BGGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BGGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BGGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BGGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BGBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BGBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BGBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BGBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGBA)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BGAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGAR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BGAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BGAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BGAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BGAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BBRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BBRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BBRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBRB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BBRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBRA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BBGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BBGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BBGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BBGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BBBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BBBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BBBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BBBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBBA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BBAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BBAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BBAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BBAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BBAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BARR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BARR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BARG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BARG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BARG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BARB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BARB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BARA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BARA)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BAGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BAGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BAGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BAGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BAGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BAGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BAGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BAGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BAGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BABR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BABR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BABG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BABG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BABB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BABB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BABA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BABA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 BAAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BAAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 BAAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BAAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 BAAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BAAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="B"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 BAAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(BAAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 ARRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 ARRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 ARRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARRB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 ARRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARRA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 ARGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 ARGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARGG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 ARGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARGB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 ARGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 ARBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARBR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 ARBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARBG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 ARBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 ARBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARBA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 ARAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 ARAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 ARAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="R"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 ARAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ARAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 AGRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGRR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 AGRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGRG)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 AGRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGRB)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGRB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 AGRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGRA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 AGGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 AGGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 AGGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 AGGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGGA)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 AGBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGBR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 AGBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 AGBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 AGBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGBA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 AGAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 AGAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 AGAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="G"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 AGAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AGAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 ABRR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABRR)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 ABRG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABRG)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABRG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 ABRB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABRB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 ABRA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABRA)}");
        }

        /// <summary>
        /// Gets or sets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 ABGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABGR)}");
            set => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 ABGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 ABGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 ABGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 ABBR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABBR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 ABBG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABBG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 ABBB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABBB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 ABBA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABBA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 ABAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 ABAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 ABAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="B"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 ABAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(ABAA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 AARR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AARR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 AARG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AARG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 AARB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AARB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="R"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 AARA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AARA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 AAGR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AAGR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 AAGG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AAGG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 AAGB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AAGB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="G"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 AAGA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AAGA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 AABR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AABR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 AABG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AABG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 AABB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AABB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="B"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 AABA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AABA)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="R"/>.
        /// </summary>
        public Bool4 AAAR
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AAAR)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="G"/>.
        /// </summary>
        public Bool4 AAAG
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AAAG)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="B"/>.
        /// </summary>
        public Bool4 AAAB
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AAAB)}");
        }

        /// <summary>
        /// Gets a <see cref="Bool4"/> value with the values <see cref="A"/>, <see cref="A"/>, <see cref="A"/>, <see cref="A"/>.
        /// </summary>
        public Bool4 AAAA
        {
            get => throw new InvalidExecutionContextException($"{nameof(Bool4)}.{nameof(AAAA)}");
        }
    }
}
