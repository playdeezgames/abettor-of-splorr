﻿Public Class GameController
    Inherits BaseGameController(Of Hue, Command, Sfx, GameState)
    Private ReadOnly _configSink As Action(Of (Integer, Integer), Single)

    Public Sub New(windowSizeSource As Func(Of (Integer, Integer)), fullScreenSource As Func(Of Boolean), volumeSource As Func(Of Single), configSink As Action(Of (Integer, Integer), Single))
        MyBase.New(windowSizeSource(), fullScreenSource(), volumeSource())
        _configSink = configSink
        _configSink(Size, Volume)
        SetState(GameState.PlaceHolder, New PlaceHolderState(Me, AddressOf SetCurrentState))
    End Sub
End Class
