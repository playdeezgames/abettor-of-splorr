Public Class DisplayBuffer(Of THue As Structure)
    Implements IDisplayBuffer(Of THue)
    Private _texture As Texture2D
    Private _buffer As THue()
    Sub New(texture As Texture2D)
        _texture = texture
        ReDim _buffer(_texture.Width * _texture.Height - 1)
    End Sub

    Public Sub Commit() Implements IDisplayBuffer(Of THue).Commit
        _texture.SetData(_buffer)
    End Sub

    Public Sub SetPixel(x As Integer, y As Integer, hue As THue) Implements IDisplayBuffer(Of THue).SetPixel
        If x < 0 OrElse y < 0 OrElse x >= _texture.Width OrElse y >= _texture.Height Then
            Return
        End If
        _buffer(x + y * _texture.Width) = hue
    End Sub
End Class
