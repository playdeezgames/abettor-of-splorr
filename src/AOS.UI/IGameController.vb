Public Interface IGameController(Of THue, TCommand, TSfx)
    Inherits ICommandHandler(Of TCommand)
    Inherits IRenderer(Of THue)
    Inherits ISfxHandler(Of TSfx)
    Inherits IWindowSizerizer
    Inherits IUpdatorator
    ReadOnly Property QuitRequested As Boolean
End Interface
