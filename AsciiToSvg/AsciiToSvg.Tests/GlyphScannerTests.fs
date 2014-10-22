namespace AsciiToSvg.Tests

module GlyphScanner =

  open System

  open AsciiToSvg
  open AsciiToSvg.GlyphScanner
  open AsciiToSvg.Tests.TxtFile

  module ArrowGlyph_txt =

    let scanGridResultExpected =
      [|{ glyphKind = ArrowUp; gridCoord = { col = 1; row = 1 }; glyphOptions = Map.empty }
        { glyphKind = ArrowUp; gridCoord = { col = 5; row = 1 }; glyphOptions = Map.empty }
        { glyphKind = ArrowDown; gridCoord = { col = 12; row = 2 }; glyphOptions = Map.empty }
        { glyphKind = ArrowDown; gridCoord = { col = 16; row = 2 }; glyphOptions = Map.empty }
        { glyphKind = ArrowLeftToRight; gridCoord = { col = 28; row = 1 }; glyphOptions = Map.empty }
        { glyphKind = ArrowLeftToRight; gridCoord = { col = 28; row = 2 }; glyphOptions = Map.empty }
        { glyphKind = ArrowRightToLeft; gridCoord = { col = 45; row = 1 }; glyphOptions = Map.empty }
        { glyphKind = ArrowRightToLeft; gridCoord = { col = 45; row = 2 }; glyphOptions = Map.empty }|]
    let scanGridResult = ArrowGlyph_txt.makeGridResult |> ScanGlyphs
    let scanGridResultMapped =
      scanGridResult
      |> Array.map (fun x -> Array.IndexOf(scanGridResultExpected, x))
      |> Array.sort