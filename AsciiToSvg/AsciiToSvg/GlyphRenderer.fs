module AsciiToSvg.GlyphRenderer

open System

open AsciiToSvg.SvgDocument



let arrowTemplate fill scale svgCoord ax ay bx by cx cy dx dy ex ey =
  let scalex x = (x * scale.scx + svgCoord.px) |> fun x -> Math.Round(x, 3)
  let scaley y = (y * scale.scy + svgCoord.py) |> fun x -> Math.Round(x, 3)
  [ "      <polygon "
    sprintf "fill=\"%s\" " fill
    sprintf "points=\"%.3f,%.3f " (scalex ax) (scaley ay)
    sprintf "%.3f,%.3f " (scalex bx) (scaley by)
    sprintf "%.3f,%.3f " (scalex cx) (scaley cy)
    sprintf "%.3f,%.3f " (scalex ax) (scaley ay)
    sprintf "%.3f,%.3f " (scalex dx) (scaley dy)
    sprintf "%.3f,%.3f\" />" (scalex ex) (scaley ey) ]
  |>  String.Concat

let calculateSvgCoord (glyph: GlyphKindProperties) scale =
  { px = ((float) glyph.gridCoord.row * GlyphWidth * scale.scx) |> fun x -> Math.Round(x, 3)
    py = ((float) glyph.gridCoord.col * GlyphHeight * scale.scy) |> fun x -> Math.Round(x, 3) }

type ArrowUpRenderer() = class
  let ax = 4.0
  let ay = 7.0
  let bx = 0.0
  let by = 7.0
  let cx = 4.0
  let cy = 0.0
  let dx = 8.0
  let dy = 7.0
  let ex = 4.0
  let ey = 14.0

  interface IGlyphRenderer with

    member x.Render scale options glyph =
      match glyph with
      | ArrowUp glyph' ->
        let svgCoord = calculateSvgCoord glyph' scale
        let fill = match options.TryFind("fill") with | Some x -> x | _ -> "black"
        arrowTemplate fill scale svgCoord ax ay bx by cx cy dx dy ex ey
        |> Some
      | _ -> None

end

type ArrowDownRenderer() = class
  let ax = 4.0
  let ay = 7.0
  let bx = 0.0
  let by = 7.0
  let cx = 4.0
  let cy = 14.0
  let dx = 9.0
  let dy = 7.0
  let ex = 4.0
  let ey = 0.0

  interface IGlyphRenderer with

    member x.Render scale options glyph =
      match glyph with
      | ArrowDown glyph' ->
        let svgCoord = calculateSvgCoord glyph' scale
        let fill = match options.TryFind("fill") with | Some x -> x | _ -> "black"
        arrowTemplate fill scale svgCoord ax ay bx by cx cy dx dy ex ey
        |> Some
      | _ -> None
end

type ArrowLeftToRightRenderer() = class
  let ax = 1.0
  let ay = 7.0
  let bx = 1.0
  let by = 3.0
  let cx = 8.0
  let cy = 7.0
  let dx = 1.0
  let dy = 11.0
  let ex = 0.0
  let ey = 7.0

  interface IGlyphRenderer with

    member x.Render scale options glyph =
      match glyph with
      | ArrowLeftToRight glyph' ->
        let svgCoord = calculateSvgCoord glyph' scale
        let fill = match options.TryFind("fill") with | Some x -> x | _ -> "black"
        arrowTemplate fill scale svgCoord ax ay bx by cx cy dx dy ex ey
        |> Some
      | _ -> None
end

type ArrowRightToLeftRenderer() = class
  let ax = 7.0
  let ay = 7.0
  let bx = 7.0
  let by = 11.0
  let cx = 0.0
  let cy = 7.0
  let dx = 7.0
  let dy = 3.0
  let ex = 8.0
  let ey = 7.0

  interface IGlyphRenderer with

    member x.Render scale options glyph =
      match glyph with
      | ArrowLeftToRight glyph' ->
        let svgCoord = calculateSvgCoord glyph' scale
        let fill = match options.TryFind("fill") with | Some x -> x | _ -> "black"
        arrowTemplate fill scale svgCoord ax ay bx by cx cy dx dy ex ey
        |> Some
      | _ -> None
end