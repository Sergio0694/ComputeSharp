using System;
using System.Threading.Tasks;
using ComputeSharp.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputeSharp.Tests.Internals;

[TestClass]
[TestCategory("NativeObject")]
public class NativeObjectTests
{
    [TestMethod]
    public void NormalDispose_GettingLeasesFails()
    {
        DisposableObject obj = new();

        obj.Dispose();

        Assert.IsTrue(obj.IsDisposed);

        using NativeObject.Lease lease0 = obj.TryGetReferenceTrackingLease(out bool leaseTaken);

        Assert.IsFalse(leaseTaken);

        _ = Assert.ThrowsException<ObjectDisposedException>(() => obj.GetReferenceTrackingLease());
    }

    [TestMethod]
    public void DisposeWithLease()
    {
        DisposableObject obj = new();

        NativeObject.Lease lease0 = obj.GetReferenceTrackingLease();

        obj.Dispose();

        Assert.IsFalse(obj.IsDisposed);

        lease0.Dispose();

        Assert.IsTrue(obj.IsDisposed);

        // This doesn't do anything
        lease0.Dispose();
    }

    [TestMethod]
    public void DisposeWithTryGetLease()
    {
        DisposableObject obj = new();

        NativeObject.Lease lease0 = obj.TryGetReferenceTrackingLease(out bool leaseTaken);

        Assert.IsTrue(leaseTaken);

        obj.Dispose();

        Assert.IsFalse(obj.IsDisposed);

        lease0.Dispose();

        Assert.IsTrue(obj.IsDisposed);

        lease0.Dispose();
    }

    [TestMethod]
    public void DisposeWithDangerousAddRefsAndRelease()
    {
        DisposableObject obj = new();

        obj.DangerousAddRef();

        obj.Dispose();

        Assert.IsFalse(obj.IsDisposed);

        obj.DangerousRelease();

        Assert.IsTrue(obj.IsDisposed);
    }

    [TestMethod]
    public void Dispose_Multithreaded()
    {
        DisposableObject obj = new();

        _ = Parallel.For(0, 2048, i =>
        {
            for (int j = 0; j < 4; j++)
            {
                using NativeObject.Lease lease = obj.GetReferenceTrackingLease();
            }
        });

        Assert.IsFalse(obj.IsDisposed);

        obj.Dispose();

        Assert.IsTrue(obj.IsDisposed);
    }

    [TestMethod]
    public void Dispose_MultithreadedWithConcurrentDispose()
    {
        DisposableObject obj = new();

        _ = Parallel.For(0, 2048, i =>
        {
            bool isDisposed = false;

            if (i == 1024)
            {
                isDisposed = true;

                obj.Dispose();
            }

            for (int j = 0; j < 4; j++)
            {
                using NativeObject.Lease lease = obj.TryGetReferenceTrackingLease(out bool leaseTaken);

                if (isDisposed)
                {
                    Assert.IsFalse(leaseTaken);
                }
            }
        });

        Assert.IsTrue(obj.IsDisposed);
    }

    [TestMethod]
    public void Dispose_MultithreadedWithConcurrentDisposeAndOutstandingLease()
    {
        DisposableObject obj = new();

        NativeObject.Lease outstandingLease = obj.GetReferenceTrackingLease();

        _ = Parallel.For(0, 2048, i =>
        {
            bool isDisposed = false;

            if (i == 1024)
            {
                isDisposed = true;

                obj.Dispose();
            }

            for (int j = 0; j < 4; j++)
            {
                using NativeObject.Lease lease = obj.TryGetReferenceTrackingLease(out bool leaseTaken);

                if (isDisposed)
                {
                    Assert.IsFalse(leaseTaken);
                }
            }
        });

        Assert.IsFalse(obj.IsDisposed);

        outstandingLease.Dispose();

        Assert.IsTrue(obj.IsDisposed);
    }

    private sealed class DisposableObject : NativeObject
    {
        public bool IsDisposed { get; private set; }

        private protected override void OnDispose()
        {
            Assert.IsFalse(IsDisposed);

            IsDisposed = true;
        }
    }
}