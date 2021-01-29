namespace ComputeSharp
{
    /// <summary>
    /// An <see langword="enum"/> that indicates a mode to use when allocating resources.
    /// </summary>
    public enum AllocationMode
    {
        /// <summary>
        /// The default allocation mode for graphics resources. Allocated buffers are not explicitly cleared when created.
        /// They can still be cleared for security reasons in some situations, but this step will be skipped whenever possible.
        /// As such, always make sure not to read directly to buffers created this way before writing data to them first.
        /// </summary>
        Default,

        /// <summary>
        /// Clear allocated buffers when creating them.
        /// </summary>
        Clear
    }
}
