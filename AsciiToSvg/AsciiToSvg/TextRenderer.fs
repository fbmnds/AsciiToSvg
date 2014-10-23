[<RequireQualifiedAccess>]
module  AsciiToSvg.TextRenderer


open AsciiToSvg.SvgDocument


let Render scale options text = ScalableTextTemplate text options scale

let RenderAll scale options texts =
  texts
  |> Array.Parallel.map (Render scale options)