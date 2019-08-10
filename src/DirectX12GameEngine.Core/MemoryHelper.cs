using System;
using System.Runtime.CompilerServices;

namespace DirectX12GameEngine.Core
{
    public static class MemoryHelper
    {
        public static unsafe void Copy(IntPtr source, IntPtr destination, int sizeInBytesToCopy)
        {
            Copy(new Span<byte>(source.ToPointer(), sizeInBytesToCopy), destination);
        }

        public static unsafe void Copy<T>(Span<T> source, IntPtr destination) where T : unmanaged
        {
            source.CopyTo(new Span<T>(destination.ToPointer(), source.Length));
        }

        public static unsafe void Copy<T>(IntPtr source, Span<T> destination) where T : unmanaged
        {
            new Span<T>(source.ToPointer(), destination.Length).CopyTo(destination);
        }

        public static unsafe void Copy<T>(in T source, IntPtr destination) where T : unmanaged
        {
            Unsafe.Copy(destination.ToPointer(), ref Unsafe.AsRef(source));
        }

        public static unsafe void Copy<T>(IntPtr source, ref T destination) where T : unmanaged
        {
            Unsafe.Copy(ref destination, source.ToPointer());
        }
    }
}
