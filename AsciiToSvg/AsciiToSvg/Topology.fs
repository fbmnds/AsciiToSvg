module AsciiToSvg.Topology


open AsciiToSvg.GlyphScanner

let IsUpperLeftCornerKind (glyph: Glyph) =
  match glyph.glyphKind with
  | UpperLeftCorner
  | UpperLeftAndRightCorner
  | UpperAndLowerLeftCorner
  | CrossCorner
  //
  | RoundUpperLeftCorner
  | RoundUpperLeftAndRightCorner
  | RoundUpperAndLowerLeftCorner
  | RoundCrossCorner
  //
  | LargeUpperLeftCorner
  | LargeUpperLeftAndRightCorner
  | LargeUpperAndLowerLeftCorner
  | LargeCrossCorner -> true
  | _ -> false

let IsUpperRightCornerKind (glyph: Glyph) =
  match glyph.glyphKind with
  | UpperRightCorner
  | UpperLeftAndRightCorner
  | UpperAndLowerRightCorner
  | CrossCorner
  //
  | RoundUpperRightCorner
  | RoundUpperLeftAndRightCorner
  | RoundUpperAndLowerRightCorner
  | RoundCrossCorner
  //
  | LargeUpperRightCorner
  | LargeUpperLeftAndRightCorner
  | LargeUpperAndLowerRightCorner
  | LargeCrossCorner -> true
  | _ -> false

let IsLowerRightCornerKind (glyph: Glyph) =
  match glyph.glyphKind with
  | LowerRightCorner
  | LowerLeftAndRightCorner
  | UpperAndLowerRightCorner
  | CrossCorner
  //
  | RoundLowerRightCorner
  | RoundLowerLeftAndRightCorner
  | RoundUpperAndLowerRightCorner
  | RoundCrossCorner
  //
  | LargeLowerRightCorner
  | LargeLowerLeftAndRightCorner
  | LargeUpperAndLowerRightCorner
  | LargeCrossCorner -> true
  | _ -> false

let IsLowerLeftCornerKind (glyph: Glyph) =
  match glyph.glyphKind with
  | LowerLeftCorner
  | LowerLeftAndRightCorner
  | UpperAndLowerLeftCorner
  | CrossCorner
  //
  | RoundLowerLeftCorner
  | RoundLowerLeftAndRightCorner
  | RoundUpperAndLowerLeftCorner
  | RoundCrossCorner
  //
  | LargeLowerLeftCorner
  | LargeLowerLeftAndRightCorner
  | LargeUpperAndLowerLeftCorner
  | LargeCrossCorner -> true
  | _ -> false

let IsLeftCornerKind glyph = IsUpperLeftCornerKind glyph || IsLowerLeftCornerKind glyph
let IsNotLeftCornerKind glyph = not (IsLeftCornerKind glyph)

let IsRightCornerKind glyph = IsUpperRightCornerKind glyph || IsLowerRightCornerKind glyph
let IsNotRightCornerKind glyph = not (IsRightCornerKind glyph)

let IsHorizontalLineSegmentKind (glyph: Glyph) =
  match glyph.glyphKind with
  | UpperLeftAndRightCorner
  | LowerLeftAndRightCorner
  | CrossCorner
  | RoundUpperLeftAndRightCorner
  | RoundLowerLeftAndRightCorner
  | RoundCrossCorner
  | LargeUpperLeftAndRightCorner
  | LargeLowerLeftAndRightCorner
  | LargeCrossCorner
  | UpTick
  | DownTick -> true
  | _ -> false

let IsVerticalLineSegmentKind (glyph: Glyph) =
  match glyph.glyphKind with
  | UpperAndLowerLeftCorner
  | UpperAndLowerRightCorner
  | CrossCorner
  | RoundUpperAndLowerLeftCorner
  | RoundUpperAndLowerRightCorner
  | RoundCrossCorner
  | LargeUpperAndLowerLeftCorner
  | LargeUpperAndLowerRightCorner
  | LargeCrossCorner -> true
  | _ -> false

let flipLine (line: Line) = 
  if line.gridCoordStart <= line.gridCoordEnd then line 
  else
    { orientation = line.orientation
      gridCoordStart = line.gridCoordEnd
      gridCoordEnd = line.gridCoordStart
      linechars = line.linechars
      lineOptions = line.lineOptions }

let findBoxCorners (glyphs: Glyph[]) =
  let upperLeftCorners = glyphs |> Array.filter IsUpperLeftCornerKind
  let upperRightCorners = glyphs |> Array.filter IsUpperRightCornerKind
  let lowerRightCorners = glyphs |> Array.filter IsLowerRightCornerKind
  let lowerLeftCorners = glyphs |> Array.filter IsLowerLeftCornerKind
  [|upperLeftCorners; upperRightCorners; lowerRightCorners; lowerLeftCorners|]

let findBoxes (horizLines: Line[]) 
              (vertLines: Line[]) 
              (upperLeftCorners: Glyph[])
              (upperRightCorners: Glyph[])
              (lowerRightCorners: Glyph[])
              (lowerLeftCorners: Glyph[]) =
  [|for uLC in upperLeftCorners do
      for uRC in upperRightCorners do 
        if uLC.gridCoord.IsHorizontalLeftOf uRC.gridCoord then
          for hUL' in horizLines do
            let hUL = flipLine hUL'
            if uLC.gridCoord.IsLeftOf hUL.gridCoordStart &&
               uRC.gridCoord.IsRightOf hUL.gridCoordEnd 
            then
              for lRC in lowerRightCorners do
                if lRC.gridCoord.IsVerticalBelowOf uRC.gridCoord then  
                  for vRL' in vertLines do
                    let vRL = flipLine vRL'
                    if uRC.gridCoord.IsAboveOf vRL.gridCoordStart &&
                       lRC.gridCoord.IsBelowOf vRL.gridCoordEnd
                    then
                      for lLC in lowerLeftCorners do
                        if lLC.gridCoord.IsHorizontalLeftOf lRC.gridCoord &&
                           lLC.gridCoord.IsVerticalBelowOf lRC.gridCoord
                        then
                          for hLL' in horizLines do
                            let hLL = flipLine hLL'
                            if lLC.gridCoord.IsLeftOf hLL.gridCoordStart &&
                               lRC.gridCoord.IsRightOf hLL.gridCoordEnd
                            then
                              for vLL' in vertLines do
                                let vLL = flipLine vLL'
                                if 
                                   uLC.gridCoord.IsAboveOf vLL.gridCoordStart &&
                                   lLC.gridCoord.IsBelowOf vLL.gridCoordEnd
                                then
                                  yield uLC, hUL, uRC, vRL, lRC, hLL, lLC, vLL|]

let findMiniBoxes (upperLeftCorners: Glyph[])
                  (upperRightCorners: Glyph[])
                  (lowerRightCorners: Glyph[])
                  (lowerLeftCorners: Glyph[]) =
  [|for uLC in upperLeftCorners do
      for uRC in upperRightCorners do 
        if uLC.gridCoord.IsLeftOf uRC.gridCoord then
          for lRC in lowerRightCorners do
            if uRC.gridCoord.IsAboveOf lRC.gridCoord then
              for lLC in lowerLeftCorners do
                if lRC.gridCoord.IsRightOf lLC.gridCoord &&
                   lLC.gridCoord.IsBelowOf uLC.gridCoord
                then
                  yield uLC, uRC, lRC, lLC|]

let findHorizontalPathBetween (corner1: Glyph) (corner2: Glyph) (glyphs: Glyph[]) (lines: Line[]) =
  let leftCorner, rightCorner = if corner1 <= corner2 then corner1, corner2 else corner2, corner1
  if not (leftCorner.gridCoord.IsHorizontalLeftOf rightCorner.gridCoord) then [||], [||] else
  if leftCorner.gridCoord.IsLeftOf rightCorner.gridCoord then [|leftCorner; rightCorner|], [||]
  else
    let linesInBetween = 
      lines 
      |> Array.filter (fun line -> 
        line.orientation = Horizontal && 
        line.gridCoordStart.IsHorizontalRightOf leftCorner.gridCoord &&
        line.gridCoordEnd.IsHorizontalRightOf leftCorner.gridCoord &&
        line.gridCoordStart.IsHorizontalLeftOf rightCorner.gridCoord &&
        line.gridCoordEnd.IsHorizontalLeftOf rightCorner.gridCoord) 
      |> Array.sortWith (fun x y -> compare x.gridCoordStart y.gridCoordStart)
    let glyphsInBetween =
      glyphs
      |> Array.filter (fun glyph ->
        glyph.gridCoord.IsHorizontalRightOf leftCorner.gridCoord &&
        glyph.gridCoord.IsHorizontalLeftOf rightCorner.gridCoord &&
        IsHorizontalLineSegmentKind glyph)
      |> Array.sortWith (fun x y -> compare x.gridCoord y.gridCoord)
    let coveredCoords =
      [|for l in linesInBetween do yield [|for c in [l.gridCoordStart.col .. l.gridCoordEnd.col] do yield c|]|] 
      |> Array.concat
      |> fun x -> (Array.concat [[|for g in glyphsInBetween do yield g.gridCoord.col|]; x])
      |> Array.sort
    if coveredCoords = [|for i in [leftCorner.gridCoord.col + 1 .. rightCorner.gridCoord.col - 1] do yield i|] then
      Array.concat [[|leftCorner|]; glyphsInBetween; [|rightCorner|]], linesInBetween
    else [||], [||]

let findVerticalPathBetween (corner1: Glyph) (corner2: Glyph) (glyphs: Glyph[]) (lines: Line[]) =
  let upperCorner, lowerCorner = if corner1 <= corner2 then corner1, corner2 else corner2, corner1
  if not (upperCorner.gridCoord.IsVerticalAboveOf lowerCorner.gridCoord) then [||], [||] else
  if upperCorner.gridCoord.IsAboveOf lowerCorner.gridCoord then [|upperCorner; lowerCorner|], [||]
  else
    let linesInBetween = 
      lines 
      |> Array.filter (fun line -> 
        line.orientation = Vertical && 
        line.gridCoordStart.IsVerticalBelowOf upperCorner.gridCoord &&
        line.gridCoordEnd.IsVerticalBelowOf upperCorner.gridCoord &&
        line.gridCoordStart.IsVerticalAboveOf lowerCorner.gridCoord &&
        line.gridCoordEnd.IsVerticalAboveOf lowerCorner.gridCoord) 
      |> Array.sortWith (fun x y -> compare x.gridCoordStart y.gridCoordStart)
    let glyphsInBetween =
      glyphs
      |> Array.filter (fun glyph ->
        glyph.gridCoord.IsVerticalBelowOf upperCorner.gridCoord &&
        glyph.gridCoord.IsVerticalAboveOf lowerCorner.gridCoord &&
        IsVerticalLineSegmentKind glyph)
      |> Array.sortWith (fun x y -> compare x.gridCoord y.gridCoord)
    let coveredCoords =
      [|for l in linesInBetween do yield [|for c in [l.gridCoordStart.col .. l.gridCoordEnd.col] do yield c|]|] 
      |> Array.concat
      |> fun x -> (Array.concat [[|for g in glyphsInBetween do yield g.gridCoord.row|]; x])
      |> Array.sort
    if coveredCoords = [|for i in [upperCorner.gridCoord.row + 1 .. lowerCorner.gridCoord.row - 1] do yield i|] then
      Array.concat [[|upperCorner|]; glyphsInBetween; [|lowerCorner|]], linesInBetween
    else [||], [||]

let findPathBoxes (horizLines: Line[]) 
                  (vertLines: Line[]) 
                  (glyphs: Glyph[])
                  (upperLeftCorners: Glyph[])
                  (upperRightCorners: Glyph[])
                  (lowerRightCorners: Glyph[])
                  (lowerLeftCorners: Glyph[]) =
  [|for uLC in upperLeftCorners do
      for uRC in upperRightCorners do
        let hUP = findHorizontalPathBetween uLC uRC glyphs horizLines
        if (fst hUP).Length > 0 then
          for lRC in lowerRightCorners do
            if lRC.gridCoord.IsVerticalBelowOf uRC.gridCoord then
              let vRP = findVerticalPathBetween uRC lRC glyphs vertLines
              if (fst vRP).Length > 0 then
                for lLC in lowerLeftCorners do
                  if lLC.gridCoord.IsVerticalBelowOf uLC.gridCoord &&
                     lLC.gridCoord.IsHorizontalLeftOf lRC.gridCoord
                  then
                    let hLP = findHorizontalPathBetween lLC lRC glyphs horizLines
                    if (fst hLP).Length > 0 then
                      let vLP = findVerticalPathBetween lLC lRC glyphs vertLines
                      if (fst vLP).Length > 0 then
                        yield uLC, hUP, uRC, vRP, lRC, hLP, lLC, vLP|]