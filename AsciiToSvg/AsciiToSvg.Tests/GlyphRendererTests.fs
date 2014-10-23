namespace AsciiToSvg.Tests

module GlyphRenderer =

  open AsciiToSvg
  open AsciiToSvg.SvgDocument
  open AsciiToSvg.Tests.TxtFile
  open AsciiToSvg.Tests.GlyphScanner

  module ArrowGlyph_txt =

    let options =
      ["canvas-width", ((float)ArrowGlyph_txt.makeGridResult.[0].Length * GlyphWidth).ToString(culture);
       "canvas-height", ((float)ArrowGlyph_txt.makeGridResult.Length * GlyphHeight).ToString(culture)]
      |> Map.ofList

    let renderResult =
      ArrowGlyph_txt.scanGridResult
      |> Array.map (GlyphRenderer.Render Scale options)
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

    let arrowGlyphsWithoutTextAsSvg =
      [|SvgTemplateOpen(options)
        renderResult
        SvgTemplateClose|]
      |> Array.fold (fun r s -> r + s + "\n") ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let arrowGlyphsWithoutTextAsSvgExpected =
      @"../../TestSvgFiles/ArrowGlyphsWithoutText.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")


  module ArrowGlyphWithFrame_txt =

    let options =
      ["canvas-width", ((float)ArrowGlyphWithFrame_txt.makeGridResult.[0].Length * GlyphWidth).ToString(culture);
       "canvas-height", ((float)ArrowGlyphWithFrame_txt.makeGridResult.Length * GlyphHeight).ToString(culture);
       "canvas-font-family", "Courier New"]
      |> Map.ofList

    let renderResult =
      ArrowGlyphWithFrame_txt.scanGridResult
      |> Array.Parallel.map (GlyphRenderer.Render Scale options)
      |> Array.sort
      |> Array.fold (fun r s -> r + s + "\n") ""

    let arrowGlyphsFramedWithoutTextAsSvg =
      [|SvgTemplateOpen(options)
        renderResult
        SvgTemplateClose|]
      |> Array.fold (fun r s -> r + s + "\n") ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let arrowGlyphsFramedWithoutTextAsSvgExpected =
      @"../../TestSvgFiles/ArrowGlyphsFramedWithoutText.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")