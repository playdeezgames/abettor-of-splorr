Module Program
    Sub Main(args As String())
        Dim frameBuffer = New FrameBuffer(ScreenColumns, ScreenRows)
        Dim engine = New Engine(frameBuffer, 1, False, 0.0F)
        Using host As New Host(
            "This is a title!",
            frameBuffer,
            engine,
            engine,
            1,
            False,
            0.0F,
            New Dictionary(Of String, String) From
            {
                {"RollDice", "RollDice.wav"}
            })
            host.Run()
        End Using
    End Sub
End Module
