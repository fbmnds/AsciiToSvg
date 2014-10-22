namespace AsciiToSvg.Tests

module LineScanner =

  open AsciiToSvg
  open AsciiToSvg.LineScanner
  open AsciiToSvg.Tests.TxtFile


  module ArrowGlyph_txt =

    let horizLines = ArrowGlyph_txt.makeGridResult |> ScanLineHorizontally
    let vertLines = ArrowGlyph_txt.makeGridResult |> ScanLineVertically
    let allLines = ArrowGlyph_txt.makeGridResult |> ScanLine

    let allLinesExpected =
      ([|{ orientation = Horizontal
           gridCorrdStart = { col = 27; row = 1 }
           gridCorrdEnd = { col = 27; row = 1 }
           linechars = [|'-'|]
           lineOptions = Map.empty };
         { orientation = Horizontal
           gridCorrdStart = { col = 46; row = 1 }
           gridCorrdEnd = { col = 46; row = 1 }
           linechars = [|'-'|]
           lineOptions = Map.empty };
         { orientation = Horizontal
           gridCorrdStart = { col = 27; row = 2 }
           gridCorrdEnd = { col = 27; row = 2}
           linechars = [|'+'|]
           lineOptions = Map.empty };
         { orientation = Horizontal
           gridCorrdStart = { col = 46; row = 2 }
           gridCorrdEnd = { col = 46; row = 2 }
           linechars = [|'+'|]
           lineOptions = Map.empty }|],
       [|{ orientation = Vertical
           gridCorrdStart = { col = 1; row = 2 }
           gridCorrdEnd = { col = 1; row = 2 }
           linechars = [|'|'|]
           lineOptions = Map.empty };
         { orientation = Vertical
           gridCorrdStart = {col = 5; row = 2 }
           gridCorrdEnd = { col = 5; row = 2 }
           linechars = [|'+'|]
           lineOptions = Map.empty };
         { orientation = Vertical
           gridCorrdStart = { col = 12; row = 1 }
           gridCorrdEnd = { col = 12; row = 1 }
           linechars = [|'|'|]
           lineOptions = Map.empty };
         { orientation = Vertical
           gridCorrdStart = {col = 16; row = 1 }
           gridCorrdEnd = { col = 16; row = 1 }
           linechars = [|'+'|]
           lineOptions = Map.empty }|])

    let lineScanResult = (horizLines, vertLines) = allLinesExpected && allLinesExpected = allLines
