Imports System.ComponentModel

Public Class FrmSST4500_1_0_0E_login
    Private Sub CmdLogin_Click(sender As Object, e As EventArgs) Handles CmdLogin.Click
        'OKボタン
        Dim inputtext = TxtInputPass.Text
        If inputtext = "" Then
            MessageBox.Show("Enter the password",
                            "Enter the password",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
        Else
            passResult = 1
            strTemp = inputtext
            Me.Visible = False
        End If
    End Sub

    Private Sub CmdCancel_Click(sender As Object, e As EventArgs) Handles CmdCancel.Click
        'キャンセル
        passResult = 0
        Me.Visible = False
    End Sub

    Private Sub CmdPasswdChg_Click(sender As Object, e As EventArgs) Handles CmdPasswdChg.Click
        FlgPasswdChg = 1
        FrmSST4500_1_0_0E_passchg.Visible = True
    End Sub
End Class