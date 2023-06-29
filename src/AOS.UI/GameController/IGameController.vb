Public Interface IGameController
    Inherits ISfxHandler
    Inherits IWindowSizerizer
    Inherits IUpdatorator
    ReadOnly Property QuitRequested As Boolean
    Sub SaveConfig()
    Sub HandleCommand(cmd As String)
    Sub Render(displayBuffer As IPixelSink)
End Interface
