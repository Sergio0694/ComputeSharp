using System;
#if !SOURCE_GENERATOR
using ComputeSharp.D2D1Interop.Helpers;
#endif

namespace ComputeSharp.D2D1Interop;

/// <summary>
/// An attribute that indicates that a given D2D1 shader should be precompiled at build time and embedded
/// directly into the containing assembly as static bytecode, to avoid compiling it at runtime.
/// <para>
/// This attribute can be used to annotate shader types as follows:
/// <code>
/// // A compute shader that is dispatched on a target buffer
/// [D2DEmbeddedBytecode(D2D1ShaderProfile.PixelShader50)]
/// struct MyShader : ID2D1PixelShader
/// {
/// }
/// </code>
/// </para>
/// <para>
/// </para>
/// The runtime compilation will automatically be skipped if the shader bytecode is then retrieved using a matching profile,
/// otherwise the usual runtime compilation will be used as fallback (or an exception will be thrown, depending on the API).
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DEmbeddedBytecodeAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="D2DEmbeddedBytecodeAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="shaderProfile">The target shader profile to use to compile the shader.</param>
    public D2DEmbeddedBytecodeAttribute(D2D1ShaderProfile shaderProfile)
    {
#if !SOURCE_GENERATOR
        ShaderProfile = shaderProfile switch
        {
            D2D1ShaderProfile.PixelShader40 or
            D2D1ShaderProfile.PixelShader40Level91 or
            D2D1ShaderProfile.PixelShader40Level93 or
            D2D1ShaderProfile.PixelShader41 or
            D2D1ShaderProfile.PixelShader50 => shaderProfile,
            _ => ThrowHelper.ThrowArgumentException<D2D1ShaderProfile>(nameof(shaderProfile), "Invalid shader profile value.")
        };
#endif
    }

    /// <summary>
    /// Gets the number of threads in each thread group for the X axis
    /// </summary>
    public D2D1ShaderProfile ShaderProfile { get; }
}
