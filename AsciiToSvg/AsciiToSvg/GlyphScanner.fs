module AsciiToSvg.GlyphScanner


let IsGlyphByPattern (pattern: GlyphPattern) (txtGrid: TxtGrid) col row =
  pattern
  |> Array.map (fun x ->
    match x with
    | (_, Wildcard _) -> true
    | (coord, (Letter letter)) ->
      try
        txtGrid.[row+coord.row].[col+coord.col] = letter
      with _ -> false)
  |> Array.reduce (fun x y -> x && y)

let IsArrowUp (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter '^'))
      ({ col = 0; row = 1 }, (Letter '+'))|]
    [|({ col = 0; row = 0 }, (Letter '^'))
      ({ col = 0; row = 1 }, (Letter '|'))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsArrowDown (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter 'v'))
      ({ col = 0; row = -1 },(Letter '+'))|]
    [|({ col = 0; row = 0 }, (Letter 'v'))
      ({ col = 0; row = -1 }, (Letter '|'))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsArrowLeftToRight (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter '>'))
      ({ col = -1; row = 0 }, (Letter '+'))|]
    [|({ col = 0; row = 0 }, (Letter '>'))
      ({ col = -1; row = 0 }, (Letter '-'))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let IsArrowRightToLeft (txtGrid: TxtGrid) col row =
  [|[|({ col = 0; row = 0 }, (Letter '<'))
      ({ col = 1; row = 0 }, (Letter '+'))|]
    [|({ col = 0; row = 0 }, (Letter '<'))
      ({ col = 1; row = 0 }, (Letter '-'))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid col row)
  |> Array.reduce (fun x y -> x || y)

let ScanGrid (grid : TxtGrid) : GlyphKindProperties[] =
  let scan i j =
    let glyphProperty glyphKind : GlyphKindProperties =
     { glyphKind = glyphKind;
       gridCoord = { col = i; row = j };
       glyphOptions = Map.empty }
    if (IsArrowUp grid i j) then glyphProperty ArrowUp else
    if (IsArrowDown grid i j) then glyphProperty ArrowDown else
    if (IsArrowLeftToRight grid i j) then glyphProperty ArrowLeftToRight else
    if (IsArrowRightToLeft grid i j) then glyphProperty ArrowRightToLeft
    else
      glyphProperty Empty
  if grid.Length = 0 then [||]
  else
    grid
    |> Array.Parallel.mapi (fun row -> Array.mapi (fun col _ -> scan col row))
    |> Array.Parallel.mapi (fun row -> Array.filter (fun x -> x.glyphKind <> Empty))
    |> Array.concat

type ArrowUpScanner() = class

  interface IGlyphScanner with

    member x.Scan (grid : TxtGrid) : GlyphKindProperties[] =
        let scan letter i j =
            if (IsArrowUp grid i j) then
              [| { glyphKind = ArrowUp
                   gridCoord = { col = i; row = j }
                   glyphOptions = Map.empty } |]
            else
              [||]
        if grid.Length = 0 then [||]
        else
          grid
          |> Array.Parallel.mapi (fun i -> Array.mapi (fun j c -> scan c j i))
          |> Array.Parallel.map Array.concat
          |> Array.concat

end
