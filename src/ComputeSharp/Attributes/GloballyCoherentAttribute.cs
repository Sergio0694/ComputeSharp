using System;

namespace ComputeSharp;

/// <summary>
/// An attribute that indicates that a <see cref="ReadWriteBuffer{T}"/> field in
/// a shader type should use the <see langword="globallycoherent"/> modifier.
/// </summary>
/// <remarks>
/// <para>
/// The <see langword="globallycoherent"/> modifier causes memory barriers and syncs to
/// flush data across the entire GPU such that other groups can see writes. Without this
/// modifier, a memory barrier or sync will only flush a UAV within the current thread group.
/// </para>
/// <para>
/// In other words, if compute shader thread in a given thread group needs to perform loads of
/// data that was written by atomics or stores in another thread group, the UAV slot where the
/// data resides must be tagged as <see langword="globallycoherent"/>, so the implementation
/// can ignore the local cache. Otherwise, this form of cross-thread group data sharing will
/// produce undefined results.
/// </para>
/// </remarks>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class GloballyCoherentAttribute : Attribute;