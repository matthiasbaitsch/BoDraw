using Avalonia.Media;

namespace BoDraw;

/// <summary>
/// Defines the public API for a BoDraw drawing surface.
/// </summary>
public interface IBoDraw
{
    /// <summary>Gets or sets the background color of the drawing surface.</summary>
    Color Background { get; set; }

    /// <summary>
    /// Adds one or more shapes to the drawing. Shapes are painted in the order they are added,
    /// so shapes added first appear behind shapes added later.
    /// </summary>
    /// <example>
    /// Add shapes s1, s2 and s3.
    /// <code>
    /// bd.Add(s1, s2, s3);
    /// </code>
    /// </example>
    /// <param name="shapes">The shapes to add.</param>
    void Add(params Shape[] shapes);

    /// <summary>Removes all shapes from the drawing.</summary>
    void Clear();
}