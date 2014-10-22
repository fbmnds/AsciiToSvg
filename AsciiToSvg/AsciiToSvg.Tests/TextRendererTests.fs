namespace AsciiToSvg.Tests

module TextRenderer =

  open AsciiToSvg
  open AsciiToSvg.SvgDocument
  open AsciiToSvg.TextRenderer
  open AsciiToSvg.Tests.GlyphScanner

  // TODO: add Line rendering to this test case
  //
  CanvasWidth <- (float)ArrowGlyph_txt.makeGridResult.[0].Length * GlyphWidth
  CanvasHeight <- (float)ArrowGlyph_txt.makeGridResult.Length * GlyphHeight
  let renderedText = (RenderAll Scale Map.empty Tests.TextScanner.text).[0]
  let arrowGlyphsAsSvg =
    SvgTemplateOpen + (sprintf "\n%s\n" renderedText ) + Tests.GlyphRenderer.renderResult + SvgTemplateClose
    |> fun x -> regex(@"\r\n").Replace(x, "\n")
  let arrowGlyphsAsSvgExpected =
    @"../../TestSvgFiles/ArrowGlyphs.svg"
    |> readFileAsText
    |> function | Success x -> x | _ -> ""
    |> fun x -> regex(@"\r\n").Replace(x, "\n")

