<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSST4500_1_0_0E_login
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSST4500_1_0_0E_login))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtInputPass = New System.Windows.Forms.TextBox()
        Me.CmdLogin = New System.Windows.Forms.Button()
        Me.CmdCancel = New System.Windows.Forms.Button()
        Me.CmdPasswdChg = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "パスワードを入力して下さい"
        '
        'TxtInputPass
        '
        Me.TxtInputPass.Location = New System.Drawing.Point(12, 25)
        Me.TxtInputPass.Name = "TxtInputPass"
        Me.TxtInputPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtInputPass.Size = New System.Drawing.Size(260, 19)
        Me.TxtInputPass.TabIndex = 1
        '
        'CmdLogin
        '
        Me.CmdLogin.Location = New System.Drawing.Point(116, 50)
        Me.CmdLogin.Name = "CmdLogin"
        Me.CmdLogin.Size = New System.Drawing.Size(75, 23)
        Me.CmdLogin.TabIndex = 2
        Me.CmdLogin.Text = "ログイン"
        Me.CmdLogin.UseVisualStyleBackColor = True
        '
        'CmdCancel
        '
        Me.CmdCancel.Location = New System.Drawing.Point(197, 50)
        Me.CmdCancel.Name = "CmdCancel"
        Me.CmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.CmdCancel.TabIndex = 3
        Me.CmdCancel.Text = "キャンセル"
        Me.CmdCancel.UseVisualStyleBackColor = True
        '
        'CmdPasswdChg
        '
        Me.CmdPasswdChg.Location = New System.Drawing.Point(12, 50)
        Me.CmdPasswdChg.Name = "CmdPasswdChg"
        Me.CmdPasswdChg.Size = New System.Drawing.Size(75, 23)
        Me.CmdPasswdChg.TabIndex = 4
        Me.CmdPasswdChg.Text = "変更"
        Me.CmdPasswdChg.UseVisualStyleBackColor = True
        '
        'FrmSST4500_1_0_0J_login
        '
        Me.AcceptButton = Me.CmdLogin
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 82)
        Me.ControlBox = False
        Me.Controls.Add(Me.CmdPasswdChg)
        Me.Controls.Add(Me.CmdCancel)
        Me.Controls.Add(Me.CmdLogin)
        Me.Controls.Add(Me.TxtInputPass)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSST4500_1_0_0J_login"
        Me.Text = "管理者モードログイン"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TxtInputPass As TextBox
    Friend WithEvents CmdLogin As Button
    Friend WithEvents CmdCancel As Button
    Friend WithEvents CmdPasswdChg As Button
End Class
