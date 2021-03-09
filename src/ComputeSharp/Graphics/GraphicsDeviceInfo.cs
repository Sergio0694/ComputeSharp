using TerraFX.Interop;
using static TerraFX.Interop.DXGI_ADAPTER_FLAG;

namespace ComputeSharp
{
    /// <summary>
    /// A type containing info on a given device that can be used.
    /// </summary>
    public readonly struct GraphicsDeviceInfo
    {
        /// <summary>
        /// Creates a new <see cref="GraphicsDeviceInfo"/> instance with the specified parameters.
        /// </summary>
        /// <param name="dxgiDescription1">The available info for the associated device.</param>
        internal unsafe GraphicsDeviceInfo(DXGI_ADAPTER_DESC1* dxgiDescription1)
        {
            Luid = Luid.FromLUID(dxgiDescription1->AdapterLuid);
            Name = new string((char*)dxgiDescription1->Description);
            DedicatedMemorySize = dxgiDescription1->DedicatedVideoMemory;
            SharedMemorySize = dxgiDescription1->SharedSystemMemory;
            IsHardwareAccelerated = (dxgiDescription1->Flags & (uint)DXGI_ADAPTER_FLAG_SOFTWARE) == 0;
        }

        /// <summary>
        /// Gets the locally unique identifier for the current device.
        /// </summary>
        public Luid Luid { get; }

        /// <summary>
        /// Gets the name of the associated device.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the size of the dedicated memory for the associated device.
        /// </summary>
        public nuint DedicatedMemorySize { get; }

        /// <summary>
        /// Gets the size of the shared system memory for the associated device.
        /// </summary>
        public nuint SharedMemorySize { get; }

        /// <summary>
        /// Gets whether or not the associated device is hardware accelerated.
        /// This value is <see langword="false"/> for software fallback devices.
        /// </summary>
        public bool IsHardwareAccelerated { get; }
    }
}
