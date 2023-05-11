Public Interface IPixelSink(Of THue)
    Sub SetPixel(x As Integer, y As Integer, hue As THue)
    Sub Stretch(
            source As IPixelSource(Of THue),
            fromLocation As (Integer, Integer),
            toLocation As (Integer, Integer),
            size As (Integer, Integer),
            scale As (Integer, Integer),
            filter As Func(Of THue, Boolean))
    Sub Colorize(Of TSourceHue)(
                               source As IPixelSource(Of TSourceHue),
                               fromLocation As (Integer, Integer),
                               toLocation As (Integer, Integer),
                               size As (Integer, Integer),
                               xform As Func(Of TSourceHue, THue))
    Sub Fill(location As (Integer, Integer), size As (Integer, Integer), hue As THue)
    Sub Frame(location As (Integer, Integer), size As (Integer, Integer), hue As THue)
End Interface
