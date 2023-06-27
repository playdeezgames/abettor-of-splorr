Public Interface IGameController
    Inherits ICommandHandler
    Inherits IRenderer
    Inherits ISfxHandler
    Inherits IWindowSizerizer
    Inherits IUpdatorator
    ReadOnly Property QuitRequested As Boolean
End Interface
