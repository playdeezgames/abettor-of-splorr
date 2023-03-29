Public Module GameContext
    Public Const ViewWidth = 160
    Public Const ViewHeight = 90
    Public Function DrawHueMapper(colorIndex As Integer) As Hue
        Return CType(colorIndex, Hue)
    End Function
End Module
