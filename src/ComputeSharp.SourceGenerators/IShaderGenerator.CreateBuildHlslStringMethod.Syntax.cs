using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using ComputeSharp.__Internals;
using ComputeSharp.SourceGenerators.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618, RS1024

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
public sealed partial class IShaderGenerator
{
    /// <inheritdoc/>
    internal static partial class BuildHlslString
    {
        /// <summary>
        /// Creates a <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslString</c> method.
        /// </summary>
        /// <param name="hlslSourceInfo">The input <see cref="HlslShaderSourceInfo"/> instance to use.</param>
        /// <param name="supportsDynamicShaders">Indicates whether or not dynamic shaders are supported.</param>
        /// <returns>The resulting <see cref="MethodDeclarationSyntax"/> instance for the <c>BuildHlslString</c> method.</returns>
        public static MethodDeclarationSyntax GetSyntax(HlslShaderSourceInfo hlslSourceInfo, bool supportsDynamicShaders)
        {
            // Generate the necessary body statements depending on whether dynamic shaders are supported
            ImmutableArray<StatementSyntax> bodyStatements = supportsDynamicShaders
                ? GenerateRenderMethodBody(hlslSourceInfo)
                : GenerateEmptyMethodBody();

            // This code produces a method declaration as follows:
            //
            // readonly void global::ComputeSharp.__Internals.IShader.BuildHlslString(out global::ComputeSharp.__Internals.ArrayPoolStringBuilder builder, int threadsX, int threadsY, int threadsZ)
            // {
            //     <BODY>
            // }
            return
                MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword)),
                    Identifier("BuildHlslString"))
                .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.__Internals.{nameof(IShader)}")))
                .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
                .AddParameterListParameters(
                    Parameter(Identifier("builder")).AddModifiers(Token(SyntaxKind.OutKeyword)).WithType(IdentifierName("global::ComputeSharp.__Internals.ArrayPoolStringBuilder")),
                    Parameter(Identifier("threadsX")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("threadsY")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                    Parameter(Identifier("threadsZ")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))))
                .WithBody(Block(bodyStatements));
        }

        /// <summary>
        /// Produces the series of statements for the empty fallback method.
        /// </summary>
        /// <returns>A series of statements for when dynamic shaders are not supported.</returns>
        private static ImmutableArray<StatementSyntax> GenerateEmptyMethodBody()
        {
            // builder = global::ComputeSharp.__Internals.ArrayPoolStringBuilder.Create(0);
            return
                ImmutableArray.Create<StatementSyntax>(
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("builder"),
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("global::ComputeSharp.__Internals.ArrayPoolStringBuilder"),
                                    IdentifierName("Create")))
                            .AddArgumentListArguments(
                                Argument(LiteralExpression(
                                    SyntaxKind.NumericLiteralExpression,
                                    Literal(0)))))));
        }

        /// <summary>
        /// Produces the series of statements to build the current HLSL source.
        /// </summary>
        /// <param name="hlslSourceInfo">The input <see cref="HlslShaderSourceInfo"/> instance to use.</param>
        /// <returns>The series of statements to build the HLSL source to compile to execute the current shader.</returns>
        private static ImmutableArray<StatementSyntax> GenerateRenderMethodBody(HlslShaderSourceInfo hlslSourceInfo)
        {
            ImmutableArray<StatementSyntax>.Builder statements = ImmutableArray.CreateBuilder<StatementSyntax>();
            StringBuilder textBuilder = new();
            int capturedDelegates = 0;
            int prologueStatements = 0;
            int sizeHint = 64;

            void AppendLF()
            {
                textBuilder.Append('\n');
            }

            void AppendLine(string text)
            {
                textBuilder.Append(text);
            }

            void AppendParsedStatement(string text)
            {
                FlushText();

                statements.Add(ParseStatement(text));
            }

            void FlushText()
            {
                if (textBuilder.Length > 0)
                {
                    string text = textBuilder.ToString();

                    textBuilder.Append(text);

                    sizeHint += textBuilder.Length;

                    statements.Add(
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName("builder"), IdentifierName("Append")))
                                .AddArgumentListArguments(Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(text))))));

                    textBuilder.Clear();
                }
            }

            // Declare the hashsets to track imported members and types from delegates, if needed
            if (!hlslSourceInfo.Delegates.IsEmpty)
            {
                void DeclareMapping(int index, string name, IEnumerable<string> items)
                {
                    // global::System.Collections.Generic.HashSet<string> <NAME> = new();
                    statements.Insert(index,
                        LocalDeclarationStatement(VariableDeclaration(
                        GenericName(Identifier("global::System.Collections.Generic.HashSet"))
                        .AddTypeArgumentListArguments(PredefinedType(Token(SyntaxKind.StringKeyword))))
                        .AddVariables(
                            VariableDeclarator(Identifier(name))
                            .WithInitializer(EqualsValueClause(ImplicitObjectCreationExpression())))));

                    prologueStatements++;

                    // <NAME>.Add("<ITEM>");
                    foreach (var item in items)
                    {
                        statements.Add(
                            ExpressionStatement(
                                InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName(name),
                                        IdentifierName("Add")))
                                .AddArgumentListArguments(Argument(
                                    LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(item))))));

                        prologueStatements++;
                    }
                }

                DeclareMapping(0, "__typeNames", hlslSourceInfo.DefinedTypes);
                DeclareMapping(1, "__constantNames", hlslSourceInfo.DefinedConstants);
                DeclareMapping(2, "__methodNames", hlslSourceInfo.MethodSignatures);

                // Go through all existing delegate fields, if any
                foreach (string fieldName in hlslSourceInfo.Delegates)
                {
                    // global::ComputeSharp.__Internals.ShaderMethodSourceAttribute __<DELEGATE_NAME>Attribute = global::ComputeSharp.__Internals.ShaderMethodSourceAttribute.GetForDelegate(<DELEGATE_NAME>, "<DELEGATE_NAME>");
                    statements.Add(
                        LocalDeclarationStatement(VariableDeclaration(IdentifierName($"global::ComputeSharp.__Internals.{nameof(ShaderMethodSourceAttribute)}"))
                        .AddVariables(
                            VariableDeclarator(Identifier($"__{fieldName}Attribute"))
                            .WithInitializer(
                                EqualsValueClause(
                                    InvocationExpression(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName($"global::ComputeSharp.__Internals.{nameof(ShaderMethodSourceAttribute)}"),
                                            IdentifierName(nameof(ShaderMethodSourceAttribute.GetForDelegate))))
                                    .AddArgumentListArguments(
                                        Argument(IdentifierName(fieldName)),
                                        Argument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(fieldName)))))))));

                    capturedDelegates++;
                    prologueStatements++;
                }
            }

            // Header and thread ids
            AppendLine(hlslSourceInfo.HeaderAndThreadsX);
            AppendParsedStatement("builder.Append(threadsX);");
            AppendLine(hlslSourceInfo.ThreadsY);
            AppendParsedStatement("builder.Append(threadsY);");
            AppendLine(hlslSourceInfo.ThreadsZ);
            AppendParsedStatement("builder.Append(threadsZ);");

            // Define declarations
            AppendLine(hlslSourceInfo.Defines);

            // Defines from captured delegates
            foreach (string fieldName in hlslSourceInfo.Delegates)
            {
                AppendParsedStatement($"__{fieldName}Attribute.AppendConstants(ref builder, __constantNames);");
            }

            // Static fields and declared types
            AppendLine(hlslSourceInfo.StaticFieldsAndDeclaredTypes);

            // Declared types from captured delegates
            foreach (string fieldName in hlslSourceInfo.Delegates)
            {
                AppendParsedStatement($"__{fieldName}Attribute.AppendTypes(ref builder, __typeNames);");
            }

            // Captured variables
            AppendLine(hlslSourceInfo.CapturedFieldsAndResourcesAndForwardDeclarations);

            // Forward declarations from captured delegates
            foreach (string fieldName in hlslSourceInfo.Delegates)
            {
                AppendParsedStatement($"__{fieldName}Attribute.AppendForwardDeclarations(ref builder, __methodNames);");
            }

            // Remove all forward declarations from methods that are embedded into the shader.
            // This is necessary to avoid duplicate definitions from methods from delegates.
            if (capturedDelegates > 0)
            {
                // <NAME>.Add("<ITEM>");
                foreach (string forwardDeclaration in hlslSourceInfo.MethodSignatures)
                {
                    FlushText();

                    statements.Add(
                        ExpressionStatement(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("__methodNames"),
                                    IdentifierName("Remove")))
                            .AddArgumentListArguments(Argument(
                                LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(forwardDeclaration))))));
                }
            }

            // Captured methods
            AppendLine(hlslSourceInfo.CapturedMethods);

            // Captured methods from captured delegates
            foreach (string fieldName in hlslSourceInfo.Delegates)
            {
                AppendParsedStatement($"__{fieldName}Attribute.AppendMethods(ref builder, __methodNames);");
            }

            // Captured delegate methods
            foreach (string fieldName in hlslSourceInfo.Delegates)
            {
                AppendLF();
                AppendParsedStatement($"__{fieldName}Attribute.AppendMappedInvokeMethod(ref builder, \"{fieldName}\");");
                AppendLF();
            }

            // Entry point
            AppendLine(hlslSourceInfo.EntryPoint);

            FlushText();

            // builder = global::ComputeSharp.__Internals.ArrayPoolStringBuilder.Create(<SIZE_HINT>);
            statements.Insert(
                prologueStatements,
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("builder"),
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("global::ComputeSharp.__Internals.ArrayPoolStringBuilder"),
                                IdentifierName("Create")))
                        .AddArgumentListArguments(
                            Argument(LiteralExpression(
                                SyntaxKind.NumericLiteralExpression,
                                Literal(sizeHint)))))));

            return statements.ToImmutable();
        }
    }
}
