using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Extensions;
using ComputeSharp.SourceGeneration.Helpers;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class D2DPixelShaderDescriptorGenerator
{
    /// <summary>
    /// A helper with all logic to generate the <c>EffectFactory</c> property.
    /// </summary>
    private static class EffectFactory
    {
        /// <summary>
        /// Writes the <c>EffectFactory</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("/// <inheritdoc/>");
            writer.WriteGeneratedAttributes(GeneratorName);
            writer.WriteLine($"static unsafe nint global::ComputeSharp.D2D1.Descriptors.ID2D1PixelShaderDescriptor<{info.Hierarchy.Hierarchy[0].QualifiedName}>.EffectFactory");

            using (writer.WriteBlock())
            {
                writer.WriteLine("[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]");
                writer.WriteLine("get");

                using (writer.WriteBlock())
                {
                    writer.WriteLine($$"""
                        [global::System.Runtime.InteropServices.UnmanagedCallersOnly(CallConvs = [typeof(global::System.Runtime.CompilerServices.CallConvStdcall)])]
                        static int EffectFactory(void** effectImpl)
                        {
                            return global::ComputeSharp.D2D1.Interop.D2D1PixelShaderEffect.CreateEffectUnsafe<{{info.Hierarchy.Hierarchy[0].QualifiedName}}>(effectImpl);
                        }

                        return (nint)(delegate* unmanaged[Stdcall]<void**, int>)&EffectFactory;
                        """, isMultiline: true);
                }
            }
        }
    }
}