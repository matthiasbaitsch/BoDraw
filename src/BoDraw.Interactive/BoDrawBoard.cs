using Avalonia;
using Avalonia.Media.Imaging;
using Microsoft.AspNetCore.Html;
using System.Diagnostics.CodeAnalysis;


namespace bodraw;

public class BoDrawBoard : BoDrawBase
{

    public PixelSize Size = new PixelSize(600, 400);

    [SetsRequiredMembers]
    public BoDrawBoard()
    {
        this.Canvas = new BoDrawCanvas();
    }

    public IHtmlContent Show()
    {
        using var ms = new MemoryStream();
        using var bitmap = new RenderTargetBitmap(this.Size);

        this.Canvas.Arrange(new Rect(0, 0, this.Size.Width, this.Size.Height));
        bitmap.Render(this.Canvas);
        bitmap.Save(ms);

        return new HtmlString($"<img src='data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}' />");
    }
}
