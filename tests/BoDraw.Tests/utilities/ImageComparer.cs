using System.Runtime.CompilerServices;

namespace BoDraw.Tests;

static class ImageComparer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifierSettings.UseSsimForPng(0.99);
    }
}
