using System;

namespace DirectX12GameEngine.Graphics
{
    public class WindowHandle
    {
        public WindowHandle(IntPtr handle)
        {
            Handle = handle;
        }

        public IntPtr Handle { get; }
    }
}
