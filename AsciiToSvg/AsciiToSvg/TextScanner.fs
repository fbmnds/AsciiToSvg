module AsciiToSvg.TextScanner

open AsciiToSvg.GlyphScanner

let ScanTextByChars (grid : TxtGrid) : Text[] =
  let scan col row =
    if IsNotGlyph grid col row then
      { text = grid.[row].[col].ToString()
        gridCoord = { col = col; row = row }
        glyphOptions = Map.empty }
    else
      { text = ""
        gridCoord = { col = col; row = row }
        glyphOptions = Map.empty }
  if grid.Length = 0 then [||]
  else
    grid
    |> Array.Parallel.mapi (fun row -> Array.mapi (fun col _ -> scan col row))
    |> Array.Parallel.mapi (fun row -> Array.filter (fun x -> x.text <> ""))
    |> Array.concat

let ScanText (grid : TxtGrid) (*: Text[]*) =
  let testStart col row =
    if col = 0 then (IsNotGlyph grid col row)
    else (IsGlyph grid (col - 1) row) && (IsNotGlyph grid col row)
  let testEnd col row =
    if col = (grid.[row].Length - 1) then (IsNotGlyph grid col row)
    else (IsNotGlyph grid col row) && (IsGlyph grid (col + 1) row)
  let textPos row (line: char[]) =
    Array.zip
      (line |> Array.Parallel.mapi (fun col _ -> if (testStart col row) then col else -1) |> Array.filter (fun col -> col > -1))
      (line |> Array.Parallel.mapi (fun col _ -> if (testEnd col row) then col else -1) |> Array.filter (fun col -> col > -1))
  grid
  |> Array.Parallel.mapi (fun row line -> row, (textPos row line))
  |> Array.Parallel.map
    (fun (row, positions) ->
      (positions |> Array.map (fun (posStart, posEnd) ->
        posStart, row, [| for col in [posStart .. posEnd] do yield grid.[row].[col] |] |> fun cs -> new string(cs))))
  |> Array.concat
  |> Array.filter (fun (_, _, str) -> not (str =~ (regex @"^\s*$")))
  |> Array.Parallel.map (fun (col, row, text) ->
    { text = text
      gridCoord = { col = col; row = row }
      glyphOptions = Map.empty })
