Public Class Host
    Inherits Game
    Private _gameController As IGameController
    Private ReadOnly _graphics As GraphicsDeviceManager
    Private _spriteBatch As SpriteBatch
    Private _backBuffer As RenderTarget2D
    Private ReadOnly _frameBuffer As IFrameBuffer
    Private _solidTexture As Texture2D
    Private _fontTexture As Texture2D
    Private ReadOnly _sourceRectangles(256) As Rectangle
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
    Sub New(frameBuffer As IFrameBuffer, gameController As IGameController)
        _graphics = New GraphicsDeviceManager(Me)
        _frameBuffer = frameBuffer
        _gameController = gameController
        Content.RootDirectory = "Content"
    End Sub
    Protected Overrides Sub LoadContent()
        MyBase.LoadContent()
        _spriteBatch = New SpriteBatch(GraphicsDevice)
        _backBuffer = New RenderTarget2D(GraphicsDevice, ScreenWidth, ScreenHeight)
        _solidTexture = New Texture2D(GraphicsDevice, 1, 1)
        _solidTexture.SetData(New List(Of Color) From {Color.White}.ToArray)
        _fontTexture = Texture2D.FromFile(GraphicsDevice, FontFilename)
        Dim cellWidth = _fontTexture.Width \ 16
        Dim cellHeight = _fontTexture.Height \ 16
        For row = 0 To 15
            For column = 0 To 15
                _sourceRectangles(column + row * 16) = New Rectangle(cellWidth * column, cellHeight * row, cellWidth, cellHeight)
            Next
        Next
    End Sub
    Protected Overrides Sub Initialize()
        _graphics.PreferredBackBufferWidth = ScreenWidth
        _graphics.PreferredBackBufferHeight = ScreenHeight
        _graphics.IsFullScreen = True
        _graphics.ApplyChanges()
        MyBase.Initialize()
    End Sub
    Protected Overrides Sub Update(gameTime As GameTime)
        MyBase.Update(gameTime)
        _gameController.Update(_frameBuffer, gameTime.ElapsedGameTime.Ticks)
    End Sub
    Protected Overrides Sub Draw(gameTime As GameTime)
        UpdateBackBuffer()
        _spriteBatch.Begin()
        _spriteBatch.Draw(_backBuffer, New Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White)
        _spriteBatch.End()
        MyBase.Draw(gameTime)
    End Sub
    Private Sub UpdateBackBuffer()
        GraphicsDevice.SetRenderTarget(_backBuffer)
        Dim cellWidth = _backBuffer.Width \ _frameBuffer.Columns
        Dim cellHeight = _backBuffer.Height \ _frameBuffer.Rows
        _spriteBatch.Begin()
        For row = 0 To _frameBuffer.Rows - 1
            For column = 0 To _frameBuffer.Columns - 1
                Dim cell = _frameBuffer.GetCell(column, row)
                Dim destinationRect = New Rectangle(cellWidth * column, cellHeight * row, cellWidth, cellHeight)
                _spriteBatch.Draw(_solidTexture, destinationRect, _colors(cell.BackgroundColor))
                _spriteBatch.Draw(_fontTexture, destinationRect, _sourceRectangles(Asc(cell.Character) Mod _sourceRectangles.Length), _colors(cell.ForegroundColor))
            Next
        Next
        _spriteBatch.End()
        GraphicsDevice.SetRenderTarget(Nothing)
    End Sub
End Class
