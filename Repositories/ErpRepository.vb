Imports System.Data.SqlClient

Public Class ErpRepository
	Private _connectionString As String

	Public Sub New(connectionString As String)
		_connectionString = connectionString
	End Sub

	Public Function SearchProduct(text As String) As List(Of ErpProduct)
		Dim fetched As New List(Of ErpProduct)
		Using conn As New SqlConnection(_connectionString)
			conn.Open()

			Dim cmd As New SqlCommand("
				SELECT proID, proCodigo, proDescricao, proReferencia FROM Produto 
				WHERE proCodigo LIKE @text 
				OR proDescricao LIKE @text 
				OR proReferencia LIKE @text", conn)

			cmd.Parameters.AddWithValue("@text", "%" & text & "%")

			Using reader As SqlDataReader = cmd.ExecuteReader()
				While reader.Read
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
