﻿module AsciiToSvg.GlyphScanner


let IsGlyphByPattern (pattern: GlyphPattern) (txtGrid: TxtGrid) col row =
  let test coord letter =
    try
      txtGrid.[row+coord.row].[col+coord.col] = letter
    with _ -> false
  pattern
  |> Array.Parallel.map (fun x ->
    match x with
    | (_, Wildcard _) -> true
    | (coord, (Letter letters)) ->
      letters |> Array.map (fun letter -> test coord letter) |> Array.reduce (fun x y -> x || y))
  |> Array.reduce (fun x y -> x && y)

// #region Line Glyphs

let IsVerticalLine (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'|'|]))
      ({ col = 0; row = -1 }, (Letter [|'|'; '+'; '^'|]))|]
    [|({ col = 0; row = 0 }, (Letter [|'|'|]))
      ({ col = 0; row = 1 }, (Letter [|'|'; '+'; 'v'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsHorizontalLine (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'-'|]))
      ({ col = -1; row = 0 }, (Letter [|'-'; '+'; '<'|]))|]
    [|({ col = 0; row = 0 }, (Letter [|'-'|]))
      ({ col = 1; row = 0 }, (Letter [|'-'; '+'; '>'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsCross (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = 0; row = -1 }, (Letter [|'|'; '+'; '^'|]))|]
    [|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = 0; row = 1 }, (Letter [|'|'; '+'; 'v'|]))|]
    [|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = -1; row = 0 }, (Letter [|'-'; '+'; '<'|]))|]
    [|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = 1; row = 0 }, (Letter [|'-'; '+'; '>'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

// #endregion

// #region Arrows

let IsArrowUp (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'^'|]))
      ({ col = 0; row = 1 }, (Letter [|'+'; '|'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsArrowDown (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'v'|]))
      ({ col = 0; row = -1 },(Letter [|'+'; '|'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsArrowLeftToRight (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'>'|]))
      ({ col = -1; row = 0 }, (Letter [|'+'; '-'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsArrowRightToLeft (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'<'|]))
      ({ col = 1; row = 0 }, (Letter [|'+'; '-'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

// #endregion

// #region Corners

let IsUpperLeftCorner (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = 1; row = 0 }, (Letter [|'-'; '+'|]))
      ({ col = 0; row = 1 }, (Letter [|'|'; '+'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsLowerLeftCorner (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = 1; row = 0 }, (Letter [|'-'; '+'|]))
      ({ col = 0; row = -1 }, (Letter [|'|'; '+'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsUpperRightCorner (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = -1; row = 0 }, (Letter [|'-'; '+'|]))
      ({ col = 0; row = 1 }, (Letter [|'|'; '+'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsLowerRightCorner (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = -1; row = 0 }, (Letter [|'-'; '+'|]))
      ({ col = 0; row = -1 }, (Letter [|'|'; '+'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsUpperLeftAndRightCorner (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = 1; row = 0 }, (Letter [|'-'; '+'|]))
      ({ col = -1; row = 0 }, (Letter [|'-'; '+'|]))
      ({ col = 0; row = 1 }, (Letter [|'|'; '+'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsLowerLeftAndRightCorner (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = 1; row = 0 }, (Letter [|'-'; '+'|]))
      ({ col = -1; row = 0 }, (Letter [|'-'; '+'|]))
      ({ col = 0; row = -1 }, (Letter [|'|'; '+'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsUpperAndLowerRightCorner (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = -1; row = 0 }, (Letter [|'-'; '+'|]))
      ({ col = 0; row = -1 }, (Letter [|'|'; '+'|]))
      ({ col = 0; row = 1 }, (Letter [|'|'; '+'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsUpperAndLowerLeftCorner (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = 1; row = 0 }, (Letter [|'-'; '+'|]))
      ({ col = 0; row = -1 }, (Letter [|'|'; '+'|]))
      ({ col = 0; row = 1 }, (Letter [|'|'; '+'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsCrossCorner (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter [|'+'|]))
      ({ col = -1; row = 0 }, (Letter [|'-'; '+'|]))
      ({ col = 1; row = 0 }, (Letter [|'-'; '+'|]))
      ({ col = 0; row = -1 }, (Letter [|'|'; '+'|]))
      ({ col = 0; row = 1 }, (Letter [|'|'; '+'|]))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

// #endregion

// #region Not implemented

let IsRoundUpperLeftCorner (txtGrid: TxtGrid) col row = false
let IsRoundLowerLeftCorner (txtGrid: TxtGrid) col row = false
let IsRoundUpperRightCorner (txtGrid: TxtGrid) col row = false
let IsRoundLowerRightCorner (txtGrid: TxtGrid) col row = false
  //
let IsRoundUpperLeftAndRightCorner (txtGrid: TxtGrid) col row = false
let IsRoundLowerLeftAndRightCorner (txtGrid: TxtGrid) col row = false
let IsRoundUpperAndLowerRightCorner (txtGrid: TxtGrid) col row = false
let IsRoundUpperAndLowerLeftCorner (txtGrid: TxtGrid) col row = false
let IsRoundCrossCorner (txtGrid: TxtGrid) col row = false
  //
let IsDiamondLeftCorner (txtGrid: TxtGrid) col row = false
let IsDiamondRightCorner (txtGrid: TxtGrid) col row = false
let IsDiamondUpperCorner (txtGrid: TxtGrid) col row = false
let IsDiamondLowerCorner (txtGrid: TxtGrid) col row = false
  //
let IsDiamondUpperAndLeftCorner (txtGrid: TxtGrid) col row = false
let IsDiamondUpperAndRightCorner (txtGrid: TxtGrid) col row = false
let IsDiamondLowerAndLeftCorner (txtGrid: TxtGrid) col row = false
let IsDiamondLowerAndRightCorner (txtGrid: TxtGrid) col row = false
let IsDiamondCrossCorner (txtGrid: TxtGrid) col row = false
  //
let IsLargeUpperLeftCorner (txtGrid: TxtGrid) col row = false
let IsLargeLowerLeftCorner (txtGrid: TxtGrid) col row = false
let IsLargeUpperRightCorner (txtGrid: TxtGrid) col row = false
let IsLargeLowerRightCorner (txtGrid: TxtGrid) col row = false
  //
let IsLargeUpperLeftAndRightCorner (txtGrid: TxtGrid) col row = false
let IsLargeLowerLeftAndRightCorner (txtGrid: TxtGrid) col row = false
let IsLargeUpperAndLowerRightCorner (txtGrid: TxtGrid) col row = false
let IsLargeUpperAndLowerLeftCorner (txtGrid: TxtGrid) col row = false
let IsLargeCrossCorner (txtGrid: TxtGrid) col row = false
  //
let IsEllipse (txtGrid: TxtGrid) col row = false
let IsCircle (txtGrid: TxtGrid) col row = false

// #endregion

let ScanGlyphs (grid : TxtGrid) : Glyph[] =
  let scan i j =
    let glyphProperty glyphKind : Glyph =
     { glyphKind = glyphKind;
       gridCoord = { col = i; row = j };
       glyphOptions = Map.empty }
    // Arrows
    if (IsArrowUp grid i j) then glyphProperty ArrowUp else
    if (IsArrowDown grid i j) then glyphProperty ArrowDown else
    if (IsArrowLeftToRight grid i j) then glyphProperty ArrowLeftToRight else
    if (IsArrowRightToLeft grid i j) then glyphProperty ArrowRightToLeft else
    // Corners
    if (IsUpperLeftCorner grid i j) then glyphProperty UpperLeftCorner else
    if (IsLowerLeftCorner grid i j) then glyphProperty LowerLeftCorner else
    if (IsUpperRightCorner grid i j) then glyphProperty UpperRightCorner else
    if (IsLowerRightCorner grid i j) then glyphProperty LowerRightCorner else
    if (IsUpperLeftAndRightCorner grid i j) then glyphProperty UpperLeftAndRightCorner else
    if (IsLowerLeftAndRightCorner grid i j) then glyphProperty LowerLeftAndRightCorner else
    if (IsUpperAndLowerRightCorner grid i j) then glyphProperty UpperAndLowerRightCorner else
    if (IsUpperAndLowerLeftCorner grid i j) then glyphProperty UpperAndLowerLeftCorner else
    if (IsCrossCorner grid i j) then glyphProperty CrossCorner else
// #region Not implemented
    // RoundCorners
    if (IsRoundUpperLeftCorner grid i j) then glyphProperty RoundUpperLeftCorner else
    if (IsRoundLowerLeftCorner grid i j) then glyphProperty RoundLowerLeftCorner else
    if (IsRoundUpperRightCorner grid i j) then glyphProperty RoundUpperRightCorner else
    if (IsRoundLowerRightCorner grid i j) then glyphProperty RoundLowerRightCorner else
    //
    if (IsRoundUpperLeftAndRightCorner grid i j) then glyphProperty RoundUpperLeftAndRightCorner else
    if (IsRoundLowerLeftAndRightCorner grid i j) then glyphProperty RoundLowerLeftAndRightCorner else
    if (IsRoundUpperAndLowerRightCorner grid i j) then glyphProperty RoundUpperAndLowerRightCorner else
    if (IsRoundUpperAndLowerLeftCorner grid i j) then glyphProperty RoundUpperAndLowerLeftCorner else
    if (IsRoundCrossCorner grid i j) then glyphProperty RoundCrossCorner else
    // DiamondCorners
    if (IsDiamondLeftCorner grid i j) then glyphProperty DiamondLeftCorner else
    if (IsDiamondRightCorner grid i j) then glyphProperty DiamondRightCorner else
    if (IsDiamondUpperCorner grid i j) then glyphProperty DiamondUpperCorner else
    if (IsDiamondLowerCorner grid i j) then glyphProperty DiamondUpperCorner else
    //
    if (IsDiamondUpperAndLeftCorner grid i j) then glyphProperty DiamondUpperAndLeftCorner else
    if (IsDiamondUpperAndRightCorner grid i j) then glyphProperty DiamondUpperAndRightCorner else
    if (IsDiamondLowerAndLeftCorner grid i j) then glyphProperty DiamondLowerAndLeftCorner else
    if (IsDiamondLowerAndRightCorner grid i j) then glyphProperty DiamondLowerAndRightCorner else
    if (IsDiamondCrossCorner grid i j) then glyphProperty DiamondCrossCorner else
    // LargeCorners
    if (IsLargeUpperLeftCorner grid i j) then glyphProperty LargeUpperLeftCorner else
    if (IsLargeLowerLeftCorner grid i j) then glyphProperty LargeLowerLeftCorner else
    if (IsLargeUpperRightCorner grid i j) then glyphProperty LargeUpperRightCorner else
    if (IsLargeLowerRightCorner grid i j) then glyphProperty LargeLowerRightCorner else
    //
    if (IsLargeUpperLeftAndRightCorner grid i j) then glyphProperty LargeUpperLeftAndRightCorner else
    if (IsLargeLowerLeftAndRightCorner grid i j) then glyphProperty LargeLowerLeftAndRightCorner else
    if (IsLargeUpperAndLowerRightCorner grid i j) then glyphProperty LargeUpperAndLowerRightCorner else
    if (IsLargeUpperAndLowerLeftCorner grid i j) then glyphProperty LargeUpperAndLowerLeftCorner else
    if (IsLargeCrossCorner grid i j) then glyphProperty LargeCrossCorner else
    //  Ellipse & Circle
    if (IsEllipse grid i j) then glyphProperty Ellipse else
    if (IsCircle grid i j) then glyphProperty Circle
// #endregion
    else
      glyphProperty Empty
  if grid.Length = 0 then [||]
  else
    grid
    |> Array.Parallel.mapi (fun row -> Array.mapi (fun col _ -> scan col row))
    |> Array.Parallel.mapi (fun row -> Array.filter (fun x -> x.glyphKind <> Empty))
    |> Array.concat

let IsGlyph (grid: TxtGrid) col row =
  [|// Line Glyph
    (IsVerticalLine grid col row)
    (IsHorizontalLine grid col row)
    (IsCross grid col row)
    // Arrows
    (IsArrowUp grid col row)
    (IsArrowDown grid col row)
    (IsArrowLeftToRight grid col row)
    (IsArrowRightToLeft grid col row)
    // Corners
    (IsUpperLeftCorner grid col row)
    (IsLowerLeftCorner grid col row)
    (IsUpperRightCorner grid col row)
    (IsLowerRightCorner grid col row)
    (IsUpperLeftAndRightCorner grid col row)
    (IsLowerLeftAndRightCorner grid col row)
    (IsUpperAndLowerRightCorner grid col row)
    (IsUpperAndLowerLeftCorner grid col row)
    (IsCrossCorner grid col row)
// #region Not implemented
    // RoundCorners
    (IsRoundUpperLeftCorner grid col row)
    (IsRoundLowerLeftCorner grid col row)
    (IsRoundUpperRightCorner grid col row)
    (IsRoundLowerRightCorner grid col row)
    //
    (IsRoundUpperLeftAndRightCorner grid col row)
    (IsRoundLowerLeftAndRightCorner grid col row)
    (IsRoundUpperAndLowerRightCorner grid col row)
    (IsRoundUpperAndLowerLeftCorner grid col row)
    (IsRoundCrossCorner grid col row)
    // DiamondCorners
    (IsDiamondLeftCorner grid col row)
    (IsDiamondRightCorner grid col row)
    (IsDiamondUpperCorner grid col row)
    (IsDiamondLowerCorner grid col row)
    //
    (IsDiamondUpperAndLeftCorner grid col row)
    (IsDiamondUpperAndRightCorner grid col row)
    (IsDiamondLowerAndLeftCorner grid col row)
    (IsDiamondLowerAndRightCorner grid col row)
    (IsDiamondCrossCorner grid col row)
    // LargeCorners
    (IsLargeUpperLeftCorner grid col row)
    (IsLargeLowerLeftCorner grid col row)
    (IsLargeUpperRightCorner grid col row)
    (IsLargeLowerRightCorner grid col row)
    //
    (IsLargeUpperLeftAndRightCorner grid col row)
    (IsLargeLowerLeftAndRightCorner grid col row)
    (IsLargeUpperAndLowerRightCorner grid col row)
    (IsLargeUpperAndLowerLeftCorner grid col row)
    (IsLargeCrossCorner grid col row)
    //  Ellipse & Circle
    (IsEllipse grid col row)
    (IsCircle grid col row)|]
// #endregion
    |> Array.reduce (fun x y -> x || y)

let IsNotGlyph (grid: TxtGrid) col row = not (IsGlyph grid col row)