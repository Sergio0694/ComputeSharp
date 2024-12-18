using System;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp;
#else
namespace ComputeSharp.D2D1.WinUI;
#endif

/// <summary>
/// An attribute that indicates that a given property represents an effect property, which should invalidate the effect.
/// In order to use this attribute, the containing type has to inherit from <see cref="CanvasEffect"/>.
/// <para>
/// This attribute can be used as follows:
/// <code>
/// partial class MyEffect : CanvasEffect
/// {
///     [GeneratedCanvasEffectProperty]
///     public int BlurAmount { get; set; }
/// }
/// </code>
/// </para>
/// And with this, code analogous to this will be generated:
/// <code>
/// partial class MyEffect
/// {
///     public partial int BlurAmount
///     {
///         get => field;
///         set
///         {
///             if (EqualityComparer&lt;int&gt;.Default.Equals(field, value))
///             {
///                 return;
///             }
/// 
///             int oldValue = field;
///     
///             OnBlurAmountChanging(value);
///             OnBlurAmountChanging(oldValue, value);
/// 
///             field = value;
/// 
///             OnBlurAmountChanged(value);
///             OnBlurAmountChanged(oldValue, value);
///     
///             InvalidateEffectGraph(&lt;INVALIDATION_TYPE&gt;);
///         }
///     }
/// 
///     partial void OnBlurAmountChanging(int newValue);
///     partial void OnBlurAmountChanging(int oldValue, int newValue);
///     partial void OnBlurAmountChanged(int newValue);
///     partial void OnBlurAmountChanged(int oldValue, int newValue);
/// }
/// </code>
/// </summary>
/// <param name="invalidationType">The invalidation type to request.</param>
/// <remarks>
/// The generated <c>OnChanging</c> and <c>OnChanged</c> methods will be completely removed by
/// the compiler if an implementation for the is not provided, making them fully pay-for-play.
/// </remarks>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class GeneratedCanvasEffectPropertyAttribute(CanvasEffectInvalidationType invalidationType = CanvasEffectInvalidationType.Update) : Attribute
{
    /// <summary>
    /// Gets the requested invalidation type for the effect graph when changing the proprty value.
    /// </summary>
    public CanvasEffectInvalidationType InvalidationType { get; } = invalidationType;
}