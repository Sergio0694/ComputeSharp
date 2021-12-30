namespace System.Threading.Tasks;

/// <summary>
/// A polyfill type that mirrors the non-generic <see cref="TaskCompletionSource{T}"/> type on .NET 6.
/// </summary>
internal sealed class TaskCompletionSource : TaskCompletionSource<object?>
{
    /// <summary>
    /// Transitions the underlying <see cref="Task"/> into the <see cref="TaskStatus.RanToCompletion"/> state.
    /// </summary>
    public void SetResult()
    {
        SetResult(null);
    }
}
