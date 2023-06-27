Public Interface IGameController(Of THue As Structure, TSfx)
    Inherits ICommandHandler
    Inherits IRenderer(Of THue)
    Inherits ISfxHandler(Of TSfx)
    Inherits IWindowSizerizer
    Inherits IUpdatorator
    ReadOnly Property QuitRequested As Boolean
End Interface
