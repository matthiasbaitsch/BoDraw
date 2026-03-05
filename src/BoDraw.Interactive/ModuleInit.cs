using Avalonia;
using Avalonia.Headless;
using System.Runtime.CompilerServices;

namespace bodraw;


class App : Application { public override void Initialize() { } }

public static class ModuleInit
{

    public static AppBuilder? AppBuilder;

    [ModuleInitializer]
    public static void Initialize()
    {
        AppBuilder = AppBuilder
            .Configure<App>()
            .UseSkia()
            .UseHeadless(new AvaloniaHeadlessPlatformOptions { UseHeadlessDrawing = false })
            .SetupWithoutStarting();
    }
}