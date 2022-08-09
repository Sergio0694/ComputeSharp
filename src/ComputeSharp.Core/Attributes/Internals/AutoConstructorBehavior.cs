namespace ComputeSharp;

/// <summary>
/// Indicates the behavior of fields annotated with <see cref="AutoConstructorBehaviorAttribute"/>.
/// </summary>
internal enum AutoConstructorBehavior
{
    /// <summary>
    /// Causes <see cref="AutoConstructorAttribute"/> to ignore fields and set their values to <see langword="default"/>.
    /// </summary>
    IgnoreAndSetToDefault
}
