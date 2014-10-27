namespace AsciiToSvg.Tests

module TextRenderer =

  open AsciiToSvg
  open AsciiToSvg.SvgDocument
  open AsciiToSvg.Tests.TxtFile
  open AsciiToSvg.Tests.TextScanner
  open AsciiToSvg.Tests.GlyphRenderer

  module ArrowGlyph_txt =

    let options : SvgOption =
      ["canvas-width", (Number ((float)ArrowGlyph_txt.makeGridResult.[0].Length * GlyphWidth));
       "canvas-height", (Number ((float)ArrowGlyph_txt.makeGridResult.Length * GlyphHeight))]
      |> Map.ofList

    let renderedText = (TextRenderer.RenderAll Scale options ArrowGlyph_txt.text).[0]
    let arrowGlyphsWithoutLinesAsSvg =
      SvgTemplateOpen(options) + (sprintf "\n%s\n" renderedText ) + ArrowGlyph_txt.renderResult + SvgTemplateClose
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let arrowGlyphsWithoutLinesAsSvgExpected =
      @"../../TestSvgFiles/ArrowGlyphsWithoutLines.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let renderedTextTabbed =
      (TextRenderer.RenderAll Scale options ArrowGlyph_txt.textTabbed)
      |> Array.fold (fun r s -> r + s + "\n") ""
    let arrowGlyphsTabbedAsSvg =
      SvgTemplateOpen(options) + (sprintf "\n%s\n" renderedTextTabbed ) + ArrowGlyph_txt.renderResult + SvgTemplateClose
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let arrowGlyphsTabbedAsSvgExpected =
      @"../../TestSvgFiles/ArrowGlyphsTabbed.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

  module ArrowGlyphWithFrame_txt =

    let options : SvgOption =
      ["canvas-width", (Number ((float)ArrowGlyphWithFrame_txt.makeGridResult.[0].Length * GlyphWidth));
       "canvas-height", (Number ((float)ArrowGlyphWithFrame_txt.makeGridResult.Length * GlyphHeight));
       "canvas-font-family", (JString "Courier New");
       "canvas-font-size", (Number 15.0)]
      |> Map.ofList

    let renderedFramedTextTabbed =
      (TextRenderer.RenderAll Scale options ArrowGlyphWithFrame_txt.textTabbed)
      |> Array.fold (fun r s -> r + s + "\n") ""

    let arrowGlyphsFramedTabbedAsSvg =
      [|SvgTemplateOpen(options)
        (sprintf "\n%s\n" renderedFramedTextTabbed )
        ArrowGlyphWithFrame_txt.renderResult
        SvgTemplateClose|]
      |> Array.fold (fun r s -> r + s + "\n") ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let arrowGlyphsFramedTabbedAsSvgExpected =
      @"../../TestSvgFiles/ArrowGlyphsFramedTabbed.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

  module ZeroMQ_Fig1_txt =

    let options : SvgOption =
      ["canvas-width", (Number ((float)ZeroMQ_Fig1_txt.makeGridResult.[0].Length * GlyphWidth));
       "canvas-height", (Number ((float)ZeroMQ_Fig1_txt.makeGridResult.Length * GlyphHeight));
       "canvas-font-family", (JString "Courier New");
       "canvas-font-size", (Number 15.0)]
      |> Map.ofList

    let renderedFramedTextTabbed =
      (TextRenderer.RenderAll Scale options ZeroMQ_Fig1_txt.textTabbed)
      |> Array.fold (fun r s -> r + s + "\n") ""

    let zeroMQ_Fig1TabbedAsSvg =
      [|SvgTemplateOpen(options)
        (sprintf "\n%s\n" renderedFramedTextTabbed )
        ZeroMQ_Fig1_txt.renderResult
        SvgTemplateClose|]
      |> Array.fold (fun r s -> r + s + "\n") ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let zeroMQ_Fig1TabbedAsSvgExpected =
      @"../../TestSvgFiles/ZeroMQ_Fig1Tabbed.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")
