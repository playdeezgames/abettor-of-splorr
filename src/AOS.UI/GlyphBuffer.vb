Public Class GlyphBuffer
    Inherits OffscreenBuffer(Of Boolean)
    Public ReadOnly Property Width As Integer
    Public ReadOnly Property Height As Integer
    Public Sub New(font As FontData, glyph As Char)
        MyBase.New((font.Glyphs(glyph).Width, font.Height))
        Height = font.Height
        Width = font.Glyphs(glyph).Width
        For Each row In font.Glyphs(glyph).Lines
            For Each column In row.Value
                SetPixel(column, row.Key, True)
            Next
        Next
    End Sub
    Public Sub CopyTo(Of THue As Structure)(
                                           sink As IPixelSink(Of THue),
                                           position As (Integer, Integer),
                                           hue As THue)
        sink.Colorize(Me, (0, 0), position, (Width, Height), Function(x) If(x, hue, Nothing))
    End Sub
End Class
