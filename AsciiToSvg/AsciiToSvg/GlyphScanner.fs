module AsciiToSvg.GlyphScanner


type ArrowUpScanner() = class

  interface IGlyphScanner with

    override x.Scan (grid : TxtGrid) : Glyph[] =
      let scan letter i j =
          if letter <> '^' || (grid.[i+1].[j] <> '+' && grid.[i+1].[j] <> '|') then [||]
          else
            [| { letter = '^'
                 gridCoord = { row = i; col = j }
                 glyphOptions = Map.empty } |> ArrowUp |]
      if grid.Length = 0 then [||]
      else
        grid
        |> Array.mapi (fun i -> Array.mapi (fun j c -> scan c i j))
        |> Array.map Array.concat
        |> Array.concat

end