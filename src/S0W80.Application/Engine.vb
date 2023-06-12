Public Class Engine
    Inherits BasePresenter
    Implements IEngine
    Private ReadOnly _frameBuffer As IFrameBuffer
    Private _lineBuffer As String = String.Empty
    Private _fullScreen As Boolean
    Private _scale As Integer
    Private _volume As Single
    Sub New(
           frameBuffer As IFrameBuffer,
           scale As Integer,
           fullScreen As Boolean,
           volume As Single)
        _frameBuffer = frameBuffer
        _fullScreen = fullScreen
        _scale = scale
        _volume = volume
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
                        Execute(_lineBuffer)
                        _lineBuffer = String.Empty
                    Case Else
                        _frameBuffer.Write(command)
                        _lineBuffer &= command
                End Select
            End If
        Next
    End Sub

    Private Sub Execute(lineBuffer As String)
        Dim tokens = lineBuffer.ToLowerInvariant.Split(" "c)
        If Not tokens.Any Then
            Return
        End If
        Select Case tokens(0)
            Case QuitText
                DoQuit()
            Case FullSceenText
                _fullScreen = Not _fullScreen
                DoResize(_scale, _fullScreen)
            Case ScaleText
                ExecuteScale(tokens.Skip(1))
            Case SfxTest
                DoSfx("RollDice")
            Case VolumeText
                ExecuteVolume(tokens.Skip(1))
        End Select
    End Sub

    Private Sub ExecuteVolume(tokens As IEnumerable(Of String))
        If Not tokens.Any Then
            Dim oldColor = _frameBuffer.ForegroundColor
            _frameBuffer.WriteLine("Volume needs a number!")
            _frameBuffer.ForegroundColor = oldColor
            Return
        End If
        Dim token = tokens(0)
        Dim newVolume As Single
        If Not Single.TryParse(token, newVolume) Then
            Dim oldColor = _frameBuffer.ForegroundColor
            _frameBuffer.WriteLine("Scale needs a number!")
            _frameBuffer.ForegroundColor = oldColor
            Return
        End If
        _volume = Math.Clamp(newVolume, 0.0F, 1.0F)
        DoVolume(_volume)
    End Sub

    Private Sub ExecuteScale(tokens As IEnumerable(Of String))
        If Not tokens.Any Then
            Dim oldColor = _frameBuffer.ForegroundColor
            _frameBuffer.WriteLine("Scale needs a number!")
            _frameBuffer.ForegroundColor = oldColor
            Return
        End If
        Dim token = tokens(0)
        Dim newScale As Integer
        If Not Int32.TryParse(token, newScale) Then
            Dim oldColor = _frameBuffer.ForegroundColor
            _frameBuffer.WriteLine("Scale needs a number!")
            _frameBuffer.ForegroundColor = oldColor
            Return
        End If
        _scale = Math.Clamp(newScale, 1, 12)
        DoResize(_scale, _fullScreen)
    End Sub
End Class
