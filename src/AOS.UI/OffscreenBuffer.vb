Public Class OffscreenBuffer(Of THue As Structure)
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
    Const Zero = 0
    Public Shared Function Create(transform As Func(Of Char, THue), ParamArray lines As String()) As OffscreenBuffer(Of THue)
        Dim buffer As New OffscreenBuffer(Of THue)((lines.First.Length, lines.Length))
        For y = Zero To lines.Length - 1
            Dim line = lines(y)
            Dim x = Zero
            For Each character In line
                buffer.SetPixel(x, y, transform(character))
                x += 1
            Next
        Next
        Return buffer
    End Function
End Class
