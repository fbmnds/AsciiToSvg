namespace AsciiToSvg

[<AutoOpen>]
module Results =

    let appendError label
                    (exn : System.Exception)
                    (msg : Error -> AsciiToSvgMessage)
                    (failureList : AsciiToSvgMessage list) =
        Failure  ( ((label |> (ErrorLabel), exn.Message |> (Stacktrace) ) |> msg) :: failureList )

    let setError label
                 (exn : System.Exception)
                 (msg : Error -> AsciiToSvgMessage) =
        appendError label exn msg []

    // mostly unused boilerplate code; cleanup deferred
    let messageToString message =
        let toString s (x : ErrorLabel) (y : Stacktrace) = sprintf "%s\n%A\n%A\n" s x y
        match message with
        // Http error messages
        | InvalidUrl                    -> "Invalid url"
        | SendDataError (x,y)           -> toString "Send data error" x y
        | GetResponseError (x,y)        -> toString "Get response error" x y
        | InvalidCredentials            -> "Invalid credentials"
        | HttpTimeOut                   -> "Http time out"
        // Other error messages
        | ReadSecretFileError (x,y)           -> toString "Read secret file error" x y
        | ReadFileError (x,y)                 -> toString "Read file error" x y
        | WriteFileError (x,y)                -> toString "Write file error" x y
        | ClusterUrlError (x,y)               -> toString "Cluster url error" x y
        | EncryptedCollectionParseError (x,y) -> toString "Encrypted collection parse error" x y
        | FirstCryptoKeyError (x,y)           -> toString "First crypto key error" x y
        | DecryptCryptoKeysError (x,y)        -> toString "Decrypt crypto keys error" x y
        | GetCryptoKeysFromStringError (x,y)  -> toString "Get crypto keys from string error" x y
        | GetCryptoKeysError (x,y)            -> toString "Get crypto keys error" x y
        | GetCryptoKeysFromFileError (x,y)    -> toString "Get crypto keys from file error" x y
        | DecryptCollectionError (x,y)        -> toString "Decrypt collections error" x y
        | GetBookmarksError (x,y)             -> toString "Get crypto keys error" x y
        | ParseMetaGlobalPayloadError (x,y)   -> toString "Parse meta global payload error" x y
        | ParseMetaGlobalError (x,y)          -> toString "Parse meta global error" x y
        | Base32DecodeError (x,y)             -> toString "base32Decode error" x y
        | CyclicBookmarkFolders (x,y)         -> toString "Cyclic bookmark folders error" x y
        | UnescapeJsonStringError(x,y)        -> toString "Received invalid escaped JSON-string" x y
        | InternetExplorerFavoritesRegistryError (x,y) ->
            toString "Registry read error for Internet Explorer 'Favorites' folder" x y
        | CreateDirectoryError (x,y)          -> toString "Failed to create directory" x y
        | GetPasswordsError (x,y)             -> toString "Get password error" x y

    let concatMessagesWith separator message =
        message
        |> List.map messageToString
        |> List.reduce (fun s1 s2 -> s1 + separator + s2)

    let bind switchFunction twoTrackInput =
        match twoTrackInput with
        | Success s -> switchFunction s
        | Failure f -> Failure f

    let bind2 (switchFunction : unit -> Result<unit>) twoTrackInput =
        match twoTrackInput with
        | Success () -> switchFunction ()
        | Failure f ->
            match switchFunction () with
            | Success () -> Failure f
            | Failure f' -> f' @ f |> Failure

    let map singleTrackFunction =
        bind (singleTrackFunction >> Success)

    let tee deadEndFunction oneTrackInput =
        deadEndFunction oneTrackInput
        oneTrackInput

    let (>>=) twoTrackInput switchFunction =
        bind switchFunction twoTrackInput

    let setOrFail result =
        match result with
        | Success result' -> result'
        | Failure result' -> failwith (sprintf "Failed to set result:\n %s" (concatMessagesWith "\n" result'))

    type MaybeBuilder() =
        member this.Zero() = Success None
        member this.Yield(x) = Success (Some x)
        member this.Bind(m, f) = bind f m
        member this.Return(x)  = Success x

    let maybe = MaybeBuilder()

