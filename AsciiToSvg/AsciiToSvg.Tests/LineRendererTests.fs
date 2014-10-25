﻿namespace AsciiToSvg.Tests

module LineRenderer =

  open AsciiToSvg
  open AsciiToSvg.SvgDocument
  open AsciiToSvg.Tests.TxtFile
  open AsciiToSvg.Tests.GlyphRenderer
  open AsciiToSvg.Tests.TextRenderer
  open AsciiToSvg.Tests.LineScanner

  module ArrowGlyph_txt =

    let options =
      ["canvas-width", ((float)ArrowGlyph_txt.makeGridResult.[0].Length * GlyphWidth).ToString(culture);
       "canvas-height", ((float)ArrowGlyph_txt.makeGridResult.Length * GlyphHeight).ToString(culture)]
      |> Map.ofList

    let renderedAllLines =
      [|LineRenderer.RenderAll Scale options ArrowGlyph_txt.horizLines
        LineRenderer.RenderAll Scale options ArrowGlyph_txt.vertLines|]
      |> Array.fold (fun r s -> r + s) ""

    let arrowGlyphsAsSvg =
      [|SvgTemplateOpen(options)
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

  module ArrowGlyphWithFrame_txt =

    let options =
      ["canvas-width", ((float)ArrowGlyphWithFrame_txt.makeGridResult.[0].Length * GlyphWidth).ToString(culture);
        "canvas-height", ((float)ArrowGlyphWithFrame_txt.makeGridResult.Length * GlyphHeight).ToString(culture);
        "canvas-font-family", "Courier New"]
      |> Map.ofList

    let renderedAllLines =
      [|LineRenderer.RenderAll Scale options ArrowGlyphWithFrame_txt.horizLines
        LineRenderer.RenderAll Scale options ArrowGlyphWithFrame_txt.vertLines|]
      |> Array.fold (fun r s -> r + s) ""

    let arrowGlyphsWithFrameAsSvg =
      [|SvgTemplateOpen(options)
        ArrowGlyphWithFrame_txt.renderedFramedTextTabbed
        ArrowGlyphWithFrame_txt.renderResult
        renderedAllLines
        SvgTemplateClose|]
      |> Array.fold (fun r s -> r + s) ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let arrowGlyphsWithFrameAsSvgExpected =
      @"../../TestSvgFiles/ArrowGlyphsWithFrame.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

  module TestPolygonBox_txt =

    let options =
      ["canvas-width", ((float)TestPolygonBox_txt.makeGridResult.[0].Length * GlyphWidth).ToString(culture);
       "canvas-height", ((float)TestPolygonBox_txt.makeGridResult.Length * GlyphHeight).ToString(culture);
       "canvas-font-family", "Courier New"]
      |> Map.ofList

    let renderedAllLines =
      [|LineRenderer.RenderAll Scale options TestPolygonBox_txt.horizLines
        LineRenderer.RenderAll Scale options TestPolygonBox_txt.vertLines|]
      |> Array.fold (fun r s -> r + s) ""

    let testPolygonBoxAsSvg =
      [|SvgTemplateOpen(options)
        TestPolygonBox_txt.renderResult
        renderedAllLines
        SvgTemplateClose|]
      |> Array.fold (fun r s -> r + s) ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let testPolygonBoxAsSvgExpected =
      @"../../TestSvgFiles/TestPolygonBox.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")


  module TestMiniBox_txt =

    let options =
      ["canvas-width", ((float)TestMiniBox_txt.makeGridResult.[0].Length * GlyphWidth).ToString(culture);
       "canvas-height", ((float)TestMiniBox_txt.makeGridResult.Length * GlyphHeight).ToString(culture)]
      |> Map.ofList

    let renderedAllLines =
      [|LineRenderer.RenderAll Scale options TestMiniBox_txt.horizLines
        LineRenderer.RenderAll Scale options TestMiniBox_txt.vertLines|]
      |> Array.fold (fun r s -> r + s) ""

    let testMiniBoxAsSvg =
      [|SvgTemplateOpen(options)
        TestMiniBox_txt.renderResult
        renderedAllLines
        SvgTemplateClose|]
      |> Array.fold (fun r s -> r + s) ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

    let testMiniBoxAsSvgExpected =
      @"../../TestSvgFiles/TestMiniBox.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")

  module ZeroMQ_Fig1_txt =

    let options =
      ["canvas-width", ((float)ZeroMQ_Fig1_txt.makeGridResult.[0].Length * GlyphWidth).ToString(culture);
       "canvas-height", ((float)ZeroMQ_Fig1_txt.makeGridResult.Length * GlyphHeight).ToString(culture);
       "canvas-font-family", "Courier New"]
      |> Map.ofList

    let renderedAllLines =
      [|LineRenderer.RenderAll Scale options ZeroMQ_Fig1_txt.horizLines
        LineRenderer.RenderAll Scale options ZeroMQ_Fig1_txt.vertLines|]
      |> Array.fold (fun r s -> r + s) ""

    let zeroMQ_Fig1AsSvg =
      [|SvgTemplateOpen(options)
        ZeroMQ_Fig1_txt.renderedFramedTextTabbed
        ZeroMQ_Fig1_txt.renderResult
        renderedAllLines
        SvgTemplateClose|]
      |> Array.fold (fun r s -> r + s) ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")
    System.IO.File.WriteAllText(@"../../TestSvgFiles/ZeroMQ_Fig1.svg", zeroMQ_Fig1AsSvg)
    let zeroMQ_Fig1AsSvgExpected =
      @"../../TestSvgFiles/ZeroMQ_Fig1.svg"
      |> readFileAsText
      |> function | Success x -> x | _ -> ""
      |> fun x -> regex(@"\r\n").Replace(x, "\n")
