Public MustInherit Class BaseGameController(Of THue, TCommand, TSfx)
    Implements ICommandHandler(Of TCommand)
    Implements IRenderer(Of THue)
    Implements ISfxHandler(Of TSfx)
    Implements IWindowSizerizer
    Implements IUpdatorator
    Private _windowSize As (Integer, Integer)
    Public Property Size As (Integer, Integer) Implements IWindowSizerizer.Size
        Get
            Return _windowSize
        End Get
        Set(value As (Integer, Integer))
            If value.Item1 <> _windowSize.Item1 OrElse value.Item2 <> _windowSize.Item2 Then
                _windowSize = value
                RaiseEvent OnSizeChange(_windowSize)
            End If
        End Set
    End Property
    Public Property Volume As Single Implements ISfxHandler(Of TSfx).Volume
    Sub New(windowSize As (Integer, Integer), volume As Single)
        _windowSize = windowSize
        Me.Volume = volume
    End Sub
    Public Event OnSfx As ISfxHandler(Of TSfx).OnSfxEventHandler Implements ISfxHandler(Of TSfx).OnSfx
    Public Event OnSizeChange(newSize As (Integer, Integer)) Implements IWindowSizerizer.OnSizeChange
    Public MustOverride Sub HandleCommand(command As TCommand) Implements ICommandHandler(Of TCommand).HandleCommand
    Public MustOverride Sub Render(displayBuffer As IPixelSink(Of THue)) Implements IRenderer(Of THue).Render

    Public Sub PlaySfx(sfx As TSfx) Implements ISfxHandler(Of TSfx).PlaySfx
        RaiseEvent OnSfx(sfx)
    End Sub
End Class
