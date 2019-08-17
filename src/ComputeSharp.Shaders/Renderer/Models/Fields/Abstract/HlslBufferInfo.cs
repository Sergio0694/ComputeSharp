using System;

namespace ComputeSharp.Shaders.Renderer.Models.Fields.Abstract
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a shader buffer field
    /// </summary>
    internal abstract class HlslBufferInfo : CapturedFieldInfo
    {
        /// <summary>
        /// Gets or sets the index of the current buffer, relative to its own type
        /// </summary>
        public int BufferIndex { get; set; }

        /// <summary>
        /// Creates a new <see cref="HlslBufferInfo"/> instance with the specified parameters
        /// </summary>
        /// <param name="fieldCsharpType">The type of the current field in the C# source</param>
        /// <param name="fieldHlslType">The type of the current field in the HLSL shader</param>
        /// <param name="fieldName">The name of the current field</param>
        /// <param name="bufferIndex">The index of the current buffer field</param>
        protected HlslBufferInfo(Type fieldCsharpType, string fieldHlslType, string fieldName, int bufferIndex) : base(fieldCsharpType, fieldHlslType, fieldName)
        {
            BufferIndex = bufferIndex;
        }
    }
}
