using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="struct"/> that indicates the ids of a given GPU thread running a compute shader.
    /// </summary>
    public readonly ref struct ThreadIds
    {
        /// <summary>
        /// Gets the X id of the current thread.
        /// </summary>
        public int X => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(X)}");

        /// <summary>
        /// Gets the Y id of the current thread.
        /// </summary>
        public int Y => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(Y)}");

        /// <summary>
        /// Gets the Z id of the current thread.
        /// </summary>
        public int Z => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(Z)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="X"/> value for all components.
        /// </summary>
        public Int2 XX => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(XX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
        public Int2 XY => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(XY)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="X"/> and <see cref="Z"/> values.
        /// </summary>
        public Int2 XZ => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(XZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Y"/> and <see cref="X"/> values.
        /// </summary>
        public Int2 YX => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(YX)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="Y"/> value for all components.
        /// </summary>
        public Int2 YY => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(YY)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Y"/> and <see cref="Z"/> values.
        /// </summary>
        public Int2 YZ => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(YZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Z"/> and <see cref="X"/> values.
        /// </summary>
        public Int2 ZX => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(ZX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int2"/> value with the <see cref="Z"/> and <see cref="Y"/> values.
        /// </summary>
        public Int2 ZY => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(ZY)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the <see cref="Z"/> value for all components.
        /// </summary>
        public Int2 ZZ => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/> value for all components.
        /// </summary>
        public Int3 XXX => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(XXX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
        public Int3 XXY => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(XXY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="X"/> and <see cref="Z"/> values.
        /// </summary>
        public Int3 XXZ => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="X"/> values.
        /// </summary>
        public Int3 XYX => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(XYX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Y"/> values.
        /// </summary>
        public Int3 XYY => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(XYY)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> values.
        /// </summary>
        public Int3 XYZ => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(XYZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="X"/> values.
        /// </summary>
        public Int3 XZX => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(XZX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Y"/> values.
        /// </summary>
        public Int3 XZY => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(XZY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="X"/>, <see cref="Z"/> and <see cref="Z"/> values.
        /// </summary>
        public Int3 XZZ => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="X"/> values.
        /// </summary>
        public Int3 YXX => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(YXX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
        public Int3 YXY => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(YXY)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="X"/> and <see cref="Z"/> values.
        /// </summary>
        public Int3 YXZ => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(YXZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="X"/> values.
        /// </summary>
        public Int3 YYX => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(YYX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/> value for all components.
        /// </summary>
        public Int3 YYY => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(YYY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Y"/> and <see cref="Z"/> values.
        /// </summary>
        public Int3 YYZ => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="X"/> values.
        /// </summary>
        public Int3 YZX => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(YZX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Y"/> values.
        /// </summary>
        public Int3 YZY => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(YZY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Y"/>, <see cref="Z"/> and <see cref="Z"/> values.
        /// </summary>
        public Int3 YZZ => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Z"/> values.
        /// </summary>
        public Int3 ZXX => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Y"/> values.
        /// </summary>
        public Int3 ZXY => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(ZXY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="X"/> and <see cref="Z"/> values.
        /// </summary>
        public Int3 ZXZ => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets or sets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="X"/> values.
        /// </summary>
        public Int3 ZYX => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(ZYX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Y"/> values.
        /// </summary>
        public Int3 ZYY => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Y"/> and <see cref="Z"/> values.
        /// </summary>
        public Int3 ZYZ => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="X"/> values.
        /// </summary>
        public Int3 ZZX => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/>, <see cref="Z"/> and <see cref="Y"/> values.
        /// </summary>
        public Int3 ZZY => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the <see cref="Z"/> value for all components.
        /// </summary>
        public Int3 ZZZ => throw new InvalidExecutionContextException($"{nameof(ThreadIds)}.{nameof(ZZZ)}");
    }
}
