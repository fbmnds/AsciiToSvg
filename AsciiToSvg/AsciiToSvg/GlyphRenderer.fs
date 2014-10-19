module AsciiToSvg.GlyphRenderer

open System


let svgTemplateOpen =
  "<?xml version=\"1.0\" standalone=\"no\"?>" +
  "<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\"" +
  "  \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">" +
  """
  <!-- Created with ASCIIToSVG (https://github.com/fbmnds/a2svg) -->
  <svg width="{$canvasWidth}px" height="{$canvasHeight}px" version="1.1"
    xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
    <defs>
      <filter id="dsFilterNoBlur" width="150%" height="150%">
        <feOffset result="offOut" in="SourceGraphic" dx="3" dy="3"/>
        <feColorMatrix result="matrixOut" in="offOut" type="matrix" values="0.2 0 0 0 0 0 0.2 0 0 0 0 0 0.2 0 0 0 0 0 1 0"/>
        <feBlend in="SourceGraphic" in2="matrixOut" mode="normal"/>
      </filter>
      <filter id="dsFilter" width="150%" height="150%">
        <feOffset result="offOut" in="SourceGraphic" dx="3" dy="3"/>
        <feColorMatrix result="matrixOut" in="offOut" type="matrix" values="0.2 0 0 0 0 0 0.2 0 0 0 0 0 0.2 0 0 0 0 0 1 0"/>
        <feGaussianBlur result="blurOut" in="matrixOut" stdDeviation="3"/>
        <feBlend in="SourceGraphic" in2="blurOut" mode="normal"/>
      </filter>
      <marker id="iPointer"
        viewBox="0 0 10 10" refX="5" refY="5"
        markerUnits="strokeWidth"
        markerWidth="8" markerHeight="7"
        orient="auto">
        <path d="M 10 0 L 10 10 L 0 5 z" />
      </marker>
      <marker id="Pointer"
        viewBox="0 0 10 10" refX="5" refY="5"
        markerUnits="strokeWidth"
        markerWidth="8" markerHeight="7"
        orient="auto">
        <path d="M 0 0 L 10 5 L 0 10 z" />
      </marker>
    </defs>
  """

let svgTemplateClose = "  </svg>"

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
  { px = ((float) glyph.gridCoord.row * scale.scx) |> fun x -> Math.Round(x, 3)
    py = ((float) glyph.gridCoord.col * scale.scy) |> fun x -> Math.Round(x, 3) }

type ArrowUpRenderer() = class
  let ax = 7.0
  let ay = 4.0
  let bx = 7.0
  let by = 0.0
  let cx = 0.0
  let cy = 4.0
  let dx = 7.0
  let dy = 8.0
  let ex = 14.0
  let ey = 4.0

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
  let ax = 7.0
  let ay = 4.0
  let bx = 7.0
  let by = 0.0
  let cx = 14.0
  let cy = 4.0
  let dx = 7.0
  let dy = 9.0
  let ex = 0.0
  let ey = 4.0

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
  let ax = 7.0
  let ay = 1.0
  let bx = 3.0
  let by = 1.0
  let cx = 7.0
  let cy = 8.0
  let dx = 11.0
  let dy = 1.0
  let ex = 7.0
  let ey = 0.0

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
  let bx = 11.0
  let by = 7.0
  let cx = 7.0
  let cy = 00.0
  let dx = 3.0
  let dy = 7.0
  let ex = 7.0
  let ey = 8.0

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