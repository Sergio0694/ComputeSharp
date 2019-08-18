using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Reflection;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.Shaders.Translation.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputeSharp.Shaders.Extensions
{
    /// <summary>
    /// A <see langword="class"/> with some extension methods for C# syntax nodes
    /// </summary>
    internal static class SyntaxNodeExtensions
    {
        /// <summary>
        /// Checks a <see cref="SyntaxNode"/> value and replaces the value type to be HLSL compatible, if needed
        /// </summary>
        /// <typeparam name="TRoot">The type of the input <see cref="SyntaxNode"/> instance</typeparam>
        /// <param name="node">The input <see cref="SyntaxNode"/> to check and modify if needed</param>
        /// <param name="type">The <see cref="TypeSyntax"/> to use for the input node</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that represents a type compatible with HLSL</returns>
        [Pure]
        public static TRoot ReplaceType<TRoot>(this TRoot node, TypeSyntax type) where TRoot : SyntaxNode
        {
            string value = HlslKnownTypes.GetMappedName(type.ToString());

            // If the HLSL mapped full type name equals the original type, just return the input node
            if (value == type.ToString()) return node;

            // Process and return the type name
            TypeSyntax newType = SyntaxFactory.ParseTypeName(value).WithLeadingTrivia(type.GetLeadingTrivia()).WithTrailingTrivia(type.GetTrailingTrivia());
            return node.ReplaceNode(type, newType);
        }

        /// <summary>
        /// Checks a <see cref="MemberAccessExpressionSyntax"/> instance and replaces it to be HLSL compatible, if needed
        /// </summary>
        /// <param name="node">The input <see cref="MemberAccessExpressionSyntax"/> to check and modify if needed</param>
        /// <param name="semanticModel">The <see cref="SemanticModel"/> to use to load symbols for the input node</param>
        /// <param name="variable">The info on parsed static members, if any</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that is compatible with HLSL</returns>
        [Pure]
        public static SyntaxNode ReplaceMember(this MemberAccessExpressionSyntax node, SemanticModel semanticModel, out (string Name, ReadableMember MemberInfo)? variable)
        {
            // Set the variable to null, replace it later on if needed
            variable = null;

            SymbolInfo containingMemberSymbolInfo;
            ISymbol? memberSymbol;
            try
            {
                containingMemberSymbolInfo = semanticModel.GetSymbolInfo(node.Expression);
                SymbolInfo memberSymbolInfo = semanticModel.GetSymbolInfo(node.Name);
                memberSymbol = memberSymbolInfo.Symbol ?? memberSymbolInfo.CandidateSymbols.FirstOrDefault();
            }
            catch (ArgumentException)
            {
                // Member access on a captured HLSL-compatible field or property
                string name = node.Name.ToFullString();
                if (name == "X" || name == "Y" || name == "Z" || name == "W") return node.WithName(SyntaxFactory.IdentifierName(name.ToLowerInvariant()));
                return node;
            }

            // If the input member has no symbol or is not a field, property or method, just return it
            if (memberSymbol is null ||
                memberSymbol.Kind != SymbolKind.Field &&
                memberSymbol.Kind != SymbolKind.Property &&
                memberSymbol.Kind != SymbolKind.Method)
            {
                return node;
            }

            // Handle static fields as a special case
            if (memberSymbol.IsStatic && (
                memberSymbol.Kind == SymbolKind.Field ||
                memberSymbol.Kind == SymbolKind.Property))
            {
                // Get the containing type
                string
                    typeFullname = memberSymbol.ContainingType.ToString(),
                    assemblyFullname = memberSymbol.ContainingAssembly.ToString();
                Type fieldDeclaringType = Type.GetType($"{typeFullname}, {assemblyFullname}");

                // Retrieve the field or property info
                bool isReadonly;
                ReadableMember memberInfo;
                switch (memberSymbol.Kind)
                {
                    case SymbolKind.Field:
                        FieldInfo fieldInfo = fieldDeclaringType.GetField(memberSymbol.Name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                        isReadonly = fieldInfo.IsInitOnly;
                        memberInfo = fieldInfo;
                        break;
                    case SymbolKind.Property:
                        PropertyInfo propertyInfo = fieldDeclaringType.GetProperty(memberSymbol.Name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                        isReadonly = !propertyInfo.CanWrite;
                        memberInfo = propertyInfo;
                        break;
                    default: throw new InvalidOperationException($"Invalid symbol kind: {memberSymbol.Kind}");
                }

                // Constant replacement if the value is a readonly scalar value
                if (isReadonly && HlslKnownTypes.IsKnownScalarType(memberInfo.MemberType))
                {
                    return memberInfo.GetValue(null) switch
                    {
                        true => SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword)),
                        false => SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword)),
                        IFormattable scalar => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.ParseToken(scalar.ToString(null, CultureInfo.InvariantCulture))),
                        _ => throw new InvalidOperationException($"Invalid field of type {memberInfo.MemberType}")
                    };
                }

                // Captured member, treat it like any other captured variable in the closure
                string name = $"{containingMemberSymbolInfo.Symbol.Name}_{memberInfo.Name}";
                variable = (name, memberInfo);
                return SyntaxFactory.IdentifierName(name);
            }

            // Process the input node if it's a known method invocation
            if (HlslKnownMethods.TryGetMappedName(containingMemberSymbolInfo.Symbol, memberSymbol) is string mappedName)
            {
                return SyntaxFactory.IdentifierName(mappedName).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
            }

            return node;
        }
    }
}
