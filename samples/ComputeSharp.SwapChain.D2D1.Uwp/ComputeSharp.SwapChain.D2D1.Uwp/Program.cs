using System;
using System.Numerics;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;

#pragma warning disable

namespace ComputeSharp.SwapChain.D2D1.Uwp;

public class App : IFrameworkViewSource, IFrameworkView
{
    private CoreApplicationView view;
    private CoreWindow window;

    public IFrameworkView CreateView()
    {
        return this;
    }

    public void Initialize(CoreApplicationView applicationView)
    {
        view = applicationView;
    }

    public void SetWindow(CoreWindow window)
    {
        this.window = window;
    }

    public void Load(string entryPoint) { }

    public void Run()
    {
        /* Replace this with your app code */

        Compositor compositor = new Compositor();
        ContainerVisual windowRoot = compositor.CreateContainerVisual();
        windowRoot.RelativeSizeAdjustment = new Vector2(1.0f, 1.0f);
        CompositionTarget target = compositor.CreateTargetForCurrentView();
        target.Root = windowRoot;

        SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
        spriteVisual.Brush = compositor.CreateColorBrush(Colors.Black);
        spriteVisual.RelativeSizeAdjustment = new Vector2(1.0f, 1.0f);
        windowRoot.Children.InsertAtTop(spriteVisual);

        ColorKeyFrameAnimation colorAnimation = compositor.CreateColorKeyFrameAnimation();

        colorAnimation.InsertKeyFrame(0.0f, Colors.Black);
        colorAnimation.InsertKeyFrame(0.5f, Colors.White);
        colorAnimation.InsertKeyFrame(1.0f, Colors.Black);

        colorAnimation.Duration = TimeSpan.FromSeconds(5);
        colorAnimation.InterpolationColorSpace = CompositionColorSpace.Hsl;
        colorAnimation.IterationBehavior = AnimationIterationBehavior.Forever;

        spriteVisual.Brush.StartAnimation("Color", colorAnimation);

        window.Activate();

        CoreDispatcher dispatcher = window.Dispatcher;
        dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessUntilQuit);
    }

    public void Uninitialize()
    {

    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        CoreApplication.Run(new App());
    }
}
