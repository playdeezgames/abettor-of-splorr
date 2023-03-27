Public Interface IWindowSizerizer
    Property Size As (Integer, Integer)
    Sub SetSizeHook(hook As Action(Of (Integer, Integer)))
End Interface
