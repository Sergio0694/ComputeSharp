namespace DirectX12GameEngine.Graphics
{
    public struct TextureDescription
    {
        public TextureDimension Dimension;

        public int Width;

        public int Height;

        public int Depth;

        public int ArraySize;

        public int MipLevels;

        public PixelFormat Format;

        public int MultisampleCount;

        public GraphicsHeapType HeapType;

        public TextureFlags Flags;

        public static TextureDescription New2D(int width, int height, PixelFormat format, TextureFlags textureFlags = TextureFlags.ShaderResource, int mipCount = 1, int arraySize = 1, int multisampleCount = 1, GraphicsHeapType heapType = GraphicsHeapType.Default)
        {
            return new TextureDescription
            {
                Dimension = TextureDimension.Texture2D,
                Width = width,
                Height = height,
                Depth = 1,
                ArraySize = arraySize,
                MultisampleCount = multisampleCount,
                Flags = textureFlags,
                Format = format,
                MipLevels = mipCount,
                HeapType = heapType,
            };
        }

        public static implicit operator TextureDescription(ImageDescription description)
        {
            return new TextureDescription()
            {
                Dimension = description.Dimension,
                Width = description.Width,
                Height = description.Height,
                Depth = description.Depth,
                ArraySize = description.ArraySize,
                MipLevels = description.MipLevels,
                Format = description.Format,
                Flags = TextureFlags.ShaderResource,
                MultisampleCount = 1,
                HeapType = GraphicsHeapType.Default
            };
        }

        public static implicit operator ImageDescription(TextureDescription description)
        {
            return new ImageDescription()
            {
                Dimension = description.Dimension,
                Width = description.Width,
                Height = description.Height,
                Depth = description.Depth,
                ArraySize = description.ArraySize,
                MipLevels = description.MipLevels,
                Format = description.Format,
            };
        }
    }
}
