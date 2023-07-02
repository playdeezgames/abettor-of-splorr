Friend Class WindowSizeState
    Inherits BasePickerState(Of String)
    Public Sub New(parent As IGameController, setState As Action(Of String, Boolean), context As IUIContext)
        MyBase.New(parent, setState, context, "<placeholder>", context.ControlsText("Select", "Cancel"), BoilerplateState.Options)
    End Sub
    Protected Overrides Sub OnActivateMenuItem(value As (String, String))
        Dim tokens = value.Item2.Split("x"c)
        Dim width = CInt(tokens(0))
        Dim height = CInt(tokens(1))
        Parent.Size = (width, height)
        SaveConfig()
        HeaderText = $"Current Size: {Size.Item1}x{Size.Item2}"
    End Sub
    Protected Overrides Function InitializeMenuItems() As List(Of (String, String))
        Dim currentSize = $"{Size.Item1}x{Size.Item2}"
        HeaderText = $"Current Size: {currentSize}"
        Dim menuItems = Context.AvailableWindowSizes.Select(Function(x) ($"{x.Item1}x{x.Item2}", $"{x.Item1}x{x.Item2}")).ToList
        MenuItemIndex = Math.Max(0, menuItems.FindIndex(Function(x) x.Item1 = currentSize))
        Return menuItems
    End Function
End Class
