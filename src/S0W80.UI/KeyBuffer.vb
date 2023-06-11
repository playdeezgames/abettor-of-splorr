Public Class KeyBuffer
    Implements IKeyBuffer
    Private ReadOnly _queue As New Queue(Of String)
    Public ReadOnly Property HasAny As Boolean Implements IKeyBuffer.HasAny
        Get
            Return _queue.Any
        End Get
    End Property

    Public Sub Add(character As Char) Implements IKeyBuffer.Add
        _queue.Enqueue(character)
    End Sub
    Public Sub Add(command As String) Implements IKeyBuffer.Add
        _queue.Enqueue(command)
    End Sub

    Public Function ReadNext() As String Implements IKeyBuffer.ReadNext
        Return _queue.Dequeue()
    End Function
End Class
