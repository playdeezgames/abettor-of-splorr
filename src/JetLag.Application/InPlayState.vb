Public Class InPlayState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState?, Boolean))
        MyBase.New(parent, setState)
    End Sub
    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.Left
                If _delta <> -1 Then
                    _delta = -1
                    CommitScore()
                End If
            Case Command.Right
                If _delta <> 1 Then
                    _delta = 1
                    CommitScore()
                End If
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        GameContext.Render(displayBuffer)
    End Sub
    Public Overrides Sub Update(elapsedTime As TimeSpan)
        timer += elapsedTime.TotalSeconds
        If timer < frameTimer Then
            Return
        End If
        _runLength += 1
        timer -= frameTimer
        For row = 0 To cellRows - 2
            _blocks(row) = _blocks(row + 1)
        Next
        _blocks(cellRows - 1) = _random.Next(1, cellColumns - 1)
        For row = 0 To tailRows - 2
            _tail(row) = _tail(row + 1)
        Next
        _tail(tailRows - 1) += _delta
        If _tail(tailRows - 1) = _blocks(tailRows - 1) OrElse _tail(tailRows - 1) <= 0 OrElse _tail(tailRows - 1) >= cellColumns - 1 Then
            PlaySfx(Sfx.Death)
            SetState(GameState.GameOver)
        End If
    End Sub
End Class
