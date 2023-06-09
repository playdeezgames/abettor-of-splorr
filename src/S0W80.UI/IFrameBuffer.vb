Public Interface IFrameBuffer
    Sub SetCharacter(column As Integer, row As Integer, character As Byte)
    Sub SetAttribute(column As Integer, row As Integer, attribute As Byte)
    Function GetCharacter(column As Integer, row As Integer) As Byte
    Function GetAttribute(column As Integer, row As Integer) As Byte
    Sub WriteText(column As Integer, row As Integer, text As String, attribute As Byte)
    Sub Fill(column As Integer, row As Integer, width As Integer, height As Integer, character As Byte, attribute As Byte)
End Interface
