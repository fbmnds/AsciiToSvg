﻿namespace AsciiToSvg.Tests

module GlyphScanner =

  open System

  open AsciiToSvg
  open AsciiToSvg.GlyphScanner
  open AsciiToSvg.Tests.TxtFile

  module ArrowGlyph_txt =

    let scanGridResultExpected =
      [|{ glyphKind = ArrowDown; gridCoord = { col = 12; row = 2 }; glyphOptions = Map.empty }
        { glyphKind = ArrowDown; gridCoord = { col = 16; row = 2 }; glyphOptions = Map.empty }
        { glyphKind = ArrowLeftToRight; gridCoord = { col = 28; row = 1 }; glyphOptions = Map.empty }
        { glyphKind = ArrowLeftToRight; gridCoord = { col = 28; row = 2 }; glyphOptions = Map.empty }
        { glyphKind = ArrowRightToLeft; gridCoord = { col = 45; row = 1 }; glyphOptions = Map.empty }
        { glyphKind = ArrowRightToLeft; gridCoord = { col = 45; row = 2 }; glyphOptions = Map.empty }
        { glyphKind = ArrowUp; gridCoord = { col = 1; row = 1 }; glyphOptions = Map.empty }
        { glyphKind = ArrowUp; gridCoord = { col = 5; row = 1 }; glyphOptions = Map.empty }|]
    let scanGridResult = ArrowGlyph_txt.makeGridResult |> ScanGlyphs
    let scanGridResultMapped =
      scanGridResult
      |> Array.map (fun x -> Array.IndexOf(scanGridResultExpected, x))
      |> Array.sort

  module ArrowGlyphWithFrame_txt =

    let scanGridResultExpected =
      [|{ glyphKind = ArrowDown; gridCoord = {col = 15; row = 4 }; glyphOptions = Map.empty }
        { glyphKind = ArrowDown; gridCoord = {col = 19; row = 4 }; glyphOptions = Map.empty }
        { glyphKind = ArrowLeftToRight; gridCoord = {col = 32; row = 3 }; glyphOptions = Map.empty }
        { glyphKind = ArrowLeftToRight; gridCoord = {col = 32; row = 4 }; glyphOptions = Map.empty }
        { glyphKind = ArrowRightToLeft; gridCoord = {col = 50; row = 3 }; glyphOptions = Map.empty }
        { glyphKind = ArrowRightToLeft; gridCoord = {col = 50; row = 4 }; glyphOptions = Map.empty }
        { glyphKind = ArrowUp; gridCoord = {col = 3; row = 3 }; glyphOptions = Map.empty }
        { glyphKind = ArrowUp; gridCoord = {col = 7; row = 3 }; glyphOptions = Map.empty }
        { glyphKind = CrossCorner; gridCoord = {col = 10; row = 2 }; glyphOptions = Map.empty }
        { glyphKind = CrossCorner; gridCoord = {col = 22; row = 2 }; glyphOptions = Map.empty }
        { glyphKind = CrossCorner; gridCoord = {col = 41; row = 2 }; glyphOptions = Map.empty }
        { glyphKind = LowerLeftAndRightCorner; gridCoord = {col = 10; row = 5 }; glyphOptions = Map.empty }
        { glyphKind = LowerLeftAndRightCorner; gridCoord = {col = 22; row = 5 }; glyphOptions = Map.empty }
        { glyphKind = LowerLeftAndRightCorner; gridCoord = {col = 41; row = 5 }; glyphOptions = Map.empty }
        { glyphKind = LowerLeftCorner; gridCoord = {col = 0; row = 5 }; glyphOptions = Map.empty }
        { glyphKind = LowerRightCorner; gridCoord = {col = 60; row = 5 }; glyphOptions =Map.empty }
        { glyphKind = UpperAndLowerLeftCorner; gridCoord = {col = 0; row = 2 }; glyphOptions = Map.empty }
        { glyphKind = UpperAndLowerRightCorner; gridCoord = {col = 60; row = 2 }; glyphOptions = Map.empty }
        { glyphKind = UpperLeftAndRightCorner; gridCoord = {col = 10;  row = 0 }; glyphOptions = Map.empty }
        { glyphKind = UpperLeftAndRightCorner; gridCoord = {col = 22; row = 0 }; glyphOptions = Map.empty }
        { glyphKind = UpperLeftAndRightCorner; gridCoord = {col = 41; row = 0 }; glyphOptions = Map.empty }
        { glyphKind = UpperLeftCorner; gridCoord = {col = 0; row = 0 }; glyphOptions = Map.empty }
        { glyphKind = UpperRightCorner; gridCoord = {col = 60; row = 0 }; glyphOptions = Map.empty }|]
    let scanGridResult = ArrowGlyphWithFrame_txt.makeGridResult |> ScanGlyphs
    let scanGridResultMapped =
      scanGridResult
      |> Array.map (fun x -> Array.IndexOf(scanGridResultExpected, x))
      |> Array.sort
