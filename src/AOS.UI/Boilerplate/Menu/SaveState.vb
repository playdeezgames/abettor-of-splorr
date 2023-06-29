Friend Class SaveState
    Inherits BasePickerState
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context, "Save Game", context.ControlsText("Select", "Cancel"), BoilerplateState.GameMenu)
    End Sub
    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Dim slotIndex = CInt(value.Item2)
        Context.SaveGame(slotIndex)
        SetState(BoilerplateState.GameMenu)
    End Sub
    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Dim result As New List(Of (String, String))
        For slotIndex = 1 To 5
            result.Add(($"Slot {slotIndex}{If(Context.DoesSlotExist(slotIndex), "(will overwrite)", "")}", $"{slotIndex}"))
        Next
        Return result
    End Function
End Class
