module AsciiToSvg.Tests


open System

// https://github.com/fsharp/FsCheck/blob/master/Docs/Documentation.md
// https://github.com/fsharp/FsUnit
// https://code.google.com/p/unquote/
open NUnit.Framework

open AsciiToSvg
open AsciiToSvg.GlyphScanner
open AsciiToSvg.GlyphRenderer

[<Test>]
let ``TxtFile : TestLogo.txt``() =
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


//--------------------------------------------------------------------------------------------------------------------


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
  let makeGridResult = splitTxtResult |> fun (a, b) -> b |> makeTrimmedGrid
  Assert.AreEqual (makeGridResultExpected, makeGridResult)

  let scanner = new ArrowUpScanner() :> IGlyphScanner
  let scanResult = makeGridResult |> scanner.Scan
  let scanResultExpected =
    [| ArrowUp { letter = '^';
         gridCoord = {row = 1; col = 1;};
         glyphOptions = Map.empty };
       ArrowUp { letter = '^';
         gridCoord = {row = 1; col = 5;};
         glyphOptions = Map.empty }|]
  Assert.AreEqual (scanResultExpected, scanResult)

  let scanGridResultExpected =
    [|ArrowUp { letter = '^'; gridCoord = { row = 1; col = 1 }; glyphOptions = Map.empty }
      ArrowUp { letter = '^'; gridCoord = { row = 1; col = 5 }; glyphOptions = Map.empty }
      ArrowDown { letter = 'v'; gridCoord = { row = 2; col = 12 }; glyphOptions = Map.empty }
      ArrowDown { letter = 'v'; gridCoord = { row = 2; col = 16 }; glyphOptions = Map.empty }
      ArrowLeftToRight { letter = '>'; gridCoord = { row = 1; col = 28 }; glyphOptions = Map.empty }
      ArrowLeftToRight { letter = '>'; gridCoord = { row = 2; col = 28 }; glyphOptions = Map.empty }
      ArrowRightToLeft { letter = '<'; gridCoord = { row = 1; col = 45 }; glyphOptions = Map.empty }
      ArrowRightToLeft { letter = '<'; gridCoord = { row = 2; col = 45 }; glyphOptions = Map.empty }|]
  let scanGridResult = makeGridResult |> ScanGrid
  let scanGridResultMapped =
    scanGridResult
    |> Array.map (fun x -> Array.IndexOf(scanGridResultExpected, x))
    |> Array.sort
  Assert.AreEqual ([|0; 1; 2; 3; 4; 5; 6; 7|], scanGridResultMapped)

  let renderResult =
    [| (ArrowUpRenderer() :> IGlyphRenderer).Render { scx = 1.0; scy = 1.0 } Map.empty
       (ArrowDownRenderer() :> IGlyphRenderer).Render { scx = 1.0; scy = 1.0 } Map.empty
       (ArrowLeftToRightRenderer() :> IGlyphRenderer).Render { scx = 1.0; scy = 1.0 } Map.empty
       (ArrowRightToLeftRenderer() :> IGlyphRenderer).Render { scx = 1.0; scy = 1.0 } Map.empty |]
    |> Array.Parallel.map (fun f -> Array.Parallel.map f scanGridResult)
    |> Array.concat
    |> Array.choose id
    |> Array.sort

  printfn "%A" renderResult

  let renderResultExpected =
    [|"      <polygon fill=\"black\" points=\"8.000,29.000 4.000,29.000 8.000,36.000 8.000,29.000 12.000,29.000 8.000,28.000\" />";
      "      <polygon fill=\"black\" points=\"8.000,35.000 12.000,35.000 8.000,28.000 8.000,35.000 4.000,35.000 8.000,36.000\" />";
      "      <polygon fill=\"black\" points=\"8.000,35.000 4.000,35.000 11.000,35.000 8.000,35.000 8.000,35.000 8.000,36.000\" />";
      "      <polygon fill=\"black\" points=\"8.000,5.000 8.000,13.000 1.000,9.000 8.000,5.000 8.000,9.000 15.000,9.000\" />";
      "      <polygon fill=\"black\" points=\"9.000,12.000 9.000,20.000 16.000,16.000 9.000,12.000 9.000,16.000 2.000,16.000\" />";
      "      <polygon fill=\"black\" points=\"9.000,16.000 9.000,24.000 16.000,20.000 9.000,16.000 9.000,20.000 2.000,20.000\" />";
      "      <polygon fill=\"black\" points=\"9.000,28.000 5.000,35.000 12.000,35.000 9.000,28.000 9.000,35.000 9.000,36.000\" />";
      "      <polygon fill=\"black\" points=\"9.000,35.000 5.000,35.000 12.000,35.000 9.000,35.000 9.000,35.000 9.000,36.000\" />"|]
  //Assert.AreEqual (renderResultExpected, renderResult)

  let ArrowGlyphsAsSvg =
    svgTemplateOpen + (renderResult |> Array.fold (fun r s -> r + s + "\n") "") + svgTemplateClose

  printfn "%A" ArrowGlyphsAsSvg

//--------------------------------------------------------------------------------------------------------------------

sprintf "Test run finished at %A" (DateTime.Now.ToLocalTime())
|> printfn "%s"
