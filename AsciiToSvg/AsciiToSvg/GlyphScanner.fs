module AsciiToSvg.GlyphScanner


let IsGlyphByPattern (pattern: GlyphPattern) (txtGrid: TxtGrid) row col =
  pattern
  |> Array.Parallel.map (fun x ->
    match x with
    | (_, Wildcard _) -> true
    | (coord, (Letter letter)) ->
      try
        txtGrid.[row+coord.row].[col+coord.col] = letter
      with _ -> false)
  |> Array.reduce (fun x y -> x && y)

let IsArrowUp (txtGrid: TxtGrid) row col =
  [|[|({ row = 0; col = 0},(Letter '^'))
      ({ row = 1; col = 0},(Letter '+'))|]
    [|({ row = 0; col = 0},(Letter '^'))
      ({ row = 1; col = 0},(Letter '|'))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid row col)
  |> Array.reduce (fun x y -> x || y)

let IsArrowDown (txtGrid: TxtGrid) row col =
  [|[|({ row = 0; col = 0},(Letter 'v'))
      ({ row = -1; col = 0},(Letter '+'))|]
    [|({ row = 0; col = 0},(Letter 'v'))
      ({ row = -1; col = 0},(Letter '|'))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid row col)
  |> Array.reduce (fun x y -> x || y)

let IsArrowLeftToRight (txtGrid: TxtGrid) row col =
  [|[|({ row = 0; col = 0},(Letter '>'))
      ({ row = 0; col = -1},(Letter '+'))|]
    [|({ row = 0; col = 0},(Letter '>'))
      ({ row = 0; col = -1},(Letter '-'))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid row col)
  |> Array.reduce (fun x y -> x || y)

let IsArrowRightToLeft (txtGrid: TxtGrid) row col =
  [|[|({ row = 0; col = 0},(Letter '<'))
      ({ row = 0; col = 1},(Letter '+'))|]
    [|({ row = 0; col = 0},(Letter '<'))
      ({ row = 0; col = 1},(Letter '-'))|]|]
  |> Array.Parallel.map (fun pattern -> IsGlyphByPattern pattern txtGrid row col)
  |> Array.reduce (fun x y -> x || y)

let ScanGrid (grid : TxtGrid) : Glyph[] =
  let scan letter i j =
    let glyphProperty : GlyphKindProperties =
     { letter = letter;
       gridCoord = { row = i; col = j };
       glyphOptions = Map.empty }
    if (IsArrowUp grid i j) then [| glyphProperty |> ArrowUp |] else
    if (IsArrowDown grid i j) then [| glyphProperty |> ArrowDown |] else
    if (IsArrowLeftToRight grid i j) then [| glyphProperty |> ArrowLeftToRight |] else
    if (IsArrowRightToLeft grid i j) then [| glyphProperty |> ArrowRightToLeft |]
    else
      [||]
  if grid.Length = 0 then [||]
  else
    grid
    |> Array.Parallel.mapi (fun row -> Array.Parallel.mapi (fun col letter -> scan letter row col))
    |> Array.Parallel.map Array.concat
    |> Array.concat

type ArrowUpScanner() = class

  interface IGlyphScanner with

    member x.Scan (grid : TxtGrid) : Glyph[] =
        let scan letter i j =
            if (IsArrowUp grid i j) then
              [| { letter = '^'
                   gridCoord = { row = i; col = j }
                   glyphOptions = Map.empty } |> ArrowUp |]
            else
              [||]
        if grid.Length = 0 then [||]
        else
          grid
          |> Array.Parallel.mapi (fun i -> Array.mapi (fun j c -> scan c i j))
          |> Array.Parallel.map Array.concat
          |> Array.concat

end
