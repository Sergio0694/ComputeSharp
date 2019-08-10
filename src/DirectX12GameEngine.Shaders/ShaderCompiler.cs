using System;
using System.IO;
using DotNetDxc;
using SharpDX.D3DCompiler;

namespace DirectX12GameEngine.Shaders
{
    public static class ShaderCompiler
    {
        static ShaderCompiler()
        {
            HlslDxcLib.DxcCreateInstanceFn = DefaultDxcLib.GetDxcCreateInstanceFn();
        }

        public static byte[] CompileShaderFile(string filePath, ShaderVersion? version = null, string? entryPoint = null)
        {
            string shaderSource = File.ReadAllText(filePath);
            return CompileShader(shaderSource, version, entryPoint, filePath);
        }

        public static byte[] CompileShader(string shaderSource, ShaderVersion? version = null, string? entryPoint = null, string filePath = "")
        {
            IDxcCompiler compiler = HlslDxcLib.CreateDxcCompiler();
            IDxcLibrary library = HlslDxcLib.CreateDxcLibrary();

            const uint CP_UTF16 = 1200;

            IDxcBlobEncoding sourceBlob = library.CreateBlobWithEncodingOnHeapCopy(shaderSource, (uint)(shaderSource.Length * 2), CP_UTF16);
            IDxcOperationResult result = compiler.Compile(sourceBlob, filePath, entryPoint ?? GetDefaultEntryPoint(version), $"{GetShaderProfile(version)}_6_1", new[] { "-Zpr" }, 1, null, 0, library.CreateIncludeHandler());

            if (result.GetStatus() == 0)
            {
                IDxcBlob blob = result.GetResult();
                byte[] bytecode = GetBytesFromBlob(blob);

                return bytecode;
            }
            else
            {
                string resultText = GetStringFromBlob(library, result.GetErrors());
                throw new Exception(resultText);
            }
        }

        public static byte[] CompileShaderLegacy(string shaderSource, ShaderVersion? version = null, string? entryPoint = null)
        {
            return ShaderBytecode.Compile(shaderSource, entryPoint ?? GetDefaultEntryPoint(version), $"{GetShaderProfile(version)}_5_1", ShaderFlags.PackMatrixRowMajor);
        }

        public static unsafe byte[] GetBytesFromBlob(IDxcBlob blob)
        {
            byte* pMem = (byte*)blob.GetBufferPointer();
            uint size = blob.GetBufferSize();
            byte[] result = new byte[size];

            fixed (byte* pTarget = result)
            {
                for (uint i = 0; i < size; i++)
                {
                    pTarget[i] = pMem[i];
                }
            }

            return result;
        }

        public static unsafe string GetStringFromBlob(IDxcLibrary library, IDxcBlob blob)
        {
            blob = library.GetBlobAstUf16(blob);
            return new string(blob.GetBufferPointer(), 0, (int)(blob.GetBufferSize() / 2));
        }

        private static string GetDefaultEntryPoint(ShaderVersion? version) => version switch
        {
            ShaderVersion.ComputeShader => "CSMain",
            ShaderVersion.VertexShader => "VSMain",
            ShaderVersion.PixelShader => "PSMain",
            ShaderVersion.HullShader => "HSMain",
            ShaderVersion.DomainShader => "DSMain",
            ShaderVersion.GeometryShader => "GSMain",
            _ => ""
        };

        private static string GetShaderProfile(ShaderVersion? version) => version switch
        {
            ShaderVersion.ComputeShader => "cs",
            ShaderVersion.VertexShader => "vs",
            ShaderVersion.PixelShader => "ps",
            ShaderVersion.HullShader => "hs",
            ShaderVersion.DomainShader => "ds",
            ShaderVersion.GeometryShader => "gs",
            _ => "lib"
        };
    }
}
