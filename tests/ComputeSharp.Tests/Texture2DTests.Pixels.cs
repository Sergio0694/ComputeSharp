using System;
using System.Numerics;
using System.Reflection;
using ComputeSharp.__Internals;
using ComputeSharp.Graphics.Buffers.Abstract;
using ComputeSharp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS0618

namespace ComputeSharp.Tests
{
    public partial class Texture2DTests
    {
        [TestMethod]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Bgra32), typeof(Vector4))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Bgra32), typeof(Float4))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Bgra32), typeof(Double4))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(R16), typeof(float))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(R16), typeof(double))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(R8), typeof(float))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(R8), typeof(double))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Rg16), typeof(Vector2))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Rg16), typeof(Float2))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Rg16), typeof(Double2))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Rg32), typeof(Vector2))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Rg32), typeof(Float2))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Rg32), typeof(Double2))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Rgba32), typeof(Vector4))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Rgba32), typeof(Float4))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Rgba32), typeof(Double4))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Rgba64), typeof(Vector4))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Rgba64), typeof(Float4))]
        [DataRow(typeof(ReadOnlyTexture2D<,>), typeof(Rgba64), typeof(Double4))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Bgra32), typeof(Vector4))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Bgra32), typeof(Float4))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Bgra32), typeof(Double4))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(R16), typeof(float))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(R16), typeof(double))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(R8), typeof(float))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(R8), typeof(double))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Rg16), typeof(Vector2))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Rg16), typeof(Float2))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Rg16), typeof(Double2))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Rg32), typeof(Vector2))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Rg32), typeof(Float2))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Rg32), typeof(Double2))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Rgba32), typeof(Vector4))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Rgba32), typeof(Float4))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Rgba32), typeof(Double4))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Rgba64), typeof(Vector4))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Rgba64), typeof(Float4))]
        [DataRow(typeof(ReadWriteTexture2D<,>), typeof(Rgba64), typeof(Double4))]
        public void Allocate_Uninitialized_Pixel_Ok(Type textureType, Type t, Type tPixel)
        {
            static void Test<T, TPixel>(Type textureType)
                where T : unmanaged, IUnorm<TPixel>
                where TPixel : unmanaged
            {
                using Texture2D<T> texture = Gpu.Default.AllocateTexture2D<T, TPixel>(textureType, 128, 128);

                Assert.IsNotNull(texture);
                Assert.AreEqual(texture.Width, 128);
                Assert.AreEqual(texture.Height, 128);
                Assert.AreSame(texture.GraphicsDevice, Gpu.Default);

                if (textureType == typeof(ReadOnlyTexture2D<,>))
                {
                    Assert.IsTrue(texture is ReadOnlyTexture2D<T, TPixel>);
                }
                else
                {
                    Assert.IsTrue(texture is ReadWriteTexture2D<T, TPixel>);
                }
            }

            try
            {
                new Action<Type>(Test<Rgba32, Vector4>).Method.GetGenericMethodDefinition().MakeGenericMethod(t, tPixel).Invoke(null, new[] { textureType });
            }
            catch (TargetInvocationException e) when (e.InnerException is not null)
            {
                throw e.InnerException;
            }
        }
    }
}
