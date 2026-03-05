using Avalonia;
using Avalonia.Layout;
using Avalonia.Controls;
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
        this.Canvas.HorizontalAlignment = HorizontalAlignment.Stretch;
        this.Canvas.VerticalAlignment = VerticalAlignment.Stretch;
    }

    public IHtmlContent Show()
    {
        var window = new Window { Width = this.Size.Width, Height = this.Size.Height };
        var bitmap = new RenderTargetBitmap(this.Size);
        var ms = new MemoryStream();

        window.Content = this.Canvas;
        window.Show();
        bitmap.Render(this.Canvas);
        bitmap.Save(ms);
        ms.Position = 0;

        return new HtmlString($"<img src='data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}' />");
    }
}