using System.ComponentModel;
using System.Threading.Tasks;
using ComputeSharp.Tests.Attributes;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.DeviceLost;

[TestClass]
[TestCategory("DeviceLost")]
public partial class DeviceLostTests
{
    [CombinatorialTestMethod]
    [AllDevices]
    public async Task DeviceLost_RaiseEvent(Device device)
    {
        GraphicsDevice graphicsDevice = device.Get();

        bool win32ExceptionThrown = false;
        TaskCompletionSource<(object? Sender, DeviceLostReason Reason)> tcs = new();

        // Register the device lost callback
        graphicsDevice.DeviceLost += (s, e) => tcs.SetResult((s, e));

        try
        {
            using ReadOnlyBuffer<float> bufferA = graphicsDevice.AllocateReadOnlyBuffer<float>(128);
            using ReadWriteBuffer<float> bufferB = graphicsDevice.AllocateReadWriteBuffer<float>(128);

            graphicsDevice.For(128, new ComputeShaderGettingStuck(bufferA, bufferB, 12345678, 87654321, 42));

            // The shader above will always hang, but it's not guaranteed that the DirectX runtime will detect
            // the error right when executing that command. As such, we can force it to be detected by running
            // any other command after that. At this point the device is lost, so any operation will fail.
            graphicsDevice.For(128, new ComputeShaderGettingStuck(bufferA, bufferB, 2, 2, 42));
        }
        catch (Win32Exception)
        {
            win32ExceptionThrown = true;
        }

        // The previous shader must hang and cause an exception
        Assert.IsTrue(win32ExceptionThrown);

        // Wait up to a second for the event to be raised (it's raised asynchronously on a thread pool thread)
        await Task.WhenAny(tcs.Task, Task.Delay(1000));

        // Ensure the event has been raised, and get the results
        Assert.IsTrue(tcs.Task.IsCompleted);
        Assert.AreEqual(tcs.Task.Status, TaskStatus.RanToCompletion);

        (object? sender, DeviceLostReason reason) = tcs.Task.Result;

        Assert.IsNotNull(sender);
        Assert.AreSame(sender, graphicsDevice);
        Assert.IsTrue(reason is DeviceLostReason.DeviceHung or DeviceLostReason.DeviceReset);
    }

    [AutoConstructor]
    [EmbeddedBytecode(DispatchAxis.X)]
    internal readonly partial struct ComputeShaderGettingStuck : IComputeShader
    {
        private readonly ReadOnlyBuffer<float> bufferA;
        private readonly ReadWriteBuffer<float> bufferB;
        private readonly float a;
        private readonly float b;
        private readonly int target;

        /// <inheritdoc/>
        public void Execute()
        {
            if (ThreadIds.X == target)
            {
                for (float x = 0; x < a; x += 0.1f)
                {
                    for (float y = 0; y < b; y += 0.1f)
                    {
                        bufferB[ThreadIds.X] += bufferA[ThreadIds.X] * x * y;
                    }
                }
            }
            else
            {
                bufferB[ThreadIds.X] = bufferA[ThreadIds.X];
            }            
        }
    }
}
