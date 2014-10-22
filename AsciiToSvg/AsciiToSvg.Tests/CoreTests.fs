namespace AsciiToSvg.Tests

module Core =

  open System

  open NUnit.Framework

  open AsciiToSvg
  open AsciiToSvg.Tests.TxtFile
  open AsciiToSvg.Tests.GlyphScanner
  open AsciiToSvg.Tests.GlyphRenderer
  open AsciiToSvg.Tests.TextRenderer
  open AsciiToSvg.Tests.LineScanner

  // #region AsciiToSvg.Common.Tests

  [<Test>]
  let ``Common : matchPositions`` () =
    Assert.AreEqual (Tests.TxtFile.matchPositionsExpected, Tests.TxtFile.matchPositionsResult)

  // #endregion

  // #region AsciiToSvg.SvgDocument.Tests

  [<Test>]
  let ``SvgDocument : ConvertCoordGridToSvg`` () =
    Assert.AreEqual(Tests.SvgDocument.svgCoordExpected, Tests.SvgDocument.svgCoord)
  [<Test>]
  let ``SvgDocument : ShiftedCoordGridToSvg`` () =
    Assert.AreEqual(Tests.SvgDocument.shiftedCoordExpected, Tests.SvgDocument.shiftedCoord)
  [<Test>]
  let ``SvgDocument : shiftColCoordGridToSvg`` () =
    Assert.AreEqual(Tests.SvgDocument.colsExpected, Tests.SvgDocument.cols)
  [<Test>]
  let ``SvgDocument : shiftRowCoordGridToSvg`` () =
    Assert.AreEqual(Tests.SvgDocument.rowsExpected, Tests.SvgDocument.rows)

  // #endregion

  // #region AsciiToSvg.TxtFile.Tests

  [<Test>]
  let ``TxtFile : leftOffset``() =
    Assert.AreEqual(ArrowGlyph_txt.leftOffsetExpected, ArrowGlyph_txt.leftOffsetResult)

  [<Test>]
  let ``TxtFile : trimWithOffset``() =
    Assert.AreEqual(ArrowGlyph_txt.trimWithOffsetExpected, ArrowGlyph_txt.trimWithOffsetResult)

  [<Test>]
  let ``TxtFile : splitText``() =
    Tests.TxtFile.splitTxtResult = Tests.TxtFile.splitTxtResultExpected
    |>  Assert.True
    ArrowGlyph_txt.splitTxtResultOk
    |>  Assert.True

  [<Test>]
  let ``TxtFile : makeFramedGrid`` () =
    Assert.AreEqual (Tests.TxtFile.makeFramedGridResultExpected, Tests.TxtFile.makeFramedGridResult)

  [<Test>]
  let ``TxtFile : makeTrimmedGrid`` () =
    Assert.AreEqual (ArrowGlyph_txt.makeGridResultExpected, ArrowGlyph_txt.makeGridResult)

  [<Test>]
  let ``TxtFile : replaceOption`` () =
    Assert.AreEqual (Tests.TxtFile.replaceOptionResultExpected, Tests.TxtFile.replaceOptionResult)

  // #endregion

  // #region AsciiToSvg.GlyphScanner.Tests

  [<Test>]
  let ``GlyphScanner : ScanGlyphs``() =
    Assert.AreEqual ([|0; 1; 2; 3; 4; 5; 6; 7|], ArrowGlyph_txt.scanGridResultMapped)

  // #endregion

  // #region  AsciiToSvg.GlyphRenderer.Tests

  [<Test>]
  let ``GlyphRenderer : Render``() =
    Assert.AreEqual (ArrowGlyph_txt.renderResultExpected, ArrowGlyph_txt.renderResult)
    Assert.AreEqual(ArrowGlyph_txt.arrowGlyphsWithoutTextAsSvgExpected, ArrowGlyph_txt.arrowGlyphsWithoutTextAsSvg)

  // #endregion

  // #region  AsciiToSvg.TextScanner.Tests

  [<Test>]
  let ``TextScanner : ScanText``() =
    Assert.AreEqual(Tests.TextScanner.textExpected, Tests.TextScanner.text)

  [<Test>]
  let ``TextScanner : ScanTabbedText``() =
    Assert.AreEqual(Tests.TextScanner.textTabbedExpected, Tests.TextScanner.textTabbed)

  // #endregion

  // #region  AsciiToSvg.TextRenderer.Tests

  [<Test>]
  let ``TextRenderer : RenderAll``() =
    Assert.AreEqual(ArrowGlyph_txt.arrowGlyphsAsSvgExpected, ArrowGlyph_txt.arrowGlyphsAsSvg)

  // #endregion

  // #region  AsciiToSvg.LineScanner.Tests

  [<Test>]
  let ``LineScanner : ScanLineHorizontally`` () =
    ArrowGlyph_txt.lineScanOK
    |> Assert.True

  // #endregion

  sprintf "Test run finished at %A" (DateTime.Now.ToLocalTime())
  |> printfn "%s"
