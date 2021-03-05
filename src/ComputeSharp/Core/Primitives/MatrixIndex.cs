namespace ComputeSharp
{
    /// <summary>
    /// A type indicating a base-1 index into a matrix type. This can be used to extract references to swizzled vectors within a given matrix value.
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-per-component-math"/>.
    /// <para>This type is meant to be used within a <see langword="using static"/> directive to simplify the code:</para>
    /// <para>
    /// <c>
    /// using static ComputeSharp.MatrixIndex;
    /// Float2x2 matrix = 0;
    /// Float2 row = matrix[M11, M12];
    /// </c>
    /// </para>
    /// </summary>
    public enum MatrixIndex
    {
        /// <summary>
        /// The first item in the first row.
        /// </summary>
        M11,

        /// <summary>
        /// The second item in the first row.
        /// </summary>
        M12,

        /// <summary>
        /// The third item in the first row.
        /// </summary>
        M13,

        /// <summary>
        /// The fourth item in the first row.
        /// </summary>
        M14,

        /// <summary>
        /// The first item in the second row.
        /// </summary>
        M21,

        /// <summary>
        /// The second item in the second row.
        /// </summary>
        M22,

        /// <summary>
        /// The third item in the second row.
        /// </summary>
        M23,

        /// <summary>
        /// The fourth item in the second row.
        /// </summary>
        M24,

        /// <summary>
        /// The first item in the third row.
        /// </summary>
        M31,

        /// <summary>
        /// The second item in the third row.
        /// </summary>
        M32,

        /// <summary>
        /// The third item in the third row.
        /// </summary>
        M33,

        /// <summary>
        /// The fourth item in the third row.
        /// </summary>
        M34,

        /// <summary>
        /// The first item in the fourth row.
        /// </summary>
        M41,

        /// <summary>
        /// The second item in the fourth row.
        /// </summary>
        M42,

        /// <summary>
        /// The third item in the fourth row.
        /// </summary>
        M43,

        /// <summary>
        /// The fourth item in the fourth row.
        /// </summary>
        M44
    }
}
