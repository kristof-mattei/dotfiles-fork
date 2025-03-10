# Pandoc

## Syntax Highlighting

- Uses KDE syntax, definition [here](https://docs.kde.org/stable5/en/kate/katepart/highlight.html)

- Can use in Kate, possibly putting in this directory? From <https://github.com/jgm/highlighting-kate/blob/master/xml/maxima.xml>

  - `Usage: place maxima.xml in $KDEDIR/share/apps/katepart/syntax`

Based on this [page](https://api.kde.org/frameworks/syntax-highlighting/html/#syntax-definition-files),

For local user 	$HOME/.local/share/org.kde.syntax-highlighting/syntax/
For Flatpak packages 	$HOME/.var/app/package-name/data/org.kde.syntax-highlighting/syntax/
For Snap packages 	$HOME/snap/package-name/current/.local/share/org.kde.syntax-highlighting/syntax/
On Windows® 	&#37;USERPROFILE&#37;&#92;AppData&#92;Local&#92;org.kde.syntax-highlighting&#92;syntax&#92;
On macOS® 	$HOME/Library/Application Support/org.kde.syntax-highlighting/syntax/

Also described in <https://docs.kde.org/trunk5/en/kate/katepart/highlight.html>

## Tex target

Will need `\tightlist` command. Default definition is:

```tex
\providecommand{\tightlist}{\setlength{\itemsep}{0pt}\setlength{\parskip}{0pt}}
```

## Plain Text Target

`--to plain`

## Setting margins or other PDF/Latex options from command line

<https://unix.stackexchange.com/a/397495/296724>

```sh
pandoc -V geometry:"margin=1in, landscape"
```


## Templates

- Used with the `standalone` option.
- Syntax:

```
$--   // Comment
$......$ or ${  }   // Delimiters to put special stuff

Variables:
$foo$
$foo.bar.baz$
$foo_bar.baz-bim$
$ foo $
${foo}
${foo.bar.baz}
${foo_bar.baz-bim}
${ foo }

Conditionals:
$if(foo)$bar$endif$

$if(foo)$
  $foo$
$endif$

$if(foo)$
part one
$else$
part two
$endif$

${if(foo)}bar${endif}

${if(foo)}
  ${foo}
${endif}

${if(foo)}
${ foo.bar }
${else}
no foo!
${endif}

```
[For loops](https://pandoc.org/MANUAL.html#for-loops)

Pipes: Simple Transformations

- pairs
- uppercase
- lowercase
- length
- reverse
- first
- last
- rest
- allbutlast
- chomp
- nowrap
- alpha
- roman
- <left|center|right> n "leftborder" "rightborder"


## Columns in Presentations

```
# Use fenced divs

:::: columns

:::: column
Stuff
::::

:::: column
Stuff
::::
```

## Default MS Word Template

```
pandoc -o custom-reference.docx --print-default-data-file reference.docx
```

## Image Width

`![](file.jpg){ width=50% }`

