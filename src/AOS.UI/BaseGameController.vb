﻿Public Class BaseGameController(Of THue, TCommand, TSfx, TState As Structure)
    Implements IGameController(Of THue, TCommand, TSfx)
    Private _windowSize As (Integer, Integer)
    Private _sizeHook As Action(Of (Integer, Integer))
    Private ReadOnly _states As New Dictionary(Of TState, BaseGameState(Of THue, TCommand, TSfx, TState))
    Private _stateStack As New Stack(Of TState)
    Protected Sub SetCurrentState(state As TState?, push As Boolean)
        If Not push Then
            PopState()
        End If
        If state.HasValue Then
            PushState(state.Value)
            _states(_stateStack.Peek).OnStart()
        End If
    End Sub

    Private Sub PushState(state As TState)
        _stateStack.Push(state)
    End Sub

    Private Function PopState() As TState?
        If _stateStack.Any Then
            Return _stateStack.Pop()
        End If
        Return Nothing
    End Function

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

    Public ReadOnly Property QuitRequested As Boolean Implements IGameController(Of THue, TCommand, TSfx).QuitRequested
        Get
            Return Not _stateStack.Any
        End Get
    End Property

    Sub New(windowSize As (Integer, Integer), volume As Single)
        _windowSize = windowSize
        Me.Volume = volume
    End Sub
    Private OnSfx As Action(Of TSfx)
    Public Sub HandleCommand(command As TCommand) Implements ICommandHandler(Of TCommand).HandleCommand
        _states(_stateStack.Peek).HandleCommand(command)
    End Sub
    Public Sub Render(displayBuffer As IPixelSink(Of THue)) Implements IRenderer(Of THue).Render
        _states(_stateStack.Peek).Render(displayBuffer)
    End Sub

    Public Sub PlaySfx(sfx As TSfx) Implements ISfxHandler(Of TSfx).PlaySfx
        OnSfx(sfx)
    End Sub

    Public Sub Update(elapsedTime As TimeSpan) Implements IUpdatorator.Update
        _states(_stateStack.Peek).Update(elapsedTime)
    End Sub

    Public Sub SetSfxHook(handler As Action(Of TSfx)) Implements ISfxHandler(Of TSfx).SetSfxHook
        OnSfx = handler
    End Sub

    Public Sub SetSizeHook(hook As Action(Of (Integer, Integer))) Implements IWindowSizerizer.SetSizeHook
        _sizeHook = hook
    End Sub
End Class
