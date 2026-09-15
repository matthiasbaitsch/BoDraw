# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Run the app demo (opens an Avalonia window)
dotnet run --project demo/BoDraw.App.Demo
```

## Architecture

BoDraw is a simple drawing library with two layers:

- **`src/BoDraw`** — Core library (no UI dependency beyond Avalonia base). Contains the `Shape` hierarchy and `Drawing`. Namespace: `BoDraw`.
- **`src/BoDraw.App`** — Avalonia desktop wrapper. `BoDrawApp` opens a `MainWindow` and runs the Avalonia event loop. Entry point for scripts via `bd.Show()`.

### Shape hierarchy

See `docs/index.md` for the shape hierarchy and coordinate system overview.

### Coordinate system

The Y-axis is flipped (mathematical convention, Y points up). `Drawing` applies a global `scale/-scale` transform, so shapes that render text or images must push a local counter-transform — see `Text.cs` and `Image.cs`.

### IBoDraw

`IBoDraw` defines `Add(params Shape[])`, `Clear()`, and `Background`. 

### Colors

`Colors` in `src/BoDraw/draw/Colors.cs` re-exports Avalonia's named colors under the `BoDraw` namespace so students don't need to import `Avalonia.Media`.

### Tests

xUnit tests in `tests/BoDraw.Tests/`. Tests focus on `Bounds` calculation (pure math, no rendering context needed). Each shape class has a corresponding `*Tests.cs` file.

## Preferences

- Do not use `=>` expression-body syntax for methods or properties. Always use full block bodies.
- Store memories in this CLAUDE.md file, not in the `~/.claude` folder.
- Don't treat casual reactions/observations (e.g. "puh, still a lot of code", "hmm, nasty") as implicit instructions to keep coding. Stop and ask what's wanted, or ask a clarifying question, instead of assuming a remark is a command to act. Only explicit requests ("can we use polymorphism instead", "please simplify X") or confirmations should trigger edits.
- In `Directory.Packages.props` (or similar shared version files), when several `PackageVersion`/`PackageReference` entries share one product's version (e.g. all `Avalonia.*` packages), factor it into an MSBuild property (e.g. `<AvaloniaVersion>12.1.1</AvaloniaVersion>`) and reference it via `$(AvaloniaVersion)` from the start, rather than repeating the literal version string per entry and only extracting it after being asked.
