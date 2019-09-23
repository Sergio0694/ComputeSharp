using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Reflection;
using ComputeSharp;
using ComputeSharp.Shaders.Mappings;
using ComputeSharp.Shaders.Translation.Models;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
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
        /// <param name="method">The info on parsed static methods, if any</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that is compatible with HLSL</returns>
        [Pure]
        public static SyntaxNode ReplaceMember(this MemberAccessExpressionSyntax node, SemanticModel semanticModel, out (string Name, ReadableMember MemberInfo)? variable, out (string Name, MethodInfo MethodInfo)? method)
        {
            // Set the out parameters to null, replace them later on if needed
            variable = null;
            method = null;

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

            // If the input member has no symbol, try to load it manually
            if (memberSymbol is null)
            {
                string expression = node.WithoutTrivia().ToFullString();
                int index = expression.LastIndexOf(Type.Delimiter);
                string fullname = expression.Substring(0, index);
                if (!(AppDomain.CurrentDomain.GetAssemblies().Select(a => a.GetType(fullname)).FirstOrDefault(t => t != null) is Type type))
                {
                    // The current node can't possibly represent a field or a property, so just return it
                    return node;
                }

                // Try to get the target static field or property, if present
                string name = expression.Substring(index + 1, expression.Length - fullname.Length - 1);
                MemberInfo[] memberInfos = type.GetMember(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                if (memberInfos.Length == 0) return node;
                bool isReadonly;
                ReadableMember memberInfo;
                switch (memberInfos.First())
                {
                    case FieldInfo fieldInfo:
                        isReadonly = fieldInfo.IsInitOnly;
                        memberInfo = fieldInfo;
                        break;
                    case PropertyInfo propertyInfo:
                        isReadonly = !propertyInfo.CanWrite;
                        memberInfo = propertyInfo;
                        break;
                    case MethodInfo methodInfo:
                        string methodName = $"{methodInfo.DeclaringType.Name}_{methodInfo.Name}";
                        method = (methodName, methodInfo);
                        return SyntaxFactory.IdentifierName(methodName).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
                    default: throw new InvalidOperationException($"Invalid symbol kind: {memberInfos.First().GetType()}");
                }

                // Handle the loaded info
                return ProcessStaticMember(node, memberInfo, isReadonly, ref variable);
            }

            // If the input member is not a field, property or method, just return it
            if (memberSymbol.Kind != SymbolKind.Field &&
                memberSymbol.Kind != SymbolKind.Property &&
                memberSymbol.Kind != SymbolKind.Method)
            {
                return node;
            }

            // Process the input node if it's a known method invocation
            if (HlslKnownMethods.TryGetMappedName(containingMemberSymbolInfo.Symbol, memberSymbol, out string? mappedName))
            {
                string expression = memberSymbol.IsStatic ? mappedName : $"{node.Expression}{mappedName}";
                return SyntaxFactory.IdentifierName(expression).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
            }

            // Handle static members as a special case
            if (memberSymbol.IsStatic && (
                memberSymbol.Kind == SymbolKind.Field ||
                memberSymbol.Kind == SymbolKind.Property ||
                memberSymbol.Kind == SymbolKind.Method))
            {
                // Get the containing type
                string
                    typeFullname = memberSymbol.ContainingType.ToString(),
                    assemblyFullname = memberSymbol.ContainingAssembly.ToString();
                Type memberDeclaringType = Type.GetType($"{typeFullname}, {assemblyFullname}");

                // Static field or property
                if (memberSymbol.Kind == SymbolKind.Field || memberSymbol.Kind == SymbolKind.Property)
                {
                    bool isReadonly;
                    ReadableMember memberInfo;
                    switch (memberSymbol.Kind)
                    {
                        case SymbolKind.Field:
                            FieldInfo fieldInfo = memberDeclaringType.GetField(memberSymbol.Name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                            isReadonly = fieldInfo.IsInitOnly;
                            memberInfo = fieldInfo;
                            break;
                        case SymbolKind.Property:
                            PropertyInfo propertyInfo = memberDeclaringType.GetProperty(memberSymbol.Name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                            isReadonly = !propertyInfo.CanWrite;
                            memberInfo = propertyInfo;
                            break;
                        default: throw new InvalidOperationException($"Invalid symbol kind: {memberSymbol.Kind}");
                    }

                    // Handle the loaded info
                    return ProcessStaticMember(node, memberInfo, isReadonly, ref variable);
                }

                // Static method
                MethodInfo methodInfo = memberDeclaringType.GetMethod(memberSymbol.Name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                string name = $"{methodInfo.DeclaringType.Name}_{methodInfo.Name}";
                method = (name, methodInfo);

                return SyntaxFactory.IdentifierName(name).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
            }

            return node;
        }

        /// <summary>
        /// Checks a <see cref="InvocationExpressionSyntax"/> instance and replaces it to be HLSL compatible, if needed
        /// </summary>
        /// <param name="node">The input <see cref="InvocationExpressionSyntax"/> to check and modify if needed</param>
        /// <param name="declaringType">The <see cref="Type"/> in which the input syntax tree is declared in</param>
        /// <param name="variable">The info on parsed static members, if any</param>
        /// <param name="method">The info on parsed static methods, if any</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that is compatible with HLSL</returns>
        [Pure]
        public static InvocationExpressionSyntax ReplaceInvocation(this InvocationExpressionSyntax node, Type declaringType, out (string Name, ReadableMember MemberInfo)? variable, out (string Name, MethodInfo MethodInfo)? method)
        {
            // Set the variable and method to null
            variable = null;
            method = null;

            // Explore the nested classes from bottom to top
            if (declaringType.TryFindAncestorMember(node.Expression.ToString(), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, out var result))
            {
                // If a static method or delegate is found, track it
                string name = $"{result.Value.DeclaringType.Name}_{result.Value.MemberInfo.Name}";
                switch (result.Value.MemberInfo)
                {
                    case FieldInfo fieldInfo:
                        variable = (name, fieldInfo);
                        break;
                    case PropertyInfo propertyInfo:
                        variable = (name, propertyInfo);
                        break;
                    case MethodInfo methodInfo:
                        method = (name, methodInfo);
                        return SyntaxFactory.InvocationExpression(SyntaxFactory.ParseExpression(name), node.ArgumentList).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
                    default: throw new InvalidOperationException($"Invalid symbol kind: {result.Value.MemberInfo.GetType()}");
                }

                // Return the mapped expression
                return SyntaxFactory.InvocationExpression(SyntaxFactory.ParseExpression(name), node.ArgumentList).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
            }

            return node;
        }

        /// <summary>
        /// Checks a <see cref="IdentifierNameSyntax"/> instance and replaces it to be HLSL compatible, if needed
        /// </summary>
        /// <param name="node">The input <see cref="IdentifierNameSyntax"/> to check and modify if needed</param>
        /// <param name="declaringType">The <see cref="Type"/> in which the input syntax tree is declared in</param>
        /// <param name="variable">The info on parsed static members, if any</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that is compatible with HLSL</returns>
        [Pure]
        public static IdentifierNameSyntax ReplaceIdentifierName(this IdentifierNameSyntax node, Type declaringType, out (string Name, ReadableMember MemberInfo)? variable)
        {
            // Set the variable to null
            variable = null;

            // Explore the nested classes from bottom to top
            if (declaringType.TryFindAncestorMember(node.Identifier.ToString(), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, out var result))
            {
                // If a static scalar or vector member is found, track it
                string name = $"{result.Value.DeclaringType.Name}_{result.Value.MemberInfo.Name}";
                switch (result.Value.MemberInfo)
                {
                    case FieldInfo fieldInfo when HlslKnownTypes.IsKnownType(fieldInfo.FieldType):
                        variable = (name, fieldInfo);
                        break;
                    case PropertyInfo propertyInfo when HlslKnownTypes.IsKnownType(propertyInfo.PropertyType):
                        variable = (name, propertyInfo);
                        break;
                    default: return node;
                }

                // Return the mapped expression
                return SyntaxFactory.IdentifierName(name).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
            }

            return node;
        }

        /// <summary>
        /// Processes a loaded static member, either a field or a property
        /// </summary>
        /// <param name="node">The input <see cref="MemberAccessExpressionSyntax"/> to check and modify if needed</param>
        /// <param name="memberInfo">The wrapped member that needs to be processed</param>
        /// <param name="isReadonly">Indicates whether or not the target member is readonly</param>
        /// <param name="variable">The info on parsed static members, if any</param>
        /// <returns>A <see cref="SyntaxNode"/> instance that is compatible with HLSL</returns>
        private static SyntaxNode ProcessStaticMember(SyntaxNode node, ReadableMember memberInfo, bool isReadonly, ref (string Name, ReadableMember MemberInfo)? variable)
        {
            // Constant replacement if the value is a readonly scalar value
            if (isReadonly && HlslKnownTypes.IsKnownScalarType(memberInfo.MemberType))
            {
                LiteralExpressionSyntax expression = memberInfo.GetValue(null) switch
                {
                    true => SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword)),
                    false => SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword)),
                    Bool b when b => SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.TrueKeyword)),
                    Bool b when !b => SyntaxFactory.LiteralExpression(SyntaxKind.TrueLiteralExpression, SyntaxFactory.Token(SyntaxKind.FalseKeyword)),
                    IFormattable scalar => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.ParseToken(scalar.ToString(null, CultureInfo.InvariantCulture))),
                    _ => throw new InvalidOperationException($"Invalid field of type {memberInfo.MemberType}")
                };
                return expression.WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
            }

            // Captured member, treat it like any other captured variable in the closure
            string name = $"{memberInfo.DeclaringType.Name}_{memberInfo.Name}";
            variable = (name, memberInfo);
            return SyntaxFactory.IdentifierName(name).WithLeadingTrivia(node.GetLeadingTrivia()).WithTrailingTrivia(node.GetTrailingTrivia());
        }
    }
}
