using System;
using System.IO;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using ComputeSharp.BokehBlur.Processors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Convolution;
using ImageSharpRgba32 = SixLabors.ImageSharp.PixelFormats.Rgba32;

namespace ComputeSharp.Benchmark.Imaging
{
    /// <summary>
    /// A <see langword="class"/> that benchmarks the image processors against the reference in ImageSharp.
    /// </summary>
    [SimpleJob(RunStrategy.Monitoring)]
    [CategoriesColumn]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class ImagingBenchmark : IDisposable
    {
        /// <summary>
        /// The loaded image.
        /// </summary>
        private Image<ImageSharpRgba32>? image;

        /// <summary>
        /// Initial setup for a benchmarking session.
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Imaging", "city.jpg");

            this.image = Image.Load<ImageSharpRgba32>(path);

            Bokeh_Cpu();
            Bokeh_Gpu();
            Gaussian_Cpu();
            Gaussian_Gpu();
        }

        /// <inheritdoc/>
        [GlobalCleanup]
        public void Dispose()
        {
            this.image!.Dispose();
        }

        /// <summary>
        /// Runs a bokeh blur effect on the CPU.
        /// </summary>
        [Benchmark(Baseline = true)]
        [BenchmarkCategory("BOKEH")]
        public void Bokeh_Cpu()
        {
            this.image!.Mutate(c => c.ApplyProcessor(new BokehBlurProcessor(80, 2, 3)));
        }

        /// <summary>
        /// Runs a bokeh blur effect on the GPU.
        /// </summary>
        [Benchmark]
        [BenchmarkCategory("BOKEH")]
        public void Bokeh_Gpu()
        {
            this.image!.Mutate(c => c.ApplyProcessor(new HlslBokehBlurProcessor(80, 2)));
        }

        /// <summary>
        /// Runs a gaussian blur effect on the CPU.
        /// </summary>
        [Benchmark(Baseline = true)]
        [BenchmarkCategory("GAUSSIAN")]
        public void Gaussian_Cpu()
        {
            this.image!.Mutate(c => c.ApplyProcessor(new GaussianBlurProcessor(80)));
        }

        /// <summary>
        /// Runs a gaussian blur effect on the GPU.
        /// </summary>
        [Benchmark]
        [BenchmarkCategory("GAUSSIAN")]
        public void Gaussian_Gpu()
        {
            this.image!.Mutate(c => c.ApplyProcessor(new HlslGaussianBlurProcessor(80)));
        }
    }
}
