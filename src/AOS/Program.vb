Module Program
    Private Sub Updatifier(buffer As Color(), width As Integer, height As Integer)
        buffer(0) = New Color(0, 0, 0, 255)
    End Sub
    Sub Main(args As String())
        Using host As New Host(1280, 720, 160, 90, AddressOf Updatifier)
            host.Run()
        End Using
    End Sub
End Module
