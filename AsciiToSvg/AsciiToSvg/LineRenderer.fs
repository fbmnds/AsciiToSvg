[<RequireQualifiedAccess>]
module AsciiToSvg.LineRenderer

open AsciiToSvg.SvgDocument

let Render scale options (line: Line) =
  ScalableLineTemplate line options scale

let RenderAll scale options (lines: Line[]) =
  lines
  |> Array.Parallel.map (fun line -> ScalableLineTemplate line options scale)
  |> Array.fold (fun r s -> r + s + "\n") ""