Public Interface ISfxHandler
    Sub SetSfxHook(handler As Action(Of String))
    Sub PlaySfx(sfx As String)
    Property Volume As Single
End Interface
