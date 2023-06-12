Public Class GameController
    Implements IGameController
    Private ReadOnly _frameBuffer As IFrameBuffer
    Sub New(frameBuffer As IFrameBuffer)
        _frameBuffer = frameBuffer
    End Sub

    Public Sub Update(keyBuffer As IKeyBuffer, ticks As Long) Implements IGameController.Update
        While keyBuffer.HasAny
            Dim command = keyBuffer.ReadNext()
            If command.Length = 1 Then
                Select Case command
                    Case ChrW(8)
                        _frameBuffer.CursorColumn -= 1
                        _frameBuffer.Write(" "c)
                        _frameBuffer.CursorColumn -= 1
                    Case ChrW(13)
                        _frameBuffer.WriteLine("")
                    Case Else
                        _frameBuffer.Write(command)
                End Select
            End If
        End While
    End Sub
End Class
