Imports System.IO
Imports System.Text.Json
Public MustInherit Class UIContext(Of TGameContext)
    Implements IUIContext(Of TGameContext)
    Private ReadOnly fonts As New Dictionary(Of String, Font)
    ReadOnly Property ViewSize As (Integer, Integer) Implements IUIContext(Of TGameContext).ViewSize
    Public MustOverride ReadOnly Property AvailableWindowSizes As IEnumerable(Of (Integer, Integer)) Implements IUIContext(Of TGameContext).AvailableWindowSizes

    Public ReadOnly Property Game As TGameContext Implements IUIContext(Of TGameContext).Game

    Sub New(game As TGameContext, fontFilenames As IReadOnlyDictionary(Of String, String), viewSize As (Integer, Integer))
        Me.Game = game
        Me.ViewSize = viewSize
        For Each entry In fontFilenames
            fonts(entry.Key) = New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText(entry.Value)))
        Next
    End Sub

    Public Sub ShowStatusBar(displayBuffer As IPixelSink, font As Font, text As String, foreground As Integer, background As Integer) Implements IUIContext(Of TGameContext).ShowStatusBar
        displayBuffer.Fill((0, ViewSize.Item2 - font.Height), (ViewSize.Item1, font.Height), background)
        font.WriteText(displayBuffer, (ViewSize.Item1 \ 2 - font.TextWidth(text) \ 2, ViewSize.Item2 - font.Height), text, foreground)
    End Sub

    Public Function Font(gameFont As String) As Font Implements IUIContext(Of TGameContext).Font
        Return fonts(gameFont)
    End Function

    Public MustOverride Sub ShowSplashContent(displayBuffer As IPixelSink, font As Font) Implements IUIContext(Of TGameContext).ShowSplashContent

    Public Sub ShowHeader(displayBuffer As IPixelSink, font As Font, text As String, foreground As Integer, background As Integer) Implements IUIContext(Of TGameContext).ShowHeader
        displayBuffer.Fill((0, 0), (ViewSize.Item1, font.Height), background)
        font.WriteText(displayBuffer, (ViewSize.Item1 \ 2 - font.TextWidth(text) \ 2, 0), text, foreground)
    End Sub

    Public Function ControlsText(aButtonText As String, bButtonText As String) As String Implements IUIContext(Of TGameContext).ControlsText
        Return $"Space/(A) - {aButtonText} | Esc/(B) - {bButtonText}"
    End Function

    Public MustOverride Sub ShowAboutContent(displayBuffer As IPixelSink, font As Font) Implements IUIContext(Of TGameContext).ShowAboutContent
    Public MustOverride Sub AbandonGame() Implements IUIContext(Of TGameContext).AbandonGame
    Public MustOverride Sub LoadGame(slot As Integer) Implements IUIContext(Of TGameContext).LoadGame
    Public MustOverride Sub SaveGame(slot As Integer) Implements IUIContext(Of TGameContext).SaveGame
    Public MustOverride Function DoesSlotExist(slot As Integer) As Boolean Implements IUIContext(Of TGameContext).DoesSlotExist
End Class
