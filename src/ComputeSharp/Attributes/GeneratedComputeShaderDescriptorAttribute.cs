using System;

namespace ComputeSharp;

/// <summary>
/// An attribute that indicates that a given compute shader should have its associated descriptor code being generated.
/// <para>
/// This attribute can be used on shader types that are declared as <see langword="partial"/>:
/// <code>
/// [GeneratedComputeShaderDescriptor]
/// partial struct MyShader : IComputeShader
/// {
/// }
/// </code>
/// </para>
/// <para>
/// The shader type must implement either <see cref="IComputeShader"/> or <see cref="IComputeShader{TPixel}"/>.
/// </para>
/// <para>
/// When a shader is annotated with <see cref="GeneratedComputeShaderDescriptorAttribute"/>, the source generator will also add
/// <see cref="Descriptors.IComputeShaderDescriptor{T}"/> to its implemented interfaces, and implement its APIs automatically.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class GeneratedComputeShaderDescriptorAttribute : Attribute;