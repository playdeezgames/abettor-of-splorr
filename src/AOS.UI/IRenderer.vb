Public Interface IRenderer(Of THue As Structure)
    Sub Render(displayBuffer As IPixelSink(Of THue))
End Interface
