module AsciiToSvg.TxtFile

open AsciiToSvg.Json

let TxtOptionRegex = regex @"^\[([^\]]+)\]:?\s+({[^}]+?})"
let isTxtOption str = (str =~ TxtOptionRegex)

let leftOffset (lines: string []) =
  if lines.Length = 0 then 0 else
  lines
  |> Array.filter (fun line -> line.Length <> 0 && line.TrimStart(' ').Length <> 0)
  |> Array.Parallel.map (fun line -> line.Length - line.TrimStart(' ').Length)
  |> Array.min

let trimWithOffset offset (lines: string[])  =
  if offset < 1 then lines |> Array.Parallel.map (fun line -> line.TrimEnd(' '))
  else
    lines
    |> Array.Parallel.map (fun line -> try line.Substring(offset).TrimEnd(' ') with _ -> line)

let framedSplitTxt lines =
  let getOptions (options : string []) =
    [| for o in options do
        yield TxtOptionRegex.Match(o).Groups.[1].Value, TxtOptionRegex.Match(o).Groups.[2].Value |]
  match lines with
  | Success lines ->
    lines
    |> Seq.groupBy (fun s -> isTxtOption s)
    |> Seq.map (fun (k, v) -> k, Array.ofSeq v)
    |> Array.ofSeq
    |> function
    | [| (false, ascii); (true, options) |] ->
      options |> getOptions,
      [| for line in ascii do yield sprintf " %s " line |]
    | [| (false, ascii) |] ->
      [||],
      [| for line in ascii do yield sprintf " %s " line |]
    | _ -> [||], [||]
  | _ -> [||], [||]

let splitTxt lines =
  let getOptions (options : string []) =
    [| for o in options do
        yield TxtOptionRegex.Match(o).Groups.[1].Value, TxtOptionRegex.Match(o).Groups.[2].Value |]
  match lines with
  | Success lines ->
    lines
    |> Seq.groupBy (fun s -> isTxtOption s)
    |> Seq.map (fun (k, v) -> k, Array.ofSeq v)
    |> Array.ofSeq
    |> function
    | [| (false, ascii); (true, options) |] ->
      options |> getOptions, ascii
    | [| (false, ascii) |] ->
      [||], ascii
    | _ -> [||], [||]
  | _ -> [||], [||]

let makeFramedGrid ascii : TxtGrid =
  let yLength = ascii |> Array.map String.length |> Array.max
  let fillArray (arr: string) = [| for i in [0..yLength-arr.Length] do yield ' ' |]
  let emptyLine = [| for i in [0..yLength] do yield ' ' |]
  [| for i in [0..ascii.Length+1] do
      yield
        if i = 0 || i = ascii.Length+1 then emptyLine
        else Array.concat [ascii.[i-1].ToCharArray(); (fillArray ascii.[i-1]) ]
  |]

let removeBlankTrimmedLines (lines: string[]) =
  lines
  |> Array.Parallel.map (fun line -> line.Length, line)
  |> Seq.ofArray
  |> Seq.skipWhile (fun (x, _) -> x < 1)
  |> Seq.takeWhile (fun _ -> true)
  |> Array.ofSeq
  |> Array.Parallel.map (fun (_, line) -> line)

let removeTrailingBlankTrimmedLines (lines: string[]) =
  lines |> Array.rev |> removeBlankTrimmedLines |> Array.rev

let fillingBlanks yLength (arr: string) = [| for i in [1..yLength-arr.Length] do yield ' ' |]

let makeTrimmedGrid (ascii: string[]) : TxtGrid =
  let ascii' =
    ascii
    |> Array.Parallel.map (fun line -> line.TrimEnd(' '))
    |> removeBlankTrimmedLines
    |> removeTrailingBlankTrimmedLines
    |> fun x -> trimWithOffset (leftOffset x) x
  let yLength = ascii' |> Array.Parallel.map String.length |> Array.max
  [| for i in [0..ascii'.Length-1] do yield Array.concat [ascii'.[i].ToCharArray(); (fillingBlanks yLength ascii'.[i]) ] |]

let replaceOptionInLine letter (option : string) line =
  let replacement =
    [| for i in [ 0..option.Length + 3 ] do
         yield letter |]
    |> fun x -> new string(x)
  (matchPositions (sprintf "-\[%s\]-" option) line) |> List.map (fun i -> i + 1),
  regex(sprintf "-\[%s\]-" option).Replace(line, replacement)

let replaceOptionInAscii letter (ascii : string []) (option : string) =
  ascii
  |> Array.Parallel.mapi (fun row line -> row, line, (replaceOptionInLine letter option line))
  |> Array.Parallel.map (fun (row, line, (cols, line')) ->
       if cols.Length = 0 then Seq.empty, line
       else (cols |> Seq.map (fun col -> row, col)), line')
  |> toTupleOfArrays

let parseOption (log: ILogger) (optionKey, optionValue) =
  optionValue
  |> parse
  |> function
  | Success value -> Some (optionKey, value)
  | _ as x -> log.LogLine LogLevel.Error "%A" x; None

let parseAllOptions (log: ILogger) (options: (string*string)[]) : SvgOption =
  options
  |> Array.Parallel.map (parseOption log)
  |> Array.choose id
  |> Map.ofArray

let makeGrid (ascii: string[]) : TxtGrid =
  let yLength = ascii |> Array.Parallel.map String.length |> Array.max
  [| for i in [0..ascii.Length-1] do yield Array.concat [ascii.[i].ToCharArray(); (fillingBlanks yLength ascii.[i]) ] |]
