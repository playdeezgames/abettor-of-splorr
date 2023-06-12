Module Program
    Sub Main(args As String())
        Dim frameBuffer = New FrameBuffer(ScreenColumns, ScreenRows, 4)
        Dim gameController = New GameController(frameBuffer)
        Using host As New Host(frameBuffer, gameController)
            host.Run()
        End Using
    End Sub
End Module
