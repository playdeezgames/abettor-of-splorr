Friend Class PlaceHolderState
    Inherits BaseGameState(Of Hue, Command, Sfx, GameState)
    Private thingie As OffscreenBuffer(Of Hue)
    Private _sprite As Sprite(Of Hue)
    Private ReadOnly spriteData As New List(Of String) From
        {
            "            ",
            "            ",
            "            ",
            "   ......   ",
            "  .XXXXXX.  ",
            " .XXXXXXXX. ",
            ".XX.X.XXXXX.",
            ".XX.X.XXXXX.",
            ".XX.X.XXXXX.",
            ".XXXXXXXXXX.",
            " .XXXXXXXX. ",
            "  ........  "
        }

    Public Sub New(parent As IGameController(Of Hue, Command, Sfx), setState As Action(Of GameState))
        MyBase.New(parent, setState)
        thingie = New OffscreenBuffer(Of Hue)((48, 48))
        _sprite = New Sprite(Of Hue)(spriteData, AddressOf BlobColors)
        Dim y = 0
        For Each line In spriteData
            Dim x = 0
            For Each character In line
                Dim h As Hue
                Select Case character
                    Case "X"c
                        h = Hue.LightCyan
                    Case "."c
                        h = Hue.Black
                    Case Else
                        h = Hue.LightMagenta
                End Select
                thingie.Fill((x * 4, y * 4), (4, 4), h)
                x += 1
            Next
            y += 1
        Next
    End Sub

    Private Function BlobColors(character As Char) As Hue
        Select Case character
            Case "X"c
                Return Hue.LightCyan
            Case "."c
                Return Hue.Black
            Case Else
                Return Hue.LightMagenta
        End Select
    End Function

    Public Overrides Sub HandleCommand(command As Command)
    End Sub

    Public Overrides Sub Render(displayBuffer As IPixelSink(Of Hue))
        displayBuffer.Fill((0, 0), (GameContext.FrameWidth, GameContext.FrameHeight), Hue.Blue)
        Dim drawer As New Drawer(Of Hue)(displayBuffer)
        drawer.
            MoveTo(GameContext.FrameWidth \ 4, GameContext.FrameHeight \ 4).
            Color(Hue.White).
            Down(GameContext.FrameHeight \ 2 - 1).
            Right(GameContext.FrameWidth \ 2 - 1).
            Up(GameContext.FrameHeight \ 2 - 1).
            Left(GameContext.FrameWidth \ 2 - 1).
            MoveTo(0, 0).
            Repeat(20, Function(d) d.Right(1).DownRight(1)).
            MoveTo(0, FrameHeight - 1).
            Repeat(20, Function(d) d.Right(1).UpRight(1)).
            MoveTo(FrameWidth - 1, 0).
            Repeat(20, Function(d) d.Left(1).DownLeft(1)).
            MoveTo(FrameWidth - 1, FrameHeight - 1).
            Repeat(20, Function(d) d.Left(1).UpLeft(1))
        _sprite.StretchTo(displayBuffer, (56, 22), (4, 4), Function(x) x <> Hue.LightMagenta)
    End Sub

    Public Overrides Sub Update(elapsedTime As TimeSpan)
    End Sub
End Class
