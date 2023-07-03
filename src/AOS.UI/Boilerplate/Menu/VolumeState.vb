﻿Friend Class VolumeState(Of TGameContext)
    Inherits BasePickerState(Of TGameContext, Single)
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of TGameContext))
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Select", "Cancel"), BoilerplateState.Options)
    End Sub
    Protected Overrides Sub OnActivateMenuItem(value As (String, Single))
        Volume = value.Item2
        PlaySfx(BoilerplateSfx.SfxVolumeTest)
        SaveConfig()
        HeaderText = $"Volume (Currently: {Volume * 100:f0}%)"
    End Sub
    Protected Overrides Function InitializeMenuItems() As List(Of (String, Single))
        Dim currentVolume = $"{Volume * 100:f0}%"
        HeaderText = $"Volume (Currently: {currentVolume})"
        Dim result As New List(Of (String, Single))
        For index = 0 To 10
            Dim menuItemText = $"{index * 10}%"
            result.Add((menuItemText, index / 10.0F))
            If menuItemText = currentVolume Then
                MenuItemIndex = index
            End If
        Next
        Return result
    End Function
End Class
