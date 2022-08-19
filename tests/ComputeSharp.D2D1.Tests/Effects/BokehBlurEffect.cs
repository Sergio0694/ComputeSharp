using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ComputeSharp.D2D1;
using ComputeSharp.D2D1.Interop;
using ComputeSharp.D2D1.Tests.Extensions;
using ComputeSharp.D2D1.Tests.Helpers;
using TerraFX.Interop.DirectX;
using TerraFX.Interop.Windows;

namespace ComputeSharp.BokehBlur.Processors;

/// <summary>
/// A bokeh blur effect, using linearly separable convolutions in the complex space for better efficiency.
/// </summary>
public sealed partial class BokehBlurEffect
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
    private readonly (float[] Real, float[] Imaginary)[] Kernels;

    /// <summary>
    /// The scaling factor for kernel values.
    /// </summary>
    private readonly float KernelsScale;

    /// <summary>
    /// The mapping of initialized complex kernels and parameters, to speed up the initialization of new <see cref="BokehBlurEffect"/> instances.
    /// </summary>
    private static readonly ConcurrentDictionary<(int Radius, int ComponentsCount), (Vector4[] Parameters, float Scale, (float[] Real, float[] Imaginary)[] Kernels)> Cache = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="BokehBlurEffect"/> class.
    /// </summary>
    /// <param name="radius">The size of the area to sample.</param>
    /// <param name="componentsCount">The number of components to use to approximate the original 2D bokeh blur convolution kernel.</param>
    public BokehBlurEffect(int radius, int componentsCount)
    {
        Radius = radius;
        KernelSize = Radius * 2 + 1;
        ComponentsCount = componentsCount;

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
            var kernels = CreateComplexKernels();

            NormalizeKernels(kernels, KernelParameters);

            Kernels = ConvertKernelsToStructureOfArrays(kernels);

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
        int r = Radius;
        int n = -r;

        for (int i = 0; i < KernelSize; i++, n++)
        {
            // Incrementally compute the range values
            float value = n * KernelsScale * (1f / r);

            value *= value;

            // Fill in the complex kernel values
            Unsafe.Add(ref baseRef, i) = new Complex64(
                (float)(Math.Exp(-a * value) * Math.Cos(b * value)),
                (float)(Math.Exp(-a * value) * Math.Sin(b * value)));
        }

        return kernel;
    }

    /// <summary>
    /// Normalizes the kernels with respect to A * real + B * imaginary.
    /// </summary>
    /// <param name="kernels">The input kernels to normalize.</param>
    /// <param name="kernelParameters">The kernel parameters to use to normalize the kernels.</param>
    private static void NormalizeKernels(Complex64[][] kernels, Vector4[] kernelParameters)
    {
        // Calculate the complex weighted sum
        float total = 0;
        Span<Complex64[]> kernelsSpan = kernels.AsSpan();
        ref Complex64[] baseKernelsRef = ref MemoryMarshal.GetReference(kernelsSpan);
        ref Vector4 baseParamsRef = ref MemoryMarshal.GetReference(kernelParameters.AsSpan());

        for (int i = 0; i < kernelParameters.Length; i++)
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
        float scalar = 1f / (float)Math.Sqrt(total);

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

    /// <summary>
    /// Converts the kernels into structure of arrays.
    /// </summary>
    /// <param name="kernels">The input kernels to convert.</param>
    private static (float[] Real, float[] Imaginary)[] ConvertKernelsToStructureOfArrays(Complex64[][] kernels)
    {
        var structureOfArrayKernels = new (float[] Real, float[] Imaginary)[kernels.Length];

        for (int i = 0; i < kernels.Length; i++)
        {
            Complex64[] kernel = kernels[i];
            var real = new float[kernel.Length];
            var imaginary = new float[kernel.Length];

            for (int j = 0; j < kernel.Length; j++)
            {
                real[j] = kernel[j].Real;
                imaginary[j] = kernel[j].Imaginary;
            }

            structureOfArrayKernels[i] = (real, imaginary);
        }

        return structureOfArrayKernels;
    }

    /// <summary>
    /// Applies the effect on an image loaded from a given path and writes the result to the output path.
    /// </summary>
    /// <param name="sourcePath">The source path for the image to run the effect on.</param>
    /// <param name="destinationPath">The destination path for the result.</param>
    public unsafe void ApplyEffect(string sourcePath, string destinationPath)
    {
        using ComPtr<ID2D1Factory2> d2D1Factory2 = D2D1Helper.CreateD2D1Factory2();
        using ComPtr<ID2D1Device> d2D1Device = D2D1Helper.CreateD2D1Device(d2D1Factory2.Get());
        using ComPtr<ID2D1DeviceContext> d2D1DeviceContext = D2D1Helper.CreateD2D1DeviceContext(d2D1Device.Get());

        // Register all necessary effects
        D2D1PixelShaderEffect.RegisterForD2D1Factory1(d2D1Factory2.Get(), static () => new VerticalConvolution(), out _);
        D2D1PixelShaderEffect.RegisterForD2D1Factory1(d2D1Factory2.Get(), static () => new HorizontalConvolutionAndAccumulatePartials(), out _);
        D2D1PixelShaderEffect.RegisterForD2D1Factory1<GammaHighlight>(d2D1Factory2.Get(), out _);
        D2D1PixelShaderEffect.RegisterForD2D1Factory1<InverseGammaHighlight>(d2D1Factory2.Get(), out _);

        int numberOfComponents = KernelParameters.Length;

        using ComPtr<ID2D1Effect> gammaExposureEffect = default;
        using ComPtr<ID2D1Effect> inverseGammaExposureEffect = default;
        using ComPtr<ID2D1Effect> compositeEffect = default;
        Span<ComPtr<ID2D1Effect>> verticalConvolutionEffectsForReals = stackalloc ComPtr<ID2D1Effect>[numberOfComponents];
        Span<ComPtr<ID2D1Effect>> verticalConvolutionEffectsForImaginaries = stackalloc ComPtr<ID2D1Effect>[numberOfComponents];
        Span<ComPtr<ID2D1Effect>> horizontalConvolutionEffects = stackalloc ComPtr<ID2D1Effect>[numberOfComponents];

        try
        {
            // Create an instance of each effect        
            D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<GammaHighlight>(d2D1DeviceContext.Get(), (void**)gammaExposureEffect.GetAddressOf());
            D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<InverseGammaHighlight>(d2D1DeviceContext.Get(), (void**)inverseGammaExposureEffect.GetAddressOf());

            for (int i = 0; i < numberOfComponents; i++)
            {
                D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<VerticalConvolution.Shader>(d2D1DeviceContext.Get(), (void**)verticalConvolutionEffectsForReals[i].GetAddressOf());
                D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<VerticalConvolution.Shader>(d2D1DeviceContext.Get(), (void**)verticalConvolutionEffectsForImaginaries[i].GetAddressOf());
                D2D1PixelShaderEffect.CreateFromD2D1DeviceContext<HorizontalConvolutionAndAccumulatePartials.Shader>(d2D1DeviceContext.Get(), (void**)horizontalConvolutionEffects[i].GetAddressOf());
            }

            // If partials need to be summed, also create the composite effect
            if (numberOfComponents > 1)
            {
                d2D1DeviceContext.Get()->CreateEffect(
                    effectId: (Guid*)Unsafe.AsPointer(ref Unsafe.AsRef(in CLSID.CLSID_D2D1Composite)),
                    effect: compositeEffect.GetAddressOf()).Assert();

                D2D1_BUFFER_PRECISION d2D1BufferPrecision = D2D1_BUFFER_PRECISION.D2D1_BUFFER_PRECISION_32BPC_FLOAT;

                // Set the channel precision to 32 bits manually to avoid banding
                compositeEffect.Get()->SetValue((uint)D2D1_PROPERTY.D2D1_PROPERTY_PRECISION, (byte*)&d2D1BufferPrecision, sizeof(D2D1_BUFFER_PRECISION)).Assert();

                D2D1_COMPOSITE_MODE d2D1CompositeMode = D2D1_COMPOSITE_MODE.D2D1_COMPOSITE_MODE_PLUS;

                // Set the mode to plus
                compositeEffect.Get()->SetValue((uint)D2D1_COMPOSITE_PROP.D2D1_COMPOSITE_PROP_MODE, (byte*)&d2D1CompositeMode, sizeof(D2D1_COMPOSITE_MODE)).Assert();
            }

            ReadOnlyMemory<byte> pixels = ImageHelper.LoadBitmapFromFile(sourcePath, out uint width, out uint height);

            // Load the source bitmap and create a destination bitmap
            using ComPtr<ID2D1Bitmap> d2D1BitmapSource = D2D1Helper.CreateD2D1BitmapAndSetAsSource(d2D1DeviceContext.Get(), pixels, width, height, gammaExposureEffect.Get());
            using ComPtr<ID2D1Bitmap> d2D1BitmapTarget = D2D1Helper.CreateD2D1BitmapAndSetAsTarget(d2D1DeviceContext.Get(), width, height);

            (D2D1ResourceTextureManager Reals, D2D1ResourceTextureManager Imaginaries)[] kernels = new (D2D1ResourceTextureManager, D2D1ResourceTextureManager)[numberOfComponents];
            uint kernelSize = (uint)KernelSize;

            for (int i = 0; i < numberOfComponents; i++)
            {
                // Create the resource texture for the real kernel
                D2D1ResourceTextureManager kernelReals = new(
                    extents: new Span<uint>(&kernelSize, 1),
                    bufferPrecision: D2D1BufferPrecision.Float32,
                    channelDepth: D2D1ChannelDepth.One,
                    filter: D2D1Filter.MinMagMipPoint,
                    extendModes: stackalloc[] { D2D1ExtendMode.Clamp },
                    data: MemoryMarshal.AsBytes(Kernels[i].Real.AsSpan()),
                    strides: ReadOnlySpan<uint>.Empty);

                // Also create the one for the imaginary kernel
                D2D1ResourceTextureManager kernelImaginary = new(
                    extents: new Span<uint>(&kernelSize, 1),
                    bufferPrecision: D2D1BufferPrecision.Float32,
                    channelDepth: D2D1ChannelDepth.One,
                    filter: D2D1Filter.MinMagMipPoint,
                    extendModes: stackalloc[] { D2D1ExtendMode.Clamp },
                    data: MemoryMarshal.AsBytes(Kernels[i].Imaginary.AsSpan()),
                    strides: ReadOnlySpan<uint>.Empty);

                kernels[i] = (kernelReals, kernelImaginary);
            }

            // Assign the resource textures
            for (int i = 0; i < numberOfComponents; i++)
            {
                D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(verticalConvolutionEffectsForReals[i].Get(), kernels[i].Reals, 1);
                D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(verticalConvolutionEffectsForImaginaries[i].Get(), kernels[i].Imaginaries, 1);
                D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(horizontalConvolutionEffects[i].Get(), kernels[i].Reals, 2);
                D2D1PixelShaderEffect.SetResourceTextureManagerForD2D1Effect(horizontalConvolutionEffects[i].Get(), kernels[i].Imaginaries, 3);
            }

            // Set the constant buffers
            for (int i = 0; i < numberOfComponents; i++)
            {
                D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(new VerticalConvolution.Shader((int)kernelSize), verticalConvolutionEffectsForReals[i].Get());
                D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(new VerticalConvolution.Shader((int)kernelSize), verticalConvolutionEffectsForImaginaries[i].Get());
                D2D1PixelShaderEffect.SetConstantBufferForD2D1Effect(new HorizontalConvolutionAndAccumulatePartials.Shader((int)kernelSize, KernelParameters[i].Z, KernelParameters[i].W), horizontalConvolutionEffects[i].Get());
            }

            // Build the effect graph:
            //
            // [INPUT] ---> [GAMMA EXPOSURE] ---> [VERTICAL REAL] ------
            //                      \                                   \
            //                       -----------> [VERTICAL IMAGINARY] ----> [HORIZONTAL] ---> [INVERSE GAMMA EXPOSURE] ---> [OUTPUT]
            for (int i = 0; i < numberOfComponents; i++)
            {
                verticalConvolutionEffectsForReals[i].Get()->SetInputEffect(0, gammaExposureEffect.Get());
                verticalConvolutionEffectsForImaginaries[i].Get()->SetInputEffect(0, gammaExposureEffect.Get());
                horizontalConvolutionEffects[i].Get()->SetInputEffect(0, verticalConvolutionEffectsForReals[i].Get());
                horizontalConvolutionEffects[i].Get()->SetInputEffect(1, verticalConvolutionEffectsForImaginaries[i].Get());
            }

            // Connect to the output node
            if (numberOfComponents == 1)
            {
                inverseGammaExposureEffect.Get()->SetInputEffect(0, horizontalConvolutionEffects[0].Get());
            }
            else
            {
                compositeEffect.Get()->SetInputCount((uint)numberOfComponents);

                // Aggregate all partial results with the composite effect
                for (int i = 0; i < numberOfComponents; i++)
                {
                    compositeEffect.Get()->SetInputEffect((uint)i, horizontalConvolutionEffects[i].Get());
                }

                inverseGammaExposureEffect.Get()->SetInputEffect(0, compositeEffect.Get());
            }

            d2D1DeviceContext.Get()->BeginDraw();

            // Draw the final result
            d2D1DeviceContext.Get()->DrawImage(
                effect: inverseGammaExposureEffect.Get(),
                targetOffset: null,
                imageRectangle: null,
                interpolationMode: D2D1_INTERPOLATION_MODE.D2D1_INTERPOLATION_MODE_NEAREST_NEIGHBOR,
                compositeMode: D2D1_COMPOSITE_MODE.D2D1_COMPOSITE_MODE_SOURCE_COPY);

            d2D1DeviceContext.Get()->EndDraw().Assert();

            using ComPtr<ID2D1Bitmap1> d2D1Bitmap1Buffer = D2D1Helper.CreateD2D1Bitmap1Buffer(d2D1DeviceContext.Get(), d2D1BitmapTarget.Get(), out D2D1_MAPPED_RECT d2D1MappedRect);

            _ = Directory.CreateDirectory(Path.GetDirectoryName(destinationPath)!);

            ImageHelper.SaveBitmapToFile(destinationPath, width, height, d2D1MappedRect.pitch, d2D1MappedRect.bits);
        }
        finally
        {
            for (int i = 0; i < numberOfComponents; i++)
            {
                verticalConvolutionEffectsForReals[i].Dispose();
                verticalConvolutionEffectsForImaginaries[i].Dispose();
                horizontalConvolutionEffects[i].Dispose();
            }
        }
    }

    /// <summary>
    /// The vertical convolution shader and transform.
    /// </summary>
    private sealed partial class VerticalConvolution : ID2D1TransformMapper<VerticalConvolution.Shader>
    {
        /// <summary>
        /// The length of the convolution kernel.
        /// </summary>
        private int kernelLength;

        /// <inheritdoc/>
        public void MapInputsToOutput(in Shader shader, ReadOnlySpan<Rectangle> inputs, ReadOnlySpan<Rectangle> opaqueInputs, out Rectangle output, out Rectangle opaqueOutput)
        {
            this.kernelLength = shader.kernelLength;

            output = inputs[0];
            opaqueOutput = Rectangle.Empty;
        }

        /// <inheritdoc/>
        public void MapInvalidOutput(int inputIndex, Rectangle invalidInput, out Rectangle invalidOutput)
        {
            invalidOutput = invalidInput;
        }

        /// <inheritdoc/>
        public void MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs)
        {
            Rectangle input = Rectangle.Inflate(output, 0, this.kernelLength);

            inputs.Fill(input);
        }

        /// <summary>
        /// Kernel for the vertical convolution pass for real or imaginary components.
        /// </summary>
        [D2DInputCount(1)]
        [D2DInputComplex(0)]
        [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
        [D2DOutputBuffer(D2D1BufferPrecision.Float32, D2D1ChannelDepth.Four)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
        [D2DRequiresScenePosition]
        [AutoConstructor]
        public partial struct Shader : ID2D1PixelShader
        {
            public readonly int kernelLength;

            [D2DResourceTextureIndex(1)]
            private readonly D2D1ResourceTexture1D<float> kernel;

            /// <inheritdoc/>
            public float4 Execute()
            {
                float4 result = float4.Zero;
                int length = this.kernelLength;
                int radiusY = length >> 1;

                for (int i = 0; i < length; i++)
                {
                    float4 color = D2D.SampleInputAtOffset(0, new float2(0, i - radiusY));
                    float factor = this.kernel[i];

                    result += factor * color;
                }

                return result;
            }
        }
    }

    /// <summary>
    /// The horizontal convolutin and partial accumulation effect and transform.
    /// </summary>
    private sealed partial class HorizontalConvolutionAndAccumulatePartials : ID2D1TransformMapper<HorizontalConvolutionAndAccumulatePartials.Shader>
    {
        /// <summary>
        /// The length of the convolution kernel.
        /// </summary>
        private int kernelLength;

        /// <inheritdoc/>
        public void MapInputsToOutput(in Shader shader, ReadOnlySpan<Rectangle> inputs, ReadOnlySpan<Rectangle> opaqueInputs, out Rectangle output, out Rectangle opaqueOutput)
        {
            this.kernelLength = shader.kernelLength;

            output = inputs[0];
            opaqueOutput = Rectangle.Empty;
        }

        /// <inheritdoc/>
        public void MapInvalidOutput(int inputIndex, Rectangle invalidInput, out Rectangle invalidOutput)
        {
            invalidOutput = invalidInput;
        }

        /// <inheritdoc/>
        public void MapOutputToInputs(in Rectangle output, Span<Rectangle> inputs)
        {
            Rectangle input = Rectangle.Inflate(output, this.kernelLength, 0);

            inputs.Fill(input);
        }

        /// <summary>
        /// Kernel for the horizontal convolution pass.
        /// </summary>
        [D2DInputCount(2)]
        [D2DInputComplex(0)]
        [D2DInputComplex(1)]
        [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
        [D2DInputDescription(1, D2D1Filter.MinMagMipPoint)]
        [D2DOutputBuffer(D2D1BufferPrecision.Float32, D2D1ChannelDepth.Four)]
        [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
        [D2DRequiresScenePosition]
        [AutoConstructor]
        public partial struct Shader : ID2D1PixelShader
        {
            public readonly int kernelLength;
            private readonly float z;
            private readonly float w;

            [D2DResourceTextureIndex(2)]
            private readonly D2D1ResourceTexture1D<float> kernelReals;

            [D2DResourceTextureIndex(3)]
            private readonly D2D1ResourceTexture1D<float> kernelImaginaries;

            /// <inheritdoc/>
            public float4 Execute()
            {
                float4 real = float4.Zero;
                float4 imaginary = float4.Zero;
                int length = this.kernelLength;
                int radiusX = length >> 1;

                for (int i = 0; i < length; i++)
                {
                    float4 sourceReal = D2D.SampleInputAtOffset(0, new float2(i - radiusX, 0));
                    float4 sourceImaginary = D2D.SampleInputAtOffset(1, new float2(i - radiusX, 0));
                    float realFactor = kernelReals[i];
                    float imaginaryFactor = kernelImaginaries[i];

                    real += realFactor * sourceReal - imaginaryFactor * sourceImaginary;
                    imaginary += realFactor * sourceImaginary + imaginaryFactor * sourceReal;
                }

                return real * z + imaginary * w;
            }
        }
    }

    /// <summary>
    /// Kernel for the gamma highlight pass.
    /// </summary>
    [D2DInputCount(1)]
    [D2DInputSimple(0)]
    [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
    [D2DOutputBuffer(D2D1BufferPrecision.Float32, D2D1ChannelDepth.Four)]
    [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
    [AutoConstructor]
    internal readonly partial struct GammaHighlight : ID2D1PixelShader
    {
        /// <inheritdoc/>
        public float4 Execute()
        {
            float4 pixel = D2D.GetInput(0);

            pixel.XYZ = pixel.XYZ * pixel.XYZ * pixel.XYZ;

            return pixel;
        }
    }

    /// <summary>
    /// Kernel for the inverse gamma highlight pass.
    /// </summary>
    [D2DInputCount(1)]
    [D2DInputSimple(0)]
    [D2DInputDescription(0, D2D1Filter.MinMagMipPoint)]
    [D2DOutputBuffer(D2D1BufferPrecision.Float32, D2D1ChannelDepth.Four)]
    [D2DShaderProfile(D2D1ShaderProfile.PixelShader50)]
    [AutoConstructor]
    internal readonly partial struct InverseGammaHighlight : ID2D1PixelShader
    {
        /// <inheritdoc/>
        public float4 Execute()
        {
            float4 pixel = D2D.GetInput(0);

            pixel = Hlsl.Clamp(pixel, 0, float.MaxValue);
            pixel.XYZ = Hlsl.Pow(pixel.XYZ, 1 / 3f);

            return pixel;
        }
    }

    /// <summary>
    /// Represents a complex number, where the real and imaginary parts are stored as <see cref="float"/> values.
    /// </summary>
    private readonly struct Complex64
    {
        /// <summary>
        /// The real part of the complex number.
        /// </summary>
        public readonly float Real;

        /// <summary>
        /// The imaginary part of the complex number.
        /// </summary>
        public readonly float Imaginary;

        /// <summary>
        /// Initializes a new instance of the <see cref="Complex64"/> struct.
        /// </summary>
        /// <param name="real">The real part in the complex number.</param>
        /// <param name="imaginary">The imaginary part in the complex number.</param>
        public Complex64(float real, float imaginary)
        {
            this.Real = real;
            this.Imaginary = imaginary;
        }

        /// <summary>
        /// Performs the multiplication operation between a <see cref="Complex64"/> instance and a <see cref="float"/> scalar.
        /// </summary>
        /// <param name="value">The <see cref="Complex64"/> value to multiply.</param>
        /// <param name="scalar">The <see cref="float"/> scalar to use to multiply the <see cref="Complex64"/> value.</param>
        /// <returns>The <see cref="Complex64"/> result.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex64 operator *(Complex64 value, float scalar) => new(value.Real * scalar, value.Imaginary * scalar);
    }
}
