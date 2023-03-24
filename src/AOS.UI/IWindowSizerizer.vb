Public Interface IWindowSizerizer
    Property Size As (Integer, Integer)
    Event OnSizeChange(newSize As (Integer, Integer))
End Interface
