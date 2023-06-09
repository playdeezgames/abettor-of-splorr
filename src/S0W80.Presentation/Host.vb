Public Class Host
    Inherits Game
    Private ReadOnly _graphics As GraphicsDeviceManager
    Private ReadOnly _random As New Random
    Const _frameBufferWidth = 640
    Const _frameBufferHeight = 400
    Private _cellWidth As Integer
    Private _cellHeight As Integer
    Private _cellColumns As Integer
    Private _cellRows As Integer
    Private _texture As Texture2D
    Private _fillTexture As Texture2D
    Private ReadOnly _sourceRectangles(255) As Rectangle
    Private _destinationRectangles As Rectangle()
    Private _bufferCharacters As Byte()
    Private _bufferAttributes As Byte()
    Private _spriteBatch As SpriteBatch
    Private ReadOnly _colors() As Color = {
        New Color(0, 0, 0),
        New Color(0, 0, 170),
        New Color(0, 170, 0),
        New Color(0, 170, 170),
        New Color(170, 0, 0),
        New Color(170, 0, 170),
        New Color(170, 85, 0),
        New Color(170, 170, 170),
        New Color(85, 85, 85),
        New Color(85, 85, 255),
        New Color(85, 255, 85),
        New Color(85, 255, 255),
        New Color(255, 85, 85),
        New Color(255, 85, 170),
        New Color(255, 255, 85),
        New Color(255, 255, 255)
    }
    Sub New()
        _graphics = New GraphicsDeviceManager(Me)
        Content.RootDirectory = "Content"
    End Sub
    Protected Overrides Sub LoadContent()
        MyBase.LoadContent()
        _spriteBatch = New SpriteBatch(GraphicsDevice)
        _texture = Texture2D.FromFile(GraphicsDevice, "Font8x16.png")
        _fillTexture = New Texture2D(GraphicsDevice, 1, 1)
        Dim data() As Color = {Color.White}
        _fillTexture.SetData(data)
        _cellWidth = _texture.Width \ 16
        _cellHeight = _texture.Height \ 16
        _cellColumns = _frameBufferWidth \ _cellWidth
        _cellRows = _frameBufferHeight \ _cellHeight
        For row = 0 To 15
            For column = 0 To 15
                _sourceRectangles(column + row * 16) = New Rectangle(column * _cellWidth, row * _cellHeight, _cellWidth, _cellHeight)
            Next
        Next
        ReDim _destinationRectangles(_cellColumns * _cellRows - 1)
        ReDim _bufferAttributes(_cellColumns * _cellRows - 1)
        ReDim _bufferCharacters(_cellColumns * _cellRows - 1)
        For row = 0 To _cellRows - 1
            For column = 0 To _cellColumns - 1
                _destinationRectangles(row * _cellColumns + column) = New Rectangle(column * _cellWidth, row * _cellHeight, _cellWidth, _cellHeight)
                _bufferCharacters(row * _cellColumns + column) = CByte(_random.Next(256))
                _bufferAttributes(row * _cellColumns + column) = CByte(_random.Next(256))
            Next
        Next
    End Sub
    Protected Overrides Sub Initialize()
        _graphics.PreferredBackBufferWidth = _frameBufferWidth
        _graphics.PreferredBackBufferHeight = _frameBufferHeight
        _graphics.ApplyChanges()
        MyBase.Initialize()
    End Sub
    Protected Overrides Sub Update(gameTime As GameTime)
        MyBase.Update(gameTime)
    End Sub
    Protected Overrides Sub Draw(gameTime As GameTime)
        MyBase.Draw(gameTime)
        _spriteBatch.Begin(samplerState:=SamplerState.PointClamp)
        For index = 0 To _cellColumns * _cellRows - 1
            _spriteBatch.Draw(_fillTexture, _destinationRectangles(index), _colors(_bufferAttributes(index) \ 16))
            _spriteBatch.Draw(_texture, _destinationRectangles(index), _sourceRectangles(_bufferCharacters(index)), _colors(_bufferAttributes(index) And 15))
        Next
        _spriteBatch.End()
    End Sub
End Class
