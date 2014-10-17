namespace AsciiToSvg

type TxtGrid = char[][]

type GridCoordinates = { row : int; col : int }
type SvgCoordinates = { px : float; py : float }

type SvgOption = Map<string, string>

type SvgScale = { scx : float; scy : float }

type GlyphKindProperties =
  { letter : char
    gridCoord : GridCoordinates
    glyphOptions : SvgOption }

type Glyph =
  | ArrowUp of GlyphKindProperties
  | ArrowDown of GlyphKindProperties
  | ArrowLeftToRight of GlyphKindProperties
  | ArrowRightToLeft of GlyphKindProperties
  //
  | UpperLeftCorner of GlyphKindProperties
  | LowerLeftCorner of GlyphKindProperties
  | UpperRightCorner of GlyphKindProperties
  | LowerRightCorner of GlyphKindProperties
  //
  | UpperLeftRightCorner of GlyphKindProperties
  | LowerLeftRightCorner of GlyphKindProperties
  | UpperLowerRightCorner of GlyphKindProperties
  | UpperLowerLeftCorner of GlyphKindProperties
  | AllCorner of GlyphKindProperties
  //
  | RoundUpperLeftCorner of GlyphKindProperties
  | RoundLowerLeftCorner of GlyphKindProperties
  | RoundUpperRightCorner of GlyphKindProperties
  | RoundLowerRightCorner of GlyphKindProperties
  //
  | RoundUpperLeftRightCorner of GlyphKindProperties
  | RoundLowerLeftRightCorner of GlyphKindProperties
  | RoundUpperLowerRightCorner of GlyphKindProperties
  | RoundUpperLowerLeftCorner of GlyphKindProperties
  | RoundAllCorner of GlyphKindProperties
  //
  | DiamondLeftCorner of GlyphKindProperties
  | DiamondRightCorner of GlyphKindProperties
  | DiamondUpperCorner of GlyphKindProperties
  | DiamondLowerCorner of GlyphKindProperties
  //
  | DiamondUpperAndLeftCorner of GlyphKindProperties
  | DiamondUpperAndRightCorner of GlyphKindProperties
  | DiamondLowerAndLeftCorner of GlyphKindProperties
  | DiamondLowerAndRightCorner of GlyphKindProperties
  | DiamondAllCorner of GlyphKindProperties
  //
  | LargeUpperLeftCorner of GlyphKindProperties
  | LargeLowerLeftCorner of GlyphKindProperties
  | LargeUpperRightCorner of GlyphKindProperties
  | LargeLowerRightCorner of GlyphKindProperties
  //
  | LargeUpperLeftRightCorner of GlyphKindProperties
  | LargeLowerLeftRightCorner of GlyphKindProperties
  | LargeUpperLowerRightCorner of GlyphKindProperties
  | LargeUpperLowerLeftCorner of GlyphKindProperties
  | LargeAllCorner of GlyphKindProperties
  //
  | Ellipse of GlyphKindProperties
  | Circle of GlyphKindProperties

type IGlyphScanner =
  abstract Scan : TxtGrid -> Glyph[]

type ScannerRepository  = Map<char, IGlyphScanner>

type IGlyphRenderer =
  abstract Render : SvgScale -> SvgOption -> Glyph -> string

type RendererRepository = Map<Glyph, IGlyphRenderer>

type SvgShape =
  | Glyph
  | Box
  | Line

type SvgComponent =
  | SvgShape
  | Text

// Error handling

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


// Logging Interface

type LogMessageBaseType =
    | String of string
    | Integer of int
    | Integer64 of int64
    | Float of float


type LogMessage = seq<LogMessageBaseType>

type ILogger =
    inherit System.IDisposable
    abstract member Log: string -> LogMessage -> unit
