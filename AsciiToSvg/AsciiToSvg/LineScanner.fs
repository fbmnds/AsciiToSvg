[<RequireQualifiedAccess>]
module AsciiToSvg.LineScanner

open AsciiToSvg.GlyphScanner


let ScanLineHorizontally (grid : TxtGrid) : Line[] =
  let IsNotHorizGlyph grid col row = not (IsHorizontalGlyph grid col row)
  let testHorizStart col row =
    let IsInLineSequence = (IsNotHorizGlyph grid (col - 1) row) && (IsHorizontalGlyph grid col row)
    let IsNextToCorner = (IsCornerKind grid (col - 1) row) && (IsHorizontalGlyph grid col row)
    if col = 0 then (IsHorizontalGlyph grid col row)
    else IsInLineSequence || IsNextToCorner
  let testHorizEnd col row =
    let IsInLineSequence = if col > 0 then (IsHorizontalGlyph grid col row) && (IsNotHorizGlyph grid (col + 1) row) else false
    let IsNextToCorner = if col > 0 then (IsHorizontalGlyph grid col row) && (IsCornerKind grid (col + 1) row) else false
    if col = (grid.[row].Length - 1) then (IsHorizontalGlyph grid col row)
    else IsInLineSequence || IsNextToCorner
  let horizLinePos row (line: char[]) =
    let line1 =
      line |> Array.Parallel.mapi (fun col _ -> if (testHorizStart col row) then col else -1) |> Array.filter (fun col -> col > -1)
    let line2 =
      line |> Array.Parallel.mapi (fun col _ -> if (testHorizEnd col row) then col else -1) |> Array.filter (fun col -> col > -1)
    if line1.Length = line2.Length then Array.zip line1 line2
    else
#if DEBUG
      printfn "%A" line; printfn "%A" line1; printfn "%A" line2;
#endif
      Array.empty
  let horizLines =
    grid
    |> Array.Parallel.mapi (fun row line -> row, (horizLinePos row line))
    |> Array.Parallel.map
      (fun (row, positions) ->
        (positions |> Array.map (fun (posStart, posEnd) ->
          posStart, row, posEnd, [| for col in [posStart .. posEnd] do yield grid.[row].[col] |] )))
    |> Array.concat
    |> Array.Parallel.map (fun (col, row, colEnd, chars) ->
      { orientation = (Horizontal)
        gridCoordStart = { col = col; row = row }
        gridCoordEnd = { col = colEnd; row = row }
        linechars = chars
        lineOptions = Map.empty })
  horizLines

let ScanLineVertically (grid : TxtGrid)  =
  let IsNotVertGlyph grid col row = not (IsVerticalGlyph grid col row)
  let testVertStart col row =
    let IsInLineSequence = (IsNotVertGlyph grid col (row - 1)) && (IsVerticalGlyph grid col row)
    let IsNextToCorner = (IsCornerKind grid col (row - 1)) && (IsVerticalGlyph grid col row)
    if row = 0 then (IsVerticalGlyph grid col row)
    else IsInLineSequence || IsNextToCorner
  let testVertEnd col row =
    let IsInLineSequence = (IsVerticalGlyph grid col row) && (IsNotVertGlyph grid col (row + 1))
    let IsNextToCorner = (IsVerticalGlyph grid col row) && (IsCornerKind grid col (row + 1))
    if row = (grid.Length - 1) then (IsVerticalGlyph grid col row)
    else IsInLineSequence || IsNextToCorner
  let gridT =
    [| for col in [0..grid.[0].Length-1] do yield [| for row in [0..grid.Length-1] do yield grid.[row].[col] |] |]
  let startPos =
    gridT
    |> Array.Parallel.mapi (fun col line -> line |> Array.mapi (fun row _ -> if testVertStart col row then row else -1))
    |> Array.Parallel.map (fun line -> line |> Array.filter (fun x -> x > -1))
  let endPos =
    gridT
    |> Array.Parallel.mapi (fun col line -> line |> Array.mapi (fun row _ -> if testVertEnd col row then row else -1))
    |> Array.Parallel.map (fun line -> line |> Array.filter (fun x -> x > -1))
  try matrixZip startPos endPos
  with
  | _ ->
#if DEBUG
      printfn "%A" startPos; printfn "%A" endPos
#endif
      [||]
  |> Array.Parallel.mapi (fun col line -> [| for i in [0..line.Length-1] do yield col,(fst line.[i]), (snd line.[i]) |])
  |> Array.concat
  |> Array.Parallel.map (fun (col, rowStart, rowEnd) ->
    { orientation = (Vertical)
      gridCoordStart = { col = col; row = rowStart }
      gridCoordEnd = { col = col; row = rowEnd }
      linechars = [|for i in [rowStart..rowEnd] do yield grid.[i].[col]|]
      lineOptions = Map.empty })

let ScanLine (grid : TxtGrid) : Line[] * Line[] = ScanLineHorizontally grid, ScanLineVertically grid