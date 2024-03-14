<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSST4500_1_0_0E_pchchg
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
        Me.CmdOK = New System.Windows.Forms.Button()
        Me.CmdCancel = New System.Windows.Forms.Button()
        Me.Rb_Disable = New System.Windows.Forms.RadioButton()
        Me.Rb_Enable = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'CmdOK
        '
        Me.CmdOK.Location = New System.Drawing.Point(147, 76)
        Me.CmdOK.Name = "CmdOK"
        Me.CmdOK.Size = New System.Drawing.Size(75, 23)
        Me.CmdOK.TabIndex = 7
        Me.CmdOK.Text = "OK"
        Me.CmdOK.UseVisualStyleBackColor = True
        '
        'CmdCancel
        '
        Me.CmdCancel.Location = New System.Drawing.Point(66, 76)
        Me.CmdCancel.Name = "CmdCancel"
        Me.CmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.CmdCancel.TabIndex = 6
        Me.CmdCancel.Text = "キャンセル"
        Me.CmdCancel.UseVisualStyleBackColor = True
        '
        'Rb_Disable
        '
        Me.Rb_Disable.AutoSize = True
        Me.Rb_Disable.Location = New System.Drawing.Point(12, 12)
        Me.Rb_Disable.Name = "Rb_Disable"
        Me.Rb_Disable.Size = New System.Drawing.Size(59, 16)
        Me.Rb_Disable.TabIndex = 5
        Me.Rb_Disable.TabStop = True
        Me.Rb_Disable.Text = "非表示"
        Me.Rb_Disable.UseVisualStyleBackColor = True
        '
        'Rb_Enable
        '
        Me.Rb_Enable.AutoSize = True
        Me.Rb_Enable.Location = New System.Drawing.Point(116, 12)
        Me.Rb_Enable.Name = "Rb_Enable"
        Me.Rb_Enable.Size = New System.Drawing.Size(47, 16)
        Me.Rb_Enable.TabIndex = 4
        Me.Rb_Enable.TabStop = True
        Me.Rb_Enable.Text = "表示"
        Me.Rb_Enable.UseVisualStyleBackColor = True
        '
        'FrmSST4500_1_0_0J_pchchg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(234, 111)
        Me.ControlBox = False
        Me.Controls.Add(Me.CmdOK)
        Me.Controls.Add(Me.CmdCancel)
        Me.Controls.Add(Me.Rb_Disable)
        Me.Controls.Add(Me.Rb_Enable)
        Me.Name = "FrmSST4500_1_0_0J_pchchg"
        Me.Text = "ピッチ拡張設定切り替え"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CmdOK As Button
    Friend WithEvents CmdCancel As Button
    Friend WithEvents Rb_Disable As RadioButton
    Friend WithEvents Rb_Enable As RadioButton
End Class
