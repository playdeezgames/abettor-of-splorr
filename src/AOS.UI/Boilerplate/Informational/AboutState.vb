Friend Class AboutState(Of TGameContext)
    Inherits BaseGameState(Of TGameContext)
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of TGameContext))
        MyBase.New(parent, setState, context)
    End Sub
    Public Overrides Sub HandleCommand(cmd As String)
        SetState(BoilerplateState.MainMenu)
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink)
        displayBuffer.Fill((0, 0), Context.ViewSize, BoilerplateHue.Black)
        Context.ShowAboutContent(displayBuffer, Context.Font(UIFont))
    End Sub
End Class
