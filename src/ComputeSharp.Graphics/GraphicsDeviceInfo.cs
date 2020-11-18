using TerraFX.Interop;

namespace ComputeSharp.Graphics
{
    /// <summary>
    /// A type containing info on a given DX12.0 device that can be used.
    /// </summary>
    public readonly struct GraphicsDeviceInfo
    {
        /// <summary>
        /// Creates a new <see cref="GraphicsDeviceInfo"/> instance with the specified parameters.
        /// </summary>
        /// <param name="description">The available info for the associate device.</param>
        internal unsafe GraphicsDeviceInfo(DXGI_ADAPTER_DESC1* dxgiDescription1)
        {
            Luid = *(Luid*)&dxgiDescription1->AdapterLuid;
            Name = new string((char*)dxgiDescription1->Description);
            MemorySize = dxgiDescription1->DedicatedVideoMemory;
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
        /// Gets the size of the dedicated video memory for the associated device.
        /// </summary>
        public nuint MemorySize { get; }
    }
}
