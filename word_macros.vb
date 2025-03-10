
Sub AutoFitTables()

    Dim tbl As Table

    For Each tbl In ActiveDocument.Tables
        tbl.AutoFitBehavior wdAutoFitContent
    Next tbl

End Sub

Sub CCLLCFigures()
    Dim pgh As Paragraph

    For Each pgh In ActiveDocument.Paragraphs
        ' Images are part of the "InlineShapes"
        If pgh.Range.InlineShapes.Count > 0 Then
            pgh.Style = "CCLLC Figure"
        End If
    Next pgh

End Sub

Sub CCLLCTables()

    Dim tbl As Table


    For Each tbl In ActiveDocument.Tables
            If InStr(tbl.Cell(1, 1).Range.Text, "Field") Or InStr(tbl.Cell(1, 1).Range.Text, "Test") Then
                tbl.Style = "ccxtable"
            End If
    Next tbl

End Sub

Sub FigureCaption()
' This routine adds a new Figure caption, including period and space.
'
' Typically bound to Alt-c
'
'
    Selection.InsertCaption Label:="Figure", TitleAutoText:="InsertCaption1", _
        Title:="", Position:=wdCaptionPositionBelow, ExcludeLabel:=0
    Selection.TypeText Text:=". "
End Sub

Sub TableCaption()
' This routine adds a new Table caption, including period and space.
'
' Typically bound to Alt-t
'
'
    Selection.InsertCaption Label:="Table", TitleAutoText:="InsertCaption1", _
        Title:="", Position:=wdCaptionPositionAbove, ExcludeLabel:=0
    Selection.TypeText Text:=". "
End Sub

Sub UpdateCaptionStyling()
' This procedure searches through paragrpahs for those that have the
' default "Caption" style. It then checks for the word "Figure" or "Table" in
' text of that paragraph to apply a more specific caption style for either
' Figures or Tables. This is important, as usually tables have the caption placed above
' and figures have the caption placed below. The "Spacing After" property should be
' small for the table, and larger for the figure.

    Dim pgh As Paragraph

    For Each pgh In ActiveDocument.Paragraphs

        ' Caption is default style, "Image Caption" is from pandoc generated figure captions, "Table Caption" for pandoc generated tables
        If pgh.Style = "Caption" Or pgh.Style = "Image Caption" Or pgh.Style = "Table Caption" Then

            If InStr(pgh.Range.Text, "Figure") Then
                pgh.Style = "figure-caption"
            ElseIf InStr(pgh.Range.Text, "Table") Then
                pgh.Style = "table-caption"
            End If
        End If
    Next pgh


End Sub

Sub ClearExistingTableFormats()

    Dim tbl As Table

    For Each tbl In ActiveDocument.Tables
            tbl.Select
            Selection.ClearFormatting
    Next tbl
End Sub

Sub ListCustomKeyBindings()

    CustomizationContext = NormalTemplate

    For Each aKey In KeyBindings
     Selection.InsertAfter aKey.Command & vbTab & aKey.KeyString & vbCr
     Selection.Collapse Direction:=wdCollapseEnd
    Next aKey

End Sub


Sub SelectColumn()
'
' Macro2 Macro
'
'
    Selection.SelectColumn
End Sub

Sub CrossReferenceTable()
'
' This macro allows for fast cross referencing of tables
' USAGE:
'   Type number of table, select/highlight it
'   Run this macro
'
' Typically bound to Alt-o

Dim TableNumber As String
Dim TrimmedTableNubmer As String

TableNumber = Selection.Text

TrimmedTableNumber = Trim(TableNumber)


Selection.InsertCrossReference ReferenceType:="Table", ReferenceKind:= _
        wdOnlyLabelAndNumber, ReferenceItem:=TrimmedTableNumber, InsertAsHyperlink:=True, _
        IncludePosition:=False, SeparateNumbers:=False, SeparatorString:=" "

End Sub

Sub CrossReferenceFigure()
'
' This macro allows for fast cross referencing of figures
' USAGE:
'   Type number of figure, select/highlight it
'   Run this macro
'
' Typically bound to Alt-d (for [d]iagram)

Dim Number As String

FigureNumber = Selection.Text

Selection.InsertCrossReference ReferenceType:="Figure", ReferenceKind:= _
        wdOnlyLabelAndNumber, ReferenceItem:=FigureNumber, InsertAsHyperlink:=True, _
        IncludePosition:=False, SeparateNumbers:=False, SeparatorString:=" "

End Sub


Sub FixSectionNumbering()
'
' By default, Word has the inane behavior to restart page numbering
' on every section break.
'

Dim Section As Section
Dim Footer As HeaderFooter

For Each Section In ActiveDocument.Sections
    For Each Footer In Section.Footers

        Footer.PageNumbers.RestartNumberingAtSection = False

    Next Footer
Next Section

End Sub

Sub GenTable()
' Bound to CTRL-SHIFT-T
    Selection.ConvertToTable Separator:=wdSeparateByTabs, AutoFitBehavior:=wdAutoFitContent, AutoFit:=True
     With Selection.Tables(1)
        .Style = "ccxtable"
    End With
End Sub

Sub ReplaceFigureCaptions()
'
' This macro is for replacing the "dumb" captions coming
' from a pandoc generated file.
' IMPORTANT: The only way this works is by having a Figure caption
' in the clipboard, relying on the ^c
'

    Selection.Find.ClearFormatting
    Selection.Find.Style = ActiveDocument.Styles("Image Caption")
    With Selection.Find
        .Text = "Figure [0-9]{1,}:"
        .Replacement.Text = "^c"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute Replace:=wdReplaceAll


End Sub

Sub ReplaceTableCaptions()
'
' This macro is for replacing the "dumb" captions coming
' from a pandoc generated file.
' IMPORTANT: The only way this works is by having a Table caption
' in the clipboard, relying on the ^c
'

    Selection.Find.ClearFormatting
    Selection.Find.Style = ActiveDocument.Styles("Table Caption")
    With Selection.Find
        .Text = "Table [0-9]{1,}:"
        .Replacement.Text = "^c"
        .Forward = True
        .Wrap = wdFindContinue
        .Format = True
        .MatchCase = False
        .MatchWholeWord = False
        .MatchAllWordForms = False
        .MatchSoundsLike = False
        .MatchWildcards = True
    End With
    Selection.Find.Execute Replace:=wdReplaceAll


End Sub

Sub Level1AlphabeticStart()
'
' Level1Alphabetic Macro
'
'
    Selection.Fields.Add Range:=Selection.Range, Type:=wdFieldEmpty, _
        PreserveFormatting:=False
    Selection.TypeText Text:="seq level1 \r 1 \* ALPHABETIC"
    Selection.Fields.Update
    Selection.MoveRight Unit:=wdCharacter, Count:=1
    Selection.TypeText Text:="." & vbTab
End Sub

Sub Level1AlphabeticNext()
'
' Level1Alphabetic Macro
'
'
    Selection.Fields.Add Range:=Selection.Range, Type:=wdFieldEmpty, _
        PreserveFormatting:=False
    Selection.TypeText Text:="seq level1 \* ALPHABETIC"
    Selection.Fields.Update
    Selection.MoveRight Unit:=wdCharacter, Count:=1
    Selection.TypeText Text:="." & vbTab
End Sub


Sub Level2Start()
'
' Level1Alphabetic Macro
'
'
    Selection.Fields.Add Range:=Selection.Range, Type:=wdFieldEmpty, _
        PreserveFormatting:=False
    Selection.TypeText Text:="seq level2 \r 1"
    Selection.Fields.Update
    Selection.MoveRight Unit:=wdCharacter, Count:=1
    Selection.TypeText Text:="." & vbTab
End Sub

Sub Level2Next()
'
' Level1Alphabetic Macro
'
'
    Selection.Fields.Add Range:=Selection.Range, Type:=wdFieldEmpty, _
        PreserveFormatting:=False
    Selection.TypeText Text:="seq level2"
    Selection.Fields.Update
    Selection.MoveRight Unit:=wdCharacter, Count:=1
    Selection.TypeText Text:="." & vbTab
End Sub


Sub LevelN(levelNum As String, isStart As Boolean)
'
' Level1Alphabetic Macro
    Selection.Fields.Add Range:=Selection.Range, Type:=wdFieldEmpty, _
        PreserveFormatting:=False
    If isStart Then
        Selection.TypeText Text:="seq level" & levelNum & " \r 1"
    Else
        Selection.TypeText Text:="seq level" & levelNum
    End If

    Selection.Fields.Update
    Selection.MoveRight Unit:=wdCharacter, Count:=1
    Selection.TypeText Text:="." & vbTab
End Sub

Sub Level3Start()
    Call LevelN("3", True)
End Sub
Sub Level3Next()
    Call LevelN("3", False)
End Sub
Sub Level4Start()
    Call LevelN("4", True)
End Sub
Sub Level4Next()
    Call LevelN("4", False)
End Sub
Sub Level5Start()
    Call LevelN("5", True)
End Sub
Sub Level5Next()
    Call LevelN("5", False)
End Sub

Sub TocPrint()
    Dim pgh As Paragraph
    Dim text As String

    For Each pgh In ActiveDocument.Paragraphs
        If pgh.Style = "Heading 1" Then
            ' Remove final newline character
            text = text.Substring(0, text.Length - 1)
            text = Trim(pgh.Range.Text)
            text = text & vbTab & pgh.Range.Information(wdActiveEndPageNumber) & vbLf
            Selection.InsertAfter (text)
        End If
    Next pgh
End Sub

