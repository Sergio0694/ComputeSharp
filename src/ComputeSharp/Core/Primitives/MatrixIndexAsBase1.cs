namespace ComputeSharp
{
    /// <summary>
    /// A type indicating a base-1 index into a matrix type. This can be used to extract references to swizzled vectors within a given matrix value.
    /// For more info, see <see href="https://docs.microsoft.com/windows/win32/direct3dhlsl/dx-graphics-hlsl-per-component-math"/>.
    /// <para>This type is meant to be used within a <see langword="using statuc"/> directive to simplify the code:</para>
    /// <para>
    /// <c>
    /// using static ComputeSharp.MatrixIndexAsBase1;
    /// Float2x2 matrix = 0;
    /// Float2 row = matrix[_11, _22];
    /// </c>
    /// </para>
    /// </summary>
    public enum MatrixIndexAsBase1
    {
        /// <summary>
        /// The first item in the first row.
        /// </summary>
        _11,

        /// <summary>
        /// The second item in the first row.
        /// </summary>
        _12,

        /// <summary>
        /// The third item in the first row.
        /// </summary>
        _13,

        /// <summary>
        /// The fourth item in the first row.
        /// </summary>
        _14,

        /// <summary>
        /// The first item in the second row.
        /// </summary>
        _21,

        /// <summary>
        /// The second item in the second row.
        /// </summary>
        _22,

        /// <summary>
        /// The third item in the second row.
        /// </summary>
        _23,

        /// <summary>
        /// The fourth item in the second row.
        /// </summary>
        _24,

        /// <summary>
        /// The first item in the third row.
        /// </summary>
        _31,

        /// <summary>
        /// The second item in the third row.
        /// </summary>
        _32,

        /// <summary>
        /// The third item in the third row.
        /// </summary>
        _33,

        /// <summary>
        /// The fourth item in the third row.
        /// </summary>
        _34,

        /// <summary>
        /// The first item in the fourth row.
        /// </summary>
        _41,

        /// <summary>
        /// The second item in the fourth row.
        /// </summary>
        _42,

        /// <summary>
        /// The third item in the fourth row.
        /// </summary>
        _43,

        /// <summary>
        /// The fourth item in the fourth row.
        /// </summary>
        _44
    }
}
