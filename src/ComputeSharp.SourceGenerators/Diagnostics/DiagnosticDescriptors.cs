using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGenerators.Diagnostics;

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
    public static readonly DiagnosticDescriptor InvalidShaderField = new(
        id: "CMPS0001",
        title: "Invalid shader field",
        messageFormat: "The compute shader of type {0} contains a field \"{1}\" of an invalid type {2}",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A type representing a compute shader contains a field of a type that is not supported in HLSL.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid group shared field type.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains a group shared field \"{1}\" of an invalid type {2}"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGroupSharedFieldType = new DiagnosticDescriptor(
        id: "CMPS0002",
        title: "Invalid group shared field type",
        messageFormat: "The compute shader of type {0} contains a group shared field \"{1}\" of an invalid type {2} (it must be an array)",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A group shared field must be of an array type.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid group shared field element type.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains a group shared field \"{1}\" of an invalid element type {2}"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGroupSharedFieldElementType = new DiagnosticDescriptor(
        id: "CMPS0003",
        title: "Invalid group shared field element type",
        messageFormat: "The compute shader of type {0} contains a group shared field \"{1}\" of an invalid type {2} (it must be a primitive or unmanaged type)",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A group shared field element must be of a primitive or unmanaged type.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid group shared field declaration.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains a group shared field \"{1}\" that is not static"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGroupSharedFieldDeclaration = new DiagnosticDescriptor(
        id: "CMPS0004",
        title: "Invalid group shared field declaration",
        messageFormat: "The compute shader of type {0} contains a group shared field \"{1}\" that is not static",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A group shared field must be static.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader with no resources.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains no resources to work on"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingShaderResources = new DiagnosticDescriptor(
        id: "CMPS0005",
        title: "Missing shader resources",
        messageFormat: "The compute shader of type {0} contains no resources to work on",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader must contain at least one resource.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="ThreadIds"/> usage.
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidThreadIdsUsage = new DiagnosticDescriptor(
        id: "CMPS0006",
        title: "Invalid ThreadIds usage",
        messageFormat: "The ThreadIds type can only be used within the main body of a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The ThreadIds type can only be used within the main body of a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="GroupIds"/> usage.
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGroupIdsUsage = new DiagnosticDescriptor(
        id: "CMPS0007",
        title: "Invalid GroupIds usage",
        messageFormat: "The GroupIds type can only be used within the main body of a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The GroupIds type can only be used within the main body of a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="GroupSize"/> usage.
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGroupSizeUsage = new DiagnosticDescriptor(
        id: "CMPS0008",
        title: "Invalid GroupSize usage",
        messageFormat: "The GroupSize type can only be used within the main body of a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The GroupSize type can only be used within the main body of a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="GridIds"/> usage.
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidGridIdsUsage = new DiagnosticDescriptor(
        id: "CMPS0009",
        title: "Invalid GridIds usage",
        messageFormat: "The GridIds type can only be used within the main body of a compute shader",
        category: typeof(IComputeShader).FullName,
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
    public static readonly DiagnosticDescriptor InvalidObjectCreationExpression = new DiagnosticDescriptor(
        id: "CMPS0010",
        title: "Invalid object creation expression",
        messageFormat: "The type {0} cannot be created in a compute shader (only unmanaged types are supported)",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Only unmanaged value type objects can be created in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an anonymous object creation expression.
    /// </summary>
    public static readonly DiagnosticDescriptor AnonymousObjectCreationExpression = new DiagnosticDescriptor(
        id: "CMPS0011",
        title: "Anonymous object creation expression",
        messageFormat: "An anonymous object cannot be created in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "An anonymous object cannot be created in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an async modifier on a method or function.
    /// </summary>
    public static readonly DiagnosticDescriptor AsyncModifierOnMethodOrFunction = new DiagnosticDescriptor(
        id: "CMPS0012",
        title: "Async modifier on method or function",
        messageFormat: "The async modifier cannot be used in methods or functions used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The async modifier cannot be used in methods or functions used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an await expression.
    /// </summary>
    public static readonly DiagnosticDescriptor AwaitExpression = new DiagnosticDescriptor(
        id: "CMPS0013",
        title: "Await expression",
        messageFormat: "The await expression cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The await expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a checked expression.
    /// </summary>
    public static readonly DiagnosticDescriptor CheckedExpression = new DiagnosticDescriptor(
        id: "CMPS0014",
        title: "Checked expression",
        messageFormat: "A checked expression cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A checked expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a checked statement.
    /// </summary>
    public static readonly DiagnosticDescriptor CheckedStatement = new DiagnosticDescriptor(
        id: "CMPS0015",
        title: "Checked statement",
        messageFormat: "A checked statement cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A checked statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a fixed statement.
    /// </summary>
    public static readonly DiagnosticDescriptor FixedStatement = new DiagnosticDescriptor(
        id: "CMPS0016",
        title: "Fixed statement",
        messageFormat: "A fixed statement cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A fixed statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a foreach statement.
    /// </summary>
    public static readonly DiagnosticDescriptor ForEachStatement = new DiagnosticDescriptor(
        id: "CMPS0017",
        title: "Foreach statement",
        messageFormat: "A foreach statement cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A foreach statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a lock statement.
    /// </summary>
    public static readonly DiagnosticDescriptor LockStatement = new DiagnosticDescriptor(
        id: "CMPS0018",
        title: "Foreach statement",
        messageFormat: "A lock statement cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A lock statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a query statement.
    /// </summary>
    public static readonly DiagnosticDescriptor QueryExpression = new DiagnosticDescriptor(
        id: "CMPS0019",
        title: "Foreach statement",
        messageFormat: "A LINQ query expression cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A LINQ query expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a range expression.
    /// </summary>
    public static readonly DiagnosticDescriptor RangeExpression = new DiagnosticDescriptor(
        id: "CMPS0020",
        title: "Range expression",
        messageFormat: "A range expression cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A range expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a recursive pattern.
    /// </summary>
    public static readonly DiagnosticDescriptor RecursivePattern = new DiagnosticDescriptor(
        id: "CMPS0021",
        title: "Recursive pattern",
        messageFormat: "A recursive pattern cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A recursive pattern cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a ref type.
    /// </summary>
    public static readonly DiagnosticDescriptor RefType = new DiagnosticDescriptor(
        id: "CMPS0022",
        title: "Ref type",
        messageFormat: "A compute shader cannot have a ref type declaration",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have a ref type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a relational pattern.
    /// </summary>
    public static readonly DiagnosticDescriptor RelationalPattern = new DiagnosticDescriptor(
        id: "CMPS0023",
        title: "Relational pattern",
        messageFormat: "A relational pattern cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A relational pattern cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a sizeof expression.
    /// </summary>
    public static readonly DiagnosticDescriptor SizeOfExpression = new DiagnosticDescriptor(
        id: "CMPS0024",
        title: "Sizeof expression",
        messageFormat: "A sizeof expression cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A sizeof expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a stackalloc expression.
    /// </summary>
    public static readonly DiagnosticDescriptor StackAllocArrayCreationExpression = new DiagnosticDescriptor(
        id: "CMPS0025",
        title: "Stackalloc expression",
        messageFormat: "A stackalloc expression cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A stackalloc expression cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a throw expression or statement.
    /// </summary>
    public static readonly DiagnosticDescriptor ThrowExpressionOrStatement = new DiagnosticDescriptor(
        id: "CMPS0026",
        title: "Throw expression or statement",
        messageFormat: "Throw expressions and statements cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Throw expressions and statements cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a try statement.
    /// </summary>
    public static readonly DiagnosticDescriptor TryStatement = new DiagnosticDescriptor(
        id: "CMPS0027",
        title: "Try statement",
        messageFormat: "A try statement cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A try statement cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a tuple type.
    /// </summary>
    public static readonly DiagnosticDescriptor TupleType = new DiagnosticDescriptor(
        id: "CMPS0028",
        title: "Tuple type",
        messageFormat: "A compute shader cannot have a tuple type declaration",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have a tuple type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a using statement or declaration.
    /// </summary>
    public static readonly DiagnosticDescriptor UsingStatementOrDeclaration = new DiagnosticDescriptor(
        id: "CMPS0029",
        title: "Using statement or declaration",
        messageFormat: "Using statements and declarations cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Using statements and declarations cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a yield statement.
    /// </summary>
    public static readonly DiagnosticDescriptor YieldStatement = new DiagnosticDescriptor(
        id: "CMPS0030",
        title: "Yield statement",
        messageFormat: "A yield statement cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
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
        id: "CMPS0031",
        title: "Invalid object declaration",
        messageFormat: "A variable of type {0} cannot be declared in a compute shader (only unmanaged types are supported)",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Only unmanaged value type objects can be declared in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a pointer type.
    /// </summary>
    public static readonly DiagnosticDescriptor PointerType = new DiagnosticDescriptor(
        id: "CMPS0032",
        title: "Pointer type",
        messageFormat: "A compute shader cannot have a pointer type declaration",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have a pointer type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a function pointer type.
    /// </summary>
    public static readonly DiagnosticDescriptor FunctionPointer = new DiagnosticDescriptor(
        id: "CMPS0033",
        title: "Function pointer type",
        messageFormat: "A compute shader cannot have a function pointer type declaration",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have a function pointer type declaration.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an unsafe statement.
    /// </summary>
    public static readonly DiagnosticDescriptor UnsafeStatement = new DiagnosticDescriptor(
        id: "CMPS0034",
        title: "Unsafe statement",
        messageFormat: "A compute shader cannot have an unsafe statement",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A compute shader cannot have an unsafe statement.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an unsafe modifier on a method or function.
    /// </summary>
    public static readonly DiagnosticDescriptor UnsafeModifierOnMethodOrFunction = new DiagnosticDescriptor(
        id: "CMPS0035",
        title: "Unsafe modifier on method or function",
        messageFormat: "The unsafe modifier cannot be used in methods or functions used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The unsafe modifier cannot be used in methods or functions used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a string literal.
    /// </summary>
    public static readonly DiagnosticDescriptor StringLiteralExpression = new DiagnosticDescriptor(
        id: "CMPS0036",
        title: "String literal expression",
        messageFormat: "String literal expressions cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "String literal expressions cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an incorrect matrix swizzling property argument.
    /// </summary>
    public static readonly DiagnosticDescriptor NonConstantMatrixSwizzledIndex = new DiagnosticDescriptor(
        id: "CMPS0037",
        title: "Non constant matrix swizzled property argument",
        messageFormat: "The arguments in a swizzled indexer for a matrix type must be compile-time constants",
        category: typeof(ShaderMethodSourceGenerator).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The arguments in a swizzled indexer for a matrix type must be compile-time constants.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid shader static field type.
    /// <para>
    /// Format: <c>"The compute shader of type {0} contains a static field \"{1}\" of an invalid type {2} (only primitive, vector and matrix types are supported)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidShaderStaticFieldType = new DiagnosticDescriptor(
        id: "CMPS0038",
        title: "Invalid shader static field type",
        messageFormat: "The compute shader of type {0} contains a static field \"{1}\" of an invalid type {2} (only primitive, vector and matrix types are supported)",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A type representing a compute shader contains a static field of a type that is not supported.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="DispatchSize"/> usage.
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidDispatchSizeUsage = new DiagnosticDescriptor(
        id: "CMPS0039",
        title: "Invalid DispatchSize usage",
        messageFormat: "The DispatchSize type can only be used within the main body of a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The DispatchSize type can only be used within the main body of a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a property declaration.
    /// </summary>
    public static readonly DiagnosticDescriptor PropertyDeclaration = new DiagnosticDescriptor(
        id: "CMPS0040",
        title: "Property declaration",
        messageFormat: "Property declarations cannot be used in a compute shader",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Property declarations cannot be used in a compute shader.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader with a root signature that is too large.
    /// <para>
    /// Format: <c>"The compute shader of type {0} has exceeded the maximum allowed size for captured values and resources"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor ShaderDispatchDataSizeExceeded = new DiagnosticDescriptor(
        id: "CMPS0041",
        title: "Shader dispatch data size exceeded",
        messageFormat: "The compute shader of type {0} has exceeded the maximum allowed size for captured values and resources",
        category: typeof(IComputeShader).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The compute shader of type {0} has exceeded the maximum allowed size for captured values and resources.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a type implementing multiple shader interfaces.
    /// <para>
    /// Format: <c>"The shader of type {0} cannot implement more than one shader interface"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MultipleShaderTypesImplemented = new DiagnosticDescriptor(
        id: "CMPS0042",
        title: "Multiple shader implementations for type declaration",
        messageFormat: "The shader of type {0} cannot implement more than one shader interface",
        category: typeof(IShaderGenerator).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A shader type cannot implement more than one shader interface.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a dynamic shader annotated as an embedded bytecode.
    /// <para>
    /// Format: <c>"The shader of type {0} cannot be precompiled, as it is dynamic (it captures delegate fields)"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor EmbeddedBytecodeWithDynamicShader = new DiagnosticDescriptor(
        id: "CMPS0043",
        title: "Embedded bytecode for dynamic shader",
        messageFormat: "The shader of type {0} cannot be precompiled, as it is dynamic (it captures delegate fields)",
        category: typeof(IShaderGenerator).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "A shader type cannot be precompiled if it captures one or more delegate values.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an embedded bytecode shader with invalid thread ids values.
    /// <para>
    /// Format: <c>"The shader of type {0} is annotated with invalid thread ids values ({1}, {2}, {3})"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidEmbeddedBytecodeThreadIds = new DiagnosticDescriptor(
        id: "CMPS0044",
        title: "Invalid thread ids for shader with embedded bytecode",
        messageFormat: "The shader of type {0} is annotated with invalid thread ids values ({1}, {2}, {3})",
        category: typeof(IShaderGenerator).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The thread ids values for a shader marked as embedded bytecode have to be in the valid range.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an embedded bytecode shader failed due to a Win32 exception.
    /// <para>
    /// Format: <c>"The shader of type {0} failed to compile due to a Win32 exception (HRESULT: {1:X8}, Message: "{2}")"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor EmbeddedBytecodeFailedWithWin32Exception = new DiagnosticDescriptor(
        id: "CMPS0045",
        title: "Embedded bytecode compilation failed due to Win32 exception",
        messageFormat: "The shader of type {0} failed to compile due to a Win32 exception (HRESULT: {1:X8}, Message: \"{2}\")",
        category: typeof(IShaderGenerator).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The embedded bytecode for a shader failed to be compiled due to a Win32 exception.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an embedded bytecode shader failed due to an HLSL compilation exception.
    /// <para>
    /// Format: <c>"The shader of type {{0}} failed to compile due to an HLSL compiler error (Message: "{1}")"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor EmbeddedBytecodeFailedWithHlslCompilationException = new DiagnosticDescriptor(
        id: "CMPS0046",
        title: "Embedded bytecode compilation failed due to an HLSL compiler error",
        messageFormat: "The shader of type {0} failed to compile due to an HLSL compiler error (Message: \"{1}\")",
        category: typeof(IShaderGenerator).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The embedded bytecode for a shader failed to be compiled due to an HLSL compiler error.",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for a shader without the embedded bytecode attribute, when dynamic shaders are not supported.
    /// <para>
    /// Format: <c>"The shader of type {0} needs to be annotated as having embedded bytecode (using the [EmbeddedBytecode] attribute), as dynamic shader compilation is not supported"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor MissingEmbeddedBytecodeAttributeWhenDynamicShaderCompilationIsNotSupported = new DiagnosticDescriptor(
        id: "CMPS0047",
        title: "Embedded bytecode compilation failed due to an HLSL compiler error",
        messageFormat: "The shader of type {0} needs to be annotated as having embedded bytecode (using the [EmbeddedBytecode] attribute), as dynamic shader compilation is not supported",
        category: typeof(IShaderGenerator).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "When dynamic shader compilation is not supported (ie. when ComputeSharp.Dynamic is not referenced), all shaders need to be annotated as having embedded bytecode (using the [EmbeddedBytecode] attribute).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> for an embedded bytecode shader with an invalid dispatch axis value.
    /// <para>
    /// Format: <c>"The shader of type {0} is annotated with an invalid dispatch axis value"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor InvalidEmbeddedBytecodeDispatchAxis = new DiagnosticDescriptor(
        id: "CMPS0048",
        title: "Invalid dispatch axis for shader with embedded bytecode",
        messageFormat: "The shader of type {0} is annotated with with an invalid dispatch axis value",
        category: typeof(IShaderGenerator).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The dispatch axis value for a shader marked as embedded bytecode have to be valid (flags are not supported).",
        helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");
}
