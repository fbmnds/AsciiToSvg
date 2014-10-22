namespace AsciiToSvg.Tests

module GlyphScanner =

  open System

  open AsciiToSvg
  open AsciiToSvg.Common
  open AsciiToSvg.TxtFile
  open AsciiToSvg.GlyphScanner

  let splitTxtResultExpected : (string * string)[] * string[] =
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
  let splitTxtResultOk = (splitTxtResultExpected = splitTxtResult)


  let leftOffsetExpected = 1
  let leftOffsetResult = leftOffset (snd splitTxtResult)

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
  let trimWithOffsetResult = trimWithOffset 1 (snd splitTxtResult)

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