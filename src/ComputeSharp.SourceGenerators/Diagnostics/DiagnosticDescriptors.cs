using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGeneration.Diagnostics;

/// <inheritdoc/>
partial class DiagnosticDescriptors
{
    /// <summary>
    /// The diagnostic id for <see cref="MissingComputeShaderDescriptorOnComputeShaderType"/>.
    /// </summary>
    public const string MissingComputeShaderDescriptorOnComputeShaderTypeId = "CMPS0053";

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid shader field.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains a field "{1}" of an invalid type {2}"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidShaderField = new(
        id: "CMPS0001",
        title: "Invalid shader field",
        messageFormat: """The compute shader of type {0} contains a field "{1}" of an invalid type {2}""",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A type representing a compute shader contains a field of a type that is not supported in HLSL.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid group shared field type.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains a group shared field "{1}" of an invalid type {2}"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGroupSharedFieldType = new(
        id: "CMPS0002",
        title: "Invalid group shared field type",
        messageFormat: """The compute shader of type {0} contains a group shared field "{1}" of an invalid type {2} (it must be an array)""",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A group shared field must be of an array type.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid group shared field element type.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains a group shared field "{1}" of an invalid element type {2}"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGroupSharedFieldElementType = new(
        id: "CMPS0003",
        title: "Invalid group shared field element type",
        messageFormat: """The compute shader of type {0} contains a group shared field "{1}" of an invalid type {2} (it must be a primitive or unmanaged type)""",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A group shared field element must be of a primitive or unmanaged type.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid group shared field declaration.
    /// <para>
    /// Format: <c>"The field "{0}" is annotated with [GroupShared], but is not a valid target for it (only static fields of array type in compute shader types, with an unmanaged element type can be annotated with [GroupShared])"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGroupSharedFieldDeclaration = new(
        id: "CMPS0004",
        title: "Invalid [GroupShared] field declaration",
        messageFormat: """The field "{0}" is annotated with [GroupShared], but is not a valid target for it (only static fields of array type in compute shader types, with an unmanaged element type can be annotated with [GroupShared])""",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [GroupShared] attribute is only valid on static fields of array type in compute shader types, with an unmanaged element type.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader with no resources.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains no resources to work on"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingShaderResources = new(
        id: "CMPS0005",
        title: "Missing shader resources",
        messageFormat: "The compute shader of type {0} contains no resources to work on",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader must contain at least one resource.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="ThreadIds"/> usage.
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidThreadIdsUsage = new(
        id: "CMPS0006",
        title: "Invalid ThreadIds usage",
        messageFormat: "The ThreadIds type can only be used within the main body of a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The ThreadIds type can only be used within the main body of a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="GroupIds"/> usage.
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGroupIdsUsage = new(
        id: "CMPS0007",
        title: "Invalid GroupIds usage",
        messageFormat: "The GroupIds type can only be used within the main body of a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The GroupIds type can only be used within the main body of a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="GroupSize"/> usage.
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGroupSizeUsage = new(
        id: "CMPS0008",
        title: "Invalid GroupSize usage",
        messageFormat: "The GroupSize type can only be used within the main body of a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The GroupSize type can only be used within the main body of a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="GridIds"/> usage.
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGridIdsUsage = new(
        id: "CMPS0009",
        title: "Invalid GridIds usage",
        messageFormat: "The GridIds type can only be used within the main body of a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The GridIds type can only be used within the main body of a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid object creation expression.
    /// <para>
    /// Format: <c>"The type {0} cannot be created in a compute shader"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidObjectCreationExpression = new(
        id: "CMPS0010",
        title: "Invalid object creation expression",
        messageFormat: "The type {0} cannot be created in a compute shader (only unmanaged types are supported)",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Only unmanaged value type objects can be created in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an anonymous object creation expression.
    /// </summary>
    public static readonly DiagnosticDescriptor AnonymousObjectCreationExpression = new(
        id: "CMPS0011",
        title: "Anonymous object creation expression",
        messageFormat: "An anonymous object cannot be created in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "An anonymous object cannot be created in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an async modifier on a method or function.
    /// </summary>
    public static readonly DiagnosticDescriptor AsyncModifierOnMethodOrFunction = new(
        id: "CMPS0012",
        title: "Async modifier on method or function",
        messageFormat: "The async modifier cannot be used in methods or functions used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The async modifier cannot be used in methods or functions used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an await expression.
    /// </summary>
    public static readonly DiagnosticDescriptor AwaitExpression = new(
        id: "CMPS0013",
        title: "Await expression",
        messageFormat: "The await expression cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The await expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a checked expression.
    /// </summary>
    public static readonly DiagnosticDescriptor CheckedExpression = new(
        id: "CMPS0014",
        title: "Checked expression",
        messageFormat: "A checked expression cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A checked expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a checked statement.
    /// </summary>
    public static readonly DiagnosticDescriptor CheckedStatement = new(
        id: "CMPS0015",
        title: "Checked statement",
        messageFormat: "A checked statement cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A checked statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a fixed statement.
    /// </summary>
    public static readonly DiagnosticDescriptor FixedStatement = new(
        id: "CMPS0016",
        title: "Fixed statement",
        messageFormat: "A fixed statement cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A fixed statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a foreach statement.
    /// </summary>
    public static readonly DiagnosticDescriptor ForEachStatement = new(
        id: "CMPS0017",
        title: "Foreach statement",
        messageFormat: "A foreach statement cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A foreach statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a lock statement.
    /// </summary>
    public static readonly DiagnosticDescriptor LockStatement = new(
        id: "CMPS0018",
        title: "Foreach statement",
        messageFormat: "A lock statement cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A lock statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a query statement.
    /// </summary>
    public static readonly DiagnosticDescriptor QueryExpression = new(
        id: "CMPS0019",
        title: "Foreach statement",
        messageFormat: "A LINQ query expression cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A LINQ query expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a range expression.
    /// </summary>
    public static readonly DiagnosticDescriptor RangeExpression = new(
        id: "CMPS0020",
        title: "Range expression",
        messageFormat: "A range expression cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A range expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a recursive pattern.
    /// </summary>
    public static readonly DiagnosticDescriptor RecursivePattern = new(
        id: "CMPS0021",
        title: "Recursive pattern",
        messageFormat: "A recursive pattern cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A recursive pattern cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a ref type.
    /// </summary>
    public static readonly DiagnosticDescriptor RefType = new(
        id: "CMPS0022",
        title: "Ref type",
        messageFormat: "A compute shader cannot have a ref type declaration",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have a ref type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a relational pattern.
    /// </summary>
    public static readonly DiagnosticDescriptor RelationalPattern = new(
        id: "CMPS0023",
        title: "Relational pattern",
        messageFormat: "A relational pattern cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A relational pattern cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a sizeof expression.
    /// </summary>
    public static readonly DiagnosticDescriptor SizeOfExpression = new(
        id: "CMPS0024",
        title: "Sizeof expression",
        messageFormat: "A sizeof expression cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A sizeof expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a stackalloc expression.
    /// </summary>
    public static readonly DiagnosticDescriptor StackAllocArrayCreationExpression = new(
        id: "CMPS0025",
        title: "Stackalloc expression",
        messageFormat: "A stackalloc expression cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A stackalloc expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a throw expression or statement.
    /// </summary>
    public static readonly DiagnosticDescriptor ThrowExpressionOrStatement = new(
        id: "CMPS0026",
        title: "Throw expression or statement",
        messageFormat: "Throw expressions and statements cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Throw expressions and statements cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a try statement.
    /// </summary>
    public static readonly DiagnosticDescriptor TryStatement = new(
        id: "CMPS0027",
        title: "Try statement",
        messageFormat: "A try statement cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A try statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a tuple type.
    /// </summary>
    public static readonly DiagnosticDescriptor TupleType = new(
        id: "CMPS0028",
        title: "Tuple type",
        messageFormat: "A compute shader cannot have a tuple type declaration",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have a tuple type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a using statement or declaration.
    /// </summary>
    public static readonly DiagnosticDescriptor UsingStatementOrDeclaration = new(
        id: "CMPS0029",
        title: "Using statement or declaration",
        messageFormat: "Using statements and declarations cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Using statements and declarations cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a yield statement.
    /// </summary>
    public static readonly DiagnosticDescriptor YieldStatement = new(
        id: "CMPS0030",
        title: "Yield statement",
        messageFormat: "A yield statement cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
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
    public static readonly DiagnosticDescriptor InvalidObjectDeclaration = new(
        id: "CMPS0031",
        title: "Invalid object declaration",
        messageFormat: "A variable of type {0} cannot be declared in a compute shader (only unmanaged types are supported)",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Only unmanaged value type objects can be declared in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a pointer type.
    /// </summary>
    public static readonly DiagnosticDescriptor PointerType = new(
        id: "CMPS0032",
        title: "Pointer type",
        messageFormat: "A compute shader cannot have a pointer type declaration",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have a pointer type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a function pointer type.
    /// </summary>
    public static readonly DiagnosticDescriptor FunctionPointer = new(
        id: "CMPS0033",
        title: "Function pointer type",
        messageFormat: "A compute shader cannot have a function pointer type declaration",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have a function pointer type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an unsafe statement.
    /// </summary>
    public static readonly DiagnosticDescriptor UnsafeStatement = new(
        id: "CMPS0034",
        title: "Unsafe statement",
        messageFormat: "A compute shader cannot have an unsafe statement",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have an unsafe statement.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an unsafe modifier on a method or function.
    /// </summary>
    public static readonly DiagnosticDescriptor UnsafeModifierOnMethodOrFunction = new(
        id: "CMPS0035",
        title: "Unsafe modifier on method or function",
        messageFormat: "The unsafe modifier cannot be used in methods or functions used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The unsafe modifier cannot be used in methods or functions used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a string literal.
    /// </summary>
    public static readonly DiagnosticDescriptor StringLiteralExpression = new(
        id: "CMPS0036",
        title: "String literal expression",
        messageFormat: "String literal expressions cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "String literal expressions cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an incorrect matrix swizzling property argument.
    /// </summary>
    public static readonly DiagnosticDescriptor NonConstantMatrixSwizzledIndex = new(
        id: "CMPS0037",
        title: "Non constant matrix swizzled property argument",
        messageFormat: "The arguments in a swizzled indexer for a matrix type must be compile-time constants",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The arguments in a swizzled indexer for a matrix type must be compile-time constants.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid shader static field type.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains or references a static field "{1}" of an invalid type {2} (only primitive, vector and matrix types are supported)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidShaderStaticFieldType = new(
        id: "CMPS0038",
        title: "Invalid shader static field type",
        messageFormat: """The compute shader of type {0} contains or references a static field "{1}" of an invalid type {2} (only primitive, vector and matrix types are supported)""",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A type representing a compute shader contains or references a static field of a type that is not supported.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="DispatchSize"/> usage.
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidDispatchSizeUsage = new(
        id: "CMPS0039",
        title: "Invalid DispatchSize usage",
        messageFormat: "The DispatchSize type can only be used within the main body of a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The DispatchSize type can only be used within the main body of a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a property declaration.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains an invalid property "{1}" declaration (only stateless properties explicitly implementing an interface property can be used)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidPropertyDeclaration = new(
        id: "CMPS0040",
        title: "Invalid property declaration",
        messageFormat: """The compute shader of type {0} contains an invalid property "{1}" declaration (only stateless properties explicitly implementing an interface property can be used)""",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Property declarations (except for stateless properties explicitly implementing an interface property) cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader with a root signature that is too large.
    /// <para>
    /// Format: <c>"The compute shader of type {0} has exceeded the maximum allowed size for captured values and resources (the maximum size for the root signature is 64 DWORD constants, but the actual size was {1})"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor ShaderDispatchDataSizeExceeded = new(
        id: "CMPS0041",
        title: "Shader dispatch data size exceeded",
        messageFormat: "The compute shader of type {0} has exceeded the maximum allowed size for captured values and resources (the maximum size for the root signature is 64 DWORD constants, but the actual size was {1})",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The compute shader of type {0} has exceeded the maximum allowed size for captured values and resources (the maximum size for the root signature is 64 DWORD constants).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a type implementing multiple shader interfaces.
    /// <para>
    /// Format: <c>"The shader of type {0} cannot implement more than one shader interface"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MultipleShaderTypesImplemented = new(
        id: "CMPS0042",
        title: "Multiple shader implementations for type declaration",
        messageFormat: "The shader of type {0} cannot implement more than one shader interface",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A shader type cannot implement more than one shader interface.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for invalid thread group sizes.
    /// <para>
    /// Format: <c>"The [ThreadGroupSize] attribute on shader type {0} is using invalid thread group size values"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidThreadGroupSizeAttributeValues = new(
        id: "CMPS0044",
        title: "Invalid values for [ThreadGroupSize] attribute",
        messageFormat: "The [ThreadGroupSize] attribute on shader type {0} is using invalid thread group size values",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The thread group sizes for [ThreadGroupSize] have to be in the valid range.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for HLSL bytecode shader failed due to a Win32 exception.
    /// <para>
    /// Format: <c>"The shader of type {0} failed to compile due to a Win32 exception (HRESULT: {1:X8}, Message: "{2}")"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor HlslBytecodeFailedWithWin32Exception = new(
        id: "CMPS0045",
        title: "HLSL bytecode compilation failed due to Win32 exception",
        messageFormat: """The shader of type {0} failed to compile due to a Win32 exception (HRESULT: {1:X8}, Message: "{2}")""",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The HLSL bytecode for a shader failed to be compiled due to a Win32 exception.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for HLSL bytecode shader failed due to an HLSL compilation exception.
    /// <para>
    /// Format: <c>"The shader of type {0} failed to compile due to an HLSL compiler error (Message: "{1}")"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor HlslBytecodeFailedWithCompilationException = new(
        id: "CMPS0046",
        title: "HLSL bytecode compilation failed due to an HLSL compiler error",
        messageFormat: """The shader of type {0} failed to compile due to an HLSL compiler error (Message: "{1}")""",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The HLSL bytecode for a shader failed to be compiled due to an HLSL compiler error.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader without the thread group size attribute.
    /// <para>
    /// Format: <c>"The shader of type {0} needs to be annotated with [ThreadGroupSize], as dynamic shader compilation is not supported"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingThreadGroupSizeAttribute = new(
        id: "CMPS0047",
        title: "Missing [ThreadGroupSize] attribute on shader type",
        messageFormat: "The shader of type {0} needs to be annotated with [ThreadGroupSize] to be compiled at build time, as dynamic shader compilation is not supported",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "All shaders need to be annotated with the [ThreadGroupSize] attribute to be compiled at build time.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for shaders shader with an invalid DefaultThreadGroupSizes value.
    /// <para>
    /// Format: <c>"The [ThreadGroupSize] attribute on shader type {0} is using an invalid DefaultThreadGroupSizes value"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidThreadGroupSizeAttributeDefaultThreadGroupSizes = new(
        id: "CMPS0048",
        title: "Invalid DefaultThreadGroupSizes value for [ThreadGroupSize] use",
        messageFormat: "The [ThreadGroupSize] attribute on shader type {0} is using an invalid DefaultThreadGroupSizes value",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The DefaultThreadGroupSizes value for [ThreadGroupSize] attributes have to be valid.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a method or constructor invocation that is not valid from a shader.
    /// <para>
    /// Format: <c>"The method or constructor {0} cannot be used in a shader (methods or constructors need to either be HLSL intrinsics or with source available for analysis)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidMethodOrConstructorCall = new(
        id: "CMPS0049",
        title: "Invalid method or constructor invocation from a shader",
        messageFormat: "The method or constructor {0} cannot be used in a shader (methods or constructors need to either be HLSL intrinsics or with source available for analysis)",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Shaders can only invoke methods or constructors that are either HLSL intrinsics or with source available for analysis.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid discovered type.
    /// <para>
    /// Format: <c>"The compute shader or method {0} uses the invalid type {1}"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidDiscoveredType = new(
        id: "CMPS0050",
        title: "Invalid discovered type",
        messageFormat: "The compute shader or method {0} uses the invalid type {1} (only some .NET primitive types, HLSL primitive, vector and matrix types, and custom types containing these types can be used, and bool fields in custom struct types have to be replaced with the ComputeSharp.Bool type for alignment reasons)",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Shaders and shader methods can only use supported types (some .NET primitive types, HLSL primitive, vector and matrix types, and custom types containing these types can be used, and bool fields in custom struct types have to be replaced with the ComputeSharp.Bool type for alignment reasons).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a an invalid copy operation for <c>ComputeContext</c>.
    /// <para>
    /// Format: <c>"The compute shader or method {0} uses the invalid type {1}"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidComputeContextCopy = new(
        id: "CMPS0051",
        title: "Invalid ComputeContext copy operation",
        messageFormat: "The ComputeContext type cannot be copied (consider passing it via ref readonly or in instead) and cannot be used as a field of value types (as it could be indirectly copied)",
        category: "ComputeSharp",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The ComputeContext type cannot be copied (and values should rather be passed via ref readonly or in instead) and cannot be used as a field of value types (as it could be indirectly copied).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>AllowUnsafeBlocks</c> option is not set.
    /// <para>
    /// Format: <c>"Using [GeneratedComputeShaderDescriptor] requires unsafe blocks being enabled, as they are needed by the source generators to emit valid code (add &lt;AllowUnsafeBlocks&gt;true&lt;/AllowUnsafeBlocks&gt; to your .csproj/.props file)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingAllowUnsafeBlocksOption = new(
        id: "CMPS0052",
        title: "Missing 'AllowUnsafeBlocks' compilation option",
        messageFormat: "Using [GeneratedComputeShaderDescriptor] requires unsafe blocks being enabled, as they are needed by the source generators to emit valid code (add <AllowUnsafeBlocks>true</AllowUnsafeBlocks> to your .csproj/.props file)",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Unsafe blocks must be enabled when using [GeneratedComputeShaderDescriptor] for the source generators to emit valid code (the <AllowUnsafeBlocks>true</AllowUnsafeBlocks> option must be set in the .csproj/.props file).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when a compute shader type doesn't have an associated descriptor.
    /// <para>
    /// Format: <c>"The compute shader of type {0} does not have an associated descriptor (it can be autogenerated via the [GeneratedComputeShaderDescriptor] attribute)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingComputeShaderDescriptorOnComputeShaderType = new(
        id: MissingComputeShaderDescriptorOnComputeShaderTypeId,
        title: "Missing descriptor for compute pixel shader type",
        messageFormat: "The compute shader of type {0} does not have an associated descriptor (it can be autogenerated via the [GeneratedComputeShaderDescriptor] attribute)",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "All compute shader types must have an associated descriptor (it can be autogenerated via the [GeneratedComputeShaderDescriptor] attribute).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp",
        customTags: WellKnownDiagnosticTags.CompilationEnd);

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>[GeneratedComputeShaderDescriptor]</c> attribute is being used on an invalid target type.
    /// <para>
    /// Format: <c>"The type {0} is not a valid target for the [GeneratedComputeShaderDescriptor] attribute (only non generic types implementing the IComputeShader or IComputeShader&lt;TPixel&gt; interface are valid)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGeneratedComputeShaderDescriptorAttributeTarget = new(
        id: "CMPS0054",
        title: "Invalid [GeneratedComputeShaderDescriptor] attribute target",
        messageFormat: "The type {0} is not a valid target for the [GeneratedComputeShaderDescriptor] attribute (only non generic types implementing the IComputeShader or IComputeShader<TPixel> interface are valid)",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [GeneratedComputeShaderDescriptor] attribute must be used on non generic types that implement the IComputeShader or IComputeShader<TPixel> interfaces.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when the <c>[GeneratedComputeShaderDescriptor]</c> attribute is being used on a type that is not accessible from its containing assembly.
    /// <para>
    /// Format: <c>"The [GeneratedComputeShaderDescriptor] attribute requires target types to be accessible from their containing assembly (type {0} has less effective accessibility than internal)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor NotAccessibleTargetTypeForGeneratedComputeShaderDescriptorAttribute = new(
        id: "CMPS0055",
        title: "Not accessible type using the [GeneratedComputeShaderDescriptor] attribute",
        messageFormat: "The [GeneratedComputeShaderDescriptor] attribute requires target types to be accessible from their containing assembly (type {0} has less effective accessibility than internal)",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [GeneratedComputeShaderDescriptor] attribute requires target types to be accessible from their containing assembly.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when a field in a type using the <c>[GeneratedComputeShaderDescriptor]</c> attribute has a type that is not accessible from its containing assembly.
    /// <para>
    /// Format: <c>"The [GeneratedComputeShaderDescriptor] attribute requires the type of all fields of target types to be accessible from their containing assembly (type {0} has a field "{1}" of type {2} that has less effective accessibility than internal)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor NotAccessibleFieldTypeInTargetTypeForGeneratedComputeShaderDescriptorAttribute = new(
        id: "CMPS0056",
        title: "Not accessible field type in type using the [GeneratedComputeShaderDescriptor] attribute",
        messageFormat: """The [GeneratedComputeShaderDescriptor] attribute requires the type of all fields of target types to be accessible from their containing assembly (type {0} has a field "{1}" of type {2} that has less effective accessibility than internal)""",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [GeneratedComputeShaderDescriptor] attribute requires the type of all fields of target types to be accessible from their containing assembly.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when a shader type with any fields is not readonly.
    /// <para>
    /// Format: <c>"The shader of type {0} is not readonly (shaders cannot mutate their instance state while running, so shader types not being readonly makes them error prone)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor NotReadOnlyShaderType = new(
        id: "CMPS0057",
        title: "Not readonly shader type (using IComputeShader or IComputeShader<T>)",
        messageFormat: "The shader of type {0} is not readonly (shaders cannot mutate their instance state while running, so shader types not being readonly makes them error prone)",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Shader types should be readonly (shaders cannot mutate their instance state while running, so shader types not being readonly makes them error prone).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for when a field annotated with <c>[GloballyCoherent]</c> is not valid.
    /// <para>
    /// Format: <c>"The field "{0}" is annotated with [GloballyCoherent], but is not a valid target for it (only ReadWriteBuffer&lt;T&gt; instance fields in compute shader types can be annotated with [GloballyCoherent])"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGloballyCoherentFieldDeclaration = new(
        id: "CMPS0058",
        title: "Invalid [GloballyCoherent] field declaration",
        messageFormat: """The field "{0}" is annotated with [GloballyCoherent], but is not a valid target for it (only ReadWriteBuffer<T> instance fields in compute shader types can be annotated with [GloballyCoherent])""",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The [GloballyCoherent] attribute is only valid on ReadWriteBuffer<T> instance fields in compute shader types.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an initializer expression.
    /// </summary>
    public static readonly DiagnosticDescriptor InitializerExpression = new(
        id: "CMPS0059",
        title: "Initializer expression",
        messageFormat: "An initializer expression cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "An initializer expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a collection expression.
    /// </summary>
    public static readonly DiagnosticDescriptor CollectionExpression = new(
        id: "CMPS0060",
        title: "Collection expression",
        messageFormat: "A collection expression cannot be used in a compute shader",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A collection expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a constructor with a base constructor declaration.
    /// <para>
    /// Format: <c>"The constructor {0} has a base constructor declaration, which cannot be used in a shader (only standalone constructors are allowed)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidBaseConstructorDeclaration = new(
        id: "CMPS0061",
        title: "Invalid base constructor declaration",
        messageFormat: "The constructor {0} has a base constructor declaration, which cannot be used in a shader (only standalone constructors are allowed)",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Only standalone constructors (with no base constructor declaration) can be used in a shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a <see langword="this"/> expression.
    /// </summary>
    public static readonly DiagnosticDescriptor ThisExpression = new(
        id: "CMPS0062",
        title: "Invalid 'this' expression",
        messageFormat: "A compute shader cannot use a 'this' expression outside of member accesses (such as 'this.field')",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot use a 'this' expression outside of member accesses (such as 'this.field').",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invocation of a <c>Math</c> or <c>MathF</c> API.
    /// <para>
    /// Format: <c>"The method {0} cannot be used in a shader, use equivalent APIs from the Hlsl type instead"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidMathOrMathFCall = new(
        id: "CMPS0063",
        title: "Invalid Math or MathF invocation from a shader",
        messageFormat: "The method {0} cannot be used in a shader, use equivalent APIs from the Hlsl type instead",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Methods from the Math and MathF types cannot be used in a shader, and equivalent APIs from the Hlsl type should be used instead.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader missing [RequiresDoublePrecisionSupport].
    /// <para>
    /// Format: <c>"The shader {0} requires double precision support, but it does not have the [RequiresDoublePrecisionSupport] attribute on it (adding the attribute is necessary to explicitly opt-in to that functionality)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingRequiresDoublePrecisionSupportAttribute = new(
        id: "CMPS0064",
        title: "Missing [RequiresDoublePrecisionSupport] attribute",
        messageFormat: "The shader {0} requires double precision support, but it does not have the [RequiresDoublePrecisionSupport] attribute on it (adding the attribute is necessary to explicitly opt-in to that functionality)",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Shaders performing double precision operations must be annotated with [RequiresDoublePrecisionSupport] to explicitly opt-in to that functionality.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader is unnecessarily using [RequiresDoublePrecisionSupportAttribute].
    /// <para>
    /// Format: <c>"The shader {0} does not require double precision support, but it has the [RequiresDoublePrecisionSupport] attribute on it (using the attribute is not needed if the shader is not performing any double precision operations)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor UnnecessaryRequiresDoublePrecisionSupportAttribute = new(
        id: "CMPS0065",
        title: "Unnecessary [RequiresDoublePrecisionSupport] attribute",
        messageFormat: "The shader {0} does not require double precision support, but it has the [RequiresDoublePrecisionSupport] attribute on it (using the attribute is not needed if the shader is not performing any double precision operations)",
        category: "ComputeSharp.Shaders",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Shaders not performing any double precision operations should not be annotated with [RequiresDoublePrecisionSupport], as the attribute is not needed in that case.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");
}