using Microsoft.UI.Xaml;
using Windows.ApplicationModel;

namespace ComputeSharp.SwapChain.WinUI;

/// <summary>
/// A window shell for the MainView.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ExtendsContentIntoTitleBar = true;

        Title = Package.Current.DisplayName;
        SetTitleBar(this.TitleBarRectangle);
    }

    /// <summary>
    /// Stops all rendering when the application is closing.
    /// </summary>
    public void OnShutdown()
    {
        this.MainView.OnShutdown();
    }
}