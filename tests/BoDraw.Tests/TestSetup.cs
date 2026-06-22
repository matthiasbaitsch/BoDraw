using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Skia;
using Avalonia.Headless;
using Xunit;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]
[assembly: CollectionBehavior(DisableTestParallelization = true)]

public class TestApp : Application { }

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<TestApp>()
            .UseSkia()
            .UseHeadless(new AvaloniaHeadlessPlatformOptions { UseHeadlessDrawing = false });
    }

    [ModuleInitializer]
    public static void InitializeVerify()
    {
        VerifierSettings.UseSsimForPng(0.99);
    }
}

