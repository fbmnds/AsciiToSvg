namespace AsciiToSvg.Tests

module TextScanner =

  open AsciiToSvg
  open AsciiToSvg.TextScanner
  open AsciiToSvg.Tests.TxtFile

  module ArrowGlyph_txt =

    let text = ArrowGlyph_txt.makeGridResult |> ScanText
    let textExpected =
      [|{ text = "ArrowUp  ArrowDown  ArrowLeftToRight  ArrowRightToLeft"
          gridCoord = { col = 0; row = 0 }
          glyphOptions = Map.empty }|]

    let textTabbed = ArrowGlyph_txt.makeGridResult |> ScanTabbedText

    let textTabbedExpected =
      [|{ text = "ArrowUp"
          gridCoord = { col = 0; row = 0 }
          glyphOptions = Map.empty }
        { text = "ArrowDown"
          gridCoord = { col = 9; row = 0 }
          glyphOptions = Map.empty }
        { text = "ArrowLeftToRight"
          gridCoord = { col = 20; row = 0 }
          glyphOptions = Map.empty }
        { text = "ArrowRightToLeft"
          gridCoord = { col = 38; row = 0 }
          glyphOptions = Map.empty }|]

  module ArrowGlyphWithFrame_txt =

    let textTabbed = ArrowGlyphWithFrame_txt.makeGridResult |> ScanTabbedText

    let textTabbedExpected =
      [|{ text = "ArrowUp"; gridCoord = {col = 2; row = 1 }; glyphOptions = Map.empty }
        { text = "ArrowDown"; gridCoord = {col = 12; row = 1 }; glyphOptions = Map.empty }
        { text = "ArrowLeftToRight"; gridCoord = {col = 24; row = 1 }; glyphOptions = Map.empty }
        { text = "ArrowRightToLeft"; gridCoord = {col = 43; row = 1 }; glyphOptions = Map.empty }|]

  module ZeroMQ_Fig1_txt =

    let textTabbed = ZeroMQ_Fig1_txt.makeGridResult |> ScanTabbedText

    let textTabbedExpected =
      [|{ text = "TCP socket"; gridCoord = {col = 2; row = 2;}; glyphOptions = Map.empty };
        { text = "ZAP!"; gridCoord = {col = 39; row = 2;}; glyphOptions = Map.empty };
        { text = "BOOM!"; gridCoord = {col = 15; row = 3;}; glyphOptions = Map.empty };
        { text = "ZeroMQ socket"; gridCoord = {col = 25; row = 3;}; glyphOptions = Map.empty };
        { text = "|"; gridCoord = {col = 40; row = 3;}; glyphOptions = Map.empty };
        { text = "POW!!"; gridCoord = {col = 40; row = 4;}; glyphOptions = Map.empty };
        { text = "Spandex"; gridCoord = {col = 23; row = 9;}; glyphOptions = Map.empty };
        { text = "Cosmic rays"; gridCoord = {col = 23; row = 11;}; glyphOptions = Map.empty };
        { text = "Illegal radioisotopes from"; gridCoord = {col = 1; row = 13;}; glyphOptions = Map.empty };
        { text = "secret Soviet atomic city"; gridCoord = {col = 1; row = 14;}; glyphOptions = Map.empty };
        { text = "source: https://raw.githubusercontent.com/imatix/zguide/master/images/fig1.txt"; gridCoord = {col = 1; row = 16;}; glyphOptions = Map.empty }|]
