Public Interface IUIContext(Of TGameContext)
    ReadOnly Property Game As TGameContext
    Function Font(fontName As String) As Font
    Sub ShowStatusBar(displayBuffer As IPixelSink, font As Font, text As String, foreground As Integer, background As Integer)
    Sub ShowHeader(displayBuffer As IPixelSink, font As Font, text As String, foreground As Integer, background As Integer)
    ReadOnly Property ViewSize As (Integer, Integer)
    Sub ShowSplashContent(displayBuffer As IPixelSink, font As Font)
    Sub ShowAboutContent(displayBuffer As IPixelSink, font As Font)
    Function ControlsText(aButtonText As String, bButtonText As String) As String
    ReadOnly Property AvailableWindowSizes As IEnumerable(Of (Integer, Integer))
    Sub AbandonGame()
    Sub LoadGame(slot As Integer)
    Sub SaveGame(slot As Integer)
    Function DoesSlotExist(slot As Integer) As Boolean
End Interface
