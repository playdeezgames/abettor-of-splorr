Public Interface ISfxHandler(Of TSfx)
    Event OnSfx(sfx As TSfx)
    Sub PlaySfx(sfx As TSfx)
    Property Volume As Single
End Interface
