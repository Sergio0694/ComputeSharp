using System.ComponentModel;
using System.Diagnostics;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComputeSharp.Graphics.Commands;

namespace ComputeSharp.Tests.Internals;

[TestClass]
[DoNotParallelize]
public unsafe partial class FenceWaitTests
{
    private sealed class PublicationBox { public FenceWaitPublication Value; }

    private struct Context
    {
        public FenceWaitPublication Publication;
        public nint Event;
        public nint Wait;
        public GCHandle Completion;
    }

    [LibraryImport("kernel32.dll", SetLastError = true)]
    private static partial nint CreateEventW(nint attributes, int manualReset, int initialState, nint name);
    [LibraryImport("kernel32.dll", SetLastError = true)]
    private static partial int SetEvent(nint handle);
    [LibraryImport("kernel32.dll", SetLastError = true)]
    private static partial int CloseHandle(nint handle);
    [LibraryImport("kernel32.dll", SetLastError = true)]
    private static partial int RegisterWaitForSingleObject(nint* wait, nint handle,
        delegate* unmanaged<void*, byte, void> callback, void* context, uint timeout, uint flags);
    [LibraryImport("kernel32.dll", SetLastError = true)]
    private static partial int UnregisterWaitEx(nint wait, nint completionEvent);

    [TestMethod]
    public void PublicationAndNativeWaitLifetime()
    {
        foreach (bool callbackFirst in new[] { false, true })
        {
            FenceWaitPublication value = default;
            bool first = callbackFirst ? value.OnSignaled() : value.OnRegistered();
            bool second = callbackFirst ? value.OnRegistered() : value.OnSignaled();
            if (first || !second)
            {
                throw new Exception("Fence wait publication assigned the wrong cleanup owner.");
            }

            try { _ = value.OnRegistered(); throw new Exception("Duplicate publication accepted."); }
            catch (InvalidOperationException) { }

            try { _ = value.OnSignaled(); throw new Exception("Duplicate signal accepted."); }
            catch (InvalidOperationException) { }
        }

        _ = Parallel.For(0, 10_000, _ =>
        {
            PublicationBox box = new();
            bool published = false, signaled = false;
            Parallel.Invoke(() => published = box.Value.OnRegistered(), () => signaled = box.Value.OnSignaled());
            if (published == signaled)
            {
                throw new Exception("Concurrent publication did not produce exactly one cleanup owner.");
            }
        });
        if (!FenceWaitPublication.UnregisterAccepted(true, 0, false)
            || !FenceWaitPublication.UnregisterAccepted(false, 997, true)
            || FenceWaitPublication.UnregisterAccepted(false, 997, false)
            || FenceWaitPublication.UnregisterAccepted(false, 6, true))
        {
            throw new Exception("Unregister success/pending/error classification failed.");
        }

        // Warm the thread pool before measuring kernel handles, then alternate
        // callback-before-publication and callback-after-publication explicitly.
        for (int i = 0; i < 32; i++)
        {
            NativeProbe(i % 2 == 0);
        }

        int before = Handles();
        for (int i = 0; i < 2_000; i++)
        {
            NativeProbe(i % 2 == 0);
        }

        Thread.Sleep(100); // OS finishes deferred deletion after callbacks return.
        int after = Handles();
        if (after > before + 32)
        {
            throw new Exception($"Native wait registrations accumulated: handles {before} -> {after}.");
        }

        // Verify source-generated last-error capture without handing an invalid
        // synchronization object to the OS thread pool.
        int invalid = SetEvent(0);
        int error = Marshal.GetLastPInvokeError();
        if (invalid != 0 || error != 6)
        {
            throw new Exception($"Native failure did not preserve ERROR_INVALID_HANDLE: {invalid}/{error}.");
        }
    }

    private static int Handles() { using Process process = Process.GetCurrentProcess(); return process.HandleCount; }

    private static void NativeProbe(bool early)
    {
        TaskCompletionSource<bool> result = new(TaskCreationOptions.RunContinuationsAsynchronously);
        Context* context = (Context*)NativeMemory.AllocZeroed((nuint)sizeof(Context));
        if (context is null)
        {
            throw new OutOfMemoryException();
        }

        context->Completion = GCHandle.Alloc(result);
        context->Event = CreateEventW(0, 0, early ? 1 : 0, 0);
        if (context->Event == 0)
        {
            throw new Win32Exception(Marshal.GetLastPInvokeError());
        }

        nint wait;
        if (RegisterWaitForSingleObject(&wait, context->Event, &Callback, context, uint.MaxValue, 8) == 0)
        {
            throw new Win32Exception(Marshal.GetLastPInvokeError());
        }

        if (early)
        {
            // The test controls publication so the early path is guaranteed,
            // rather than merely hoping the event callback wins a timing race.
            Stopwatch deadline = Stopwatch.StartNew();
            while (!result.Task.IsCompleted && Volatile.Read(ref earlySignal) == 0)
            {
                if (deadline.Elapsed.TotalSeconds > 10)
                {
                    throw new TimeoutException("Native early callback never arrived.");
                }

                _ = Thread.Yield();
            }

            Volatile.Write(ref earlySignal, 0);
        }

        context->Wait = wait;
        if (context->Publication.OnRegistered())
        {
            Cleanup(context, false);
        }
        else if (!early && SetEvent(context->Event) == 0)
        {
            throw new Win32Exception(Marshal.GetLastPInvokeError());
        }

        if (!result.Task.Wait(TimeSpan.FromSeconds(10)))
        {
            throw new TimeoutException("Native wait cleanup never completed.");
        }

        _ = result.Task.GetAwaiter().GetResult();
    }

    private static int earlySignal;

    [UnmanagedCallersOnly]
    private static void Callback(void* pointer, byte timedOut)
    {
        Context* context = (Context*)pointer;
        if (context->Publication.OnSignaled())
        {
            Cleanup(context, true);
        }
        else
        {
            Volatile.Write(ref earlySignal, 1);
        }
    }

    private static void Cleanup(Context* context, bool insideCallback)
    {
        int ok = UnregisterWaitEx(context->Wait, insideCallback ? 0 : -1);
        int error = ok == 0 ? Marshal.GetLastPInvokeError() : 0;
        TaskCompletionSource<bool> completion = (TaskCompletionSource<bool>)context->Completion.Target!;
        if (!FenceWaitPublication.UnregisterAccepted(ok != 0, error, insideCallback))
        {
            completion.SetException(new Win32Exception(error));
            return;
        }

        context->Completion.Free();
        int closed = CloseHandle(context->Event);
        NativeMemory.Free(context);
        if (closed == 0)
        {
            completion.SetException(new Exception("Native event close failed."));
        }
        else
        {
            completion.SetResult(true);
        }
    }
}