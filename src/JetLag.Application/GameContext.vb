Imports System.IO
Imports System.Text.Json

Friend Module GameContext
    Const viewWidth = 320
    Const viewHeight = 180
    Const cellWidth = 8
    Friend Const cellColumns = viewWidth \ cellWidth
    Const cellHeight = 8
    Friend Const cellRows = viewHeight \ cellHeight
    Friend Const tailRows = cellRows \ 4
    Friend ReadOnly _blocks(cellRows) As Integer
    Friend ReadOnly _tail(tailRows) As Integer
    Private _score As Integer
    Friend _runLength As Integer
    Private ReadOnly _digits(9) As GlyphBuffer
    Friend _delta As Integer
    Friend ReadOnly _random As New Random
    Friend timer As Double
    Friend frameTimer As Double = 0.1
    Friend _fontData As FontData
    Friend _font As Font
    Friend Sub Initialize()
        _fontData = JsonSerializer.Deserialize(Of FontData)(File.ReadAllText("CyFont4x6.json"))
        LoadDigits()
        ResetBoard()
    End Sub
    Private Sub LoadDigits()
        _font = New Font(_fontData)
        _digits(0) = New GlyphBuffer(_fontData, "0"c)
        _digits(1) = New GlyphBuffer(_fontData, "1"c)
        _digits(2) = New GlyphBuffer(_fontData, "2"c)
        _digits(3) = New GlyphBuffer(_fontData, "3"c)
        _digits(4) = New GlyphBuffer(_fontData, "4"c)
        _digits(5) = New GlyphBuffer(_fontData, "5"c)
        _digits(6) = New GlyphBuffer(_fontData, "6"c)
        _digits(7) = New GlyphBuffer(_fontData, "7"c)
        _digits(8) = New GlyphBuffer(_fontData, "8"c)
        _digits(9) = New GlyphBuffer(_fontData, "9"c)
    End Sub
    Friend Sub CommitScore()
        _score += ((_runLength) * (_runLength + 1) \ 2)
        _runLength = 0
    End Sub

    Friend Sub ResetBoard()
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

    Private Function PlotCell(x As Integer, y As Integer) As (Integer, Integer)
        Return (x * cellWidth, y * cellHeight)
    End Function

    Friend Sub Render(displayBuffer As IPixelSink(Of Hue))
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
            _digits(digit).CopyTo(displayBuffer, (x, 0), Hue.Green)
            x += _digits(digit).Width
        Next
    End Sub
    Private Function MapFontHue(pixel As Boolean) As Hue?
        If pixel Then
            Return Hue.Green
        End If
        Return Nothing
    End Function
End Module
