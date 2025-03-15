using ComputeSharp.Core.Intrinsics;

#pragma warning disable IDE0022, IDE0060

namespace ComputeSharp;

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
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/allmemorybarrier"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void AllMemoryBarrier() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(AllMemoryBarrier)}()");

    /// <summary>
    /// Blocks execution of all threads in a group until all memory accesses have been completed and all threads in the group have reached this call.
    /// </summary>
    /// <remarks>
    /// A memory barrier guarantees that outstanding memory operations have completed.
    /// Threads are synchronized at GroupSync barriers.
    /// This may stall a thread or threads if memory operations are in progress.
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/allmemorybarrierwithgroupsync"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void AllMemoryBarrierWithGroupSync() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(AllMemoryBarrierWithGroupSync)}()");

    /// <summary>
    /// Blocks execution of all threads in a group until all device memory accesses have been completed.
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/devicememorybarrier"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void DeviceMemoryBarrier() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(DeviceMemoryBarrier)}()");

    /// <summary>
    /// Blocks execution of all threads in a group until all device memory accesses have been completed and all threads in the group have reached this call.
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/devicememorybarrierwithgroupsync"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void DeviceMemoryBarrierWithGroupSync() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(DeviceMemoryBarrierWithGroupSync)}()");

    /// <summary>
    /// Blocks execution of all threads in a group until all group shared accesses have been completed.
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/groupmemorybarrier"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void GroupMemoryBarrier() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(GroupMemoryBarrier)}()");

    /// <summary>
    /// Blocks execution of all threads in a group until all group shared accesses have been completed and all threads in the group have reached this call.
    /// </summary>
    /// <remarks>
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/groupmemorybarrierwithgroupsync"/>.
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    public static void GroupMemoryBarrierWithGroupSync() => throw new InvalidExecutionContextException($"{typeof(Hlsl)}.{nameof(GroupMemoryBarrierWithGroupSync)}()");

    /// <summary>
    /// Returns a lighting coefficient vector.
    /// </summary>
    /// <param name="nDotL">The dot product of the normalized surface normal and the light vector.</param>
    /// <param name="nDotH">The dot product of the half-angle vector and the surface normal.</param>
    /// <param name="m">A specular exponent.</param>
    /// <returns>The lighting coefficient vector.</returns>
    /// <remarks>
    /// This function returns a lighting coefficient vector (ambient, diffuse, specular, 1) where:
    /// <list type="bullet">
    ///     <item>Ambient: <c>1</c>.</item>
    ///     <item>Diffuse: <c>n * l &lt; 0 ? 0 : n * l</c>.</item>
    ///     <item>Specular: <c>n * l &lt; 0 || n * h &lt; 0 ? 0 : (n * h) ^ m</c>.</item>
    /// </list>
    /// Where the vector <c>n</c> is the normal vector, <c>l</c> is the direction to light and <c>h</c> is the half vector.
    /// <para>For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-lit"/>.</para>
    /// <para>This method is an intrinsic and can only be used within a shader on the GPU. Using it on the CPU is undefined behavior.</para>
    /// </remarks>
    [HlslIntrinsicName("lit")]
    public static float Lit(float nDotL, float nDotH, float m) => default;
}