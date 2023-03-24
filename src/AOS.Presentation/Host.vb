Public Class Host(Of THue, TCommand As Structure, TSfx As Structure)
    Inherits Game
    Private ReadOnly _windowWidth As Integer
    Private ReadOnly _windowHeight As Integer
    Private ReadOnly _viewWidth As Integer
    Private ReadOnly _viewHeight As Integer

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
    Private _sfxSoundEffects As New Dictionary(Of TSfx, SoundEffect)
    Private _sfxFilenames As IReadOnlyDictionary(Of TSfx, String)
    Sub New(
           windowWidth As Integer,
           windowHeight As Integer,
           viewWidth As Integer,
           viewHeight As Integer,
           bufferCreator As Func(Of Texture2D, IDisplayBuffer(Of THue)),
           renderer As IRenderer(Of THue),
           commandTransform As Func(Of Keys, TCommand?),
           commandHandler As ICommandHandler(Of TCommand),
           sfxHandler As ISfxHandler(Of TSfx),
           sfxFileNames As IReadOnlyDictionary(Of TSfx, String))
        _graphics = New GraphicsDeviceManager(Me)
        _windowHeight = windowHeight
        _windowWidth = windowWidth
        _viewWidth = viewWidth
        _viewHeight = viewHeight
        _renderer = renderer
        _bufferCreator = bufferCreator
        _commandTransform = commandTransform
        _commandHandler = commandHandler
        _sfxHandler = sfxHandler
        _sfxFilenames = sfxFileNames
        Content.RootDirectory = "Content"
    End Sub
    Protected Overrides Sub Initialize()
        _graphics.PreferredBackBufferWidth = _windowWidth
        _graphics.PreferredBackBufferHeight = _windowHeight
        _graphics.ApplyChanges()
        _keyboardState = Keyboard.GetState
        For Each entry In _sfxFilenames
            _sfxSoundEffects(entry.Key) = SoundEffect.FromFile(entry.Value)
        Next
        AddHandler _sfxHandler.OnSfx, AddressOf OnSfx
        MyBase.Initialize()
    End Sub

    Private Sub OnSfx(sfx As TSfx)
        If _sfxSoundEffects.ContainsKey(sfx) Then
            _sfxSoundEffects(sfx).Play()
        End If
    End Sub

    Protected Overrides Sub LoadContent()
        _spriteBatch = New SpriteBatch(GraphicsDevice)
        _texture = New Texture2D(GraphicsDevice, _viewWidth, _viewHeight)
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
        _renderer.Render(_displayBuffer)
        _displayBuffer.Commit()
        MyBase.Update(gameTime)
    End Sub
    Protected Overrides Sub Draw(gameTime As GameTime)
        _graphics.GraphicsDevice.Clear(Color.Magenta)
        _spriteBatch.Begin(samplerState:=SamplerState.PointClamp)
        _spriteBatch.Draw(_texture, New Rectangle(0, 0, _windowWidth, _windowHeight), Nothing, Color.White)
        _spriteBatch.End()
        MyBase.Draw(gameTime)
    End Sub
End Class
