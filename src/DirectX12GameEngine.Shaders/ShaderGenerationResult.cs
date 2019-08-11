using System;
using System.Reflection;

namespace DirectX12GameEngine.Shaders
{
    public class ShaderGenerationResult
    {
        public ShaderGenerationResult(string shaderSource, string computeShader)
        {
            ShaderSource = shaderSource;
            ComputeShader = computeShader;
        }

        public string ShaderSource { get; }

        public string ComputeShader { get; set; }
    }
}
