﻿Public MustInherit Class BasePickerState(Of TGameContext, TItem)
    Inherits BaseGameState(Of TGameContext)
    Private _menuItems As New List(Of (String, TItem))
    Protected MenuItemIndex As Integer
    Private ReadOnly _statusBarText As String
    Protected Property HeaderText As String
    Private ReadOnly _cancelGameState As String
    Public Sub New(
                  parent As IGameController,
                  setState As Action(Of String, Boolean),
                  context As IUIContext(Of TGameContext),
                  headerText As String,
                  statusBarText As String,
                  cancelGameState As String)
        MyBase.New(parent, setState, context)
        _statusBarText = statusBarText
        _cancelGameState = cancelGameState
        Me.HeaderText = headerText
    End Sub
    Public Overrides Sub HandleCommand(cmd As String)
        Select Case cmd
            Case Command.B
                SetState(_cancelGameState)
            Case Command.A
                OnActivateMenuItem(_menuItems(MenuItemIndex))
            Case Command.Up
                MenuItemIndex = (MenuItemIndex + _menuItems.Count - 1) Mod _menuItems.Count
            Case Command.Down
                MenuItemIndex = (MenuItemIndex + 1) Mod _menuItems.Count
        End Select
    End Sub
    Protected MustOverride Sub OnActivateMenuItem(value As (String, TItem))
    Public Overrides Sub Render(displayBuffer As IPixelSink)
        displayBuffer.Fill((0, 0), (Context.ViewSize.Item1, Context.ViewSize.Item2), BoilerplateHue.Black)
        Dim font = Context.Font(UIFont)
        displayBuffer.Fill((0, Context.ViewSize.Item2 \ 2 - font.Height \ 2), (Context.ViewSize.Item1, font.Height), BoilerplateHue.Blue)
        Dim y = Context.ViewSize.Item2 \ 2 - font.Height \ 2 - MenuItemIndex * font.Height
        Dim index = 0
        For Each menuItem In _menuItems
            Dim x = Context.ViewSize.Item1 \ 2 - font.TextWidth(menuItem.Item1) \ 2
            Dim h = If(index = MenuItemIndex, BoilerplateHue.Black, BoilerplateHue.Blue)
            font.WriteText(displayBuffer, (x, y), menuItem.Item1, h)
            index += 1
            y += font.Height
        Next
        Context.ShowHeader(displayBuffer, font, HeaderText, BoilerplateHue.Orange, BoilerplateHue.Black)
        Context.ShowStatusBar(displayBuffer, font, _statusBarText, BoilerplateHue.Black, BoilerplateHue.LightGray)
    End Sub
    Public Overrides Sub OnStart()
        MenuItemIndex = 0
        _menuItems = InitializeMenuItems()
    End Sub
    Protected MustOverride Function InitializeMenuItems() As List(Of (String, TItem))
End Class
