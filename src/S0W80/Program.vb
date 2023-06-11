Imports S0W80.Application

Module Program
    Sub Main(args As String())
        Dim gameController = New GameController
        Dim frameBuffer = New FrameBuffer(ScreenColumns, ScreenRows)
        frameBuffer.ForegroundColor = 0
        frameBuffer.BackgroundColor = 4
        Using host As New Host(frameBuffer, gameController)
            host.Run()
        End Using
        'Using host As New OldHost(gameController)
        '    host.Run()
        'End Using
    End Sub
End Module
