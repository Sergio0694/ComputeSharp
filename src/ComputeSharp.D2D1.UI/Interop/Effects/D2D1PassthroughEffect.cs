using System;
using ComputeSharp.D2D1.Extensions;
using TerraFX.Interop.DirectX;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp.Interop.Effects;
#else
namespace ComputeSharp.D2D1.WinUI.Interop.Effects;
#endif

/// <summary>
/// An implementation of a D2D1 passthrough effect.
/// </summary>
internal static unsafe class D2D1PassthroughEffect
{
    /// <summary>
    /// Registers a passthrough effect, by calling <c>ID2D1Factory1::RegisterEffectFromString</c>.
    /// </summary>
    /// <param name="d2D1Factory1">A pointer to the <c>ID2D1Factory1</c> instance to use.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="d2D1Factory1"/> is <see langword="null"/>.</exception>
    /// <remarks>For more info, see <see href="https://docs.microsoft.com/windows/win32/api/d2d1_1/nf-d2d1_1-id2d1factory1-registereffectfromstring"/>.</remarks>
    public static void RegisterForD2D1Factory1(ID2D1Factory1* d2D1Factory1)
    {
        default(ArgumentNullException).ThrowIfNull(d2D1Factory1);

        const string registrationXml = """
            <?xml version='1.0'?>
            <Effect>
                <Property name='DisplayName' type='string' value='ComputeSharp.D2D1.Uwp.PassthroughEffect'/>
                <Property name='Author' type='string' value='ComputeSharp.D2D1.Uwp'/>
                <Property name='Category' type='string' value='Stylize'/>
                <Property name='Description' type='string' value='A passthrough D2D1 effect'/>
                <Inputs minimum="1" maximum="1">
                    <Input name='Source'/>
                </Inputs>
            </Effect>
            """;

        fixed (char* pXml = registrationXml)
        {
            Guid guid = typeof(PassthroughEffect).GUID;

            // Register the effect
            d2D1Factory1->RegisterEffectFromString(
                classId: &guid,
                propertyXml: (ushort*)pXml,
                bindings: null,
                bindingsCount: 0,
                effectFactory: PassthroughEffect.Factory).Assert();
        }
    }
}