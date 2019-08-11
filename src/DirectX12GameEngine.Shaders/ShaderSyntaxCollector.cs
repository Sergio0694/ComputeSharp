using System;
using System.Linq;
using DirectX12GameEngine.Shaders.Mappings;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DirectX12GameEngine.Shaders
{
    public class ShaderSyntaxCollector : CSharpSyntaxWalker
    {
        private readonly SemanticModel semanticModel;
        private readonly ShaderGenerator shaderGenerator;

        public ShaderSyntaxCollector(ShaderGenerator shaderGenerator, SemanticModel semanticModel)
        {
            this.shaderGenerator = shaderGenerator;
            this.semanticModel = semanticModel;
        }

        public override void VisitParameter(ParameterSyntax node)
        {
            base.VisitParameter(node);

            TypeInfo typeInfo = semanticModel.GetTypeInfo(node.Type);

            string fullTypeName = typeInfo.Type.ToDisplayString(
                new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces))
                + ", " + typeInfo.Type.ContainingAssembly.Identity.ToString();

            shaderGenerator.AddType(Type.GetType(fullTypeName));
        }

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
        {
            base.VisitMemberAccessExpression(node);

            SymbolInfo containingMemberSymbolInfo = semanticModel.GetSymbolInfo(node.Expression);
            SymbolInfo memberSymbolInfo = semanticModel.GetSymbolInfo(node.Name);

            ISymbol? memberSymbol = memberSymbolInfo.Symbol ?? memberSymbolInfo.CandidateSymbols.FirstOrDefault();

            if (memberSymbol is null || (memberSymbol.Kind != SymbolKind.Field && memberSymbol.Kind != SymbolKind.Property && memberSymbol.Kind != SymbolKind.Method))
            {
                return;
            }

            ISymbol containingMemberSymbol = containingMemberSymbolInfo.Symbol;

            if (!HlslKnownMethods.IsKnownMethod(containingMemberSymbol, memberSymbol))
            {
                ISymbol symbol = containingMemberSymbol.IsStatic ? containingMemberSymbol : memberSymbol.ContainingType;

                string fullTypeName = symbol.ToDisplayString(
                    new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces))
                    + ", " + symbol.ContainingAssembly.Identity.ToString();

                shaderGenerator.AddType(Type.GetType(fullTypeName));
            }
        }
    }
}
