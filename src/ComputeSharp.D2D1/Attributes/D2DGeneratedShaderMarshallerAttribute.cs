using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute that indicates that a given D2D1 shader should have its associated marshalling code being generated.
/// <para>
/// This attribute can be used on shader types that are declared as <see langword="partial"/>:
/// <code>
/// [D2DGeneratedShaderMarshaller]
/// partial struct MyShader : ID2D1PixelShader
/// {
/// }
/// </code>
/// </para>
/// <para>
/// When a shader is annotated with <see cref="D2DGeneratedShaderMarshallerAttribute"/>, the source generator will also add
/// <see cref="Descriptors.ID2D1PixelShaderDescriptor{T}"/> to its implemented interfaces, and implement its APIs automatically.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DGeneratedShaderMarshallerAttribute : Attribute
{
}