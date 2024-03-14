Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Text
Imports System.Drawing.Printing
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports Microsoft.Office.Core
Imports System.Configuration
'Imports Microsoft.Office.Interop.Excel

Public Class FrmSST4500_1_0_0E_meas
    Const Rad = 3.141592654 / 180

    Dim Kt As Long = 0
    Dim Ku As Integer = 0
    Dim Sa As String = ""
    Dim fname As String = ""
    Dim result As DialogResult
    Dim _flgRx As Integer
    Dim Kp As Long
    'Dim Es As Object
    Dim result2 As Integer
    Dim Menu_AutoPrn As ToolStripMenuItem
    'Dim title_text As String
    Dim flgInitEnd As Integer = 0

    Private Sub FrmSST4500_1_0_0E_meas_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size
        Menu_AutoPrn = DirectCast(印刷ToolStripMenuItem, ToolStripMenuItem)

        Me.Text = My.Application.Info.ProductName & " Single Sheet (Ver:" & My.Application.Info.Version.ToString & ")"
        title_text1 = Me.Text
        Me.LblProductNameMeas.Text = My.Application.Info.ProductName

    End Sub

    Private Sub TimMeas_Tick(sender As Object, e As EventArgs) Handles TimMeas.Tick

        Select Case FlgMainMeas
            Case 1   '初期化処理
                If FlgAdmin <> 0 Then
                    '管理者モード
                    AdmVisible_onoff(True)
                Else
                    '通常モード
                    AdmVisible_onoff(False)
                End If

                '一旦タイマーを止める ※ファイル選択ダイアログが出続けてしまう為
                TimMeas.Enabled = False

                '測定仕様ファイルの選択
                result = LoadDefConstName(fname, True)

                If result = DialogResult.OK Then
                    StrConstFileName = fname

                    LoadConst(Me, title_text1)

                    If FlgMeasAutoPrn = True Then
                        Menu_AutoPrn.Checked = True
                    Else
                        Menu_AutoPrn.Checked = False
                    End If

                    ClsNoMeas()
                    ClsData()

                    FileNo = 0
                    MeasNo = 1

                    CmdMeas.Enabled = True
                    'CmdMeas.BackColor = SystemColors.Control
                    'CmdMeas.BackColor = frm_MeasButton_bc
                    'CmdMeas.ForeColor = frm_MeasButton_fc
                    'CmdMeas.FlatStyle = FlatStyle.System
                    CmdMeasButton_set(_rdy)
                    CmdMeas.Text = "測定開始"
                    測定開始ToolStripMenuItem.Enabled = True
                    測定開始ToolStripMenuItem.Text = "測定開始"

                    If FlgAdmin <> 0 Then
                        OldDataToolStripMenuItem.Enabled = True
                        CmdOldDataLoad.Enabled = True
                        LoadToolStripMenuItem.Enabled = True
                    End If

                    'CmdEtcOldMeasData.Enabled = True
                    ToolStripStatusLabel4.Text = "Ready "

                    ClsBakInfoMeas()

                    FlgMainMeas = 0
                    FlgHoldMeas = 0
                ElseIf result = DialogResult.Cancel Then
                    'FlgMainMeas = 1
                    Visible = False
                    FlgMainSplash = 0
                    FlgMainMeas = 0
                    FrmSST4500_1_0_0E_main.Visible = True
                End If

                timerCount1 = 0
                TimMeas.Enabled = True
                flgInitEnd = 1  '初期化完了

            Case 2
                '測定ボタンクリック
                If FlgHoldMeas = 0 Or SampleNo = 0 Then
                    FlgHoldMeas = 1
                    '測定画面が表示された最初のみ実行する
                    DataDate = Now.ToString("yy/MM/dd")
                    DataDate_cur = DataDate
                    FileDate = Now.ToString("yyMMdd")
                    If FlgDBF = 0 Then
                        DataTime = Now.ToString("HH:mm:ss")
                        DataTime_cur = DataTime
                        FileTime = Now.ToString("HHmmss")
                    Else
                        DataTime = Now.ToString("HH:mm")
                        DataTime_cur = DataTime
                        FileTime = Now.ToString("HHmm")
                    End If
                    'If FlgTest <> 0 Then
                    'TxtSetMogi()
                    'End If
                    MachineNo = TxtMachNoCur.Text
                    Sample = TxtSmplNamCur.Text
                    Mark = TxtMarkCur.Text

                    '自動保存ファイルの準備
                    OpenDataFile()
                    SaveDataTitle()
                End If

                ConditionDisable()

                DrawGraphCurData_clear()
                DrawGraphBakData_clear()
                DrawCalcCurData_init()
                DrawCalcBakData_init()
                DrawMeasCurData_init()
                DrawMeasBakData_init()
                GraphInitMeas()    'ClsDisplayMeasの代わり
                ClsBakInfoMeas()    'ClsFConditionMeasの代わり

                FlgPkcd = 0
                FlgDpmd = 0
                SampleNo += 1
                MeasDataMax = SampleNo
                MeasDataNo = SampleNo
                TxtMeasNumCur.Text = SampleNo
                DataPrcStr(1, SampleNo, 1) = TxtMachNoCur.Text
                DataPrcStr(1, SampleNo, 2) = TxtSmplNamCur.Text
                DataPrcStr(1, SampleNo, 3) = TxtMarkCur.Text
                DataPrcStr(1, SampleNo, 5) = Str(SampleNo)

                If FlgTest = 0 Then
                    FlgMainMeas = 3
                Else
                    timerCount1 = 0
                    FlgMainMeas = 4
                End If

            Case 3
                UsbOpen()

                FlgMainMeas = 0

                CmdMeas.Enabled = False
                測定開始ToolStripMenuItem.Enabled = False

                strWdata = "MES" & vbCr
                UsbWrite(strWdata)

                timerCount1 = 0
                FlgMainMeas = 301

            Case 301
                strRxdata = ""
                _flgRx = UsbRead(strRxdata)

                If _flgRx = 0 Then
                    If strRxdata = "MEAS" & vbCr Then
                        ToolStripStatusLabel4.Text = "Measuring "
                        'CmdMeas.BackColor = Color.Yellow
                        'CmdMeas.BackColor = frm_MeasuringButton_bc
                        'CmdMeas.ForeColor = frm_MeasuringButton_fc
                        'CmdMeas.FlatStyle = FlatStyle.Standard
                        CmdMeasButton_set(_mes)
                        CmdMeas.Text = "測定中"
                        測定開始ToolStripMenuItem.Text = "測定中"
                        timerCount1 = 0
                        FlgMainMeas = 4
                    Else
                        '基本的にこの状態にはならないはず
                        'ToolStripStatusLabel4.Text = "Measuring2 "
                        'CmdMeas.BackColor = Color.Yellow
                        'CmdMeas.FlatStyle = FlatStyle.Standard
                        'CmdMeas.Text = "測定中"
                        'timerCount1 = 0
                        'FlgMainMeas = 4
                        '---> 測定中止に画面を開きなおして測定開始をするとこの状態になる
                        'ただし空欄ではなく測定結果が受信される
                        'この時点で同期が外れているので無視してエラーに持っていく
                        FlgMainMeas = 99
                    End If
                Else
                    If timerCount1 >= cmd_timeout Then
                        'タイムアウトエラー
                        FlgMainMeas = 99
                    Else
                        timerCount1 += 1
                    End If
                End If

            Case 4
                CmdMeas.Enabled = False
                'CmdMeas.BackColor = Color.Yellow
                'CmdMeas.BackColor = frm_MeasuringButton_bc
                'CmdMeas.ForeColor = frm_MeasuringButton_fc
                'CmdMeas.FlatStyle = FlatStyle.Standard
                CmdMeasButton_set(_mes)
                CmdMeas.Text = "測定中"
                測定開始ToolStripMenuItem.Enabled = False
                測定開始ToolStripMenuItem.Text = "測定中"

                timerCount1 += 1

                If FlgTest = 0 And timerCount1 Mod 50 = 0 Then
                    ToolStripStatusLabel4.Text &= "o"
                ElseIf FlgTest <> 0 And timerCount1 Mod 5 = 0 Then
                    ToolStripStatusLabel4.Text &= "o"
                End If

                If FlgTest = 0 And timerCount1 >= 600 Then
                    timerCount1 = 0
                    FlgMainMeas = 5
                    'ElseIf FlgTest <> 0 And timerCount1 = 300 Then
                ElseIf FlgTest <> 0 And timerCount1 >= test_count1 Then
                    timerCount1 = 0
                    FlgMainMeas = 5
                End If

            Case 5
                timerCount1 += 1

                If FlgTest = 0 And timerCount1 Mod 20 = 0 Then
                    ToolStripStatusLabel4.Text &= "-"

                    strRxdata = ""
                    _flgRx = UsbRead(strRxdata)

                    If _flgRx = 0 Then
                        FlgMainMeas = 6
                        If strRxdata <> "" Then
                            'vbcrを削除する
                            strRxdata = Strings.Left(strRxdata, Len(strRxdata) - 1)
                            ToolStripStatusLabel4.Text = strRxdata
                        Else
                            '空欄だったらデータエラー
                            FlgMainMeas = 99
                        End If
                    Else
                        If timerCount1 >= timeout_time Then  '測定は140程度
                            'タイムアウトエラー
                            FlgMainMeas = 99
                        End If
                    End If
                ElseIf FlgTest <> 0 And timerCount1 Mod 2 = 0 Then
                    ToolStripStatusLabel4.Text &= "-"
                    'If timerCount1 = 100 Then
                    If timerCount1 >= test_count2 Then
                        FlgMainMeas = 6
                    End If
                End If

            Case 6
                KdData = 1
                ResolveData()

                DrawCalcData()
                DrawMeasData()
                DrawGraph()
                DrawAxisCur()
                SaveData()

                If FlgTest = 0 Then
                    UsbClose()
                End If

                FlgMainMeas = 0

                CmdMeas.Enabled = True
                'CmdMeas.BackColor = SystemColors.Control
                'CmdMeas.BackColor = frm_MeasButton_bc
                'CmdMeas.ForeColor = frm_MeasButton_fc
                'CmdMeas.FlatStyle = FlatStyle.System
                CmdMeasButton_set(_rdy)
                CmdMeas.Text = "測定開始"
                測定開始ToolStripMenuItem.Enabled = True
                測定開始ToolStripMenuItem.Text = "測定開始"

                ConditionEnable()
                ToolStripStatusLabel4.Text = "Ready "

                '印刷
                If FlgMeasAutoPrn = 1 Then
                    TimMeas.Enabled = False
                    PrintoutMeas()
                    TimMeas.Enabled = True
                End If

                FlgPkcd = 0
                FlgDpmd = 0

            Case 10
                '他の測定データボタン
                Kp = MeasDataMax
                Dim input_ret As String

                TimMeas.Enabled = False

                input_ret = InputBox("測定No.入力", "測定No.選択", Str(Kp))

                If input_ret = String.Empty Then
                    'たぶんキャンセル
                    'キャンセルなら何もしない
                ElseIf input_ret = "" Then
                    MessageBox.Show("測定No.を入力してください。",
                                    "入力値エラー",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                Else
                    If IsNumeric(input_ret) Then
                        SampleNo = input_ret

                        If SampleNo <= 0 Or SampleNo > Kp Then
                            MessageBox.Show("入力値に誤りがあります。",
                                            "入力値エラー",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation)
                        Else
                            If FlgAdmin = 0 Then
                                LblMeasNumCur_nom.Text = Str(SampleNo)
                            Else
                                LblMeasNumCur_adm.Text = Str(SampleNo)
                            End If
                            MeasDataNo = SampleNo

                            DrawGraphCurData_clear()
                            DrawCalcCurData_init()
                            DrawMeasCurData_init()
                            GraphInitMeas()

                            KdData = 1
                            SampleNo = MeasDataNo

                            DrawCalcData()
                            DrawMeasData()
                            DrawGraph()
                            DrawAxisCur()

                            'If MeasDataNo <> 0 Then
                            'KdData = 3
                            'SampleNo = FileDataNo
                            'DrawCalcData()
                            'DrawMeasData()
                            'DrawGraph()
                            'DrawAxisBak()
                            'End If
                        End If
                    Else
                        MessageBox.Show("数値で入力してください。",
                                        "入力値エラー",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                End If
                SampleNo = Kp
                FlgMainMeas = 0

                TimMeas.Enabled = True

            Case 20
                '測定条件が変わってクリアする
                MeasDataNo = 0
                FlgMainMeas = 0
                FlgHoldMeas = 0

                FlgPkcd = 0
                FlgDpmd = 0

                'SampleNo = 0
                'TxtMeasNumCur.Text = Str(SampleNo)
                ClsNoMeas()

                MeasDataMax = SampleNo
                MeasDataNo = SampleNo

                DrawGraphCurData_clear()
                DrawGraphBakData_clear()
                DrawCalcCurData_init()
                DrawCalcBakData_init()
                DrawMeasCurData_init()
                DrawMeasBakData_init()
                GraphInitMeas()    'ClsDisplayMeasの代わり
                ClsBakInfoMeas()    'ClsFConditionMeasの代わり

            Case 40
                '過去データ読込ボタン
                Kt = SampleNo
                Sa = StrFileName
                Ku = FileNumData

                TimMeas.Enabled = False

                result = LoadOldDataName(fname)

                If result = DialogResult.OK Then
                    StrFileName = fname

                    FileNo = 1

                    result2 = LoadData()

                    If result2 < 1 Then
                        If result2 = 0 Then
                            MessageBox.Show("データがありませんでした。",
                                            "ファイルエラー",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)
                        ElseIf result2 = -2 Then
                            MessageBox.Show("ファイルフォーマットが異なります。",
                                            "ファイルエラー",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)
                        ElseIf result2 = -1 Then
                            MessageBox.Show("データが破損しています。",
                                            "ファイルエラー",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)
                        End If
                        FlgMainMeas = 0
                    Else
                        'Input a Sample No.へ
                        FlgMainMeas = 401
                    End If
                ElseIf result = DialogResult.Cancel Then
                    FlgMainMeas = 0
                End If

                TimMeas.Enabled = True

            Case 401
                '過去データ読込ボタン続き
                Dim input_ret As String

                TimMeas.Enabled = False

                input_ret = InputBox("測定No.入力", "測定No.選択", Str(FileDataMax))
                '↑valするとキャンセルした時に0が返ってくる
                '  valしないと""になる

                'ここではキャンセルも空データも許さない
                If input_ret = "" Then
                    MessageBox.Show("測定No.を入力してください。",
                                    "入力値エラー",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                Else
                    If IsNumeric(input_ret) Then
                        SampleNo = input_ret

                        If SampleNo = 0 Or SampleNo > FileDataMax Then
                            MessageBox.Show("入力値に誤りがあります。",
                                            "入力値エラー",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation)
                        Else
                            WrtOldMeasInfo()
                            MakeDisplayData()
                            ConditionEnable()

                            If FlgHoldMeas = 0 Then
                                DrawGraphBakData_clear()
                                DrawCalcBakData_init()
                                DrawMeasBakData_init()
                                GraphInitMeas()

                                '測定していない状態でも
                                '過去データを重ね書きされるように
                                FlgHoldMeas = 1
                            End If
                            KdData = 3
                            DrawCalcData()
                            DrawMeasData()
                            DrawGraph()
                            DrawAxisBak()

                            SampleNo = Kt
                            StrFileName = Sa
                            FileNumData = Ku

                            FlgMainMeas = 0
                        End If
                    Else
                        MessageBox.Show("数値で入力してください。",
                                        "入力値エラー",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                End If
                TimMeas.Enabled = True

            Case 41
                '過去の他の測定データボタン
                Dim input_ret As String

                Kt = SampleNo
                Kp = FileDataMax

                TimMeas.Enabled = False

                input_ret = InputBox("測定No.入力", "測定No.選択", Str(Kp))

                If input_ret = String.Empty Then
                    'たぶんキャンセル
                    'キャンセルなら何もしない
                ElseIf input_ret = "" Then
                    MessageBox.Show("測定No.を入力してください。",
                                    "入力値エラー",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                Else
                    If IsNumeric(input_ret) = True Then
                        SampleNo = input_ret

                        If SampleNo = 0 Or SampleNo > Kp Then
                            MessageBox.Show("入力値に誤りがあります。",
                                            "入力値エラー",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation)
                        Else
                            FileDataNo = SampleNo

                            DrawGraphBakData_clear()
                            DrawCalcBakData_init()
                            DrawMeasBakData_init()
                            GraphInitMeas()

                            KdData = 3
                            SampleNo = FileDataNo
                            DrawCalcData()
                            DrawMeasData()
                            DrawGraph()
                            DrawAxisBak()
                        End If
                    Else
                        MessageBox.Show("数値で入力してください。",
                                        "入力値エラー",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                End If

                SampleNo = Kt
                FlgMainMeas = 0

                TimMeas.Enabled = True

            Case 90
                '終了ボタン

                TimMeas.Enabled = False

                If FlgTest = 0 Then
                    UsbClose()
                End If

                '測定仕様ファイルの保存処理
                If FlgConstChg = True Then
                    result = MessageBox.Show("測定仕様が変更されています。" & vbCrLf &
                                             "変更内容を保存しますか？" & vbCrLf &
                                             "はい : 上書き" & vbCrLf &
                                             "いいえ : 名前を付けて保存" & vbCrLf &
                                             "キャンセル : 保存しないで終了",
                                             "測定仕様変更確認",
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Information)
                    Select Case result
                        Case DialogResult.Yes
                            SaveConst(StrConstFilePath)
                        Case DialogResult.No
                            SaveConstMeas()
                        Case DialogResult.Cancel
                            'なにもしない
                    End Select

                End If

                frmClose()
                Visible = False
                timerCount1 = 0
                FlgMainMeas = 91

                TimMeas.Enabled = True

            Case 91
                '終了ボタン続き
                timerCount1 += 1
                If timerCount1 = 10 Then
                    TimMeas.Enabled = False

                    'CmdMeas.BackColor = SystemColors.Control
                    'CmdMeas.BackColor = frm_MeasButton_bc
                    'CmdMeas.ForeColor = frm_MeasButton_fc
                    CmdMeasButton_set(_rdy)
                    CmdMeas.Text = "測定開始"
                    測定開始ToolStripMenuItem.Text = "測定開始"

                    FrmSST4500_1_0_0E_main.Visible = True

                    FlgMainSplash = 11
                    FlgMainMeas = 0
                    flgInitEnd = 0
                End If

            Case 99
                ToolStripStatusLabel4.Text = "Received Data Error(Data Nothing or Timeout)"
                If FlgTest = 0 Then
                    UsbClose()
                End If
                FlgMainMeas = 0

        End Select
    End Sub

    Private Sub WrtOldMeasInfo()
        '過去の測定仕様にデータを展開
        '管理者モードのみ

        If FlgDBF = 0 Then
            'データモード通常
            TxtMachNoBak.Text = DataFileStr(FileNo, 1, 1)
            If DataFileStr(FileNo, 1, 4) = "" Then
                TxtSmplNamBak.Text = DataFileStr(FileNo, 1, 2)
            Else
                TxtSmplNamBak.Text = DataFileStr(FileNo, 1, 2) & "," &
                                     DataFileStr(FileNo, 1, 4)
            End If
            TxtMarkBak.Text = DataFileStr(FileNo, 1, 3)
            TxtMeasNumBak.Text = FileDataMax
        Else
            'データモード特殊1
            TxtMachNoBak.Text = DataFileStr(FileNo, 1, 1)
            TxtSmplNamBak.Text = DataFileStr(FileNo, 1, 2)
            TxtMarkBak.Text = DataFileStr(FileNo, 1, 3)
            '斤量は無視する
            TxtMeasNumBak.Text = FileDataMax
        End If
    End Sub

    Private Sub ClsNoMeas()
        SampleNo = 0
        Me.TxtMeasNumCur.Text = SampleNo
    End Sub

    Private Sub CmdQuitSinglesheet_Click(sender As Object, e As EventArgs) Handles CmdQuitSinglesheet.Click
        FlgMainMeas = 90
    End Sub

    Private Sub ChkMeasAutoPrn_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMeasAutoPrn.CheckedChanged
        If ChkMeasAutoPrn.Checked = True Then
            FlgMeasAutoPrn = 1
            If Menu_AutoPrn.Checked = False Then
                Menu_AutoPrn.Checked = True
                'FlgConstChg = True  '変更有の状態にセットする
            End If
        Else
            FlgMeasAutoPrn = 0
            If Menu_AutoPrn.Checked = True Then
                Menu_AutoPrn.Checked = False
                'FlgConstChg = True  '変更有の状態にセットする

            End If
        End If
        If flgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text1)
        End If
    End Sub

    Private Sub FrmSST4500_1_0_0E_meas_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            CmdMeas.Enabled = False
            CmdEtcMeasData.Enabled = False
            CmdOldDataLoad.Enabled = False
            CmdEtcOldMeasData.Enabled = False
            CmdMeasPrint.Enabled = False
            CmdMeasResultSave.Enabled = False

            GbMeasSpec.Enabled = True

            測定開始ToolStripMenuItem.Enabled = False
            他の測定データ選択ToolStripMenuItem1.Enabled = False
            OldDataToolStripMenuItem.Enabled = False
            LoadToolStripMenuItem.Enabled = False
            AnotherMeasDataSelToolStripMenuItem.Enabled = False
            手動印刷ToolStripMenuItem.Enabled = False
            保存ToolStripMenuItem1.Enabled = False
            MeasSpecToolStripMenuItem.Enabled = True
            印刷ToolStripMenuItem.Enabled = True

            TxtMachNoCur.Enabled = True
            TxtSmplNamCur.Enabled = True
            TxtMarkCur.Enabled = True

            ChkMeasAutoPrn.Enabled = True

            If FlgAdmin <> 0 Then
                TxtMachNoBak.Enabled = True
                TxtSmplNamBak.Enabled = True
                TxtMarkBak.Enabled = True
            End If

            TimMeas.Enabled = True

            DrawGraphCurData_clear()
            DrawGraphBakData_clear()
            DrawGraph_init()
            DrawCalcCurData_init()
            DrawMeasCurData_init()
            DrawCalcBakData_init()
            DrawMeasBakData_init()
            GraphInitMeas()

            timerCount1 = 0
            FileNumConst = 0
            FileNumData = 0

            meas_dbf_chg(FlgDBF)

            'FlgMainMeas = 1
        Else
            DrawGraphCurData_clear()
            DrawGraphBakData_clear()
            DrawGraph_init()
            DrawCalcCurData_init()
            DrawMeasCurData_init()
            DrawCalcBakData_init()
            DrawMeasBakData_init()
            GraphInitMeas()

            ClsCurInfoMeas()
            ClsBakInfoMeas()
        End If
    End Sub

    Private Sub CmdMeas_Click(sender As Object, e As EventArgs) Handles CmdMeas.Click
        Dim ret As DialogResult

        If FlgConstChg = True Then
            If FlgConstChg_MeasStart = False Then
                ret = MessageBox.Show("測定仕様が保存されていませんが、" & vbCrLf &
                      "測定を開始しますか？",
                      "測定開始確認",
                      MessageBoxButtons.YesNo,
                      MessageBoxIcon.Warning)
                If ret = vbYes Then
                    FlgConstChg_MeasStart = True
                    If FlgTest = 1 Then
                        FlgTest = 2
                    End If
                    FlgMainMeas = 2
                Else
                    FlgConstChg_MeasStart = False
                    Exit Sub
                End If
            Else
                If FlgTest = 1 Then
                    FlgTest = 2
                End If
                FlgMainMeas = 2
            End If
        Else
            If FlgTest = 1 Then
                FlgTest = 2
            End If
            FlgMainMeas = 2
        End If
    End Sub

    Private Sub GraphInitMeas()
        'ClsDisplayMeas()の代わり
        'WakCalData,WakMeasDataはTableLayoutで用意済み
        PictureBox1.CreateGraphics.Clear(BackColor)

        'If SampleNo = 0 And FlgHoldMeas = 0 Then
        'Exit Sub
        'End If

        DrawGraph_init()
        'DrawCalcData_init()
        'DrawMeasData_init()

        Dim path1 As New GraphicsPath
        path1.StartFigure()
        path1.AddLine(0, 0, 0, 450)
        path1.StartFigure()
        path1.AddLine(0, 0, 450, 0)
        path1.StartFigure()
        path1.AddLine(450, 0, 450, 450)
        path1.StartFigure()
        path1.AddLine(0, 450, 450, 450)
        meas_waku_path1.Add(path1)

        Dim path2 As New GraphicsPath
        path2.StartFigure()
        path2.AddLine(25, 225, 425, 225)
        path2.StartFigure()
        path2.AddLine(225, 20, 225, 430)
        meas_waku_path2.Add(path2)

        ytop_label = "MD=0 deg."
        ybtm_label = "180"
        xleft_label = "270"
        xright1_label = "CD"
        xright2_label = "=90 deg."

        PictureBox1.Refresh()
    End Sub

    Private Sub ClsCurInfoMeas()
        TxtMachNoCur.Text = ""
        TxtSmplNamCur.Text = ""
        TxtMarkCur.Text = ""
        TxtMeasNumCur.Text = ""
    End Sub

    Private Sub ClsBakInfoMeas()
        '過去データのテキストボックスをクリアする
        'ClsFConditionMeas()の代わり
        TxtMachNoBak.Text = ""
        TxtSmplNamBak.Text = ""
        TxtMarkBak.Text = ""
        TxtMeasNumBak.Text = ""
    End Sub

    Private Sub AdmVisible_onoff(ByVal sw As Boolean)
        CmdOldDataLoad.Visible = sw
        CmdOldDataLoad.Enabled = sw
        CmdClsGraph.Visible = sw
        CmdClsGraph.Enabled = sw
        LoadToolStripMenuItem.Enabled = sw
        CmdEtcOldMeasData.Visible = sw
        CmdEtcOldMeasData.Enabled = sw
        AnotherMeasDataSelToolStripMenuItem.Enabled = sw
        GroupBox5.Visible = sw
        OldDataToolStripMenuItem.Enabled = sw
        TblMeasInfo_adm.Visible = sw
        TblMeasInfo_nom.Visible = Not sw
        TblMeasData_adm.Visible = sw
        TblMeasData_nom.Visible = Not sw
        LblMeasSpecBak.Visible = sw
        TxtMachNoBak.Visible = sw
        TxtSmplNamBak.Visible = sw
        If FlgDBF = 1 Then
            TxtMarkBak.Visible = sw
        End If
        TxtMeasNumBak.Visible = sw
    End Sub

    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        Dim pen_waku_1 As New Pen(frm_MeasGraphWaku_color, 1)
        Dim pen_waku_2 As New Pen(frm_MeasGraphWaku_color, 2)
        Dim pen_graphold_1 As New Pen(frm_MeasOldData_color, 1)
        Dim pen_graphcur_1 As New Pen(frm_MeasCurData_color, 1)

        For Each path As GraphicsPath In meas_waku_path1
            e.Graphics.DrawPath(pen_waku_1, path)
        Next

        For Each path As GraphicsPath In meas_waku_path2
            e.Graphics.DrawPath(pen_waku_2, path)
        Next

        Dim fnt As New Font("MS UI Gothic", 9)
        Dim waku_brush As Brush = New SolidBrush(frm_MeasGraphWaku_color)
        e.Graphics.DrawString(ytop_label, fnt, waku_brush, 204, 5)
        e.Graphics.DrawString(ybtm_label, fnt, waku_brush, 213, 435)
        e.Graphics.DrawString(xleft_label, fnt, waku_brush, 2, 219)
        e.Graphics.DrawString(xright1_label, fnt, waku_brush, 427, 213)
        e.Graphics.DrawString(xright2_label, fnt, waku_brush, 427, 227)

        For Each path As GraphicsPath In axis_path_cur
            e.Graphics.DrawPath(pen_graphcur_1, path)
        Next

        For Each path As GraphicsPath In axis_path_bak
            e.Graphics.DrawPath(pen_graphold_1, path)
        Next

        For Each path As GraphicsPath In square_path1
            e.Graphics.DrawPath(pen_graphcur_1, path)
        Next

        For Each path As GraphicsPath In square_path2
            e.Graphics.DrawPath(pen_graphcur_1, path)
        Next

        For Each path As GraphicsPath In triangle_path1
            e.Graphics.DrawPath(pen_graphold_1, path)
        Next

        For Each path As GraphicsPath In triangle_path2
            e.Graphics.DrawPath(pen_graphold_1, path)
        Next

    End Sub

    Private Sub DrawCalcCurData_init()
        LblMeasNumCur_nom.Text = ""
        LblAnglPeakCur_nom.Text = ""
        LblAnglDeepCur_nom.Text = ""
        LblratioMDCDCur_nom.Text = ""
        LblratioPKDPCur_nom.Text = ""
        LblSpdMDCur_nom.Text = ""
        LblSpdCDCur_nom.Text = ""
        LblSpdPeakCur_nom.Text = ""
        LblSpdDeepCur_nom.Text = ""
        LblTSIMDCur_nom.Text = ""
        LblTSICDCur_nom.Text = ""

        LblMeasNumCur_adm.Text = ""
        LblAnglPeakCur_adm.Text = ""
        LblAnglDeepCur_adm.Text = ""
        LblratioMDCDCur_adm.Text = ""
        LblratioPKDPCur_adm.Text = ""
        LblSpdMDCur_adm.Text = ""
        LblSpdCDCur_adm.Text = ""
        LblSpdPeakCur_adm.Text = ""
        LblSpdDeepCur_adm.Text = ""
        LblTSIMDCur_adm.Text = ""
        LblTSICDCur_adm.Text = ""
    End Sub

    Private Sub DrawCalcBakData_init()
        LblMeasNumBak_adm.Text = ""
        LblAnglPeakBak_adm.Text = ""
        LblAnglDeepBak_adm.Text = ""
        LblratioMDCDBak_adm.Text = ""
        LblratioPKDPBak_adm.Text = ""
        LblSpdMDBak_adm.Text = ""
        LblSpdCDBak_adm.Text = ""
        LblSpdPeakBak_adm.Text = ""
        LblSpdDeepBak_adm.Text = ""
        LblTSIMDBak_adm.Text = ""
        LblTSICDBak_adm.Text = ""
    End Sub

    Private Sub DrawMeasCurData_init()
        LblMeasDatCur1_nom.Text = ""
        LblMeasDatCur2_nom.Text = ""
        LblMeasDatCur3_nom.Text = ""
        LblMeasDatCur4_nom.Text = ""
        LblMeasDatCur5_nom.Text = ""
        LblMeasDatCur6_nom.Text = ""
        LblMeasDatCur7_nom.Text = ""
        LblMeasDatCur8_nom.Text = ""
        LblMeasDatCur9_nom.Text = ""
        LblMeasDatCur10_nom.Text = ""
        LblMeasDatCur11_nom.Text = ""
        LblMeasDatCur12_nom.Text = ""
        LblMeasDatCur13_nom.Text = ""
        LblMeasDatCur14_nom.Text = ""
        LblMeasDatCur15_nom.Text = ""
        LblMeasDatCur16_nom.Text = ""

        LblMeasDatCur1_adm.Text = ""
        LblMeasDatCur2_adm.Text = ""
        LblMeasDatCur3_adm.Text = ""
        LblMeasDatCur4_adm.Text = ""
        LblMeasDatCur5_adm.Text = ""
        LblMeasDatCur6_adm.Text = ""
        LblMeasDatCur7_adm.Text = ""
        LblMeasDatCur8_adm.Text = ""
        LblMeasDatCur9_adm.Text = ""
        LblMeasDatCur10_adm.Text = ""
        LblMeasDatCur11_adm.Text = ""
        LblMeasDatCur12_adm.Text = ""
        LblMeasDatCur13_adm.Text = ""
        LblMeasDatCur14_adm.Text = ""
        LblMeasDatCur15_adm.Text = ""
        LblMeasDatCur16_adm.Text = ""
    End Sub

    Private Sub DrawMeasBakData_init()
        LblMeasDatBak1_adm.Text = ""
        LblMeasDatBak2_adm.Text = ""
        LblMeasDatBak3_adm.Text = ""
        LblMeasDatBak4_adm.Text = ""
        LblMeasDatBak5_adm.Text = ""
        LblMeasDatBak6_adm.Text = ""
        LblMeasDatBak7_adm.Text = ""
        LblMeasDatBak8_adm.Text = ""
        LblMeasDatBak9_adm.Text = ""
        LblMeasDatBak10_adm.Text = ""
        LblMeasDatBak11_adm.Text = ""
        LblMeasDatBak12_adm.Text = ""
        LblMeasDatBak13_adm.Text = ""
        LblMeasDatBak14_adm.Text = ""
        LblMeasDatBak15_adm.Text = ""
        LblMeasDatBak16_adm.Text = ""
    End Sub

    Private Sub DrawGraph_init()
        meas_waku_path1.Clear()
        meas_waku_path2.Clear()
        'axis_path.Clear()
        'square_path1.Clear()
        'square_path2.Clear()
        'triangle_path1.Clear()
        'triangle_path2.Clear()
        ytop_label = ""
        ybtm_label = ""
        xleft_label = ""
        xright1_label = ""
        xright2_label = ""
    End Sub

    Private Sub DrawGraphCurData_clear()
        axis_path_cur.Clear()
        square_path1.Clear()
        square_path2.Clear()
    End Sub

    Private Sub DrawGraphBakData_clear()
        axis_path_bak.Clear()
        triangle_path1.Clear()
        triangle_path2.Clear()
    End Sub


    Private Sub DrawGraph()
        'square_path1.Clear()
        'Square_path2.Clear()
        Dim DataMax As Single
        Dim DataMin As Single
        Dim Px As Single
        Dim Py As Single
        Dim N As Integer
        Dim DataK As Single

        If SampleNo = 0 Then
            Exit Sub
        End If

        DataMax = 0
        DataMin = 9999

        For N = 3 To 18
            If DataPrcNum(KdData, SampleNo, N) > DataMax Then
                DataMax = DataPrcNum(KdData, SampleNo, N)
            End If
            If DataPrcNum(KdData, SampleNo, N) < DataMin Then
                DataMin = DataPrcNum(KdData, SampleNo, N)
            End If
        Next

        For N = 0 To 7
            DataK = DataPrcNum(KdData, SampleNo, N + 11)
            If DataK = 0 Then
                DataK = 10
            End If

            Py = 225 - (180 * Math.Cos((N + 8) * 11.25 * Rad)) * (DataK / DataMax)
            Px = 225 + (180 * Math.Sin((N + 8) * 11.25 * Rad)) * (DataK / DataMax)

            If KdData = 1 Then
                DrawSquare(Px, Py)
            Else
                DrawTriangle(Px, Py)
            End If

            Py = 225 + (180 * Math.Cos((N + 8) * 11.25 * Rad)) * (DataK / DataMax)
            Px = 225 - (180 * Math.Sin((N + 8) * 11.25 * Rad)) * (DataK / DataMax)

            If KdData = 1 Then
                DrawSquare(Px, Py)
            Else
                DrawTriangle(Px, Py)
            End If

            DataK = DataPrcNum(KdData, SampleNo, N + 3)
            If DataK = 0 Then
                DataK = 10
            End If

            Py = 225 - (180 * Math.Cos(N * 11.25 * Rad)) * (DataK / DataMax)
            Px = 225 + (180 * Math.Sin(N * 11.25 * Rad)) * (DataK / DataMax)
            If KdData = 1 Then
                DrawSquare(Px, Py)
            Else
                DrawTriangle(Px, Py)
            End If

            Py = 225 + (180 * Math.Cos(N * 11.25 * Rad)) * (DataK / DataMax)
            Px = 225 - (180 * Math.Sin(N * 11.25 * Rad)) * (DataK / DataMax)
            If KdData = 1 Then
                DrawSquare(Px, Py)
            Else
                DrawTriangle(Px, Py)
            End If
        Next
    End Sub

    Private Sub DrawSquare(Px As Single, Py As Single)
        Const rect_size = 4

        Dim path As New GraphicsPath

        path.StartFigure()
        path.AddRectangle(New Rectangle(Px - rect_size, Py - rect_size, rect_size * 2, rect_size * 2))
        square_path1.Add(path)

        path.StartFigure()
        path.AddRectangle(New Rectangle(Px, Py, 1, 1))
        square_path2.Add(path)

        PictureBox1.Refresh()

        'path.Dispose()
    End Sub

    Private Sub DrawTriangle(Px As Single, Py As Single)
        Const tri_size_v = 4
        Const tri_size_h = 5
        Dim path As New GraphicsPath

        path.StartFigure()
        path.AddLine(Px - tri_size_h, Py + tri_size_v, Px + tri_size_h, Py + tri_size_v)
        path.AddLine(Px + tri_size_h, Py + tri_size_v, Px, Py - tri_size_v)
        path.AddLine(Px, Py - tri_size_v, Px - tri_size_h, Py + tri_size_h)
        triangle_path1.Add(path)

        path.StartFigure()
        path.AddRectangle(New Rectangle(Px, Py, 1, 1))
        triangle_path2.Add(path)

        PictureBox1.Refresh()

        'path.Dispose()
    End Sub

    Private Sub DrawAxisCur()
        Dim MdX As Single
        Dim MdY As Single
        Dim CdX As Single
        Dim CdY As Single
        Dim DataPeak As Single
        Dim DataDeep As Single
        Dim Ds As String
        Dim Ds_1 As String

        Dim path As New GraphicsPath
        axis_path_cur.Clear()

        If SampleNo = 0 Then
            Exit Sub

        End If

        If KdData = 1 Then

        End If

        Ds = DataPrcStr(KdData, SampleNo, 9)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            DataPeak = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            DataPeak = Val(Ds)
        End If
        MdX = 200 * Math.Sin(DataPeak * Rad)
        MdY = 200 * Math.Cos(DataPeak * Rad)

        path.StartFigure()
        path.AddLine(225 + MdX, 225 - MdY, 225 - MdX, 225 + MdY)

        Ds = DataPrcStr(KdData, SampleNo, 8)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            DataDeep = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            DataDeep = Val(Ds)
        End If
        CdX = 200 * Math.Cos(DataDeep * Rad)
        CdY = 200 * Math.Sin(DataDeep * Rad)

        path.StartFigure()
        path.AddLine(225 - CdX, 225 - CdY, 225 + CdX, 225 + CdY)

        axis_path_cur.Add(path)

        PictureBox1.Refresh()

        'path.Dispose()
    End Sub

    Private Sub DrawAxisBak()
        Dim MdX As Single
        Dim MdY As Single
        Dim CdX As Single
        Dim CdY As Single
        Dim DataPeak As Single
        Dim DataDeep As Single
        Dim Ds As String
        Dim Ds_1 As String

        Dim path As New GraphicsPath
        axis_path_bak.Clear()

        If SampleNo = 0 Then
            Exit Sub

        End If

        If KdData = 1 Then

        End If

        Ds = DataPrcStr(KdData, SampleNo, 9)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            DataPeak = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            DataPeak = Val(Ds)
        End If
        MdX = 200 * Math.Sin(DataPeak * Rad)
        MdY = 200 * Math.Cos(DataPeak * Rad)

        path.StartFigure()
        path.AddLine(225 + MdX, 225 - MdY, 225 - MdX, 225 + MdY)

        Ds = DataPrcStr(KdData, SampleNo, 8)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            DataDeep = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            DataDeep = Val(Ds)
        End If
        CdX = 200 * Math.Cos(DataDeep * Rad)
        CdY = 200 * Math.Sin(DataDeep * Rad)

        path.StartFigure()
        path.AddLine(225 - CdX, 225 - CdY, 225 + CdX, 225 + CdY)

        axis_path_bak.Add(path)

        PictureBox1.Refresh()

        'path.Dispose()
    End Sub

    Private Sub DrawCalcData()
        Dim DataK1 As Single
        Dim DataK2 As Single
        Dim DataK3 As Single
        Dim DataK4 As Single
        Dim Ds1 As String
        Dim Ds2 As String
        Dim Ds1_1 As String
        Dim Ds2_1 As String

        If SampleNo = 0 Then
            Exit Sub
        End If

        Ds1 = DataPrcStr(KdData, SampleNo, 9)
        Ds1_1 = Strings.Left(Ds1, 1)
        Ds2 = DataPrcStr(KdData, SampleNo, 8)
        Ds2_1 = Strings.Left(Ds2, 1)
        DataK1 = Val(DataPrcStr(KdData, SampleNo, 10))
        DataK2 = Val(DataPrcStr(KdData, SampleNo, 11))
        DataK3 = DataPrcNum(KdData, SampleNo, 3) ^ 2
        DataK4 = DataPrcNum(KdData, SampleNo, 11) ^ 2

        If KdData = 1 Then
            If FlgAdmin = 0 Then
                LblMeasNumCur_nom.Text = SampleNo
                If Ds1_1 = "C" Or Ds1_1 = "M" Then
                    LblAnglPeakCur_nom.Text = Format(Val(Strings.Right(Ds1, Len(Ds1) - 2)), "0.0")
                Else
                    LblAnglPeakCur_nom.Text = Format(Val(Ds1), "0.0")
                End If
                If Ds2_1 = "C" Or Ds2_1 = "M" Then
                    LblAnglDeepCur_nom.Text = Format(Val(Strings.Right(Ds2, Len(Ds2) - 2)), "0.0")
                Else
                    LblAnglDeepCur_nom.Text = Format(Val(Ds2), "0.0")
                End If
                LblratioMDCDCur_nom.Text = Format(DataK1, "0.00")
                LblratioPKDPCur_nom.Text = Format(DataK2, "0.00")
                LblSpdMDCur_nom.Text = Format(DataPrcNum(KdData, SampleNo, 3), "0.00")
                LblSpdCDCur_nom.Text = Format(DataPrcNum(KdData, SampleNo, 11), "0.00")
                LblSpdPeakCur_nom.Text = Format(DataPrcNum(KdData, SampleNo, 2), "0.00")
                LblSpdDeepCur_nom.Text = Format(DataPrcNum(KdData, SampleNo, 1), "0.00")
                LblTSIMDCur_nom.Text = Format(DataK3, "0.00")
                LblTSICDCur_nom.Text = Format(DataK4, "0.00")
            Else
                LblMeasNumCur_adm.Text = SampleNo
                If Ds1_1 = "C" Or Ds1_1 = "M" Then
                    LblAnglPeakCur_adm.Text = Format(Val(Strings.Right(Ds1, Len(Ds1) - 2)), "0.0")
                Else
                    LblAnglPeakCur_adm.Text = Format(Val(Ds1), "0.0")
                End If
                If Ds2_1 = "C" Or Ds2_1 = "M" Then
                    LblAnglDeepCur_adm.Text = Format(Val(Strings.Right(Ds2, Len(Ds2) - 2)), "0.0")
                Else
                    LblAnglDeepCur_adm.Text = Format(Val(Ds2), "0.0")
                End If
                LblratioMDCDCur_adm.Text = Format(DataK1, "0.00")
                LblratioPKDPCur_adm.Text = Format(DataK2, "0.00")
                LblSpdMDCur_adm.Text = Format(DataPrcNum(KdData, SampleNo, 3), "0.00")
                LblSpdCDCur_adm.Text = Format(DataPrcNum(KdData, SampleNo, 11), "0.00")
                LblSpdPeakCur_adm.Text = Format(DataPrcNum(KdData, SampleNo, 2), "0.00")
                LblSpdDeepCur_adm.Text = Format(DataPrcNum(KdData, SampleNo, 1), "0.00")
                LblTSIMDCur_adm.Text = Format(DataK3, "0.00")
                LblTSICDCur_adm.Text = Format(DataK4, "0.00")
            End If
        Else
            LblMeasNumBak_adm.Text = SampleNo
            If Ds1_1 = "C" Or Ds1_1 = "M" Then
                LblAnglPeakBak_adm.Text = Format(Val(Strings.Right(Ds1, Len(Ds1) - 2)), "0.0")
            Else
                LblAnglPeakBak_adm.Text = Format(Val(Ds1), "0.0")
            End If
            If Ds2_1 = "C" Or Ds2_1 = "M" Then
                LblAnglDeepBak_adm.Text = Format(Val(Strings.Right(Ds2, Len(Ds2) - 2)), "0.0")
            Else
                LblAnglDeepBak_adm.Text = Format(Val(Ds2), "0.0")
            End If
            LblratioMDCDBak_adm.Text = Format(DataK1, "0.00")
            LblratioPKDPBak_adm.Text = Format(DataK2, "0.00")
            LblSpdMDBak_adm.Text = Format(DataPrcNum(KdData, SampleNo, 3), "0.00")
            LblSpdCDBak_adm.Text = Format(DataPrcNum(KdData, SampleNo, 11), "0.00")
            LblSpdPeakBak_adm.Text = Format(DataPrcNum(KdData, SampleNo, 2), "0.00")
            LblSpdDeepBak_adm.Text = Format(DataPrcNum(KdData, SampleNo, 1), "0.00")
            LblTSIMDBak_adm.Text = Format(DataK3, "0.00")
            LblTSICDBak_adm.Text = Format(DataK4, "0.00")
        End If

    End Sub

    Private Sub DrawMeasData()
        If SampleNo = 0 Then
            Exit Sub
        End If

        If KdData = 1 Then
            If FlgAdmin = 0 Then
                LblMeasDatCur1_nom.Text = Format(DataPrcNum(KdData, SampleNo, 3), "0.00")
                LblMeasDatCur2_nom.Text = Format(DataPrcNum(KdData, SampleNo, 4), "0.00")
                LblMeasDatCur3_nom.Text = Format(DataPrcNum(KdData, SampleNo, 5), "0.00")
                LblMeasDatCur4_nom.Text = Format(DataPrcNum(KdData, SampleNo, 6), "0.00")
                LblMeasDatCur5_nom.Text = Format(DataPrcNum(KdData, SampleNo, 7), "0.00")
                LblMeasDatCur6_nom.Text = Format(DataPrcNum(KdData, SampleNo, 8), "0.00")
                LblMeasDatCur7_nom.Text = Format(DataPrcNum(KdData, SampleNo, 9), "0.00")
                LblMeasDatCur8_nom.Text = Format(DataPrcNum(KdData, SampleNo, 10), "0.00")
                LblMeasDatCur9_nom.Text = Format(DataPrcNum(KdData, SampleNo, 11), "0.00")
                LblMeasDatCur10_nom.Text = Format(DataPrcNum(KdData, SampleNo, 12), "0.00")
                LblMeasDatCur11_nom.Text = Format(DataPrcNum(KdData, SampleNo, 13), "0.00")
                LblMeasDatCur12_nom.Text = Format(DataPrcNum(KdData, SampleNo, 14), "0.00")
                LblMeasDatCur13_nom.Text = Format(DataPrcNum(KdData, SampleNo, 15), "0.00")
                LblMeasDatCur14_nom.Text = Format(DataPrcNum(KdData, SampleNo, 16), "0.00")
                LblMeasDatCur15_nom.Text = Format(DataPrcNum(KdData, SampleNo, 17), "0.00")
                LblMeasDatCur16_nom.Text = Format(DataPrcNum(KdData, SampleNo, 18), "0.00")
            Else
                LblMeasDatCur1_adm.Text = Format(DataPrcNum(KdData, SampleNo, 3), "0.00")
                LblMeasDatCur2_adm.Text = Format(DataPrcNum(KdData, SampleNo, 4), "0.00")
                LblMeasDatCur3_adm.Text = Format(DataPrcNum(KdData, SampleNo, 5), "0.00")
                LblMeasDatCur4_adm.Text = Format(DataPrcNum(KdData, SampleNo, 6), "0.00")
                LblMeasDatCur5_adm.Text = Format(DataPrcNum(KdData, SampleNo, 7), "0.00")
                LblMeasDatCur6_adm.Text = Format(DataPrcNum(KdData, SampleNo, 8), "0.00")
                LblMeasDatCur7_adm.Text = Format(DataPrcNum(KdData, SampleNo, 9), "0.00")
                LblMeasDatCur8_adm.Text = Format(DataPrcNum(KdData, SampleNo, 10), "0.00")
                LblMeasDatCur9_adm.Text = Format(DataPrcNum(KdData, SampleNo, 11), "0.00")
                LblMeasDatCur10_adm.Text = Format(DataPrcNum(KdData, SampleNo, 12), "0.00")
                LblMeasDatCur11_adm.Text = Format(DataPrcNum(KdData, SampleNo, 13), "0.00")
                LblMeasDatCur12_adm.Text = Format(DataPrcNum(KdData, SampleNo, 14), "0.00")
                LblMeasDatCur13_adm.Text = Format(DataPrcNum(KdData, SampleNo, 15), "0.00")
                LblMeasDatCur14_adm.Text = Format(DataPrcNum(KdData, SampleNo, 16), "0.00")
                LblMeasDatCur15_adm.Text = Format(DataPrcNum(KdData, SampleNo, 17), "0.00")
                LblMeasDatCur16_adm.Text = Format(DataPrcNum(KdData, SampleNo, 18), "0.00")
            End If
        Else
            LblMeasDatBak1_adm.Text = Format(DataPrcNum(KdData, SampleNo, 3), "0.00")
            LblMeasDatBak2_adm.Text = Format(DataPrcNum(KdData, SampleNo, 4), "0.00")
            LblMeasDatBak3_adm.Text = Format(DataPrcNum(KdData, SampleNo, 5), "0.00")
            LblMeasDatBak4_adm.Text = Format(DataPrcNum(KdData, SampleNo, 6), "0.00")
            LblMeasDatBak5_adm.Text = Format(DataPrcNum(KdData, SampleNo, 7), "0.00")
            LblMeasDatBak6_adm.Text = Format(DataPrcNum(KdData, SampleNo, 8), "0.00")
            LblMeasDatBak7_adm.Text = Format(DataPrcNum(KdData, SampleNo, 9), "0.00")
            LblMeasDatBak8_adm.Text = Format(DataPrcNum(KdData, SampleNo, 10), "0.00")
            LblMeasDatBak9_adm.Text = Format(DataPrcNum(KdData, SampleNo, 11), "0.00")
            LblMeasDatBak10_adm.Text = Format(DataPrcNum(KdData, SampleNo, 12), "0.00")
            LblMeasDatBak11_adm.Text = Format(DataPrcNum(KdData, SampleNo, 13), "0.00")
            LblMeasDatBak12_adm.Text = Format(DataPrcNum(KdData, SampleNo, 14), "0.00")
            LblMeasDatBak13_adm.Text = Format(DataPrcNum(KdData, SampleNo, 15), "0.00")
            LblMeasDatBak14_adm.Text = Format(DataPrcNum(KdData, SampleNo, 16), "0.00")
            LblMeasDatBak15_adm.Text = Format(DataPrcNum(KdData, SampleNo, 17), "0.00")
            LblMeasDatBak16_adm.Text = Format(DataPrcNum(KdData, SampleNo, 18), "0.00")

        End If

    End Sub

    Private Sub SaveConstMeas()
        'ソフト起動時に実行済み
        'Dim curdir As String
        'curdir = Directory.GetCurrentDirectory
        Dim Ret As DialogResult
        Dim FilePath As String = ""
        Dim sample_tmp As String()
        Dim filter_tmp As String
        Dim chk_filename As String
        Dim chk_filehead As String
        Dim sample_tmp_len As Integer
        Dim _points As Integer

        MachineNo = TxtMachNoCur.Text
        If FlgDBF = 0 Then
            '測定データフォーマット　通常
            'データが3つの場合、NAC03までのフォーマット
            'データが2つの場合、NAC04以降のフォーマット
            sample_tmp = Split(TxtSmplNamCur.Text, ",")
            sample_tmp_len = UBound(sample_tmp)
            If sample_tmp_len = 2 Then
                For i = 0 To sample_tmp_len
                    If i = 0 Then
                        Sample = sample_tmp(i)
                    ElseIf i = 1 Then
                        Mark = sample_tmp(i)
                    ElseIf i = 2 Then
                        BW = sample_tmp(i)
                    End If
                Next
            Else
                For i = 0 To sample_tmp_len
                    If i = 0 Then
                        Sample = sample_tmp(i)
                    ElseIf i = 1 Then
                        BW = sample_tmp(i)
                    End If
                Next
                Mark = TxtMarkCur.Text
            End If
        Else
            '測定データフォーマット　特殊1
            Sample = TxtSmplNamCur.Text
            Mark = TxtMarkCur.Text
            '斤量は無視する
        End If

        If ChkMeasAutoPrn.Checked = True Then
            FlgMeasAutoPrn = 1
        Else
            FlgMeasAutoPrn = 0
        End If

        Select Case FlgProfile
            Case 0
                filter_tmp = "Constant File(SG*.cns)|SG*.cns"
                StrFileName = "SG_0_" & Trim(MachineNo) & "_" & Trim(Sample) & ".cns"
                chk_filehead = "SG"
            Case 1
                If FlgPitchExp = 0 Then
                    _points = Trim(Str(Points))
                Else
                    _points = UBound(PchExp_PchData) + 2
                End If
                filter_tmp = "Constant File(PF*.cns)|PF*.cns"
                StrFileName = "PF_" & _points & "_" & Trim(MachineNo) & "_" & Trim(Sample) & ".cns"
                chk_filehead = "PF"
            Case 2
                filter_tmp = "Constant File(CT*.cns)|CT*.cns"
                StrFileName = "CT_" & Trim(Str(Points)) & "_" & Trim(MachineNo) & "_" & Trim(Sample) & ".cns"
                chk_filehead = "CT"
            Case 3
                filter_tmp = "Constant File(LG*.cns)|LG*.cns"
                StrFileName = "LG_0_" & Trim(MachineNo) & "_" & Trim(Sample) & ".cns"
                chk_filehead = "LG"
            Case Else
                filter_tmp = "Constant File(SG*.cns)|SG*.cns"
                StrFileName = "SG_0_" & Trim(MachineNo) & "_" & Trim(Sample) & ".cns"
                chk_filehead = "SG"
        End Select

        Using dialog As New SaveFileDialog
            With dialog
                .InitialDirectory = cur_dir & DEF_CONST_FILE_FLD
                .Title = "測定仕様ファイルの保存"
                .Filter = filter_tmp
                .FileName = StrFileName

                Ret = .ShowDialog

                If Ret = DialogResult.OK Then
                    FilePath = .FileName

                    chk_filename = Strings.Left(Path.GetFileName(FilePath), 2)
                    If chk_filename <> chk_filehead Then
                        MessageBox.Show("ファイル名の先頭は、必ず「" & chk_filehead & "」として下さい。" & vbCrLf &
                                        "一旦保存処理を終了します。",
                                        "ファイル名エラー",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    StrConstFileName = Path.GetFileName(FilePath)
                    StrConstFilePath = FilePath
                    Using sw As New StreamWriter(FilePath, False, Encoding.UTF8)
                        If FlgProfile = 1 Then
                            sw.WriteLine(MachineNo & "," & Sample & "," &
                                         Mark & "," & BW & "," &
                                         DataDate & "," & DataTime & "," &
                                         FlgProfile & "," & Length & "," &
                                         Pitch & "," & Points & "," &
                                         FlgInch & "," & FlgPrfDisplay & "," &
                                         FlgMeasAutoPrn & "," & FlgPrfAutoPrn & "," &
                                         FlgPrfPrint & "," & FlgAlternate & "," &
                                         FlgVelocityRange & "," & FlgAngleRange & "," &
                                         FlgPkCenterAngle & "," & FlgDpCenterAngle & "," &
                                         FlgPitchExp & "," & PchExpSettingFile_FullPath)
                        Else
                            sw.WriteLine(MachineNo & "," & Sample & "," &
                                         Mark & "," & BW & "," &
                                         DataDate & "," & DataTime & "," &
                                         FlgProfile & "," & Length & "," &
                                         Pitch & "," & Points & "," &
                                         FlgInch & "," & FlgPrfDisplay & "," &
                                         FlgMeasAutoPrn & "," & FlgPrfAutoPrn & "," &
                                         FlgPrfPrint & "," & FlgAlternate & "," &
                                         FlgVelocityRange & "," & FlgAngleRange & "," &
                                         FlgPkCenterAngle & "," & FlgDpCenterAngle)
                        End If
                    End Using

                    Dim _filename2 As String
                    _filename2 = Path.GetFileNameWithoutExtension(StrConstFileName)
                    Me.Text = title_text1 & " (" & _filename2 & ")"
                    FlgConstChg = False '変更無し状態に初期化
                    FlgConstChg_MeasStart = False
                End If
            End With
        End Using
    End Sub

    Private Sub CmdMeasSpecSel_Click(sender As Object, e As EventArgs) Handles CmdMeasSpecSel.Click
        SelConstMeas()
    End Sub

    Private Sub SelConstMeas()
        Dim result As DialogResult
        Dim fname As String = ""

        result = LoadDefConstName(fname, False)

        If result = DialogResult.OK Then
            StrConstFileName = fname

            flgInitEnd = 0

            LoadConst(Me, title_text1)

            flgInitEnd = 1

            'ClsNoMeas()    'FlgMainMeas = 20 で実行される
            ClsData()

            FlgMainMeas = 20
        End If
    End Sub

    Private Sub CmdEtcMeasData_Click(sender As Object, e As EventArgs) Handles CmdEtcMeasData.Click
        FlgMainMeas = 10
    End Sub

    Private Sub CmdOldDataLoad_Click(sender As Object, e As EventArgs) Handles CmdOldDataLoad.Click
        FlgMainMeas = 40
    End Sub

    Private Sub CmdEtcOldMeasData_Click(sender As Object, e As EventArgs) Handles CmdEtcOldMeasData.Click
        FlgMainMeas = 41
    End Sub

    Private Sub CmdMeasSpecSave_Click(sender As Object, e As EventArgs) Handles CmdMeasSpecSave.Click
        SaveConstMeas()
    End Sub

    Private Sub TxtMachNoCur_TextChanged(sender As Object, e As EventArgs) Handles TxtMachNoCur.TextChanged
        MachineNo = TxtMachNoCur.Text
        'FlgConstChg = True  '変更有の状態にセットする
        If flgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text1)
        End If
        FlgMainMeas = 20
    End Sub

    Private Sub TxtSmplNamCur_TextChanged(sender As Object, e As EventArgs) Handles TxtSmplNamCur.TextChanged
        Sample = TxtSmplNamCur.Text
        'FlgConstChg = True  '変更有の状態にセットする
        If flgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text1)
        End If
        FlgMainMeas = 20
    End Sub

    Private Sub TxtMarkCur_TextChanged(sender As Object, e As EventArgs) Handles TxtMarkCur.TextChanged
        Mark = TxtMarkCur.Text
        'FlgConstChg = True  '変更有の状態にセットする
        If flgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text1)
        End If
        FlgMainMeas = 20
    End Sub

    Private Sub ConditionDisable()
        TxtMachNoCur.Enabled = False
        TxtSmplNamCur.Enabled = False
        TxtMarkCur.Enabled = False
        TxtMachNoBak.Enabled = False
        TxtSmplNamBak.Enabled = False
        TxtMarkBak.Enabled = False
        GbMeasSpec.Enabled = False
        MeasSpecToolStripMenuItem.Enabled = False
        'CmdMeasSpecSel.Enabled = False
        'CmdMeasSpecSave.Enabled = False
        'GbPrint.Enabled = False
        CmdMeasPrint.Enabled = False
        手動印刷ToolStripMenuItem.Enabled = False
        CmdMeasResultSave.Enabled = False
        保存ToolStripMenuItem1.Enabled = False
        ChkMeasAutoPrn.Enabled = False
        印刷ToolStripMenuItem.Enabled = False
        CmdEtcMeasData.Enabled = False
        他の測定データ選択ToolStripMenuItem1.Enabled = False
        CmdOldDataLoad.Enabled = False
        CmdEtcOldMeasData.Enabled = False
        OldDataToolStripMenuItem.Enabled = False
        LoadToolStripMenuItem.Enabled = False
        AnotherMeasDataSelToolStripMenuItem.Enabled = False
        'CmdQuitSinglesheet.Enabled = False
        設定ToolStripMenuItem1.Enabled = False
        CmdClsGraph.Enabled = False
    End Sub

    Private Sub ConditionEnable()
        TxtMachNoCur.Enabled = True
        TxtSmplNamCur.Enabled = True
        TxtMarkCur.Enabled = True
        GbMeasSpec.Enabled = True
        MeasSpecToolStripMenuItem.Enabled = True
        'CmdMeasSpecSel.Enabled = True
        'CmdMeasSpecSave.Enabled = True
        If Val(TxtMeasNumCur.Text) > 0 Then
            CmdEtcMeasData.Enabled = True
            他の測定データ選択ToolStripMenuItem1.Enabled = True
        End If
        'GbPrint.Enabled = True
        CmdMeasPrint.Enabled = True
        手動印刷ToolStripMenuItem.Enabled = True
        CmdMeasResultSave.Enabled = True
        保存ToolStripMenuItem1.Enabled = True
        ChkMeasAutoPrn.Enabled = True
        印刷ToolStripMenuItem.Enabled = True
        'CmdQuitSinglesheet.Enabled = True
        If FlgAdmin <> 0 Then
            OldDataToolStripMenuItem.Enabled = True
            CmdOldDataLoad.Enabled = True
            LoadToolStripMenuItem.Enabled = True
            CmdClsGraph.Enabled = True
            TxtMachNoBak.Enabled = True
            TxtSmplNamBak.Enabled = True
            TxtMarkBak.Enabled = True
            If Val(TxtMeasNumBak.Text) > 0 Then
                CmdEtcOldMeasData.Enabled = True
                AnotherMeasDataSelToolStripMenuItem.Enabled = True
            End If
        End If
        設定ToolStripMenuItem1.Enabled = True
    End Sub

    Private Sub CmdMeasPrint_Click(sender As Object, e As EventArgs) Handles CmdMeasPrint.Click
        PrintoutMeas()
    End Sub

    Private Sub PrintoutMeas()
        Dim flgprintpreview As Boolean
        flgprintpreview = My.Settings._printpreview

        If FlgAdmin = 1 Then
            '管理者モード時の印刷

            PrintDocument_adm.OriginAtMargins = True
            '1/100インチで指定する
            PrintDocument_adm.DefaultPageSettings.Margins = New Margins(Prn_left_margin, Prn_right_margin,
                                                                        Prn_top_margin, Prn_btm_margin)
            If FlgMeasAutoPrn = 0 Then
                If flgprintpreview = True Then
                    PPD_amd.ShowDialog()
                Else
                    PrintDocument_adm.Print()
                End If
            Else
                PrintDocument_adm.Print()
            End If
        Else
            '通常モード時の印刷
            PrintDocument_nom.OriginAtMargins = True
            PrintDocument_nom.DefaultPageSettings.Margins = New Margins(Prn_left_margin, Prn_right_margin,
                                                                            Prn_top_margin, Prn_btm_margin)
            If FlgMeasAutoPrn = 0 Then
                If flgprintpreview = True Then
                    PPD_nom.ShowDialog()
                Else
                    PrintDocument_nom.Print()
                End If
            Else

                'PrintDocument_nom.PrinterSettings.PrinterName = "Microsoft Print to PDF"
                'PrintDocument_nom.PrinterSettings.PrintToFile = True
                'PrintDocument_nom.PrinterSettings.PrintFileName = curdir & DEF_RESULT_FILE_FLD & "\" & StrDataFileName

                PrintDocument_nom.Print()
            End If
        End If
    End Sub

    Private Sub PrintDocument_adm_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument_adm.PrintPage
        e.Graphics.Clear(Color.White)
        meas_prn_linepath1.Clear()
        prn_meas_waku_path2.Clear()
        prn_square_path1.Clear()
        prn_square_path2.Clear()
        prn_axis_path_cur.Clear()
        prn_triangle_path1.Clear()
        prn_triangle_path2.Clear()
        prn_axis_path_bak.Clear()

        '管理者モード時の印刷
        Const gyou_height25 = 25
        Const cell_height25 = 25
        Const cell_width67 = 65
        Const cell_width74 = 74
        Const cell_width43 = 43
        Const cell_padding_left = 5
        Const prn_graph_width = 500

        Dim stringSize As SizeF
        Dim string_tmp As String
        Dim title_height As Single
        Dim pen_black_1 As New Pen(Color.Black, 1)
        Dim pen_black_2 As New Pen(Color.Black, 2)
        Dim pen_blue_1 As New Pen(Color.Blue, 1)
        Dim fnt_14 As New Font("MS UI Gothic", 14)
        Dim fnt_10 As New Font("MS UI Gothic", 10)
        Dim fnt_9 As New Font("MS US Gothic", 9)

        Dim printbc_brush As Brush = New SolidBrush(frm_MeasForm_bc)
        Dim print_curdata_brush As Brush = New SolidBrush(frm_MeasCurData_color)
        Dim print_olddata_brush As Brush = New SolidBrush(frm_MeasOldData_color)
        Dim printfc_brush As Brush = New SolidBrush(frm_MeasForm_fc)

        Dim paper_width As Integer = e.MarginBounds.Width
        Dim paper_height As Integer = e.MarginBounds.Height
        'Console.WriteLine(paper_width)
        'Console.WriteLine(paper_height)

        '用紙の色（印刷範囲全体）
        If frm_MeasForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
            e.Graphics.FillRectangle(printbc_brush, -Prn_left_margin, -Prn_top_margin, paper_width + Prn_left_margin + Prn_right_margin * 2, paper_height + Prn_top_margin + Prn_btm_margin * 2)
        End If

        string_tmp = My.Application.Info.ProductName & " シングルシート"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_14)
        title_height = stringSize.Height

        e.Graphics.DrawString(string_tmp, fnt_14, printfc_brush, 0, 0)

        '測定仕様枠
        Dim measspec_hyoutop As Single = title_height + gyou_height25
        Dim path As New GraphicsPath
        path.StartFigure()
        path.AddLine(0, measspec_hyoutop,
                     paper_width, measspec_hyoutop)
        path.StartFigure()
        path.AddLine(0, measspec_hyoutop + (cell_height25 * 1),
                     paper_width, measspec_hyoutop + (cell_height25 * 1))
        path.StartFigure()
        path.AddLine(0, measspec_hyoutop + (cell_height25 * 2),
                     paper_width, measspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(0, measspec_hyoutop + (cell_height25 * 3),
                     paper_width, measspec_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(0, measspec_hyoutop,
                     0, measspec_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(120, measspec_hyoutop,
                     120, measspec_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(120 + 150, measspec_hyoutop,
                     120 + 150, measspec_hyoutop + (cell_height25 * 3))
        If FlgDBF = 1 Then
            path.StartFigure()
            path.AddLine(paper_width - (100 + 100), measspec_hyoutop,
                         paper_width - (100 + 100), measspec_hyoutop + (cell_height25 * 3))
        End If
        path.StartFigure()
        path.AddLine(paper_width - 100, measspec_hyoutop,
                     paper_width - 100, measspec_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(paper_width, measspec_hyoutop,
                     paper_width, measspec_hyoutop + (cell_height25 * 3))

        '測定結果計算値
        Dim measresultcalc_hyoutop As Single = measspec_hyoutop + (cell_height25 * 3)
        path.StartFigure()
        path.AddLine(0, measresultcalc_hyoutop + (cell_height25 * 1),
                     paper_width, measresultcalc_hyoutop + (cell_height25 * 1))
        path.StartFigure()
        path.AddLine(cell_width74 + cell_width43, measresultcalc_hyoutop + (cell_height25 * 2),
                     paper_width, measresultcalc_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(0, measresultcalc_hyoutop + (cell_height25 * 3),
                     paper_width, measresultcalc_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(0, measresultcalc_hyoutop + (cell_height25 * 4),
                     paper_width, measresultcalc_hyoutop + (cell_height25 * 4))
        path.StartFigure()
        path.AddLine(0, measresultcalc_hyoutop + (cell_height25 * 5),
                     paper_width, measspec_hyoutop + (cell_height25 * 3) + (cell_height25 * 5))
        path.StartFigure()
        path.AddLine(0, measresultcalc_hyoutop + (cell_height25 * 1),
                     0, measresultcalc_hyoutop + (cell_height25 * 5))
        path.StartFigure()
        path.AddLine(cell_width74, measresultcalc_hyoutop + (cell_height25 * 1),
                     cell_width74, measresultcalc_hyoutop + (cell_height25 * 5))
        path.StartFigure()
        path.AddLine(cell_width74 + cell_width43, measresultcalc_hyoutop + (cell_height25 * 1),
                     cell_width74 + cell_width43, measresultcalc_hyoutop + (cell_height25 * 5))
        For i = 1 To 10
            path.StartFigure()
            If i Mod 2 = 0 Then
                path.AddLine(cell_width74 + cell_width43 + (cell_width67 * i), measresultcalc_hyoutop + (cell_height25 * 1),
                             cell_width74 + cell_width43 + (cell_width67 * i), measresultcalc_hyoutop + (cell_height25 * 5))
            Else
                path.AddLine(cell_width74 + cell_width43 + (cell_width67 * i), measresultcalc_hyoutop + (cell_height25 * 2),
                             cell_width74 + cell_width43 + (cell_width67 * i), measresultcalc_hyoutop + (cell_height25 * 5))
            End If
        Next

        '測定データ表
        Dim meas_data_table_top As Single = measspec_hyoutop + cell_height25 * 9
        Dim meas_data_table_col_width As Single = (paper_width - prn_graph_width) / 3

        For i = 0 To 18
            path.StartFigure()
            If i = 1 Then
                path.AddLine(prn_graph_width + meas_data_table_col_width, meas_data_table_top + cell_height25 * i,
                             paper_width, meas_data_table_top + cell_height25 * i)
            Else
                path.AddLine(prn_graph_width, meas_data_table_top + cell_height25 * i,
                             paper_width, meas_data_table_top + cell_height25 * i)
            End If
        Next
        path.StartFigure()
        path.AddLine(prn_graph_width, meas_data_table_top,
                     prn_graph_width, meas_data_table_top + cell_height25 * 18)
        path.StartFigure()
        path.AddLine(paper_width, meas_data_table_top,
                     paper_width, meas_data_table_top + cell_height25 * 18)
        path.StartFigure()
        path.AddLine(prn_graph_width + meas_data_table_col_width * 1, meas_data_table_top,
                     prn_graph_width + meas_data_table_col_width * 1, meas_data_table_top + cell_height25 * 18)
        path.StartFigure()
        path.AddLine(prn_graph_width + meas_data_table_col_width * 2, meas_data_table_top + cell_height25,
                     prn_graph_width + meas_data_table_col_width * 2, meas_data_table_top + cell_height25 * 18)

        '測定データの測定日時
        Dim MeasDataNo_cur As Integer = Val(LblMeasNumCur_adm.Text)
        If MeasDataNo_cur > 0 Then
            string_tmp = "測定データ  測定　日付：" & DataDate_cur & "   時間：" & DataTime_cur
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - stringSize.Width, 0)
        End If

        '過去データの測定日時
        Dim MeasDataNo_bak As Integer = Val(LblMeasNumBak_adm.Text)
        If MeasDataNo_bak > 0 Then
            string_tmp = "過去データ  測定　日付：" & DataDate_bak & "   時間：" & DataTime_bak
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                  paper_width - stringSize.Width,
                                  stringSize.Height + 5)
        End If

        string_tmp = "マシーンNo."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              120 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "サンプル名"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              120 + 150 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        If FlgDBF = 1 Then
            string_tmp = "マーク"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  paper_width - (100 + 120) + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        End If
        string_tmp = "測定回数"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - 100 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "測定仕様"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_padding_left,
                              title_height + gyou_height25 + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "過去の仕様"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_padding_left,
                              title_height + gyou_height25 + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "データ"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + cell_height25 - stringSize.Height / 2)
        string_tmp = "測定"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + 8)
        string_tmp = "No."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + 2)
        string_tmp = "配向角[deg.]"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "配向比"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 2 + cell_width67 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "伝播速度[Km/S]"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 4 + cell_width67 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 6 + cell_width67 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - meas_data_table_col_width - stringSize.Width / 2,
                              meas_data_table_top + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "TSI[Km/S]^2"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 8 + cell_width67 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Peak"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 0 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 6 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Deep"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 1 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 7 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "MD/CD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 2 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Peak/Deep"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_9)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 3 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "MD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_9)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 4 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 8 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "CD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 5 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 9 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)

        string_tmp = "測定データ"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        '測定結果計算表のタイトル
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        '測定データ表のタイトル
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              prn_graph_width + meas_data_table_col_width + meas_data_table_col_width / 2 - stringSize.Width / 2,
                              meas_data_table_top + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)

        string_tmp = "過去データ"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        '測定結果計算表のタイトル
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_width74 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
        '測定データ表のタイトル
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              paper_width - meas_data_table_col_width / 2 - stringSize.Width / 2, meas_data_table_top + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        'マシーンNo. cur
        string_tmp = TxtMachNoCur.Text
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              120 + cell_padding_left,
                              measspec_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        'マシーンNo. bak
        string_tmp = TxtMachNoBak.Text
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              120 + cell_padding_left,
                              measspec_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        'サンプル名 cur
        string_tmp = TxtSmplNamCur.Text
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              120 + 150 + cell_padding_left,
                              measspec_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        'サンプル名 bak
        string_tmp = TxtSmplNamBak.Text
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              120 + 150 + cell_padding_left,
                              measspec_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        If FlgDBF = 1 Then
            'マーク cur
            string_tmp = TxtMarkCur.Text
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - (100 + 120) + cell_padding_left,
                                  measspec_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
            'マーク bak
            string_tmp = TxtMarkBak.Text
            e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                  paper_width - (100 + 120) + cell_padding_left,
                                  measspec_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        End If
        '測定回数 cur
        string_tmp = Trim(TxtMeasNumCur.Text)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              paper_width - 100 + cell_padding_left,
                              measspec_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        '測定回数 bak
        string_tmp = Trim(TxtMeasNumBak.Text)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              paper_width - 100 + cell_padding_left,
                              measspec_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '測定No. cur
        string_tmp = LblMeasNumCur_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        '配向角Peak cur
        string_tmp = LblAnglPeakCur_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 0 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '配向角Deep cur
        string_tmp = LblAnglDeepCur_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 1 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '配向比MD/CD cur
        string_tmp = LblratioMDCDCur_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 2 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '配向比Peak/Deep cur
        string_tmp = LblratioPKDPCur_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 3 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '伝播速度MD cur
        string_tmp = LblSpdMDCur_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 4 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '伝播速度CD cur
        string_tmp = LblSpdCDCur_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 5 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '伝播速度Peak cur
        string_tmp = LblSpdPeakCur_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 6 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '伝播速度Deep cur
        string_tmp = LblSpdDeepCur_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 7 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        'TSIMD cur
        string_tmp = LblTSIMDCur_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 8 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        'TSICD cur
        string_tmp = LblTSICDCur_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 9 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '測定No. bak
        string_tmp = LblMeasNumBak_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_width74 + cell_width43 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        '配向角Peak bak
        string_tmp = LblAnglPeakBak_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 0 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        '配向角Deep bak
        string_tmp = LblAnglDeepBak_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 1 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        '配向比MD/CD bak
        string_tmp = LblratioMDCDBak_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 2 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        '配向比Peak/Deep bak
        string_tmp = LblratioPKDPBak_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 3 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        '伝播速度MD bak
        string_tmp = LblSpdMDBak_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 4 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        '伝播速度CD bak
        string_tmp = LblSpdCDBak_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 5 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        '伝播速度Peak bak
        string_tmp = LblSpdPeakBak_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 6 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        '伝播速度Deep bak
        string_tmp = LblSpdDeepBak_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 7 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        'TSIMD bak
        string_tmp = LblTSIMDBak_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 8 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        'TSICD bak
        string_tmp = LblTSICDBak_adm.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 9 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)


        'Dim meas_data_hyou_col1_width As Single = (Prn_left_margin + paper_width - 200) - (Prn_left_margin + prn_graph_width + 25)
        string_tmp = "角度[deg.]"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              prn_graph_width + meas_data_table_col_width / 2 - stringSize.Width / 2,
                              meas_data_table_top + cell_height25 - stringSize.Height / 2)
        For i = 2 To 17
            Select Case i
                Case 2 : string_tmp = "  0.00"
                Case 3 : string_tmp = " 11.25"
                Case 4 : string_tmp = " 22.50"
                Case 5 : string_tmp = " 33.75"
                Case 6 : string_tmp = " 45.00"
                Case 7 : string_tmp = " 56.25"
                Case 8 : string_tmp = " 67.50"
                Case 9 : string_tmp = " 78.75"
                Case 10 : string_tmp = " 90.00"
                Case 11 : string_tmp = "101.25"
                Case 12 : string_tmp = "112.50"
                Case 13 : string_tmp = "123.75"
                Case 14 : string_tmp = "135.00"
                Case 15 : string_tmp = "146.25"
                Case 16 : string_tmp = "157.50"
                Case 17 : string_tmp = "168.75"
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  prn_graph_width + meas_data_table_col_width / 2 - stringSize.Width / 2,
                                  meas_data_table_top + cell_height25 * i + cell_height25 / 2 - stringSize.Height / 2)
        Next

        For i = 2 To 17
            Select Case i
                Case 2 : string_tmp = LblMeasDatCur1_adm.Text
                Case 3 : string_tmp = LblMeasDatCur2_adm.Text
                Case 4 : string_tmp = LblMeasDatCur3_adm.Text
                Case 5 : string_tmp = LblMeasDatCur4_adm.Text
                Case 6 : string_tmp = LblMeasDatCur5_adm.Text
                Case 7 : string_tmp = LblMeasDatCur6_adm.Text
                Case 8 : string_tmp = LblMeasDatCur7_adm.Text
                Case 9 : string_tmp = LblMeasDatCur8_adm.Text
                Case 10 : string_tmp = LblMeasDatCur9_adm.Text
                Case 11 : string_tmp = LblMeasDatCur10_adm.Text
                Case 12 : string_tmp = LblMeasDatCur11_adm.Text
                Case 13 : string_tmp = LblMeasDatCur12_adm.Text
                Case 14 : string_tmp = LblMeasDatCur13_adm.Text
                Case 15 : string_tmp = LblMeasDatCur14_adm.Text
                Case 16 : string_tmp = LblMeasDatCur15_adm.Text
                Case 17 : string_tmp = LblMeasDatCur16_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  prn_graph_width + meas_data_table_col_width + meas_data_table_col_width / 2 - stringSize.Width / 2,
                                  meas_data_table_top + cell_height25 * i + cell_height25 / 2 - stringSize.Height / 2)
        Next

        For i = 2 To 17
            Select Case i
                Case 2 : string_tmp = LblMeasDatBak1_adm.Text
                Case 3 : string_tmp = LblMeasDatBak2_adm.Text
                Case 4 : string_tmp = LblMeasDatBak3_adm.Text
                Case 5 : string_tmp = LblMeasDatBak4_adm.Text
                Case 6 : string_tmp = LblMeasDatBak5_adm.Text
                Case 7 : string_tmp = LblMeasDatBak6_adm.Text
                Case 8 : string_tmp = LblMeasDatBak7_adm.Text
                Case 9 : string_tmp = LblMeasDatBak8_adm.Text
                Case 10 : string_tmp = LblMeasDatBak9_adm.Text
                Case 11 : string_tmp = LblMeasDatBak10_adm.Text
                Case 12 : string_tmp = LblMeasDatBak11_adm.Text
                Case 13 : string_tmp = LblMeasDatBak12_adm.Text
                Case 14 : string_tmp = LblMeasDatBak13_adm.Text
                Case 15 : string_tmp = LblMeasDatBak14_adm.Text
                Case 16 : string_tmp = LblMeasDatBak15_adm.Text
                Case 17 : string_tmp = LblMeasDatBak16_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                  paper_width - meas_data_table_col_width / 2 - stringSize.Width / 2,
                                  meas_data_table_top + cell_height25 * i + cell_height25 / 2 - stringSize.Height / 2)
        Next

        'グラフを画像として貼り付ける
        Dim bmp As New Bitmap(PictureBox1.Width, PictureBox1.Height)
        PictureBox1.DrawToBitmap(bmp, New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height))
        bmp.MakeTransparent(BackColor)
        e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.High

        Dim bmp_resize As Bitmap = New Bitmap(bmp, bmp.Width * 1, bmp.Height * 1)
        e.Graphics.DrawImage(bmp_resize, 0, meas_data_table_top)

        meas_prn_linepath1.Add(path)

        For Each path_tmp As GraphicsPath In meas_prn_linepath1
            e.Graphics.DrawPath(pen_black_1, path_tmp)
        Next

        path.Dispose()
        bmp.Dispose()
        bmp_resize.Dispose()
        pen_black_1.Dispose()
        pen_black_2.Dispose()
        pen_blue_1.Dispose()
        fnt_10.Dispose()
        fnt_14.Dispose()
        fnt_9.Dispose()

    End Sub

    Private Sub PrnDrawGraph(ByVal KdData_tmp As Integer,
                             ByVal center_x As Single,
                             ByVal center_y As Single,
                             ByVal graph_size As Integer,
                             ByVal SampleNo_tmp As Integer)
        Dim DataMax As Single
        Dim DataMin As Single
        Dim Px As Single
        Dim Py As Single
        Dim N As Integer
        Dim DataK As Single

        DataMax = 0
        DataMin = 9999

        For N = 3 To 18
            If DataPrcNum(KdData_tmp, SampleNo_tmp, N) > DataMax Then
                DataMax = DataPrcNum(KdData_tmp, SampleNo_tmp, N)
            End If
            If DataPrcNum(KdData_tmp, SampleNo_tmp, N) < DataMin Then
                DataMin = DataPrcNum(KdData_tmp, SampleNo_tmp, N)
            End If
        Next

        For N = 0 To 7
            DataK = DataPrcNum(KdData_tmp, SampleNo_tmp, N + 11)
            If DataK = 0 Then
                DataK = 10
            End If

            Py = center_y - (graph_size / 2 * Math.Cos((N + 8) * 11.25 * Rad)) * (DataK / DataMax)
            Px = center_x + (graph_size / 2 * Math.Sin((N + 8) * 11.25 * Rad)) * (DataK / DataMax)

            If KdData_tmp = 1 Then
                PrnDrawSquare(Px, Py)
            Else
                PrnDrawTriangle(Px, Py)
            End If

            Py = center_y + (graph_size / 2 * Math.Cos((N + 8) * 11.25 * Rad)) * (DataK / DataMax)
            Px = center_x - (graph_size / 2 * Math.Sin((N + 8) * 11.25 * Rad)) * (DataK / DataMax)

            If KdData_tmp = 1 Then
                PrnDrawSquare(Px, Py)
            Else
                PrnDrawTriangle(Px, Py)
            End If

            DataK = DataPrcNum(KdData_tmp, SampleNo_tmp, N + 3)
            If DataK = 0 Then
                DataK = 10
            End If

            Py = center_y - (graph_size / 2 * Math.Cos(N * 11.25 * Rad)) * (DataK / DataMax)
            Px = center_x + (graph_size / 2 * Math.Sin(N * 11.25 * Rad)) * (DataK / DataMax)
            If KdData_tmp = 1 Then
                PrnDrawSquare(Px, Py)
            Else
                PrnDrawTriangle(Px, Py)
            End If

            Py = center_y + (graph_size / 2 * Math.Cos(N * 11.25 * Rad)) * (DataK / DataMax)
            Px = center_x - (graph_size / 2 * Math.Sin(N * 11.25 * Rad)) * (DataK / DataMax)
            If KdData_tmp = 1 Then
                PrnDrawSquare(Px, Py)
            Else
                PrnDrawTriangle(Px, Py)
            End If
        Next
    End Sub

    Private Sub PrnDrawSquare(Px As Single, Py As Single)
        Const rect_size = 4

        Dim path As New Drawing2D.GraphicsPath

        path.StartFigure()
        path.AddRectangle(New Rectangle(Px - rect_size, Py - rect_size, rect_size * 2, rect_size * 2))
        prn_square_path1.Add(path)

        path.StartFigure()
        path.AddRectangle(New Rectangle(Px, Py, 1, 1))
        prn_square_path2.Add(path)

        path.Dispose()
    End Sub

    Private Sub PrnDrawTriangle(Px As Single, Py As Single)
        Const tri_size_v = 4
        Const tri_size_h = 5
        Dim path As New Drawing2D.GraphicsPath

        path.StartFigure()
        path.AddLine(Px - tri_size_h, Py + tri_size_v, Px + tri_size_h, Py + tri_size_v)
        path.AddLine(Px + tri_size_h, Py + tri_size_v, Px, Py - tri_size_v)
        path.AddLine(Px, Py - tri_size_v, Px - tri_size_h, Py + tri_size_h)
        prn_triangle_path1.Add(path)

        path.StartFigure()
        path.AddRectangle(New Rectangle(Px, Py, 1, 1))
        prn_triangle_path2.Add(path)

        path.Dispose()
    End Sub

    Private Sub PrnDrawAxisCur(ByVal KdData_tmp As Integer,
                               ByVal center_x As Single,
                               ByVal center_y As Single,
                               ByVal SampleNo_tmp As Integer)
        Dim MdX As Single
        Dim MdY As Single
        Dim CdX As Single
        Dim CdY As Single
        Dim DataPeak As Single
        Dim DataDeep As Single
        Dim Ds As String
        Dim Ds_1 As String

        Dim path As New Drawing2D.GraphicsPath

        Ds = DataPrcStr(KdData_tmp, SampleNo_tmp, 9)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            DataPeak = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            DataPeak = Val(Ds)
        End If
        MdX = 200 * Math.Sin(DataPeak * Rad)
        MdY = 200 * Math.Cos(DataPeak * Rad)

        path.StartFigure()
        path.AddLine(center_x + MdX, center_y - MdY, center_x - MdX, center_y + MdY)

        Ds = DataPrcStr(KdData_tmp, SampleNo_tmp, 8)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            DataDeep = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            DataDeep = Val(Ds)
        End If
        CdX = 200 * Math.Cos(DataDeep * Rad)
        CdY = 200 * Math.Sin(DataDeep * Rad)

        path.StartFigure()
        path.AddLine(center_x - CdX, center_y - CdY, center_x + CdX, center_y + CdY)

        prn_axis_path_cur.Add(path)

        path.Dispose()
    End Sub

    Private Sub PrnDrawAxisBak(ByVal KdData_tmp As Integer,
                               ByVal center_x As Single,
                               ByVal center_y As Single,
                               ByVal SampleNo_tmp As Integer)
        Dim MdX As Single
        Dim MdY As Single
        Dim CdX As Single
        Dim CdY As Single
        Dim DataPeak As Single
        Dim DataDeep As Single
        Dim Ds As String
        Dim Ds_1 As String

        Dim path As New Drawing2D.GraphicsPath

        Ds = DataPrcStr(KdData_tmp, SampleNo_tmp, 9)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            DataPeak = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            DataPeak = Val(Ds)
        End If
        MdX = 200 * Math.Sin(DataPeak * Rad)
        MdY = 200 * Math.Cos(DataPeak * Rad)

        path.StartFigure()
        path.AddLine(center_x + MdX, center_y - MdY, center_x - MdX, center_y + MdY)

        Ds = DataPrcStr(KdData_tmp, SampleNo_tmp, 8)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            DataDeep = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            DataDeep = Val(Ds)
        End If
        CdX = 200 * Math.Cos(DataDeep * Rad)
        CdY = 200 * Math.Sin(DataDeep * Rad)

        path.StartFigure()
        path.AddLine(center_x - CdX, center_y - CdY, center_x + CdX, center_y + CdY)

        prn_axis_path_bak.Add(path)

        path.Dispose()
    End Sub

    Private Sub PrintDocument_nom_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument_nom.PrintPage
        e.Graphics.Clear(Color.White)
        meas_prn_linepath1.Clear()
        prn_meas_waku_path2.Clear()
        prn_square_path1.Clear()
        prn_square_path2.Clear()
        prn_axis_path_cur.Clear()
        prn_triangle_path1.Clear()
        prn_triangle_path2.Clear()
        prn_axis_path_bak.Clear()

        '通常モード時の印刷
        Const gyou_height25 = 25
        Const cell_height25 = 25
        Const cell_width67 = 65
        Const cell_width74 = 74
        Const cell_width43 = 43
        Const cell_padding_left = 5
        Const prn_graph_width = 500

        Dim stringSize As SizeF
        Dim string_tmp As String
        Dim title_height As Single
        Dim pen_black_1 As New Pen(Color.Black, 1)
        Dim pen_black_2 As New Pen(Color.Black, 2)
        Dim pen_blue_1 As New Pen(Color.Blue, 1)
        Dim fnt_20 As New Font("MS UI Gothic", 20)
        Dim fnt_10 As New Font("MS UI Gothic", 10)
        Dim fnt_9 As New Font("MS US Gothic", 9)

        Dim printbc_brush As Brush = New SolidBrush(frm_MeasForm_bc)
        Dim print_curdata_brush As Brush = New SolidBrush(frm_MeasCurData_color)
        Dim print_olddata_brush As Brush = New SolidBrush(frm_MeasOldData_color)
        Dim printfc_brush As Brush = New SolidBrush(frm_MeasForm_fc)

        Dim paper_width As Integer = e.MarginBounds.Width
        Dim paper_height As Integer = e.MarginBounds.Height

        '用紙の色（印刷範囲全体）
        If frm_MeasForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
            e.Graphics.FillRectangle(printbc_brush, -Prn_left_margin, -Prn_top_margin, paper_width + Prn_left_margin + Prn_right_margin * 2, paper_height + Prn_top_margin + Prn_btm_margin * 2)
        End If
        'Console.WriteLine(e.Graphics.PageUnit)
        'Console.WriteLine(e.PageSettings.Margins.Left)
        'Console.WriteLine(e.PageSettings.Margins.Right)
        'Console.WriteLine(e.PageSettings.Margins.Top)
        'Console.WriteLine(e.PageSettings.Margins.Bottom)
        'Console.WriteLine(e.MarginBounds.Width)
        'Console.WriteLine(e.MarginBounds.Height)
        'Console.WriteLine(PrintDocument_nom.OriginAtMargins)

        'e.Graphics.DrawRectangle(New Pen(Color.Black, 1), New Rectangle(0, 0, e.MarginBounds.Width, e.MarginBounds.Height))

        string_tmp = My.Application.Info.ProductName & " シングルシート"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_20)
        title_height = stringSize.Height
        e.Graphics.DrawString(string_tmp, fnt_20, printfc_brush, 0, 0)

        '測定仕様枠
        Dim measspec_hyoutop As Single = title_height + gyou_height25
        Dim path As New GraphicsPath
        path.StartFigure()
        path.AddLine(0, measspec_hyoutop,
                     paper_width, measspec_hyoutop)
        path.StartFigure()
        path.AddLine(0, measspec_hyoutop + (cell_height25 * 1),
                     paper_width, measspec_hyoutop + (cell_height25 * 1))
        path.StartFigure()
        path.AddLine(0, measspec_hyoutop + (cell_height25 * 2),
                     paper_width, measspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(0, measspec_hyoutop,
                     0, measspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(120, measspec_hyoutop,
                     120, measspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(120 + 150, measspec_hyoutop,
                     120 + 150, measspec_hyoutop + (cell_height25 * 2))
        If FlgDBF = 1 Then
            path.StartFigure()
            path.AddLine(paper_width - (100 + 100), measspec_hyoutop,
                         paper_width - (100 + 100), measspec_hyoutop + (cell_height25 * 2))
        End If
        path.StartFigure()
        path.AddLine(paper_width - 100, measspec_hyoutop,
                     paper_width - 100, measspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(paper_width, measspec_hyoutop,
                     paper_width, measspec_hyoutop + (cell_height25 * 2))

        '測定結果計算値
        Dim measresultcalc_hyoutop As Single = measspec_hyoutop + (cell_height25 * 2)
        path.StartFigure()
        path.AddLine(0, measresultcalc_hyoutop + (cell_height25 * 1),
                     paper_width, measresultcalc_hyoutop + (cell_height25 * 1))
        path.StartFigure()
        path.AddLine(cell_width74 + cell_width43, measresultcalc_hyoutop + (cell_height25 * 2),
                     paper_width, measresultcalc_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(0, measresultcalc_hyoutop + (cell_height25 * 3),
                     paper_width, measresultcalc_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(0, measresultcalc_hyoutop + (cell_height25 * 4),
                     paper_width, measresultcalc_hyoutop + (cell_height25 * 4))

        path.StartFigure()
        path.AddLine(0, measresultcalc_hyoutop + (cell_height25 * 1),
                     0, measresultcalc_hyoutop + (cell_height25 * 4))
        path.StartFigure()
        path.AddLine(cell_width74, measresultcalc_hyoutop + (cell_height25 * 1),
                     cell_width74, measresultcalc_hyoutop + (cell_height25 * 4))
        path.StartFigure()
        path.AddLine(cell_width74 + cell_width43, measresultcalc_hyoutop + (cell_height25 * 1),
                     cell_width74 + cell_width43, measresultcalc_hyoutop + (cell_height25 * 4))
        For i = 1 To 10
            path.StartFigure()
            If i Mod 2 = 0 Then
                path.AddLine(cell_width74 + cell_width43 + (cell_width67 * i), measresultcalc_hyoutop + (cell_height25 * 1),
                             cell_width74 + cell_width43 + (cell_width67 * i), measresultcalc_hyoutop + (cell_height25 * 4))
            Else
                path.AddLine(cell_width74 + cell_width43 + (cell_width67 * i), measresultcalc_hyoutop + (cell_height25 * 2),
                             cell_width74 + cell_width43 + (cell_width67 * i), measresultcalc_hyoutop + (cell_height25 * 4))
            End If
        Next

        '測定データ表
        Dim meas_data_table_top As Single = measspec_hyoutop + cell_height25 * 7
        Dim meas_data_table_col_width As Single = (paper_width - prn_graph_width) / 3

        For i = 0 To 18
            path.StartFigure()
            If i = 1 Then
                path.AddLine(prn_graph_width + meas_data_table_col_width, meas_data_table_top + cell_height25 * i,
                             paper_width, meas_data_table_top + cell_height25 * i)
            Else
                path.AddLine(prn_graph_width, meas_data_table_top + cell_height25 * i,
                             paper_width, meas_data_table_top + cell_height25 * i)
            End If
        Next
        path.StartFigure()
        path.AddLine(prn_graph_width, meas_data_table_top,
                     prn_graph_width, meas_data_table_top + cell_height25 * 18)
        path.StartFigure()
        path.AddLine(paper_width, meas_data_table_top,
                     paper_width, meas_data_table_top + cell_height25 * 18)
        path.StartFigure()
        path.AddLine(prn_graph_width + meas_data_table_col_width, meas_data_table_top,
                     prn_graph_width + meas_data_table_col_width, meas_data_table_top + cell_height25 * 18)

        Dim MeasDataNo_cur As Integer = Val(LblMeasNumCur_nom.Text)
        If MeasDataNo_cur > 0 Then
            string_tmp = "測定データ  測定　日付：" & DataDate_cur & "   時間：" & DataTime_cur
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - stringSize.Width, 0)
        End If

        string_tmp = "マシーンNo."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              120 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "サンプル名"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              120 + 150 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        If FlgDBF = 1 Then
            string_tmp = "マーク"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  paper_width - (100 + 120) + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        End If
        string_tmp = "測定回数"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - 100 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)

        string_tmp = "測定仕様"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_padding_left,
                              title_height + gyou_height25 + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "データ"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + 19)
        string_tmp = "測定"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + 8)
        string_tmp = "No."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + 2)
        string_tmp = "配向角[deg.]"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "配向比"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 2 + cell_width67 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "伝播速度[Km/S]"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 4 + cell_width67 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 6 + cell_width67 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - meas_data_table_col_width - stringSize.Width / 2,
                              meas_data_table_top + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "TSI[Km/S]^2"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 8 + cell_width67 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Peak"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 0 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 6 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Deep"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 1 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 7 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "MD/CD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 2 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Peak/Deep"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_9)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 3 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "MD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_9)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 4 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 8 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "CD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 5 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              cell_width74 + cell_width43 + cell_width67 * 9 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)

        string_tmp = "測定データ"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        '測定結果計算表のタイトル
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        '測定データ表のタイトル
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              paper_width - meas_data_table_col_width - stringSize.Width / 2,
                              meas_data_table_top + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        'マシーンNo. cur
        string_tmp = TxtMachNoCur.Text
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              120 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        'サンプル名 cur
        string_tmp = TxtSmplNamCur.Text
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              120 + 150 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        If FlgDBF = 1 Then
            'マーク cur
            string_tmp = TxtMarkCur.Text
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - (100 + 120) + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        End If
        '測定回数 cur
        string_tmp = Trim(TxtMeasNumCur.Text)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              paper_width - 100 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 * 1 + cell_height25 / 2 - stringSize.Height / 2)
        '測定No. cur
        string_tmp = LblMeasNumCur_nom.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        '配向角Peak cur
        string_tmp = LblAnglPeakCur_nom.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 0 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '配向角Deep cur
        string_tmp = LblAnglDeepCur_nom.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 1 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '配向比MD/CD cur
        string_tmp = LblratioMDCDCur_nom.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 2 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '配向比Peak/Deep cur
        string_tmp = LblratioPKDPCur_nom.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 3 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '伝播速度MD cur
        string_tmp = LblSpdMDCur_nom.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 4 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '伝播速度CD cur
        string_tmp = LblSpdCDCur_nom.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 5 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '伝播速度Peak cur
        string_tmp = LblSpdPeakCur_nom.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 6 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        '伝播速度Deep cur
        string_tmp = LblSpdDeepCur_nom.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 7 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        'TSIMD cur
        string_tmp = LblTSIMDCur_nom.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 8 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        'TSICD cur
        string_tmp = LblTSICDCur_nom.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_width74 + cell_width43 + cell_width67 * 9 + cell_width67 / 2 - stringSize.Width / 2,
                              measresultcalc_hyoutop + gyou_height25 + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)

        'Dim meas_data_hyou_col1_width As Single = paper_width - 200 - prn_graph_width + 25
        string_tmp = "角度[deg.]"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              prn_graph_width + meas_data_table_col_width / 2 - stringSize.Width / 2,
                              meas_data_table_top + cell_height25 - stringSize.Height / 2)
        For i = 2 To 17
            Select Case i
                Case 2 : string_tmp = "  0.00"
                Case 3 : string_tmp = " 11.25"
                Case 4 : string_tmp = " 22.50"
                Case 5 : string_tmp = " 33.75"
                Case 6 : string_tmp = " 45.00"
                Case 7 : string_tmp = " 56.25"
                Case 8 : string_tmp = " 67.50"
                Case 9 : string_tmp = " 78.75"
                Case 10 : string_tmp = " 90.00"
                Case 11 : string_tmp = "101.25"
                Case 12 : string_tmp = "112.50"
                Case 13 : string_tmp = "123.75"
                Case 14 : string_tmp = "135.00"
                Case 15 : string_tmp = "146.25"
                Case 16 : string_tmp = "157.50"
                Case 17 : string_tmp = "168.75"
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              prn_graph_width + meas_data_table_col_width / 2 - stringSize.Width / 2,
                              meas_data_table_top + cell_height25 * i + cell_height25 / 2 - stringSize.Height / 2)
        Next

        For i = 2 To 17
            Select Case i
                Case 2 : string_tmp = LblMeasDatCur1_nom.Text
                Case 3 : string_tmp = LblMeasDatCur2_nom.Text
                Case 4 : string_tmp = LblMeasDatCur3_nom.Text
                Case 5 : string_tmp = LblMeasDatCur4_nom.Text
                Case 6 : string_tmp = LblMeasDatCur5_nom.Text
                Case 7 : string_tmp = LblMeasDatCur6_nom.Text
                Case 8 : string_tmp = LblMeasDatCur7_nom.Text
                Case 9 : string_tmp = LblMeasDatCur8_nom.Text
                Case 10 : string_tmp = LblMeasDatCur9_nom.Text
                Case 11 : string_tmp = LblMeasDatCur10_nom.Text
                Case 12 : string_tmp = LblMeasDatCur11_nom.Text
                Case 13 : string_tmp = LblMeasDatCur12_nom.Text
                Case 14 : string_tmp = LblMeasDatCur13_nom.Text
                Case 15 : string_tmp = LblMeasDatCur14_nom.Text
                Case 16 : string_tmp = LblMeasDatCur15_nom.Text
                Case 17 : string_tmp = LblMeasDatCur16_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - meas_data_table_col_width - stringSize.Width / 2,
                                  meas_data_table_top + cell_height25 * i + cell_height25 / 2 - stringSize.Height / 2)
        Next

        'グラフを画像として貼り付ける
        Dim bmp As New Bitmap(PictureBox1.Width, PictureBox1.Height)
        PictureBox1.DrawToBitmap(bmp, New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height))
        bmp.MakeTransparent(BackColor)
        e.Graphics.InterpolationMode = InterpolationMode.High

        Dim bmp_resize As Bitmap = New Bitmap(bmp, bmp.Width * 1, bmp.Height * 1)
        e.Graphics.DrawImage(bmp_resize, 0, meas_data_table_top)

        'path.StartFigure()
        'path.AddRectangle(New Rectangle(0, meas_data_table_top,
        '450, 450))

        meas_prn_linepath1.Add(path)

        For Each path_tmp As GraphicsPath In meas_prn_linepath1
            e.Graphics.DrawPath(pen_black_1, path_tmp)
        Next

        bmp.Dispose()
        bmp_resize.Dispose()
        pen_black_1.Dispose()
        pen_black_2.Dispose()
        pen_blue_1.Dispose()
        fnt_10.Dispose()
        fnt_20.Dispose()
        fnt_9.Dispose()

    End Sub

    Private Sub MeasResultSave()
        CmdMeasResultSave.Enabled = False
        CmdMeasResultSave.Text = "保存中"
        保存ToolStripMenuItem1.Enabled = False
        保存ToolStripMenuItem1.Text = "保存中"

        Dim Ret As DialogResult
        Dim FilePath As String = ""
        Dim SaveDate As String
        Dim SaveTime As String
        Dim SaveDefFileName As String

        Dim excelApp As New Excel.Application
        Dim excelBooks As Excel.Workbooks = excelApp.Workbooks
        Dim excelBook As Excel.Workbook = excelBooks.Add()
        Dim sheet As Excel.Worksheet = excelApp.Worksheets("sheet1")

        Try
            Using dialog As New SaveFileDialog
                With dialog
                    .InitialDirectory = SG_ResultSave_path

                    '.RestoreDirectory = True
                    .Title = "測定結果保存"
                    .Filter = "Excelファイル(*.xlsx)|*.xlsx"

                    SaveDate = Now.ToString("yyyyMMdd")
                    SaveTime = Now.ToString("HHmmss")
                    SaveDefFileName = SaveDate & SaveTime & ".xlsx"

                    .FileName = SaveDefFileName

                    Ret = .ShowDialog

                    If Ret = DialogResult.OK Then
                        FilePath = .FileName

                        SG_ResultSave_path = Path.GetDirectoryName(FilePath)
                        My.Settings._sgresultsave_path = SG_ResultSave_path
                        My.Settings.Save()

                        excelApp.Visible = False

                        '測定結果
                        If FlgAdmin = 0 Then
                            '通常モード時
                            With sheet
                                .Cells.Locked = False
                                If frm_MeasForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                                    .Cells.Interior.Color = frm_MeasForm_bc
                                End If

                                .Cells(1, 1) = My.Application.Info.ProductName & " シングルシート"
                                .Range(.Cells(1, 1), .Cells(1, 1)).Font.Color = frm_MeasForm_fc
                                .Cells(2, 1) = "測定データ 測定　日付：" & DataDate_cur & "  時間：" & DataTime_cur
                                .Range(.Cells(2, 1), .Cells(2, 1)).Font.Color = frm_MeasCurData_color
                                .Range(.Cells(1, 1), .Cells(2, 1)).Locked = True

                                .Cells(4, 2) = "マシーンNo."
                                .Cells(4, 3) = "サンプル名"
                                If FlgDBF = 1 Then
                                    .Cells(4, 4) = "マーク"
                                    .Cells(4, 5) = "測定回数"
                                    .Range(.Cells(4, 2), .Cells(4, 5)).Font.Color = frm_MeasForm_fc
                                Else
                                    .Cells(4, 4) = "測定回数"
                                    .Range(.Cells(4, 2), .Cells(4, 4)).Font.Color = frm_MeasForm_fc
                                    .Cells(5, 1) = "測定仕様"
                                End If
                                .Cells(5, 2) = TxtMachNoCur.Text
                                .Cells(5, 3) = TxtSmplNamCur.Text
                                If FlgDBF = 1 Then
                                    .Cells(5, 4) = TxtMarkCur.Text
                                    .Cells(5, 5) = TxtMeasNumCur.Text
                                    .Range(.Cells(5, 1), .Cells(5, 5)).Font.Color = frm_MeasCurData_color
                                    .Range(.Cells(4, 1), .Cells(5, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(4, 1), .Cells(5, 5)).Locked = True
                                Else
                                    .Cells(5, 4) = TxtMeasNumCur.Text
                                    .Range(.Cells(5, 1), .Cells(5, 4)).Font.Color = frm_MeasCurData_color
                                    .Range(.Cells(4, 1), .Cells(5, 4)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(4, 1), .Cells(5, 4)).Locked = True
                                End If
                                .Range(.Cells(7, 1), .Cells(8, 1)).MergeCells = True
                                .Cells(7, 1) = "データ"
                                .Range(.Cells(7, 2), .Cells(8, 2)).MergeCells = True
                                .Cells(7, 2) = "測定No."
                                .Range(.Cells(7, 3), .Cells(7, 4)).MergeCells = True
                                .Cells(7, 3) = "配向角[deg.]"
                                .Cells(8, 3) = "Peak"
                                .Cells(8, 4) = "Deep"
                                .Range(.Cells(7, 5), .Cells(7, 6)).MergeCells = True
                                .Cells(7, 5) = "配向比"
                                .Cells(8, 5) = "MD/CD"
                                .Cells(8, 6) = "Peak/Deep"
                                .Range(.Cells(7, 7), .Cells(7, 8)).MergeCells = True
                                .Cells(7, 7) = "伝播速度[Km/S]"
                                .Cells(8, 7) = "MD"
                                .Cells(8, 8) = "CD"
                                .Range(.Cells(7, 9), .Cells(7, 10)).MergeCells = True
                                .Cells(7, 9) = "伝播速度[Km/S]"
                                .Cells(8, 9) = "Peak"
                                .Cells(8, 10) = "Deep"
                                .Range(.Cells(7, 11), .Cells(7, 12)).MergeCells = True
                                .Cells(7, 11) = "TSI(Km/S)^2"
                                .Cells(8, 11) = "MD"
                                .Cells(8, 12) = "CD"
                                .Range(.Cells(7, 1), .Cells(8, 12)).Font.Color = frm_MeasForm_fc
                                .Cells(9, 1) = "測定データ"
                                .Cells(9, 2) = LblMeasNumCur_nom.Text
                                .Cells(9, 3) = LblAnglPeakCur_nom.Text
                                .Cells(9, 4) = LblAnglDeepCur_nom.Text
                                .Cells(9, 5) = LblratioMDCDCur_nom.Text
                                .Cells(9, 6) = LblratioPKDPCur_nom.Text
                                .Cells(9, 7) = LblSpdMDCur_nom.Text
                                .Cells(9, 8) = LblSpdCDCur_nom.Text
                                .Cells(9, 9) = LblSpdPeakCur_nom.Text
                                .Cells(9, 10) = LblSpdDeepCur_nom.Text
                                .Cells(9, 11) = LblTSIMDCur_nom.Text
                                .Cells(9, 12) = LblTSICDCur_nom.Text
                                .Range(.Cells(9, 1), .Cells(9, 12)).Font.Color = frm_MeasCurData_color
                                .Range(.Cells(7, 1), .Cells(9, 12)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(7, 1), .Cells(9, 12)).Locked = True

                                .Range(.Cells(11, 10), .Cells(12, 10)).MergeCells = True
                                .Cells(11, 10) = "角度[deg.]"
                                .Cells(11, 11) = "伝播速度[Km/S]"
                                .Range(.Cells(11, 10), .Cells(11, 11)).Font.Color = frm_MeasForm_fc
                                .Cells(13, 10) = "0.00"
                                .Cells(14, 10) = "11.25"
                                .Cells(15, 10) = "22.50"
                                .Cells(16, 10) = "33.75"
                                .Cells(17, 10) = "45.00"
                                .Cells(18, 10) = "56.25"
                                .Cells(19, 10) = "67.50"
                                .Cells(20, 10) = "78.75"
                                .Cells(21, 10) = "90.00"
                                .Cells(22, 10) = "101.25"
                                .Cells(23, 10) = "112.50"
                                .Cells(24, 10) = "123.75"
                                .Cells(25, 10) = "135.00"
                                .Cells(26, 10) = "146.25"
                                .Cells(27, 10) = "157.50"
                                .Cells(28, 10) = "168.75"
                                .Range(.Cells(13, 10), .Cells(28, 10)).Font.Color = frm_MeasForm_fc

                                .Cells(12, 11) = "測定データ"
                                .Cells(13, 11) = LblMeasDatCur1_nom.Text
                                .Cells(14, 11) = LblMeasDatCur2_nom.Text
                                .Cells(15, 11) = LblMeasDatCur3_nom.Text
                                .Cells(16, 11) = LblMeasDatCur4_nom.Text
                                .Cells(17, 11) = LblMeasDatCur5_nom.Text
                                .Cells(18, 11) = LblMeasDatCur6_nom.Text
                                .Cells(19, 11) = LblMeasDatCur7_nom.Text
                                .Cells(20, 11) = LblMeasDatCur8_nom.Text
                                .Cells(21, 11) = LblMeasDatCur9_nom.Text
                                .Cells(22, 11) = LblMeasDatCur10_nom.Text
                                .Cells(23, 11) = LblMeasDatCur11_nom.Text
                                .Cells(24, 11) = LblMeasDatCur12_nom.Text
                                .Cells(25, 11) = LblMeasDatCur13_nom.Text
                                .Cells(26, 11) = LblMeasDatCur14_nom.Text
                                .Cells(27, 11) = LblMeasDatCur15_nom.Text
                                .Cells(28, 11) = LblMeasDatCur16_nom.Text
                                .Range(.Cells(12, 11), .Cells(28, 11)).Font.Color = frm_MeasCurData_color
                                .Range(.Cells(11, 10), .Cells(28, 11)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(11, 10), .Cells(28, 11)).Locked = True

                                'グラフを画像として貼り付ける
                                Dim bmp As New Bitmap(PictureBox1.Width, PictureBox1.Height)
                                bmp.MakeTransparent(BackColor)
                                PictureBox1.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))

                                bmp.Save(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Shapes.AddPicture(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp",
                                                   MsoTriState.msoFalse,
                                                   MsoTriState.msoTrue,
                                                   .Cells(11, 1).Left,
                                                   .Cells(11, 1).Top,
                                                   bmp.Width * 0.8,
                                                   bmp.Height * 0.8)
                                bmp.Dispose()
                                File.Delete(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Protect()
                            End With
                        Else
                            '管理者モード時
                            With sheet
                                .Cells.Locked = False
                                If frm_MeasForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                                    .Cells.Interior.Color = frm_MeasForm_bc
                                End If

                                .Cells(1, 1) = My.Application.Info.ProductName & " シングルシート"
                                .Range(.Cells(1, 1), .Cells(1, 1)).Font.Color = frm_MeasForm_fc
                                .Cells(2, 1) = "測定データ 測定　日付：" & DataDate_cur & "  時間：" & DataTime_cur
                                .Range(.Cells(2, 1), .Cells(2, 1)).Font.Color = frm_MeasCurData_color
                                .Cells(3, 1) = "過去データ 測定　日付：" & DataDate_bak & "  時間：" & DataTime_bak
                                .Range(.Cells(3, 1), .Cells(3, 1)).Font.Color = frm_MeasOldData_color
                                .Range(.Cells(1, 1), .Cells(3, 1)).Locked = True

                                .Cells(5, 2) = "マシーンNo."
                                .Cells(5, 3) = "サンプル名"
                                If FlgDBF = 1 Then
                                    .Cells(5, 4) = "マーク"
                                    .Cells(5, 5) = "測定回数"
                                    .Range(.Cells(5, 2), .Cells(5, 5)).Font.Color = frm_MeasForm_fc
                                Else
                                    .Cells(5, 4) = "測定回数"
                                    .Range(.Cells(5, 2), .Cells(5, 4)).Font.Color = frm_MeasForm_fc
                                End If
                                .Cells(6, 1) = "測定仕様"
                                .Cells(6, 2) = TxtMachNoCur.Text
                                .Cells(6, 3) = TxtSmplNamCur.Text
                                If FlgDBF = 1 Then
                                    .Cells(6, 4) = TxtMarkCur.Text
                                    .Cells(6, 5) = TxtMeasNumCur.Text
                                    .Range(.Cells(6, 1), .Cells(6, 5)).Font.Color = frm_MeasCurData_color
                                Else
                                    .Cells(6, 4) = TxtMeasNumCur.Text
                                    .Range(.Cells(6, 1), .Cells(6, 4)).Font.Color = frm_MeasCurData_color
                                End If
                                .Cells(7, 1) = "過去の仕様"
                                .Cells(7, 2) = TxtMachNoBak.Text
                                .Cells(7, 3) = TxtSmplNamBak.Text
                                .Cells(7, 4) = TxtMarkBak.Text
                                .Cells(7, 5) = TxtMeasNumBak.Text
                                .Range(.Cells(7, 1), .Cells(7, 5)).Font.Color = frm_MeasOldData_color
                                .Range(.Cells(5, 1), .Cells(7, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(5, 1), .Cells(7, 5)).Locked = True

                                .Range(.Cells(9, 1), .Cells(10, 1)).MergeCells = True
                                .Cells(9, 1) = "データ"
                                .Range(.Cells(9, 2), .Cells(10, 2)).MergeCells = True
                                .Cells(9, 2) = "測定No."
                                .Range(.Cells(9, 3), .Cells(9, 4)).MergeCells = True
                                .Cells(9, 3) = "配向角[deg.]"
                                .Cells(10, 3) = "Peak"
                                .Cells(10, 4) = "Deep"
                                .Range(.Cells(9, 5), .Cells(9, 6)).MergeCells = True
                                .Cells(9, 5) = "配向比"
                                .Cells(10, 5) = "MD/CD"
                                .Cells(10, 6) = "Peak/Deep"
                                .Range(.Cells(9, 7), .Cells(9, 8)).MergeCells = True
                                .Cells(9, 7) = "伝播速度[Km/S]"
                                .Cells(10, 7) = "MD"
                                .Cells(10, 8) = "CD"
                                .Range(.Cells(9, 9), .Cells(9, 10)).MergeCells = True
                                .Cells(9, 9) = "伝播速度[Km/S]"
                                .Cells(10, 9) = "Peak"
                                .Cells(10, 10) = "Deep"
                                .Range(.Cells(9, 11), .Cells(9, 12)).MergeCells = True
                                .Cells(9, 11) = "TSI(Km/S)^2"
                                .Cells(10, 11) = "MD"
                                .Cells(10, 12) = "CD"
                                .Range(.Cells(9, 1), .Cells(10, 12)).Font.Color = frm_MeasForm_fc
                                .Cells(11, 1) = "測定データ"
                                .Cells(11, 2) = LblMeasNumCur_adm.Text
                                .Cells(11, 3) = LblAnglPeakCur_adm.Text
                                .Cells(11, 4) = LblAnglDeepCur_adm.Text
                                .Cells(11, 5) = LblratioMDCDCur_adm.Text
                                .Cells(11, 6) = LblratioPKDPCur_adm.Text
                                .Cells(11, 7) = LblSpdMDCur_adm.Text
                                .Cells(11, 8) = LblSpdCDCur_adm.Text
                                .Cells(11, 9) = LblSpdPeakCur_adm.Text
                                .Cells(11, 10) = LblSpdDeepCur_adm.Text
                                .Cells(11, 11) = LblTSIMDCur_adm.Text
                                .Cells(11, 12) = LblTSICDCur_adm.Text
                                .Range(.Cells(11, 1), .Cells(11, 12)).Font.Color = frm_MeasCurData_color
                                .Cells(12, 1) = "過去データ"
                                .Cells(12, 2) = LblMeasNumBak_adm.Text
                                .Cells(12, 3) = LblAnglPeakBak_adm.Text
                                .Cells(12, 4) = LblAnglDeepBak_adm.Text
                                .Cells(12, 5) = LblratioMDCDBak_adm.Text
                                .Cells(12, 6) = LblratioPKDPBak_adm.Text
                                .Cells(12, 7) = LblSpdMDBak_adm.Text
                                .Cells(12, 8) = LblSpdCDBak_adm.Text
                                .Cells(12, 9) = LblSpdPeakBak_adm.Text
                                .Cells(12, 10) = LblSpdDeepBak_adm.Text
                                .Cells(12, 11) = LblTSIMDBak_adm.Text
                                .Cells(12, 12) = LblTSICDBak_adm.Text
                                .Range(.Cells(12, 1), .Cells(12, 12)).Font.Color = frm_MeasOldData_color
                                .Range(.Cells(9, 1), .Cells(12, 12)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(9, 1), .Cells(12, 12)).Locked = True

                                .Range(.Cells(14, 10), .Cells(15, 10)).MergeCells = True
                                .Cells(14, 10) = "角度[deg.]"
                                .Range(.Cells(14, 11), .Cells(14, 12)).MergeCells = True
                                .Cells(14, 11) = "伝播速度[Km/S]"
                                .Range(.Cells(14, 10), .Cells(14, 11)).Font.Color = frm_MeasForm_fc
                                .Cells(16, 10) = "0.00"
                                .Cells(17, 10) = "11.25"
                                .Cells(18, 10) = "22.50"
                                .Cells(19, 10) = "33.75"
                                .Cells(20, 10) = "45.00"
                                .Cells(21, 10) = "56.25"
                                .Cells(22, 10) = "67.50"
                                .Cells(23, 10) = "78.75"
                                .Cells(24, 10) = "90.00"
                                .Cells(25, 10) = "101.25"
                                .Cells(26, 10) = "112.50"
                                .Cells(27, 10) = "123.75"
                                .Cells(28, 10) = "135.00"
                                .Cells(29, 10) = "146.25"
                                .Cells(30, 10) = "157.50"
                                .Cells(31, 10) = "168.75"
                                .Range(.Cells(16, 10), .Cells(31, 10)).Font.Color = frm_MeasForm_fc

                                .Cells(15, 11) = "測定データ"
                                .Cells(16, 11) = LblMeasDatCur1_adm.Text
                                .Cells(17, 11) = LblMeasDatCur2_adm.Text
                                .Cells(18, 11) = LblMeasDatCur3_adm.Text
                                .Cells(19, 11) = LblMeasDatCur4_adm.Text
                                .Cells(20, 11) = LblMeasDatCur5_adm.Text
                                .Cells(21, 11) = LblMeasDatCur6_adm.Text
                                .Cells(22, 11) = LblMeasDatCur7_adm.Text
                                .Cells(23, 11) = LblMeasDatCur8_adm.Text
                                .Cells(24, 11) = LblMeasDatCur9_adm.Text
                                .Cells(25, 11) = LblMeasDatCur10_adm.Text
                                .Cells(26, 11) = LblMeasDatCur11_adm.Text
                                .Cells(27, 11) = LblMeasDatCur12_adm.Text
                                .Cells(28, 11) = LblMeasDatCur13_adm.Text
                                .Cells(29, 11) = LblMeasDatCur14_adm.Text
                                .Cells(30, 11) = LblMeasDatCur15_adm.Text
                                .Cells(31, 11) = LblMeasDatCur16_adm.Text
                                .Range(.Cells(15, 11), .Cells(31, 11)).Font.Color = frm_MeasCurData_color

                                .Cells(15, 12) = "過去データ"
                                .Cells(16, 12) = LblMeasDatBak1_adm.Text
                                .Cells(17, 12) = LblMeasDatBak2_adm.Text
                                .Cells(18, 12) = LblMeasDatBak3_adm.Text
                                .Cells(19, 12) = LblMeasDatBak4_adm.Text
                                .Cells(20, 12) = LblMeasDatBak5_adm.Text
                                .Cells(21, 12) = LblMeasDatBak6_adm.Text
                                .Cells(22, 12) = LblMeasDatBak7_adm.Text
                                .Cells(23, 12) = LblMeasDatBak8_adm.Text
                                .Cells(24, 12) = LblMeasDatBak9_adm.Text
                                .Cells(25, 12) = LblMeasDatBak10_adm.Text
                                .Cells(26, 12) = LblMeasDatBak11_adm.Text
                                .Cells(27, 12) = LblMeasDatBak12_adm.Text
                                .Cells(28, 12) = LblMeasDatBak13_adm.Text
                                .Cells(29, 12) = LblMeasDatBak14_adm.Text
                                .Cells(30, 12) = LblMeasDatBak15_adm.Text
                                .Cells(31, 12) = LblMeasDatBak16_adm.Text
                                .Range(.Cells(15, 12), .Cells(31, 12)).Font.Color = frm_MeasOldData_color
                                .Range(.Cells(14, 10), .Cells(31, 12)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(14, 10), .Cells(31, 12)).Locked = True

                                'グラフを画像として貼り付ける
                                Dim bmp As New Bitmap(PictureBox1.Width, PictureBox1.Height)
                                PictureBox1.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))
                                bmp.Save(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Shapes.AddPicture(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp",
                                                MsoTriState.msoFalse,
                                                MsoTriState.msoTrue,
                                                .Cells(14, 1).Left,
                                                .Cells(14, 1).Top,
                                                bmp.Width * 0.9,
                                                bmp.Height * 0.9)

                                bmp.Dispose()
                                File.Delete(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Protect()
                            End With
                        End If

                        sheet.Activate()

                        '保存する
                        excelApp.DisplayAlerts = False
                        excelBook.SaveAs(FilePath)
                        excelApp.DisplayAlerts = True
                    End If
                End With
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            excelBook.Close()
            excelApp.Quit()
            Marshal.ReleaseComObject(sheet)
            Marshal.ReleaseComObject(excelBook)
            Marshal.ReleaseComObject(excelApp)

            CmdMeasResultSave.Text = "保　存"
            CmdMeasResultSave.Enabled = True
            保存ToolStripMenuItem1.Text = "保存"
            保存ToolStripMenuItem1.Enabled = True
        End Try
    End Sub

    Private Sub CmdMeasResultSave_Click(sender As Object, e As EventArgs) Handles CmdMeasResultSave.Click
        MeasResultSave()
    End Sub

    Private Sub FrmSST4500_1_0_0E_meas_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        FlgMainMeas = 90
    End Sub

    Private Sub 選択ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChoiceToolStripMenuItem.Click
        SelConstMeas()
    End Sub

    Private Sub 保存ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveConstMeas()
    End Sub

    Private Sub 読込ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        FlgMainMeas = 40
    End Sub

    Private Sub 他の測定データ選択ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnotherMeasDataSelToolStripMenuItem.Click
        FlgMainMeas = 41
    End Sub

    Private Sub 終了ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        FlgMainMeas = 90
    End Sub

    Private Sub 測定開始ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 測定開始ToolStripMenuItem.Click
        If FlgTest = 1 Then
            FlgTest = 2
        End If

        FlgMainMeas = 2
    End Sub

    Private Sub 他の測定データ選択ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles 他の測定データ選択ToolStripMenuItem1.Click
        FlgMainMeas = 10
    End Sub

    Private Sub 印刷ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 印刷ToolStripMenuItem.Click
        Menu_AutoPrn.Checked = Not Menu_AutoPrn.Checked
        If Menu_AutoPrn.Checked = True Then
            If ChkMeasAutoPrn.Checked = False Then
                ChkMeasAutoPrn.Checked = True
            End If
        Else
            If ChkMeasAutoPrn.Checked = True Then
                ChkMeasAutoPrn.Checked = False
            End If
        End If
    End Sub

    Private Sub 手動印刷ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 手動印刷ToolStripMenuItem.Click
        PrintoutMeas()
    End Sub

    Private Sub 保存ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles 保存ToolStripMenuItem1.Click
        MeasResultSave()
    End Sub

    Private Sub 設定ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles 設定ToolStripMenuItem1.Click
        FrmSST4500_1_0_0E_setting.Visible = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        FrmSST4500_1_0_0E_colorsetting.Visible = True
    End Sub

    Private Sub SST4500についてToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SST4500についてToolStripMenuItem.Click
        FrmSST4500_1_0_0E_helpinfo.ShowDialog()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        FrmSST4500_1_0_0E_helpinfo.ShowDialog()
    End Sub

    Private Sub meas_dbf_chg(ByVal sw As Integer)
        Select Case sw
            Case 0  '通常
                Label1.Visible = False
                TxtSmplNamCur.Width = TXTSMPWIDTH_0 + TXTSMPWIDTH_add
                TxtMarkCur.Visible = False
                TxtSmplNamBak.Width = TXTSMPWIDTH_0 + TXTSMPWIDTH_add
                TxtMarkBak.Visible = False
            Case 1  '特殊1
                Label1.Visible = True
                TxtSmplNamCur.Width = TXTSMPWIDTH_0
                TxtMarkCur.Visible = True
                TxtSmplNamBak.Width = TXTSMPWIDTH_0
                TxtMarkBak.Visible = True
        End Select
    End Sub

    Private Sub CmdClsGraph_Click(sender As Object, e As EventArgs) Handles CmdClsGraph.Click
        DrawGraphCurData_clear()
        DrawGraphBakData_clear()
        DrawGraph_init()
        DrawCalcCurData_init()
        DrawMeasCurData_init()
        DrawCalcBakData_init()
        DrawMeasBakData_init()
        GraphInitMeas()
        ClsBakInfoMeas()

        ClsNoMeas()
        ClsData()

        timerCount1 = 0
        FileNumConst = 0
        FileNumData = 0
    End Sub
End Class