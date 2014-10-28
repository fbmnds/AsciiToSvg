module AsciiToSvg.Log


open System

/// ILogger log-to-console implementation
type ConsoleLogger() =
  let withColorContext backgroundColor foregroundColor f =
        let restoreBackgroundColor = Console.BackgroundColor
        let restoreForegroundColor = Console.ForegroundColor
        Console.BackgroundColor <- backgroundColor
        Console.ForegroundColor <- foregroundColor
        f
        Console.BackgroundColor <- restoreBackgroundColor
        Console.ForegroundColor <- restoreForegroundColor

  let printfColored bg fg format y = printf format y |> withColorContext bg fg

  let log level format y =
    match level with
    | Error -> printfColored Console.BackgroundColor ConsoleColor.DarkRed format y
    | Warning -> printfColored Console.BackgroundColor ConsoleColor.DarkYellow format y
    | Info -> printfColored Console.BackgroundColor Console.ForegroundColor format y

  interface ILogger with
    member x.Log level format y = log level format y
    member x.LogLine level format y = log level format y; printfn ""
    member x.Dispose() = ()


/// ILogger do-not-log implementation
type PseudoLogger() =
  interface ILogger with
    member x.Log _ _ _ = ()
    member x.LogLine _ _ _  = ()
    member x.Dispose() = ()

let consoleLogger = (new ConsoleLogger() :> ILogger)
let logError format parms = consoleLogger.Log LogLevel.Warning format parms
let logWarning format parms = consoleLogger.Log LogLevel.Warning format parms
let logInfo format parms = consoleLogger.LogLine LogLevel.Info format parms
