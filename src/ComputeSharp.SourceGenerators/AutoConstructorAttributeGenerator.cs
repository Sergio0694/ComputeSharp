using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ComputeSharp.SourceGenerators
{
    [Generator]
    public class AutoConstructorAttributeGenerator : ISourceGenerator
    {
        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
            string attributeSource = @"
            using System;

            namespace ComputeSharp
            {
                /// <summary>
                /// A shader that indicates that a target shader type should get an automatic constructor for all fields.
                /// </summary>
                [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
                public sealed class AutoConstructorAttribute : Attribute
                {
                }
            }";

            // Add the [AutoConstructor] attribute
            context.AddSource("__ComputeSharp_AutoConstructorAttribute", SourceText.From(attributeSource, Encoding.UTF8));

            // Find all the [AutoConstructor] usages
            ImmutableArray<AttributeSyntax> attributes = (
                from tree in context.Compilation.SyntaxTrees
                from attribute in tree.GetRoot().DescendantNodes().OfType<AttributeSyntax>()
                where attribute.Name.ToString() == "AutoConstructor"
                select attribute).ToImmutableArray();

            foreach (AttributeSyntax attribute in attributes)
            {
                StructDeclarationSyntax structDeclaration = attribute.FirstAncestorOrSelf<StructDeclarationSyntax>()!;
                SemanticModel semanticModel = context.Compilation.GetSemanticModel(structDeclaration.SyntaxTree);
                INamedTypeSymbol structDeclarationSymbol = semanticModel.GetDeclaredSymbol(structDeclaration)!;

                // Extract the info on the type to process
                var namespaceName = structDeclarationSymbol.ContainingNamespace.Name;
                var structName = structDeclaration.Identifier.Text;
                var structModifiers = structDeclaration.Modifiers;
                var fields = (
                    from fieldDeclaration in structDeclaration.Members.OfType<FieldDeclarationSyntax>()
                    let fieldType = fieldDeclaration.Declaration.Type
                    let typeName = semanticModel.GetTypeInfo(fieldType).Type!.ToDisplayString()
                    let fieldFullType = ParseTypeName(typeName)
                    from fieldVariable in fieldDeclaration.Declaration.Variables
                    select (Type: fieldFullType, fieldVariable.Identifier)).ToImmutableArray();

                // Create the constructor declaration for the type. This will
                // produce a constructor with simple initialization of all variables:
                //
                // public MyType(Foo a, Bar b, Baz c, ...)
                // {
                //     this.a = a;
                //     this.b = b;
                //     this.c = c;
                //     ...
                // }
                //
                // This will be placed inside the original namespace, and with all the
                // necessary using directive at the top for the input parameters.
                var source =
                    CompilationUnit().AddMembers(
                    NamespaceDeclaration(IdentifierName(namespaceName)).AddMembers(
                    StructDeclaration(structName).WithModifiers(structModifiers).AddMembers(
                    ConstructorDeclaration(structName)
                    .AddModifiers(Token(SyntaxKind.PublicKeyword))
                    .AddParameterListParameters(fields.Select(field => Parameter(field.Identifier).WithType(field.Type)).ToArray())
                    .AddBodyStatements(fields.Select(field => ParseStatement($"this.{field.Identifier} = {field.Identifier};")).ToArray()))))
                    .NormalizeWhitespace()
                    .ToFullString();

                // Add the partial type
                context.AddSource($"__ComputeSharp_AutoConstructorAttribute_{structDeclarationSymbol.Name}", SourceText.From(source, Encoding.UTF8));
            }
        }
    }
}

