Imports System.Net.Http.Headers

Public Class FrameBuffer
    Implements IFrameBuffer
    Private ReadOnly _cells As List(Of IFrameBufferCell)
    Private _cursorRow As Integer
    Private _cursorColumn As Integer
    Private _foregroundColor As Integer
    Private _backgroundColor As Integer
    Private _cursorStart As Integer
    Private _cursorEnd As Integer
    Sub New(columns As Integer, rows As Integer, Optional foregroundColor As Integer = 7, Optional backgroundColor As Integer = 0, Optional cursorStart As Integer = 14, Optional cursorEnd As Integer = 15)
        Me.Columns = columns
        Me.Rows = rows
        Me.ForegroundColor = foregroundColor
        Me.BackgroundColor = backgroundColor
        Me.CursorStart = cursorStart
        Me.CursorEnd = cursorEnd
        _cells = New List(Of IFrameBufferCell)
        While _cells.Count < columns * rows
            _cells.Add(New FrameBufferCell)
        End While
    End Sub
    Public ReadOnly Property Rows As Integer Implements IFrameBuffer.Rows
    Public ReadOnly Property Columns As Integer Implements IFrameBuffer.Columns
    Public Property CursorRow As Integer Implements IFrameBuffer.CursorRow
        Get
            Return _cursorRow
        End Get
        Set(value As Integer)
            _cursorRow = Math.Clamp(value, 0, Rows - 1)
        End Set
    End Property
    Public Property CursorColumn As Integer Implements IFrameBuffer.CursorColumn
        Get
            Return _cursorColumn
        End Get
        Set(value As Integer)
            _cursorColumn = Math.Clamp(value, 0, Columns - 1)
        End Set
    End Property
    Public Property ForegroundColor As Integer Implements IFrameBuffer.ForegroundColor
        Get
            Return _foregroundColor
        End Get
        Set(value As Integer)
            _foregroundColor = value And 15
        End Set
    End Property
    Public Property BackgroundColor As Integer Implements IFrameBuffer.BackgroundColor
        Get
            Return _backgroundColor
        End Get
        Set(value As Integer)
            _backgroundColor = value And 15
        End Set
    End Property

    Public Property CursorStart As Integer Implements IFrameBuffer.CursorStart
        Get
            Return _cursorStart
        End Get
        Set(value As Integer)
            _cursorStart = Math.Clamp(value, 0, 15)
        End Set
    End Property

    Public Property CursorEnd As Integer Implements IFrameBuffer.CursorEnd
        Get
            Return _cursorEnd
        End Get
        Set(value As Integer)
            _cursorEnd = Math.Clamp(value, 0, 15)
        End Set
    End Property

    Public Sub Write(text As String) Implements IFrameBuffer.Write
        For Each character In text
            Write(character)
        Next
    End Sub
    Public Sub Write(character As Char) Implements IFrameBuffer.Write
        With GetCell(CursorColumn, CursorRow)
            .BackgroundColor = BackgroundColor
            .ForegroundColor = ForegroundColor
            .Character = character
        End With
        If CursorColumn < Columns - 1 Then
            CursorColumn += 1
            Return
        End If
        CursorColumn = 0
        If CursorRow < Rows - 1 Then
            CursorRow += 1
            Return
        End If
        ScrollBuffer()
    End Sub
    Private Sub ScrollBuffer()
        For row = 0 To Rows - 2
            For column = 0 To Columns - 1
                Dim fromCell = GetCell(column, row + 1)
                With GetCell(column, row)
                    .BackgroundColor = fromCell.BackgroundColor
                    .ForegroundColor = fromCell.ForegroundColor
                    .Character = fromCell.Character
                End With
            Next
        Next
        For column = 0 To Columns - 1
            With GetCell(column, Rows - 1)
                .BackgroundColor = BackgroundColor
                .ForegroundColor = ForegroundColor
                .Character = " "c
            End With
        Next
    End Sub
    Public Sub WriteLine(text As String) Implements IFrameBuffer.WriteLine
        Write(text)
        Do
            Write(" "c)
        Loop Until CursorColumn = 0
    End Sub
    Public Function GetCell(column As Integer, row As Integer) As IFrameBufferCell Implements IFrameBuffer.GetCell
        If column < 0 OrElse column >= Columns OrElse row < 0 OrElse row >= Rows Then
            Return Nothing
        End If
        Return _cells(column + row * Columns)
    End Function
End Class
