Public Class HueBuffer(Of THue)
    Inherits BaseDisplayBuffer(Of THue, THue)

    Public Sub New(size As (Integer, Integer))
        MyBase.New(size, Function(x) x)
    End Sub

    Public Overrides Sub Commit()
    End Sub
End Class
