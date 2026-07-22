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

			Dim sql As New Text.StringBuilder()
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
					cmd.Parameters.AddWithValue($"@t{i}", "%" & terms(i) & "%")
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
					SET 
						proDescricao = @description, 
						proReferencia = @reference,
						proMarcaID = (
							SELECT TOP (1) marID
							FROM Marca
							WHERE marDescricao = @mark
						),
						proSGrupoID = (
							SELECT TOP (1) sgruID
							FROM Sgrupo
							WHERE sgruDescricao = @subgroup
						)
					WHERE proCodigo = @itemCode", conn)

			cmd.Parameters.AddWithValue("@itemCode", product.ItemCode)
			cmd.Parameters.AddWithValue("@description", product.Description)
			cmd.Parameters.AddWithValue("@reference", If(String.IsNullOrWhiteSpace(product.Reference), DBNull.Value, product.Reference))
			cmd.Parameters.AddWithValue("@mark", product.Mark)
			cmd.Parameters.AddWithValue("@subgroup", product.Subgroup)

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
						INSERT INTO Produto (proCodigo, proDescricao, proReferencia, proMarcaID, proSGrupoID, {AppSettings.ErpDuplicatedColumns})
						SELECT @nextCodeFormated, 
						@currentDescription, 
						@currentReference, 
						(
							SELECT TOP (1) marID
							FROM Marca
							WHERE marDescricao = @currentMark
						),
						(
							SELECT TOP (1) sgruID
							FROM Sgrupo
							WHERE sgruDescricao = @currentSubgroup
						),
						{AppSettings.ErpDuplicatedColumns}
						FROM Produto
						WHERE proCodigo = @baseCode", conn, trans)

					cmdDuplicateProduct.Parameters.AddWithValue("@nextCodeFormated", nextCodeFormated)
					cmdDuplicateProduct.Parameters.AddWithValue("@currentDescription", currentProduct.Description)
					cmdDuplicateProduct.Parameters.AddWithValue("@currentReference", If(String.IsNullOrWhiteSpace(currentProduct.Reference), DBNull.Value, currentProduct.Reference))
					cmdDuplicateProduct.Parameters.AddWithValue("@currentMark", currentProduct.Mark)
					cmdDuplicateProduct.Parameters.AddWithValue("@currentSubgroup", currentProduct.Subgroup)
					cmdDuplicateProduct.Parameters.AddWithValue("@baseCode", baseProduct.ItemCode)

					Dim rowsAffected = Await cmdDuplicateProduct.ExecuteNonQueryAsync()

					If rowsAffected = 0 Then
						Throw New Exception($"Produto base '{baseProduct.ItemCode}' não encontrado.")
					End If

					trans.Commit()

					Return nextCodeFormated
				Catch ex As Exception
					trans.Rollback()
					Throw New Exception("Erro ao cadastrar produto no banco de dados do erp: " & ex.Message)
				End Try
			End Using
		End Using
	End Function

	Public Async Function GetProductDetails(product As ErpProduct) As Task
		Using conn As New SqlConnection(_connectionString)
			Await conn.OpenAsync()

			Dim cmd As New SqlCommand("
				SELECT p.proUNCompra, p.proUNEstoque, p.proTipoItem, m.marDescricao, t.tipoDescricao, g.gruDescricao, sg.sgruDescricao, c.clasDescricao
				FROM Produto p
				LEFT JOIN Marca m ON p.proMarcaID = m.marID
				LEFT JOIN Tipo t ON p.proTipoID = t.tipoID
				LEFT JOIN Grupo g ON p.proGrupoID = g.gruID
				LEFT JOIN Sgrupo sg ON p.proSGrupoID = sg.sgruID
				LEFT JOIN ClasFiscal c ON p.proClasFiscalCompraID = c.clasID
				WHERE p.proCodigo = @itemCode", conn)

			cmd.Parameters.AddWithValue("@itemCode", product.ItemCode)

			Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync
				While Await reader.ReadAsync()
					product.PurchaseUnit = reader("proUNCompra").ToString
					product.StockUnit = reader("proUNEstoque").ToString
					product.TaxType = GetItemTypeName(reader("proTipoItem").ToString)
					product.Mark = reader("marDescricao").ToString
					product.Type = reader("tipoDescricao").ToString
					product.Group = reader("gruDescricao").ToString
					product.Subgroup = reader("sgruDescricao").ToString
					product.PurchaseTaxClassification = reader("clasDescricao").ToString
				End While
			End Using
		End Using
    End Function

	Private Function GetItemTypeName(tipoItemVal As String) As String
		Select Case tipoItemVal
			Case "0" : Return "Mercadoria para Revenda"
			Case "1" : Return "Matéria-Prima"
			Case "2" : Return "Embalagem"
			Case "3" : Return "Produto em Processo"
			Case "4" : Return "Produto Acabado"
			Case "5" : Return "Subproduto"
			Case "6" : Return "Produto Intermediário"
			Case "7" : Return "Material de Uso e Consumo"
			Case "8" : Return "Ativo Imobilizado"
			Case "9" : Return "Serviços"
			Case "10" : Return "Outros Insumos"
			Case "99" : Return "Outras"
			Case Else : Return tipoItemVal
		End Select
	End Function

	Public Function GetAllMarks() As List(Of String)
		Dim marks As New List(Of String)

		Using conn As New SqlConnection(_connectionString)
			conn.Open()

			Dim cmd As New SqlCommand("SELECT marDescricao FROM Marca ORDER BY marDescricao", conn)

			Using reader As SqlDataReader = cmd.ExecuteReader()
				While reader.Read()
					marks.Add(reader("marDescricao").ToString())
				End While
			End Using
		End Using

		Return marks
	End Function

	Public Async Function SearchSubgroups(text As String, token As CancellationToken) As Task(Of List(Of String))
		Dim fetched As New List(Of String)

		Dim terms = text.Split(
			{" "c, ControlChars.Tab, ControlChars.Cr, ControlChars.Lf},
			StringSplitOptions.RemoveEmptyEntries
		)

		Using conn As New SqlConnection(_connectionString)
			Await conn.OpenAsync(token)

			Dim sql As New Text.StringBuilder()
			sql.AppendLine("SELECT sgruDescricao")
			sql.AppendLine("FROM Sgrupo")
			sql.AppendLine("WHERE 1 = 1")

			For i As Integer = 0 To terms.Length - 1
				sql.AppendLine($"AND sgruDescricao LIKE @t{i}")
			Next

			Using cmd As New SqlCommand(sql.ToString, conn)
				For i As Integer = 0 To terms.Length - 1
					cmd.Parameters.AddWithValue($"@t{i}", "%" & terms(i) & "%")
				Next

				Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync(token)
					While Await reader.ReadAsync(token)
						fetched.Add(reader("sgruDescricao").ToString())
					End While
				End Using
			End Using
		End Using

		Return fetched
	End Function

	Public Async Function SubgroupExists(subgroupDescription As String) As Task(Of Boolean)
		Using conn As New SqlConnection(_connectionString)
			Await conn.OpenAsync()

			Dim cmd As New SqlCommand("SELECT 1 FROM Sgrupo WHERE sgruDescricao = @subgroupDescription", conn)
			cmd.Parameters.AddWithValue("@subgroupDescription", subgroupDescription)

			Return Await cmd.ExecuteScalarAsync() IsNot Nothing
		End Using
	End Function

	Public Async Function DescriptionExists(description As String, excludingItemCode As String) As Task(Of Boolean)
		Using conn As New SqlConnection(_connectionString)
			Await conn.OpenAsync()

			Dim cmd As New SqlCommand("
				SELECT 1 FROM Produto
				WHERE proDescricao = @description
				AND proCodigo <> @excludingItemCode", conn)

			cmd.Parameters.AddWithValue("@description", description)
			cmd.Parameters.AddWithValue("@excludingItemCode", excludingItemCode)

			Return Await cmd.ExecuteScalarAsync() IsNot Nothing
		End Using
	End Function
End Class
