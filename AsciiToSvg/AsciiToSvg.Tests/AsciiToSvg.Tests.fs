module AsciiToSvg.Tests


open System

// https://github.com/fsharp/FsCheck/blob/master/Docs/Documentation.md
// https://github.com/fsharp/FsUnit
// https://code.google.com/p/unquote/
open NUnit.Framework

open AsciiToSvg
open AsciiToSvg.TxtFile
open AsciiToSvg.GlyphScanner
//open AsciiToSvg.GlyphRenderer
open AsciiToSvg.SvgDocument



// #region Handling input files

[<Test>]
let ``Handling input files : TestLogo.txt``() =
  let splitTxtResultExpected =
    [|("Logo", "{\"fill\":\"#88d\",\"a2s:delref\":true}")|],
       [|" .-[Logo]------------------.   ";
         " |                         |   ";
         " | .---.-. .-----. .-----. |   ";
         " | | .-. | +-->  | |  <--+ |    ";
         " | | '-' | |  <--+ +-->  | |  ";
         " | '---'-' '-----' '-----' |  ";
         " |  ascii     2      svg   |  ";
         " |                         |  ";
         " '-------------------------'  ";
         "  https://9vx.org/~dho/a2s/   ";
         "   "|]
  let splitTxtResult =
    @"../../TestTxtFiles/TestLogo.txt"
    |> readFile
    |> framedSplitTxt
  splitTxtResult = splitTxtResultExpected
  |>  Assert.True

  let makeGridResultExpected =
    [|[|' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' ';
        ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' ';
        ' '; ' '; ' '; ' '; ' '|];
      [|' '; '.'; '-'; '['; 'L'; 'o'; 'g'; 'o'; ']'; '-'; '-'; '-'; '-'; '-';
        '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '.';
        ' '; ' '; ' '; ' '; ' '|];
      [|' '; '|'; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' ';
        ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; '|';
        ' '; ' '; ' '; ' '; ' '|];
      [|' '; '|'; ' '; '.'; '-'; '-'; '-'; '.'; '-'; '.'; ' '; '.'; '-'; '-';
        '-'; '-'; '-'; '.'; ' '; '.'; '-'; '-'; '-'; '-'; '-'; '.'; ' '; '|';
        ' '; ' '; ' '; ' '; ' '|];
      [|' '; '|'; ' '; '|'; ' '; '.'; '-'; '.'; ' '; '|'; ' '; '+'; '-'; '-';
        '>'; ' '; ' '; '|'; ' '; '|'; ' '; ' '; '<'; '-'; '-'; '+'; ' '; '|';
        ' '; ' '; ' '; ' '; ' '|];
      [|' '; '|'; ' '; '|'; ' '; '\''; '-'; '\''; ' '; '|'; ' '; '|'; ' '; ' ';
        '<'; '-'; '-'; '+'; ' '; '+'; '-'; '-'; '>'; ' '; ' '; '|'; ' '; '|';
        ' '; ' '; ' '; ' '; ' '|];
      [|' '; '|'; ' '; '\''; '-'; '-'; '-'; '\''; '-'; '\''; ' '; '\''; '-'; '-';
        '-'; '-'; '-'; '\''; ' '; '\''; '-'; '-'; '-'; '-'; '-'; '\''; ' '; '|';
        ' '; ' '; ' '; ' '; ' '|];
      [|' '; '|'; ' '; ' '; 'a'; 's'; 'c'; 'i'; 'i'; ' '; ' '; ' '; ' '; ' ';
        '2'; ' '; ' '; ' '; ' '; ' '; ' '; 's'; 'v'; 'g'; ' '; ' '; ' '; '|';
        ' '; ' '; ' '; ' '; ' '|];
      [|' '; '|'; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' ';
        ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; '|';
        ' '; ' '; ' '; ' '; ' '|];
      [|' '; '\''; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-';
        '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '\'';
        ' '; ' '; ' '; ' '; ' '|];
      [|' '; ' '; 'h'; 't'; 't'; 'p'; 's'; ':'; '/'; '/'; '9'; 'v'; 'x'; '.';
        'o'; 'r'; 'g'; '/'; '~'; 'd'; 'h'; 'o'; '/'; 'a'; '2'; 's'; '/'; ' ';
        ' '; ' '; ' '; ' '; ' '|];
      [|' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' ';
        ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' ';
        ' '; ' '; ' '; ' '; ' '|];
      [|' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' ';
        ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' ';
        ' '; ' '; ' '; ' '; ' '|]|]
  let makeGridResult = splitTxtResult |> fun (a, b) -> b |> makeFramedGrid
  Assert.AreEqual (makeGridResultExpected, makeGridResult)

  let matchPositionsExpected = [2; 25]
  let replaceOptionResultExpected =
    ([2; 25], "-----------------------------------------")
  let input = "--[Logo]-----------------[Logo]----------"
            //    01234567890123456789012345
  Assert.AreEqual (matchPositionsExpected, matchPositions "Logo" input)
  Assert.AreEqual (replaceOptionResultExpected, replaceOption '-' "Logo" input)

// #endregion

// #region Scanning and rendering Glyphs (arrows)

[<Test>]
let ``GlyphRenderer : ArrowGlyphs.txt``() =
  let splitTxtResultExpected =
    [||],
    [|"";
      " ArrowUp  ArrowDown  ArrowLeftToRight  ArrowRightToLeft";
      "  ^   ^      |   +          ->                <-";
      "  |   +      v   v          +>                <+";
      " "|]
  let splitTxtResult =
    @"../../TestTxtFiles/ArrowGlyphs.txt"
    |> readFile
    |> splitTxt
  splitTxtResult = splitTxtResultExpected
  |>  Assert.True

  let leftOffsetExpected = 1
  Assert.AreEqual(leftOffsetExpected, (leftOffset (snd splitTxtResult)))

  let trimWithOffsetExpected =
    [|[||];
      [|'A'; 'r'; 'r'; 'o'; 'w'; 'U'; 'p'; ' '; ' '; 'A'; 'r'; 'r'; 'o'; 'w'; 'D';
        'o'; 'w'; 'n'; ' '; ' '; 'A'; 'r'; 'r'; 'o'; 'w'; 'L'; 'e'; 'f'; 't'; 'T';
        'o'; 'R'; 'i'; 'g'; 'h'; 't'; ' '; ' '; 'A'; 'r'; 'r'; 'o'; 'w'; 'R'; 'i';
        'g'; 'h'; 't'; 'T'; 'o'; 'L'; 'e'; 'f'; 't'|];
      [|' '; '^'; ' '; ' '; ' '; '^'; ' '; ' '; ' '; ' '; ' '; ' '; '|'; ' '; ' ';
        ' '; '+'; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; '-'; '>'; ' ';
        ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' ';
        '<'; '-'|];
      [|' '; '|'; ' '; ' '; ' '; '+'; ' '; ' '; ' '; ' '; ' '; ' '; 'v'; ' '; ' ';
        ' '; 'v'; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; '+'; '>'; ' ';
        ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' ';
        '<'; '+'|];
      [||]|]
  Assert.AreEqual(trimWithOffsetExpected, (trimWithOffset 1 (snd splitTxtResult)))

  let makeGridResultExpected =
    [|[|'A'; 'r'; 'r'; 'o'; 'w'; 'U'; 'p'; ' '; ' '; 'A'; 'r'; 'r'; 'o'; 'w'; 'D';
        'o'; 'w'; 'n'; ' '; ' '; 'A'; 'r'; 'r'; 'o'; 'w'; 'L'; 'e'; 'f'; 't'; 'T';
        'o'; 'R'; 'i'; 'g'; 'h'; 't'; ' '; ' '; 'A'; 'r'; 'r'; 'o'; 'w'; 'R'; 'i';
        'g'; 'h'; 't'; 'T'; 'o'; 'L'; 'e'; 'f'; 't'|];
      [|' '; '^'; ' '; ' '; ' '; '^'; ' '; ' '; ' '; ' '; ' '; ' '; '|'; ' '; ' ';
        ' '; '+'; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; '-'; '>'; ' ';
        ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' ';
        '<'; '-'; ' '; ' '; ' '; ' '; ' '; ' '; ' '|];
      [|' '; '|'; ' '; ' '; ' '; '+'; ' '; ' '; ' '; ' '; ' '; ' '; 'v'; ' '; ' ';
        ' '; 'v'; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; '+'; '>'; ' ';
        ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' '; ' ';
        '<'; '+'; ' '; ' '; ' '; ' '; ' '; ' '; ' '|]|]
  let makeGridResult = splitTxtResult |> fun (_, b) -> b |> makeTrimmedGrid
  Assert.AreEqual (makeGridResultExpected, makeGridResult)

  let gridCoord = [|{ col = 0; row = 2 }; { col = 2; row = 0 }|]
  let svgCoord = gridCoord |> Array.map (ConvertCoordGridToSvg Scale)
  let svgCoordExpected = [|{ colpx = 0.0; rowpx = 30.0 }; { colpx = 18.0; rowpx = 0.0 }|]
  Assert.AreEqual(svgCoordExpected, svgCoord)
  let rowdpx = 5.0
  let coldpx = 3.0
  let shiftedCoord = gridCoord |> Array.map (ShiftedCoordGridToSvg Scale coldpx rowdpx)
  let shiftedCoordExpected = [|{ colpx = 3.0; rowpx = 35.0 }; { colpx = 21.0; rowpx = 5.0 }|]
  Assert.AreEqual(shiftedCoordExpected, shiftedCoord)

  let ax = 0.0
  let ay = 7.0
  let bx = 8.0
  let by = 7.0
  let cx = 4.0
  let cy = 0.0
  let dx = 4.0
  let dy = 7.0
  let ex = 4.0
  let ey = 14.0
  let cols = [|ax; bx; cx; dx; ex|] |> Array.map (fun x -> shiftColCoordGridToSvg Scale x { col = 5; row = 1 })
  let rows = [|ay; by; cy; dy; ey|] |> Array.map (fun x -> shiftRowCoordGridToSvg Scale x { col = 5; row = 1 })
  let colsExpected = [|45.0; 53.0; 49.0; 49.0; 49.0|]
  let rowsExpected = [|22.0; 22.0; 15.0; 22.0; 29.0|]
  Assert.AreEqual(colsExpected, cols)
  Assert.AreEqual(rowsExpected, rows)

  let scanGridResultExpected =
    [|{ glyphKind = ArrowUp; gridCoord = { col = 1; row = 1 }; glyphOptions = Map.empty }
      { glyphKind = ArrowUp; gridCoord = { col = 5; row = 1 }; glyphOptions = Map.empty }
      { glyphKind = ArrowDown; gridCoord = { col = 12; row = 2 }; glyphOptions = Map.empty }
      { glyphKind = ArrowDown; gridCoord = { col = 16; row = 2 }; glyphOptions = Map.empty }
      { glyphKind = ArrowLeftToRight; gridCoord = { col = 28; row = 1 }; glyphOptions = Map.empty }
      { glyphKind = ArrowLeftToRight; gridCoord = { col = 28; row = 2 }; glyphOptions = Map.empty }
      { glyphKind = ArrowRightToLeft; gridCoord = { col = 45; row = 1 }; glyphOptions = Map.empty }
      { glyphKind = ArrowRightToLeft; gridCoord = { col = 45; row = 2 }; glyphOptions = Map.empty }|]
  let scanGridResult = makeGridResult |> ScanGlyphs
  let scanGridResultMapped =
    scanGridResult
    |> Array.map (fun x -> Array.IndexOf(scanGridResultExpected, x))
    |> Array.sort
  Assert.AreEqual ([|0; 1; 2; 3; 4; 5; 6; 7|], scanGridResultMapped)

  let renderResult =
    scanGridResult
    |> Array.map (GlyphRenderer.Render Scale Map.empty)
    |> Array.fold (fun r s -> r + s + "\n") ""

  let renderResultExpected =
    "      <polygon fill=\"black\" points=\"9.000,22.000 17.000,22.000 13.000,15.000 9.000,22.000\" />\n" +
    "      <line stroke=\"black\" stroke-width=\"1\" x1=\"13.000\" y1=\"22.000\" x2=\"13.000\" y2=\"29.000\" />\n\n" +

    "      <polygon fill=\"black\" points=\"45.000,22.000 53.000,22.000 49.000,15.000 45.000,22.000\" />\n" +
    "      <line stroke=\"black\" stroke-width=\"1\" x1=\"49.000\" y1=\"22.000\" x2=\"49.000\" y2=\"29.000\" />\n\n" +

    "      <polygon fill=\"black\" points=\"253.000,18.000 253.000,26.000 260.000,22.000 253.000,18.000\" />\n" +
    "      <line stroke=\"black\" stroke-width=\"1\" x1=\"253.000\" y1=\"22.000\" x2=\"252.000\" y2=\"22.000\" />\n\n" +

    "      <polygon fill=\"black\" points=\"412.000,18.000 412.000,26.000 405.000,22.000 412.000,18.000\" />\n" +
    "      <line stroke=\"black\" stroke-width=\"1\" x1=\"412.000\" y1=\"22.000\" x2=\"413.000\" y2=\"22.000\" />\n\n" +

    "      <polygon fill=\"black\" points=\"108.000,37.000 116.000,37.000 112.000,44.000 108.000,37.000\" />\n" +
    "      <line stroke=\"black\" stroke-width=\"1\" x1=\"112.000\" y1=\"37.000\" x2=\"112.000\" y2=\"30.000\" />\n\n" +

    "      <polygon fill=\"black\" points=\"144.000,37.000 152.000,37.000 148.000,44.000 144.000,37.000\" />\n" +
    "      <line stroke=\"black\" stroke-width=\"1\" x1=\"148.000\" y1=\"37.000\" x2=\"148.000\" y2=\"30.000\" />\n\n" +

    "      <polygon fill=\"black\" points=\"253.000,33.000 253.000,41.000 260.000,37.000 253.000,33.000\" />\n" +
    "      <line stroke=\"black\" stroke-width=\"1\" x1=\"253.000\" y1=\"37.000\" x2=\"252.000\" y2=\"37.000\" />\n\n" +

    "      <polygon fill=\"black\" points=\"412.000,33.000 412.000,41.000 405.000,37.000 412.000,33.000\" />\n" +
    "      <line stroke=\"black\" stroke-width=\"1\" x1=\"412.000\" y1=\"37.000\" x2=\"413.000\" y2=\"37.000\" />\n\n"
  Assert.AreEqual (renderResultExpected, renderResult)

  CanvasWidth <- 450.0
  CanvasHeight <- 75.0
  let arrowGlyphsWithoutTextAsSvg =
    SvgTemplateOpen + renderResult + SvgTemplateClose
    |> fun x -> regex(@"\r\n").Replace(x, "\n")
  let arrowGlyphsWithoutTextAsSvgExpected =
    @"../../TestSvgFiles/ArrowGlyphsWithoutText.svg"
    |> readFileAsText
    |> function | Success x -> x | _ -> ""
    |> fun x -> regex(@"\r\n").Replace(x, "\n")
  Assert.AreEqual(arrowGlyphsWithoutTextAsSvgExpected, arrowGlyphsWithoutTextAsSvg)

  let text = makeGridResult |> AsciiToSvg.TextScanner.ScanText
  let textExpected =
    [|{ text = "ArrowUp  ArrowDown  ArrowLeftToRight  ArrowRightToLeft"
        gridCoord = { col = 0; row = 0 }
        glyphOptions = Map.empty }|]
  Assert.AreEqual(textExpected, text)


  // TODO: add Line rendering to this test case
  //
  CanvasWidth <- (float)makeGridResult.[0].Length * GlyphWidth
  CanvasHeight <- (float)makeGridResult.Length * GlyphHeight
  let renderedText = (TextRenderer.RenderAll Scale Map.empty text).[0]
  let arrowGlyphsAsSvg =
    SvgTemplateOpen + (sprintf "\n%s\n" renderedText ) + renderResult + SvgTemplateClose
    |> fun x -> regex(@"\r\n").Replace(x, "\n")
  let arrowGlyphsAsSvgExpected =
    @"../../TestSvgFiles/ArrowGlyphs.svg"
    |> readFileAsText
    |> function | Success x -> x | _ -> ""
    |> fun x -> regex(@"\r\n").Replace(x, "\n")
  Assert.AreEqual(arrowGlyphsAsSvgExpected, arrowGlyphsAsSvg)

// #endregion

  sprintf "[OK] Test run finished at %A" (DateTime.Now.ToLocalTime())
  |> printfn "%s"
