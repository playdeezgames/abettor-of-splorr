Public Class GameController
    Inherits BaseGameController(Of Hue, Command, Sfx, GameState)
    Private ReadOnly _configSink As Action(Of (Integer, Integer), Single)

    Public Sub New(windowSizeSource As Func(Of (Integer, Integer)), volumeSource As Func(Of Single), configSink As Action(Of (Integer, Integer), Single))
        MyBase.New(windowSizeSource(), volumeSource())
        _configSink = configSink
        _configSink(Size, Volume)
        GameContext.Initialize()
        SetState(GameState.InPlay, New InPlayState(Me, AddressOf SetCurrentState))
        SetState(GameState.GameOver, New GameOverState(Me, AddressOf SetCurrentState))
        SetCurrentState(GameState.GameOver)
    End Sub
End Class
