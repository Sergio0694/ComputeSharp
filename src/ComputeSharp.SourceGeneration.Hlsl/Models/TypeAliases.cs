#pragma warning disable IDE0005

// Shared type aliases for transparent models for gathered HLSL info. These are used
// when writing the transpiled HLSL. No need for concrete types for any of these, so
// just using type aliases keeps things simpler while still making the code explicit.
global using HlslConstant = (string Name, string Value);
global using HlslUserType = (string Name, string Definition);
global using HlslResourceField = (string MetadataName, string Name, string Type);
global using HlslValueField = (string Name, string Type);
global using HlslResourceTextureField = (string Name, string Type, int Index);
global using HlslStaticField = (string Name, string TypeDeclaration, string? Assignment);
global using HlslSharedBuffer = (string Name, string Type, int? Count);
global using HlslMethod = (string Signature, string Declaration);