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
It is potentially a nifty enhancement for the [FSharp.Formatting](https://github.com/tpetricek/FSharp.Formatting) tool.

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

![ArrowGlyphsWithFrame](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/TestPngFiles/ArrowGlyphsWithFrame.png?raw=true)


The usage of polygon shapes looks like this:

``` text
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
```

This input renders to:

![TestPolygonBox](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/TestPngFiles/TestPolygonBox.png?raw=true)
                    
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