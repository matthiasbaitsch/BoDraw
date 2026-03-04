using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace bodraw;

public partial class App : Application
{

    private Drawing drawing;

    public App()
    {
        this.drawing = new Drawing();
    }

    public App(Drawing d)
    {
        this.drawing = d;
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow(this.drawing);
        }

        base.OnFrameworkInitializationCompleted();
    }
}