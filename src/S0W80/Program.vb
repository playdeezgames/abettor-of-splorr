Module Program
    Sub Main(args As String())
        Dim frameBuffer = New FrameBuffer(ScreenColumns, ScreenRows)
        Dim engine = New Engine(frameBuffer, 1, False)
        Using host As New Host(frameBuffer, engine, engine)
            host.Run()
        End Using
    End Sub
End Module
