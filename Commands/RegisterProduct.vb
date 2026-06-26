Imports System.Windows.Forms
Imports SolidEdgeFramework

Module RegisterProduct

	Public Async Sub RegisterProduct(app As SolidEdgeFramework.Application)
		Try
			Dim currentProduct As ErpProduct = GetCurrentProduct(app)

			Dim erpRepository As New ErpRepository(AppSettings.ErpConnectionString)

			Dim registerProductForm As New RegisterProductForm(erpRepository, currentProduct)
			registerProductForm.ShowDialog()

			If registerProductForm.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
				Exit Sub
			End If

			Dim baseProduct As ErpProduct = registerProductForm.SelectedBaseProduct

			If String.IsNullOrWhiteSpace(currentProduct.ItemCode) Then
				currentProduct.ItemCode = Await erpRepository.DuplicateProduct(currentProduct, baseProduct)

				MessageBox.Show(New WindowWrapper(app.hWnd), "Produto cadastrado com sucesso. Código: " & currentProduct.ItemCode, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Else
				Await erpRepository.UpdateProduct(currentProduct)
				MessageBox.Show(New WindowWrapper(app.hWnd), "As informações de Descrição e Referência do produto foram atualizadas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
			End If

			UpdateSEDocumentProperties(app, currentProduct)

		Catch ex As Exception
			MessageBox.Show(New WindowWrapper(app.hWnd), ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

	End Sub

	Private Function GetCurrentProduct(app As SolidEdgeFramework.Application) As ErpProduct
		Dim doc = TryCast(app.ActiveDocument, SolidEdgeDocument)
		Dim product As New ErpProduct

		Dim SI = GetPropSet("SummaryInformation", doc)
		product.ItemCode = GetPropValue(SI, "Keywords")
		product.Description = GetPropValue(SI, "Title")
		product.Reference = GetPropValue(SI, "Comments")

		Return product
	End Function

	Private Sub UpdateSEDocumentProperties(app As SolidEdgeFramework.Application, product As ErpProduct)
		Dim doc = TryCast(app.ActiveDocument, SolidEdgeDocument)

		Dim SI = GetPropSet("SummaryInformation", doc)
		SetPropValue(SI, "Keywords", product.ItemCode)
		SetPropValue(SI, "Title", product.Description)
		SetPropValue(SI, "Comments", product.Reference)
	End Sub
End Module
