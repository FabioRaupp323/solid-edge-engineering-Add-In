Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms

Public Class RegisterProductForm
	Private _erpRepository As ErpRepository
	Private _timer As Windows.Forms.Timer
	Private _cancellationToken As CancellationTokenSource
	Private _currentProduct As ErpProduct

	Private _selectedBaseProduct As ErpProduct
	Public ReadOnly Property SelectedBaseProduct As ErpProduct
		Get
			Return _selectedBaseProduct
		End Get
	End Property

	Public Sub New(erpRepository As ErpRepository, currentProduct As ErpProduct)
		InitializeComponent()

		cmbBaseProduct.DrawMode = DrawMode.OwnerDrawVariable
		cmbBaseProduct.IntegralHeight = False

		_erpRepository = erpRepository
		_currentProduct = currentProduct
		SetDefaultTexts()
		If Not String.IsNullOrWhiteSpace(currentProduct.ItemCode) Then
			cmbBaseProduct.Enabled = False
		End If

		_cancellationToken = New CancellationTokenSource
		_timer = New Windows.Forms.Timer
		_timer.Interval = 1000

		AddHandler _timer.Tick, AddressOf TimerTick
	End Sub

	Private Sub cmbBaseProduct_TextUpdate(sender As Object, e As EventArgs) Handles cmbBaseProduct.TextUpdate
		_timer.Stop()
		_timer.Start()
	End Sub

	Private Async Sub TimerTick(sender As Object, e As EventArgs)
		_timer.Stop()

		_cancellationToken.Cancel()

		_cancellationToken = New CancellationTokenSource

		lblLoading.Visible = True

		Try
			Dim items As List(Of ErpProduct) = Await _erpRepository.SearchProduct(cmbBaseProduct.Text, _cancellationToken.Token)

			cmbBaseProduct.BeginUpdate()
			cmbBaseProduct.SelectedIndex = -1
			cmbBaseProduct.Items.Clear()
			For Each item In items
				cmbBaseProduct.Items.Add(item)
			Next
			cmbBaseProduct.EndUpdate()

			cmbBaseProduct.DroppedDown = True

			cmbBaseProduct.SelectionStart = cmbBaseProduct.Text.Length

		Catch ex As OperationCanceledException

		Finally
			lblLoading.Visible = False
		End Try
	End Sub

	Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
		Me.DialogResult = DialogResult.Cancel
	End Sub

	Private Async Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

		If String.IsNullOrWhiteSpace(txtDescription.Text) Then
			MessageBox.Show(Me, "O campo 'Descrição' não pode ser vazio", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		If txtDescription.Text.Length > 120 Then
			MessageBox.Show(Me, "O campo 'Descrição' não pode ter mais de 120 caracteres", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		If txtReference.Text.Length > 30 Then
			MessageBox.Show(Me, "O campo 'Referência' não pode ter mais de 30 caracteres", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		If String.IsNullOrWhiteSpace(txtItemCode.Text) Then
			If cmbBaseProduct.SelectedItem Is Nothing Then
				MessageBox.Show(Me, "Selecione um produto base para o cadastro.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub

			End If
		Else
			If Not Await _erpRepository.ProductExists(txtItemCode.Text) Then
				MessageBox.Show(Me, "Nenhum produto encontrado com o código " & txtItemCode.Text & ". Selecione um produto base para criar um novo cadastro.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				txtItemCode.Text = ""
				Exit Sub

			End If
		End If

		_currentProduct.Description = txtDescription.Text.Trim.ToUpper
		_currentProduct.Reference = txtReference.Text.Trim.ToUpper
		_currentProduct.ItemCode = txtItemCode.Text.Trim

		_selectedBaseProduct = cmbBaseProduct.SelectedItem
		Me.DialogResult = DialogResult.OK
	End Sub

	Private Sub cmbBaseProduct_MeasureItem(sender As Object, e As MeasureItemEventArgs) Handles cmbBaseProduct.MeasureItem
		If e.Index < 0 Then Return

		Dim texto As String = cmbBaseProduct.Items(e.Index).ToString()

		Dim tamanho As SizeF = e.Graphics.MeasureString(texto, cmbBaseProduct.Font, cmbBaseProduct.Width)

		e.ItemHeight = CInt(tamanho.Height) + 4
	End Sub

	Private Sub cmbBaseProduct_DrawItem(sender As Object, e As DrawItemEventArgs) Handles cmbBaseProduct.DrawItem
		If e.Index < 0 Then Return

		e.DrawBackground()

		Dim texto As String = cmbBaseProduct.Items(e.Index).ToString()

		Dim cor As Color = If((e.State And DrawItemState.Selected) = DrawItemState.Selected, Color.White, Color.Black)

		TextRenderer.DrawText(e.Graphics, texto, e.Font, e.Bounds, cor, TextFormatFlags.WordBreak Or TextFormatFlags.Left Or TextFormatFlags.VerticalCenter)
	End Sub

	Private Sub SetDefaultTexts()
		txtItemCode.Text = _currentProduct.ItemCode
		txtDescription.Text = _currentProduct.Description
		txtReference.Text = _currentProduct.Reference

	End Sub

	Private Sub cmbBaseProduct_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbBaseProduct.SelectionChangeCommitted
		If String.IsNullOrWhiteSpace(txtDescription.Text) Then
			txtDescription.Text = cmbBaseProduct.SelectedItem.Description
		End If
		If String.IsNullOrWhiteSpace(txtReference.Text) Then
			txtReference.Text = cmbBaseProduct.SelectedItem.Reference
		End If
	End Sub
End Class