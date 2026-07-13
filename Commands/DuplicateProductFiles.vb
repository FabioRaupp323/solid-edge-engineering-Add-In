Imports System.Windows.Forms
Imports System.IO
Imports SolidEdgeFramework

Module DuplicateProductFiles
	Public Async Function Run(app As SolidEdgeFramework.Application, baseProduct As ErpProduct, erpRepository As ErpRepository) As Task
		Try
			Dim folderDialog As New FolderBrowserDialog()

			If folderDialog.ShowDialog(New WindowWrapper(app.hWnd)) <> DialogResult.OK Then
				Exit Function
			End If

			Dim destinationFolder As String = folderDialog.SelectedPath

			Dim currentProduct As New ErpProduct With {
				.Description = baseProduct.Description,
				.Reference = baseProduct.Reference}

			Dim fileName As String = Path.GetFileName(baseProduct.FilePath)
			Dim destinationPath As String = Path.Combine(destinationFolder, fileName)

			File.Copy(baseProduct.FilePath, destinationPath)

			Dim dftPath As String = GetDftPath(baseProduct.FilePath)
			If Not dftPath = "Faltando Desenho" Then
				Dim dftFileName As String = Path.GetFileName(dftPath)
				Dim dftDestinationPath As String = Path.Combine(destinationFolder, dftFileName)
				File.Copy(dftPath, dftDestinationPath)

				SetPropertyViaFileProperties("ExtendedSummaryInformation", "Status", "0", dftDestinationPath)
				Dim dftDoc = TryCast(app.Documents.Open(dftDestinationPath), SolidEdgeDocument)
			End If

			currentProduct.ItemCode = Await erpRepository.DuplicateProduct(currentProduct, baseProduct)

			SetPropertyViaFileProperties("ExtendedSummaryInformation", "Status", "0", destinationPath)
			SetPropertyViaFileProperties("SummaryInformation", "Keywords", currentProduct.ItemCode, destinationPath)

			Dim doc = TryCast(app.Documents.Open(destinationPath), SolidEdgeDocument)

			MessageBox.Show(New WindowWrapper(app.hWnd), "Produto duplicado com sucesso. Novo código: " & currentProduct.ItemCode, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			MessageBox.Show(New WindowWrapper(app.hWnd), ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Function
End Module
