Public Interface IGameController(Of THue As Structure)
    Inherits ICommandHandler
    Inherits IRenderer(Of THue)
    Inherits ISfxHandler
    Inherits IWindowSizerizer
    Inherits IUpdatorator
    ReadOnly Property QuitRequested As Boolean
End Interface
