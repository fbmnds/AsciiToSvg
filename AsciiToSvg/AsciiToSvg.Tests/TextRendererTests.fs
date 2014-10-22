namespace AsciiToSvg.Tests

module TextRenderer =

  open AsciiToSvg
  open AsciiToSvg.SvgDocument
  open AsciiToSvg.TextRenderer
  open AsciiToSvg.Tests.TxtFile
  open AsciiToSvg.Tests.GlyphRenderer

  module ArrowGlyph_txt =
    CanvasWidth <- (float)ArrowGlyph_txt.makeGridResult.[0].Length * GlyphWidth
    CanvasHeight <- (float)ArrowGlyph_txt.makeGridResult.Length * GlyphHeight
    let renderedText = (RenderAll Scale Map.empty Tests.TextScanner.text).[0]
    let arrowGlyphsAsSvg =
      SvgTemplateOpen + (sprintf "\n%s\n" renderedText ) + ArrowGlyph_txt.renderResult + SvgTemplateClose
      |> fun x -> regex(@"\r\n").Replace(x, "\n")
    let arrowGlyphsAsSvgExpected =
      @"../../TestSvgFiles/ArrowGlyphsWithoutLines.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let renderedTextTabbed = (RenderAll Scale Map.empty Tests.TextScanner.textTabbed) |> Array.fold (fun r s -> r + s + "\n") ""
    let arrowGlyphsTabbedAsSvg =
      SvgTemplateOpen + (sprintf "\n%s\n" renderedTextTabbed ) + ArrowGlyph_txt.renderResult + SvgTemplateClose
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let arrowGlyphsTabbedAsSvgExpected =
      @"../../TestSvgFiles/ArrowGlyphsTabbed.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")
