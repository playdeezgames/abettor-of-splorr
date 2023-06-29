Public Interface IGameController
    Inherits IWindowSizerizer
    ReadOnly Property QuitRequested As Boolean
    Sub SaveConfig()
    Sub HandleCommand(cmd As String)
    Sub Render(displayBuffer As IPixelSink)
    Sub Update(elapsedTime As TimeSpan)
    Sub SetSfxHook(handler As Action(Of String))
    Sub PlaySfx(sfx As String)
    Property Volume As Single
End Interface
