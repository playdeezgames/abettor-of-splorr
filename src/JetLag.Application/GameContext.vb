﻿Imports System.IO
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
    Friend _delta As Integer
    Friend ReadOnly _random As New Random
    Friend timer As Double
    Friend frameTimer As Double = 0.1
    Friend _font As Font
    Friend Sub Initialize()
        _font = New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText("CyFont4x6.json")))
        ResetBoard()
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
        Dim x = cellWidth
        _font.WriteText(displayBuffer, (cellWidth, 0), $"Score: {_score}", Hue.Green)
    End Sub
    Private Function MapFontHue(pixel As Boolean) As Hue?
        If pixel Then
            Return Hue.Green
        End If
        Return Nothing
    End Function
End Module
