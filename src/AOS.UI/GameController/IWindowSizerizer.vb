Public Interface IWindowSizerizer
    Property Size As (Integer, Integer)
    Property FullScreen As Boolean
    Sub SetSizeHook(hook As Action(Of (Integer, Integer), Boolean))
End Interface
