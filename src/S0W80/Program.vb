Imports S0W80.Application

Module Program
    Sub Main(args As String())
        Dim gameController = New GameController
        Using host As New Host(gameController)
            host.Run()
        End Using
    End Sub
End Module
