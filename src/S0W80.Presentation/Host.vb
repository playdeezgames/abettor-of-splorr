Imports System.Data.Common

Public Class Host
    Inherits Game
    Private _keyboardState As KeyboardState
    Private ReadOnly _engine As IEngine
    Private ReadOnly _presenter As IPresenter
    Private ReadOnly _graphics As GraphicsDeviceManager
    Private _spriteBatch As SpriteBatch
    Private _backBuffer As RenderTarget2D
    Private ReadOnly _frameBuffer As IFrameBuffer
    Private _solidTexture As Texture2D
    Private _fontTexture As Texture2D
    Private _scale As Integer
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
    Sub New(frameBuffer As IFrameBuffer, engine As IEngine, presenter As IPresenter, Optional scale As Integer = 2)
        _graphics = New GraphicsDeviceManager(Me)
        _frameBuffer = frameBuffer
        _engine = engine
        _scale = scale
        _presenter = presenter
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
        _graphics.PreferredBackBufferWidth = ScreenWidth * _scale
        _graphics.PreferredBackBufferHeight = ScreenHeight * _scale
        _graphics.IsFullScreen = False
        _graphics.ApplyChanges()
        AddHandler _presenter.Quit, AddressOf HandleQuit
        MyBase.Initialize()
    End Sub

    Private Sub HandleQuit()
        Me.Exit()
    End Sub

    Protected Overrides Sub Update(gameTime As GameTime)
        MyBase.Update(gameTime)
        _engine.Update(UpdateKeyState(), gameTime.ElapsedGameTime.Ticks)
    End Sub

    Private ReadOnly _characters As IReadOnlyDictionary(Of Boolean, IReadOnlyDictionary(Of Keys, Char)) =
        New Dictionary(Of Boolean, IReadOnlyDictionary(Of Keys, Char)) From
        {
            {
                False,
                New Dictionary(Of Keys, Char) From
                {
                    {Keys.A, "a"c},
                    {Keys.B, "b"c},
                    {Keys.C, "c"c},
                    {Keys.D, "d"c},
                    {Keys.E, "e"c},
                    {Keys.F, "f"c},
                    {Keys.G, "g"c},
                    {Keys.H, "h"c},
                    {Keys.I, "i"c},
                    {Keys.J, "j"c},
                    {Keys.K, "k"c},
                    {Keys.L, "l"c},
                    {Keys.M, "m"c},
                    {Keys.N, "n"c},
                    {Keys.O, "o"c},
                    {Keys.P, "p"c},
                    {Keys.Q, "q"c},
                    {Keys.R, "r"c},
                    {Keys.S, "s"c},
                    {Keys.T, "t"c},
                    {Keys.U, "u"c},
                    {Keys.V, "v"c},
                    {Keys.W, "w"c},
                    {Keys.X, "x"c},
                    {Keys.Y, "y"c},
                    {Keys.Z, "z"c},
                    {Keys.D0, "0"c},
                    {Keys.D1, "1"c},
                    {Keys.D2, "2"c},
                    {Keys.D3, "3"c},
                    {Keys.D4, "4"c},
                    {Keys.D5, "5"c},
                    {Keys.D6, "6"c},
                    {Keys.D7, "7"c},
                    {Keys.D8, "8"c},
                    {Keys.D9, "9"c},
                    {Keys.OemMinus, "-"c},
                    {Keys.OemPlus, "="c},
                    {Keys.OemOpenBrackets, "["c},
                    {Keys.OemCloseBrackets, "]"c},
                    {Keys.OemPipe, "\"c},
                    {Keys.OemSemicolon, ";"c},
                    {Keys.OemQuotes, "'"c},
                    {Keys.OemComma, ","c},
                    {Keys.OemPeriod, "."c},
                    {Keys.OemQuestion, "/"c},
                    {Keys.OemTilde, "`"c}
                }
            },
            {
                True,
                New Dictionary(Of Keys, Char) From
                {
                    {Keys.A, "A"c},
                    {Keys.B, "B"c},
                    {Keys.C, "C"c},
                    {Keys.D, "D"c},
                    {Keys.E, "E"c},
                    {Keys.F, "F"c},
                    {Keys.G, "G"c},
                    {Keys.H, "H"c},
                    {Keys.I, "I"c},
                    {Keys.J, "J"c},
                    {Keys.K, "K"c},
                    {Keys.L, "L"c},
                    {Keys.M, "M"c},
                    {Keys.N, "N"c},
                    {Keys.O, "O"c},
                    {Keys.P, "P"c},
                    {Keys.Q, "Q"c},
                    {Keys.R, "R"c},
                    {Keys.S, "S"c},
                    {Keys.T, "T"c},
                    {Keys.U, "U"c},
                    {Keys.V, "V"c},
                    {Keys.W, "W"c},
                    {Keys.X, "X"c},
                    {Keys.Y, "Y"c},
                    {Keys.Z, "Z"c},
                    {Keys.D0, ")"c},
                    {Keys.D1, "!"c},
                    {Keys.D2, "@"c},
                    {Keys.D3, "#"c},
                    {Keys.D4, "$"c},
                    {Keys.D5, "%"c},
                    {Keys.D6, "^"c},
                    {Keys.D7, "&"c},
                    {Keys.D8, "*"c},
                    {Keys.D9, "("c},
                    {Keys.OemMinus, "_"c},
                    {Keys.OemPlus, "+"c},
                    {Keys.OemOpenBrackets, "{"c},
                    {Keys.OemCloseBrackets, "}"c},
                    {Keys.OemPipe, "|"c},
                    {Keys.OemSemicolon, ":"c},
                    {Keys.OemQuotes, """"c},
                    {Keys.OemComma, "<"c},
                    {Keys.OemPeriod, ">"c},
                    {Keys.OemQuestion, "?"c},
                    {Keys.OemTilde, "~"c}
                }
            }
        }

    Private Function UpdateKeyState() As IEnumerable(Of String)
        Dim result As New List(Of String)
        Dim keyboardState = Keyboard.GetState()
        Dim capsLock = keyboardState.CapsLock
        Dim numLock = keyboardState.NumLock
        Dim shift = keyboardState.IsKeyDown(Keys.LeftShift) OrElse keyboardState.IsKeyDown(Keys.RightShift)
        For Each pressedKey In keyboardState.GetPressedKeys().Where(Function(x) _keyboardState.IsKeyUp(x))
            Select Case pressedKey
                Case Keys.Space
                    result.Add(" "c)
                Case Keys.Tab
                    result.Add(Chr(9))
                Case Keys.Back
                    result.Add(Chr(8))
                Case Keys.Enter
                    result.Add(Chr(13))
                Case Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K, Keys.L, Keys.M, Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z
                    result.Add(_characters(shift Xor capsLock)(pressedKey))
                Case Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.OemMinus, Keys.OemPlus, Keys.OemOpenBrackets, Keys.OemCloseBrackets, Keys.OemPipe, Keys.OemSemicolon, Keys.OemQuotes, Keys.OemComma, Keys.OemPeriod, Keys.OemQuestion, Keys.OemTilde
                    result.Add(_characters(shift)(pressedKey))
                Case Keys.LeftShift, Keys.RightShift, Keys.CapsLock, Keys.NumLock
                    'ignore!
                Case Else
                    result.Add(pressedKey.ToString)
            End Select
        Next
        _keyboardState = keyboardState
        Return result
    End Function

    Protected Overrides Sub Draw(gameTime As GameTime)
        UpdateBackBuffer()
        _spriteBatch.Begin(samplerState:=SamplerState.PointClamp)
        _spriteBatch.Draw(_backBuffer, New Rectangle(0, 0, ScreenWidth * _scale, ScreenHeight * _scale), Color.White)
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
        If _frameBuffer.CursorStart <= _frameBuffer.CursorEnd Then
            Dim destinationRect = New Rectangle(cellWidth * _frameBuffer.CursorColumn, cellHeight * _frameBuffer.CursorRow + _frameBuffer.CursorStart, cellWidth, _frameBuffer.CursorEnd - _frameBuffer.CursorStart + 1)
            _spriteBatch.Draw(_solidTexture, destinationRect, _colors(_frameBuffer.ForegroundColor))
        End If
        _spriteBatch.End()
        GraphicsDevice.SetRenderTarget(Nothing)
    End Sub
End Class
