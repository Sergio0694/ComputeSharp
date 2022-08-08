using Microsoft.CodeAnalysis;

namespace ComputeSharp.D2D1.SourceGenerators.Diagnostics;

#pragma warning disable IDE0090 // Use 'new(...)'

/// <summary>
/// A container for all <see cref="DiagnosticDescriptor"/> instances for errors reported by analyzers in this project.
/// </summary>
internal static class DiagnosticDescriptors
{
    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid shader field.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains a field \"{1}\" of an invalid type {2}"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidShaderField = new DiagnosticDescriptor(
        id: "CMPSD2D0001",
        title: "Invalid shader field",
        messageFormat: "The compute shader of type {0} contains a field \"{1}\" of an invalid type {2}",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A type representing a compute shader contains a field of a type that is not supported in HLSL.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid object creation expression.
    /// <para>
    /// Format: <c>"The type {0} cannot be created in a compute shader"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidObjectCreationExpression = new DiagnosticDescriptor(
        id: "CMPSD2D0002",
        title: "Invalid object creation expression",
        messageFormat: "The type {0} cannot be created in a compute shader (only unmanaged types are supported)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Only unmanaged value type objects can be created in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an anonymous object creation expression.
    /// </summary>
    public static readonly DiagnosticDescriptor AnonymousObjectCreationExpression = new DiagnosticDescriptor(
        id: "CMPSD2D0003",
        title: "Anonymous object creation expression",
        messageFormat: "An anonymous object cannot be created in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "An anonymous object cannot be created in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an async modifier on a method or function.
    /// </summary>
    public static readonly DiagnosticDescriptor AsyncModifierOnMethodOrFunction = new DiagnosticDescriptor(
        id: "CMPSD2D0004",
        title: "Async modifier on method or function",
        messageFormat: "The async modifier cannot be used in methods or functions used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The async modifier cannot be used in methods or functions used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an await expression.
    /// </summary>
    public static readonly DiagnosticDescriptor AwaitExpression = new DiagnosticDescriptor(
        id: "CMPSD2D0005",
        title: "Await expression",
        messageFormat: "The await expression cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The await expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a checked expression.
    /// </summary>
    public static readonly DiagnosticDescriptor CheckedExpression = new DiagnosticDescriptor(
        id: "CMPSD2D0006",
        title: "Checked expression",
        messageFormat: "A checked expression cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A checked expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a checked statement.
    /// </summary>
    public static readonly DiagnosticDescriptor CheckedStatement = new DiagnosticDescriptor(
        id: "CMPSD2D0007",
        title: "Checked statement",
        messageFormat: "A checked statement cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A checked statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a fixed statement.
    /// </summary>
    public static readonly DiagnosticDescriptor FixedStatement = new DiagnosticDescriptor(
        id: "CMPSD2D0008",
        title: "Fixed statement",
        messageFormat: "A fixed statement cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A fixed statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a foreach statement.
    /// </summary>
    public static readonly DiagnosticDescriptor ForEachStatement = new DiagnosticDescriptor(
        id: "CMPSD2D0009",
        title: "Foreach statement",
        messageFormat: "A foreach statement cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A foreach statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a lock statement.
    /// </summary>
    public static readonly DiagnosticDescriptor LockStatement = new DiagnosticDescriptor(
        id: "CMPSD2D0010",
        title: "Foreach statement",
        messageFormat: "A lock statement cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A lock statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a query statement.
    /// </summary>
    public static readonly DiagnosticDescriptor QueryExpression = new DiagnosticDescriptor(
        id: "CMPSD2D0011",
        title: "Foreach statement",
        messageFormat: "A LINQ query expression cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A LINQ query expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a range expression.
    /// </summary>
    public static readonly DiagnosticDescriptor RangeExpression = new DiagnosticDescriptor(
        id: "CMPSD2D0012",
        title: "Range expression",
        messageFormat: "A range expression cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A range expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a recursive pattern.
    /// </summary>
    public static readonly DiagnosticDescriptor RecursivePattern = new DiagnosticDescriptor(
        id: "CMPSD2D0013",
        title: "Recursive pattern",
        messageFormat: "A recursive pattern cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A recursive pattern cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a ref type.
    /// </summary>
    public static readonly DiagnosticDescriptor RefType = new DiagnosticDescriptor(
        id: "CMPSD2D0014",
        title: "Ref type",
        messageFormat: "A compute shader cannot have a ref type declaration",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have a ref type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a relational pattern.
    /// </summary>
    public static readonly DiagnosticDescriptor RelationalPattern = new DiagnosticDescriptor(
        id: "CMPSD2D0015",
        title: "Relational pattern",
        messageFormat: "A relational pattern cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A relational pattern cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a sizeof expression.
    /// </summary>
    public static readonly DiagnosticDescriptor SizeOfExpression = new DiagnosticDescriptor(
        id: "CMPSD2D0016",
        title: "Sizeof expression",
        messageFormat: "A sizeof expression cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A sizeof expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a stackalloc expression.
    /// </summary>
    public static readonly DiagnosticDescriptor StackAllocArrayCreationExpression = new DiagnosticDescriptor(
        id: "CMPSD2D0017",
        title: "Stackalloc expression",
        messageFormat: "A stackalloc expression cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A stackalloc expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a throw expression or statement.
    /// </summary>
    public static readonly DiagnosticDescriptor ThrowExpressionOrStatement = new DiagnosticDescriptor(
        id: "CMPSD2D0018",
        title: "Throw expression or statement",
        messageFormat: "Throw expressions and statements cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Throw expressions and statements cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a try statement.
    /// </summary>
    public static readonly DiagnosticDescriptor TryStatement = new DiagnosticDescriptor(
        id: "CMPSD2D0019",
        title: "Try statement",
        messageFormat: "A try statement cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A try statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a tuple type.
    /// </summary>
    public static readonly DiagnosticDescriptor TupleType = new DiagnosticDescriptor(
        id: "CMPSD2D0020",
        title: "Tuple type",
        messageFormat: "A compute shader cannot have a tuple type declaration",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have a tuple type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a using statement or declaration.
    /// </summary>
    public static readonly DiagnosticDescriptor UsingStatementOrDeclaration = new DiagnosticDescriptor(
        id: "CMPSD2D0021",
        title: "Using statement or declaration",
        messageFormat: "Using statements and declarations cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Using statements and declarations cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a yield statement.
    /// </summary>
    public static readonly DiagnosticDescriptor YieldStatement = new DiagnosticDescriptor(
        id: "CMPSD2D0022",
        title: "Yield statement",
        messageFormat: "A yield statement cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A yield statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid object declaration.
    /// <para>
    /// Format: <c>"A variable of type {0} cannot be declared in a compute shader"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidObjectDeclaration = new DiagnosticDescriptor(
        id: "CMPSD2D0023",
        title: "Invalid object declaration",
        messageFormat: "A variable of type {0} cannot be declared in a compute shader (only unmanaged types are supported)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Only unmanaged value type objects can be declared in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a pointer type.
    /// </summary>
    public static readonly DiagnosticDescriptor PointerType = new DiagnosticDescriptor(
        id: "CMPSD2D0024",
        title: "Pointer type",
        messageFormat: "A compute shader cannot have a pointer type declaration",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have a pointer type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a function pointer type.
    /// </summary>
    public static readonly DiagnosticDescriptor FunctionPointer = new DiagnosticDescriptor(
        id: "CMPSD2D0025",
        title: "Function pointer type",
        messageFormat: "A compute shader cannot have a function pointer type declaration",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have a function pointer type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an unsafe statement.
    /// </summary>
    public static readonly DiagnosticDescriptor UnsafeStatement = new DiagnosticDescriptor(
        id: "CMPSD2D0026",
        title: "Unsafe statement",
        messageFormat: "A compute shader cannot have an unsafe statement",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have an unsafe statement.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an unsafe modifier on a method or function.
    /// </summary>
    public static readonly DiagnosticDescriptor UnsafeModifierOnMethodOrFunction = new DiagnosticDescriptor(
        id: "CMPSD2D0027",
        title: "Unsafe modifier on method or function",
        messageFormat: "The unsafe modifier cannot be used in methods or functions used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The unsafe modifier cannot be used in methods or functions used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a string literal.
    /// </summary>
    public static readonly DiagnosticDescriptor StringLiteralExpression = new DiagnosticDescriptor(
        id: "CMPSD2D0028",
        title: "String literal expression",
        messageFormat: "String literal expressions cannot be used in a compute shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "String literal expressions cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an incorrect matrix swizzling property argument.
    /// </summary>
    public static readonly DiagnosticDescriptor NonConstantMatrixSwizzledIndex = new DiagnosticDescriptor(
        id: "CMPSD2D0029",
        title: "Non constant matrix swizzled property argument",
        messageFormat: "The arguments in a swizzled indexer for a matrix type must be compile-time constants",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The arguments in a swizzled indexer for a matrix type must be compile-time constants.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid shader static readonly field type.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains a static readonly field \"{1}\" of an invalid type {2} (only primitive, vector and matrix types are supported)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidShaderStaticFieldType = new DiagnosticDescriptor(
        id: "CMPSD2D0030",
        title: "Invalid shader static readonly field type",
        messageFormat: "The compute shader of type {0} contains a static readonly field \"{1}\" of an invalid type {2} (only primitive, vector and matrix types are supported)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A type representing a compute shader contains a static readonly field of a type that is not supported.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a property declaration.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} contains an invalid property \"{1}\" declaration (only stateless properties explicitly implementing an interface property can be used)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidPropertyDeclaration = new DiagnosticDescriptor(
        id: "CMPSD2D0031",
        title: "Invalid property declaration",
        messageFormat: "The D2D1 shader of type {0} contains an invalid property \"{1}\" declaration (only stateless properties explicitly implementing an interface property can be used)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Property declarations (except for stateless properties explicitly implementing an interface property) cannot be used in a D2D1 shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader with a root signature that is too large.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} has exceeded the maximum allowed size for captured values"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor ShaderDispatchDataSizeExceeded = new DiagnosticDescriptor(
        id: "CMPSD2D0032",
        title: "Shader dispatch data size exceeded",
        messageFormat: "The D2D1 shader of type {0} has exceeded the maximum allowed size for captured values",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The D2D1 shader of type {0} has exceeded the maximum allowed size for captured values.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an embedded bytecode shader failed due to a Win32 exception.
    /// <para>
    /// Format: <c>"The shader of type {0} failed to compile due to a Win32 exception (HRESULT: {1:X8}, Message: "{2}")"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor EmbeddedBytecodeFailedWithWin32Exception = new DiagnosticDescriptor(
        id: "CMPSD2D0033",
        title: "Embedded bytecode compilation failed due to Win32 exception",
        messageFormat: "The shader of type {0} failed to compile due to a Win32 exception (HRESULT: {1:X8}, Message: \"{2}\")",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The embedded bytecode for a shader failed to be compiled due to a Win32 exception.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an embedded bytecode shader failed due to an HLSL compilation exception.
    /// <para>
    /// Format: <c>"The shader of type {0} failed to compile due to an HLSL compiler error (Message: "{1}")"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor EmbeddedBytecodeFailedWithDxcCompilationException = new DiagnosticDescriptor(
        id: "CMPSD2D0034",
        title: "Embedded bytecode compilation failed due to an HLSL compiler error",
        messageFormat: "The shader of type {0} failed to compile due to an HLSL compiler error (Message: \"{1}\")",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The embedded bytecode for a shader failed to be compiled due to an HLSL compiler error.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a invalid D2D input count value.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using an incorrect value for [D2DInputCount] (the number of inputs must be in the [0, 8] range)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2DInputCount = new DiagnosticDescriptor(
        id: "CMPSD2D0035",
        title: "Invalid D2D1 shader input count",
        messageFormat: "The D2D1 shader of type {0} is using an incorrect value for [D2DInputCount] (the number of inputs must be in the [0, 8] range)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader must have a number of inputs in the [1, 8] range.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for repeated indices for D2D simple indices.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using repeated indices for some of its [D2DInputSimple] attributes"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor RepeatedD2DInputSimpleIndices = new DiagnosticDescriptor(
        id: "CMPSD2D0036",
        title: "Repeated D2D1 shader simple input indices",
        messageFormat: "The D2D1 shader of type {0} is using repeated indices for some of its [D2DInputSimple] attributes",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader must only have unique indices for all of its [D2DInputSimple] attributes.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for repeated indices for D2D complex indices.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using repeated indices for some of its [D2DInputComplex] attributes"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor RepeatedD2DInputComplexIndices = new DiagnosticDescriptor(
        id: "CMPSD2D0037",
        title: "Repeated D2D1 shader complex input indices",
        messageFormat: "The D2D1 shader of type {0} is using repeated indices for some of its [D2DInputComplex] attributes",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader must only have unique indices for all of its [D2DInputComplex] attributes.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for invalid combination of simple and complex indices.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using some indices in common for simple and complex input indices"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidSimpleAndComplexIndicesCombination = new DiagnosticDescriptor(
        id: "CMPSD2D0038",
        title: "Invalid D2D1 shader simple and complex indices combination",
        messageFormat: "The D2D1 shader of type {0} is using some indices in common for simple and complex input indices",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader cannot use the same indices to indicate both simple and complex inputs.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader with an out of range input index (or more).
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using some out of range input indices"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor OutOfRangeInputIndex = new DiagnosticDescriptor(
        id: "CMPSD2D0039",
        title: "Our of range D2D1 shader input indices",
        messageFormat: "The D2D1 shader of type {0} is using some out of range input indices",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader must have all the indices of its simple and complex inputs in the valid range (between 0 and the number of declared inputs).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a method invocation that is not valid from a shader.
    /// <para>
    /// Format: <c>"The method {0} cannot be used in a D2D1 shader (methods need to either be HLSL intrinsics or with source available for analysis)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidMethodCall = new DiagnosticDescriptor(
        id: "CMPSD2D0040",
        title: "Invalid method invocation from a D2D1 shader",
        messageFormat: "The method {0} cannot be used in a D2D1 shader (methods need to either be HLSL intrinsics or with source available for analysis)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader can only invoke methods that are either HLSL intrinsics or with source available for analysis.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid discovered type.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} uses the invalid type {1}"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidDiscoveredType = new DiagnosticDescriptor(
        id: "CMPSD2D0041",
        title: "Invalid discovered type",
        messageFormat: "The D2D1 shader of type {0} uses the invalid type {1} (only some .NET primitives and vector types, HLSL primitive, vector and matrix types, and custom types containing these types can be used)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "D2D1 shaders can only use supported types (some .NET primitives and vector types, HLSL primitive, vector and matrix types, and custom types containing these types can be used).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader with an out of range input description index (or more).
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using some out of range input description indices"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor OutOfRangeInputDescriptionIndex = new DiagnosticDescriptor(
        id: "CMPSD2D0042",
        title: "Our of range D2D1 shader input description indices",
        messageFormat: "The D2D1 shader of type {0} is using some out of range input description indices",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader must have all the indices of its input descriptions in the valid range (between 0 and the number of declared inputs).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for repeated indices for D2D input descriptions.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using repeated indices for some of its [D2DInputDescription] attributes"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor RepeatedD2DInputDescriptionIndices = new DiagnosticDescriptor(
        id: "CMPSD2D0043",
        title: "Repeated D2D1 shader input description indices",
        messageFormat: "The D2D1 shader of type {0} is using repeated indices for some of its [D2DInputDescription] attributes",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader must only have unique indices for all of its [D2DInputDescription] attributes.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for the PackMatrixColumnMajor option being used.
    /// <para>
    /// Format: <c>"The D2D1 shader of type (or assembly) {0} is using the PackMatrixColumnMajor option in its [D2DCompileOptions] attribute"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidPackMatrixColumnMajorOption = new DiagnosticDescriptor(
        id: "CMPSD2D0044",
        title: "Invalid PackMatrixColumnMajor compile option",
        messageFormat: "The D2D1 shader of type (or assembly) {0} is using the PackMatrixColumnMajor option in its [D2DCompileOptions] attribute",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader generated with ComputeSharp.D2D1 (or an assembly) cannot use the PackMatrixColumnMajor option, as that is not compatible with the generated code used to load shader constant buffers.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>[D2DRequiresPosition]</c> attribute is missing.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using D2D1 APIs that require the [D2DRequiresPosition] attribute to be used (that is, D2D1.GetScenePosition() and D2D1.SampleInputAtPosition(int, float2)), but the shader type is not annotated accordingly"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingD2DRequiresPositionAttribute = new DiagnosticDescriptor(
        id: "CMPSD2D0045",
        title: "Missing [D2DRequiresPosition] attribute",
        messageFormat: "The D2D1 shader of type {0} is using D2D1 APIs that require the [D2DRequiresPosition] attribute to be used (that is, D2D.GetScenePosition() and D2D.SampleInputAtPosition(int, float2)), but the shader type is not annotated accordingly",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader using functionality that needs position info (ie. when [D2DRequiresPosition] is used, which is mandatory when calling either D2D.GetScenePosition() or D2D.SampleInputAtPosition(int, float2)) needs to be annotated accordingly.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");
}
