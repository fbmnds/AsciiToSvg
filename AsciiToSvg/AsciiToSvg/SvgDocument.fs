module AsciiToSvg.SvgDocument

let mutable FontSize = 12.0
let mutable FontOffsetWidth = 0.0
let mutable FontOffsetHeight = 11.0
let mutable GlyphWidth = 9.0
let mutable GlyphHeight = 15.0
let mutable CanvasWidth = 720.0  //  80 glyphs per line
let mutable CanvasHeight = 675.0 // 45 lines

let svgTemplateOpen =
  "<?xml version=\"1.0\" standalone=\"no\"?>" +
  "<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\"" +
  "  \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">" +
  "<!-- Created with ASCIIToSVG (https://github.com/fbmnds/a2svg) -->" +
  (sprintf "<svg width=\"%.0fpx\" height=\"%.0fpx\" font-size=\"%.1f\" version=\"1.1\"" CanvasWidth CanvasHeight FontSize) +
  """
    xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
    <defs>
      <filter id="dsFilterNoBlur" width="150%" height="150%">
        <feOffset result="offOut" in="SourceGraphic" dx="3" dy="3"/>
        <feColorMatrix result="matrixOut" in="offOut" type="matrix" values="0.2 0 0 0 0 0 0.2 0 0 0 0 0 0.2 0 0 0 0 0 1 0"/>
        <feBlend in="SourceGraphic" in2="matrixOut" mode="normal"/>
      </filter>
      <filter id="dsFilter" width="150%" height="150%">
        <feOffset result="offOut" in="SourceGraphic" dx="3" dy="3"/>
        <feColorMatrix result="matrixOut" in="offOut" type="matrix" values="0.2 0 0 0 0 0 0.2 0 0 0 0 0 0.2 0 0 0 0 0 1 0"/>
        <feGaussianBlur result="blurOut" in="matrixOut" stdDeviation="3"/>
        <feBlend in="SourceGraphic" in2="blurOut" mode="normal"/>
      </filter>
      <marker id="iPointer"
        viewBox="0 0 10 10" refX="5" refY="5"
        markerUnits="strokeWidth"
        markerWidth="8" markerHeight="7"
        orient="auto">
        <path d="M 10 0 L 10 10 L 0 5 z" />
      </marker>
      <marker id="Pointer"
        viewBox="0 0 10 10" refX="5" refY="5"
        markerUnits="strokeWidth"
        markerWidth="8" markerHeight="7"
        orient="auto">
        <path d="M 0 0 L 10 5 L 0 10 z" />
      </marker>
    </defs>
  """

let svgTemplateClose = "  </svg>"