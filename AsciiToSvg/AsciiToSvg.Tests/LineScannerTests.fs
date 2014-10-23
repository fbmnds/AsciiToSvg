namespace AsciiToSvg.Tests

module LineScanner =

  open AsciiToSvg
  open AsciiToSvg.Tests.TxtFile


  module ArrowGlyph_txt =

    let horizLines = ArrowGlyph_txt.makeGridResult |> LineScanner.ScanLineHorizontally
    let vertLines = ArrowGlyph_txt.makeGridResult |> LineScanner.ScanLineVertically
    let allLines = ArrowGlyph_txt.makeGridResult |> LineScanner.ScanLine

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

  module ArrowGlyphWithFrame_txt =

    let horizLines = ArrowGlyphWithFrame_txt.makeGridResult |> LineScanner.ScanLineHorizontally
    let vertLines = ArrowGlyphWithFrame_txt.makeGridResult |> LineScanner.ScanLineVertically
    let allLines = ArrowGlyphWithFrame_txt.makeGridResult |> LineScanner.ScanLine

    let allLinesExpected = 
      ([| { orientation = Horizontal
            gridCorrdStart = 
              { col = 0
                row = 0 }
            gridCorrdEnd = 
              { col = 60
                row = 0 }
            linechars = 
              [| '+'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '+'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; 
                 '-'; '+'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '+'; 
                 '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '+' |]
            lineOptions = Map.empty }
          { orientation = Horizontal
            gridCorrdStart = 
              { col = 0
                row = 2 }
            gridCorrdEnd = 
              { col = 60
                row = 2 }
            linechars = 
              [| '+'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '+'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; 
                 '-'; '+'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '+'; 
                 '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '+' |]
            lineOptions = Map.empty }
          { orientation = Horizontal
            gridCorrdStart = 
              { col = 31
                row = 3 }
            gridCorrdEnd = 
              { col = 31
                row = 3 }
            linechars = [| '-' |]
            lineOptions = Map.empty }
          { orientation = Horizontal
            gridCorrdStart = 
              { col = 51
                row = 3 }
            gridCorrdEnd = 
              { col = 51
                row = 3 }
            linechars = [| '-' |]
            lineOptions = Map.empty }
          { orientation = Horizontal
            gridCorrdStart = 
              { col = 31
                row = 4 }
            gridCorrdEnd = 
              { col = 31
                row = 4 }
            linechars = [| '+' |]
            lineOptions = Map.empty }
          { orientation = Horizontal
            gridCorrdStart = 
              { col = 51
                row = 4 }
            gridCorrdEnd = 
              { col = 51
                row = 4 }
            linechars = [| '+' |]
            lineOptions = Map.empty }
          { orientation = Horizontal
            gridCorrdStart = 
              { col = 0
                row = 5 }
            gridCorrdEnd = 
              { col = 60
                row = 5 }
            linechars = 
              [| '+'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '+'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; 
                 '-'; '+'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '+'; 
                 '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '+' |]
            lineOptions = Map.empty } |], 
       [| { orientation = Vertical
            gridCorrdStart = 
              { col = 0
                row = 0 }
            gridCorrdEnd = 
              { col = 0
                row = 5 }
            linechars = [| '+'; '|'; '+'; '|'; '|'; '+' |]
            lineOptions = Map.empty }
          { orientation = Vertical
            gridCorrdStart = 
              { col = 3
                row = 4 }
            gridCorrdEnd = 
              { col = 3
                row = 4 }
            linechars = [| '|' |]
            lineOptions = Map.empty }
          { orientation = Vertical
            gridCorrdStart = 
              { col = 7
                row = 4 }
            gridCorrdEnd = 
              { col = 7
                row = 4 }
            linechars = [| '+' |]
            lineOptions = Map.empty }
          { orientation = Vertical
            gridCorrdStart = 
              { col = 10
                row = 0 }
            gridCorrdEnd = 
              { col = 10
                row = 5 }
            linechars = [| '+'; '|'; '+'; '|'; '|'; '+' |]
            lineOptions = Map.empty }
          { orientation = Vertical
            gridCorrdStart = 
              { col = 15
                row = 3 }
            gridCorrdEnd = 
              { col = 15
                row = 3 }
            linechars = [| '|' |]
            lineOptions = Map.empty }
          { orientation = Vertical
            gridCorrdStart = 
              { col = 19
                row = 3 }
            gridCorrdEnd = 
              { col = 19
                row = 3 }
            linechars = [| '+' |]
            lineOptions = Map.empty }
          { orientation = Vertical
            gridCorrdStart = 
              { col = 22
                row = 0 }
            gridCorrdEnd = 
              { col = 22
                row = 5 }
            linechars = [| '+'; '|'; '+'; '|'; '|'; '+' |]
            lineOptions = Map.empty }
          { orientation = Vertical
            gridCorrdStart = 
              { col = 41
                row = 0 }
            gridCorrdEnd = 
              { col = 41
                row = 5 }
            linechars = [| '+'; '|'; '+'; '|'; '|'; '+' |]
            lineOptions = Map.empty }
          { orientation = Vertical
            gridCorrdStart = 
              { col = 60
                row = 0 }
            gridCorrdEnd = 
              { col = 60
                row = 5 }
            linechars = [| '+'; '|'; '+'; '|'; '|'; '+' |]
            lineOptions = Map.empty } |])

    let lineScanResult = (horizLines, vertLines) = allLinesExpected && allLinesExpected = allLines