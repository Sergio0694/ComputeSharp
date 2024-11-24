using System;
using System.Diagnostics;
using ComputeSharp.D2D1.Interop;

namespace ComputeSharp.D2D1.Intrinsics;

/// <summary>
/// An attribute indicating the input type associated with a given D2D HLSL intrinsic.
/// </summary>
/// <param name="inputType">The input type for the current instance.</param>
/// <remarks>
/// <para>If this attribute is not present, methods will be considered as supporting all input types.</para>
/// <para>This matches the behavior for <see cref="Core.Intrinsics.HlslIntrinsicNameAttribute"/>, when not present.</para>
/// </remarks>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
[Conditional("SOURCE_GENERATOR")]
internal sealed class HlslD2DIntrinsicInputTypeAttribute(D2D1PixelShaderInputType inputType) : Attribute
{
    /// <summary>
    /// Gets the input type for the current instance.
    /// </summary>
    public D2D1PixelShaderInputType InputType { get; } = inputType;
}