Public Class BasePresenter
    Implements IPresenter
    Public Event Quit() Implements IPresenter.Quit
    Protected Sub DoQuit()
        RaiseEvent Quit()
    End Sub
End Class
