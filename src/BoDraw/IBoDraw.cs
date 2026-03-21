using Avalonia.Media;

namespace bodraw;

/// <summary>
/// Defines the public API for a BoDraw drawing surface.
/// </summary>
public interface IBoDraw
{
    /// <summary>Gets or sets the background color of the drawing surface.</summary>
    Color Background { get; set; }

    /// <summary>Adds one or more shapes to the drawing.</summary>
    /// <param name="shapes">The shapes to add.</param>
    void Add(params Shape[] shapes);

    /// <summary>Removes all shapes from the drawing.</summary>
    void Clear();
}