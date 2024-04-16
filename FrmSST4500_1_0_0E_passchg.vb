Public Class FrmSST4500_1_0_0E_passchg

    Private Sub CmdPasswdSave_Click(sender As Object, e As EventArgs) Handles CmdPasswdSave.Click
        Dim old_pass As String = TxtOldPasswd.Text
        Dim new_pass As String = TxtNewPasswd.Text
        Dim new_pass2 As String = TxtNewPasswd2.Text
        Dim ret As DialogResult
        Dim next_step As Integer

        next_step = 0

        If old_pass = "" Then
            MessageBox.Show("Input Current Password",
                            StrPassErr,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
        Else
            If new_pass = "" Then
                MessageBox.Show("Input New Password",
                                StrPassErr,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
            Else
                next_step = 1
            End If
        End If

        If next_step = 1 Then
            If FlgPasswdChg = 1 Then
                'admパスワード変更
                If old_pass = passwd_adm Then
                    If new_pass = new_pass2 Then
                        ret = MessageBox.Show("Are you sure to change admin. Password?",
                                              StrConfirmPassChg,
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Information)

                        If ret = vbYes Then
                            Dim passwd_temp As String = new_pass
                            Dim wrapper As New Simple3Des(passwd_key)
                            Dim cipherText As String = wrapper.EncryptData(passwd_temp)
                            My.Settings._passwd_adm = cipherText
                            My.Settings.Save()
                            FrmSST4500_1_0_0E_login.TxtInputPass.Text = ""
                            Me.Visible = False
                        End If
                    Else
                        MessageBox.Show("New password did not match",
                                        StrPassErr,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                Else
                    MessageBox.Show("Curent password is incorrect",
                                    StrPassErr,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                End If
            ElseIf FlgPasswdChg = 2 Then
                'adm2パスワード変更
                If old_pass = passwd_adm2 Then
                    If new_pass = new_pass2 Then
                        ret = MessageBox.Show("Are you sure to change admin.mode 2 password?",
                                              StrConfirmPassChg,
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Information)
                        If ret = vbYes Then
                            Dim passwd_temp As String = new_pass
                            Dim wrapper As New Simple3Des(passwd_key)
                            Dim cipherText As String = wrapper.EncryptData(passwd_temp)
                            My.Settings._passwd_adm2chg = cipherText
                            My.Settings.Save()
                            FrmSST4500_1_0_0E_login.TxtInputPass.Text = ""
                            Me.Visible = False
                        End If
                    Else
                        MessageBox.Show("New password did not match",
                                        StrPassErr,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                Else
                    MessageBox.Show("Curent password is incorrect",
                                    StrPassErr,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                End If
            ElseIf FlgPasswdChg = 3 Then
                'dbfsettingパスワード変更
                If old_pass = passwd_dbfsetting Then
                    If new_pass = new_pass2 Then
                        ret = MessageBox.Show("Are you sure you want to change" & vbCrLf &
                                              "the password for setting data format?",
                                              StrConfirmPassChg,
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Information)
                        If ret = vbYes Then
                            Dim passwd_temp As String = new_pass
                            Dim wrapper As New Simple3Des(passwd_key)
                            Dim cipherText As String = wrapper.EncryptData(passwd_temp)
                            My.Settings._dbf_settingchg = cipherText
                            My.Settings.Save()
                            FrmSST4500_1_0_0E_login.TxtInputPass.Text = ""
                            Me.Visible = False
                        End If
                    Else
                        MessageBox.Show("New password did not match",
                                        StrPassErr,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                Else
                    MessageBox.Show("Curent password is incorrect",
                                    StrPassErr,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                End If
            ElseIf FlgPasswdChg = 4 Then
                'PchExpSettingパスワード変更
                If old_pass = passwd_pchexpsetting Then
                    If new_pass = new_pass2 Then
                        ret = MessageBox.Show("Are you sure you want to change" & vbCrLf &
                                              "the password for setting pitch?",
                                              StrConfirmPassChg,
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Information)
                        If ret = vbYes Then
                            Dim passwd_temp As String = new_pass
                            Dim wrapper As New Simple3Des(passwd_key)
                            Dim cipherText As String = wrapper.EncryptData(passwd_temp)
                            My.Settings._pchexp_setting = cipherText
                            My.Settings.Save()
                            FrmSST4500_1_0_0E_login.TxtInputPass.Text = ""
                            Me.Visible = False
                        End If
                    Else
                        MessageBox.Show("New password did not match",
                                        StrPassErr,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                Else
                    MessageBox.Show("Curent password is incorrect",
                                    StrPassErr,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                End If
            End If

            TxtOldPasswd.Text = ""
            TxtNewPasswd.Text = ""
            TxtNewPasswd2.Text = ""
        End If

    End Sub

    Private Sub CmdCancel_Click(sender As Object, e As EventArgs) Handles CmdCancel.Click
        Me.Visible = False
    End Sub

    Private Sub FrmSST4500_1_0_0E_passchg_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size

        If Me.Visible = True Then
            If FlgPasswdChg = 1 Then
                Me.Text = "Password Change"
            ElseIf FlgPasswdChg = 2 Then
                Me.Text = "Password Change 2"
            ElseIf FlgPasswdChg = 3 Then
                Me.Text = "Password Change 3"
            ElseIf FlgPasswdChg = 4 Then
                Me.Text = "Password Change4"
            Else
                Me.Text = "Password Change"
            End If
        End If
    End Sub
End Class