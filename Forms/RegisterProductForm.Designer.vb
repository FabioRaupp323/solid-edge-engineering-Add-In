<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RegisterProductForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RegisterProductForm))
		Me.lblBaseProduct = New System.Windows.Forms.Label()
		Me.grpBaseProduct = New System.Windows.Forms.GroupBox()
		Me.lblLoading = New System.Windows.Forms.Label()
		Me.cmbBaseProduct = New System.Windows.Forms.ComboBox()
		Me.grpProductInfo = New System.Windows.Forms.GroupBox()
		Me.txtStockUnit = New System.Windows.Forms.TextBox()
		Me.txtPurchaseUnit = New System.Windows.Forms.TextBox()
		Me.lblStockUnit = New System.Windows.Forms.Label()
		Me.lblPurchaseUnit = New System.Windows.Forms.Label()
		Me.txtReference = New System.Windows.Forms.TextBox()
		Me.lblReference = New System.Windows.Forms.Label()
		Me.txtDescription = New System.Windows.Forms.TextBox()
		Me.lblDescription = New System.Windows.Forms.Label()
		Me.txtItemCode = New System.Windows.Forms.TextBox()
		Me.lblItemCode = New System.Windows.Forms.Label()
		Me.btnSave = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.btnDuplicate = New System.Windows.Forms.Button()
		Me.grpProductClassification = New System.Windows.Forms.GroupBox()
		Me.cmbSubgroup = New System.Windows.Forms.ComboBox()
		Me.lblSubgroup = New System.Windows.Forms.Label()
		Me.lblGroup = New System.Windows.Forms.Label()
		Me.txtGroup = New System.Windows.Forms.TextBox()
		Me.cmbMark = New System.Windows.Forms.ComboBox()
		Me.lblType = New System.Windows.Forms.Label()
		Me.txtType = New System.Windows.Forms.TextBox()
		Me.lblMark = New System.Windows.Forms.Label()
		Me.grpTaxInformation = New System.Windows.Forms.GroupBox()
		Me.txtTaxType = New System.Windows.Forms.TextBox()
		Me.lblPurchaseTaxClassification = New System.Windows.Forms.Label()
		Me.txtPurchaseTaxClassification = New System.Windows.Forms.TextBox()
		Me.lblTaxType = New System.Windows.Forms.Label()
		Me.grpBaseProduct.SuspendLayout()
		Me.grpProductInfo.SuspendLayout()
		Me.grpProductClassification.SuspendLayout()
		Me.grpTaxInformation.SuspendLayout()
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
		Me.cmbBaseProduct.BackColor = System.Drawing.Color.White
		Me.cmbBaseProduct.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmbBaseProduct.ForeColor = System.Drawing.Color.Black
		Me.cmbBaseProduct.FormattingEnabled = True
		Me.cmbBaseProduct.Location = New System.Drawing.Point(6, 44)
		Me.cmbBaseProduct.Name = "cmbBaseProduct"
		Me.cmbBaseProduct.Size = New System.Drawing.Size(511, 24)
		Me.cmbBaseProduct.TabIndex = 1
		'
		'grpProductInfo
		'
		Me.grpProductInfo.Controls.Add(Me.txtStockUnit)
		Me.grpProductInfo.Controls.Add(Me.txtPurchaseUnit)
		Me.grpProductInfo.Controls.Add(Me.lblStockUnit)
		Me.grpProductInfo.Controls.Add(Me.lblPurchaseUnit)
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
		'txtStockUnit
		'
		Me.txtStockUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtStockUnit.Enabled = False
		Me.txtStockUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtStockUnit.Location = New System.Drawing.Point(469, 103)
		Me.txtStockUnit.Name = "txtStockUnit"
		Me.txtStockUnit.Size = New System.Drawing.Size(115, 22)
		Me.txtStockUnit.TabIndex = 12
		'
		'txtPurchaseUnit
		'
		Me.txtPurchaseUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPurchaseUnit.Enabled = False
		Me.txtPurchaseUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPurchaseUnit.Location = New System.Drawing.Point(469, 75)
		Me.txtPurchaseUnit.Name = "txtPurchaseUnit"
		Me.txtPurchaseUnit.Size = New System.Drawing.Size(115, 22)
		Me.txtPurchaseUnit.TabIndex = 11
		'
		'lblStockUnit
		'
		Me.lblStockUnit.AutoSize = True
		Me.lblStockUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStockUnit.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblStockUnit.Location = New System.Drawing.Point(286, 105)
		Me.lblStockUnit.Name = "lblStockUnit"
		Me.lblStockUnit.Size = New System.Drawing.Size(159, 16)
		Me.lblStockUnit.TabIndex = 10
		Me.lblStockUnit.Text = "Unidade Estoque/Venda:"
		'
		'lblPurchaseUnit
		'
		Me.lblPurchaseUnit.AutoSize = True
		Me.lblPurchaseUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblPurchaseUnit.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblPurchaseUnit.Location = New System.Drawing.Point(286, 82)
		Me.lblPurchaseUnit.Name = "lblPurchaseUnit"
		Me.lblPurchaseUnit.Size = New System.Drawing.Size(113, 16)
		Me.lblPurchaseUnit.TabIndex = 9
		Me.lblPurchaseUnit.Text = "Unidade Compra:"
		'
		'txtReference
		'
		Me.txtReference.BackColor = System.Drawing.Color.White
		Me.txtReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtReference.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtReference.ForeColor = System.Drawing.Color.Black
		Me.txtReference.Location = New System.Drawing.Point(9, 100)
		Me.txtReference.MaxLength = 30
		Me.txtReference.Name = "txtReference"
		Me.txtReference.Size = New System.Drawing.Size(265, 22)
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
		Me.txtDescription.BackColor = System.Drawing.Color.White
		Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDescription.ForeColor = System.Drawing.Color.Black
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
		Me.lblItemCode.Location = New System.Drawing.Point(5, 20)
		Me.lblItemCode.Name = "lblItemCode"
		Me.lblItemCode.Size = New System.Drawing.Size(54, 16)
		Me.lblItemCode.TabIndex = 3
		Me.lblItemCode.Text = "Código:"
		'
		'btnSave
		'
		Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnSave.ForeColor = System.Drawing.SystemColors.Desktop
		Me.btnSave.Location = New System.Drawing.Point(533, 460)
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
		Me.btnCancel.Location = New System.Drawing.Point(444, 460)
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
		'
		'grpProductClassification
		'
		Me.grpProductClassification.Controls.Add(Me.cmbSubgroup)
		Me.grpProductClassification.Controls.Add(Me.lblSubgroup)
		Me.grpProductClassification.Controls.Add(Me.lblGroup)
		Me.grpProductClassification.Controls.Add(Me.txtGroup)
		Me.grpProductClassification.Controls.Add(Me.cmbMark)
		Me.grpProductClassification.Controls.Add(Me.lblType)
		Me.grpProductClassification.Controls.Add(Me.txtType)
		Me.grpProductClassification.Controls.Add(Me.lblMark)
		Me.grpProductClassification.FlatStyle = System.Windows.Forms.FlatStyle.System
		Me.grpProductClassification.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.grpProductClassification.ForeColor = System.Drawing.SystemColors.GrayText
		Me.grpProductClassification.Location = New System.Drawing.Point(10, 229)
		Me.grpProductClassification.Name = "grpProductClassification"
		Me.grpProductClassification.Size = New System.Drawing.Size(600, 133)
		Me.grpProductClassification.TabIndex = 13
		Me.grpProductClassification.TabStop = False
		Me.grpProductClassification.Text = "Classificação do Produto"
		'
		'cmbSubgroup
		'
		Me.cmbSubgroup.BackColor = System.Drawing.Color.White
		Me.cmbSubgroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmbSubgroup.ForeColor = System.Drawing.Color.Black
		Me.cmbSubgroup.FormattingEnabled = True
		Me.cmbSubgroup.Location = New System.Drawing.Point(318, 102)
		Me.cmbSubgroup.Name = "cmbSubgroup"
		Me.cmbSubgroup.Size = New System.Drawing.Size(276, 24)
		Me.cmbSubgroup.TabIndex = 10
		'
		'lblSubgroup
		'
		Me.lblSubgroup.AutoSize = True
		Me.lblSubgroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblSubgroup.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblSubgroup.Location = New System.Drawing.Point(313, 79)
		Me.lblSubgroup.Name = "lblSubgroup"
		Me.lblSubgroup.Size = New System.Drawing.Size(69, 16)
		Me.lblSubgroup.TabIndex = 9
		Me.lblSubgroup.Text = "Subgrupo:"
		'
		'lblGroup
		'
		Me.lblGroup.AutoSize = True
		Me.lblGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblGroup.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblGroup.Location = New System.Drawing.Point(6, 79)
		Me.lblGroup.Name = "lblGroup"
		Me.lblGroup.Size = New System.Drawing.Size(47, 16)
		Me.lblGroup.TabIndex = 8
		Me.lblGroup.Text = "Grupo:"
		'
		'txtGroup
		'
		Me.txtGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtGroup.Enabled = False
		Me.txtGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtGroup.Location = New System.Drawing.Point(9, 103)
		Me.txtGroup.Name = "txtGroup"
		Me.txtGroup.Size = New System.Drawing.Size(290, 22)
		Me.txtGroup.TabIndex = 7
		'
		'cmbMark
		'
		Me.cmbMark.BackColor = System.Drawing.Color.White
		Me.cmbMark.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbMark.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmbMark.ForeColor = System.Drawing.Color.Black
		Me.cmbMark.FormattingEnabled = True
		Me.cmbMark.Location = New System.Drawing.Point(9, 49)
		Me.cmbMark.Name = "cmbMark"
		Me.cmbMark.Size = New System.Drawing.Size(292, 24)
		Me.cmbMark.TabIndex = 6
		'
		'lblType
		'
		Me.lblType.AutoSize = True
		Me.lblType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblType.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblType.Location = New System.Drawing.Point(313, 25)
		Me.lblType.Name = "lblType"
		Me.lblType.Size = New System.Drawing.Size(38, 16)
		Me.lblType.TabIndex = 5
		Me.lblType.Text = "Tipo:"
		'
		'txtType
		'
		Me.txtType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtType.Enabled = False
		Me.txtType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtType.Location = New System.Drawing.Point(316, 50)
		Me.txtType.Name = "txtType"
		Me.txtType.Size = New System.Drawing.Size(278, 22)
		Me.txtType.TabIndex = 4
		'
		'lblMark
		'
		Me.lblMark.AutoSize = True
		Me.lblMark.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblMark.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblMark.Location = New System.Drawing.Point(8, 25)
		Me.lblMark.Name = "lblMark"
		Me.lblMark.Size = New System.Drawing.Size(48, 16)
		Me.lblMark.TabIndex = 3
		Me.lblMark.Text = "Marca:"
		'
		'grpTaxInformation
		'
		Me.grpTaxInformation.Controls.Add(Me.txtTaxType)
		Me.grpTaxInformation.Controls.Add(Me.lblPurchaseTaxClassification)
		Me.grpTaxInformation.Controls.Add(Me.txtPurchaseTaxClassification)
		Me.grpTaxInformation.Controls.Add(Me.lblTaxType)
		Me.grpTaxInformation.FlatStyle = System.Windows.Forms.FlatStyle.System
		Me.grpTaxInformation.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.grpTaxInformation.ForeColor = System.Drawing.SystemColors.GrayText
		Me.grpTaxInformation.Location = New System.Drawing.Point(10, 368)
		Me.grpTaxInformation.Name = "grpTaxInformation"
		Me.grpTaxInformation.Size = New System.Drawing.Size(600, 83)
		Me.grpTaxInformation.TabIndex = 14
		Me.grpTaxInformation.TabStop = False
		Me.grpTaxInformation.Text = "Informações Fiscais"
		'
		'txtTaxType
		'
		Me.txtTaxType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTaxType.Enabled = False
		Me.txtTaxType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTaxType.Location = New System.Drawing.Point(6, 48)
		Me.txtTaxType.Name = "txtTaxType"
		Me.txtTaxType.Size = New System.Drawing.Size(295, 22)
		Me.txtTaxType.TabIndex = 6
		'
		'lblPurchaseTaxClassification
		'
		Me.lblPurchaseTaxClassification.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.lblPurchaseTaxClassification.AutoSize = True
		Me.lblPurchaseTaxClassification.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblPurchaseTaxClassification.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblPurchaseTaxClassification.Location = New System.Drawing.Point(315, 24)
		Me.lblPurchaseTaxClassification.Name = "lblPurchaseTaxClassification"
		Me.lblPurchaseTaxClassification.Size = New System.Drawing.Size(200, 16)
		Me.lblPurchaseTaxClassification.TabIndex = 5
		Me.lblPurchaseTaxClassification.Text = "Classificação Fiscal de Compra:"
		'
		'txtPurchaseTaxClassification
		'
		Me.txtPurchaseTaxClassification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPurchaseTaxClassification.Enabled = False
		Me.txtPurchaseTaxClassification.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPurchaseTaxClassification.Location = New System.Drawing.Point(318, 48)
		Me.txtPurchaseTaxClassification.Name = "txtPurchaseTaxClassification"
		Me.txtPurchaseTaxClassification.Size = New System.Drawing.Size(276, 22)
		Me.txtPurchaseTaxClassification.TabIndex = 4
		'
		'lblTaxType
		'
		Me.lblTaxType.AutoSize = True
		Me.lblTaxType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTaxType.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblTaxType.Location = New System.Drawing.Point(6, 24)
		Me.lblTaxType.Name = "lblTaxType"
		Me.lblTaxType.Size = New System.Drawing.Size(77, 16)
		Me.lblTaxType.TabIndex = 3
		Me.lblTaxType.Text = "Tipo Fiscal:"
		'
		'RegisterProductForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(616, 490)
		Me.Controls.Add(Me.grpTaxInformation)
		Me.Controls.Add(Me.grpProductClassification)
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
		Me.grpProductClassification.ResumeLayout(False)
		Me.grpProductClassification.PerformLayout()
		Me.grpTaxInformation.ResumeLayout(False)
		Me.grpTaxInformation.PerformLayout()
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
	Friend WithEvents txtStockUnit As Windows.Forms.TextBox
	Friend WithEvents txtPurchaseUnit As Windows.Forms.TextBox
	Friend WithEvents lblStockUnit As Windows.Forms.Label
	Friend WithEvents lblPurchaseUnit As Windows.Forms.Label
	Friend WithEvents grpProductClassification As Windows.Forms.GroupBox
	Friend WithEvents lblType As Windows.Forms.Label
	Friend WithEvents txtType As Windows.Forms.TextBox
	Friend WithEvents lblMark As Windows.Forms.Label
	Friend WithEvents cmbSubgroup As Windows.Forms.ComboBox
	Friend WithEvents lblSubgroup As Windows.Forms.Label
	Friend WithEvents lblGroup As Windows.Forms.Label
	Friend WithEvents txtGroup As Windows.Forms.TextBox
	Friend WithEvents cmbMark As Windows.Forms.ComboBox
	Friend WithEvents grpTaxInformation As Windows.Forms.GroupBox
	Friend WithEvents lblPurchaseTaxClassification As Windows.Forms.Label
	Friend WithEvents txtPurchaseTaxClassification As Windows.Forms.TextBox
	Friend WithEvents lblTaxType As Windows.Forms.Label
	Friend WithEvents txtTaxType As Windows.Forms.TextBox
End Class
