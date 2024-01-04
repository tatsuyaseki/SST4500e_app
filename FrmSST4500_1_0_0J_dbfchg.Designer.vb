<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSST4500_1_0_0J_dbfchg
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
        Me.Rb_default = New System.Windows.Forms.RadioButton()
        Me.Rb_custum1 = New System.Windows.Forms.RadioButton()
        Me.CmdCancel = New System.Windows.Forms.Button()
        Me.CmdOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Rb_default
        '
        Me.Rb_default.AutoSize = True
        Me.Rb_default.Location = New System.Drawing.Point(12, 12)
        Me.Rb_default.Name = "Rb_default"
        Me.Rb_default.Size = New System.Drawing.Size(47, 16)
        Me.Rb_default.TabIndex = 0
        Me.Rb_default.TabStop = True
        Me.Rb_default.Text = "標準"
        Me.Rb_default.UseVisualStyleBackColor = True
        '
        'Rb_custum1
        '
        Me.Rb_custum1.AutoSize = True
        Me.Rb_custum1.Location = New System.Drawing.Point(124, 12)
        Me.Rb_custum1.Name = "Rb_custum1"
        Me.Rb_custum1.Size = New System.Drawing.Size(53, 16)
        Me.Rb_custum1.TabIndex = 1
        Me.Rb_custum1.TabStop = True
        Me.Rb_custum1.Text = "特殊1"
        Me.Rb_custum1.UseVisualStyleBackColor = True
        '
        'CmdCancel
        '
        Me.CmdCancel.Location = New System.Drawing.Point(66, 76)
        Me.CmdCancel.Name = "CmdCancel"
        Me.CmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.CmdCancel.TabIndex = 2
        Me.CmdCancel.Text = "キャンセル"
        Me.CmdCancel.UseVisualStyleBackColor = True
        '
        'CmdOK
        '
        Me.CmdOK.Location = New System.Drawing.Point(147, 76)
        Me.CmdOK.Name = "CmdOK"
        Me.CmdOK.Size = New System.Drawing.Size(75, 23)
        Me.CmdOK.TabIndex = 3
        Me.CmdOK.Text = "OK"
        Me.CmdOK.UseVisualStyleBackColor = True
        '
        'FrmSST4500_1_1_0J_dbfchg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(234, 111)
        Me.ControlBox = False
        Me.Controls.Add(Me.CmdOK)
        Me.Controls.Add(Me.CmdCancel)
        Me.Controls.Add(Me.Rb_custum1)
        Me.Controls.Add(Me.Rb_default)
        Me.Name = "FrmSST4500_1_1_0J_dbfchg"
        Me.Text = "測定データフォーマット切り替え"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Rb_default As RadioButton
    Friend WithEvents Rb_custum1 As RadioButton
    Friend WithEvents CmdCancel As Button
    Friend WithEvents CmdOK As Button
End Class
