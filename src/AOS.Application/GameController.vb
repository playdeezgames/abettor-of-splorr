Public Class GameController
    Inherits BaseGameController
    Private x As Integer = 0
    Private y As Integer = 0

    Public Sub New(windowSizeSource As Func(Of (Integer, Integer)), volumeSource As Func(Of Single))
        MyBase.New(windowSizeSource(), volumeSource())
    End Sub

    Public Overrides Sub HandleCommand(command As Command)
        Select Case command
            Case Command.Up
                y -= 1
            Case Command.Down
                y += 1
            Case Command.Left
                x -= 1
            Case Command.Right
                x += 1
                PlaySfx(Sfx.UnlockDoor)
        End Select
    End Sub

    Public Overrides Sub Render(displayBuffer As IDisplayBuffer(Of Hue))
        displayBuffer.SetPixel(x, y, Hue.Blue)
    End Sub
End Class
