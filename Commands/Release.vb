Imports System.IO
Imports System.Windows.Forms
Imports SolidEdgeFramework

Module Release
	Public Sub ReleaseDocument(app As SolidEdgeFramework.Application)
		Try
			Dim doc = TryCast(app.ActiveDocument, SolidEdgeDocument)

			Dim ESI = GetPropSet("ExtendedSummaryInformation", doc)
			SetPropValue(ESI, "Status", "3")

			doc.Save()

			MessageBox.Show(New WindowWrapper(app.hWnd), $"O documento {doc.Name} foi Liberado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)

			If Path.GetExtension(doc.FullName) = ".dft" Then
				Dim modelLinks As SolidEdgeDraft.ModelLinks = doc.ModelLinks
				Dim model3D As SolidEdgeDocument
				If modelLinks.Count > 0 Then
					model3D = modelLinks.Item(1).ModelDocument
				Else
					Throw New Exception("Nenhum link de modelo encontrado no DFT.")
				End If

				Dim doc3D = TryCast(app.Documents.Open(model3D.FullName), SolidEdgeDocument)

				Dim ESI3D = GetPropSet("ExtendedSummaryInformation", doc3D)

				SetPropValue(ESI3D, "Status", "3")

				doc3D.Save()

				MessageBox.Show(New WindowWrapper(app.hWnd), $"O documento {doc3D.Name} também foi Liberado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Else
				Dim dftPath As String = GetDftPath(doc.FullName)
				If dftPath <> "Faltando Desenho" Then
					Dim docDft = TryCast(app.Documents.Open(dftPath), SolidEdgeDocument)

					Dim ESIDft = GetPropSet("ExtendedSummaryInformation", docDft)

					SetPropValue(ESIDft, "Status", "3")

					docDft.Save()

					MessageBox.Show(New WindowWrapper(app.hWnd), $"O documento {docDft.Name} também foi Liberado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
				End If
			End If

		Catch ex As Exception
			MessageBox.Show(New WindowWrapper(app.hWnd), ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub
End Module
