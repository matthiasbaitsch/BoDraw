# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build the core library
dotnet build src/BoDraw/BoDraw.csproj

# Build everything
dotnet build

# Run all tests
dotnet test

# Run a single test by name
dotnet test --filter "TestBoundsEmpty"

# Run the app demo (opens an Avalonia window)
dotnet run --project demo/BoDraw.App.Demo
```

## Architecture

BoDraw is a simple drawing library with three layers:

- **`src/BoDraw`** — Core library (no UI dependency beyond Avalonia base). Contains the `Shape` hierarchy and `Drawing`. Namespace: `bodraw`.
- **`src/BoDraw.App`** — Avalonia desktop wrapper. `BoDrawApp` opens a `MainWindow` and runs the Avalonia event loop. Entry point for scripts via `bd.Show()`.
- **`src/BoDraw.Interactive`** — Jupyter/.NET Interactive wrapper. `BoDrawBoard.Show()` renders to a PNG and returns `IHtmlContent` for notebook display.

### Shape hierarchy

```
Shape (abstract)
├── LineLikeShape (abstract) — has Pen, Color, Thickness
│   ├── Line
│   └── Polyline
├── AreaLikeShape (abstract) — has Brush, Pen, FillColor, LineColor, LineThickness
│   ├── Rectangle
│   └── Ellipse
└── Text — extends Shape directly; has Content, Position, FontSize, Color
```

`Shape.Draw(double scale, DrawingContext ctx)` is `internal` and called by `Drawing`. Subclasses implement either `Draw(ctx, Pen)` or `Draw(ctx, Brush?, Pen?)`. The `scale` parameter is used only to scale pen thickness via `Shape.ScalePen()`.

### Coordinate system

The Y-axis is **flipped** (mathematical convention, Y points up). `Drawing.CreateTransform` applies `Matrix.CreateScale(scale, -scale)`, which mirrors all shapes vertically. Geometric shapes are unaffected, but `Text` must push a local counter-transform to render right-side up:

```csharp
var transform = Matrix.CreateTranslation(Position.X, Position.Y)
    .Append(Matrix.CreateScale(1, -1));
using (ctx.PushTransform(transform)) { ... }
```

### IBoDraw / BoDrawBase

`IBoDraw` defines `Add(params Shape[])`, `Clear()`, and `Background`. `BoDrawBase` implements it by delegating to a `BoDrawCanvas`. Both `BoDrawApp` and `BoDrawBoard` extend `BoDrawBase`.

### Colors

`Colors` in `src/BoDraw/draw/Colors.cs` re-exports Avalonia's named colors under the `bodraw` namespace so students don't need to import `Avalonia.Media`.

### Tests

xUnit tests in `tests/BoDraw.Tests/`. Tests focus on `Bounds` calculation (pure math, no rendering context needed). Each shape class has a corresponding `*Tests.cs` file.
