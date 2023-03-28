Public MustInherit Class BaseGameController(Of THue As Structure, TCommand, TSfx, TState)
    Implements IGameController(Of THue, TCommand, TSfx)
    Private _windowSize As (Integer, Integer)
    Private _sizeHook As Action(Of (Integer, Integer))
    Private ReadOnly _states As New Dictionary(Of TState, BaseGameState(Of THue, TCommand, TSfx, TState))
    Private _state As TState
    Protected Sub SetCurrentState(state As TState)
        _state = state
    End Sub
    Protected Sub SetState(state As TState, handler As BaseGameState(Of THue, TCommand, TSfx, TState))
        _states(state) = handler
    End Sub
    Public Property Size As (Integer, Integer) Implements IWindowSizerizer.Size
        Get
            Return _windowSize
        End Get
        Set(value As (Integer, Integer))
            If value.Item1 <> _windowSize.Item1 OrElse value.Item2 <> _windowSize.Item2 Then
                _windowSize = value
                _sizeHook(_windowSize)
            End If
        End Set
    End Property
    Public Property Volume As Single Implements ISfxHandler(Of TSfx).Volume
    Sub New(windowSize As (Integer, Integer), volume As Single)
        _windowSize = windowSize
        Me.Volume = volume
    End Sub
    Private OnSfx As Action(Of TSfx)
    Public MustOverride Sub HandleCommand(command As TCommand) Implements ICommandHandler(Of TCommand).HandleCommand
    Public MustOverride Sub Render(displayBuffer As IPixelSink(Of THue)) Implements IRenderer(Of THue).Render

    Public Sub PlaySfx(sfx As TSfx) Implements ISfxHandler(Of TSfx).PlaySfx
        OnSfx(sfx)
    End Sub

    Public MustOverride Sub Update(elapsedTime As TimeSpan) Implements IUpdatorator.Update

    Public Sub SetSfxHook(handler As Action(Of TSfx)) Implements ISfxHandler(Of TSfx).SetSfxHook
        OnSfx = handler
    End Sub

    Public Sub SetSizeHook(hook As Action(Of (Integer, Integer))) Implements IWindowSizerizer.SetSizeHook
        _sizeHook = hook
    End Sub
End Class
