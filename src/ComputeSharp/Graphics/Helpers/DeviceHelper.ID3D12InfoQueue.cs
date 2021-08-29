#if DEBUG

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using TerraFX.Interop;
using static TerraFX.Interop.D3D12_MESSAGE_SEVERITY;

namespace ComputeSharp.Graphics.Helpers;

/// <inheritdoc/>
internal static partial class DeviceHelper
{
    /// <summary>
    /// Flushes all the pending debug messages for all existing <see cref="ID3D12Device"/> instances to the console/debugger.
    /// It also checks whether or not there are any error messages being logged that didn't result in an actual crash yet.
    /// </summary>
    /// <return>Whether or not there are any logged errors or warnings.</return>
    public static unsafe bool FlushAllID3D12InfoQueueMessagesAndCheckForErrorsOrWarnings()
    {
        bool hasErrorsOrWarnings = false;

        lock (DevicesCache)
        {
            int j = 0;
            StringBuilder builder = new(1024);

            foreach (var pair in D3D12InfoQueueMap)
            {
                var device = DevicesCache[pair.Key];
                var queue = pair.Value;

                ulong messages = queue.Get()->GetNumStoredMessagesAllowedByRetrievalFilter();

                for (ulong i = 0; i < messages; i++)
                {
                    nuint length;
                    queue.Get()->GetMessage(i, null, &length);

                    D3D12_MESSAGE* message = (D3D12_MESSAGE*)Marshal.AllocHGlobal((nint)length);

                    queue.Get()->GetMessage(i, message, &length);

                    builder.Clear();
                    builder.AppendLine($"[D3D12 message #{i} for \"{device}\" (HW: {device.IsHardwareAccelerated}, UMA: {device.IsCacheCoherentUMA})]");
                    builder.AppendLine($"[Category]: {Enum.GetName(message->Category)}");
                    builder.AppendLine($"[Severity]: {Enum.GetName(message->Severity)}");
                    builder.AppendLine($"[ID]: {Enum.GetName(message->ID)}");
                    builder.Append($"[Description]: \"{new string(message->pDescription)}\"");

                    Marshal.FreeHGlobal((IntPtr)message);

                    if (message->Severity is D3D12_MESSAGE_SEVERITY_ERROR or D3D12_MESSAGE_SEVERITY_CORRUPTION or D3D12_MESSAGE_SEVERITY_WARNING)
                    {
                        hasErrorsOrWarnings = true;
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
                }

                queue.Get()->ClearStoredMessages();

                j++;
            }
        }

        return hasErrorsOrWarnings;
    }
}

#endif
