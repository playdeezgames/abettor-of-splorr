Public Class OffscreenBuffer(Of THue)
    Inherits BasePixelSink(Of THue)
    Implements IPixelSource(Of THue)
    Private ReadOnly _size As (Integer, Integer)
    Private ReadOnly _buffer As THue()
    Sub New(size As (Integer, Integer))
        _size = size
        ReDim _buffer(_size.Item1 * _size.Item2 - 1)
    End Sub
    Public Overrides Sub SetPixel(x As Integer, y As Integer, hue As THue)
        _buffer(x + y * _size.Item1) = hue
    End Sub

    Public Function GetPixel(x As Integer, y As Integer) As THue Implements IPixelSource(Of THue).GetPixel
        Return _buffer(x + y * _size.Item1)
    End Function
End Class
