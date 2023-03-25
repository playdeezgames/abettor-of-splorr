Public Class GameController
    Inherits BaseGameController
    Private ReadOnly _configSink As Action(Of (Integer, Integer), Single)
    Private ReadOnly sprite As OffscreenBuffer(Of Hue)
    Private x As Integer = 0
    Private y As Integer = 0
    Private deltaX As Integer = 1
    Private deltaY As Integer = 1
    Const spriteWidth = 8
    Const spriteHeight = 8
    Const viewWidth = 160
    Const viewHeight = 90

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

    Public Overrides Sub HandleCommand(command As Command)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Copy(sprite, (0, 0), (x, y), (spriteWidth, spriteHeight), Function(x) True)
        x = x + deltaX
        y = y + deltaY
        If x = 0 Then deltaX = Math.Abs(deltaX)
        If y = 0 Then deltaY = Math.Abs(deltaY)
        If x = viewWidth - spriteWidth Then deltaX = -Math.Abs(deltaX)
        If y = viewHeight - spriteHeight Then deltaY = -Math.Abs(deltaY)
    End Sub
End Class
