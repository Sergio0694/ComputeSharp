namespace DirectX12GameEngine.Graphics
{
    public struct BufferDescription
    {
        public BufferDescription(int sizeInBytes, BufferFlags bufferFlags, GraphicsHeapType heapType)
        {
            SizeInBytes = sizeInBytes;
            Flags = bufferFlags;
            HeapType = heapType;
        }

        public int SizeInBytes;

        public BufferFlags Flags;

        public GraphicsHeapType HeapType;
    }
}
