Imports System.Windows.Forms
Imports System.IO
Imports SolidEdgeFramework

Module DuplicateProductFiles
	Public Async Function Run(app As SolidEdgeFramework.Application, baseProduct As ErpProduct, erpRepository As ErpRepository) As Task
		Try
			Dim folderDialog As New FolderBrowserDialog()
			folderDialog.SelectedPath = Path.GetDirectoryName(baseProduct.FilePath)

			If folderDialog.ShowDialog(New WindowWrapper(app.hWnd)) <> DialogResult.OK Then
				Exit Function
			End If

			Dim destinationFolder As String = folderDialog.SelectedPath

			Dim currentProduct As New ErpProduct With {
				.Description = baseProduct.Description,
				.Reference = baseProduct.Reference}

			Dim fileName As String = BuildProductFileName(currentProduct, Path.GetExtension(baseProduct.FilePath))
			Dim destinationPath As String = Path.Combine(destinationFolder, fileName)

			File.Copy(baseProduct.FilePath, destinationPath, True)

			currentProduct.ItemCode = Await erpRepository.DuplicateProduct(currentProduct, baseProduct)

			Dim finalFileName As String = BuildProductFileName(currentProduct, Path.GetExtension(destinationPath))
			Dim finalDestinationPath As String = Path.Combine(destinationFolder, finalFileName)
			File.Move(destinationPath, finalDestinationPath)

			SetPropertyViaFileProperties("ExtendedSummaryInformation", "Status", "0", finalDestinationPath)
			SetPropertyViaFileProperties("SummaryInformation", "Keywords", currentProduct.ItemCode, finalDestinationPath)

			Dim doc = TryCast(app.Documents.Open(finalDestinationPath), SolidEdgeDocument)

			Dim dftPath As String = GetDftPath(baseProduct.FilePath)
			If dftPath <> "Faltando Desenho" Then
				Dim dftFileName As String = BuildProductFileName(currentProduct, Path.GetExtension(dftPath))
				Dim dftDestinationPath As String = Path.Combine(destinationFolder, dftFileName)
				File.Copy(dftPath, dftDestinationPath, True)

				SetPropertyViaFileProperties("ExtendedSummaryInformation", "Status", "0", dftDestinationPath)
				Dim dftDoc = TryCast(app.Documents.Open(dftDestinationPath), SolidEdgeDocument)

				Dim modelLinks As SolidEdgeDraft.ModelLinks = dftDoc.ModelLinks
				If modelLinks.Count > 0 Then
					Dim link As SolidEdgeDraft.ModelLink = modelLinks.Item(1)
					link.ChangeSource(finalDestinationPath)
				Else
					Throw New Exception("Nenhum link de modelo encontrado no DFT.")
				End If

				dftDoc.Save()
			End If

			MessageBox.Show(New WindowWrapper(app.hWnd), "Produto duplicado com sucesso. Novo código: " & currentProduct.ItemCode, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
		Catch ex As Exception
			MessageBox.Show(New WindowWrapper(app.hWnd), ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Function

End Module
