Friend Class PlaceHolderState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (GameContext.ViewWidth, GameContext.ViewHeight), Hue.Blue)
        Dim drawer As New Drawer(Of Hue)(displayBuffer)
        drawer.
            MoveTo(GameContext.ViewWidth \ 4, GameContext.ViewHeight \ 4).
            Color(Hue.White).
            Down(GameContext.ViewHeight \ 2).
            Right(GameContext.ViewWidth \ 2).
            Up(GameContext.ViewHeight \ 2).
            Left(GameContext.ViewWidth \ 2).
            MoveTo(0, 0).
            Repeat(20, Function(d) d.Right(1).DownRight(1)).
            MoveTo(0, ViewHeight - 1).
            Repeat(20, Function(d) d.Right(1).UpRight(1)).
            MoveTo(ViewWidth - 1, 0).
            Repeat(20, Function(d) d.Left(1).DownLeft(1)).
            MoveTo(ViewWidth - 1, ViewHeight - 1).
            Repeat(20, Function(d) d.Left(1).UpLeft(1))
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
