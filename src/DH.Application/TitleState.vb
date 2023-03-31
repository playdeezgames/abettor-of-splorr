Friend Class TitleState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (ViewWidth, ViewHeight), Hue.Black)
        For column = 0 To MazeColumns - 1
            For row = 0 To MazeRows - 1
                displayBuffer.Fill((column * CellWidth, row * CellHeight), (CellWidth \ 2, CellHeight \ 2), Hue.White)
                Dim northDoor = Maze.GetCell(column, row).GetDoor(Direction.North)
                If northDoor Is Nothing OrElse Not northDoor.Open Then
                    displayBuffer.Fill((column * CellWidth + CellWidth \ 2, row * CellHeight), (CellWidth \ 2, CellHeight \ 2), Hue.White)
                End If
                Dim westDoor = Maze.GetCell(column, row).GetDoor(Direction.West)
                If westDoor Is Nothing OrElse Not westDoor.Open Then
                    displayBuffer.Fill((column * CellWidth, row * CellHeight + CellHeight \ 2), (CellWidth \ 2, CellHeight \ 2), Hue.White)
                End If
            Next
        Next
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
