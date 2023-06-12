Public Class BasePresenter
    Implements IPresenter
    Public Event OnQuit() Implements IPresenter.OnQuit
    Public Event OnResize(scale As Integer, flag As Boolean) Implements IPresenter.OnResize
    Public Event OnVolume(volume As Single) Implements IPresenter.OnVolume
    Public Event OnSfx(sfx As String) Implements IPresenter.OnSfx
    Protected Sub DoQuit()
        RaiseEvent OnQuit()
    End Sub
    Protected Sub DoResize(scale As Integer, flag As Boolean)
        RaiseEvent OnResize(scale, flag)
    End Sub
    Protected Sub DoVolume(volume As Single)
        RaiseEvent OnVolume(volume)
    End Sub
    Protected Sub DoSfx(sfxName As String)
        RaiseEvent OnSfx(sfxName)
    End Sub
End Class
