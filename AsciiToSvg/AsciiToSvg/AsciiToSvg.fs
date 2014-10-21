namespace AsciiToSvg

type TxtGrid = char[][]

type GridCoordinates = { col : int; row : int }
type SvgCoordinates = { colpx : float; rowpx : float }

type SvgOption = Map<string, string>

type SvgScale = { colsc : float; rowsc : float }

type Glyph =
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
  | Ellipse
  | Circle
  //
  | Empty

type GlyphKindProperties =
  { glyphKind : Glyph
    gridCoord : GridCoordinates
    glyphOptions : SvgOption }

type GlyphLetter =
  | Letter of char
  | Wildcard

type GlyphPattern = (GridCoordinates * GlyphLetter)[]

type IGlyphScanner =
  abstract Scan :  TxtGrid -> GlyphKindProperties[]

type ScannerRepository = IGlyphScanner []

type IGlyphRenderer =
  abstract Render : SvgScale -> SvgOption -> GlyphKindProperties -> string option

type RendererRepository = Map<Glyph, IGlyphRenderer>

type SvgShape =
  | Glyph
  | Box
  | Line

type SvgComponent =
  | SvgShape
  | Text

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

type LogMessageBaseType =
    | String of string
    | Integer of int
    | Integer64 of int64
    | Float of float


type LogMessage = seq<LogMessageBaseType>

type ILogger =
    inherit System.IDisposable
    abstract member Log: string -> LogMessage -> unit

//#endregion