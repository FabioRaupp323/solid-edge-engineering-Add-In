Imports System.Collections.Specialized
Imports System.Data.SqlClient

Public Class BomRepository
    Private _connectionString As String

    Public Sub New(connectionString As String)
        _connectionString = connectionString
    End Sub

    Public Function GetVersion(numeroSerie As String) As Integer
        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Dim cmd As New SqlCommand("
                SELECT MAX(Version) FROM BomVersion
                WHERE Serial = @Serial", conn)

            cmd.Parameters.AddWithValue("@Serial", numeroSerie)

            Dim lastVersion = cmd.ExecuteScalar()

            If lastVersion Is DBNull.Value Then
                lastVersion = 0
            End If

            Return CInt(lastVersion) + 1
        End Using
    End Function

    Public Function ReadLastVersion(version As Integer, serial As String) As Dictionary(Of String, BomItem)
        Dim oldItems As New Dictionary(Of String, BomItem)
        Dim versionId As Integer

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Using cmd As New SqlCommand("
                SELECT Id FROM BomVersion 
                WHERE Serial = @Serial 
                AND Version = @Version", conn)

                cmd.Parameters.AddWithValue("@Serial", serial)
                cmd.Parameters.AddWithValue("@Version", version)
                versionId = CInt(cmd.ExecuteScalar())
            End Using

            Using cmd As New SqlCommand("
                SELECT * FROM BomItem 
                WHERE BomVersionId = @BomVersionId", conn)

                cmd.Parameters.AddWithValue("@BomVersionId", versionId)

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim item As New BomItem With {
                            .Category = reader("Category").ToString(),
                            .ItemCode = reader("ItemCode").ToString(),
                            .Quantity = Convert.ToInt32(reader("Quantity")),
                            .Title = reader("Title").ToString(),
                            .Specifications = reader("Specifications").ToString(),
                            .Material = reader("Material").ToString(),
                            .StatusSE = reader("StatusSE").ToString(),
                            .PathPDF = reader("PathPDF").ToString(),
                            .Path3D = reader("Path3D").ToString(),
                            .PathParent = reader("PathParent").ToString(),
                            .PathDFT = reader("PathDFT").ToString(),
                            .LastAuthor3D = reader("LastAuthor3D").ToString(),
                            .Revision = If(reader("Revision") Is DBNull.Value, Nothing, reader("Revision").ToString())
                        }
                        oldItems.Add(item.PathParent & "|" & item.Path3D, item)
                    End While
                End Using
            End Using
        End Using

        Return oldItems
    End Function

    Public Sub SaveToDB(serial As String, version As Integer, items As OrderedDictionary, title As String, customer As String)

        Using conn As New SqlConnection(_connectionString)
            conn.Open()

            Using trans = conn.BeginTransaction()
                Try
                    Dim cmdVersion As New SqlCommand("
                        INSERT INTO BomVersion (Serial, Version, Issuer, Title, Customer)
                        VALUES (@Serial, @Version, @Issuer, @Title, @Customer);
                        SELECT SCOPE_IDENTITY();
                    ", conn, trans)

                    cmdVersion.Parameters.AddWithValue("@Serial", serial)
                    cmdVersion.Parameters.AddWithValue("@Version", version)
                    cmdVersion.Parameters.AddWithValue("@Issuer", Environment.UserName)
                    cmdVersion.Parameters.AddWithValue("@Title", title)
                    cmdVersion.Parameters.AddWithValue("@Customer", customer)

                    Dim bomVersionId = cmdVersion.ExecuteScalar()

                    For Each item As BomItem In items.Values
                        Dim parentId As Integer? = Nothing

                        For Each i As BomItem In items.Values
                            If i.Path3D = item.PathParent Then
                                parentId = i.GeneratedId
                                Exit For
                            End If
                        Next

                        Dim cmdItem As New SqlCommand("
                            INSERT INTO BomItem
                            (BomVersionId, ParentItemId, Category, ItemCode,
                             Quantity, Title, Specifications, Material,
                             StatusSE, PathPDF, Path3D, PathParent, PathDFT, LastAuthor3D, CreatorDFT, Creator3D, Revision)
                            VALUES
                            (@BomVersionId, @ParentItemId, @Category, @ItemCode,
                             @Quantity, @Title, @Specifications, @Material,
                             @StatusSE, @PathPDF, @Path3D, @PathParent, @PathDFT, @LastAuthor3D, @CreatorDFT, @Creator3D, @Revision);
                            SELECT SCOPE_IDENTITY()
                        ", conn, trans)

                        cmdItem.Parameters.AddWithValue("@BomVersionId", bomVersionId)
                        cmdItem.Parameters.AddWithValue("@ParentItemId", If(parentId.HasValue, parentId.Value, DBNull.Value))
                        cmdItem.Parameters.AddWithValue("@Category", item.Category)
                        cmdItem.Parameters.AddWithValue("@ItemCode", item.ItemCode)
                        cmdItem.Parameters.AddWithValue("@Quantity", item.Quantity)
                        cmdItem.Parameters.AddWithValue("@Title", item.Title)
                        cmdItem.Parameters.AddWithValue("@Specifications", item.Specifications)
                        cmdItem.Parameters.AddWithValue("@Material", item.Material)
                        cmdItem.Parameters.AddWithValue("@StatusSE", item.StatusSE)
                        cmdItem.Parameters.AddWithValue("@PathPDF", item.PathPDF)
                        cmdItem.Parameters.AddWithValue("@Path3D", item.Path3D)
                        cmdItem.Parameters.AddWithValue("@PathParent", item.PathParent)
                        cmdItem.Parameters.AddWithValue("@PathDFT", item.PathDFT)
                        cmdItem.Parameters.AddWithValue("@LastAuthor3D", item.LastAuthor3D)
                        cmdItem.Parameters.AddWithValue("@CreatorDFT", item.CreatorDFT)
                        cmdItem.Parameters.AddWithValue("@Creator3D", item.Creator3D)
                        cmdItem.Parameters.AddWithValue("@Revision", item.Revision)

                        item.GeneratedId = Convert.ToInt32(cmdItem.ExecuteScalar())
                    Next

                    trans.Commit()

                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception("Erro ao salvar no banco: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

End Class