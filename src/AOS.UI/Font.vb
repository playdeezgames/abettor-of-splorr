﻿Public Class Font
    Private ReadOnly _glyphs As New Dictionary(Of Char, GlyphBuffer)
    Public ReadOnly Height As Integer
    Public Sub New(fontData As FontData)
        Height = fontData.Height
        For Each glyph In fontData.Glyphs.Keys
            _glyphs(glyph) = New GlyphBuffer(fontData, glyph)
        Next
    End Sub
    Public Sub WriteText(Of THue As Structure)(sink As IPixelSink(Of THue), position As (Integer, Integer), text As String, hue As THue)
        For Each character In text
            Dim buffer = _glyphs(character)
            buffer.CopyTo(sink, position, hue)
            position = (position.Item1 + buffer.Width, position.Item2)
        Next
    End Sub
End Class
