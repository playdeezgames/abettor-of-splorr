Public Class GameController
    Inherits BaseGameController(Of Hue, Command, Sfx, GameState)
    Private ReadOnly _configSink As Action(Of (Integer, Integer), Single)
    Private ReadOnly sprite As OffscreenBuffer(Of Hue)
    Const spriteWidth = 8
    Const spriteHeight = 8

    Public Sub New(windowSizeSource As Func(Of (Integer, Integer)), volumeSource As Func(Of Single), configSink As Action(Of (Integer, Integer), Single))
        MyBase.New(windowSizeSource(), volumeSource())
        _configSink = configSink
        _configSink(Size, Volume)
        sprite = New OffscreenBuffer(Of Hue)((spriteWidth, spriteHeight))
        For spriteX = 0 To spriteWidth - 1
            For spriteY = 0 To spriteHeight - 1
                sprite.SetPixel(spriteX, spriteY, CType((spriteX + spriteY) Mod 15, Hue))
            Next
        Next
    End Sub
End Class
