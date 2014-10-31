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
let replaceAll pattern (substitute: string) target = (regex pattern).Replace(target, substitute)

let (|Match|_|) pattern input =
    let re = Regex.Match(input, pattern)
    if re.Success then Some re.Value else None

let (|Matches|_|) pattern input =
  let re = Regex(pattern).Matches(input)
  Some ( [ for x in re -> x.Index, x.Value ] )

let matchPositions pattern input =
  let re = Regex(pattern).Matches(input)
  [ for x in re -> x.Index ]

let toInt str =
   match System.Int32.TryParse(str) with
   | (true, int) -> int
   | _ -> 0

let toFloat str =
   match System.Double.TryParse(str) with
   | (true, dbl) -> dbl
   | _ -> 0.0

let toArrayOfTuples (x: 'T[])  =
  [|for j in [0..(x.Length/2)-1] do yield x.[2*j],x.[2*j+1]|];;

let toTupleOfArrays (x: ('T1*'T2)[]) =
  [| for pair in x do yield (fst pair) |>  Array.ofSeq |], [| for pair in x do yield (snd pair) |]

let matrixZip (A: 'T1[][]) (B: 'T2[][]) =
  [| for i in [0..A.Length-1] do yield Array.zip A.[i] B.[i] |]
