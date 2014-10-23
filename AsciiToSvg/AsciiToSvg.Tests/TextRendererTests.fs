namespace AsciiToSvg.Tests

module TextRenderer =

  open AsciiToSvg
  open AsciiToSvg.SvgDocument
  open AsciiToSvg.Tests.TxtFile
  open AsciiToSvg.Tests.TextScanner
  open AsciiToSvg.Tests.GlyphRenderer

  module ArrowGlyph_txt =

    let options =
      ["canvas-width", ((float)ArrowGlyph_txt.makeGridResult.[0].Length * GlyphWidth).ToString(culture);
       "canvas-height", ((float)ArrowGlyph_txt.makeGridResult.Length * GlyphHeight).ToString(culture)]
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

    let options =
      ["canvas-width", ((float)ArrowGlyphWithFrame_txt.makeGridResult.[0].Length * GlyphWidth).ToString(culture);
       "canvas-height", ((float)ArrowGlyphWithFrame_txt.makeGridResult.Length * GlyphHeight).ToString(culture);
       "canvas-font-family", "Courier New";
       "canvas-font-size", "15.0"]
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
