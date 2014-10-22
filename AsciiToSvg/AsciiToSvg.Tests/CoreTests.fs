namespace AsciiToSvg.Tests

module Core =

  open System

  open NUnit.Framework

  open AsciiToSvg


  // #region AsciiToSvg.Common.Tests

  [<Test>]
  let ``AsciiToSvg.Common : matchPositions`` () =
    Assert.AreEqual (Tests.TxtFile.matchPositionsExpected, Tests.TxtFile.matchPositionsResult)

  // #endregion

  // #region AsciiToSvg.SvgDocument.Tests

  [<Test>]
  let ``AsciiToSvg.SvgDocument : ConvertCoordGridToSvg`` () =
    Assert.AreEqual(Tests.SvgDocument.svgCoordExpected, Tests.SvgDocument.svgCoord)
  [<Test>]
  let ``AsciiToSvg.SvgDocument : ShiftedCoordGridToSvg`` () =
    Assert.AreEqual(Tests.SvgDocument.shiftedCoordExpected, Tests.SvgDocument.shiftedCoord)
  [<Test>]
  let ``AsciiToSvg.SvgDocument : shiftColCoordGridToSvg`` () =
    Assert.AreEqual(Tests.SvgDocument.colsExpected, Tests.SvgDocument.cols)
  [<Test>]
  let ``AsciiToSvg.SvgDocument : shiftRowCoordGridToSvg`` () =
    Assert.AreEqual(Tests.SvgDocument.rowsExpected, Tests.SvgDocument.rows)

  // #endregion

  // #region AsciiToSvg.TxtFile.Tests

  [<Test>]
  let ``AsciiToSvg.TxtFile : leftOffset``() =
    Assert.AreEqual(Tests.GlyphScanner.leftOffsetExpected, Tests.GlyphScanner.leftOffsetResult)

  [<Test>]
  let ``AsciiToSvg.TxtFile : trimWithOffset``() =
    Assert.AreEqual(Tests.GlyphScanner.trimWithOffsetExpected, Tests.GlyphScanner.trimWithOffsetResult)

  [<Test>]
  let ``AsciiToSvg.TxtFile : splitText``() =
    Tests.TxtFile.splitTxtResult = Tests.TxtFile.splitTxtResultExpected
    |>  Assert.True
    //Assert.AreEqual(Tests.GlyphScanner.splitTxtResult, Tests.GlyphScanner.splitTxtResultExpected)
    Tests.GlyphScanner.splitTxtResultOk
    |>  Assert.True

  [<Test>]
  let ``AsciiToSvg.TxtFile : makeFramedGrid`` () =
    Assert.AreEqual (Tests.TxtFile.makeFramedGridResultExpected, Tests.TxtFile.makeFramedGridResult)

  [<Test>]
  let ``AsciiToSvg.TxtFile : makeTrimmedGrid`` () =
    Assert.AreEqual (Tests.GlyphScanner.makeGridResultExpected, Tests.GlyphScanner.makeGridResult)

  [<Test>]
  let ``AsciiToSvg.TxtFile : replaceOption`` () =
    Assert.AreEqual (Tests.TxtFile.replaceOptionResultExpected, Tests.TxtFile.replaceOptionResult)

  // #endregion

  // #region AsciiToSvg.GlyphScanner.Tests

  [<Test>]
  let ``AsciiToSvg.GlyphScanner : ScanGlyphs``() =
    Assert.AreEqual ([|0; 1; 2; 3; 4; 5; 6; 7|], Tests.GlyphScanner.scanGridResultMapped)

  // #endregion

  // #region  AsciiToSvg.GlyphRenderer.Tests

  [<Test>]
  let ``AsciiToSvg.GlyphRenderer : Render``() =
    Assert.AreEqual (Tests.GlyphRenderer.renderResultExpected, Tests.GlyphRenderer.renderResult)
    Assert.AreEqual(Tests.GlyphRenderer.arrowGlyphsWithoutTextAsSvgExpected, Tests.GlyphRenderer.arrowGlyphsWithoutTextAsSvg)

  // #endregion

  // #region  AsciiToSvg.TextScanner.Tests

  [<Test>]
  let ``AsciiToSvg.TextScanner : ScanText``() =
    Assert.AreEqual(Tests.TextScanner.textExpected, Tests.TextScanner.text)

  // #endregion

  // #region  AsciiToSvg.TextRenderer.Tests

  [<Test>]
  let ``AsciiToSvg.TextRenderer : RenderAll``() =
    Assert.AreEqual(Tests.TextRenderer.arrowGlyphsAsSvgExpected, Tests.TextRenderer.arrowGlyphsAsSvg)

  // #endregion

  sprintf "Test run finished at %A" (DateTime.Now.ToLocalTime())
  |> printfn "%s"
