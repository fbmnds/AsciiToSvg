﻿namespace AsciiToSvg

type TxtGrid = char[][]

type GridCoordinates = { col : int; row : int }

type SvgScale = { colsc : float; rowsc : float }

type SvgCoordinates = { colpx : float; rowpx : float }

type JsonValue =
    | Number of float
    | JString of string
    | Boolean of bool
    | Array of JsonValue list
    | JObject of (string * JsonValue) list
    | Null

type SvgOption = Map<string, JsonValue>

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
  | UpTick
  | DownTick
  //
// #region Not implemented
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
  | Ellipse
  | Circle
  //
// #endregion
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