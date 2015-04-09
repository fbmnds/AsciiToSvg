AsciiToSvg
==========

A library for converting ASCII text files into SVG graphics

## What is it?

AsciiToSvg is a library intended to support authoring arbitrary content in plain text format. 
The inspiration of the approach was influenced by the workflow description 
of the project documentation process in the [ZeroMQ Guide][2].

The ZeroMQ project team  is using an  [interesting tool set][3] to convert plain ASCII text input 
into SVG vector graphics consisting of a Perl wrapper script for a PHP port of a C Parser/Lexer based package. 
Interestingly, the sourcecode comments contain a hint, that parts of this software is current use of the SQlite project, but I did not check this.

Devon H. O'Dell, the author of the Perl/PHP tool, pointed me to the [official site][18] here on Github, as the ZeroMQ fork is outdated and does not conttain the latest bug fixes. 

AsciiToSvg is a F# implementation that serves the same purpose as this Perl/PHP tool.

## Why another implementation?

The existing implementation comes with nice examples, especially the logo:

![OriginalAsciiToSvgLogo](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/TestPngFiles/OriginalAsciiToSvgLogo.png?raw=true)

The current version of the the F# implementation does not yet support color and shading options but this is work in progress.

While the existing tool serves serious doumentation purposes, it failed to render the first example that I tried.
The tool actually bailed out with an Out-of-Memory exception on this input:

``` text
printing.el version 6.9.3    ps-print.el version 7.3.5


Menu Layout
-----------

The `printing' menu (Tools/Printing or File/Print) has the following layout:

       +-----------------------------+
A   0  |   Printing Interface        |
       +-----------------------------+       +-A---------+     +-B------+
I   1  |   PostScript Preview       >|-------|Directory >|-----|1-up    |
    2  |   PostScript Print         >|---- A |Buffer    >|-- B |2-up    |
    3  |   PostScript Printer: name >|---- C |Region    >|-- B |4-up    |
       +-----------------------------+       |Mode      >|-- B |Other...|
II  4  |   Printify                 >|-----\ |File      >|--\  +--------+
    5  |   Print                    >|---\ | |Despool... |  |
    6  |   Text Printer: name       >|-\ | | +-----------+  |
       +-----------------------------+ | | | +---------+   +------------+
III 7  |[ ]Landscape                 | | | \-|Directory|   | No Prep... | Ia
    8  |[ ]Print Header              | | |   |Buffer   |   +------------+ Ib
    9  |[ ]Print Header Frame        | | |   |Region   |   |   name    >|- C
    10 |[ ]Line Number               | | |   +---------+   +------------+
    11 |[ ]Zebra Stripes             | | |   +---------+   |   1-up...  | Ic
    12 |[ ]Duplex                    | | \---|Directory|   |   2-up...  |
    13 |[ ]Tumble                    | \--\  |Buffer   |   |   4-up...  |
    14 |[ ]Upside-Down               |    |  |Region   |   |   Other... |
    15 |   Print All Pages          >|--\ |  |Mode     |   +------------+
       +-----------------------------+  | |  +---------+   |[ ]Landscape| Id
IV  16 |[ ]Spool Buffer              |  | |  +-C-------+   |[ ]Duplex   | Ie
    17 |[ ]Print with faces          |  | \--|( )name A|   |[ ]Tumble   | If
    18 |[ ]Print via Ghostscript     |  |    |( )name B|   +------------+
       +-----------------------------+  |    |...      |
V   19 |[ ]Auto Region               |  |    |(*)name  |
    20 |[ ]Auto Mode                 |  |    |...      |
    21 |[ ]Menu Lock                 |  |    +---------+   +--------------+
       +-----------------------------+  \------------------|(*)All Pages  |
VI  22 |   Customize                >|--- D  +-D------+    |( )Even Pages |
    23 |   Show Settings            >|-------|printing|    |( )Odd Pages  |
    24 |   Help                      |       |ps-print|    |( )Even Sheets|
       +-----------------------------+       |lpr     |    |( )Odd Sheets |
                                             +--------+    +--------------+

```

The F# implementation however produces an acceptable outcome on this example:

![EmacsPrintHelp](https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/TestPngFiles/EmacsPrintHelp.png?raw=true)

## How does it work?

AsciiToSvg is conceptionally different from the forementioned implementation. 
A good entry point for examining the differences is the 
[pattern identification][4] in combination with the [line identification][5] in F#
in contrast to the [multi-step parsing][6] with its ['generic, recursive line walker'][7] 
that needs to be supported by a [follow wall algorithm][8].

AsciiToSvg is based on the notion of 'glyphs', i.e. symbols consisting of ASCII letter combinations, that are 
interpreted and rendered as SVG graphic elements like ['text'][9], 
['line'][10] and ['polygon'][11].

The concept includes the possibility to annotate SVG options to the ASCII input text for the rendering process. 
It resembles the idea as applied for generating the logo ([input][12], 
[svg][13]).

The steps to generate a SVG graphic with the new F# tool are:

* read the ASCII text file into memory and separate the picture part from the options part
* make a trimmed grid, i.e. remove trailing spces and blank lines (important for proper scaling of the graphic)
* set consistent SVG options (scaling, text font)
* scan the ASCII input
* render the glyphs
* scan the text, use tabbed text scan option for proper positioning
* render the text
* scan the lines
* render the lines
* produce the SVG graphic

The [`EmacsPrintHelp.fsx`][15] script file demonstrates how to put everything together. 

## Why using AsciiToSvg instead of Graphiz, InkScape, ...?

It applies the same rationale why one would like to use Markdown instead of Microsoft Word, TeX/LaTeX, and alike.
It boils down to trading ease in workflow using automation capabilities versus feature richness and UI experience of the alternatives.

It also depends on your own personal attitude towards the outdated 70-ish retro look of ASCII graphics and the support of your editor of choice to draw them.
The library should therefore play well with Emacs/Org-Mode and Vim, but will provide significantly less fun in Visual Studio or Xcode.

## What are the caveats?

The library does not contain an abstration for SVG documents. For a feature set on par with the Perl/PHP/C implementation, this is also not necessary.

As mentioned above, the library does not yet provide the option mechanism that the original tool uses for annotating color and shading information.

Github does not support SVG redering in markup text for security reasons. There are some work arounds by using 3rd party websites mentioned on [SO][16]. 
This enforces however the need for another backend in order to convert the SVG format, for example into PNG, which compromises the ease of processing.

Drawing slanted lines and circle/ellipses is a challenge fo ASCII based graphics because of the limited resolution and the disproportion of width and height of ASCII characters. 
Hence, the F# logo is for example not a good candidate for this tool. It should however be fairly easy to inject arbitrary SVG shapes 
via some include directive into the forementioned option mechanism.

The sourcecode is undocumented and documentation other than this Readme file is an open issue that will be eventually closed. 
The ZeroMQ Guide provides a [guideline][17] on ASCII graphics that applies here as well to a some greater extent.

## Are there further examples?

For regression testing, the library comes with some examples that cover the current feature set. It is however not a fancy gallery.

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

The usage of polygon shapes, which is a big challenge for the Perl/PHP/C implementation, looks like this:

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
 [12]: https://github.com/imatix/zguide/blob/master/bin/asciitosvg/logo.txt
 [13]: https://github.com/imatix/zguide/blob/master/bin/asciitosvg/logo.svg
 [14]: https://github.com/tpetricek/FSharp.Formatting
 [15]: https://github.com/fbmnds/AsciiToSvg/blob/master/AsciiToSvg/AsciiToSvg.Tests/EmacsPrintHelp.fsx
 [16]: http://stackoverflow.com/questions/13808020/include-an-svg-hosted-on-github-in-markdown
 [17]: https://github.com/imatix/zguide/tree/master/bin/asciitosvg
 [18]: https://github.com/dhobsd/asciitosvg