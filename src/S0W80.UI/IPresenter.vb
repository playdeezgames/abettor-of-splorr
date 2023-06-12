Public Interface IPresenter
    Event OnQuit()
    Event OnResize(scale As Integer, flag As Boolean)
    Event OnVolume(volume As Single)
    Event OnSfx(sfx As String)
End Interface
