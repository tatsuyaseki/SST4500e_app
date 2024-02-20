
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.IO
Imports System.Security.Cryptography

Public Class FrmSST4500_1_0_0J_main
    Dim _status As Short
    Dim _flgRx As Integer
    Dim _flgfeeder As Integer

    Private Function CmdUSBOpen_Click() As Integer

        _status = UsbOpen()
        If _status = 0 Then
            ToolStripStatusLabel1.Text = "USB接続NG / status=" & Str(ftStatus)
            FrmSST4500_1_0_0J_meas.ToolStripStatusLabel1.Text = ToolStripStatusLabel1.Text
            FrmSST4500_1_0_0J_Profile.ToolStripStatusLabel1.Text = ToolStripStatusLabel1.Text
            FrmSST4500_1_0_0J_test.ToolStripStatusLabel1.Text = ToolStripStatusLabel1.Text
        ElseIf _status = -1 Then
            ToolStripStatusLabel1.Text = "USB DLL Not Found"
            FrmSST4500_1_0_0J_meas.ToolStripStatusLabel1.Text = ToolStripStatusLabel1.Text
            FrmSST4500_1_0_0J_Profile.ToolStripStatusLabel1.Text = ToolStripStatusLabel1.Text
            FrmSST4500_1_0_0J_test.ToolStripStatusLabel1.Text = ToolStripStatusLabel1.Text
        Else
            ToolStripStatusLabel1.Text = "USB接続OK = " & Str(lngHandle)
            FrmSST4500_1_0_0J_meas.ToolStripStatusLabel1.Text = ToolStripStatusLabel1.Text
            FrmSST4500_1_0_0J_Profile.ToolStripStatusLabel1.Text = ToolStripStatusLabel1.Text
            FrmSST4500_1_0_0J_test.ToolStripStatusLabel1.Text = ToolStripStatusLabel1.Text
        End If
        Return _status
    End Function

    Private Sub Form1_FormClosed1(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If FlgFTDLLerr = 0 Then
            UsbClose()
        End If
        End
    End Sub

    Private Sub Form1_Load_1(ByVal sender As System.Object, ByVale As System.EventArgs) Handles MyBase.Load
        'Me.MaximumSize = Me.Size
        Dim config As System.Configuration.Configuration =
            System.Configuration.ConfigurationManager.OpenExeConfiguration(
            System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal)
        FrmSST4500_1_0_0J_setting.txtUserconf.Text = config.FilePath
        Console.WriteLine(config.FilePath)

        Me.MinimumSize = Me.Size

        cur_dir = Directory.GetCurrentDirectory
        Me.Text = My.Application.Info.ProductName & " Menu (Ver:" & My.Application.Info.Version.ToString & ")"
        Me.LblProductNameMenu.Text = My.Application.Info.ProductName

        SG_ResultSave_path = My.Settings._sgresultsave_path
        If SG_ResultSave_path = "" Then
            SG_ResultSave_path = cur_dir & DEF_RESULT_FILE_FLD
        End If

        PF_ResultSave_path = My.Settings._pfresultsave_path
        If PF_ResultSave_path = "" Then
            PF_ResultSave_path = cur_dir & DEF_RESULT_FILE_FLD
        End If

        CT_ResultSave_path = My.Settings._ctresultsave_path
        If CT_ResultSave_path = "" Then
            CT_ResultSave_path = cur_dir & DEF_RESULT_FILE_FLD
        End If

        LG_ResultSave_path = My.Settings._lgresultsave_path
        If LG_ResultSave_path = "" Then
            LG_ResultSave_path = cur_dir & DEF_RESULT_FILE_FLD
        End If

        'Dataフォルダの確認
        Dim data_path As String
        data_path = cur_dir & DEF_DATA_FILE_FLD
        If Directory.Exists(data_path) = False Then
            Directory.CreateDirectory(data_path)
        End If

        'Resultフォルダの確認
        Dim result_path As String
        result_path = cur_dir & DEF_RESULT_FILE_FLD & "\"
        If Directory.Exists(result_path) = False Then
            Directory.CreateDirectory(result_path)
        End If

        '印刷　マージン
        Prn_left_margin = My.Settings._printmargin_left
        Prn_right_margin = My.Settings._printmargin_right
        Prn_top_margin = My.Settings._printmargin_top
        Prn_btm_margin = My.Settings._printmargin_bottom
        FlgPrnBc_enable = My.Settings._printbc

        'pen color
        angdpgraph_color = My.Settings._angdpgraph_color
        angpkgraph_color = My.Settings._angpkgraph_color
        angdpgraph3_color = My.Settings._angdpgraph_color3
        angpkgraph3_color = My.Settings._angpkgraph_color3
        ratpkdpgraph_color = My.Settings._ratpkdpgraph_color
        ratmdcdgraph_color = My.Settings._ratmdcdgraph_color
        ratpkdpgraph3_color = My.Settings._ratpkdpgraph_color3
        ratmdcdgraph3_color = My.Settings._ratmdcdgraph_color3
        velomdgraph_color = My.Settings._velomdgraph_color
        velocdgraph_color = My.Settings._velocdgraph_color
        velopkgraph_color = My.Settings._velopkgraph_color
        velodpgraph_color = My.Settings._velodpgraph_color
        velomdgraph3_color = My.Settings._velomdgraph_color3
        velocdgraph3_color = My.Settings._velocdgraph_color3
        velopkgraph3_color = My.Settings._velopkgraph_color3
        velodpgraph3_color = My.Settings._velodpgraph_color3
        tsimdgraph_color = My.Settings._tsimdgraph_color
        tsicdgraph_color = My.Settings._tsicdgraph_color
        tsimdgraph3_color = My.Settings._tsimdgraph_color3
        tsicdgraph3_color = My.Settings._tsicdgraph_color3

        mainform_color_setting_load()
        measform_color_setting_load()
        prfform_color_setting_load()

        'test count
        test_count1 = My.Settings._test_count1              'テストモード蒔の予備加圧時間
        test_count2 = My.Settings._test_count2              'テストモード時の測定時間
        test_count1_prf = My.Settings._test_count1_prf
        test_count2_prf = My.Settings._test_count2_prf
        test_count1_md = My.Settings._test_count1_md
        test_count2_md = My.Settings._test_count2_md

        timeout_time = My.Settings._timeout_time
        cmd_timeout = My.Settings._cmd_timeout
        feed_timeout = My.Settings._feed_timeout

        FlgDBF = My.Settings._flg_dbf
        If FlgDBF = 1 Then
            FrmSST4500_1_0_0J_dbfchg.Rb_custum1.Checked = True
            ToolStripStatusLabel4.Text = "特殊1"
        Else
            FrmSST4500_1_0_0J_dbfchg.Rb_default.Checked = True
            ToolStripStatusLabel4.Text = ""
        End If
        FrmSST4500_1_0_0J_meas.ToolStripStatusLabel5.Text = ToolStripStatusLabel4.Text
        FrmSST4500_1_0_0J_Profile.ToolStripStatusLabel5.Text = ToolStripStatusLabel4.Text
        FrmSST4500_1_0_0J_test.ToolStripStatusLabel5.Text = ToolStripStatusLabel4.Text

        FlgPchExp_Visible = My.Settings._flg_pchexp_visible
        With FrmSST4500_1_0_0J_Profile
            If FlgPchExp_Visible = 1 Then
                'ピッチ拡張表示蒔
                .ChkPitchExp_Ena.Visible = True
                .ChkPitchExp_Dis.Visible = True
                .LblPitchExp.Visible = True
                .LblPitch.Visible = False
            Else
                'ピッチ拡張非表示蒔
                .ChkPitchExp_Ena.Visible = False
                .ChkPitchExp_Dis.Visible = False
                .LblPitch.Visible = False
                .LblPitchExp.Visible = True
                FlgPitchExp = 0     '無効時は強制的にピッチ拡張OFF
            End If
        End With

        FlgFTDLLerr = 0

        TimerCountS = 0
        TimSplash.Enabled = True

        FlgInitSplash = 0
        FlgMainSplash = 1

        ToolStripStatusLabel3.Text = "未接続"
        FrmSST4500_1_0_0J_meas.ToolStripStatusLabel3.Text = ToolStripStatusLabel3.Text
        FrmSST4500_1_0_0J_Profile.ToolStripStatusLabel3.Text = ToolStripStatusLabel3.Text
        FrmSST4500_1_0_0J_test.ToolStripStatusLabel3.Text = ToolStripStatusLabel3.Text

        Dim wrapper As New Simple3Des(passwd_key)
        Dim passwd_adm_temp As String
        Dim passwd_adm2_temp As String
        Dim passwd_adm2_chg_temp As String
        Dim passwd_dbfsetting_temp As String
        Dim passwd_dbfsetting_chg_temp As String
        Dim passwd_pchexpsetting_temp As String
        Dim passwd_pchexpsetting_chg_temp As String

        'Console.WriteLine("passwd_adm : " & wrapper.EncryptData("SST4500"))
        'Console.WriteLine("passwd_adm2 : " & wrapper.EncryptData("NMR8001"))
        'Console.WriteLine("passwd_adm2_chg : " & wrapper.EncryptData("NMRCHG"))
        'Console.WriteLine("passwd_dbfsetting : " & wrapper.EncryptData("DBFSET"))
        'Console.WriteLine("passwd_dbfsetting_chg : " & wrapper.EncryptData("DBFCHG"))
        'Console.WriteLine("passwd_pchexp_setting : " & wrapper.EncryptData("PCHSET"))
        'Console.WriteLine("passwd_pchexp_setting_chg : " & wrapper.EncryptData("PCHCHG"))

        passwd_adm_temp = My.Settings._passwd_adm
        passwd_adm = wrapper.DecryptData(passwd_adm_temp)
        passwd_adm2_temp = My.Settings._passwd_adm2
        passwd_adm2 = wrapper.DecryptData(passwd_adm2_temp)
        passwd_adm2_chg_temp = My.Settings._passwd_adm2chg
        passwd_adm2_chg = wrapper.DecryptData(passwd_adm2_chg_temp)
        passwd_dbfsetting_temp = My.Settings._dbf_setting
        passwd_dbfsetting = wrapper.DecryptData(passwd_dbfsetting_temp)
        passwd_dbfsetting_chg_temp = My.Settings._dbf_settingchg
        passwd_dbfsetting_chg = wrapper.DecryptData(passwd_dbfsetting_chg_temp)
        passwd_pchexpsetting_temp = My.Settings._pchexp_setting
        passwd_pchexpsetting = wrapper.DecryptData(passwd_pchexpsetting_temp)
        passwd_pchexpsetting_chg_temp = My.Settings._pchexp_settingchg
        passwd_pchexpsetting_chg = wrapper.DecryptData(passwd_pchexpsetting_chg_temp)

        mainform_color_init()
        mainform_borderstyle_init()
        measform_color_init()
        measform_borderstyle_init()
        prfform_color_init()
        prfform_borderstyle_init()

    End Sub

    Private Sub TimSplash_Tick(sender As Object, e As EventArgs) Handles TimSplash.Tick


        Select Case FlgMainSplash
            Case 1
                PBEnable_OFF()
                Mode_cont(FlgAdmin, FlgTest)

                TimSplash.Enabled = False

                _status = CmdUSBOpen_Click()
                If _status = 1 Then
                    FlgMainSplash = 2
                    TimerCountS = 0
                ElseIf _status = -1 Then
                    S_MogiData()

                    FlgMainSplash = 3
                    FlgTest = 1
                    FlgFeeder = 1
                End If

                TimSplash.Enabled = True

            Case 2
                TimerCountS += 1
                If TimerCountS >= 50 Then
                    FlgMainSplash = 21
                End If

            Case 11
                TimerCountS = 0
                FlgMainSplash = 12

            Case 12
                TimerCountS += 1

                If TimerCountS = 10 Then
                    PBEnable_ON()
                    TimerCountS = 0
                    FlgMainSplash = 13
                End If

            Case 13
                FlgMainSplash = 0

            Case 21
                strWdata = "RDY" & vbCr
                UsbWrite(strWdata)
                FlgMainSplash = 22

            Case 22
                'REDY or REDZ を受信するまで繰り返す
                'なので途中でUSB接続や電源ONでも接続を確立できる
                strRxdata = ""
                _flgRx = UsbRead(strRxdata)

                If _flgRx = 0 Then
                    If strRxdata = "REDY" & vbCr Then
                        FlgMainSplash = 3
                        FlgAdmin = 0
                        _flgfeeder = 1
                    ElseIf strRxdata = "REDZ" & vbCr Then
                        FlgMainSplash = 3
                        _flgfeeder = 0
                    End If
                    ToolStripStatusLabel3.Text = "接続済み"
                    FrmSST4500_1_0_0J_meas.ToolStripStatusLabel3.Text = ToolStripStatusLabel3.Text
                    FrmSST4500_1_0_0J_Profile.ToolStripStatusLabel3.Text = ToolStripStatusLabel3.Text
                    FrmSST4500_1_0_0J_test.ToolStripStatusLabel3.Text = ToolStripStatusLabel3.Text
                    FlgFeeder = _flgfeeder
                Else
                    TimerCountS = 0
                    FlgMainSplash = 2
                End If

            Case 3  'PCとの接続OK又はテストモード
                TimerCountS += 1
                If TimerCountS Mod 5 = 0 Then
                    PBEnable_ON()
                    TimerCountS = 0
                    FlgMainSplash = 31
                End If

            Case 31 '初期化完了
                TimerCountS += 1
                If TimerCountS = 10 Then
                    FlgInitSplash = 1
                    FlgMainSplash = 0
                    If FlgFTDLLerr = 0 Then
                        UsbClose()
                    End If
                End If

            Case 50
                TimSplash.Enabled = False
                FlgMainSplash = 0
                strTemp = ""
                Dim passForm As New FrmSST4500_1_0_0J_login
                passForm.ShowDialog()
                passForm.Dispose()
                FlgMainSplash = 51
                TimSplash.Enabled = True

            Case 51
                TimSplash.Enabled = False
                If passResult = 1 Then
                    If strTemp = passwd_adm Then
                        FlgAdmin = 1
                        FlgMainSplash = 52
                        TimerCountS = 0
                    ElseIf strTemp = passwd_adm2 Then
                        FlgAdmin = 2
                        FlgMainSplash = 52
                        TimerCountS = 0
                    ElseIf strTemp = passwd_adm2_chg Then
                        FlgPasswdChg = 2
                        FlgMainSplash = 0
                        TimerCountS = 0
                        FrmSST4500_1_0_0J_passchg.Visible = True
                    ElseIf strTemp = passwd_dbfsetting Then
                        FlgMainSplash = 0
                        TimerCountS = 0
                        FrmSST4500_1_0_0J_dbfchg.Visible = True
                    ElseIf strTemp = passwd_dbfsetting_chg Then
                        FlgPasswdChg = 3
                        FlgMainSplash = 0
                        TimerCountS = 0
                        FrmSST4500_1_0_0J_passchg.Visible = True
                    ElseIf strTemp = passwd_pchexpsetting Then
                        FlgMainSplash = 0
                        TimerCountS = 0
                        FrmSST4500_1_0_0J_pchchg.Visible = True
                    ElseIf strTemp = passwd_pchexpsetting_chg Then
                        FlgPasswdChg = 4
                        FlgMainSplash = 0
                        TimerCountS = 0
                        FrmSST4500_1_0_0J_passchg.Visible = True
                    Else
                        FlgAdmin = 0
                        FlgMainSplash = 0
                        MessageBox.Show("パスワードが違います。",
                                        "パスワードエラー",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                Else
                    FlgAdmin = 0
                    FlgMainSplash = 0
                End If
                TimSplash.Enabled = True

            Case 52
                TimerCountS += 1
                If TimerCountS > 10 Then
                    PBEnable_ON()
                    TimerCountS = 0
                    FlgMainSplash = 0
                End If
        End Select
    End Sub

    Private Sub Mode_cont(ByVal FlgAdm As Integer, ByVal FlgTest As Integer)
        If FlgAdm = 1 Then
            If FlgTest = 1 Then
                ToolStripStatusLabel2.Text = "管理者モード1(TEST)"
                FrmSST4500_1_0_0J_meas.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
                FrmSST4500_1_0_0J_Profile.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
                FrmSST4500_1_0_0J_test.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
            Else
                ToolStripStatusLabel2.Text = "管理者モード1"
                FrmSST4500_1_0_0J_meas.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
                FrmSST4500_1_0_0J_Profile.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
                FrmSST4500_1_0_0J_test.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
            End If
        ElseIf FlgAdm = 2 Then
            If FlgTest = 1 Then
                ToolStripStatusLabel2.Text = "管理者モード2(TEST)"
                FrmSST4500_1_0_0J_meas.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
                FrmSST4500_1_0_0J_Profile.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
                FrmSST4500_1_0_0J_test.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
            Else
                ToolStripStatusLabel2.Text = "管理者モード2"
                FrmSST4500_1_0_0J_meas.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
                FrmSST4500_1_0_0J_Profile.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
                FrmSST4500_1_0_0J_test.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
            End If
        Else
            If FlgTest = 1 Then
                ToolStripStatusLabel2.Text = "通常モード(TEST)"
                FrmSST4500_1_0_0J_meas.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
                FrmSST4500_1_0_0J_Profile.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
                FrmSST4500_1_0_0J_test.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
            Else
                ToolStripStatusLabel2.Text = "通常モード"
                FrmSST4500_1_0_0J_meas.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
                FrmSST4500_1_0_0J_Profile.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
                FrmSST4500_1_0_0J_test.ToolStripStatusLabel2.Text = ToolStripStatusLabel2.Text
            End If
        End If
    End Sub

    Private Sub PBEnable_ON()
        'シングルシートボタン 有効化
        'カットシートボタン有効化
        'プロファイルボタン有効化(フィーダー有りの時のみ

        CmdSinglesheet.Enabled = True
        シングルシートToolStripMenuItem.Enabled = True
        CmdCutSheetProfile.Enabled = True
        カットシートToolStripMenuItem.Enabled = True
        If FlgFeeder = 1 Then
            CmdProfile.Enabled = True
            プロファイルToolStripMenuItem.Enabled = True
        Else
            CmdProfile.Enabled = False
            プロファイルToolStripMenuItem.Enabled = False
        End If
        CmdAdmin.Enabled = True
        管理者ログインToolStripMenuItem.Enabled = True

        If FlgAdmin = 1 Then   '管理者モード"SST4000"
            If FlgFeeder = 1 Then   'フィーダー有り
                'CmdMDlong.Visible = True
                CmdMDlong.Enabled = True
                MD長尺測定ToolStripMenuItem1.Enabled = True
            Else                    'フィーダー無し
                'CmdMDlong.Visible = False
                CmdMDlong.Enabled = False
                MD長尺測定ToolStripMenuItem1.Enabled = False
            End If
            'CmdTest.Visible = False
            CmdTest.Enabled = False
            試験調整ToolStripMenuItem.Enabled = False
        ElseIf FlgAdmin = 2 Then    '管理者モード"NMR"
            If FlgFeeder = 1 Then
                'CmdMDlong.Visible = True
                CmdMDlong.Enabled = True
                MD長尺測定ToolStripMenuItem1.Enabled = True
            Else
                'CmdMDlong.Visible = False
                CmdMDlong.Enabled = False
                MD長尺測定ToolStripMenuItem1.Enabled = False
            End If
            'CmdTest.Visible = True
            CmdTest.Enabled = True
            試験調整ToolStripMenuItem.Enabled = True
        Else
            'CmdMDlong.Visible = False
            CmdMDlong.Enabled = False
            MD長尺測定ToolStripMenuItem1.Enabled = False
            'CmdTest.Visible = False
            CmdTest.Enabled = False
            試験調整ToolStripMenuItem.Enabled = False
        End If
        Mode_cont(FlgAdmin, FlgTest)
    End Sub

    Private Sub PBEnable_OFF()
        CmdSinglesheet.Enabled = False
        CmdCutSheetProfile.Enabled = False
        CmdProfile.Enabled = False
        CmdAdmin.Enabled = False
        'CmdMDlong.Visible = False
        CmdMDlong.Enabled = False
        'CmdTest.Visible = False
        CmdTest.Enabled = False
        シングルシートToolStripMenuItem.Enabled = False
        カットシートToolStripMenuItem.Enabled = False
        プロファイルToolStripMenuItem.Enabled = False
        MD長尺測定ToolStripMenuItem1.Enabled = False
        試験調整ToolStripMenuItem.Enabled = False
        管理者ログインToolStripMenuItem.Enabled = False
    End Sub

    Private Sub PBEnable_OFF_adm()
        '管理者モードを終了した時
        'CmdMDlong.Visible = False
        CmdMDlong.Enabled = False
        MD長尺測定ToolStripMenuItem1.Enabled = False
        'CmdTest.Visible = False
        CmdTest.Enabled = False
        試験調整ToolStripMenuItem.Enabled = False
    End Sub

    Private Sub CmdAdmin_Click(sender As Object, e As EventArgs) Handles CmdAdmin.Click
        admin_login()
    End Sub

    Private Sub admin_login()
        If FlgAdmin = 0 Then
            'FlgAdmin = 1
            FlgMainSplash = 50
        Else
            FlgAdmin = 0
            PBEnable_OFF_adm()
            Mode_cont(FlgAdmin, FlgTest)
            FlgMainSplash = 0
        End If
    End Sub

    Private Sub CmdQuitSplash_Click(sender As Object, e As EventArgs) Handles CmdQuitSplash.Click
        If FlgFTDLLerr = 0 Then
            UsbClose()
        End If
        Me.Close()
    End Sub

    Private Sub LblProduct_Click(sender As Object, e As EventArgs) Handles LblProductNameMenu.Click
        If FlgTest = 0 Then
            S_MogiData()

            FlgMainSplash = 3
            FlgTest = 1
            FlgFeeder = 1
        Else
            FlgTest = 0
            FlgFeeder = _flgfeeder
            FlgMainSplash = 1
            FlgAdmin = 0
        End If
    End Sub

    Private Sub CmdSinglesheet_Click(sender As Object, e As EventArgs) Handles CmdSinglesheet.Click
        singlesheet_run()
    End Sub

    Private Sub CmdProfile_Click(sender As Object, e As EventArgs) Handles CmdProfile.Click
        profile_run()
    End Sub

    Private Sub profile_run()
        TimSplash.Enabled = False
        Me.Visible = False
        With FrmSST4500_1_0_0J_Profile
            .TabControl1.SelectedIndex = 0
            .Visible = True
        End With

        FlgMainSplash = 0
        FlgInitSplash = 0
        FlgMainProfile = 1
        FlgProfile = 1
    End Sub

    Private Sub CmdCutSheetProfile_Click(sender As Object, e As EventArgs) Handles CmdCutSheetProfile.Click
        cutsheet_run()
    End Sub

    Private Sub cutsheet_run()
        TimSplash.Enabled = False
        Me.Visible = False
        With FrmSST4500_1_0_0J_Profile
            .TabControl1.SelectedIndex = 0
            .Visible = True
        End With

        FlgMainSplash = 0
        FlgInitSplash = 0
        FlgMainProfile = 1
        FlgProfile = 2
    End Sub


    Private Sub CmdMDlong_Click(sender As Object, e As EventArgs) Handles CmdMDlong.Click
        TimSplash.Enabled = False
        Me.Visible = False
        With FrmSST4500_1_0_0J_Profile
            .TabControl1.SelectedIndex = 0
            .Visible = True
        End With

        FlgMainSplash = 0
        FlgInitSplash = 0
        FlgMainProfile = 1
        FlgProfile = 3
    End Sub

    Private Sub FrmSST4500_1_0_0J_main_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            timerCount1 = 0

            TimSplash.Enabled = True
        End If
    End Sub

    Private Sub CmdTest_Click(sender As Object, e As EventArgs) Handles CmdTest.Click
        TimSplash.Enabled = False
        Me.Visible = False
        FrmSST4500_1_0_0J_test.Visible = True

        FlgMainSplash = 0
        FlgInitSplash = 0
        FlgMainTest = 1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        FrmSST4500_1_0_0J_colorsetting.Visible = True
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Console.WriteLine(e.Graphics.PageUnit)
        'e.Graphics.PageUnit = GraphicsUnit.Millimeter
        Console.WriteLine(e.Graphics.PageUnit)
        Console.WriteLine(e.PageSettings.Margins.Left)
        Console.WriteLine(e.PageSettings.Margins.Right)
        Console.WriteLine(e.PageSettings.Margins.Top)
        Console.WriteLine(e.PageSettings.Margins.Bottom)
        Console.WriteLine(e.MarginBounds.Width)
        Console.WriteLine(e.MarginBounds.Height)
        Console.WriteLine(PrintDocument1.OriginAtMargins)
        e.Graphics.DrawRectangle(New Pen(Color.Black, 1), New Rectangle(0, 0, e.MarginBounds.Width, e.MarginBounds.Height))

    End Sub

    Private Sub FrmSST4500_1_0_0J_main_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim pen As New Pen(frm_MainLine_color, 2)
        Dim fnt_16 As New Font("MS UI Gothic", 16, FontStyle.Bold)
        Dim main_form_line_path As New List(Of GraphicsPath)
        Dim path As New GraphicsPath
        Dim rect As Rectangle = New Rectangle(12, 112, 758, 160)
        Dim rect2 As Rectangle = New Rectangle(12, 305, 758, 160)
        Dim stringSize As SizeF
        Dim label_brush As Brush = New SolidBrush(frm_MainForm_fc)
        Const rect_width2 = 379
        Const radius = 10
        Const label_padding = 5

        e.Graphics.Clear(BackColor)

        Dim string_tmp As String
        string_tmp = "測定モード選択"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_16)

        path.StartFigure()
        path.AddArc(rect.Left, rect.Top, radius * 2, radius * 2, 180, 90)
        path.AddLine(rect.Left + radius, rect.Top, rect.Left + rect_width2 - stringSize.Width / 2 - label_padding, rect.Top)
        path.StartFigure()
        path.AddLine(rect.Left + rect_width2 + stringSize.Width / 2 + label_padding, rect.Top, rect.Right - radius, rect.Top)
        path.AddArc(rect.Right - radius * 2, rect.Top, radius * 2, radius * 2, 270, 90)
        path.AddLine(rect.Right, rect.Top + radius, rect.Right, rect.Bottom - radius)
        path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90)
        path.AddLine(rect.Right - radius, rect.Bottom, rect.Left + radius, rect.Bottom)
        path.AddArc(rect.Left, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90)
        path.AddLine(rect.Left, rect.Bottom - radius, rect.Left, rect.Top + radius)
        'main_form_line_path.Add(path)

        e.Graphics.DrawString(string_tmp, fnt_16, label_brush, rect.Left + rect_width2 - stringSize.Width / 2, 112 - stringSize.Height / 2)

        string_tmp = "管理者モード"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_16)

        path.StartFigure()
        path.AddArc(rect2.Left, rect2.Top, radius * 2, radius * 2, 180, 90)
        path.AddLine(rect2.Left + radius, rect2.Top, rect2.Left + rect_width2 - stringSize.Width / 2 - label_padding, rect2.Top)
        path.StartFigure()
        path.AddLine(rect2.Left + rect_width2 + stringSize.Width / 2 + label_padding, rect2.Top, rect2.Right - radius, rect2.Top)
        path.AddArc(rect2.Right - radius * 2, rect2.Top, radius * 2, radius * 2, 270, 90)
        path.AddLine(rect2.Right, rect2.Top + radius, rect2.Right, rect2.Bottom - radius)
        path.AddArc(rect2.Right - radius * 2, rect2.Bottom - radius * 2, radius * 2, radius * 2, 0, 90)
        path.AddLine(rect2.Right - radius, rect2.Bottom, rect2.Left + radius, rect2.Bottom)
        path.AddArc(rect2.Left, rect2.Bottom - radius * 2, radius * 2, radius * 2, 90, 90)
        path.AddLine(rect2.Left, rect2.Bottom - radius, rect2.Left, rect2.Top + radius)
        main_form_line_path.Add(path)

        e.Graphics.DrawString(string_tmp, fnt_16, label_brush, rect.Left + rect_width2 - stringSize.Width / 2, 305 - stringSize.Height / 2)


        For Each path_tmp As GraphicsPath In main_form_line_path
            e.Graphics.DrawPath(pen, path_tmp)
        Next

        pen.Dispose()
        fnt_16.Dispose()
        label_brush.Dispose()
        path.Dispose()
        e.Dispose()

    End Sub

    Private Sub 終了ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 終了ToolStripMenuItem.Click
        If FlgFTDLLerr = 0 Then
            UsbClose()
        End If
        Me.Close()
    End Sub

    Private Sub シングルシートToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles シングルシートToolStripMenuItem.Click
        singlesheet_run()
    End Sub

    Private Sub singlesheet_run()
        TimSplash.Enabled = False
        Me.Visible = False
        FrmSST4500_1_0_0J_meas.Visible = True

        FlgMainSplash = 0
        FlgInitSplash = 0
        FlgMainMeas = 1
        FlgProfile = 0
    End Sub

    Private Sub プロファイルToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles プロファイルToolStripMenuItem.Click
        profile_run()
    End Sub

    Private Sub カットシートToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles カットシートToolStripMenuItem.Click
        cutsheet_run()
    End Sub

    Private Sub 管理者ログインToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 管理者ログインToolStripMenuItem.Click
        admin_login()
    End Sub

    Private Sub パスワード変更ToolStripMenuItem_Click(sender As Object, e As EventArgs)
        FlgPasswdChg = 1
        FrmSST4500_1_0_0J_passchg.Visible = True
    End Sub

    Private Sub 設定ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles 設定ToolStripMenuItem1.Click
        FrmSST4500_1_0_0J_setting.Visible = True
    End Sub

    Private Sub SST4500についてToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SST4500についてToolStripMenuItem.Click
        FrmSST4500_1_0_0J_helpinfo.ShowDialog()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        FrmSST4500_1_0_0J_helpinfo.ShowDialog()
    End Sub

End Class

Public NotInheritable Class Simple3Des
    Private TripleDes As New TripleDESCryptoServiceProvider

    Private Function TruncateHash(
        ByVal key As String,
        ByVal length As Integer) As Byte()

        Dim sha1 As New SHA1CryptoServiceProvider

        Dim keyBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(key)
        Dim hash() As Byte = sha1.ComputeHash(keyBytes)

        ReDim Preserve hash(length - 1)
        Return hash
    End Function

    Sub New(ByVal key As String)
        TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
        TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)
    End Sub

    Public Function EncryptData(
        ByVal plaintext As String) As String

        Dim plaintextBytes() As Byte =
            System.Text.Encoding.Unicode.GetBytes(plaintext)

        Dim ms As New System.IO.MemoryStream

        Dim encStream As New CryptoStream(ms,
            TripleDes.CreateEncryptor(),
            System.Security.Cryptography.CryptoStreamMode.Write)

        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        Return Convert.ToBase64String(ms.ToArray)
    End Function

    Public Function DecryptData(
        ByVal encryptedtext As String) As String

        Dim encryptedBytes() As Byte =
            Convert.FromBase64String(encryptedtext)

        Dim ms As New System.IO.MemoryStream

        Dim decStream As New CryptoStream(ms,
            TripleDes.CreateDecryptor(),
            System.Security.Cryptography.CryptoStreamMode.Write)

        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    End Function
End Class
