module AsciiToSvg.LineScanner

open AsciiToSvg.GlyphScanner


let ScanLineHorizontally (grid : TxtGrid) : Line[] =
  let IsNotHorizGlyph grid col row = not(IsHorizontalGlyph grid col row || IsCrossGlyph grid col row)
  let testHorizStart col row =
    if col = 0 then (IsHorizontalGlyph grid col row)
    else (IsNotHorizGlyph grid (col - 1) row) && (IsHorizontalGlyph grid col row)
  let testHorizEnd col row =
    if col = (grid.[row].Length - 1) then (IsHorizontalGlyph grid col row)
    else (IsHorizontalGlyph grid col row) && (IsNotHorizGlyph grid (col + 1) row)
  let horizLinePos row (line: char[]) =
    Array.zip
      (line |> Array.Parallel.mapi (fun col _ -> if (testHorizStart col row) then col else -1) |> Array.filter (fun col -> col > -1))
      (line |> Array.Parallel.mapi (fun col _ -> if (testHorizEnd col row) then col else -1) |> Array.filter (fun col -> col > -1))
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
        gridCorrdStart = { col = col; row = row }
        gridCorrdEnd = { col = colEnd; row = row }
        linechars = chars
        lineOptions = Map.empty })
  horizLines

let ScanLineVertically (grid : TxtGrid) : Line[] =
  let gridT =
    [| for col in [0..grid.[0].Length-1] do yield [| for row in [0..grid.Length-1] do yield grid.[row].[col] |] |]
  let IsNotVertGlyph grid col row = not(IsVerticalGlyph grid col row || IsCrossGlyph grid col row)
  let testVertStart col row =
    if row = 0 then (IsVerticalGlyph grid row col)
    else (IsNotVertGlyph grid (row - 1) col) && (IsVerticalGlyph grid row col)
  let testVertEnd col row =
    if row = (gridT.Length - 1) then (IsVerticalGlyph grid row col)
    else (IsVerticalGlyph grid row col) && (IsNotVertGlyph grid (row + 1) col)
  let vertLinePos row (line: char[]) =
    Array.zip
      (line |> Array.Parallel.mapi (fun col _ -> if (testVertStart col row) then col else -1) |> Array.filter (fun col -> col > -1))
      (line |> Array.Parallel.mapi (fun col _ -> if (testVertEnd col row) then col else -1) |> Array.filter (fun col -> col > -1))
  let vertLines =
    gridT
    |> Array.Parallel.mapi (fun row line -> row, (vertLinePos row line))
    |> Array.Parallel.map
      (fun (row, positions) ->
        (positions |> Array.map (fun (posStart, posEnd) ->
          posStart, row, posEnd, [| for col in [posStart .. posEnd] do yield gridT.[row].[col] |] )))
    |> Array.concat
    |> Array.Parallel.map (fun (col, row, colEnd, chars) ->
      { orientation = (Vertical)
        gridCorrdStart = { col = col; row = row }
        gridCorrdEnd = { col = colEnd; row = row }
        linechars = chars
        lineOptions = Map.empty })
  vertLines

let ScanLine (grid : TxtGrid) : Line[] * Line[] = ScanLineHorizontally grid, ScanLineVertically grid
