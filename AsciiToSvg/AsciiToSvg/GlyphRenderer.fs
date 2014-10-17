module AsciiToSvg.GlyphRenderer

open System


let pseudoGlyphProperties = { letter = ' '; gridCoord = { row = -1; col = -1 }; glyphOptions = Map.empty }

let fillArrowTemplate fill scale svgCoord ax ay bx by cx cy dx dy ex ey =
  let scalex x = x * scale.scx + svgCoord.px
  let scaley y = y * scale.scy + svgCoord.py
  [ "<polygon "
    sprintf "fill=\"%s\"" fill
    sprintf "points=\"%f %f,\"" (scalex ax) (scaley ay)
    sprintf "%f %f, " (scalex bx) (scaley by)
    sprintf "%f %f," (scalex cx) (scaley cy)
    sprintf "%f %f," (scalex ax) (scaley ay)
    sprintf "%f %f, " (scalex dx) (scaley dy)
    sprintf "%f %f \" />" (scalex ex) (scaley ey) ]
  |>  String.Concat

let calculateSvgCoord (glyph: GlyphKindProperties) scale =
  { px = (float) glyph.gridCoord.row * scale.scx |> fun x -> Math.Round(x, 3)
    py = (float) glyph.gridCoord.col * scale.scy |> fun x -> Math.Round(x, 3) }

type ArrowUpRenderer() = class
  let ax = 7.0
  let ay = 0.0
  let bx = 7.0
  let by = 8.0
  let cx = 0.0
  let cy = 4.0
  let dx = 7.0
  let dy = 4.0
  let ex = 14.0
  let ey = 4.0

  interface IGlyphRenderer with

    member x.Render scale options glyph =
      let glyph' = match glyph with | ArrowUp x -> x | _ -> pseudoGlyphProperties
      let svgCoord = calculateSvgCoord glyph' scale
      let fill = match options.TryFind("fill") with | Some x -> x | _ -> "black"
      fillArrowTemplate fill scale svgCoord ax ay bx by cx cy dx dy ex ey

end

type ArrowDownRenderer() = class
  let ax = 7.0
  let ay = 0.0
  let bx = 7.0
  let by = 8.0
  let cx = 14.0
  let cy = 4.0
  let dx = 7.0
  let dy = 4.0
  let ex = 0.0
  let ey = 4.0

  interface IGlyphRenderer with

    member x.Render scale options glyph =
      let glyph' = match glyph with | ArrowDown x -> x | _ -> pseudoGlyphProperties
      let svgCoord = calculateSvgCoord glyph' scale
      let fill = match options.TryFind("fill") with | Some x -> x | _ -> "black"
      fillArrowTemplate fill scale svgCoord ax ay bx by cx cy dx dy ex ey

end

type ArrowLeftToRightRenderer() = class
  let ax = 7.0
  let ay = 7.0
  let bx = 3.0
  let by = 7.0
  let cx = 10.0
  let cy = 7.0
  let dx = 7.0
  let dy = 7.0
  let ex = 7.0
  let ey = 8.0

  interface IGlyphRenderer with

    member x.Render scale options glyph =
      let glyph' = match glyph with | ArrowLeftToRight x -> x | _ -> pseudoGlyphProperties
      let svgCoord = calculateSvgCoord glyph' scale
      let fill = match options.TryFind("fill") with | Some x -> x | _ -> "black"
      fillArrowTemplate fill scale svgCoord ax ay bx by cx cy dx dy ex ey

end

type ArrowRightToLeftRenderer() = class
  let ax = 7.0
  let ay = 0.0
  let bx = 3.0
  let by = 7.0
  let cx = 10.0
  let cy = 7.0
  let dx = 7.0
  let dy = 7.0
  let ex = 7.0
  let ey = 8.0

  interface IGlyphRenderer with

    member x.Render scale options glyph =
      let glyph' = match glyph with | ArrowRightToLeft x -> x | _ -> pseudoGlyphProperties
      let svgCoord = calculateSvgCoord glyph' scale
      let fill = match options.TryFind("fill") with | Some x -> x | _ -> "black"
      fillArrowTemplate fill scale svgCoord ax ay bx by cx cy dx dy ex ey

end