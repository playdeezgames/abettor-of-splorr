Public Class GameController
    Implements IGameController

    Public Sub Update(frameBuffer As IFrameBuffer, ticks As Long) Implements IGameController.Update
        frameBuffer.Write("Hello, SPLORR!!  ")
    End Sub
End Class
