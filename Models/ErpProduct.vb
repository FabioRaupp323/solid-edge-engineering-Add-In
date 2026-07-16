Public Class ErpProduct
	Public Property ItemCode As String
	Public Property Description As String
	Public Property Reference As String
	Public Property FilePath As String
	Public Property Mark As String
	Public Property Type As String
	Public Property Group As String
	Public Property Subgroup As String
	Public Property TaxType As String
	Public Property PurchaseTaxClassification As String
	Public Property PurchaseUnit As String
	Public Property StockUnit As String

	Public Overrides Function ToString() As String
		Return ItemCode & " - " & Description & " - " & Reference
	End Function
End Class
