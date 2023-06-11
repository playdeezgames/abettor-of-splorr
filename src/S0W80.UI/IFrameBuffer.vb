Public Interface IFrameBuffer
    ReadOnly Property Rows As Integer
    ReadOnly Property Columns As Integer
    Function GetCell(column As Integer, row As Integer) As IFrameBufferCell
End Interface
