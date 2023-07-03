﻿Friend Class GameMenuState(Of TGameContext)
    Inherits BasePickerState(Of TGameContext, String)
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of TGameContext))
        MyBase.New(parent, setState, context, "Game Menu", context.ControlsText("Select", "Cancel"), BoilerplateState.Neutral)
    End Sub
    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Select Case value.Item2
            Case ContinueGameText
                SetState(BoilerplateState.Neutral)
            Case SaveGameText
                SetState(BoilerplateState.Save)
            Case OptionsText
                SetStates(BoilerplateState.Options, BoilerplateState.GameMenu)
            Case AbandonGameText
                SetState(BoilerplateState.Abandon)
        End Select
    End Sub
    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Return New List(Of (String, String)) From
            {
                (ContinueGameText, ContinueGameText),
                (SaveGameText, SaveGameText),
                (OptionsText, OptionsText),
                (AbandonGameText, AbandonGameText)
            }
    End Function
End Class
