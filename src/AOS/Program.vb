Module Program
    Private x As Integer = 0
    Private y As Integer = 0
    Private Sub Updatifier(buffer As Color(), width As Integer, height As Integer)
        buffer(x + y * width) = New Color(0, 0, 0, 255)
    End Sub
    Sub Main(args As String())
        Using host As New Host(1280, 720, 160, 90, AddressOf Updatifier, AddressOf Commanderator)
            host.Run()
        End Using
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
