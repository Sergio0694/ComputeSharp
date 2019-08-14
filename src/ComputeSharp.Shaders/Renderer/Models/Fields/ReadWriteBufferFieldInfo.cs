using ComputeSharp.Shaders.Renderer.Models.Fields.Abstract;

namespace ComputeSharp.Shaders.Renderer.Models.Fields
{
    /// <summary>
    /// A <see langword="class"/> that contains info on a read write buffer field
    /// </summary>
    internal sealed class ReadWriteBufferFieldInfo : FieldInfoBase
    {
        /// <summary>
        /// Gets whether or not the current <see cref="FieldInfoBase"/> instance represents a read write buffer (always <see langword="true"/>)
        /// </summary>
        public bool IsReadWriteBuffer { get; } = true;

        /// <summary>
        /// Gets the index of the current read write buffer field
        /// </summary>
        public int ReadWriteBufferIndex { get; set; }

        /// <summary>
        /// Creates a new <see cref="ReadWriteBufferFieldInfo"/> instance with the specified parameters
        /// </summary>
        /// <param name="fieldType">The type of the current field</param>
        /// <param name="fieldName">The name of the current field</param>
        /// <param name="bufferIndex">The index of the current read write buffer field</param>
        public ReadWriteBufferFieldInfo(string fieldType, string fieldName, int bufferIndex) : base(fieldType, fieldName)
        {
            ReadWriteBufferIndex = bufferIndex;
        }
    }
}
