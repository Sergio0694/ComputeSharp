using System;

namespace ComputeSharp.D2D1;

/// <summary>
/// An attribute for a D2D1 shader indicating that the shader function calls helper methods that use the scene position
/// value (that is, <see cref="D2D.GetScenePosition"/>). This parameter should only be included when necessary, since
/// only one function per linked shader can utilize this parameter. Defining this value is optional.
/// </summary>
/// <remarks>
/// <para>This attribute directly maps to <c>#define D2D_REQUIRES_SCENE_POSITION</c>.</para>
/// <para>For more info, see <see href="https://docs.microsoft.com/en-us/windows/win32/direct2d/hlsl-helpers"/>.</para>
/// </remarks>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public sealed class D2DRequiresScenePositionAttribute : Attribute;