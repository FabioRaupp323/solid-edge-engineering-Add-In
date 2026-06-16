Imports System.Threading

Public Class RegisterProductForm
	Private _erpRepository As ErpRepository
	Private _timer As Windows.Forms.Timer
	Private _cancellationToken As CancellationTokenSource

	Public Sub New(erpRepository As ErpRepository)
		InitializeComponent()

		_erpRepository = erpRepository
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
End Class