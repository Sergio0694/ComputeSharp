namespace DirectX12GameEngine.Shaders.Numerics
{
    public struct UInt3
    {
        public uint X;

        public uint Y;

        public uint Z;

        public UInt2 XY => new UInt2 { X = X, Y = Y };
    }
}
