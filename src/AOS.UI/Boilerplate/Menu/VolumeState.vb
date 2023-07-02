Friend Class VolumeState
    Inherits BasePickerState(Of String)
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Select", "Cancel"), BoilerplateState.Options)
    End Sub
    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Dim percent = CInt(value.Item2)
        Volume = percent * 0.01F
        PlaySfx(BoilerplateSfx.SfxVolumeTest)
        SaveConfig()
        HeaderText = $"Volume (Currently: {Volume * 100:f0}%)"
    End Sub
    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Dim currentVolume = $"{Volume * 100:f0}%"
        HeaderText = $"Volume (Currently: {currentVolume})"
        Dim result As New List(Of (String, String))
        For index = 0 To 10
            Dim menuItemText = $"{index * 10}%"
            result.Add((menuItemText, $"{index * 10}"))
            If menuItemText = currentVolume Then
                MenuItemIndex = index
            End If
        Next
        Return result
    End Function
End Class
