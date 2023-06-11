Public Interface IKeyBuffer
    ReadOnly Property HasAny As Boolean
    Sub Add(character As Char)
    Sub Add(command As String)
    Function ReadNext() As String
End Interface
