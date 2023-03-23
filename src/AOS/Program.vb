Module Program
    Private x As Integer = 0
    Private y As Integer = 0
    Sub Main(args As String())
        Using host As New Host(Of Hue)(1280, 720, 160, 90, AddressOf BufferCreatorator, AddressOf Updatifier, AddressOf Commanderator)
            host.Run()
        End Using
    End Sub

    Private Function BufferCreatorator(texture As Texture2D) As IDisplayBuffer(Of Hue)
        Return New DisplayBuffer(Of Hue)(texture, AddressOf TransformHue)
    End Function

    Private ReadOnly hueTable As IReadOnlyDictionary(Of Hue, Color) =
        New Dictionary(Of Hue, Color) From
        {
            {Hue.Black, New Color(0, 0, 0, 255)},
            {Hue.Blue, New Color(0, 0, 170, 255)},
            {Hue.Green, New Color(0, 170, 0, 255)},
            {Hue.Cyan, New Color(0, 170, 170, 255)},
            {Hue.Red, New Color(170, 0, 0, 255)},
            {Hue.Magenta, New Color(170, 0, 170, 255)},
            {Hue.Brown, New Color(170, 85, 0, 255)},
            {Hue.Gray, New Color(170, 170, 170, 255)},
            {Hue.DarkGray, New Color(85, 85, 85, 255)},
            {Hue.LightBlue, New Color(85, 85, 255, 255)},
            {Hue.LightGreen, New Color(85, 255, 85, 255)},
            {Hue.LightCyan, New Color(85, 255, 255, 255)},
            {Hue.LightRed, New Color(255, 85, 85, 255)},
            {Hue.LightMagenta, New Color(255, 85, 255, 255)},
            {Hue.Yellow, New Color(255, 255, 85, 255)},
            {Hue.White, New Color(255, 255, 255, 255)}
        }

    Private Function TransformHue(hue As Hue) As Color
        Return hueTable(hue)
    End Function

    Private Sub Updatifier(displayBuffer As IDisplayBuffer(Of Hue))
        displayBuffer.SetPixel(x, y, Hue.Black)
    End Sub

    Private Sub Commanderator(pressedKeys() As Keys)
        For Each pressedKey In pressedKeys
            Select Case pressedKey
                Case Keys.Up
                    y -= 1
                Case Keys.Down
                    y += 1
                Case Keys.Left
                    x -= 1
                Case Keys.Right
                    x += 1
            End Select
        Next
    End Sub
End Module
