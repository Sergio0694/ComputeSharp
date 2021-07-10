using System;
using System.ComponentModel;

namespace ComputeSharp.__Internals
{
    /// <summary>
    /// A base <see langword="interface"/> representing a metadatadata loader for a shader being dispatched.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("This interface is not intended to be used directly by user code")]
    public interface IDispatchMetadataLoader
    {
        /// <summary>
        /// Loads an opaque metadata handle from the given shader metadata.
        /// </summary>
        /// <param name="serializedMetadata">The serialized metadata for the current shader.</param>
        /// <param name="resourceDescriptors">The sequence of resource descriptors for the current shader.</param>
        /// <param name="result">The resulting opaque metadata handle.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("This method is not intended to be called directly by user code")]
        void LoadMetadataHandle(ReadOnlySpan<byte> serializedMetadata, ReadOnlySpan<ResourceDescriptor> resourceDescriptors, out IntPtr result);
    }
}
