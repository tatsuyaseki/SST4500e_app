Public Class FrmSST4500_1_0_0J_passchg

    Private Sub CmdPasswdSave_Click(sender As Object, e As EventArgs) Handles CmdPasswdSave.Click
        Dim old_pass As String = TxtOldPasswd.Text
        Dim new_pass As String = TxtNewPasswd.Text
        Dim new_pass2 As String = TxtNewPasswd2.Text
        Dim ret As DialogResult
        Dim next_step As Integer

        next_step = 0

        If old_pass = "" Then
            MessageBox.Show("古いパスワードを入力してください。",
                            "パスワードエラー",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
        Else
            If new_pass = "" Then
                MessageBox.Show("新しいパスワードを入力してください。",
                            "パスワードエラー",
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
                        ret = MessageBox.Show("管理者モード2のパスワードを" & vbCrLf &
                                              "変更してもよろしいですか？",
                                              "パスワード変更確認",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Information)

                        If ret = vbYes Then
                            Dim passwd_temp As String = new_pass
                            Dim wrapper As New Simple3Des(passwd_key)
                            Dim cipherText As String = wrapper.EncryptData(passwd_temp)
                            My.Settings._passwd_adm = cipherText
                            My.Settings.Save()
                            FrmSST4500_1_0_0J_login.TxtInputPass.Text = ""
                            Me.Visible = False
                        End If
                    Else
                        MessageBox.Show("新しいパスワードが一致しませんでした。",
                                        "パスワードエラー",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                Else
                    MessageBox.Show("古いパスワードが違います。",
                                    "パスワードエラー",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                End If
            ElseIf FlgPasswdChg = 2 Then
                'adm2パスワード変更
                If old_pass = passwd_adm2 Then
                    If new_pass = new_pass2 Then
                        ret = MessageBox.Show("管理者モード2のパスワードを" & vbCrLf &
                                              "変更してもよろしいですか？",
                                              "パスワード変更確認",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Information)
                        If ret = vbYes Then
                            Dim passwd_temp As String = new_pass
                            Dim wrapper As New Simple3Des(passwd_key)
                            Dim cipherText As String = wrapper.EncryptData(passwd_temp)
                            My.Settings._passwd_adm2chg = cipherText
                            My.Settings.Save()
                            FrmSST4500_1_0_0J_login.TxtInputPass.Text = ""
                            Me.Visible = False
                        End If
                    Else
                        MessageBox.Show("新しいパスワードが一致しませんでした。",
                                        "パスワードエラー",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                Else
                    MessageBox.Show("古いパスワードが違います。",
                                    "パスワードエラー",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                End If
            ElseIf FlgPasswdChg = 3 Then
                'dbfsettingパスワード変更
                If old_pass = passwd_dbfsetting Then
                    If new_pass = new_pass2 Then
                        ret = MessageBox.Show("測定データフォーマット設定用の" & vbCrLf &
                                              "パスワードを変更してもよろしいですか？",
                                              "パスワード変更確認",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Information)
                        If ret = vbYes Then
                            Dim passwd_temp As String = new_pass
                            Dim wrapper As New Simple3Des(passwd_key)
                            Dim cipherText As String = wrapper.EncryptData(passwd_temp)
                            My.Settings._dbf_settingchg = cipherText
                            My.Settings.Save()
                            FrmSST4500_1_0_0J_login.TxtInputPass.Text = ""
                            Me.Visible = False
                        End If
                    Else
                        MessageBox.Show("新しいパスワードが一致しませんでした。",
                                        "パスワードエラー",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                Else
                    MessageBox.Show("古いパスワードが違います。",
                                    "パスワードエラー",
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

    Private Sub FrmSST4500_1_0_0J_passchg_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size

        If Me.Visible = True Then
            If FlgPasswdChg = 1 Then
                Me.Text = "パスワード変更"
            ElseIf FlgPasswdChg = 2 Then
                Me.Text = "パスワード変更2"
            ElseIf FlgPasswdChg = 3 Then
                Me.Text = "パスワード変更3"
            End If
        End If
    End Sub
End Class