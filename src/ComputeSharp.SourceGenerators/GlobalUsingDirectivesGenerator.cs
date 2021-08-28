using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace ComputeSharp.SourceGenerators
{
    /// <summary>
    /// A source generator to create global using directives for the HLSL primitive types.
    /// </summary>
    [Generator]
    public sealed class GlobalUsingDirectivesGenerator : ISourceGenerator
    {
        /// <inheritdoc/>
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForPostInitialization(static context =>
            {
                const string filename = "ComputeSharp.SourceGenerators.EmbeddedResources.GlobalUsingDirectives.cs";

                using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename);
                using StreamReader reader = new(stream);

                string globalUsingDirectives = reader.ReadToEnd();

                context.AddSource("GlobalUsingDirectives.cs", SourceText.From(globalUsingDirectives, Encoding.UTF8));
            });
        }

        /// <inheritdoc/>
        public void Execute(GeneratorExecutionContext context)
        {
        }
    }
}

