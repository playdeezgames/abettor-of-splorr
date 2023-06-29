Public Class BaseGameController
    Implements IGameController
    Protected ReadOnly Settings As ISettings
    Private _sizeHook As Action(Of (Integer, Integer), Boolean)
    Private ReadOnly _states As New Dictionary(Of String, BaseGameState)
    Private ReadOnly _stateStack As New Stack(Of String)
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

    Protected Sub SetState(state As String, handler As BaseGameState)
        _states(state) = handler
    End Sub
    Public Property Size As (Integer, Integer) Implements IGameController.Size
        Get
            Return Settings.WindowSize
        End Get
        Set(value As (Integer, Integer))
            If value.Item1 <> Settings.WindowSize.Item1 OrElse value.Item2 <> Settings.WindowSize.Item2 Then
                Settings.WindowSize = value
                _sizeHook(Settings.WindowSize, Settings.FullScreen)
            End If
        End Set
    End Property
    Public Property Volume As Single Implements IGameController.Volume
        Get
            Return Settings.Volume
        End Get
        Set(value As Single)
            Settings.Volume = Math.Clamp(value, 0.0F, 1.0F)
        End Set
    End Property
    Public ReadOnly Property QuitRequested As Boolean Implements IGameController.QuitRequested
        Get
            Return Not _stateStack.Any
        End Get
    End Property
    Public Property FullScreen As Boolean Implements IGameController.FullScreen
        Get
            Return Settings.FullScreen
        End Get
        Set(value As Boolean)
            If value <> Settings.FullScreen Then
                Settings.FullScreen = value
                _sizeHook(Settings.WindowSize, Settings.FullScreen)
            End If
        End Set
    End Property
    Sub New(settings As ISettings, context As IUIContext)
        Me.Settings = settings
        Me.Settings.Save()
        Me.Volume = settings.Volume
        SetState(BoilerplateState.Splash, New SplashState(Me, AddressOf SetCurrentState, context))
    End Sub
    Private OnSfx As Action(Of String)
    Public Sub HandleCommand(command As String) Implements IGameController.HandleCommand
        _states(_stateStack.Peek).HandleCommand(command)
    End Sub
    Public Sub Render(displayBuffer As IPixelSink) Implements IGameController.Render
        _states(_stateStack.Peek).Render(displayBuffer)
    End Sub

    Public Sub PlaySfx(sfx As String) Implements IGameController.PlaySfx
        OnSfx(sfx)
    End Sub

    Public Sub Update(elapsedTime As TimeSpan) Implements IGameController.Update
        _states(_stateStack.Peek).Update(elapsedTime)
    End Sub

    Public Sub SetSfxHook(handler As Action(Of String)) Implements IGameController.SetSfxHook
        OnSfx = handler
    End Sub

    Public Sub SetSizeHook(hook As Action(Of (Integer, Integer), Boolean)) Implements IGameController.SetSizeHook
        _sizeHook = hook
    End Sub

    Public Sub SaveConfig() Implements IGameController.SaveConfig
        Settings.Save()
    End Sub
End Class
