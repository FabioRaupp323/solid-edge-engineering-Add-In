Imports System.Data.SqlClient
Imports System.Threading
Imports SolidEdgeConstants

Public Class ErpRepository
	Private _connectionString As String

	Public Sub New(connectionString As String)
		_connectionString = connectionString
	End Sub

	Public Async Function SearchProduct(text As String, token As CancellationToken) As Task(Of List(Of ErpProduct))
		Dim fetched As New List(Of ErpProduct)

		If text.Trim.Length < 3 Then
			Return fetched
		End If

		Dim terms = text.Split(
			{" "c, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf},
			StringSplitOptions.RemoveEmptyEntries
		)

		Using conn As New SqlConnection(_connectionString)
			Await conn.OpenAsync(token)

			Dim sql As New System.Text.StringBuilder()
			sql.AppendLine("SELECT proCodigo, proDescricao, proReferencia")
			sql.AppendLine("FROM Produto")
			sql.AppendLine("WHERE 1 = 1")

			For i As Integer = 0 To terms.Length - 1
				sql.AppendLine("AND (")
				sql.AppendLine($"	proCodigo LIKE @t{i}")
				sql.AppendLine($"	OR proDescricao LIKE @t{i}")
				sql.AppendLine($"	OR proReferencia LIKE @t{i}")
				sql.AppendLine(")")
			Next

			Using cmd As New SqlCommand(sql.ToString(), conn)
				For i As Integer = 0 To terms.Length - 1
					cmd.Parameters.AddWithValue($"t{i}", "%" & terms(i) & "%")
				Next

				Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync(token)
					While Await reader.ReadAsync(token)
						Dim product As New ErpProduct With {
							.ItemCode = reader("proCodigo").ToString(),
							.Description = reader("proDescricao").ToString(),
							.Reference = reader("proReferencia").ToString()
						}
						fetched.Add(product)
					End While
				End Using
			End Using
		End Using

		Return fetched
	End Function

	Public Async Function ProductExists(itemCode As String) As Task(Of Boolean)
		Using conn As New SqlConnection(_connectionString)
			Await conn.OpenAsync()

			Dim cmd As New SqlCommand("
				SELECT 1 FROM Produto
				WHERE proCodigo = @itemCode", conn)

			cmd.Parameters.AddWithValue("@itemCode", itemCode)

			Dim exist As Boolean = If(Await cmd.ExecuteScalarAsync Is Nothing, False, True)

			Return exist
		End Using
	End Function

	Public Async Function UpdateProduct(product As ErpProduct) As Task
		Using conn As New SqlConnection(_connectionString)
			Await conn.OpenAsync()

			Dim cmd As New SqlCommand("
					UPDATE Produto 
					SET proDescricao = @description, proReferencia = @reference
					WHERE proCodigo = @itemCode", conn)

			cmd.Parameters.AddWithValue("@itemCode", product.ItemCode)
			cmd.Parameters.AddWithValue("@description", product.Description)
			cmd.Parameters.AddWithValue("@reference", If(String.IsNullOrWhiteSpace(product.Reference), DBNull.Value, product.Reference))

			Dim rowsAffected As Integer = Await cmd.ExecuteNonQueryAsync()

			If rowsAffected = 0 Then
				Throw New Exception($"Nenhum produto encontrado com o código {product.ItemCode}.")
			End If
		End Using
    End Function

	Public Async Function DuplicateProduct(currentProduct As ErpProduct, baseProduct As ErpProduct) As Task(Of String)
		Using conn As New SqlConnection(_connectionString)
			Await conn.OpenAsync()

			Using trans = conn.BeginTransaction()
				Try
					Dim cmdReadLastCode As New SqlCommand("
						SELECT conValor FROM Config WITH (UPDLOCK, HOLDLOCK)
						WHERE conID = 'Produto'", conn, trans)

					Dim lastCode As Integer = Convert.ToInt32(Await cmdReadLastCode.ExecuteScalarAsync())

					Dim nextCode As Integer = lastCode + 1

					Dim cmdUpdateLastCode As New SqlCommand("
						UPDATE Config
						SET conValor = @nextCode
						WHERE conID = 'Produto'", conn, trans)

					cmdUpdateLastCode.Parameters.AddWithValue("@nextCode", nextCode)

					Await cmdUpdateLastCode.ExecuteNonQueryAsync()

					Dim nextCodeFormated As String = nextCode.ToString("000000")

					Dim cmdDuplicateProduct As New SqlCommand($"
						INSERT INTO Produto (proCodigo, proDescricao, proReferencia, {AppSettings.ErpDuplicatedColumns})
						SELECT @nextCodeFormated, @currentDescription, @currentReference, {AppSettings.ErpDuplicatedColumns}
						FROM Produto
						WHERE proCodigo = @baseCode", conn, trans)

					cmdDuplicateProduct.Parameters.AddWithValue("@nextCodeFormated", nextCodeFormated)
					cmdDuplicateProduct.Parameters.AddWithValue("@currentDescription", currentProduct.Description)
					cmdDuplicateProduct.Parameters.AddWithValue("@currentReference", If(String.IsNullOrWhiteSpace(currentProduct.Reference), DBNull.Value, currentProduct.Reference))
					cmdDuplicateProduct.Parameters.AddWithValue("@baseCode", baseProduct.ItemCode)

					Await cmdDuplicateProduct.ExecuteNonQueryAsync()

					trans.Commit()

					Return nextCodeFormated
				Catch ex As Exception
					trans.Rollback()
					Throw New Exception("Erro ao cadastrar produto no banco de dados do erp: " & ex.Message)
				End Try
			End Using
		End Using
    End Function

End Class
