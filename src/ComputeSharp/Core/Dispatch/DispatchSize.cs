using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that indicates the size of the current shader dispatch being executed.
    /// That is, it enables a shader to access info on the targeted number of invocations along each axis.
    /// </summary>
    public static class DispatchSize
    {
        /// <summary>
        /// Gets the total number of dispatch invocations being executed.
        /// </summary>
        public static int Count => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(Count)}");

        /// <summary>
        /// Gets the number of dispatch invocations being executed on the X axis.
        /// </summary>
        public static int X => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(X)}");

        /// <summary>
        /// Gets the number of dispatch invocations being executed on the Y axis.
        /// </summary>
        public static int Y => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(Y)}");

        /// <summary>
        /// Gets the number of dispatch invocations being executed on the Z axis.
        /// </summary>
        public static int Z => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(Z)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public static Int2 XX => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(XX)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public static Int2 XY => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(XY)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public static Int2 XZ => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(XZ)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public static Int2 YX => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(YX)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public static Int2 YY => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(YY)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public static Int2 YZ => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(YZ)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public static Int2 ZX => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(ZX)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public static Int2 ZY => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(ZY)}");

        /// <summary>
        /// Gets an <see cref="Int2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public static Int2 ZZ => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(ZZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 XXX => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(XXX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 XXY => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(XXY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 XXZ => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(XXZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 XYX => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(XYX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 XYY => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(XYY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 XYZ => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(XYZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 XZX => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(XZX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 XZY => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(XZY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 XZZ => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(XZZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 YXX => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(YXX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 YXY => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(YXY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 YXZ => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(YXZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 YYX => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(YYX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 YYY => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(YYY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 YYZ => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(YYZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 YZX => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(YZX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 YZY => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(YZY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 YZZ => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(YZZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 ZXX => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(ZXX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 ZXY => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(ZXY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 ZXZ => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(ZXZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 ZYX => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(ZYX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 ZYY => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(ZYY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 ZYZ => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(ZYZ)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
        /// </summary>
        public static Int3 ZZX => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(ZZX)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
        /// </summary>
        public static Int3 ZZY => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(ZZY)}");

        /// <summary>
        /// Gets an <see cref="Int3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
        /// </summary>
        public static Int3 ZZZ => throw new InvalidExecutionContextException($"{typeof(DispatchSize)}.{nameof(ZZZ)}");
    }
}
