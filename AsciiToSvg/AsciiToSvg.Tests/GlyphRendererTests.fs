namespace AsciiToSvg.Tests

module GlyphRenderer =

  open AsciiToSvg
  open AsciiToSvg.GlyphRenderer
  open AsciiToSvg.SvgDocument
  open AsciiToSvg.Tests.GlyphScanner

  module ArrowGlyph_txt =
    let renderResult =
      ArrowGlyph_txt.scanGridResult
      |> Array.map (Render Scale Map.empty)
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
