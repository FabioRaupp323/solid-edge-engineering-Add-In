Imports System.IO
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

			MsgBox($"Arquivo {IO.Path.GetFileName(path)} salvo em {AppSettings.StepIgsOutputPath}")

		Catch ex As Exception
			MsgBox("Processo exportação do STEP interrompido: " & ex.Message)
		End Try

	End Sub

End Module