Imports System.Collections.Specialized
Imports SolidEdgeAssembly

Module ExtractBOM

    Public Sub Run(app As SolidEdgeFramework.Application)
        Try
            Dim asm = TryCast(app.ActiveDocument, AssemblyDocument)
            If asm Is Nothing Then
                MsgBox("Abra uma montagem antes de usar este comando.")
                Exit Sub
            End If

            If asm.Occurrences.Count = 0 Then
                MsgBox("Montagem vazia.")
                Exit Sub
            End If

            Dim items As New OrderedDictionary()

            AddRootItem(asm, items)

            GoThrough(asm.Occurrences, items, asm.FullName)

            Dim serial = GetPropValue(asm.Properties("ProjectInformation"), "Document Number")

            Dim version = GetVersion(serial)
            If version > 1 Then
                Dim equal As Boolean = CompareWithLastVersion(items, version - 1, serial)
                If equal Then
                    Throw New Exception("Nenhuma alteração detectada em relação à última versão.")
                End If
            End If

            Dim customer = GetPropValue(asm.Properties("DocumentSummaryInformation"), "Company")

            SaveToDB(serial, version, items, asm.Name, customer)

            MsgBox("BOM salva no banco de dados com sucesso!")
        Catch ex As Exception
            MsgBox("Processo interrompido: " & ex.Message)
        End Try
    End Sub

    Private Sub AddRootItem(asm As AssemblyDocument, items As OrderedDictionary)

        Dim item = SolidEdgePropertyReader.ReadDocumentProperties(asm)

        item.Quantity = 1
        item.PathParent = "ROOT"
        item.Path3D = asm.FullName
        item.PathDFT = GetDftPath(asm.FullName)
        item.CreatorDFT = ReadDftAuthor(item.PathDFT)
        item.PathPDF = GetPdfPath(item.ItemCode, item.Category)

        items.Add("ROOT|" & asm.FullName, item)
    End Sub

    Private Sub GoThrough(occs As Occurrences, items As OrderedDictionary, parentFile As String)

        For Each occ As Occurrence In occs
            Try
                Dim path3D As String = occ.OccurrenceFileName
                Dim key As String = parentFile & "|" & path3D

                If items.Contains(key) Then
                    CType(items(key), BomItem).Quantity += 1
                    Continue For
                End If

                Dim item = SolidEdgePropertyReader.ReadDocumentProperties(occ.OccurrenceDocument)
                item.Quantity = 1
                item.PathParent = parentFile
                item.Path3D = path3D
                item.PathDFT = GetDftPath(path3D)
                item.CreatorDFT = ReadDftAuthor(item.PathDFT)
                item.PathPDF = GetPdfPath(item.ItemCode, item.Category)

                items.Add(key, item)

                If occ.Subassembly Then
                    Dim subAsm = TryCast(occ.OccurrenceDocument, AssemblyDocument)
                    If subAsm IsNot Nothing Then GoThrough(subAsm.Occurrences, items, path3D)
                End If

            Catch ex As Exception
                Throw New Exception("Erro ao processar o arquivo: " & occ.OccurrenceFileName & " " & ex.Message)
            End Try
        Next
    End Sub

    Private Function CompareWithLastVersion(items As OrderedDictionary, version As Integer, serial As String) As Boolean

        Dim oldItems As Dictionary(Of String, BomItem) = ReadLastVersion(version, serial)

        If items.Count <> oldItems.Count Then Return False

        For Each key As String In items.Keys
            If Not oldItems.ContainsKey(key) Then Return False

            Dim itemNovo As BomItem = CType(items(key), BomItem)
            Dim itemAntigo As BomItem = oldItems(key)

            If itemNovo.Category <> itemAntigo.Category OrElse
               itemNovo.ItemCode <> itemAntigo.ItemCode OrElse
               itemNovo.Quantity <> itemAntigo.Quantity OrElse
               itemNovo.Title <> itemAntigo.Title OrElse
               itemNovo.Specifications <> itemAntigo.Specifications OrElse
               itemNovo.Material <> itemAntigo.Material OrElse
               itemNovo.StatusSE <> itemAntigo.StatusSE OrElse
               itemNovo.PathPDF <> itemAntigo.PathPDF OrElse
               itemNovo.Path3D <> itemAntigo.Path3D OrElse
               itemNovo.PathParent <> itemAntigo.PathParent OrElse
               itemNovo.PathDFT <> itemAntigo.PathDFT OrElse
               itemNovo.LastAuthor3D <> itemAntigo.LastAuthor3D OrElse
               itemNovo.Revision <> itemAntigo.Revision Then
                Return False
            End If
        Next

        Return True
    End Function
End Module