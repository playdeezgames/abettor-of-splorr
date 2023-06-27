Public Interface ISettings
    ReadOnly Property WindowSize As (Integer, Integer)
    ReadOnly Property FullScreen As Boolean
    ReadOnly Property Volume As Single
    Sub Save()
End Interface
