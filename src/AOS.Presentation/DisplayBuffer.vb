﻿Friend Class DisplayBuffer
    Inherits BasePixelSink
    Implements IDisplayBuffer
    Private ReadOnly _texture As Texture2D
    Private ReadOnly _transform As Func(Of Integer, Color)
    Private ReadOnly _hueTable As IReadOnlyDictionary(Of Integer, Color)
    Protected _buffer As Color()
    Sub New(texture As Texture2D, transform As Func(Of Integer, Color), hueTable As IReadOnlyDictionary(Of Integer, Color))
        _texture = texture
        _transform = transform
        _hueTable = hueTable
        ReDim _buffer(_texture.Width * _texture.Height - 1)
    End Sub
    Public Sub Commit() Implements IDisplayBuffer.Commit
        _texture.SetData(_buffer)
    End Sub
    Const Zero = 0
    Public Overrides Sub SetPixel(x As Integer, y As Integer, hue As Integer) Implements IDisplayBuffer.SetPixel
        If x < Zero OrElse y < Zero OrElse x >= _texture.Width OrElse y >= _texture.Height Then
            Return
        End If
        _buffer(x + y * _texture.Width) = _transform(hue)
    End Sub
End Class
