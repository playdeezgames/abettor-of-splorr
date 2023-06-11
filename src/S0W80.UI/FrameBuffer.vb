Public Class FrameBuffer
    Implements IFrameBuffer
    Private ReadOnly _cells As List(Of IFrameBufferCell)
    Sub New(columns As Integer, rows As Integer)
        Me.Columns = columns
        Me.Rows = rows
        _cells = New List(Of IFrameBufferCell)
        While _cells.Count < columns * rows
            _cells.Add(New FrameBufferCell)
        End While
    End Sub
    Public ReadOnly Property Rows As Integer Implements IFrameBuffer.Rows
    Public ReadOnly Property Columns As Integer Implements IFrameBuffer.Columns

    Public Function GetCell(column As Integer, row As Integer) As IFrameBufferCell Implements IFrameBuffer.GetCell
        If column < 0 OrElse column >= Columns OrElse row < 0 OrElse row >= Rows Then
            Return Nothing
        End If
        Return _cells(column + row * Columns)
    End Function
End Class
