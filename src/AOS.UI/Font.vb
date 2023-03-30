Public Class Font
    Private ReadOnly _glyphs As New Dictionary(Of Char, GlyphBuffer)
    Public ReadOnly Height As Integer
    Public Sub New(fontData As FontData)
        Height = fontData.Height
        For Each glyph In fontData.Glyphs.Keys
            _glyphs(glyph) = New GlyphBuffer(fontData, glyph)
        Next
    End Sub
End Class
