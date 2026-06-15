using System.Reflection;
using Avalonia.Headless.XUnit;

namespace BoDraw.Tests;

public class DemoTests
{
    private static readonly Assembly DemoAssembly = typeof(ArrowDemo).Assembly;

    // Demos excluded from reference tests:
    // - ClipDemo: uses Random.Shared, produces non-deterministic output
    // - ImageReadDemo: requires assets/hs-bo_logo_en.png at runtime
    // - PolygonDemo: requires assets/hs-bo_logo_en.png at runtime
    private static readonly HashSet<string> Excluded = ["ClipDemo", "ImageReadDemo", "PolygonDemo"];

    public static IEnumerable<object[]> GetDemos()
    {
        return DemoAssembly
            .GetTypes()
            .Where(t => t.GetMethod("Draw", BindingFlags.Public | BindingFlags.Static,
                                    null, [typeof(IBoDraw)], null) != null
                     && !Excluded.Contains(t.Name))
            .OrderBy(t => t.Name)
            .Select(t => new object[] { t.Name });
    }

    [AvaloniaTheory]
    [MemberData(nameof(GetDemos))]
    public Task VerifyDemo(string demoName)
    {
        var type = DemoAssembly.GetTypes().First(t => t.Name == demoName);
        var method = type.GetMethod("Draw", BindingFlags.Public | BindingFlags.Static)!;
        return RenderHelper.VerifyRendering(canvas => method.Invoke(null, [canvas]));
    }
}
