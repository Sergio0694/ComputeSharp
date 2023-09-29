using System;
using ComputeSharp.D2D1.SourceGenerators.Models;
using ComputeSharp.SourceGeneration.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.D2D1.SourceGenerators;

/// <inheritdoc/>
partial class ID2D1ShaderGenerator
{
    /// <inheritoc/>
    private static partial class InputDescriptions
    {
        /// <summary>
        /// Writes the <c>InputDescriptions</c> property.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="writer">The <see cref="IndentedTextWriter"/> instance to write into.</param>
        public static void WriteSyntax(D2D1ShaderInfo info, IndentedTextWriter writer)
        {
            writer.WriteLine("readonly global::System.ReadOnlyMemory<global::ComputeSharp.D2D1.Interop.D2D1InputDescription> global::ComputeSharp.D2D1.__Internals.ID2D1Shader.InputDescriptions => ");

            // If there are no input descriptions, just return a default expression.
            // Otherwise, return the shared array with cached input descriptions.
            if (info.InputDescriptions.IsEmpty)
            {
                writer.WriteLine("default;");
            }
            else
            {
                writer.WriteLine("Data.InputDescriptions;");
            }
        }

        /// <summary>
        /// Registers a callback to generate an additional data member, if needed.
        /// </summary>
        /// <param name="info">The input <see cref="D2D1ShaderInfo"/> instance with gathered shader info.</param>
        /// <param name="callbacks">The registered callbacks to generate additional data members.</param>
        public static void RegisterAdditionalDataMemberSyntax(D2D1ShaderInfo info, ImmutableArrayBuilder<IndentedTextWriter.Callback<D2D1ShaderInfo>> callbacks)
        {
            // If there are no input descriptions, there is nothing to di
            if (info.ResourceTextureDescriptions.IsEmpty)
            {
                return;
            }

            // Declare the shared array with input descriptions
            static void Callback(D2D1ShaderInfo info, IndentedTextWriter writer)
            {
                writer.WriteLine("""/// <summary>The singleton <see cref="global::ComputeSharp.D2D1.Interop.D2D1InputDescription"/> array instance.</summary>""");
                writer.WriteLine("""public static readonly global::ComputeSharp.D2D1.Interop.D2D1InputDescription[] InputDescriptions = """);

                // Initialize all input descriptions
                using (writer.WriteBlock())
                {
                    for (int i = 0; i < info.InputDescriptions.Length; i++)
                    {
                        InputDescription description = info.InputDescriptions[i];

                        writer.Write($$"""new global::ComputeSharp.D2D1.Interop.D2D1InputDescription({description.Index}, {description.Filter}) { LevelOfDetailCount = {{description.LevelOfDetailCount}} }""");

                        if (i < info.InputDescriptions.Length - 1)
                        {
                            writer.WriteLine(",");
                        }
                    }
                }
            }

            callbacks.Add(Callback);
        }

        /// <summary>
        /// Gets any type declarations for additional members.
        /// </summary>
        /// <param name="memberDeclarations">The additional members that are needed.</param>
        /// <returns>Any type declarations for additional members.</returns>
        public static TypeDeclarationSyntax[] GetDataTypeDeclarations(MemberDeclarationSyntax[] memberDeclarations)
        {
            if (memberDeclarations.Length == 0)
            {
                return Array.Empty<TypeDeclarationSyntax>();
            }

            // Create the container type declaration:
            //
            // /// <summary>
            // /// A container type for additional data needed by the shader.
            // /// </summary>
            // [global::System.CodeDom.Compiler.GeneratedCode("...", "...")]
            // [global::System.Diagnostics.DebuggerNonUserCode]
            // [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
            // file static class Data
            // {
            //     <FIELD_DECLARATION>
            // }
            TypeDeclarationSyntax dataTypeDeclaration =
                ClassDeclaration("Data")
                .AddModifiers(Token(SyntaxKind.FileKeyword), Token(SyntaxKind.StaticKeyword))
                .AddAttributeLists(
                    AttributeList(SingletonSeparatedList(
                        Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode")).AddArgumentListArguments(
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).FullName))),
                            AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(ID2D1ShaderGenerator).Assembly.GetName().Version.ToString())))))),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.DebuggerNonUserCode")))),
                    AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage")))))
                .AddMembers(memberDeclarations)
                .WithLeadingTrivia(
                    Comment("/// <summary>"),
                    Comment("/// A container type for input descriptions."),
                    Comment("/// </summary>"));

            return new[] { dataTypeDeclaration };
        }
    }
}