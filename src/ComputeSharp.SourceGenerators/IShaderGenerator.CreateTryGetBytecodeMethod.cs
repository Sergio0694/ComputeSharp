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
        string hlslSource,
        out string? bytecodeLiterals)
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
            .WithBody(GetShaderBytecodeBody(structDeclarationSymbol, isDynamicShader, hlslSource, out bytecodeLiterals));
    }

    /// <summary>
    /// Gets a <see cref="BlockSyntax"/> instance with the logic to try to get a compiled shader bytecode.
    /// </summary>
    /// <param name="structDeclarationSymbol">The input <see cref="INamedTypeSymbol"/> instance to process.</param>
    /// <param name="isDynamicShader">Indicates whether or not the shader is dynamic (ie. captures delegates).</param>
    /// <param name="hlslSource">The generated HLSL source code (ignoring captured delegates, if present).</param>
    /// <param name="bytecodeLiterals">The resulting bytecode literals to insert into the final source code.</param>
    /// <returns>The <see cref="BlockSyntax"/> instance to hash the input shader.</returns>
    [Pure]
    private static unsafe BlockSyntax GetShaderBytecodeBody(INamedTypeSymbol structDeclarationSymbol, bool isDynamicShader, string hlslSource, out string? bytecodeLiterals)
    {
        AttributeData? attribute = structDeclarationSymbol.GetAttributes().FirstOrDefault(static a => a.AttributeClass is { Name: nameof(EmbeddedBytecodeAttribute) });

        // No embedded shader was requested
        if (attribute is null)
        {
            bytecodeLiterals = null;

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
            bytecodeLiterals = null;

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
            bytecodeLiterals = null;

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

            // Build and set the text with the literal expressions
            bytecodeLiterals = BuildShaderBytecodeExpressionString(new ReadOnlySpan<byte>(buffer, length));

            // This code produces a method declaration as follows:
            //
            // if (threadsX == <X> && threadsY == <Y> && threadsZ == <Z>)
            // {
            //     bytecode = new byte[] { __EMBEDDED_SHADER_BYTECODE };
            //
            //     return true;
            // }
            //
            // bytecode = default;
            //
            // return false;
            //
            // Note that the __EMBEDDED_SHADER_BYTECODE identifier will be replaced after the string
            // is created by the generator. This greatly speeds up the code generation, as it avoids
            // having to create/parse tens of thousands of literal expression nodes for every shader.
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
                                ArrayCreationExpression(
                                    ArrayType(PredefinedType(Token(SyntaxKind.ByteKeyword)))
                                    .AddRankSpecifiers(ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(OmittedArraySizeExpression()))))
                                .WithInitializer(
                                    InitializerExpression(
                                        SyntaxKind.ArrayInitializerExpression,
                                        SingletonSeparatedList<ExpressionSyntax>(IdentifierName("__EMBEDDED_SHADER_BYTECODE")))))),
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
            bytecodeLiterals = null;

            // TODO
            return Block();
        }
        catch (HlslCompilationException)
        {
            bytecodeLiterals = null;

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

    /// <summary>
    /// A mapping of precomputed literals for all <see cref="byte"/> values.
    /// </summary>
    private static readonly string[] formattedBytes =
    {
        "0x00", "0x01", "0x02", "0x03", "0x04", "0x05", "0x06", "0x07", "0x08", "0x09", "0x0A", "0x0B", "0x0C", "0x0D", "0x0E", "0x0F",
        "0x10", "0x11", "0x12", "0x13", "0x14", "0x15", "0x16", "0x17", "0x18", "0x19", "0x1A", "0x1B", "0x1C", "0x1D", "0x1E", "0x1F",
        "0x20", "0x21", "0x22", "0x23", "0x24", "0x25", "0x26", "0x27", "0x28", "0x29", "0x2A", "0x2B", "0x2C", "0x2D", "0x2E", "0x2F",
        "0x30", "0x31", "0x32", "0x33", "0x34", "0x35", "0x36", "0x37", "0x38", "0x39", "0x3A", "0x3B", "0x3C", "0x3D", "0x3E", "0x3F",
        "0x40", "0x41", "0x42", "0x43", "0x44", "0x45", "0x46", "0x47", "0x48", "0x49", "0x4A", "0x4B", "0x4C", "0x4D", "0x4E", "0x4F",
        "0x50", "0x51", "0x52", "0x53", "0x54", "0x55", "0x56", "0x57", "0x58", "0x59", "0x5A", "0x5B", "0x5C", "0x5D", "0x5E", "0x5F",
        "0x60", "0x61", "0x62", "0x63", "0x64", "0x65", "0x66", "0x67", "0x68", "0x69", "0x6A", "0x6B", "0x6C", "0x6D", "0x6E", "0x6F",
        "0x70", "0x71", "0x72", "0x73", "0x74", "0x75", "0x76", "0x77", "0x78", "0x79", "0x7A", "0x7B", "0x7C", "0x7D", "0x7E", "0x7F",
        "0x80", "0x81", "0x82", "0x83", "0x84", "0x85", "0x86", "0x87", "0x88", "0x89", "0x8A", "0x8B", "0x8C", "0x8D", "0x8E", "0x8F",
        "0x90", "0x91", "0x92", "0x93", "0x94", "0x95", "0x96", "0x97", "0x98", "0x99", "0x9A", "0x9B", "0x9C", "0x9D", "0x9E", "0x9F",
        "0xA0", "0xA1", "0xA2", "0xA3", "0xA4", "0xA5", "0xA6", "0xA7", "0xA8", "0xA9", "0xAA", "0xAB", "0xAC", "0xAD", "0xAE", "0xAF",
        "0xB0", "0xB1", "0xB2", "0xB3", "0xB4", "0xB5", "0xB6", "0xB7", "0xB8", "0xB9", "0xBA", "0xBB", "0xBC", "0xBD", "0xBE", "0xBF",
        "0xC0", "0xC1", "0xC2", "0xC3", "0xC4", "0xC5", "0xC6", "0xC7", "0xC8", "0xC9", "0xCA", "0xCB", "0xCC", "0xCD", "0xCE", "0xCF",
        "0xD0", "0xD1", "0xD2", "0xD3", "0xD4", "0xD5", "0xD6", "0xD7", "0xD8", "0xD9", "0xDA", "0xDB", "0xDC", "0xDD", "0xDE", "0xDF",
        "0xE0", "0xE1", "0xE2", "0xE3", "0xE4", "0xE5", "0xE6", "0xE7", "0xE8", "0xE9", "0xEA", "0xEB", "0xEC", "0xED", "0xEE", "0xEF",
        "0xF0", "0xF1", "0xF2", "0xF3", "0xF4", "0xF5", "0xF6", "0xF7", "0xF8", "0xF9", "0xFA", "0xFB", "0xFC", "0xFD", "0xFE", "0xFF"
    };

    /// <summary>
    /// Creates a formatted expression to initialize a <see cref="ReadOnlySpan{T}"/> with a given shader data.
    /// </summary>
    /// <param name="bytecode">The input shader bytecode to serialize.</param>
    /// <returns>A formatted <see cref="string"/> with the serialized data.</returns>
    private static string BuildShaderBytecodeExpressionString(ReadOnlySpan<byte> bytecode)
    {
        //The estimation is 4 characters per byte (up to "255" in hex), plus ", " to separate sequential values
        using ArrayPoolStringBuilder builder = ArrayPoolStringBuilder.Create(bytecode.Length * 6);

        builder.Append(formattedBytes[bytecode[0]]);

        foreach (byte b in bytecode.Slice(1))
        {
            builder.Append(", ");
            builder.Append(formattedBytes[b]);
        }

        return builder.WrittenSpan.ToString();
    }
}
