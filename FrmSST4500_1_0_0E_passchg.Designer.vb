﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSST4500_1_0_0E_passchg
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSST4500_1_0_0E_passchg))
        Me.TxtOldPasswd = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtNewPasswd = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtNewPasswd2 = New System.Windows.Forms.TextBox()
        Me.CmdPasswdSave = New System.Windows.Forms.Button()
        Me.CmdCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TxtOldPasswd
        '
        Me.TxtOldPasswd.Location = New System.Drawing.Point(12, 24)
        Me.TxtOldPasswd.Name = "TxtOldPasswd"
        Me.TxtOldPasswd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtOldPasswd.Size = New System.Drawing.Size(260, 19)
        Me.TxtOldPasswd.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 12)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Current Password"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 12)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "New Password"
        '
        'TxtNewPasswd
        '
        Me.TxtNewPasswd.Location = New System.Drawing.Point(12, 70)
        Me.TxtNewPasswd.Name = "TxtNewPasswd"
        Me.TxtNewPasswd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtNewPasswd.Size = New System.Drawing.Size(260, 19)
        Me.TxtNewPasswd.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(157, 12)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "New Password (Confirmation)"
        '
        'TxtNewPasswd2
        '
        Me.TxtNewPasswd2.Location = New System.Drawing.Point(12, 111)
        Me.TxtNewPasswd2.Name = "TxtNewPasswd2"
        Me.TxtNewPasswd2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtNewPasswd2.Size = New System.Drawing.Size(260, 19)
        Me.TxtNewPasswd2.TabIndex = 2
        '
        'CmdPasswdSave
        '
        Me.CmdPasswdSave.Location = New System.Drawing.Point(117, 136)
        Me.CmdPasswdSave.Name = "CmdPasswdSave"
        Me.CmdPasswdSave.Size = New System.Drawing.Size(75, 23)
        Me.CmdPasswdSave.TabIndex = 12
        Me.CmdPasswdSave.Text = "Change"
        Me.CmdPasswdSave.UseVisualStyleBackColor = True
        '
        'CmdCancel
        '
        Me.CmdCancel.Location = New System.Drawing.Point(198, 136)
        Me.CmdCancel.Name = "CmdCancel"
        Me.CmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.CmdCancel.TabIndex = 13
        Me.CmdCancel.Text = "Cancel"
        Me.CmdCancel.UseVisualStyleBackColor = True
        '
        'FrmSST4500_1_0_0E_passchg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 169)
        Me.ControlBox = False
        Me.Controls.Add(Me.CmdCancel)
        Me.Controls.Add(Me.CmdPasswdSave)
        Me.Controls.Add(Me.TxtNewPasswd2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtNewPasswd)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtOldPasswd)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmSST4500_1_0_0E_passchg"
        Me.Text = "Password Change"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtOldPasswd As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtNewPasswd As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtNewPasswd2 As TextBox
    Friend WithEvents CmdPasswdSave As Button
    Friend WithEvents CmdCancel As Button
End Class
