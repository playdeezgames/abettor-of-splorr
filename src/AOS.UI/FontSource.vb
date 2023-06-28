﻿Imports System.IO
Imports System.Text.Json
Public Class FontSource
    Implements IFontSource
    Private ReadOnly fonts As New Dictionary(Of String, Font)
    Sub New(fontFilenames As IReadOnlyDictionary(Of String, String))
        For Each entry In fontFilenames
            fonts(entry.Key) = New Font(JsonSerializer.Deserialize(Of FontData)(File.ReadAllText(entry.Value)))
        Next
    End Sub
    Public Function GetFont(gameFont As String) As Font Implements IFontSource.GetFont
        Return fonts(gameFont)
    End Function
End Class