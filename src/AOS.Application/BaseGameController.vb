Public MustInherit Class BaseGameController
    Implements ICommandHandler(Of Command)
    Implements IRenderer(Of Hue)
    Implements ISfxHandler(Of Sfx)
    Implements IWindowSizerizer
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
    Public Property Volume As Single Implements ISfxHandler(Of Sfx).Volume
    Sub New(windowSize As (Integer, Integer), volume As Single)
        _windowSize = windowSize
        Me.Volume = volume
    End Sub
    Public Event OnSfx As ISfxHandler(Of Sfx).OnSfxEventHandler Implements ISfxHandler(Of Sfx).OnSfx
    Public Event OnSizeChange(newSize As (Integer, Integer)) Implements IWindowSizerizer.OnSizeChange
    Public MustOverride Sub HandleCommand(command As Command) Implements ICommandHandler(Of Command).HandleCommand
    Public MustOverride Sub Render(displayBuffer As IPixelSink(Of Hue)) Implements IRenderer(Of Hue).Render

    Public Sub PlaySfx(sfx As Sfx) Implements ISfxHandler(Of Sfx).PlaySfx
        RaiseEvent OnSfx(sfx)
    End Sub
End Class
