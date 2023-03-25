Public Class DisplayBuffer(Of THue)
    Implements IDisplayBuffer(Of THue)
    Private _texture As Texture2D
    Sub New(texture As Texture2D, transform As Func(Of THue, Color))
        _texture = texture
        _transform = transform
        ReDim _buffer(_texture.Width * _texture.Height - 1)
    End Sub

    Public Sub Commit() Implements IDisplayBuffer(Of THue).Commit
        _texture.SetData(_buffer)
    End Sub
    Private ReadOnly _transform As Func(Of THue, Color)
    Protected _buffer As Color()

    Public Sub SetPixel(x As Integer, y As Integer, hue As THue) Implements IDisplayBuffer(Of THue).SetPixel
        If x < 0 OrElse y < 0 OrElse x >= _texture.Width OrElse y >= _texture.Height Then
            Return
        End If
        _buffer(x + y * _texture.Width) = _transform(hue)
    End Sub

    Public Sub Copy(source As IPixelSource(Of THue), fromLocation As (Integer, Integer), toLocation As (Integer, Integer), size As (Integer, Integer), filter As Func(Of THue, Boolean)) Implements IPixelSink(Of THue).Copy
        For x = 0 To size.Item1 - 1
            For y = 0 To size.Item2 - 1
                Dim hue = source.GetPixel(x + fromLocation.Item1, y + fromLocation.Item2)
                If filter(hue) Then
                    SetPixel(x + toLocation.Item1, y + toLocation.Item2, hue)
                End If
            Next
        Next
    End Sub

    Public Sub Fill(location As (Integer, Integer), size As (Integer, Integer), hue As THue) Implements IPixelSink(Of THue).Fill
        For x = 0 To size.Item1 - 1
            For y = 0 To size.Item2 - 1
                SetPixel(location.Item1 + x, location.Item2 + y, hue)
            Next
        Next
    End Sub
End Class
