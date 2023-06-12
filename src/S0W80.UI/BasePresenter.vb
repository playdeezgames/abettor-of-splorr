Public Class BasePresenter
    Implements IPresenter
    Public Event Quit() Implements IPresenter.Quit
    Public Event FullScreen(flag As Boolean) Implements IPresenter.FullScreen
    Protected Sub DoQuit()
        RaiseEvent Quit()
    End Sub
    Protected Sub DoFullScreen(flag As Boolean)
        RaiseEvent FullScreen(flag)
    End Sub
End Class
