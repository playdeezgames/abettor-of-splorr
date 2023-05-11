Public Class Drawer(Of THue)
    Private X As Integer
    Private Y As Integer
    Private Hue As THue
    Private sink As IPixelSink(Of THue)
    Sub New(
           sink As IPixelSink(Of THue),
           Optional position As (Integer, Integer) = Nothing,
           Optional hue As THue = Nothing)
        Me.sink = sink
        X = position.Item1
        Y = position.Item2
        Me.Hue = hue
    End Sub
    Const Zero = 0
    Public Function Repeat(iterationCount As Integer, operation As Func(Of Drawer(Of THue), Drawer(Of THue))) As Drawer(Of THue)
        Dim result As Drawer(Of THue) = Me
        While iterationCount > Zero
            result = operation(result)
            iterationCount -= 1
        End While
        Return result
    End Function

    Public Function DownRight(stepCount As Integer) As Drawer(Of THue)
        While stepCount > Zero
            sink.SetPixel(X, Y, Hue)
            Y += 1
            X += 1
            stepCount -= 1
        End While
        Return Me
    End Function

    Public Function UpRight(stepCount As Integer) As Drawer(Of THue)
        While stepCount > Zero
            sink.SetPixel(X, Y, Hue)
            Y -= 1
            X += 1
            stepCount -= 1
        End While
        Return Me
    End Function
    Public Function DownLeft(stepCount As Integer) As Drawer(Of THue)
        While stepCount > Zero
            sink.SetPixel(X, Y, Hue)
            Y += 1
            X -= 1
            stepCount -= 1
        End While
        Return Me
    End Function

    Public Function UpLeft(stepCount As Integer) As Drawer(Of THue)
        While stepCount > Zero
            sink.SetPixel(X, Y, Hue)
            Y -= 1
            X -= 1
            stepCount -= 1
        End While
        Return Me
    End Function

    Public Function MoveTo(x As Integer, y As Integer) As Drawer(Of THue)
        Me.X = x
        Me.Y = y
        Return Me
    End Function

    Public Function Color(hue As THue) As Drawer(Of THue)
        Me.Hue = hue
        Return Me
    End Function

    Public Function Up(stepCount As Integer) As Drawer(Of THue)
        While stepCount > Zero
            sink.SetPixel(X, Y, Hue)
            Y -= 1
            stepCount -= 1
        End While
        Return Me
    End Function

    Public Function Down(stepCount As Integer) As Drawer(Of THue)
        While stepCount > Zero
            sink.SetPixel(X, Y, Hue)
            Y += 1
            stepCount -= 1
        End While
        Return Me
    End Function

    Public Function Left(stepCount As Integer) As Drawer(Of THue)
        While stepCount > Zero
            sink.SetPixel(X, Y, Hue)
            X -= 1
            stepCount -= 1
        End While
        Return Me
    End Function

    Public Function Right(stepCount As Integer) As Drawer(Of THue)
        While stepCount > Zero
            sink.SetPixel(X, Y, Hue)
            X += 1
            stepCount -= 1
        End While
        Return Me
    End Function
End Class
