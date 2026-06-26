Imports System.IO
Imports System.Windows.Forms
Imports SolidEdgeDraft
Imports SolidEdgeFramework

Module ExportDft
	Public Sub ExportPdfDxf(app As SolidEdgeFramework.Application)
		Try
			Dim dft As DraftDocument = TryCast(app.ActiveDocument, DraftDocument)
			Dim itemCode As String = ""
			itemCode = Get3DItemCode(dft)

			Dim sheetDrawing As Sheet = Nothing
			Dim sheetsPO As New List(Of Sheet)
			Dim sheetsLA As New List(Of Sheet)
			Dim sheetsPOLA As New List(Of Sheet)

			For Each sheet As Sheet In dft.Sheets
				If sheet Is Nothing Then Continue For

				Dim name As String = sheet.Name.Trim.ToUpper
				If name = "DESENHO" Then
					sheetDrawing = sheet
				ElseIf name.EndsWith("POLA") Then
					sheetsPOLA.Add(sheet)
				ElseIf name.EndsWith("PO") Then
					sheetsPO.Add(sheet)
				ElseIf name.EndsWith("LA") Then
					sheetsLA.Add(sheet)
				End If
			Next

			'PDF
			Try
				Dim pathPDF As String = AppSettings.PdfDxfOutputPath & itemCode & ".pdf"

				If sheetDrawing IsNot Nothing Then
					sheetDrawing.Activate()
					dft.SaveAs(pathPDF)

					MessageBox.Show(New WindowWrapper(app.hWnd), $"Arquivo {Path.GetFileName(pathPDF)} salvo em {AppSettings.PdfDxfOutputPath}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
				Else
					Throw New Exception("Folha ""Desenho"" não encontrada.")
				End If
			Catch ex As Exception
				Throw New Exception("Processo exportação do PDF interrompido: " & ex.Message)
			End Try

			'DXF
			Try
				Dim sheetsWithContent As New List(Of Sheet)

				If sheetsPO.Count = 0 AndAlso sheetsLA.Count = 0 AndAlso sheetsPOLA.Count = 0 Then
					Throw New Exception($"Nenhuma folha do tipo PO, LA ou POLA foi encontrada.")
				End If

				'PO
				For Each sheet In sheetsPO
					If sheet.DrawingViews.Count > 0 Then
						sheetsWithContent.Add(sheet)
					End If
				Next

				'LA
				For Each sheet In sheetsLA
					If sheet.DrawingViews.Count > 0 Then
						sheetsWithContent.Add(sheet)
					End If
				Next

				'POLA
				For Each sheet In sheetsPOLA
					If sheet.DrawingViews.Count > 0 Then
						sheetsWithContent.Add(sheet)
					End If
				Next

				If sheetsWithContent.Count < 1 Then
					Throw New Exception("Nenhuma das folhas PO, LA e POLA possuem vistas.")
				Else
					Dim files = Directory.GetFiles(AppSettings.PdfDxfOutputPath, itemCode & "*.dwg")
					For Each file In files
						IO.File.Delete(file)
						MessageBox.Show(New WindowWrapper(app.hWnd), $"Arquivo DWG desatualizado ""{file}"" deletado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information)
					Next

					For Each sheet In sheetsWithContent
						Dim path As String = AppSettings.PdfDxfOutputPath & itemCode & " " & sheet.Name.Trim() & ".dxf"

						sheet.Activate()
						dft.SaveAs(path)
						MessageBox.Show(New WindowWrapper(app.hWnd), $"Arquivo {IO.Path.GetFileName(path)} salvo em {AppSettings.PdfDxfOutputPath}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
					Next
				End If

			Catch ex As Exception
				Throw New Exception("Processo exportação do DXF interrompido: " & ex.Message)
			End Try
		Catch ex As Exception
			MessageBox.Show(New WindowWrapper(app.hWnd), "Erro. " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Function Get3DItemCode(dft As DraftDocument) As String

		Dim link As SolidEdgeDraft.ModelLink
		Dim modelLinks As SolidEdgeDraft.ModelLinks = dft.ModelLinks
		If modelLinks.Count > 0 Then
			link = modelLinks.Item(1)
		Else
			Throw New Exception("Nenhum link de modelo encontrado no DFT.")
		End If

		Dim model3D As SolidEdgeDocument = link.ModelDocument

		Dim keywords = ""
		Dim SI = model3D.Properties("SummaryInformation")
		keywords = GetPropValue(SI, "Keywords")

		If keywords = "" Then
			Throw New Exception("A propriedade keywords (código) está vazia.")
		End If

		Return keywords
	End Function
End Module