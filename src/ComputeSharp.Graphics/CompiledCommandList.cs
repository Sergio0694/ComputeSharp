using SharpDX.Direct3D12;

namespace ComputeSharp.Graphics
{
    public sealed class CompiledCommandList
    {
        internal CompiledCommandList(CommandList builder, CommandAllocator nativeCommandAllocator, GraphicsCommandList nativeCommandList)
        {
            Builder = builder;
            NativeCommandAllocator = nativeCommandAllocator;
            NativeCommandList = nativeCommandList;
        }

        internal CommandList Builder { get; set; }

        internal CommandAllocator NativeCommandAllocator { get; set; }

        internal GraphicsCommandList NativeCommandList { get; set; }
    }
}
