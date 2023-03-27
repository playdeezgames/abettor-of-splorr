Public Interface ISfxHandler(Of TSfx)
    Sub SetSfxHook(handler As Action(Of TSfx))
    Sub PlaySfx(sfx As TSfx)
    Property Volume As Single
End Interface
