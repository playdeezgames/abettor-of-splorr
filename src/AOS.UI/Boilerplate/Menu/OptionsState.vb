﻿Friend Class OptionsState(Of TGameContext)
    Inherits BasePickerState(Of TGameContext, String)
    Public Sub New(
                  parent As IGameController,
                  setState As Action(Of String, Boolean),
                  context As IUIContext(Of TGameContext))
        MyBase.New(parent, setState, context, "Options", context.ControlsText("Select", "Cancel"), Nothing)
    End Sub

    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Select Case value.Item2
            Case ToggleFullScreenText
                Parent.FullScreen = Not Parent.FullScreen
                SaveConfig()
            Case SetWindowSizeText
                SetState(BoilerplateState.WindowSize)
            Case SetVolumeText
                SetState(BoilerplateState.Volume)
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Return New List(Of (String, String)) From
            {
                (ToggleFullScreenText, ToggleFullScreenText),
                (SetWindowSizeText, SetWindowSizeText),
                (SetVolumeText, SetVolumeText)
            }
    End Function
End Class
