Public Interface IGameController
    Inherits ISfxHandler
    Inherits IWindowSizerizer
    ReadOnly Property QuitRequested As Boolean
    Sub SaveConfig()
    Sub HandleCommand(cmd As String)
    Sub Render(displayBuffer As IPixelSink)
    Sub Update(elapsedTime As TimeSpan)
End Interface
