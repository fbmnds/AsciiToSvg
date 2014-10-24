AsciiToSvg
==========

A library for converting ASCII text files into SVG graphics

## What is it?

AsciiToSvg is a library intended to support authoring arbitrary content in plain text format. 
The inspiration of the approach was influenced by the workflow description 
of the project documentation process in the [ZeroMQ Guide][2].

The tool used by the ZeroMQ project to convert plain ASCII text input into SVG vector graphics is an 
[interesting tool set][3] consisting of a Perl wrapper script for a PHP port of a C Parser/Lexer based package. 

AsciiToSvg is a F# implementation of a tool that serves the same purpose as the Perl/PHP/C tool.

## How does it work?

AsciiToSvg is a conceptionally different from the forementioned implementation. 
A good entry point for examining the differences is the 
[pattern identification][4] in combination with the [line identification][5]
in contrast to the [multi-step parsing][6] with its ['generic, recursive line walker'][7] 
that needs to be supported by a [follow wall algorithm][8].

AsciiToSvg is conceptionally based on the notion of 'glyphs', i.e. symbols consisting of ASCII letter combinations, that are 
interpreted and rendered as SVG graphic elements like ['text'][9], 
['line'][10] and ['polygon'][11].

The concept include the possibility to include options in the ASCII input text. 
It resembles the idea as laid out in this example: 
[input](https://github.com/imatix/zguide/blob/master/bin/asciitosvg/logo.txt), 
[svg](https://github.com/imatix/zguide/blob/master/bin/asciitosvg/logo.svg).
This feature is not yet fully implemented but planned and will be explained later.

The steps to generate a SVG graphic are:

* [read the file into memory and separate the picture part from the options part](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/TxtFileTests.fs#L141)
* [make a trimmed grid, i.e. remove trailing spces and blank lines (important for proper scaling of the graphic)](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/TxtFileTests.fs#L178)
* [scan the ASCII input](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/GlyphScannerTests.fs#L54)
* [set the approriate SVG options (scaling)](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/GlyphRendererTests.fs#L63)
* [render the glyphs](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/GlyphRendererTests.fs#L69)
* [scan the text, use tabbed text scan option for proper positioning](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/TextScannerTests.fs#L34)
* [set consistent SVG options (scaling, text font)](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/TextRendererTests.fs#L44)
* [render the text](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/TextRendererTests.fs#L51)
* [scan the lines](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/LineScannerTests.fs#L63)
* [render the lines](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/LineRendererTests.fs#L47)
* [produce SVG](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/LineRendererTests.fs#L52)

## How does it look like?

A basic example is given by the following ASCII input file:

    [lang=text]
    +---------+-----------+------------------+------------------+
    | ArrowUp | ArrowDown | ArrowLeftToRight | ArrowRightToLeft |
    +---------+-----------+------------------+------------------+
    |  ^   ^  |    |   +  |        ->        |        <-        |
    |  |   +  |    v   v  |        +>        |        <+        |
    +---------+-----------+------------------+------------------+ 

The resulting SVG file looks like this:

<svg width="549px" height="90px" font-size="12" version="1.1"

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
  </defs>
        <text x="18.000" y="26.000" style="fill:black" font-family="Courier New" font-size="15.0">
 ArrowUp 
      </text>
      <text x="108.000" y="26.000" style="fill:black" font-family="Courier New" font-size="15.0">
 ArrowDown 
      </text>
      <text x="216.000" y="26.000" style="fill:black" font-family="Courier New" font-size="15.0">
 ArrowLeftToRight 
      </text>
      <text x="387.000" y="26.000" style="fill:black" font-family="Courier New" font-size="15.0">
 ArrowRightToLeft 
      </text>
      <line stroke="black" stroke-width="1" x1="198.000" y1="37.000" x2="206.000" y2="37.000" />
      <line stroke="black" stroke-width="1" x1="202.000" y1="30.000" x2="202.000" y2="44.000" />

      <line stroke="black" stroke-width="1" x1="198.000" y1="7.000" x2="206.000" y2="7.000" />
      <line stroke="black" stroke-width="1" x1="202.000" y1="7.000" x2="202.000" y2="14.000" />

      <line stroke="black" stroke-width="1" x1="198.000" y1="82.000" x2="206.000" y2="82.000" />
      <line stroke="black" stroke-width="1" x1="202.000" y1="82.000" x2="202.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="369.000" y1="37.000" x2="377.000" y2="37.000" />
      <line stroke="black" stroke-width="1" x1="373.000" y1="30.000" x2="373.000" y2="44.000" />

      <line stroke="black" stroke-width="1" x1="369.000" y1="7.000" x2="377.000" y2="7.000" />
      <line stroke="black" stroke-width="1" x1="373.000" y1="7.000" x2="373.000" y2="14.000" />

      <line stroke="black" stroke-width="1" x1="369.000" y1="82.000" x2="377.000" y2="82.000" />
      <line stroke="black" stroke-width="1" x1="373.000" y1="82.000" x2="373.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="4.000" y1="30.000" x2="4.000" y2="44.000" />
      <line stroke="black" stroke-width="1" x1="4.000" y1="37.000" x2="8.000" y2="37.000" />

      <line stroke="black" stroke-width="1" x1="4.000" y1="7.000" x2="4.000" y2="14.000" />
      <line stroke="black" stroke-width="1" x1="4.000" y1="7.000" x2="8.000" y2="7.000" />

      <line stroke="black" stroke-width="1" x1="4.000" y1="82.000" x2="4.000" y2="75.000" />
      <line stroke="black" stroke-width="1" x1="4.000" y1="82.000" x2="8.000" y2="82.000" />

      <line stroke="black" stroke-width="1" x1="544.000" y1="30.000" x2="544.000" y2="44.000" />
      <line stroke="black" stroke-width="1" x1="544.000" y1="37.000" x2="540.000" y2="37.000" />

      <line stroke="black" stroke-width="1" x1="544.000" y1="7.000" x2="540.000" y2="7.000" />
      <line stroke="black" stroke-width="1" x1="544.000" y1="7.000" x2="544.000" y2="14.000" />

      <line stroke="black" stroke-width="1" x1="544.000" y1="82.000" x2="540.000" y2="82.000" />
      <line stroke="black" stroke-width="1" x1="544.000" y1="82.000" x2="544.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="90.000" y1="37.000" x2="98.000" y2="37.000" />
      <line stroke="black" stroke-width="1" x1="94.000" y1="30.000" x2="94.000" y2="44.000" />

      <line stroke="black" stroke-width="1" x1="90.000" y1="7.000" x2="98.000" y2="7.000" />
      <line stroke="black" stroke-width="1" x1="94.000" y1="7.000" x2="94.000" y2="14.000" />

      <line stroke="black" stroke-width="1" x1="90.000" y1="82.000" x2="98.000" y2="82.000" />
      <line stroke="black" stroke-width="1" x1="94.000" y1="82.000" x2="94.000" y2="75.000" />

      <polygon fill="black" points="135.000,67.000 143.000,67.000 139.000,74.000 135.000,67.000" />
      <line stroke="black" stroke-width="1" x1="139.000" y1="67.000" x2="139.000" y2="60.000" />

      <polygon fill="black" points="171.000,67.000 179.000,67.000 175.000,74.000 171.000,67.000" />
      <line stroke="black" stroke-width="1" x1="175.000" y1="67.000" x2="175.000" y2="60.000" />

      <polygon fill="black" points="27.000,52.000 35.000,52.000 31.000,45.000 27.000,52.000" />
      <line stroke="black" stroke-width="1" x1="31.000" y1="52.000" x2="31.000" y2="59.000" />

      <polygon fill="black" points="289.000,48.000 289.000,56.000 296.000,52.000 289.000,48.000" />
      <line stroke="black" stroke-width="1" x1="289.000" y1="52.000" x2="288.000" y2="52.000" />

      <polygon fill="black" points="289.000,63.000 289.000,71.000 296.000,67.000 289.000,63.000" />
      <line stroke="black" stroke-width="1" x1="289.000" y1="67.000" x2="288.000" y2="67.000" />

      <polygon fill="black" points="457.000,48.000 457.000,56.000 450.000,52.000 457.000,48.000" />
      <line stroke="black" stroke-width="1" x1="457.000" y1="52.000" x2="458.000" y2="52.000" />

      <polygon fill="black" points="457.000,63.000 457.000,71.000 450.000,67.000 457.000,63.000" />
      <line stroke="black" stroke-width="1" x1="457.000" y1="67.000" x2="458.000" y2="67.000" />

      <polygon fill="black" points="63.000,52.000 71.000,52.000 67.000,45.000 63.000,52.000" />
      <line stroke="black" stroke-width="1" x1="67.000" y1="52.000" x2="67.000" y2="59.000" />

      <line stroke="black" stroke-width="1" x1="8.000" y1="7.000" x2="90.000" y2="7.000" />

      <line stroke="black" stroke-width="1" x1="98.000" y1="7.000" x2="198.000" y2="7.000" />

      <line stroke="black" stroke-width="1" x1="206.000" y1="7.000" x2="369.000" y2="7.000" />

      <line stroke="black" stroke-width="1" x1="377.000" y1="7.000" x2="540.000" y2="7.000" />

      <line stroke="black" stroke-width="1" x1="8.000" y1="37.000" x2="90.000" y2="37.000" />

      <line stroke="black" stroke-width="1" x1="98.000" y1="37.000" x2="198.000" y2="37.000" />

      <line stroke="black" stroke-width="1" x1="206.000" y1="37.000" x2="369.000" y2="37.000" />

      <line stroke="black" stroke-width="1" x1="377.000" y1="37.000" x2="540.000" y2="37.000" />

      <line stroke="black" stroke-width="1" x1="278.000" y1="52.000" x2="288.000" y2="52.000" />

      <line stroke="black" stroke-width="1" x1="458.000" y1="52.000" x2="468.000" y2="52.000" />

      <line stroke="black" stroke-width="1" x1="278.000" y1="67.000" x2="288.000" y2="67.000" />

      <line stroke="black" stroke-width="1" x1="458.000" y1="67.000" x2="468.000" y2="67.000" />

      <line stroke="black" stroke-width="1" x1="8.000" y1="82.000" x2="90.000" y2="82.000" />

      <line stroke="black" stroke-width="1" x1="98.000" y1="82.000" x2="198.000" y2="82.000" />

      <line stroke="black" stroke-width="1" x1="206.000" y1="82.000" x2="369.000" y2="82.000" />

      <line stroke="black" stroke-width="1" x1="377.000" y1="82.000" x2="540.000" y2="82.000" />

      <line stroke="black" stroke-width="1" x1="4.000" y1="14.000" x2="4.000" y2="30.000" />

      <line stroke="black" stroke-width="1" x1="4.000" y1="44.000" x2="4.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="31.000" y1="59.000" x2="31.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="67.000" y1="59.000" x2="67.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="94.000" y1="14.000" x2="94.000" y2="30.000" />

      <line stroke="black" stroke-width="1" x1="94.000" y1="44.000" x2="94.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="139.000" y1="44.000" x2="139.000" y2="60.000" />

      <line stroke="black" stroke-width="1" x1="175.000" y1="44.000" x2="175.000" y2="60.000" />

      <line stroke="black" stroke-width="1" x1="202.000" y1="14.000" x2="202.000" y2="30.000" />

      <line stroke="black" stroke-width="1" x1="202.000" y1="44.000" x2="202.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="373.000" y1="14.000" x2="373.000" y2="30.000" />

      <line stroke="black" stroke-width="1" x1="373.000" y1="44.000" x2="373.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="544.000" y1="14.000" x2="544.000" y2="30.000" />

      <line stroke="black" stroke-width="1" x1="544.000" y1="44.000" x2="544.000" y2="75.000" />

  </svg>


The usage of polygon shapes looks like this:

    [lang=text]
                |
                v
             -<-+------+
                |      |
   +            |      |
   +------------+---++-+
   |                ||
   |                ++
   |                ||
   |                ||
   +----------------++
                    ++

This input renders to:

<svg width="189px" height="180px" font-size="12" version="1.1"

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
  </defs>
        <line stroke="black" stroke-width="1" x1="117.000" y1="37.000" x2="125.000" y2="37.000" />
      <line stroke="black" stroke-width="1" x1="121.000" y1="37.000" x2="121.000" y2="44.000" />

      <line stroke="black" stroke-width="1" x1="117.000" y1="82.000" x2="125.000" y2="82.000" />
      <line stroke="black" stroke-width="1" x1="121.000" y1="82.000" x2="121.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="153.000" y1="157.000" x2="161.000" y2="157.000" />
      <line stroke="black" stroke-width="1" x1="157.000" y1="150.000" x2="157.000" y2="164.000" />

      <line stroke="black" stroke-width="1" x1="153.000" y1="82.000" x2="161.000" y2="82.000" />
      <line stroke="black" stroke-width="1" x1="157.000" y1="82.000" x2="157.000" y2="89.000" />

      <line stroke="black" stroke-width="1" x1="157.000" y1="105.000" x2="157.000" y2="119.000" />
      <line stroke="black" stroke-width="1" x1="157.000" y1="112.000" x2="161.000" y2="112.000" />

      <line stroke="black" stroke-width="1" x1="157.000" y1="172.000" x2="157.000" y2="165.000" />
      <line stroke="black" stroke-width="1" x1="157.000" y1="172.000" x2="161.000" y2="172.000" />

      <line stroke="black" stroke-width="1" x1="162.000" y1="82.000" x2="170.000" y2="82.000" />
      <line stroke="black" stroke-width="1" x1="166.000" y1="82.000" x2="166.000" y2="89.000" />

      <line stroke="black" stroke-width="1" x1="166.000" y1="105.000" x2="166.000" y2="119.000" />
      <line stroke="black" stroke-width="1" x1="166.000" y1="112.000" x2="162.000" y2="112.000" />

      <line stroke="black" stroke-width="1" x1="166.000" y1="150.000" x2="166.000" y2="164.000" />
      <line stroke="black" stroke-width="1" x1="166.000" y1="157.000" x2="162.000" y2="157.000" />

      <line stroke="black" stroke-width="1" x1="166.000" y1="172.000" x2="162.000" y2="172.000" />
      <line stroke="black" stroke-width="1" x1="166.000" y1="172.000" x2="166.000" y2="165.000" />

      <line stroke="black" stroke-width="1" x1="184.000" y1="37.000" x2="180.000" y2="37.000" />
      <line stroke="black" stroke-width="1" x1="184.000" y1="37.000" x2="184.000" y2="44.000" />

      <line stroke="black" stroke-width="1" x1="184.000" y1="82.000" x2="180.000" y2="82.000" />
      <line stroke="black" stroke-width="1" x1="184.000" y1="82.000" x2="184.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="4.000" y1="157.000" x2="4.000" y2="150.000" />
      <line stroke="black" stroke-width="1" x1="4.000" y1="157.000" x2="8.000" y2="157.000" />

      <line stroke="black" stroke-width="1" x1="4.000" y1="75.000" x2="4.000" y2="89.000" />
      <line stroke="black" stroke-width="1" x1="4.000" y1="82.000" x2="8.000" y2="82.000" />

      <polygon fill="black" points="106.000,33.000 106.000,41.000 99.000,37.000 106.000,33.000" />
      <line stroke="black" stroke-width="1" x1="106.000" y1="37.000" x2="107.000" y2="37.000" />

      <polygon fill="black" points="117.000,22.000 125.000,22.000 121.000,29.000 117.000,22.000" />
      <line stroke="black" stroke-width="1" x1="121.000" y1="22.000" x2="121.000" y2="15.000" />

      <line stroke="black" stroke-width="1" x1="107.000" y1="37.000" x2="117.000" y2="37.000" />

      <line stroke="black" stroke-width="1" x1="125.000" y1="37.000" x2="180.000" y2="37.000" />

      <line stroke="black" stroke-width="1" x1="8.000" y1="82.000" x2="117.000" y2="82.000" />

      <line stroke="black" stroke-width="1" x1="125.000" y1="82.000" x2="153.000" y2="82.000" />

      <line stroke="black" stroke-width="1" x1="170.000" y1="82.000" x2="180.000" y2="82.000" />

      <line stroke="black" stroke-width="1" x1="8.000" y1="157.000" x2="153.000" y2="157.000" />

      <line stroke="black" stroke-width="1" x1="4.000" y1="59.000" x2="4.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="4.000" y1="89.000" x2="4.000" y2="150.000" />

      <line stroke="black" stroke-width="1" x1="121.000" y1="-1.000" x2="121.000" y2="15.000" />

      <line stroke="black" stroke-width="1" x1="121.000" y1="44.000" x2="121.000" y2="75.000" />

      <line stroke="black" stroke-width="1" x1="157.000" y1="89.000" x2="157.000" y2="105.000" />

      <line stroke="black" stroke-width="1" x1="157.000" y1="119.000" x2="157.000" y2="150.000" />

      <line stroke="black" stroke-width="1" x1="166.000" y1="89.000" x2="166.000" y2="105.000" />

      <line stroke="black" stroke-width="1" x1="166.000" y1="119.000" x2="166.000" y2="150.000" />

      <line stroke="black" stroke-width="1" x1="184.000" y1="44.000" x2="184.000" y2="75.000" />

  </svg>                    
                    
## Library license

The library is available under Apache 2.0. For more information see the [License file][1] in the GitHub repository

 [1]: https://github.com/fbmnds/a2svg/blob/master/LICENSE
 [2]: http://zguide.zeromq.org/page:all#Removing-Friction
 [3]: https://github.com/imatix/zguide/tree/master/bin/asciitosvg
 [4]: https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg/GlyphScanner.fs
 [5]: https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg/LineScanner.fs
 [6]: https://github.com/imatix/zguide/blob/master/bin/asciitosvg/ASCIIToSVG.php#L1323
 [7]: https://github.com/imatix/zguide/blob/master/bin/asciitosvg/ASCIIToSVG.php#L1926
 [8]: https://github.com/imatix/zguide/blob/master/bin/asciitosvg/ASCIIToSVG.php#L2043
 [9]: http://www.w3.org/TR/SVG/text.html#TextElement
 [10]: http://www.w3.org/TR/SVG/shapes.html#LineElement
 [11]: http://www.w3.org/TR/SVG/shapes.html#PolygonElement