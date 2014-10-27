module AsciiToSvg.Json

// http://fssnip.net/8y

type token =
    | WhiteSpace
    | Symbol of char
    | StrToken of string
    | NumToken of float
    | BoolToken of bool

let bool = System.Boolean.Parse

let private unquote (s:string) = s.Substring(1,s.Length-2)

let toToken = function
    | Match @"^\s+" s -> s, WhiteSpace
    | Match @"^""[^""\\]*(?:\\.[^""\\]*)*""" s -> s, s |> unquote |> StrToken
    | Match @"^\{|^\}|^\[|^\]|^:|^," s -> s, s.[0] |> Symbol
    | Match @"^\d+(\.\d+)?|\.\d+" s -> s, s |> float |> NumToken
    | Match @"^true|false" s -> s, s |> bool |> BoolToken
    | _ as s -> invalidOp (sprintf "Unknown token: >%s<" s)

let tokenize s =
    let rec tokenize' index (s:string) =
        if index = s.Length then []
        else
            let next = s.Substring index
            let text, token = toToken next
            token :: tokenize' (index + text.Length) s
    tokenize' 0 s
    |> List.choose (function WhiteSpace -> None | t -> Some t)

let rec (|ValueRec|_|) = function
    | NumToken n::t -> Some(Number n, t)
    | BoolToken b::t -> Some(Boolean b, t)
    | StrToken s::t -> Some(JString s, t)
    | Symbol '['::ValuesRec(vs, Symbol ']'::t) -> Some(Array vs,t)
    | Symbol '{'::PairsRec(ps, Symbol '}'::t) -> Some(JObject ps,t)
    | [] -> Some(Null,[])
    | _ -> None
and (|ValuesRec|_|) = function
    | ValueRec(p,t) ->
        let rec aux p' = function
            | Symbol ','::ValueRec(p,t) -> aux (p::p') t
            | t -> p' |> List.rev,t
        Some(aux [p] t)
    | _ -> None
and (|PairRec|_|) = function
    | StrToken k::Symbol ':'::ValueRec(v,t) -> Some((k,v), t)
    | _ -> None
and (|PairsRec|_|) = function
    | PairRec(p,t) ->
        let rec aux p' = function
            | Symbol ','::PairRec(p,t) -> aux (p::p') t
            | t -> p' |> List.rev,t
        Some(aux [p] t)
    | _ -> None

let parse s =
  try
    tokenize s |> function
    | ValueRec(v,[]) -> v
    | _ -> failwith "Failed to parse JSON"
    |> Success
  with
  | _ as x -> appendError (sprintf "Failed to parse JSON") x (JsonParseError) List.empty