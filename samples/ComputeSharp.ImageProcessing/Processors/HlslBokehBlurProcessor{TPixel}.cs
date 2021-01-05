using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.BokehBlur.Processors
{
    /// <inheritdoc/>
    public sealed partial class HlslBokehBlurProcessor
    {
        /// <summary>
        /// Applies bokeh blur processing to the image.
        /// </summary>
        /// <remarks>This processor is based on the code from Mike Pound, see <a href="https://github.com/mikepound/convolve">github.com/mikepound/convolve</a>.</remarks>
        public sealed partial class Implementation : ImageProcessor<ImageSharpRgba32>
        {
            /// <summary>
            /// The kernel radius.
            /// </summary>
            private readonly int Radius;

            /// <summary>
            /// The maximum size of the kernel in either direction.
            /// </summary>
            private readonly int KernelSize;

            /// <summary>
            /// The number of components to use when applying the bokeh blur.
            /// </summary>
            private readonly int ComponentsCount;

            /// <summary>
            /// The kernel parameters to use for the current instance (a: X, b: Y, A: Z, B: W)
            /// </summary>
            private readonly Vector4[] KernelParameters;

            /// <summary>
            /// The kernel components for the current instance.
            /// </summary>
            private readonly Complex64[][] Kernels;

            /// <summary>
            /// The scaling factor for kernel values.
            /// </summary>
            private readonly float KernelsScale;

            /// <summary>
            /// The mapping of initialized complex kernels and parameters, to speed up the initialization of new <see cref="HlslBokehBlurProcessor{TPixel}"/> instances.
            /// </summary>
            private static readonly ConcurrentDictionary<(int Radius, int ComponentsCount), (Vector4[] Parameters, float Scale, Complex64[][] Kernels)> Cache = new();

            /// <summary>
            /// Initializes a new instance of the <see cref="Implementation"/> class.
            /// </summary>
            /// <param name="definition">The <see cref="HlslBokehBlurProcessor"/> defining the processor parameters.</param>
            /// <param name="configuration">The configuration which allows altering default behaviour or extending the library.</param>
            /// <param name="source">The source <see cref="Image{TPixel}"/> instance to modify.</param>
            /// <param name="sourceRectangle">The source <see cref="Rectangle"/> that indicates the area to edit.</param>
            public Implementation(HlslBokehBlurProcessor definition, Configuration configuration, Image<ImageSharpRgba32> source, Rectangle sourceRectangle)
                : base(configuration, source, sourceRectangle)
            {
                Radius = definition.Radius;
                KernelSize = Radius * 2 + 1;
                ComponentsCount = definition.Components;

                // Reuse the initialized values from the cache, if possible
                var parameters = (Radius, ComponentsCount);
                if (Cache.TryGetValue(parameters, out var info))
                {
                    KernelParameters = info.Parameters;
                    KernelsScale = info.Scale;
                    Kernels = info.Kernels;
                }
                else
                {
                    // Initialize the complex kernels and parameters with the current arguments
                    (KernelParameters, KernelsScale) = GetParameters();
                    Kernels = CreateComplexKernels();
                    NormalizeKernels();

                    // Store them in the cache for future use
                    Cache.TryAdd(parameters, (KernelParameters, KernelsScale, Kernels));
                }
            }

            /// <summary>
            /// Gets the kernel scales to adjust the component values in each kernel.
            /// </summary>
            private static IReadOnlyList<float> KernelScales { get; } = new[] { 1.4f, 1.2f, 1.2f, 1.2f, 1.2f, 1.2f };

            /// <summary>
            /// Gets the available bokeh blur kernel parameters.
            /// </summary>
            private static IReadOnlyList<Vector4[]> KernelComponents { get; } = new[]
            {
                // 1 component
                new[] { new Vector4(0.862325f, 1.624835f, 0.767583f, 1.862321f) },

                // 2 components
                new[]
                {
                    new Vector4(0.886528f, 5.268909f, 0.411259f, -0.548794f),
                    new Vector4(1.960518f, 1.558213f, 0.513282f, 4.56111f)
                },

                // 3 components
                new[]
                {
                    new Vector4(2.17649f, 5.043495f, 1.621035f, -2.105439f),
                    new Vector4(1.019306f, 9.027613f, -0.28086f, -0.162882f),
                    new Vector4(2.81511f, 1.597273f, -0.366471f, 10.300301f)
                },

                // 4 components
                new[]
                {
                    new Vector4(4.338459f, 1.553635f, -5.767909f, 46.164397f),
                    new Vector4(3.839993f, 4.693183f, 9.795391f, -15.227561f),
                    new Vector4(2.791880f, 8.178137f, -3.048324f, 0.302959f),
                    new Vector4(1.342190f, 12.328289f, 0.010001f, 0.244650f)
                },

                // 5 components
                new[]
                {
                    new Vector4(4.892608f, 1.685979f, -22.356787f, 85.91246f),
                    new Vector4(4.71187f, 4.998496f, 35.918936f, -28.875618f),
                    new Vector4(4.052795f, 8.244168f, -13.212253f, -1.578428f),
                    new Vector4(2.929212f, 11.900859f, 0.507991f, 1.816328f),
                    new Vector4(1.512961f, 16.116382f, 0.138051f, -0.01f)
                },

                // 6 components
                new[]
                {
                    new Vector4(5.143778f, 2.079813f, -82.326596f, 111.231024f),
                    new Vector4(5.612426f, 6.153387f, 113.878661f, 58.004879f),
                    new Vector4(5.982921f, 9.802895f, 39.479083f, -162.028887f),
                    new Vector4(6.505167f, 11.059237f, -71.286026f, 95.027069f),
                    new Vector4(3.869579f, 14.81052f, 1.405746f, -3.704914f),
                    new Vector4(2.201904f, 19.032909f, -0.152784f, -0.107988f)
                }
            };

            /// <summary>
            /// Gets the kernel parameters and scaling factor for the current count value in the current instance.
            /// </summary>
            private (Vector4[] Parameters, float Scale) GetParameters()
            {
                // Prepare the kernel components
                int index = Math.Max(0, Math.Min(ComponentsCount - 1, KernelComponents.Count));
                return (KernelComponents[index], KernelScales[index]);
            }

            /// <summary>
            /// Creates the collection of complex 1D kernels with the specified parameters.
            /// </summary>
            private Complex64[][] CreateComplexKernels()
            {
                var kernels = new Complex64[KernelParameters.Length][];
                ref Vector4 baseRef = ref MemoryMarshal.GetReference(KernelParameters.AsSpan());
                for (int i = 0; i < KernelParameters.Length; i++)
                {
                    ref Vector4 paramsRef = ref Unsafe.Add(ref baseRef, i);
                    kernels[i] = CreateComplex1DKernel(paramsRef.X, paramsRef.Y);
                }

                return kernels;
            }

            /// <summary>
            /// Creates a complex 1D kernel with the specified parameters.
            /// </summary>
            /// <param name="a">The exponential parameter for each complex component.</param>
            /// <param name="b">The angle component for each complex component.</param>
            private Complex64[] CreateComplex1DKernel(float a, float b)
            {
                var kernel = new Complex64[KernelSize];
                ref Complex64 baseRef = ref MemoryMarshal.GetReference(kernel.AsSpan());
                int r = Radius, n = -r;

                for (int i = 0; i < KernelSize; i++, n++)
                {
                    // Incrementally compute the range values
                    float value = n * KernelsScale * (1f / r);
                    value *= value;

                    // Fill in the complex kernel values
                    Unsafe.Add(ref baseRef, i) = new Complex64(
                        MathF.Exp(-a * value) * MathF.Cos(b * value),
                        MathF.Exp(-a * value) * MathF.Sin(b * value));
                }

                return kernel;
            }

            /// <summary>
            /// Normalizes the kernels with respect to A * real + B * imaginary.
            /// </summary>
            private void NormalizeKernels()
            {
                // Calculate the complex weighted sum
                float total = 0;
                Span<Complex64[]> kernelsSpan = Kernels.AsSpan();
                ref Complex64[] baseKernelsRef = ref MemoryMarshal.GetReference(kernelsSpan);
                ref Vector4 baseParamsRef = ref MemoryMarshal.GetReference(KernelParameters.AsSpan());

                for (int i = 0; i < KernelParameters.Length; i++)
                {
                    ref Complex64[] kernelRef = ref Unsafe.Add(ref baseKernelsRef, i);
                    int length = kernelRef.Length;
                    ref Complex64 valueRef = ref kernelRef[0];
                    ref Vector4 paramsRef = ref Unsafe.Add(ref baseParamsRef, i);

                    for (int j = 0; j < length; j++)
                    {
                        for (int k = 0; k < length; k++)
                        {
                            ref Complex64 jRef = ref Unsafe.Add(ref valueRef, j);
                            ref Complex64 kRef = ref Unsafe.Add(ref valueRef, k);
                            total +=
                                paramsRef.Z * (jRef.Real * kRef.Real - jRef.Imaginary * kRef.Imaginary)
                                + paramsRef.W * (jRef.Real * kRef.Imaginary + jRef.Imaginary * kRef.Real);
                        }
                    }
                }

                // Normalize the kernels
                float scalar = 1f / MathF.Sqrt(total);
                for (int i = 0; i < kernelsSpan.Length; i++)
                {
                    ref Complex64[] kernelsRef = ref Unsafe.Add(ref baseKernelsRef, i);
                    int length = kernelsRef.Length;
                    ref Complex64 valueRef = ref kernelsRef[0];

                    for (int j = 0; j < length; j++)
                    {
                        Unsafe.Add(ref valueRef, j) *= scalar;
                    }
                }
            }

            /// <inheritdoc/>
            protected override void OnFrameApply(ImageFrame<ImageSharpRgba32> source)
            {
                foreach (Memory<ImageSharpRgba32> memory in source.GetPixelMemoryGroup())
                {
                    // Preliminary gamma highlight pass
                    using IMemoryOwner<Vector4> source4 = GetExposedVector4Buffer(memory);

                    using ReadOnlyTexture2D<Vector4> sourceTexture = Gpu.Default.AllocateReadOnlyTexture2D<Vector4>(source4.Memory.Span, source.Width, source.Height);
                    using ReadWriteTexture2D<Vector4> processingTexture = Gpu.Default.AllocateReadWriteTexture2D<Vector4>(source.Width, source.Height);
                    using ReadWriteTexture2D<Vector4> realTexture = Gpu.Default.AllocateReadWriteTexture2D<Vector4>(source.Width, source.Height);
                    using ReadWriteTexture2D<Vector4> imaginaryTexture = Gpu.Default.AllocateReadWriteTexture2D<Vector4>(source.Width, source.Height);
                    using ReadOnlyBuffer<Complex64> kernelBuffer = Gpu.Default.AllocateReadOnlyBuffer<Complex64>(KernelSize);

                    ref Vector4 param0 = ref KernelParameters[0]; // Avoid bounds check to access the kernel parameters

                    // Perform two 1D convolutions for each component in the current instance
                    for (int j = 0; j < Kernels.Length; j++)
                    {
                        kernelBuffer.SetData(Kernels[j]);
                        Vector4 parameters = Unsafe.Add(ref param0, j);

                        ApplyVerticalConvolution(sourceTexture, realTexture, imaginaryTexture, kernelBuffer);
                        ApplyHorizontalConvolutionAndAccumulatePartials(realTexture, imaginaryTexture, processingTexture, kernelBuffer, parameters.Z, parameters.W);
                    }

                    // Apply the inverse gamma exposure pass
                    processingTexture.GetData(source4.Memory.Span);

                    // Write the final pixel data
                    ApplyInverseGammaExposure(source4.Memory, memory);
                }
            }

            /// <summary>
            /// Performs a vertical 1D complex convolution with the specified parameters.
            /// </summary>
            /// <param name="source">The source <see cref="ReadOnlyTexture2D{T}"/> to read data from.</param>
            /// <param name="reals">The target <see cref="ReadWriteTexture2D{T}"/> to write the real results to.</param>
            /// <param name="imaginaries">The target <see cref="ReadWriteTexture2D{T}"/> to write the imaginary results to.</param>
            /// <param name="kernel">The <see cref="ReadOnlyBuffer{T}"/> with the values for the current complex kernel.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void ApplyVerticalConvolution(
                ReadOnlyTexture2D<Vector4> source,
                ReadWriteTexture2D<Vector4> reals,
                ReadWriteTexture2D<Vector4> imaginaries,
                ReadOnlyBuffer<Complex64> kernel)
            {
                int height = Source.Height;
                int width = Source.Width;

                VerticalConvolutionProcessor shader = new(
                    maxY: height - 1,
                    maxX: width - 1,
                    kernelLength: kernel.Length,
                    source,
                    reals,
                    imaginaries,
                    kernel);

                Gpu.Default.For(width, height, in shader);
            }

            /// <summary>
            /// Kernel for the vertical convolution pass.
            /// </summary>
            [AutoConstructor]
            internal partial struct VerticalConvolutionProcessor : IComputeShader
            {
                public int maxY;
                public int maxX;
                public int kernelLength;

                public ReadOnlyTexture2D<Vector4> source;
                public ReadWriteTexture2D<Vector4> reals;
                public ReadWriteTexture2D<Vector4> imaginaries;
                public ReadOnlyBuffer<Complex64> kernel;

                /// <inheritdoc/>
                public void Execute(ThreadIds ids)
                {
                    Vector4 real = Vector4.Zero;
                    Vector4 imaginary = Vector4.Zero;
                    int radiusY = kernelLength >> 1;
                    int sourceOffsetColumnBase = ids.X;

                    for (int i = 0; i < kernelLength; i++)
                    {
                        int offsetY = Hlsl.Clamp(ids.Y + i - radiusY, 0, maxY);
                        int offsetX = Hlsl.Clamp(sourceOffsetColumnBase, 0, maxX);
                        Vector4 color = source[offsetX, offsetY];
                        Complex64 factors = kernel[i];

                        real += factors.Real * color;
                        imaginary += factors.Imaginary * color;
                    }

                    reals[ids.XY] = real;
                    imaginaries[ids.XY] = imaginary;
                }
            }

            /// <summary>
            /// Performs an horizontal 1D complex convolution with the specified parameters.
            /// </summary>
            /// <param name="reals">The source <see cref="ReadWriteTexture2D{T}"/> to read the real results from.</param>
            /// <param name="imaginaries">The source <see cref="ReadWriteTexture2D{T}"/> to read the imaginary results from.</param>
            /// <param name="kernel">The <see cref="ReadOnlyBuffer{T}"/> with the values for the current complex kernel.</param>
            /// <param name="z">The weight factor for the real component of the complex pixel values.</param>
            /// <param name="w">The weight factor for the imaginary component of the complex pixel values.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void ApplyHorizontalConvolutionAndAccumulatePartials(
                ReadWriteTexture2D<Vector4> reals,
                ReadWriteTexture2D<Vector4> imaginaries,
                ReadWriteTexture2D<Vector4> target,
                ReadOnlyBuffer<Complex64> kernel,
                float z,
                float w)
            {
                int height = Source.Height;
                int width = Source.Width;

                HorizontalConvolutionAndAccumulatePartialsProcessor shader = new(
                    maxY: height - 1,
                    maxX: width - 1,
                    kernelLength: kernel.Length,
                    z,
                    w,
                    reals,
                    imaginaries,
                    target,
                    kernel);

                Gpu.Default.For(width, height, in shader);
            }

            /// <summary>
            /// Kernel for the horizontal convolution pass.
            /// </summary>
            [AutoConstructor]
            internal partial struct HorizontalConvolutionAndAccumulatePartialsProcessor : IComputeShader
            {
                public int maxY;
                public int maxX;
                public int kernelLength;
                public float z;
                public float w;

                public ReadWriteTexture2D<Vector4> reals;
                public ReadWriteTexture2D<Vector4> imaginaries;
                public ReadWriteTexture2D<Vector4> target;
                public ReadOnlyBuffer<Complex64> kernel;

                /// <inheritdoc/>
                public void Execute(ThreadIds ids)
                {
                    Vector4 real = Vector4.Zero;
                    Vector4 imaginary = Vector4.Zero;
                    int radiusX = kernelLength >> 1;
                    int sourceOffsetColumnBase = ids.X;
                    int offsetY = Hlsl.Clamp(ids.Y, 0, maxY);

                    for (int i = 0; i < kernelLength; i++)
                    {
                        int offsetX = Hlsl.Clamp(sourceOffsetColumnBase + i - radiusX, 0, maxX);
                        Vector4 sourceReal = reals[offsetX, offsetY];
                        Vector4 sourceImaginary = imaginaries[offsetX, offsetY];
                        Complex64 factors = kernel[i];

                        real += factors.Real * sourceReal - factors.Imaginary * sourceImaginary;
                        imaginary += factors.Real * sourceImaginary + factors.Imaginary * sourceReal;
                    }

                    target[ids.XY] += real * z + imaginary * w;
                }
            }

            /// <summary>
            /// Applies the gamma correction/highlight to the input pixel buffer and returns an <see cref="IMemoryOwner{T}"/> instance with <see cref="Vector4"/> values.
            /// </summary>
            /// <param name="source">The source image.</param>
            [Pure]
            private IMemoryOwner<Vector4> GetExposedVector4Buffer(Memory<ImageSharpRgba32> source)
            {
                IMemoryOwner<Vector4> target = Configuration.MemoryAllocator.Allocate<Vector4>(source.Length);

                int width = Source.Width;
                int height = source.Length / width;

                ParallelRowIterator.IterateRows(
                    Configuration,
                    new Rectangle(0, 0, width, height),
                    new GetExposedVector4BufferProcessor(width, source, target.Memory, Configuration));

                return target;
            }

            /// <summary>
            /// The processor for <see cref="GetExposedVector4Buffer"/>.
            /// </summary>
            [AutoConstructor]
            private readonly partial struct GetExposedVector4BufferProcessor : IRowOperation
            {
                private readonly int width;

                private readonly Memory<ImageSharpRgba32> source;
                private readonly Memory<Vector4> target;
                private readonly Configuration configuration;

                /// <inheritdoc/>
                public void Invoke(int y)
                {
                    int offset = y * width;

                    Span<ImageSharpRgba32> rowTPixel = source.Span.Slice(offset, width);
                    Span<Vector4> rowVector4 = target.Span.Slice(offset, width);

                    PixelOperations<ImageSharpRgba32>.Instance.ToVector4(configuration, rowTPixel, rowVector4);

                    foreach (ref Vector4 v in rowVector4)
                    {
                        Vector4 pixel = v;
                        float alpha = pixel.W;

                        pixel = pixel * pixel * pixel;
                        pixel.W = alpha;

                        v = pixel;
                    }
                }
            }

            /// <summary>
            /// Applies the inverse gamma exposure pass to compute the final pixel values for a target image.
            /// </summary>
            /// <param name="source">The source <see cref="Vector4"/> buffer to read from.</param>
            /// <param name="target">The target image.</param>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void ApplyInverseGammaExposure(Memory<Vector4> source, Memory<ImageSharpRgba32> target)
            {
                int width = Source.Width;
                int height = source.Length / width;

                ParallelRowIterator.IterateRows(
                    Configuration,
                    new Rectangle(0, 0, width, height),
                    new ApplyInverseGammaExposureProcessor(width, source, target, Configuration));
            }

            /// <summary>
            /// The processor for <see cref="ApplyInverseGammaExposure"/>.
            /// </summary>
            [AutoConstructor]
            private readonly partial struct ApplyInverseGammaExposureProcessor : IRowOperation
            {
                private readonly int width;

                private readonly Memory<Vector4> source;
                private readonly Memory<ImageSharpRgba32> target;
                private readonly Configuration configuration;

                /// <inheritdoc/>
                public void Invoke(int y)
                {
                    int offset = y * width;

                    Span<Vector4> rowVector4 = source.Span.Slice(offset, width);
                    Span<ImageSharpRgba32> rowTPixel = target.Span.Slice(offset, width);

                    Vector4 low = Vector4.Zero;
                    var high = new Vector4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

                    foreach (ref Vector4 v in rowVector4)
                    {
                        var clamp = Vector4.Clamp(v, low, high);

                        clamp.X = MathF.Pow(clamp.X, 1 / 3f);
                        clamp.Y = MathF.Pow(clamp.Y, 1 / 3f);
                        clamp.Z = MathF.Pow(clamp.Z, 1 / 3f);

                        v = clamp;
                    }

                    PixelOperations<ImageSharpRgba32>.Instance.FromVector4Destructive(configuration, rowVector4, rowTPixel);
                }
            }
        }
    }
}
