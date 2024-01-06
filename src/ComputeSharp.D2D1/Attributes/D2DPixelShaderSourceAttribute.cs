using System;
using System.Diagnostics.CodeAnalysis;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute that indicates that a given D2D1 shader should be precompiled at build time and embedded
/// directly into the containing assembly as static bytecode, to be retrieved as a <see cref="ReadOnlySpan{T}"/>.
/// <para>
/// This attribute can be used to annotate methods as follows:
/// <code>
/// [D2DPixelShaderSource("""
///     #define D2D_INPUT_COUNT 1
///     #define D2D_INPUT0_SIMPLE
///
///     #include ""d2d1effecthelpers.hlsli""
///
///     D2D_PS_ENTRY(PSMain)
///     {
///         float4 color = D2DGetInput(0);
///         float3 rgb = saturate(1.0 - color.rgb);
///         return float4(rgb, 1);
///     }
///     """)]
/// static partial ReadOnlySpan&lt;byte&gt; GetBytecode();
/// </code>
/// </para>
/// <para>
/// </para>
/// <para>
/// Methods can also be annotated with <see cref="D2DShaderProfileAttribute"/> and <see cref="D2DCompileOptionsAttribute"/>
/// to further customize the shader profile to use when compiling the HLSL source, and the compile options to use.
/// </para>
/// </summary>
/// <param name="hlslSource">The number of texture inputs for the shader.</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class D2DPixelShaderSourceAttribute([StringSyntax("Hlsl")] string hlslSource) : Attribute
{
    /// <summary>
    /// Gets the number of texture inputs for the shader.
    /// </summary>
    public string HlslSource { get; } = hlslSource;
}