Public Class DisplayBuffer(Of THue)
    Inherits BaseDisplayBuffer(Of THue, Color)
    Private _texture As Texture2D
    Sub New(texture As Texture2D, transform As Func(Of THue, Color))
        MyBase.New((texture.Width, texture.Height), transform)
        _texture = texture
    End Sub

    Public Overrides Sub Commit()
        _texture.SetData(_buffer)
    End Sub
End Class
