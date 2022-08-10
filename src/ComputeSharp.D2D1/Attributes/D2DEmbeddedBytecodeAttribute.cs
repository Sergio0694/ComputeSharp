using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute that indicates that a given D2D1 shader should be precompiled at build time and embedded
/// directly into the containing assembly as static bytecode, to avoid compiling it at runtime.
/// <para>
/// This attribute can be used to annotate shader types as follows:
/// <code>
/// [D2DEmbeddedBytecode(D2D1ShaderProfile.PixelShader50)]
/// struct MyShader : ID2D1PixelShader
/// {
/// }
/// </code>
/// </para>
/// <para>
/// </para>
/// <para>
/// The runtime compilation will automatically be skipped if the shader bytecode is then retrieved using a matching profile,
/// otherwise the usual runtime compilation will be used as fallback (or an exception will be thrown, depending on the API).
/// </para>
/// <para>
/// This attribute can also be added to a whole assembly, and will be used by default if not overridden by a shader type.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Assembly | AttributeTargets.Method, AllowMultiple = false)]
public sealed class D2DEmbeddedBytecodeAttribute : Attribute
{
    /// <summary>
    /// Creates a new <see cref="D2DEmbeddedBytecodeAttribute"/> instance with the specified parameters.
    /// </summary>
    /// <param name="shaderProfile">The target shader profile to use to compile the shader.</param>
    public D2DEmbeddedBytecodeAttribute(D2D1ShaderProfile shaderProfile)
    {
        ShaderProfile = shaderProfile;
    }

    /// <summary>
    /// Gets the number of threads in each thread group for the X axis
    /// </summary>
    public D2D1ShaderProfile ShaderProfile { get; }
}
