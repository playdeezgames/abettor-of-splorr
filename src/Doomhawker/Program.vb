Module Program
    Const ConfigFileName = "config.json"
    Sub Main(args As String())
        Dim config = LoadConfig()
        Dim gameController As New GameController(
            Function() (config.WindowWidth, config.WindowHeight),
            Function() config.SfxVolume,
            AddressOf SaveConfig)
        Using host As New Host(Of Hue, Command, Sfx)(
            gameController,
            (GameContext.ViewWidth, GameContext.ViewHeight),
            AddressOf BufferCreatorator,
            AddressOf CommandTransformerator,
            AddressOf GamePadTransformer,
            New Dictionary(Of Sfx, String) From
            {
            })
            host.Run()
        End Using
    End Sub

    Private Function GamePadTransformer(newState As GamePadState, oldState As GamePadState) As Command()
        Dim results As New List(Of Command)
        If newState.DPad.Up = ButtonState.Pressed AndAlso oldState.DPad.Up = ButtonState.Released Then
            results.Add(Command.Up)
        End If
        If newState.DPad.Down = ButtonState.Pressed AndAlso oldState.DPad.Down = ButtonState.Released Then
            results.Add(Command.Down)
        End If
        If newState.DPad.Left = ButtonState.Pressed AndAlso oldState.DPad.Left = ButtonState.Released Then
            results.Add(Command.Left)
        End If
        If newState.DPad.Right = ButtonState.Pressed AndAlso oldState.DPad.Right = ButtonState.Released Then
            results.Add(Command.Right)
        End If
        If newState.Buttons.A = ButtonState.Pressed AndAlso oldState.Buttons.A = ButtonState.Released Then
            results.Add(Command.Fire)
        End If
        Return results.ToArray
    End Function

    Private Sub SaveConfig(windowSize As (Integer, Integer), volume As Single)
        File.WriteAllText(ConfigFileName, JsonSerializer.Serialize(New DHConfig With {.SfxVolume = volume, .WindowHeight = windowSize.Item2, .WindowWidth = windowSize.Item1}))
    End Sub
    Const DefaultWindowWidth = 1280
    Const DefaultWindowHeight = 720
    Const DefaultSfxVolume = 1.0F
    Private Function LoadConfig() As DHConfig
        Try
            Return JsonSerializer.Deserialize(Of DHConfig)(File.ReadAllText(ConfigFileName))
        Catch ex As Exception
            Return New DHConfig With
            {
                .WindowWidth = DefaultWindowWidth,
                .WindowHeight = DefaultWindowHeight,
                .SfxVolume = DefaultSfxVolume
            }
        End Try
    End Function

    Private ReadOnly keyTable As IReadOnlyDictionary(Of Keys, Command) =
        New Dictionary(Of Keys, Command) From
        {
            {Keys.Up, Command.Up},
            {Keys.Right, Command.Right},
            {Keys.Left, Command.Left},
            {Keys.Down, Command.Down},
            {Keys.Space, Command.Fire}
        }
    Private Function CommandTransformerator(key As Keys) As Command?
        If keyTable.ContainsKey(key) Then
            Return keyTable(key)
        End If
        Return Nothing
    End Function
    Private Function BufferCreatorator(texture As Texture2D) As IDisplayBuffer(Of Hue)
        Return New DisplayBuffer(Of Hue)(texture, AddressOf TransformHue)
    End Function
    Private ReadOnly hueTable As IReadOnlyDictionary(Of Hue, Color) =
        New Dictionary(Of Hue, Color) From
        {
            {Hue.Black, New Color(0, 0, 0, 255)},
            {Hue.Blue, New Color(0, 0, 170, 255)},
            {Hue.Green, New Color(0, 170, 0, 255)},
            {Hue.Cyan, New Color(0, 170, 170, 255)},
            {Hue.Red, New Color(170, 0, 0, 255)},
            {Hue.Magenta, New Color(170, 0, 170, 255)},
            {Hue.Brown, New Color(170, 85, 0, 255)},
            {Hue.Gray, New Color(170, 170, 170, 255)},
            {Hue.DarkGray, New Color(85, 85, 85, 255)},
            {Hue.LightBlue, New Color(85, 85, 255, 255)},
            {Hue.LightGreen, New Color(85, 255, 85, 255)},
            {Hue.LightCyan, New Color(85, 255, 255, 255)},
            {Hue.LightRed, New Color(255, 85, 85, 255)},
            {Hue.LightMagenta, New Color(255, 85, 255, 255)},
            {Hue.Yellow, New Color(255, 255, 85, 255)},
            {Hue.White, New Color(255, 255, 255, 255)}
        }
    Private Function TransformHue(hue As Hue) As Color
        Return hueTable(hue)
    End Function
End Module
