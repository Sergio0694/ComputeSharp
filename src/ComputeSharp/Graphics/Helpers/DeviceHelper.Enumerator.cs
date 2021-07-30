using System;
using System.Collections;
using System.Collections.Generic;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Graphics.Extensions;
using ComputeSharp.Interop;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;
using HRESULT = System.Int32;
using static TerraFX.Interop.D3D_FEATURE_LEVEL;
using static TerraFX.Interop.D3D_SHADER_MODEL;
using static TerraFX.Interop.DXGI_GPU_PREFERENCE;

namespace ComputeSharp.Graphics.Helpers
{
    /// <inheritdoc/>
    internal static partial class DeviceHelper
    {
        /// <summary>
        /// A custom query type that iterates through devices matching a given predicate.
        /// </summary>
        public sealed unsafe class DeviceQuery : IEnumerable<GraphicsDevice>
        {
            /// <summary>
            /// The <see cref="Predicate{T}"/> instance to use to select devices to create.
            /// </summary>
            private readonly Predicate<GraphicsDeviceInfo>? predicate;

            /// <summary>
            /// Creates a new <see cref="Enumerator"/> instance for the given predicate.
            /// </summary>
            /// <param name="predicate">The input predicate to select devices to create.</param>
            public DeviceQuery(Predicate<GraphicsDeviceInfo>? predicate)
            {
                this.predicate = predicate;
            }

            /// <inheritdoc/>
            public IEnumerator<GraphicsDevice> GetEnumerator()
            {
                return new Enumerator(this.predicate);
            }

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(this.predicate);
            }

            /// <summary>
            /// The enumerator type for a <see cref="DeviceQuery"/> instance.
            /// </summary>
            private sealed unsafe class Enumerator : NativeObject, IEnumerator<GraphicsDevice>
            {
                /// <summary>
                /// The <see cref="Predicate{T}"/> instance to use to select devices to create, if present.
                /// </summary>
                private readonly Predicate<GraphicsDeviceInfo>? predicate;

                /// <summary>
                /// The <see cref="IDXGIFactory6"/> instance used to enumerate devices.
                /// </summary>
                private ComPtr<IDXGIFactory6> dxgiFactory6;

                /// <summary>
                /// Indicates whether or not the enumerator has already been initialized and <see cref="dxgiFactory6"/> is set.
                /// </summary>
                private bool isInitialized;

                /// <summary>
                /// The current adapter index to enumerate adapters from <see cref="dxgiFactory6"/>.
                /// </summary>
                private uint index;

                /// <summary>
                /// Indicates whether or not the enumerator has completed the enumeration of all possible devices.
                /// </summary>
                private bool isCompleted;

                /// <summary>
                /// The current <see cref="GraphicsDevice"/> instance to return.
                /// </summary>
                private GraphicsDevice? graphicsDevice;

                /// <summary>
                /// Creates a new <see cref="Enumerator"/> instance for the given predicate.
                /// </summary>
                /// <param name="predicate">The input predicate to select devices to create.</param>
                public Enumerator(Predicate<GraphicsDeviceInfo>? predicate)
                {
                    this.predicate = predicate;
                }

                /// <inheritdoc/>
                public GraphicsDevice Current => this.graphicsDevice!;

                /// <inheritdoc/>
                object IEnumerator.Current => this.graphicsDevice!;

                /// <inheritdoc/>
                public bool MoveNext()
                {
                    if (!this.isInitialized)
                    {
                        this.isInitialized = true;

                        fixed (IDXGIFactory6** dxgiFactory6 = this.dxgiFactory6)
                        {
                            CreateDXGIFactory6(dxgiFactory6);
                        }
                    }

                    if (this.isCompleted) return false;

                    while (true)
                    {
                        using ComPtr<IDXGIAdapter1> dxgiAdapter1 = default;

                        HRESULT enumAdapters1Result = this.dxgiFactory6.Get()->EnumAdapterByGpuPreference(
                            this.index,
                            DXGI_GPU_PREFERENCE_HIGH_PERFORMANCE,
                            FX.__uuidof<IDXGIAdapter1>(),
                            dxgiAdapter1.GetVoidAddressOf());

                        if (enumAdapters1Result == FX.DXGI_ERROR_NOT_FOUND)
                        {
                            this.dxgiFactory6.Get()->EnumWarpAdapter(FX.__uuidof<IDXGIAdapter1>(), dxgiAdapter1.GetVoidAddressOf()).Assert();

                            DXGI_ADAPTER_DESC1 dxgiDescription1;

                            dxgiAdapter1.Get()->GetDesc1(&dxgiDescription1).Assert();

                            HRESULT createDeviceResult = FX.D3D12CreateDevice(
                                dxgiAdapter1.AsIUnknown().Get(),
                                D3D_FEATURE_LEVEL_11_0,
                                FX.__uuidof<ID3D12Device>(),
                                null);

                            if (FX.SUCCEEDED(createDeviceResult) &&
                                this.predicate?.Invoke(new GraphicsDeviceInfo(&dxgiDescription1)) != false)
                            {
                                using ComPtr<ID3D12Device> d3D12Device = default;

                                FX.D3D12CreateDevice(
                                    dxgiAdapter1.AsIUnknown().Get(),
                                    D3D_FEATURE_LEVEL_11_0,
                                    FX.__uuidof<ID3D12Device>(),
                                    d3D12Device.GetVoidAddressOf()).Assert();

                                this.graphicsDevice = GetOrCreateDevice(d3D12Device.Get(), (IDXGIAdapter*)dxgiAdapter1.Get(), &dxgiDescription1);
                                this.isCompleted = true;

                                return true;
                            }

                            return false;
                        }
                        else
                        {
                            enumAdapters1Result.Assert();

                            this.index++;

                            DXGI_ADAPTER_DESC1 dxgiDescription1;

                            dxgiAdapter1.Get()->GetDesc1(&dxgiDescription1).Assert();

                            if (dxgiDescription1.VendorId == MicrosoftVendorId &&
                                dxgiDescription1.DeviceId == WarpDeviceId)
                            {
                                continue;
                            }

                            HRESULT createDeviceResult = FX.D3D12CreateDevice(
                                dxgiAdapter1.AsIUnknown().Get(),
                                D3D_FEATURE_LEVEL_11_0,
                                FX.__uuidof<ID3D12Device>(),
                                null);

                            if (FX.SUCCEEDED(createDeviceResult) &&
                                this.predicate?.Invoke(new GraphicsDeviceInfo(&dxgiDescription1)) != false)
                            {
                                using ComPtr<ID3D12Device> d3D12Device = default;

                                FX.D3D12CreateDevice(
                                    dxgiAdapter1.AsIUnknown().Get(),
                                    D3D_FEATURE_LEVEL_11_0,
                                    FX.__uuidof<ID3D12Device>(),
                                    d3D12Device.GetVoidAddressOf()).Assert();

                                if (d3D12Device.Get()->IsShaderModelSupported(D3D_SHADER_MODEL_6_0))
                                {
                                    this.graphicsDevice = GetOrCreateDevice(d3D12Device.Get(), (IDXGIAdapter*)dxgiAdapter1.Get(), &dxgiDescription1);

                                    return true;
                                }
                            }
                        }
                    }
                }

                /// <inheritdoc/>
                public void Reset()
                {
                    throw new NotImplementedException();
                }

                /// <inheritdoc/>
                protected override void OnDispose()
                {
                    this.dxgiFactory6.Dispose();
                }
            }
        }
    }
}
