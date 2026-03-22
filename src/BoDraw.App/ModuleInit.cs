using Avalonia;
using System.Runtime.CompilerServices;

namespace BoDraw;

public static class ModuleInit
{

    public static AppBuilder? AppBuilder;

    [ModuleInitializer]
    public static void Initialize()
    {
        AppBuilder = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .SetupWithoutStarting();
    }
}