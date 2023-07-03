﻿Friend Class MainMenuState(Of TGameContext)
    Inherits BasePickerState(Of TGameContext, String)
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext(Of TGameContext))
        MyBase.New(parent, setState, context, "Main Menu", context.ControlsText("Select", "Quit"), BoilerplateState.ConfirmQuit)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Select Case value.Item2
            Case QuitText
                SetState(BoilerplateState.ConfirmQuit)
            Case OptionsText
                SetStates(BoilerplateState.Options, BoilerplateState.MainMenu)
            Case AboutText
                SetState(BoilerplateState.About)
            Case EmbarkText
                SetState(BoilerplateState.Embark)
            Case LoadText
                SetState(BoilerplateState.Load)
            Case Else
                Throw New NotImplementedException()
        End Select
    End Sub

    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Return New List(Of (String, String)) From
            {
                (EmbarkText, EmbarkText),
                (LoadText, LoadText),
                (OptionsText, OptionsText),
                (AboutText, AboutText),
                (QuitText, QuitText)
            }
    End Function
End Class
