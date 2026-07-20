Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Windows.Interop
Imports SolidEdgeFileProperties
Imports SolidEdgeFramework

Module SolidEdgePropertyHelper

    Public Function ReadDocumentProperties(doc As SolidEdgeDocument) As BomItem
        Try
            Dim item As New BomItem
            ' ===== SUMMARY INFORMATION =====
            Dim SI = GetPropSet("SummaryInformation", doc)
            item.ItemCode = GetPropValue(SI, "Keywords")
            item.Title = GetPropValue(SI, "Title")
            item.Specifications = GetPropValue(SI, "Comments")
            item.LastAuthor3D = GetPropValue(SI, "Last Author")
            item.Creator3D = GetPropValue(SI, "Author")

            ' ===== MECHANICAL MODELING =====
            Dim MM = GetPropSet("MechanicalModeling", doc)
            item.Material = GetPropValue(MM, "Material")

            ' ===== CUSTOM PROPERTIES =====
            Dim C = GetPropSet("Custom", doc)
            item.Category = GetPropValue(C, "CATEGORIA")

            ' ===== STATUS =====
            Dim ESI = GetPropSet("ExtendedSummaryInformation", doc)
            Dim statusVal = GetPropValue(ESI, "Status")
            item.StatusSE = GetStatusName(statusVal)

            ' ===== PROJECT INFORMATION =====
            Dim PI = GetPropSet("ProjectInformation", doc)
            item.Revision = GetPropValue(PI, "Revision")

            Return item
        Catch ex As Exception
            Throw New Exception("Erro ao ler propriedades do 3D: " & ex.Message)
        End Try
    End Function

    Public Function ReadDftAuthor(pathDFT As String) As String

        If pathDFT = "Faltando Desenho" Then
            Return ""
        End If

        Try
            Dim propertySets As New SolidEdgeFileProperties.PropertySets
            propertySets.Open(pathDFT, True)

            Dim props As Object = propertySets("SummaryInformation")
            Dim authorProp As Object = props("Author")
            Dim author As String = authorProp.Value

            propertySets.Close()
            Marshal.ReleaseComObject(propertySets)
            propertySets = Nothing

            Return author
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function GetDftPath(path3D As String) As String
        Dim dftPath As String = Path.ChangeExtension(path3D, ".dft")

        If File.Exists(dftPath) Then
            Return dftPath
        End If

        Return "Faltando Desenho"
    End Function

    Public Function GetPdfPath(itemCode As String, category As String) As String
        Dim pdfPath As String = AppSettings.PdfDxfOutputPath & itemCode & ".pdf"

        Dim requiresPdf = category = "FABRICADO" OrElse category = "ESTOQUE" OrElse category = "PADRÃO"

        If Not requiresPdf Then Return "Sem PDF"

        If File.Exists(pdfPath) Then
            Return pdfPath
        End If

        Return "Faltando PDF"
    End Function

    Private Function GetStatusName(statusVal As String) As String
        Select Case statusVal
            Case "0" : Return "Available"
            Case "1" : Return "InWork"
            Case "2" : Return "InReview"
            Case "3" : Return "Released"
            Case "4" : Return "Baselined"
            Case "5" : Return "Obsolete"
            Case Else : Return statusVal
        End Select
    End Function

    Public Function GetPropValue(propSet As Object, propName As String) As String
        Try
            Return propSet(propName).Value.ToString()
        Catch
            Return ""
        End Try
    End Function

    Public Sub SetPropValue(propSet As Object, propName As String, value As String)
        Try
            propSet(propName).Value = value
        Catch ex As Exception
            Throw New Exception("Erro ao definir propriedades do documento: " & ex.Message)
        End Try
    End Sub

    Public Function GetPropSet(propSetName As String, doc As SolidEdgeDocument) As Object
        Try
            Return doc.Properties(propSetName)
        Catch
            Return ""
        End Try
    End Function

    Public Sub SetPropertyViaFileProperties(propSetName As String, propName As String, value As String, docPath As String)
        Try
            Dim propSets = New SolidEdgeFileProperties.PropertySets
            propSets.Open(docPath, False)

            Dim propSet As Object = propSets(propSetName)
            Dim prop As Object = propSet(propName)

            prop.Value = value

            propSet.Save()
            propSets.Save()
            propSets.Close()
            Marshal.ReleaseComObject(propSets)
            propSets = Nothing

        Catch ex As Exception
            Throw New Exception("Erro ao definir propriedades via FileProperties do documento: " & ex.Message)
        End Try
    End Sub
    Public Function PropertyExists(propSet As SolidEdgeFramework.Properties, propertyName As String) As Boolean
        For Each prop As SolidEdgeFramework.Property In propSet
            If String.Equals(prop.Name, propertyName, StringComparison.OrdinalIgnoreCase) Then
                Return True
            End If
        Next

        Return False
    End Function
End Module