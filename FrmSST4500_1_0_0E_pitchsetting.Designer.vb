<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSST4500_1_0_0E_pitchsetting
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSST4500_1_0_0E_pitchsetting))
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
        Me.CmdLoad = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtPchExpLoadedFile = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        Me.DataGridView1.Location = New System.Drawing.Point(3, 3)
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
        Me.CmdRowsAdd.Location = New System.Drawing.Point(213, 184)
        Me.CmdRowsAdd.Name = "CmdRowsAdd"
        Me.CmdRowsAdd.Size = New System.Drawing.Size(75, 23)
        Me.CmdRowsAdd.TabIndex = 1
        Me.CmdRowsAdd.Text = "行追加"
        Me.CmdRowsAdd.UseVisualStyleBackColor = True
        '
        'CmdRowsDel
        '
        Me.CmdRowsDel.Location = New System.Drawing.Point(213, 213)
        Me.CmdRowsDel.Name = "CmdRowsDel"
        Me.CmdRowsDel.Size = New System.Drawing.Size(75, 23)
        Me.CmdRowsDel.TabIndex = 2
        Me.CmdRowsDel.Text = "行削除"
        Me.CmdRowsDel.UseVisualStyleBackColor = True
        '
        'CmdRowsMvUp
        '
        Me.CmdRowsMvUp.Location = New System.Drawing.Point(213, 242)
        Me.CmdRowsMvUp.Name = "CmdRowsMvUp"
        Me.CmdRowsMvUp.Size = New System.Drawing.Size(75, 23)
        Me.CmdRowsMvUp.TabIndex = 3
        Me.CmdRowsMvUp.Text = "↑"
        Me.CmdRowsMvUp.UseVisualStyleBackColor = True
        '
        'CmdRowsMvDn
        '
        Me.CmdRowsMvDn.Location = New System.Drawing.Point(213, 271)
        Me.CmdRowsMvDn.Name = "CmdRowsMvDn"
        Me.CmdRowsMvDn.Size = New System.Drawing.Size(75, 23)
        Me.CmdRowsMvDn.TabIndex = 4
        Me.CmdRowsMvDn.Text = "↓"
        Me.CmdRowsMvDn.UseVisualStyleBackColor = True
        '
        'CmdSave
        '
        Me.CmdSave.Location = New System.Drawing.Point(109, 336)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.Size = New System.Drawing.Size(75, 23)
        Me.CmdSave.TabIndex = 5
        Me.CmdSave.Text = "保存"
        Me.CmdSave.UseVisualStyleBackColor = True
        '
        'CmdClose
        '
        Me.CmdClose.Location = New System.Drawing.Point(213, 336)
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
        Me.CmdAllRowsDel.Location = New System.Drawing.Point(213, 300)
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
        'CmdLoad
        '
        Me.CmdLoad.Location = New System.Drawing.Point(12, 336)
        Me.CmdLoad.Name = "CmdLoad"
        Me.CmdLoad.Size = New System.Drawing.Size(75, 23)
        Me.CmdLoad.TabIndex = 19
        Me.CmdLoad.Text = "読込"
        Me.CmdLoad.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(10, 371)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(163, 12)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "読み込み済みピッチ拡張ファイル："
        '
        'TxtPchExpLoadedFile
        '
        Me.TxtPchExpLoadedFile.Location = New System.Drawing.Point(11, 387)
        Me.TxtPchExpLoadedFile.Name = "TxtPchExpLoadedFile"
        Me.TxtPchExpLoadedFile.Size = New System.Drawing.Size(276, 19)
        Me.TxtPchExpLoadedFile.TabIndex = 21
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(11, 151)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(185, 180)
        Me.TabControl1.TabIndex = 22
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DataGridView1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(177, 154)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "測定仕様"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DataGridView2)
        Me.TabPage2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(177, 154)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "過去の仕様"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToOrderColumns = True
        Me.DataGridView2.AllowUserToResizeRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2})
        Me.DataGridView2.Location = New System.Drawing.Point(3, 3)
        Me.DataGridView2.MultiSelect = False
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.RowTemplate.Height = 21
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView2.Size = New System.Drawing.Size(171, 148)
        Me.DataGridView2.TabIndex = 1
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "No."
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 50
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "ピッチ(mm)"
        Me.DataGridViewTextBoxColumn2.MinimumWidth = 100
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'FrmSST4500_1_0_0J_pitchsetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(302, 416)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TxtPchExpLoadedFile)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.CmdLoad)
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmSST4500_1_0_0J_pitchsetting"
        Me.Text = "ピッチ拡張設定"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents CmdLoad As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtPchExpLoadedFile As TextBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
End Class
