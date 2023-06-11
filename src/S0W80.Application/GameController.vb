Public Class GameController
    Implements IGameController

    Public Sub Update(frameBuffer As IFrameBuffer, keyBuffer As IKeyBuffer, ticks As Long) Implements IGameController.Update
        If keyBuffer.HasAny Then
            Dim command = keyBuffer.ReadNext()
            Select Case command
                Case ChrW(8)
                    frameBuffer.CursorColumn -= 1
                    frameBuffer.Write(" "c)
                    frameBuffer.CursorColumn -= 1
                Case ChrW(13)
                    frameBuffer.WriteLine("")
                Case Else
                    frameBuffer.Write(command)
            End Select
        End If
    End Sub
End Class
