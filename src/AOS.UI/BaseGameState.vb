Public MustInherit Class BaseGameState(Of THue As Structure, TCommand, TSfx, TState)
    Implements IGameController(Of THue, TCommand, TSfx)

    Protected ReadOnly Property Parent As IGameController(Of THue, TCommand, TSfx)
    Protected ReadOnly SetState As Action(Of TState)
    Sub New(parent As IGameController(Of THue, TCommand, TSfx), setState As Action(Of TState))
        Me.Parent = parent
        Me.SetState = setState
    End Sub
    Public Property Volume As Single Implements ISfxHandler(Of TSfx).Volume
        Get
            Return Parent.Volume
        End Get
        Set(value As Single)
            Parent.Volume = value
        End Set
    End Property
    Public Property Size As (Integer, Integer) Implements IWindowSizerizer.Size
        Get
            Return Parent.Size
        End Get
        Set(value As (Integer, Integer))
            Parent.Size = value
        End Set
    End Property

    Public Property QuitRequested As Boolean Implements IGameController(Of THue, TCommand, TSfx).QuitRequested
        Get
            Return Parent.QuitRequested
        End Get
        Set(value As Boolean)
            Parent.QuitRequested = value
        End Set
    End Property

    Public MustOverride Sub HandleCommand(command As TCommand) Implements ICommandHandler(Of TCommand).HandleCommand
    Public MustOverride Sub Render(displayBuffer As IPixelSink(Of THue)) Implements IRenderer(Of THue).Render
    Public Sub SetSfxHook(handler As Action(Of TSfx)) Implements ISfxHandler(Of TSfx).SetSfxHook
        Parent.SetSfxHook(handler)
    End Sub
    Public Sub PlaySfx(sfx As TSfx) Implements ISfxHandler(Of TSfx).PlaySfx
        Parent.PlaySfx(sfx)
    End Sub
    Public Sub SetSizeHook(hook As Action(Of (Integer, Integer))) Implements IWindowSizerizer.SetSizeHook
        Parent.SetSizeHook(hook)
    End Sub
    Public MustOverride Sub Update(elapsedTime As TimeSpan) Implements IUpdatorator.Update
End Class
