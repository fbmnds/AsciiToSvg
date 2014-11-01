open System
open System.IO

Environment.CurrentDirectory <- Environment.GetEnvironmentVariable("PROJECTS") + @"\a2svg\AsciiToSvg";;

#r "bin/Debug/AsciiToSvg.dll"

open AsciiToSvg

// in LineScannerTests.fs:
//      printfn "%A" allLines
// capture output from the test runner ouput window
// paste into LineScannerTests.fs -> allLinesExpected
// apply tidyUpLineScanner
// OK with manual check of the result, otherwise it is cheating
let tidyUpLine file =
  @"AsciiToSvg.Tests\" + file
  |> readFileAsText
  |> function
  | Success text -> text
  | _ -> ""
  |> replaceAll @"map \[\];" "Map.empty "
  |> replaceAll @"}; {o" "}\n          { o"
  |> replaceAll @"[;]*\s*[\r]?\n" " \n"
  |> fun x -> File.WriteAllText(@"AsciiToSvg.Tests\" + file, x)

tidyUpLine "LineScannerTests.fs"


let tidyUpGlyphs file =
  @"AsciiToSvg.Tests\" + file
  |> readFileAsText
  |> function
  | Success text -> text
  | _ -> ""
  |> replaceAll @"map \[\];" "Map.empty "
  // bring all attributes on one line
  |> replaceAll @";\s*[\r]?\n\s+" "; "
  // separate glyph records
  |> replaceAll @"}; {\s*glyphKind" "};\n        { glyphKind"
  |> replaceAll @"}, {\s*glyphKind" "},\n        { glyphKind"
  |> replaceAll @"^\s*{\s*glyphKind" "         { glyphKind"
  // trim blanks
  |> replaceAll @"\s*$" Environment.NewLine
  |> fun x -> File.WriteAllText(@"AsciiToSvg.Tests\" + file, x)

tidyUpGlyphs "GlyphScannerTests.fs"

let tidyUpGlyphsAndLines file =
  @"AsciiToSvg.Tests\" + file
  |> readFileAsText
  |> function
  | Success text -> text
  | _ -> ""
  |> replaceAll @"map \[\];" "Map.empty "
  // bring all attributes on one line
  |> replaceAll @";\s*[\r]?\n\s+" "; "
  // separate glyph records
  |> replaceAll @"}; {\s*glyphKind" "};\n        { glyphKind"
  |> replaceAll @"}, {\s*glyphKind" "},\n        { glyphKind"
  |> replaceAll @"^\s*{\s*glyphKind" "         { glyphKind"
  // separate line records
  |> replaceAll @"}; {o" "};\n          { o"
  |> replaceAll @"}, {ori" "},\n          { ori"
  |> replaceAll @"^\s*{\s*ori" "         { ori"
  // trim blanks
  |> replaceAll @"\s*$" Environment.NewLine
  |> fun x -> File.WriteAllText(@"AsciiToSvg.Tests\" + file, x)

tidyUpGlyphsAndLines "TopologyTests.fs"
