Public MustInherit Class BasePixelSink(Of THue As Structure)
    Implements IPixelSink(Of THue)
    Public MustOverride Sub SetPixel(x As Integer, y As Integer, hue As THue) Implements IPixelSink(Of THue).SetPixel
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

    Public Sub Colorize(Of TSourceHue)(source As IPixelSource(Of TSourceHue), fromLocation As (Integer, Integer), toLocation As (Integer, Integer), size As (Integer, Integer), xform As Func(Of TSourceHue, THue?)) Implements IPixelSink(Of THue).Colorize
        For x = 0 To size.Item1 - 1
            For y = 0 To size.Item2 - 1
                Dim sourceHue = source.GetPixel(x + fromLocation.Item1, y + fromLocation.Item2)
                Dim destinationHue = xform(sourceHue)
                If destinationHue.HasValue Then
                    SetPixel(x + toLocation.Item1, y + toLocation.Item2, destinationHue.Value)
                End If
            Next
        Next
    End Sub
End Class
