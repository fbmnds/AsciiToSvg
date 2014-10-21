module AsciiToSvg.GlyphRenderer


open AsciiToSvg.SvgDocument


let Render scale options glyph =
  match glyph.glyphKind with
  | ArrowUp  -> ScalableArrowTemplate glyph options scale 0.0 7.0 8.0 7.0 4.0 0.0 4.0 7.0 4.0 14.0
  | ArrowDown -> ScalableArrowTemplate glyph options scale 0.0 7.0 8.0 7.0 4.0 14.0 4.0 7.0 4.0 0.0
  | ArrowLeftToRight -> ScalableArrowTemplate glyph options scale 1.0 3.0 1.0 11.0 8.0 7.0 1.0 7.0 0.0 7.0
  | ArrowRightToLeft -> ScalableArrowTemplate glyph options scale 7.0 3.0 7.0 11.0 0.0 7.0 7.0 7.0 8.0 7.0
  | _ -> ""




