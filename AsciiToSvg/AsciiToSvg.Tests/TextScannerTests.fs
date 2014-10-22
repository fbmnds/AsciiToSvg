namespace AsciiToSvg.Tests

module TextScanner =

  open AsciiToSvg
  open AsciiToSvg.TextScanner

  let text = Tests.GlyphScanner.makeGridResult |> ScanText
  let textExpected =
    [|{ text = "ArrowUp  ArrowDown  ArrowLeftToRight  ArrowRightToLeft"
        gridCoord = { col = 0; row = 0 }
        glyphOptions = Map.empty }|]


