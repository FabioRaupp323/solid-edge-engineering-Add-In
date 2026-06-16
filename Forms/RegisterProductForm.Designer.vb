<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RegisterProductForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.lblBaseProduct = New System.Windows.Forms.Label()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.lblLoading = New System.Windows.Forms.Label()
		Me.cmbBaseProduct = New System.Windows.Forms.ComboBox()
		Me.GroupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'lblBaseProduct
		'
		Me.lblBaseProduct.AutoSize = True
		Me.lblBaseProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblBaseProduct.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblBaseProduct.Location = New System.Drawing.Point(8, 32)
		Me.lblBaseProduct.Name = "lblBaseProduct"
		Me.lblBaseProduct.Size = New System.Drawing.Size(92, 16)
		Me.lblBaseProduct.TabIndex = 0
		Me.lblBaseProduct.Text = "Produto Base:"
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.lblLoading)
		Me.GroupBox1.Controls.Add(Me.cmbBaseProduct)
		Me.GroupBox1.Controls.Add(Me.lblBaseProduct)
		Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark
		Me.GroupBox1.Location = New System.Drawing.Point(10, 10)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(785, 201)
		Me.GroupBox1.TabIndex = 2
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Informações do Produto Base"
		'
		'lblLoading
		'
		Me.lblLoading.AutoSize = True
		Me.lblLoading.Location = New System.Drawing.Point(359, 33)
		Me.lblLoading.Name = "lblLoading"
		Me.lblLoading.Size = New System.Drawing.Size(71, 15)
		Me.lblLoading.TabIndex = 2
		Me.lblLoading.Text = "Buscando..."
		Me.lblLoading.Visible = False
		'
		'cmbBaseProduct
		'
		Me.cmbBaseProduct.BackColor = System.Drawing.SystemColors.Window
		Me.cmbBaseProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmbBaseProduct.FormattingEnabled = True
		Me.cmbBaseProduct.Location = New System.Drawing.Point(106, 29)
		Me.cmbBaseProduct.Name = "cmbBaseProduct"
		Me.cmbBaseProduct.Size = New System.Drawing.Size(247, 24)
		Me.cmbBaseProduct.TabIndex = 1
		'
		'RegisterProductForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(839, 450)
		Me.Controls.Add(Me.GroupBox1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Name = "RegisterProductForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Cadastrar Produto"
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents lblBaseProduct As Windows.Forms.Label
	Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
	Friend WithEvents cmbBaseProduct As Windows.Forms.ComboBox
	Friend WithEvents lblLoading As Windows.Forms.Label
End Class
