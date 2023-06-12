Public Class Engine
    Inherits BasePresenter
    Implements IEngine
    Private ReadOnly _frameBuffer As IFrameBuffer
    Private _lineBuffer As String = String.Empty
    Private _fullScreen As Boolean
    Sub New(frameBuffer As IFrameBuffer, fullScreen As Boolean)
        _frameBuffer = frameBuffer
        _fullScreen = fullScreen
    End Sub
    Public Sub Update(commands As IEnumerable(Of String), ticks As Long) Implements IEngine.Update
        For Each command In commands
            If command.Length = 1 Then
                Select Case command
                    Case ChrW(8)
                        _frameBuffer.CursorColumn -= 1
                        _frameBuffer.Write(" "c)
                        _frameBuffer.CursorColumn -= 1
                        _lineBuffer = If(_lineBuffer.Any, _lineBuffer.Substring(0, _lineBuffer.Length - 1), _lineBuffer)
                    Case ChrW(13)
                        _frameBuffer.WriteLine("")
                        DoStuff(_lineBuffer)
                        _lineBuffer = String.Empty
                    Case Else
                        _frameBuffer.Write(command)
                        _lineBuffer &= command
                End Select
            End If
        Next
    End Sub

    Private Sub DoStuff(lineBuffer As String)
        Select Case lineBuffer.ToLowerInvariant()
            Case QuitText
                DoQuit()
            Case FullSceenText
                _fullScreen = Not _fullScreen
                DoFullScreen(_fullScreen)
        End Select
    End Sub
End Class
