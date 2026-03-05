using Avalonia;
using System.Runtime.CompilerServices;

namespace bodraw;

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