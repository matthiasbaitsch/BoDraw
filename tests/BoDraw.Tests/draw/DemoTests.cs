using System.Reflection;
using Avalonia.Headless.XUnit;

namespace BoDraw.Tests;

public class DemoTests
{
    private static readonly Assembly DemoAssembly = typeof(ArrowDemo).Assembly;

    public static IEnumerable<object[]> GetDemos()
    {
        return DemoAssembly
            .GetTypes()
            .Where(t => t.GetMethod(
                    "Draw", BindingFlags.Public | BindingFlags.Static, null, [typeof(IBoDraw)], null
                ) != null
            )
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
