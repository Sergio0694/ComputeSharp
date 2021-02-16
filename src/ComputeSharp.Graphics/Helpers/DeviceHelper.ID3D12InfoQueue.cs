#if DEBUG

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using TerraFX.Interop;

namespace ComputeSharp.Graphics.Helpers
{
    /// <inheritdoc/>
    internal static partial class DeviceHelper
    {
        /// <summary>
        /// Flushes all the pending debug messages for all existing <see cref="ID3D12Device"/> instances to the console/debugger.
        /// </summary>
        public static unsafe void FlushAllID3D12InfoQueueMessages()
        {
            lock (DevicesCache)
            {
                int j = 0;
                StringBuilder builder = new(1024);

                foreach (var pair in D3D12InfoQueueMap)
                {
                    var device = DevicesCache[pair.Key];
                    var queue = pair.Value;

                    ulong messages = queue.Get()->GetNumStoredMessagesAllowedByRetrievalFilter();

                    if (messages > 0)
                    {
                        builder.Clear();
                        builder.AppendLine("================ ID3D12InfoQueue =================");
                        builder.AppendLine($"[ID3D12InfoQueue messages for device #{j}");
                        builder.AppendLine($"[Luid]: {device.Luid}");
                        builder.AppendLine($"[Name]: {device.Name}");
                        builder.AppendLine($"[DedicatedMemorySize]: {device.DedicatedMemorySize}");
                        builder.AppendLine($"[IsHardwareAccelerated]: {device.IsHardwareAccelerated}");
                        builder.AppendLine($"[IsCacheCoherentUMA]: {device.IsCacheCoherentUMA}");
                        builder.AppendLine($"[ComputeUnits]: {device.ComputeUnits}");
                        builder.AppendLine($"[WavefrontSize]: {device.WavefrontSize}");
                        builder.Append("==================================================");

                        for (ulong i = 0; i < messages; i++)
                        {
                            nuint length;
                            queue.Get()->GetMessage(i, null, &length);

                            D3D12_MESSAGE* message = (D3D12_MESSAGE*)Marshal.AllocHGlobal((nint)length);

                            queue.Get()->GetMessage(i, message, &length);

                            builder.AppendLine();
                            builder.AppendLine($"[D3D12 debug message #{i}]");
                            builder.AppendLine($"[Category]: {Enum.GetName(message->Category)}");
                            builder.AppendLine($"[Severity]: {Enum.GetName(message->Severity)}");
                            builder.AppendLine($"[ID]: {Enum.GetName(message->ID)}");
                            builder.AppendLine($"[Description]: \"{new string(message->pDescription)}\"");
                            builder.Append("==================================================");

                            Marshal.FreeHGlobal((IntPtr)message);
                        }

                        string text = builder.ToString();

                        if (Debugger.IsAttached)
                        {
                            Debug.WriteLine(text);
                        }
                        else
                        {
                            Console.WriteLine(text);
                        }

                        queue.Get()->ClearStoredMessages();
                    }

                    j++;
                }
            }
        }
    }
}

#endif
