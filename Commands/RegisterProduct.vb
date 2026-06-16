Module RegisterProduct

	Public Sub RegisterProduct(app As SolidEdgeFramework.Application)
		Dim erpRepository As New ErpRepository(AppSettings.ErpConnectionString)

		Dim registerProductForm As New RegisterProductForm(erpRepository)
	End Sub
End Module
