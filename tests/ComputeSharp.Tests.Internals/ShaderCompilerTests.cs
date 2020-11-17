using ComputeSharp.Shaders.Translation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TerraFX.Interop;

namespace ComputeSharp.Tests.Internals
{
    [TestClass]
    [TestCategory("ShaderCompiler")]
    public class ShaderCompilerTests
    {
        private const string ShaderSource = @"
        /* ===============
         * AUTO GENERATED
         * ===============
         * This shader was created by ComputeSharp.
         * For info or issues: https://github.com/Sergio0694/ComputeSharp */

        // Scalar/vector variables
        cbuffer _ : register(b0)
        {
            uint __x__; // Target X iterations
            uint __y__; // Target Y iterations
            uint __z__; // Target Z iterations
            int width;
        }

        // ReadWriteBuffer<float> buffer ""buffer""
        RWStructuredBuffer<float> buffer : register(u1);

        // Shader body
        [Shader(""compute"")]
        [NumThreads(32, 1, 1)]
        void CSMain(uint3 ids : SV_DispatchThreadId)
        {
            if (ids.x < __x__ &&
                ids.y < __y__ &&
                ids.z < __z__) // Automatic bounds check
            {
                int i = ids.x + ids.y * width;
                buffer[i] *= 2;
            }
        }
        ";

        [TestMethod]
        public void CompileTest()
        {
            using ComPtr<IDxcBlob> dxcBlob = ShaderCompiler.CompileShader(ShaderSource);
        }
    }
}
