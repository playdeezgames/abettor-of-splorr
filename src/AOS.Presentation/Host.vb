Public Class Host(Of THue As Structure, TCommand As Structure, TSfx As Structure)
    Inherits Game
    Private ReadOnly _controller As IGameController(Of THue, TCommand, TSfx)

    Private ReadOnly _viewSize As (Integer, Integer)

    Private ReadOnly _graphics As GraphicsDeviceManager
    Private ReadOnly _bufferCreator As Func(Of Texture2D, IDisplayBuffer(Of THue))
    Private _texture As Texture2D
    Private _spriteBatch As SpriteBatch
    Private _displayBuffer As IDisplayBuffer(Of THue)

    Private ReadOnly _commandTransform As Func(Of Keys, TCommand?)
    Private _keyboardState As KeyboardState
    Private ReadOnly _gamePadTransform As Func(Of GamePadState, GamePadState, TCommand())
    Private _gamePadState As GamePadState

    Private ReadOnly _sfxSoundEffects As New Dictionary(Of TSfx, SoundEffect)
    Private ReadOnly _sfxFilenames As IReadOnlyDictionary(Of TSfx, String)
    Private ReadOnly _title As String
    Sub New(
           title As String,
           controller As IGameController(Of THue, TCommand, TSfx),
           viewSize As (Integer, Integer),
           bufferCreator As Func(Of Texture2D, IDisplayBuffer(Of THue)),
           commandTransform As Func(Of Keys, TCommand?),
           gamePadTransform As Func(Of GamePadState, GamePadState, TCommand()),
           sfxFileNames As IReadOnlyDictionary(Of TSfx, String))
        _title = title
        _graphics = New GraphicsDeviceManager(Me)
        _controller = controller
        _viewSize = viewSize
        _bufferCreator = bufferCreator
        _commandTransform = commandTransform
        _gamePadTransform = gamePadTransform
        _sfxFilenames = sfxFileNames
        Content.RootDirectory = "Content"
    End Sub
    Protected Overrides Sub Initialize()
        _controller.SetSizeHook(AddressOf OnWindowSizeChange)
        Window.Title = _title
        OnWindowSizeChange(_controller.Size, _controller.FullScreen)
        _keyboardState = Keyboard.GetState
        _gamePadState = GamePad.GetState(PlayerIndex.One)
        For Each entry In _sfxFilenames
            _sfxSoundEffects(entry.Key) = SoundEffect.FromFile(entry.Value)
        Next
        _controller.SetSfxHook(AddressOf OnSfx)
        MyBase.Initialize()
    End Sub

    Private Sub OnWindowSizeChange(newSize As (Integer, Integer), fullScreen As Boolean)
        _graphics.PreferredBackBufferWidth = newSize.Item1
        _graphics.PreferredBackBufferHeight = newSize.Item2
        _graphics.IsFullScreen = fullScreen
        _graphics.ApplyChanges()
    End Sub
    Const Pitch = 0.0F
    Const Pan = 0.0F
    Private Sub OnSfx(sfx As TSfx)
        If _sfxSoundEffects.ContainsKey(sfx) Then
            _sfxSoundEffects(sfx).Play(_controller.Volume, Pitch, Pan)
        End If
    End Sub

    Protected Overrides Sub LoadContent()
        _spriteBatch = New SpriteBatch(GraphicsDevice)
        _texture = New Texture2D(GraphicsDevice, _viewSize.Item1, _viewSize.Item2)
        _displayBuffer = _bufferCreator(_texture)
    End Sub
    Protected Overrides Sub Update(gameTime As GameTime)
        UpdateKeyboardState()
        UpdateGamePadState()
        If _controller.QuitRequested Then
            Me.Exit()
            Return
        End If
        _controller.Update(gameTime.ElapsedGameTime)
        _controller.Render(_displayBuffer)
        _displayBuffer.Commit()
        MyBase.Update(gameTime)
    End Sub

    Private Sub UpdateGamePadState()
        Dim newState = GamePad.GetState(PlayerIndex.One)
        If newState.IsConnected Then
            For Each cmd In _gamePadTransform(newState, _gamePadState)
                _controller.HandleCommand(cmd)
            Next
        End If
        _gamePadState = newState
    End Sub

    Private Sub UpdateKeyboardState()
        Dim newState = Keyboard.GetState()
        Dim keysPressed = newState.GetPressedKeys().Where(Function(k) _keyboardState.IsKeyUp(k)).ToArray
        For Each keyPressed In keysPressed
            Dim command = _commandTransform(keyPressed)
            If command.HasValue Then
                _controller.HandleCommand(command.Value)
            End If
        Next
        _keyboardState = newState
    End Sub
    Const Zero = 0
    Protected Overrides Sub Draw(gameTime As GameTime)
        _graphics.GraphicsDevice.Clear(Color.Black)
        _spriteBatch.Begin(samplerState:=SamplerState.PointClamp)
        _spriteBatch.Draw(_texture, New Rectangle(Zero, Zero, _controller.Size.Item1, _controller.Size.Item2), Nothing, Color.White)
        _spriteBatch.End()
        MyBase.Draw(gameTime)
    End Sub
End Class
