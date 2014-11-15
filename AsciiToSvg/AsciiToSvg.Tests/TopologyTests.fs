namespace AsciiToSvg.Tests 


module TopologyTests = 

  open AsciiToSvg 
  open AsciiToSvg.Topology
  open AsciiToSvg.Topology.Operators
  open AsciiToSvg.Tests.GlyphScanner 
  open AsciiToSvg.Tests.LineScanner 

  module TestBoxes_txt = 

    let horizLines, vertLines = TestBoxes_txt.allLines 
    let glyphs = TestBoxes_txt.scanGridResult 
    let upperLeftCorners = glyphs |> Array.filter IsUpperLeftCornerKind 
    let upperRightCorners = glyphs |> Array.filter IsUpperRightCornerKind 
    let lowerRightCorners = glyphs |> Array.filter IsLowerRightCornerKind 
    let lowerLeftCorners = glyphs |> Array.filter IsLowerLeftCornerKind 
    let upperLeftCornersExpected = 
      [|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 0; row = 0;}; glyphOptions = Map.empty } 
        { glyphKind = RoundUpperLeftCorner; gridCoord = {col = 2; row = 2;}; glyphOptions = Map.empty } 
        { glyphKind = RoundUpperLeftCorner; gridCoord = {col = 10; row = 2;}; glyphOptions = Map.empty } 
        { glyphKind = RoundUpperLeftCorner; gridCoord = {col = 18; row = 2;}; glyphOptions = Map.empty } 
        { glyphKind = RoundUpperLeftCorner; gridCoord = {col = 4; row = 3;}; glyphOptions = Map.empty }
        { glyphKind = UpperAndLowerLeftCorner; gridCoord = {col = 10; row = 3;}; glyphOptions = Map.empty }
        { glyphKind = UpperAndLowerLeftCorner; gridCoord = {col = 18; row = 4;}; glyphOptions = Map.empty }|] 
    let upperRightCornersExpected = 
      [|{ glyphKind = RoundUpperRightCorner; gridCoord = {col = 26; row = 0;}; glyphOptions = Map.empty } 
        { glyphKind = RoundUpperRightCorner; gridCoord = {col = 8; row = 2;}; glyphOptions = Map.empty } 
        { glyphKind = RoundUpperRightCorner; gridCoord = {col = 16; row = 2;}; glyphOptions = Map.empty } 
        { glyphKind = RoundUpperRightCorner; gridCoord = {col = 24; row = 2;}; glyphOptions = Map.empty } 
        { glyphKind = RoundUpperRightCorner; gridCoord = {col = 6; row = 3;}; glyphOptions = Map.empty } 
        { glyphKind = UpperAndLowerRightCorner; gridCoord = {col = 24; row = 3;}; glyphOptions = Map.empty }
        { glyphKind = UpperAndLowerRightCorner; gridCoord = {col = 16; row = 4;}; glyphOptions = Map.empty }|] 
    let lowerRightCornersExpected = 
      [|{ glyphKind = UpperAndLowerRightCorner; gridCoord = {col = 24; row = 3;}; glyphOptions = Map.empty }
        { glyphKind = RoundLowerRightCorner; gridCoord = {col = 6; row = 4;}; glyphOptions = Map.empty } 
        { glyphKind = UpperAndLowerRightCorner; gridCoord = {col = 16; row = 4;}; glyphOptions = Map.empty } 
        { glyphKind = RoundLowerRightCorner; gridCoord = {col = 8; row = 5;}; glyphOptions = Map.empty } 
        { glyphKind = RoundLowerRightCorner; gridCoord = {col = 16; row = 5;}; glyphOptions = Map.empty } 
        { glyphKind = RoundLowerRightCorner; gridCoord = {col = 24; row = 5;}; glyphOptions = Map.empty } 
        { glyphKind = RoundLowerRightCorner; gridCoord = {col = 26; row = 8;}; glyphOptions = Map.empty }|] 
    let lowerLeftCornersExpected = 
      [|{ glyphKind = UpperAndLowerLeftCorner; gridCoord = {col = 10; row = 3;}; glyphOptions = Map.empty }
        { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 4; row = 4;}; glyphOptions = Map.empty }
        { glyphKind = UpperAndLowerLeftCorner; gridCoord = {col = 18; row = 4;}; glyphOptions = Map.empty }
        { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 2; row = 5;}; glyphOptions = Map.empty } 
        { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 10; row = 5;}; glyphOptions = Map.empty } 
        { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 18; row = 5;}; glyphOptions = Map.empty } 
        { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 0; row = 8;}; glyphOptions = Map.empty }|] 

    let findBoxCornersExpected = [|upperLeftCorners; upperRightCorners; lowerRightCorners; lowerLeftCorners|] 
    let findBoxCornersResult = glyphs |> findBoxCorners 

    let leftFlipLineOK = 
      let hl = { orientation = Horizontal; gridCoordStart = {col = 19; row = 4;}; gridCoordEnd = {col = 20; row = 4;}; linechars = [|'-'; '-'|]; lineOptions = Map.empty } 
      let hl' = { orientation = Horizontal; gridCoordStart = {col = 20; row = 4;}; gridCoordEnd = {col = 19; row = 4;}; linechars = [|'-'; '-'|]; lineOptions = Map.empty } 
      let vl = { orientation = Vertical; gridCoordStart = {col = 8; row = 3;}; gridCoordEnd = {col = 8; row = 4;}; linechars = [|'|'; '|'|]; lineOptions = Map.empty } 
      let vl' = { orientation = Vertical; gridCoordStart = {col = 8; row = 4;}; gridCoordEnd = {col = 8; row = 3;}; linechars = [|'|'; '|'|]; lineOptions = Map.empty } 
      (hl = flipLine hl) && 
      (hl = flipLine hl') && 
      (vl = flipLine vl) && 
      (vl = flipLine vl') 

    let findBoxesResult = FindBoxes horizLines vertLines upperLeftCorners upperRightCorners lowerRightCorners lowerLeftCorners 
    let findBoxesExpected = 
      [|({ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 0; row = 0;}; glyphOptions = Map.empty }, 
         { orientation = Horizontal; gridCoordStart = {col = 1; row = 0;}; gridCoordEnd = {col = 25; row = 0;}; linechars = [|'-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }, 
         { glyphKind = RoundUpperRightCorner; gridCoord = {col = 26; row = 0;}; glyphOptions = Map.empty }, 
         { orientation = Vertical; gridCoordStart = {col = 26; row = 1;}; gridCoordEnd = {col = 26; row = 7;}; linechars = [|'|'; '|'; '|'; '|'; '|'; '|'; '|'|]; lineOptions = Map.empty }, 
         { glyphKind = RoundLowerRightCorner; gridCoord = {col = 26; row = 8;}; glyphOptions = Map.empty }, 
         { orientation = Horizontal; gridCoordStart = {col = 1; row = 8;}; gridCoordEnd = {col = 25; row = 8;}; linechars = [|'-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }, 
         { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 0; row = 8;}; glyphOptions = Map.empty }, 
         { orientation = Vertical; gridCoordStart = {col = 0; row = 1;}; gridCoordEnd = {col = 0; row = 7;}; linechars = [|'|'; '|'; '|'; '|'; '|'; '|'; '|'|]; lineOptions = Map.empty }); 
        ({ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 2; row = 2;}; glyphOptions = Map.empty }, 
         { orientation = Horizontal; gridCoordStart = {col = 3; row = 2;}; gridCoordEnd = {col = 7; row = 2;}; linechars = [|'-'; '-'; '-'; '.'; '-'|]; lineOptions = Map.empty }, 
         { glyphKind = RoundUpperRightCorner; gridCoord = {col = 8; row = 2;}; glyphOptions = Map.empty }, 
         { orientation = Vertical; gridCoordStart = {col = 8; row = 3;}; gridCoordEnd = {col = 8; row = 4;}; linechars = [|'|'; '|'|]; lineOptions = Map.empty }, 
         { glyphKind = RoundLowerRightCorner; gridCoord = {col = 8; row = 5;}; glyphOptions = Map.empty }, 
         { orientation = Horizontal; gridCoordStart = {col = 3; row = 5;}; gridCoordEnd = {col = 7; row = 5;}; linechars = [|'-'; '-'; '-'; '\''; '-'|]; lineOptions = Map.empty }, 
         { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 2; row = 5;}; glyphOptions = Map.empty }, 
         { orientation = Vertical; gridCoordStart = {col = 2; row = 3;}; gridCoordEnd = {col = 2; row = 4;}; linechars = [|'|'; '|'|]; lineOptions = Map.empty })|] 

    let uLC1 = { glyphKind = RoundUpperLeftCorner; gridCoord = {col = 0; row = 0;}; glyphOptions = Map.empty }
    let uRC1 = { glyphKind = RoundUpperRightCorner; gridCoord = {col = 26; row = 0;}; glyphOptions = Map.empty }
    let hUL1 = findHorizontalPathBetween uRC1 uLC1 glyphs horizLines
    let hUL1Expected =
      [|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 0; row = 0;}; glyphOptions = Map.empty };
        { glyphKind = RoundUpperRightCorner; gridCoord = {col = 26; row = 0;}; glyphOptions = Map.empty }|],
      [|{ orientation = Horizontal; gridCoordStart = {col = 1; row = 0;}; gridCoordEnd = {col = 25; row = 0;}; linechars = [|'-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }|]
    let hUL1OK = hUL1 = hUL1Expected

    let uLC2 = { glyphKind = RoundUpperLeftCorner; gridCoord = {col = 2; row = 2;}; glyphOptions = Map.empty } 
    let uRC2 = { glyphKind = RoundUpperRightCorner; gridCoord = {col = 8; row = 2;}; glyphOptions = Map.empty } 
    let hUL2 = findHorizontalPathBetween uRC2 uLC2 glyphs horizLines
    let hUL2Expected =
      [|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 2; row = 2;}; glyphOptions = Map.empty };
        { glyphKind = RoundUpperRightCorner; gridCoord = {col = 8; row = 2;}; glyphOptions = Map.empty }|],
      [|{ orientation = Horizontal; gridCoordStart = {col = 3; row = 2;}; gridCoordEnd = {col = 7; row = 2;}; linechars = [|'-'; '-'; '-'; '.'; '-'|]; lineOptions = Map.empty }|]
    let hUL2OK = hUL2 = hUL2Expected

    let lLC1 = { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 2; row = 5;}; glyphOptions = Map.empty } 
    let lRC1 = { glyphKind = RoundLowerRightCorner; gridCoord = {col = 8; row = 5;}; glyphOptions = Map.empty }
    let hLL1 = findHorizontalPathBetween lRC1 lLC1 glyphs horizLines
    let hLL1Expected =
      [|{ glyphKind = RoundLowerLeftCorner; gridCoord = {col = 2; row = 5;}; glyphOptions = Map.empty };
        { glyphKind = RoundLowerRightCorner; gridCoord = {col = 8; row = 5;}; glyphOptions = Map.empty }|],
      [|{ orientation = Horizontal; gridCoordStart = {col = 3; row = 5;}; gridCoordEnd = {col = 7; row = 5;}; linechars = [|'-'; '-'; '-'; '\''; '-'|]; lineOptions = Map.empty }|]
    let hLL1OK = hLL1 = hLL1Expected
    
    let vLL1 = findVerticalPathBetween uRC2 lRC1 glyphs vertLines
    let vLL1Expected =
      [|{ glyphKind = RoundUpperRightCorner; gridCoord = {col = 8; row = 2;}; glyphOptions = Map.empty };
        { glyphKind = RoundLowerRightCorner; gridCoord = {col = 8; row = 5;}; glyphOptions = Map.empty }|],
      [|{ orientation = Vertical; gridCoordStart = {col = 8; row = 3;}; gridCoordEnd = {col = 8; row = 4;}; linechars = [|'|'; '|'|]; lineOptions = Map.empty }|]
    let vLL1OK = vLL1 = vLL1Expected

    let uLC3 = { glyphKind = RoundUpperLeftCorner; gridCoord = {col = 10; row = 2;}; glyphOptions = Map.empty }
    let lLC3 = { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 10; row = 5;}; glyphOptions = Map.empty } 
    let vLL2 = findVerticalPathBetween uLC3 lLC3 glyphs vertLines
    let vLL2Expected =
      [|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 10; row = 2;}; glyphOptions = Map.empty };
        { glyphKind = UpperAndLowerLeftCorner; gridCoord = {col = 10; row = 3;}; glyphOptions = Map.empty };
        { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 10; row = 5;}; glyphOptions = Map.empty }|], 
      [|{ orientation = Vertical; gridCoordStart = {col = 10; row = 4;}; gridCoordEnd = {col = 10; row = 4;}; linechars = [|'|'|]; lineOptions = Map.empty }|]
    let vLL2OK = vLL2 = vLL2Expected

    let findPathBoxesResult = FindPathBoxes TestBoxes_txt.allLines glyphs
    let findPathBoxesExpected =
      [|// Outer box
        (([|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 0; row = 0;}; glyphOptions = Map.empty };
            { glyphKind = RoundUpperRightCorner; gridCoord = {col = 26; row = 0;}; glyphOptions = Map.empty }|],
          [|{ orientation = Horizontal; gridCoordStart = {col = 1; row = 0;}; gridCoordEnd = {col = 25; row = 0;}; linechars = [|'-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundUpperRightCorner; gridCoord = {col = 26; row = 0;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerRightCorner; gridCoord = {col = 26; row = 8;}; glyphOptions = Map.empty }|],
          [|{ orientation = Vertical; gridCoordStart = {col = 26; row = 1;}; gridCoordEnd = {col = 26; row = 7;}; linechars = [|'|'; '|'; '|'; '|'; '|'; '|'; '|'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundLowerLeftCorner; gridCoord = {col = 0; row = 8;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerRightCorner; gridCoord = {col = 26; row = 8;}; glyphOptions = Map.empty }|],
          [|{ orientation = Horizontal; gridCoordStart = {col = 1; row = 8;}; gridCoordEnd = {col = 25; row = 8;}; linechars = [|'-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 0; row = 0;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 0; row = 8;}; glyphOptions = Map.empty }|],
          [|{ orientation = Vertical; gridCoordStart = {col = 0; row = 1;}; gridCoordEnd = {col = 0; row = 7;}; linechars = [|'|'; '|'; '|'; '|'; '|'; '|'; '|'|]; lineOptions = Map.empty }|])); 
        // Inner 'a' box
        (([|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 2; row = 2;}; glyphOptions = Map.empty };
            { glyphKind = RoundUpperRightCorner; gridCoord = {col = 8; row = 2;}; glyphOptions = Map.empty }|],
          [|{ orientation = Horizontal; gridCoordStart = {col = 3; row = 2;}; gridCoordEnd = {col = 7; row = 2;}; linechars = [|'-'; '-'; '-'; '.'; '-'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundUpperRightCorner; gridCoord = {col = 8; row = 2;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerRightCorner; gridCoord = {col = 8; row = 5;}; glyphOptions = Map.empty }|],
          [|{ orientation = Vertical; gridCoordStart = {col = 8; row = 3;}; gridCoordEnd = {col = 8; row = 4;}; linechars = [|'|'; '|'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundLowerLeftCorner; gridCoord = {col = 2; row = 5;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerRightCorner; gridCoord = {col = 8; row = 5;}; glyphOptions = Map.empty }|],
          [|{ orientation = Horizontal; gridCoordStart = {col = 3; row = 5;}; gridCoordEnd = {col = 7; row = 5;}; linechars = [|'-'; '-'; '-'; '\''; '-'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 2; row = 2;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 2; row = 5;}; glyphOptions = Map.empty }|],
          [|{ orientation = Vertical; gridCoordStart = {col = 2; row = 3;}; gridCoordEnd = {col = 2; row = 4;}; linechars = [|'|'; '|'|]; lineOptions = Map.empty }|]));
        // Inner '2' box
        (([|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 10; row = 2;}; glyphOptions = Map.empty };
            { glyphKind = RoundUpperRightCorner; gridCoord = {col = 16; row = 2;}; glyphOptions = Map.empty }|],
          [|{ orientation = Horizontal; gridCoordStart = {col = 11; row = 2;}; gridCoordEnd = {col = 15; row = 2;}; linechars = [|'-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundUpperRightCorner; gridCoord = {col = 16; row = 2;}; glyphOptions = Map.empty };
            { glyphKind = UpperAndLowerRightCorner; gridCoord = {col = 16; row = 4;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerRightCorner; gridCoord = {col = 16; row = 5;}; glyphOptions = Map.empty }|], 
          [|{ orientation = Vertical; gridCoordStart = {col = 16; row = 3;}; gridCoordEnd = {col = 16; row = 3;}; linechars = [|'|'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundLowerLeftCorner; gridCoord = {col = 10; row = 5;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerRightCorner; gridCoord = {col = 16; row = 5;}; glyphOptions = Map.empty }|],
          [|{ orientation = Horizontal; gridCoordStart = {col = 11; row = 5;}; gridCoordEnd = {col = 15; row = 5;}; linechars = [|'-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 10; row = 2;}; glyphOptions = Map.empty };
            { glyphKind = UpperAndLowerLeftCorner; gridCoord = {col = 10; row = 3;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 10; row = 5;}; glyphOptions = Map.empty }|], 
          [|{ orientation = Vertical; gridCoordStart = {col = 10; row = 4;}; gridCoordEnd = {col = 10; row = 4;}; linechars = [|'|'|]; lineOptions = Map.empty }|]));
        // Inner 's' box
        (([|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 18; row = 2;}; glyphOptions = Map.empty };
            { glyphKind = RoundUpperRightCorner; gridCoord = {col = 24; row = 2;}; glyphOptions = Map.empty }|],
          [|{ orientation = Horizontal; gridCoordStart = {col = 19; row = 2;}; gridCoordEnd = {col = 23; row = 2;}; linechars = [|'-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundUpperRightCorner; gridCoord = {col = 24; row = 2;}; glyphOptions = Map.empty };
            { glyphKind = UpperAndLowerRightCorner; gridCoord = {col = 24; row = 3;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerRightCorner; gridCoord = {col = 24; row = 5;}; glyphOptions = Map.empty }|],
          [|{ orientation = Vertical; gridCoordStart = {col = 24; row = 4;}; gridCoordEnd = {col = 24; row = 4;}; linechars = [|'|'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundLowerLeftCorner; gridCoord = {col = 18; row = 5;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerRightCorner; gridCoord = {col = 24; row = 5;}; glyphOptions = Map.empty }|],
          [|{ orientation = Horizontal; gridCoordStart = {col = 19; row = 5;}; gridCoordEnd = {col = 23; row = 5;}; linechars = [|'-'; '-'; '-'; '-'; '-'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 18; row = 2;}; glyphOptions = Map.empty };
            { glyphKind = UpperAndLowerLeftCorner; gridCoord = {col = 18; row = 4;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 18; row = 5;}; glyphOptions = Map.empty }|], 
          [|{ orientation = Vertical; gridCoordStart = {col = 18; row = 3;}; gridCoordEnd = {col = 18; row = 3;}; linechars = [|'|'|]; lineOptions = Map.empty }|]));
        // Inner box in inner 'a' box
        (([|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 4; row = 3;}; glyphOptions = Map.empty };
            { glyphKind = RoundUpperRightCorner; gridCoord = {col = 6; row = 3;}; glyphOptions = Map.empty }|],
          [|{ orientation = Horizontal; gridCoordStart = {col = 5; row = 3;}; gridCoordEnd = {col = 5; row = 3;}; linechars = [|'-'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundUpperRightCorner; gridCoord = {col = 6; row = 3;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerRightCorner; gridCoord = {col = 6; row = 4;}; glyphOptions = Map.empty }|], 
          [||]),
         ([|{ glyphKind = RoundLowerLeftCorner; gridCoord = {col = 4; row = 4;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerRightCorner; gridCoord = {col = 6; row = 4;}; glyphOptions = Map.empty }|],
          [|{ orientation = Horizontal; gridCoordStart = {col = 5; row = 4;}; gridCoordEnd = {col = 5; row = 4;}; linechars = [|'-'|]; lineOptions = Map.empty }|]),
         ([|{ glyphKind = RoundUpperLeftCorner; gridCoord = {col = 4; row = 3;}; glyphOptions = Map.empty };
            { glyphKind = RoundLowerLeftCorner; gridCoord = {col = 4; row = 4;}; glyphOptions = Map.empty }|], 
          [||]))|]

    let findPathBoxesOK = findPathBoxesExpected = findPathBoxesResult

