using ComputeSharp.SourceGeneration.Helpers;
using ComputeSharp.SourceGeneration.Models;
using ComputeSharp.SourceGenerators.Models;

namespace ComputeSharp.SourceGeneration.SyntaxProcessors;

/// <inheritdoc/>
partial class ConstantBufferSyntaxProcessor
{
    /// <inheritdoc/>
    private static partial bool HasArtificialFields()
    {
        return true;
    }

    /// <inheritdoc/>
    static partial void AppendArtificialFields(IConstantBufferInfo info, IndentedTextWriter writer)
    {
        // The X and Y axes are always present
        writer.WriteLine("""
            /// <summary>The artificial field backing <see cref="DispatchSize.X"/>.</summary>
            [FieldOffset(0)]
            public int __x;

            /// <summary>The artificial field backing <see cref="DispatchSize.Y"/>.</summary>
            [FieldOffset(4)]
            public int __y;
            """, isMultiline: true);

        // The Z axis is only present for non pixel-shader-like compute shaders
        writer.WriteLineIf(!((ShaderInfo)info).IsPixelShaderLike);
        writer.WriteLineIf(
            condition: !((ShaderInfo)info).IsPixelShaderLike,
            """
            /// <summary>The artificial field backing <see cref="DispatchSize.Z"/>.</summary>
            [FieldOffset(8)]
            public int __z;
            """, isMultiline: true);

        // Leave a trailing blank line if the shader has any fields
        writer.WriteLineIf(!info.Fields.IsEmpty);
    }

    /// <inheritdoc/>
    static partial void InitializeArtificialFields(IConstantBufferInfo info, IndentedTextWriter writer)
    {
        // Ignore the artificial fields (they will be set separately)
        writer.WriteLine("Unsafe.SkipInit(out buffer.__x);");
        writer.WriteLine("Unsafe.SkipInit(out buffer.__y);");
        writer.WriteLineIf(!((ShaderInfo)info).IsPixelShaderLike, "Unsafe.SkipInit(out buffer.__z);");
        writer.WriteLineIf(!info.Fields.IsEmpty);
    }
}