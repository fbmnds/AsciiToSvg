namespace AsciiToSvg.Tests

module LineRenderer =

  open AsciiToSvg
  open AsciiToSvg.SvgDocument
  open AsciiToSvg.LineRenderer
  open AsciiToSvg.Tests.TxtFile
  open AsciiToSvg.Tests.GlyphRenderer
  open AsciiToSvg.Tests.TextRenderer
  open AsciiToSvg.Tests.LineScanner

  module ArrowGlyph_txt =
    CanvasWidth <- 450.0
    CanvasHeight <- 75.0

    let renderedAllLines =
      [|RenderAll Scale Map.empty ArrowGlyph_txt.horizLines
        RenderAll Scale Map.empty ArrowGlyph_txt.vertLines|]
      |> Array.fold (fun r s -> r + s) ""

    let arrowGlyphsAsSvg =
      [|SvgTemplateOpen
        ArrowGlyph_txt.renderedTextTabbed
        ArrowGlyph_txt.renderResult
        renderedAllLines
        SvgTemplateClose|]
      |> Array.fold (fun r s -> r + s) ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let arrowGlyphsSvgExpected =
      @"../../TestSvgFiles/ArrowGlyphs.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")
