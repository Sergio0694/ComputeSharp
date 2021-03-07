using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that indicates the size info of a given GPU thread group running a compute shader within.
    /// </summary>
    public static class GroupSize
    {
        /// <summary>
        /// Gets the total size of the current thread group.
        /// </summary>
        public static int Count => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(Count)}");

        /// <summary>
        /// Gets the size of the X axis of the current thread group.
        /// </summary>
        public static int X => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(X)}");

        /// <summary>
        /// Gets the size of the Y axis of the current thread group.
        /// </summary>
        public static int Y => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(Y)}");

        /// <summary>
        /// Gets the size of the Z axis of the current thread group.
        /// </summary>
        public static int Z => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(Z)}");

        /// <summary>
        /// Gets a <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public static Int2 XX => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(XX)}");

        /// <summary>
        /// Gets a <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public static Int2 XY => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(XY)}");

        /// <summary>
        /// Gets a <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public static Int2 XZ => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(XZ)}");

        /// <summary>
        /// Gets a <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public static Int2 YX => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(YX)}");

        /// <summary>
        /// Gets a <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public static Int2 YY => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(YY)}");

        /// <summary>
        /// Gets a <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public static Int2 YZ => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(YZ)}");

        /// <summary>
        /// Gets a <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public static Int2 ZX => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(ZX)}");

        /// <summary>
        /// Gets a <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public static Int2 ZY => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(ZY)}");

        /// <summary>
        /// Gets a <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public static Int2 ZZ => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 XXX => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(XXX)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 XXY => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(XXY)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 XXZ => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 XYX => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(XYX)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 XYY => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(XYY)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 XYZ => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(XYZ)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 XZX => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(XZX)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 XZY => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(XZY)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 XZZ => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 YXX => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(YXX)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 YXY => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(YXY)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 YXZ => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(YXZ)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 YYX => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(YYX)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 YYY => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(YYY)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 YYZ => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 YZX => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(YZX)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 YZY => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(YZY)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 YZZ => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 ZXX => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 ZXY => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(ZXY)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 ZXZ => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 ZYX => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(ZYX)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 ZYY => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 ZYZ => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 ZZX => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 ZZY => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets a <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 ZZZ => throw new InvalidExecutionContextException($"{typeof(GroupSize)}.{nameof(ZZZ)}");
    }
}
