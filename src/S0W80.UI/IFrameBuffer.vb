﻿Public Interface IFrameBuffer
    ReadOnly Property Rows As Integer
    ReadOnly Property Columns As Integer
    Function GetCell(column As Integer, row As Integer) As IFrameBufferCell
    Property CursorRow As Integer
    Property CursorColumn As Integer
    Property ForegroundColor As Integer
    Property BackgroundColor As Integer
    Sub Write(chracter As Char)
    Sub Write(text As String)
    Sub WriteLine(text As String)
End Interface