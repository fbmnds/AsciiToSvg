[<AutoOpen>]
module AsciiToSvg.Common

open System.IO
open System.Text.RegularExpressions



let readFile file =
  try
    file
    |> File.ReadAllLines
    |> Success
  with ex -> ReadFileError |> Results.setError (sprintf "Error while reading file '%s'" file) ex

let readFileAsText file =
  try
    file
    |> File.ReadAllText
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