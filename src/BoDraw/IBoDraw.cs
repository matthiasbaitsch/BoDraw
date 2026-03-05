using Avalonia.Media;

namespace bodraw;

public interface IBoDraw
{
    Color Background { get; set; }

    void Add(params Shape[] shapes);

    void Clear();
}