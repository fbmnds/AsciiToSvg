module AsciiToSvg.Topology


module Operators =

  let compareGridCoordinates x y =
    if x.row < y.row then -1 else
    if x.col < y.col then -1 else
    if x.row = y.row && x.col = y.col then 0
    else 1

  let flipLine (line: Line) =
    if compareGridCoordinates line.gridCoordStart line.gridCoordEnd < 1 then line
    else
      { orientation = line.orientation
        gridCoordStart = line.gridCoordEnd
        gridCoordEnd = line.gridCoordStart
        linechars = line.linechars
        lineOptions = line.lineOptions }

  /// Glyph x is left of Glyph y
  let (?>?) (x: Glyph) (y: Glyph) = (x.gridCoord.col + 1 = y.gridCoord.col) && (x.gridCoord.row = y.gridCoord.row)
  /// Glyph x is horizontally left of Glyph y
  let (?>>?) (x: Glyph) (y: Glyph) = (x.gridCoord.col < y.gridCoord.col) && (x.gridCoord.row = y.gridCoord.row)
  /// Glyph x is right of Glyph y
  let (?<?) (x: Glyph) (y: Glyph) = (x.gridCoord.col - 1 = y.gridCoord.col) && (x.gridCoord.row = y.gridCoord.row)
  /// Glyph x is horizontally right of Glyph y
  let (?<<?) (x: Glyph) (y: Glyph) = (x.gridCoord.col > y.gridCoord.col) && (x.gridCoord.row = y.gridCoord.row)
  /// Glyph x is above of Glyph y
  let (?^?) (x: Glyph) (y: Glyph) = (x.gridCoord.col = y.gridCoord.col) && (x.gridCoord.row + 1 = y.gridCoord.row)
  /// Glyph x is vertically above of Glyph y
  let (?^^?) (x: Glyph) (y: Glyph) = (x.gridCoord.col = y.gridCoord.col) && (x.gridCoord.row < y.gridCoord.row)
  /// Glyph x is below of Glyph y
  let (?|?) (x: Glyph) (y: Glyph) = (x.gridCoord.col = y.gridCoord.col) && (x.gridCoord.row - 1 = y.gridCoord.row)
  /// Glyph x is vertically below of Glyph y
  let (?||?) (x: Glyph) (y: Glyph) = (x.gridCoord.col = y.gridCoord.col) && (x.gridCoord.row > y.gridCoord.row)


  /// Glyph x is left of Line y
  let (?>-) (x: Glyph) (y: Line) = 
    y |> flipLine |> fun y -> (x.gridCoord.col + 1 = y.gridCoordStart.col) && (x.gridCoord.row = y.gridCoordStart.row)
  /// Glyph x is horizontally left of Line y
  let (?>>-) (x: Glyph) (y: Line) = 
    y |> flipLine |> fun y -> (x.gridCoord.col < y.gridCoordStart.col) && (x.gridCoord.row = y.gridCoordStart.row)
  /// Glyph x is right of Line y
  let (?<-) (x: Glyph) (y: Line) = 
    y |> flipLine |> fun y -> (x.gridCoord.col - 1 = y.gridCoordEnd.col) && (x.gridCoord.row = y.gridCoordEnd.row)
  /// Glyph x is horizontally right of Line y
  let (?<<-) (x: Glyph) (y: Line) = 
    y |> flipLine |> fun y -> (x.gridCoord.col > y.gridCoordEnd.col) && (x.gridCoord.row = y.gridCoordEnd.row)
  /// Glyph x is above of Line y
  let (?^-) (x: Glyph) (y: Line) = 
    y |> flipLine |> fun y -> (x.gridCoord.col = y.gridCoordStart.col) && (x.gridCoord.row + 1 = y.gridCoordStart.row)
  /// Glyph x is vertically above of Line y
  let (?^^-) (x: Glyph) (y: Line) = 
    y |> flipLine |> fun y -> (x.gridCoord.col = y.gridCoordStart.col) && (x.gridCoord.row < y.gridCoordStart.row)
  /// Glyph x is below of Line y
  let (?|-) (x: Glyph) (y: Line) = 
    y |> flipLine |> fun y -> (x.gridCoord.col = y.gridCoordEnd.col) && (x.gridCoord.row - 1 = y.gridCoordEnd.row)
  /// Glyph x is vertically below of Line y
  let (?||-) (x: Glyph) (y: Line) = 
    y |> flipLine |> fun y -> (x.gridCoord.col = y.gridCoordEnd.col) && (x.gridCoord.row > y.gridCoordEnd.row)

  /// Line x is horizontally left of Line y
  let (->>-) (x: Line) (y: Line) =
    ((x |> flipLine), (y |> flipLine)) |> fun (x, y) -> 
      (x.gridCoordEnd.col < y.gridCoordStart.col) &&
      (x.gridCoordEnd.row = y.gridCoordStart.row) &&
      (x.gridCoordStart.row = x.gridCoordEnd.row) &&
      (y.gridCoordStart.row = y.gridCoordEnd.row)
  /// Line x is horizontally right of Line y
  let (-<<-) (x: Line) (y: Line) =
    ((x |> flipLine), (y |> flipLine)) |> fun (x, y) -> 
      (x.gridCoordStart.col > y.gridCoordEnd.col) &&
      (x.gridCoordStart.row = y.gridCoordEnd.row) &&
      (x.gridCoordStart.row = x.gridCoordEnd.row) &&
      (y.gridCoordStart.row = y.gridCoordEnd.row)

  /// Line x is vertically above of Line y
  let (-^^-) (x: Line) (y: Line) =
    ((x |> flipLine), (y |> flipLine)) |> fun (x, y) ->
      (x.gridCoordEnd.row < y.gridCoordStart.row) &&
      (x.gridCoordEnd.col = y.gridCoordStart.col) &&
      (x.gridCoordStart.col = x.gridCoordEnd.col) &&
      (y.gridCoordStart.col = y.gridCoordEnd.col)

  /// Line x is vertically below of Line y
  let (-||-) (x: Line) (y: Line) =
    ((x |> flipLine), (y |> flipLine)) |> fun (x, y) ->
      (x.gridCoordStart.row > y.gridCoordEnd.row) &&
      (x.gridCoordStart.col = y.gridCoordEnd.col) &&
      (x.gridCoordStart.col = x.gridCoordEnd.col) &&
      (y.gridCoordStart.col = y.gridCoordEnd.col)

open Operators

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

let findBoxCorners (glyphs: Glyph[]) =
  let upperLeftCorners = glyphs |> Array.filter IsUpperLeftCornerKind
  let upperRightCorners = glyphs |> Array.filter IsUpperRightCornerKind
  let lowerRightCorners = glyphs |> Array.filter IsLowerRightCornerKind
  let lowerLeftCorners = glyphs |> Array.filter IsLowerLeftCornerKind
  [|upperLeftCorners; upperRightCorners; lowerRightCorners; lowerLeftCorners|]

// only finds those boxes which edges do not span across corners
let FindBoxes (horizLines: Line[])
              (vertLines: Line[])
              (upperLeftCorners: Glyph[])
              (upperRightCorners: Glyph[])
              (lowerRightCorners: Glyph[])
              (lowerLeftCorners: Glyph[]) =
  [|for uLC in upperLeftCorners do
      for uRC in upperRightCorners do
        if uLC ?>>? uRC then
          for hUL in horizLines do
            if uLC ?>- hUL && uRC ?<- hUL then
              for lRC in lowerRightCorners do
                if lRC ?||? uRC then
                  for vRL in vertLines do
                    if uRC ?^- vRL then
                      for lLC in lowerLeftCorners do
                        if lLC ?>? lRC && lLC ?||? uLC then
                          for hLL in horizLines do
                            if lLC ?>- hLL && lRC ?<- hLL then
                              for vLL in vertLines do
                                if uLC ?^^- vLL && lLC ?||- vLL then
                                  yield uLC, hUL, uRC, vRL, lRC, hLL, lLC, vLL|]

// finds boxes that consist only of corner glyphs
let FindMiniBoxes (upperLeftCorners: Glyph[])
                  (upperRightCorners: Glyph[])
                  (lowerRightCorners: Glyph[])
                  (lowerLeftCorners: Glyph[]) =
  [|for uLC in upperLeftCorners do
      for uRC in upperRightCorners do
        if uLC ?>? uRC then
          for lRC in lowerRightCorners do
            if uRC ?^? lRC then
              for lLC in lowerLeftCorners do
                if lRC ?<? lLC && lLC ?|? uLC then
                  yield uLC, uRC, lRC, lLC|]

let findHorizontalPathBetween (corner1: Glyph) (corner2: Glyph) (glyphs: Glyph[]) (lines: Line[]) =
  let leftCorner, rightCorner = if corner1 ?>>? corner2 then corner1, corner2 else corner2, corner1
  if not (leftCorner ?>>? rightCorner) then [||], [||] else
  if leftCorner ?>? rightCorner then [|leftCorner; rightCorner|], [||]
  else
    let linesInBetween =
      lines
      |> Array.filter (fun line ->
        line.orientation = Horizontal &&
        leftCorner ?>>- line &&
        rightCorner ?<<- line)
      |> Array.sortWith (fun x y -> 
        if x -<<- y then -1 else
        if x ->>- y then 1
        else 0) 
    let glyphsInBetween =
      glyphs
      |> Array.filter (fun glyph ->
        glyph ?<<? leftCorner &&
        glyph ?>>? rightCorner &&
        glyph.glyphKind <> UpTick &&
        glyph.glyphKind <> DownTick &&
        IsHorizontalLineSegmentKind glyph)
      |> Array.sortWith (fun x y ->
        if x ?<<? y then -1 else
        if x ?>>? y then 1 
        else 0)
    let coveredCoords =
      [|for l in linesInBetween do yield [|for c in [l.gridCoordStart.col .. l.gridCoordEnd.col] do yield c|]|]
      |> Array.concat
      |> fun x -> (Array.concat [[|for g in glyphsInBetween do yield g.gridCoord.col|]; x])
      |> Array.sort
    if coveredCoords = [|for i in [leftCorner.gridCoord.col + 1 .. rightCorner.gridCoord.col - 1] do yield i|] then
      Array.concat [[|leftCorner|]; glyphsInBetween; [|rightCorner|]], linesInBetween
    else [||], [||]

let findVerticalPathBetween (corner1: Glyph) (corner2: Glyph) (glyphs: Glyph[]) (lines: Line[]) =
  let upperCorner, lowerCorner = if corner1 ?^^? corner2 then corner1, corner2 else corner2, corner1
  if not (upperCorner ?^^? lowerCorner) then [||], [||] else
  if upperCorner ?^? lowerCorner then [|upperCorner; lowerCorner|], [||]
  else
    let linesInBetween = 
      lines
      |> Array.filter (fun line -> 
        line.orientation = Vertical &&
        upperCorner ?^^- line &&
        lowerCorner ?||- line) 
      |> Array.sortWith (fun x y ->
        if x -||- y then -1 else
        if x -^^- y then 1
        else 0)
    let glyphsInBetween =
      glyphs
      |> Array.filter (fun glyph ->
        glyph ?||? upperCorner &&
        glyph ?^^? lowerCorner &&
        IsVerticalLineSegmentKind glyph)
      |> Array.sortWith (fun x y ->
        if x ?||? y then -1 else
        if x ?^^? y then 1
        else 0)
    let coveredCoords =
      [|for l in linesInBetween do yield [|for c in [l.gridCoordStart.row .. l.gridCoordEnd.row] do yield c|]|] 
      |> Array.concat
      |> fun x -> (Array.concat [[|for g in glyphsInBetween do yield g.gridCoord.row|]; x])
      |> Array.sort
    if coveredCoords = [|for i in [upperCorner.gridCoord.row + 1 .. lowerCorner.gridCoord.row - 1] do yield i|] then
      Array.concat [[|upperCorner|]; glyphsInBetween; [|lowerCorner|]], linesInBetween
    //else [||], [||]
    else
      Array.concat [[|upperCorner|]; glyphsInBetween; [|lowerCorner|]], linesInBetween

let FindPathBoxes (allLines: Line[] * Line[]) (glyphs: Glyph[]) =
  let horizLines = fst allLines
  let vertLines = snd allLines
  let corners = findBoxCorners glyphs
  let upperLeftCorners = corners.[0]
  let upperRightCorners = corners.[1]
  let lowerRightCorners = corners.[2]
  let lowerLeftCorners = corners.[3]
  [|for uLC in upperLeftCorners do
      for uRC in upperRightCorners do
        if uLC ?>>? uRC then
          let hUP = findHorizontalPathBetween uLC uRC glyphs horizLines
          if (fst hUP).Length > 0 then
            for lRC in lowerRightCorners do
              if lRC ?||? uRC then
                let vRP = findVerticalPathBetween uRC lRC glyphs vertLines
                if (fst vRP).Length > 0 then
                  for lLC in lowerLeftCorners do
                    if lLC ?||? uLC && lLC ?>>? lRC then
                      let hLP = findHorizontalPathBetween lLC lRC glyphs horizLines
                      if (fst hLP).Length > 0 then
                        let vLP = findVerticalPathBetween uLC lLC glyphs vertLines
                        if (fst vLP).Length > 0 then
                          yield hUP, vRP, hLP, vLP|]