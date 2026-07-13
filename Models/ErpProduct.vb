Public Class ErpProduct
	Public Property ItemCode As String
	Public Property Description As String
	Public Property Reference As String
	Public Property FilePath As String

	Public Overrides Function ToString() As String
		Return ItemCode & " - " & Description & " - " & Reference
	End Function
End Class
