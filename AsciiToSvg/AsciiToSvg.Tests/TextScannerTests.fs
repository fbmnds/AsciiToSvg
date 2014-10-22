namespace AsciiToSvg.Tests

module TextScanner =

  open AsciiToSvg
  open AsciiToSvg.TextScanner
  open AsciiToSvg.Tests.TxtFile


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

