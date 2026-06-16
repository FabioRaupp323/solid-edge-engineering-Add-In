Imports System.Data.SqlClient
Imports System.Threading

Public Class ErpRepository
	Private _connectionString As String

	Public Sub New(connectionString As String)
		_connectionString = connectionString
	End Sub

	Public Async Function SearchProduct(text As String, token As CancellationToken) As Task(Of List(Of ErpProduct))
		Dim fetched As New List(Of ErpProduct)
		Using conn As New SqlConnection(_connectionString)
			Await conn.OpenAsync(token)

			Dim cmd As New SqlCommand("
				SELECT proID, proCodigo, proDescricao, proReferencia FROM Produto 
				WHERE proCodigo LIKE @text 
				OR proDescricao LIKE @text 
				OR proReferencia LIKE @text", conn)

			cmd.Parameters.AddWithValue("@text", "%" & text & "%")

			Using reader As SqlDataReader = Await cmd.ExecuteReaderAsync(token)
				While Await reader.ReadAsync(token)
					Dim product As New ErpProduct With {
						.Id = Convert.ToInt32(reader("proID")),
						.ItemCode = reader("proCodigo").ToString,
						.Description = reader("proDescricao").ToString,
						.Reference = reader("proReferencia").ToString
					}
					fetched.Add(product)
				End While
			End Using
		End Using

		Return fetched
	End Function
End Class
