Imports System.IO
Imports System.Text.Json
Public MustInherit Class UIContext
    Implements IUIContext
    Private ReadOnly fonts As New Dictionary(Of String, Font)
    ReadOnly Property ViewSize As (Integer, Integer) Implements IUIContext.ViewSize
    Sub New(fontFilenames As IReadOnlyDictionary(Of String, String), viewSize As (Integer, Integer))
        Me.ViewSize = viewSize
        For Each entry In fontFilenames
            fonts(entry.Key) = New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText(entry.Value)))
        Next
    End Sub

    Public Sub ShowStatusBar(displayBuffer As IPixelSink, font As Font, text As String, foreground As Integer, background As Integer) Implements IUIContext.ShowStatusBar
        displayBuffer.Fill((0, ViewSize.Item2 - font.Height), (ViewSize.Item1, font.Height), background)
        font.WriteText(displayBuffer, (ViewSize.Item1 \ 2 - font.TextWidth(text) \ 2, ViewSize.Item2 - font.Height), text, foreground)
    End Sub

    Public Function Font(gameFont As String) As Font Implements IUIContext.Font
        Return fonts(gameFont)
    End Function

    Public MustOverride Sub ShowSplashContent(displayBuffer As IPixelSink, font As Font) Implements IUIContext.ShowSplashContent
End Class
