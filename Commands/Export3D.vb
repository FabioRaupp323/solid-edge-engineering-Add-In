Imports System.IO
Imports System.Windows.Forms
Imports SolidEdgeFramework

Module Export3D

	Public Sub Export3D(app As SolidEdgeFramework.Application, fileTypeExtension As String)
		Try
			Dim doc As SolidEdgeDocument = TryCast(app.ActiveDocument, SolidEdgeDocument)

			Dim SI = GetPropSet("SummaryInformation", doc)
			Dim ItemCode As String = GetPropValue(SI, "Keywords")

			Dim path As String = AppSettings.StepIgsOutputPath & ItemCode & fileTypeExtension
			doc.SaveAs(path)

			Dim autoCreatedLogFile As String = IO.Path.ChangeExtension(path, ".log")
			If File.Exists(autoCreatedLogFile) Then
				File.Delete(autoCreatedLogFile)
			End If

			MessageBox.Show(New WindowWrapper(app.hWnd), $"Arquivo {IO.Path.GetFileName(path)} salvo em {AppSettings.StepIgsOutputPath}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)

		Catch ex As Exception
			MessageBox.Show(New WindowWrapper(app.hWnd), "Processo exportação do STEP interrompido: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

	End Sub

End Module