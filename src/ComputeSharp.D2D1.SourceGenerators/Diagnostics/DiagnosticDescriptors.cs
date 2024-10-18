using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Diagnostics;

/// <inheritdoc/>
partial class DiagnosticDescriptors
{
    /// <summary>
    /// The diagnostic id for <see cref="MissingPixelShaderDescriptorOnPixelShaderType"/>.
    /// </summary>
    public const string MissingPixelShaderDescriptorOnPixelShaderTypeId = "CMPSD2D0065";

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid shader field.
    /// <para>
    /// Format: <c>"The pixel shader of type {0} contains a field "{1}" of an invalid type {2}"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidShaderField = new(
        id: "CMPSD2D0001",
        title: "Invalid shader field",
        messageFormat: """The pixel shader of type {0} contains a field "{1}" of an invalid type {2}""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A type representing a pixel shader contains a field of a type that is not supported in HLSL.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid object creation expression.
    /// <para>
    /// Format: <c>"The type {0} cannot be created in a pixel shader"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidObjectCreationExpression = new(
        id: "CMPSD2D0002",
        title: "Invalid object creation expression",
        messageFormat: "The type {0} cannot be created in a pixel shader (only unmanaged types are supported)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Only unmanaged value type objects can be created in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an anonymous object creation expression.
    /// </summary>
    public static readonly DiagnosticDescriptor AnonymousObjectCreationExpression = new(
        id: "CMPSD2D0003",
        title: "Anonymous object creation expression",
        messageFormat: "An anonymous object cannot be created in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "An anonymous object cannot be created in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an async modifier on a method or function.
    /// </summary>
    public static readonly DiagnosticDescriptor AsyncModifierOnMethodOrFunction = new(
        id: "CMPSD2D0004",
        title: "Async modifier on method or function",
        messageFormat: "The async modifier cannot be used in methods or functions used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The async modifier cannot be used in methods or functions used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an await expression.
    /// </summary>
    public static readonly DiagnosticDescriptor AwaitExpression = new(
        id: "CMPSD2D0005",
        title: "Await expression",
        messageFormat: "The await expression cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The await expression cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a checked expression.
    /// </summary>
    public static readonly DiagnosticDescriptor CheckedExpression = new(
        id: "CMPSD2D0006",
        title: "Checked expression",
        messageFormat: "A checked expression cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A checked expression cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a checked statement.
    /// </summary>
    public static readonly DiagnosticDescriptor CheckedStatement = new(
        id: "CMPSD2D0007",
        title: "Checked statement",
        messageFormat: "A checked statement cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A checked statement cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a fixed statement.
    /// </summary>
    public static readonly DiagnosticDescriptor FixedStatement = new(
        id: "CMPSD2D0008",
        title: "Fixed statement",
        messageFormat: "A fixed statement cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A fixed statement cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a foreach statement.
    /// </summary>
    public static readonly DiagnosticDescriptor ForEachStatement = new(
        id: "CMPSD2D0009",
        title: "Foreach statement",
        messageFormat: "A foreach statement cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A foreach statement cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a lock statement.
    /// </summary>
    public static readonly DiagnosticDescriptor LockStatement = new(
        id: "CMPSD2D0010",
        title: "Foreach statement",
        messageFormat: "A lock statement cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A lock statement cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a query statement.
    /// </summary>
    public static readonly DiagnosticDescriptor QueryExpression = new(
        id: "CMPSD2D0011",
        title: "Foreach statement",
        messageFormat: "A LINQ query expression cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A LINQ query expression cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a range expression.
    /// </summary>
    public static readonly DiagnosticDescriptor RangeExpression = new(
        id: "CMPSD2D0012",
        title: "Range expression",
        messageFormat: "A range expression cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A range expression cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a recursive pattern.
    /// </summary>
    public static readonly DiagnosticDescriptor RecursivePattern = new(
        id: "CMPSD2D0013",
        title: "Recursive pattern",
        messageFormat: "A recursive pattern cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A recursive pattern cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a ref type.
    /// </summary>
    public static readonly DiagnosticDescriptor RefType = new(
        id: "CMPSD2D0014",
        title: "Ref type",
        messageFormat: "A pixel shader cannot have a ref type declaration",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A pixel shader cannot have a ref type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a relational pattern.
    /// </summary>
    public static readonly DiagnosticDescriptor RelationalPattern = new(
        id: "CMPSD2D0015",
        title: "Relational pattern",
        messageFormat: "A relational pattern cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A relational pattern cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a sizeof expression.
    /// </summary>
    public static readonly DiagnosticDescriptor SizeOfExpression = new(
        id: "CMPSD2D0016",
        title: "Sizeof expression",
        messageFormat: "A sizeof expression cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A sizeof expression cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a stackalloc expression.
    /// </summary>
    public static readonly DiagnosticDescriptor StackAllocArrayCreationExpression = new(
        id: "CMPSD2D0017",
        title: "Stackalloc expression",
        messageFormat: "A stackalloc expression cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A stackalloc expression cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a throw expression or statement.
    /// </summary>
    public static readonly DiagnosticDescriptor ThrowExpressionOrStatement = new(
        id: "CMPSD2D0018",
        title: "Throw expression or statement",
        messageFormat: "Throw expressions and statements cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Throw expressions and statements cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a try statement.
    /// </summary>
    public static readonly DiagnosticDescriptor TryStatement = new(
        id: "CMPSD2D0019",
        title: "Try statement",
        messageFormat: "A try statement cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A try statement cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a tuple type.
    /// </summary>
    public static readonly DiagnosticDescriptor TupleType = new(
        id: "CMPSD2D0020",
        title: "Tuple type",
        messageFormat: "A pixel shader cannot have a tuple type declaration",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A pixel shader cannot have a tuple type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a using statement or declaration.
    /// </summary>
    public static readonly DiagnosticDescriptor UsingStatementOrDeclaration = new(
        id: "CMPSD2D0021",
        title: "Using statement or declaration",
        messageFormat: "Using statements and declarations cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Using statements and declarations cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a yield statement.
    /// </summary>
    public static readonly DiagnosticDescriptor YieldStatement = new(
        id: "CMPSD2D0022",
        title: "Yield statement",
        messageFormat: "A yield statement cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A yield statement cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid object declaration.
    /// <para>
    /// Format: <c>"A variable of type {0} cannot be declared in a pixel shader"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidObjectDeclaration = new(
        id: "CMPSD2D0023",
        title: "Invalid object declaration",
        messageFormat: "A variable of type {0} cannot be declared in a pixel shader (only unmanaged types are supported)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Only unmanaged value type objects can be declared in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a pointer type.
    /// </summary>
    public static readonly DiagnosticDescriptor PointerType = new(
        id: "CMPSD2D0024",
        title: "Pointer type",
        messageFormat: "A pixel shader cannot have a pointer type declaration",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A pixel shader cannot have a pointer type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a function pointer type.
    /// </summary>
    public static readonly DiagnosticDescriptor FunctionPointer = new(
        id: "CMPSD2D0025",
        title: "Function pointer type",
        messageFormat: "A pixel shader cannot have a function pointer type declaration",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A pixel shader cannot have a function pointer type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an unsafe statement.
    /// </summary>
    public static readonly DiagnosticDescriptor UnsafeStatement = new(
        id: "CMPSD2D0026",
        title: "Unsafe statement",
        messageFormat: "A pixel shader cannot have an unsafe statement",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A pixel shader cannot have an unsafe statement.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an unsafe modifier on a method or function.
    /// </summary>
    public static readonly DiagnosticDescriptor UnsafeModifierOnMethodOrFunction = new(
        id: "CMPSD2D0027",
        title: "Unsafe modifier on method or function",
        messageFormat: "The unsafe modifier cannot be used in methods or functions used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The unsafe modifier cannot be used in methods or functions used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a string literal.
    /// </summary>
    public static readonly DiagnosticDescriptor StringLiteralExpression = new(
        id: "CMPSD2D0028",
        title: "String literal expression",
        messageFormat: "String literal expressions cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "String literal expressions cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an incorrect matrix swizzling property argument.
    /// </summary>
    public static readonly DiagnosticDescriptor NonConstantMatrixSwizzledIndex = new(
        id: "CMPSD2D0029",
        title: "Non constant matrix swizzled property argument",
        messageFormat: "The arguments in a swizzled indexer for a matrix type must be compile-time constants",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The arguments in a swizzled indexer for a matrix type must be compile-time constants.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid static field type.
    /// <para>
    /// Format: <c>"The pixel shader of type {0} contains or references a static field "{1}" of an invalid type {2} (only primitive, vector and matrix types are supported)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidShaderStaticFieldType = new(
        id: "CMPSD2D0030",
        title: "Invalid shader static field type",
        messageFormat: """The pixel shader of type {0} contains or references a static field "{1}" of an invalid type {2} (only primitive, vector and matrix types are supported)""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A type representing a pixel shader contains or references a static field of a type that is not supported.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a property declaration.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} contains an invalid property "{1}" declaration (only stateless properties explicitly implementing an interface property can be used)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidPropertyDeclaration = new(
        id: "CMPSD2D0031",
        title: "Invalid property declaration",
        messageFormat: """The D2D1 shader of type {0} contains an invalid property "{1}" declaration (only stateless properties explicitly implementing an interface property can be used)""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Property declarations (except for stateless properties explicitly implementing an interface property) cannot be used in a D2D1 shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader with a root signature that is too large.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} has exceeded the maximum allowed size for captured values (the maximum size is 64KB, and the actual constant buffer size was {1} bytes)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor ExceededDispatchDataSize = new(
        id: "CMPSD2D0032",
        title: "Shader dispatch data size exceeded",
        messageFormat: "The D2D1 shader of type {0} has exceeded the maximum allowed size for captured values (the maximum size is 64KB, and the actual constant buffer size was {1} bytes)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The D2D1 shader of type {0} has exceeded the maximum allowed size for captured values (the maximum size for constant buffers is 64KB).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for HLSL bytecode shader failed due to a Win32 exception.
    /// <para>
    /// Format: <c>"The shader of type {0} failed to compile due to a Win32 exception (HRESULT: {1:X8}, Message: "{2}")"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor HlslBytecodeFailedWithWin32Exception = new(
        id: "CMPSD2D0033",
        title: "HLSL bytecode compilation failed due to Win32 exception",
        messageFormat: """The shader of type {0} failed to compile due to a Win32 exception (HRESULT: {1:X8}, Message: "{2}")""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The embedded bytecode for a shader failed to be compiled due to a Win32 exception.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for HLSL bytecode shader failed due to an HLSL compilation exception.
    /// <para>
    /// Format: <c>"The shader of type {0} failed to compile due to an HLSL compiler error (Message: "{1}")"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor HlslBytecodeFailedWithCompilationException = new(
        id: "CMPSD2D0034",
        title: "HLSL bytecode compilation failed due to an HLSL compiler error",
        messageFormat: """The shader of type {0} failed to compile due to an HLSL compiler error (Message: "{1}")""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The embedded bytecode for a shader failed to be compiled due to an HLSL compiler error.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a invalid D2D input count value.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using an incorrect value for [D2DInputCount] (the number of inputs must be in the [0, 8] range, but it was {1})"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2DInputCount = new(
        id: "CMPSD2D0035",
        title: "Invalid D2D1 shader input count",
        messageFormat: "The D2D1 shader of type {0} is using an incorrect value for [D2DInputCount] (the number of inputs must be in the [0, 8] range, but it was {1})",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader must have a number of inputs in the [0, 8] range.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for repeated indices for D2D simple indices.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using repeated indices for some of its [D2DInputSimple] attributes"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor RepeatedD2DInputSimpleIndices = new(
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
    public static readonly DiagnosticDescriptor RepeatedD2DInputComplexIndices = new(
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
    public static readonly DiagnosticDescriptor InvalidSimpleAndComplexIndicesCombination = new(
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
    public static readonly DiagnosticDescriptor OutOfRangeInputIndex = new(
        id: "CMPSD2D0039",
        title: "Out of range D2D1 shader input indices",
        messageFormat: "The D2D1 shader of type {0} is using some out of range input indices",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader must have all the indices of its simple and complex inputs in the valid range (between 0 and the number of declared inputs).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a method or constructor invocation that is not valid from a shader.
    /// <para>
    /// Format: <c>"The method or constructor {0} cannot be used in a D2D1 shader (methods or constructors need to either be HLSL intrinsics or with source available for analysis)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidMethodOrConstructorCall = new(
        id: "CMPSD2D0040",
        title: "Invalid method or constructor invocation from a D2D1 shader",
        messageFormat: "The method or constructor {0} cannot be used in a D2D1 shader (methods or constructors need to either be HLSL intrinsics or with source available for analysis)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader can only invoke methods or constructors that are either HLSL intrinsics or with source available for analysis.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid discovered type.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} uses the invalid type {1}"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidDiscoveredType = new(
        id: "CMPSD2D0041",
        title: "Invalid discovered type",
        messageFormat: "The D2D1 shader of type {0} uses the invalid type {1} (only some .NET primitive types, HLSL primitive, vector and matrix types, and custom types containing these types can be used, and custom types containing these types can be used, and bool fields in custom struct types have to be replaced with the ComputeSharp.Bool type for alignment reasons)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "D2D1 shaders can only use supported types (some .NET primitive types, HLSL primitive, vector and matrix types, and custom types containing these types can be used, and custom types containing these types can be used, and bool fields in custom struct types have to be replaced with the ComputeSharp.Bool type for alignment reasons).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader with an out of range input description index (or more).
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using some out of range input description indices"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor OutOfRangeInputDescriptionIndex = new(
        id: "CMPSD2D0042",
        title: "Out of range D2D1 shader input description indices",
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
    public static readonly DiagnosticDescriptor RepeatedD2DInputDescriptionIndices = new(
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
    public static readonly DiagnosticDescriptor InvalidPackMatrixColumnMajorOption = new(
        id: "CMPSD2D0044",
        title: "Invalid PackMatrixColumnMajor compile option",
        messageFormat: "The D2D1 shader of type (or assembly) {0} is using the PackMatrixColumnMajor option in its [D2DCompileOptions] attribute",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader generated with ComputeSharp.D2D1 (or an assembly) cannot use the PackMatrixColumnMajor option, as that is not compatible with the generated code used to load shader constant buffers.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp",
        customTags: WellKnownDiagnosticTags.CompilationEnd);

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>[D2DRequiresScenePosition]</c> attribute is missing.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using D2D1 APIs that require the [D2DRequiresScenePosition] attribute to be used (that is, D2D1.GetScenePosition() and D2D1.SampleInputAtPosition(int, float2)), but the shader type is not annotated accordingly"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingD2DRequiresScenePositionAttribute = new(
        id: "CMPSD2D0045",
        title: "Missing [D2DRequiresScenePosition] attribute",
        messageFormat: "The D2D1 shader of type {0} is using D2D1 APIs that require the [D2DRequiresScenePosition] attribute to be used (that is, D2D.GetScenePosition() and D2D.SampleInputAtPosition(int, float2)), but the shader type is not annotated accordingly",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader using functionality that needs position info (ie. when [D2DRequiresScenePosition] is used, which is mandatory when calling either D2D.GetScenePosition() or D2D.SampleInputAtPosition(int, float2)) needs to be annotated accordingly.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader with an overlapping resource texture index (or more).
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using some resource texture indices that overlap with the shader input indices"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor ResourceTextureIndexOverlappingWithInputIndex = new(
        id: "CMPSD2D0046",
        title: "D2D1 shader resource texture indices overlapping input indices",
        messageFormat: "The D2D1 shader of type {0} is using some resource texture indices that overlap with the shader input indices",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader must have all the indices of its resource textures in the valid range (between the input count and 16, exclusive).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader with an out of range resource texture index (or more).
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using some out of range resource texture indices"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor OutOfRangeResourceTextureIndex = new(
        id: "CMPSD2D0047",
        title: "Out of range D2D1 shader resource texture indices",
        messageFormat: "The D2D1 shader of type {0} is using some out of range resource texture indices",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader must have all the indices of its resource textures in the valid range (between the input count and 16, exclusive).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for repeated indices for D2D resource texture indices.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is using repeated indices for some of its [D2DResourceTextureIndex] attributes"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor RepeatedD2DResourceTextureIndices = new(
        id: "CMPSD2D0048",
        title: "Repeated D2D1 shader resource texture indices",
        messageFormat: "The D2D1 shader of type {0} is using repeated indices for some of its [D2DResourceTextureIndex] attributes",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader must only have unique indices for all of its [D2DResourceTextureIndex] attributes.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid location of <c>[D2DResourceTextureIndex]</c>.
    /// <para>
    /// Format: <c>"The field "{0}" (in type {1}) is using [D2DResourceTextureIndex] on an invalid location (the attribute can only be used on D2D1 resource texture types, but the field is of type {2})"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2DResourceTextureIndexAttributeLocation = new(
        id: "CMPSD2D0049",
        title: "Invalid [D2DResourceTextureIndex] location",
        messageFormat: """The field "{0}" (in type {1}) is using [D2DResourceTextureIndex] incorrectly (the attribute can only be used on D2D1 resource texture types, but the field is of type {2})""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A field is using [D2DResourceTextureIndex] incorrectly (the attribute can only be used on D2D1 resource texture types).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a missing <c>[D2DResourceTextureIndex]</c>.
    /// <para>
    /// Format: <c>"The field "{0}" (in type {1}) is of a D2D1 resource texture type but is missing the [D2DResourceTextureIndex] attribute"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingD2DResourceTextureIndexAttribute = new(
        id: "CMPSD2D0050",
        title: "Missing [D2DResourceTextureIndex] attribute",
        messageFormat: """The field "{0}" (in type {1}) is of a D2D1 resource texture type but is missing the [D2DResourceTextureIndex] attribute""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "All fields of a D2D1 resource texture type must be annotated using the [D2DResourceTextureIndex] attribute.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a resource texture using an invalid element type.
    /// <para>
    /// Format: <c>"The field "{0}" (in type {1}) using a D2D1 resource texture of type {2} has an invalid element type (only float and float4 type arguments are supported)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidResourceTextureElementType = new(
        id: "CMPSD2D0051",
        title: "Invalid D2D1 resource texture element type",
        messageFormat: """The field "{0}" (in type {1}) using a D2D1 resource texture of type {2} has an invalid element type (only float and float4 type arguments are supported)""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The element type of D2D1 resource texture fields in a D2D1 shader must be either float or float4.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid HLSL source in <c>[D2DPixelShaderSource]</c>.
    /// <para>
    /// Format: <c>"The method "{0}" (in type {1}) is using [D2DPixelShaderSource] with an invalid HLSL source argument"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2DPixelShaderSource = new(
        id: "CMPSD2D0052",
        title: "Invalid [D2DPixelShaderSource] HLSL source argument",
        messageFormat: """The method "{0}" (in type {1}) is using [D2DPixelShaderSource] with an invalid HLSL source argument""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Methods using [D2DPixelShaderSource] must pass a valid string as the HLSL source argument.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an embedded bytecode D2D shader failed due to a Win32 exception.
    /// <para>
    /// Format: <c>"Compiling the HLSL source for method "{1}" (in type {0}) failed due to a Win32 exception (HRESULT: {2:X8}, Message: "{3}")"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor D2DPixelShaderSourceCompilationFailedWithWin32Exception = new(
        id: "CMPSD2D0053",
        title: "D2D shader compilation failed due to Win32 exception",
        messageFormat: """Compiling the HLSL source for method "{1}" (in type {0}) failed due to a Win32 exception (HRESULT: {2:X8}, Message: "{3}")""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The embedded bytecode for an input HLSl source failed to be compiled due to a Win32 exception.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an embedded bytecode D2D shader failed due to an HLSL compilation exception.
    /// <para>
    /// Format: <c>"Compiling the HLSL source for method "{1}" (in type {0}) failed due to an HLSL compiler error (Message: "{2}")"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor D2DPixelShaderSourceCompilationFailedWithFxcCompilationException = new(
        id: "CMPSD2D0054",
        title: "D2D shader compilation failed due to an HLSL compiler error",
        messageFormat: """Compiling the HLSL source for method "{1}" (in type {0}) failed due to an HLSL compiler error (Message: "{2}")""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The embedded bytecode for an input HLSl source failed to be compiled due to an HLSL compiler error.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an embedded bytecode D2D shader with no shader profile set.
    /// <para>
    /// Format: <c>"The method "{0}" (in type {1}) is using [D2DPixelShaderSource] but has not specified the shader profile to use"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingShaderProfileForD2DPixelShaderSource = new(
        id: "CMPSD2D0055",
        title: "Missing shader profile for D2D pixel shader source",
        messageFormat: """The method "{0}" (in type {1}) is using [D2DPixelShaderSource] but has not specified the shader profile to use""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Precompiled shaders using [D2DPixelShaderSource] must explicitly indicate the shader profile to use.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an embedded bytecode D2D shader with no compile options set.
    /// <para>
    /// Format: <c>"The method "{0}" (in type {1}) is using [D2DPixelShaderSource] but has not specified the compile options to use"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingCompileOptionsForD2DPixelShaderSource = new(
        id: "CMPSD2D0056",
        title: "Missing compile options for D2D pixel shader source",
        messageFormat: """The method "{0}" (in type {1}) is using [D2DPixelShaderSource] but has not specified the compile options to use""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Precompiled shaders using [D2DPixelShaderSource] must explicitly indicate the compile options.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when a method with [D2DPixelShaderSource] has an invalid return type.
    /// <para>
    /// Format: <c>"The method "{0}" (in type {1}) is using [D2DPixelShaderSource] but has an invalid return type {2} (it must return a ReadOnlySpan&lt;byte&gt;)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2DPixelShaderSourceMethodReturnType = new(
        id: "CMPSD2D0057",
        title: "Missing compile options for D2D pixel shader source",
        messageFormat: """The method "{0}" (in type {1}) is using [D2DPixelShaderSource] but has an invalid return type {2} (it must return a ReadOnlySpan<byte>)""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Methods using using [D2DPixelShaderSource] must use ReadOnlySpan<byte> as the return type.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>[D2DInputCount]</c> attribute is missing.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} is not annotated with the [D2DInputCount] attribute (it is mandatory for all D2D1 shader types)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingD2DInputCountAttribute = new(
        id: "CMPSD2D0058",
        title: "Missing [D2DResourceTextureIndex] attribute",
        messageFormat: "The D2D1 shader of type {0} is not annotated with the [D2DInputCount] attribute (it is mandatory for all D2D1 shader types)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A D2D1 shader must be annotated with the [D2DInputCount] attribute.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>[D2DEffectId]</c> attribute is being used with an invalid value.
    /// <para>
    /// Format: <c>"The [D2DEffectId] attribute is being used with an invalid value (the input text must be a valid GUID)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2DEffectIdAttributeValue = new(
        id: "CMPSD2D0059",
        title: "Invalid [D2DEffectId] attribute value",
        messageFormat: "The [D2DEffectId] attribute is being used with an invalid value (the input text must be a valid GUID)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [D2DEffectId] attribute must use a valid GUID expression as value.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>[D2DEffectDisplayName]</c> attribute is being used with an invalid value.
    /// <para>
    /// Format: <c>"The [D2DEffectDisplayName] attribute is being used with an invalid value"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2DEffectDisplayNameAttributeValue = new(
        id: "CMPSD2D0060",
        title: "Invalid [D2DEffectDisplayName] attribute value",
        messageFormat: "The [D2DEffectDisplayName] attribute is being used with an invalid value",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [D2DEffectDisplayName] attribute must contain valid text.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>[D2DEffectDescription]</c> attribute is being used with an invalid value.
    /// <para>
    /// Format: <c>"The [D2DEffectDescription] attribute is being used with an invalid value"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2DEffectDescriptionAttributeValue = new(
        id: "CMPSD2D0061",
        title: "Invalid [D2DEffectDescription] attribute value",
        messageFormat: "The [D2DEffectDescription] attribute is being used with an invalid value",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [D2DEffectDescription] attribute must contain valid text.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>[D2DEffectCategory]</c> attribute is being used with an invalid value.
    /// <para>
    /// Format: <c>"The [D2DEffectDescription] attribute is being used with an invalid value"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2DEffectCategoryAttributeValue = new(
        id: "CMPSD2D0062",
        title: "Invalid [D2DEffectCategory] attribute value",
        messageFormat: "The [D2DEffectCategory] attribute is being used with an invalid value",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [D2DEffectCategory] attribute must contain valid text.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>[D2DEffectAuthor]</c> attribute is being used with an invalid value.
    /// <para>
    /// Format: <c>"The [D2DEffectAuthor] attribute is being used with an invalid value"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2DEffectAuthorAttributeValue = new(
        id: "CMPSD2D0063",
        title: "Invalid [D2DEffectAuthor] attribute value",
        messageFormat: "The [D2DEffectAuthor] attribute is being used with an invalid value",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [D2DEffectAuthor] attribute must contain valid text.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>AllowUnsafeBlocks</c> option is not set.
    /// <para>
    /// Format: <c>"Using [D2DGeneratedPixelShaderDescriptor] requires unsafe blocks being enabled, as they are needed by the source generators to emit valid code (add &lt;AllowUnsafeBlocks&gt;true&lt;/AllowUnsafeBlocks&gt; to your .csproj/.props file)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingAllowUnsafeBlocksOption = new(
        id: "CMPSD2D0064",
        title: "Missing 'AllowUnsafeBlocks' compilation option",
        messageFormat: "Using [D2DGeneratedPixelShaderDescriptor] requires unsafe blocks being enabled, as they are needed by the source generators to emit valid code (add <AllowUnsafeBlocks>true</AllowUnsafeBlocks> to your .csproj/.props file)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Unsafe blocks must be enabled when using [D2DGeneratedPixelShaderDescriptor] for the source generators to emit valid code (the <AllowUnsafeBlocks>true</AllowUnsafeBlocks> option must be set in the .csproj/.props file).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when a pixel shader type doesn't have an associated descriptor.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} does not have an associated descriptor (it can be autogenerated via the [D2DGeneratedPixelShaderDescriptor] attribute)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingPixelShaderDescriptorOnPixelShaderType = new(
        id: MissingPixelShaderDescriptorOnPixelShaderTypeId,
        title: "Missing descriptor for D2D1 pixel shader type",
        messageFormat: "The D2D1 shader of type {0} does not have an associated descriptor (it can be autogenerated via the [D2DGeneratedPixelShaderDescriptor] attribute)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "All D2D1 shader types must have an associated descriptor (it can be autogenerated via the [D2DGeneratedPixelShaderDescriptor] attribute).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp",
        customTags: WellKnownDiagnosticTags.CompilationEnd);

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>[D2DGeneratedPixelShaderDescriptor]</c> attribute is being used on an invalid target type.
    /// <para>
    /// Format: <c>"The type {0} is not a valid target for the [D2DGeneratedPixelShaderDescriptor] attribute (only non generic types implementing the ID2D1PixelShader interface are valid)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2DGeneratedPixelShaderDescriptorAttributeTarget = new(
        id: "CMPSD2D0066",
        title: "Invalid [D2DGeneratedPixelShaderDescriptor] attribute target",
        messageFormat: "The type {0} is not a valid target for the [D2DGeneratedPixelShaderDescriptor] attribute (only non generic types implementing ID2D1PixelShader interface are valid)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [D2DGeneratedPixelShaderDescriptor] attribute must be used on non generic types that implement the ID2D1PixelShader interface.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>[D2DGeneratedPixelShaderDescriptor]</c> attribute is being used on a type that is not accessible from its containing assembly.
    /// <para>
    /// Format: <c>"The [D2DGeneratedPixelShaderDescriptor] attribute requires target types to be accessible from their containing assembly (type {0} has less effective accessibility than internal)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor NotAccessibleTargetTypeForD2DGeneratedPixelShaderDescriptorAttribute = new(
        id: "CMPSD2D0067",
        title: "Not accessible type using the [D2DGeneratedPixelShaderDescriptor] attribute",
        messageFormat: "The [D2DGeneratedPixelShaderDescriptor] attribute requires target types to be accessible from their containing assembly (type {0} has less effective accessibility than internal)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [D2DGeneratedPixelShaderDescriptor] attribute requires target types to be accessible from their containing assembly.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when a field in a type using the <c>[D2DGeneratedPixelShaderDescriptor]</c> attribute has a type that is not accessible from its containing assembly.
    /// <para>
    /// Format: <c>"The [D2DGeneratedPixelShaderDescriptor] attribute requires the type of all fields of target types to be accessible from their containing assembly (type {0} has a field "{1}" of type {2} that has less effective accessibility than internal)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor NotAccessibleFieldTypeInTargetTypeForD2DGeneratedPixelShaderDescriptorAttribute = new(
        id: "CMPSD2D0068",
        title: "Not accessible field type in type using the [D2DGeneratedPixelShaderDescriptor] attribute",
        messageFormat: """The [D2DGeneratedPixelShaderDescriptor] attribute requires the type of all fields of target types to be accessible from their containing assembly (type {0} has a field "{1}" of type {2} that has less effective accessibility than internal)""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [D2DGeneratedPixelShaderDescriptor] attribute requires the type of all fields of target types to be accessible from their containing assembly.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid use of <c>[D2DPixelOptions]</c> indicating trivial sampling.
    /// <para>
    /// Format: <c>"The D2D1 shader of type {0} shouldn't use D2D1PixelOptions.TrivialSampling in its [D2DPixelOptions] attribute, as it has one or more complex inputs (either mark the inputs as simple, or remove the trivial sampling option)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2D1PixelOptionsTrivialSamplingOnShaderType = new(
        id: "CMPSD2D0069",
        title: "Invalid [D2DPixelOptions] use",
        messageFormat: """The D2D1 shader of type {0} shouldn't use D2D1PixelOptions.TrivialSampling in its [D2DPixelOptions] attribute, as it has one or more complex inputs (either mark the inputs as simple, or remove the trivial sampling option)""",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "A D2D1 shader shouldn't use D2D1PixelOptions.TrivialSampling in its [D2DPixelOptions] attribute if it has one or more complex inputs (because trivial sampling shaders can only sample pixels at the same scene coordinate as the output pixel, a shader using this option should only have simple inputs).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when a shader type with any fields is not readonly.
    /// <para>
    /// Format: <c>"The shader of type {0} is not readonly (shaders cannot mutate their instance state while running, so shader types not being readonly makes them error prone)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor NotReadOnlyShaderType = new(
        id: "CMPSD2D0070",
        title: "Not readonly shader type (using ID2D1PixelShader)",
        messageFormat: "The shader of type {0} is not readonly (shaders cannot mutate their instance state while running, so shader types not being readonly makes them error prone)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Shader types should be readonly (shaders cannot mutate their instance state while running, so shader types not being readonly makes them error prone).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an initializer expression.
    /// </summary>
    public static readonly DiagnosticDescriptor InitializerExpression = new(
        id: "CMPSD2D0071",
        title: "Initializer expression",
        messageFormat: "An initializer expression cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "An initializer expression cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a collection expression.
    /// </summary>
    public static readonly DiagnosticDescriptor CollectionExpression = new(
        id: "CMPSD2D0072",
        title: "Collection expression",
        messageFormat: "A collection expression cannot be used in a pixel shader",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A collection expression cannot be used in a pixel shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a constructor with a base constructor declaration.
    /// <para>
    /// Format: <c>"The constructor {0} has a base constructor declaration, which cannot be used in a D2D1 shader (only standalone constructors are allowed)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidBaseConstructorDeclaration = new(
        id: "CMPSD2D0073",
        title: "Invalid base constructor declaration",
        messageFormat: "The constructor {0} has a base constructor declaration, which cannot be used in a D2D1 shader (only standalone constructors are allowed)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Only standalone constructors (with no base constructor declaration) can be used in a D2D1 shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a <see langword="this"/> expression.
    /// </summary>
    public static readonly DiagnosticDescriptor ThisExpression = new(
        id: "CMPSD2D0074",
        title: "Invalid 'this' expression",
        messageFormat: "A pixel shader cannot use a 'this' expression outside of member accesses (such as 'this.field')",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A pixel shader cannot use a 'this' expression outside of member accesses (such as 'this.field').",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invocation of a <c>Math</c> or <c>MathF</c> API.
    /// <para>
    /// Format: <c>"The method {0} cannot be used in a shader, use equivalent APIs from the Hlsl type instead"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidMathOrMathFCall = new(
        id: "CMPSD2D0075",
        title: "Invalid Math or MathF invocation from a shader",
        messageFormat: "The method {0} cannot be used in a shader, use equivalent APIs from the Hlsl type instead",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Methods from the Math and MathF types cannot be used in a shader, and equivalent APIs from the Hlsl type should be used instead.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader not precompiled but with runtime compilation disabled.
    /// <para>
    /// Format: <c>"The shader {0} is not precompiled, but runtime compilation is not enabled (either precompile the shader by using [D2DShaderProfile], which is recommended, or enable runtime compilation using [D2DEnableRuntimeCompilation])"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor D2DRuntimeCompilationDisabled = new(
        id: "CMPSD2D0076",
        title: "D2D runtime compilation disabled",
        messageFormat: "The shader {0} is not precompiled, but runtime compilation is not enabled (either precompile the shader by using [D2DShaderProfile], which is recommended, or enable runtime compilation using [D2DEnableRuntimeCompilation])",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Shaders must either be precompiled using [D2DShaderProfile], which is recommended, or runtime compilation for shaders must be enabled using [D2DEnableRuntimeCompilation].",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an unnecessary [D2DEnableRuntimeCompilation] use.
    /// <para>
    /// Format: <c>"The shader {0} is annotated with [D2DEnableRuntimeCompilation], but its containing assembly is already annotated with this attribute (so using it again on the shader type is unnecessary)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor D2DRuntimeCompilationAlreadyEnabled = new(
        id: "CMPSD2D0077",
        title: "D2D runtime compilation already enabled",
        messageFormat: "The shader {0} is annotated with [D2DEnableRuntimeCompilation], but its containing assembly is already annotated with this attribute (so using it again on the shader type is unnecessary)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: "If an assembly is annotated with [D2DEnableRuntimeCompilation], that applies to all shader types within it. That means that using [D2DEnableRuntimeCompilation] on shader types too is unnecessary.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an unnecessary [D2DEnableRuntimeCompilation] use on .
    /// <para>
    /// Format: <c>"The shader {0} is annotated with [D2DEnableRuntimeCompilation], but it is also being precompiled, as it has [D2DShaderProfile] on the type or containing assembly (so using [D2DEnableRuntimeCompilation] is unnecessary)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor D2DRuntimeCompilationOnTypeNotNecessary = new(
        id: "CMPSD2D0078",
        title: "D2D runtime compilation on type is not necessary",
        messageFormat: "The shader {0} is annotated with [D2DEnableRuntimeCompilation], but it is also being precompiled, as it has [D2DShaderProfile] on the type or containing assembly (so using [D2DEnableRuntimeCompilation] is unnecessary)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "If a shader is precompiled (ie. it has [D2DShaderProfile] on the type declaration or on its containing assembly), also using [D2DEnableRuntimeCompilation] is unnecessary.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an unnecessary [D2DEnableRuntimeCompilation] use on an assembly.
    /// <para>
    /// Format: <c>"The assembly {0} is annotated with [D2DEnableRuntimeCompilation], but it is also has an assembly-level [D2DShaderProfile] attribute, which will cause all shaders to be precompiled (so using [D2DEnableRuntimeCompilation] is unnecessary)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor D2DRuntimeCompilationOnAssemblyNotNecessary = new(
        id: "CMPSD2D0079",
        title: "D2D runtime compilation on assembly is not necessary",
        messageFormat: "The assembly {0} is annotated with [D2DEnableRuntimeCompilation], but it is also has an assembly-level [D2DShaderProfile] attribute, which will cause all shaders to be precompiled (so using [D2DEnableRuntimeCompilation] is unnecessary)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "If an assembly is using [D2DShaderProfile] (meaning that all shaders declared within it will be precompiled), also using [D2DEnableRuntimeCompilation] is unnecessary.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp",
        customTags: WellKnownDiagnosticTags.CompilationEnd);

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader missing [D2DRequiresDoublePrecisionSupport].
    /// <para>
    /// Format: <c>"The shader {0} requires double precision support, but it does not have the [D2DRequiresDoublePrecisionSupport] attribute on it (adding the attribute is necessary to explicitly opt-in to that functionality)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingRequiresDoublePrecisionSupportAttribute = new(
        id: "CMPSD2D0080",
        title: "Missing [D2DRequiresDoublePrecisionSupport] attribute",
        messageFormat: "The shader {0} requires double precision support, but it does not have the [D2DRequiresDoublePrecisionSupport] attribute on it (adding the attribute is necessary to explicitly opt-in to that functionality)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Shaders performing double precision operations must be annotated with [D2DRequiresDoublePrecisionSupport] to explicitly opt-in to that functionality.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader is unnecessarily using [D2DRequiresDoublePrecisionSupport].
    /// <para>
    /// Format: <c>"The shader {0} does not require double precision support, but it has the [D2DRequiresDoublePrecisionSupport] attribute on it (using the attribute is not needed if the shader is not performing any double precision operations)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor UnnecessaryRequiresDoublePrecisionSupportAttribute = new(
        id: "CMPSD2D0081",
        title: "Unnecessary [D2DRequiresDoublePrecisionSupport] attribute",
        messageFormat: "The shader {0} does not require double precision support, but it has the [D2DRequiresDoublePrecisionSupport] attribute on it (using the attribute is not needed if the shader is not performing any double precision operations)",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Shaders not performing any double precision operations should not be annotated with [D2DRequiresDoublePrecisionSupport], as the attribute is not needed in that case.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader is using [D2DRequiresDoublePrecisionSupport] incorrectly.
    /// <para>
    /// Format: <c>"The shader {0} has the [D2DRequiresDoublePrecisionSupport] attribute on it, but it is not precompiled, so validation for use of double precision operations cannot be performed"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidD2DRequiresDoublePrecisionSupportAttribute = new(
        id: "CMPSD2D0082",
        title: "Invalid [D2DRequiresDoublePrecisionSupport] attribute",
        messageFormat: "The shader {0} has the [D2DRequiresDoublePrecisionSupport] attribute on it, but it is not precompiled, so validation for use of double precision operations cannot be performed",
        category: "ComputeSharp.D2D1.Shaders",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Shaders can only be annotated with [D2DRequiresDoublePrecisionSupport] to perform validation for use of double precision operations if they are precompiled.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");
}