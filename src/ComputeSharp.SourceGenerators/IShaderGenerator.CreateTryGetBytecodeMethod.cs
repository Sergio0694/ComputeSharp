using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using ComputeSharp.__Internals;
using ComputeSharp.Exceptions;
using ComputeSharp.Shaders.Translation;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS0618

namespace ComputeSharp.SourceGenerators;

/// <inheritdoc/>
public sealed partial class IShaderGenerator
{
    /// <summary>
    /// Indicates whether the required <c>dxcompiler.dll</c> and <c>dxil.dll</c> libraries have been loaded.
    /// </summary>
    private static bool areDxcLibrariesLoaded;

    /// <inheritdoc/>
    private static partial MethodDeclarationSyntax CreateTryGetBytecodeMethod(
        GeneratorExecutionContext context,
        StructDeclarationSyntax structDeclaration,
        INamedTypeSymbol structDeclarationSymbol,
        bool isDynamicShader,
        string hlslSource)
    {
        // This code produces a method declaration as follows:
        //
        // readonly int global::ComputeSharp.__Internals.IShader.TryGetBytecode(int threadsX, int threadsY, int threadsZ, out ReadOnlySpan<byte> bytecode)
        // {
        //     <BODY>
        // }
        return
            MethodDeclaration(
                PredefinedType(Token(SyntaxKind.BoolKeyword)),
                Identifier("TryGetBytecode"))
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(IdentifierName($"global::ComputeSharp.__Internals.{nameof(IShader)}")))
            .AddModifiers(Token(SyntaxKind.ReadOnlyKeyword))
            .AddParameterListParameters(
                Parameter(Identifier("threadsX")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                Parameter(Identifier("threadsY")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                Parameter(Identifier("threadsZ")).WithType(PredefinedType(Token(SyntaxKind.IntKeyword))),
                Parameter(Identifier("bytecode")).AddModifiers(Token(SyntaxKind.OutKeyword)).WithType(IdentifierName("global::System.ReadOnlySpan<byte>")))
            .WithBody(GetShaderBytecodeBody(structDeclarationSymbol, isDynamicShader, hlslSource));
    }

    /// <summary>
    /// Gets a <see cref="BlockSyntax"/> instance with the logic to try to get a compiled shader bytecode.
    /// </summary>
    /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
    /// <param name="isDynamicShader">Indicates whether or not the shader is dynamic (ie. captures delegates).</param>
    /// <param name="hlslSource">The generated HLSL source code (ignoring captured delegates, if present).</param>
    /// <returns>The <see cref="BlockSyntax"/> instance to hash the input shader.</returns>
    [Pure]
    private static unsafe BlockSyntax GetShaderBytecodeBody(INamedTypeSymbol structDeclarationSymbol, bool isDynamicShader, string hlslSource)
    {
        AttributeData? attribute = structDeclarationSymbol.GetAttributes().FirstOrDefault(static a => a.AttributeClass is { Name: nameof(EmbeddedBytecodeAttribute) });

        // No embedded shader was requested
        if (attribute is null)
        {
            // This code produces a method declaration as follows:
            //
            // bytecode = default;
            //
            // return false;
            return
                Block(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("bytecode"),
                        LiteralExpression(SyntaxKind.DefaultLiteralExpression, Token(SyntaxKind.DefaultKeyword)))),
                ReturnStatement(
                    LiteralExpression(
                        SyntaxKind.FalseLiteralExpression)));
        }

        // If the current shader is dynamic, emit a diagnostic error
        if (isDynamicShader)
        {
            // TODO
            return Block();
        }

        // Validate the thread number arguments
        if (attribute.ConstructorArguments[0].Value is not int threadsX ||
            attribute.ConstructorArguments[1].Value is not int threadsY ||
            attribute.ConstructorArguments[2].Value is not int threadsZ ||
            threadsX is < 1 or > 1024 ||
            threadsY is < 1 or > 1024 ||
            threadsZ is < 1 or > 64)
        {
            // TODO
            return Block();
        }

        LoadNativeDxcLibraries();

        try
        {
            // Replace the actual thread num values
            hlslSource = hlslSource.Replace("<THREADSX>", threadsX.ToString()).Replace("<THREADSY>", threadsY.ToString()).Replace("<THREADSZ>", threadsZ.ToString());

            // Compile the shader bytecode
            using ComPtr<IDxcBlob> dxcBlobBytecode = ShaderCompiler.Instance.CompileShader(hlslSource.AsSpan());

            byte* buffer = (byte*)dxcBlobBytecode.Get()->GetBufferPointer();
            int length = checked((int)dxcBlobBytecode.Get()->GetBufferSize());
            string bytecode = string.Join(",", new Span<byte>(buffer, length).ToArray().Select(static b => $"0x{b:X2}"));

            // This code produces a method declaration as follows:
            //
            // if (threadsX == <X> && threadsY == <Y> && threadsZ == <Z>)
            // {
            //     bytecode = new byte[] { 0x00, 0x01, ..., 0xFF };
            //
            //     return true;
            // }
            //
            // bytecode = default;
            //
            // return false;
            return
                Block(
                IfStatement(
                    BinaryExpression(
                        SyntaxKind.LogicalAndExpression,
                        BinaryExpression(
                            SyntaxKind.LogicalAndExpression,
                            BinaryExpression(
                                SyntaxKind.EqualsExpression,
                                IdentifierName("threadsX"),
                                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(threadsX))),
                            BinaryExpression(
                                SyntaxKind.EqualsExpression,
                                IdentifierName("threadsY"),
                                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(threadsY)))),
                        BinaryExpression(
                            SyntaxKind.EqualsExpression,
                            IdentifierName("threadsZ"),
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(threadsZ)))),
                    Block(
                        ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                IdentifierName("bytecode"),
                                ParseExpression($"new byte[] {{ {bytecode} }}"))),
                        ReturnStatement(LiteralExpression(SyntaxKind.TrueLiteralExpression)))),
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("bytecode"),
                        LiteralExpression(SyntaxKind.DefaultLiteralExpression, Token(SyntaxKind.DefaultKeyword)))),
                ReturnStatement(LiteralExpression(SyntaxKind.FalseLiteralExpression)));
        }
        catch (Win32Exception)
        {
            // TODO
            return Block();
        }
        catch (HlslCompilationException)
        {
            // TODO
            return Block();
        }
    }

    /// <summary>
    /// Extracts and loads the <c>dxcompiler.dll</c> and <c>dxil.dll</c> libraries.
    /// </summary>
    /// <exception cref="NotSupportedException">Thrown if the CPU architecture is not supported.</exception>
    /// <exception cref="Win32Exception">Thrown if a library fails to load.</exception>
    private static void LoadNativeDxcLibraries()
    {
        // Extracts a specified native library for a given runtime identifier
        static string ExtractLibrary(string rid, string name)
        {
            string sourceFilename = $"ComputeSharp.SourceGenerators.ComputeSharp.Libraries.{rid}.{name}.dll";
            string targetFilename = Path.Combine(Path.GetTempPath(), "ComputeSharp.SourceGenerators", Path.GetRandomFileName(), rid, $"{name}.dll");

            Directory.CreateDirectory(Path.GetDirectoryName(targetFilename));

            using Stream sourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(sourceFilename);

            try
            {
                using Stream destinationStream = File.Open(targetFilename, FileMode.CreateNew, FileAccess.Write);

                sourceStream.CopyTo(destinationStream);
            }
            catch (IOException)
            {
            }

            return targetFilename;
        }

        // Loads a target native library
        static unsafe void LoadLibrary(string filename)
        {
            [DllImport("kernel32", ExactSpelling = true, SetLastError = true)]
            static extern void* LoadLibraryW(ushort* lpLibFileName);

            fixed (char* p = filename)
            {
                if (LoadLibraryW((ushort*)p) is null)
                {
                    int hresult = Marshal.GetLastWin32Error();

                    throw new Win32Exception(hresult, $"Failed to load {Path.GetFileName(filename)}.");
                }
            }
        }

        if (!areDxcLibrariesLoaded)
        {
            string rid = RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.X64 => "x64",
                Architecture.Arm64 => "arm64",
                _ => throw new NotSupportedException("Invalid process architecture")
            };

            LoadLibrary(ExtractLibrary(rid, "dxil"));
            LoadLibrary(ExtractLibrary(rid, "dxcompiler"));
        }

        areDxcLibrariesLoaded = true;
    }
}
