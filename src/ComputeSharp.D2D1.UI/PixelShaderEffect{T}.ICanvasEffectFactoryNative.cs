using System;
using System.Runtime.CompilerServices;
using ABI.Microsoft.Graphics.Canvas;
using ComputeSharp.D2D1.Extensions;
using ComputeSharp.D2D1.Interop.Effects;
#if WINDOWS_UWP
using ComputeSharp.D2D1.Uwp.Extensions;
using ComputeSharp.D2D1.Uwp.Helpers;
#else
using ComputeSharp.D2D1.WinUI.Extensions;
using ComputeSharp.D2D1.WinUI.Helpers;
#endif
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using TerraFX.Interop.WinRT;

#if WINDOWS_UWP
namespace ComputeSharp.D2D1.Uwp;
#else
namespace ComputeSharp.D2D1.WinUI;
#endif

/// <inheritdoc/>
unsafe partial class PixelShaderEffect<T>
{
    /// <summary>
    /// A manager type to handle the effect factory registration logic for <see cref="ICanvasEffectFactoryNative"/>.
    /// </summary>
    private sealed class EffectFactoryManager
    {
        /// <summary>
        /// Lock object used to synchronize the registration logic.
        /// </summary>
        private readonly object lockObject = new();

        /// <summary>
        /// Indicates whether an effect factory for <see cref="PixelShaderEffect{T}"/> has been registered.
        /// </summary>
        private volatile bool isEffectFactoryRegistered;

        /// <summary>
        /// Creates a new <see cref="EffectFactoryManager"/> instance.
        /// </summary>
        private EffectFactoryManager()
        {
        }

        /// <summary>
        /// Gets the shared <see cref="EffectFactoryManager"/> instance.
        /// </summary>
        public static EffectFactoryManager Instance { get; } = new();

        /// <summary>
        /// Ensures an effect factory is registered in Win2D for the current effect type, or registers one otherwise.
        /// </summary>
        public void EnsureEffectFactoryIsRegistered()
        {
            if (this.isEffectFactoryRegistered)
            {
                return;
            }

            EnsureEffectFactoryIsRegisteredWithLock();
        }

        /// <summary>
        /// Same as <see cref="EnsureEffectFactoryIsRegistered"/>, but with a slower path using a lock.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void EnsureEffectFactoryIsRegisteredWithLock()
        {
            lock (this.lockObject)
            {
                if (!this.isEffectFactoryRegistered)
                {
                    ResourceManager.RegisterEffectFactory(
                        effectId: PixelShaderEffect.For<T>.Instance.Id,
                        factory: new EffectFactory());

                    this.isEffectFactoryRegistered = true;
                }
            }
        }
    }

    /// <summary>
    /// A managed implementation of <see cref="ICanvasEffectFactoryNative"/> for <see cref="PixelShaderEffect{T}"/>.
    /// </summary>
    private sealed class EffectFactory : ICanvasEffectFactoryNative.Interface
    {
        /// <inheritdoc/>
        int ICanvasEffectFactoryNative.Interface.CreateWrapper(ICanvasDevice* device, ID2D1Effect* resource, float dpi, IInspectable** wrapper)
        {
            PixelShaderEffect<T> @this;

            using (ComPtr<ID2D1Device1> d2D1Device1 = default)
            {
                // Get the underlying ID2D1Device1 instance for the input device
                device->GetD2DDevice(d2D1Device1.GetAddressOf()).Assert();

                // Validate that the input underlying D2D device and resource share the same D2D factory
                using (ComPtr<ID2D1Image> d2D1ImageResource = default)
                using (ComPtr<ID2D1Factory> d2D1FactoryFromDevice = default)
                using (ComPtr<ID2D1Factory> d2D1FactoryFromResource = default)
                {
                    resource->CopyTo(d2D1ImageResource.GetAddressOf()).Assert();

                    d2D1Device1.Get()->GetFactory(d2D1FactoryFromDevice.GetAddressOf());
                    d2D1ImageResource.Get()->GetFactory(d2D1FactoryFromResource.GetAddressOf());

                    // Shared logic and comments from Win2D, as this validation path is the same as that of CanvasEffect.
                    // There is a small chance of constructing a PixelShaderEffect<T> object that is in an invalid state
                    // when using this method to create a managed wrapper indirectly from the interop APIs exposed in the
                    // public headers. In general, this would happen if someone did manual interop to get a managed wrapper
                    // for an orphaned effect previously retrieved from an existing PixelShaderEffect<T> instance that has
                    // since changed. For more info and a full example, see https://github.com/microsoft/Win2D/issues/913.
                    // Here we're validating the D2D factories from the wrapped effect and the device to try to minimize
                    // the chances of this happening. We can't be 100% sure since there is no way with the existing D2D
                    // APIs to fully determine whether two resources are "compatible", short of just trying to draw with
                    // both of them to see if that fails. But, comparing factories is at least a nice additional check
                    // to perform, and due to the fact that Win2D creates a new D2D factory per CanvasDevice, this should
                    // already cover most scenarios where this might potentially happen, so that's good enough.
                    if (!d2D1FactoryFromDevice.Get()->IsSameInstance(d2D1FactoryFromResource.Get()))
                    {
                        *wrapper = null;

                        return E.E_INVALIDARG;
                    }
                }

                // Only initialize the effect once we're past the validation
                @this = new PixelShaderEffect<T>();

                // Initialize the effect state
                device->CopyTo(ref @this.canvasDevice).Assert();
                d2D1Device1.CopyTo(ref @this.d2D1RealizationDevice).Assert();
                resource->CopyTo(ref @this.d2D1Effect).Assert();
            }

            // Register the new wrapper for the input resource. This has to be done manually in this
            // case, as the effect is not being realized through the usual path from GetD2DImage.
            ResourceManager.RegisterWrapper((IUnknown*)resource, this);

            // Unwrap the PixelShaderEffect<T> and return its CCW to the caller
            RcwMarshaller.GetNativeObject(@this, wrapper);

            return S.S_OK;
        }
    }
}