Public Class Host(Of THue As Structure, TCommand As Structure, TSfx As Structure)
    Inherits Game
    Private ReadOnly _windowSizerizer As IWindowSizerizer
    Private ReadOnly _viewSize As (Integer, Integer)

    Private ReadOnly _graphics As GraphicsDeviceManager
    Private ReadOnly _renderer As IRenderer(Of THue)
    Private ReadOnly _bufferCreator As Func(Of Texture2D, IDisplayBuffer(Of THue))
    Private _texture As Texture2D
    Private _spriteBatch As SpriteBatch
    Private _displayBuffer As IDisplayBuffer(Of THue)

    Private ReadOnly _commandTransform As Func(Of Keys, TCommand?)
    Private ReadOnly _commandHandler As ICommandHandler(Of TCommand)
    Private _keyboardState As KeyboardState

    Private ReadOnly _sfxHandler As ISfxHandler(Of TSfx)
    Private ReadOnly _sfxSoundEffects As New Dictionary(Of TSfx, SoundEffect)
    Private ReadOnly _sfxFilenames As IReadOnlyDictionary(Of TSfx, String)
    Private ReadOnly _updatatorator As IUpdatorator
    Sub New(
           windowSizerizer As IWindowSizerizer,
           viewSize As (Integer, Integer),
           bufferCreator As Func(Of Texture2D, IDisplayBuffer(Of THue)),
           renderer As IRenderer(Of THue),
           commandTransform As Func(Of Keys, TCommand?),
           commandHandler As ICommandHandler(Of TCommand),
           sfxHandler As ISfxHandler(Of TSfx),
           sfxFileNames As IReadOnlyDictionary(Of TSfx, String),
           updatorator As IUpdatorator)
        _graphics = New GraphicsDeviceManager(Me)
        _windowSizerizer = windowSizerizer
        _viewSize = viewSize
        _renderer = renderer
        _bufferCreator = bufferCreator
        _commandTransform = commandTransform
        _commandHandler = commandHandler
        _sfxHandler = sfxHandler
        _sfxFilenames = sfxFileNames
        _updatatorator = updatorator
        Content.RootDirectory = "Content"
    End Sub
    Protected Overrides Sub Initialize()
        _windowSizerizer.SetSizeHook(AddressOf OnWindowSizeChange)
        OnWindowSizeChange(_windowSizerizer.Size)
        _keyboardState = Keyboard.GetState
        For Each entry In _sfxFilenames
            _sfxSoundEffects(entry.Key) = SoundEffect.FromFile(entry.Value)
        Next
        _sfxHandler.SetSfxHook(AddressOf OnSfx)
        MyBase.Initialize()
    End Sub

    Private Sub OnWindowSizeChange(newSize As (Integer, Integer))
        _graphics.PreferredBackBufferWidth = newSize.Item1
        _graphics.PreferredBackBufferHeight = newSize.Item2
        _graphics.ApplyChanges()
    End Sub

    Private Sub OnSfx(sfx As TSfx)
        If _sfxSoundEffects.ContainsKey(sfx) Then
            _sfxSoundEffects(sfx).Play(_sfxHandler.Volume, 0.0F, 0.0F)
        End If
    End Sub

    Protected Overrides Sub LoadContent()
        _spriteBatch = New SpriteBatch(GraphicsDevice)
        _texture = New Texture2D(GraphicsDevice, _viewSize.Item1, _viewSize.Item2)
        _displayBuffer = _bufferCreator(_texture)
    End Sub
    Protected Overrides Sub Update(gameTime As GameTime)
        Dim newState = Keyboard.GetState()
        Dim keysPressed = newState.GetPressedKeys().Where(Function(k) _keyboardState.IsKeyUp(k)).ToArray
        For Each keyPressed In keysPressed
            Dim command = _commandTransform(keyPressed)
            If command.HasValue Then
                _commandHandler.HandleCommand(command.Value)
            End If
        Next
        _keyboardState = newState
        _updatatorator.Update(gameTime.ElapsedGameTime)
        _renderer.Render(_displayBuffer)
        _displayBuffer.Commit()
        MyBase.Update(gameTime)
    End Sub
    Protected Overrides Sub Draw(gameTime As GameTime)
        _graphics.GraphicsDevice.Clear(Color.Magenta)
        _spriteBatch.Begin(samplerState:=SamplerState.PointClamp)
        _spriteBatch.Draw(_texture, New Rectangle(0, 0, _windowSizerizer.Size.Item1, _windowSizerizer.Size.Item2), Nothing, Color.White)
        _spriteBatch.End()
        MyBase.Draw(gameTime)
    End Sub
End Class
