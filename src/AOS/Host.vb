Public Class Host
    Inherits Game
    Private ReadOnly _windowWidth As Integer
    Private ReadOnly _windowHeight As Integer
    Private ReadOnly _viewWidth As Integer
    Private ReadOnly _viewHeight As Integer
    Private ReadOnly _graphics As GraphicsDeviceManager
    Private _texture As Texture2D
    Private _spriteBatch As SpriteBatch
    Private _buffer As Color()
    Private _updatifier As Action(Of Color(), Integer, Integer)
    Sub New(
           windowWidth As Integer,
           windowHeight As Integer,
           viewWidth As Integer,
           viewHeight As Integer,
           updatifier As Action(Of Color(), Integer, Integer))
        _graphics = New GraphicsDeviceManager(Me)
        _windowHeight = windowHeight
        _windowWidth = windowWidth
        _viewWidth = viewWidth
        _viewHeight = viewHeight
        _updatifier = updatifier
        ReDim _buffer(_viewWidth * _viewHeight - 1)
        Content.RootDirectory = "Content"
    End Sub
    Protected Overrides Sub Initialize()
        _graphics.PreferredBackBufferWidth = _windowWidth
        _graphics.PreferredBackBufferHeight = _windowHeight
        _graphics.ApplyChanges()
        MyBase.Initialize()
    End Sub
    Protected Overrides Sub LoadContent()
        _spriteBatch = New SpriteBatch(GraphicsDevice)
        _texture = New Texture2D(GraphicsDevice, _viewWidth, _viewHeight)
    End Sub
    Protected Overrides Sub Update(gameTime As GameTime)
        _updatifier(_buffer, _viewWidth, _viewHeight)
        _texture.SetData(_buffer)
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
