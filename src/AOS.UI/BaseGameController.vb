Public Class BaseGameController(Of THue As Structure)
    Implements IGameController(Of THue)
    Private _windowSize As (Integer, Integer)
    Private _fullScreen As Boolean
    Private _sizeHook As Action(Of (Integer, Integer), Boolean)
    Private ReadOnly _states As New Dictionary(Of String, BaseGameState(Of THue))
    Private _stateStack As New Stack(Of String)
    Protected Sub SetCurrentState(state As String, push As Boolean)
        If Not push Then
            PopState()
        End If
        If Not String.IsNullOrEmpty(state) Then
            PushState(state)
            _states(_stateStack.Peek).OnStart()
        End If
    End Sub

    Private Sub PushState(state As String)
        _stateStack.Push(state)
    End Sub

    Private Function PopState() As String
        If _stateStack.Any Then
            Return _stateStack.Pop()
        End If
        Return Nothing
    End Function

    Protected Sub SetState(state As String, handler As BaseGameState(Of THue))
        _states(state) = handler
    End Sub
    Public Property Size As (Integer, Integer) Implements IWindowSizerizer.Size
        Get
            Return _windowSize
        End Get
        Set(value As (Integer, Integer))
            If value.Item1 <> _windowSize.Item1 OrElse value.Item2 <> _windowSize.Item2 Then
                _windowSize = value
                _sizeHook(_windowSize, _fullScreen)
            End If
        End Set
    End Property
    Public Property Volume As Single Implements ISfxHandler.Volume

    Public ReadOnly Property QuitRequested As Boolean Implements IGameController(Of THue).QuitRequested
        Get
            Return Not _stateStack.Any
        End Get
    End Property

    Public Property FullScreen As Boolean Implements IWindowSizerizer.FullScreen
        Get
            Return _fullScreen
        End Get
        Set(value As Boolean)
            If value <> _fullScreen Then
                _fullScreen = value
                _sizeHook(_windowSize, _fullScreen)
            End If
        End Set
    End Property

    Sub New(windowSize As (Integer, Integer), fullScreen As Boolean, volume As Single)
        _windowSize = windowSize
        _fullScreen = fullScreen
        Me.Volume = volume
    End Sub
    Private OnSfx As Action(Of String)
    Public Sub HandleCommand(command As String) Implements ICommandHandler.HandleCommand
        _states(_stateStack.Peek).HandleCommand(command)
    End Sub
    Public Sub Render(displayBuffer As IPixelSink(Of THue)) Implements IRenderer(Of THue).Render
        _states(_stateStack.Peek).Render(displayBuffer)
    End Sub

    Public Sub PlaySfx(sfx As String) Implements ISfxHandler.PlaySfx
        OnSfx(sfx)
    End Sub

    Public Sub Update(elapsedTime As TimeSpan) Implements IUpdatorator.Update
        _states(_stateStack.Peek).Update(elapsedTime)
    End Sub

    Public Sub SetSfxHook(handler As Action(Of String)) Implements ISfxHandler.SetSfxHook
        OnSfx = handler
    End Sub

    Public Sub SetSizeHook(hook As Action(Of (Integer, Integer), Boolean)) Implements IWindowSizerizer.SetSizeHook
        _sizeHook = hook
    End Sub
End Class
