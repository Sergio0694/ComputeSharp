using Microsoft.CodeAnalysis;

namespace ComputeSharp.SourceGenerators.Diagnostics
{
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
        public static DiagnosticDescriptor InvalidShaderField = new(
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
        public static DiagnosticDescriptor InvalidGroupSharedFieldType = new(
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
        public static DiagnosticDescriptor InvalidGroupSharedFieldElementType = new(
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
        public static DiagnosticDescriptor InvalidGroupSharedFieldDeclaration = new(
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
        public static DiagnosticDescriptor MissingShaderResources = new(
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
        /// <para>
        /// Format: <c>"The ThreadIds type is used in method {0} of type {1}"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidThreadIdsUsage = new(
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
        /// <para>
        /// Format: <c>"The GroupIds type is used in method {0} of type {1}"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidGroupIdsUsage = new(
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
        /// <para>
        /// Format: <c>"The GroupSize type is used in method {0} of type {1}"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidGroupSizeUsage = new(
            id: "CMPS0008",
            title: "Invalid GroupSize usage",
            messageFormat: "The GroupSize type can only be used within the main body of a compute shader",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "The GroupSize type can only be used within the main body of a compute shader.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

        /// <summary>
        /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid <see cref="WarpIds"/> usage.
        /// <para>
        /// Format: <c>"The WarpIds type is used in method {0} of type {1}"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidWarpIdsUsage = new(
            id: "CMPS0009",
            title: "Invalid WarpIds usage",
            messageFormat: "The WarpIds type can only be used within the main body of a compute shader",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "The WarpIds type can only be used within the main body of a compute shader.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");

        /// <summary>
        /// Gets a <see cref="DiagnosticDescriptor"/> for an invalid object creation expression.
        /// <para>
        /// Format: <c>"The type {0} cannot be created in a compute shader"</c>.
        /// </para>
        /// </summary>
        public static DiagnosticDescriptor InvalidObjectCreationExpression = new(
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
        public static DiagnosticDescriptor AnonymousObjectCreationExpression = new(
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
        public static DiagnosticDescriptor AsyncModifierOnMethodOrFunction = new(
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
        public static DiagnosticDescriptor AwaitExpression = new(
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
        public static DiagnosticDescriptor CheckedExpression = new(
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
        public static DiagnosticDescriptor CheckedStatement = new(
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
        public static DiagnosticDescriptor FixedStatement = new(
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
        public static DiagnosticDescriptor ForEachStatement = new(
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
        public static DiagnosticDescriptor LockStatement = new(
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
        public static DiagnosticDescriptor QueryExpression = new(
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
        public static DiagnosticDescriptor RangeExpression = new(
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
        public static DiagnosticDescriptor RecursivePattern = new(
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
        public static DiagnosticDescriptor RefType = new(
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
        public static DiagnosticDescriptor RelationalPattern = new(
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
        public static DiagnosticDescriptor SizeOfExpression = new(
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
        public static DiagnosticDescriptor StackAllocArrayCreationExpression = new(
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
        public static DiagnosticDescriptor ThrowExpressionOrStatement = new(
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
        public static DiagnosticDescriptor TryStatement = new(
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
        public static DiagnosticDescriptor TupleType = new(
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
        public static DiagnosticDescriptor UsingStatementOrDeclaration = new(
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
        public static DiagnosticDescriptor YieldStatement = new(
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
        public static DiagnosticDescriptor InvalidObjectDeclaration = new(
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
        public static DiagnosticDescriptor PointerType = new(
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
        public static DiagnosticDescriptor FunctionPointer = new(
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
        public static DiagnosticDescriptor UnsafeStatement = new(
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
        public static DiagnosticDescriptor UnsafeModifierOnMethodOrFunction = new(
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
        public static DiagnosticDescriptor StringLiteralExpression = new(
            id: "CMPS0036",
            title: "String literal expression",
            messageFormat: "String literal expressions cannot be used in a compute shader",
            category: typeof(IComputeShader).FullName,
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: "String literal expressions cannot be used in a compute shader.",
            helpLinkUri: "https://github.com/Sergio0694/ComputeSharp");
    }
}
