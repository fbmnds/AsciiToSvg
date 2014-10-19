[<AutoOpen>]
module AsciiToSvg.TxtFile

open System.IO
open System.Text.RegularExpressions

let readFile file =
  try
    file
    |> File.ReadAllLines
    |> Success
  with ex -> ReadFileError |> Results.setError (sprintf "Error while reading file '%s'" file) ex

let regex s = new Regex(s)
let (=~) s (re : Regex) = re.IsMatch(s)

let (|Matches|_|) pattern input =
  let re = Regex(pattern).Matches(input)
  Some ( [ for x in re -> x.Index, x.Value ] )

let matchPositions pattern input =
  let re = Regex(pattern).Matches(input)
  [ for x in re -> x.Index-1 ]

let TxtOptionRegex = regex @"^\[([^\]]+)\]:?\s+({[^}]+?})"
let isTxtOption str = (str =~ TxtOptionRegex)

let leftOffset lines =
  let re = regex@"\s*[^\s]"
  lines
  |> Array.Parallel.map (fun line -> re.Match(line).Groups.[0].Value.Length - 1)
  |> Array.filter (fun x -> x > 0)
  |> fun x -> if x.Length = 0 then 0 else Array.min x

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

let makeFramedGrid ascii =
  let yLength = ascii |> Array.map String.length |> Array.max
  let fillArray (arr: string) = [| for i in [0..yLength-arr.Length] do yield ' ' |]
  let emptyLine = [| for i in [0..yLength] do yield ' ' |]
  [| for i in [0..ascii.Length+1] do
      yield
        if i = 0 || i = ascii.Length+1 then emptyLine
        else Array.concat [ascii.[i-1].ToCharArray(); (fillArray ascii.[i-1]) ]
  |]

let makeTrimmedGrid (ascii': string[]) =
  let offset = leftOffset ascii'
  let ascii =
    ascii'
    |> Array.Parallel.map (fun line -> line.TrimEnd(' '))
    |> Seq.ofArray |> Seq.skipWhile (fun x -> x.Length < 1) |> Seq.takeWhile (fun x -> x.Length > 0) |> Array.ofSeq
    |> trimWithOffset offset
  let yLength =
    ascii
    |> Array.Parallel.map String.length |> Array.max
  let fillArray (arr: string) = [| for i in [1..yLength-arr.Length] do yield ' ' |]
  [| for i in [0..ascii.Length-1] do yield Array.concat [ascii.[i].ToCharArray(); (fillArray ascii.[i]) ] |]

let replaceOption letter (option: string) ascii =
  let replacement = [| for i in [0..option.Length+3] do yield letter |] |> fun x -> new string (x)
  matchPositions option ascii, regex(sprintf "-\[%s\]-" option).Replace(ascii, replacement)
