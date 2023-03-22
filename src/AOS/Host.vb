Public Class Host
    Inherits Game
    Private graphics As GraphicsDeviceManager
    Private spriteBatch As SpriteBatch
    Sub New()
        graphics = New GraphicsDeviceManager(Me)
        Content.RootDirectory = "Content"
    End Sub
    Protected Overrides Sub Initialize()
        MyBase.Initialize()
    End Sub
    Protected Overrides Sub LoadContent()
        spriteBatch = New SpriteBatch(GraphicsDevice)
    End Sub
    Protected Overrides Sub Update(gameTime As GameTime)
        MyBase.Update(gameTime)
    End Sub
    Protected Overrides Sub Draw(gameTime As GameTime)
        graphics.GraphicsDevice.Clear(Color.Magenta)
        MyBase.Draw(gameTime)
    End Sub
End Class
