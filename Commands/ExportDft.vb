Imports System.IO
Imports SolidEdgeDraft
Imports SolidEdgeFramework

Module ExportDft
	Public Sub ExportPdfDxf(app As SolidEdgeFramework.Application)
		Try
			Dim dft As DraftDocument = TryCast(app.ActiveDocument, DraftDocument)
			Dim itemCode As String = ""

			itemCode = Get3DItemCode(dft)

			Dim sheetDrawing As Sheet = Nothing
			Dim sheetPO As Sheet = Nothing
			Dim sheetLA As Sheet = Nothing
			Dim sheetPOLA As Sheet = Nothing

			For Each sheet As Sheet In dft.Sheets
				Select Case sheet.Name.Trim()
					Case "Desenho"
						sheetDrawing = sheet
					Case "PO"
						sheetPO = sheet
					Case "LA"
						sheetLA = sheet
					Case "POLA"
						sheetPOLA = sheet
				End Select
			Next

			'PDF
			Try
				Dim pathPDF As String = AppSettings.PdfDxfOutputPath & itemCode & ".pdf"

				If sheetDrawing IsNot Nothing Then
					sheetDrawing.Activate()
					dft.SaveAs(pathPDF)

					MsgBox($"Arquivo {Path.GetFileName(pathPDF)} salvo em {AppSettings.PdfDxfOutputPath}")
				Else
					Throw New Exception("Folha ""Desenho"" não encontrada.")
				End If
			Catch ex As Exception
				Throw New Exception("Processo exportação do PDF interrompido: " & ex.Message)
			End Try

			'DXF
			Try
				Dim sheetsWithContent As New List(Of Sheet)

				If sheetPO Is Nothing Or sheetLA Is Nothing Or sheetPOLA Is Nothing Then
					Throw New Exception($"As folhas PO, LA e POLA não foram encontradas.")
				Else
					If sheetPO.DrawingViews.Count > 0 Then
						sheetsWithContent.Add(sheetPO)
					End If
					If sheetLA.DrawingViews.Count > 0 Then
						sheetsWithContent.Add(sheetLA)
					End If
					If sheetPOLA.DrawingViews.Count > 0 Then
						sheetsWithContent.Add(sheetPOLA)
					End If
				End If

				If sheetsWithContent.Count < 1 Then
					Throw New Exception("Nenhuma das folhas PO, LA e POLA possuem vistas.")
				Else
					Dim files = Directory.GetFiles(AppSettings.PdfDxfOutputPath, itemCode & "*.dwg")
					For Each file In files
						IO.File.Delete(file)
						MsgBox($"Arquivo DWG desatualizado ""{file}"" deletado.")
					Next

					For Each sheet In sheetsWithContent
						Dim path As String = AppSettings.PdfDxfOutputPath & itemCode & " " & sheet.Name.Trim() & ".dxf"

						sheet.Activate()
						dft.SaveAs(path)
						MsgBox($"Arquivo {IO.Path.GetFileName(path)} salvo em {AppSettings.PdfDxfOutputPath}")
					Next
				End If

			Catch ex As Exception
				Throw New Exception("Processo exportação do DXF interrompido: " & ex.Message)
			End Try
		Catch ex As Exception
			MsgBox("Erro. " & ex.Message)
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