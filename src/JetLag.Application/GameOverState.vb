Public Class GameOverState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        If command = Command.Fire Then
            ResetBoard()
            SetState(GameState.InPlay)
        End If
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        GameContext.Render(displayBuffer)
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
        'do nothing
    End Sub
End Class
