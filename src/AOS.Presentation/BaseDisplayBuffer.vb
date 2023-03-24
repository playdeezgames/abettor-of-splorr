Public MustInherit Class BaseDisplayBuffer(Of THue, TStoredHue)
    Implements IDisplayBuffer(Of THue)
    Private ReadOnly _transform As Func(Of THue, TStoredHue)
    Private _size As (Integer, Integer)
    Protected _buffer As TStoredHue()
    Sub New(size As (Integer, Integer), transform As Func(Of THue, TStoredHue))
        _size = size
        _transform = transform
        ReDim _buffer(size.Item1 * size.Item2 - 1)
    End Sub

    Public MustOverride Sub Commit() Implements IDisplayBuffer(Of THue).Commit

    Public Sub SetPixel(x As Integer, y As Integer, hue As THue) Implements IDisplayBuffer(Of THue).SetPixel
        If x < 0 OrElse y < 0 OrElse x >= _size.Item1 OrElse y >= _size.Item2 Then
            Return
        End If
        _buffer(x + y * _size.Item1) = _transform(hue)
    End Sub
End Class
