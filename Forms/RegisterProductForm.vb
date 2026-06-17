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

		_erpRepository = erpRepository
		_currentProduct = currentProduct
		SetDefaultTexts()

		_cancellationToken = New CancellationTokenSource
		_timer = New Windows.Forms.Timer
		_timer.Interval = 500

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
			cmbBaseProduct.Items.Clear()
			For Each item In items
				cmbBaseProduct.Items.Add(item)
			Next
			cmbBaseProduct.EndUpdate()

			If items.Count > 0 Then
				cmbBaseProduct.DroppedDown = True
			End If

		Catch ex As OperationCanceledException

		Finally
			lblLoading.Visible = False
		End Try
	End Sub

	Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
		Me.DialogResult = DialogResult.Cancel
	End Sub

	Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

		If String.IsNullOrWhiteSpace(txtDescription.Text) Then
			MessageBox.Show("O campo 'Descrição' não pode ser vazio", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub

		ElseIf String.IsNullOrWhiteSpace(txtReference.Text) Then
			MessageBox.Show("O campo 'Referência' não pode ser vazio", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If

		If String.IsNullOrWhiteSpace(txtItemCode.Text) Then
			If cmbBaseProduct.SelectedItem Is Nothing Then
				MessageBox.Show("Selecione um produto base para o cadastro.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning)
				Exit Sub

			End If
		End If

		_currentProduct.Description = txtDescription.Text.Trim
		_currentProduct.Reference = txtReference.Text.Trim

		_selectedBaseProduct = cmbBaseProduct.SelectedItem
		Me.DialogResult = DialogResult.OK
	End Sub

	Private Sub SetDefaultTexts()
		txtItemCode.Text = _currentProduct.ItemCode
		txtDescription.Text = _currentProduct.Description
		txtReference.Text = _currentProduct.Reference

	End Sub
End Class