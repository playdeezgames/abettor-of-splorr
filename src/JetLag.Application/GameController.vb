Public Class GameController
    Inherits BaseGameController(Of Hue, Command, Sfx)
    Private ReadOnly _configSink As Action(Of (Integer, Integer), Single)
    Const viewWidth = 320
    Const viewHeight = 180
    Const cellWidth = 8
    Const cellColumns = viewWidth \ cellWidth
    Const cellHeight = 8
    Const cellRows = viewHeight \ cellHeight
    Const tailRows = cellRows \ 4
    Private ReadOnly _blocks(cellRows) As Integer
    Private ReadOnly _tail(tailRows) As Integer
    Private _delta As Integer
    Private ReadOnly _random As New Random
    Private timer As Double
    Const frameTimer As Double = 0.1

    Public Sub New(windowSizeSource As Func(Of (Integer, Integer)), volumeSource As Func(Of Single), configSink As Action(Of (Integer, Integer), Single))
        MyBase.New(windowSizeSource(), volumeSource())
        _configSink = configSink
        _configSink(Size, Volume)
        ResetBoard()
    End Sub

    Private Sub ResetBoard()
        For row = 0 To tailRows - 1
            _tail(row) = cellColumns \ 2
        Next
        For row = 0 To cellRows - 1
            _blocks(row) = 0
        Next
        _delta = 1
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.Left
                _delta = -1
            Case Command.Right
                _delta = 1
        End Select
    End Sub

    Private Function PlotCell(x As Integer, y As Integer) As (Integer, Integer)
        Return (x * cellWidth, y * cellHeight)
    End Function

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill(PlotCell(0, 0), (viewWidth, viewHeight), Hue.Black)
        For row = 0 To tailRows - 1
            displayBuffer.Fill(
                PlotCell(_tail(row), row),
                (cellWidth, cellHeight),
                If(row < tailRows - 1, Hue.Yellow, Hue.Red))
        Next
        For row = 0 To cellRows - 1
            displayBuffer.Fill(PlotCell(_blocks(row), row), (cellWidth, cellHeight), Hue.White)
        Next
        displayBuffer.Fill(PlotCell(0, 0), (cellWidth, viewHeight), Hue.Blue)
        displayBuffer.Fill(PlotCell(cellColumns - 1, 0), (cellWidth, viewHeight), Hue.Blue)
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
        timer += elapsedTime.TotalSeconds
        If timer < frameTimer Then
            Return
        End If
        timer -= frameTimer
        For row = 0 To cellRows - 2
            _blocks(row) = _blocks(row + 1)
        Next
        _blocks(cellRows - 1) = _random.Next(1, cellColumns - 1)
        For row = 0 To tailRows - 2
            _tail(row) = _tail(row + 1)
        Next
        _tail(tailRows - 1) += _delta
    End Sub
End Class
