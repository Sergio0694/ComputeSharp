using SharpDX;
using SharpDX.DXGI;
using System;
using System.Runtime.InteropServices;
using Windows.Graphics.DirectX.Direct3D11;

namespace DirectX12GameEngine.Graphics
{
    internal static class Direct3DInterop
    {
        private static readonly Guid ID3D11Resource = new Guid("DC8E63F3-D12B-4952-B47B-5E45026A862D");

        [ComImport]
        [Guid("A9B3D012-3DF2-4EE3-B8D1-8695F457D3C1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComVisible(true)]
        private interface IDirect3DDxgiInterfaceAccess : IDisposable
        {
            IntPtr GetInterface([In] ref Guid iid);
        }

        internal static IDirect3DDevice CreateDirect3DDevice(Device dxgiDevice)
        {
            Result result = CreateDirect3D11DeviceFromDXGIDevice(dxgiDevice.NativePointer, out IntPtr graphicsDevice);

            if (result.Failure) throw new InvalidOperationException(result.Code.ToString());

            IDirect3DDevice d3DInteropDevice = (IDirect3DDevice)Marshal.GetObjectForIUnknown(graphicsDevice);
            Marshal.Release(graphicsDevice);

            return d3DInteropDevice;
        }

        internal static IDirect3DSurface CreateDirect3DSurface(Surface dxgiSurface)
        {
            Result result = CreateDirect3D11SurfaceFromDXGISurface(dxgiSurface.NativePointer, out IntPtr graphicsSurface);

            if (result.Failure) throw new InvalidOperationException(result.Code.ToString());

            IDirect3DSurface d3DSurface = (IDirect3DSurface)Marshal.GetObjectForIUnknown(graphicsSurface);
            Marshal.Release(graphicsSurface);

            return d3DSurface;
        }

        internal static Device CreateDXGIDevice(IDirect3DDevice direct3DDevice)
        {
            IDirect3DDxgiInterfaceAccess dxgiDeviceInterfaceAccess = (IDirect3DDxgiInterfaceAccess)direct3DDevice;
            IntPtr device = dxgiDeviceInterfaceAccess.GetInterface(ID3D11Resource);

            return new Device(device);
        }

        internal static Surface CreateDXGISurface(IDirect3DSurface direct3DSurface)
        {
            IDirect3DDxgiInterfaceAccess dxgiSurfaceInterfaceAccess = (IDirect3DDxgiInterfaceAccess)direct3DSurface;
            IntPtr surface = dxgiSurfaceInterfaceAccess.GetInterface(ID3D11Resource);

            return new Surface(surface);
        }

        [DllImport("d3d11.dll", EntryPoint = "CreateDirect3D11DeviceFromDXGIDevice",
            SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern Result CreateDirect3D11DeviceFromDXGIDevice(IntPtr dxgiDevice, out IntPtr graphicsDevice);

        [DllImport("d3d11.dll", EntryPoint = "CreateDirect3D11SurfaceFromDXGISurface",
            SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern Result CreateDirect3D11SurfaceFromDXGISurface(IntPtr dxgiSurface, out IntPtr graphicsSurface);
    }
}
