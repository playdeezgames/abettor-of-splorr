Public Interface IGameController(Of THue As Structure, TCommand, TSfx)
    Inherits ICommandHandler(Of TCommand)
    Inherits IRenderer(Of THue)
    Inherits ISfxHandler(Of TSfx)
    Inherits IWindowSizerizer
    Inherits IUpdatorator
End Interface
