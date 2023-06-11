Imports S0W80.Application

Module Program
    Sub Main(args As String())
        Dim gameController = New GameController
        Dim frameBuffer = New FrameBuffer(ScreenColumns, ScreenRows)
        With frameBuffer.GetCell(0, 0)
            .BackgroundColor = 4
            .ForegroundColor = 0
            .Character = "H"c
        End With
        Using host As New Host(frameBuffer)
            host.Run()
        End Using
        'Using host As New OldHost(gameController)
        '    host.Run()
        'End Using
    End Sub
End Module
