using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <inheritdoc cref="ThreadIds"/>
    public static partial class ThreadIds
    {
        /// <summary>
        /// A <see langword="class"/> that indicates the normalized ids of a given GPU thread running a compute shader.
        /// These ids represent equivalent info to those from <see cref="ThreadIds"/>, but normalized in the [0, 1] range.
        /// The range used for the normalization is the one given by the target dispatch size (see <see cref="DispatchSize"/>).
        /// </summary>
        public static class Normalized
        {
            /// <summary>
            /// Gets the normalized X id of the current thread.
            /// </summary>
            public static float X => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(X)}");

            /// <summary>
            /// Gets the normalized Y id of the current thread.
            /// </summary>
            public static float Y => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(Y)}");

            /// <summary>
            /// Gets the normalized Z id of the current thread.
            /// </summary>
            public static float Z => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(Z)}");

            /// <summary>
            /// Gets a <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="X"/>.
            /// </summary>
            public static Float2 XX => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(XX)}");

            /// <summary>
            /// Gets a <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Y"/>.
            /// </summary>
            public static Float2 XY => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(XY)}");

            /// <summary>
            /// Gets a <see cref="Float2"/> value with the components <see cref="X"/>, <see cref="Z"/>.
            /// </summary>
            public static Float2 XZ => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(XZ)}");

            /// <summary>
            /// Gets a <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="X"/>.
            /// </summary>
            public static Float2 YX => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(YX)}");

            /// <summary>
            /// Gets a <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Y"/>.
            /// </summary>
            public static Float2 YY => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(YY)}");

            /// <summary>
            /// Gets a <see cref="Float2"/> value with the components <see cref="Y"/>, <see cref="Z"/>.
            /// </summary>
            public static Float2 YZ => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(YZ)}");

            /// <summary>
            /// Gets a <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="X"/>.
            /// </summary>
            public static Float2 ZX => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(ZX)}");

            /// <summary>
            /// Gets a <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Y"/>.
            /// </summary>
            public static Float2 ZY => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(ZY)}");

            /// <summary>
            /// Gets a <see cref="Float2"/> value with the components <see cref="Z"/>, <see cref="Z"/>.
            /// </summary>
            public static Float2 ZZ => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(ZZ)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="X"/>.
            /// </summary>
            public static Float3 XXX => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(XXX)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Y"/>.
            /// </summary>
            public static Float3 XXY => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(XXY)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="X"/>, <see cref="Z"/>.
            /// </summary>
            public static Float3 XXZ => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(XXZ)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="X"/>.
            /// </summary>
            public static Float3 XYX => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(XYX)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Y"/>.
            /// </summary>
            public static Float3 XYY => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(XYY)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>.
            /// </summary>
            public static Float3 XYZ => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(XYZ)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="X"/>.
            /// </summary>
            public static Float3 XZX => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(XZX)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Y"/>.
            /// </summary>
            public static Float3 XZY => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(XZY)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="X"/>, <see cref="Z"/>, <see cref="Z"/>.
            /// </summary>
            public static Float3 XZZ => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(XZZ)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="X"/>.
            /// </summary>
            public static Float3 YXX => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(YXX)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Y"/>.
            /// </summary>
            public static Float3 YXY => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(YXY)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="X"/>, <see cref="Z"/>.
            /// </summary>
            public static Float3 YXZ => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(YXZ)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="X"/>.
            /// </summary>
            public static Float3 YYX => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(YYX)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Y"/>.
            /// </summary>
            public static Float3 YYY => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(YYY)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Y"/>, <see cref="Z"/>.
            /// </summary>
            public static Float3 YYZ => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(YYZ)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="X"/>.
            /// </summary>
            public static Float3 YZX => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(YZX)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Y"/>.
            /// </summary>
            public static Float3 YZY => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(YZY)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Y"/>, <see cref="Z"/>, <see cref="Z"/>.
            /// </summary>
            public static Float3 YZZ => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(YZZ)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="X"/>.
            /// </summary>
            public static Float3 ZXX => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(ZXX)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Y"/>.
            /// </summary>
            public static Float3 ZXY => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(ZXY)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="X"/>, <see cref="Z"/>.
            /// </summary>
            public static Float3 ZXZ => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(ZXZ)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="X"/>.
            /// </summary>
            public static Float3 ZYX => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(ZYX)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Y"/>.
            /// </summary>
            public static Float3 ZYY => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(ZYY)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Y"/>, <see cref="Z"/>.
            /// </summary>
            public static Float3 ZYZ => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(ZYZ)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="X"/>.
            /// </summary>
            public static Float3 ZZX => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(ZZX)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Y"/>.
            /// </summary>
            public static Float3 ZZY => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(ZZY)}");

            /// <summary>
            /// Gets a <see cref="Float3"/> value with the components <see cref="Z"/>, <see cref="Z"/>, <see cref="Z"/>.
            /// </summary>
            public static Float3 ZZZ => throw new InvalidExecutionContextException($"{typeof(Normalized)}.{nameof(ZZZ)}");
        }
    }
}
