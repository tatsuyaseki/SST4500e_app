<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSST4500_1_0_0J_pitchsetting
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSST4500_1_0_0J_pitchsetting))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CmdRowsAdd = New System.Windows.Forms.Button()
        Me.CmdRowsDel = New System.Windows.Forms.Button()
        Me.CmdRowsMvUp = New System.Windows.Forms.Button()
        Me.CmdRowsMvDn = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtLength = New System.Windows.Forms.TextBox()
        Me.TxtPoints = New System.Windows.Forms.TextBox()
        Me.TxtLengthSum = New System.Windows.Forms.TextBox()
        Me.LblResult = New System.Windows.Forms.Label()
        Me.TxtPitchNum = New System.Windows.Forms.TextBox()
        Me.CmdAllRowsDel = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        Me.DataGridView1.Location = New System.Drawing.Point(21, 155)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView1.Size = New System.Drawing.Size(171, 148)
        Me.DataGridView1.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.HeaderText = "No."
        Me.Column1.MinimumWidth = 50
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 50
        '
        'Column2
        '
        Me.Column2.HeaderText = "ピッチ(mm)"
        Me.Column2.MinimumWidth = 100
        Me.Column2.Name = "Column2"
        '
        'CmdRowsAdd
        '
        Me.CmdRowsAdd.Location = New System.Drawing.Point(209, 164)
        Me.CmdRowsAdd.Name = "CmdRowsAdd"
        Me.CmdRowsAdd.Size = New System.Drawing.Size(75, 23)
        Me.CmdRowsAdd.TabIndex = 1
        Me.CmdRowsAdd.Text = "行追加"
        Me.CmdRowsAdd.UseVisualStyleBackColor = True
        '
        'CmdRowsDel
        '
        Me.CmdRowsDel.Location = New System.Drawing.Point(209, 193)
        Me.CmdRowsDel.Name = "CmdRowsDel"
        Me.CmdRowsDel.Size = New System.Drawing.Size(75, 23)
        Me.CmdRowsDel.TabIndex = 2
        Me.CmdRowsDel.Text = "行削除"
        Me.CmdRowsDel.UseVisualStyleBackColor = True
        '
        'CmdRowsMvUp
        '
        Me.CmdRowsMvUp.Location = New System.Drawing.Point(209, 222)
        Me.CmdRowsMvUp.Name = "CmdRowsMvUp"
        Me.CmdRowsMvUp.Size = New System.Drawing.Size(75, 23)
        Me.CmdRowsMvUp.TabIndex = 3
        Me.CmdRowsMvUp.Text = "↑"
        Me.CmdRowsMvUp.UseVisualStyleBackColor = True
        '
        'CmdRowsMvDn
        '
        Me.CmdRowsMvDn.Location = New System.Drawing.Point(209, 251)
        Me.CmdRowsMvDn.Name = "CmdRowsMvDn"
        Me.CmdRowsMvDn.Size = New System.Drawing.Size(75, 23)
        Me.CmdRowsMvDn.TabIndex = 4
        Me.CmdRowsMvDn.Text = "↓"
        Me.CmdRowsMvDn.UseVisualStyleBackColor = True
        '
        'CmdSave
        '
        Me.CmdSave.Location = New System.Drawing.Point(209, 309)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.Size = New System.Drawing.Size(75, 23)
        Me.CmdSave.TabIndex = 5
        Me.CmdSave.Text = "保存"
        Me.CmdSave.UseVisualStyleBackColor = True
        '
        'CmdClose
        '
        Me.CmdClose.Location = New System.Drawing.Point(117, 309)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.Size = New System.Drawing.Size(75, 23)
        Me.CmdClose.TabIndex = 6
        Me.CmdClose.Text = "閉じる"
        Me.CmdClose.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(19, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "サンプル長さ"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 10.0!)
        Me.Label2.Location = New System.Drawing.Point(19, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 14)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "総測定個所数"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 10.0!)
        Me.Label3.Location = New System.Drawing.Point(18, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 14)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "合計長"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 10.0!)
        Me.Label4.Location = New System.Drawing.Point(19, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 14)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "設定数"
        '
        'TxtLength
        '
        Me.TxtLength.Enabled = False
        Me.TxtLength.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtLength.Location = New System.Drawing.Point(124, 8)
        Me.TxtLength.Name = "TxtLength"
        Me.TxtLength.Size = New System.Drawing.Size(60, 22)
        Me.TxtLength.TabIndex = 12
        '
        'TxtPoints
        '
        Me.TxtPoints.Enabled = False
        Me.TxtPoints.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPoints.Location = New System.Drawing.Point(124, 36)
        Me.TxtPoints.Name = "TxtPoints"
        Me.TxtPoints.Size = New System.Drawing.Size(60, 22)
        Me.TxtPoints.TabIndex = 13
        '
        'TxtLengthSum
        '
        Me.TxtLengthSum.Enabled = False
        Me.TxtLengthSum.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtLengthSum.Location = New System.Drawing.Point(124, 64)
        Me.TxtLengthSum.Name = "TxtLengthSum"
        Me.TxtLengthSum.Size = New System.Drawing.Size(60, 22)
        Me.TxtLengthSum.TabIndex = 14
        '
        'LblResult
        '
        Me.LblResult.AutoSize = True
        Me.LblResult.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblResult.ForeColor = System.Drawing.Color.Green
        Me.LblResult.Location = New System.Drawing.Point(190, 69)
        Me.LblResult.Name = "LblResult"
        Me.LblResult.Size = New System.Drawing.Size(26, 13)
        Me.LblResult.TabIndex = 15
        Me.LblResult.Text = "OK"
        '
        'TxtPitchNum
        '
        Me.TxtPitchNum.Enabled = False
        Me.TxtPitchNum.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtPitchNum.Location = New System.Drawing.Point(124, 123)
        Me.TxtPitchNum.Name = "TxtPitchNum"
        Me.TxtPitchNum.Size = New System.Drawing.Size(60, 22)
        Me.TxtPitchNum.TabIndex = 16
        '
        'CmdAllRowsDel
        '
        Me.CmdAllRowsDel.Location = New System.Drawing.Point(209, 280)
        Me.CmdAllRowsDel.Name = "CmdAllRowsDel"
        Me.CmdAllRowsDel.Size = New System.Drawing.Size(75, 23)
        Me.CmdAllRowsDel.TabIndex = 17
        Me.CmdAllRowsDel.Text = "全行削除"
        Me.CmdAllRowsDel.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(34, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(236, 24)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "※サンプル長 - 両端補正値(420mm)以下となる" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "　様に設定してください。"
        '
        'FrmSST4500_1_0_0J_pitchsetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(300, 342)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CmdAllRowsDel)
        Me.Controls.Add(Me.TxtPitchNum)
        Me.Controls.Add(Me.LblResult)
        Me.Controls.Add(Me.TxtLengthSum)
        Me.Controls.Add(Me.TxtPoints)
        Me.Controls.Add(Me.TxtLength)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CmdClose)
        Me.Controls.Add(Me.CmdSave)
        Me.Controls.Add(Me.CmdRowsMvDn)
        Me.Controls.Add(Me.CmdRowsMvUp)
        Me.Controls.Add(Me.CmdRowsDel)
        Me.Controls.Add(Me.CmdRowsAdd)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmSST4500_1_0_0J_pitchsetting"
        Me.Text = "ピッチ拡張設定"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents CmdRowsAdd As Button
    Friend WithEvents CmdRowsDel As Button
    Friend WithEvents CmdRowsMvUp As Button
    Friend WithEvents CmdRowsMvDn As Button
    Friend WithEvents CmdSave As Button
    Friend WithEvents CmdClose As Button
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents TxtLength As TextBox
    Friend WithEvents TxtPoints As TextBox
    Friend WithEvents TxtLengthSum As TextBox
    Friend WithEvents LblResult As Label
    Friend WithEvents TxtPitchNum As TextBox
    Friend WithEvents CmdAllRowsDel As Button
    Friend WithEvents Label5 As Label
End Class
