Friend Class SplashState(Of TGameContext)
    Inherits BaseGameState(Of TGameContext)
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of TGameContext))
        MyBase.New(parent, setState, context)
    End Sub

    Public Overrides Sub HandleCommand(cmd As String)
        Select Case cmd
            Case Command.A
                SetState(BoilerplateState.MainMenu)
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink)
        displayBuffer.Fill((0, 0), Context.ViewSize, BoilerplateHue.Black)
        Context.ShowSplashContent(displayBuffer, Context.Font(UIFont))
    End Sub
End Class
