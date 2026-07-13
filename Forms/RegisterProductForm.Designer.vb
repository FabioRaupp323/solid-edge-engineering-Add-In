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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RegisterProductForm))
		Me.lblBaseProduct = New System.Windows.Forms.Label()
		Me.grpBaseProduct = New System.Windows.Forms.GroupBox()
		Me.lblLoading = New System.Windows.Forms.Label()
		Me.cmbBaseProduct = New System.Windows.Forms.ComboBox()
		Me.grpProductInfo = New System.Windows.Forms.GroupBox()
		Me.txtReference = New System.Windows.Forms.TextBox()
		Me.lblReference = New System.Windows.Forms.Label()
		Me.txtDescription = New System.Windows.Forms.TextBox()
		Me.lblDescription = New System.Windows.Forms.Label()
		Me.txtItemCode = New System.Windows.Forms.TextBox()
		Me.lblItemCode = New System.Windows.Forms.Label()
		Me.btnSave = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.btnDuplicate = New System.Windows.Forms.Button()
		Me.grpBaseProduct.SuspendLayout()
		Me.grpProductInfo.SuspendLayout()
		Me.SuspendLayout()
		'
		'lblBaseProduct
		'
		Me.lblBaseProduct.AutoSize = True
		Me.lblBaseProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblBaseProduct.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblBaseProduct.Location = New System.Drawing.Point(6, 25)
		Me.lblBaseProduct.Name = "lblBaseProduct"
		Me.lblBaseProduct.Size = New System.Drawing.Size(92, 16)
		Me.lblBaseProduct.TabIndex = 0
		Me.lblBaseProduct.Text = "Produto Base:"
		'
		'grpBaseProduct
		'
		Me.grpBaseProduct.Controls.Add(Me.lblLoading)
		Me.grpBaseProduct.Controls.Add(Me.cmbBaseProduct)
		Me.grpBaseProduct.Controls.Add(Me.lblBaseProduct)
		Me.grpBaseProduct.FlatStyle = System.Windows.Forms.FlatStyle.System
		Me.grpBaseProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.grpBaseProduct.ForeColor = System.Drawing.SystemColors.GrayText
		Me.grpBaseProduct.Location = New System.Drawing.Point(10, 5)
		Me.grpBaseProduct.Name = "grpBaseProduct"
		Me.grpBaseProduct.Size = New System.Drawing.Size(600, 79)
		Me.grpBaseProduct.TabIndex = 2
		Me.grpBaseProduct.TabStop = False
		Me.grpBaseProduct.Text = "Selecionar Produto Base"
		'
		'lblLoading
		'
		Me.lblLoading.AutoSize = True
		Me.lblLoading.Location = New System.Drawing.Point(523, 47)
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
		Me.cmbBaseProduct.ForeColor = System.Drawing.SystemColors.Desktop
		Me.cmbBaseProduct.FormattingEnabled = True
		Me.cmbBaseProduct.Location = New System.Drawing.Point(6, 44)
		Me.cmbBaseProduct.Name = "cmbBaseProduct"
		Me.cmbBaseProduct.Size = New System.Drawing.Size(511, 24)
		Me.cmbBaseProduct.TabIndex = 1
		'
		'grpProductInfo
		'
		Me.grpProductInfo.Controls.Add(Me.txtReference)
		Me.grpProductInfo.Controls.Add(Me.lblReference)
		Me.grpProductInfo.Controls.Add(Me.txtDescription)
		Me.grpProductInfo.Controls.Add(Me.lblDescription)
		Me.grpProductInfo.Controls.Add(Me.txtItemCode)
		Me.grpProductInfo.Controls.Add(Me.lblItemCode)
		Me.grpProductInfo.FlatStyle = System.Windows.Forms.FlatStyle.System
		Me.grpProductInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.grpProductInfo.ForeColor = System.Drawing.SystemColors.GrayText
		Me.grpProductInfo.Location = New System.Drawing.Point(10, 90)
		Me.grpProductInfo.Name = "grpProductInfo"
		Me.grpProductInfo.Size = New System.Drawing.Size(600, 133)
		Me.grpProductInfo.TabIndex = 3
		Me.grpProductInfo.TabStop = False
		Me.grpProductInfo.Text = "Informações do Produto"
		'
		'txtReference
		'
		Me.txtReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtReference.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtReference.ForeColor = System.Drawing.SystemColors.Desktop
		Me.txtReference.Location = New System.Drawing.Point(9, 100)
		Me.txtReference.MaxLength = 30
		Me.txtReference.Name = "txtReference"
		Me.txtReference.Size = New System.Drawing.Size(585, 22)
		Me.txtReference.TabIndex = 8
		'
		'lblReference
		'
		Me.lblReference.AutoSize = True
		Me.lblReference.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblReference.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblReference.Location = New System.Drawing.Point(8, 81)
		Me.lblReference.Name = "lblReference"
		Me.lblReference.Size = New System.Drawing.Size(76, 16)
		Me.lblReference.TabIndex = 7
		Me.lblReference.Text = "Referência:"
		'
		'txtDescription
		'
		Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDescription.ForeColor = System.Drawing.SystemColors.Desktop
		Me.txtDescription.Location = New System.Drawing.Point(126, 47)
		Me.txtDescription.MaxLength = 120
		Me.txtDescription.Name = "txtDescription"
		Me.txtDescription.Size = New System.Drawing.Size(468, 22)
		Me.txtDescription.TabIndex = 6
		'
		'lblDescription
		'
		Me.lblDescription.AutoSize = True
		Me.lblDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblDescription.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblDescription.Location = New System.Drawing.Point(123, 25)
		Me.lblDescription.Name = "lblDescription"
		Me.lblDescription.Size = New System.Drawing.Size(72, 16)
		Me.lblDescription.TabIndex = 5
		Me.lblDescription.Text = "Descrição:"
		'
		'txtItemCode
		'
		Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtItemCode.Enabled = False
		Me.txtItemCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtItemCode.Location = New System.Drawing.Point(9, 47)
		Me.txtItemCode.Name = "txtItemCode"
		Me.txtItemCode.Size = New System.Drawing.Size(100, 22)
		Me.txtItemCode.TabIndex = 4
		'
		'lblItemCode
		'
		Me.lblItemCode.AutoSize = True
		Me.lblItemCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblItemCode.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblItemCode.Location = New System.Drawing.Point(6, 28)
		Me.lblItemCode.Name = "lblItemCode"
		Me.lblItemCode.Size = New System.Drawing.Size(54, 16)
		Me.lblItemCode.TabIndex = 3
		Me.lblItemCode.Text = "Código:"
		'
		'btnSave
		'
		Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnSave.ForeColor = System.Drawing.SystemColors.Desktop
		Me.btnSave.Location = New System.Drawing.Point(535, 229)
		Me.btnSave.Name = "btnSave"
		Me.btnSave.Size = New System.Drawing.Size(75, 23)
		Me.btnSave.TabIndex = 4
		Me.btnSave.Text = "Salvar"
		Me.btnSave.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnCancel.ForeColor = System.Drawing.SystemColors.Desktop
		Me.btnCancel.Location = New System.Drawing.Point(446, 229)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(83, 23)
		Me.btnCancel.TabIndex = 5
		Me.btnCancel.Text = "Cancelar"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'btnDuplicate
		'
		Me.btnDuplicate.Enabled = False
		Me.btnDuplicate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnDuplicate.ForeColor = System.Drawing.SystemColors.Desktop
		Me.btnDuplicate.Location = New System.Drawing.Point(8, 457)
		Me.btnDuplicate.Name = "btnDuplicate"
		Me.btnDuplicate.Size = New System.Drawing.Size(83, 26)
		Me.btnDuplicate.TabIndex = 6
		Me.btnDuplicate.Text = "Duplicar"
		Me.btnDuplicate.UseVisualStyleBackColor = True
		'RegisterProductForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(618, 260)
		Me.Controls.Add(Me.btnDuplicate)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnSave)
		Me.Controls.Add(Me.grpProductInfo)
		Me.Controls.Add(Me.grpBaseProduct)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "RegisterProductForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Cadastrar Produto"
		Me.grpBaseProduct.ResumeLayout(False)
		Me.grpBaseProduct.PerformLayout()
		Me.grpProductInfo.ResumeLayout(False)
		Me.grpProductInfo.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents lblBaseProduct As Windows.Forms.Label
	Friend WithEvents grpBaseProduct As Windows.Forms.GroupBox
	Friend WithEvents cmbBaseProduct As Windows.Forms.ComboBox
	Friend WithEvents lblLoading As Windows.Forms.Label
	Friend WithEvents grpProductInfo As Windows.Forms.GroupBox
	Friend WithEvents lblItemCode As Windows.Forms.Label
	Friend WithEvents lblReference As Windows.Forms.Label
	Friend WithEvents txtDescription As Windows.Forms.TextBox
	Friend WithEvents lblDescription As Windows.Forms.Label
	Friend WithEvents txtItemCode As Windows.Forms.TextBox
	Friend WithEvents txtReference As Windows.Forms.TextBox
	Friend WithEvents btnSave As Windows.Forms.Button
	Friend WithEvents btnCancel As Windows.Forms.Button
	Friend WithEvents btnDuplicate As Windows.Forms.Button
End Class
