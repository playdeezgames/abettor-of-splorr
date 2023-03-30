Public Class Sprite(Of THue As Structure)
    Inherits OffscreenBuffer(Of THue)
    Public ReadOnly Property Width As Integer
    Public ReadOnly Property Height As Integer
    Public Sub New(lines As IReadOnlyList(Of String), transform As Func(Of Char, THue))
        MyBase.New((lines(0).Length, lines.Count))
        Width = lines(0).Length
        Height = lines.Count
        Dim y = 0
        For Each line In lines
            Dim x = 0
            For Each character In line
                SetPixel(x, y, transform(character))
                x += 1
            Next
            y += 1
        Next line
    End Sub

    Public Sub StretchTo(sink As IPixelSink(Of THue), position As (Integer, Integer), scale As (Integer, Integer), filter As Func(Of THue, Boolean))
        sink.Stretch(Me, (0, 0), position, (Width, Height), scale, filter)
    End Sub
End Class
