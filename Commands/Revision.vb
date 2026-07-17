Imports System.Windows.Forms
Imports System.Windows.Forms.LinkLabel
Imports SolidEdgeDraft
Imports SolidEdgeFramework

Module Revision
	Public Sub Revise(app As SolidEdgeFramework.Application)
		Try
			Dim doc = TryCast(app.ActiveDocument, SolidEdgeDocument)
			Dim docPath As String = doc.FullName

			If IO.Path.GetExtension(docPath) = ".dft" Then
				Dim dftDoc = TryCast(doc, DraftDocument)

				Dim modelLinks As ModelLinks = dftDoc.ModelLinks
				Dim link As String = ""
				If modelLinks.Count = 0 Then
					Throw New Exception("Este desenho não possui nenhum modelo 3D vinculado.")
				End If

				link = modelLinks.Item(1).FileName
				CloseIfOpen(app, link)
				SetPropertyViaFileProperties("ExtendedSummaryInformation", "Status", "0", link)
				Dim doc3D = TryCast(app.Documents.Open(link), SolidEdgeDocument)
				IncrementRevisionNumber(doc3D)
				doc3D.Save()

				doc.Close()
				SetPropertyViaFileProperties("ExtendedSummaryInformation", "Status", "0", docPath)
				doc = TryCast(app.Documents.Open(docPath), SolidEdgeDocument)

				MessageBox.Show(New WindowWrapper(app.hWnd), "Documentos desbloqueados e número de revisão incrementado", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
			Else
				doc.Close()
				SetPropertyViaFileProperties("ExtendedSummaryInformation", "Status", "0", docPath)
				doc = app.Documents.Open(docPath)
				IncrementRevisionNumber(doc)
				doc.Save()

				Dim dftPath As String = GetDftPath(doc.FullName)

				If Not dftPath = "Faltando Desenho" Then
					CloseIfOpen(app, dftPath)
					SetPropertyViaFileProperties("ExtendedSummaryInformation", "Status", "0", dftPath)
					Dim docDft = TryCast(app.Documents.Open(dftPath), SolidEdgeDocument)

					MessageBox.Show(New WindowWrapper(app.hWnd), "Documentos desbloqueados e número de revisão incrementado", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
				Else
					MessageBox.Show(New WindowWrapper(app.hWnd), "Nenhum desenho vinculado foi encontrado. Documento 3D desbloqueado e número de revisão incrementado.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
				End If
			End If
		Catch ex As Exception
			MessageBox.Show(New WindowWrapper(app.hWnd), ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub IncrementRevisionNumber(doc As SolidEdgeDocument)
		Try
			Dim PI = GetPropSet("ProjectInformation", doc)
			Dim revisionNumber = GetPropValue(PI, "Revision")

			Dim revisionNumberIncremented = Convert.ToInt32(If(String.IsNullOrWhiteSpace(revisionNumber), 0, revisionNumber)) + 1

			SetPropValue(PI, "Revision", revisionNumberIncremented.ToString())

		Catch ex As Exception
			Throw New Exception("Erro ao incrementar número da revisão: " & ex.Message)
		End Try
	End Sub

	Private Sub CloseIfOpen(app As SolidEdgeFramework.Application, path As String)
		For Each openDoc As SolidEdgeDocument In app.Documents
			If String.Equals(openDoc.FullName, path, StringComparison.OrdinalIgnoreCase) Then
				openDoc.Close()
				Exit Sub
			End If
		Next
	End Sub
End Module
