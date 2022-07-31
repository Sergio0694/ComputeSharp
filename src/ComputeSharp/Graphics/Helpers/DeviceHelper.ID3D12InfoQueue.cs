using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using CommunityToolkit.Diagnostics;
using ComputeSharp.Core.Extensions;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;
using static TerraFX.Interop.DirectX.D3D12_MESSAGE_ID;
using static TerraFX.Interop.DirectX.D3D12_MESSAGE_SEVERITY;
#if NET6_0_OR_GREATER
using Enum = System.Enum;
#else
using Enum = ComputeSharp.NetStandard.System.Enum;
#endif

namespace ComputeSharp.Graphics.Helpers;

/// <inheritdoc/>
partial class DeviceHelper
{
    /// <summary>
    /// A shared <see cref="StringBuilder"/> used from <see cref="FlushAllID3D12InfoQueueMessagesAndCheckForErrorsOrWarnings"/> to create messages.
    /// </summary>
    private static StringBuilder? infoQueueMessageBuilder;

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
            StringBuilder builder = infoQueueMessageBuilder ??= new(1024);

            foreach (var pair in D3D12InfoQueueMap)
            {
                GraphicsDevice device = DevicesCache[pair.Key];
                ID3D12InfoQueue* queue = pair.Value.Get();

                ulong messages = queue->GetNumStoredMessagesAllowedByRetrievalFilter();

                for (ulong i = 0; i < messages; i++)
                {
                    nuint length;

                    queue->GetMessage(i, null, &length);

                    D3D12_MESSAGE* message = (D3D12_MESSAGE*)NativeMemory.Alloc(length);

                    try
                    {
                        queue->GetMessage(i, message, &length);

                        builder.Clear();
                        builder.AppendLine($"[D3D12 message #{i} for \"{device}\" (HW: {device.IsHardwareAccelerated}, UMA: {device.IsCacheCoherentUMA})]");
                        builder.AppendLine($"[Category]: {Enum.GetName(message->Category)}");
                        builder.AppendLine($"[Severity]: {Enum.GetName(message->Severity)}");
                        builder.AppendLine($"[ID]: {Enum.GetName(message->ID)}");

                        bool isDeviceRemovedMessage = message->ID is D3D12_MESSAGE_ID_DEVICE_REMOVAL_PROCESS_AT_FAULT or D3D12_MESSAGE_ID_DEVICE_REMOVAL_PROCESS_POSSIBLY_AT_FAULT or D3D12_MESSAGE_ID_DEVICE_REMOVAL_PROCESS_NOT_AT_FAULT;

                        // Special handling for device removal messages
                        if (isDeviceRemovedMessage)
                        {
                            HRESULT result = device.D3D12Device->GetDeviceRemovedReason();

                            // If the device removal reason is available, also include that in the output message
                            if (result != S.S_OK)
                            {
                                string deviceRemovalReason = (int)result switch
                                {
                                    DXGI.DXGI_ERROR_DEVICE_HUNG => nameof(DXGI.DXGI_ERROR_DEVICE_HUNG),
                                    DXGI.DXGI_ERROR_DEVICE_REMOVED => nameof(DXGI.DXGI_ERROR_DEVICE_REMOVED),
                                    DXGI.DXGI_ERROR_DEVICE_RESET => nameof(DXGI.DXGI_ERROR_DEVICE_RESET),
                                    DXGI.DXGI_ERROR_DRIVER_INTERNAL_ERROR => nameof(DXGI.DXGI_ERROR_DRIVER_INTERNAL_ERROR),
                                    DXGI.DXGI_ERROR_INVALID_CALL => nameof(DXGI.DXGI_ERROR_INVALID_CALL),
                                    _ => ThrowHelper.ThrowArgumentOutOfRangeException<string>("Invalid GetDeviceRemovedReason HRESULT.")
                                };

                                builder.AppendLine($"[Reason]: {deviceRemovalReason}");
                            }
                        }

                        // If DRED is enabled, also output the available auto breadcrumbs
                        if (isDeviceRemovedMessage && Configuration.IsDeviceRemovedExtendedDataEnabled)
                        {
                            builder.AppendLine($"[Description]: \"{new string(message->pDescription)}\"");

                            using ComPtr<ID3D12DeviceRemovedExtendedData1> d3D12DeviceRemovedExtendedData = default;

                            // Get the DRED data
                            int hresult = device.D3D12Device->QueryInterface(
                                riid: Windows.__uuidof<ID3D12DeviceRemovedExtendedData1>(),
                                ppvObject: d3D12DeviceRemovedExtendedData.GetVoidAddressOf());

                            if (hresult != S.S_OK)
                            {
                                ThrowHelper.ThrowWin32Exception(hresult);
                            }

                            D3D12_DRED_AUTO_BREADCRUMBS_OUTPUT1 d3D12DredAutoBreadcrumbsOutput;

                            // Get the auto breadcrumbs for the current event
                            hresult = d3D12DeviceRemovedExtendedData.Get()->GetAutoBreadcrumbsOutput1(&d3D12DredAutoBreadcrumbsOutput);

                            if (hresult != S.S_OK)
                            {
                                ThrowHelper.ThrowWin32Exception(hresult);
                            }

                            builder.AppendLine("[DRED breadcrumbs START] ===============================");

                            int breadcrumbNodeIndex = 0;

                            // Traverse all the auto breadcrumb nodes and print their contents
                            for (D3D12_AUTO_BREADCRUMB_NODE1* d3D12AutoBreadcrumbNode = d3D12DredAutoBreadcrumbsOutput.pHeadAutoBreadcrumbNode;
                                 d3D12AutoBreadcrumbNode is not null;
                                 d3D12AutoBreadcrumbNode = d3D12AutoBreadcrumbNode->pNext, breadcrumbNodeIndex++)
                            {
                                uint numberOfExecutedOps = *d3D12AutoBreadcrumbNode->pLastBreadcrumbValue;

                                // For each auto breadcrumb node, not all recorded breadcrumb have actually been executed. To reduce the verbosity of the
                                // output and only show the most relevant info, indicate how many breadcrumbs were recorded and how many were actually
                                // executed, but then only display the executed ones in the code below. This makes it easier to debug hangs for users.
                                builder.AppendLine($"[NODE #{breadcrumbNodeIndex}]: {d3D12AutoBreadcrumbNode->BreadcrumbCount} total breadcrumb(s), {numberOfExecutedOps} executed breadcrumb(s)");

                                uint contextIndex = 0;

                                for (uint commandIndex = 0; commandIndex < numberOfExecutedOps; commandIndex++)
                                {
                                    // An D3D12_AUTO_BREADCRUMB_NODE1 object has two pointers:
                                    //   - pCommandHistory, pointing to the full list of available breadcrumbs.
                                    //   - pBreadcrumbContexts, pointing to the list of available contexts.
                                    // Each context also has a field to indicate the index of the breadcrumb it belongs to. Since these indices are
                                    // sorted in ascending order, the following code loops through the executed breadcrumbs and then only checks if
                                    // the current index for the context list is within bounds, and if so, if the current context has a matching index.
                                    // If that is the case, the context message is extracted and the index is incremented, otherwise just the opcode
                                    // is added to the debug output. Essentially, this is somewhat similar to a merge algorithm for two ordered lists.
                                    if (contextIndex < d3D12AutoBreadcrumbNode->BreadcrumbContextsCount &&
                                        d3D12AutoBreadcrumbNode->pBreadcrumbContexts[contextIndex].BreadcrumbIndex == commandIndex)
                                    {
                                        string context = new((char*)d3D12AutoBreadcrumbNode->pBreadcrumbContexts[contextIndex++].pContextString);

                                        builder.AppendLine($">> [OP #{commandIndex}]: {d3D12AutoBreadcrumbNode->pCommandHistory[commandIndex]}, \"{context}\"");
                                    }
                                    else
                                    {
                                        builder.AppendLine($">> [OP #{commandIndex}]: {d3D12AutoBreadcrumbNode->pCommandHistory[commandIndex]}");
                                    }
                                }
                            }

                            builder.AppendLine("================================= [DRED breadcrumbs END]");
                        }
                        else
                        {
                            builder.Append($"[Description]: \"{new string(message->pDescription)}\"");
                        }

                        if (message->Severity is D3D12_MESSAGE_SEVERITY_ERROR or D3D12_MESSAGE_SEVERITY_CORRUPTION or D3D12_MESSAGE_SEVERITY_WARNING)
                        {
                            hasErrorsOrWarnings = true;
                        }
                    }
                    finally
                    {
                        NativeMemory.Free(message);
                    }

                    Trace.WriteLine(builder);
                }

                queue->ClearStoredMessages();
            }
        }

        return hasErrorsOrWarnings;
    }
}
