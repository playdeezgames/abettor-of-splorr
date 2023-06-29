Public Interface IGameController
    Inherits IRenderer
    Inherits ISfxHandler
    Inherits IWindowSizerizer
    Inherits IUpdatorator
    ReadOnly Property QuitRequested As Boolean
    Sub SaveConfig()
    Sub HandleCommand(cmd As String)
End Interface
