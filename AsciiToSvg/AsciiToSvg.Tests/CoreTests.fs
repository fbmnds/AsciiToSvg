namespace AsciiToSvg.Tests

module Core =

  open System

  open NUnit.Framework

  open AsciiToSvg
  open AsciiToSvg.Tests.TxtFile
  open AsciiToSvg.Tests.GlyphScanner
  open AsciiToSvg.Tests.GlyphRenderer
  open AsciiToSvg.Tests.TextScanner
  open AsciiToSvg.Tests.TextRenderer
  open AsciiToSvg.Tests.LineScanner
  open AsciiToSvg.Tests.LineRenderer

  // #region AsciiToSvg.Common.Tests

  [<Test>]
  let ``Common : matchPositions : TestLogo.txt`` () =
    Assert.AreEqual (TestLogo_txt.matchPositionsExpected, TestLogo_txt.matchPositionsResult)

  // #endregion

    // #region AsciiToSvg.Common.Tests

  [<Test>]
  let ``Json : parse : TestLogo.txt`` () =
    Assert.AreEqual (TestLogo_txt.parseFailureResultExpected, TestLogo_txt.parseFailureResult)
    Assert.AreEqual (TestLogo_txt.parseSuccessResultExpected, TestLogo_txt.parseSuccessResult)

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
  let ``TxtFile : leftOffset : TestLogo.txt``() =
    Assert.AreEqual(TestLogo_txt.leftOffsetExpected, TestLogo_txt.leftOffsetResult)
  [<Test>]
  let ``TxtFile : leftOffset : ArrowGlyph.txt``() =
    Assert.AreEqual(ArrowGlyph_txt.leftOffsetExpected, ArrowGlyph_txt.leftOffsetResult)
  [<Test>]
  let ``TxtFile : leftOffset : ArrowGlyphWithFrame.txt``() =
    Assert.AreEqual(ArrowGlyphWithFrame_txt.leftOffsetExpected, ArrowGlyphWithFrame_txt.leftOffsetResult)
  [<Test>]
  let ``TxtFile : leftOffset : ZeroMQ_Fig1.txt``() =
    Assert.AreEqual(ZeroMQ_Fig1_txt.leftOffsetExpected, ZeroMQ_Fig1_txt.leftOffsetResult)

  [<Test>]
  let ``TxtFile : trimWithOffset : ArrowGlyph_txt``() =
    Assert.AreEqual(ArrowGlyph_txt.trimWithOffsetExpected, ArrowGlyph_txt.trimWithOffsetResult)

  [<Test>]
  let ``TxtFile : splitText : TestLogo.txt``() =
    Assert.True(TestLogo_txt.splitTxtResult = TestLogo_txt.splitTxtResultExpected)
  [<Test>]
  let ``TxtFile : splitText : ArrowGlyph_txt``() =
    Assert.True(ArrowGlyph_txt.splitTxtResultOk)
  [<Test>]
  let ``TxtFile : splitText : ArrowGlyphWithFrame_txt``() =
    Assert.True(ArrowGlyphWithFrame_txt.splitTxtResultOk)
  [<Test>]
  let ``TxtFile : splitText : TestPolygonBox_txt``() =
    Assert.True(TestPolygonBox_txt.splitTxtResultOk)
  [<Test>]
  let ``TxtFile : splitText : TestMiniBox_txt``() =
    Assert.True(TestMiniBox_txt.splitTxtResultOk)
  [<Test>]
  let ``TxtFile : splitText : ZeroMQ_Fig1_txt``() =
    Assert.True(ZeroMQ_Fig1_txt.splitTxtResultOk)


  [<Test>]
  let ``TxtFile : makeFramedGrid : TestLogo.txt`` () =
    Assert.AreEqual (TestLogo_txt.makeFramedGridResultExpected, TestLogo_txt.makeFramedGridResult)

  [<Test>]
  let ``TxtFile : makeTrimmedGrid : ArrowGlyph_txt`` () =
    Assert.AreEqual (ArrowGlyph_txt.makeGridResultExpected, ArrowGlyph_txt.makeGridResult)
  [<Test>]
  let ``TxtFile : makeTrimmedGrid : ArrowGlyphWithFrame_txt`` () =
    Assert.AreEqual (ArrowGlyphWithFrame_txt.makeGridResultExpected, ArrowGlyphWithFrame_txt.makeGridResult)
  [<Test>]
  let ``TxtFile : makeTrimmedGrid : TestPolygonBox_txt`` () =
    Assert.AreEqual (TestPolygonBox_txt.makeGridResultExpected, TestPolygonBox_txt.makeGridResult)
  [<Test>]
  let ``TxtFile : makeTrimmedGrid : TestMiniBox_txt`` () =
    Assert.AreEqual (TestMiniBox_txt.makeGridResultExpected, TestMiniBox_txt.makeGridResult)
  [<Test>]
  let ``TxtFile : makeTrimmedGrid : ZeroMQ_Fig1_txt`` () =
    Assert.AreEqual (ZeroMQ_Fig1_txt.makeGridResultExpected, ZeroMQ_Fig1_txt.makeGridResult)

  [<Test>]
  let ``TxtFile : replaceOptionInLine : TestLogo.txt`` () =
    Assert.AreEqual (TestLogo_txt.replaceOptionInLineExpected, TestLogo_txt.replaceOptionInLineResult)

  [<Test>]
  let ``TxtFile : replaceOptionInAscii : TestLogo.txt`` () =
    Assert.True (TestLogo_txt.replaceOptionInAsciiOK)

  // #endregion

  // #region AsciiToSvg.GlyphScanner.Tests

  [<Test>]
  let ``GlyphScanner : ScanGlyphs : ArrowGlyph_txt``() =
    Assert.AreEqual ([|0; 1; 2; 3; 4; 5; 6; 7|], ArrowGlyph_txt.scanGridResultMapped)
  [<Test>]
  let ``GlyphScanner : ScanGlyphs : ArrowGlyphWithFrame_txt``() =
    [|0; 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12; 13; 14; 15; 16; 17; 18; 19; 20; 21; 22|]
    |> fun x -> Assert.AreEqual(ArrowGlyphWithFrame_txt.scanGridResultMapped, x)
  [<Test>]
  let ``GlyphScanner : ScanGlyphs : TestPolygonBox_txt``() =
    [|0; 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12; 13; 14; 15|]
    |> fun x -> Assert.AreEqual(TestPolygonBox_txt.scanGridResultMapped, x)
  [<Test>]
  let ``GlyphScanner : ScanGlyphs : TestMiniBox_txt``() =
    Assert.AreEqual ([|0; 1; 2; 3|], TestMiniBox_txt.scanGridResultMapped)
  [<Test>]
  let ``GlyphScanner : ScanGlyphs : ZeroMQ_Fig1_txt``() =
    [|0; 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12; 13; 14;|]
    |> fun x -> Assert.AreEqual(ZeroMQ_Fig1_txt.scanGridResultMapped, x)

  // #endregion

  // #region  AsciiToSvg.GlyphRenderer.Tests

  [<Test>]
  let ``GlyphRenderer : Render : ArrowGlyph_txt``() =
    Assert.AreEqual (ArrowGlyph_txt.renderResultExpected, ArrowGlyph_txt.renderResult)
    Assert.AreEqual(ArrowGlyph_txt.arrowGlyphsWithoutTextAsSvgExpected, ArrowGlyph_txt.arrowGlyphsWithoutTextAsSvg)
  [<Test>]
  let ``GlyphRenderer : Render : ArrowGlyphWithFrame_txt``() =
    Assert.AreEqual(ArrowGlyphWithFrame_txt.arrowGlyphsFramedWithoutTextAsSvgExpected, ArrowGlyphWithFrame_txt.arrowGlyphsFramedWithoutTextAsSvg)
  [<Test>]
  let ``GlyphRenderer : Render : TestPolygonBox_txt``() =
    Assert.AreEqual(TestPolygonBox_txt.testPolygonBoxGlyphsOnlyAsSvgExpected, TestPolygonBox_txt.testPolygonBoxGlyphsOnlyAsSvg)
  [<Test>]
  let ``GlyphRenderer : Render : TestMiniBox_txt``() =
    Assert.AreEqual(TestMiniBox_txt.testMiniBoxGlyphsOnlyAsSvgExpected, TestMiniBox_txt.testMiniBoxGlyphsOnlyAsSvg)
  [<Test>]
  let ``GlyphRenderer : Render : ZeroMQ_Fig1_txt``() =
    Assert.AreEqual(ZeroMQ_Fig1_txt.zeroMQ_Fig1GlyphsOnlyAsSvgExpected, ZeroMQ_Fig1_txt.zeroMQ_Fig1GlyphsOnlyAsSvg)

  // #endregion

  // #region  AsciiToSvg.TextScanner.Tests

  [<Test>]
  let ``TextScanner : ScanText : ArrowGlyph_txt``() =
    Assert.AreEqual(ArrowGlyph_txt.textExpected, ArrowGlyph_txt.text)

  [<Test>]
  let ``TextScanner : ScanTabbedText : ArrowGlyph_txt``() =
    Assert.AreEqual(ArrowGlyph_txt.textTabbedExpected, ArrowGlyph_txt.textTabbed)
  [<Test>]
  let ``TextScanner : ScanTabbedText : ArrowGlyphWithFrame_txt``() =
    Assert.AreEqual(ArrowGlyphWithFrame_txt.textTabbedExpected, ArrowGlyphWithFrame_txt.textTabbed)
  [<Test>]
  let ``TextScanner : ScanTabbedText : ZeroMQ_Fig1_txt``() =
    Assert.AreEqual(ZeroMQ_Fig1_txt.textTabbedExpected, ZeroMQ_Fig1_txt.textTabbed)

  // #endregion

  // #region  AsciiToSvg.TextRenderer.Tests

  [<Test>]
  let ``TextRenderer : RenderAll : ArrowGlyph_txt``() =
    Assert.AreEqual(ArrowGlyph_txt.arrowGlyphsWithoutLinesAsSvgExpected, ArrowGlyph_txt.arrowGlyphsWithoutLinesAsSvg)
  [<Test>]
  let ``TextRenderer : RenderAll : ArrowGlyphWithFrame_txt``() =
    Assert.AreEqual(ArrowGlyphWithFrame_txt.arrowGlyphsFramedTabbedAsSvgExpected, ArrowGlyphWithFrame_txt.arrowGlyphsFramedTabbedAsSvg)
  [<Test>]
  let ``TextRenderer : RenderAll : ZeroMQ_Fig1_txt``() =
    Assert.AreEqual(ZeroMQ_Fig1_txt.zeroMQ_Fig1TabbedAsSvgExpected, ZeroMQ_Fig1_txt.zeroMQ_Fig1TabbedAsSvg)

  // #endregion

  // #region  AsciiToSvg.LineScanner.Tests

  [<Test>]
  let ``LineScanner : ScanLine : ArrowGlyph_txt`` () =
     Assert.True(ArrowGlyph_txt.lineScanResult)
  [<Test>]
  let ``LineScanner : ScanLine : ArrowGlyphWithFrame_txt`` () =
    Assert.True(ArrowGlyphWithFrame_txt.lineScanResult)
  [<Test>]
  let ``LineScanner : ScanLine : TestPolygonBox_txt`` () =
    Assert.True(TestPolygonBox_txt.lineScanResult)
  [<Test>]
  let ``LineScanner : ScanLine : TestMiniBox_txt`` () =
    Assert.True(TestMiniBox_txt.lineScanResult)
  [<Test>]
  let ``LineScanner : ScanLine : ZeroMQ_Fig1_txt`` () =
    Assert.True(ZeroMQ_Fig1_txt.lineScanResult)

  // #endregion

  // #region  AsciiToSvg.LineRenderer.Tests

  [<Test>]
  let ``LineRenderer : RenderAll : ArrowGlyph_txt`` () =
    Assert.AreEqual(ArrowGlyph_txt.arrowGlyphsSvgExpected, ArrowGlyph_txt.arrowGlyphsAsSvg)
  [<Test>]
  let ``LineRenderer : RenderAll : ArrowGlyphWithFrame_txt`` () =
    Assert.AreEqual(ArrowGlyphWithFrame_txt.arrowGlyphsWithFrameAsSvgExpected, ArrowGlyphWithFrame_txt.arrowGlyphsWithFrameAsSvg)
  [<Test>]
  let ``LineRenderer : RenderAll : TestPolygonBox_txt`` () =
    Assert.AreEqual(TestPolygonBox_txt.testPolygonBoxAsSvgExpected, TestPolygonBox_txt.testPolygonBoxAsSvg)
  [<Test>]
  let ``LineRenderer : RenderAll : TestMiniBox_txt`` () =
    Assert.AreEqual(TestMiniBox_txt.testMiniBoxAsSvgExpected, TestMiniBox_txt.testMiniBoxAsSvg)
  [<Test>]
  let ``LineRenderer : RenderAll : ZeroMQ_Fig1_txt`` () =
    Assert.AreEqual(ZeroMQ_Fig1_txt.zeroMQ_Fig1AsSvgExpected, ZeroMQ_Fig1_txt.zeroMQ_Fig1AsSvg)

  // #endregion

  sprintf "Test run finished at %A" (DateTime.Now.ToLocalTime())
  |> printfn "%s"
