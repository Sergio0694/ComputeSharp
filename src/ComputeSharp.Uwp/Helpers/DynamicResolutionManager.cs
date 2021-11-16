using System;
using System.Threading;

namespace ComputeSharp.Uwp.Helpers;

/// <summary>
/// A helper type that is responsible for applying dynamic resolution changes.
/// </summary>
internal unsafe ref struct DynamicResolutionManager
{
    /// <summary>
    /// The target frame time in ticks to reach 60fps (set to 64fps).
    /// </summary>
    public const long TargetFrameTimeInTicksFor60fps = 156250;

    /// <summary>
    /// The upper frame time threshold in ticks to reach 60fps.
    /// </summary>
    private const long UpperFrameTimeThresholdInTicksFor60fps = 166666;

    /// <summary>
    /// The lower frame time threshold in ticks to reach 60fps (set to 68fps).
    /// </summary>
    private const long LowerFrameTimeThresholdInTicksFor60fps = 147058;

    /// <summary>
    /// The size of the sliding frame time window to monitor frame times.
    /// </summary>
    private const int SlidingFrameTimeWindowLength = 32;

    /// <summary>
    /// The threshold for scale factor updates.
    /// </summary>
    private const float ScaleFactorDeltaThreshold = 0.005f;

    /// <summary>
    /// The constant multiplicative factor for the scale factor.
    /// </summary>
    private const float ScaleFactorDeltaK = 0.8f;

    /// <summary>
    /// The window of registered frame times for previous frames.
    /// </summary>
    private fixed long frameTimesInTicks[SlidingFrameTimeWindowLength];

    /// <summary>
    /// The current index into <see cref="frameTimesInTicks"/>.
    /// </summary>
    /// <remarks>
    /// This will have a value of <c>-1</c> when a scale factor update has just been applied, indicating
    /// that the following frame update should not be tracked to contribute towards the next dynamic resolution
    /// check. This is done to avoid having the swap chain resize cost influence the moving average of frame times.
    /// </remarks>
    private int frameTimeOffset;

    /// <summary>
    /// The current sum of values within <see cref="frameTimesInTicks"/>.
    /// </summary>
    private long slidingFrameTimeWindowSum;

    /// <summary>
    /// Initializes a new <see cref="DynamicResolutionManager"/> instance.
    /// </summary>
    /// <param name="manager">The resulting <see cref="DynamicResolutionManager"/> instance.</param>
    public static void Create(out DynamicResolutionManager manager)
    {
        manager.frameTimeOffset = 0;
        manager.slidingFrameTimeWindowSum = TargetFrameTimeInTicksFor60fps * SlidingFrameTimeWindowLength;

        fixed (long* p = &manager.frameTimesInTicks[0])
        {
            new Span<long>(p, SlidingFrameTimeWindowLength).Fill(TargetFrameTimeInTicksFor60fps);
        }
    }

    /// <summary>
    /// Advances one frame and updates the scale factor, if needed.
    /// </summary>
    /// <param name="frameTimeInTicks">The frame time in ticks for the last rendered frame.</param>
    /// <param name="scaleFactor">The current scale factor value being used to render frames.</param>
    /// <returns>Whether or not <paramref name="scaleFactor"/> has been updated and a resize is needed.</returns>
    public bool Advance(long frameTimeInTicks, ref float scaleFactor)
    {
        // Ignore the current frame if a resize has just happened
        if (this.frameTimeOffset == -1)
        {
            this.frameTimeOffset = 0;

            return false;
        }

        ref long previousFrameTimeInTicks = ref this.frameTimesInTicks[this.frameTimeOffset];

        this.slidingFrameTimeWindowSum -= previousFrameTimeInTicks;
        this.slidingFrameTimeWindowSum += frameTimeInTicks;

        previousFrameTimeInTicks = frameTimeInTicks;

        this.frameTimeOffset = (int)(((uint)this.frameTimeOffset + 1) % SlidingFrameTimeWindowLength);

        // Check for a dynamic resolution update if the end of the sliding window has been reached.
        // This is not done every frame to reduce the overhead and amortize for render time fluctuations.
        if (this.frameTimeOffset == 0)
        {
            // This formula is adapted from https://software.intel.com/content/www/us/en/develop/articles/dynamic-resolution-rendering-article.html.
            long averageFrameTimeInTicks = this.slidingFrameTimeWindowSum / SlidingFrameTimeWindowLength;

            if (averageFrameTimeInTicks is < LowerFrameTimeThresholdInTicksFor60fps or > UpperFrameTimeThresholdInTicksFor60fps)
            {
                float
                    frameTimeDelta = (TargetFrameTimeInTicksFor60fps - averageFrameTimeInTicks) / (float)averageFrameTimeInTicks,
                    scaleFactorDelta = scaleFactor * frameTimeDelta * ScaleFactorDeltaK,
                    updatedScaleFactor = Math.Clamp(scaleFactor + scaleFactorDelta, 0.10f, 1.0f);

                // Apply the scale factor update if the target scale has changed enough. This helps to avoid
                // too frequence resolution changes if the scale factor only changes very little every time.
                if (Math.Abs(scaleFactor - updatedScaleFactor) >= ScaleFactorDeltaThreshold)
                {
                    Volatile.Write(ref scaleFactor, updatedScaleFactor);

                    this.frameTimeOffset = -1;

                    return true;
                }
            }
        }

        return false;
    }
}