Public MustInherit Class BaseGameState
    Implements IGameController
    Protected ReadOnly Property Parent As IGameController
    Private ReadOnly SetCurrentState As Action(Of String, Boolean)
    Protected Context As IUIContext
    Protected Const Zero = 0
    Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        Me.Parent = parent
        Me.SetCurrentState = setState
        Me.Context = context
    End Sub
    Protected Sub PopState()
        SetCurrentState(Nothing, False)
    End Sub
    Protected Sub SetState(state As String)
        SetCurrentState(state, False)
    End Sub
    Protected Sub SetStates(pushedState As String, nextState As String)
        SetCurrentState(nextState, False)
        SetCurrentState(pushedState, True)
    End Sub
    Public Property Volume As Single Implements ISfxHandler.Volume
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

    Public ReadOnly Property QuitRequested As Boolean Implements IGameController.QuitRequested
        Get
            Return Parent.QuitRequested
        End Get
    End Property

    Public Property FullScreen As Boolean Implements IWindowSizerizer.FullScreen
        Get
            Return Parent.FullScreen
        End Get
        Set(value As Boolean)
            Parent.FullScreen = value
        End Set
    End Property

    Public MustOverride Sub HandleCommand(cmd As String) Implements IGameController.HandleCommand
    Public MustOverride Sub Render(displayBuffer As IPixelSink) Implements IGameController.Render
    Public Sub SetSfxHook(handler As Action(Of String)) Implements ISfxHandler.SetSfxHook
        Parent.SetSfxHook(handler)
    End Sub
    Public Sub PlaySfx(sfx As String) Implements ISfxHandler.PlaySfx
        Parent.PlaySfx(sfx)
    End Sub
    Public Sub SetSizeHook(hook As Action(Of (Integer, Integer), Boolean)) Implements IWindowSizerizer.SetSizeHook
        Parent.SetSizeHook(hook)
    End Sub
    Public Overridable Sub Update(elapsedTime As TimeSpan) Implements IUpdatorator.Update
        'default implementation: do nothing!
    End Sub
    Public Overridable Sub OnStart()
        'default: do nothing!
    End Sub

    Public Sub SaveConfig() Implements IGameController.SaveConfig
        Parent.SaveConfig()
    End Sub
End Class
