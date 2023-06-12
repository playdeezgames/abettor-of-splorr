Module Program
    Sub Main(args As String())
        Dim frameBuffer = New FrameBuffer(ScreenColumns, ScreenRows, 4)
        Dim engine = New Engine(frameBuffer)
        Using host As New Host(frameBuffer, engine, engine)
            host.Run()
        End Using
    End Sub
End Module
