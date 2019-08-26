using System.Diagnostics.Contracts;
using System.Numerics;
using ComputeSharp.Graphics.Exceptions;

namespace ComputeSharp
{
    /// <summary>
    /// A <see langword="class"/> that maps the supported HLSL intrinsic functions that can be used in compute shaders
    /// </summary>
    public static class Hlsl
    {
        /// <summary>
        /// Returns the absolute value of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The absolute value of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static int Abs(in int x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Abs)}(int)");

        /// <summary>
        /// Returns the absolute value of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The absolute value of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Int2 Abs(in Int2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Abs)}({nameof(Int2)})");

        /// <summary>
        /// Returns the absolute value of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The absolute value of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Int3 Abs(in Int3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Abs)}({nameof(Int3)})");

        /// <summary>
        /// Returns the absolute value of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The absolute value of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Int4 Abs(in Int4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Abs)}({nameof(Int4)})");

        /// <summary>
        /// Returns the absolute value of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The absolute value of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Abs(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Abs)}(float)");

        /// <summary>
        /// Returns the absolute value of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The absolute value of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Abs(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Abs)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the absolute value of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The absolute value of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Abs(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Abs)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the absolute value of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The absolute value of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector4 Abs(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Abs)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the arccosine of the specified value
        /// </summary>
        /// <param name="x">The specified value, which should be within the range [-1 to 1]</param>
        /// <returns>The arccosine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Acos(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Acos)}(float)");

        /// <summary>
        /// Returns the arccosine of the specified value
        /// </summary>
        /// <param name="x">The specified value, where each component should be within the range [-1 to 1]</param>
        /// <returns>The arccosine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Acos(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Acos)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the arccosine of the specified value
        /// </summary>
        /// <param name="x">The specified value, where each component should be within the range [-1 to 1]</param>
        /// <returns>The arccosine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Acos(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Acos)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the arccosine of the specified value
        /// </summary>
        /// <param name="x">The specified value, where each component should be within the range [-1 to 1]</param>
        /// <returns>The arccosine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Acos(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Acos)}({nameof(Vector4)})");

        /// <summary>
        /// Determines if all components of the specified value are non-zero
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns><see langword="true"/> if all components of the <paramref name="x"/> parameter are non-zero; otherwise, <see langword="false"/></returns>
        [Pure]
        public static bool All(in Int2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(All)}({nameof(Int2)})");

        /// <summary>
        /// Determines if all components of the specified value are non-zero
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns><see langword="true"/> if all components of the <paramref name="x"/> parameter are non-zero; otherwise, <see langword="false"/></returns>
        [Pure]
        public static bool All(in Int3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(All)}({nameof(Int3)})");

        /// <summary>
        /// Determines if all components of the specified value are non-zero
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns><see langword="true"/> if all components of the <paramref name="x"/> parameter are non-zero; otherwise, <see langword="false"/></returns>
        [Pure]
        public static bool All(in Int4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(All)}({nameof(Int4)})");

        /// <summary>
        /// Determines if all components of the specified value are non-zero
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns><see langword="true"/> if all components of the <paramref name="x"/> parameter are non-zero; otherwise, <see langword="false"/></returns>
        [Pure]
        public static bool All(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(All)}({nameof(Vector2)})");

        /// <summary>
        /// Determines if all components of the specified value are non-zero
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns><see langword="true"/> if all components of the <paramref name="x"/> parameter are non-zero; otherwise, <see langword="false"/></returns>
        [Pure]
        public static bool All(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(All)}({nameof(Vector3)})");

        /// <summary>
        /// Determines if all components of the specified value are non-zero
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns><see langword="true"/> if all components of the <paramref name="x"/> parameter are non-zero; otherwise, <see langword="false"/></returns>
        [Pure]
        public static bool All(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(All)}({nameof(Vector4)})");

        /// <summary>
        /// Determines if any components of the specified value are non-zero
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns><see langword="true"/> if any components of the <paramref name="x"/> parameter are non-zero; otherwise, <see langword="false"/></returns>
        [Pure]
        public static bool Any(in Int2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Any)}({nameof(Int2)})");

        /// <summary>
        /// Determines if any components of the specified value are non-zero
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns><see langword="true"/> if any components of the <paramref name="x"/> parameter are non-zero; otherwise, <see langword="false"/></returns>
        [Pure]
        public static bool Any(in Int3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Any)}({nameof(Int3)})");

        /// <summary>
        /// Determines if any components of the specified value are non-zero
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns><see langword="true"/> if any components of the <paramref name="x"/> parameter are non-zero; otherwise, <see langword="false"/></returns>
        [Pure]
        public static bool Any(in Int4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Any)}({nameof(Int4)})");

        /// <summary>
        /// Determines if any components of the specified value are non-zero
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns><see langword="true"/> if any components of the <paramref name="x"/> parameter are non-zero; otherwise, <see langword="false"/></returns>
        [Pure]
        public static bool Any(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Any)}({nameof(Vector2)})");

        /// <summary>
        /// Determines if any components of the specified value are non-zero
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns><see langword="true"/> if any components of the <paramref name="x"/> parameter are non-zero; otherwise, <see langword="false"/></returns>
        [Pure]
        public static bool Any(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Any)}({nameof(Vector3)})");

        /// <summary>
        /// Determines if any components of the specified value are non-zero
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns><see langword="true"/> if any components of the <paramref name="x"/> parameter are non-zero; otherwise, <see langword="false"/></returns>
        [Pure]
        public static bool Any(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Any)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the arcsine of the specified value
        /// </summary>
        /// <param name="x">The specified value, which should be within the range [-1 to 1]</param>
        /// <returns>The arcsine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Asin(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Asin)}(float)");

        /// <summary>
        /// Returns the arcsine of the specified value
        /// </summary>
        /// <param name="x">The specified value, where each component should be within the range [-1 to 1]</param>
        /// <returns>The arcsine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Asin(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Asin)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the arcsine of the specified value
        /// </summary>
        /// <param name="x">The specified value, where each component should be within the range [-1 to 1]</param>
        /// <returns>The arcsine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Asin(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Asin)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the arcsine of the specified value
        /// </summary>
        /// <param name="x">The specified value, where each component should be within the range [-1 to 1]</param>
        /// <returns>The arcsine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Asin(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Asin)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the arctangent of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The arctangent of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Atan(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Atan)}(float)");

        /// <summary>
        /// Returns the arctangent of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The arctangent of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Atan(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Atan)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the arctangent of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The arctangent of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Atan(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Atan)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the arctangent of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The arctangent of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Atan(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Atan)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the arctangent of two values (<paramref name="y"/>,<paramref name="x"/>)
        /// </summary>
        /// <param name="y">The <paramref name="y"/> value</param>
        /// <param name="x">The <paramref name="x"/> value</param>
        /// <returns>The arctangent of (<paramref name="y"/>,<paramref name="x"/>)</returns>
        [Pure]
        public static float Atan2(in float y, in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Atan2)}(float,float)");

        /// <summary>
        /// Returns the arctangent of two values (<paramref name="y"/>,<paramref name="x"/>)
        /// </summary>
        /// <param name="y">The <paramref name="y"/> value</param>
        /// <param name="x">The <paramref name="x"/> value</param>
        /// <returns>The arctangent of (<paramref name="y"/>,<paramref name="x"/>)</returns>
        [Pure]
        public static Vector2 Atan2(in Vector2 y, in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Atan2)}({nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Returns the arctangent of two values (<paramref name="y"/>,<paramref name="x"/>)
        /// </summary>
        /// <param name="y">The <paramref name="y"/> value</param>
        /// <param name="x">The <paramref name="x"/> value</param>
        /// <returns>The arctangent of (<paramref name="y"/>,<paramref name="x"/>)</returns>
        [Pure]
        public static Vector3 Atan2(in Vector3 y, in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Atan2)}({nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Returns the arctangent of two values (<paramref name="y"/>,<paramref name="x"/>)
        /// </summary>
        /// <param name="y">The <paramref name="y"/> value</param>
        /// <param name="x">The <paramref name="x"/> value</param>
        /// <returns>The arctangent of (<paramref name="y"/>,<paramref name="x"/>)</returns>
        [Pure]
        public static Vector3 Atan2(in Vector4 y, in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Atan2)}({nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Returns the smallest integer value that is greater than or equal to the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The smallest integer value (returned as a floating-point type) that is greater than or equal to the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Ceil(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Ceil)}(float)");

        /// <summary>
        /// Returns the smallest integer value that is greater than or equal to the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The smallest integer value (returned as a floating-point type) that is greater than or equal to the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Ceil(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Ceil)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the smallest integer value that is greater than or equal to the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The smallest integer value (returned as a floating-point type) that is greater than or equal to the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Ceil(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Ceil)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the smallest integer value that is greater than or equal to the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The smallest integer value (returned as a floating-point type) that is greater than or equal to the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Ceil(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Ceil)}({nameof(Vector4)})");

        /// <summary>
        /// Clamps the specified value to the specified minimum and maximum range
        /// </summary>
        /// <param name="x">A value to clamp</param>
        /// <param name="min">The specified minimum range</param>
        /// <param name="max">The specified maximum range</param>
        /// <returns>The clamped value for the x parameter</returns>
        /// <remarks>For values of -INF or INF, clamp will behave as expected. However for values of NaN, the results are undefined</remarks>
        [Pure]
        public static int Clamp(in int x, in int min, in int max) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Clamp)}(int,int,int)");

        /// <summary>
        /// Clamps the specified value to the specified minimum and maximum range
        /// </summary>
        /// <param name="x">A value to clamp</param>
        /// <param name="min">The specified minimum range</param>
        /// <param name="max">The specified maximum range</param>
        /// <returns>The clamped value for the x parameter</returns>
        /// <remarks>For values of -INF or INF, clamp will behave as expected. However for values of NaN, the results are undefined</remarks>
        [Pure]
        public static Int2 Clamp(in Int2 x, in Int2 min, in Int2 max) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Clamp)}({nameof(Int2)},{nameof(Int2)},{nameof(Int2)})");

        /// <summary>
        /// Clamps the specified value to the specified minimum and maximum range
        /// </summary>
        /// <param name="x">A value to clamp</param>
        /// <param name="min">The specified minimum range</param>
        /// <param name="max">The specified maximum range</param>
        /// <returns>The clamped value for the x parameter</returns>
        /// <remarks>For values of -INF or INF, clamp will behave as expected. However for values of NaN, the results are undefined</remarks>
        [Pure]
        public static Int3 Clamp(in Int3 x, in Int3 min, in Int3 max) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Clamp)}({nameof(Int3)},{nameof(Int3)},{nameof(Int3)})");

        /// <summary>
        /// Clamps the specified value to the specified minimum and maximum range
        /// </summary>
        /// <param name="x">A value to clamp</param>
        /// <param name="min">The specified minimum range</param>
        /// <param name="max">The specified maximum range</param>
        /// <returns>The clamped value for the x parameter</returns>
        /// <remarks>For values of -INF or INF, clamp will behave as expected. However for values of NaN, the results are undefined</remarks>
        [Pure]
        public static Int4 Clamp(in Int4 x, in Int4 min, in Int4 max) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Clamp)}({nameof(Int4)},{nameof(Int4)},{nameof(Int4)})");

        /// <summary>
        /// Clamps the specified value to the specified minimum and maximum range
        /// </summary>
        /// <param name="x">A value to clamp</param>
        /// <param name="min">The specified minimum range</param>
        /// <param name="max">The specified maximum range</param>
        /// <returns>The clamped value for the x parameter</returns>
        /// <remarks>For values of -INF or INF, clamp will behave as expected. However for values of NaN, the results are undefined</remarks>
        [Pure]
        public static float Clamp(in float x, in float min, in float max) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Clamp)}(float,float,float)");

        /// <summary>
        /// Clamps the specified value to the specified minimum and maximum range
        /// </summary>
        /// <param name="x">A value to clamp</param>
        /// <param name="min">The specified minimum range</param>
        /// <param name="max">The specified maximum range</param>
        /// <returns>The clamped value for the x parameter</returns>
        /// <remarks>For values of -INF or INF, clamp will behave as expected. However for values of NaN, the results are undefined</remarks>
        [Pure]
        public static Vector2 Clamp(in Vector2 x, in Vector2 min, in Vector2 max) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Clamp)}({nameof(Vector2)},{nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Clamps the specified value to the specified minimum and maximum range
        /// </summary>
        /// <param name="x">A value to clamp</param>
        /// <param name="min">The specified minimum range</param>
        /// <param name="max">The specified maximum range</param>
        /// <returns>The clamped value for the x parameter</returns>
        /// <remarks>For values of -INF or INF, clamp will behave as expected. However for values of NaN, the results are undefined</remarks>
        [Pure]
        public static Vector3 Clamp(in Vector3 x, in Vector3 min, in Vector3 max) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Clamp)}({nameof(Vector3)},{nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Clamps the specified value to the specified minimum and maximum range
        /// </summary>
        /// <param name="x">A value to clamp</param>
        /// <param name="min">The specified minimum range</param>
        /// <param name="max">The specified maximum range</param>
        /// <returns>The clamped value for the x parameter</returns>
        /// <remarks>For values of -INF or INF, clamp will behave as expected. However for values of NaN, the results are undefined</remarks>
        [Pure]
        public static Vector4 Clamp(in Vector4 x, in Vector4 min, in Vector4 max) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Clamp)}({nameof(Vector4)},{nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Returns the cosine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The cosine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Cos(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Cos)}(float)");

        /// <summary>
        /// Returns the cosine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The cosine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Cos(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Cos)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the cosine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The cosine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Cos(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Cos)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the cosine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The cosine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Cos(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Cos)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the hyperbolic cosine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The hyperbolic cosine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Cosh(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Cosh)}(float)");

        /// <summary>
        /// Returns the hyperbolic cosine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The hyperbolic cosine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Cosh(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Cosh)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the hyperbolic cosine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The hyperbolic cosine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Cosh(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Cosh)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the hyperbolic cosine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The hyperbolic cosine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Cosh(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Cosh)}({nameof(Vector4)})");

        /// <summary>
        /// Counts the number of bits in the input value
        /// </summary>
        /// <param name="x">The input value</param>
        /// <returns>The number of bits</returns>
        [Pure]
        public static uint CountBits(in uint x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(CountBits)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the cross product of two <see cref="Vector3"/> values
        /// </summary>
        /// <param name="x">The first vector</param>
        /// <param name="y">The second vector</param>
        /// <returns>The cross product of the <paramref name="x"/> parameter and the <paramref name="y"/> parameter</returns>
        [Pure]
        public static Vector3 Cross(in Vector3 x, in Vector3 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Cross)}({nameof(Vector3)})");

        /// <summary>
        /// Converts the specified value from radians to degrees
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The result of converting the <paramref name="x"/> parameter from radians to degrees</returns>
        [Pure]
        public static float Degrees(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Degrees)}(float)");

        /// <summary>
        /// Converts the specified value from radians to degrees
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The result of converting the <paramref name="x"/> parameter from radians to degrees</returns>
        [Pure]
        public static Vector2 Degrees(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Degrees)}({nameof(Vector2)})");

        /// <summary>
        /// Converts the specified value from radians to degrees
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The result of converting the <paramref name="x"/> parameter from radians to degrees</returns>
        [Pure]
        public static Vector3 Degrees(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Degrees)}({nameof(Vector3)})");

        /// <summary>
        /// Converts the specified value from radians to degrees
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The result of converting the <paramref name="x"/> parameter from radians to degrees</returns>
        [Pure]
        public static Vector3 Degrees(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Degrees)}({nameof(Vector4)})");

        /// <summary>
        /// Returns a distance scalar between two <see cref="Vector2"/> values
        /// </summary>
        /// <param name="x">The first <see cref="Vector2"/> value to compare</param>
        /// <param name="y">The second <see cref="Vector2"/> value to compare</param>
        /// <returns>A scalar value that represents the distance between the <paramref name="x"/> parameter and the <paramref name="y"/> parameter</returns>
        [Pure]
        public static float Distance(in Vector2 x, in Vector2 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Distance)}({nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Returns a distance scalar between two <see cref="Vector3"/> values
        /// </summary>
        /// <param name="x">The first <see cref="Vector3"/> value to compare</param>
        /// <param name="y">The second <see cref="Vector3"/> value to compare</param>
        /// <returns>A scalar value that represents the distance between the <paramref name="x"/> parameter and the <paramref name="y"/> parameter</returns>
        [Pure]
        public static float Distance(in Vector3 x, in Vector3 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Distance)}({nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Returns a distance scalar between two <see cref="Vector4"/> values
        /// </summary>
        /// <param name="x">The first <see cref="Vector4"/> value to compare</param>
        /// <param name="y">The second <see cref="Vector4"/> value to compare</param>
        /// <returns>A scalar value that represents the distance between the <paramref name="x"/> parameter and the <paramref name="y"/> parameter</returns>
        [Pure]
        public static float Distance(in Vector4 x, in Vector4 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Distance)}({nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Returns the dot product of two <see cref="Vector2"/> values
        /// </summary>
        /// <param name="x">The first <see cref="Vector2"/> vector</param>
        /// <param name="y">The second <see cref="Vector2"/> vector</param>
        /// <returns>The dot product of the <paramref name="x"/> parameter and the <paramref name="y"/> parameter</returns>
        [Pure]
        public static float Dot(in Vector2 x, in Vector2 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Dot)}({nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Returns the dot product of two <see cref="Vector3"/> values
        /// </summary>
        /// <param name="x">The first <see cref="Vector3"/> vector</param>
        /// <param name="y">The second <see cref="Vector3"/> vector</param>
        /// <returns>The dot product of the <paramref name="x"/> parameter and the <paramref name="y"/> parameter</returns>
        [Pure]
        public static float Dot(in Vector3 x, in Vector3 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Dot)}({nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Returns the dot product of two <see cref="Vector4"/> values
        /// </summary>
        /// <param name="x">The first <see cref="Vector4"/> vector</param>
        /// <param name="y">The second <see cref="Vector4"/> vector</param>
        /// <returns>The dot product of the <paramref name="x"/> parameter and the <paramref name="y"/> parameter</returns>
        [Pure]
        public static float Dot(in Vector4 x, in Vector4 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Dot)}({nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Returns the base-e exponential of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-e exponential of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Exp(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Exp)}(float)");

        /// <summary>
        /// Returns the base-e exponential of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-e exponential of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Exp(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Exp)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the base-e exponential of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-e exponential of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Exp(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Exp)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the base-e exponential of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-e exponential of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector4 Exp(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Exp)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the base-2 exponential of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-2 exponential of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Exp2(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Exp2)}(float)");

        /// <summary>
        /// Returns the base-2 exponential of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-2 exponential of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Exp2(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Exp2)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the base-2 exponential of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-2 exponential of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Exp2(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Exp2)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the base-2 exponential of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-2 exponential of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector4 Exp2(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Exp2)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the largest integer that is less than or equal to the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The largest integer value (returned as a floating-point type) that is less than or equal to the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Floor(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Floor)}(float)");

        /// <summary>
        /// Returns the largest integer that is less than or equal to the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The largest integer value (returned as a floating-point type) that is less than or equal to the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Floor(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Floor)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the largest integer that is less than or equal to the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The largest integer value (returned as a floating-point type) that is less than or equal to the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Floor(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Floor)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the largest integer that is less than or equal to the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The largest integer value (returned as a floating-point type) that is less than or equal to the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector4 Floor(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Floor)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the floating-point remainder of <paramref name="x"/>/<paramref name="y"/>
        /// </summary>
        /// <param name="x">The divident</param>
        /// <param name="y">The divisor</param>
        /// <returns>The floating-point remainder of the <paramref name="x"/> parameter divided by the <paramref name="y"/> parameter</returns>
        [Pure]
        public static float Fmod(in float x, in float y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Fmod)}(float)");

        /// <summary>
        /// Returns the floating-point remainder of <paramref name="x"/>/<paramref name="y"/>
        /// </summary>
        /// <param name="x">The divident</param>
        /// <param name="y">The divisor</param>
        /// <returns>The floating-point remainder of the <paramref name="x"/> parameter divided by the <paramref name="y"/> parameter</returns>
        [Pure]
        public static Vector2 Fmod(in Vector2 x, in Vector2 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Fmod)}({nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Returns the floating-point remainder of <paramref name="x"/>/<paramref name="y"/>
        /// </summary>
        /// <param name="x">The divident</param>
        /// <param name="y">The divisor</param>
        /// <returns>The floating-point remainder of the <paramref name="x"/> parameter divided by the <paramref name="y"/> parameter</returns>
        [Pure]
        public static Vector3 Fmod(in Vector3 x, in Vector3 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Fmod)}({nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Returns the floating-point remainder of <paramref name="x"/>/<paramref name="y"/>
        /// </summary>
        /// <param name="x">The divident</param>
        /// <param name="y">The divisor</param>
        /// <returns>The floating-point remainder of the <paramref name="x"/> parameter divided by the <paramref name="y"/> parameter</returns>
        [Pure]
        public static Vector4 Fmod(in Vector4 x, in Vector4 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Fmod)}({nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Returns the fractional (or decimal) part of <paramref name="x"/>, which is greater than or equal to 0 and less than 1
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The fractional part of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Frac(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Frac)}(float)");

        /// <summary>
        /// Returns the fractional (or decimal) part of <paramref name="x"/>, which is greater than or equal to 0 and less than 1
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The fractional part of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Frac(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Frac)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the fractional (or decimal) part of <paramref name="x"/>, which is greater than or equal to 0 and less than 1
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The fractional part of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Frac(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Frac)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the fractional (or decimal) part of <paramref name="x"/>, which is greater than or equal to 0 and less than 1
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The fractional part of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector4 Frac(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Frac)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the mantissa and exponent of the specified floating-point value
        /// </summary>
        /// <param name="x">The specified floating-point value (if the x parameter is 0, this function returns 0 for both the mantissa and the exponent)</param>
        /// <param name="exp">The returned exponent of the <paramref name="x"/> parameter</param>
        /// <returns>The fractional part of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Frexp(in float x, out float exp) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Frexp)}(float,float)");

        /// <summary>
        /// Returns the mantissa and exponent of the specified floating-point value
        /// </summary>
        /// <param name="x">The specified floating-point value (if the x parameter is 0, this function returns 0 for both the mantissa and the exponent)</param>
        /// <param name="exp">The returned exponent of the <paramref name="x"/> parameter</param>
        /// <returns>The fractional part of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Frexp(in Vector2 x, out Vector2 exp) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Frexp)}({nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Returns the mantissa and exponent of the specified floating-point value
        /// </summary>
        /// <param name="x">The specified floating-point value (if the x parameter is 0, this function returns 0 for both the mantissa and the exponent)</param>
        /// <param name="exp">The returned exponent of the <paramref name="x"/> parameter</param>
        /// <returns>The fractional part of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Frexp(in Vector3 x, out Vector3 exp) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Frac)}({nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Returns the mantissa and exponent of the specified floating-point value
        /// </summary>
        /// <param name="x">The specified floating-point value (if the x parameter is 0, this function returns 0 for both the mantissa and the exponent)</param>
        /// <param name="exp">The returned exponent of the <paramref name="x"/> parameter</param>
        /// <returns>The fractional part of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector4 Frexp(in Vector4 x, out Vector4 exp) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Frexp)}({nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Determines if the specified floating-point value is finite
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>Returns <see langword="true"/> if the <paramref name="x"/> parameter is finite, otherwise <see langword="false"/></returns>
        [Pure]
        public static bool IsFinite(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(IsFinite)}(float)");

        /// <summary>
        /// Determines if the specified floating-point value is infinite
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>Returns <see langword="true"/> if the <paramref name="x"/> parameter is infinite, otherwise <see langword="false"/></returns>
        [Pure]
        public static float IsInfinite(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(IsInfinite)}(float)");

        /// <summary>
        /// Determines if the specified floating-point value is <see cref="float.NaN"/>
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>Returns <see langword="true"/> if the <paramref name="x"/> parameter is <see cref="float.NaN"/>, otherwise <see langword="false"/></returns>
        [Pure]
        public static float IsNaN(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(IsNaN)}(float)");

        /// <summary>
        /// Returns the result of multiplying the specified value by two, raised to the power of the specified exponent
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <param name="exp">The specified exponent</param>
        /// <returns>The result of multiplying the <paramref name="x"/> parameter by two, raised to the power of the <paramref name="exp"/> parameter</returns>
        [Pure]
        public static float Ldexp(in float x, in float exp) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Ldexp)}(float)");

        /// <summary>
        /// Returns the result of multiplying the specified value by two, raised to the power of the specified exponent
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <param name="exp">The specified exponent</param>
        /// <returns>The result of multiplying the <paramref name="x"/> parameter by two, raised to the power of the <paramref name="exp"/> parameter</returns>
        [Pure]
        public static Vector2 Ldexp(in Vector2 x, in Vector2 exp) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Ldexp)}({nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Returns the result of multiplying the specified value by two, raised to the power of the specified exponent
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <param name="exp">The specified exponent</param>
        /// <returns>The result of multiplying the <paramref name="x"/> parameter by two, raised to the power of the <paramref name="exp"/> parameter</returns>
        [Pure]
        public static Vector3 Ldexp(in Vector3 x, in Vector3 exp) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Ldexp)}({nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Returns the result of multiplying the specified value by two, raised to the power of the specified exponent
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <param name="exp">The specified exponent</param>
        /// <returns>The result of multiplying the <paramref name="x"/> parameter by two, raised to the power of the <paramref name="exp"/> parameter</returns>
        [Pure]
        public static Vector4 Ldexp(in Vector4 x, in Vector4 exp) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Ldexp)}({nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Performs a linear interpolation
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <param name="s">A value that linearly interpolates between the <paramref name="x"/> parameter and the <paramref name="y"/> parameter</param>
        /// <returns>The result of the linear interpolation</returns>
        [Pure]
        public static float Lerp(in float x, in float y, in float s) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Ldexp)}(float,float,float)");

        /// <summary>
        /// Performs a linear interpolation
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <param name="s">A value that linearly interpolates between the <paramref name="x"/> parameter and the <paramref name="y"/> parameter</param>
        /// <returns>The result of the linear interpolation</returns>
        [Pure]
        public static Vector2 Lerp(in Vector2 x, in Vector2 y, in Vector2 s) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Ldexp)}({nameof(Vector2)},{nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Performs a linear interpolation
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <param name="s">A value that linearly interpolates between the <paramref name="x"/> parameter and the <paramref name="y"/> parameter</param>
        /// <returns>The result of the linear interpolation</returns>
        [Pure]
        public static Vector3 Lerp(in Vector3 x, in Vector3 y, in Vector3 s) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Ldexp)}({nameof(Vector3)},{nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Performs a linear interpolation
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <param name="s">A value that linearly interpolates between the <paramref name="x"/> parameter and the <paramref name="y"/> parameter</param>
        /// <returns>The result of the linear interpolation</returns>
        [Pure]
        public static Vector2 Lerp(in Vector4 x, in Vector4 y, in Vector4 s) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Ldexp)}({nameof(Vector4)},{nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Returns the base-e logarithm of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-e logarithm of the <paramref name="x"/> parameter (if <paramref name="x"/> parameter is negative this function returns indefinite, and if <paramref name="x"/> is 0, this function returns -INF)</returns>
        [Pure]
        public static float Log(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Log)}(float)");

        /// <summary>
        /// Returns the base-e logarithm of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-e logarithm of the <paramref name="x"/> parameter (if <paramref name="x"/> parameter is negative this function returns indefinite, and if <paramref name="x"/> is 0, this function returns -INF)</returns>
        [Pure]
        public static Vector2 Log(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Log)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the base-e logarithm of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-e logarithm of the <paramref name="x"/> parameter (if <paramref name="x"/> parameter is negative this function returns indefinite, and if <paramref name="x"/> is 0, this function returns -INF)</returns>
        [Pure]
        public static Vector3 Log(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Log)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the base-e logarithm of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-e logarithm of the <paramref name="x"/> parameter (if <paramref name="x"/> parameter is negative this function returns indefinite, and if <paramref name="x"/> is 0, this function returns -INF)</returns>
        [Pure]
        public static Vector4 Log(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Log)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the base-10 logarithm of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-10 logarithm of the <paramref name="x"/> parameter (if <paramref name="x"/> parameter is negative this function returns indefinite, and if <paramref name="x"/> is 0, this function returns -INF)</returns>
        [Pure]
        public static float Log10(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Log10)}(float)");

        /// <summary>
        /// Returns the base-10 logarithm of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-10 logarithm of the <paramref name="x"/> parameter (if <paramref name="x"/> parameter is negative this function returns indefinite, and if <paramref name="x"/> is 0, this function returns -INF)</returns>
        [Pure]
        public static Vector2 Log10(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Log10)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the base-10 logarithm of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-10 logarithm of the <paramref name="x"/> parameter (if <paramref name="x"/> parameter is negative this function returns indefinite, and if <paramref name="x"/> is 0, this function returns -INF)</returns>
        [Pure]
        public static Vector3 Log10(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Log10)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the base-10 logarithm of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-10 logarithm of the <paramref name="x"/> parameter (if <paramref name="x"/> parameter is negative this function returns indefinite, and if <paramref name="x"/> is 0, this function returns -INF)</returns>
        [Pure]
        public static Vector4 Log10(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Log10)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the base-2 logarithm of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-2 logarithm of the <paramref name="x"/> parameter (if <paramref name="x"/> parameter is negative this function returns indefinite, and if <paramref name="x"/> is 0, this function returns -INF)</returns>
        [Pure]
        public static float Log2(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Log2)}(float)");

        /// <summary>
        /// Returns the base-2 logarithm of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-2 logarithm of the <paramref name="x"/> parameter (if <paramref name="x"/> parameter is negative this function returns indefinite, and if <paramref name="x"/> is 0, this function returns -INF)</returns>
        [Pure]
        public static Vector2 Log2(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Log2)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the base-2 logarithm of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-2 logarithm of the <paramref name="x"/> parameter (if <paramref name="x"/> parameter is negative this function returns indefinite, and if <paramref name="x"/> is 0, this function returns -INF)</returns>
        [Pure]
        public static Vector3 Log2(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Log2)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the base-2 logarithm of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The base-2 logarithm of the <paramref name="x"/> parameter (if <paramref name="x"/> parameter is negative this function returns indefinite, and if <paramref name="x"/> is 0, this function returns -INF)</returns>
        [Pure]
        public static Vector4 Log2(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Log2)}({nameof(Vector4)})");

        /// <summary>
        /// Selects the greater of <paramref name="x"/> and <paramref name="y"/>
        /// </summary>
        /// <param name="x">The first specified value</param>
        /// <param name="y">The second specified exponent</param>
        /// <returns>The <paramref name="x"/> or <paramref name="y"/> parameter, whichever is the largest value</returns>
        /// <remarks>For values of -INF or INF, <see cref="Max(in int,in int)"/> will behave as expected</remarks>
        [Pure]
        public static int Max(in int x, in int y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Max)}(int,int)");

        /// <summary>
        /// Selects the greater of <paramref name="x"/> and <paramref name="y"/>
        /// </summary>
        /// <param name="x">The first specified value</param>
        /// <param name="y">The second specified exponent</param>
        /// <returns>The <paramref name="x"/> or <paramref name="y"/> parameter, whichever is the largest value</returns>
        /// <remarks>For values of -INF or INF, <see cref="Max(in float,in float)"/> will behave as expected, but for for values of <see cref="float.NaN"/>, the results are undefined</remarks>
        [Pure]
        public static float Max(in float x, in float y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Max)}(float,float)");

        /// <summary>
        /// Selects the greater of <paramref name="x"/> and <paramref name="y"/>
        /// </summary>
        /// <param name="x">The first specified value</param>
        /// <param name="y">The second specified exponent</param>
        /// <returns>The <paramref name="x"/> or <paramref name="y"/> parameter, whichever is the largest value</returns>
        /// <remarks>For values of -INF or INF, <see cref="Max(in Vector2,in Vector2)"/> will behave as expected, but for for values of <see cref="float.NaN"/>, the results are undefined</remarks>
        [Pure]
        public static Vector2 Max(in Vector2 x, in Vector2 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Max)}({nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Selects the greater of <paramref name="x"/> and <paramref name="y"/>
        /// </summary>
        /// <param name="x">The first specified value</param>
        /// <param name="y">The second specified exponent</param>
        /// <returns>The <paramref name="x"/> or <paramref name="y"/> parameter, whichever is the largest value</returns>
        /// <remarks>For values of -INF or INF, <see cref="Max(in Vector3,in Vector3)"/> will behave as expected, but for for values of <see cref="float.NaN"/>, the results are undefined</remarks>
        [Pure]
        public static Vector3 Max(in Vector3 x, in Vector3 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Max)}({nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Selects the greater of <paramref name="x"/> and <paramref name="y"/>
        /// </summary>
        /// <param name="x">The first specified value</param>
        /// <param name="y">The second specified exponent</param>
        /// <returns>The <paramref name="x"/> or <paramref name="y"/> parameter, whichever is the largest value</returns>
        /// <remarks>For values of -INF or INF, <see cref="Max(in Vector4,in Vector4)"/> will behave as expected, but for for values of <see cref="float.NaN"/>, the results are undefined</remarks>
        [Pure]
        public static Vector4 Max(in Vector4 x, in Vector4 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Max)}({nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Selects the lesser of <paramref name="x"/> and <paramref name="y"/>
        /// </summary>
        /// <param name="x">The first specified value</param>
        /// <param name="y">The second specified exponent</param>
        /// <returns>The <paramref name="x"/> or <paramref name="y"/> parameter, whichever is the smallest value</returns>
        /// <remarks>For values of -INF or INF, <see cref="Min(in int,in int)"/> will behave as expected</remarks>
        [Pure]
        public static int Min(in int x, in int y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Min)}(int,int)");

        /// <summary>
        /// Selects the lesser of <paramref name="x"/> and <paramref name="y"/>
        /// </summary>
        /// <param name="x">The first specified value</param>
        /// <param name="y">The second specified exponent</param>
        /// <returns>The <paramref name="x"/> or <paramref name="y"/> parameter, whichever is the smallest value</returns>
        /// <remarks>For values of -INF or INF, <see cref="Min(in float,in float)"/> will behave as expected, but for for values of <see cref="float.NaN"/>, the results are undefined</remarks>
        [Pure]
        public static float Min(in float x, in float y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Min)}(float,float)");

        /// <summary>
        /// Selects the lesser of <paramref name="x"/> and <paramref name="y"/>
        /// </summary>
        /// <param name="x">The first specified value</param>
        /// <param name="y">The second specified exponent</param>
        /// <returns>The <paramref name="x"/> or <paramref name="y"/> parameter, whichever is the smallest value</returns>
        /// <remarks>For values of -INF or INF, <see cref="Min(in Vector2,in Vector2)"/> will behave as expected, but for for values of <see cref="float.NaN"/>, the results are undefined</remarks>
        [Pure]
        public static Vector2 Min(in Vector2 x, in Vector2 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Min)}({nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Selects the lesser of <paramref name="x"/> and <paramref name="y"/>
        /// </summary>
        /// <param name="x">The first specified value</param>
        /// <param name="y">The second specified exponent</param>
        /// <returns>The <paramref name="x"/> or <paramref name="y"/> parameter, whichever is the smallest value</returns>
        /// <remarks>For values of -INF or INF, <see cref="Min(in Vector3,in Vector3)"/> will behave as expected, but for for values of <see cref="float.NaN"/>, the results are undefined</remarks>
        [Pure]
        public static Vector3 Min(in Vector3 x, in Vector3 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Min)}({nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Selects the lesser of <paramref name="x"/> and <paramref name="y"/>
        /// </summary>
        /// <param name="x">The first specified value</param>
        /// <param name="y">The second specified exponent</param>
        /// <returns>The <paramref name="x"/> or <paramref name="y"/> parameter, whichever is the smallest value</returns>
        /// <remarks>For values of -INF or INF, <see cref="Min(in Vector4,in Vector4)"/> will behave as expected, but for for values of <see cref="float.NaN"/>, the results are undefined</remarks>
        [Pure]
        public static Vector4 Min(in Vector4 x, in Vector4 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Min)}({nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Splits the value <paramref name="x"/> into fractional and integer parts, each of which has the same sign as <paramref name="x"/>
        /// </summary>
        /// <param name="x">The input value</param>
        /// <param name="ip">The integer portion of <paramref name="x"/></param>
        /// <returns>The signed-fractional portion of <paramref name="x"/></returns>
        [Pure]
        public static int Modf(in int x, out int ip) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Modf)}(int,int)");

        /// <summary>
        /// Splits the value <paramref name="x"/> into fractional and integer parts, each of which has the same sign as <paramref name="x"/>
        /// </summary>
        /// <param name="x">The input value</param>
        /// <param name="ip">The integer portion of <paramref name="x"/></param>
        /// <returns>The signed-fractional portion of <paramref name="x"/></returns>
        [Pure]
        public static float Modf(in float x, out float ip) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Modf)}(float,float)");

        /// <summary>
        /// Splits the value <paramref name="x"/> into fractional and integer parts, each of which has the same sign as <paramref name="x"/>
        /// </summary>
        /// <param name="x">The input value</param>
        /// <param name="ip">The integer portion of <paramref name="x"/></param>
        /// <returns>The signed-fractional portion of <paramref name="x"/></returns>
        [Pure]
        public static Vector2 Modf(in Vector2 x, out Vector2 ip) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Modf)}({nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Splits the value <paramref name="x"/> into fractional and integer parts, each of which has the same sign as <paramref name="x"/>
        /// </summary>
        /// <param name="x">The input value</param>
        /// <param name="ip">The integer portion of <paramref name="x"/></param>
        /// <returns>The signed-fractional portion of <paramref name="x"/></returns>
        [Pure]
        public static Vector3 Modf(in Vector3 x, out Vector3 ip) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Modf)}({nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Splits the value <paramref name="x"/> into fractional and integer parts, each of which has the same sign as <paramref name="x"/>
        /// </summary>
        /// <param name="x">The input value</param>
        /// <param name="ip">The integer portion of <paramref name="x"/></param>
        /// <returns>The signed-fractional portion of <paramref name="x"/></returns>
        [Pure]
        public static Vector4 Modf(in Vector4 x, out Vector4 ip) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Modf)}({nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Multiplies <paramref name="x"/> and <paramref name="y"/> using matrix math
        /// </summary>
        /// <param name="x">The first input value (if <paramref name="x"/> is a vector, it treated as a row vector)</param>
        /// <param name="y">The second input value (if <paramref name="y"/> is a vector, it treated as a column vector)</param>
        /// <returns>The result of <paramref name="x"/> times <paramref name="y"/></returns>
        [Pure]
        public static Vector2 Mul(in float x, in Vector2 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Mul)}(float,{nameof(Vector2)})");

        /// <summary>
        /// Multiplies <paramref name="x"/> and <paramref name="y"/> using matrix math
        /// </summary>
        /// <param name="x">The first input value (if <paramref name="x"/> is a vector, it treated as a row vector)</param>
        /// <param name="y">The second input value (if <paramref name="y"/> is a vector, it treated as a column vector)</param>
        /// <returns>The result of <paramref name="x"/> times <paramref name="y"/></returns>
        [Pure]
        public static Vector3 Mul(in float x, in Vector3 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Mul)}(float,{nameof(Vector3)})");

        /// <summary>
        /// Multiplies <paramref name="x"/> and <paramref name="y"/> using matrix math
        /// </summary>
        /// <param name="x">The first input value (if <paramref name="x"/> is a vector, it treated as a row vector)</param>
        /// <param name="y">The second input value (if <paramref name="y"/> is a vector, it treated as a column vector)</param>
        /// <returns>The result of <paramref name="x"/> times <paramref name="y"/></returns>
        [Pure]
        public static Vector3 Mul(in float x, in Vector4 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Mul)}(float,{nameof(Vector4)})");

        /// <summary>
        /// Multiplies <paramref name="x"/> and <paramref name="y"/> using matrix math
        /// </summary>
        /// <param name="x">The first input value (if <paramref name="x"/> is a vector, it treated as a row vector)</param>
        /// <param name="y">The second input value (if <paramref name="y"/> is a vector, it treated as a column vector)</param>
        /// <returns>The result of <paramref name="x"/> times <paramref name="y"/></returns>
        [Pure]
        public static Vector2 Mul(in Vector2 x, in float y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Mul)}({nameof(Vector2)},float)");

        /// <summary>
        /// Multiplies <paramref name="x"/> and <paramref name="y"/> using matrix math
        /// </summary>
        /// <param name="x">The first input value (if <paramref name="x"/> is a vector, it treated as a row vector)</param>
        /// <param name="y">The second input value (if <paramref name="y"/> is a vector, it treated as a column vector)</param>
        /// <returns>The result of <paramref name="x"/> times <paramref name="y"/></returns>
        [Pure]
        public static Vector3 Mul(in Vector3 x, in float y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Mul)}({nameof(Vector3)},float)");

        /// <summary>
        /// Multiplies <paramref name="x"/> and <paramref name="y"/> using matrix math
        /// </summary>
        /// <param name="x">The first input value (if <paramref name="x"/> is a vector, it treated as a row vector)</param>
        /// <param name="y">The second input value (if <paramref name="y"/> is a vector, it treated as a column vector)</param>
        /// <returns>The result of <paramref name="x"/> times <paramref name="y"/></returns>
        [Pure]
        public static Vector4 Mul(in Vector4 x, in float y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Mul)}({nameof(Vector4)},float)");

        /// <summary>
        /// Multiplies <paramref name="x"/> and <paramref name="y"/> using matrix math
        /// </summary>
        /// <param name="x">The first input value (if <paramref name="x"/> is a vector, it treated as a row vector)</param>
        /// <param name="y">The second input value (if <paramref name="y"/> is a vector, it treated as a column vector)</param>
        /// <returns>The result of <paramref name="x"/> times <paramref name="y"/></returns>
        [Pure]
        public static float Mul(in Vector2 x, in Vector2 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Mul)}({nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Multiplies <paramref name="x"/> and <paramref name="y"/> using matrix math
        /// </summary>
        /// <param name="x">The first input value (if <paramref name="x"/> is a vector, it treated as a row vector)</param>
        /// <param name="y">The second input value (if <paramref name="y"/> is a vector, it treated as a column vector)</param>
        /// <returns>The result of <paramref name="x"/> times <paramref name="y"/></returns>
        [Pure]
        public static float Mul(in Vector3 x, in Vector3 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Mul)}({nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Multiplies <paramref name="x"/> and <paramref name="y"/> using matrix math
        /// </summary>
        /// <param name="x">The first input value (if <paramref name="x"/> is a vector, it treated as a row vector)</param>
        /// <param name="y">The second input value (if <paramref name="y"/> is a vector, it treated as a column vector)</param>
        /// <returns>The result of <paramref name="x"/> times <paramref name="y"/></returns>
        [Pure]
        public static float Mul(in Vector4 x, in Vector4 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Mul)}({nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Normalizes the specified floating-point vector according to <paramref name="x"/> / length(<paramref name="x"/>)
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The normalized <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Normalize(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Normalize)}({nameof(Vector2)})");

        /// <summary>
        /// Normalizes the specified floating-point vector according to <paramref name="x"/> / length(<paramref name="x"/>)
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The normalized <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Normalize(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Normalize)}({nameof(Vector3)})");

        /// <summary>
        /// Normalizes the specified floating-point vector according to <paramref name="x"/> / length(<paramref name="x"/>)
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The normalized <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector4 Normalize(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Normalize)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the specified value raised to the specified power
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <param name="y">The specified power</param>
        /// <returns>The <paramref name="x"/> parameter raised to the power of the <paramref name="y"/> parameter</returns>
        /// <remarks>See <a href="https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-pow"/> for details on special cases</remarks>
        [Pure]
        public static float Pow(in float x, in float y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Pow)}(float,float)");

        /// <summary>
        /// Returns the specified value raised to the specified power
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <param name="y">The specified power</param>
        /// <returns>The <paramref name="x"/> parameter raised to the power of the <paramref name="y"/> parameter</returns>
        /// <remarks>See <a href="https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-pow"/> for details on special cases</remarks>
        [Pure]
        public static Vector2 Pow(in Vector2 x, in Vector2 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Pow)}({nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Returns the specified value raised to the specified power
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <param name="y">The specified power</param>
        /// <returns>The <paramref name="x"/> parameter raised to the power of the <paramref name="y"/> parameter</returns>
        /// <remarks>See <a href="https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-pow"/> for details on special cases</remarks>
        [Pure]
        public static Vector3 Pow(in Vector3 x, in Vector3 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Pow)}({nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Returns the specified value raised to the specified power
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <param name="y">The specified power</param>
        /// <returns>The <paramref name="x"/> parameter raised to the power of the <paramref name="y"/> parameter</returns>
        /// <remarks>See <a href="https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-pow"/> for details on special cases</remarks>
        [Pure]
        public static Vector4 Pow(in Vector4 x, in Vector4 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Pow)}({nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Converts the specified value from degrees to radians
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The <paramref name="x"/> parameter converted from degrees to radians</returns>
        [Pure]
        public static float Radians(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Radians)}(float)");

        /// <summary>
        /// Converts the specified value from degrees to radians
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The <paramref name="x"/> parameter converted from degrees to radians</returns>
        [Pure]
        public static Vector2 Radians(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Radians)}({nameof(Vector2)})");

        /// <summary>
        /// Converts the specified value from degrees to radians
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The <paramref name="x"/> parameter converted from degrees to radians</returns>
        [Pure]
        public static Vector3 Radians(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Radians)}({nameof(Vector3)})");

        /// <summary>
        /// Converts the specified value from degrees to radians
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The <paramref name="x"/> parameter converted from degrees to radians</returns>
        [Pure]
        public static Vector4 Radians(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Radians)}({nameof(Vector4)})");

        /// <summary>
        /// Calculates a fast, approximate reciprocal of a given value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The reciprocal of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Rcp(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Rcp)}(float)");

        /// <summary>
        /// Calculates a fast, approximate, per-component reciprocal
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The reciprocal of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Rcp(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Rcp)}({nameof(Vector2)})");

        /// <summary>
        /// Calculates a fast, approximate, per-component reciprocal
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The reciprocal of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Rcp(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Rcp)}({nameof(Vector3)})");

        /// <summary>
        /// Calculates a fast, approximate, per-component reciprocal
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The reciprocal of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector4 Rcp(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Rcp)}({nameof(Vector4)})");

        /// <summary>
        /// Rounds the specified value to the nearest integer
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The <paramref name="x"/> parameter, rounded to the nearest integer within a floating-point type</returns>
        [Pure]
        public static float Round(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Round)}(float)");

        /// <summary>
        /// Rounds the specified value to the nearest integer
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The <paramref name="x"/> parameter, rounded to the nearest integer within a floating-point type</returns>
        [Pure]
        public static Vector2 Round(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Round)}({nameof(Vector2)})");

        /// <summary>
        /// Rounds the specified value to the nearest integer
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The <paramref name="x"/> parameter, rounded to the nearest integer within a floating-point type</returns>
        [Pure]
        public static Vector3 Round(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Round)}({nameof(Vector3)})");

        /// <summary>
        /// Rounds the specified value to the nearest integer
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The <paramref name="x"/> parameter, rounded to the nearest integer within a floating-point type</returns>
        [Pure]
        public static Vector4 Round(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Round)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the reciprocal of the square root of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The reciprocal of the square root of the <paramref name="x"/> parameter</returns>
        /// <remarks>This function uses the following formula: 1 / sqrt(<paramref name="x"/>)</remarks>
        [Pure]
        public static float Rsqrt(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Rsqrt)}(float)");

        /// <summary>
        /// Returns the reciprocal of the square root of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The reciprocal of the square root of the <paramref name="x"/> parameter</returns>
        /// <remarks>This function uses the following formula: 1 / sqrt(<paramref name="x"/>)</remarks>
        [Pure]
        public static Vector2 Rsqrt(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Rsqrt)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the reciprocal of the square root of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The reciprocal of the square root of the <paramref name="x"/> parameter</returns>
        /// <remarks>This function uses the following formula: 1 / sqrt(<paramref name="x"/>)</remarks>
        [Pure]
        public static Vector3 Rsqrt(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Rsqrt)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the reciprocal of the square root of the specified value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The reciprocal of the square root of the <paramref name="x"/> parameter</returns>
        /// <remarks>This function uses the following formula: 1 / sqrt(<paramref name="x"/>)</remarks>
        [Pure]
        public static Vector4 Rsqrt(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Rsqrt)}({nameof(Vector4)})");

        /// <summary>
        /// Clamps the specified value within the range [0,1]
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The <paramref name="x"/> parameter, clamped within the range [0,1]</returns>
        [Pure]
        public static float Saturate(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Saturate)}(float)");

        /// <summary>
        /// Clamps the specified value within the range [0,1]
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The <paramref name="x"/> parameter, clamped within the range [0,1]</returns>
        [Pure]
        public static Vector2 Saturate(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Saturate)}({nameof(Vector2)})");

        /// <summary>
        /// Clamps the specified value within the range [0,1]
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The <paramref name="x"/> parameter, clamped within the range [0,1]</returns>
        [Pure]
        public static Vector3 Saturate(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Saturate)}({nameof(Vector3)})");

        /// <summary>
        /// Clamps the specified value within the range [0,1]
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The <paramref name="x"/> parameter, clamped within the range [0,1]</returns>
        [Pure]
        public static Vector4 Saturate(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Saturate)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the sign of <paramref name="x"/>
        /// </summary>
        /// <param name="x">The input value</param>
        /// <returns>Returns -1 if <paramref name="x"/> is less than zero, 0 if <paramref name="x"/> equals zero and 1 if <paramref name="x"/> is greater than zero</returns>
        [Pure]
        public static float Sign(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sign)}(float)");

        /// <summary>
        /// Returns the sign of <paramref name="x"/>
        /// </summary>
        /// <param name="x">The input value</param>
        /// <returns>Returns -1 if <paramref name="x"/> is less than zero, 0 if <paramref name="x"/> equals zero and 1 if <paramref name="x"/> is greater than zero</returns>
        [Pure]
        public static Vector2 Sign(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sign)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the sign of <paramref name="x"/>
        /// </summary>
        /// <param name="x">The input value</param>
        /// <returns>Returns -1 if <paramref name="x"/> is less than zero, 0 if <paramref name="x"/> equals zero and 1 if <paramref name="x"/> is greater than zero</returns>
        [Pure]
        public static Vector3 Sign(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sign)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the sign of <paramref name="x"/>
        /// </summary>
        /// <param name="x">The input value</param>
        /// <returns>Returns -1 if <paramref name="x"/> is less than zero, 0 if <paramref name="x"/> equals zero and 1 if <paramref name="x"/> is greater than zero</returns>
        [Pure]
        public static Vector4 Sign(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sign)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the sine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The sine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Sin(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sin)}(float)");

        /// <summary>
        /// Returns the sine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The sine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Sin(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sin)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the sine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The sine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Sin(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sin)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the sine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The sine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector4 Sin(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sin)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the sine and cosine of <paramref name="x"/>
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <param name="s">Returns the sine of <paramref name="x"/></param>
        /// <param name="c">Returns the cosine of <paramref name="x"/></param>
        public static void SinCos(in float x, out float s, out float c) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(SinCos)}(float,float,float)");

        /// <summary>
        /// Returns the sine and cosine of <paramref name="x"/>
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <param name="s">Returns the sine of <paramref name="x"/></param>
        /// <param name="c">Returns the cosine of <paramref name="x"/></param>
        public static void SinCos(in Vector2 x, out Vector2 s, out Vector2 c) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(SinCos)}({nameof(Vector2)},{nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Returns the sine and cosine of <paramref name="x"/>
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <param name="s">Returns the sine of <paramref name="x"/></param>
        /// <param name="c">Returns the cosine of <paramref name="x"/></param>
        public static void SinCos(in Vector3 x, out Vector3 s, out Vector3 c) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(SinCos)}({nameof(Vector3)},{nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Returns the sine and cosine of <paramref name="x"/>
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <param name="s">Returns the sine of <paramref name="x"/></param>
        /// <param name="c">Returns the cosine of <paramref name="x"/></param>
        public static void SinCos(in Vector4 x, out Vector4 s, out Vector4 c) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(SinCos)}({nameof(Vector4)},{nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Returns the hyperbolic sine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The hyperbolic sine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Sinh(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sinh)}(float)");

        /// <summary>
        /// Returns the hyperbolic sine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The hyperbolic sine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Sinh(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sinh)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the hyperbolic sine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The hyperbolic sine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Sinh(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sinh)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the hyperbolic sine of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The hyperbolic sine of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector4 Sinh(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sinh)}({nameof(Vector4)})");

        /// <summary>
        /// Returns a smooth Hermite interpolation between 0 and 1, if <paramref name="x"/> is in the range [<paramref name="min"/>, <paramref name="max"/>]
        /// </summary>
        /// <param name="min">The minimum range of the <paramref name="x"/> parameter</param>
        /// <param name="max">The maximum range of the <paramref name="x"/> parameter</param>
        /// <param name="x">The specified value to be interpolated</param>
        /// <returns>0 if <paramref name="x"/> is less than <paramref name="min"/>, 1 if <paramref name="x"/> is greater than <paramref name="max"/>, otherwise a value between 0 and 1 if <paramref name="x"/> is in the range [<paramref name="min"/>, <paramref name="max"/>]</returns>
        [Pure]
        public static float SmoothStep(in float min, in float max, in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(SmoothStep)}(float,float,float)");

        /// <summary>
        /// Returns a smooth Hermite interpolation between 0 and 1, if <paramref name="x"/> is in the range [<paramref name="min"/>, <paramref name="max"/>]
        /// </summary>
        /// <param name="min">The minimum range of the <paramref name="x"/> parameter</param>
        /// <param name="max">The maximum range of the <paramref name="x"/> parameter</param>
        /// <param name="x">The specified value to be interpolated</param>
        /// <returns>0 if <paramref name="x"/> is less than <paramref name="min"/>, 1 if <paramref name="x"/> is greater than <paramref name="max"/>, otherwise a value between 0 and 1 if <paramref name="x"/> is in the range [<paramref name="min"/>, <paramref name="max"/>]</returns>
        [Pure]
        public static Vector2 SmoothStep(in Vector2 min, in Vector2 max, in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(SmoothStep)}({nameof(Vector2)},{nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Returns a smooth Hermite interpolation between 0 and 1, if <paramref name="x"/> is in the range [<paramref name="min"/>, <paramref name="max"/>]
        /// </summary>
        /// <param name="min">The minimum range of the <paramref name="x"/> parameter</param>
        /// <param name="max">The maximum range of the <paramref name="x"/> parameter</param>
        /// <param name="x">The specified value to be interpolated</param>
        /// <returns>0 if <paramref name="x"/> is less than <paramref name="min"/>, 1 if <paramref name="x"/> is greater than <paramref name="max"/>, otherwise a value between 0 and 1 if <paramref name="x"/> is in the range [<paramref name="min"/>, <paramref name="max"/>]</returns>
        [Pure]
        public static Vector3 SmoothStep(in Vector3 min, in Vector3 max, in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(SmoothStep)}({nameof(Vector3)},{nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Returns a smooth Hermite interpolation between 0 and 1, if <paramref name="x"/> is in the range [<paramref name="min"/>, <paramref name="max"/>]
        /// </summary>
        /// <param name="min">The minimum range of the <paramref name="x"/> parameter</param>
        /// <param name="max">The maximum range of the <paramref name="x"/> parameter</param>
        /// <param name="x">The specified value to be interpolated</param>
        /// <returns>0 if <paramref name="x"/> is less than <paramref name="min"/>, 1 if <paramref name="x"/> is greater than <paramref name="max"/>, otherwise a value between 0 and 1 if <paramref name="x"/> is in the range [<paramref name="min"/>, <paramref name="max"/>]</returns>
        [Pure]
        public static Vector4 SmoothStep(in Vector4 min, in Vector4 max, in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(SmoothStep)}({nameof(Vector4)},{nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Returns the square root of the specified floating-point value
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The square root of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Sqrt(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sqrt)}(float)");

        /// <summary>
        /// Returns the square root of the specified floating-point value, per component
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The square root of the <paramref name="x"/> parameter, per component</returns>
        [Pure]
        public static Vector2 Sqrt(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sqrt)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the square root of the specified floating-point value, per component
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The square root of the <paramref name="x"/> parameter, per component</returns>
        [Pure]
        public static Vector3 Sqrt(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sqrt)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the square root of the specified floating-point value, per component
        /// </summary>
        /// <param name="x">The specified value</param>
        /// <returns>The square root of the <paramref name="x"/> parameter, per component</returns>
        [Pure]
        public static Vector4 Sqrt(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Sqrt)}({nameof(Vector4)})");

        /// <summary>
        /// Compares two values, returning 0 or 1 based on which value is greater
        /// </summary>
        /// <param name="x">The first specified value to compare</param>
        /// <param name="y">The second specified value to compare</param>
        /// <returns>1 if the <paramref name="x"/> parameter is greater than or equal to the <paramref name="y"/> parameter, otherwise, 0</returns>
        /// <remarks>This function uses the following formula: (x >= y) ? 1 : 0</remarks>
        [Pure]
        public static float Step(in float x, in float y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Step)}(float,float)");

        /// <summary>
        /// Compares two values, returning 0 or 1 based on which value is greater
        /// </summary>
        /// <param name="x">The first specified value to compare</param>
        /// <param name="y">The second specified value to compare</param>
        /// <returns>1 if the <paramref name="x"/> parameter is greater than or equal to the <paramref name="y"/> parameter, otherwise, 0</returns>
        /// <remarks>This function uses the following formula: (x >= y) ? 1 : 0</remarks>
        [Pure]
        public static Vector2 Step(in Vector2 x, in Vector2 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Step)}({nameof(Vector2)},{nameof(Vector2)})");

        /// <summary>
        /// Compares two values, returning 0 or 1 based on which value is greater
        /// </summary>
        /// <param name="x">The first specified value to compare</param>
        /// <param name="y">The second specified value to compare</param>
        /// <returns>1 if the <paramref name="x"/> parameter is greater than or equal to the <paramref name="y"/> parameter, otherwise, 0</returns>
        /// <remarks>This function uses the following formula: (x >= y) ? 1 : 0</remarks>
        [Pure]
        public static Vector3 Step(in Vector3 x, in Vector3 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Step)}({nameof(Vector3)},{nameof(Vector3)})");

        /// <summary>
        /// Compares two values, returning 0 or 1 based on which value is greater
        /// </summary>
        /// <param name="x">The first specified value to compare</param>
        /// <param name="y">The second specified value to compare</param>
        /// <returns>1 if the <paramref name="x"/> parameter is greater than or equal to the <paramref name="y"/> parameter, otherwise, 0</returns>
        /// <remarks>This function uses the following formula: (x >= y) ? 1 : 0</remarks>
        [Pure]
        public static Vector4 Step(in Vector4 x, in Vector4 y) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Step)}({nameof(Vector4)},{nameof(Vector4)})");

        /// <summary>
        /// Returns the tangent of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The tangent of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Tan(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Tan)}(float)");

        /// <summary>
        /// Returns the tangent of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The tangent of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Tan(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Tan)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the tangent of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The tangent of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Tan(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Tan)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the tangent of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The tangent of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector4 Tan(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Tan)}({nameof(Vector4)})");

        /// <summary>
        /// Returns the hyperbolic tangent of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The hyperbolic tangent of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static float Tanh(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Tanh)}(float)");

        /// <summary>
        /// Returns the hyperbolic tangent of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The hyperbolic tangent of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector2 Tanh(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Tanh)}({nameof(Vector2)})");

        /// <summary>
        /// Returns the hyperbolic tangent of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The hyperbolic tangent of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector3 Tanh(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Tanh)}({nameof(Vector3)})");

        /// <summary>
        /// Returns the hyperbolic tangent of the specified value
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The hyperbolic tangent of the <paramref name="x"/> parameter</returns>
        [Pure]
        public static Vector4 Tanh(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Tanh)}({nameof(Vector4)})");

        /// <summary>
        /// Truncates a floating-point value to the integer component
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The <paramref name="x"/> parameter truncated to an integer component</returns>
        [Pure]
        public static float Trunc(in float x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Trunc)}(float)");

        /// <summary>
        /// Truncates a floating-point value to the integer component
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The <paramref name="x"/> parameter truncated to an integer component</returns>
        [Pure]
        public static Vector2 Trunc(in Vector2 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Trunc)}({nameof(Vector2)})");

        /// <summary>
        /// Truncates a floating-point value to the integer component
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The <paramref name="x"/> parameter truncated to an integer component</returns>
        [Pure]
        public static Vector3 Trunc(in Vector3 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Trunc)}({nameof(Vector3)})");

        /// <summary>
        /// Truncates a floating-point value to the integer component
        /// </summary>
        /// <param name="x">The specified value, in radians</param>
        /// <returns>The <paramref name="x"/> parameter truncated to an integer component</returns>
        [Pure]
        public static Vector4 Trunc(in Vector4 x) => throw new InvalidExecutionContextException($"{nameof(Hlsl)}.{nameof(Trunc)}({nameof(Vector4)})");
    }
}
