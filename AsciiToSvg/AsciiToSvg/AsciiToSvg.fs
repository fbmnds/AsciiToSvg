namespace AsciiToSvg

type TxtGrid = char[][]

[<CustomEquality; CustomComparison>]
type GridCoordinates =
  { col : int; row : int }
  member x.IsLeftOf (y: GridCoordinates) = (x.col + 1 = y.col) && (x.row = y.row)
  member x.IsRightOf (y: GridCoordinates) = (x.col - 1 = y.col) && (x.row = y.row)
  member x.IsAboveOf (y: GridCoordinates) =  (x.col = y.col) && (x.row + 1 = y.row)
  member x.IsBelowOf (y: GridCoordinates) =  (x.col = y.col) && (x.row - 1 = y.row)
  member x.IsHorizontalLeftOf (y: GridCoordinates) = (x.col < y.col) && (x.row = y.row)
  member x.IsHorizontalRightOf (y: GridCoordinates) = (x.col > y.col) && (x.row = y.row)
  member x.IsVerticalAboveOf (y: GridCoordinates) =  (x.col = y.col) && (x.row < y.row)
  member x.IsVerticalBelowOf (y: GridCoordinates) =  (x.col = y.col) && (x.row > y.row)
  override x.Equals(yobj) =
    match yobj with
    | :? GridCoordinates as y -> x.col = y.col && x.row = y.row
    | _ -> false
  override x.GetHashCode() = hash x
  interface System.IComparable with
    member x.CompareTo yobj =
      match yobj with
      | :? GridCoordinates as y ->
        if x.row < y.row then -1 else
        if x.col < y.col then -1 else
        if x.row = y.row && x.col = y.col then 0
        else 1
      | _ -> invalidArg "yobj" "cannot compare values of different types"

[<CustomEquality; CustomComparison>]
type SvgCoordinates =
  { colpx : float; rowpx : float }
  override x.Equals(yobj) =
    match yobj with
    | :? SvgCoordinates as y -> x.colpx = y.colpx && x.rowpx = y.rowpx
    | _ -> false
  override x.GetHashCode() = hash x
  interface System.IComparable with
    member x.CompareTo yobj =
      match yobj with
      | :? SvgCoordinates as y ->
        if x.rowpx < y.rowpx then -1 else
        if x.colpx < y.colpx then -1 else
        if x.rowpx = y.rowpx && x.colpx = y.colpx then 0
        else 1
      | _ -> invalidArg "yobj" "cannot compare values of different types"

type JsonValue =
    | Number of float
    | JString of string
    | Boolean of bool
    | Array of JsonValue list
    | JObject of (string * JsonValue) list
    | Null

type SvgOption = Map<string, JsonValue>

type SvgScale = { colsc : float; rowsc : float }

type GlyphKind =
  | ArrowUp
  | ArrowDown
  | ArrowLeftToRight
  | ArrowRightToLeft
  //
  | UpperLeftCorner
  | LowerLeftCorner
  | UpperRightCorner
  | LowerRightCorner
  //
  | UpperLeftAndRightCorner
  | LowerLeftAndRightCorner
  | UpperAndLowerRightCorner
  | UpperAndLowerLeftCorner
  | CrossCorner
  //
  | RoundUpperLeftCorner
  | RoundLowerLeftCorner
  | RoundUpperRightCorner
  | RoundLowerRightCorner
  //
  | RoundUpperLeftAndRightCorner
  | RoundLowerLeftAndRightCorner
  | RoundUpperAndLowerRightCorner
  | RoundUpperAndLowerLeftCorner
  | RoundCrossCorner
  //
  | DiamondLeftCorner
  | DiamondRightCorner
  | DiamondUpperCorner
  | DiamondLowerCorner
  //
  | DiamondUpperAndLeftCorner
  | DiamondUpperAndRightCorner
  | DiamondLowerAndLeftCorner
  | DiamondLowerAndRightCorner
  | DiamondCrossCorner
  //
  | LargeUpperLeftCorner
  | LargeLowerLeftCorner
  | LargeUpperRightCorner
  | LargeLowerRightCorner
  //
  | LargeUpperLeftAndRightCorner
  | LargeLowerLeftAndRightCorner
  | LargeUpperAndLowerRightCorner
  | LargeUpperAndLowerLeftCorner
  | LargeCrossCorner
  //
  | UpTick
  | DownTick
  //
  | Ellipse
  | Circle
  //
  | Empty

type Glyph =
  { glyphKind : GlyphKind
    gridCoord : GridCoordinates
    glyphOptions : SvgOption }

type GlyphLetter =
  | Letter of char[]
  | Wildcard

type GlyphPattern = (GridCoordinates * GlyphLetter)[]

type Text =
  { text : string
    gridCoord : GridCoordinates
    glyphOptions : SvgOption }

type Orientation =
  | Vertical
  | Horizontal

type Line =
  { orientation : Orientation
    gridCoordStart : GridCoordinates
    gridCoordEnd : GridCoordinates
    linechars : char[]
    lineOptions : SvgOption }
  member x.Contains (coord: GridCoordinates) =
    (x.gridCoordStart.IsHorizontalLeftOf coord && x.gridCoordEnd.IsHorizontalRightOf coord) ||
    (x.gridCoordStart.IsVerticalAboveOf coord && x.gridCoordEnd.IsVerticalBelowOf coord)

type SvgElement =
  | Glyph of Glyph
  | Box
  | Line
  | Text of Text

// #region Error handling

type ErrorLabel = ErrorLabel of string
type Stacktrace = Stacktrace of string
type Error = ErrorLabel * Stacktrace

type AsciiToSvgMessage =
    // Http error messages
    | InvalidUrl
    | SendDataError of Error
    | GetResponseError of Error
    | InvalidCredentials
    | HttpTimeOut
    // Other error messages
    | JsonParseError of Error
    | ReadSecretFileError of Error
    | ReadFileError of Error
    | WriteFileError of Error
    | ClusterUrlError of Error
    | EncryptedCollectionParseError of Error
    | FirstCryptoKeyError of Error
    | DecryptCryptoKeysError of Error
    | GetCryptoKeysFromStringError of Error
    | GetCryptoKeysError of Error
    | GetCryptoKeysFromFileError of Error
    | ParseMetaGlobalError of Error
    | Base32DecodeError of Error
    // Firefox collections errors
    | DecryptCollectionError of Error
    | GetBookmarksError of Error
    | ParseMetaGlobalPayloadError of Error
    | CyclicBookmarkFolders of Error
    | UnescapeJsonStringError of Error
    | GetPasswordsError of Error
    // InternetExplorer bookmark error
    | InternetExplorerFavoritesRegistryError of Error
    | CreateDirectoryError of Error

type Result<'TEntity> =
    | Success of 'TEntity
    | Failure of AsciiToSvgMessage list

// #endregion

// #region Logging Interface

type LogLevel =
    | Error
    | Warning
    | Info

type ILogger =
    inherit System.IDisposable
    abstract member Log: LogLevel -> Printf.StringFormat<'a, unit> -> 'a
    abstract member LogLine: LogLevel -> Printf.StringFormat<'a, unit> -> 'a

//#endregion