Public Class GameController
    Implements ICommandHandler(Of Command)
    Implements IRenderer(Of Hue)
    Implements ISfxHandler(Of Sfx)
    Implements IWindowSizerizer
    Private _windowSize As (Integer, Integer) = (1280, 720)
    Private x As Integer = 0
    Private y As Integer = 0

    Public Property Size As (Integer, Integer) Implements IWindowSizerizer.Size
        Get
            Return _windowSize
        End Get
        Set(value As (Integer, Integer))
            If value.Item1 <> _windowSize.Item1 OrElse value.Item2 <> _windowSize.Item2 Then
                _windowSize = value
                RaiseEvent OnSizeChange(_windowSize)
            End If
        End Set
    End Property

    Public Event OnSfx As ISfxHandler(Of Sfx).OnSfxEventHandler Implements ISfxHandler(Of Sfx).OnSfx
    Public Event OnSizeChange(newSize As (Integer, Integer)) Implements IWindowSizerizer.OnSizeChange

    Public Sub HandleCommand(command As Command) Implements ICommandHandler(Of Command).HandleCommand
        Select Case command
            Case Command.Up
                y -= 1
            Case Command.Down
                y += 1
            Case Command.Left
                x -= 1
            Case Command.Right
                x += 1
        End Select
    End Sub
    Public Sub Render(displayBuffer As IDisplayBuffer(Of Hue)) Implements IRenderer(Of Hue).Render
        displayBuffer.SetPixel(x, y, Hue.Blue)
    End Sub
End Class
