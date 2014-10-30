[<RequireQualifiedAccess>]
module AsciiToSvg.GlyphRenderer


open AsciiToSvg.SvgDocument


let Render scale options glyph =
  match glyph.glyphKind with
  | ArrowUp -> ScalableArrowTemplate glyph options scale 0.0 7.0 8.0 7.0 4.0 0.0 4.0 7.0 4.0 14.0
  | ArrowDown -> ScalableArrowTemplate glyph options scale 0.0 7.0 8.0 7.0 4.0 14.0 4.0 7.0 4.0 0.0
  | ArrowLeftToRight -> ScalableArrowTemplate glyph options scale 1.0 3.0 1.0 11.0 8.0 7.0 1.0 7.0 0.0 7.0
  | ArrowRightToLeft -> ScalableArrowTemplate glyph options scale 7.0 3.0 7.0 11.0 0.0 7.0 7.0 7.0 8.0 7.0
  //
  | UpperLeftCorner -> ScalableCornerTemplate glyph options scale
  | LowerLeftCorner -> ScalableCornerTemplate glyph options scale
  | UpperRightCorner -> ScalableCornerTemplate glyph options scale
  | LowerRightCorner -> ScalableCornerTemplate glyph options scale
  //
  | UpperLeftAndRightCorner -> ScalableCornerTemplate glyph options scale
  | LowerLeftAndRightCorner -> ScalableCornerTemplate glyph options scale
  | UpperAndLowerRightCorner -> ScalableCornerTemplate glyph options scale
  | UpperAndLowerLeftCorner -> ScalableCornerTemplate glyph options scale
  | CrossCorner -> ScalableCornerTemplate glyph options scale
  //
  | RoundUpperLeftCorner -> ScalableRoundCornerTemplate glyph options scale
  | RoundLowerLeftCorner -> ScalableRoundCornerTemplate glyph options scale
  | RoundUpperRightCorner -> ScalableRoundCornerTemplate glyph options scale
  | RoundLowerRightCorner -> ScalableRoundCornerTemplate glyph options scale
  //
  | RoundUpperLeftAndRightCorner -> ScalableRoundCornerTemplate glyph options scale
  | RoundLowerLeftAndRightCorner -> ScalableRoundCornerTemplate glyph options scale
  | RoundUpperAndLowerRightCorner -> ScalableRoundCornerTemplate glyph options scale
  | RoundUpperAndLowerLeftCorner -> ScalableRoundCornerTemplate glyph options scale
  | RoundCrossCorner -> ScalableRoundCornerTemplate glyph options scale
  //
  | UpTick -> ScalableCornerTemplate glyph options scale
  | DownTick -> ScalableCornerTemplate glyph options scale
  | _ -> ""

let RenderAll scale options (glyphs: Glyph[]) = glyphs |> Array.Parallel.map (Render scale options)
