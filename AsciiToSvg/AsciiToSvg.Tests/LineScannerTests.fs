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
           gridCorrdStart = { col = 2; row = 1 }
           gridCorrdEnd = { col = 2; row = 1 }
           linechars = [|'|'|]
           lineOptions = Map.empty };
         { orientation = Vertical
           gridCorrdStart = {col = 2; row = 5 }
           gridCorrdEnd = { col = 2; row = 5 }
           linechars = [|'+'|]
           lineOptions = Map.empty };
         { orientation = Vertical
           gridCorrdStart = { col = 1; row = 12 }
           gridCorrdEnd = { col = 1; row = 12 }
           linechars = [|'|'|]
           lineOptions = Map.empty };
         { orientation = Vertical
           gridCorrdStart = {col = 1; row = 16 }
           gridCorrdEnd = { col = 1; row = 16 }
           linechars = [|'+'|]
           lineOptions = Map.empty }|])

    let lineScanOK = (horizLines, vertLines) = allLinesExpected && allLinesExpected = allLines
