Friend Class SplashState
    Inherits BaseGameState
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context)
    End Sub

    Public Overrides Sub HandleCommand(cmd As String)
        Select Case cmd
            Case Command.A
                SetState(BoilerplateState.MainMenu)
        End Select
    End Sub
    Public Overrides Sub Render(displayBuffer As IPixelSink)
        displayBuffer.Fill((0, 0), Context.ViewSize, BoilerplateHue.Black)
        Dim font = Context.Font(UIFont)
        With font
            .WriteText(displayBuffer, (0, 0), "A Game in VB.NET About", BoilerplateHue.Orange)
            .WriteText(displayBuffer, (0, font.Height), "Yer, the Monster,", BoilerplateHue.Orange)
            .WriteText(displayBuffer, (0, font.Height * 2), "and Starting with Nothing", BoilerplateHue.Orange)
            .WriteText(displayBuffer, (0, font.Height * 4), "A Production of TheGrumpyGameDev", BoilerplateHue.Tan)
            .WriteText(displayBuffer, (0, font.Height * 6), "For ""Learn You a Game Jam: Pixel Edition""", BoilerplateHue.Brown)

            Context.ShowStatusBar(displayBuffer, font, "Space/(A)", BoilerplateHue.Black, BoilerplateHue.LightGray)
        End With
    End Sub
End Class
