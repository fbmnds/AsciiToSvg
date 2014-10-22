namespace AsciiToSvg.Tests

module SvgDocument =

  open AsciiToSvg
  open AsciiToSvg.SvgDocument

  let gridCoord = [|{ col = 0; row = 2 }; { col = 2; row = 0 }|]
  let svgCoord = gridCoord |> Array.map (ConvertCoordGridToSvg Scale)
  let svgCoordExpected = [|{ colpx = 0.0; rowpx = 30.0 }; { colpx = 18.0; rowpx = 0.0 }|]

  let rowdpx = 5.0
  let coldpx = 3.0
  let shiftedCoord = gridCoord |> Array.map (ShiftedCoordGridToSvg Scale coldpx rowdpx)
  let shiftedCoordExpected = [|{ colpx = 3.0; rowpx = 35.0 }; { colpx = 21.0; rowpx = 5.0 }|]

  let ax = 0.0
  let ay = 7.0
  let bx = 8.0
  let by = 7.0
  let cx = 4.0
  let cy = 0.0
  let dx = 4.0
  let dy = 7.0
  let ex = 4.0
  let ey = 14.0
  let cols = [|ax; bx; cx; dx; ex|] |> Array.map (fun x -> shiftColCoordGridToSvg Scale x { col = 5; row = 1 })
  let rows = [|ay; by; cy; dy; ey|] |> Array.map (fun x -> shiftRowCoordGridToSvg Scale x { col = 5; row = 1 })
  let colsExpected = [|45.0; 53.0; 49.0; 49.0; 49.0|]
  let rowsExpected = [|22.0; 22.0; 15.0; 22.0; 29.0|]


