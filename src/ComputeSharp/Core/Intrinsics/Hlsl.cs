using ComputeSharp.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that maps the supported HLSL intrinsic functions that can be used in compute shaders.
    /// </summary>
    public static partial class Hlsl
    {
        /// <summary>
        /// Blocks execution of all threads in a group until all memory accesses have been completed.
        /// </summary>
        /// <remarks>
        /// A memory barrier guarantees that outstanding memory operations have completed.
        /// Threads are synchronized at GroupSync barriers.
        /// This may stall a thread or threads if memory operations are in progress.
        /// </remarks>
        public static void AllMemoryBarrier() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(AllMemoryBarrier)}()");

        /// <summary>
        /// Blocks execution of all threads in a group until all memory accesses have been completed and all threads in the group have reached this call.
        /// </summary>
        /// <remarks>
        /// A memory barrier guarantees that outstanding memory operations have completed.
        /// Threads are synchronized at GroupSync barriers.
        /// This may stall a thread or threads if memory operations are in progress.
        /// </remarks>
        public static void AllMemoryBarrierWithGroupSync() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(AllMemoryBarrierWithGroupSync)}()");

        /// <summary>
        /// Blocks execution of all threads in a group until all device memory accesses have been completed.
        /// </summary>
        public static void DeviceMemoryBarrier() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(DeviceMemoryBarrier)}()");

        /// <summary>
        /// Blocks execution of all threads in a group until all device memory accesses have been completed and all threads in the group have reached this call.
        /// </summary>
        public static void DeviceMemoryBarrierWithGroupSync() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(DeviceMemoryBarrierWithGroupSync)}()");

        /// <summary>
        /// Blocks execution of all threads in a group until all group shared accesses have been completed.
        /// </summary>
        public static void GroupMemoryBarrier() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(GroupMemoryBarrier)}()");

        /// <summary>
        /// Blocks execution of all threads in a group until all group shared accesses have been completed and all threads in the group have reached this call.
        /// </summary>
        public static void GroupMemoryBarrierWithGroupSync() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(GroupMemoryBarrierWithGroupSync)}()");

        /// <summary>
        /// Performs a guaranteed atomic add of a value to a destination.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedAdd(int destination, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAdd)}({typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Performs a guaranteed atomic add of a value to a destination.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedAdd(uint destination, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAdd)}({typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Performs a guaranteed atomic add of a value to a destination.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original input value.</param>
        public static void InterlockedAdd(int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAdd)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Performs a guaranteed atomic add of a value to a destination.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original input value.</param>
        public static void InterlockedAdd(uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAdd)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Performs a guaranteed atomic and of a value to a destination.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedAnd(int destination, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAnd)}({typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Performs a guaranteed atomic and of a value to a destination.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedAnd(uint destination, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAnd)}({typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Performs a guaranteed atomic and of a value to a destination.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original input value.</param>
        public static void InterlockedAnd(int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAnd)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Performs a guaranteed atomic and of a value to a destination.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original input value.</param>
        public static void InterlockedAnd(uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedAnd)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Atomically compares the destination with the comparison value. If they are identical, the destination
        /// is overwritten with the input value. The original value is set to the destination's original value.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="comparison">The comparison value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original value.</param>
        public static void InterlockedCompareExchange(int destination, int comparison, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedCompareExchange)}({typeof(int)}, {typeof(int)}, {typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Atomically compares the destination with the comparison value. If they are identical, the destination
        /// is overwritten with the input value. The original value is set to the destination's original value.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="comparison">The comparison value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original value.</param>
        public static void InterlockedCompareExchange(uint destination, uint comparison, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedCompareExchange)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Atomically compares the destination to the comparison value. If they
        /// are identical, the destination is overwritten with the input value.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="comparison">The comparison value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedCompareStore(int destination, int comparison, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedCompareStore)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Atomically compares the destination to the comparison value. If they
        /// are identical, the destination is overwritten with the input value.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="comparison">The comparison value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedCompareStore(uint destination, uint comparison, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedCompareStore)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Assigns value to dest and returns the original value.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original value.</param>
        public static void InterlockedExchange(int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedExchange)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Assigns value to dest and returns the original value.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original value.</param>
        public static void InterlockedExchange(uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedExchange)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Performs a guaranteed atomic max.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedMax(int destination, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMax)}({typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Performs a guaranteed atomic max.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedMax(uint destination, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMax)}({typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Performs a guaranteed atomic max.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original value.</param>
        public static void InterlockedMax(int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMax)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Performs a guaranteed atomic max.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original value.</param>
        public static void InterlockedMax(uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMax)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Performs a guaranteed atomic min.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedMin(int destination, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMin)}({typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Performs a guaranteed atomic min.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedMin(uint destination, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMin)}({typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Performs a guaranteed atomic min.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original value.</param>
        public static void InterlockedMin(int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMin)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Performs a guaranteed atomic min.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original value.</param>
        public static void InterlockedMin(uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedMin)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Performs a guaranteed atomic or.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedOr(int destination, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedOr)}({typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Performs a guaranteed atomic or.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedOr(uint destination, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedOr)}({typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Performs a guaranteed atomic or.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original value.</param>
        public static void InterlockedOr(int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedOr)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Performs a guaranteed atomic or.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original value.</param>
        public static void InterlockedOr(uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedOr)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Performs a guaranteed atomic xor.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedXor(int destination, int value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedXor)}({typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Performs a guaranteed atomic xor.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        public static void InterlockedXor(uint destination, uint value) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedXor)}({typeof(uint)}, {typeof(uint)})");

        /// <summary>
        /// Performs a guaranteed atomic xor.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original value.</param>
        public static void InterlockedXor(int destination, int value, out int original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedXor)}({typeof(int)}, {typeof(int)}, {typeof(int)})");

        /// <summary>
        /// Performs a guaranteed atomic xor.
        /// </summary>
        /// <param name="destination">The destination value.</param>
        /// <param name="value">The input value.</param>
        /// <param name="original">The original value.</param>
        public static void InterlockedXor(uint destination, uint value, out uint original) => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(InterlockedXor)}({typeof(uint)}, {typeof(uint)}, {typeof(uint)})");
    }
}
