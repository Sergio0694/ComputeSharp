using System.Runtime.CompilerServices;
using ComputeSharp.D2D1.Shaders.Interop.Effects.ResourceManagers;

#pragma warning disable CS0649

namespace ComputeSharp.D2D1.Interop.Effects;

/// <inheritdoc/>
partial struct PixelShaderEffect
{
    /// <summary>
    /// A buffer of 16 <see cref="ID2D1ResourceTextureManager"/> objects.
    /// </summary>
    private unsafe struct ResourceTextureManagerBuffer
    {
        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 0.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager0;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 1.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager1;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 2.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager2;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 3.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager3;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 4.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager4;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 5.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager5;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 6.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager6;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 7.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager7;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 8.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager8;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 9.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager9;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 10.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager10;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 11.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager11;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 12.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager12;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 13.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager13;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 14.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager14;

        /// <summary>
        /// The pointer to the <see cref="ID2D1ResourceTextureManager"/> instance at index 15.
        /// </summary>
        public ID2D1ResourceTextureManager* ResourceTextureManager15;

        /// <summary>
        /// Gets a <see langword="ref"/> to the <see cref="ID2D1ResourceTextureManager"/> pointer at a given index.
        /// </summary>
        /// <param name="index">The index of the instance to retrieve.</param>
        /// <returns>A <see langword="ref"/> to the <see cref="ID2D1ResourceTextureManager"/> pointer at a given index.</returns>
        public ref ID2D1ResourceTextureManager* this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                ID2D1ResourceTextureManager** @this = (ID2D1ResourceTextureManager**)Unsafe.AsPointer(ref this);

                return ref @this[index];
            }
        }
    }
}