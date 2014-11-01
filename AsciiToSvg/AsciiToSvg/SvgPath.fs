module AsciiToSvg.SvgPath


open SvgDocument

type Data =
  | Ma of floats
  | Mr of floats
  | La of floats
  | Lr of floats
  | Va of float
  | Vr of float
  | Ha of float
  | Hr of float
  | Ca of floats * floats
  | Cr of floats * floats
  | Sa of floats * floats
  | Sr of floats * floats
  | Qa of floats * floats
  | Qr of floats * floats
  | Z
and floats = float * float

type Path = Data[]

let LeftToRightLineToPath (line: Line) : Path =
  // safe brute force estimate of data.Length
  let data = Array.create<Data> (2*line.linechars.Length) Z
  let rec lineToPath (line': char[]) i =
    if line'.Length = 0 then if i = 0 then [||] else data.[..i] 
    else
      match line'.[0], i, data.[i - 1] with
      | '-', 0, _ -> data.[0] <- Hr(GlyphWidth); lineToPath line'.[1..] 1
      | '-', _, Hr x -> data.[i - 1] <- Hr(x + GlyphWidth); lineToPath line'.[1..] i
      | '-', _, _ -> data.[i] <- Hr(GlyphWidth); lineToPath line'.[1..] (i + 1)
      | '.', _, _ -> 
        data.[i] <- Qr((GlyphWidthHalf, 0.0), (0.0, GlyphHeightHalf))
        data.[i + 1] <- Qr((0.0, -1.0 * GlyphHeightHalf), (GlyphWidthHalf, 0.0))
        lineToPath line'.[1..] (i + 2)
      | '\'', _, _ -> 
        data.[i] <- Qr((GlyphWidthHalf, 0.0), (0.0, -1.0 * GlyphHeightHalf))
        data.[i + 1] <- Qr((0.0, GlyphHeightHalf), (GlyphWidthHalf, 0.0))
        lineToPath line'.[1..] (i + 2)
      | _ -> Log.logError "Invalid SVG path at left-to-right line : %A" line; [||]
  lineToPath line.linechars 0

let RightToLeftLineToPath (line: Line) : Path =
  // safe brute force estimate of data.Length
  let data = Array.create<Data> (2*line.linechars.Length) Z
  let rec lineToPath (line': char[]) i =
    if line'.Length = 0 then if i = 0 then [||] else data.[..i] 
    else
      match line'.[0], i, data.[i - 1] with
      | '-', 0, _ -> data.[0] <- Hr(-1.0 * GlyphWidth); lineToPath line'.[1..] 1
      | '-', _, Hr x -> data.[i - 1] <- Hr(x - GlyphWidth); lineToPath line'.[1..] i
      | '-', _, _ -> data.[i] <- Hr(-1.0 * GlyphWidth); lineToPath line'.[1..] (i + 1)
      | '.', _, _ -> 
        data.[i] <- Qr((-1.0 * GlyphWidthHalf, 0.0), (0.0, GlyphHeightHalf))
        data.[i + 1] <- Qr((0.0, -1.0 * GlyphHeightHalf), (-1.0 * GlyphWidthHalf, 0.0))
        lineToPath line'.[1..] (i + 2)
      | '\'', _, _ -> 
        data.[i] <- Qr((-1.0 * GlyphWidthHalf, 0.0), (0.0, -1.0 * GlyphHeightHalf))
        data.[i + 1] <- Qr((0.0, GlyphHeightHalf), (-1.0 * GlyphWidthHalf, 0.0))
        lineToPath line'.[1..] (i + 2)
      | _ -> Log.logError "Invalid SVG path at right-to-left line : %A" line; [||]
  lineToPath (line.linechars |> Array.rev) 0

let DownwardsLineToPath (line: Line) : Path = [|Vr((float)line.linechars.Length * GlyphHeight)|]

let UpwardsLineToPath (line: Line) : Path = [|Vr(-1.0 * (float)line.linechars.Length * GlyphHeight)|]

let GlyphToLeftToRightPath (glyph: Glyph) : Path =
  match glyph.glyphKind with
  | UpperLeftAndRightCorner
  | LowerLeftAndRightCorner 
  | CrossCorner -> 
    [|Hr(GlyphWidth)|]
  | RoundUpperLeftAndRightCorner
  | DownTick
  | RoundCrossCorner -> 
    [|Qr((GlyphWidthHalf, 0.0), (0.0, GlyphHeightHalf));
      Qr((0.0, -1.0 * GlyphHeightHalf), (GlyphWidthHalf, 0.0))|]
  | RoundLowerLeftAndRightCorner
  | UpTick ->
    [|Qr((GlyphWidthHalf, 0.0), (0.0, -1.0 * GlyphHeightHalf))
      Qr((0.0, GlyphHeightHalf), (GlyphWidthHalf, 0.0))|]
  | LargeUpperLeftAndRightCorner
  | LargeLowerLeftAndRightCorner
  | LargeCrossCorner -> 
    [||] 
  | _ -> Log.logError "Invalid SVG path at left-to-right kind glyph : %A" glyph; [||]

let GlyphToDownwardsPath (glyph: Glyph) : Path =
  match glyph.glyphKind with
  | UpperAndLowerLeftCorner
  | UpperAndLowerRightCorner
  | CrossCorner ->
    [|Vr(GlyphHeight)|]
  | RoundUpperAndLowerLeftCorner ->
    [|Qr((0.0, GlyphHeightHalf), (GlyphWidthHalf, 0.0))
      Qr((-1.0 * GlyphWidthHalf, 0.0), (0.0, GlyphHeightHalf))|]
  | RoundUpperAndLowerRightCorner
  | RoundCrossCorner ->
    [|Qr((0.0, GlyphHeightHalf), (-1.0 * GlyphWidthHalf, 0.0))
      Qr((GlyphWidthHalf, 0.0), (0.0, GlyphHeightHalf))|]
  | LargeUpperAndLowerLeftCorner
  | LargeUpperAndLowerRightCorner
  | LargeCrossCorner ->
    [||]
  | _ -> Log.logError "Invalid SVG path at downwards kind glyph : %A" glyph; [||]

let GlyphToUpwardsPath (glyph: Glyph) : Path =
  match glyph.glyphKind with
  | UpperAndLowerLeftCorner
  | UpperAndLowerRightCorner
  | CrossCorner ->
    [|Vr(-1.0 * GlyphHeight)|]
  | RoundUpperAndLowerLeftCorner ->
    [|Qr((0.0, -1.0 * GlyphHeightHalf), (GlyphWidthHalf, 0.0))
      Qr((-1.0 * GlyphWidthHalf, 0.0), (0.0, -1.0 * GlyphHeightHalf))|]
  | RoundUpperAndLowerRightCorner
  | RoundCrossCorner ->
    [|Qr((0.0, -1.0 * GlyphHeightHalf), (-1.0 * GlyphWidthHalf, 0.0))
      Qr((GlyphWidthHalf, 0.0), (0.0, -1.0 * GlyphHeightHalf))|]
  | LargeUpperAndLowerLeftCorner
  | LargeUpperAndLowerRightCorner
  | LargeCrossCorner ->
    [||]
  | _ -> Log.logError "Invalid SVG path at upwards kind glyph : %A" glyph; [||]

type PathBox =
  { hUP : Glyph[] * Line[]
    vRP : Glyph[] * Line[]
    hLP : Glyph[] * Line[]
    vLP : Glyph[] * Line[] }
  member x.ToPath() =
    try 
      let pathOrigin = { col = (fst x.hUP).[0].gridCoord.col; row = (fst x.hUP).[0].gridCoord.row }
      [|[| Ma((float)pathOrigin.col * GlyphWidth + GlyphWidthHalf, (float)pathOrigin.row * GlyphHeight + GlyphHeightHalf) |]|]
      |> Array.concat
    with 
    | _ -> [||]
