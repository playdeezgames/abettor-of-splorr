Public Interface ISettings
    Property WindowSize As (Integer, Integer)
    Property FullScreen As Boolean
    ReadOnly Property Volume As Single
    Sub Save()
End Interface
