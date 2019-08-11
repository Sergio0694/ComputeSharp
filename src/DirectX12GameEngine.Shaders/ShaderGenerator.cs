using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using DirectX12GameEngine.Graphics.Buffers.Abstract;
using DirectX12GameEngine.Shaders.Mappings;
using DirectX12GameEngine.Shaders.Primitives;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SharpDX.Direct3D12;
using Vector2 = System.Numerics.Vector2;
using Vector4 = System.Numerics.Vector4;

namespace DirectX12GameEngine.Shaders
{
    public class ShaderGenerator
    {
        private static readonly object compilationLock = new object();
        private static readonly Dictionary<string, CSharpDecompiler> decompilers = new Dictionary<string, CSharpDecompiler>();
        private static readonly IEnumerable<PEFile> peFiles;
        private static readonly Dictionary<Type, (PEFile PEFile, TypeDefinition TypeDefinition)> typeDefinitions = new Dictionary<Type, (PEFile, TypeDefinition)>();

        private static Compilation compilation;

        private readonly List<ShaderTypeDefinition> collectedTypes = new List<ShaderTypeDefinition>();
        private readonly BindingFlags bindingAttr;
        private readonly HlslBindingTracker bindingTracker = new HlslBindingTracker();
        private readonly object shader;
        private readonly StringWriter stringWriter = new StringWriter();
        private readonly IndentedTextWriter writer;

        private ShaderGenerationResult? result;

        static ShaderGenerator()
        {
            IEnumerable<string> assemblyPaths;

            if (!string.IsNullOrEmpty(Assembly.GetEntryAssembly().Location))
            {
                assemblyPaths = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).Select(a => a.Location);
            }
            else
            {
                assemblyPaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll").Where(p =>
                {
                    try
                    {
                        PEFile peFile = new PEFile(p);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });
            }

            var metadataReferences = assemblyPaths.Select(p => MetadataReference.CreateFromFile(p));
            peFiles = assemblyPaths.Select(p => new PEFile(p));

            compilation = CSharpCompilation.Create("ShaderAssembly").WithReferences(metadataReferences);

            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }

        private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs e)
        {
            if (!e.LoadedAssembly.IsDynamic)
            {
                PortableExecutableReference metadataReference = MetadataReference.CreateFromFile(e.LoadedAssembly.Location);
                compilation = compilation.AddReferences(metadataReference);
            }
        }

        public ShaderGenerator(object shader, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
        {
            this.shader = shader;
            this.bindingAttr = bindingAttr;

            writer = new IndentedTextWriter(stringWriter);
        }

        public bool IsGenerated => result != null;

        public void AddType(Type type)
        {
            CollectStructure(type, null);
        }

        public RootParameter[] RootParameters { get; private set; }

        public IReadOnlyList<GraphicsResource> ReadWriteBuffers { get; private set; }

        public ShaderGenerationResult GenerateShaderForLambda()
        {
            if (result != null) return result;

            if (!(shader is Action<ThreadIds> action)) throw new InvalidOperationException("Missing lambda shader");

            Type shaderType = action.Method.DeclaringType;
            object shaderInstance = action.Target;

            var fields = shaderType.GetFields().ToArray();

            // Collect the fields
            foreach (FieldInfo fieldInfo in fields)
            {
                CollectStructure(fieldInfo.FieldType, fieldInfo.GetValue(shaderInstance));
            }

            // Collect the method
            CollectTopLevelMethod(action.Method);

            // Writing stage
            foreach (ShaderTypeDefinition type in collectedTypes)
            {
                WriteStructure(type.Type, type.Instance);
            }

            // Write the fields, assuming that all buffers are unordered access views, and the other fields are constant buffers
            var parameters = new List<DescriptorRange>();
            var rwbuffers = new List<GraphicsResource>();
            foreach (FieldInfo fieldInfo in fields)
            {
                Type? memberType = fieldInfo.FieldType;

                if (memberType.IsGenericType && memberType.GetGenericTypeDefinition() == typeof(RWBufferResource<>))
                {
                    var range = new DescriptorRange(DescriptorRangeType.UnorderedAccessView, 1, bindingTracker.UnorderedAccessView);
                    parameters.Add(range);
                    var buffer = fieldInfo.GetValue(shaderInstance);
                    var resource = (GraphicsResource)memberType.GetProperty(nameof(RWBufferResource<byte>.Buffer)).GetValue(buffer);
                    rwbuffers.Add(resource);
                    WriteUnorderedAccessView(fieldInfo, memberType, bindingTracker.UnorderedAccessView++);
                }
                //else WriteConstantBuffer(fieldInfo, memberType, bindingTracker.ConstantBuffer++);
                else WriteStaticVariable(fieldInfo, memberType, fieldInfo.GetValue(shaderInstance));
            }
            RootParameters = parameters.Select(range => new RootParameter(ShaderVisibility.All, range)).ToArray();
            ReadWriteBuffers = rwbuffers;

            // Write the actual shader body
            writer.WriteLine("[Shader(\"compute\")]");
            writer.WriteLine("[NumThreads(5, 5, 1)]");
            WriteTopLevelMethod(action.Method);

            stringWriter.GetStringBuilder().TrimEnd();
            writer.WriteLine();

            result = new ShaderGenerationResult(stringWriter.ToString(), "Foo"); /* action.Method.Name */

            return result;
        }

        private void CollectStructure(Type type, object? obj)
        {
            type = type.GetElementOrDeclaredType();

            if (HlslKnownTypes.IsKnownType(type) || collectedTypes.Any(d => d.Type == type)) return;

            ShaderTypeDefinition shaderTypeDefinition = new ShaderTypeDefinition(type, obj);

            if (type.IsEnum)
            {
                collectedTypes.Add(shaderTypeDefinition);
                return;
            }

            Type parentType = type.BaseType;

            while (parentType != null)
            {
                CollectStructure(parentType, obj);
                parentType = parentType.BaseType;
            }

            foreach (Type interfaceType in type.GetInterfaces())
            {
                CollectStructure(interfaceType, obj);
            }

            var memberInfos = new MemberInfo[0]; //type.GetMembersInOrder(bindingAttr);

            foreach (MemberInfo memberInfo in memberInfos)
            {
                Type? memberType = memberInfo.GetMemberType(obj);

                if (memberInfo is MethodInfo methodInfo)
                {
                    if (type.IsInterface)
                    {
                        CollectMethod(methodInfo);
                    }
                }
                else if (memberType != null && memberType != type)
                {
                    CollectStructure(memberType, memberInfo.GetMemberValue(obj));
                }
            }

            collectedTypes.Add(shaderTypeDefinition);
        }

        private void WriteStructure(Type type, object? obj)
        {
            if (type.IsEnum)
            {
                writer.WriteLine($"enum class {type.Name}");
            }
            else if (type.IsInterface)
            {
                writer.WriteLine($"interface {type.Name}");
            }
            else
            {
                writer.Write($"struct {type.Name}");

                if (type.BaseType != null && type.BaseType != typeof(object) && type.BaseType != typeof(ValueType))
                {
                    writer.Write($" : {type.BaseType.Name}, ");

                    // NOTE: Types might no expose every interface method.

                    //foreach (Type interfaceType in type.GetInterfaces())
                    //{
                    //    writer.Write(interfaceType.Name + ", ");
                    //}

                    stringWriter.GetStringBuilder().Length -= 2;
                }

                writer.WriteLine();
            }

            writer.WriteLine("{");
            writer.Indent++;

            //var fieldAndPropertyInfos = type.GetMembersInOrder(bindingAttr | BindingFlags.DeclaredOnly).Where(m => m is FieldInfo || m is PropertyInfo);
            //var methodInfos = type.GetMembersInTypeHierarchyInOrder(bindingAttr).Where(m => m is MethodInfo);
            //var memberInfos = fieldAndPropertyInfos.Concat(methodInfos);

            foreach (MemberInfo memberInfo in new MemberInfo[0])
            {
                Type? memberType = memberInfo.GetMemberType(obj);

                if (memberInfo is MethodInfo methodInfo)
                {
                    if (type.IsInterface)
                    {
                        WriteMethod(methodInfo);
                    }
                }
                else if (memberType != null)
                {
                    if (type.IsEnum)
                    {
                        writer.Write(memberInfo.Name);
                        writer.WriteLine(",");
                    }
                    else
                    {
                        WriteStructureField(memberInfo, memberType);
                    }
                }
            }

            stringWriter.GetStringBuilder().TrimEnd();

            writer.Indent--;
            writer.WriteLine();
            writer.WriteLine("};");
            writer.WriteLine();

            if (type.IsEnum) return;

            foreach (MemberInfo memberInfo in new MemberInfo[0].Where(m => m.IsStatic()))
            {
                Type? memberType = memberInfo.GetMemberType(obj);

                if (memberType != null)
                {
                    WriteStaticStructureField(memberInfo, memberType);
                }
            }
        }

        private void WriteStructureField(MemberInfo memberInfo, Type memberType)
        {
            if (memberInfo.IsStatic())
            {
                writer.Write("static");
                writer.Write(" ");
            }

            writer.Write($"{HlslKnownTypes.GetMappedName(memberType)} {memberInfo.Name}");

            int arrayCount = memberType.IsArray ? 2 : 0;
            writer.Write(GetArrayString(arrayCount));

            writer.WriteLine(";");
            writer.WriteLine();
        }

        private void WriteStaticStructureField(MemberInfo memberInfo, Type memberType)
        {
            string declaringType = HlslKnownTypes.GetMappedName(memberInfo.DeclaringType);
            writer.WriteLine($"static {HlslKnownTypes.GetMappedName(memberType)} {declaringType}::{memberInfo.Name};");
            writer.WriteLine();
        }

        private void WriteStaticVariable(MemberInfo memberInfo, Type memberType, object value)
        {
            writer.Write($"static {HlslKnownTypes.GetMappedName(memberType)} {memberInfo.Name}");
            writer.Write($" = {value};");
            writer.WriteLine();
        }

        private void WriteConstantBuffer(MemberInfo memberInfo, Type memberType, int binding)
        {
            int arrayCount = memberType.IsArray ? 2 : 0;

            writer.Write($"cbuffer {memberInfo.Name}Buffer");
            writer.WriteLine($" : register(b{binding})");
            writer.WriteLine("{");
            writer.Indent++;
            writer.WriteLine($"{HlslKnownTypes.GetMappedName(memberType)} {memberInfo.Name}{GetArrayString(arrayCount)};");
            writer.Indent--;
            writer.WriteLine("}");
            writer.WriteLine();
        }

        private void WriteUnorderedAccessView(MemberInfo memberInfo, Type memberType, int binding)
        {
            writer.Write($"{HlslKnownTypes.GetMappedName(memberType)} {memberInfo.Name}");
            writer.Write(" : SV_DispatchThreadId");
            writer.Write($" : register(u{binding})");
            writer.WriteLine(";");
            writer.WriteLine();
        }

        private static string GetArrayString(int arrayCount) => arrayCount > 0 ? $"[{arrayCount}]" : "";

        private void CollectTopLevelMethod(MethodInfo methodInfo)
        {
            IList<MethodInfo> methodInfos = methodInfo.GetBaseMethods();

            for (int depth = methodInfos.Count - 1; depth >= 0; depth--)
            {
                MethodInfo currentMethodInfo = methodInfos[depth];
                CollectMethod(currentMethodInfo);
            }
        }

        private void CollectMethod(MethodInfo methodInfo)
        {
            GetSyntaxTree(methodInfo, out SyntaxNode root, out SemanticModel semanticModel);

            ShaderSyntaxCollector syntaxCollector = new ShaderSyntaxCollector(this, semanticModel);
            syntaxCollector.Visit(root);
        }

        private void WriteTopLevelMethod(MethodInfo methodInfo)
        {
            IList<MethodInfo> methodInfos = methodInfo.GetBaseMethods();

            for (int depth = methodInfos.Count - 1; depth >= 0; depth--)
            {
                MethodInfo currentMethodInfo = methodInfos[depth];
                WriteMethod(currentMethodInfo, depth);
            }
        }

        private void WriteMethod(MethodInfo methodInfo, int depth = 0)
        {
            GetSyntaxTree(methodInfo, out SyntaxNode root, out SemanticModel semanticModel);

            ShaderSyntaxRewriter syntaxRewriter = new ShaderSyntaxRewriter(semanticModel, true, depth);
            root = syntaxRewriter.Visit(root);

            string shaderSource = root.ToFullString();
            shaderSource = shaderSource.Replace("internal void <Main>b__0", "void Foo");
            shaderSource = shaderSource.Replace("uint3 id", "uint3 id : SV_DispatchThreadId");

            // TODO: See why the System namespace in System.Math is not present in UWP projects.
            shaderSource = shaderSource.Replace("Math.Max", "max");
            shaderSource = shaderSource.Replace("Math.Pow", "pow");
            shaderSource = shaderSource.Replace("Math.Sin", "sin");

            shaderSource = shaderSource.Replace("vector", "vec");
            shaderSource = Regex.Replace(shaderSource, @"\d+[fF]", m => m.Value.Replace("f", ""));

            // Indent every line
            string indent = "";

            for (int i = 0; i < writer.Indent; i++)
            {
                indent += IndentedTextWriter.DefaultTabString;
            }

            shaderSource = shaderSource.Replace(Environment.NewLine, Environment.NewLine + indent).TrimEnd(' ');

            writer.WriteLine(shaderSource);
        }

        private static void GetSyntaxTree(MethodInfo methodInfo, out SyntaxNode root, out SemanticModel semanticModel)
        {
            lock (compilationLock)
            {
                GetMethodHandle(methodInfo, out string assemblyPath, out EntityHandle methodHandle);

                if (!decompilers.TryGetValue(assemblyPath, out CSharpDecompiler decompiler))
                {
                    decompiler = CreateDecompiler(assemblyPath);
                    decompilers.Add(assemblyPath, decompiler);
                }

                string sourceCode = decompiler.DecompileAsString(methodHandle);

                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
                root = syntaxTree.GetRoot();

                compilation = compilation.AddSyntaxTrees(syntaxTree);
                semanticModel = compilation.GetSemanticModel(syntaxTree);
            }
        }

        private static void GetMethodHandle(MethodInfo methodInfo, out string assemblyPath, out EntityHandle methodHandle)
        {
            assemblyPath = methodInfo.DeclaringType.Assembly.Location;

            if (!string.IsNullOrEmpty(assemblyPath))
            {
                methodHandle = MetadataTokenHelpers.TryAsEntityHandle(methodInfo.MetadataToken) ?? throw new InvalidOperationException();
            }
            else
            {
                if (!typeDefinitions.TryGetValue(methodInfo.DeclaringType, out var tuple))
                {
                    foreach (PEFile peFile in peFiles)
                    {
                        TypeDefinitionHandle typeDefinitionHandle = peFile.Metadata.TypeDefinitions.FirstOrDefault(t => t.GetFullTypeName(peFile.Metadata).ToString() == methodInfo.DeclaringType.FullName);

                        if (!typeDefinitionHandle.IsNil)
                        {
                            tuple = (peFile, peFile.Metadata.GetTypeDefinition(typeDefinitionHandle));
                            typeDefinitions.Add(methodInfo.DeclaringType, tuple);

                            break;
                        }
                    }

                    if (tuple.PEFile is null) throw new InvalidOperationException();
                }

                PEFile peFileForMethod = tuple.PEFile;
                TypeDefinition typeDefinition = tuple.TypeDefinition;

                assemblyPath = peFileForMethod.FileName;

                methodHandle = typeDefinition.GetMethods()
                    .Where(m => peFileForMethod.Metadata.StringComparer.Equals(peFileForMethod.Metadata.GetMethodDefinition(m).Name, methodInfo.Name))
                    .First(m => peFileForMethod.Metadata.GetMethodDefinition(m).GetParameters().Count == methodInfo.GetParameters().Length);
            }
        }

        private static CSharpDecompiler CreateDecompiler(string assemblyPath)
        {
            UniversalAssemblyResolver resolver = new UniversalAssemblyResolver(assemblyPath, false, "netstandard");

            DecompilerSettings decompilerSettings = new DecompilerSettings(ICSharpCode.Decompiler.CSharp.LanguageVersion.Latest)
            {
                ObjectOrCollectionInitializers = false,
                UsingDeclarations = false
            };

            decompilerSettings.CSharpFormattingOptions.IndentationString = IndentedTextWriter.DefaultTabString;

            return new CSharpDecompiler(assemblyPath, resolver, decompilerSettings);
        }

        internal static class HlslKnownMethods
        {
            private static readonly Dictionary<string, string> knownMethods = new Dictionary<string, string>()
            {
                { "System.Math.Cos", "cos" },
                { "System.MathF.Cos", "cos" },
                { "System.Math.Max", "max" },
                { "System.Math.Pow", "pow" },
                { "System.MathF.Pow", "pow" },
                { "System.Math.Sin", "sin" },
                { "System.MathF.Sin", "sin" },
                { "System.Math.PI", "3.1415926535897931" },
                { "System.MathF.PI", "3.14159274f" },

                { "DirectX12GameEngine.Shaders.Numerics.Vector2.Length", "length" },

                { "DirectX12GameEngine.Shaders.Numerics.UInt2.X", ".x" },
                { "DirectX12GameEngine.Shaders.Numerics.UInt2.Y", ".y" },

                { "DirectX12GameEngine.Shaders.Numerics.ThreadIds.X", ".x" },
                { "DirectX12GameEngine.Shaders.Numerics.ThreadIds.Y", ".y" },
                { "DirectX12GameEngine.Shaders.Numerics.ThreadIds.Z", ".z" },
                { "DirectX12GameEngine.Shaders.Numerics.ThreadIds.XY", ".xy" },

                { "System.Numerics.Vector3.X", ".x" },
                { "System.Numerics.Vector3.Y", ".y" },
                { "System.Numerics.Vector3.Z", ".z" },
                { "System.Numerics.Vector3.Cross", "cross" },
                { "System.Numerics.Vector3.Dot", "dot" },
                { "System.Numerics.Vector3.Lerp", "lerp" },
                { "System.Numerics.Vector3.Transform", "mul" },
                { "System.Numerics.Vector3.TransformNormal", "mul" },
                { "System.Numerics.Vector3.Normalize", "normalize" },
                { "System.Numerics.Vector3.Zero", "(float3)0" },
                { "System.Numerics.Vector3.One", "float3(1.0f, 1.0f, 1.0f)" },
                { "System.Numerics.Vector3.UnitX", "float3(1.0f, 0.0f, 0.0f)" },
                { "System.Numerics.Vector3.UnitY", "float3(0.0f, 1.0f, 0.0f)" },
                { "System.Numerics.Vector3.UnitZ", "float3(0.0f, 0.0f, 1.0f)" },

                { "System.Numerics.Vector4.X", ".x" },
                { "System.Numerics.Vector4.Y", ".y" },
                { "System.Numerics.Vector4.Z", ".z" },
                { "System.Numerics.Vector4.W", ".w" },
                { "System.Numerics.Vector4.Lerp", "lerp" },
                { "System.Numerics.Vector4.Transform", "mul" },
                { "System.Numerics.Vector4.Normalize", "normalize" },
                { "System.Numerics.Vector4.Zero", "(float4)0" },
                { "System.Numerics.Vector4.One", "float4(1.0f, 1.0f, 1.0f, 1.0f)" },

                { "System.Numerics.Matrix4x4.Multiply", "mul" },
                { "System.Numerics.Matrix4x4.Transpose", "transpose" },
                { "System.Numerics.Matrix4x4.Translation", "[3].xyz" },
                { "System.Numerics.Matrix4x4.M11", "[0][0]" },
                { "System.Numerics.Matrix4x4.M12", "[0][1]" },
                { "System.Numerics.Matrix4x4.M13", "[0][2]" },
                { "System.Numerics.Matrix4x4.M14", "[0][3]" },
                { "System.Numerics.Matrix4x4.M21", "[1][0]" },
                { "System.Numerics.Matrix4x4.M22", "[1][1]" },
                { "System.Numerics.Matrix4x4.M23", "[1][2]" },
                { "System.Numerics.Matrix4x4.M24", "[1][3]" },
                { "System.Numerics.Matrix4x4.M31", "[2][0]" },
                { "System.Numerics.Matrix4x4.M32", "[2][1]" },
                { "System.Numerics.Matrix4x4.M33", "[2][2]" },
                { "System.Numerics.Matrix4x4.M34", "[2][3]" },
                { "System.Numerics.Matrix4x4.M41", "[3][0]" },
                { "System.Numerics.Matrix4x4.M42", "[3][1]" },
                { "System.Numerics.Matrix4x4.M43", "[3][2]" },
                { "System.Numerics.Matrix4x4.M44", "[3][3]" }
            };

            public static bool Contains(ISymbol containingMemberSymbol, ISymbol memberSymbol)
            {
                string fullTypeName = containingMemberSymbol.IsStatic ? containingMemberSymbol.ToString() : memberSymbol.ContainingType.ToString();

                if (knownMethods.ContainsKey(fullTypeName + Type.Delimiter + memberSymbol.Name))
                {
                    return true;
                }

                return false;
            }

            public static string? GetMappedName(ISymbol containingMemberSymbol, ISymbol memberSymbol)
            {
                string fullTypeName = containingMemberSymbol.IsStatic ? containingMemberSymbol.ToString() : memberSymbol.ContainingType.ToString();

                if (knownMethods.TryGetValue(fullTypeName + Type.Delimiter + memberSymbol.Name, out string mapped))
                {
                    if (!memberSymbol.IsStatic)
                    {
                        return containingMemberSymbol.Name + mapped;
                    }

                    return mapped;
                }

                if (memberSymbol.IsStatic)
                {
                    return containingMemberSymbol.Name + "::" + memberSymbol.Name;
                }

                return null;
            }
        }

        private class HlslBindingTracker
        {
            public int ConstantBuffer { get; set; }

            public int Sampler { get; set; }

            public int Texture { get; set; }

            public int UnorderedAccessView { get; set; }

            public int StaticResource { get; set; }
        }

        private class ShaderTypeDefinition
        {
            public ShaderTypeDefinition(Type type, object? instance)
            {
                Type = type;
                Instance = instance;
            }

            public object? Instance { get; }

            public Type Type { get; }

            public List<ResourceDefinition> ResourceDefinitions { get; } = new List<ResourceDefinition>();
        }

        private class ResourceDefinition
        {
            public ResourceDefinition(Type memberType)
            {
                MemberType = memberType;
            }

            public Type MemberType { get; }
        }

        private class FakeMemberInfo : MemberInfo
        {
            public FakeMemberInfo(string name)
            {
                Name = name;
            }

            public override Type DeclaringType => throw new NotImplementedException();

            public override MemberTypes MemberType => MemberTypes.Field;

            public override string Name { get; }

            public override Type ReflectedType => throw new NotImplementedException();

            public override object[] GetCustomAttributes(bool inherit)
            {
                return Array.Empty<object>();
            }

            public override object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                return Array.Empty<object>();
            }

            public override bool IsDefined(Type attributeType, bool inherit)
            {
                return false;
            }
        }
    }
}
