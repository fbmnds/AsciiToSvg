
System.Environment.CurrentDirectory <- System.Environment.GetEnvironmentVariable("PROJECTS") + @"\a2svg\AsciiToSvg";;

#r "bin/Debug/AsciiToSvg.dll"

open AsciiToSvg
open AsciiToSvg.SvgDocument
open AsciiToSvg.TxtFile
open AsciiToSvg.GlyphScanner
open AsciiToSvg.TextScanner

type emacsPrintHelp =
  { grid : TxtGrid;
    options : SvgOption
    glyphs : Glyph[]
    renderedGlyphs : string
    text : Text[]
    renderedText : string
    lines :  Line[] * Line[]
    renderedLines : string }


let emacsPrintHelpGrid =
  @"TestTxtFiles/EmacsPrintHelp.txt"
  |> readFile
  |> splitTxt
  |> fun (_, b) -> b
  |> makeTrimmedGrid

let options =
  ["canvas-width", ((float)emacsPrintHelpGrid.[0].Length * GlyphWidth).ToString(culture);
  "canvas-height", ((float)emacsPrintHelpGrid.Length * GlyphHeight).ToString(culture);
  "canvas-font-family", "Courier New";
  "canvas-font-size", "15.0"]
  |> Map.ofList

let emacsPrintHelpGlyphs = emacsPrintHelpGrid |> ScanGlyphs

let emacsPrintHelpRenderedGlyphs =
  emacsPrintHelpGlyphs
  |> Array.Parallel.map (GlyphRenderer.Render Scale options)
  |> Array.sort
  |> Array.fold (fun r s -> r + s + "\n") ""

let emacsPrintHelpTabbedText = emacsPrintHelpGrid |> ScanTabbedText

let emacsPrintHelpRenderedText =
  (TextRenderer.RenderAll Scale options emacsPrintHelpTabbedText)
  |> Array.fold (fun r s -> r + s + "\n") ""

let emacsPrintHelpAllLines = emacsPrintHelpGrid |> LineScanner.ScanLine

let emacsPrintHelpRenderedLines =
  [|LineRenderer.RenderAll Scale options (fst emacsPrintHelpAllLines)
    LineRenderer.RenderAll Scale options (snd emacsPrintHelpAllLines)|]
  |> Array.fold (fun r s -> r + s) ""

let emacsPrintHelpSvg =
  [|SvgTemplateOpen(options)
    emacsPrintHelpRenderedGlyphs
    emacsPrintHelpRenderedText
    emacsPrintHelpRenderedLines
    SvgTemplateClose|]
  |> Array.fold (fun r s -> r + s) ""
  |> fun x -> regex(@"\r\n").Replace(x, "\n")


System.IO.File.WriteAllText(@"TestSvgFiles/EmacsPrintHelp.svg", emacsPrintHelpSvg)