Public Class BasePresenter
    Implements IPresenter
    Public Event Quit() Implements IPresenter.Quit
    Public Event Resize(scale As Integer, flag As Boolean) Implements IPresenter.Resize
    Protected Sub DoQuit()
        RaiseEvent Quit()
    End Sub
    Protected Sub DoResize(scale As Integer, flag As Boolean)
        RaiseEvent Resize(scale, flag)
    End Sub
End Class
