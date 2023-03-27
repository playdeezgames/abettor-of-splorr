Imports System.IO
Imports System.Runtime.Serialization.Json
Imports System.Text.Json

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
    Private _score As Integer
    Private _runLength As Integer
    Private ReadOnly _digits(9) As OffscreenBuffer(Of Boolean)
    Private _delta As Integer
    Private ReadOnly _random As New Random
    Private timer As Double
    Const frameTimer As Double = 0.1
    Private _gameOver As Boolean = True
    Private _font As FontData

    Public Sub New(windowSizeSource As Func(Of (Integer, Integer)), volumeSource As Func(Of Single), configSink As Action(Of (Integer, Integer), Single))
        MyBase.New(windowSizeSource(), volumeSource())
        _configSink = configSink
        _configSink(Size, Volume)
        _font = JsonSerializer.Deserialize(Of FontData)(File.ReadAllText("CyFont4x6.json"))
        LoadDigits()
        ResetBoard()
    End Sub

    Private Sub LoadDigits()
        _digits(0) = _font.ToOffscreenBuffer("0"c)
        _digits(1) = _font.ToOffscreenBuffer("1"c)
        _digits(2) = _font.ToOffscreenBuffer("2"c)
        _digits(3) = _font.ToOffscreenBuffer("3"c)
        _digits(4) = _font.ToOffscreenBuffer("4"c)
        _digits(5) = _font.ToOffscreenBuffer("5"c)
        _digits(6) = _font.ToOffscreenBuffer("6"c)
        _digits(7) = _font.ToOffscreenBuffer("7"c)
        _digits(8) = _font.ToOffscreenBuffer("8"c)
        _digits(9) = _font.ToOffscreenBuffer("9"c)
    End Sub

    Private Function MapDigitPixel(character As Char) As Boolean
        Return character = "X"c
    End Function

    Private Sub ResetBoard()
        For row = 0 To tailRows - 1
            _tail(row) = cellColumns \ 2
        Next
        For row = 0 To cellRows - 1
            _blocks(row) = 0
        Next
        _delta = 1
        _score = 0
        _runLength = 0
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        If _gameOver Then
            If command = Command.Fire Then
                ResetBoard()
                _gameOver = False
            End If
        Else
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
        End If
    End Sub

    Private Sub CommitScore()
        _score += ((_runLength) * (_runLength + 1) \ 2)
        _runLength = 0
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
        Dim scoreString = _score.ToString
        Dim x = cellWidth
        For Each character In scoreString
            Dim digit = AscW(character) - AscW("0"c)
            Dim fromBuffer = _digits(digit)
            displayBuffer.Colorize(
                fromBuffer,
                (0, 0),
                (x, 0),
                (5, 7),
                AddressOf MapFontHue)
            x += 5
        Next
    End Sub

    Private Function MapFontHue(pixel As Boolean) As Hue?
        If pixel Then
            Return Hue.Green
        End If
        Return Nothing
    End Function

    Public Overrides Sub Update(elapsedTime As TimeSpan)
        If _gameOver Then
            Return
        End If
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
        _gameOver = _tail(tailRows - 1) = _blocks(tailRows - 1) OrElse _tail(tailRows - 1) <= 0 OrElse _tail(tailRows - 1) >= cellColumns - 1
        If _gameOver Then
            PlaySfx(Sfx.Death)
        End If
    End Sub
End Class
