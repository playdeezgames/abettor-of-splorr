Public Module GameContext
    Public Const ViewWidth = 160
    Public Const ViewHeight = 90
    Public Maze As Maze(Of Direction)
    Private ReadOnly DirectionTable As IReadOnlyDictionary(Of Direction, MazeDirection(Of Direction)) =
        New Dictionary(Of Direction, MazeDirection(Of Direction)) From
        {
            {Direction.North, New MazeDirection(Of Direction)(Direction.South, 0, -1)},
            {Direction.East, New MazeDirection(Of Direction)(Direction.West, 1, 0)},
            {Direction.South, New MazeDirection(Of Direction)(Direction.North, 0, 1)},
            {Direction.West, New MazeDirection(Of Direction)(Direction.East, -1, 0)}
        }
    Public Const MazeColumns = 16
    Public Const MazeRows = 9
    Public Const CellWidth = 10
    Public Const CellHeight = 10

    Public Sub Initialize()
        Maze = New Maze(Of Direction)(MazeColumns, MazeRows, DirectionTable)
        Maze.Generate()
    End Sub
End Module
