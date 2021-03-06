using System;
using System.Collections;
using System.Collections.Generic;
using ComputeSharp.Core.Extensions;
using ComputeSharp.Interop;
using TerraFX.Interop;
using FX = TerraFX.Interop.Windows;
using HRESULT = System.Int32;

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
            private readonly Predicate<GraphicsDeviceInfo> predicate;

            /// <summary>
            /// Creates a new <see cref="Enumerator"/> instance for the given predicate.
            /// </summary>
            /// <param name="predicate">The input predicate to select devices to create.</param>
            public DeviceQuery(Predicate<GraphicsDeviceInfo> predicate)
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
                /// The <see cref="Predicate{T}"/> instance to use to select devices to create.
                /// </summary>
                private readonly Predicate<GraphicsDeviceInfo> predicate;

                /// <summary>
                /// The <see cref="IDXGIFactory4"/> instance used to enumerate devices.
                /// </summary>
                private ComPtr<IDXGIFactory4> dxgiFactory4;

                /// <summary>
                /// Indicates whether or not the enumerator has already been initialized and <see cref="dxgiFactory4"/> is set.
                /// </summary>
                private bool isInitialized;

                /// <summary>
                /// The current adapter index to enumerate adapters from <see cref="dxgiFactory4"/>.
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
                public Enumerator(Predicate<GraphicsDeviceInfo> predicate)
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

                        fixed (IDXGIFactory4** dxgiFactory4 = this.dxgiFactory4)
                        {
                            EnableDebugMode();

                            FX.CreateDXGIFactory2(IDXGIFactoryCreationFlags, FX.__uuidof<IDXGIFactory4>(), (void**)dxgiFactory4).Assert();
                        }
                    }

                    if (this.isCompleted) return false;

                    while (true)
                    {
                        using ComPtr<IDXGIAdapter1> dxgiAdapter1 = default;

                        HRESULT enumAdapters1Result = this.dxgiFactory4.Get()->EnumAdapters1(this.index, dxgiAdapter1.GetAddressOf());

                        if (enumAdapters1Result == FX.DXGI_ERROR_NOT_FOUND)
                        {
                            this.dxgiFactory4.Get()->EnumWarpAdapter(FX.__uuidof<IDXGIAdapter1>(), dxgiAdapter1.GetVoidAddressOf()).Assert();

                            DXGI_ADAPTER_DESC1 dxgiDescription1;

                            dxgiAdapter1.Get()->GetDesc1(&dxgiDescription1).Assert();

                            HRESULT createDeviceResult = FX.D3D12CreateDevice(
                                dxgiAdapter1.AsIUnknown().Get(),
                                D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0,
                                FX.__uuidof<ID3D12Device>(),
                                null);

                            if (FX.SUCCEEDED(createDeviceResult) &&
                                this.predicate(new GraphicsDeviceInfo(&dxgiDescription1)))
                            {
                                using ComPtr<ID3D12Device> d3D12Device = default;

                                FX.D3D12CreateDevice(
                                    dxgiAdapter1.AsIUnknown().Get(),
                                    D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0,
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
                                D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0,
                                FX.__uuidof<ID3D12Device>(),
                                null);

                            if (FX.SUCCEEDED(createDeviceResult) &&
                                this.predicate(new GraphicsDeviceInfo(&dxgiDescription1)))
                            {
                                using ComPtr<ID3D12Device> d3D12Device = default;

                                FX.D3D12CreateDevice(
                                    dxgiAdapter1.AsIUnknown().Get(),
                                    D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_11_0,
                                    FX.__uuidof<ID3D12Device>(),
                                    d3D12Device.GetVoidAddressOf()).Assert();

                                this.graphicsDevice = GetOrCreateDevice(d3D12Device.Get(), (IDXGIAdapter*)dxgiAdapter1.Get(), &dxgiDescription1);

                                return true;
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
                protected override bool OnDispose()
                {
                    this.dxgiFactory4.Dispose();

                    return true;
                }
            }
        }
    }
}
