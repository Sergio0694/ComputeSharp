using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using ComputeSharp.Core.Extensions;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Helpers
{
    internal static class DeviceDebugHelper
    {
        public static unsafe void FlushMessages()
        {
            int j = 0;

            foreach (var pair in DeviceHelper.D3D12InfoQueueMap)
            {
                var device = DeviceHelper.DevicesCache[pair.Key];
                var queue = pair.Value;

                ulong messages = queue.Get()->GetNumStoredMessages();

                if (messages > 0)
                {
                    var builder = new StringBuilder();
                    builder.AppendLine("====================[ID3D12InfoQueue]====================");
                    builder.AppendLine($"[ID3D12InfoQueue messages for device #{j}");
                    builder.AppendLine($"[LUID]: {device.Luid}");
                    builder.AppendLine($"[NAME]: {device.Name}");
                    builder.AppendLine($"[DEDICATED MEMORY]: {device.DedicatedMemorySize}");
                    builder.AppendLine($"[IS HARDWARE]: {device.IsHardwareAccelerated}");
                    builder.AppendLine($"[IS UMA]: {device.IsCacheCoherentUMA}");
                    builder.AppendLine($"[COMPUTE UNITS]: {device.ComputeUnits}");
                    builder.AppendLine($"[WAVE SIZE]: {device.WavefrontSize}");
                    builder.Append("=========================================================");

                    for (ulong i = 0; i < messages; i++)
                    {
                        nuint length;
                        queue.Get()->GetMessage(i, null, &length);

                        D3D12_MESSAGE* message = (D3D12_MESSAGE*)Marshal.AllocHGlobal((nint)length);

                        queue.Get()->GetMessage(i, message, &length);

                        builder.AppendLine();
                        builder.AppendLine($"[D3D12 DEBUG MESSAGE #{i}]");
                        builder.AppendLine($"[CATEGORY]: {Enum.GetName(message->Category)}");
                        builder.AppendLine($"[SEVERITY]: {Enum.GetName(message->Severity)}");
                        builder.AppendLine($"[ID]: {Enum.GetName(message->ID)}");
                        builder.AppendLine($"[INFO]: \"{new string(message->pDescription)}\"");
                        builder.Append("=========================================================");

                        Marshal.FreeHGlobal((IntPtr)message);
                    }

                    string text = builder.ToString();

                    Console.WriteLine(text);

                    if (Debugger.IsAttached)
                    {
                        Debug.WriteLine(text);
                    }

                    queue.Get()->ClearStoredMessages();
                }

                j++;
            }
        }
    }

    /// <inheritdoc/>
    internal static partial class DeviceHelper
    {
        /// <summary>
        /// The local cache of <see cref="GraphicsDevice"/> instances that are currently usable.
        /// </summary>
        internal static readonly Dictionary<Luid, GraphicsDevice> DevicesCache = new();

        internal static readonly Dictionary<Luid, ComPtr<ID3D12InfoQueue>> D3D12InfoQueueMap = new();

        /// <summary>
        /// Retrieves a <see cref="GraphicsDevice"/> instance for an <see cref="ID3D12Device"/> object.
        /// </summary>
        /// <param name="d3D12Device">The <see cref="ID3D12Device"/> to use to get a <see cref="GraphicsDevice"/> instance.</param>
        /// <param name="dxgiDescription1">The available info for the <see cref="GraphicsDevice"/> instance.</param>
        /// <returns>A <see cref="GraphicsDevice"/> instance for the input device.</returns>
        private static unsafe GraphicsDevice GetOrCreateDevice(ComPtr<ID3D12Device> d3D12Device, DXGI_ADAPTER_DESC1* dxgiDescription1)
        {
            lock (DevicesCache)
            {
                Luid luid = Luid.FromLUID(dxgiDescription1->AdapterLuid);

                if (!DevicesCache.TryGetValue(luid, out GraphicsDevice? device))
                {
                    device = new GraphicsDevice(d3D12Device, dxgiDescription1);

                    DevicesCache.Add(luid, device);

                    ComPtr<ID3D12InfoQueue> d3D12InfoQueue = default;

                    int res = d3D12Device.Get()->QueryInterface(Windows.__uuidof<ID3D12InfoQueue>(), d3D12InfoQueue.GetVoidAddressOf());

                    D3D12InfoQueueMap.Add(luid, d3D12InfoQueue);
                }

                return device;
            }
        }

        /// <summary>
        /// Removes a <see cref="GraphicsDevice"/> from the internal cache.
        /// </summary>
        /// <param name="device">The <see cref="GraphicsDevice"/> instance to remove from the internal cache.</param>
        public static void NotifyDisposedDevice(GraphicsDevice device)
        {
            lock (DevicesCache)
            {
                DevicesCache.Remove(device.Luid);
                D3D12InfoQueueMap.Remove(device.Luid, out var queue);

                queue.Dispose();
            }
        }
    }
}
