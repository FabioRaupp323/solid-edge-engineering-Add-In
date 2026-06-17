Module RegisterProduct

	Public Sub RegisterProduct(app As SolidEdgeFramework.Application)
		Dim erpRepository As New ErpRepository(AppSettings.ErpConnectionString)

		Dim registerProductForm As New RegisterProductForm(erpRepository)
		registerProductForm.ShowDialog()

		If registerProductForm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
			Exit Sub
		End If

		Dim baseProduct As ErpProduct = registerProductForm.SelectedBaseProduct

	End Sub
End Module
