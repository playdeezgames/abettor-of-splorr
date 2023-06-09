Public Class GameController
    Implements IGameController

    Public Sub Update(frameBuffer As IFrameBuffer) Implements IGameController.Update
        frameBuffer.Fill(0, 0, 80, 25, 0, 0)
        frameBuffer.WriteText(0, 0, "Hello, world!", 15)
        frameBuffer.WriteText(0, 1, "This is a test of color!", 4)
        frameBuffer.WriteText(0, 2, "This is a test of inverted color!", 64)
    End Sub
End Class
