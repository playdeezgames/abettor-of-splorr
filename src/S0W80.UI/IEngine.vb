Public Interface IEngine
    Sub Update(commands As IEnumerable(Of String), ticks As Long)
    Sub Initialize()
End Interface
