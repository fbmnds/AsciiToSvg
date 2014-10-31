namespace AsciiToSvg.Tests

module LineScanner =

  open AsciiToSvg
  open AsciiToSvg.Tests.TxtFile

  module ArrowGlyph_txt =

    let horizLines = ArrowGlyph_txt.makeGridResult |> LineScanner.ScanLineHorizontally
    let vertLines = ArrowGlyph_txt.makeGridResult |> LineScanner.ScanLineVertically
    let allLines = ArrowGlyph_txt.makeGridResult |> LineScanner.ScanLine
    let allLinesExpected =
      ([|{ orientation = Horizontal; gridCoordStart = { col = 27; row = 1 }; gridCoordEnd = { col = 27; row = 1 }; linechars = [|'-'|]; lineOptions = Map.empty }; { orientation = Horizontal; gridCoordStart = { col = 46; row = 1 }; gridCoordEnd = { col = 46; row = 1 }; linechars = [|'-'|]; lineOptions = Map.empty }; { orientation = Horizontal; gridCoordStart = { col = 27; row = 2 }; gridCoordEnd = { col = 27; row = 2}; linechars = [|'+'|]; lineOptions = Map.empty }; { orientation = Horizontal; gridCoordStart = { col = 46; row = 2 }; gridCoordEnd = { col = 46; row = 2 }; linechars = [|'+'|]; lineOptions = Map.empty }|],
       [|{ orientation = Vertical; gridCoordStart = { col = 1; row = 2 }; gridCoordEnd = { col = 1; row = 2 }; linechars = [|'|'|]; lineOptions = Map.empty }; { orientation = Vertical; gridCoordStart = {col = 5; row = 2 }; gridCoordEnd = { col = 5; row = 2 }; linechars = [|'+'|]; lineOptions = Map.empty }; { orientation = Vertical; gridCoordStart = { col = 12; row = 1 }; gridCoordEnd = { col = 12; row = 1 }; linechars = [|'|'|]; lineOptions = Map.empty }; { orientation = Vertical; gridCoordStart = {col = 16; row = 1 }; gridCoordEnd = { col = 16; row = 1 }; linechars = [|'+'|]; lineOptions = Map.empty }|])
    let lineScanResult = (horizLines, vertLines) = allLinesExpected && allLinesExpected = allLines

  module ArrowGlyphWithFrame_txt =

    let horizLines = ArrowGlyphWithFrame_txt.makeGridResult |> LineScanner.ScanLineHorizontally
    let vertLines = ArrowGlyphWithFrame_txt.makeGridResult |> LineScanner.ScanLineVertically
    let allLines = ArrowGlyphWithFrame_txt.makeGridResult |> LineScanner.ScanLine
    let allLinesExpected =
      ([| { orientation = Horizontal; gridCoordStart = { col = 1; row = 0 }; gridCoordEnd = { col = 9; row = 0 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 11; row = 0 }; gridCoordEnd = { col = 21; row = 0 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 23; row = 0 }; gridCoordEnd = { col = 40; row = 0 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 42; row = 0 }; gridCoordEnd = { col = 59; row = 0 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 1; row = 2 }; gridCoordEnd = { col = 9; row = 2 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 11; row = 2 }; gridCoordEnd = { col = 21; row = 2 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 23; row = 2 }; gridCoordEnd = { col = 40; row = 2 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 42; row = 2 }; gridCoordEnd = { col = 59; row = 2 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 31; row = 3 }; gridCoordEnd = { col = 31; row = 3 }; linechars = [| '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 51; row = 3 }; gridCoordEnd = { col = 51; row = 3 }; linechars = [| '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 31; row = 4 }; gridCoordEnd = { col = 31; row = 4 }; linechars = [| '+' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 51; row = 4 }; gridCoordEnd = { col = 51; row = 4 }; linechars = [| '+' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 1; row = 5 }; gridCoordEnd = { col = 9; row = 5 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 11; row = 5 }; gridCoordEnd = { col = 21; row = 5 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 23; row = 5 }; gridCoordEnd = { col = 40; row = 5 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 42; row = 5 }; gridCoordEnd = { col = 59; row = 5 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty } |],
       [| { orientation = Vertical; gridCoordStart = { col = 0; row = 1 }; gridCoordEnd = { col = 0; row = 1 }; linechars = [| '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 0; row = 3 }; gridCoordEnd = { col = 0; row = 4 }; linechars = [| '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 3; row = 4 }; gridCoordEnd = { col = 3; row = 4 }; linechars = [| '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 7; row = 4 }; gridCoordEnd = { col = 7; row = 4 }; linechars = [| '+' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 10; row = 1 }; gridCoordEnd = { col = 10; row = 1 }; linechars = [| '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 10; row = 3 }; gridCoordEnd = { col = 10; row = 4 }; linechars = [| '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 15; row = 3 }; gridCoordEnd = { col = 15; row = 3 }; linechars = [| '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 19; row = 3 }; gridCoordEnd = { col = 19; row = 3 }; linechars = [| '+' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 22; row = 1 }; gridCoordEnd = { col = 22; row = 1 }; linechars = [| '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 22; row = 3 }; gridCoordEnd = { col = 22; row = 4 }; linechars = [| '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 41; row = 1 }; gridCoordEnd = { col = 41; row = 1 }; linechars = [| '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 41; row = 3 }; gridCoordEnd = { col = 41; row = 4 }; linechars = [| '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 60; row = 1 }; gridCoordEnd = { col = 60; row = 1 }; linechars = [| '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 60; row = 3 }; gridCoordEnd = { col = 60; row = 4 }; linechars = [| '|'; '|' |]; lineOptions = Map.empty } |])
    let lineScanResult = (horizLines, vertLines) = allLinesExpected && allLinesExpected = allLines
  module TestPolygonBox_txt =
    let horizLines = TestPolygonBox_txt.makeGridResult |> LineScanner.ScanLineHorizontally
    let vertLines = TestPolygonBox_txt.makeGridResult |> LineScanner.ScanLineVertically
    let allLines = TestPolygonBox_txt.makeGridResult |> LineScanner.ScanLine
    let allLinesExpected =
      ([| { orientation = Horizontal; gridCoordStart = { col = 12; row = 2 }; gridCoordEnd = { col = 12; row = 2 }; linechars = [| '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 14; row = 2 }; gridCoordEnd = { col = 19; row = 2 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 1; row = 5 }; gridCoordEnd = { col = 12; row = 5 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 14; row = 5 }; gridCoordEnd = { col = 16; row = 5 }; linechars = [| '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 19; row = 5 }; gridCoordEnd = { col = 19; row = 5 }; linechars = [| '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 1; row = 10 }; gridCoordEnd = { col = 16; row = 10 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty } |],
       [| { orientation = Vertical; gridCoordStart = { col = 0; row = 4 }; gridCoordEnd = { col = 0; row = 4 }; linechars = [| '+' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 0; row = 6 }; gridCoordEnd = { col = 0; row = 9 }; linechars = [| '|'; '|'; '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 13; row = 0 }; gridCoordEnd = { col = 13; row = 0 }; linechars = [| '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 13; row = 3 }; gridCoordEnd = { col = 13; row = 4 }; linechars = [| '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 17; row = 6 }; gridCoordEnd = { col = 17; row = 6 }; linechars = [| '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 17; row = 8 }; gridCoordEnd = { col = 17; row = 9 }; linechars = [| '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 18; row = 6 }; gridCoordEnd = { col = 18; row = 6 }; linechars = [| '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 18; row = 8 }; gridCoordEnd = { col = 18; row = 9 }; linechars = [| '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 20; row = 3 }; gridCoordEnd = { col = 20; row = 4 }; linechars = [| '|'; '|' |]; lineOptions = Map.empty } |])
    let lineScanResult = (horizLines, vertLines) = allLinesExpected && allLinesExpected = allLines

  module TestMiniBox_txt =

    let horizLines = TestMiniBox_txt.makeGridResult |> LineScanner.ScanLineHorizontally
    let vertLines = TestMiniBox_txt.makeGridResult |> LineScanner.ScanLineVertically
    let allLines = TestMiniBox_txt.makeGridResult |> LineScanner.ScanLine
    let allLinesExpected =
      ([|{ orientation = Horizontal; gridCoordStart = {col = 1; row = 0;}; gridCoordEnd = {col = 1; row = 0;}; linechars = [|'-'|]; lineOptions = Map.empty }; { orientation = Horizontal; gridCoordStart = {col = 1; row = 2;}; gridCoordEnd = {col = 1; row = 2;}; linechars = [|'-'|]; lineOptions = Map.empty }|],
       [|{ orientation = Vertical; gridCoordStart = {col = 0; row = 1;}; gridCoordEnd = {col = 0; row = 1;}; linechars = [|'|'|]; lineOptions = Map.empty }; { orientation = Vertical; gridCoordStart = {col = 2; row = 1;}; gridCoordEnd = {col = 2; row = 1;}; linechars = [|'|'|]; lineOptions = Map.empty }|])
    let lineScanResult = (horizLines, vertLines) = allLinesExpected && allLinesExpected = allLines

  module ZeroMQ_Fig1_txt =

    let horizLines = ZeroMQ_Fig1_txt.makeGridResult |> LineScanner.ScanLineHorizontally
    let vertLines = ZeroMQ_Fig1_txt.makeGridResult |> LineScanner.ScanLineVertically
    let allLines = ZeroMQ_Fig1_txt.makeGridResult |> LineScanner.ScanLine
    let allLinesExpected =
      ([| { orientation = Horizontal; gridCoordStart = { col = 1; row = 0 }; gridCoordEnd = { col = 12; row = 0 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 23; row = 0 }; gridCoordEnd = { col = 36; row = 0 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 14; row = 2 }; gridCoordEnd = { col = 20; row = 2 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 1; row = 4 }; gridCoordEnd = { col = 12; row = 4 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 23; row = 6 }; gridCoordEnd = { col = 36; row = 6 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 13; row = 9 }; gridCoordEnd = { col = 21; row = 9 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = { col = 8; row = 11 }; gridCoordEnd = { col = 21; row = 11 }; linechars = [| '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-' |]; lineOptions = Map.empty } |],
       [| { orientation = Vertical; gridCoordStart = { col = 0; row = 1 }; gridCoordEnd = { col = 0; row = 3 }; linechars = [| '|'; '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 2; row = 6 }; gridCoordEnd = { col = 2; row = 11 }; linechars = [| '|'; '|'; '|'; '|'; '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 7; row = 6 }; gridCoordEnd = { col = 7; row = 10 }; linechars = [| '|'; '|'; '|'; '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 12; row = 6 }; gridCoordEnd = { col = 12; row = 8 }; linechars = [| '|'; '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 13; row = 1 }; gridCoordEnd = { col = 13; row = 1 }; linechars = [| '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 13; row = 3 }; gridCoordEnd = { col = 13; row = 3 }; linechars = [| '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 22; row = 1 }; gridCoordEnd = { col = 22; row = 5 }; linechars = [| '|'; '|'; '|'; '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 37; row = 1 }; gridCoordEnd = { col = 37; row = 2 }; linechars = [| '|'; '|' |]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = { col = 37; row = 4 }; gridCoordEnd = { col = 37; row = 5 }; linechars = [| '|'; '|' |]; lineOptions = Map.empty } |])
    let lineScanResult = (horizLines, vertLines) = allLinesExpected && allLinesExpected = allLines

  module TestBoxes_txt =

    let horizLines = TestBoxes_txt.makeGridResult |> LineScanner.ScanLineHorizontally
    let vertLines = TestBoxes_txt.makeGridResult |> LineScanner.ScanLineVertically
    let allLines = TestBoxes_txt.makeGridResult |> LineScanner.ScanLine
    let allLinesExpected =
      ([| { orientation = Horizontal; gridCoordStart = {col = 1; row = 0;}; gridCoordEnd = {col = 25; row = 0;}; linechars = [|'-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 3; row = 2;}; gridCoordEnd = {col = 7; row = 2;}; linechars = [|'-'; '-'; '-'; '.'; '-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 11; row = 2;}; gridCoordEnd = {col = 15; row = 2;}; linechars = [|'-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 19; row = 2;}; gridCoordEnd = {col = 23; row = 2;}; linechars = [|'-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 5; row = 3;}; gridCoordEnd = {col = 5; row = 3;}; linechars = [|'-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 11; row = 3;}; gridCoordEnd = {col = 12; row = 3;}; linechars = [|'-'; '-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 22; row = 3;}; gridCoordEnd = {col = 23; row = 3;}; linechars = [|'-'; '-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 5; row = 4;}; gridCoordEnd = {col = 5; row = 4;}; linechars = [|'-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 14; row = 4;}; gridCoordEnd = {col = 15; row = 4;}; linechars = [|'-'; '-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 19; row = 4;}; gridCoordEnd = {col = 20; row = 4;}; linechars = [|'-'; '-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 3; row = 5;}; gridCoordEnd = {col = 7; row = 5;}; linechars = [|'-'; '-'; '-'; '\''; '-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 11; row = 5;}; gridCoordEnd = {col = 15; row = 5;}; linechars = [|'-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 19; row = 5;}; gridCoordEnd = {col = 23; row = 5;}; linechars = [|'-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }
          { orientation = Horizontal; gridCoordStart = {col = 1; row = 8;}; gridCoordEnd = {col = 25; row = 8;}; linechars = [|'-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }|],
       [| { orientation = Vertical; gridCoordStart = {col = 0; row = 1;}; gridCoordEnd = {col = 0; row = 7;}; linechars = [|'|'; '|'; '|'; '|'; '|'; '|'; '|'|]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = {col = 2; row = 3;}; gridCoordEnd = {col = 2; row = 4;}; linechars = [|'|'; '|'|]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = {col = 8; row = 3;}; gridCoordEnd = {col = 8; row = 4;}; linechars = [|'|'; '|'|]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = {col = 10; row = 4;}; gridCoordEnd = {col = 10; row = 4;}; linechars = [|'|'|]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = {col = 16; row = 3;}; gridCoordEnd = {col = 16; row = 3;}; linechars = [|'|'|]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = {col = 18; row = 3;}; gridCoordEnd = {col = 18; row = 3;}; linechars = [|'|'|]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = {col = 24; row = 4;}; gridCoordEnd = {col = 24; row = 4;}; linechars = [|'|'|]; lineOptions = Map.empty }
          { orientation = Vertical; gridCoordStart = {col = 26; row = 1;}; gridCoordEnd = {col = 26; row = 7;}; linechars = [|'|'; '|'; '|'; '|'; '|'; '|'; '|'|]; lineOptions = Map.empty }|])
    let lineScanResult = (horizLines, vertLines) = allLinesExpected && allLinesExpected = allLines
