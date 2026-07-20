Imports System.Windows.Forms
Imports SolidEdgeFramework

Module RegisterProduct

	Public Async Sub RegisterProduct(app As SolidEdgeFramework.Application)
		Dim wasFromSelection As Boolean = False
		Dim doc As SolidEdgeDocument = Nothing

		Try
			doc = GetTargetDocument(app, wasFromSelection)
			Dim currentProduct As ErpProduct = GetCurrentProduct(doc)

			Dim erpRepository As New ErpRepository(AppSettings.ErpConnectionString)

			If Not String.IsNullOrWhiteSpace(currentProduct.ItemCode) AndAlso Not Await erpRepository.ProductExists(currentProduct.ItemCode) Then
				MessageBox.Show(New WindowWrapper(app.hWnd), "Nenhum produto encontrado com o código " & currentProduct.ItemCode & ". Selecione um produto base para criar um novo cadastro.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				currentProduct.ItemCode = ""
			End If

			If Not String.IsNullOrWhiteSpace(currentProduct.ItemCode) Then Await erpRepository.GetProductDetails(currentProduct)

			Dim registerProductForm As New RegisterProductForm(erpRepository, currentProduct)
			registerProductForm.ShowDialog()

			If registerProductForm.DialogResult = DialogResult.Cancel Then
				Exit Sub
			ElseIf registerProductForm.DialogResult = DialogResult.Yes Then
				Await DuplicateProductFiles.Run(app, currentProduct, erpRepository)
				Exit Sub
			End If

			Dim baseProduct As ErpProduct = registerProductForm.SelectedBaseProduct

			If String.IsNullOrWhiteSpace(currentProduct.ItemCode) Then
				currentProduct.ItemCode = Await erpRepository.DuplicateProduct(currentProduct, baseProduct)

				MessageBox.Show(New WindowWrapper(app.hWnd), "Produto cadastrado com sucesso. Código: " & currentProduct.ItemCode, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Else
				Await erpRepository.UpdateProduct(currentProduct)
				MessageBox.Show(New WindowWrapper(app.hWnd), "As informações do produto foram atualizadas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
			End If

			UpdateSEDocumentProperties(doc, currentProduct)

		Catch ex As Exception
			MessageBox.Show(New WindowWrapper(app.hWnd), ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)

		Finally
			If wasFromSelection Then
				doc.Close()
			End If
		End Try

	End Sub

	Private Function GetCurrentProduct(doc As SolidEdgeDocument) As ErpProduct
		Dim product As New ErpProduct With {.FilePath = doc.FullName}

		Dim SI = GetPropSet("SummaryInformation", doc)
		product.ItemCode = GetPropValue(SI, "Keywords")
		product.Description = GetPropValue(SI, "Title")
		product.Reference = GetPropValue(SI, "Comments")

		Return product
	End Function

	Private Sub UpdateSEDocumentProperties(doc As SolidEdgeDocument, product As ErpProduct)
		Dim SI = GetPropSet("SummaryInformation", doc)
		SetPropValue(SI, "Keywords", product.ItemCode)
		SetPropValue(SI, "Title", product.Description)
		SetPropValue(SI, "Comments", product.Reference)

		Dim C = GetPropSet("Custom", doc)
		If PropertyExists(C, "CATEGORIA") Then
			SetPropValue(C, "CATEGORIA", product.Mark)
		Else
			C.Add("CATEGORIA", product.Mark)
		End If

		doc.Save()
	End Sub
End Module
