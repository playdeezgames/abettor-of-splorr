Public Class DisplayBuffer(Of THue As Structure)
    Inherits BasePixelSink(Of THue)
    Implements IDisplayBuffer(Of THue)
    Private ReadOnly _texture As Texture2D
    Private ReadOnly _transform As Func(Of THue, Color)
    Protected _buffer As Color()
    Sub New(texture As Texture2D, transform As Func(Of THue, Color))
        _texture = texture
        _transform = transform
        ReDim _buffer(_texture.Width * _texture.Height - 1)
    End Sub
    Public Sub Commit() Implements IDisplayBuffer(Of THue).Commit
        _texture.SetData(_buffer)
    End Sub
    Public Overrides Sub SetPixel(x As Integer, y As Integer, hue As THue) Implements IDisplayBuffer(Of THue).SetPixel
        If x < 0 OrElse y < 0 OrElse x >= _texture.Width OrElse y >= _texture.Height Then
            Return
        End If
        _buffer(x + y * _texture.Width) = _transform(hue)
    End Sub
End Class
