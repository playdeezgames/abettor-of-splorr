Public Interface IUIContext
    Function Font(fontName As String) As Font
    Sub ShowStatusBar(displayBuffer As IPixelSink, font As Font, text As String, foreground As Integer, background As Integer)
    ReadOnly Property ViewSize As (Integer, Integer)
    Sub ShowSplashContent(displayBuffer As IPixelSink, font As Font)
End Interface
