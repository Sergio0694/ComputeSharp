using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_FEATURE;
using FX = TerraFX.Interop.Windows;

namespace ComputeSharp.Graphics.Helpers
{
    /// <summary>
    /// A helper <see langword="class"/> exposing some runtime or OS dependent D3D12 features.
    /// </summary>
    internal static class D3D12FeatureHelper
    {
        /// <summary>
        /// The currently supported <see cref="D3D12_HEAP_FLAGS.D3D12_HEAP_FLAG_CREATE_NOT_ZEROED"/> flag (or fallback).
        /// </summary>
        private static readonly D3D12_HEAP_FLAGS D3D12_HEAP_FLAG_CREATE_NOT_ZEROED = GetD3D12_HEAP_FLAG_CREATE_NOT_ZEROED();

        /// <summary>
        /// Gets the <see cref="D3D12_HEAP_FLAGS"/> value for a given <see cref="AllocationMode"/> setting.
        /// </summary>
        /// <param name="allocationMode">The allocation mode to use for creating new resources.</param>
        /// <returns>The <see cref="D3D12_HEAP_FLAGS"/> matching <paramref name="allocationMode"/> that is currently supported</returns>
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static D3D12_HEAP_FLAGS GetD3D12HeapFlags(AllocationMode allocationMode)
        {
            if (allocationMode == AllocationMode.Default)
            {
                return D3D12_HEAP_FLAG_CREATE_NOT_ZEROED;
            }

            return D3D12_HEAP_FLAGS.D3D12_HEAP_FLAG_NONE;
        }

        /// <summary>
        /// Initializes the <see cref="D3D12_HEAP_FLAG_CREATE_NOT_ZEROED"/> field.
        /// </summary>
        /// <returns>The currently supported <see cref="D3D12_HEAP_FLAGS.D3D12_HEAP_FLAG_CREATE_NOT_ZEROED"/> flag.</returns>
        private static unsafe D3D12_HEAP_FLAGS GetD3D12_HEAP_FLAG_CREATE_NOT_ZEROED()
        {
            lock (DeviceHelper.DevicesCache)
            {
                foreach (KeyValuePair<Luid, GraphicsDevice> pair in DeviceHelper.DevicesCache)
                {
                    D3D12_FEATURE_DATA_D3D12_OPTIONS7 d3D12Options7;

                    int result = pair.Value.D3D12Device->CheckFeatureSupport(
                        D3D12_FEATURE_D3D12_OPTIONS7,
                        &d3D12Options7,
                        (uint)sizeof(D3D12_FEATURE_DATA_D3D12_OPTIONS7));

                    if (result == FX.S_OK)
                    {
                        return D3D12_HEAP_FLAGS.D3D12_HEAP_FLAG_CREATE_NOT_ZEROED;
                    }

                    break;
                }

                return D3D12_HEAP_FLAGS.D3D12_HEAP_FLAG_NONE;
            }
        }
    }
}
