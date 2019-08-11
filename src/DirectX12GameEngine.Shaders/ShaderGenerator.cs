using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DirectX12GameEngine.Graphics.Buffers.Abstract;
using DirectX12GameEngine.Shaders.Mappings;
using DirectX12GameEngine.Shaders.Primitives;
using Microsoft.CodeAnalysis;
using SharpDX.Direct3D12;

namespace DirectX12GameEngine.Shaders
{
    public class ShaderGenerator
    {
        private readonly List<ShaderTypeDefinition> collectedTypes = new List<ShaderTypeDefinition>();
        private readonly BindingFlags bindingAttr;
        private readonly HlslBindingTracker bindingTracker = new HlslBindingTracker();
        private readonly object shader;
        private readonly StringWriter stringWriter = new StringWriter();
        private readonly IndentedTextWriter writer;

        private ShaderGenerationResult? result;

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

            result = new ShaderGenerationResult(stringWriter.ToString(), "CSMain"); /* action.Method.Name */

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
            MethodDecompiler.Instance.GetSyntaxTree(methodInfo, out SyntaxNode root, out SemanticModel semanticModel);

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
            MethodDecompiler.Instance.GetSyntaxTree(methodInfo, out SyntaxNode root, out SemanticModel semanticModel);

            ShaderSyntaxRewriter syntaxRewriter = new ShaderSyntaxRewriter(semanticModel, true, depth);
            root = syntaxRewriter.Visit(root);

            string shaderSource = root.ToFullString();
            shaderSource = shaderSource.Replace("internal void <Main>b__0", "void CSMain");
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

        private class HlslBindingTracker
        {
            public int ConstantBuffer { get; set; }

            public int UnorderedAccessView { get; set; }
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
        }
    }
}
