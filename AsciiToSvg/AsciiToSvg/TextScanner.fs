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

let ScanText (grid : TxtGrid) : Text[] =
  let testStart col row =
    if col = 0 then (IsNotGlyph grid col row)
    else (IsGlyph grid (col - 1) row) && (IsNotGlyph grid col row)
  let testEnd col row =
    if col = (grid.[row].Length - 1) then (IsNotGlyph grid col row)
    else (IsNotGlyph grid col row) && (IsGlyph grid (col + 1) row)
  let textPos row (line: char[]) =
    let line1 = line |> Array.Parallel.mapi (fun col _ -> if (testStart col row) then col else -1) |> Array.filter (fun col -> col > -1)
    let line2 = line |> Array.Parallel.mapi (fun col _ -> if (testEnd col row) then col else -1) |> Array.filter (fun col -> col > -1)
    if line1.Length = line2.Length then Array.zip line1 line2
    else
#if DEBUG
      printfn "%A" line; printfn "%A" line1; printfn "%A" line2;
#endif
      Array.empty
  grid
  |> Array.Parallel.mapi (fun row line -> row, (textPos row line))
  |> Array.Parallel.map
    (fun (row, positions) ->
      (positions |> Array.map (fun (posStart, posEnd) ->
        posStart, row, [| for col in [posStart .. posEnd] do yield grid.[row].[col] |] |> fun cs -> new string(cs))))
  |> Array.concat
  |> Array.filter (fun (_, _, str) -> not (str =~ (regex @"^\s*$")))
  |> Array.Parallel.map (fun (col, row, text) ->
    { text = text.TrimEnd(' ')
      gridCoord = { col = col; row = row }
      glyphOptions = Map.empty })

let textAtPosition (text: Text) (position: int*int) =
  let pos1 = (fst position)
  { text = text.text.Substring(pos1, (snd position) - pos1 + 1);
    gridCoord = { col = pos1 + text.gridCoord.col; row = text.gridCoord.row }
    glyphOptions = text.glyphOptions }

let shiftTextByPos (text: Text) (positions: (int*int)[]) =
  if positions.Length = 0 then [| text |] else
  positions
  |> Array.map (textAtPosition text)

let ScanTabbedText (grid : TxtGrid) : Text[] =
  let testStart (line: char[]) col =
    if line.Length < 3 then false else
    if col = 0 then line.[0] <> ' ' else
    if col = 1 then line.[0] = ' ' && line.[1] <> ' ' else
    line.[col-2] = ' ' && line.[col-1] = ' ' && line.[col] <> ' '
  let testEnd (line: char[]) col =
    if line.Length < 3 then false else
    if col < line.Length - 2 then line.[col] <> ' ' && line.[col+1] = ' ' && line.[col+2] = ' ' else
    if col < line.Length - 1 then line.[col] <> ' ' && line.[col+1] = ' '
    else line.[col] <> ' '
  let wordPos (line: char[]) =
    let line1 =
      line |> Array.mapi (fun col _ -> if (testStart line col) then col else -1) |> Array.filter (fun col -> col > -1)
    let line2 =
      line |> Array.mapi (fun col _ -> if (testEnd line col) then col else -1) |> Array.filter (fun col -> col > -1)
    if line1.Length = line2.Length then Array.zip line1 line2
    else
#if DEBUG
      printfn "%A" line; printfn "%A" line1; printfn "%A" line2;
#endif
      Array.empty
  grid
  |> ScanText
  |> Array.Parallel.map (fun text -> text, wordPos (text.text.ToCharArray()))
  |> Array.Parallel.map (fun (text, pos) -> shiftTextByPos text pos)
  |> Array.concat
