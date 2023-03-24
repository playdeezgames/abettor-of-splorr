Public Class GameController
    Implements ICommandHandler(Of Command)
    Implements IRenderer(Of Hue)
    Implements ISfxHandler(Of Sfx)
    Private x As Integer = 0
    Private y As Integer = 0
    Public Event OnSfx As ISfxHandler(Of Sfx).OnSfxEventHandler Implements ISfxHandler(Of Sfx).OnSfx

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
