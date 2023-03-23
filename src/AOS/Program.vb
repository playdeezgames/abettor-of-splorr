Module Program
    Private x As Integer = 0
    Private y As Integer = 0
    Sub Main(args As String())
        Using host As New Host(Of Color)(1280, 720, 160, 90, AddressOf BufferCreatorator, AddressOf Updatifier, AddressOf Commanderator)
            host.Run()
        End Using
    End Sub

    Private Function BufferCreatorator(texture As Texture2D) As IDisplayBuffer(Of Color)
        Return New DisplayBuffer(Of Color)(texture)
    End Function

    Private Sub Updatifier(displayBuffer As IDisplayBuffer(Of Color))
        displayBuffer.SetPixel(x, y, New Color(0, 0, 0, 255))
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
