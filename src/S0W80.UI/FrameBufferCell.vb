Friend Class FrameBufferCell
    Implements IFrameBufferCell
    Private _foregroundColor As Integer
    Private _backgroundColor As Integer
    Public Property BackgroundColor As Integer Implements IFrameBufferCell.BackgroundColor
        Get
            Return _backgroundColor
        End Get
        Set(value As Integer)
            _backgroundColor = value And 15
        End Set
    End Property
    Public Property ForegroundColor As Integer Implements IFrameBufferCell.ForegroundColor
        Get
            Return _foregroundColor
        End Get
        Set(value As Integer)
            _foregroundColor = value And 15
        End Set
    End Property
    Public Property Character As Char Implements IFrameBufferCell.Character
End Class
