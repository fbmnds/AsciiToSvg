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
  open AsciiToSvg.Tests.TopologyTests

  // #region AsciiToSvg.Common.Tests

  [<Test; Category "AsciiToSvg.Common">]
  let ``Common : matchPositions : TestLogo.txt`` () =
    Assert.AreEqual (TestLogo_txt.matchPositionsExpected, TestLogo_txt.matchPositionsResult)

  // #endregion

    // #region AsciiToSvg.Common.Tests

  [<Test; Category "AsciiToSvg.Common">]
  let ``Json : parse : TestLogo.txt`` () =
    Assert.AreEqual (TestLogo_txt.parseFailureResultExpected, TestLogo_txt.parseFailureResult)
    Assert.AreEqual (TestLogo_txt.parseSuccessResultExpected, TestLogo_txt.parseSuccessResult)

  // #endregion

  // #region AsciiToSvg.SvgDocument.Tests

  [<Test; Category "AsciiToSvg.SvgDocument">]
  let ``SvgDocument : ConvertCoordGridToSvg`` () =
    Assert.AreEqual(Tests.SvgDocument.svgCoordExpected, Tests.SvgDocument.svgCoord)
  [<Test; Category "AsciiToSvg.SvgDocument">]
  let ``SvgDocument : ShiftedCoordGridToSvg`` () =
    Assert.AreEqual(Tests.SvgDocument.shiftedCoordExpected, Tests.SvgDocument.shiftedCoord)
  [<Test; Category "AsciiToSvg.SvgDocument">]
  let ``SvgDocument : shiftColCoordGridToSvg`` () =
    Assert.AreEqual(Tests.SvgDocument.colsExpected, Tests.SvgDocument.cols)
  [<Test; Category "AsciiToSvg.SvgDocument">]
  let ``SvgDocument : shiftRowCoordGridToSvg`` () =
    Assert.AreEqual(Tests.SvgDocument.rowsExpected, Tests.SvgDocument.rows)

  // #endregion

  // #region AsciiToSvg.TxtFile.Tests

  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : leftOffset : TestLogo.txt``() =
    Assert.AreEqual(TestLogo_txt.leftOffsetExpected, TestLogo_txt.leftOffsetResult)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : leftOffset : ArrowGlyph.txt``() =
    Assert.AreEqual(ArrowGlyph_txt.leftOffsetExpected, ArrowGlyph_txt.leftOffsetResult)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : leftOffset : ArrowGlyphWithFrame.txt``() =
    Assert.AreEqual(ArrowGlyphWithFrame_txt.leftOffsetExpected, ArrowGlyphWithFrame_txt.leftOffsetResult)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : leftOffset : ZeroMQ_Fig1.txt``() =
    Assert.AreEqual(ZeroMQ_Fig1_txt.leftOffsetExpected, ZeroMQ_Fig1_txt.leftOffsetResult)

  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : trimWithOffset : ArrowGlyph_txt``() =
    Assert.AreEqual(ArrowGlyph_txt.trimWithOffsetExpected, ArrowGlyph_txt.trimWithOffsetResult)

  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : splitText : TestLogo.txt``() =
    Assert.True(TestLogo_txt.splitTxtResult = TestLogo_txt.splitTxtResultExpected)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : splitText : ArrowGlyph_txt``() =
    Assert.True(ArrowGlyph_txt.splitTxtResultOk)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : splitText : ArrowGlyphWithFrame_txt``() =
    Assert.True(ArrowGlyphWithFrame_txt.splitTxtResultOk)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : splitText : TestPolygonBox_txt``() =
    Assert.True(TestPolygonBox_txt.splitTxtResultOk)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : splitText : TestMiniBox_txt``() =
    Assert.True(TestMiniBox_txt.splitTxtResultOk)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : splitText : ZeroMQ_Fig1_txt``() =
    Assert.True(ZeroMQ_Fig1_txt.splitTxtResultOk)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : splitText : TestBoxes.txt``() =
    Assert.True(TestBoxes_txt.splitTxtResult = TestBoxes_txt.splitTxtResultExpected)

  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : makeFramedGrid : TestLogo.txt`` () =
    Assert.AreEqual (TestLogo_txt.makeFramedGridResultExpected, TestLogo_txt.makeFramedGridResult)

  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : makeTrimmedGrid : ArrowGlyph_txt`` () =
    Assert.AreEqual (ArrowGlyph_txt.makeGridResultExpected, ArrowGlyph_txt.makeGridResult)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : makeTrimmedGrid : ArrowGlyphWithFrame_txt`` () =
    Assert.AreEqual (ArrowGlyphWithFrame_txt.makeGridResultExpected, ArrowGlyphWithFrame_txt.makeGridResult)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : makeTrimmedGrid : TestPolygonBox_txt`` () =
    Assert.AreEqual (TestPolygonBox_txt.makeGridResultExpected, TestPolygonBox_txt.makeGridResult)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : makeTrimmedGrid : TestMiniBox_txt`` () =
    Assert.AreEqual (TestMiniBox_txt.makeGridResultExpected, TestMiniBox_txt.makeGridResult)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : makeTrimmedGrid : ZeroMQ_Fig1_txt`` () =
    Assert.AreEqual (ZeroMQ_Fig1_txt.makeGridResultExpected, ZeroMQ_Fig1_txt.makeGridResult)
  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : makeTrimmedGrid : TestBoxes_txt`` () =
    Assert.AreEqual (TestBoxes_txt.makeGridExpected, TestBoxes_txt.makeGridResult)

  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : replaceOptionInLine : TestLogo.txt`` () =
    Assert.AreEqual (TestLogo_txt.replaceOptionInLineExpected, TestLogo_txt.replaceOptionInLineResult)

  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : replaceOptionInAscii : TestLogo.txt`` () =
    Assert.True (TestLogo_txt.replaceOptionInAsciiOK)

  [<Test; Category "AsciiToSvg.TxtFile">]
  let ``TxtFile : parseAllOptions : TestLogo.TxtFile`` () =
    Assert.AreEqual (TestLogo_txt.parseAllOptionsExpected, TestLogo_txt.parseAllOptionsResult)

  // #endregion

  // #region AsciiToSvg.GlyphScanner.Tests

  [<Test; Category "AsciiToSvg.GlyphScanner">]
  let ``GlyphScanner : ScanGlyphs : ArrowGlyph_txt``() =
    Assert.AreEqual ([|0; 1; 2; 3; 4; 5; 6; 7|], ArrowGlyph_txt.scanGridResultMapped)
  [<Test; Category "AsciiToSvg.GlyphScanner">]
  let ``GlyphScanner : ScanGlyphs : ArrowGlyphWithFrame_txt``() =
    [|0; 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12; 13; 14; 15; 16; 17; 18; 19; 20; 21; 22|]
    |> fun x -> Assert.AreEqual(ArrowGlyphWithFrame_txt.scanGridResultMapped, x)
  [<Test; Category "AsciiToSvg.GlyphScanner">]
  let ``GlyphScanner : ScanGlyphs : TestPolygonBox_txt``() =
    [|0; 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12; 13; 14; 15|]
    |> fun x -> Assert.AreEqual(TestPolygonBox_txt.scanGridResultMapped, x)
  [<Test; Category "AsciiToSvg.GlyphScanner">]
  let ``GlyphScanner : ScanGlyphs : TestMiniBox_txt``() =
    Assert.AreEqual ([|0; 1; 2; 3|], TestMiniBox_txt.scanGridResultMapped)
  [<Test; Category "AsciiToSvg.GlyphScanner">]
  let ``GlyphScanner : ScanGlyphs : ZeroMQ_Fig1_txt``() =
    [|0; 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12; 13; 14;|]
    |> fun x -> Assert.AreEqual(ZeroMQ_Fig1_txt.scanGridResultMapped, x)
  [<Test; Category "AsciiToSvg.GlyphScanner">]
  let ``GlyphScanner : ScanGlyphs : TestBoxes_txt``() =
    [|0; 1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12; 13; 14; 15; 16; 17; 18; 19; 20; 21; 22; 23; 24; 25; 26; 27; 28; 29|]
    |> fun x -> Assert.AreEqual(TestBoxes_txt.scanGridResultMapped, x)

  // #endregion

  // #region  AsciiToSvg.GlyphRenderer.Tests

  [<Test; Category "AsciiToSvg.GlyphRenderer">]
  let ``GlyphRenderer : Render : ArrowGlyph_txt``() =
    Assert.AreEqual (ArrowGlyph_txt.renderResultExpected, ArrowGlyph_txt.renderResult)
    Assert.AreEqual(ArrowGlyph_txt.arrowGlyphsWithoutTextAsSvgExpected, ArrowGlyph_txt.arrowGlyphsWithoutTextAsSvg)
  [<Test; Category "AsciiToSvg.GlyphRenderer">]
  let ``GlyphRenderer : Render : ArrowGlyphWithFrame_txt``() =
    Assert.AreEqual(ArrowGlyphWithFrame_txt.arrowGlyphsFramedWithoutTextAsSvgExpected, ArrowGlyphWithFrame_txt.arrowGlyphsFramedWithoutTextAsSvg)
  [<Test; Category "AsciiToSvg.GlyphRenderer">]
  let ``GlyphRenderer : Render : TestPolygonBox_txt``() =
    Assert.AreEqual(TestPolygonBox_txt.testPolygonBoxGlyphsOnlyAsSvgExpected, TestPolygonBox_txt.testPolygonBoxGlyphsOnlyAsSvg)
  [<Test; Category "AsciiToSvg.GlyphRenderer">]
  let ``GlyphRenderer : Render : TestMiniBox_txt``() =
    Assert.AreEqual(TestMiniBox_txt.testMiniBoxGlyphsOnlyAsSvgExpected, TestMiniBox_txt.testMiniBoxGlyphsOnlyAsSvg)
  [<Test; Category "AsciiToSvg.GlyphRenderer">]
  let ``GlyphRenderer : Render : ZeroMQ_Fig1_txt``() =
    Assert.AreEqual(ZeroMQ_Fig1_txt.zeroMQ_Fig1GlyphsOnlyAsSvgExpected, ZeroMQ_Fig1_txt.zeroMQ_Fig1GlyphsOnlyAsSvg)

  // #endregion

  // #region  AsciiToSvg.TextScanner.Tests

  [<Test; Category "AsciiToSvg.TextScanner">]
  let ``TextScanner : ScanText : ArrowGlyph_txt``() =
    Assert.AreEqual(ArrowGlyph_txt.textExpected, ArrowGlyph_txt.text)

  [<Test; Category "AsciiToSvg.TextScanner">]
  let ``TextScanner : ScanTabbedText : ArrowGlyph_txt``() =
    Assert.AreEqual(ArrowGlyph_txt.textTabbedExpected, ArrowGlyph_txt.textTabbed)
  [<Test; Category "AsciiToSvg.TextScanner">]
  let ``TextScanner : ScanTabbedText : ArrowGlyphWithFrame_txt``() =
    Assert.AreEqual(ArrowGlyphWithFrame_txt.textTabbedExpected, ArrowGlyphWithFrame_txt.textTabbed)
  [<Test; Category "AsciiToSvg.TextScanner">]
  let ``TextScanner : ScanTabbedText : ZeroMQ_Fig1_txt``() =
    Assert.AreEqual(ZeroMQ_Fig1_txt.textTabbedExpected, ZeroMQ_Fig1_txt.textTabbed)
  [<Test; Category "AsciiToSvg.TextScanner">]
  let ``TextScanner : ScanTabbedText : TestBoxes_txt`` () =
    Assert.AreEqual(TestBoxes_txt.textTabbedExpected, TestBoxes_txt.textTabbedResult)

  // #endregion

  // #region  AsciiToSvg.TextRenderer.Tests

  [<Test; Category "AsciiToSvg.TextRenderer">]
  let ``TextRenderer : RenderAll : ArrowGlyph_txt``() =
    Assert.AreEqual(ArrowGlyph_txt.arrowGlyphsWithoutLinesAsSvgExpected, ArrowGlyph_txt.arrowGlyphsWithoutLinesAsSvg)
  [<Test; Category "AsciiToSvg.TextRenderer">]
  let ``TextRenderer : RenderAll : ArrowGlyphWithFrame_txt``() =
    Assert.AreEqual(ArrowGlyphWithFrame_txt.arrowGlyphsFramedTabbedAsSvgExpected, ArrowGlyphWithFrame_txt.arrowGlyphsFramedTabbedAsSvg)
  [<Test; Category "AsciiToSvg.TextRenderer">]
  let ``TextRenderer : RenderAll : ZeroMQ_Fig1_txt``() =
    Assert.AreEqual(ZeroMQ_Fig1_txt.zeroMQ_Fig1TabbedAsSvgExpected, ZeroMQ_Fig1_txt.zeroMQ_Fig1TabbedAsSvg)

  // #endregion

  // #region  AsciiToSvg.LineScanner.Tests

  [<Test; Category "AsciiToSvg.LineScanner">]
  let ``LineScanner : ScanLine : ArrowGlyph_txt`` () =
     Assert.True(ArrowGlyph_txt.lineScanResult)
  [<Test; Category "AsciiToSvg.LineScanner">]
  let ``LineScanner : ScanLine : ArrowGlyphWithFrame_txt`` () =
    Assert.True(ArrowGlyphWithFrame_txt.lineScanResult)
  [<Test; Category "AsciiToSvg.LineScanner">]
  let ``LineScanner : ScanLine : TestPolygonBox_txt`` () =
    Assert.True(TestPolygonBox_txt.lineScanResult)
  [<Test; Category "AsciiToSvg.LineScanner">]
  let ``LineScanner : ScanLine : TestMiniBox_txt`` () =
    Assert.True(TestMiniBox_txt.lineScanResult)
  [<Test; Category "AsciiToSvg.LineScanner">]
  let ``LineScanner : ScanLine : ZeroMQ_Fig1_txt`` () =
    Assert.True(ZeroMQ_Fig1_txt.lineScanResult)
  [<Test; Category "AsciiToSvg.LineScanner">]
  let ``LineScanner : ScanLine : TestBoxes_txt`` () =
    Assert.True(TestBoxes_txt.lineScanResult)

  // #endregion

  // #region  AsciiToSvg.LineRenderer.Tests

  [<Test; Category "AsciiToSvg.LineRenderer">]
  let ``LineRenderer : RenderAll : ArrowGlyph_txt`` () =
    Assert.AreEqual(ArrowGlyph_txt.arrowGlyphsSvgExpected, ArrowGlyph_txt.arrowGlyphsAsSvg)
  [<Test; Category "AsciiToSvg.LineRenderer">]
  let ``LineRenderer : RenderAll : ArrowGlyphWithFrame_txt`` () =
    Assert.AreEqual(ArrowGlyphWithFrame_txt.arrowGlyphsWithFrameAsSvgExpected, ArrowGlyphWithFrame_txt.arrowGlyphsWithFrameAsSvg)
  [<Test; Category "AsciiToSvg.LineRenderer">]
  let ``LineRenderer : RenderAll : TestPolygonBox_txt`` () =
    Assert.AreEqual(TestPolygonBox_txt.testPolygonBoxAsSvgExpected, TestPolygonBox_txt.testPolygonBoxAsSvg)
  [<Test; Category "AsciiToSvg.LineRenderer">]
  let ``LineRenderer : RenderAll : TestMiniBox_txt`` () =
    Assert.AreEqual(TestMiniBox_txt.testMiniBoxAsSvgExpected, TestMiniBox_txt.testMiniBoxAsSvg)
  [<Test; Category "AsciiToSvg.LineRenderer">]
  let ``LineRenderer : RenderAll : ZeroMQ_Fig1_txt`` () =
    Assert.AreEqual(ZeroMQ_Fig1_txt.zeroMQ_Fig1AsSvgExpected, ZeroMQ_Fig1_txt.zeroMQ_Fig1AsSvg)

  // #endregion

  // #region  AsciiToSvg.Topology.Tests
  
  [<Test; Category "AsciiToSvg.Topology">]
  let ``Topology : IsUpperLeftCornerKind : TestBoxes_txt`` () =
    Assert.AreEqual(TestBoxes_txt.upperLeftCornersExpected, TestBoxes_txt.upperLeftCorners)

  [<Test; Category "AsciiToSvg.Topology">]
  let ``Topology : IsUpperRightCornerKind : TestBoxes_txt`` () =
    Assert.AreEqual(TestBoxes_txt.upperRightCornersExpected, TestBoxes_txt.upperRightCorners)

  [<Test; Category "AsciiToSvg.Topology">]
  let ``Topology : IsLowerLeftCornerKind : TestBoxes_txt`` () =
    Assert.AreEqual(TestBoxes_txt.lowerLeftCornersExpected, TestBoxes_txt.lowerLeftCorners)

  [<Test; Category "AsciiToSvg.Topology">]
  let ``Topology : IsLowerRightCornerKind : TestBoxes_txt`` () =
    Assert.AreEqual(TestBoxes_txt.lowerRightCornersExpected, TestBoxes_txt.lowerRightCorners)

  [<Test; Category "AsciiToSvg.Topology">]
  let ``Topology : findBoxCorners : TestBoxes_txt`` () =
    Assert.AreEqual(TestBoxes_txt.findBoxCornersExpected, TestBoxes_txt.findBoxCornersResult)

  [<Test; Category "AsciiToSvg.Topology">]
  let ``Topology : flipLine : TestBoxes_txt`` () =
    Assert.True(TestBoxes_txt.leftFlipLineOK)

  [<Test; Category "AsciiToSvg.Topology">]
  let ``Topology : findHorizontalPathBetween : TestBoxes_txt`` () =
    Assert.True(TestBoxes_txt.hUL1OK)
    Assert.True(TestBoxes_txt.hUL2OK)
    Assert.True(TestBoxes_txt.hLL1OK)

  [<Test; Category "AsciiToSvg.Topology">]
  let ``Topology : findVerticalPathBetween : TestBoxes_txt`` () =
    Assert.True(TestBoxes_txt.vLL1OK)

  // #endregion

  Log.logInfo "Test run finished at %A" (DateTime.Now.ToLocalTime())

