Public Interface IDisplayBuffer(Of THue)
    Sub Commit()
    Sub SetPixel(x As Integer, y As Integer, hue As THue)
End Interface
