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
            PixelShaderEffect<T> @this = new();

            using (ComPtr<ID2D1Device1> d2D1Device1 = default)
            {
                // Get the underlying ID2D1Device1 instance for the input device
                device->GetD2DDevice(d2D1Device1.GetAddressOf()).Assert();

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