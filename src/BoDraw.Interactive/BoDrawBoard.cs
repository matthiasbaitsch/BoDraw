using Avalonia;
using Avalonia.Media.Imaging;
using Microsoft.AspNetCore.Html;


namespace BoDraw;

/// <summary>
/// Jupyter/.NET Interactive entry point. Renders the drawing to a PNG and returns it as an
/// <see cref="IHtmlContent"/> inline image when <see cref="Show"/> is called.
/// </summary>
public class BoDrawBoard : BoDrawBase
{

    /// <summary>The pixel dimensions of the rendered output image. Defaults to 600 × 400.</summary>
    public PixelSize Size = new PixelSize(600, 400);

    /// <summary>Creates a new board instance with a default canvas.</summary>
    public BoDrawBoard()
    {
        this.Canvas = new BoDrawCanvas();
    }

    /// <summary>Renders the drawing to a PNG and returns it as an inline HTML image.</summary>
    public IHtmlContent Show()
    {
        using var ms = new MemoryStream();
        using var bitmap = new RenderTargetBitmap(this.Size);

        this.Canvas.Arrange(new Rect(0, 0, this.Size.Width, this.Size.Height));
        bitmap.Render(this.Canvas);
        bitmap.Save(ms);

        return new HtmlString($"<img src='data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}' />");
    }

    /// <summary>Not supported in interactive mode.</summary>
    public override void Animate(double duration, Action<double> frame)
    {
        throw new NotImplementedException();
    }
}
