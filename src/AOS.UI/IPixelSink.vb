Public Interface IPixelSink(Of THue)
    Sub SetPixel(x As Integer, y As Integer, hue As THue)
    Sub Copy(
            source As IPixelSource(Of THue),
            fromLocation As (Integer, Integer),
            toLocation As (Integer, Integer),
            size As (Integer, Integer),
            filter As Func(Of THue, Boolean))
End Interface
