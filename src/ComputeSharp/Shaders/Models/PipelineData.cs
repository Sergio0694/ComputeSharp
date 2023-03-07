using ComputeSharp.Interop;

namespace ComputeSharp.Shaders.Models;

/// <summary>
/// A <see langword="class"/> representing a custom pipeline state for a compute operation.
/// </summary>
internal abstract unsafe class PipelineData : ReferenceTrackedObject
{
    protected override abstract void DangerousOnDispose();
}