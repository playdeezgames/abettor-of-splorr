Friend Class OldFrameBuffer
    Implements IOldFrameBuffer
    Private ReadOnly _columns As Integer
    Private ReadOnly _rows As Integer
    Private ReadOnly _characters As Byte()
    Private ReadOnly _attributes As Byte()
    Sub New(columns As Integer, rows As Integer, characters As Byte(), attributes As Byte())
        _columns = columns
        _rows = rows
        _characters = characters
        _attributes = attributes
    End Sub

    Public Sub SetCharacter(column As Integer, row As Integer, character As Byte) Implements IOldFrameBuffer.SetCharacter
        _characters(column + row * _columns) = character
    End Sub

    Public Sub SetAttribute(column As Integer, row As Integer, attribute As Byte) Implements IOldFrameBuffer.SetAttribute
        _attributes(column + row * _columns) = attribute
    End Sub

    Public Sub WriteText(column As Integer, row As Integer, text As String, attribute As Byte) Implements IOldFrameBuffer.WriteText
        For Each character In text
            SetCharacter(column, row, CByte(Asc(character) And 255))
            SetAttribute(column, row, attribute)
            column += 1
        Next
    End Sub

    Public Sub Fill(column As Integer, row As Integer, width As Integer, height As Integer, character As Byte, attribute As Byte) Implements IOldFrameBuffer.Fill
        For x = column To column + width - 1
            For y = row To row + height - 1
                SetCharacter(x, y, character)
                SetAttribute(x, y, attribute)
            Next
        Next
    End Sub

    Public Function GetCharacter(column As Integer, row As Integer) As Byte Implements IOldFrameBuffer.GetCharacter
        Return _characters(column + row * _columns)
    End Function

    Public Function GetAttribute(column As Integer, row As Integer) As Byte Implements IOldFrameBuffer.GetAttribute
        Return _attributes(column + row * _columns)
    End Function
End Class
