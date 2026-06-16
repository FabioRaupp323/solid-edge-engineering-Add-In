Public Class ErpProduct
	Public Property Id As Integer
	Public Property ItemCode As String
	Public Property Description As String
	Public Property Reference As String

	Public Overrides Function ToString() As String
		Return ItemCode & " - " & Description & " _ " & Reference
	End Function
End Class
