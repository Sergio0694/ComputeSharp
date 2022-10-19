using System;
using System.Runtime.CompilerServices;
using ComputeSharp.Interop;

namespace ComputeSharp.Resources
{
    /// <inheritdoc/>
    partial class Buffer<T>
    {
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations.
        /// </summary>
        ~Buffer()
        {
            this.referenceTracker.Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.referenceTracker.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IReferenceTrackedObject.GetReferenceTracker"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref ReferenceTracker GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }

        /// <inheritdoc/>
        ref ReferenceTracker IReferenceTrackedObject.GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }
    }
}

namespace ComputeSharp.Resources
{
    /// <inheritdoc/>
    partial class Texture1D<T>
    {
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations.
        /// </summary>
        ~Texture1D()
        {
            this.referenceTracker.Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.referenceTracker.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IReferenceTrackedObject.GetReferenceTracker"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref ReferenceTracker GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }

        /// <inheritdoc/>
        ref ReferenceTracker IReferenceTrackedObject.GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }
        /// <inheritdoc cref="IReferenceTrackedObject.DangerousRelease"/>
        protected virtual partial void DangerousRelease();

        /// <inheritdoc/>
        void IReferenceTrackedObject.DangerousRelease()
        {
            DangerousRelease();
        }
    }
}

namespace ComputeSharp.Resources
{
    /// <inheritdoc/>
    partial class Texture2D<T>
    {
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations.
        /// </summary>
        ~Texture2D()
        {
            this.referenceTracker.Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.referenceTracker.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IReferenceTrackedObject.GetReferenceTracker"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref ReferenceTracker GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }

        /// <inheritdoc/>
        ref ReferenceTracker IReferenceTrackedObject.GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }
        /// <inheritdoc cref="IReferenceTrackedObject.DangerousRelease"/>
        protected virtual partial void DangerousRelease();

        /// <inheritdoc/>
        void IReferenceTrackedObject.DangerousRelease()
        {
            DangerousRelease();
        }
    }
}

namespace ComputeSharp.Resources
{
    /// <inheritdoc/>
    partial class Texture3D<T>
    {
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations.
        /// </summary>
        ~Texture3D()
        {
            this.referenceTracker.Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.referenceTracker.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IReferenceTrackedObject.GetReferenceTracker"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref ReferenceTracker GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }

        /// <inheritdoc/>
        ref ReferenceTracker IReferenceTrackedObject.GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }
        /// <inheritdoc cref="IReferenceTrackedObject.DangerousRelease"/>
        protected virtual partial void DangerousRelease();

        /// <inheritdoc/>
        void IReferenceTrackedObject.DangerousRelease()
        {
            DangerousRelease();
        }
    }
}

namespace ComputeSharp.Resources
{
    /// <inheritdoc/>
    partial class TransferBuffer<T>
    {
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations.
        /// </summary>
        ~TransferBuffer()
        {
            this.referenceTracker.Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.referenceTracker.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IReferenceTrackedObject.GetReferenceTracker"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref ReferenceTracker GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }

        /// <inheritdoc/>
        ref ReferenceTracker IReferenceTrackedObject.GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }
    }
}

namespace ComputeSharp.Resources
{
    /// <inheritdoc/>
    partial class TransferTexture1D<T>
    {
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations.
        /// </summary>
        ~TransferTexture1D()
        {
            this.referenceTracker.Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.referenceTracker.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IReferenceTrackedObject.GetReferenceTracker"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref ReferenceTracker GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }

        /// <inheritdoc/>
        ref ReferenceTracker IReferenceTrackedObject.GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }
    }
}

namespace ComputeSharp.Resources
{
    /// <inheritdoc/>
    partial class TransferTexture2D<T>
    {
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations.
        /// </summary>
        ~TransferTexture2D()
        {
            this.referenceTracker.Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.referenceTracker.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IReferenceTrackedObject.GetReferenceTracker"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref ReferenceTracker GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }

        /// <inheritdoc/>
        ref ReferenceTracker IReferenceTrackedObject.GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }
    }
}

namespace ComputeSharp.Resources
{
    /// <inheritdoc/>
    partial class TransferTexture3D<T>
    {
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations.
        /// </summary>
        ~TransferTexture3D()
        {
            this.referenceTracker.Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.referenceTracker.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IReferenceTrackedObject.GetReferenceTracker"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref ReferenceTracker GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }

        /// <inheritdoc/>
        ref ReferenceTracker IReferenceTrackedObject.GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }
    }
}

namespace ComputeSharp
{
    /// <inheritdoc/>
    partial class GraphicsDevice
    {
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations.
        /// </summary>
        ~GraphicsDevice()
        {
            this.referenceTracker.Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.referenceTracker.Dispose();

            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="IReferenceTrackedObject.GetReferenceTracker"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref ReferenceTracker GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }

        /// <inheritdoc/>
        ref ReferenceTracker IReferenceTrackedObject.GetReferenceTracker()
        {
            return ref this.referenceTracker;
        }
    }
}
