using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace ComputeSharp.Core.SourceGenerators;

/// <summary>
/// A source generator to create global using directives for the HLSL primitive types.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class GlobalUsingDirectivesGenerator : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static context =>
        {
            const string filename = "ComputeSharp.Core.SourceGenerators.EmbeddedResources.GlobalUsingDirectives.cs";

            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename);
            using StreamReader reader = new(stream);

            string globalUsingDirectives = reader.ReadToEnd();

            context.AddSource("GlobalUsingDirectives.g.cs", globalUsingDirectives);
        });
    }
}