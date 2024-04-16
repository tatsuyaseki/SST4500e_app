Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Text
Imports System.Drawing.Printing
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports Microsoft.Office.Core
'Imports Microsoft.Office.Interop.Excel

Public Class FrmSST4500_1_0_0E_Profile
    Const Rad = 3.141592654 / 180
    'Const LnCmp = 420    '両端補正値
    'Const min_Pitch = 10    '最小ピッチ
    'Const min_Points = 2
    'Const max_Pitch = 9999

    Const graph_x_sta = 50
    Const graph_x_end = 600

    '配向角グラフ描画
    Const angle_yaxis_max = 268
    Const angle_yaxis_min = 18
    Const angle_SclY = 25

    '配向比グラフ描画
    Const ratio_yaxis_max = 268
    Const ratio_yaxis_min = 18
    Const ratio_SclY = 50

    '伝播速度グラフ描画
    Const velo_yaxis_max = 268
    Const velo_yaxis_min = 18
    Const velo_SclY = 50

    'TSIグラフ描画
    Const tsi_yaxis_max = 268
    Const tsi_yaxis_min = 18
    Const tsi_SclY = 50

    'MDLong
    Const lg_graph_max = 250
    'Const lg_graph_max = 25    'デバッグ用
    Const lg_stepscale = lg_graph_max / 5
    Const lg_def_shiftxnum = lg_stepscale * 3
    'Const lg_def_shiftxnum = lg_stepscale * 1
    Const lg_shiftx_ss = lg_stepscale * 8
    Const lg_sample_max = 30000
    'Const lg_sample_max = 41    'デバッグ用

    Dim result As DialogResult
    Dim result2 As Integer
    Dim fname As String = ""
    Dim M As Integer
    Dim N As Integer
    Dim Kp As Integer
    Dim Fn As Integer
    Dim Kt1 As Long
    Dim Kt2 As Long
    Dim Kt3 As Integer
    Dim Kt4 As Long
    Dim Kt5 As Long
    Dim Ms As Single
    Dim Ns As Single
    Dim JS As String
    Dim Ks As String
    Dim Ks_1 As String
    Dim Ls As String
    Dim Ls_1 As String
    Dim Es As Object
    Dim HsbVal As Long
    Dim KshiftX As Integer
    Dim Kx As Single

    Dim _flgRx As Integer

    Dim curPrnPageNumber As Integer
    Dim curPrnDataNumber As Integer
    Dim curPrnRow As Integer
    Dim targetPrnRow As Integer

    Dim groupMenuUnit As ToolStripMenuItem() = New ToolStripMenuItem() {
        Me.MmToolStripMenuItem,
        Me.InchToolStripMenuItem}
    Dim Menu_AutoPrn As ToolStripMenuItem
    Dim MenuPrn_AngleRatio As ToolStripMenuItem
    Dim MenuPrn_VeloTSI As ToolStripMenuItem
    Dim MenuPrn_measData As ToolStripMenuItem
    Dim MenuPrn_OldData As ToolStripMenuItem
    Dim MenuPrn_AvgData As ToolStripMenuItem

    'Dim title_text As String
    Public FlgInitEnd As Integer = 0
    Dim _length_bak As Single
    Dim _pitch_bak As Single
    Dim _points_bak As Integer
    Dim _ret As Integer

    Dim _points As Single

    Private Sub TimProfile_Tick(sender As Object, e As EventArgs) Handles TimProfile.Tick

        Select Case FlgMainProfile
            Case 1  '初期化
                If FlgAdmin <> 0 Then
                    '管理者モード
                    AdmVisible_onoff(True)
                Else
                    '通常モード
                    AdmVisible_onoff(False)
                End If

                Select Case FlgProfile
                    Case 1
                        Me.Text = My.Application.Info.ProductName & " Profile (Ver:" & My.Application.Info.Version.ToString & ")"
                        LblPrfTitle.Text = "Profile"
                    Case 2
                        Me.Text = My.Application.Info.ProductName & " Cut Sheet (Ver:" & My.Application.Info.Version.ToString & ")"
                        LblPrfTitle.Text = "Cut Sheet"
                    Case 3
                        Me.Text = My.Application.Info.ProductName & " MD Long Sample (Ver:" & My.Application.Info.Version.ToString & ")"
                        LblPrfTitle.Text = "MD Long Sample"
                    Case Else
                        Me.Text = My.Application.Info.ProductName & " Profile (Ver:" & My.Application.Info.Version.ToString & ")"
                        LblPrfTitle.Text = "Profile"
                End Select
                title_text2 = Me.Text
                LblProductNamePrf.Text = My.Application.Info.ProductName

                '一旦タイマーを止める ※ファイル選択ダイアログが出続けてしまう為
                TimProfile.Enabled = False

                '測定仕様ファイルの選択
                result = LoadDefConstName(fname, True)

                If result = DialogResult.OK Then
                    StrConstFileName = fname

                    LoadConst(Me, title_text2)

                    If FlgPitchExp = 1 Then
                        'ピッチ拡張が有効な場合
                        LoadConstPitch(PchExpSettingFile_FullPath)
                        If FlgPitchExp_Load = 1 Then
                            TxtLength.Text = PchExp_Length
                            TxtPitch.Text = PchExp_PchData(0)
                            _points = UBound(PchExp_PchData) + 2
                            TxtPoints.Text = _points
                            'TxtPoints.Text = UBound(PchExp_PchData) + 2
                        End If
                    Else
                        FlgPitchExp_Load = 0
                    End If

                    '----
                    SetConst_Menu()

                    ClsNoPrf()

                    FileNo = 0
                    MeasNo = 0

                    If FlgProfile = 3 Then
                        Points = lg_graph_max
                        TxtPoints.Text = Points
                    End If

                    DrawCalcCurData_init()
                    DrawCalcBakData_init()
                    DrawCalcAvgData_init()
                    DrawTableData_init()
                    If FlgPitchExp = 1 Then
                        GraphInitPrf(_points)
                    Else
                        GraphInitPrf(Points)
                    End If

                    LblAngCenter.Text = PkAngCent
                    LblAngCenter.Visible = True

                    'CmdMeas.Enabled = True
                    'CmdMeasButton_set(_rdy)
                    'CmdMeas.Text = "測定開始"
                    '測定開始ToolStripMenuItem.Enabled = True
                    '測定開始ToolStripMenuItem.Text = "測定開始"
                    '測定中断ToolStripMenuItem.Enabled = False
                    '終了ToolStripMenuItem.Enabled = True
                    MeasStartInit()

                    If FlgAdmin <> 0 Then
                        OldDataToolStripMenuItem.Enabled = True
                        CmdOldDataLoad.Enabled = True
                        LoadToolStripMenuItem.Enabled = True
                    End If

                    ToolStripStatusLabel4.Text = "Ready "

                    TxtMeasLotCur.Text = 0

                    HScrollBar1.Visible = False
                    HScrollBar1.Enabled = False
                    HScrollBar2.Visible = False
                    HScrollBar2.Enabled = False

                    SetPrintChk()

                    FlgScroll = 0
                    FlgHoldMeas = 0
                    FlgMainProfile = 0

                ElseIf result = DialogResult.Cancel Then
                    'FlgMainProfile = 1
                    Visible = False
                    FlgMainSplash = 0
                    FlgMainProfile = 0
                    FrmSST4500_1_0_0E_main.Visible = True
                End If

                timerCount1 = 0
                TimProfile.Enabled = True
                FlgInitEnd = 1

            Case 2
                '測定開始
                FlgHoldMeas = 1
                CmdMeas.Enabled = False
                MeasStartToolStripMenuItem.Enabled = False
                MeasStopToolStripMenuItem.Enabled = True
                ConditionDisable()

                HScrollBar1.Visible = False
                HScrollBar1.Enabled = False
                HScrollBar2.Visible = False
                HScrollBar2.Enabled = False

                KdData = 1
                SampleNo = 0
                FileNo = 0
                FlgStop = 0

                FlgPkcd = 0
                FlgDpmd = 0

                If FlgProfile = 3 Then
                    CmdQuitProfile.Text = StrMeasStop
                ElseIf FlgProfile = 1 Then
                    CmdQuitProfile.Text = StrMeasInterrupt
                ElseIf FlgProfile = 2 Then
                    CmdQuitProfile.Text = StrMeasInterrupt
                End If
                QuitToolStripMenuItem.Enabled = False

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

                MachineNo = TxtMachNoCur.Text
                Sample = TxtSmplNamCur.Text
                Mark = TxtMarkCur.Text

                OpenDataFile()

                MeasNo += 1
                M = MeasNo

                SaveDataTitle()

                If FlgPitchExp = 0 Then
                    'Points = Val(TxtPoints.Text)
                    'Pitch = Val(TxtPitch.Text)
                    FlgPchExpMes = 0    'ピッチ拡張無効で測定
                Else

                    Points = UBound(PchExp_PchData) + 2 'ピッチ拡張のデータ数
                    '測定開始時は、サンプル長さ、ピッチ、測定個所数はピッチ拡張設定の内容に変わっているので
                    'バックアップは別の場所でする必要がある
                    '_length_bak = Val(TxtLength.Text)
                    '_pitch_bak = Val(TxtPitch.Text)
                    '_points_bak = Val(TxtPoints.Text)
                    'TxtPoints.Text = Points
                    'ピッチはPchExp_PchData()から取得する
                    FlgPchExpMes = 1    'ピッチ拡張有効で測定
                End If

                DrawCalcCurData_init()
                DrawCalcBakData_init()
                DrawCalcAvgData_init()
                DrawTableData_init()
                GraphInitPrf(Points)
                Cls_PchExpOld()

                KdData = 1
                InitializeMaxMinInt()

                MeasNo = M
                TxtMeasLotCur.Text = MeasNo

                If FlgTest = 0 Then
                    FlgMainProfile = 3
                Else
                    FlgMainProfile = 100
                End If

            Case 3
                UsbOpen()

                If FlgProfile = 2 Then
                    TxtPitch.Text = 100
                End If

                'ピッチ拡張設定時
                If FlgPitchExp <> 0 Then
                    Pitch = PchExp_PchData(SampleNo)
                End If

                'Pitch送信
                _ret = SendPch2(Pitch)

                If _ret = 1 Then
                    FlgMainProfile = 4
                Else
                    FlgMainProfile = 99
                End If

                'timerCount1 = 0
                'FlgMainProfile = 301

            Case 301
                'Pitch送信の返信
                strRxdata = ""
                _flgRx = UsbRead(strRxdata)

                If _flgRx = 0 Then
                    timerCount1 = 0
                    FlgMainProfile = 4
                Else
                    If timerCount1 >= cmd_timeout Then
                        'コマンドタイムアウトエラー
                        FlgMainProfile = 99
                    Else
                        timerCount1 += 1
                    End If
                End If

            Case 399
                'ピッチ拡張設定時の2回目以降の測定開始前のピッチ設定
                Pitch = PchExp_PchData(SampleNo)

                _ret = SendPch2(Pitch)

                If _ret = 1 Then
                    FlgMainProfile = 4
                Else
                    FlgMainProfile = 99
                End If

            Case 4
                CmdMeas.Enabled = False

                If FlgPitchExp <> 0 Then
                    TxtPitch.Text = Pitch
                End If

                SampleNo += 1
                MeasDataMax = SampleNo
                TxtMeasNumCur.Text = SampleNo

                DataPrcStr(1, SampleNo, 1) = TxtMachNoCur.Text
                DataPrcStr(1, SampleNo, 2) = TxtSmplNamCur.Text
                DataPrcStr(1, SampleNo, 3) = TxtMarkCur.Text
                DataPrcStr(1, SampleNo, 5) = Str(SampleNo)

                strWdata = "MES" & vbCr
                UsbWrite(strWdata)

                timerCount1 = 0
                FlgMainProfile = 401

            Case 401
                strRxdata = ""
                _flgRx = UsbRead(strRxdata)

                If _flgRx = 0 Then
                    If strRxdata = "MEAS" & vbCr Then
                        TimProfile.Enabled = False
                        ToolStripStatusLabel4.Text = StrMeasuring
                        CmdMeasButton_set(_mes)
                        CmdMeas.Text = StrMeasuring
                        MeasStartToolStripMenuItem.Text = StrMeasuring
                        TimProfile.Enabled = True

                        'カットシートの場合、2回目以降が4番から始まる
                        If FlgProfile = 2 Then
                            CmdQuitProfile.Text = StrMeasInterrupt
                            QuitToolStripMenuItem.Enabled = False
                        End If

                        timerCount1 = 0
                        FlgMainProfile = 5
                    Else
                        '基本的にこの状態にはならないはず
                        FlgMainProfile = 99

                    End If
                Else
                    If timerCount1 >= cmd_timeout Then
                        'コマンドタイムアウトエラー
                        FlgMainProfile = 99
                    Else
                        timerCount1 += 1
                    End If
                End If

            Case 5
                timerCount1 += 1
                If timerCount1 Mod 50 = 0 Then
                    ToolStripStatusLabel4.Text &= "o"
                End If

                If timerCount1 >= 600 Then
                    timerCount1 = 0
                    FlgMainProfile = 6
                End If

            Case 6
                timerCount1 += 1

                If timerCount1 Mod 20 = 0 Then
                    ToolStripStatusLabel4.Text &= "->"

                    strRxdata = ""
                    _flgRx = UsbRead(strRxdata)

                    If _flgRx = 0 Then
                        FlgMainProfile = 7
                        If strRxdata <> "" Then
                            strRxdata = Strings.Left(strRxdata, Len(strRxdata) - 1)
                            ToolStripStatusLabel4.Text = strRxdata
                        Else
                            '空欄だったらデータエラー
                            FlgMainProfile = 99
                        End If
                    Else
                        If timerCount1 >= timeout_time Then  '測定は140程度
                            'コマンドタイムアウトエラー
                            FlgMainProfile = 99
                        End If
                    End If
                End If

            Case 7
                '測定結果　受信データ整理と表示
                KdData = 1
                ResolveData()

                KdData = 1
                DataMaxMinInt()

                SaveData()

                KdData = 1
                FlgLine = 2

                PrfSaidDataAngle(0)
                PrfSaidDataRatio(0)
                PrfGraphAngleRatio()
                PrfSaidDataVelo(0)
                PrfSaidDataTSI(0)
                PrfGraphVelocityTSI()

                Select Case FlgProfile
                    Case 1  'プロファイルモード
                        If SampleNo = Points Then
                            FlgMainProfile = 10
                        ElseIf FlgStop = 1 Then
                            FlgMainProfile = 10
                        Else
                            FlgMainProfile = 8
                        End If

                    Case 2  'カットシートプロファイルモード
                        If SampleNo = Points Then
                            FlgMainProfile = 10
                        ElseIf FlgStop = 1 Then
                            FlgMainProfile = 10
                        Else
                            ToolStripStatusLabel4.Text = StrNextSample
                            'CmdMeas.Enabled = True
                            'CmdMeas.Text = "測定開始"
                            'CmdMeasButton_set(_rdy)
                            '測定開始ToolStripMenuItem.Enabled = True
                            '測定開始ToolStripMenuItem.Text = "測定開始"
                            '測定中断ToolStripMenuItem.Enabled = False
                            '終了ToolStripMenuItem.Enabled = True
                            MeasStartInit()

                            CmdQuitProfile.Text = StrQuit
                            FlgHoldMeas = 2
                            FlgMainProfile = 0
                        End If

                    Case 3  'MD長尺サンプルモード
                        If FlgStop = 1 Then
                            FlgMainProfile = 8
                            Points = SampleNo
                        ElseIf SampleNo = lg_sample_max Then
                            FlgStop = 1
                            FlgMainProfile = 8
                            Points = SampleNo
                        ElseIf SampleNo > (lg_stepscale * 2) And (SampleNo - lg_graph_max) Mod lg_def_shiftxnum = 0 Then
                            GraphShift()
                            FlgMainProfile = 8
                        Else
                            FlgMainProfile = 8
                        End If

                End Select

            Case 8
                strWdata = "FED" & vbCr
                UsbWrite(strWdata)

                strRxdata = ""
                timerCount1 = 0
                _flgRx = UsbRead(strRxdata)
                While _flgRx = 1
                    _flgRx = UsbRead(strRxdata)

                End While

                If _flgRx = 0 Then
                    If strRxdata = "FEED" & vbCr Then
                        ToolStripStatusLabel4.Text = StrFeedingSample
                        timerCount1 = 0
                        FlgMainProfile = 9
                    Else
                        '基本的にこの状態にはならないはず
                        'ToolStripStatusLabel4.Text = "サンプル送り中2"
                        'timerCount1 = 0
                        'FlgMainProfile = 9

                        FlgMainProfile = 99
                    End If
                Else
                    FlgMainProfile = 99

                End If


                'FlgMainProfile = 801

            Case 801
                'FED返信
                strRxdata = ""
                _flgRx = UsbRead(strRxdata)

                If _flgRx = 0 Then
                    If strRxdata = "FEED" & vbCr Then
                        ToolStripStatusLabel4.Text = StrFeedingSample
                        timerCount1 = 0
                        FlgMainProfile = 9
                    Else
                        '基本的にこの状態にはならないはず
                        'ToolStripStatusLabel4.Text = "サンプル送り中2"
                        'timerCount1 = 0
                        'FlgMainProfile = 9

                        FlgMainProfile = 99
                    End If
                Else
                    If timerCount1 >= timeout_time Then
                        '測定タイムアウトエラー
                        FlgMainProfile = 99
                    Else
                        timerCount1 += 1
                    End If
                End If

            Case 9
                timerCount1 += 1
                If timerCount1 Mod 20 = 0 Then
                    ToolStripStatusLabel4.Text &= "=>"
                End If

                strRxdata = ""
                _flgRx = UsbRead(strRxdata)

                If _flgRx = 0 Then
                    Select Case FlgProfile
                        Case 1

                            If FlgStop = 1 And strRxdata = "MESF" & vbCr Then
                                FlgMainProfile = 10
                            ElseIf strRxdata = "NOSP" & vbCr Then
                                FlgMainProfile = 10
                            Else
                                If FlgPitchExp = 0 Then
                                    FlgMainProfile = 4
                                Else
                                    If SampleNo = Points - 1 Then
                                        '最後の測定はピッチを更新しない
                                        FlgMainProfile = 4
                                    Else
                                        FlgMainProfile = 399
                                    End If
                                End If
                            End If

                        Case 2
                            'カットシートなので処理なし

                        Case 3
                            If FlgStop = 1 And strRxdata = "MESF" & vbCr Then
                                FlgMainProfile = 10
                            ElseIf strRxdata = "NOSP" & vbCr Then
                                FlgMainProfile = 10
                            ElseIf FlgStop = 0 And strRxdata = "MESF" & vbCr Then
                                FlgMainProfile = 4
                            End If
                    End Select
                Else
                    If timerCount1 >= feed_timeout Then
                        '初期値5000
                        'フィードタイムアウトエラー
                        FlgMainProfile = 99
                    End If
                End If

            Case 10
                '測定終了処理
                ToolStripStatusLabel4.Text = StrMeasCompleted
                CmdQuitProfile.Text = StrQuit
                CmdQuitProfile.Enabled = True
                QuitToolStripMenuItem.Enabled = True
                MeasStopToolStripMenuItem.Enabled = False

                'Points = SampleNo
                '中断で測定を終了した場合、Pointsが中断した時の測定回数に更新すると
                '測定仕様を変更した際にPointsでグラフが初期化されるので不都合が生じるため
                'コメントアウトする

                If FlgProfile = 3 Then
                    ScrollBar_init(SampleNo)
                End If

                FlgMainProfile = 0

                FlgLongMeas = 1

                'CmdMeas.Enabled = True
                'CmdMeas.Text = "測定開始"
                'CmdMeasButton_set(_rdy)
                '測定開始ToolStripMenuItem.Enabled = True
                '測定開始ToolStripMenuItem.Text = "測定開始"
                '測定中断ToolStripMenuItem.Enabled = False
                '終了ToolStripMenuItem.Enabled = True
                MeasStartInit()

                ConditionEnable()

                FlgStop = 0

                If FlgTest = 0 Then
                    UsbClose()
                End If

                FlgHoldMeas = 0

                If FlgPrfAutoPrn = 1 Then
                    TimProfile.Enabled = False
                    PrintoutPrf()
                    TimProfile.Enabled = True
                End If

                '測定完了時に測定データのファイル名を修正する
                '測定を既定の回数しなかった場合に、総測定個所数が実際と異なるため
                '総測定個所数の部分を実際の数に変更する
                If SampleNo > 0 Then
                    DataFileRename(FlgProfile,
                                   cur_dir,
                                   SampleNo,
                                   Trim(MachineNo),
                                   Trim(Sample),
                                   FileDate,
                                   FileTime)
                End If

                'ピッチ拡張時に値を復元
                'If FlgPitchExp <> 0 Then
                'TxtLength.Text = _length_bak
                'TxtPitch.Text = _pitch_bak
                'TxtPoints.Text = _points_bak
                'End If

                'ピッチ拡張時にピッチを最初の値にする
                If FlgPitchExp <> 0 Then
                    TxtPitch.Text = PchExp_PchData(0)
                End If

            Case 20
                ClsNoPrf()

                DrawCalcCurData_init()
                DrawCalcBakData_init()
                DrawCalcAvgData_init()
                DrawTableData_init()
                If FlgPitchExp = 1 Then
                    GraphInitPrf(PchExp_Points)
                Else
                    GraphInitPrf(Points)
                End If

                'CmdMeas.Enabled = True
                'CmdMeasButton_set(_rdy)
                'CmdMeas.Text = "測定開始"
                '測定開始ToolStripMenuItem.Enabled = True
                '測定開始ToolStripMenuItem.Text = "測定開始"
                '測定中断ToolStripMenuItem.Enabled = False
                '終了ToolStripMenuItem.Enabled = True
                MeasStartInit()

                SetPrintChk()

                FlgHoldMeas = 0
                FlgMainProfile = 0

            Case 21
                TimProfile.Enabled = False
                'サンプル長さ

                If FlgInch = 0 Then
                    'mm
                    Length_tmp = TxtLength.Text
                Else
                    'inch
                    Length_tmp = Math.Round(Val(TxtLength.Text) * 25.4, 0, MidpointRounding.AwayFromZero)
                End If

                Pitch_tmp = Math.Round((Length_tmp - LnCmp) / (Points - 1), 0, MidpointRounding.AwayFromZero)


                If Pitch_tmp < min_Pitch Then
                    'ピッチがmin_Pitch未満の場合、ピッチをmin_Pitchに補正して、Pointsを再計算する
                    Pitch_tmp = min_Pitch
                    Points_tmp = Math.Round((Length_tmp - LnCmp) / Pitch_tmp, 0, MidpointRounding.AwayFromZero)
                    If Points_tmp <> TxtPoints.Text Then
                        '再計算の結果、入力値と異なっていたら
                        MessageBox.Show("As the pitch must be more than " & min_Pitch & "mm," & vbCrLf &
                                        "the pitch was corrected to " & min_Pitch & "mm" & vbCrLf &
                                        "Accordingly the total number of meas.points were corrected to " & Points_tmp,
                                        "Correct Pitch and Total Meas.Number of Position",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    Else
                        MessageBox.Show("As the pitch must be more than " & min_Pitch & "mm," & vbCrLf &
                                        "the pitch was corrected to " & min_Pitch & "mm",
                                        "Correct Pitch",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                ElseIf Pitch_tmp > max_Pitch Then
                    'ピッチがmax_Pitch以上の場合、ピッチをmax_Pitchに補正して、Lengthを戻す
                    Pitch_tmp = max_Pitch
                    Points_tmp = Math.Round((Length_tmp - LnCmp) / Pitch_tmp, 0, MidpointRounding.AwayFromZero)
                    If Points_tmp <> TxtPoints.Text Then
                        '再計算の結果、入力値と異なっていたら
                        MessageBox.Show("As the pitch must be less than " & max_Pitch & "mm," & vbCrLf &
                                        "the pitch was corrected to " & max_Pitch & "mm" & vbCrLf &
                                        "Accordingly the total number of meas.points were corrected to " & Points_tmp,
                                        "Correct Pitch and Total Meas.Number of Position",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    Else
                        MessageBox.Show("As the pitch must be more than " & min_Pitch & "mm," & vbCrLf &
                                        "the pitch was corrected to " & min_Pitch & "mm",
                                        "Correct Pitch",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                Else
                    Points_tmp = Math.Round((Length_tmp - LnCmp) / Pitch_tmp, 0, MidpointRounding.AwayFromZero) + 1
                End If

                If Length <> Length_tmp Then
                    If FlgInitEnd = 1 Then
                        ConstChangeTrue(Me, title_text2)
                    End If
                End If

                Length = Length_tmp
                Pitch = Pitch_tmp
                Points = Points_tmp

                If FlgInch = 0 Then
                    TxtPitch.Text = Pitch
                Else
                    TxtPitch.Text = Math.Round(Pitch / 25.4, 2, MidpointRounding.AwayFromZero)
                End If
                TxtPoints.Text = Points

                DrawCalcCurData_init()
                DrawCalcBakData_init()
                DrawCalcAvgData_init()
                DrawTableData_init()
                GraphInitPrf(Points)

                FlgMainProfile = 0
                TimProfile.Enabled = True

            Case 22
                'Points変更

                TimProfile.Enabled = False

                Points_tmp = TxtPoints.Text

                If FlgProfile = 1 Then

                    Pitch_tmp = Math.Round((Length - LnCmp) / (Points_tmp - 1), 0, MidpointRounding.AwayFromZero)
                    'ピッチを計算した結果、min_Pitch未満になった場合、ピッチをmin_Pitchに補正して、Pointsを再計算する
                    If Pitch_tmp < min_Pitch Then
                        Pitch_tmp = min_Pitch
                        Points_tmp = Math.Round((Length - LnCmp) / Pitch_tmp, 0, MidpointRounding.AwayFromZero) + 1
                        If Points_tmp <> TxtPoints.Text Then
                            MessageBox.Show("As the pitch must be more than " & min_Pitch & "mm," & vbCrLf &
                                            "the pitch was corrected to " & min_Pitch & "mm" & vbCrLf &
                                            "Accordingly the total number of meas.points were corrected to " & Points_tmp,
                                            "Correct Pitch and Total Meas.Number of Position",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation)
                        Else
                            MessageBox.Show("As the pitch must be more than " & min_Pitch & "mm," & vbCrLf &
                                            "the pitch was corrected to " & min_Pitch & "mm",
                                            "Correct Pitch",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation)
                        End If
                    ElseIf Pitch_tmp > max_Pitch Then
                        Points_tmp = Points
                        Pitch_tmp = Pitch
                        MessageBox.Show("As the pitch must be less than " & max_Pitch & "mm," & vbCrLf &
                                        "the total number of meas.points was corrected to " & Points,
                                        "Correct Pitch",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    Else
                        Points_tmp = Math.Round((Length - LnCmp) / Pitch_tmp, 0, MidpointRounding.AwayFromZero) + 1
                    End If

                    If Points_tmp <> Points Then
                        If FlgInitEnd = 1 Then
                            ConstChangeTrue(Me, title_text2)
                        End If
                    End If

                    Pitch = Pitch_tmp

                    If FlgInch = 0 Then
                        TxtPitch.Text = Pitch
                    Else
                        TxtPitch.Text = Math.Round(Pitch / 25.4, 2, MidpointRounding.AwayFromZero)
                    End If

                End If

                Points = Points_tmp
                TxtPoints.Text = Points

                DrawCalcCurData_init()
                DrawCalcBakData_init()
                DrawCalcAvgData_init()
                DrawTableData_init()
                GraphInitPrf(Points)

                FlgMainProfile = 0
                TimProfile.Enabled = True

            Case 23
                'Pitch変更
                TimProfile.Enabled = False

                If FlgProfile = 1 Then
                    If FlgInch = 0 Then
                        Pitch_tmp = TxtPitch.Text
                    Else
                        Pitch_tmp = Math.Round(Val(TxtPitch.Text) * 25.4, 0, MidpointRounding.AwayFromZero)
                    End If

                    Points_tmp = Math.Round((Length - LnCmp) / Pitch_tmp, 0, MidpointRounding.AwayFromZero) + 1
                    If Points_tmp < min_Points Then
                        Points_tmp = min_Points
                        Pitch_tmp = Length - LnCmp
                        MessageBox.Show("As the total meas.points must be more than " & min_Points & "," & vbCrLf &
                                        "the points was corrected to " & min_Points & " and the pitch was corrected to" & Pitch & "mm." & vbCrLf &
                                        "Further, the pitch ws corrected to " & Pitch_tmp & "mm by recalculation.",
                                        "Correct Pitch",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    Else
                        '計算で出たPointsで再度Pitchを計算する
                        Pitch_tmp = Math.Round((Length - LnCmp) / (Points_tmp - 1), 0, MidpointRounding.AwayFromZero)
                        If Pitch_tmp > max_Pitch Then
                            Points_tmp = Points
                            Pitch_tmp = Pitch
                            MessageBox.Show("As the pitch must be more than " & min_Pitch & "mm," & vbCrLf &
                                            "the pitch was corrected to " & min_Pitch & "mm",
                                            "Correct Pitch",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation)
                        End If
                    End If

                    If Pitch_tmp <> Pitch Then
                        If FlgInitEnd = 1 Then
                            ConstChangeTrue(Me, title_text2)
                        End If
                    End If

                    Points = Points_tmp
                    TxtPoints.Text = Points

                End If

                Pitch = Pitch_tmp
                If FlgInch = 0 Then
                    TxtPitch.Text = Pitch
                Else
                    TxtPitch.Text = Math.Round(Pitch / 25.4, 2, MidpointRounding.AwayFromZero)
                End If


                DrawCalcCurData_init()
                DrawCalcBakData_init()
                DrawCalcAvgData_init()
                DrawTableData_init()
                GraphInitPrf(Points)

                FlgMainProfile = 0
                TimProfile.Enabled = True

            Case 24 'Inchに変更
                TxtLength.Text = Math.Round(Length / 25.4, 2, MidpointRounding.AwayFromZero)
                TxtPitch.Text = Math.Round(Pitch / 25.4, 2, MidpointRounding.AwayFromZero)
                If TxtLengthOld.Text <> "" Then
                    TxtLengthOld.Text = Math.Round(Length / 25.4, 2, MidpointRounding.AwayFromZero)
                End If
                If TxtPitchOld.Text <> "" Then
                    TxtPitchOld.Text = Math.Round(Pitch / 25.4, 2, MidpointRounding.AwayFromZero)
                End If

                FlgMainProfile = 0

            Case 25 'mmに変更
                TxtLength.Text = Length
                TxtPitch.Text = Pitch
                If TxtLengthOld.Text <> "" Then
                    TxtLengthOld.Text = LengthOld
                End If
                If TxtPitchOld.Text <> "" Then
                    TxtPitchOld.Text = PitchOld
                End If

                FlgMainProfile = 0

            Case 26 'Angle Graph Range Change
                KdData = 1
                RedrawGraphAngle()

                If FileNo <> 0 Then
                    KdData = 3
                    Kt1 = SampleNo
                    Kt2 = Points

                    SampleNo = FileDataMax
                    Points = SampleNo
                    RedrawGraphAngleOld()

                    SampleNo = Kt1
                    Points = Kt2
                End If

                FlgMainProfile = flgTemp

            Case 27
                'Angle Center Change
                Dim input_ret As String

                Kt1 = SampleNo
                Kt3 = FileNo

                TimProfile.Enabled = False

                input_ret = InputBox(StrOriAngCentValInput, StrOriAngCentValSet, PkAngCent)

                If input_ret = String.Empty Then
                    '多分キャンセル
                    'キャンセルはなにもしない
                ElseIf input_ret = "" Then
                    '空入力は0とする
                    PkAngCent = 0
                    FlgPkCenterAngle = PkAngCent
                    LblAngCenter.Text = PkAngCent
                Else
                    If IsNumeric(input_ret) = True Then
                        If input_ret > 90 Then
                            input_ret = 90
                        ElseIf input_ret < -90 Then
                            input_ret = -90
                        End If
                        PkAngCent = input_ret
                        FlgPkCenterAngle = PkAngCent
                        LblAngCenter.Text = PkAngCent

                        KdData = 1
                        RedrawGraphAngle()

                        If FileNo <> 0 Then
                            KdData = 3
                            Kt1 = SampleNo
                            Kt2 = Points

                            SampleNo = FileDataMax
                            Points = SampleNo
                            RedrawGraphAngleOld()

                            SampleNo = Kt1
                            Points = Kt2
                        End If
                    Else
                        MessageBox.Show(StrEntNo,
                                        StrIncorrectNo,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                End If

                SampleNo = Kt1
                FileNo = Kt3

                FlgMainProfile = flgTemp

                TimProfile.Enabled = True

            Case 28 'Velocity Graph Range Change
                KdData = 1
                RedrawGraphVelocity()

                If FileNo <> 0 Then
                    KdData = 3
                    Kt1 = SampleNo
                    Kt2 = Points

                    SampleNo = FileDataMax
                    Points = SampleNo
                    RedrawGraphVelocityOld()

                    SampleNo = Kt1
                    Points = Kt2
                End If

                FlgMainProfile = flgTemp

            Case 29 'TSI Graph Range Change
                KdData = 1
                RedrawGraphTSI()

                If FileNo <> 0 Then
                    KdData = 3
                    Kt1 = SampleNo
                    Kt2 = Points

                    SampleNo = FileDataMax
                    Points = SampleNo
                    KdData = 3
                    RedrawGraphTSIOld()

                    SampleNo = Kt1
                    Points = Kt2
                End If
                FlgMainProfile = flgTemp

            Case 40
                '過去データ読み込み
                Kt2 = SampleNo

                If FlgProfile = 3 Then
                    If FileNo > 0 Then
                        FlgMainProfile = 44
                        Exit Sub
                    End If
                Else
                    If FileNo > 9 Then
                        FlgMainProfile = 44
                        Exit Sub
                    End If
                End If

                TimProfile.Enabled = False

                'HScrollBar1.Visible = True
                'HScrollBar1.Enabled = True
                'HScrollBar2.Visible = True
                'HScrollBar2.Enabled = True

                result = LoadOldDataName(fname)

                If result = DialogResult.OK Then
                    StrFileName = fname

                    FileNo += 1

                    result2 = LoadData()

                    If result2 < 1 Then
                        If result2 = 0 Then
                            MessageBox.Show(StrNoData,
                                            StrFileErr,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)
                        ElseIf result2 = -2 Then
                            MessageBox.Show(StrIncorrectFileFormat,
                                            StrFileErr,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)
                        ElseIf result2 = -1 Then
                            MessageBox.Show(StrDataCorrupted,
                                            StrFileErr,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error)
                        End If
                        FileNo -= 1
                        FlgMainProfile = 0
                    Else
                        '測定データと過去データの測定個所数をチェックする
                        '一致していたら以降を処理する
                        'ただし、過去データのみ読み込むこともできるので。。。
                        If SampleNo > 0 Then
                            '測定済みの場合
                            If SampleNo = FileDataMax Then
                                If FlgProfile = 1 Then  'プロファイルモード
                                    'ピッチ拡張有効の確認を最初にする
                                    If FlgPchExpMes = 1 Or FlgPchExpMes_old = 1 Then
                                        result = MessageBox.Show("Data measured with activated pitch setting" & vbCrLf &
                                                                 "Matching of pitch setings is not checked" & vbCrLf &
                                                                 "Do ou want to load past data ?",
                                                                 "Confirm of Pitch Setting",
                                                                 MessageBoxButtons.YesNo,
                                                                 MessageBoxIcon.Warning)
                                        If result = DialogResult.No Then
                                            SampleNo = Kt2
                                            FileNo -= 1
                                            FlgMainProfile = 0
                                            TimProfile.Enabled = True
                                            Exit Sub
                                        End If
                                    ElseIf Length <> LengthOld Or Pitch <> PitchOld Then
                                        result = MessageBox.Show("Sample Length of Meas.Data = " & Length & vbCrLf &
                                                                 "Past Data Sample Length = " & LengthOld & vbCrLf &
                                                                 "Number of pitches in meas.data = " & Pitch & vbCrLf &
                                                                 "Number of pitches in past data = " & PitchOld & vbCrLf &
                                                                 "The sample length or pitch didn't match," & vbCrLf &
                                                                 "are you sureto load past data?",
                                                                 "Error of Sample Length or Pitch Number Mismatch",
                                                                 MessageBoxButtons.YesNo,
                                                                 MessageBoxIcon.Exclamation)
                                        If result = DialogResult.No Then
                                            SampleNo = Kt2
                                            FileNo -= 1
                                            FlgMainProfile = 0
                                            TimProfile.Enabled = True
                                            Exit Sub
                                        End If
                                    End If
                                ElseIf FlgProfile = 3 Then  'MD長尺
                                    If Pitch <> PitchOld Then
                                        result = MessageBox.Show("Pitch of Meas.data = " & Pitch & vbCrLf &
                                                                 "Past data pitch = " & PitchOld & vbCrLf &
                                                                 "Although the number of pitches does not match," & vbCrLf &
                                                                 "are you sure want to load past data?",
                                                                 "Error of Number of Pitch Mismatch",
                                                                 MessageBoxButtons.YesNo,
                                                                 MessageBoxIcon.Exclamation)
                                        If result = DialogResult.No Then
                                            SampleNo = Kt2
                                            FileNo -= 1
                                            FlgMainProfile = 0
                                            TimProfile.Enabled = True
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            Else
                                MessageBox.Show("Number of meas.points of meas.data = " & SampleNo & vbCrLf &
                                                "Number of past meas.points = " & FileDataMax & vbCrLf &
                                                "The number of meas.points in the meas.data and past data didn't match" & vbCrLf &
                                                "Please load past data that matches the number of meas.points" & vbCrLf &
                                                "The loading process will be temporarily interrupted",
                                                "Error of Number of Point Mismatch",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Error)
                                SampleNo = Kt2
                                FileNo -= 1
                                FlgMainProfile = 0
                                TimProfile.Enabled = True
                                Exit Sub
                            End If
                        End If

                        WrtOldMeasInfo()
                        Kt1 = SampleNo

                        'SampleNo = FileDataMax
                        MakeDisplayData()

                        SampleNo = FileDataMax
                        Points = SampleNo
                        KdData = 3
                        InitializeMaxMinInt()

                        If FlgProfile = 3 Then
                            FlgMainProfile = 42
                        Else
                            FlgMainProfile = 41
                        End If

                        SampleNo = Kt2

                    End If
                ElseIf result = DialogResult.Cancel Then
                    FlgMainProfile = 0
                End If

                TimProfile.Enabled = True

            Case 41
                Kt2 = SampleNo
                Kt1 = FileDataMax

                KdData = 3
                For SampleNo = 1 To Kt1
                    DataMaxMinInt()
                Next

                KdData = 3
                SampleNo = FileDataMax
                PrfSaidDataAngle(0)
                PrfSaidDataRatio(0)
                PrfSaidDataTSI(0)
                PrfSaidDataVelo(0)

                If Kt2 > 0 Then
                    KdData = 1
                    SampleNo = Kt2
                    PrfSaidDataAngle(0)
                    PrfSaidDataRatio(0)
                    PrfSaidDataTSI(0)
                    PrfSaidDataVelo(0)
                End If

                '-------過去データ
                KdData = 3
                FlgLine = 11

                prf_waku_angle_Xpath.Clear()
                prf_waku_ratio_Xpath.Clear()
                prf_waku_velo_Xpath.Clear()
                prf_waku_tsi_Xpath.Clear()
                XScale(Points)

                For SampleNo = 1 To Kt1
                    PrfGraphAngleRatioOld()
                    PrfGraphVelocityTSIOld()
                Next

                '------測定データ復元
                If MeasDataMax > 0 Then
                    KdData = 1
                    PosX1(KdData) = 0
                    PosX2(KdData) = 0
                    FlgLine = 2
                    For SampleNo = 1 To Kt2
                        PrfGraphAngleRatio()
                        PrfGraphVelocityTSI()
                    Next

                End If

                'If FlgProfile = 3 Then
                'CmdAvg.Enabled = True
                'ElseIf MeasDataMax = FileDataMax Then
                'なぜMD長尺で条件なしに平均値ボタンを有効にしているのか不明
                '測定データのみ、過去データのみで平均値実行は正常動作しない
                'If MeasDataMax = FileDataMax Then  'ConditionEnableの中で実行している
                'CmdAvg.Enabled = True
                'End If

                ConditionEnable()

                Points = Val(TxtPoints.Text)
                SampleNo = Kt2
                FlgMainProfile = 0

            Case 42
                FlgLine = 1
                Kt2 = SampleNo

                KdData = 3
                For SampleNo = 1 To FileDataMax
                    DataMaxMinInt()
                Next

                KdData = 3
                SampleNo = FileDataMax
                PrfSaidDataAngle(0)
                PrfSaidDataRatio(0)
                PrfSaidDataTSI(0)
                PrfSaidDataVelo(0)

                KdData = 1
                SampleNo = MeasDataMax
                PrfSaidDataAngle(0)
                PrfSaidDataRatio(0)
                PrfSaidDataTSI(0)
                PrfSaidDataVelo(0)

                If MeasNo = 0 Then
                    Kt1 = FileDataMax

                    If Kt1 < lg_graph_max Then
                        DspPointx = 1
                    ElseIf Kt1 < lg_shiftx_ss Then
                        DspPointx = (lg_def_shiftxnum + 1)
                    Else
                        DspPointx = Int((Kt1 - lg_graph_max) / lg_def_shiftxnum) * lg_def_shiftxnum + (lg_def_shiftxnum + 1)
                    End If

                    SampleNo = Kt1
                    XScale(Points)
                Else
                    Kt1 = FileDataMax
                    If Kt1 > DspPointx + lg_graph_max Then
                        Kt1 = DspPointx + (lg_graph_max - 1)
                    End If
                End If

                FlgLine = 1
                KdData = 3
                For SampleNo = DspPointx To Kt1
                    PrfGraphAngleRatio()
                    PrfGraphVelocityTSI()
                Next

                Kt1 = FileDataMax
                If Kt1 < MeasDataMax Then
                    Kt1 = MeasDataMax
                End If

                ScrollBar_init(Kt1)

                ConditionEnable()

                SampleNo = Kt2
                FlgMainProfile = 0

            Case 44
                TimProfile.Enabled = False

                If FlgProfile = 3 Then
                    MessageBox.Show("When in MD long sample mode," & vbCrLf &
                                    "it is not possible to read two or more past data",
                                    "Error of Past Data Reading",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                Else
                    MessageBox.Show("It is not possible to read more then 10 pieces of past data",
                                    "Error of Past Data Reading",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                End If

                SampleNo = Kt2
                FlgMainProfile = 0

                TimProfile.Enabled = True

            Case 45
                '平均値計算
                SampleNo = MeasDataMax
                Kt1 = SampleNo
                Kt3 = FileNo

                Kt4 = FileDataMax
                If Kt4 > MeasDataMax And MeasDataMax > 1 Then
                    Kt4 = MeasDataMax
                End If

                KdData = 0
                ClsDataAvg()
                DataCount = 0

                If DataPrcNum(1, SampleNo, 1) <> 0 Then
                    For SampleNo = 1 To Kt4
                        DataPrcStr(0, SampleNo, 8) = DataPrcStr(1, SampleNo, 8)
                        DataPrcStr(0, SampleNo, 9) = DataPrcStr(1, SampleNo, 9)
                        DataPrcStr(0, SampleNo, 10) = DataPrcStr(1, SampleNo, 10)
                        DataPrcStr(0, SampleNo, 11) = DataPrcStr(1, SampleNo, 11)

                        For N = 1 To 18
                            DataPrcNum(0, SampleNo, N) = DataPrcNum(1, SampleNo, N)
                        Next
                    Next

                    DataCount = 1
                Else
                    For SampleNo = 1 To Kt4
                        DataPrcStr(0, SampleNo, 8) = ""
                        DataPrcStr(0, SampleNo, 9) = ""
                        DataPrcStr(0, SampleNo, 10) = ""
                        DataPrcStr(0, SampleNo, 11) = ""

                        For N = 1 To 18
                            DataPrcNum(0, SampleNo, N) = 0
                        Next
                    Next

                    DataCount = 0
                End If

                If FileNo <> 0 Then
                    For Kp = 1 To Kt3
                        For M = 1 To Kt4
                            Ks = DataPrcStr(0, M, 8)
                            Ks_1 = Strings.Left(Ks, 1)
                            Ls = DataFileStr(Kp, M, 8)
                            Ls_1 = Strings.Left(Ls, 1)
                            JS = Strings.Left(Ks, 2)
                            If Ks_1 = "C" Or Ks_1 = "M" Then
                                Ms = Val(Strings.Right(Ks, Len(Ks) - 2))
                            Else
                                Ms = Val(Ks)
                            End If
                            If Ls_1 = "C" Or Ls_1 = "M" Then
                                Ns = Val(Strings.Right(Ls, Len(Ls) - 2))
                            Else
                                Ns = Val(Ls)
                            End If
                            If Ks_1 = "C" Or Ks_1 = "M" Then
                                DataPrcStr(0, M, 8) = JS + Str(Ms + Ns)
                            Else
                                DataPrcStr(0, M, 8) = Str(Ms + Ns)
                            End If
                            Ks = DataPrcStr(0, M, 9)
                            Ks_1 = Strings.Left(Ks, 1)
                            Ls = DataFileStr(Kp, M, 9)
                            Ls_1 = Strings.Left(Ls, 1)
                            JS = Strings.Left(Ks, 2)
                            If Ks_1 = "C" Or Ks_1 = "M" Then
                                Ms = Val(Strings.Right(Ks, Len(Ks) - 2))
                            Else
                                Ms = Val(Ks)
                            End If
                            If Ls_1 = "C" Or Ls_1 = "M" Then
                                Ns = Val(Strings.Right(Ls, Len(Ls) - 2))
                            Else
                                Ns = Val(Ls)
                            End If
                            If Ks_1 = "C" Or Ks_1 = "M" Then
                                DataPrcStr(0, M, 9) = JS + Str(Ms + Ns)
                            Else
                                DataPrcStr(0, M, 9) = Str(Ms + Ns)
                            End If

                            DataPrcStr(0, M, 10) = Str(Val(DataPrcStr(0, M, 10)) + Val(DataFileStr(Kp, M, 10)))
                            DataPrcStr(0, M, 11) = Str(Val(DataPrcStr(0, M, 11)) + Val(DataFileStr(Kp, M, 11)))

                            For N = 1 To 18
                                DataPrcNum(0, M, N) += DataFileNum(Kp, M, N)
                            Next
                        Next
                        DataCount += 1
                    Next
                End If

                For M = 1 To Kt4
                    Ks = DataPrcStr(0, M, 8)
                    Ks_1 = Strings.Left(Ks, 1)
                    JS = Strings.Left(Ks, 2)
                    If Ks_1 = "C" Or Ks_1 = "M" Then
                        Ms = Val(Strings.Right(Ks, Len(Ks) - 2))
                        DataPrcStr(0, M, 8) = JS + Str(Ms / DataCount)
                    Else
                        Ms = Val(Ks)
                        DataPrcStr(0, M, 8) = Str(Ms / DataCount)
                    End If

                    Ks = DataPrcStr(0, M, 9)
                    Ks_1 = Strings.Left(Ks, 1)
                    JS = Strings.Left(Ks, 2)
                    If Ks_1 = "C" Or Ks_1 = "M" Then
                        Ms = Val(Strings.Right(Ks, Len(Ks) - 2))
                        DataPrcStr(0, M, 9) = JS + Str(Ms / DataCount)
                    Else
                        Ms = Val(Ks)
                        DataPrcStr(0, M, 9) = Str(Ms / DataCount)
                    End If

                    DataPrcStr(0, M, 10) = Str(Val(DataPrcStr(0, M, 10)) / DataCount)
                    DataPrcStr(0, M, 11) = Str(Val(DataPrcStr(0, M, 11)) / DataCount)

                    For N = 1 To 18
                        DataPrcNum(0, M, N) /= DataCount
                    Next
                Next

                KdData = 0
                InitializeMaxMinInt()

                KdData = 0
                For SampleNo = 1 To Kt4
                    DataMaxMinInt()
                Next

                Kt1 = MeasDataMax
                SampleNo = Kt1
                FlgScroll = 1

                ReDrawGraph()
                FlgScroll = 0

                If Kt4 >= DspPointx + lg_graph_max Then
                    Kt5 = DspPointx + (lg_graph_max - 1)
                Else
                    Kt5 = Kt4
                End If

                KdData = 0
                PosX1(KdData) = 0
                PosX2(KdData) = 0
                FlgLine = 3

                For SampleNo = DspPointx To Kt5
                    PrfGraphAngleRatio()
                    PrfGraphVelocityTSI()
                Next

                SampleNo = Kt1
                'KdData = 0で処理? 
                DrawCalcAvgData_init()
                PrfSaidDataAngle(0)
                PrfSaidDataRatio(0)
                PrfSaidDataTSI(0)
                PrfSaidDataVelo(0)

                KdData = 1
                DrawCalcCurData_init()
                PrfSaidDataAngle(0)
                PrfSaidDataRatio(0)
                PrfSaidDataTSI(0)
                PrfSaidDataVelo(0)

                If Kt3 > 0 Then
                    '過去データがあれば
                    KdData = 3
                    DrawCalcBakData_init()
                    PrfSaidDataAngle(0)
                    PrfSaidDataRatio(0)
                    PrfSaidDataTSI(0)
                    PrfSaidDataVelo(0)
                End If

                SampleNo = Kt1
                FileNo = Kt3

                FlgAvg = 2
                FlgMainProfile = 0

            Case 90
                '終了ボタン

                TimProfile.Enabled = False

                If FlgProfile = 2 Then
                    'カットシートで中断ではなく終了で終わらせた場合に
                    '測定データのファイル名を修正するため
                    '測定を既定の回数しなかった場合に、総測定個所数が実際と異なるため
                    '総測定個所数の部分を実際の数に変更する
                    If SampleNo > 0 Then
                        DataFileRename(FlgProfile,
                                       cur_dir,
                                       SampleNo,
                                       Trim(MachineNo),
                                       Trim(Sample),
                                       FileDate,
                                       FileTime)
                    End If
                End If

                ToolStripStatusLabel4.Text = ""

                If FlgTest = 0 Then
                    UsbClose()
                End If

                '測定仕様ファイルの保存処理
                If FlgConstChg = True Then
                    result = MessageBox.Show("Meas.Spec. has changed" & vbCrLf &
                                             "Do you want to save the changes?" & vbCrLf &
                                             "Yes : Overwrite" & vbCrLf &
                                             "No : Save as" & vbCrLf &
                                             "Cancel : Exit without saving",
                                             StrConfirmMeasSpecChg,
                                             MessageBoxButtons.YesNoCancel,
                                             MessageBoxIcon.Information)
                    Select Case result
                        Case DialogResult.Yes
                            SaveConst(StrConstFilePath)
                        Case DialogResult.No
                            SaveConstPrf()
                        Case DialogResult.Cancel
                            'なにもしない
                    End Select

                End If

                frmClose()
                Me.Visible = False
                FlgHoldMeas = 0
                FlgMainProfile = 91
                timerCount1 = 0

                TimProfile.Enabled = True

            Case 91
                timerCount1 += 1
                If timerCount1 = 10 Then
                    TimProfile.Enabled = False

                    CmdMeasButton_set(_rdy)
                    CmdMeas.Text = StrMeasStart
                    MeasStartToolStripMenuItem.Text = StrMeasStart

                    FrmSST4500_1_0_0E_main.Visible = True
                    FlgMainSplash = 11
                    FlgMainProfile = 0
                    FlgInitEnd = 0
                End If

            Case 99
                ToolStripStatusLabel4.Text = "Received Data Error(Data Nothing or Timeout) : " & strRxdata
                FlgHoldMeas = 0
                If FlgTest = 0 Then
                    UsbClose()
                End If
                FlgMainProfile = 0

            'テストモード
            Case 100
                CmdMeas.Enabled = False
                MeasStartToolStripMenuItem.Enabled = False
                MeasStopToolStripMenuItem.Enabled = True

                If FlgPitchExp <> 0 Then
                    'テストモードの時のみ下記if文が必要
                    '通常測定時は別の方法で回避している
                    If SampleNo <> Points - 1 Then
                        Pitch = PchExp_PchData(SampleNo)
                    End If
                    TxtPitch.Text = Pitch
                End If

                TimProfile.Enabled = False
                ToolStripStatusLabel4.Text = StrMeasuringSpace
                CmdMeasButton_set(_mes)
                CmdMeas.Text = StrMeasuring
                MeasStartToolStripMenuItem.Text = StrMeasuring
                TimProfile.Enabled = True

                'カットシートの場合、2回目以降が100番から始まる
                If FlgProfile = 2 Then
                    CmdQuitProfile.Text = StrMeasInterrupt
                    QuitToolStripMenuItem.Enabled = False
                End If

                timerCount1 = 0
                FlgMainProfile = 101

            Case 101
                timerCount1 += 1

                If FlgProfile <> 3 Then
                    If timerCount1 Mod 5 = 0 Then
                        ToolStripStatusLabel4.Text &= "o"
                    End If

                    If timerCount1 >= test_count1_prf Then
                        timerCount1 = 0
                        FlgMainProfile = 102
                    End If
                Else
                    If timerCount1 Mod 5 = 0 Then
                        ToolStripStatusLabel4.Text &= "o"
                    End If

                    If timerCount1 >= test_count1_md Then
                        timerCount1 = 0
                        FlgMainProfile = 102
                    End If
                End If

            Case 102
                timerCount1 += 1

                If FlgProfile <> 3 Then
                    If timerCount1 Mod 2 = 0 Then
                        ToolStripStatusLabel4.Text &= "->"
                    End If

                    If timerCount1 >= test_count2_prf Then
                        timerCount1 = 0
                        FlgMainProfile = 103
                    End If
                Else
                    If timerCount1 Mod 2 = 0 Then
                        ToolStripStatusLabel4.Text &= "->"
                    End If

                    If timerCount1 >= test_count2_md Then
                        timerCount1 = 0
                        FlgMainProfile = 103
                    End If
                End If

            Case 103
                SampleNo += 1
                MeasDataMax = SampleNo
                TxtMeasNumCur.Text = SampleNo

                KdData = 1
                ResolveData()

                KdData = 1
                DataMaxMinInt()

                DataPrcStr(1, SampleNo, 1) = TxtMachNoCur.Text
                DataPrcStr(1, SampleNo, 2) = TxtSmplNamCur.Text
                DataPrcStr(1, SampleNo, 3) = TxtMarkCur.Text
                DataPrcStr(1, SampleNo, 5) = Str(SampleNo)

                SaveData()

                KdData = 1
                FlgLine = 2

                PrfSaidDataAngle(0)
                PrfSaidDataRatio(0)
                PrfGraphAngleRatio()
                PrfSaidDataVelo(0)
                PrfSaidDataTSI(0)
                PrfGraphVelocityTSI()

                FlgMainProfile = 104

            Case 104
                Select Case FlgProfile
                    Case 1
                        If SampleNo = Points Then
                            FlgMainProfile = 150
                        ElseIf FlgStop = 1 Then
                            FlgMainProfile = 150
                        Else
                            FlgMainProfile = 110
                        End If

                    Case 2
                        If SampleNo = Points Then
                            FlgMainProfile = 150
                        ElseIf FlgStop = 1 Then
                            FlgMainProfile = 150
                        Else
                            ToolStripStatusLabel4.Text = StrNextSample
                            'CmdMeas.Enabled = True
                            'CmdMeas.Text = "測定開始"
                            'CmdMeasButton_set(_rdy)
                            '測定開始ToolStripMenuItem.Enabled = True
                            '測定開始ToolStripMenuItem.Text = "測定開始"
                            '測定中断ToolStripMenuItem.Enabled = False
                            '終了ToolStripMenuItem.Enabled = True
                            MeasStartInit()

                            CmdQuitProfile.Text = StrQuit
                            FlgHoldMeas = 2
                            FlgMainProfile = 0
                        End If

                    Case 3
                        If FlgStop = 1 Then
                            FlgMainProfile = 150
                            Kx = 0
                        Else
                            '新バージョンでMarkを省いたため30000を基準

                            If SampleNo = lg_sample_max Then
                                FlgMainProfile = 150
                                Kx = 0
                            Else
                                FlgMainProfile = 110
                            End If
                        End If
                End Select

            Case 110
                If FlgProfile = 3 Then
                    'If SampleNo > (lg_stepscale * 1) And (SampleNo - lg_graph_max) Mod lg_def_shiftxnum = 0 Then
                    If SampleNo > (lg_stepscale * 2) And (SampleNo - lg_graph_max) Mod lg_def_shiftxnum = 0 Then
                        GraphShift()
                    End If
                End If

                ToolStripStatusLabel4.Text = StrFeedingSample
                timerCount1 = 0
                FlgMainProfile = 111

            Case 111
                timerCount1 += 1
                If FlgProfile <> 3 Then
                    If timerCount1 Mod 2 = 0 Then
                        ToolStripStatusLabel4.Text &= "=>"
                    End If

                    If FlgPitchExp = 0 Then
                        If timerCount1 >= Math.Round(Pitch / 20) Then
                            timerCount1 = 0
                            FlgMainProfile = 100
                        End If
                    Else
                        If timerCount1 >= Math.Round(PchExp_PchData(SampleNo - 1) / 20) Then
                            timerCount1 = 0
                            FlgMainProfile = 100
                        End If
                    End If
                Else
                    If timerCount1 Mod 2 = 0 Then
                        ToolStripStatusLabel4.Text &= "=>"
                    End If

                    If timerCount1 >= Math.Round(Pitch / 20) Then
                        timerCount1 = 0
                        FlgMainProfile = 100
                    End If
                End If

            Case 150
                ToolStripStatusLabel4.Text = StrMeasCompleted
                CmdQuitProfile.Text = StrQuit
                CmdQuitProfile.Enabled = True
                QuitToolStripMenuItem.Enabled = True
                MeasStopToolStripMenuItem.Enabled = False

                'Points = SampleNo
                '中断で測定を終了した場合、Pointsが中断した時の測定回数に更新すると
                '測定仕様を変更した際にPointsでグラフが初期化されるので不都合が生じるため
                'コメントアウトする

                If FlgProfile = 3 Then
                    ScrollBar_init(SampleNo)
                End If

                FlgMainProfile = 0

                FlgLongMeas = 1

                'CmdMeas.Enabled = True
                'CmdMeas.Text = "測定開始"
                'CmdMeasButton_set(_rdy)
                '測定開始ToolStripMenuItem.Enabled = True
                '測定開始ToolStripMenuItem.Text = "測定開始"
                '測定中断ToolStripMenuItem.Enabled = False
                '終了ToolStripMenuItem.Enabled = True
                MeasStartInit()

                ConditionEnable()

                FlgStop = 0
                FlgHoldMeas = 0

                If FlgPrfAutoPrn = 1 Then
                    TimProfile.Enabled = False
                    PrintoutPrf()
                    TimProfile.Enabled = True
                End If

                '測定完了時に測定データのファイル名を修正する
                '測定を既定の回数しなかった場合に、総測定個所数が実際と異なるため
                '総測定個所数の部分を実際の数に変更する
                If SampleNo > 0 Then
                    DataFileRename(FlgProfile,
                               cur_dir,
                               SampleNo,
                               Trim(MachineNo),
                               Trim(Sample),
                               FileDate,
                               FileTime)
                End If

                'ピッチ拡張時に値を復元
                'If FlgPitchExp <> 0 Then
                'TxtLength.Text = _length_bak
                'TxtPitch.Text = _pitch_bak
                'TxtPoints.Text = _points_bak
                'End If

                'ピッチ拡張時にピッチを最初の値にする
                If FlgPitchExp <> 0 Then
                    TxtPitch.Text = PchExp_PchData(0)
                End If

        End Select
    End Sub

    Private Sub MeasStartInit()
        CmdMeas.Enabled = True
        CmdMeasButton_set(_rdy)
        CmdMeas.Text = StrMeasStart
        MeasStartToolStripMenuItem.Enabled = True
        MeasStartToolStripMenuItem.Text = StrMeasStart
        MeasStopToolStripMenuItem.Enabled = False
        QuitToolStripMenuItem.Enabled = True
    End Sub

    Private Sub ScrollBar_init(ByVal sampleno As Integer)
        If sampleno <= lg_graph_max Then
            HScrollBar1.Visible = False
            HScrollBar1.Enabled = False
            HScrollBar2.Visible = False
            HScrollBar2.Enabled = False
        Else
            HsbHold = DspPointx - 1
            HScrollBar1.SmallChange = lg_stepscale
            HScrollBar1.LargeChange = lg_stepscale
            HScrollBar1.Maximum = HsbHold + HScrollBar1.LargeChange - 1
            RemoveHandler HScrollBar1.ValueChanged, AddressOf HScrollBar1_ValueChanged
            HScrollBar1.Value = HsbHold
            AddHandler HScrollBar1.ValueChanged, AddressOf HScrollBar1_ValueChanged
            HScrollBar1.Visible = True
            HScrollBar1.Enabled = True
            HScrollBar2.SmallChange = lg_stepscale
            HScrollBar2.LargeChange = lg_stepscale
            HScrollBar2.Maximum = HsbHold + HScrollBar2.LargeChange - 1
            RemoveHandler HScrollBar2.ValueChanged, AddressOf HScrollBar2_ValueChanged
            HScrollBar2.Value = HsbHold
            AddHandler HScrollBar2.ValueChanged, AddressOf HScrollBar2_ValueChanged
            HScrollBar2.Visible = True
            HScrollBar2.Enabled = True

            Console.WriteLine("ScrollMax: " & HScrollBar1.Maximum)
            Console.WriteLine("SLChange: " & HScrollBar1.SmallChange)
            Console.WriteLine("HsbHold: " & HsbHold)
            Console.WriteLine("SampleNo: " & sampleno)
            Console.WriteLine("MeasDataMax: " & MeasDataMax)
            Console.WriteLine("FileDataMax: " & FileDataMax)
        End If
    End Sub

    Function SendPch2(ByVal _pitch As Integer) As Integer
        Dim X As Long

        strWdata = "PCH" & vbCr
        UsbWrite(strWdata)

        X = 0
        Do
            X += 1
        Loop Until X = 30000

        'If pitch > 5000 Then
        'TxtPitch.Text = 5000
        'End If

        Select Case Len(_pitch.ToString)
            Case 4
                strWdata = _pitch.ToString & vbCr
            Case 3
                strWdata = "0" & _pitch.ToString & vbCr
            Case 2
                strWdata = "00" & _pitch.ToString & vbCr
            Case 1
                strWdata = "000" & _pitch.ToString & vbCr
        End Select

        UsbWrite(strWdata)

        strRxdata = ""
        timerCount1 = 0
        _flgRx = UsbRead(strRxdata)
        While _flgRx = 1
            _flgRx = UsbRead(strRxdata)
            Threading.Thread.Sleep(50)
            timerCount1 += 1
            If timerCount1 >= cmd_timeout Then
                Return 0
                Exit Function
            End If
        End While

        timerCount1 = 0
        Return 1
    End Function

    Private Sub SendPch()
        Dim X As Long

        strWdata = "PCH" & vbCr
        UsbWrite(strWdata)

        X = 0
        Do
            X += 1

        Loop Until X = 30000

        If Pitch > 5000 Then
            TxtPitch.Text = 5000
        End If

        Select Case Len(TxtPitch.Text)
            Case 4
                strWdata = TxtPitch.Text & vbCr
            Case 3
                strWdata = "0" & TxtPitch.Text & vbCr
            Case 2
                strWdata = "00" & TxtPitch.Text & vbCr
            Case 1
                strWdata = "000" & TxtPitch.Text & vbCr
        End Select

        UsbWrite(strWdata)
    End Sub

    Private Sub AdmVisible_onoff(ByVal sw As Boolean)
        CmdOldDataLoad.Visible = sw
        CmdOldDataLoad.Enabled = sw
        CmdClsGraph.Visible = sw
        CmdClsGraph.Enabled = sw
        CmdAvg.Visible = sw
        'CmdAvg.Enabled = sw

        If FlgDBF = 1 Then
            TxtMarkBak.Visible = sw
            TxtMarkBak.Enabled = sw
        End If
        TxtMachNoBak.Visible = sw
        TxtMachNoBak.Enabled = sw
        TxtSmplNamBak.Visible = sw
        TxtSmplNamBak.Enabled = sw
        TxtMeasNumBak.Visible = sw
        TxtMeasNumBak.Enabled = sw
        TxtMeasLotBak.Visible = sw
        TxtMeasLotBak.Enabled = sw
        TxtLengthOld.Visible = sw
        TxtPitchOld.Visible = sw
        TxtPointsOld.Visible = sw

        TblAngle_nom.Visible = Not sw
        TblPDMCratio_nom.Visible = Not sw
        TblVeloPkDp_nom.Visible = Not sw
        TblVeloMDCD_nom.Visible = Not sw
        TblTSI_nom.Visible = Not sw

        TblAngle_adm.Visible = sw
        TblPDMCratio_adm.Visible = sw
        TblVeloPkDp_adm.Visible = sw
        TblVeloMDCD_adm.Visible = sw
        TblTSI_adm.Visible = sw

        ChkPrn_OldData.Enabled = sw
        ChkPrn_AvgData.Enabled = sw

        LblMeasSpecBak.Visible = sw
        LblMeasSpecBak2.Visible = sw
        LblMeasSpecCur2.Visible = sw

        TableLayoutPanel4.Visible = sw
        TableLayoutPanel5.Visible = sw

        LoadToolStripMenuItem.Enabled = sw
        GraphDelToolStripMenuItem.Enabled = sw
        AvgDataTableToolStripMenuItem.Enabled = sw
        OldDataTableToolStripMenuItem.Enabled = sw
        AvgDataTableToolStripMenuItem.Enabled = sw
    End Sub

    Private Sub ClsNoPrf()
        SampleNo = 0
        TxtMeasNumCur.Text = SampleNo
        MeasNo = 0
        MeasDataNo = 0
        TxtMeasLotCur.Text = MeasNo
        FileNo = 0
        FileDataNo = 0
    End Sub

    Private Sub ClsGraph()
        PictureBox1.CreateGraphics.Clear(BackColor)
        PictureBox2.CreateGraphics.Clear(BackColor)
        PictureBox3.CreateGraphics.Clear(BackColor)
        PictureBox4.CreateGraphics.Clear(BackColor)

        DrawGraph_init()

        prf_waku_Xlabel_name = "Point"
        prf_waku_angle_Yaxis_label = "Orientation Angle [deg.]"
        prf_waku_angle_Pklabel_name = "Peak"
        prf_waku_angle_Dplabel_name = "Deep"
        prf_waku_ratio_Yaxis_label = "Orientation Ratio"
        prf_waku_ratio_MDCDlabel_name = "MD/CD"
        prf_waku_ratio_PkDplabel_name = "Peak/Deep"
        prf_waku_velo_Yaxis_label = "Propagation Velocity [Km/S]"
        prf_waku_velo_VMDlabel_name = "MD"
        prf_waku_velo_VCDlabel_name = "CD"
        prf_waku_velo_VPklabel_name = "Peak"
        prf_waku_velo_VDplabel_name = "Deep"
        prf_waku_tsi_Yaxis_label = "TSI [Km/S]^2"
        prf_waku_tsi_MDlabel_name = "MD"
        prf_waku_tsi_CDlabel_name = "CD"

        Dim path1a As New GraphicsPath
        Dim path1b As New GraphicsPath
        Dim path2 As New GraphicsPath
        Dim path3 As New GraphicsPath
        Dim path4 As New GraphicsPath
        Dim i As Integer

        '配向角のグラフ枠線
        For i = 0 To 10
            If i = 5 Then
                '中央のライン
                path1a.StartFigure()
                path1a.AddLine(graph_x_sta, angle_yaxis_min + (i * angle_SclY), graph_x_end, angle_yaxis_min + (i * angle_SclY))
            Else
                path1b.StartFigure()
                path1b.AddLine(graph_x_sta, angle_yaxis_min + (i * angle_SclY), graph_x_end, angle_yaxis_min + (i * angle_SclY))
            End If
        Next
        prf_waku_angle_Ypath1.Add(path1a)
        prf_waku_angle_Ypath2.Add(path1b)

        angle_yaxis_label(FlgAngleRange)

        '配向比のグラフ枠線
        For i = 0 To 5
            path2.StartFigure()
            path2.AddLine(graph_x_sta, ratio_yaxis_min + (i * ratio_SclY), graph_x_end, ratio_yaxis_min + (i * ratio_SclY))
        Next
        prf_waku_ratio_Ypath.Add(path2)

        ratio_yaxis_label()

        '伝播速度のグラフ枠線
        For i = 0 To 5
            path3.StartFigure()
            path3.AddLine(graph_x_sta, velo_yaxis_min + (i * velo_SclY), graph_x_end, velo_yaxis_min + (i * velo_SclY))
        Next
        prf_waku_velo_Ypath.Add(path3)

        velo_yaxis_label(FlgVelocityRange)

        'TSIのグラフ枠線
        For i = 0 To 5
            path4.StartFigure()
            path4.AddLine(graph_x_sta, tsi_yaxis_min + (i * tsi_SclY), graph_x_end, tsi_yaxis_min + (i * tsi_SclY))
        Next
        prf_waku_tsi_Ypath.Add(path4)

        tsi_yaxis_label(FlgTSIRange)

        If FlgProfile = 3 Then
            Points = lg_graph_max
            TxtPoints.Text = Points
        End If

        XScale(Points)

        PictureBox1.Refresh()
        PictureBox2.Refresh()
        PictureBox3.Refresh()
        PictureBox4.Refresh()
    End Sub

    Public Sub GraphInitPrf(ByVal _points As Single)
        'cmdClsGraph_Clickの代わり?
        PictureBox1.CreateGraphics.Clear(BackColor)
        PictureBox2.CreateGraphics.Clear(BackColor)
        PictureBox3.CreateGraphics.Clear(BackColor)
        PictureBox4.CreateGraphics.Clear(BackColor)

        ClsMeasDataPrf()
        ClsFileDataPrf()
        ClsBakInfoPrf()

        For KdData = 0 To 3
            ClsMaxMinInit()
        Next

        DrawGraph_init()

        PosX1(0) = 0
        PosX1(1) = 0
        PosX1(2) = 0
        PosX1(3) = 0
        PosX2(0) = 0
        PosX2(1) = 0
        PosX2(2) = 0
        PosX2(3) = 0
        DataCount = 0
        SampleNo = 0
        MeasDataMax = 0
        FileDataMax = 0
        FileNo = 0
        TxtMeasNumCur.Text = "0"
        TxtMeasLotCur.Text = "0"
        FlgAvg = 0
        FlgLongMeas = 0
        FlgHoldMeas = 0
        prf_waku_Xlabel_name = "Point"
        prf_waku_angle_Yaxis_label = "Orientation Angle [deg.]"
        prf_waku_angle_Pklabel_name = "Peak"
        prf_waku_angle_Dplabel_name = "Deep"
        prf_waku_ratio_Yaxis_label = "Orientation Ratio"
        prf_waku_ratio_MDCDlabel_name = "MD/CD"
        prf_waku_ratio_PkDplabel_name = "Peak/Deep"
        prf_waku_velo_Yaxis_label = "Propagation Velocity [Km/S]"
        prf_waku_velo_VMDlabel_name = "MD"
        prf_waku_velo_VCDlabel_name = "CD"
        prf_waku_velo_VPklabel_name = "Peak"
        prf_waku_velo_VDplabel_name = "Deep"
        prf_waku_tsi_Yaxis_label = "TSI [Km/S]^2"
        prf_waku_tsi_MDlabel_name = "MD"
        prf_waku_tsi_CDlabel_name = "CD"

        Dim path1a As New GraphicsPath
        Dim path1b As New GraphicsPath
        Dim path2 As New GraphicsPath
        Dim path3 As New GraphicsPath
        Dim path4 As New GraphicsPath
        Dim i As Integer

        '配向角のグラフ枠線
        For i = 0 To 10
            If i = 5 Then
                '中央のライン
                path1a.StartFigure()
                path1a.AddLine(graph_x_sta, angle_yaxis_min + (i * angle_SclY), graph_x_end, angle_yaxis_min + (i * angle_SclY))
            Else
                path1b.StartFigure()
                path1b.AddLine(graph_x_sta, angle_yaxis_min + (i * angle_SclY), graph_x_end, angle_yaxis_min + (i * angle_SclY))
            End If
        Next
        prf_waku_angle_Ypath1.Add(path1a)
        prf_waku_angle_Ypath2.Add(path1b)

        angle_yaxis_label(FlgAngleRange)

        '配向比のグラフ枠線
        For i = 0 To 5
            path2.StartFigure()
            path2.AddLine(graph_x_sta, ratio_yaxis_min + (i * ratio_SclY), graph_x_end, ratio_yaxis_min + (i * ratio_SclY))
        Next
        prf_waku_ratio_Ypath.Add(path2)

        ratio_yaxis_label()

        '伝播速度のグラフ枠線
        For i = 0 To 5
            path3.StartFigure()
            path3.AddLine(graph_x_sta, velo_yaxis_min + (i * velo_SclY), graph_x_end, velo_yaxis_min + (i * velo_SclY))
        Next
        prf_waku_velo_Ypath.Add(path3)

        velo_yaxis_label(FlgVelocityRange)

        'TSIのグラフ枠線
        For i = 0 To 5
            path4.StartFigure()
            path4.AddLine(graph_x_sta, tsi_yaxis_min + (i * tsi_SclY), graph_x_end, tsi_yaxis_min + (i * tsi_SclY))
        Next
        prf_waku_tsi_Ypath.Add(path4)

        tsi_yaxis_label(FlgTSIRange)

        If FlgProfile = 3 Then
            Points = lg_graph_max
            TxtPoints.Text = Points
            FlgScroll = 0
            HScrollBar1.Visible = False
            HScrollBar1.Enabled = False
            HScrollBar2.Visible = False
            HScrollBar2.Enabled = False
        End If

        XScale(_points)

        PictureBox1.Refresh()
        PictureBox2.Refresh()
        PictureBox3.Refresh()
        PictureBox4.Refresh()
    End Sub

    Private Sub DrawGraph_init()
        'グラフ表示のデータをクリアする
        prf_waku_angle_Ypath1.Clear()
        prf_waku_angle_Ypath2.Clear()
        prf_waku_angle_Xpath.Clear()

        prf_waku_ratio_Ypath.Clear()
        prf_waku_ratio_Xpath.Clear()

        prf_waku_velo_Ypath.Clear()
        prf_waku_velo_Xpath.Clear()

        prf_waku_tsi_Ypath.Clear()
        prf_waku_tsi_Xpath.Clear()

        For i = 0 To 7
            prf_waku_Xlabel(i) = ""
        Next
        prf_waku_Xlabel_name = ""

        For i = 0 To 8
            prf_waku_angle_Ylabel(i) = ""
        Next
        prf_waku_angle_Pklabel_name = ""
        prf_waku_angle_Dplabel_name = ""
        prf_waku_angle_Yaxis_label = ""

        For i = 0 To 3
            prf_waku_ratio_Ylabel(i) = ""
        Next
        prf_waku_ratio_MDCDlabel_name = ""
        prf_waku_ratio_PkDplabel_name = ""
        prf_waku_ratio_Yaxis_label = ""

        For i = 0 To 3
            prf_waku_velo_Ylabel(i) = ""
        Next
        prf_waku_velo_VMDlabel_name = ""
        prf_waku_velo_VCDlabel_name = ""
        prf_waku_velo_VPklabel_name = ""
        prf_waku_velo_VDplabel_name = ""
        prf_waku_velo_Yaxis_label = ""

        angle_peak_cur_path.Clear()     'angle-peak-graph clear
        angle_deep_cur_path.Clear()     'angle-deep-graph clear
        ratio_pkdp_cur_path.Clear()     'ratio-peak/deep-graph clear
        ratio_mdcd_cur_path.Clear()     'ratio-md/cd-graph clear
        velo_md_cur_path.Clear()        'velocity-md-graph clear
        velo_cd_cur_path.Clear()        'velocity-cd-graph clear
        velo_peak_cur_path.Clear()      'veloctiy-peak-graph clear
        velo_deep_cur_path.Clear()      'velocity-deep-graph clear
        tsi_md_cur_path.Clear()         'tsi-md-graph clear
        tsi_cd_cur_path.Clear()         'tsi-cd=graph clear

        angle_peak_old_path.Clear()     'angle-peak-graph clear
        angle_deep_old_path.Clear()     'angle-deep-graph clear
        ratio_pkdp_old_path.Clear()     'ratio-peak/deep-graph clear
        ratio_mdcd_old_path.Clear()     'ratio-md/cd-graph clear
        velo_md_old_path.Clear()        'velocity-md-graph clear
        velo_cd_old_path.Clear()        'velocity-cd-graph clear
        velo_peak_old_path.Clear()      'veloctiy-peak-graph clear
        velo_deep_old_path.Clear()      'velocity-deep-graph clear
        tsi_md_old_path.Clear()         'tsi-md-graph clear
        tsi_cd_old_path.Clear()         'tsi-cd=graph clear
    End Sub

    Private Sub angle_yaxis_label(ByVal _FlgAngleRange As Integer)
        Select Case _FlgAngleRange
            Case 0
                prf_waku_angle_Ylabel(0) = " +2.0"
                prf_waku_angle_Ylabel(1) = " +1.5"
                prf_waku_angle_Ylabel(2) = " +1.0"
                prf_waku_angle_Ylabel(3) = " +0.5"
                prf_waku_angle_Ylabel(4) = "    0"
                prf_waku_angle_Ylabel(5) = " -0.5"
                prf_waku_angle_Ylabel(6) = " -1.0"
                prf_waku_angle_Ylabel(7) = " -1.5"
                prf_waku_angle_Ylabel(8) = " -2.0"
            Case 1
                prf_waku_angle_Ylabel(0) = " +4.0"
                prf_waku_angle_Ylabel(1) = " +3.0"
                prf_waku_angle_Ylabel(2) = " +2.0"
                prf_waku_angle_Ylabel(3) = " +1.0"
                prf_waku_angle_Ylabel(4) = "    0"
                prf_waku_angle_Ylabel(5) = " -1.0"
                prf_waku_angle_Ylabel(6) = " -2.0"
                prf_waku_angle_Ylabel(7) = " -3.0"
                prf_waku_angle_Ylabel(8) = " -4.0"
            Case 2
                prf_waku_angle_Ylabel(0) = " +8.0"
                prf_waku_angle_Ylabel(1) = " +6.0"
                prf_waku_angle_Ylabel(2) = " +4.0"
                prf_waku_angle_Ylabel(3) = " +2.0"
                prf_waku_angle_Ylabel(4) = "    0"
                prf_waku_angle_Ylabel(5) = " -2.0"
                prf_waku_angle_Ylabel(6) = " -4.0"
                prf_waku_angle_Ylabel(7) = " -6.0"
                prf_waku_angle_Ylabel(8) = " -8.0"
            Case 3
                prf_waku_angle_Ylabel(0) = "+16.0"
                prf_waku_angle_Ylabel(1) = "+12.0"
                prf_waku_angle_Ylabel(2) = " +8.0"
                prf_waku_angle_Ylabel(3) = " +4.0"
                prf_waku_angle_Ylabel(4) = "    0"
                prf_waku_angle_Ylabel(5) = " -4.0"
                prf_waku_angle_Ylabel(6) = " -8.0"
                prf_waku_angle_Ylabel(7) = "-12.0"
                prf_waku_angle_Ylabel(8) = "-16.0"
            Case 4
                prf_waku_angle_Ylabel(0) = "+32.0"
                prf_waku_angle_Ylabel(1) = "+24.0"
                prf_waku_angle_Ylabel(2) = "+16.0"
                prf_waku_angle_Ylabel(3) = " +8.0"
                prf_waku_angle_Ylabel(4) = "    0"
                prf_waku_angle_Ylabel(5) = " -8.0"
                prf_waku_angle_Ylabel(6) = "-16.0"
                prf_waku_angle_Ylabel(7) = "-24.0"
                prf_waku_angle_Ylabel(8) = "-32.0"
            Case 5
                prf_waku_angle_Ylabel(0) = "+64.0"
                prf_waku_angle_Ylabel(1) = "+48.0"
                prf_waku_angle_Ylabel(2) = "+32.0"
                prf_waku_angle_Ylabel(3) = "+16.0"
                prf_waku_angle_Ylabel(4) = "    0"
                prf_waku_angle_Ylabel(5) = "-16.0"
                prf_waku_angle_Ylabel(6) = "-32.0"
                prf_waku_angle_Ylabel(7) = "-48.0"
                prf_waku_angle_Ylabel(8) = "-64.0"
            Case 6
                prf_waku_angle_Ylabel(0) = "+128.0"
                prf_waku_angle_Ylabel(1) = " +96.0"
                prf_waku_angle_Ylabel(2) = " +64.0"
                prf_waku_angle_Ylabel(3) = " +32.0"
                prf_waku_angle_Ylabel(4) = "     0"
                prf_waku_angle_Ylabel(5) = " -32.0"
                prf_waku_angle_Ylabel(6) = " -64.0"
                prf_waku_angle_Ylabel(7) = " -96.0"
                prf_waku_angle_Ylabel(8) = "+128.0"
        End Select
    End Sub

    Private Sub velo_yaxis_label(ByVal _VelocityRange As Integer)
        Select Case _VelocityRange
            Case 0
                prf_waku_velo_Ylabel(0) = "4.00"
                prf_waku_velo_Ylabel(1) = "3.00"
                prf_waku_velo_Ylabel(2) = "2.00"
                prf_waku_velo_Ylabel(3) = "1.00"
            Case 1
                prf_waku_velo_Ylabel(0) = "8.00"
                prf_waku_velo_Ylabel(1) = "6.00"
                prf_waku_velo_Ylabel(2) = "4.00"
                prf_waku_velo_Ylabel(3) = "2.00"
        End Select
    End Sub

    Private Sub tsi_yaxis_label(ByVal _TSIRange As Integer)
        Select Case _TSIRange
            Case 0
                prf_waku_tsi_Ylabel(0) = "20.0"
                prf_waku_tsi_Ylabel(1) = "15.0"
                prf_waku_tsi_Ylabel(2) = "10.0"
                prf_waku_tsi_Ylabel(3) = " 5.0"
            Case 1
                prf_waku_tsi_Ylabel(0) = "80.0"
                prf_waku_tsi_Ylabel(1) = "60.0"
                prf_waku_tsi_Ylabel(2) = "40.0"
                prf_waku_tsi_Ylabel(3) = "20.0"
        End Select
    End Sub

    Private Sub ratio_yaxis_label()
        prf_waku_ratio_Ylabel(0) = "2.0"
        prf_waku_ratio_Ylabel(1) = "1.5"
        prf_waku_ratio_Ylabel(2) = "1.0"
        prf_waku_ratio_Ylabel(3) = "0.5"
    End Sub

    Private Sub XScale(ByVal _points As Single)
        Dim i As Integer
        Dim graph_width As Integer

        graph_width = graph_x_end - graph_x_sta

        If FlgProfile = 3 Then
            If _points < lg_graph_max Then
                _points = lg_graph_max
            End If

            StepX = graph_width / (lg_graph_max - 1)
            StepScale = lg_stepscale

            If FlgScroll = 0 Then
                If SampleNo < lg_graph_max Then
                    ShiftXNum = 0
                ElseIf SampleNo < lg_shiftx_ss Then
                    ShiftXNum = lg_def_shiftxnum
                Else
                    ShiftXNum = Int((SampleNo - lg_graph_max) / lg_def_shiftxnum) * lg_def_shiftxnum + lg_def_shiftxnum
                End If
                DspPointx = ShiftXNum + 1
            Else
                ShiftXNum = DspPointx - 1
            End If

            SclX = ((graph_width) / (lg_graph_max - 1)) * lg_stepscale
        Else
            'StepX = (graph_width) / (Points - 1)
            StepX = (graph_width) / (_points - 1)
            'StepScale = Int((Points) / 5)
            StepScale = Int((_points) / 5)
            ShiftXNum = 0

            SclX = StepX * StepScale
            'If Points < 5 Then
            If _points < 5 Then
                SclX = StepX
                StepScale = 1
            End If
        End If

        Dim path1 As New GraphicsPath '配向角
        Dim path2 As New GraphicsPath '配向比
        Dim path3 As New GraphicsPath '伝播速度
        Dim path4 As New GraphicsPath 'TSI

        path1.StartFigure()
        path1.AddLine(graph_x_sta, angle_yaxis_max, graph_x_sta, angle_yaxis_min)
        path1.StartFigure()
        path1.AddLine(graph_x_end, angle_yaxis_max, graph_x_end, angle_yaxis_min)
        path2.StartFigure()
        path2.AddLine(graph_x_sta, ratio_yaxis_max, graph_x_sta, ratio_yaxis_min)
        path2.StartFigure()
        path2.AddLine(graph_x_end, ratio_yaxis_max, graph_x_end, ratio_yaxis_min)
        path3.StartFigure()
        path3.AddLine(graph_x_sta, velo_yaxis_max, graph_x_sta, velo_yaxis_min)
        path3.StartFigure()
        path3.AddLine(graph_x_end, velo_yaxis_max, graph_x_end, velo_yaxis_min)
        path4.StartFigure()
        path4.AddLine(graph_x_sta, tsi_yaxis_max, graph_x_sta, tsi_yaxis_min)
        path4.StartFigure()
        path4.AddLine(graph_x_end, tsi_yaxis_max, graph_x_end, tsi_yaxis_min)

        prf_waku_Xlabel(0) = 1 + ShiftXNum

        'If Points < 10 Then
        If _points < 10 Then
            'If Points > 2 Then
            If _points > 2 Then
                path1.StartFigure()
                path1.AddLine(graph_x_sta + SclX * 1, angle_yaxis_max, graph_x_sta + SclX * 1, angle_yaxis_min)
                path2.StartFigure()
                path2.AddLine(graph_x_sta + SclX * 1, ratio_yaxis_max, graph_x_sta + SclX * 1, ratio_yaxis_min)
                path3.StartFigure()
                path3.AddLine(graph_x_sta + SclX * 1, velo_yaxis_max, graph_x_sta + SclX * 1, velo_yaxis_min)
                path4.StartFigure()
                path4.AddLine(graph_x_sta + SclX * 1, tsi_yaxis_max, graph_x_sta + SclX * 1, tsi_yaxis_min)
                prf_waku_Xlabel(1) = StepScale * 2 + ShiftXNum
            End If
            'If Points > 3 Then
            If _points > 3 Then
                path1.StartFigure()
                path1.AddLine(graph_x_sta + SclX * 2, angle_yaxis_max, graph_x_sta + SclX * 2, angle_yaxis_min)
                path2.StartFigure()
                path2.AddLine(graph_x_sta + SclX * 2, ratio_yaxis_max, graph_x_sta + SclX * 2, ratio_yaxis_min)
                path3.StartFigure()
                path3.AddLine(graph_x_sta + SclX * 2, velo_yaxis_max, graph_x_sta + SclX * 2, velo_yaxis_min)
                path4.StartFigure()
                path4.AddLine(graph_x_sta + SclX * 2, tsi_yaxis_max, graph_x_sta + SclX * 2, tsi_yaxis_min)
                prf_waku_Xlabel(2) = StepScale * 3 + ShiftXNum
            End If
            'If Points > 4 Then
            If _points > 4 Then
                path1.StartFigure()
                path1.AddLine(graph_x_sta + SclX * 3, angle_yaxis_max, graph_x_sta + SclX * 3, angle_yaxis_min)
                path2.StartFigure()
                path2.AddLine(graph_x_sta + SclX * 3, ratio_yaxis_max, graph_x_sta + SclX * 3, ratio_yaxis_min)
                path3.StartFigure()
                path3.AddLine(graph_x_sta + SclX * 3, velo_yaxis_max, graph_x_sta + SclX * 3, velo_yaxis_min)
                path4.StartFigure()
                path4.AddLine(graph_x_sta + SclX * 3, tsi_yaxis_max, graph_x_sta + SclX * 3, tsi_yaxis_min)
                prf_waku_Xlabel(3) = StepScale * 4 + ShiftXNum
            End If
            'If Points > 5 Then
            If _points > 5 Then
                path1.StartFigure()
                path1.AddLine(graph_x_sta + SclX * 4, angle_yaxis_max, graph_x_sta + SclX * 4, angle_yaxis_min)
                path2.StartFigure()
                path2.AddLine(graph_x_sta + SclX * 4, ratio_yaxis_max, graph_x_sta + SclX * 4, ratio_yaxis_min)
                path3.StartFigure()
                path3.AddLine(graph_x_sta + SclX * 4, velo_yaxis_max, graph_x_sta + SclX * 4, velo_yaxis_min)
                path4.StartFigure()
                path4.AddLine(graph_x_sta + SclX * 4, tsi_yaxis_max, graph_x_sta + SclX * 4, tsi_yaxis_min)
                prf_waku_Xlabel(4) = StepScale * 5 + ShiftXNum
            End If
            'If Points > 6 Then
            If _points > 6 Then
                path1.StartFigure()
                path1.AddLine(graph_x_sta + SclX * 5, angle_yaxis_max, graph_x_sta + SclX * 5, angle_yaxis_min)
                path2.StartFigure()
                path2.AddLine(graph_x_sta + SclX * 5, ratio_yaxis_max, graph_x_sta + SclX * 5, ratio_yaxis_min)
                path3.StartFigure()
                path3.AddLine(graph_x_sta + SclX * 5, velo_yaxis_max, graph_x_sta + SclX * 5, velo_yaxis_min)
                path4.StartFigure()
                path4.AddLine(graph_x_sta + SclX * 5, tsi_yaxis_max, graph_x_sta + SclX * 5, tsi_yaxis_min)
                prf_waku_Xlabel(5) = StepScale * 6 + ShiftXNum
            End If
            'If Points > 7 Then
            If _points > 7 Then
                path1.StartFigure()
                path1.AddLine(graph_x_sta + SclX * 6, angle_yaxis_max, graph_x_sta + SclX * 6, angle_yaxis_min)
                path2.StartFigure()
                path2.AddLine(graph_x_sta + SclX * 6, ratio_yaxis_max, graph_x_sta + SclX * 6, ratio_yaxis_min)
                path3.StartFigure()
                path3.AddLine(graph_x_sta + SclX * 6, velo_yaxis_max, graph_x_sta + SclX * 6, velo_yaxis_min)
                path4.StartFigure()
                path4.AddLine(graph_x_sta + SclX * 6, tsi_yaxis_max, graph_x_sta + SclX * 6, tsi_yaxis_min)
                prf_waku_Xlabel(6) = StepScale * 7 + ShiftXNum
            End If
            'If Points > 8 Then
            If _points > 8 Then
                path1.StartFigure()
                path1.AddLine(graph_x_sta + SclX * 7, angle_yaxis_max, graph_x_sta + SclX * 7, angle_yaxis_min)
                path2.StartFigure()
                path2.AddLine(graph_x_sta + SclX * 7, ratio_yaxis_max, graph_x_sta + SclX * 7, ratio_yaxis_min)
                path3.StartFigure()
                path3.AddLine(graph_x_sta + SclX * 7, velo_yaxis_max, graph_x_sta + SclX * 7, velo_yaxis_min)
                path4.StartFigure()
                path4.AddLine(graph_x_sta + SclX * 7, tsi_yaxis_max, graph_x_sta + SclX * 7, tsi_yaxis_min)
                prf_waku_Xlabel(7) = StepScale * 8 + ShiftXNum
            End If
            'If Points > 9 Then
            If _points > 9 Then
                path1.StartFigure()
                path1.AddLine(graph_x_sta + SclX * 8, angle_yaxis_max, graph_x_sta + SclX * 8, angle_yaxis_min)
                path2.StartFigure()
                path2.AddLine(graph_x_sta + SclX * 8, ratio_yaxis_max, graph_x_sta + SclX * 8, ratio_yaxis_min)
                path3.StartFigure()
                path3.AddLine(graph_x_sta + SclX * 8, velo_yaxis_max, graph_x_sta + SclX * 8, velo_yaxis_min)
                path4.StartFigure()
                path4.AddLine(graph_x_sta + SclX * 8, tsi_yaxis_max, graph_x_sta + SclX * 8, tsi_yaxis_min)
                prf_waku_Xlabel(8) = StepScale * 9 + ShiftXNum
            End If
        Else
            For i = 1 To 4
                path1.StartFigure()
                path1.AddLine(graph_x_sta + SclX * i - StepX, angle_yaxis_max, graph_x_sta + SclX * i - StepX, angle_yaxis_min)
                path2.StartFigure()
                path2.AddLine(graph_x_sta + SclX * i - StepX, ratio_yaxis_max, graph_x_sta + SclX * i - StepX, ratio_yaxis_min)
                path3.StartFigure()
                path3.AddLine(graph_x_sta + SclX * i - StepX, velo_yaxis_max, graph_x_sta + SclX * i - StepX, velo_yaxis_min)
                path4.StartFigure()
                path4.AddLine(graph_x_sta + SclX * i - StepX, tsi_yaxis_max, graph_x_sta + SclX * i - StepX, tsi_yaxis_min)
                prf_waku_Xlabel(i) = StepScale * i + ShiftXNum
            Next
            'If Points - StepScale * 4 > StepScale Then
            If _points - StepScale * 4 > StepScale Then
                path1.StartFigure()
                path1.AddLine(graph_x_sta + SclX * 5 - StepX, angle_yaxis_max, graph_x_sta + SclX * 5 - StepX, angle_yaxis_min)
                path2.StartFigure()
                path2.AddLine(graph_x_sta + SclX * 5 - StepX, ratio_yaxis_max, graph_x_sta + SclX * 5 - StepX, ratio_yaxis_min)
                path3.StartFigure()
                path3.AddLine(graph_x_sta + SclX * 5 - StepX, velo_yaxis_max, graph_x_sta + SclX * 5 - StepX, velo_yaxis_min)
                path4.StartFigure()
                path4.AddLine(graph_x_sta + SclX * 5 - StepX, tsi_yaxis_max, graph_x_sta + SclX * 5 - StepX, tsi_yaxis_min)
                prf_waku_Xlabel(5) = StepScale * 5 + ShiftXNum
            End If
            'If Points - StepScale * 5 > StepScale And FlgProfile <> 3 Then
            If _points - StepScale * 5 > StepScale And FlgProfile <> 3 Then
                path1.StartFigure()
                path1.AddLine(graph_x_sta + SclX * 6 - StepX, angle_yaxis_max, graph_x_sta + SclX * 6 - StepX, angle_yaxis_min)
                path2.StartFigure()
                path2.AddLine(graph_x_sta + SclX * 6 - StepX, ratio_yaxis_max, graph_x_sta + SclX * 6 - StepX, ratio_yaxis_min)
                path3.StartFigure()
                path3.AddLine(graph_x_sta + SclX * 6 - StepX, velo_yaxis_max, graph_x_sta + SclX * 6 - StepX, velo_yaxis_min)
                path4.StartFigure()
                path4.AddLine(graph_x_sta + SclX * 6 - StepX, tsi_yaxis_max, graph_x_sta + SclX * 6 - StepX, tsi_yaxis_min)
                prf_waku_Xlabel(6) = StepScale * 6
            End If
        End If
        prf_waku_angle_Xpath.Add(path1)
        prf_waku_ratio_Xpath.Add(path2)
        prf_waku_velo_Xpath.Add(path3)
        prf_waku_tsi_Xpath.Add(path4)
    End Sub

    Private Sub ClsMeasDataPrf()
        Dim N As Integer
        Dim Kt As Long

        Kt = MeasDataMax
        If FileDataMax > Kt Then
            Kt = FileDataMax
        End If

        For KdData = 0 To 3
            For N = 1 To 11
                DataPrcStr(KdData, SampleNo, N) = ""
            Next

            For N = 1 To 18
                DataPrcNum(KdData, SampleNo, N) = 0
            Next
        Next
    End Sub

    Private Sub ClsFileDataPrf()
        Dim N As Integer

        For KdData = 0 To 10
            For SampleNo = 1 To 1000
                For N = 1 To 11
                    DataFileStr(KdData, SampleNo, N) = ""
                Next

                For N = 1 To 18
                    DataFileNum(KdData, SampleNo, N) = 0
                Next
            Next
        Next
    End Sub

    Private Sub ClsDataAvg()
        Dim M As Long
        Dim N As Long

        For M = 1 To 30000
            For N = 1 To 11
                DataPrcStr(0, M, N) = ""
            Next

            For N = 1 To 20
                DataPrcNum(0, M, N) = 0
            Next
        Next
    End Sub

    Private Sub ClsCurInfoPrf()
        TxtMachNoCur.Text = ""
        TxtSmplNamCur.Text = ""
        TxtMarkCur.Text = ""
        TxtMeasNumCur.Text = ""
        TxtMeasLotCur.Text = ""

        TxtLength.Text = ""
        TxtPitch.Text = ""
        TxtPoints.Text = ""
    End Sub

    Private Sub ClsBakInfoPrf()
        '過去データのテキストボックスをクリアする
        'ClsFConditionMeas()の代わり
        TxtMachNoBak.Text = ""
        TxtSmplNamBak.Text = ""
        TxtMarkBak.Text = ""
        TxtMeasNumBak.Text = ""
        TxtMeasLotBak.Text = ""

        TxtLengthOld.Text = ""
        TxtPitchOld.Text = ""
        TxtPointsOld.Text = ""

    End Sub

    Private Sub ClsMaxMinInit()
        DataMax1TSI(KdData) = 0
        DataMin1TSI(KdData) = 0
        DataInt1TSI(KdData) = 0
        PosX1(KdData) = 0
        PosX2(KdData) = 0
        DataMax2TSI(KdData) = 0
        DataMin2TSI(KdData) = 0
        DataInt2TSI(KdData) = 0
        DataMax1Angle(KdData) = 0
        DataMin1Angle(KdData) = 0
        DataInt1Angle(KdData) = 0
        DataMax2Angle(KdData) = 0
        DataMin2Angle(KdData) = 0
        DataInt2Angle(KdData) = 0
        DataMax1VelocityM(KdData) = 0
        DataMin1VelocityM(KdData) = 0
        DataInt1VelocityM(KdData) = 0
        DataMax2VelocityM(KdData) = 0
        DataMin2VelocityM(KdData) = 0
        DataInt2VelocityM(KdData) = 0
        DataMax1VelocityP(KdData) = 0
        DataMin1VelocityP(KdData) = 0
        DataInt1VelocityP(KdData) = 0
        DataMax2VelocityP(KdData) = 0
        DataMin2VelocityP(KdData) = 0
        DataInt2VelocityP(KdData) = 0
        DataMax1RatioM(KdData) = 0
        DataMin1RatioM(KdData) = 0
        DataInt1RatioM(KdData) = 0
        DataMax1RatioP(KdData) = 0
        DataMin1RatioP(KdData) = 0
        DataInt1RatioP(KdData) = 0
    End Sub

    Private Sub FrmSST4500_1_0_0E_profile_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            CmdMeas.Enabled = False
            MeasStartToolStripMenuItem.Enabled = False
            MeasStopToolStripMenuItem.Enabled = False
            CmdAvg.Enabled = False
            AvgToolStripMenuItem.Enabled = False

            OldDataToolStripMenuItem.Enabled = False
            CmdOldDataLoad.Enabled = False
            LoadToolStripMenuItem.Enabled = False

            CmdPrfPrint.Enabled = False
            ManualPrintToolStripMenuItem.Enabled = False
            CmdPrfResultSave.Enabled = False
            SaveExcelToolStripMenuItem1.Enabled = False
            CmdMeasSpecSave.Enabled = True
            保存ToolStripMenuItem.Enabled = True
            CmdMeasSpecSel.Enabled = True
            SelectToolStripMenuItem.Enabled = True
            CmdQuitProfile.Enabled = True
            QuitToolStripMenuItem.Enabled = True
            GbPrfSpec.Enabled = True
            AngRatToolStripMenuItem.Enabled = True
            VeloTSIToolStripMenuItem.Enabled = True
            MeasDataTableToolStripMenuItem.Enabled = True
            OldDataTableToolStripMenuItem.Enabled = True
            AvgDataTableToolStripMenuItem.Enabled = True
            TxtMachNoCur.Enabled = True
            TxtSmplNamCur.Enabled = True
            TxtMarkCur.Enabled = True
            LblAngCenter.Enabled = True
            CmdAngleRange.Enabled = True
            CmdVeloRange.Enabled = True
            CmdTSIRange.Enabled = True
            If FlgAdmin <> 0 Then
                TxtMachNoBak.Enabled = True
                TxtSmplNamBak.Enabled = True
                TxtMarkBak.Enabled = True
            End If
            If OptMm.Checked = True Then
                MmToolStripMenuItem.CheckState = CheckState.Indeterminate
                InchToolStripMenuItem.CheckState = CheckState.Unchecked
            Else
                MmToolStripMenuItem.CheckState = CheckState.Unchecked
                InchToolStripMenuItem.CheckState = CheckState.Indeterminate
            End If

            TimProfile.Enabled = True

            DrawCalcCurData_init()
            DrawCalcBakData_init()
            DrawCalcAvgData_init()
            DrawTableData_init()

            timerCount1 = 0

            LoadConstPitch_FileErr_Run = 0

            prf_dbf_chg(FlgDBF)
        Else
            '画面非表示になるとき
            ClsNoPrf()
            DrawCalcCurData_init()
            DrawCalcBakData_init()
            DrawCalcAvgData_init()
            DrawTableData_init()
            GraphInitPrf(Points)

            ClsCurInfoPrf()
            'ClsBakInfoPrn()はGraphInitPrf()内で実行済み
        End If
    End Sub

    Private Sub CmdQuitProfile_Click(sender As Object, e As EventArgs) Handles CmdQuitProfile.Click
        If CmdQuitProfile.Text = StrMeasStop Then
            'FlgProfile = 3のとき(MD長尺サンプル)
            CmdQuitProfile.Enabled = False
            QuitToolStripMenuItem.Enabled = False
            FlgStop = 1
            FlgLongMeas = 0
        ElseIf CmdQuitProfile.Text = StrMeasInterrupt Then
            CmdQuitProfile.Enabled = False
            QuitToolStripMenuItem.Enabled = False
            FlgStop = 1
        Else
            Cls_PchExpOld()
            FlgMainProfile = 90
        End If
    End Sub

    Public Sub DrawCalcCurData_init()
        LblAnglePkMaxCur_adm.Text = ""
        LblAnglePkAvgCur_adm.Text = ""
        LblAnglePkMinCur_adm.Text = ""
        LblAngleDpMaxCur_adm.Text = ""
        LblAngleDpAvgCur_adm.Text = ""
        LblAngleDpMinCur_adm.Text = ""
        LblRatioPkDpMaxCur_adm.Text = ""
        LblRatioPkDpAvgCur_adm.Text = ""
        LblRatioPkDpMinCur_adm.Text = ""
        LblRatioMDCDMaxCur_adm.Text = ""
        LblRatioMDCDAvgCur_adm.Text = ""
        LblRatioMDCDMinCur_adm.Text = ""
        LblVeloPkMaxCur_adm.Text = ""
        LblVeloPkAvgCur_adm.Text = ""
        LblVeloPkMinCur_adm.Text = ""
        LblVeloDpMaxCur_adm.Text = ""
        LblVeloDpAvgCur_adm.Text = ""
        LblVeloDpMinCur_adm.Text = ""
        LblVeloMDMaxCur_adm.Text = ""
        LblVeloMDAvgCur_adm.Text = ""
        LblVeloMDMinCur_adm.Text = ""
        LblVeloCDMaxCur_adm.Text = ""
        LblVeloCDAvgCur_adm.Text = ""
        LblVeloCDMinCur_adm.Text = ""
        LblTSIMDMaxCur_adm.Text = ""
        LblTSIMDAvgCur_adm.Text = ""
        LblTSIMDMinCur_adm.Text = ""
        LblTSICDMaxCur_adm.Text = ""
        LblTSICDAvgCur_adm.Text = ""
        LblTSICDMinCur_adm.Text = ""

        LblAnglePkMax_nom.Text = ""
        LblAnglePkAvg_nom.Text = ""
        LblAnglePkMin_nom.Text = ""
        LblAngleDpMax_nom.Text = ""
        LblAngleDpAvg_nom.Text = ""
        LblAngleDpMin_nom.Text = ""
        LblRatioPkDpMax_nom.Text = ""
        LblRatioPkDpAvg_nom.Text = ""
        LblRatioPkDpMin_nom.Text = ""
        LblRatioMDCDMax_nom.Text = ""
        LblRatioMDCDAvg_nom.Text = ""
        LblRatioMDCDMin_nom.Text = ""
        LblVeloPkMax_nom.Text = ""
        LblVeloPkAvg_nom.Text = ""
        LblVeloPkMin_nom.Text = ""
        LblVeloDpMax_nom.Text = ""
        LblVeloDpAvg_nom.Text = ""
        LblVeloDpMin_nom.Text = ""
        LblVeloMDMax_nom.Text = ""
        LblVeloMDAvg_nom.Text = ""
        LblVeloMDMin_nom.Text = ""
        LblVeloCDMax_nom.Text = ""
        LblVeloCDAvg_nom.Text = ""
        LblVeloCDMin_nom.Text = ""
        LblTSIMDMax_nom.Text = ""
        LblTSIMDAvg_nom.Text = ""
        LblTSIMDMin_nom.Text = ""
        LblTSICDMax_nom.Text = ""
        LblTSICDAvg_nom.Text = ""
        LblTSICDMin_nom.Text = ""
    End Sub

    Public Sub DrawCalcBakData_init()
        LblAnglePkMaxBak_adm.Text = ""
        LblAnglePkAvgBak_adm.Text = ""
        LblAnglePkMinBak_adm.Text = ""
        LblAngleDpMaxBak_adm.Text = ""
        LblAngleDpAvgBak_adm.Text = ""
        LblAngleDpMinBak_adm.Text = ""
        LblRatioPkDpMaxBak_adm.Text = ""
        LblRatioPkDpAvgBak_adm.Text = ""
        LblRatioPkDpMinBak_adm.Text = ""
        LblRatioMDCDMaxBak_adm.Text = ""
        LblRatioMDCDAvgBak_adm.Text = ""
        LblRatioMDCDMinBak_adm.Text = ""
        LblVeloPkMaxBak_adm.Text = ""
        LblVeloPkAvgBak_adm.Text = ""
        LblVeloPkMinBak_adm.Text = ""
        LblVeloDpMaxBak_adm.Text = ""
        LblVeloDpAvgBak_adm.Text = ""
        LblVeloDpMinBak_adm.Text = ""
        LblVeloMDMaxBak_adm.Text = ""
        LblVeloMDAvgBak_adm.Text = ""
        LblVeloMDMinBak_adm.Text = ""
        LblVeloCDMaxBak_adm.Text = ""
        LblVeloCDAvgBak_adm.Text = ""
        LblVeloCDMinBak_adm.Text = ""
        LblTSIMDMaxBak_adm.Text = ""
        LblTSIMDAvgBak_adm.Text = ""
        LblTSIMDMinBak_adm.Text = ""
        LblTSICDMaxBak_adm.Text = ""
        LblTSICDAvgBak_adm.Text = ""
        LblTSICDMinBak_adm.Text = ""
    End Sub

    Public Sub DrawCalcAvgData_init()
        LblAnglePkMaxAvg_adm.Text = ""
        LblAnglePkAvgAvg_adm.Text = ""
        LblAnglePkMinAvg_adm.Text = ""
        LblAngleDpMaxAvg_adm.Text = ""
        LblAngleDpAvgAvg_adm.Text = ""
        LblAngleDpMinAvg_adm.Text = ""
        LblRatioPkDpMaxAvg_adm.Text = ""
        LblRatioPkDpAvgAvg_adm.Text = ""
        LblRatioPkDpMinAvg_adm.Text = ""
        LblRatioMDCDMaxAvg_adm.Text = ""
        LblRatioMDCDAvgAvg_adm.Text = ""
        LblRatioMDCDMinAvg_adm.Text = ""
        LblVeloPkMaxAvg_adm.Text = ""
        LblVeloPkAvgAvg_adm.Text = ""
        LblVeloPkMinAvg_adm.Text = ""
        LblVeloDpMaxAvg_adm.Text = ""
        LblVeloDpAvgAvg_adm.Text = ""
        LblVeloDpMinAvg_adm.Text = ""
        LblVeloMDMaxAvg_adm.Text = ""
        LblVeloMDAvgAvg_adm.Text = ""
        LblVeloMDMinAvg_adm.Text = ""
        LblVeloCDMaxAvg_adm.Text = ""
        LblVeloCDAvgAvg_adm.Text = ""
        LblVeloCDMinAvg_adm.Text = ""
        LblTSIMDMaxAvg_adm.Text = ""
        LblTSIMDAvgAvg_adm.Text = ""
        LblTSIMDMinAvg_adm.Text = ""
        LblTSICDMaxAvg_adm.Text = ""
        LblTSICDAvgAvg_adm.Text = ""
        LblTSICDMinAvg_adm.Text = ""
    End Sub

    Public Sub DrawTableData_init()
        LblAnglePkMax_TB.Text = ""
        LblAnglePkAvg_TB.Text = ""
        LblAnglePkMin_TB.Text = ""
        LblAngleDpMax_TB.Text = ""
        LblAngleDpAvg_TB.Text = ""
        LblAngleDpMin_TB.Text = ""
        LblRatioMDCDMax_TB.Text = ""
        LblRatioMDCDAvg_TB.Text = ""
        LblRatioMDCDMin_TB.Text = ""
        LblRatioPkDpMax_TB.Text = ""
        LblRatioPkDpAvg_TB.Text = ""
        LblRatioPkDpMin_TB.Text = ""
        LblVeloMDMax_TB.Text = ""
        LblVeloMDAvg_TB.Text = ""
        LblVeloMDMin_TB.Text = ""
        LblVeloCDMax_TB.Text = ""
        LblVeloCDAvg_TB.Text = ""
        LblVeloCDMin_TB.Text = ""
        LblVeloPkMax_TB.Text = ""
        LblVeloPkAvg_TB.Text = ""
        LblVeloPkMin_TB.Text = ""
        LblVeloDpMax_TB.Text = ""
        LblVeloDpAvg_TB.Text = ""
        LblVeloDpMin_TB.Text = ""
        LblTSIMDMax_TB.Text = ""
        LblTSIMDAvg_TB.Text = ""
        LblTSIMDMin_TB.Text = ""
        LblTSICDMax_TB.Text = ""
        LblTSICDAvg_TB.Text = ""
        LblTSICDMin_TB.Text = ""

        DataGridView1.Rows.Clear()
        For i = 0 To 19
            DataGridView1.Rows.Add()
        Next

        LblAnglePkMaxOld_TB.Text = ""
        LblAnglePkAvgOld_TB.Text = ""
        LblAnglePkMinOld_TB.Text = ""
        LblAngleDpMaxOld_TB.Text = ""
        LblAngleDpAvgOld_TB.Text = ""
        LblAngleDpMinOld_TB.Text = ""
        LblRatioMDCDMaxOld_TB.Text = ""
        LblRatioMDCDAvgOld_TB.Text = ""
        LblRatioMDCDMinOld_TB.Text = ""
        LblRatioPkDpMaxOld_TB.Text = ""
        LblRatioPkDpAvgOld_TB.Text = ""
        LblRatioPkDpMinOld_TB.Text = ""
        LblVeloMDMaxOld_TB.Text = ""
        LblVeloMDAvgOld_TB.Text = ""
        LblVeloMDMinOld_TB.Text = ""
        LblVeloCDMaxOld_TB.Text = ""
        LblVeloCDAvgOld_TB.Text = ""
        LblVeloCDMinOld_TB.Text = ""
        LblVeloPkMaxOld_TB.Text = ""
        LblVeloPkAvgOld_TB.Text = ""
        LblVeloPkMinOld_TB.Text = ""
        LblVeloDpMaxOld_TB.Text = ""
        LblVeloDpAvgOld_TB.Text = ""
        LblVeloDpMinOld_TB.Text = ""
        LblTSIMDMaxOld_TB.Text = ""
        LblTSIMDAvgOld_TB.Text = ""
        LblTSIMDMinOld_TB.Text = ""
        LblTSICDMaxOld_TB.Text = ""
        LblTSICDAvgOld_TB.Text = ""
        LblTSICDMinOld_TB.Text = ""

        DataGridView2.Rows.Clear()
        For i = 0 To 19
            DataGridView2.Rows.Add()
        Next

        LblAnglePkMaxAvg_TB.Text = ""
        LblAnglePkAvgAvg_TB.Text = ""
        LblAnglePkMinAvg_TB.Text = ""
        LblAngleDpMaxAvg_TB.Text = ""
        LblAngleDpAvgAvg_TB.Text = ""
        LblAngleDpMinAvg_TB.Text = ""
        LblRatioMDCDMaxAvg_TB.Text = ""
        LblRatioMDCDAvgAvg_TB.Text = ""
        LblRatioMDCDMinAvg_TB.Text = ""
        LblRatioPkDpMaxAvg_TB.Text = ""
        LblRatioPkDpAvgAvg_TB.Text = ""
        LblRatioPkDpMinAvg_TB.Text = ""
        LblVeloMDMaxAvg_TB.Text = ""
        LblVeloMDAvgAvg_TB.Text = ""
        LblVeloMDMinAvg_TB.Text = ""
        LblVeloCDMaxAvg_TB.Text = ""
        LblVeloCDAvgAvg_TB.Text = ""
        LblVeloCDMinAvg_TB.Text = ""
        LblVeloPkMaxAvg_TB.Text = ""
        LblVeloPkAvgAvg_TB.Text = ""
        LblVeloPkMinAvg_TB.Text = ""
        LblVeloDpMaxAvg_TB.Text = ""
        LblVeloDpAvgAvg_TB.Text = ""
        LblVeloDpMinAvg_TB.Text = ""
        LblTSIMDMaxAvg_TB.Text = ""
        LblTSIMDAvgAvg_TB.Text = ""
        LblTSIMDMinAvg_TB.Text = ""
        LblTSICDMaxAvg_TB.Text = ""
        LblTSICDAvgAvg_TB.Text = ""
        LblTSICDMinAvg_TB.Text = ""

        DataGridView3.Rows.Clear()
        For i = 0 To 19
            DataGridView3.Rows.Add()
        Next
    End Sub

    Private Sub draw_prf_waku_angle_Ylabel(ByVal e As PaintEventArgs)
        Dim fnt As New Font("MS UI Gothic", 9)
        'Const padding_x1 = 20
        'Const padding_x2 = 17
        Const padding_x = 0.5
        Const font_Yoffset = 5
        Dim waku_brush As Brush = New SolidBrush(frm_PrfGraphWaku_color)
        Dim string_tmp As String
        Dim stringSize As SizeF

        'If FlgAngleRange <> 6 Then
        For i = 0 To 8
            If i = 4 Then
                string_tmp = LblAngCenter.Text
                stringSize = e.Graphics.MeasureString(string_tmp, fnt)

                e.Graphics.DrawString(string_tmp, fnt, waku_brush, graph_x_sta - padding_x - stringSize.Width, angle_yaxis_min + (angle_SclY * (i + 1)) - font_Yoffset)
            Else
                string_tmp = prf_waku_angle_Ylabel(i)
                stringSize = e.Graphics.MeasureString(string_tmp, fnt)
                e.Graphics.DrawString(string_tmp, fnt, waku_brush, graph_x_sta - padding_x - stringSize.Width, angle_yaxis_min + (angle_SclY * (i + 1)) - font_Yoffset)
            End If
        Next
        'Else
        'For i = 0 To 8
        'If i = 4 Then
        'e.Graphics.DrawString(LblAngCenter.Text, fnt, Brushes.Black, padding_x1, angle_yaxis_min + (angle_SclY * (i + 1)) - font_Yoffset)
        'Else
        'e.Graphics.DrawString(prf_waku_angle_Ylabel(i), fnt, Brushes.Black, padding_x2, angle_yaxis_min + (angle_SclY * (i + 1)) - font_Yoffset)
        'End If
        'Next
        'End If
    End Sub

    Private Sub draw_prf_waku_angle_Xlabel(ByVal e As PaintEventArgs)
        Dim _Points As Single

        Dim fnt As New Font("MS UI Gothic", 9)
        Dim fnt_8 As New Font("MS UI Gothic", 8)
        Dim waku_brush As Brush = New SolidBrush(frm_PrfGraphWaku_color)
        e.Graphics.DrawString(prf_waku_Xlabel_name, fnt_8, waku_brush, Xlabel_padding, angle_yaxis_max + 1)
        e.Graphics.DrawString(prf_waku_Xlabel(0), fnt, waku_brush, graph_x_sta, angle_yaxis_max)

        If MeasDataNo = 0 And FileDataNo = 0 Then
            If TxtPoints.Text <> "" Then
                _Points = TxtPoints.Text
            Else
                _Points = 0
            End If
        ElseIf MeasDataNo = 0 And FileDataNo <> 0 Then
            If TxtPointsOld.Text <> "" Then
                _Points = TxtPointsOld.Text
                If FlgProfile = 3 Then
                    If _Points < lg_graph_max Then
                        _Points = lg_graph_max
                    End If
                End If
            Else
                _Points = 0
            End If
        Else
            If TxtPoints.Text <> "" Then
                _Points = TxtPoints.Text
            Else
                _Points = 0
            End If
        End If

        If _Points < 10 Then
            If _Points > 2 Then
                e.Graphics.DrawString(prf_waku_Xlabel(1), fnt, waku_brush, graph_x_sta + SclX * 1, angle_yaxis_max)
            End If
            If _Points > 3 Then
                e.Graphics.DrawString(prf_waku_Xlabel(2), fnt, waku_brush, graph_x_sta + SclX * 2, angle_yaxis_max)
            End If
            If _Points > 4 Then
                e.Graphics.DrawString(prf_waku_Xlabel(3), fnt, waku_brush, graph_x_sta + SclX * 3, angle_yaxis_max)
            End If
            If _Points > 5 Then
                e.Graphics.DrawString(prf_waku_Xlabel(4), fnt, waku_brush, graph_x_sta + SclX * 4, angle_yaxis_max)
            End If
            If _Points > 6 Then
                e.Graphics.DrawString(prf_waku_Xlabel(5), fnt, waku_brush, graph_x_sta + SclX * 5, angle_yaxis_max)
            End If
            If _Points > 7 Then
                e.Graphics.DrawString(prf_waku_Xlabel(6), fnt, waku_brush, graph_x_sta + SclX * 6, angle_yaxis_max)
            End If
            If _Points > 8 Then
                e.Graphics.DrawString(prf_waku_Xlabel(7), fnt, waku_brush, graph_x_sta + SclX * 7, angle_yaxis_max)
            End If
            If _Points > 9 Then
                e.Graphics.DrawString(prf_waku_Xlabel(8), fnt, waku_brush, graph_x_sta + SclX * 8, angle_yaxis_max)
            End If
        Else
            For i = 1 To 4
                e.Graphics.DrawString(prf_waku_Xlabel(i), fnt, waku_brush, graph_x_sta + SclX * i - StepX, angle_yaxis_max)
            Next
            If _Points - StepScale * 4 > StepScale Then
                e.Graphics.DrawString(prf_waku_Xlabel(5), fnt, waku_brush, graph_x_sta + SclX * 5 - StepX, angle_yaxis_max)
            End If
            If _Points - StepScale * 5 > StepScale And FlgProfile <> 3 Then
                e.Graphics.DrawString(prf_waku_Xlabel(6), fnt, waku_brush, graph_x_sta + SclX * 6 - StepX, angle_yaxis_max)
            End If
        End If
    End Sub

    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        '配向角グラフ
        Dim pen_waku_1_dot2 As New Pen(frm_PrfGraphWaku_color, 1)
        pen_waku_1_dot2.DashStyle = DashStyle.DashDotDot
        Dim pen_blue_1_dot2 As New Pen(Color.Blue, 1)
        pen_blue_1_dot2.DashStyle = DashStyle.DashDotDot

        Dim waku_brush As Brush = New SolidBrush(frm_PrfGraphWaku_color)
        'Dim pen_blue_1 As New Pen(Color.Blue, 1)
        'Dim pen_blue_2 As New Pen(Color.Blue, 2)
        'Dim pen_red_1 As New Pen(Color.Red, 1)
        'Dim pen_red_2 As New Pen(Color.Red, 2)
        'Dim pen_green_1 As New Pen(Color.Green, 1)
        'Dim pen_brown_1 As New Pen(Color.Brown, 1)

        Dim angdpgraph_pen_1 As New Pen(angdpgraph_color, 1)
        Dim angdpgraph_pen_2 As New Pen(angdpgraph_color, 2)
        Dim angpkgraph_pen_1 As New Pen(angpkgraph_color, 1)
        Dim angpkgraph_pen_2 As New Pen(angpkgraph_color, 2)
        Dim angdpgraph3_pen_1 As New Pen(angdpgraph3_color, 1)
        Dim angpkgraph3_pen_1 As New Pen(angpkgraph3_color, 1)
        Dim angpklabel_brush As Brush = New SolidBrush(angpkgraph_color)
        Dim angdplabel_brush As Brush = New SolidBrush(angdpgraph_color)
        Dim angpklabel3_brush As Brush = New SolidBrush(angpkgraph3_color)
        Dim angdplabel3_brush As Brush = New SolidBrush(angdpgraph3_color)

        Dim stringSize As SizeF

        For Each path As GraphicsPath In prf_waku_angle_Ypath1
            e.Graphics.DrawPath(pen_blue_1_dot2, path)
        Next

        For Each path As GraphicsPath In prf_waku_angle_Ypath2
            e.Graphics.DrawPath(pen_waku_1_dot2, path)
        Next

        For Each path As GraphicsPath In prf_waku_angle_Xpath
            e.Graphics.DrawPath(pen_waku_1_dot2, path)
        Next

        Dim fnt As New Font("MS UI Gothic", 11, FontStyle.Bold)
        If FlgLine = 3 Then
            'e.Graphics.DrawString(prf_waku_angle_Pklabel_name, fnt, Brushes.Blue, 5, 3)
            e.Graphics.DrawString(prf_waku_angle_Pklabel_name, fnt, angpklabel3_brush, 5, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_angle_Pklabel_name, fnt)
            'e.Graphics.DrawString(prf_waku_angle_Dplabel_name, fnt, Brushes.Red, stringSize.Width + 15, 3)
            e.Graphics.DrawString(prf_waku_angle_Dplabel_name, fnt, angdplabel3_brush, stringSize.Width + 15, 3)
        Else
            e.Graphics.DrawString(prf_waku_angle_Pklabel_name, fnt, angpklabel_brush, 5, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_angle_Pklabel_name, fnt)
            e.Graphics.DrawString(prf_waku_angle_Dplabel_name, fnt, angdplabel_brush, stringSize.Width + 15, 3)
        End If

        Dim fnt1 As New Font("MS UI Gothic", 9)
        e.Graphics.RotateTransform(-90.0F)
        e.Graphics.DrawString(prf_waku_angle_Yaxis_label, fnt1, waku_brush, -200, 7)
        e.Graphics.RotateTransform(+90.0F)

        draw_prf_waku_angle_Ylabel(e)
        draw_prf_waku_angle_Xlabel(e)

        'Angle-Peak Graph
        For Each path As GraphicsPath In angle_peak_cur_path
            If FlgLine = 3 Then
                'e.Graphics.DrawPath(pen_brown_1, path)
                e.Graphics.DrawPath(angpkgraph3_pen_1, path)
            ElseIf FlgLine = 2 Then
                'e.Graphics.DrawPath(pen_blue_2, path)
                e.Graphics.DrawPath(angpkgraph_pen_2, path)
            ElseIf FlgLine = 1 Then
                'e.Graphics.DrawPath(pen_blue_1, path)
                e.Graphics.DrawPath(angpkgraph_pen_1, path)
            Else
                'e.Graphics.DrawPath(pen_blue_1, path)
                e.Graphics.DrawPath(angpkgraph_pen_1, path)
            End If
        Next

        'Angle-Deep Graph
        For Each path As GraphicsPath In angle_deep_cur_path
            If FlgLine = 3 Then
                'e.Graphics.DrawPath(pen_green_1, path)
                e.Graphics.DrawPath(angdpgraph3_pen_1, path)
            ElseIf FlgLine = 2 Then
                'e.Graphics.DrawPath(pen_red_2, path)
                e.Graphics.DrawPath(angdpgraph_pen_2, path)
            ElseIf FlgLine = 1 Then
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(angdpgraph_pen_1, path)
            Else
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(angdpgraph_pen_1, path)
            End If
        Next

        'Angle-Peak Graph old
        For Each path As GraphicsPath In angle_peak_old_path
            If FlgLine = 13 Then
                'e.Graphics.DrawPath(pen_brown_1, path)
                e.Graphics.DrawPath(angpkgraph3_pen_1, path)
            ElseIf FlgLine = 12 Then
                'e.Graphics.DrawPath(pen_blue_2, path)
                e.Graphics.DrawPath(angpkgraph_pen_2, path)
            ElseIf FlgLine = 11 Then
                'e.Graphics.DrawPath(pen_blue_1, path)
                e.Graphics.DrawPath(angpkgraph_pen_1, path)
            Else
                'e.Graphics.DrawPath(pen_blue_1, path)
                e.Graphics.DrawPath(angpkgraph_pen_1, path)
            End If
        Next

        'Angle-Deep Graph old
        For Each path As GraphicsPath In angle_deep_old_path
            If FlgLine = 13 Then
                'e.Graphics.DrawPath(pen_green_1, path)
                e.Graphics.DrawPath(angdpgraph3_pen_1, path)
            ElseIf FlgLine = 12 Then
                'e.Graphics.DrawPath(pen_red_2, path)
                e.Graphics.DrawPath(angdpgraph_pen_2, path)
            ElseIf FlgLine = 11 Then
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(angdpgraph_pen_1, path)
            Else
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(angdpgraph_pen_1, path)
            End If
        Next
    End Sub

    Private Sub PictureBox2_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox2.Paint
        '配向比グラフ
        Dim pen_waku_1_dot2 As New Pen(frm_PrfGraphWaku_color) With {.DashStyle = DashStyle.DashDotDot}
        Dim waku_brush As Brush = New SolidBrush(frm_PrfGraphWaku_color)
        'Dim pen_blue_1 As New Pen(Color.Blue, 1)
        'Dim pen_blue_2 As New Pen(Color.Blue, 2)
        'Dim pen_red_1 As New Pen(Color.Red, 1)
        'Dim pen_red_2 As New Pen(Color.Red, 2)
        'Dim pen_green_1 As New Pen(Color.Green, 1)
        'Dim pen_green_2 As New Pen(Color.Green, 2)
        'Dim pen_brown_2 As New Pen(Color.Brown, 2)

        Dim ratpkdpgraph_color_1 As New Pen(ratpkdpgraph_color, 1)
        Dim ratpkdpgraph_color_2 As New Pen(ratpkdpgraph_color, 2)
        Dim ratmdcdgraph_color_1 As New Pen(ratmdcdgraph_color, 1)
        Dim ratmdcdgraph_color_2 As New Pen(ratmdcdgraph_color, 2)
        Dim ratpkdpgraph3_color_1 As New Pen(ratpkdpgraph3_color, 1)
        Dim ratmdcdgraph3_color_1 As New Pen(ratmdcdgraph3_color, 1)
        Dim ratpkdplabel_brush As Brush = New SolidBrush(ratpkdpgraph_color)
        Dim ratmdcdlabel_brush As Brush = New SolidBrush(ratmdcdgraph_color)
        Dim ratpkdplabel3_brush As Brush = New SolidBrush(ratpkdpgraph3_color)
        Dim ratmdcdlabel3_brush As Brush = New SolidBrush(ratmdcdgraph3_color)

        Dim stringSize As SizeF

        For Each path As GraphicsPath In prf_waku_ratio_Ypath
            e.Graphics.DrawPath(pen_waku_1_dot2, path)
        Next

        For Each path As GraphicsPath In prf_waku_ratio_Xpath
            e.Graphics.DrawPath(pen_waku_1_dot2, path)
        Next

        Dim fnt As New Font("MS UI Gothic", 11, FontStyle.Bold)
        If FlgLine = 3 Then
            'e.Graphics.DrawString(prf_waku_ratio_MDCDlabel_name, fnt, Brushes.Red, 5, 3)
            e.Graphics.DrawString(prf_waku_ratio_MDCDlabel_name, fnt, ratmdcdlabel3_brush, 5, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_ratio_MDCDlabel_name, fnt)
            'e.Graphics.DrawString(prf_waku_ratio_PkDplabel_name, fnt, Brushes.Green, stringSize.Width + 15, 3)
            e.Graphics.DrawString(prf_waku_ratio_PkDplabel_name, fnt, ratpkdplabel3_brush, stringSize.Width + 15, 3)
        Else
            e.Graphics.DrawString(prf_waku_ratio_MDCDlabel_name, fnt, ratmdcdlabel_brush, 5, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_ratio_MDCDlabel_name, fnt)
            e.Graphics.DrawString(prf_waku_ratio_PkDplabel_name, fnt, ratpkdplabel_brush, stringSize.Width + 15, 3)
        End If

        Dim fnt1 As New Font("MS UI Gothic", 9)
        e.Graphics.RotateTransform(-90.0F)
        e.Graphics.DrawString(prf_waku_ratio_Yaxis_label, fnt1, waku_brush, -185, 7)
        e.Graphics.RotateTransform(+90.0F)

        draw_prf_waku_ratio_Ylabel(e)
        draw_prf_waku_ratio_Xlabel(e)

        'Ratio-Peak/Deep Graph
        For Each path As GraphicsPath In ratio_pkdp_cur_path
            If FlgLine = 3 Then
                'e.Graphics.DrawPath(pen_blue_2, path)
                e.Graphics.DrawPath(ratpkdpgraph3_color_1, path)
            ElseIf FlgLine = 2 Then
                'e.Graphics.DrawPath(pen_green_2, path)
                e.Graphics.DrawPath(ratpkdpgraph_color_2, path)
            ElseIf FlgLine = 1 Then
                'e.Graphics.DrawPath(pen_green_1, path)
                e.Graphics.DrawPath(ratpkdpgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_green_1, path)
                e.Graphics.DrawPath(ratpkdpgraph_color_1, path)
            End If
        Next

        'Ratio-MD/CD Graph
        For Each path As GraphicsPath In ratio_mdcd_cur_path
            If FlgLine = 3 Then
                'e.Graphics.DrawPath(pen_brown_2, path)
                e.Graphics.DrawPath(ratmdcdgraph3_color_1, path)
            ElseIf FlgLine = 2 Then
                'e.Graphics.DrawPath(pen_red_2, path)
                e.Graphics.DrawPath(ratmdcdgraph_color_2, path)
            ElseIf FlgLine = 1 Then
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(ratmdcdgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(ratmdcdgraph_color_1, path)
            End If
        Next

        'Ratio-Peak/Deep Graph old
        For Each path As GraphicsPath In ratio_pkdp_old_path
            If FlgLine = 13 Then
                'e.Graphics.DrawPath(pen_blue_2, path)
                e.Graphics.DrawPath(ratpkdpgraph3_color_1, path)
            ElseIf FlgLine = 12 Then
                'e.Graphics.DrawPath(pen_green_2, path)
                e.Graphics.DrawPath(ratpkdpgraph_color_2, path)
            ElseIf FlgLine = 11 Then
                'e.Graphics.DrawPath(pen_green_1, path)
                e.Graphics.DrawPath(ratpkdpgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_green_1, path)
                e.Graphics.DrawPath(ratpkdpgraph_color_1, path)
            End If
        Next

        'Ratio-MD/CD Graph
        For Each path As GraphicsPath In ratio_mdcd_old_path
            If FlgLine = 13 Then
                'e.Graphics.DrawPath(pen_brown_2, path)
                e.Graphics.DrawPath(ratmdcdgraph3_color_1, path)
            ElseIf FlgLine = 12 Then
                'e.Graphics.DrawPath(pen_red_2, path)
                e.Graphics.DrawPath(ratmdcdgraph_color_2, path)
            ElseIf FlgLine = 11 Then
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(ratmdcdgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(ratmdcdgraph_color_1, path)
            End If
        Next

    End Sub

    Private Sub PictureBox3_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox3.Paint
        '伝播速度グラフ
        Dim pen_waku_1_dot2 As New Pen(frm_PrfGraphWaku_color, 1) With {.DashStyle = DashStyle.DashDotDot}
        Dim stringSize As SizeF
        Dim stringSize_width As Single
        Dim waku_brush As Brush = New SolidBrush(frm_PrfGraphWaku_color)
        'Dim pen_blue_1 As New Pen(Color.Blue, 1)
        'Dim pen_blue_2 As New Pen(Color.Blue, 2)
        'Dim pen_red_1 As New Pen(Color.Red, 1)
        'Dim pen_red_2 As New Pen(Color.Red, 2)
        'Dim pen_brown_2 As New Pen(Color.Brown, 2)
        'Dim pen_green_2 As New Pen(Color.Green, 2)
        'Dim pen_darkgreen_1 As New Pen(Color.DarkGreen, 1)
        'Dim pen_darkgreen_2 As New Pen(Color.DarkGreen, 2)
        'Dim pen_orange_1 As New Pen(Color.Orange, 1)
        'Dim pen_orange_2 As New Pen(Color.Orange, 2)
        Dim velomdgraph_color_1 As New Pen(velomdgraph_color, 1)
        Dim velomdgraph_color_2 As New Pen(velomdgraph_color, 2)
        Dim velocdgraph_color_1 As New Pen(velocdgraph_color, 1)
        Dim velocdgraph_color_2 As New Pen(velocdgraph_color, 2)
        Dim velopkgraph_color_1 As New Pen(velopkgraph_color, 1)
        Dim velopkgraph_color_2 As New Pen(velopkgraph_color, 2)
        Dim velodpgraph_color_1 As New Pen(velodpgraph_color, 1)
        Dim velodpgraph_color_2 As New Pen(velodpgraph_color, 2)
        Dim velomdgraph3_color_1 As New Pen(velomdgraph3_color, 1)
        Dim velocdgraph3_color_1 As New Pen(velocdgraph3_color, 1)
        Dim velopkgraph3_color_1 As New Pen(velopkgraph3_color, 1)
        Dim velodpgraph3_color_1 As New Pen(velodpgraph3_color, 1)
        Dim velomdlabel_brush As Brush = New SolidBrush(velomdgraph_color)
        Dim velocdlabel_brush As Brush = New SolidBrush(velocdgraph_color)
        Dim velopklabel_brush As Brush = New SolidBrush(velopkgraph_color)
        Dim velodplabel_brush As Brush = New SolidBrush(velodpgraph_color)
        Dim velomdlabel3_brush As Brush = New SolidBrush(velomdgraph3_color)
        Dim velocdlabel3_brush As Brush = New SolidBrush(velocdgraph3_color)
        Dim velopklabel3_brush As Brush = New SolidBrush(velopkgraph3_color)
        Dim velodplabel3_brush As Brush = New SolidBrush(velodpgraph3_color)

        For Each path As GraphicsPath In prf_waku_velo_Ypath
            e.Graphics.DrawPath(pen_waku_1_dot2, path)
        Next

        For Each path As GraphicsPath In prf_waku_velo_Xpath
            e.Graphics.DrawPath(pen_waku_1_dot2, path)
        Next

        Dim fnt As New Font("MS UI Gothic", 11, FontStyle.Bold)
        If FlgLine = 3 Then
            'e.Graphics.DrawString(prf_waku_velo_VMDlabel_name, fnt, Brushes.Blue, 5, 3)
            e.Graphics.DrawString(prf_waku_velo_VMDlabel_name, fnt, velomdlabel3_brush, 5, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_velo_VMDlabel_name, fnt)
            stringSize_width = stringSize.Width
            'e.Graphics.DrawString(prf_waku_velo_VCDlabel_name, fnt, Brushes.Red, stringSize_width + 15, 3)
            e.Graphics.DrawString(prf_waku_velo_VCDlabel_name, fnt, velocdlabel3_brush, stringSize_width + 15, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_velo_VCDlabel_name, fnt)
            stringSize_width += 15 + stringSize.Width
            'e.Graphics.DrawString(prf_waku_velo_VPklabel_name, fnt, Brushes.DarkGreen, stringSize_width + 15, 3)
            e.Graphics.DrawString(prf_waku_velo_VPklabel_name, fnt, velopklabel3_brush, stringSize_width + 15, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_velo_VPklabel_name, fnt)
            stringSize_width += 15 + stringSize.Width
            'e.Graphics.DrawString(prf_waku_velo_VDplabel_name, fnt, Brushes.OrangeRed, stringSize_width + 15, 3)
            e.Graphics.DrawString(prf_waku_velo_VDplabel_name, fnt, velodplabel3_brush, stringSize_width + 15, 3)
        Else
            e.Graphics.DrawString(prf_waku_velo_VMDlabel_name, fnt, velomdlabel_brush, 5, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_velo_VMDlabel_name, fnt)
            stringSize_width = stringSize.Width
            e.Graphics.DrawString(prf_waku_velo_VCDlabel_name, fnt, velocdlabel_brush, stringSize_width + 15, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_velo_VCDlabel_name, fnt)
            stringSize_width += 15 + stringSize.Width
            e.Graphics.DrawString(prf_waku_velo_VPklabel_name, fnt, velopklabel_brush, stringSize_width + 15, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_velo_VPklabel_name, fnt)
            stringSize_width += 15 + stringSize.Width
            e.Graphics.DrawString(prf_waku_velo_VDplabel_name, fnt, velodplabel_brush, stringSize_width + 15, 3)
        End If

        Dim fnt1 As New Font("MS UI Gothic", 9)
        e.Graphics.RotateTransform(-90.0F)
        e.Graphics.DrawString(prf_waku_velo_Yaxis_label, fnt1, waku_brush, -210, 7)
        e.Graphics.RotateTransform(+90.0F)

        draw_prf_waku_velo_Ylabel(e)
        draw_prf_waku_velo_Xlabel(e)

        'Velocity-MD
        For Each path As GraphicsPath In velo_md_cur_path
            If FlgLine = 3 Then
                'e.Graphics.DrawPath(pen_brown_2, path)
                e.Graphics.DrawPath(velomdgraph3_color_1, path)
            ElseIf FlgLine = 2 Then
                'e.Graphics.DrawPath(pen_blue_2, path)
                e.Graphics.DrawPath(velomdgraph_color_2, path)
            ElseIf FlgLine = 1 Then
                'e.Graphics.DrawPath(pen_blue_1, path)
                e.Graphics.DrawPath(velomdgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_blue_1, path)
                e.Graphics.DrawPath(velomdgraph_color_1, path)
            End If
        Next

        'Velocity-CD
        For Each path As GraphicsPath In velo_cd_cur_path
            If FlgLine = 3 Then
                'e.Graphics.DrawPath(pen_brown_2, path)
                e.Graphics.DrawPath(velocdgraph3_color_1, path)
            ElseIf FlgLine = 2 Then
                'e.Graphics.DrawPath(pen_red_2, path)
                e.Graphics.DrawPath(velocdgraph_color_2, path)
            ElseIf FlgLine = 1 Then
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(velocdgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(velocdgraph_color_1, path)
            End If
        Next

        'Velocity-Peak
        For Each path As GraphicsPath In velo_peak_cur_path
            If FlgLine = 3 Then
                'e.Graphics.DrawPath(pen_green_2, path)
                e.Graphics.DrawPath(velopkgraph3_color_1, path)
            ElseIf FlgLine = 2 Then
                'e.Graphics.DrawPath(pen_darkgreen_2, path)
                e.Graphics.DrawPath(velopkgraph_color_2, path)
            ElseIf FlgLine = 1 Then
                'e.Graphics.DrawPath(pen_darkgreen_1, path)
                e.Graphics.DrawPath(velopkgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_darkgreen_1, path)
                e.Graphics.DrawPath(velopkgraph_color_1, path)
            End If
        Next

        'Velocity-Deep
        For Each path As GraphicsPath In velo_deep_cur_path
            If FlgLine = 3 Then
                'e.Graphics.DrawPath(pen_green_2, path)
                e.Graphics.DrawPath(velodpgraph3_color_1, path)
            ElseIf FlgLine = 2 Then
                'e.Graphics.DrawPath(pen_orange_2, path)
                e.Graphics.DrawPath(velodpgraph_color_2, path)
            ElseIf FlgLine = 1 Then
                'e.Graphics.DrawPath(pen_orange_1, path)
                e.Graphics.DrawPath(velodpgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_orange_1, path)
                e.Graphics.DrawPath(velodpgraph_color_1, path)
            End If
        Next

        'Velocity-MD old
        For Each path As GraphicsPath In velo_md_old_path
            If FlgLine = 13 Then
                'e.Graphics.DrawPath(pen_brown_2, path)
                e.Graphics.DrawPath(velomdgraph3_color_1, path)
            ElseIf FlgLine = 12 Then
                'e.Graphics.DrawPath(pen_blue_2, path)
                e.Graphics.DrawPath(velomdgraph_color_2, path)
            ElseIf FlgLine = 11 Then
                'e.Graphics.DrawPath(pen_blue_1, path)
                e.Graphics.DrawPath(velomdgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_blue_1, path)
                e.Graphics.DrawPath(velomdgraph_color_1, path)
            End If
        Next

        'Velocity-CD old
        For Each path As GraphicsPath In velo_cd_old_path
            If FlgLine = 13 Then
                'e.Graphics.DrawPath(pen_brown_2, path)
                e.Graphics.DrawPath(velocdgraph3_color_1, path)
            ElseIf FlgLine = 12 Then
                'e.Graphics.DrawPath(pen_red_2, path)
                e.Graphics.DrawPath(velocdgraph_color_2, path)
            ElseIf FlgLine = 11 Then
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(velocdgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(velocdgraph_color_1, path)
            End If
        Next

        'Velocity-Peak old
        For Each path As GraphicsPath In velo_peak_old_path
            If FlgLine = 13 Then
                'e.Graphics.DrawPath(pen_green_2, path)
                e.Graphics.DrawPath(velopkgraph3_color_1, path)
            ElseIf FlgLine = 12 Then
                'e.Graphics.DrawPath(pen_darkgreen_2, path)
                e.Graphics.DrawPath(velopkgraph_color_2, path)
            ElseIf FlgLine = 11 Then
                'e.Graphics.DrawPath(pen_darkgreen_1, path)
                e.Graphics.DrawPath(velopkgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_darkgreen_1, path)
                e.Graphics.DrawPath(velopkgraph_color_1, path)
            End If
        Next

        'Velocity-Deep old
        For Each path As GraphicsPath In velo_deep_old_path
            If FlgLine = 13 Then
                'e.Graphics.DrawPath(pen_green_2, path)
                e.Graphics.DrawPath(velodpgraph3_color_1, path)
            ElseIf FlgLine = 12 Then
                'e.Graphics.DrawPath(pen_orange_2, path)
                e.Graphics.DrawPath(velodpgraph_color_2, path)
            ElseIf FlgLine = 11 Then
                'e.Graphics.DrawPath(pen_orange_1, path)
                e.Graphics.DrawPath(velodpgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_orange_1, path)
                e.Graphics.DrawPath(velodpgraph_color_1, path)
            End If
        Next

    End Sub

    Private Sub PictureBox4_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox4.Paint
        'TSIグラフ
        Dim pen_waku_1_dot2 As New Pen(frm_PrfGraphWaku_color, 1)
        pen_waku_1_dot2.DashStyle = DashStyle.DashDotDot
        Dim stringSize As SizeF
        Dim waku_brush As Brush = New SolidBrush(frm_PrfGraphWaku_color)
        'Dim pen_blue_1 As New Pen(Color.Blue, 1)
        'Dim pen_blue_2 As New Pen(Color.Blue, 2)
        'Dim pen_red_1 As New Pen(Color.Red, 1)
        'Dim pen_red_2 As New Pen(Color.Red, 2)
        'Dim pen_green_2 As New Pen(Color.Green, 2)
        Dim tsimdgraph_color_1 As New Pen(tsimdgraph_color, 1)
        Dim tsimdgraph_color_2 As New Pen(tsimdgraph_color, 2)
        Dim tsimdgraph3_color_1 As New Pen(tsimdgraph3_color, 1)
        Dim tsicdgraph_color_1 As New Pen(tsicdgraph_color, 1)
        Dim tsicdgraph_color_2 As New Pen(tsicdgraph_color, 2)
        Dim tsicdgraph3_color_1 As New Pen(tsicdgraph3_color, 1)
        Dim tsimdlabel_brush As Brush = New SolidBrush(tsimdgraph_color)
        Dim tsicdlabel_brush As Brush = New SolidBrush(tsicdgraph_color)
        Dim tsimdlabel3_brush As Brush = New SolidBrush(tsimdgraph3_color)
        Dim tsicdlabel3_brush As Brush = New SolidBrush(tsicdgraph3_color)

        For Each path As GraphicsPath In prf_waku_tsi_Ypath
            e.Graphics.DrawPath(pen_waku_1_dot2, path)
        Next

        For Each path As GraphicsPath In prf_waku_tsi_Xpath
            e.Graphics.DrawPath(pen_waku_1_dot2, path)
        Next

        Dim fnt As New Font("MS UI Gothic", 11, FontStyle.Bold)
        If FlgLine = 3 Then
            'e.Graphics.DrawString(prf_waku_tsi_MDlabel_name, fnt, Brushes.Blue, 5, 3)
            e.Graphics.DrawString(prf_waku_tsi_MDlabel_name, fnt, tsimdlabel3_brush, 5, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_tsi_MDlabel_name, fnt)
            'e.Graphics.DrawString(prf_waku_tsi_CDlabel_name, fnt, Brushes.Red, stringSize.Width + 15, 3)
            e.Graphics.DrawString(prf_waku_tsi_CDlabel_name, fnt, tsicdlabel3_brush, stringSize.Width + 15, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_tsi_CDlabel_name, fnt)
        Else
            e.Graphics.DrawString(prf_waku_tsi_MDlabel_name, fnt, tsimdlabel_brush, 5, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_tsi_MDlabel_name, fnt)
            e.Graphics.DrawString(prf_waku_tsi_CDlabel_name, fnt, tsicdlabel_brush, stringSize.Width + 15, 3)
            stringSize = e.Graphics.MeasureString(prf_waku_tsi_CDlabel_name, fnt)
        End If

        Dim fnt1 As New Font("MS UI Gothic", 9)
        e.Graphics.RotateTransform(-90.0F)
        e.Graphics.DrawString(prf_waku_tsi_Yaxis_label, fnt1, waku_brush, -180, 7)
        e.Graphics.RotateTransform(+90.0F)

        draw_prf_waku_tsi_Ylabel(e)
        draw_prf_waku_tsi_Xlabel(e)

        'TSI-MD
        For Each path As GraphicsPath In tsi_md_cur_path
            If FlgLine = 3 Then
                'e.Graphics.DrawPath(pen_green_2, path)
                e.Graphics.DrawPath(tsimdgraph3_color_1, path)
            ElseIf FlgLine = 2 Then
                'e.Graphics.DrawPath(pen_blue_2, path)
                e.Graphics.DrawPath(tsimdgraph_color_2, path)
            ElseIf FlgLine = 1 Then
                'e.Graphics.DrawPath(pen_blue_1, path)
                e.Graphics.DrawPath(tsimdgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_blue_1, path)
                e.Graphics.DrawPath(tsimdgraph_color_1, path)
            End If
        Next

        'TSI-CD
        For Each path As GraphicsPath In tsi_cd_cur_path
            If FlgLine = 3 Then
                'e.Graphics.DrawPath(pen_red_2, path)
                e.Graphics.DrawPath(tsicdgraph3_color_1, path)
            ElseIf FlgLine = 2 Then
                'e.Graphics.DrawPath(pen_red_2, path)
                e.Graphics.DrawPath(tsicdgraph_color_2, path)
            ElseIf FlgLine = 1 Then
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(tsicdgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(tsicdgraph_color_1, path)
            End If
        Next

        'TSI-MD old
        For Each path As GraphicsPath In tsi_md_old_path
            If FlgLine = 13 Then
                'e.Graphics.DrawPath(pen_green_2, path)
                e.Graphics.DrawPath(tsimdgraph3_color_1, path)
            ElseIf FlgLine = 12 Then
                'e.Graphics.DrawPath(pen_blue_2, path)
                e.Graphics.DrawPath(tsimdgraph_color_2, path)
            ElseIf FlgLine = 11 Then
                'e.Graphics.DrawPath(pen_blue_1, path)
                e.Graphics.DrawPath(tsimdgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_blue_1, path)
                e.Graphics.DrawPath(tsimdgraph_color_1, path)
            End If
        Next

        'TSI-CD old
        For Each path As GraphicsPath In tsi_cd_old_path
            If FlgLine = 13 Then
                'e.Graphics.DrawPath(pen_red_2, path)
                e.Graphics.DrawPath(tsicdgraph3_color_1, path)
            ElseIf FlgLine = 12 Then
                'e.Graphics.DrawPath(pen_red_2, path)
                e.Graphics.DrawPath(tsicdgraph_color_2, path)
            ElseIf FlgLine = 11 Then
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(tsicdgraph_color_1, path)
            Else
                'e.Graphics.DrawPath(pen_red_1, path)
                e.Graphics.DrawPath(tsicdgraph_color_1, path)
            End If
        Next
    End Sub

    Private Sub draw_prf_waku_tsi_Ylabel(ByVal e As PaintEventArgs)
        Const padding_x = 0.5
        Const font_yoffset = 5
        Dim waku_brush As Brush = New SolidBrush(frm_PrfGraphWaku_color)
        Dim string_tmp As String
        Dim stringSize As SizeF
        Dim fnt As New Font("MS UI Gothic", 9)
        For i = 0 To 3
            string_tmp = prf_waku_tsi_Ylabel(i)
            stringSize = e.Graphics.MeasureString(string_tmp, fnt)
            e.Graphics.DrawString(string_tmp, fnt, waku_brush, graph_x_sta - padding_x - stringSize.Width, tsi_yaxis_min + (tsi_SclY * (i + 1)) - font_yoffset)
        Next
    End Sub

    Private Sub draw_prf_waku_ratio_Ylabel(ByVal e As PaintEventArgs)
        Const padding_x = 1
        Const font_Yoffset = 5
        Dim waku_brush As Brush = New SolidBrush(frm_PrfGraphWaku_color)
        Dim string_tmp As String
        Dim stringSize As SizeF
        Dim fnt As New Font("MS UI Gothic", 9)
        For i = 0 To 3
            string_tmp = prf_waku_ratio_Ylabel(i)
            stringSize = e.Graphics.MeasureString(string_tmp, fnt)
            e.Graphics.DrawString(string_tmp, fnt, waku_brush, graph_x_sta - padding_x - stringSize.Width, tsi_yaxis_min + (tsi_SclY * (i + 1)) - font_Yoffset)
        Next
    End Sub

    Private Sub draw_prf_waku_velo_Ylabel(ByVal e As PaintEventArgs)
        Const padding_x = 0.5
        Const font_Yoffset = 5
        Dim waku_brush As Brush = New SolidBrush(frm_PrfGraphWaku_color)
        Dim string_tmp As String
        Dim stringSize As SizeF
        Dim fnt As New Font("MS UI Gothic", 9)
        For i = 0 To 3
            string_tmp = prf_waku_velo_Ylabel(i)
            stringSize = e.Graphics.MeasureString(string_tmp, fnt)
            e.Graphics.DrawString(string_tmp, fnt, waku_brush, graph_x_sta - padding_x - stringSize.Width, tsi_yaxis_min + (tsi_SclY * (i + 1)) - font_Yoffset)
        Next
    End Sub

    Private Sub draw_prf_waku_ratio_Xlabel(ByVal e As PaintEventArgs)
        Dim _Points As Single
        Dim fnt As New Font("MS UI Gothic", 9)
        Dim fnt_8 As New Font("MS UI Gothic", 8)
        Dim waku_brush As Brush = New SolidBrush(frm_PrfGraphWaku_color)
        e.Graphics.DrawString(prf_waku_Xlabel_name, fnt_8, waku_brush, Xlabel_padding, ratio_yaxis_max + 1)
        e.Graphics.DrawString(prf_waku_Xlabel(0), fnt, waku_brush, graph_x_sta, ratio_yaxis_max)

        If MeasDataNo = 0 And FileDataNo = 0 Then
            If TxtPoints.Text <> "" Then
                _Points = TxtPoints.Text
            Else
                _Points = 0
            End If
        ElseIf MeasDataNo = 0 And FileDataNo <> 0 Then
            If TxtPointsOld.Text <> "" Then
                _Points = TxtPointsOld.Text
                If FlgProfile = 3 Then
                    If _Points < lg_graph_max Then
                        _Points = lg_graph_max
                    End If
                End If
            Else
                _Points = 0
            End If
        Else
            If TxtPoints.Text <> "" Then
                _Points = TxtPoints.Text
            Else
                _Points = 0
            End If
        End If

        If _Points < 10 Then
            If _Points > 2 Then
                e.Graphics.DrawString(prf_waku_Xlabel(1), fnt, waku_brush, graph_x_sta + SclX * 1, ratio_yaxis_max)
            End If
            If _Points > 3 Then
                e.Graphics.DrawString(prf_waku_Xlabel(2), fnt, waku_brush, graph_x_sta + SclX * 2, ratio_yaxis_max)
            End If
            If _Points > 4 Then
                e.Graphics.DrawString(prf_waku_Xlabel(3), fnt, waku_brush, graph_x_sta + SclX * 3, ratio_yaxis_max)
            End If
            If _Points > 5 Then
                e.Graphics.DrawString(prf_waku_Xlabel(4), fnt, waku_brush, graph_x_sta + SclX * 4, ratio_yaxis_max)
            End If
            If _Points > 6 Then
                e.Graphics.DrawString(prf_waku_Xlabel(5), fnt, waku_brush, graph_x_sta + SclX * 5, ratio_yaxis_max)
            End If
            If _Points > 7 Then
                e.Graphics.DrawString(prf_waku_Xlabel(6), fnt, waku_brush, graph_x_sta + SclX * 6, ratio_yaxis_max)
            End If
            If _Points > 8 Then
                e.Graphics.DrawString(prf_waku_Xlabel(7), fnt, waku_brush, graph_x_sta + SclX * 7, ratio_yaxis_max)
            End If
            If _Points > 9 Then
                e.Graphics.DrawString(prf_waku_Xlabel(8), fnt, waku_brush, graph_x_sta + SclX * 8, ratio_yaxis_max)
            End If
        Else
            For i = 1 To 4
                e.Graphics.DrawString(prf_waku_Xlabel(i), fnt, waku_brush, graph_x_sta + SclX * i - StepX, ratio_yaxis_max)
            Next
            If _Points - StepScale * 4 > StepScale Then
                e.Graphics.DrawString(prf_waku_Xlabel(5), fnt, waku_brush, graph_x_sta + SclX * 5 - StepX, ratio_yaxis_max)
            End If
            If _Points - StepScale * 5 > StepScale And FlgProfile <> 3 Then
                e.Graphics.DrawString(prf_waku_Xlabel(6), fnt, waku_brush, graph_x_sta + SclX * 6 - StepX, ratio_yaxis_max)
            End If
        End If
    End Sub

    Private Sub draw_prf_waku_velo_Xlabel(ByVal e As PaintEventArgs)
        Dim _Points As Single
        Dim fnt As New Font("MS UI Gothic", 9)
        Dim fnt_8 As New Font("MS UI Gothic", 8)
        Dim waku_brush As Brush = New SolidBrush(frm_PrfGraphWaku_color)
        e.Graphics.DrawString(prf_waku_Xlabel_name, fnt_8, waku_brush, Xlabel_padding, velo_yaxis_max + 1)
        e.Graphics.DrawString(prf_waku_Xlabel(0), fnt, waku_brush, graph_x_sta, velo_yaxis_max)

        If MeasDataNo = 0 And FileDataNo = 0 Then
            If TxtPoints.Text <> "" Then
                _Points = TxtPoints.Text
            Else
                _Points = 0
            End If
        ElseIf MeasDataNo = 0 And FileDataNo <> 0 Then
            If TxtPointsOld.Text <> "" Then
                _Points = TxtPointsOld.Text
                If FlgProfile = 3 Then
                    If _Points < lg_graph_max Then
                        _Points = lg_graph_max
                    End If
                End If
            Else
                _Points = 0
            End If
        Else
            If TxtPoints.Text <> "" Then
                _Points = TxtPoints.Text
            Else
                _Points = 0
            End If
        End If

        If _Points < 10 Then
            If _Points > 2 Then
                e.Graphics.DrawString(prf_waku_Xlabel(1), fnt, waku_brush, graph_x_sta + SclX * 1, velo_yaxis_max)
            End If
            If _Points > 3 Then
                e.Graphics.DrawString(prf_waku_Xlabel(2), fnt, waku_brush, graph_x_sta + SclX * 2, velo_yaxis_max)
            End If
            If _Points > 4 Then
                e.Graphics.DrawString(prf_waku_Xlabel(3), fnt, waku_brush, graph_x_sta + SclX * 3, velo_yaxis_max)
            End If
            If _Points > 5 Then
                e.Graphics.DrawString(prf_waku_Xlabel(4), fnt, waku_brush, graph_x_sta + SclX * 4, velo_yaxis_max)
            End If
            If _Points > 6 Then
                e.Graphics.DrawString(prf_waku_Xlabel(5), fnt, waku_brush, graph_x_sta + SclX * 5, velo_yaxis_max)
            End If
            If _Points > 7 Then
                e.Graphics.DrawString(prf_waku_Xlabel(6), fnt, waku_brush, graph_x_sta + SclX * 6, velo_yaxis_max)
            End If
            If _Points > 8 Then
                e.Graphics.DrawString(prf_waku_Xlabel(7), fnt, waku_brush, graph_x_sta + SclX * 7, velo_yaxis_max)
            End If
            If _Points > 9 Then
                e.Graphics.DrawString(prf_waku_Xlabel(8), fnt, waku_brush, graph_x_sta + SclX * 8, velo_yaxis_max)
            End If
        Else
            For i = 1 To 4
                e.Graphics.DrawString(prf_waku_Xlabel(i), fnt, waku_brush, graph_x_sta + SclX * i - StepX, velo_yaxis_max)
            Next
            If _Points - StepScale * 4 > StepScale Then
                e.Graphics.DrawString(prf_waku_Xlabel(5), fnt, waku_brush, graph_x_sta + SclX * 5 - StepX, velo_yaxis_max)
            End If
            If _Points - StepScale * 5 > StepScale And FlgProfile <> 3 Then
                e.Graphics.DrawString(prf_waku_Xlabel(6), fnt, waku_brush, graph_x_sta + SclX * 6 - StepX, velo_yaxis_max)
            End If
        End If
    End Sub

    Private Sub draw_prf_waku_tsi_Xlabel(ByVal e As PaintEventArgs)
        Dim _Points As Single
        Dim fnt As New Font("MS UI Gothic", 9)
        Dim fnt_8 As New Font("MS UI Gothic", 8)
        Dim waku_brush As Brush = New SolidBrush(frm_PrfGraphWaku_color)
        e.Graphics.DrawString(prf_waku_Xlabel_name, fnt_8, waku_brush, Xlabel_padding, tsi_yaxis_max + 1)
        e.Graphics.DrawString(prf_waku_Xlabel(0), fnt, waku_brush, graph_x_sta, tsi_yaxis_max)

        If MeasDataNo = 0 And FileDataNo = 0 Then
            If TxtPoints.Text <> "" Then
                _Points = TxtPoints.Text
            Else
                _Points = 0
            End If
        ElseIf MeasDataNo = 0 And FileDataNo <> 0 Then
            If TxtPointsOld.Text <> "" Then
                _Points = TxtPointsOld.Text
                If FlgProfile = 3 Then
                    If _Points < lg_graph_max Then
                        _Points = lg_graph_max
                    End If
                End If
            Else
                _Points = 0
            End If
        Else
            If TxtPoints.Text <> "" Then
                _Points = TxtPoints.Text
            Else
                _Points = 0
            End If
        End If

        If _Points < 10 Then
            If _Points > 2 Then
                e.Graphics.DrawString(prf_waku_Xlabel(1), fnt, waku_brush, graph_x_sta + SclX * 1, tsi_yaxis_max)
            End If
            If _Points > 3 Then
                e.Graphics.DrawString(prf_waku_Xlabel(2), fnt, waku_brush, graph_x_sta + SclX * 2, tsi_yaxis_max)
            End If
            If _Points > 4 Then
                e.Graphics.DrawString(prf_waku_Xlabel(3), fnt, waku_brush, graph_x_sta + SclX * 3, tsi_yaxis_max)
            End If
            If _Points > 5 Then
                e.Graphics.DrawString(prf_waku_Xlabel(4), fnt, waku_brush, graph_x_sta + SclX * 4, tsi_yaxis_max)
            End If
            If _Points > 6 Then
                e.Graphics.DrawString(prf_waku_Xlabel(5), fnt, waku_brush, graph_x_sta + SclX * 5, tsi_yaxis_max)
            End If
            If _Points > 7 Then
                e.Graphics.DrawString(prf_waku_Xlabel(6), fnt, waku_brush, graph_x_sta + SclX * 6, tsi_yaxis_max)
            End If
            If _Points > 8 Then
                e.Graphics.DrawString(prf_waku_Xlabel(7), fnt, waku_brush, graph_x_sta + SclX * 7, tsi_yaxis_max)
            End If
            If _Points > 9 Then
                e.Graphics.DrawString(prf_waku_Xlabel(8), fnt, waku_brush, graph_x_sta + SclX * 8, tsi_yaxis_max)
            End If
        Else
            For i = 1 To 4
                e.Graphics.DrawString(prf_waku_Xlabel(i), fnt, waku_brush, graph_x_sta + SclX * i - StepX, tsi_yaxis_max)
            Next
            If _Points - StepScale * 4 > StepScale Then
                e.Graphics.DrawString(prf_waku_Xlabel(5), fnt, waku_brush, graph_x_sta + SclX * 5 - StepX, tsi_yaxis_max)
            End If
            If _Points - StepScale * 5 > StepScale And FlgProfile <> 3 Then
                e.Graphics.DrawString(prf_waku_Xlabel(6), fnt, waku_brush, graph_x_sta + SclX * 6 - StepX, tsi_yaxis_max)
            End If
        End If
    End Sub

    Private Sub TxtMachNoCur_TextChanged(sender As Object, e As EventArgs) Handles TxtMachNoCur.TextChanged
        MachineNo = TxtMachNoCur.Text
        If FlgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text2)
        End If
        FlgMainProfile = 20
    End Sub

    Private Sub TxtSmplNamCur_TextChanged(sender As Object, e As EventArgs) Handles TxtSmplNamCur.TextChanged
        Sample = TxtSmplNamCur.Text
        If FlgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text2)
        End If
        FlgMainProfile = 20
    End Sub

    Private Sub TxtMarkCur_TextChanged(sender As Object, e As EventArgs) Handles TxtMarkCur.TextChanged
        Mark = TxtMarkCur.Text
        If FlgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text2)
        End If
        FlgMainProfile = 20
    End Sub

    Private Sub TxtPitch_Validating(sender As Object, e As CancelEventArgs) Handles TxtPitch.Validating
        Debug.Print("txtPitch.Validating")
        Dim pitch_tmp_inch As Single

        If IsNumeric(TxtPitch.Text) = False Then
            MessageBox.Show("Enter Pitch",
                            StrInputErr,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
            '数値でない場合、エラーメッセージを出して前回データを復元
            If FlgInch = 0 Then
                'mm
                TxtPitch.Text = Pitch
            Else
                'inch
                TxtPitch.Text = Math.Round(Pitch / 25.4, 2, MidpointRounding.AwayFromZero)
            End If
        Else
            If FlgInch = 0 Then
                'mm
                Pitch_tmp = Math.Truncate(Val(TxtPitch.Text))
                TxtPitch.Text = Pitch_tmp
            Else
                'inch
                pitch_tmp_inch = Math.Round(Val(TxtPitch.Text), 2, MidpointRounding.AwayFromZero)
                TxtPitch.Text = pitch_tmp_inch
                Pitch_tmp = Math.Round(pitch_tmp_inch * 25.4, 0, MidpointRounding.AwayFromZero)
            End If

            If FlgProfile <> 3 Then
                Length_tmp = Length
            Else
                Length_tmp = (Pitch_tmp * (lg_sample_max - 1)) + LnCmp
            End If

            If Pitch_tmp < min_Pitch Then
                '最小ピッチ(10mm)未満は10mmに強制的に設定する
                MessageBox.Show("As the pitch must be more than " & min_Pitch & "mm," & vbCrLf &
                                "the pitch was corrected to " & min_Pitch & "mm",
                                "Correct Pitch",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Exclamation)
                If FlgInch = 0 Then
                    'mm
                    TxtPitch.Text = min_Pitch
                Else
                    'inch
                    TxtPitch.Text = Math.Round(min_Pitch / 25.4, 2, MidpointRounding.AwayFromZero)
                End If
            ElseIf Pitch_tmp > Length_tmp - LnCmp Then
                '有効長以上の場合、エラーメッセージヲ出して前回データを復元する
                MessageBox.Show("Total pitch should be less than" & vbCrLf &
                                "Sample length - " & LnCmp & "mm (both edge length correction)",
                                StrInputErr,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
                If FlgInch = 0 Then
                    'mm
                    TxtPitch.Text = Pitch
                Else
                    'inch
                    TxtPitch.Text = Math.Round(Pitch / 25.4, 2, MidpointRounding.AwayFromZero)
                End If
            ElseIf Pitch_tmp > max_Pitch Then
                '最大ピッチ(9999mm)以上は9999mmに強制的に設定する
                MessageBox.Show("As the pitch must be less than " & max_Pitch & "mm," & vbCrLf &
                                "the pitch was corrected to " & max_Pitch & "mm",
                                "Correct Pitch",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
                If FlgInch = 0 Then
                    'mm
                    TxtPitch.Text = max_Pitch
                Else
                    'inch
                    TxtPitch.Text = Math.Round(max_Pitch / 25.4, 2, MidpointRounding.AwayFromZero)
                End If
            End If
        End If
        'この後Validatedイベントが発生する
    End Sub

    Private Sub TxtPitch_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPitch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Debug.Print("TxtPitch.KeyDown Enter")
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        End If
    End Sub

    Private Sub TxtPitch_Validated(sender As Object, e As EventArgs) Handles TxtPitch.Validated
        Debug.Print("TxtPitch.Validated")

        If FlgInch = 0 Then
            'mm
            Pitch_tmp = TxtPitch.Text
        Else
            'inch
            Pitch_tmp = Math.Round(Val(TxtPitch.Text) * 25.4, 0, MidpointRounding.AwayFromZero)
        End If

        If Pitch = Pitch_tmp Then
            Debug.Print("TxtPitch.Validated Exit")
            FlgMainProfile = 0
        Else
            If FlgInitEnd = 1 Then
                ConstChangeTrue(Me, title_text2)
            End If
            FlgMainProfile = 23
        End If
    End Sub

    Private Sub TxtPoints_Validating(sender As Object, e As CancelEventArgs) Handles TxtPoints.Validating
        '元々は2～1000の範囲を設けていたが、特に意味がなさそうなので、上限は設けない
        '最終的にはピッチの制限を受ける
        Debug.Print("TxtPoints.Validating")

        If IsNumeric(TxtPoints.Text) = False Then
            MessageBox.Show("Enter Number of Meas.Position",
                            StrInputErr,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
            '前回データを復元
            TxtPoints.Text = Points
        Else
            TxtPoints.Text = Math.Round(Val(TxtPoints.Text), 0, MidpointRounding.AwayFromZero)
            If TxtPoints.Text < min_Points Then
                MessageBox.Show("As the number of total meas.point must be than " & min_Points & "," & vbCrLf &
                                "the number of point is corrected to " & min_Points & ".",
                                "Correct Number",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
                TxtPoints.Text = min_Points
            End If
        End If
        'この後Validatedイベントが発生する
    End Sub

    Private Sub TxtPoints_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPoints.KeyDown
        If e.KeyCode = Keys.Enter Then
            Debug.Print("TxtPoints.KeyDown Enter")
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        End If
    End Sub

    Private Sub TxtPoints_Validated(sender As Object, e As EventArgs) Handles TxtPoints.Validated
        Debug.Print("TxtPoints.Validated")
        If Points = TxtPoints.Text Then
            Debug.Print("TxtPoints.Validated Exit")
            FlgMainProfile = 0
        Else
            If FlgInitEnd = 1 Then
                ConstChangeTrue(Me, title_text2)
            End If
            FlgMainProfile = 22
        End If
    End Sub

    Private Sub TxtLength_Validating(sender As Object, e As CancelEventArgs) Handles TxtLength.Validating
        Debug.Print("TxtLength.Validating")
        Dim length_tmp_inch As Single

        If IsNumeric(TxtLength.Text) = False Then
            MessageBox.Show(StrEntNo,
                            StrInputErr,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
            '前回データを復元
            If FlgInch = 0 Then
                'mm
                TxtLength.Text = Length
            Else
                'inch
                TxtLength.Text = Math.Round(Length / 25.4, 2, MidpointRounding.AwayFromZero)
            End If
        Else
            If FlgInch = 0 Then
                'mm
                Length_tmp = Math.Truncate(Val(TxtLength.Text))
                TxtLength.Text = Length_tmp
            Else
                'inch
                length_tmp_inch = Math.Round(Val(TxtLength.Text), 2, MidpointRounding.AwayFromZero)
                TxtLength.Text = length_tmp_inch
                Length_tmp = Math.Round(length_tmp_inch * 25.4, 0, MidpointRounding.AwayFromZero)
            End If

            If Length_tmp < LnCmp + min_Pitch Then
                MessageBox.Show("Input More Than " & LnCmp + min_Pitch & "mm",
                                StrInputErr,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)
                '前回データを復元
                If FlgInch = 0 Then
                    'mm
                    TxtLength.Text = Length
                Else
                    'inch
                    TxtLength.Text = Math.Round(Length / 25.4, 2, MidpointRounding.AwayFromZero)
                End If
            End If
        End If
        'この後Validatedイベントが発生する
    End Sub

    Private Sub TxtLength_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtLength.KeyDown
        If e.KeyCode = Keys.Enter Then
            Debug.Print("TxtLength.KeyDown Enter")
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        End If
    End Sub

    Private Sub TxtLength_Validated(sender As Object, e As EventArgs) Handles TxtLength.Validated
        Debug.Print("TxtLength.Validated")

        If FlgInch = 0 Then
            'mm
            Length_tmp = TxtLength.Text
        Else
            'inch
            Length_tmp = Math.Round(Val(TxtLength.Text) * 25.4, 0, MidpointRounding.AwayFromZero)
        End If

        If Length = Length_tmp Then
            Debug.Print("TxtLength.Validated value Not: Changed Exit Sub")
            FlgMainProfile = 0
        Else
            If FlgInitEnd = 1 Then
                ConstChangeTrue(Me, title_text2)
            End If
            FlgMainProfile = 21
        End If
    End Sub

    Private Sub CmdMeas_Click(sender As Object, e As EventArgs) Handles CmdMeas.Click
        '測定仕様未保存時は測定を開始しないのが望ましいが、
        '要望によりyes,noで未保存でも測定開始できるようにする
        Dim ret As DialogResult

        If FlgConstChg = True Then
            If FlgConstChg_MeasStart = False Then
                ret = MessageBox.Show(StrMeasStartMsg,
                                      StrConfirmStartMeas,
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning)
                If ret = vbYes Then
                    FlgConstChg_MeasStart = True
                    MeasRun()
                Else
                    FlgConstChg_MeasStart = False
                    Exit Sub
                End If
            Else
                MeasRun()
            End If
        Else
            MeasRun()
        End If
    End Sub

    Private Sub MeasRun()
        Dim result_tmp As DialogResult
        Dim pitch_unit As String

        If FlgInch = 0 Then
            pitch_unit = "mm"
        Else
            pitch_unit = "inch"
        End If

        If FlgHoldMeas = 0 Then
            '総測定箇所数の確認
            If FlgProfile = 1 Then
                If FlgPitchExp = 0 Then
                    result_tmp = MessageBox.Show("Do you want to start meas. with" & vbCrLf &
                                                 TxtPoints.Text & " total meas.point and this pitch and" & vbCrLf &
                                                 TxtPitch.Text & pitch_unit & " pitches ?",
                                                 "Confirm Total number of Meas.Position and Pitch",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Exclamation)
                Else
                    result_tmp = MessageBox.Show("Do you want to start meas.with " & UBound(PchExp_PchData) + 1 & " meas.pitches ?",
                                                 "Confirm Pitch Setting",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Exclamation)
                End If
                If result_tmp = DialogResult.Yes Then
                    '最初のクリック
                    '1回目の測定開始へ "PCH"送信
                    FlgMainProfile = 2
                End If
            ElseIf FlgProfile = 2 Then
                result_tmp = MessageBox.Show("Do you want to start with " & TxtPoints.Text & " total point",
                                             "Confirm Total Meas.Position",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Exclamation)
                If result_tmp = DialogResult.Yes Then
                    FlgMainProfile = 2
                End If
            Else
                result_tmp = MessageBox.Show("Do you want to start meas. with " & TxtPitch.Text & pitch_unit & " pitches ?",
                                             "Confirm Pitch Setting",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Exclamation)
                If result_tmp = DialogResult.Yes Then
                    FlgMainProfile = 2
                End If
            End If
        End If

        If FlgProfile = 2 And FlgHoldMeas = 2 Then
            'TxtStatusBox.Text = "測定中 "
            'ToolStripStatusLabel4.Text = "測定中 "
            If FlgTest = 0 Then
                '2回目以降の測定開始へ
                FlgMainProfile = 4
            Else
                FlgMainProfile = 100
            End If
        End If

        FlgAvg = 0
        FlgLongMeas = 0
    End Sub

    Private Sub ConditionEnable()
        If FlgPitchExp = 0 Then
            TxtLength.Enabled = True
            TxtPitch.Enabled = True
            TxtPoints.Enabled = True
            OptMm.Enabled = True
            OptInch.Enabled = True
            UnitToolStripMenuItem.Enabled = True
        End If
        TxtMachNoCur.Enabled = True
        TxtSmplNamCur.Enabled = True
        TxtMarkCur.Enabled = True

        GbPrfSpec.Enabled = True
        CmdPrfPrint.Enabled = True
        CmdPrfResultSave.Enabled = True
        ChkPrfAutoPrn.Enabled = True

        AngRatToolStripMenuItem.Enabled = True
        VeloTSIToolStripMenuItem.Enabled = True
        MeasDataTableToolStripMenuItem.Enabled = True
        ManualPrintToolStripMenuItem.Enabled = True
        SaveExcelToolStripMenuItem1.Enabled = True
        AutoPrintToolStripMenuItem.Enabled = True
        SettingToolStripMenuItem1.Enabled = True
        CmdAngleRange.Enabled = True
        CmdVeloRange.Enabled = True
        CmdTSIRange.Enabled = True
        LblAngCenter.Enabled = True
        If FlgAdmin <> 0 Then
            '管理者モード
            TxtMachNoBak.Enabled = True
            TxtSmplNamBak.Enabled = True
            TxtMarkBak.Enabled = True
            CmdOldDataLoad.Enabled = True
            CmdClsGraph.Enabled = True
            LoadToolStripMenuItem.Enabled = True
            GraphDelToolStripMenuItem.Enabled = True
            OldDataTableToolStripMenuItem.Enabled = True
            AvgDataTableToolStripMenuItem.Enabled = True
            '            If FlgProfile = 3 Then
            '           CmdAvg.Enabled = True   'なぜ過去データの有無にかかわらず有効にしているのか？
            'ElseIf MeasDataMax = FileDataMax Then
            If MeasDataMax = FileDataMax Then
                CmdAvg.Enabled = True
                AvgToolStripMenuItem.Enabled = True
            End If
        Else
            'AdmVisible_onofでコントロールされている
            CmdOldDataLoad.Enabled = False
            CmdAvg.Enabled = False
            CmdClsGraph.Enabled = False
            LoadToolStripMenuItem.Enabled = False
            AvgToolStripMenuItem.Enabled = False
            GraphDelToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub ConditionDisable()
        TxtMachNoCur.Enabled = False
        TxtSmplNamCur.Enabled = False
        TxtMarkCur.Enabled = False
        TxtMachNoBak.Enabled = False
        TxtSmplNamBak.Enabled = False
        TxtMarkBak.Enabled = False
        TxtLength.Enabled = False
        TxtPitch.Enabled = False
        TxtPoints.Enabled = False

        UnitToolStripMenuItem.Enabled = False
        LoadToolStripMenuItem.Enabled = False
        GraphDelToolStripMenuItem.Enabled = False
        AvgToolStripMenuItem.Enabled = False
        AngRatToolStripMenuItem.Enabled = False
        VeloTSIToolStripMenuItem.Enabled = False
        MeasDataTableToolStripMenuItem.Enabled = False
        OldDataTableToolStripMenuItem.Enabled = False
        AvgDataTableToolStripMenuItem.Enabled = False
        SaveExcelToolStripMenuItem1.Enabled = False
        ManualPrintToolStripMenuItem.Enabled = False
        AutoPrintToolStripMenuItem.Enabled = False
        SettingToolStripMenuItem1.Enabled = False

        OptMm.Enabled = False
        OptInch.Enabled = False

        CmdOldDataLoad.Enabled = False
        CmdClsGraph.Enabled = False
        CmdAvg.Enabled = False

        GbPrfSpec.Enabled = False
        CmdPrfResultSave.Enabled = False
        CmdPrfPrint.Enabled = False
        ChkPrfAutoPrn.Enabled = False
        CmdAngleRange.Enabled = False
        CmdVeloRange.Enabled = False
        CmdTSIRange.Enabled = False
        LblAngCenter.Enabled = False
    End Sub

    Private Sub DataMaxMinInt()
        Dim Kt As Double
        Dim Ds As String
        Dim Ds_1 As String

        Kt = DataPrcNum(KdData, SampleNo, 3)    'TSI-MD
        If DataMax1TSI(KdData) < Kt Then
            DataMax1TSI(KdData) = Kt
        End If
        If DataMin1TSI(KdData) > Kt Then
            DataMin1TSI(KdData) = Kt
        End If
        DataInt1TSI(KdData) += Kt ^ 2

        Kt = DataPrcNum(KdData, SampleNo, 11)   'TSI-CD
        If DataMax2TSI(KdData) < Kt Then
            DataMax2TSI(KdData) = Kt
        End If
        If DataMin2TSI(KdData) > Kt Then
            DataMin2TSI(KdData) = Kt
        End If
        DataInt2TSI(KdData) += Kt ^ 2

        'If FlgDBF = 0 Then
        Ds = DataPrcStr(KdData, SampleNo, 9)    'OrAngle-Peak
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            Kt = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            Kt = Val(Ds)
        End If
        If DataMax1Angle(KdData) < Kt Then
            DataMax1Angle(KdData) = Kt
        End If
        If DataMin1Angle(KdData) > Kt Then
            DataMin1Angle(KdData) = Kt
        End If
        DataInt1Angle(KdData) += Kt

        'If FlgDBF = 0 Then
        Ds = DataPrcStr(KdData, SampleNo, 8)    'OrAngle-Deep
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            Kt = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            Kt = Val(Ds)
        End If
        If DataMax2Angle(KdData) < Kt Then
            DataMax2Angle(KdData) = Kt
        End If
        If DataMin2Angle(KdData) > Kt Then
            DataMin2Angle(KdData) = Kt
        End If
        DataInt2Angle(KdData) += Kt

        Kt = DataPrcNum(KdData, SampleNo, 2)    'Velocity-Peak
        If DataMax1VelocityP(KdData) < Kt Then
            DataMax1VelocityP(KdData) = Kt
        End If
        If DataMin1VelocityP(KdData) > Kt Then
            DataMin1VelocityP(KdData) = Kt
        End If
        DataInt1VelocityP(KdData) += Kt

        Kt = DataPrcNum(KdData, SampleNo, 1)    'Velocity-Deep
        If DataMax2VelocityP(KdData) < Kt Then
            DataMax2VelocityP(KdData) = Kt
        End If
        If DataMin2VelocityP(KdData) > Kt Then
            DataMin2VelocityP(KdData) = Kt
        End If
        DataInt2VelocityP(KdData) += Kt

        Kt = DataPrcNum(KdData, SampleNo, 3)    'Velocity-MD
        If DataMax1VelocityM(KdData) < Kt Then
            DataMax1VelocityM(KdData) = Kt
        End If
        If DataMin1VelocityM(KdData) > Kt Then
            DataMin1VelocityM(KdData) = Kt
        End If
        DataInt1VelocityM(KdData) += Kt

        Kt = DataPrcNum(KdData, SampleNo, 11)    'Velocity-CD
        If DataMax2VelocityM(KdData) < Kt Then
            DataMax2VelocityM(KdData) = Kt
        End If
        If DataMin2VelocityM(KdData) > Kt Then
            DataMin2VelocityM(KdData) = Kt
        End If
        DataInt2VelocityM(KdData) += Kt

        Kt = Val(DataPrcStr(KdData, SampleNo, 10))  'Ratio MD/CD
        If DataMax1RatioM(KdData) < Kt Then
            DataMax1RatioM(KdData) = Kt
        End If
        If DataMin1RatioM(KdData) > Kt Then
            DataMin1RatioM(KdData) = Kt
        End If
        DataInt1RatioM(KdData) += Kt

        Kt = Val(DataPrcStr(KdData, SampleNo, 11))  'Ratio Peak/Deep
        If DataMax1RatioP(KdData) < Kt Then
            DataMax1RatioP(KdData) = Kt
        End If
        If DataMin1RatioP(KdData) > Kt Then
            DataMin1RatioP(KdData) = Kt
        End If
        DataInt1RatioP(KdData) += Kt
    End Sub

    Private Sub InitializeMaxMinInt()
        DataMax1TSI(KdData) = 0
        DataMin1TSI(KdData) = 999
        DataInt1TSI(KdData) = 0
        PosX1(KdData) = 0
        PosX2(KdData) = 0
        DataMax2TSI(KdData) = 0
        DataMin2TSI(KdData) = 999
        DataInt2TSI(KdData) = 0

        DataMax1Angle(KdData) = -360
        DataMin1Angle(KdData) = 360
        DataInt1Angle(KdData) = 0
        DataMax2Angle(KdData) = -360
        DataMin2Angle(KdData) = 360
        DataInt2Angle(KdData) = 0

        DataMax1VelocityM(KdData) = 0
        DataMin1VelocityM(KdData) = 99
        DataInt1VelocityM(KdData) = 0
        DataMax2VelocityM(KdData) = 0
        DataMin2VelocityM(KdData) = 99
        DataInt2VelocityM(KdData) = 0

        DataMax1VelocityP(KdData) = 0
        DataMin1VelocityP(KdData) = 99
        DataInt1VelocityP(KdData) = 0
        DataMax2VelocityP(KdData) = 0
        DataMin2VelocityP(KdData) = 99
        DataInt2VelocityP(KdData) = 0

        DataMax1RatioM(KdData) = 0
        DataMin1RatioM(KdData) = 99
        DataInt1RatioM(KdData) = 0
        DataMax1RatioP(KdData) = 0
        DataMin1RatioP(KdData) = 99
        DataInt1RatioP(KdData) = 0
    End Sub

    Private Sub PrfSaidDataVelo(ByVal sel As Integer)
        Dim DataMaxP As Single
        Dim DataMinP As Single
        Dim DataAvgP As Single
        Dim DataMaxD As Single
        Dim DataMinD As Single
        Dim DataAvgD As Single
        Dim DataMaxM As Single
        Dim DataMinM As Single
        Dim DataAvgM As Single
        Dim DataMaxC As Single
        Dim DataMinC As Single
        Dim DataAvgC As Single
        Dim SampleNoi As Integer

        If SampleNo < 1 Then
            If KdData = 1 Then
                LblVeloPkMaxCur_adm.Text = "0.00"
                LblVeloPkMinCur_adm.Text = "0.00"
                LblVeloPkAvgCur_adm.Text = "0.00"
                LblVeloPkMax_nom.Text = "0.00"
                LblVeloPkMin_nom.Text = "0.00"
                LblVeloPkAvg_nom.Text = "0.00"
                LblVeloPkMax_TB.Text = "0.00"
                LblVeloPkMin_TB.Text = "0.00"
                LblVeloPkAvg_TB.Text = "0.00"
                LblVeloDpMaxCur_adm.Text = "0.00"
                LblVeloDpMinCur_adm.Text = "0.00"
                LblVeloDpAvgCur_adm.Text = "0.00"
                LblVeloDpMax_nom.Text = "0.00"
                LblVeloDpMin_nom.Text = "0.00"
                LblVeloDpAvg_nom.Text = "0.00"
                LblVeloDpMax_TB.Text = "0.00"
                LblVeloDpMin_TB.Text = "0.00"
                LblVeloDpAvg_TB.Text = "0.00"
                LblVeloMDMaxCur_adm.Text = "0.00"
                LblVeloMDMinCur_adm.Text = "0.00"
                LblVeloMDAvgCur_adm.Text = "0.00"
                LblVeloMDMax_nom.Text = "0.00"
                LblVeloMDMin_nom.Text = "0.00"
                LblVeloMDAvg_nom.Text = "0.00"
                LblVeloMDMax_TB.Text = "0.00"
                LblVeloMDMin_TB.Text = "0.00"
                LblVeloMDAvg_TB.Text = "0.00"
                LblVeloCDMaxCur_adm.Text = "0.00"
                LblVeloCDMinCur_adm.Text = "0.00"
                LblVeloCDAvgCur_adm.Text = "0.00"
                LblVeloCDMax_nom.Text = "0.00"
                LblVeloCDMin_nom.Text = "0.00"
                LblVeloCDAvg_nom.Text = "0.00"
                LblVeloCDMax_TB.Text = "0.00"
                LblVeloCDMin_TB.Text = "0.00"
                LblVeloCDAvg_TB.Text = "0.00"
            ElseIf KdData = 3 Then
                LblVeloPkMaxBak_adm.Text = "0.00"
                LblVeloPkMinBak_adm.Text = "0.00"
                LblVeloPkAvgBak_adm.Text = "0.00"
                LblVeloPkMaxOld_TB.Text = "0.00"
                LblVeloPkMinOld_TB.Text = "0.00"
                LblVeloPkAvgOld_TB.Text = "0.00"
                LblVeloDpMaxBak_adm.Text = "0.00"
                LblVeloDpMinBak_adm.Text = "0.00"
                LblVeloDpAvgBak_adm.Text = "0.00"
                LblVeloDpMaxOld_TB.Text = "0.00"
                LblVeloDpMinOld_TB.Text = "0.00"
                LblVeloDpAvgOld_TB.Text = "0.00"
                LblVeloMDMaxBak_adm.Text = "0.00"
                LblVeloMDMinBak_adm.Text = "0.00"
                LblVeloMDAvgBak_adm.Text = "0.00"
                LblVeloMDMaxOld_TB.Text = "0.00"
                LblVeloMDMinOld_TB.Text = "0.00"
                LblVeloMDAvgOld_TB.Text = "0.00"
                LblVeloCDMaxBak_adm.Text = "0.00"
                LblVeloCDMinBak_adm.Text = "0.00"
                LblVeloCDAvgBak_adm.Text = "0.00"
                LblVeloCDMaxOld_TB.Text = "0.00"
                LblVeloCDMinOld_TB.Text = "0.00"
                LblVeloCDAvgOld_TB.Text = "0.00"
            ElseIf KdData = 0 Then
                LblVeloPkMaxBak_adm.Text = "0.00"
                LblVeloPkMinBak_adm.Text = "0.00"
                LblVeloPkAvgBak_adm.Text = "0.00"
                LblVeloPkMaxAvg_TB.Text = "0.00"
                LblVeloPkMinAvg_TB.Text = "0.00"
                LblVeloPkAvgAvg_TB.Text = "0.00"
                LblVeloDpMaxBak_adm.Text = "0.00"
                LblVeloDpMinBak_adm.Text = "0.00"
                LblVeloDpAvgBak_adm.Text = "0.00"
                LblVeloDpMaxAvg_TB.Text = "0.00"
                LblVeloDpMinAvg_TB.Text = "0.00"
                LblVeloDpAvgAvg_TB.Text = "0.00"
                LblVeloMDMaxBak_adm.Text = "0.00"
                LblVeloMDMinBak_adm.Text = "0.00"
                LblVeloMDAvgBak_adm.Text = "0.00"
                LblVeloMDMaxAvg_TB.Text = "0.00"
                LblVeloMDMinAvg_TB.Text = "0.00"
                LblVeloMDAvgAvg_TB.Text = "0.00"
                LblVeloCDMaxBak_adm.Text = "0.00"
                LblVeloCDMinBak_adm.Text = "0.00"
                LblVeloCDAvgBak_adm.Text = "0.00"
                LblVeloCDMaxAvg_TB.Text = "0.00"
                LblVeloCDMinAvg_TB.Text = "0.00"
                LblVeloCDAvgAvg_TB.Text = "0.00"
            End If
        Else
            'Velocity-Peak
            DataMaxP = DataMax1VelocityP(KdData)
            DataMinP = DataMin1VelocityP(KdData)
            DataAvgP = DataInt1VelocityP(KdData) / SampleNo
            'Velocity-Deep
            DataMaxD = DataMax2VelocityP(KdData)
            DataMinD = DataMin2VelocityP(KdData)
            DataAvgD = DataInt2VelocityP(KdData) / SampleNo
            'Velocity-MD
            DataMaxM = DataMax1VelocityM(KdData)
            DataMinM = DataMin1VelocityM(KdData)
            DataAvgM = DataInt1VelocityM(KdData) / SampleNo
            'Velocity-CD
            DataMaxC = DataMax2VelocityM(KdData)
            DataMinC = DataMin2VelocityM(KdData)
            DataAvgC = DataInt2VelocityM(KdData) / SampleNo

            If KdData = 1 Then
                'Velocity-Peak
                LblVeloPkMaxCur_adm.Text = Strings.Format(DataMaxP, "0.00")
                LblVeloPkAvgCur_adm.Text = Strings.Format(DataAvgP, "0.00")
                LblVeloPkMinCur_adm.Text = Strings.Format(DataMinP, "0.00")
                LblVeloPkMax_nom.Text = Strings.Format(DataMaxP, "0.00")
                LblVeloPkAvg_nom.Text = Strings.Format(DataAvgP, "0.00")
                LblVeloPkMin_nom.Text = Strings.Format(DataMinP, "0.00")

                'Velocity-Peak Table View
                LblVeloPkMax_TB.Text = Strings.Format(DataMaxP, "0.00")
                LblVeloPkAvg_TB.Text = Strings.Format(DataAvgP, "0.00")
                LblVeloPkMin_TB.Text = Strings.Format(DataMinP, "0.00")

                'Velocity-Deep
                LblVeloDpMaxCur_adm.Text = Strings.Format(DataMaxD, "0.00")
                LblVeloDpMinCur_adm.Text = Strings.Format(DataMinD, "0.00")
                LblVeloDpAvgCur_adm.Text = Strings.Format(DataAvgD, "0.00")
                LblVeloDpMax_nom.Text = Strings.Format(DataMaxD, "0.00")
                LblVeloDpMin_nom.Text = Strings.Format(DataMinD, "0.00")
                LblVeloDpAvg_nom.Text = Strings.Format(DataAvgD, "0.00")

                'Velocity-Deep Table View
                LblVeloDpMax_TB.Text = Strings.Format(DataMaxD, "0.00")
                LblVeloDpAvg_TB.Text = Strings.Format(DataAvgD, "0.00")
                LblVeloDpMin_TB.Text = Strings.Format(DataMinD, "0.00")

                'Velocity-MD
                LblVeloMDMaxCur_adm.Text = Strings.Format(DataMaxM, "0.00")
                LblVeloMDMinCur_adm.Text = Strings.Format(DataMinM, "0.00")
                LblVeloMDAvgCur_adm.Text = Strings.Format(DataAvgM, "0.00")
                LblVeloMDMax_nom.Text = Strings.Format(DataMaxM, "0.00")
                LblVeloMDMin_nom.Text = Strings.Format(DataMinM, "0.00")
                LblVeloMDAvg_nom.Text = Strings.Format(DataAvgM, "0.00")

                'Velocity-MD Table View
                LblVeloMDMax_TB.Text = Strings.Format(DataMaxM, "0.00")
                LblVeloMDAvg_TB.Text = Strings.Format(DataAvgM, "0.00")
                LblVeloMDMin_TB.Text = Strings.Format(DataMinM, "0.00")

                'Velocity-CD
                LblVeloCDMaxCur_adm.Text = Strings.Format(DataMaxC, "0.00")
                LblVeloCDMinCur_adm.Text = Strings.Format(DataMinC, "0.00")
                LblVeloCDAvgCur_adm.Text = Strings.Format(DataAvgC, "0.00")
                LblVeloCDMax_nom.Text = Strings.Format(DataMaxC, "0.00")
                LblVeloCDMin_nom.Text = Strings.Format(DataMinC, "0.00")
                LblVeloCDAvg_nom.Text = Strings.Format(DataAvgC, "0.00")

                'Velocity-CD Table View
                LblVeloCDMax_TB.Text = Strings.Format(DataMaxC, "0.00")
                LblVeloCDAvg_TB.Text = Strings.Format(DataAvgC, "0.00")
                LblVeloCDMin_TB.Text = Strings.Format(DataMinC, "0.00")

                If sel = 0 Then
                    'Velocity-MD Table Data
                    DataGridView1.Rows(SampleNo - 1).Cells(5).Value = Strings.Format(DataPrcNum(KdData, SampleNo, 3), "0.00")
                    'Velocity-CD Table Data
                    DataGridView1.Rows(SampleNo - 1).Cells(6).Value = Strings.Format(DataPrcNum(KdData, SampleNo, 11), "0.00")
                    'Velocity-Peak Table Data
                    DataGridView1.Rows(SampleNo - 1).Cells(7).Value = Strings.Format(DataPrcNum(KdData, SampleNo, 2), "0.00")
                    'Velocity-Deep Table Data
                    DataGridView1.Rows(SampleNo - 1).Cells(8).Value = Strings.Format(DataPrcNum(KdData, SampleNo, 1), "0.00")
                ElseIf sel = 1 Then
                    For SampleNoi = 1 To SampleNo
                        'Velocity-MD Table Data
                        DataGridView1.Rows(SampleNoi - 1).Cells(5).Value = Strings.Format(DataPrcNum(KdData, SampleNoi, 3), "0.00")
                        'Velocity-CD Table Data
                        DataGridView1.Rows(SampleNoi - 1).Cells(6).Value = Strings.Format(DataPrcNum(KdData, SampleNoi, 11), "0.00")
                        'Velocity-Peak Table Data
                        DataGridView1.Rows(SampleNoi - 1).Cells(7).Value = Strings.Format(DataPrcNum(KdData, SampleNoi, 2), "0.00")
                        'Velocity-Deep Table Data
                        DataGridView1.Rows(SampleNoi - 1).Cells(8).Value = Strings.Format(DataPrcNum(KdData, SampleNoi, 1), "0.00")
                    Next
                End If
            ElseIf KdData = 3 Then
                'Velocity-Peak
                LblVeloPkMaxBak_adm.Text = Strings.Format(DataMaxP, "0.00")
                LblVeloPkAvgBak_adm.Text = Strings.Format(DataAvgP, "0.00")
                LblVeloPkMinBak_adm.Text = Strings.Format(DataMinP, "0.00")

                'Velocity-Peak Table View
                LblVeloPkMaxOld_TB.Text = Strings.Format(DataMaxP, "0.00")
                LblVeloPkAvgOld_TB.Text = Strings.Format(DataAvgP, "0.00")
                LblVeloPkMinOld_TB.Text = Strings.Format(DataMinP, "0.00")

                'Velocity-Deep
                LblVeloDpMaxBak_adm.Text = Strings.Format(DataMaxD, "0.00")
                LblVeloDpMinBak_adm.Text = Strings.Format(DataMinD, "0.00")
                LblVeloDpAvgBak_adm.Text = Strings.Format(DataAvgD, "0.00")

                'Velocity-Deep Table View
                LblVeloDpMaxOld_TB.Text = Strings.Format(DataMaxD, "0.00")
                LblVeloDpAvgOld_TB.Text = Strings.Format(DataAvgD, "0.00")
                LblVeloDpMinOld_TB.Text = Strings.Format(DataMinD, "0.00")

                'Velocity-MD
                LblVeloMDMaxBak_adm.Text = Strings.Format(DataMaxM, "0.00")
                LblVeloMDMinBak_adm.Text = Strings.Format(DataMinM, "0.00")
                LblVeloMDAvgBak_adm.Text = Strings.Format(DataAvgM, "0.00")

                'Velocity-MD Table View
                LblVeloMDMaxOld_TB.Text = Strings.Format(DataMaxM, "0.00")
                LblVeloMDAvgOld_TB.Text = Strings.Format(DataAvgM, "0.00")
                LblVeloMDMinOld_TB.Text = Strings.Format(DataMinM, "0.00")

                'Velocity-CD
                LblVeloCDMaxBak_adm.Text = Strings.Format(DataMaxC, "0.00")
                LblVeloCDMinBak_adm.Text = Strings.Format(DataMinC, "0.00")
                LblVeloCDAvgBak_adm.Text = Strings.Format(DataAvgC, "0.00")

                'Velocity-CD Table View
                LblVeloCDMaxOld_TB.Text = Strings.Format(DataMaxC, "0.00")
                LblVeloCDAvgOld_TB.Text = Strings.Format(DataAvgC, "0.00")
                LblVeloCDMinOld_TB.Text = Strings.Format(DataMinC, "0.00")

                For SampleNoi = 1 To SampleNo
                    'Velocity-MD Table Data
                    DataGridView2.Rows(SampleNoi - 1).Cells(5).Value = Strings.Format(DataPrcNum(KdData, SampleNoi, 3), "0.00")
                    'Velocity-CD Table Data
                    DataGridView2.Rows(SampleNoi - 1).Cells(6).Value = Strings.Format(DataPrcNum(KdData, SampleNoi, 11), "0.00")
                    'Velocity-Peak Table Data
                    DataGridView2.Rows(SampleNoi - 1).Cells(7).Value = Strings.Format(DataPrcNum(KdData, SampleNoi, 2), "0.00")
                    'Velocity-Deep Table Data
                    DataGridView2.Rows(SampleNoi - 1).Cells(8).Value = Strings.Format(DataPrcNum(KdData, SampleNoi, 1), "0.00")
                Next
            ElseIf KdData = 0 Then
                'Velocity-Peak
                LblVeloPkMaxAvg_adm.Text = Strings.Format(DataMaxP, "0.00")
                LblVeloPkAvgAvg_adm.Text = Strings.Format(DataAvgP, "0.00")
                LblVeloPkMinAvg_adm.Text = Strings.Format(DataMinP, "0.00")

                'Velocity-Peak Table View
                LblVeloPkMaxAvg_TB.Text = Strings.Format(DataMaxP, "0.00")
                LblVeloPkAvgAvg_TB.Text = Strings.Format(DataAvgP, "0.00")
                LblVeloPkMinAvg_TB.Text = Strings.Format(DataMinP, "0.00")

                'Velocity-Deep
                LblVeloDpMaxAvg_adm.Text = Strings.Format(DataMaxD, "0.00")
                LblVeloDpMinAvg_adm.Text = Strings.Format(DataMinD, "0.00")
                LblVeloDpAvgAvg_adm.Text = Strings.Format(DataAvgD, "0.00")

                'Velocity-Deep Table View
                LblVeloDpMaxAvg_TB.Text = Strings.Format(DataMaxD, "0.00")
                LblVeloDpAvgAvg_TB.Text = Strings.Format(DataAvgD, "0.00")
                LblVeloDpMinAvg_TB.Text = Strings.Format(DataMinD, "0.00")

                'Velocity-MD
                LblVeloMDMaxAvg_adm.Text = Strings.Format(DataMaxM, "0.00")
                LblVeloMDMinAvg_adm.Text = Strings.Format(DataMinM, "0.00")
                LblVeloMDAvgAvg_adm.Text = Strings.Format(DataAvgM, "0.00")

                'Velocity-MD Table View
                LblVeloMDMaxAvg_TB.Text = Strings.Format(DataMaxM, "0.00")
                LblVeloMDAvgAvg_TB.Text = Strings.Format(DataAvgM, "0.00")
                LblVeloMDMinAvg_TB.Text = Strings.Format(DataMinM, "0.00")

                'Velocity-CD
                LblVeloCDMaxAvg_adm.Text = Strings.Format(DataMaxC, "0.00")
                LblVeloCDMinAvg_adm.Text = Strings.Format(DataMinC, "0.00")
                LblVeloCDAvgAvg_adm.Text = Strings.Format(DataAvgC, "0.00")

                'Velocity-CD Table View
                LblVeloCDMaxAvg_TB.Text = Strings.Format(DataMaxC, "0.00")
                LblVeloCDAvgAvg_TB.Text = Strings.Format(DataAvgC, "0.00")
                LblVeloCDMinAvg_TB.Text = Strings.Format(DataMinC, "0.00")

                For SampleNoi = 1 To SampleNo
                    'Velocity-MD Table Data
                    DataGridView3.Rows(SampleNoi - 1).Cells(5).Value = Strings.Format(DataPrcNum(KdData, SampleNoi, 3), "0.00")
                    'Velocity-CD Table Data
                    DataGridView3.Rows(SampleNoi - 1).Cells(6).Value = Strings.Format(DataPrcNum(KdData, SampleNoi, 11), "0.00")
                    'Velocity-Peak Table Data
                    DataGridView3.Rows(SampleNoi - 1).Cells(7).Value = Strings.Format(DataPrcNum(KdData, SampleNoi, 2), "0.00")
                    'Velocity-Deep Table Data
                    DataGridView3.Rows(SampleNoi - 1).Cells(8).Value = Strings.Format(DataPrcNum(KdData, SampleNoi, 1), "0.00")
                Next
            End If
        End If

    End Sub

    Private Sub PrfSaidDataAngle(ByVal sel As Integer)
        Dim DataMaxP As Single
        Dim DataMinP As Single
        Dim DataAvgP As Single
        Dim DataMaxD As Single
        Dim DataMinD As Single
        Dim DataAvgD As Single
        Dim DataK As Single
        Dim Ds As String
        Dim Ds_1 As String
        Dim TbRowsCount As Integer
        Dim SampleNoi As Integer

        If SampleNo < 1 Then
            If KdData = 1 Then
                LblAnglePkMaxCur_adm.Text = "0.0"
                LblAnglePkAvgCur_adm.Text = "0.0"
                LblAnglePkMinCur_adm.Text = "0.0"
                LblAnglePkMax_nom.Text = "0.0"
                LblAnglePkAvg_nom.Text = "0.0"
                LblAnglePkMin_nom.Text = "0.0"
                LblAnglePkMax_TB.Text = "0.0"
                LblAnglePkAvg_TB.Text = "0.0"
                LblAnglePkMin_TB.Text = "0.0"
                LblAngleDpMaxCur_adm.Text = "0.0"
                LblAngleDpAvgCur_adm.Text = "0.0"
                LblAngleDpMinCur_adm.Text = "0.0"
                LblAngleDpMax_nom.Text = "0.0"
                LblAngleDpAvg_nom.Text = "0.0"
                LblAngleDpMin_nom.Text = "0.0"
                LblAngleDpMax_TB.Text = "0.0"
                LblAngleDpAvg_TB.Text = "0.0"
                LblAngleDpMin_TB.Text = "0.0"
            ElseIf KdData = 3 Then
                LblAnglePkMaxBak_adm.Text = "0.0"
                LblAnglePkAvgBak_adm.Text = "0.0"
                LblAnglePkMinBak_adm.Text = "0.0"
                LblAnglePkMaxOld_TB.Text = "0.0"
                LblAnglePkAvgOld_TB.Text = "0.0"
                LblAnglePkMinOld_TB.Text = "0.0"
                LblAngleDpMaxBak_adm.Text = "0.0"
                LblAngleDpAvgBak_adm.Text = "0.0"
                LblAngleDpMinBak_adm.Text = "0.0"
                LblAngleDpMaxOld_TB.Text = "0.0"
                LblAngleDpAvgOld_TB.Text = "0.0"
                LblAngleDpMinOld_TB.Text = "0.0"
            ElseIf KdData = 0 Then
                LblAnglePkMaxAvg_adm.Text = "0.0"
                LblAnglePkAvgAvg_adm.Text = "0.0"
                LblAnglePkMinAvg_adm.Text = "0.0"
                LblAnglePkMaxAvg_TB.Text = "0.0"
                LblAnglePkAvgAvg_TB.Text = "0.0"
                LblAnglePkMinAvg_TB.Text = "0.0"
                LblAngleDpMaxAvg_adm.Text = "0.0"
                LblAngleDpAvgAvg_adm.Text = "0.0"
                LblAngleDpMinAvg_adm.Text = "0.0"
                LblAngleDpMaxAvg_TB.Text = "0.0"
                LblAngleDpAvgAvg_TB.Text = "0.0"
                LblAngleDpMinAvg_TB.Text = "0.0"
            End If
        Else
            'Angle-Peak
            DataMaxP = Math.Round(DataMax1Angle(KdData), 1)
            DataMinP = Math.Round(DataMin1Angle(KdData), 1)
            DataAvgP = Math.Round(DataInt1Angle(KdData) / SampleNo, 1)

            'Angle-Deep
            DataMaxD = Math.Round(DataMax2Angle(KdData), 1)
            DataMinD = Math.Round(DataMin2Angle(KdData), 1)
            DataAvgD = Math.Round(DataInt2Angle(KdData) / SampleNo, 1)

            If KdData = 1 Then
                'Angle-Peak
                LblAnglePkMaxCur_adm.Text = Format(DataMaxP, "+0.0;-0.0;0.0;")
                LblAnglePkAvgCur_adm.Text = Format(DataAvgP, "+0.0;-0.0;0.0;")
                LblAnglePkMinCur_adm.Text = Format(DataMinP, "+0.0;-0.0;0.0;")
                LblAnglePkMax_nom.Text = Format(DataMaxP, "+0.0;-0.0;0.0;")
                LblAnglePkAvg_nom.Text = Format(DataAvgP, "+0.0;-0.0;0.0;")
                LblAnglePkMin_nom.Text = Format(DataMinP, "+0.0;-0.0;0.0;")

                'Angle-Peak Table View
                LblAnglePkMax_TB.Text = Format(DataMaxP, "+0.0;-0.0;0.0;")
                LblAnglePkAvg_TB.Text = Format(DataAvgP, "+0.0;-0.0;0.0;")
                LblAnglePkMin_TB.Text = Format(DataMinP, "+0.0;-0.0;0.0;")

                'Angle-Deep
                LblAngleDpMaxCur_adm.Text = Format(DataMaxD, "+0.0;-0.0;0.0;")
                LblAngleDpAvgCur_adm.Text = Format(DataAvgD, "+0.0;-0.0;0.0;")
                LblAngleDpMinCur_adm.Text = Format(DataMinD, "+0.0;-0.0;0.0;")
                LblAngleDpMax_nom.Text = Format(DataMaxD, "+0.0;-0.0;0.0;")
                LblAngleDpAvg_nom.Text = Format(DataAvgD, "+0.0;-0.0;0.0;")
                LblAngleDpMin_nom.Text = Format(DataMinD, "+0.0;-0.0;0.0;")

                'Angle-Deep Table View
                LblAngleDpMax_TB.Text = Format(DataMaxD, "+0.0;-0.0;0.0;")
                LblAngleDpAvg_TB.Text = Format(DataAvgD, "+0.0;-0.0;0.0;")
                LblAngleDpMin_TB.Text = Format(DataMinD, "+0.0;-0.0;0.0;")

                If sel = 0 Then
                    'Angle-Peak Table Data
                    TbRowsCount = DataGridView1.Rows.Count
                    If SampleNo > TbRowsCount Then
                        DataGridView1.Rows.Add()
                        TbRowsCount += 1
                        'DataGridView1.FirstDisplayedScrollingRowIndex = TbRowsCount - 1
                    End If

                    DataGridView1.Rows(SampleNo - 1).Cells(0).Value = SampleNo

                    'Angle-Peak Table Data
                    'If FlgDBF = 0 Then
                    Ds = DataPrcStr(KdData, SampleNo, 9)
                    Ds_1 = Strings.Left(Ds, 1)
                    If Ds_1 = "C" Or Ds_1 = "M" Then
                        DataK = Math.Round(Val(Strings.Right(Ds, Len(Ds) - 2)), 1)
                    Else
                        DataK = Math.Round(Val(Ds), 1)
                    End If
                    DataGridView1.Rows(SampleNo - 1).Cells(1).Value = Format(DataK, "+0.0;-0.0;0.0")

                    'If FlgDBF = 0 Then
                    'Angle-Deep Table Data
                    Ds = DataPrcStr(KdData, SampleNo, 8)
                    Ds_1 = Strings.Left(Ds, 1)
                    If Ds_1 = "C" Or Ds_1 = "M" Then
                        DataK = Math.Round(Val(Strings.Right(Ds, Len(Ds) - 2)), 1)
                    Else
                        DataK = Math.Round(Val(Ds), 1)
                    End If
                    DataGridView1.Rows(SampleNo - 1).Cells(2).Value = Format(DataK, "+0.0;-0.0;0.0")

                ElseIf sel = 1 Then
                    For SampleNoi = 1 To SampleNo
                        TbRowsCount = DataGridView1.Rows.Count
                        If SampleNo > TbRowsCount Then
                            DataGridView1.Rows.Add()
                            TbRowsCount += 1
                        End If

                        DataGridView1.Rows(SampleNoi - 1).Cells(0).Value = SampleNoi

                        'Angle-Peak Table Data
                        'If FlgDBF = 0 Then
                        Ds = DataPrcStr(KdData, SampleNoi, 9)
                        Ds_1 = Strings.Left(Ds, 1)
                        If Ds_1 = "C" Or Ds_1 = "M" Then
                            DataK = Math.Round(Val(Strings.Right(Ds, Len(Ds) - 2)), 1)
                        Else
                            DataK = Math.Round(Val(Ds), 1)
                        End If
                        DataGridView1.Rows(SampleNoi - 1).Cells(1).Value = Format(DataK, "+0.0;-0.0;0.0")

                        'Angle-Deep Table Data
                        'If FlgDBF = 0 Then
                        Ds = DataPrcStr(KdData, SampleNoi, 8)
                        Ds_1 = Strings.Left(Ds, 1)
                        If Ds_1 = "C" Or Ds_1 = "M" Then
                            DataK = Math.Round(Val(Strings.Right(Ds, Len(Ds) - 2)), 1)
                        Else
                            DataK = Math.Round(Val(Ds), 1)
                        End If
                        DataGridView1.Rows(SampleNoi - 1).Cells(2).Value = Format(DataK, "+0.0;-0.0;0.0")
                    Next
                End If

            ElseIf KdData = 3 Then
                'Angle-Peak
                LblAnglePkMaxBak_adm.Text = Format(DataMaxP, "+0.0;-0.0;0.0;")
                LblAnglePkAvgBak_adm.Text = Format(DataAvgP, "+0.0;-0.0;0.0;")
                LblAnglePkMinBak_adm.Text = Format(DataMinP, "+0.0;-0.0;0.0;")

                'Angle-Peak Table View
                LblAnglePkMaxOld_TB.Text = Format(DataMaxP, "+0.0;-0.0;0.0;")
                LblAnglePkAvgOld_TB.Text = Format(DataAvgP, "+0.0;-0.0;0.0;")
                LblAnglePkMinOld_TB.Text = Format(DataMinP, "+0.0;-0.0;0.0;")

                'Angle-Deep
                LblAngleDpMaxBak_adm.Text = Format(DataMaxD, "+0.0;-0.0;0.0;")
                LblAngleDpAvgBak_adm.Text = Format(DataAvgD, "+0.0;-0.0;0.0;")
                LblAngleDpMinBak_adm.Text = Format(DataMinD, "+0.0;-0.0;0.0;")

                'Angle-Deep Table View
                LblAngleDpMaxOld_TB.Text = Format(DataMaxD, "+0.0;-0.0;0.0;")
                LblAngleDpAvgOld_TB.Text = Format(DataAvgD, "+0.0;-0.0;0.0;")
                LblAngleDpMinOld_TB.Text = Format(DataMinD, "+0.0;-0.0;0.0;")

                'Angle-Peak Table Data
                For SampleNoi = 1 To SampleNo
                    TbRowsCount = DataGridView2.Rows.Count
                    If SampleNoi > TbRowsCount Then
                        DataGridView2.Rows.Add()
                        TbRowsCount += 1
                        'DataGridView2.FirstDisplayedScrollingRowIndex = TbRowsCount - 1
                    End If

                    DataGridView2.Rows(SampleNoi - 1).Cells(0).Value = SampleNoi

                    'If FlgDBF = 0 Then
                    Ds = DataPrcStr(KdData, SampleNoi, 9)
                    Ds_1 = Strings.Left(Ds, 1)
                    If Ds_1 = "C" Or Ds_1 = "M" Then
                        DataK = Math.Round(Val(Strings.Right(Ds, Len(Ds) - 2)), 1)
                    Else
                        DataK = Math.Round(Val(Ds), 1)
                    End If
                    DataGridView2.Rows(SampleNoi - 1).Cells(1).Value = Format(DataK, "+0.0;-0.0;0.0")

                    'Angle-Deep Table Data
                    'If FlgDBF = 0 Then
                    Ds = DataPrcStr(KdData, SampleNoi, 8)
                    Ds_1 = Strings.Left(Ds, 1)
                    If Ds_1 = "C" Or Ds_1 = "M" Then
                        DataK = Math.Round(Val(Strings.Right(Ds, Len(Ds) - 2)), 1)
                    Else
                        DataK = Math.Round(Val(Ds), 1)
                    End If
                    DataGridView2.Rows(SampleNoi - 1).Cells(2).Value = Format(DataK, "+0.0;-0.0;0.0")
                Next
            ElseIf KdData = 0 Then
                'Angle-Peak
                LblAnglePkMaxAvg_adm.Text = Format(DataMaxP, "+0.0;-0.0;0.0;")
                LblAnglePkAvgAvg_adm.Text = Format(DataAvgP, "+0.0;-0.0;0.0;")
                LblAnglePkMinAvg_adm.Text = Format(DataMinP, "+0.0;-0.0;0.0;")

                'Angle-Peak Table View
                LblAnglePkMaxAvg_TB.Text = Format(DataMaxP, "+0.0;-0.0;0.0;")
                LblAnglePkAvgAvg_TB.Text = Format(DataAvgP, "+0.0;-0.0;0.0;")
                LblAnglePkMinAvg_TB.Text = Format(DataMinP, "+0.0;-0.0;0.0;")

                'Angle-Deep
                LblAngleDpMaxAvg_adm.Text = Format(DataMaxD, "+0.0;-0.0;0.0;")
                LblAngleDpAvgAvg_adm.Text = Format(DataAvgD, "+0.0;-0.0;0.0;")
                LblAngleDpMinAvg_adm.Text = Format(DataMinD, "+0.0;-0.0;0.0;")

                'Angle-Deep Table View
                LblAngleDpMaxAvg_TB.Text = Format(DataMaxD, "+0.0;-0.0;0.0;")
                LblAngleDpAvgAvg_TB.Text = Format(DataAvgD, "+0.0;-0.0;0.0;")
                LblAngleDpMinAvg_TB.Text = Format(DataMinD, "+0.0;-0.0;0.0;")

                'Angle-Peak Table Data
                For SampleNoi = 1 To SampleNo
                    TbRowsCount = DataGridView3.Rows.Count
                    If SampleNoi > TbRowsCount Then
                        DataGridView3.Rows.Add()
                        TbRowsCount += 1
                        'DataGridView2.FirstDisplayedScrollingRowIndex = TbRowsCount - 1
                    End If

                    DataGridView3.Rows(SampleNoi - 1).Cells(0).Value = SampleNoi

                    'Angle-Peak Table Data
                    'If FlgDBF = 0 Then
                    Ds = DataPrcStr(KdData, SampleNoi, 9)
                    Ds_1 = Strings.Left(Ds, 1)
                    If Ds_1 = "C" Or Ds_1 = "M" Then
                        DataK = Math.Round(Val(Strings.Right(Ds, Len(Ds) - 2)), 1)
                    Else
                        DataK = Math.Round(Val(Ds), 1)
                    End If
                    DataGridView3.Rows(SampleNoi - 1).Cells(1).Value = Format(DataK, "+0.0;-0.0;0.0")

                    'Angle-Deep Table Data
                    'If FlgDBF = 0 Then
                    Ds = DataPrcStr(KdData, SampleNoi, 8)
                    Ds_1 = Strings.Left(Ds, 1)
                    If Ds_1 = "C" Or Ds_1 = "M" Then
                        DataK = Math.Round(Val(Strings.Right(Ds, Len(Ds) - 2)), 1)
                    Else
                        DataK = Math.Round(Val(Ds), 1)
                    End If
                    DataGridView3.Rows(SampleNoi - 1).Cells(2).Value = Format(DataK, "+0.0;-0.0;0.0")
                Next

            End If
        End If
    End Sub

    Private Sub PrfSaidDataRatio(ByVal sel As Integer)
        Dim DataMaxPD As Single
        Dim DataMinPD As Single
        Dim DataAvgPD As Single
        Dim DataMaxMC As Single
        Dim DataMinMC As Single
        Dim DataAvgMC As Single
        Dim DataK As Single
        Dim SampleNoi As Integer

        If SampleNo < 1 Then
            If KdData = 1 Then
                LblRatioPkDpMaxCur_adm.Text = "0.00"
                LblRatioPkDpAvgCur_adm.Text = "0.00"
                LblRatioPkDpMinCur_adm.Text = "0.00"
                LblRatioPkDpMax_nom.Text = "0.00"
                LblRatioPkDpAvg_nom.Text = "0.00"
                LblRatioPkDpMin_nom.Text = "0.00"
                LblRatioPkDpMax_TB.Text = "0.00"
                LblRatioPkDpAvg_TB.Text = "0.00"
                LblRatioPkDpMin_TB.Text = "0.00"
                LblRatioMDCDMaxCur_adm.Text = "0.00"
                LblRatioMDCDAvgCur_adm.Text = "0.00"
                LblRatioMDCDMinCur_adm.Text = "0.00"
                LblRatioMDCDMax_nom.Text = "0.00"
                LblRatioMDCDAvg_nom.Text = "0.00"
                LblRatioMDCDMin_nom.Text = "0.00"
                LblRatioMDCDMax_TB.Text = "0.00"
                LblRatioMDCDAvg_TB.Text = "0.00"
                LblRatioMDCDMin_TB.Text = "0.00"
            ElseIf KdData = 3 Then
                LblRatioPkDpMaxBak_adm.Text = "0.00"
                LblRatioPkDpAvgBak_adm.Text = "0.00"
                LblRatioPkDpMinBak_adm.Text = "0.00"
                LblRatioPkDpMaxOld_TB.Text = "0.00"
                LblRatioPkDpAvgOld_TB.Text = "0.00"
                LblRatioPkDpMinOld_TB.Text = "0.00"
                LblRatioMDCDMaxBak_adm.Text = "0.00"
                LblRatioMDCDAvgBak_adm.Text = "0.00"
                LblRatioMDCDMinBak_adm.Text = "0.00"
                LblRatioMDCDMaxOld_TB.Text = "0.00"
                LblRatioMDCDAvgOld_TB.Text = "0.00"
                LblRatioMDCDMinOld_TB.Text = "0.00"
            ElseIf KdData = 0 Then
                LblRatioPkDpMaxAvg_adm.Text = "0.00"
                LblRatioPkDpAvgAvg_adm.Text = "0.00"
                LblRatioPkDpMinBak_adm.Text = "0.00"
                LblRatioPkDpMaxAvg_TB.Text = "0.00"
                LblRatioPkDpAvgAvg_TB.Text = "0.00"
                LblRatioPkDpMinAvg_TB.Text = "0.00"
                LblRatioMDCDMaxAvg_adm.Text = "0.00"
                LblRatioMDCDAvgAvg_adm.Text = "0.00"
                LblRatioMDCDMinAvg_adm.Text = "0.00"
                LblRatioMDCDMaxAvg_TB.Text = "0.00"
                LblRatioMDCDAvgAvg_TB.Text = "0.00"
                LblRatioMDCDMinAvg_TB.Text = "0.00"
            End If
        Else
            'Ratio Peak/Deep
            DataMaxPD = DataMax1RatioP(KdData)
            DataMinPD = DataMin1RatioP(KdData)
            DataAvgPD = DataInt1RatioP(KdData) / SampleNo

            'Ratio MD/CD
            DataMaxMC = DataMax1RatioM(KdData)
            DataMinMC = DataMin1RatioM(KdData)
            DataAvgMC = DataInt1RatioM(KdData) / SampleNo

            If KdData = 1 Then
                'Ratio Peak/Deep
                LblRatioPkDpMaxCur_adm.Text = Format(DataMaxPD, "0.00")
                LblRatioPkDpAvgCur_adm.Text = Format(DataAvgPD, "0.00")
                LblRatioPkDpMinCur_adm.Text = Format(DataMinPD, "0.00")
                LblRatioPkDpMax_nom.Text = Format(DataMaxPD, "0.00")
                LblRatioPkDpAvg_nom.Text = Format(DataAvgPD, "0.00")
                LblRatioPkDpMin_nom.Text = Format(DataMinPD, "0.00")

                'Ratio Peak/Deep Table View
                LblRatioPkDpMax_TB.Text = Format(DataMaxPD, "0.00")
                LblRatioPkDpAvg_TB.Text = Format(DataAvgPD, "0.00")
                LblRatioPkDpMin_TB.Text = Format(DataMinPD, "0.00")

                'Ratio MD/CD
                LblRatioMDCDMaxCur_adm.Text = Format(DataMaxMC, "0.00")
                LblRatioMDCDAvgCur_adm.Text = Format(DataAvgMC, "0.00")
                LblRatioMDCDMinCur_adm.Text = Format(DataMinMC, "0.00")
                LblRatioMDCDMax_nom.Text = Format(DataMaxMC, "0.00")
                LblRatioMDCDAvg_nom.Text = Format(DataAvgMC, "0.00")
                LblRatioMDCDMin_nom.Text = Format(DataMinMC, "0.00")

                'Ratio MD/CD Table View
                LblRatioMDCDMax_TB.Text = Format(DataMaxMC, "0.00")
                LblRatioMDCDAvg_TB.Text = Format(DataAvgMC, "0.00")
                LblRatioMDCDMin_TB.Text = Format(DataMinMC, "0.00")

                If sel = 0 Then
                    'Ratio Peak/Deep Table Data
                    DataK = Val(DataPrcStr(KdData, SampleNo, 11))
                    DataGridView1.Rows(SampleNo - 1).Cells(4).Value = Format(DataK, "0.00")

                    'Ratio MD/CD Table Data
                    DataK = Val(DataPrcStr(KdData, SampleNo, 10))
                    DataGridView1.Rows(SampleNo - 1).Cells(3).Value = Format(DataK, "0.00")
                ElseIf sel = 1 Then
                    For SampleNoi = 1 To SampleNo
                        'Ratio Peak/Deep Table Data
                        DataK = Val(DataPrcStr(KdData, SampleNoi, 11))
                        DataGridView1.Rows(SampleNoi - 1).Cells(4).Value = Format(DataK, "0.00")

                        'Ratio MD/CD Table Data
                        DataK = Val(DataPrcStr(KdData, SampleNoi, 10))
                        DataGridView1.Rows(SampleNoi - 1).Cells(3).Value = Format(DataK, "0.00")
                    Next
                End If

            ElseIf KdData = 3 Then
                'Ratio Peak/Deep
                LblRatioPkDpMaxBak_adm.Text = Format(DataMaxPD, "0.00")
                LblRatioPkDpAvgBak_adm.Text = Format(DataAvgPD, "0.00")
                LblRatioPkDpMinBak_adm.Text = Format(DataMinPD, "0.00")

                'Ratio Peak/Deep Table View
                LblRatioPkDpMaxOld_TB.Text = Format(DataMaxPD, "0.00")
                LblRatioPkDpAvgOld_TB.Text = Format(DataAvgPD, "0.00")
                LblRatioPkDpMinOld_TB.Text = Format(DataMinPD, "0.00")

                'Ratio MD/CD
                LblRatioMDCDMaxBak_adm.Text = Format(DataMaxMC, "0.00")
                LblRatioMDCDAvgBak_adm.Text = Format(DataAvgMC, "0.00")
                LblRatioMDCDMinBak_adm.Text = Format(DataMinMC, "0.00")

                'Ratio MD/CD Table View
                LblRatioMDCDMaxOld_TB.Text = Format(DataMaxMC, "0.00")
                LblRatioMDCDAvgOld_TB.Text = Format(DataAvgMC, "0.00")
                LblRatioMDCDMinOld_TB.Text = Format(DataMinMC, "0.00")

                For SampleNoi = 1 To SampleNo
                    'Ratio Peak/Deep Table Data
                    DataK = Val(DataPrcStr(KdData, SampleNoi, 11))
                    DataGridView2.Rows(SampleNoi - 1).Cells(4).Value = Format(DataK, "0.00")

                    'Ratio MD/CD Table Data
                    DataK = Val(DataPrcStr(KdData, SampleNoi, 10))
                    DataGridView2.Rows(SampleNoi - 1).Cells(3).Value = Format(DataK, "0.00")
                Next
            ElseIf KdData = 0 Then
                'Ratio Peak/Deep
                LblRatioPkDpMaxAvg_adm.Text = Format(DataMaxPD, "0.00")
                LblRatioPkDpAvgAvg_adm.Text = Format(DataAvgPD, "0.00")
                LblRatioPkDpMinAvg_adm.Text = Format(DataMinPD, "0.00")

                'Ratio Peak/Deep Table View
                LblRatioPkDpMaxAvg_TB.Text = Format(DataMaxPD, "0.00")
                LblRatioPkDpAvgAvg_TB.Text = Format(DataAvgPD, "0.00")
                LblRatioPkDpMinAvg_TB.Text = Format(DataMinPD, "0.00")

                'Ratio MD/CD
                LblRatioMDCDMaxAvg_adm.Text = Format(DataMaxMC, "0.00")
                LblRatioMDCDAvgAvg_adm.Text = Format(DataAvgMC, "0.00")
                LblRatioMDCDMinAvg_adm.Text = Format(DataMinMC, "0.00")

                'Ratio MD/CD Table View
                LblRatioMDCDMaxAvg_TB.Text = Format(DataMaxMC, "0.00")
                LblRatioMDCDAvgAvg_TB.Text = Format(DataAvgMC, "0.00")
                LblRatioMDCDMinAvg_TB.Text = Format(DataMinMC, "0.00")

                For SampleNoi = 1 To SampleNo
                    'Ratio Peak/Deep Table Data
                    DataK = Val(DataPrcStr(KdData, SampleNoi, 11))
                    DataGridView3.Rows(SampleNoi - 1).Cells(4).Value = Format(DataK, "0.00")

                    'Ratio MD/CD Table Data
                    DataK = Val(DataPrcStr(KdData, SampleNoi, 10))
                    DataGridView3.Rows(SampleNoi - 1).Cells(3).Value = Format(DataK, "0.00")
                Next

            End If
        End If
    End Sub

    Private Sub PrfSaidDataTSI(ByVal sel As Integer)
        Dim DataMaxM As Single
        Dim DataMinM As Single
        Dim DataAvgM As Single
        Dim DataMaxC As Single
        Dim DataMinC As Single
        Dim DataAvgC As Single
        Dim SampleNoi As Integer

        If SampleNo < 1 Then
            If KdData = 1 Then
                LblTSIMDMaxCur_adm.Text = "0.00"
                LblTSIMDAvgCur_adm.Text = "0.00"
                LblTSIMDMinCur_adm.Text = "0.00"
                LblTSIMDMax_nom.Text = "0.00"
                LblTSIMDAvg_nom.Text = "0.00"
                LblTSIMDMin_nom.Text = "0.00"
                LblTSIMDMax_TB.Text = "0.00"
                LblTSIMDAvg_TB.Text = "0.00"
                LblTSIMDMin_TB.Text = "0.00"
                LblTSICDMaxCur_adm.Text = "0.00"
                LblTSICDAvgCur_adm.Text = "0.00"
                LblTSICDMinCur_adm.Text = "0.00"
                LblTSICDMax_nom.Text = "0.00"
                LblTSICDAvg_nom.Text = "0.00"
                LblTSICDMin_nom.Text = "0.00"
                LblTSICDMax_TB.Text = "0.00"
                LblTSICDAvg_TB.Text = "0.00"
                LblTSICDMin_TB.Text = "0.00"
            ElseIf KdData = 3 Then
                LblTSIMDMaxBak_adm.Text = "0.00"
                LblTSIMDAvgBak_adm.Text = "0.00"
                LblTSIMDMinBak_adm.Text = "0.00"
                LblTSIMDMaxOld_TB.Text = "0.00"
                LblTSIMDAvgOld_TB.Text = "0.00"
                LblTSIMDMinOld_TB.Text = "0.00"
                LblTSICDMaxBak_adm.Text = "0.00"
                LblTSICDAvgBak_adm.Text = "0.00"
                LblTSICDMinBak_adm.Text = "0.00"
                LblTSICDMaxOld_TB.Text = "0.00"
                LblTSICDAvgOld_TB.Text = "0.00"
                LblTSICDMinOld_TB.Text = "0.00"
            ElseIf KdData = 0 Then
                LblTSIMDMaxAvg_adm.Text = "0.00"
                LblTSIMDAvgAvg_adm.Text = "0.00"
                LblTSIMDMinBak_adm.Text = "0.00"
                LblTSIMDMaxAvg_TB.Text = "0.00"
                LblTSIMDAvgAvg_TB.Text = "0.00"
                LblTSIMDMinAvg_TB.Text = "0.00"
                LblTSICDMaxAvg_adm.Text = "0.00"
                LblTSICDAvgAvg_adm.Text = "0.00"
                LblTSICDMinAvg_adm.Text = "0.00"
                LblTSICDMaxAvg_TB.Text = "0.00"
                LblTSICDAvgAvg_TB.Text = "0.00"
                LblTSICDMinAvg_TB.Text = "0.00"
            End If
        Else
            'TSI-MD
            DataMaxM = DataMax1TSI(KdData) ^ 2
            DataMinM = DataMin1TSI(KdData) ^ 2
            DataAvgM = DataInt1TSI(KdData) / SampleNo

            'TSI-CD
            DataMaxC = DataMax2TSI(KdData) ^ 2
            DataMinC = DataMin2TSI(KdData) ^ 2
            DataAvgC = DataInt2TSI(KdData) / SampleNo

            If KdData = 1 Then
                'TSI-MD
                LblTSIMDMaxCur_adm.Text = Format(DataMaxM, "0.00")
                LblTSIMDAvgCur_adm.Text = Format(DataAvgM, "0.00")
                LblTSIMDMinCur_adm.Text = Format(DataMinM, "0.00")
                LblTSIMDMax_nom.Text = Format(DataMaxM, "0.00")
                LblTSIMDAvg_nom.Text = Format(DataAvgM, "0.00")
                LblTSIMDMin_nom.Text = Format(DataMinM, "0.00")

                'TSI-MD Table View
                LblTSIMDMax_TB.Text = Format(DataMaxM, "0.00")
                LblTSIMDAvg_TB.Text = Format(DataAvgM, "0.00")
                LblTSIMDMin_TB.Text = Format(DataMinM, "0.00")

                'TSI-CD
                LblTSICDMaxCur_adm.Text = Format(DataMaxC, "0.00")
                LblTSICDAvgCur_adm.Text = Format(DataAvgC, "0.00")
                LblTSICDMinCur_adm.Text = Format(DataMinC, "0.00")
                LblTSICDMax_nom.Text = Format(DataMaxC, "0.00")
                LblTSICDAvg_nom.Text = Format(DataAvgC, "0.00")
                LblTSICDMin_nom.Text = Format(DataMinC, "0.00")

                'TSI-CD Table View
                LblTSICDMax_TB.Text = Format(DataMaxC, "0.00")
                LblTSICDAvg_TB.Text = Format(DataAvgC, "0.00")
                LblTSICDMin_TB.Text = Format(DataMinC, "0.00")

                If sel = 0 Then
                    'TSI-CD Table Data
                    DataGridView1.Rows(SampleNo - 1).Cells(10).Value = Format(DataPrcNum(KdData, SampleNo, 11) ^ 2, "0.00")
                    'TSI-MD Table Data
                    DataGridView1.Rows(SampleNo - 1).Cells(9).Value = Format(DataPrcNum(KdData, SampleNo, 3) ^ 2, "0.00")
                ElseIf sel = 1 Then
                    For SampleNoi = 1 To SampleNo
                        'TSI-CD Table Data
                        DataGridView1.Rows(SampleNoi - 1).Cells(10).Value = Format(DataPrcNum(KdData, SampleNoi, 11) ^ 2, "0.00")
                        'TSI-MD Table Data
                        DataGridView1.Rows(SampleNoi - 1).Cells(9).Value = Format(DataPrcNum(KdData, SampleNoi, 3) ^ 2, "0.00")
                    Next
                End If

            ElseIf KdData = 3 Then
                'TSI-MD
                LblTSIMDMaxBak_adm.Text = Format(DataMaxM, "0.00")
                LblTSIMDAvgBak_adm.Text = Format(DataAvgM, "0.00")
                LblTSIMDMinBak_adm.Text = Format(DataMinM, "0.00")

                'TSI-MD Table View
                LblTSIMDMaxOld_TB.Text = Format(DataMaxM, "0.00")
                LblTSIMDAvgOld_TB.Text = Format(DataAvgM, "0.00")
                LblTSIMDMinOld_TB.Text = Format(DataMinM, "0.00")

                'TSI-CD
                LblTSICDMaxBak_adm.Text = Format(DataMaxC, "0.00")
                LblTSICDAvgBak_adm.Text = Format(DataAvgC, "0.00")
                LblTSICDMinBak_adm.Text = Format(DataMinC, "0.00")

                'TSI-CD Table View
                LblTSICDMaxOld_TB.Text = Format(DataMaxC, "0.00")
                LblTSICDAvgOld_TB.Text = Format(DataAvgC, "0.00")
                LblTSICDMinOld_TB.Text = Format(DataMinC, "0.00")

                For SampleNoi = 1 To SampleNo
                    'TSI-CD Table Data
                    DataGridView2.Rows(SampleNoi - 1).Cells(10).Value = Format(DataPrcNum(KdData, SampleNoi, 11) ^ 2, "0.00")
                    'TSI-MD Table Data
                    DataGridView2.Rows(SampleNoi - 1).Cells(9).Value = Format(DataPrcNum(KdData, SampleNoi, 3) ^ 2, "0.00")
                Next
            ElseIf KdData = 0 Then
                'TSI-MD
                LblTSIMDMaxAvg_adm.Text = Format(DataMaxM, "0.00")
                LblTSIMDAvgAvg_adm.Text = Format(DataAvgM, "0.00")
                LblTSIMDMinAvg_adm.Text = Format(DataMinM, "0.00")

                'TSI-MD Table View
                LblTSIMDMaxAvg_TB.Text = Format(DataMaxM, "0.00")
                LblTSIMDAvgAvg_TB.Text = Format(DataAvgM, "0.00")
                LblTSIMDMinAvg_TB.Text = Format(DataMinM, "0.00")

                'TSI-CD
                LblTSICDMaxAvg_adm.Text = Format(DataMaxC, "0.00")
                LblTSICDAvgAvg_adm.Text = Format(DataAvgC, "0.00")
                LblTSICDMinAvg_adm.Text = Format(DataMinC, "0.00")

                'TSI-CD Table View
                LblTSICDMaxAvg_TB.Text = Format(DataMaxC, "0.00")
                LblTSICDAvgAvg_TB.Text = Format(DataAvgC, "0.00")
                LblTSICDMinAvg_TB.Text = Format(DataMinC, "0.00")

                For SampleNoi = 1 To SampleNo
                    'TSI-CD Table Data
                    DataGridView3.Rows(SampleNoi - 1).Cells(10).Value = Format(DataPrcNum(KdData, SampleNoi, 11) ^ 2, "0.00")
                    'TSI-MD Table Data
                    DataGridView3.Rows(SampleNoi - 1).Cells(9).Value = Format(DataPrcNum(KdData, SampleNoi, 3) ^ 2, "0.00")
                Next
            End If
        End If
    End Sub

    Private Sub PrfGraphAngleRatio()

        Dim StepX As Single
        Dim StepY As Single
        Dim PosY1 As Single
        Dim PosY2 As Single
        Dim Ky1 As Single
        Dim Ky2 As Single
        Dim Ds As String
        Dim Ds_1 As String
        Dim Graph_angle_height As Single
        Dim Graph_ratio_height As Single
        Dim Graph_width As Single
        Dim Graph_angle_Y_center As Single
        Dim graph_ratio_Y_center As Single

        Graph_angle_height = angle_yaxis_max - angle_yaxis_min
        Graph_ratio_height = ratio_yaxis_max - ratio_yaxis_min
        Graph_width = graph_x_end - graph_x_sta
        Graph_angle_Y_center = angle_yaxis_min + (Graph_angle_height / 2)
        graph_ratio_Y_center = ratio_yaxis_min + (Graph_ratio_height / 2)

        Dim path1 As New GraphicsPath
        Dim path2 As New GraphicsPath
        Dim path3 As New GraphicsPath
        Dim path4 As New GraphicsPath

        If SampleNo < 2 Then
            Exit Sub
        End If

        If FlgProfile = 3 Then
            StepX = Graph_width / (lg_graph_max - 1)
        Else
            StepX = Graph_width / (Points - 1)
        End If

        'Angle
        Select Case FlgAngleRange
            Case 0
                StepY = Graph_angle_height / 5
            Case 1
                StepY = Graph_angle_height / 10
            Case 2
                StepY = Graph_angle_height / 20
            Case 3
                StepY = Graph_angle_height / 40
            Case 4
                StepY = Graph_angle_height / 80
            Case 5
                StepY = Graph_angle_height / 160
            Case 6
                StepY = Graph_angle_height / 320
        End Select

        '---angle peak---
        Ds = DataPrcStr(KdData, SampleNo - 1, 9)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            Ky1 = Val(Strings.Right(Ds, Len(Ds) - 2)) - PkAngCent
        Else
            Ky1 = Val(Ds) - PkAngCent
        End If
        Ds = DataPrcStr(KdData, SampleNo, 9)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            Ky2 = Val(Strings.Right(Ds, Len(Ds) - 2)) - PkAngCent
        Else
            Ky2 = Val(Ds) - PkAngCent
        End If

        PosY1 = Graph_angle_Y_center - StepY * Ky1
        If PosY1 < angle_yaxis_min Then
            PosY1 = angle_yaxis_min
        ElseIf PosY1 > angle_yaxis_max Then
            PosY1 = angle_yaxis_max
        End If
        PosY2 = Graph_angle_Y_center - StepY * Ky2
        If PosY2 < angle_yaxis_min Then
            PosY2 = angle_yaxis_min
        ElseIf PosY2 > angle_yaxis_max Then
            PosY2 = angle_yaxis_max
        End If

        path1.StartFigure()
        path1.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

        '---angle deep---
        Ds = DataPrcStr(KdData, SampleNo - 1, 8)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            Ky1 = Val(Strings.Right(Ds, Len(Ds) - 2)) - PkAngCent
        Else
            Ky1 = Val(Ds) - PkAngCent
        End If
        Ds = DataPrcStr(KdData, SampleNo, 8)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            Ky2 = Val(Strings.Right(Ds, Len(Ds) - 2)) - PkAngCent
        Else
            Ky2 = Val(Ds) - PkAngCent
        End If

        PosY1 = Graph_angle_Y_center - StepY * Ky1
        If PosY1 < angle_yaxis_min Then
            PosY1 = angle_yaxis_min
        ElseIf PosY1 > angle_yaxis_max Then
            PosY1 = angle_yaxis_max
        End If
        PosY2 = Graph_angle_Y_center - StepY * Ky2
        If PosY2 < angle_yaxis_min Then
            PosY2 = angle_yaxis_min
        ElseIf PosY2 > angle_yaxis_max Then
            PosY2 = angle_yaxis_max
        End If

        path2.StartFigure()
        path2.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

        StepY = Graph_ratio_height / 2.5

        '---ratio peak---
        Ky1 = Val(DataPrcStr(KdData, SampleNo - 1, 11))
        Ky2 = Val(DataPrcStr(KdData, SampleNo, 11))

        PosY1 = ratio_yaxis_max - StepY * Ky1
        If PosY1 < angle_yaxis_min Then
            PosY1 = angle_yaxis_min
        ElseIf PosY1 > angle_yaxis_max Then
            PosY1 = angle_yaxis_max
        End If
        PosY2 = ratio_yaxis_max - StepY * Ky2
        If PosY2 < angle_yaxis_min Then
            PosY2 = angle_yaxis_min
        ElseIf PosY2 > angle_yaxis_max Then
            PosY2 = angle_yaxis_max
        End If

        path3.StartFigure()
        path3.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

        Ky1 = Val(DataPrcStr(KdData, SampleNo - 1, 10))
        Ky2 = Val(DataPrcStr(KdData, SampleNo, 10))

        PosY1 = ratio_yaxis_max - StepY * Ky1
        If PosY1 < ratio_yaxis_min Then
            PosY1 = ratio_yaxis_min
        ElseIf PosY1 > ratio_yaxis_max Then
            PosY1 = ratio_yaxis_max
        End If
        PosY2 = ratio_yaxis_max - StepY * Ky2
        If PosY2 < ratio_yaxis_min Then
            PosY2 = ratio_yaxis_min
        ElseIf PosY2 > ratio_yaxis_max Then
            PosY2 = ratio_yaxis_max
        End If

        path4.StartFigure()
        path4.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

        PosX1(KdData) += StepX

        angle_peak_cur_path.Add(path1)
        angle_deep_cur_path.Add(path2)
        ratio_pkdp_cur_path.Add(path3)
        ratio_mdcd_cur_path.Add(path4)

        PictureBox1.Refresh()
        PictureBox2.Refresh()
    End Sub

    Private Sub PrfGraphAngleRatioOld()

        Dim StepX As Single
        Dim StepY As Single
        Dim PosY1 As Single
        Dim PosY2 As Single
        Dim Ky1 As Single
        Dim Ky2 As Single
        Dim Ds As String
        Dim Ds_1 As String
        Dim Graph_angle_height As Single
        Dim Graph_ratio_height As Single
        Dim Graph_width As Single
        Dim Graph_angle_Y_center As Single
        Dim graph_ratio_Y_center As Single

        Graph_angle_height = angle_yaxis_max - angle_yaxis_min
        Graph_ratio_height = ratio_yaxis_max - ratio_yaxis_min
        Graph_width = graph_x_end - graph_x_sta
        Graph_angle_Y_center = angle_yaxis_min + (Graph_angle_height / 2)
        graph_ratio_Y_center = ratio_yaxis_min + (Graph_ratio_height / 2)

        Dim path1 As New GraphicsPath
        Dim path2 As New GraphicsPath
        Dim path3 As New GraphicsPath
        Dim path4 As New GraphicsPath

        If SampleNo < 2 Then
            Exit Sub
        End If

        If FlgProfile = 3 Then
            StepX = Graph_width / (lg_graph_max - 1)
        Else
            StepX = Graph_width / (Points - 1)
        End If

        'Angle
        Select Case FlgAngleRange
            Case 0
                StepY = Graph_angle_height / 5
            Case 1
                StepY = Graph_angle_height / 10
            Case 2
                StepY = Graph_angle_height / 20
            Case 3
                StepY = Graph_angle_height / 40
            Case 4
                StepY = Graph_angle_height / 80
            Case 5
                StepY = Graph_angle_height / 160
            Case 6
                StepY = Graph_angle_height / 320
        End Select

        '---angle peak---
        Ds = DataPrcStr(KdData, SampleNo - 1, 9)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            Ky1 = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            Ky1 = Val(Ds)
        End If
        Ds = DataPrcStr(KdData, SampleNo, 9)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            Ky2 = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            Ky2 = Val(Ds)
        End If

        PosY1 = Graph_angle_Y_center - StepY * Ky1
        If PosY1 < angle_yaxis_min Then
            PosY1 = angle_yaxis_min
        ElseIf PosY1 > angle_yaxis_max Then
            PosY1 = angle_yaxis_max
        End If
        PosY2 = Graph_angle_Y_center - StepY * Ky2
        If PosY2 < angle_yaxis_min Then
            PosY2 = angle_yaxis_min
        ElseIf PosY2 > angle_yaxis_max Then
            PosY2 = angle_yaxis_max
        End If

        path1.StartFigure()
        path1.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

        '---angle deep---
        Ds = DataPrcStr(KdData, SampleNo - 1, 8)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            Ky1 = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            Ky1 = Val(Ds)
        End If
        Ds = DataPrcStr(KdData, SampleNo, 8)
        Ds_1 = Strings.Left(Ds, 1)
        If Ds_1 = "C" Or Ds_1 = "M" Then
            Ky2 = Val(Strings.Right(Ds, Len(Ds) - 2))
        Else
            Ky2 = Val(Ds)
        End If

        PosY1 = Graph_angle_Y_center - StepY * Ky1
        If PosY1 < angle_yaxis_min Then
            PosY1 = angle_yaxis_min
        End If
        PosY2 = Graph_angle_Y_center - StepY * Ky2
        If PosY2 < angle_yaxis_min Then
            PosY2 = angle_yaxis_min
        End If

        path2.StartFigure()
        path2.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

        StepY = Graph_ratio_height / 2.5

        '---ratio peak---
        Ky1 = Val(DataPrcStr(KdData, SampleNo - 1, 11))
        Ky2 = Val(DataPrcStr(KdData, SampleNo, 11))

        PosY1 = ratio_yaxis_max - StepY * Ky1
        If PosY1 < ratio_yaxis_min Then
            PosY1 = ratio_yaxis_min
        ElseIf PosY1 > ratio_yaxis_max Then
            PosY1 = ratio_yaxis_max
        End If
        PosY2 = ratio_yaxis_max - StepY * Ky2
        If PosY2 < ratio_yaxis_min Then
            PosY2 = ratio_yaxis_min
        ElseIf PosY2 > ratio_yaxis_max Then
            PosY2 = ratio_yaxis_max
        End If

        path3.StartFigure()
        path3.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

        '---ratio deep---
        Ky1 = Val(DataPrcStr(KdData, SampleNo - 1, 10))
        Ky2 = Val(DataPrcStr(KdData, SampleNo, 10))

        PosY1 = ratio_yaxis_max - StepY * Ky1
        If PosY1 < ratio_yaxis_min Then
            PosY1 = ratio_yaxis_min
        ElseIf PosY1 > ratio_yaxis_max Then
            PosY1 = ratio_yaxis_max
        End If
        PosY2 = ratio_yaxis_max - StepY * Ky2
        If PosY2 < ratio_yaxis_min Then
            PosY2 = ratio_yaxis_min
        ElseIf PosY2 > ratio_yaxis_max Then
            PosY2 = ratio_yaxis_max
        End If

        path4.StartFigure()
        path4.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

        PosX1(KdData) += StepX

        angle_peak_old_path.Add(path1)
        angle_deep_old_path.Add(path2)
        ratio_pkdp_old_path.Add(path3)
        ratio_mdcd_old_path.Add(path4)

        PictureBox1.Refresh()
        PictureBox2.Refresh()
    End Sub


    Private Sub PrfGraphVelocityTSI()
        Dim StepX As Single
        Dim StepY As Single
        Dim PosY1 As Single
        Dim PosY2 As Single
        Dim Ky1 As Single
        Dim Ky2 As Single
        Dim Graph_velo_height As Single
        Dim Graph_tsi_height As Single
        Dim Graph_width As Single

        Graph_velo_height = velo_yaxis_max - velo_yaxis_min
        Graph_tsi_height = tsi_yaxis_max - tsi_yaxis_min
        Graph_width = graph_x_end - graph_x_sta

        Dim path1 As New GraphicsPath
        Dim path2 As New GraphicsPath
        Dim path3 As New GraphicsPath
        Dim path4 As New GraphicsPath
        Dim path5 As New GraphicsPath
        Dim path6 As New GraphicsPath

        If SampleNo < 2 Then
            Exit Sub
        End If

        If FlgProfile = 3 Then
            StepX = Graph_width / (lg_graph_max - 1)
        Else
            StepX = Graph_width / (Points - 1)
        End If

        'Velocity
        Select Case FlgVelocityRange
            Case 0
                StepY = Graph_velo_height / 5
            Case 1
                StepY = Graph_velo_height / 10
        End Select

        'Velocity-MD
        Ky1 = DataPrcNum(KdData, SampleNo - 1, 3)
        Ky2 = DataPrcNum(KdData, SampleNo, 3)
        PosY1 = velo_yaxis_max - StepY * Ky1
        If PosY1 < velo_yaxis_min Then
            PosY1 = velo_yaxis_min
        ElseIf PosY1 > velo_yaxis_max Then
            PosY1 = velo_yaxis_max
        End If
        PosY2 = velo_yaxis_max - StepY * Ky2
        If PosY2 < velo_yaxis_min Then
            PosY2 = velo_yaxis_min
        ElseIf PosY2 > velo_yaxis_max Then
            PosY2 = velo_yaxis_max
        End If

        path1.StartFigure()
        path1.AddLine(graph_x_sta + PosX2(KdData), PosY1, graph_x_sta + PosX2(KdData) + StepX, PosY2)

        'Velocity-CD
        Ky1 = DataPrcNum(KdData, SampleNo - 1, 11)
        Ky2 = DataPrcNum(KdData, SampleNo, 11)
        PosY1 = velo_yaxis_max - StepY * Ky1
        If PosY1 < velo_yaxis_min Then
            PosY1 = velo_yaxis_min
        ElseIf PosY1 > velo_yaxis_max Then
            PosY1 = velo_yaxis_max
        End If
        PosY2 = velo_yaxis_max - StepY * Ky2
        If PosY2 < velo_yaxis_min Then
            PosY2 = velo_yaxis_min
        ElseIf PosY2 > velo_yaxis_max Then
            PosY2 = velo_yaxis_max
        End If

        path2.StartFigure()
        path2.AddLine(graph_x_sta + PosX2(KdData), PosY1, graph_x_sta + PosX2(KdData) + StepX, PosY2)

        'Velocity-Peak
        Ky1 = DataPrcNum(KdData, SampleNo - 1, 2)
        Ky2 = DataPrcNum(KdData, SampleNo, 2)
        PosY1 = velo_yaxis_max - StepY * Ky1
        If PosY1 < velo_yaxis_min Then
            PosY1 = velo_yaxis_min
        ElseIf PosY1 > velo_yaxis_max Then
            PosY1 = velo_yaxis_max
        End If
        PosY2 = velo_yaxis_max - StepY * Ky2
        If PosY2 < velo_yaxis_min Then
            PosY2 = velo_yaxis_min
        ElseIf PosY2 > velo_yaxis_max Then
            PosY2 = velo_yaxis_max
        End If

        path3.StartFigure()
        path3.AddLine(graph_x_sta + PosX2(KdData), PosY1, graph_x_sta + PosX2(KdData) + StepX, PosY2)

        'Velocity-Deep
        Ky1 = DataPrcNum(KdData, SampleNo - 1, 1)
        Ky2 = DataPrcNum(KdData, SampleNo, 1)
        PosY1 = velo_yaxis_max - StepY * Ky1
        If PosY1 < velo_yaxis_min Then
            PosY1 = velo_yaxis_min
        ElseIf PosY1 > velo_yaxis_max Then
            PosY1 = velo_yaxis_max
        End If
        PosY2 = velo_yaxis_max - StepY * Ky2
        If PosY2 < velo_yaxis_min Then
            PosY2 = velo_yaxis_min
        ElseIf PosY2 > velo_yaxis_max Then
            PosY2 = velo_yaxis_max
        End If

        path4.StartFigure()
        path4.AddLine(graph_x_sta + PosX2(KdData), PosY1, graph_x_sta + PosX2(KdData) + StepX, PosY2)

        'TSI
        Select Case FlgTSIRange
            Case 0
                StepY = Graph_tsi_height / 25
            Case 1
                StepY = Graph_tsi_height / 100
        End Select

        'TSI-MD
        Ky1 = DataPrcNum(KdData, SampleNo - 1, 3) ^ 2
        Ky2 = DataPrcNum(KdData, SampleNo, 3) ^ 2
        PosY1 = tsi_yaxis_max - StepY * Ky1
        If PosY1 < tsi_yaxis_min Then
            PosY1 = tsi_yaxis_min
        ElseIf PosY1 > tsi_yaxis_max Then
            PosY1 = tsi_yaxis_max
        End If
        PosY2 = tsi_yaxis_max - StepY * Ky2
        If PosY2 < tsi_yaxis_min Then
            PosY2 = tsi_yaxis_min
        ElseIf PosY2 > tsi_yaxis_max Then
            PosY2 = tsi_yaxis_max
        End If
        path5.StartFigure()
        path5.AddLine(graph_x_sta + PosX2(KdData), PosY1, graph_x_sta + PosX2(KdData) + StepX, PosY2)

        'TSI-CD
        Ky1 = DataPrcNum(KdData, SampleNo - 1, 11) ^ 2
        Ky2 = DataPrcNum(KdData, SampleNo, 11) ^ 2
        PosY1 = tsi_yaxis_max - StepY * Ky1
        If PosY1 < tsi_yaxis_min Then
            PosY1 = tsi_yaxis_min
        ElseIf PosY1 > tsi_yaxis_max Then
            PosY1 = tsi_yaxis_max
        End If
        PosY2 = tsi_yaxis_max - StepY * Ky2
        If PosY2 < tsi_yaxis_min Then
            PosY2 = tsi_yaxis_min
        ElseIf PosY2 > tsi_yaxis_max Then
            PosY2 = tsi_yaxis_max
        End If
        path6.StartFigure()
        path6.AddLine(graph_x_sta + PosX2(KdData), PosY1, graph_x_sta + PosX2(KdData) + StepX, PosY2)

        PosX2(KdData) += StepX

        velo_md_cur_path.Add(path1)
        velo_cd_cur_path.Add(path2)
        velo_peak_cur_path.Add(path3)
        velo_deep_cur_path.Add(path4)
        tsi_md_cur_path.Add(path5)
        tsi_cd_cur_path.Add(path6)

        PictureBox3.Refresh()
        PictureBox4.Refresh()
    End Sub

    Private Sub PrfGraphVelocityTSIOld()
        Dim StepX As Single
        Dim StepY As Single
        Dim PosY1 As Single
        Dim PosY2 As Single
        Dim Ky1 As Single
        Dim Ky2 As Single
        Dim Graph_velo_height As Single
        Dim Graph_tsi_height As Single
        Dim Graph_width As Single

        Graph_velo_height = velo_yaxis_max - velo_yaxis_min
        Graph_tsi_height = tsi_yaxis_max - tsi_yaxis_min
        Graph_width = graph_x_end - graph_x_sta

        Dim path1 As New GraphicsPath
        Dim path2 As New GraphicsPath
        Dim path3 As New GraphicsPath
        Dim path4 As New GraphicsPath
        Dim path5 As New GraphicsPath
        Dim path6 As New GraphicsPath

        If SampleNo < 2 Then
            Exit Sub
        End If

        If FlgProfile = 3 Then
            StepX = Graph_width / (lg_graph_max - 1)
        Else
            StepX = Graph_width / (Points - 1)
        End If

        'Velocity
        Select Case FlgVelocityRange
            Case 0
                StepY = Graph_velo_height / 5
            Case 1
                StepY = Graph_velo_height / 10
        End Select

        'Velocity-MD
        Ky1 = DataPrcNum(KdData, SampleNo - 1, 3)
        Ky2 = DataPrcNum(KdData, SampleNo, 3)
        PosY1 = velo_yaxis_max - StepY * Ky1
        If PosY1 < velo_yaxis_min Then
            PosY1 = velo_yaxis_min
        ElseIf PosY1 > velo_yaxis_max Then
            PosY1 = velo_yaxis_max
        End If
        PosY2 = velo_yaxis_max - StepY * Ky2
        If PosY2 < velo_yaxis_min Then
            PosY2 = velo_yaxis_min
        ElseIf PosY2 > velo_yaxis_max Then
            PosY2 = velo_yaxis_max
        End If

        path1.StartFigure()
        path1.AddLine(graph_x_sta + PosX2(KdData), PosY1, graph_x_sta + PosX2(KdData) + StepX, PosY2)

        'Velocity-CD
        Ky1 = DataPrcNum(KdData, SampleNo - 1, 11)
        Ky2 = DataPrcNum(KdData, SampleNo, 11)
        PosY1 = velo_yaxis_max - StepY * Ky1
        If PosY1 < velo_yaxis_min Then
            PosY1 = velo_yaxis_min
        ElseIf PosY1 > velo_yaxis_max Then
            PosY1 = velo_yaxis_max
        End If
        PosY2 = velo_yaxis_max - StepY * Ky2
        If PosY2 < velo_yaxis_min Then
            PosY2 = velo_yaxis_min
        ElseIf PosY2 > velo_yaxis_max Then
            PosY2 = velo_yaxis_max
        End If

        path2.StartFigure()
        path2.AddLine(graph_x_sta + PosX2(KdData), PosY1, graph_x_sta + PosX2(KdData) + StepX, PosY2)

        'Velocity-Peak
        Ky1 = DataPrcNum(KdData, SampleNo - 1, 2)
        Ky2 = DataPrcNum(KdData, SampleNo, 2)
        PosY1 = velo_yaxis_max - StepY * Ky1
        If PosY1 < velo_yaxis_min Then
            PosY1 = velo_yaxis_min
        ElseIf PosY1 > velo_yaxis_max Then
            PosY1 = velo_yaxis_max
        End If
        PosY2 = velo_yaxis_max - StepY * Ky2
        If PosY2 < velo_yaxis_min Then
            PosY2 = velo_yaxis_min
        ElseIf PosY2 > velo_yaxis_max Then
            PosY2 = velo_yaxis_max
        End If

        path3.StartFigure()
        path3.AddLine(graph_x_sta + PosX2(KdData), PosY1, graph_x_sta + PosX2(KdData) + StepX, PosY2)

        'Velocity-Deep
        Ky1 = DataPrcNum(KdData, SampleNo - 1, 1)
        Ky2 = DataPrcNum(KdData, SampleNo, 1)
        PosY1 = velo_yaxis_max - StepY * Ky1
        If PosY1 < velo_yaxis_min Then
            PosY1 = velo_yaxis_min
        ElseIf PosY1 > velo_yaxis_max Then
            PosY1 = velo_yaxis_max
        End If
        PosY2 = velo_yaxis_max - StepY * Ky2
        If PosY2 < velo_yaxis_min Then
            PosY2 = velo_yaxis_min
        ElseIf PosY2 > velo_yaxis_max Then
            PosY2 = velo_yaxis_max
        End If

        path4.StartFigure()
        path4.AddLine(graph_x_sta + PosX2(KdData), PosY1, graph_x_sta + PosX2(KdData) + StepX, PosY2)

        'TSI
        Select Case FlgTSIRange
            Case 0
                StepY = Graph_tsi_height / 25
            Case 1
                StepY = Graph_tsi_height / 100
        End Select

        'TSI-MD
        Ky1 = DataPrcNum(KdData, SampleNo - 1, 3) ^ 2
        Ky2 = DataPrcNum(KdData, SampleNo, 3) ^ 2
        PosY1 = tsi_yaxis_max - StepY * Ky1
        If PosY1 < tsi_yaxis_min Then
            PosY1 = tsi_yaxis_min
        ElseIf PosY1 > tsi_yaxis_max Then
            PosY1 = tsi_yaxis_max
        End If
        PosY2 = tsi_yaxis_max - StepY * Ky2
        If PosY2 < tsi_yaxis_min Then
            PosY2 = tsi_yaxis_min
        ElseIf PosY2 > tsi_yaxis_max Then
            PosY2 = tsi_yaxis_max
        End If
        path5.StartFigure()
        path5.AddLine(graph_x_sta + PosX2(KdData), PosY1, graph_x_sta + PosX2(KdData) + StepX, PosY2)

        'TSI-CD
        Ky1 = DataPrcNum(KdData, SampleNo - 1, 11) ^ 2
        Ky2 = DataPrcNum(KdData, SampleNo, 11) ^ 2
        PosY1 = tsi_yaxis_max - StepY * Ky1
        If PosY1 < tsi_yaxis_min Then
            PosY1 = tsi_yaxis_min
        ElseIf PosY1 > tsi_yaxis_max Then
            PosY1 = tsi_yaxis_max
        End If
        PosY2 = tsi_yaxis_max - StepY * Ky2
        If PosY2 < tsi_yaxis_min Then
            PosY2 = tsi_yaxis_min
        ElseIf PosY2 > tsi_yaxis_max Then
            PosY2 = tsi_yaxis_max
        End If
        path6.StartFigure()
        path6.AddLine(graph_x_sta + PosX2(KdData), PosY1, graph_x_sta + PosX2(KdData) + StepX, PosY2)

        PosX2(KdData) += StepX

        velo_md_old_path.Add(path1)
        velo_cd_old_path.Add(path2)
        velo_peak_old_path.Add(path3)
        velo_deep_old_path.Add(path4)
        tsi_md_old_path.Add(path5)
        tsi_cd_old_path.Add(path6)

        PictureBox3.Refresh()
        PictureBox4.Refresh()
    End Sub

    Private Sub CmdAngleRange_Click(sender As Object, e As EventArgs) Handles CmdAngleRange.Click
        FlgAngleRange += 1
        If FlgAngleRange > 6 Then
            FlgAngleRange = 0
        End If

        flgTemp = FlgMainProfile
        FlgMainProfile = 26
    End Sub

    Private Sub CmdVeloRange_Click(sender As Object, e As EventArgs) Handles CmdVeloRange.Click
        FlgVelocityRange += 1
        If FlgVelocityRange > 1 Then
            FlgVelocityRange = 0
        End If

        flgTemp = FlgMainProfile
        FlgMainProfile = 28
    End Sub

    Private Sub CmdTSIRange_Click(sender As Object, e As EventArgs) Handles CmdTSIRange.Click
        FlgTSIRange += 1
        If FlgTSIRange > 1 Then
            FlgTSIRange = 0
        End If

        flgTemp = FlgMainProfile
        FlgMainProfile = 29
    End Sub

    Private Sub CmdAvg_Click(sender As Object, e As EventArgs) Handles CmdAvg.Click
        DataCount = 0
        FlgMainProfile = 45
        FlgAvg = 1
    End Sub

    Private Sub CmdClsGraph_Click(sender As Object, e As EventArgs) Handles CmdClsGraph.Click
        CmdAvg.Enabled = False
        AvgToolStripMenuItem.Enabled = False

        CmdPrfPrint.Enabled = False
        ManualPrintToolStripMenuItem.Enabled = False
        CmdPrfResultSave.Enabled = False
        SaveExcelToolStripMenuItem1.Enabled = False

        Points = TxtPoints.Text

        DrawCalcCurData_init()
        DrawCalcBakData_init()
        DrawCalcAvgData_init()
        DrawTableData_init()

        ClsNoPrf()
        GraphInitPrf(Points)

        Cls_PchExpOld()

    End Sub

    Private Sub Cls_PchExpOld()
        '過去のピッチ拡張設定関連のクリア
        PchExp_Length_old = 0
        ReDim PchExp_PchData_old(0)
        FlgPitchExp_Load_old = 0
    End Sub

    Private Sub RedrawGraphAngle()
        Dim StepX As Single
        Dim StepY As Single
        Dim PosY1 As Single
        Dim PosY2 As Single
        Dim Ky1 As Single
        Dim Ky2 As Single
        Dim Ds As String
        Dim Ds_1 As String
        Dim Graph_angle_height As Single
        Dim Graph_width As Single
        Dim Graph_Y_center As Single

        PictureBox1.CreateGraphics.Clear(BackColor)

        angle_peak_cur_path.Clear()     'angle-peak-graph clear
        angle_deep_cur_path.Clear()     'angle-deep-graph clear

        angle_yaxis_label(FlgAngleRange)

        Graph_angle_height = angle_yaxis_max - angle_yaxis_min
        Graph_width = graph_x_end - graph_x_sta
        Graph_Y_center = angle_yaxis_min + (Graph_angle_height / 2)

        Dim path1 As New GraphicsPath
        Dim path2 As New GraphicsPath
        'Dim path3 As New GraphicsPath
        'Dim path4 As New GraphicsPath

        If FlgProfile = 3 Then
            StepX = Graph_width / lg_graph_max
        Else
            StepX = Graph_width / (Points - 1)
        End If

        'Angle
        Select Case FlgAngleRange
            Case 0
                StepY = Graph_angle_height / 5
            Case 1
                StepY = Graph_angle_height / 10
            Case 2
                StepY = Graph_angle_height / 20
            Case 3
                StepY = Graph_angle_height / 40
            Case 4
                StepY = Graph_angle_height / 80
            Case 5
                StepY = Graph_angle_height / 160
            Case 6
                StepY = Graph_angle_height / 320
        End Select

        PosX1(KdData) = 0

        For i = 2 To SampleNo
            Ds = DataPrcStr(KdData, i - 1, 9)
            Ds_1 = Strings.Left(Ds, 1)
            If Ds_1 = "C" Or Ds_1 = "M" Then
                Ky1 = Val(Strings.Right(Ds, Len(Ds) - 2)) - PkAngCent
            Else
                Ky1 = Val(Ds) - PkAngCent
            End If
            Ds = DataPrcStr(KdData, i, 9)
            Ds_1 = Strings.Left(Ds, 1)
            If Ds_1 = "C" Or Ds_1 = "M" Then
                Ky2 = Val(Strings.Right(Ds, Len(Ds) - 2)) - PkAngCent
            Else
                Ky2 = Val(Ds) - PkAngCent
            End If

            PosY1 = Graph_Y_center - StepY * Ky1
            If PosY1 < angle_yaxis_min Then
                PosY1 = angle_yaxis_min
            ElseIf PosY1 > angle_yaxis_max Then
                PosY1 = angle_yaxis_max
            End If
            PosY2 = Graph_Y_center - StepY * Ky2
            If PosY2 < angle_yaxis_min Then
                PosY2 = angle_yaxis_min
            ElseIf PosY2 > angle_yaxis_max Then
                PosY2 = angle_yaxis_max
            End If

            path1.StartFigure()
            path1.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            Ds = DataPrcStr(KdData, i - 1, 8)
            Ds_1 = Strings.Left(Ds, 1)
            If Ds_1 = "C" Or Ds_1 = "M" Then
                Ky1 = Val(Strings.Right(Ds, Len(Ds) - 2)) - PkAngCent
            Else
                Ky1 = Val(Ds) - PkAngCent
            End If
            Ds = DataPrcStr(KdData, i, 8)
            Ds_1 = Strings.Left(Ds, 1)
            If Ds_1 = "C" Or Ds_1 = "M" Then
                Ky2 = Val(Strings.Right(Ds, Len(Ds) - 2)) - PkAngCent
            Else
                Ky2 = Val(Ds) - PkAngCent
            End If

            PosY1 = Graph_Y_center - StepY * Ky1
            If PosY1 < angle_yaxis_min Then
                PosY1 = angle_yaxis_min
            ElseIf PosY1 > angle_yaxis_max Then
                PosY1 = angle_yaxis_max
            End If
            PosY2 = Graph_Y_center - StepY * Ky2
            If PosY2 < angle_yaxis_min Then
                PosY2 = angle_yaxis_min
            ElseIf PosY2 > angle_yaxis_max Then
                PosY2 = angle_yaxis_max
            End If

            path2.StartFigure()
            path2.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            PosX1(KdData) += StepX
        Next

        angle_peak_cur_path.Add(path1)
        angle_deep_cur_path.Add(path2)

        PictureBox1.Refresh()
    End Sub

    Private Sub RedrawGraphAngleOld()
        Dim StepX As Single
        Dim StepY As Single
        Dim PosY1 As Single
        Dim PosY2 As Single
        Dim Ky1 As Single
        Dim Ky2 As Single
        Dim Ds As String
        Dim Ds_1 As String
        Dim Graph_angle_height As Single
        Dim Graph_width As Single
        Dim Graph_Y_center As Single

        PictureBox1.CreateGraphics.Clear(BackColor)

        angle_peak_old_path.Clear()     'angle-peak-graph clear
        angle_deep_old_path.Clear()     'angle-deep-graph clear

        angle_yaxis_label(FlgAngleRange)

        Graph_angle_height = angle_yaxis_max - angle_yaxis_min
        Graph_width = graph_x_end - graph_x_sta
        Graph_Y_center = angle_yaxis_min + (Graph_angle_height / 2)

        Dim path1 As New GraphicsPath
        Dim path2 As New GraphicsPath
        'Dim path3 As New GraphicsPath
        'Dim path4 As New GraphicsPath

        If FlgProfile = 3 Then
            StepX = Graph_width / lg_graph_max
        Else
            StepX = Graph_width / (Points - 1)
        End If

        'Angle
        Select Case FlgAngleRange
            Case 0
                StepY = Graph_angle_height / 5
            Case 1
                StepY = Graph_angle_height / 10
            Case 2
                StepY = Graph_angle_height / 20
            Case 3
                StepY = Graph_angle_height / 40
            Case 4
                StepY = Graph_angle_height / 80
            Case 5
                StepY = Graph_angle_height / 160
            Case 6
                StepY = Graph_angle_height / 320
        End Select

        PosX1(KdData) = 0

        For i = 2 To SampleNo
            Ds = DataPrcStr(KdData, i - 1, 9)
            Ds_1 = Strings.Left(Ds, 1)
            If Ds_1 = "C" Or Ds_1 = "M" Then
                Ky1 = Val(Strings.Right(Ds, Len(Ds) - 2)) - PkAngCent
            Else
                Ky1 = Val(Ds) - PkAngCent
            End If
            Ds = DataPrcStr(KdData, i, 9)
            Ds_1 = Strings.Left(Ds, 1)
            If Ds_1 = "C" Or Ds_1 = "M" Then
                Ky2 = Val(Strings.Right(Ds, Len(Ds) - 2)) - PkAngCent
            Else
                Ky2 = Val(Ds) - PkAngCent
            End If

            PosY1 = Graph_Y_center - StepY * Ky1
            If PosY1 < angle_yaxis_min Then
                PosY1 = angle_yaxis_min
            ElseIf PosY1 > angle_yaxis_max Then
                PosY1 = angle_yaxis_max
            End If
            PosY2 = Graph_Y_center - StepY * Ky2
            If PosY2 < angle_yaxis_min Then
                PosY2 = angle_yaxis_min
            ElseIf PosY2 > angle_yaxis_max Then
                PosY2 = angle_yaxis_max
            End If

            path1.StartFigure()
            path1.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            Ds = DataPrcStr(KdData, i - 1, 8)
            Ds_1 = Strings.Left(Ds, 1)
            If Ds_1 = "C" Or Ds_1 = "M" Then
                Ky1 = Val(Strings.Right(Ds, Len(Ds) - 2)) - PkAngCent
            Else
                Ky1 = Val(Ds) - PkAngCent
            End If
            Ds = DataPrcStr(KdData, i, 8)
            Ds_1 = Strings.Left(Ds, 1)
            If Ds_1 = "C" Or Ds_1 = "M" Then
                Ky2 = Val(Strings.Right(Ds, Len(Ds) - 2)) - PkAngCent
            Else
                Ky2 = Val(Ds) - PkAngCent
            End If

            PosY1 = Graph_Y_center - StepY * Ky1
            If PosY1 < angle_yaxis_min Then
                PosY1 = angle_yaxis_min
            End If
            PosY2 = Graph_Y_center - StepY * Ky2
            If PosY2 < angle_yaxis_min Then
                PosY2 = angle_yaxis_min
            End If

            path2.StartFigure()
            path2.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            PosX1(KdData) += StepX
        Next

        angle_peak_old_path.Add(path1)
        angle_deep_old_path.Add(path2)

        PictureBox1.Refresh()
    End Sub

    Private Sub RedrawGraphVelocity()
        Dim StepX As Single
        Dim StepY As Single
        Dim PosY1 As Single
        Dim PosY2 As Single
        Dim Ky1 As Single
        Dim Ky2 As Single
        Dim Graph_velo_height As Single
        Dim Graph_width As Single

        PictureBox3.CreateGraphics.Clear(BackColor)

        velo_md_cur_path.Clear()        'velocity-md-graph clear
        velo_cd_cur_path.Clear()        'velocity-cd-graph clear
        velo_peak_cur_path.Clear()      'veloctiy-peak-graph clear
        velo_deep_cur_path.Clear()      'velocity-deep-graph clear

        velo_yaxis_label(FlgVelocityRange)

        Graph_velo_height = velo_yaxis_max - velo_yaxis_min
        Graph_width = graph_x_end - graph_x_sta

        Dim path1 As New GraphicsPath
        Dim path2 As New GraphicsPath
        Dim path3 As New GraphicsPath
        Dim path4 As New GraphicsPath
        'Dim path5 As New GraphicsPath
        'Dim path6 As New GraphicsPath

        If FlgProfile = 3 Then
            StepX = Graph_width / lg_graph_max
        Else
            StepX = Graph_width / (Points - 1)
        End If

        'Velocity
        Select Case FlgVelocityRange
            Case 0
                StepY = Graph_velo_height / 5
            Case 1
                StepY = Graph_velo_height / 10
        End Select

        PosX1(KdData) = 0

        For i = 2 To SampleNo
            'Velocity-MD
            Ky1 = DataPrcNum(KdData, i - 1, 3)
            Ky2 = DataPrcNum(KdData, i, 3)
            PosY1 = velo_yaxis_max - StepY * Ky1
            If PosY1 < velo_yaxis_min Then
                PosY1 = velo_yaxis_min
            ElseIf PosY1 > velo_yaxis_max Then
                PosY1 = velo_yaxis_max
            End If
            PosY2 = velo_yaxis_max - StepY * Ky2
            If PosY2 < velo_yaxis_min Then
                PosY2 = velo_yaxis_min
            ElseIf PosY2 > velo_yaxis_max Then
                PosY2 = velo_yaxis_max
            End If

            path1.StartFigure()
            path1.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            'Velocity-CD
            Ky1 = DataPrcNum(KdData, i - 1, 11)
            Ky2 = DataPrcNum(KdData, i, 11)
            PosY1 = velo_yaxis_max - StepY * Ky1
            If PosY1 < velo_yaxis_min Then
                PosY1 = velo_yaxis_min
            ElseIf PosY1 > velo_yaxis_max Then
                PosY1 = velo_yaxis_max
            End If
            PosY2 = velo_yaxis_max - StepY * Ky2
            If PosY2 < velo_yaxis_min Then
                PosY2 = velo_yaxis_min
            ElseIf PosY2 > velo_yaxis_max Then
                PosY2 = velo_yaxis_max
            End If

            path2.StartFigure()
            path2.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            'Velocity-Peak
            Ky1 = DataPrcNum(KdData, i - 1, 2)
            Ky2 = DataPrcNum(KdData, i, 2)
            PosY1 = velo_yaxis_max - StepY * Ky1
            If PosY1 < velo_yaxis_min Then
                PosY1 = velo_yaxis_min
            ElseIf PosY1 > velo_yaxis_max Then
                PosY1 = velo_yaxis_max
            End If
            PosY2 = velo_yaxis_max - StepY * Ky2
            If PosY2 < velo_yaxis_min Then
                PosY2 = velo_yaxis_min
            ElseIf PosY2 > velo_yaxis_max Then
                PosY2 = velo_yaxis_max
            End If

            path3.StartFigure()
            path3.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            'Velocity-Deep
            Ky1 = DataPrcNum(KdData, i - 1, 1)
            Ky2 = DataPrcNum(KdData, i, 1)
            PosY1 = velo_yaxis_max - StepY * Ky1
            If PosY1 < velo_yaxis_min Then
                PosY1 = velo_yaxis_min
            ElseIf PosY1 > velo_yaxis_max Then
                PosY1 = velo_yaxis_max
            End If
            PosY2 = velo_yaxis_max - StepY * Ky2
            If PosY2 < velo_yaxis_min Then
                PosY2 = velo_yaxis_min
            ElseIf PosY2 > velo_yaxis_max Then
                PosY2 = velo_yaxis_max
            End If

            path4.StartFigure()
            path4.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            PosX1(KdData) += StepX
        Next

        velo_md_cur_path.Add(path1)
        velo_cd_cur_path.Add(path2)
        velo_peak_cur_path.Add(path3)
        velo_deep_cur_path.Add(path4)

        PictureBox3.Refresh()
    End Sub

    Private Sub RedrawGraphVelocityOld()
        Dim StepX As Single
        Dim StepY As Single
        Dim PosY1 As Single
        Dim PosY2 As Single
        Dim Ky1 As Single
        Dim Ky2 As Single
        Dim Graph_velo_height As Single
        Dim Graph_width As Single

        PictureBox3.CreateGraphics.Clear(BackColor)

        velo_md_old_path.Clear()        'velocity-md-graph clear
        velo_cd_old_path.Clear()        'velocity-cd-graph clear
        velo_peak_old_path.Clear()      'veloctiy-peak-graph clear
        velo_deep_old_path.Clear()      'velocity-deep-graph clear

        velo_yaxis_label(FlgVelocityRange)

        Graph_velo_height = velo_yaxis_max - velo_yaxis_min
        Graph_width = graph_x_end - graph_x_sta

        Dim path1 As New GraphicsPath
        Dim path2 As New GraphicsPath
        Dim path3 As New GraphicsPath
        Dim path4 As New GraphicsPath
        'Dim path5 As New GraphicsPath
        'Dim path6 As New GraphicsPath

        If FlgProfile = 3 Then
            StepX = Graph_width / lg_graph_max
        Else
            StepX = Graph_width / (Points - 1)
        End If

        'Velocity
        Select Case FlgVelocityRange
            Case 0
                StepY = Graph_velo_height / 5
            Case 1
                StepY = Graph_velo_height / 10
        End Select

        PosX1(KdData) = 0

        For i = 2 To SampleNo
            'Velocity-MD
            Ky1 = DataPrcNum(KdData, i - 1, 3)
            Ky2 = DataPrcNum(KdData, i, 3)
            PosY1 = velo_yaxis_max - StepY * Ky1
            If PosY1 < velo_yaxis_min Then
                PosY1 = velo_yaxis_min
            ElseIf PosY1 > velo_yaxis_max Then
                PosY1 = velo_yaxis_max
            End If
            PosY2 = velo_yaxis_max - StepY * Ky2
            If PosY2 < velo_yaxis_min Then
                PosY2 = velo_yaxis_min
            ElseIf PosY2 > velo_yaxis_max Then
                PosY2 = velo_yaxis_max
            End If

            path1.StartFigure()
            path1.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            'Velocity-CD
            Ky1 = DataPrcNum(KdData, i - 1, 11)
            Ky2 = DataPrcNum(KdData, i, 11)
            PosY1 = velo_yaxis_max - StepY * Ky1
            If PosY1 < velo_yaxis_min Then
                PosY1 = velo_yaxis_min
            ElseIf PosY1 > velo_yaxis_max Then
                PosY1 = velo_yaxis_max
            End If
            PosY2 = velo_yaxis_max - StepY * Ky2
            If PosY2 < velo_yaxis_min Then
                PosY2 = velo_yaxis_min
            ElseIf PosY2 > velo_yaxis_max Then
                PosY2 = velo_yaxis_max
            End If

            path2.StartFigure()
            path2.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            'Velocity-Peak
            Ky1 = DataPrcNum(KdData, i - 1, 2)
            Ky2 = DataPrcNum(KdData, i, 2)
            PosY1 = velo_yaxis_max - StepY * Ky1
            If PosY1 < velo_yaxis_min Then
                PosY1 = velo_yaxis_min
            ElseIf PosY1 > velo_yaxis_max Then
                PosY1 = velo_yaxis_max
            End If
            PosY2 = velo_yaxis_max - StepY * Ky2
            If PosY2 < velo_yaxis_min Then
                PosY2 = velo_yaxis_min
            ElseIf PosY2 > velo_yaxis_max Then
                PosY2 = velo_yaxis_max
            End If

            path3.StartFigure()
            path3.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            'Velocity-Deep
            Ky1 = DataPrcNum(KdData, i - 1, 1)
            Ky2 = DataPrcNum(KdData, i, 1)
            PosY1 = velo_yaxis_max - StepY * Ky1
            If PosY1 < velo_yaxis_min Then
                PosY1 = velo_yaxis_min
            ElseIf PosY1 > velo_yaxis_max Then
                PosY1 = velo_yaxis_max
            End If
            PosY2 = velo_yaxis_max - StepY * Ky2
            If PosY2 < velo_yaxis_min Then
                PosY2 = velo_yaxis_min
            ElseIf PosY2 > velo_yaxis_max Then
                PosY2 = velo_yaxis_max
            End If

            path4.StartFigure()
            path4.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            PosX1(KdData) += StepX
        Next

        velo_md_old_path.Add(path1)
        velo_cd_old_path.Add(path2)
        velo_peak_old_path.Add(path3)
        velo_deep_old_path.Add(path4)

        PictureBox3.Refresh()
    End Sub

    Private Sub RedrawGraphTSI()
        Dim StepX As Single
        Dim StepY As Single
        Dim PosY1 As Single
        Dim PosY2 As Single
        Dim Ky1 As Single
        Dim Ky2 As Single
        Dim Graph_tsi_height As Single
        Dim Graph_width As Single

        PictureBox4.CreateGraphics.Clear(BackColor)

        tsi_md_cur_path.Clear()
        tsi_cd_cur_path.Clear()

        tsi_yaxis_label(FlgTSIRange)

        Graph_tsi_height = tsi_yaxis_max - tsi_yaxis_min
        Graph_width = graph_x_end - graph_x_sta

        Dim path1 As New GraphicsPath
        Dim path2 As New GraphicsPath

        If FlgProfile = 3 Then
            StepX = Graph_width / lg_graph_max
        Else
            StepX = Graph_width / (Points - 1)
        End If

        'TSI
        Select Case FlgTSIRange
            Case 0
                StepY = Graph_tsi_height / 25
            Case 1
                StepY = Graph_tsi_height / 100
        End Select

        PosX1(KdData) = 0

        For i = 2 To SampleNo
            Ky1 = DataPrcNum(KdData, i - 1, 3) ^ 2
            Ky2 = DataPrcNum(KdData, i, 3) ^ 2
            PosY1 = tsi_yaxis_max - StepY * Ky1
            If PosY1 < tsi_yaxis_min Then
                PosY1 = tsi_yaxis_min
            ElseIf PosY1 > tsi_yaxis_max Then
                PosY1 = tsi_yaxis_max
            End If
            PosY2 = tsi_yaxis_max - StepY * Ky2
            If PosY2 < tsi_yaxis_min Then
                PosY2 = tsi_yaxis_min
            ElseIf PosY2 > tsi_yaxis_max Then
                PosY2 = tsi_yaxis_max
            End If

            path1.StartFigure()
            path1.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            Ky1 = DataPrcNum(KdData, i - 1, 11) ^ 2
            Ky2 = DataPrcNum(KdData, i, 11) ^ 2
            PosY1 = tsi_yaxis_max - StepY * Ky1
            If PosY1 < tsi_yaxis_min Then
                PosY1 = tsi_yaxis_min
            ElseIf PosY1 > tsi_yaxis_max Then
                PosY1 = tsi_yaxis_max
            End If
            PosY2 = tsi_yaxis_max - StepY * Ky2
            If PosY2 < tsi_yaxis_min Then
                PosY2 = tsi_yaxis_min
            ElseIf PosY2 > tsi_yaxis_max Then
                PosY2 = tsi_yaxis_max
            End If

            path2.StartFigure()
            path2.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            PosX1(KdData) += StepX
        Next

        tsi_md_cur_path.Add(path1)
        tsi_cd_cur_path.Add(path2)

        PictureBox4.Refresh()
    End Sub

    Private Sub RedrawGraphTSIOld()
        Dim StepX As Single
        Dim StepY As Single
        Dim PosY1 As Single
        Dim PosY2 As Single
        Dim Ky1 As Single
        Dim Ky2 As Single
        Dim Graph_tsi_height As Single
        Dim Graph_width As Single

        PictureBox4.CreateGraphics.Clear(BackColor)

        tsi_md_old_path.Clear()
        tsi_cd_old_path.Clear()

        tsi_yaxis_label(FlgTSIRange)

        Graph_tsi_height = tsi_yaxis_max - tsi_yaxis_min
        Graph_width = graph_x_end - graph_x_sta

        Dim path1 As New GraphicsPath
        Dim path2 As New GraphicsPath

        If FlgProfile = 3 Then
            StepX = Graph_width / lg_graph_max
        Else
            StepX = Graph_width / (Points - 1)
        End If

        'TSI
        Select Case FlgTSIRange
            Case 0
                StepY = Graph_tsi_height / 25
            Case 1
                StepY = Graph_tsi_height / 100
        End Select

        PosX1(KdData) = 0

        For i = 2 To SampleNo
            Ky1 = DataPrcNum(KdData, i - 1, 3) ^ 2
            Ky2 = DataPrcNum(KdData, i, 3) ^ 2
            PosY1 = tsi_yaxis_max - StepY * Ky1
            If PosY1 < tsi_yaxis_min Then
                PosY1 = tsi_yaxis_min
            ElseIf PosY1 > tsi_yaxis_max Then
                PosY1 = tsi_yaxis_max
            End If
            PosY2 = tsi_yaxis_max - StepY * Ky2
            If PosY2 < tsi_yaxis_min Then
                PosY2 = tsi_yaxis_min
            ElseIf PosY2 > tsi_yaxis_max Then
                PosY2 = tsi_yaxis_max
            End If

            path1.StartFigure()
            path1.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            Ky1 = DataPrcNum(KdData, i - 1, 11) ^ 2
            Ky2 = DataPrcNum(KdData, i, 11) ^ 2
            PosY1 = tsi_yaxis_max - StepY * Ky1
            If PosY1 < tsi_yaxis_min Then
                PosY1 = tsi_yaxis_min
            ElseIf PosY1 > tsi_yaxis_max Then
                PosY1 = tsi_yaxis_max
            End If
            PosY2 = tsi_yaxis_max - StepY * Ky2
            If PosY2 < tsi_yaxis_min Then
                PosY2 = tsi_yaxis_min
            ElseIf PosY2 > tsi_yaxis_max Then
                PosY2 = tsi_yaxis_max
            End If

            path2.StartFigure()
            path2.AddLine(graph_x_sta + PosX1(KdData), PosY1, graph_x_sta + PosX1(KdData) + StepX, PosY2)

            PosX1(KdData) += StepX
        Next

        tsi_md_old_path.Add(path1)
        tsi_cd_old_path.Add(path2)

        PictureBox4.Refresh()
    End Sub

    Private Sub ChkPrn_AngleRatio_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPrn_AngleRatio.CheckedChanged
        If ChkPrn_AngleRatio.Checked = True Then
            chkPrnAngleRatio = 1
            If MenuPrn_AngleRatio.Checked = False Then
                MenuPrn_AngleRatio.Checked = True
                'FlgConstChg = True  '変更有の状態にセットする
            End If
        Else
            chkPrnAngleRatio = 0
            If MenuPrn_AngleRatio.Checked = True Then
                MenuPrn_AngleRatio.Checked = False
                'FlgConstChg = True  '変更有の状態にセットする
            End If
        End If
        FlgPrfPrint = chkPrnAngleRatio * 1 +
                      chkPrnVelocityTSI * 2 +
                      chkPrnMeasData * 4 +
                      chkPrnOldData * 8 +
                      chkPrnAvgData * 16
        If FlgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text2)
        End If
    End Sub

    Private Sub ChkPrn_Velocity_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPrn_VelocityTSI.CheckedChanged
        If ChkPrn_VelocityTSI.Checked = True Then
            chkPrnVelocityTSI = 1
            If MenuPrn_VeloTSI.Checked = False Then
                MenuPrn_VeloTSI.Checked = True
            End If
        Else
            chkPrnVelocityTSI = 0
            If MenuPrn_VeloTSI.Checked = True Then
                MenuPrn_VeloTSI.Checked = False
            End If
        End If
        FlgPrfPrint = chkPrnAngleRatio * 1 +
                      chkPrnVelocityTSI * 2 +
                      chkPrnMeasData * 4 +
                      chkPrnOldData * 8 +
                      chkPrnAvgData * 16
        If FlgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text2)
        End If
    End Sub

    Private Sub ChkPrn_MeasData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPrn_MeasData.CheckedChanged
        If ChkPrn_MeasData.Checked = True Then
            chkPrnMeasData = 1
            If MenuPrn_measData.Checked = False Then
                MenuPrn_measData.Checked = True
            End If
        Else
            chkPrnMeasData = 0
            If MenuPrn_measData.Checked = True Then
                MenuPrn_measData.Checked = False
            End If
        End If
        FlgPrfPrint = chkPrnAngleRatio * 1 +
                      chkPrnVelocityTSI * 2 +
                      chkPrnMeasData * 4 +
                      chkPrnOldData * 8 +
                      chkPrnAvgData * 16
        If FlgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text2)
        End If
    End Sub

    Private Sub ChkPrn_OldData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPrn_OldData.CheckedChanged
        If ChkPrn_OldData.Checked = True Then
            chkPrnOldData = 1
            If MenuPrn_OldData.Checked = False Then
                MenuPrn_OldData.Checked = True
            End If
        Else
            chkPrnOldData = 0
            If MenuPrn_OldData.Checked = True Then
                MenuPrn_OldData.Checked = False
            End If
        End If
        FlgPrfPrint = chkPrnAngleRatio * 1 +
                      chkPrnVelocityTSI * 2 +
                      chkPrnMeasData * 4 +
                      chkPrnOldData * 8 +
                      chkPrnAvgData * 16
        If FlgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text2)
        End If
    End Sub

    Private Sub ChkPrn_AvgData_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPrn_AvgData.CheckedChanged
        If ChkPrn_AvgData.Checked = True Then
            chkPrnAvgData = 1
            If MenuPrn_AvgData.Checked = False Then
                MenuPrn_AvgData.Checked = True
            End If
        Else
            chkPrnAvgData = 0
            If MenuPrn_AvgData.Checked = True Then
                MenuPrn_AvgData.Checked = False
            End If
        End If
        FlgPrfPrint = chkPrnAngleRatio * 1 +
                      chkPrnVelocityTSI * 2 +
                      chkPrnMeasData * 4 +
                      chkPrnOldData * 8 +
                      chkPrnAvgData * 16
        If FlgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text2)
        End If
    End Sub

    Private Sub CmdMeasSpecSel_Click(sender As Object, e As EventArgs) Handles CmdMeasSpecSel.Click
        Dim result As DialogResult
        Dim fname As String = ""

        result = LoadDefConstName(fname, False)

        If result = DialogResult.OK Then
            StrConstFileName = fname
            LoadConstPitch_FileErr_Run = 0

            FlgInitEnd = 0

            LoadConst(Me, title_text2)

            FlgInitEnd = 1

            If FlgPitchExp = 1 Then
                LoadConstPitch(PchExpSettingFile_FullPath)
                If FlgPitchExp_Load = 1 Then
                    TxtLength.Text = PchExp_Length
                    TxtPitch.Text = PchExp_PchData(0)
                    PchExp_Points = UBound(PchExp_PchData) + 2
                    TxtPoints.Text = PchExp_Points
                End If
            Else
                FlgPitchExp_Load = 0
            End If

            'ClsNoPrf()
            'GraphInitPrf()

            FlgMainProfile = 20
        End If
    End Sub

    Private Sub WrtOldMeasInfo()
        '過去の測定仕様にデータを展開
        '管理者モードのみ

        If FlgDBF = 0 Then
            '測定データモード　通常
            TxtMachNoBak.Text = DataFileStr(FileNo, 1, 1)
            If DataFileStr(FileNo, 1, 4) = "" Then
                TxtSmplNamBak.Text = DataFileStr(FileNo, 1, 2)
            Else
                TxtSmplNamBak.Text = DataFileStr(FileNo, 1, 2) & "," &
                                     DataFileStr(FileNo, 1, 4)
            End If
            TxtMarkBak.Text = DataFileStr(FileNo, 1, 3)
            TxtMeasNumBak.Text = FileDataMax
            TxtMeasLotBak.Text = FileNo
        Else
            '測定データモード　特殊1
            TxtMachNoBak.Text = DataFileStr(FileNo, 1, 1)
            TxtSmplNamBak.Text = DataFileStr(FileNo, 1, 2)
            TxtMarkBak.Text = DataFileStr(FileNo, 1, 3)
            '斤量は無視する
            TxtMeasNumBak.Text = FileDataMax
            TxtMeasLotBak.Text = FileNo
        End If

        If FlgInch = 1 Then
            TxtLengthOld.Text = Math.Round(LengthOld / 25.4, 2, MidpointRounding.AwayFromZero)
            TxtPitchOld.Text = Math.Round(PitchOld / 25.4, 2, MidpointRounding.AwayFromZero)
        Else
            TxtLengthOld.Text = LengthOld
            TxtPitchOld.Text = PitchOld
        End If
        TxtPointsOld.Text = FileDataMax

    End Sub

    Private Sub CmdOldDataLoad_Click(sender As Object, e As EventArgs) Handles CmdOldDataLoad.Click
        FlgMainProfile = 40
        FlgAvg = 0
    End Sub

    Private Sub FrmSST4500_1_0_0E_Profile_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size

        groupMenuUnit = New ToolStripMenuItem() _
            {Me.MmToolStripMenuItem,
             Me.InchToolStripMenuItem}

        Menu_AutoPrn = DirectCast(AutoPrintToolStripMenuItem, ToolStripMenuItem)
        MenuPrn_AngleRatio = DirectCast(AngRatToolStripMenuItem, ToolStripMenuItem)
        MenuPrn_VeloTSI = DirectCast(VeloTSIToolStripMenuItem, ToolStripMenuItem)
        MenuPrn_measData = DirectCast(MeasDataTableToolStripMenuItem, ToolStripMenuItem)
        MenuPrn_OldData = DirectCast(OldDataTableToolStripMenuItem, ToolStripMenuItem)
        MenuPrn_AvgData = DirectCast(AvgDataTableToolStripMenuItem, ToolStripMenuItem)
    End Sub

    Private Sub SetPrintChk()
        Select Case FlgPrfPrint
            Case 0
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = False
            Case 1
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = False
            Case 2
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = False
            Case 3
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = False
            Case 4
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = False
            Case 5
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = False
            Case 6
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = False
            Case 7
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = False
            Case 8
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = False
            Case 9
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = False
            Case 10
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = False
            Case 11
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = False
            Case 12
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = False
            Case 13
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = False
            Case 14
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = False
            Case 15
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = False
            Case 16
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = True
            Case 17
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = True
            Case 18
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = True
            Case 19
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = True
            Case 20
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = True
            Case 21
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = True
            Case 22
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = True
            Case 23
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = False
                ChkPrn_AvgData.Checked = True
            Case 24
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = True
            Case 25
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = True
            Case 26
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = True
            Case 27
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = False
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = True
            Case 28
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = True
            Case 29
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = False
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = True
            Case 30
                ChkPrn_AngleRatio.Checked = False
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = True
            Case 31
                ChkPrn_AngleRatio.Checked = True
                ChkPrn_VelocityTSI.Checked = True
                ChkPrn_MeasData.Checked = True
                ChkPrn_OldData.Checked = True
                ChkPrn_AvgData.Checked = True
        End Select
    End Sub

    Private Sub OptInch_CheckedChanged(sender As Object, e As EventArgs) Handles OptInch.CheckedChanged
        MmToolStripMenuItem.CheckState = CheckState.Unchecked
        InchToolStripMenuItem.CheckState = CheckState.Indeterminate
        FlgInch = 1
        FlgMainProfile = 24
    End Sub

    Private Sub OptMm_CheckedChanged(sender As Object, e As EventArgs) Handles OptMm.CheckedChanged
        MmToolStripMenuItem.CheckState = CheckState.Indeterminate
        InchToolStripMenuItem.CheckState = CheckState.Unchecked
        FlgInch = 0
        FlgMainProfile = 25
    End Sub

    Private Sub CmdMeasSpecSave_Click(sender As Object, e As EventArgs) Handles CmdMeasSpecSave.Click
        SaveConstPrf()
    End Sub

    Private Sub SaveConstPrf()
        'ソフト起動時に実行済み
        'Dim curdir As String
        'CurDir = Directory.GetCurrentDirectory
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
            '測定データフォーマット 特殊
            Sample = TxtSmplNamCur.Text
            Mark = TxtMarkCur.Text
            '斤量は無視する
        End If

        If ChkPrfAutoPrn.Checked = True Then
            FlgPrfAutoPrn = 1
        Else
            FlgPrfAutoPrn = 0
        End If

        Select Case FlgProfile
            Case 0
                filter_tmp = "Constant File(SG*.cns)|SG*.cns"
                StrFileName = "SG_0_" & Trim(MachineNo) & "_" & Trim(Sample) & ".cns"
                chk_filehead = "SG"
            Case 1
                If FlgPitchExp = 0 Then
                    'ピッチ拡張無効時
                    _points = Trim(Str(Points))
                Else
                    'ピッチ拡張有効時
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
                .Title = StrSaveMeasSpecFile
                .Filter = filter_tmp
                .FileName = StrFileName

                Ret = .ShowDialog

                If Ret = DialogResult.OK Then
                    FilePath = .FileName

                    chk_filename = Strings.Left(Path.GetFileName(FilePath), 2)
                    If chk_filename <> chk_filehead Then
                        MessageBox.Show("File Nmae must start with """ & chk_filehead & """" & vbCrLf &
                                        "Cancel the save process",
                                        StrFileName,
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
                    Me.Text = title_text2 & " (" & _filename2 & ")"
                    FlgConstChg = False '変更無し状態に初期化
                    FlgConstChg_MeasStart = False
                End If
            End With
        End Using
    End Sub

    'Private Sub HScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar1.Scroll
    'Dim Kt1 As Long
    'Dim Ks As Integer

    'If e.OldValue = e.NewValue Then
    'Exit Sub
    'End If

    'Kt1 = SampleNo
    'FlgScroll = 1

    'Ks = Math.Round(HScrollBar1.Value / 10, 0) * 10

    'Hscroll_u(Ks)

    'HScrollBar2.Value = Ks
    'SampleNo = Kt1
    'End Sub

    Private Sub HScrollBar1_ValueChanged(sender As Object, e As EventArgs) Handles HScrollBar1.ValueChanged
        Dim Kt1 As Long
        Dim Ks As Integer

        If HScrollBar1.Value = HScrollBar2.Value Then
            Exit Sub
        End If

        Kt1 = SampleNo
        FlgScroll = 1

        Ks = HScrollBar1.Value
        Hscroll_u(Ks)

        HScrollBar2.Value = Ks
        SampleNo = Kt1
    End Sub

    'Private Sub HScrollBar2_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar2.Scroll
    'Dim Kt1 As Long
    'Dim Ks As Integer

    'If e.OldValue = e.NewValue Then
    'Exit Sub
    'End If

    'Kt1 = SampleNo
    'FlgScroll = 1

    'Ks = Math.Round(HScrollBar2.Value / 10, 0) * 10

    'Hscroll_u(Ks)

    'HScrollBar1.Value = Ks
    'SampleNo = Kt1
    'End Sub

    Private Sub HScrollBar2_ValueChanged(sender As Object, e As EventArgs) Handles HScrollBar2.ValueChanged
        Dim Kt1 As Long
        Dim Ks As Integer

        If HScrollBar1.Value = HScrollBar2.Value Then
            Exit Sub
        End If

        Kt1 = SampleNo
        FlgScroll = 1

        Ks = HScrollBar2.Value
        Hscroll_u(Ks)

        HScrollBar1.Value = Ks
        SampleNo = Kt1
    End Sub

    Private Sub Hscroll_u(Ks)
        Dim KshiftX As Integer

        KshiftX = Ks - HsbHold
        'Console.WriteLine("ScrollMax: " & HScrollBar1.Maximum)
        'Console.WriteLine("HsbHold: " & HsbHold)
        'Console.WriteLine("Ks: " & Ks)
        'Console.WriteLine("KshiftX: " & KshiftX)

        'DspPointx += KshiftX * 5
        DspPointx += KshiftX
        If DspPointx < 1 Then
            DspPointx = 1
        End If
        'Console.WriteLine("DspPointx: " & DspPointx)

        HsbHold = Ks

        SampleNo = MeasDataMax
        If SampleNo < FileDataMax Then
            SampleNo = FileDataMax
        End If
        'Console.WriteLine("SampleNo: " & SampleNo)
        'Console.WriteLine("MeasDataMax: " & MeasDataMax)
        'Console.WriteLine("FileDataMax: " & FileDataMax)

        If SampleNo <= lg_graph_max Then
            Exit Sub
        End If

        ClsGraph()
        'GraphInitPrf()
        'XScale()

        DrawCalcCurData_init()
        DrawCalcBakData_init()
        DrawCalcAvgData_init()

        If MeasDataMax > 0 Then
            KdData = 1
            PrfSaidDataAngle(1)
            PrfSaidDataRatio(1)
            PrfSaidDataTSI(1)
            PrfSaidDataVelo(1)
        End If

        If FileNo > 0 Then
            If FlgAvg = 2 Then
                KdData = 0
            Else
                KdData = 3
            End If
            PrfSaidDataAngle(0)
            PrfSaidDataRatio(0)
            PrfSaidDataTSI(0)
            PrfSaidDataVelo(0)
        End If

        If MeasDataMax > 0 Then
            KdData = 1
            If FlgAvg = 0 Then
                FlgLine = 2
            Else
                FlgLine = 1
            End If

            SampleNo = MeasDataMax
            GraphMove()
        End If

        If FileNo > 0 Then
            KdData = 3
            FlgLine = 1

            SampleNo = FileDataMax
            GraphMove()
        End If

        If FlgAvg > 0 Then
            KdData = 0
            FlgLine = 3

            SampleNo = FileDataMax
            If SampleNo > MeasDataMax And MeasDataMax > 1 Then
                SampleNo = MeasDataMax
            End If
            GraphMove()
        End If

    End Sub

    Private Sub ReDrawGraph()
        Kt2 = SampleNo

        'GraphInitPrf()
        PictureBox1.CreateGraphics.Clear(BackColor)
        PictureBox2.CreateGraphics.Clear(BackColor)
        PictureBox3.CreateGraphics.Clear(BackColor)
        PictureBox4.CreateGraphics.Clear(BackColor)

        DrawCalcCurData_init()
        DrawCalcBakData_init()
        DrawCalcAvgData_init()
        DrawTableData_init()

        KdData = 1
        'MeasDataMax = Kt1
        SampleNo = MeasDataMax

        PrfSaidDataAngle(1)
        PrfSaidDataRatio(1)
        PrfSaidDataTSI(1)
        PrfSaidDataVelo(1)

        If FileNo > 0 Then
            SampleNo = FileDataMax

            If FlgAvg = 2 Then
                KdData = 0
                If FlgProfile = 3 Then
                    If SampleNo > MeasDataMax And MeasDataMax > 1 Then
                        SampleNo = MeasDataMax
                    End If
                End If
            Else
                KdData = 3
            End If

            PrfSaidDataAngle(0)
            PrfSaidDataRatio(0)
            PrfSaidDataTSI(0)
            PrfSaidDataVelo(0)
        End If

        If MeasDataMax = 0 And FileDataMax = 0 Then
            Exit Sub
        End If

        If FlgProfile = 3 Then
            GoTo Rdg1
        Else
            GoTo Rdg5
        End If

Rdg1:
        Kt1 = MeasDataMax

        If Kt1 < DspPointx Then
            Kt1 = FileDataMax
        End If
        If Kt1 < DspPointx Then
            GoTo Rdg2
        End If

        If Kt1 - DspPointx > lg_graph_max Then
            Ks = lg_graph_max - 1
        Else
            Ks = Kt1 - DspPointx - 1
        End If

        If MeasDataMax > 0 Then
            If FlgAvg <> 0 Then
                FlgLine = 1
            Else
                FlgLine = 2
            End If
            DrawMeasGraph(Ks)
        End If

Rdg2:
        Kt1 = FileDataMax

        If Kt1 < DspPointx Then
            GoTo Rdg3
        End If

        If Kt1 - DspPointx > lg_graph_max Then
            Ks = lg_graph_max - 1
        Else
            Ks = Kt1 - DspPointx - 1
        End If

        If FileNo > 0 Then
            FlgLine = 1
            DrawFileGraph(Ks)
        End If

Rdg3:
        Kt1 = FileDataMax

        If Kt1 > MeasDataMax And MeasDataMax > 1 Then
            Kt1 = MeasDataMax
        End If

        If Kt1 < DspPointx Then
            GoTo Rdg4
        End If

        If Kt1 - DspPointx > lg_graph_max Then
            Ks = lg_graph_max - 1
        Else
            Ks = Kt1 - DspPointx - 1
        End If

        If FlgAvg = 2 Then
            FlgLine = 3
            DrawAvgGraph(Ks)
        End If

Rdg4:
        SampleNo = Kt2
        Exit Sub

Rdg5:
        DspPointx = 1

        If MeasDataMax = 0 Then
            GoTo Rdg6
        End If

        Ks = MeasDataMax - 1
        Points = MeasDataMax

        If FlgAvg <> 0 Then
            FlgLine = 1
        Else
            FlgLine = 2
        End If

        DrawMeasGraph(Ks)

Rdg6:
        If FileDataMax = 0 Then
            GoTo Rdg7
        End If

        Ks = FileDataMax - 1
        Points = FileDataMax
        FlgLine = 1
        DrawFileGraph(Ks)

Rdg7:
        If FlgAvg <> 2 Then
            GoTo Rdg8
        End If

        Kt1 = FileDataMax

        If Kt1 > MeasDataMax And MeasDataMax > 1 Then
            Kt1 = MeasDataMax
        End If

        Ks = Kt1 - 1
        Points = Kt1
        FlgLine = 3
        DrawAvgGraph(Ks)

Rdg8:
        SampleNo = Kt2

    End Sub

    Private Sub DrawAvgGraph(Ks)
        Dim Kt1 As Long

        Kt1 = SampleNo

        KdData = 0
        PosX1(KdData) = 0
        PosX2(KdData) = 0

        For SampleNo = DspPointx To DspPointx + Ks
            PrfGraphAngleRatio()
            PrfGraphVelocityTSI()
        Next

        SampleNo = Kt1
    End Sub

    Private Sub DrawMeasGraph(Ks)
        Dim Kt1 As Long

        Kt1 = SampleNo

        KdData = 1
        PosX1(KdData) = 0
        PosX2(KdData) = 0

        For SampleNo = DspPointx To DspPointx + Ks
            PrfGraphAngleRatio()
            PrfGraphVelocityTSI()
        Next

        SampleNo = Kt1
    End Sub

    Private Sub DrawFileGraph(Ks)
        Dim Kt1 As Long
        Dim Kt2 As Integer

        Kt1 = SampleNo
        Kt2 = FileNo

        KdData = 3
        For FileNo = 1 To Kt2
            MakeDisplayData()
            PosX1(KdData) = 0
            PosX2(KdData) = 0
            For SampleNo = DspPointx To DspPointx + Ks
                PrfGraphAngleRatio()
                PrfGraphVelocityTSI()
            Next
        Next

        SampleNo = Kt1
        FileNo = Kt2
    End Sub

    Private Sub GraphShift()
        Dim KdData_bak As Integer

        Dim KTp1 As Double
        Dim KTp2 As Double
        Dim KTp3 As Double
        Dim KTp4 As Double
        Dim KTp5 As Double
        Dim KTp6 As Double
        Dim KTp7 As Double
        Dim KTp8 As Double
        Dim KTp9 As Double
        Dim KTp10 As Double
        Dim Xshft As Integer
        Dim Kt1 As Long

        KdData_bak = KdData
        Kt1 = SampleNo

        'GraphInitPrf()
        ClsGraph()

        KdData = KdData_bak

        Xshft = lg_def_shiftxnum
        'Xshft = 15

        KTp1 = DataInt1TSI(KdData)
        KTp2 = DataInt2TSI(KdData)
        KTp3 = DataInt1Angle(KdData)
        KTp4 = DataInt2Angle(KdData)
        KTp5 = DataInt1VelocityM(KdData)
        KTp6 = DataInt2VelocityM(KdData)
        KTp7 = DataInt1VelocityP(KdData)
        KTp8 = DataInt2VelocityP(KdData)
        KTp9 = DataInt1RatioM(KdData)
        KTp10 = DataInt1RatioP(KdData)

        DspPointx = Kt1 - (lg_graph_max - Xshft) + 1
        'DspPointx = Kt1 - (lg_graph_max - Xshft)
        'DspPointx = Kt1 - (25 - Xshft) + 1
        FlgLine = 2
        KdData = 1
        GraphMove()

        SampleNo = Kt1

        DataInt1TSI(KdData) = KTp1
        DataInt2TSI(KdData) = KTp2
        DataInt1Angle(KdData) = KTp3
        DataInt2Angle(KdData) = KTp4
        DataInt1VelocityM(KdData) = KTp5
        DataInt2VelocityM(KdData) = KTp6
        DataInt1VelocityP(KdData) = KTp7
        DataInt2VelocityP(KdData) = KTp8
        DataInt1RatioM(KdData) = KTp9
        DataInt1RatioP(KdData) = KTp10

    End Sub

    Private Sub GraphMove()
        Dim Ks1 As Long
        Dim Kt1 As Long

        Kt1 = SampleNo

        If Kt1 - DspPointx > lg_graph_max Then
            Ks1 = DspPointx + (lg_graph_max - 1)
        Else
            '            Ks1 = Kt1
            Ks1 = Kt1 - 1
        End If

        'If Kt1 = DspPointx > 25 Then
        'Ks1 = DspPointx + 24
        'Else
        '   Ks1 = Kt1
        'End If

        PosX1(KdData) = 0
        PosX2(KdData) = 0

        For SampleNo = DspPointx + 1 To Ks1 + 1
            If SampleNo > DspPointx + lg_graph_max - 1 Then
            Else
                PrfGraphAngleRatio()
                PrfGraphVelocityTSI()
            End If

        Next

        SampleNo = Kt1
    End Sub

    Private Sub PDAngleRatio_nom_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PDAngleRatio_nom.PrintPage
        e.Graphics.Clear(Color.White)
        prf_prn_linepath1.Clear()

        Const gyou_height25 = 25
        Const cell_height25 = 25
        Const cell_padding_left = 5
        Const datacell_width = 80
        Const machno_width = 120

        Dim datahyou_width As Single = datacell_width * 3
        Dim stringSize As SizeF
        Dim string_tmp As String
        Dim title_height As Single
        Dim pen_black_1 As New Pen(Color.Black, 1)
        Dim fnt_14 As New Font("MS UI Gothic", 14)
        Dim fnt_10 As New Font("MS UI Gothic", 10)
        Dim fnt_9 As New Font("MS UI Gothic", 9)

        Dim printbc_brush As Brush = New SolidBrush(frm_PrfForm_bc)
        Dim print_curdata_brush As Brush = New SolidBrush(frm_PrfCurData_color)
        Dim print_olddata_brush As Brush = New SolidBrush(frm_PrfOldData_color)
        Dim print_avgdata_brush As Brush = New SolidBrush(frm_PrfAvgData_color)
        Dim printfc_brush As Brush = New SolidBrush(frm_PrfForm_fc)

        Dim paper_width As Integer = e.MarginBounds.Width
        Dim paper_height As Integer = e.MarginBounds.Height

        '用紙の色（印刷範囲全体）
        If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
            e.Graphics.FillRectangle(printbc_brush,
                                     -Prn_left_margin,
                                     -Prn_top_margin,
                                     paper_width + Prn_left_margin + Prn_right_margin * 2,
                                     paper_height + Prn_top_margin + Prn_btm_margin * 2)
        End If

        string_tmp = My.Application.Info.ProductName & " " & LblPrfTitle.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_14)
        title_height = stringSize.Height

        e.Graphics.DrawString(string_tmp, fnt_14, printfc_brush, 0, 0)

        Dim MeasDataNum_cur As Integer = Val(TxtMeasNumCur.Text)
        If MeasDataNum_cur > 0 Then
            string_tmp = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - stringSize.Width, 0)
        End If

        '測定仕様枠
        Dim prfspec_hyoutop As Single = title_height + gyou_height25
        Dim path As New GraphicsPath
        path.StartFigure()
        path.AddLine(0, prfspec_hyoutop, paper_width, prfspec_hyoutop)
        For i = 1 To 2
            path.StartFigure()
            path.AddLine(0, prfspec_hyoutop + (cell_height25 * i),
                         paper_width, prfspec_hyoutop + (cell_height25 * i))
        Next
        path.StartFigure()
        path.AddLine(0, prfspec_hyoutop,
                     0, prfspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(machno_width, prfspec_hyoutop,
                     machno_width, prfspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(machno_width + 150, prfspec_hyoutop,
                     machno_width + 150, prfspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(paper_width - (100 + 100 + 100), prfspec_hyoutop,
                     paper_width - (100 + 100 + 100), prfspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(paper_width - 100 - 100, prfspec_hyoutop,
                     paper_width - 100 - 100, prfspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(paper_width - 100, prfspec_hyoutop,
                     paper_width - 100, prfspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(paper_width, prfspec_hyoutop,
                     paper_width, prfspec_hyoutop + (cell_height25 * 2))

        '測定仕様　タイトル
        string_tmp = StrMachNo
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              machno_width + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrSampleName
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              machno_width + 150 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMark
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - (100 + 100 + 100) + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasNumber
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - 100 - 100 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasLot
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - 100 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasSpec
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_padding_left,
                              title_height + gyou_height25 + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)

        '測定仕様　データ
        'マシーンNo. cur
        string_tmp = TxtMachNoCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              machno_width + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        'サンプル名 cur
        string_tmp = TxtSmplNamCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              machno_width + 150 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        'マーク cur
        string_tmp = TxtMarkCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              paper_width - (100 + 100 + 100) + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        '測定回数 cur
        string_tmp = TxtMeasNumCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              paper_width - 100 - 100 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        '測定ロット数 cur
        string_tmp = TxtMeasLotCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              paper_width - 100 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)

        '------------------------
        'angle
        Dim angle_hyou_top As Single = prfspec_hyoutop + (cell_height25 * 2) + gyou_height25
        For i = 0 To 5
            path.StartFigure()
            path.AddLine(0, angle_hyou_top + (cell_height25 * i),
                         datahyou_width, angle_hyou_top + (cell_height25 * i))
        Next
        path.StartFigure()
        path.AddLine(0, angle_hyou_top,
                     0, angle_hyou_top + cell_height25 * 5)
        For i = 1 To 2
            path.StartFigure()
            path.AddLine(datacell_width * i, angle_hyou_top + cell_height25,
                         datacell_width * i, angle_hyou_top + cell_height25 * 5)
        Next
        path.StartFigure()
        path.AddLine(datahyou_width, angle_hyou_top,
                     datahyou_width, angle_hyou_top + cell_height25 * 5)

        string_tmp = StrOrientAng
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datahyou_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Peak"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 1 + datacell_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Deep"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 2 + datacell_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Max."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 0 + datacell_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Ave."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 0 + datacell_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Min."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 0 + datacell_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)

        For anglehyoucol = 1 To 2
            Select Case anglehyoucol
                Case 1 : string_tmp = LblAnglePkMax_nom.Text
                Case 2 : string_tmp = LblAngleDpMax_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  datacell_width * anglehyoucol + datacell_width / 2 - stringSize.Width / 2,
                                  angle_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        Next

        For anglehyoucol = 1 To 2
            Select Case anglehyoucol
                Case 1 : string_tmp = LblAnglePkAvg_nom.Text
                Case 2 : string_tmp = LblAngleDpAvg_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  datacell_width * anglehyoucol + datacell_width / 2 - stringSize.Width / 2,
                                  angle_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        Next

        For anglehyoucol = 1 To 2
            Select Case anglehyoucol
                Case 1 : string_tmp = LblAnglePkMin_nom.Text
                Case 2 : string_tmp = LblAngleDpMin_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  datacell_width * anglehyoucol + datacell_width / 2 - stringSize.Width / 2,
                                  angle_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
        Next

        'グラフを画像として貼り付ける
        Dim bmp1 As New Bitmap(PictureBox1.Width, PictureBox1.Height)
        PictureBox1.DrawToBitmap(bmp1, New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height))
        bmp1.MakeTransparent(BackColor)
        e.Graphics.InterpolationMode = InterpolationMode.High

        Dim bmp_resize1 As Bitmap = New Bitmap(bmp1, bmp1.Width * 1, bmp1.Height * 1)
        e.Graphics.DrawImage(bmp_resize1,
                             0, angle_hyou_top + (cell_height25 * 5) + gyou_height25,
                             bmp1.Width, bmp1.Height)
        path.StartFigure()
        path.AddRectangle(New Rectangle(0, angle_hyou_top + (cell_height25 * 5) + gyou_height25,
                                        bmp1.Width, bmp1.Height))

        '------------------------
        'ratio
        Dim ratio_hyou_top As Single = angle_hyou_top + (cell_height25 * 5) + gyou_height25 + bmp_resize1.Height + gyou_height25
        For i = 0 To 5
            path.StartFigure()
            path.AddLine(0, ratio_hyou_top + (cell_height25 * i),
                         datahyou_width, ratio_hyou_top + (cell_height25 * i))
        Next
        path.StartFigure()
        path.AddLine(0, ratio_hyou_top,
                     0, ratio_hyou_top + cell_height25 * 5)
        For i = 1 To 2
            path.StartFigure()
            path.AddLine(datacell_width * i, ratio_hyou_top + cell_height25,
                         datacell_width * i, ratio_hyou_top + cell_height25 * 5)
        Next
        path.StartFigure()
        path.AddLine(datahyou_width, ratio_hyou_top,
                     datahyou_width, ratio_hyou_top + cell_height25 * 5)

        string_tmp = StrOrientRat
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datahyou_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Peak/Deep"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 1 + datacell_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "MD/CD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 2 + datacell_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Max."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 0 + datacell_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Ave."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 0 + datacell_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Min."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 0 + datacell_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)

        For ratiohyoucol = 1 To 2
            Select Case ratiohyoucol
                Case 1 : string_tmp = LblRatioPkDpMax_nom.Text
                Case 2 : string_tmp = LblRatioMDCDMax_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  datacell_width * ratiohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                  ratio_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        Next

        For ratiohyoucol = 1 To 2
            Select Case ratiohyoucol
                Case 1 : string_tmp = LblRatioPkDpAvg_nom.Text
                Case 2 : string_tmp = LblRatioMDCDAvg_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  datacell_width * ratiohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                  ratio_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        Next

        For ratiohyoucol = 1 To 2
            Select Case ratiohyoucol
                Case 1 : string_tmp = LblRatioPkDpMin_nom.Text
                Case 2 : string_tmp = LblRatioMDCDMin_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  datacell_width * ratiohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                  ratio_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
        Next

        'グラフを画像として貼り付ける
        Dim bmp2 As New Bitmap(PictureBox2.Width, PictureBox2.Height)
        PictureBox2.DrawToBitmap(bmp2, New Rectangle(0, 0, PictureBox2.Width, PictureBox2.Height))
        bmp2.MakeTransparent(BackColor)
        e.Graphics.InterpolationMode = InterpolationMode.High

        Dim bmp_resize2 As Bitmap = New Bitmap(bmp2, PictureBox2.Width * 1, PictureBox2.Height * 1)
        e.Graphics.DrawImage(bmp_resize2,
                             0, ratio_hyou_top + (cell_height25 * 5) + gyou_height25,
                             bmp2.Width, bmp2.Height)
        path.StartFigure()
        path.AddRectangle(New Rectangle(0, ratio_hyou_top + (cell_height25 * 5) + gyou_height25,
                                        bmp2.Width, bmp2.Height))

        prf_prn_linepath1.Add(path)

        For Each path_tmp As GraphicsPath In prf_prn_linepath1
            e.Graphics.DrawPath(pen_black_1, path_tmp)
        Next

    End Sub

    Private Sub PDAngleRatio_adm_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PDAngleRatio_adm.PrintPage
        e.Graphics.Clear(Color.White)
        prf_prn_linepath1.Clear()

        Const gyou_height25 = 20
        Const cell_height25 = 25
        Const cell_padding_left = 5
        Const datacell_width = 80
        Const machno_width = 120

        Dim datahyou_width As Single = datacell_width * 5
        Dim stringSize As SizeF
        Dim string_tmp As String
        Dim title_height As Single
        Dim pen_black_1 As New Pen(Color.Black, 1)
        Dim fnt_14 As New Font("MS UI Gothic", 14)
        Dim fnt_10 As New Font("MS UI Gothic", 10)
        Dim fnt_9 As New Font("MS UI Gothic", 9)

        Dim printbc_brush As Brush = New SolidBrush(frm_PrfForm_bc)
        Dim print_curdata_brush As Brush = New SolidBrush(frm_PrfCurData_color)
        Dim print_olddata_brush As Brush = New SolidBrush(frm_PrfOldData_color)
        Dim print_avgdata_brush As Brush = New SolidBrush(frm_PrfAvgData_color)
        Dim printfc_brush As Brush = New SolidBrush(frm_PrfForm_fc)

        Dim paper_width As Integer = e.MarginBounds.Width
        Dim paper_height As Integer = e.MarginBounds.Height

        '用紙の色（印刷範囲全体）
        If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
            e.Graphics.FillRectangle(printbc_brush,
                                     -Prn_left_margin,
                                     -Prn_top_margin,
                                     paper_width + Prn_left_margin + Prn_right_margin * 2,
                                     paper_height + Prn_top_margin + Prn_btm_margin * 2)
        End If

        string_tmp = My.Application.Info.ProductName & " " & LblPrfTitle.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_14)
        title_height = stringSize.Height

        e.Graphics.DrawString(string_tmp, fnt_14, printfc_brush, 0, 0)

        '測定データの測定日時
        Dim MeasDataNum_cur As Integer = Val(TxtMeasNumCur.Text)
        If MeasDataNum_cur > 0 Then
            string_tmp = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - stringSize.Width, 0)
        End If

        '過去データの測定日時
        Dim MeasDataNo_bak As Integer = Val(TxtMeasNumBak.Text)
        If MeasDataNo_bak > 0 Then
            string_tmp = StrPastDataDate & DataDate_bak & StrMeasTime & DataTime_bak
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                  paper_width - stringSize.Width, stringSize.Height + 5)
        End If

        '測定仕様枠
        Dim prfspec_hyoutop As Single = title_height + gyou_height25
        Dim path As New GraphicsPath
        path.StartFigure()
        path.AddLine(0, prfspec_hyoutop,
                     paper_width, prfspec_hyoutop)
        For i = 1 To 3
            path.StartFigure()
            path.AddLine(0, prfspec_hyoutop + (cell_height25 * i),
                         paper_width, prfspec_hyoutop + (cell_height25 * i))
        Next
        path.StartFigure()
        path.AddLine(0, prfspec_hyoutop,
                     0, prfspec_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(machno_width, prfspec_hyoutop,
                     machno_width, prfspec_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(machno_width + 150, prfspec_hyoutop,
                     machno_width + 150, prfspec_hyoutop + (cell_height25 * 3))
        If FlgDBF = 1 Then
            path.StartFigure()
            path.AddLine(paper_width - (100 + 100 + 100), prfspec_hyoutop,
                         paper_width - (100 + 100 + 100), prfspec_hyoutop + (cell_height25 * 3))
        End If
        path.StartFigure()
        path.AddLine(paper_width - (100 + 100), prfspec_hyoutop,
                     paper_width - (100 + 100), prfspec_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(paper_width - 100, prfspec_hyoutop,
                     paper_width - 100, prfspec_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(paper_width, prfspec_hyoutop,
                     paper_width, prfspec_hyoutop + (cell_height25 * 3))

        '測定仕様　タイトル
        string_tmp = StrMachNo
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              machno_width + cell_padding_left,
                              prfspec_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrSampleName
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              machno_width + 150 + cell_padding_left,
                              prfspec_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
        If FlgDBF = 1 Then
            string_tmp = StrMark
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  paper_width - (100 + 100 + 100) + cell_padding_left,
                                  prfspec_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
        End If
        string_tmp = StrMeasNumber
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - (100 + 100) + cell_padding_left,
                              prfspec_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasLot
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - 100 + cell_padding_left,
                              prfspec_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasSpec
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrPastMeasSpec
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)

        '測定仕様　データ
        'マシーンNo. cur
        string_tmp = TxtMachNoCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              machno_width + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        'サンプル名 cur
        string_tmp = TxtSmplNamCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              machno_width + 150 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        If FlgDBF = 1 Then
            'マーク cur
            string_tmp = TxtMarkCur.Text
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - (100 + 100 + 100) + cell_padding_left,
                                  prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        End If
        '測定回数 cur
        string_tmp = TxtMeasNumCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              paper_width - 100 - 100 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        '測定ロット数 cur
        string_tmp = TxtMeasLotCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              paper_width - 100 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        'マシーンNo. bak
        string_tmp = TxtMachNoBak.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              machno_width + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
        'サンプル名 bak
        string_tmp = TxtSmplNamBak.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              machno_width + 150 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
        'マーク bak
        string_tmp = TxtMarkBak.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              paper_width - (100 + 100 + 100) + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
        '測定回数 bak
        string_tmp = TxtMeasNumBak.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              paper_width - 100 - 100 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
        '測定ロット数 bak
        string_tmp = TxtMeasLotBak.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              paper_width - 100 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)

        '----------------------------
        'angle
        Dim angle_hyou_top As Single = prfspec_hyoutop + (cell_height25 * 3) + gyou_height25
        For i = 0 To 6
            If i = 2 Then
                path.StartFigure()
                path.AddLine(datacell_width, angle_hyou_top + (cell_height25 * i),
                             datahyou_width, angle_hyou_top + (cell_height25 * i))
            Else
                path.StartFigure()
                path.AddLine(0, angle_hyou_top + (cell_height25 * i),
                             datahyou_width, angle_hyou_top + (cell_height25 * i))
            End If
        Next
        path.StartFigure()
        path.AddLine(0, angle_hyou_top,
                     0, angle_hyou_top + cell_height25 * 6)
        For i = 1 To 4
            If i Mod 2 = 0 Then
                path.StartFigure()
                path.AddLine(datacell_width * i, angle_hyou_top + cell_height25 * 2,
                             datacell_width * i, angle_hyou_top + cell_height25 * 6)
            Else
                path.StartFigure()
                path.AddLine(datacell_width * i, angle_hyou_top + cell_height25,
                             datacell_width * i, angle_hyou_top + cell_height25 * 6)
            End If
        Next
        path.StartFigure()
        path.AddLine(datahyou_width, angle_hyou_top,
                     datahyou_width, angle_hyou_top + cell_height25 * 6)

        string_tmp = StrOrientAng
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datahyou_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Peak"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Deep"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 4 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasData
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              datacell_width * 1 + datacell_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              datacell_width * 3 + datacell_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrPastData
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              datacell_width * 2 + datacell_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              datacell_width * 4 + datacell_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Max."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Ave."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Min."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width / 2 - stringSize.Width / 2,
                              angle_hyou_top + cell_height25 * 5 + cell_height25 / 2 - stringSize.Height / 2)

        For anglehyoucol = 1 To 4
            Select Case anglehyoucol
                Case 1 : string_tmp = LblAnglePkMaxCur_adm.Text
                Case 2 : string_tmp = LblAnglePkMaxBak_adm.Text
                Case 3 : string_tmp = LblAngleDpMaxCur_adm.Text
                Case 4 : string_tmp = LblAngleDpMaxBak_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            If anglehyoucol Mod 2 = 0 Then
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      datacell_width * anglehyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      angle_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
            Else
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      datacell_width * anglehyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      angle_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
            End If
        Next

        For anglehyoucol = 1 To 4
            Select Case anglehyoucol
                Case 1 : string_tmp = LblAnglePkAvgCur_adm.Text
                Case 2 : string_tmp = LblAnglePkAvgBak_adm.Text
                Case 3 : string_tmp = LblAngleDpAvgCur_adm.Text
                Case 4 : string_tmp = LblAngleDpAvgBak_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            If anglehyoucol Mod 2 = 0 Then
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      datacell_width * anglehyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      angle_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
            Else
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      datacell_width * anglehyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      angle_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
            End If
        Next

        For anglehyoucol = 1 To 4
            Select Case anglehyoucol
                Case 1 : string_tmp = LblAnglePkMinCur_adm.Text
                Case 2 : string_tmp = LblAnglePkMinBak_adm.Text
                Case 3 : string_tmp = LblAngleDpMinCur_adm.Text
                Case 4 : string_tmp = LblAngleDpMinBak_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            If anglehyoucol Mod 2 = 0 Then
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      datacell_width * anglehyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      angle_hyou_top + cell_height25 * 5 + cell_height25 / 2 - stringSize.Height / 2)
            Else
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      datacell_width * anglehyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      angle_hyou_top + cell_height25 * 5 + cell_height25 / 2 - stringSize.Height / 2)
            End If
        Next

        'グラフを画像として貼り付ける
        Dim bmp1 As New Bitmap(PictureBox1.Width, PictureBox1.Height)
        PictureBox1.DrawToBitmap(bmp1, New Rectangle(0, 0, PictureBox1.Width, PictureBox1.Height))
        bmp1.MakeTransparent(BackColor)
        e.Graphics.InterpolationMode = InterpolationMode.High

        Dim bmp_resize1 As Bitmap = New Bitmap(bmp1, bmp1.Width * 1, bmp1.Height * 1)
        e.Graphics.DrawImage(bmp_resize1,
                             0, angle_hyou_top + (cell_height25 * 6) + gyou_height25,
                             bmp1.Width, bmp1.Height)
        path.StartFigure()
        path.AddRectangle(New Rectangle(0, angle_hyou_top + (cell_height25 * 6) + gyou_height25,
                                        bmp1.Width, bmp1.Height))

        '----------------------------
        'ratio
        Dim ratio_hyou_top As Single = angle_hyou_top + (cell_height25 * 6) + gyou_height25 + bmp_resize1.Height + gyou_height25
        For i = 0 To 6
            If i = 2 Then
                path.StartFigure()
                path.AddLine(datacell_width, ratio_hyou_top + (cell_height25 * i),
                             datahyou_width, ratio_hyou_top + (cell_height25 * i))
            Else
                path.StartFigure()
                path.AddLine(0, ratio_hyou_top + (cell_height25 * i),
                             datahyou_width, ratio_hyou_top + (cell_height25 * i))
            End If
        Next
        path.StartFigure()
        path.AddLine(0, ratio_hyou_top,
                     0, ratio_hyou_top + cell_height25 * 6)
        For i = 1 To 4
            If i Mod 2 = 0 Then
                path.StartFigure()
                path.AddLine(datacell_width * i, ratio_hyou_top + cell_height25 * 2,
                             datacell_width * i, ratio_hyou_top + cell_height25 * 6)
            Else
                path.StartFigure()
                path.AddLine(datacell_width * i, ratio_hyou_top + cell_height25,
                             datacell_width * i, ratio_hyou_top + cell_height25 * 6)
            End If
        Next
        path.StartFigure()
        path.AddLine(datahyou_width, ratio_hyou_top,
                     datahyou_width, ratio_hyou_top + cell_height25 * 6)

        string_tmp = StrOrientRat
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datahyou_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Peak/Deep"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "MD/CD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 4 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasData
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              datacell_width * 1 + datacell_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              datacell_width * 3 + datacell_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrPastData
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              datacell_width * 2 + datacell_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              datacell_width * 4 + datacell_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Max."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Ave."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Min."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width / 2 - stringSize.Width / 2,
                              ratio_hyou_top + cell_height25 * 5 + cell_height25 / 2 - stringSize.Height / 2)

        For ratiohyoucol = 1 To 4
            Select Case ratiohyoucol
                Case 1 : string_tmp = LblRatioPkDpMaxCur_adm.Text
                Case 2 : string_tmp = LblRatioPkDpMaxBak_adm.Text
                Case 3 : string_tmp = LblRatioMDCDMaxCur_adm.Text
                Case 4 : string_tmp = LblRatioMDCDMaxBak_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            If ratiohyoucol Mod 2 = 0 Then
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      datacell_width * ratiohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      ratio_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
            Else
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      datacell_width * ratiohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      ratio_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
            End If
        Next

        For ratiohyoucol = 1 To 4
            Select Case ratiohyoucol
                Case 1 : string_tmp = LblRatioPkDpAvgCur_adm.Text
                Case 2 : string_tmp = LblRatioPkDpAvgBak_adm.Text
                Case 3 : string_tmp = LblRatioMDCDAvgCur_adm.Text
                Case 4 : string_tmp = LblRatioMDCDAvgBak_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            If ratiohyoucol Mod 2 = 0 Then
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      datacell_width * ratiohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      ratio_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
            Else
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      datacell_width * ratiohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      ratio_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
            End If
        Next

        For ratiohyoucol = 1 To 4
            Select Case ratiohyoucol
                Case 1 : string_tmp = LblRatioPkDpMinCur_adm.Text
                Case 2 : string_tmp = LblRatioPkDpMinBak_adm.Text
                Case 3 : string_tmp = LblRatioMDCDMinCur_adm.Text
                Case 4 : string_tmp = LblRatioMDCDMinBak_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            If ratiohyoucol Mod 2 = 0 Then
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      datacell_width * ratiohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      ratio_hyou_top + cell_height25 * 5 + cell_height25 / 2 - stringSize.Height / 2)
            Else
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      datacell_width * ratiohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      ratio_hyou_top + cell_height25 * 5 + cell_height25 / 2 - stringSize.Height / 2)
            End If
        Next

        'グラフを画像として貼り付ける
        Dim bmp2 As New Bitmap(PictureBox2.Width, PictureBox2.Height)
        PictureBox2.DrawToBitmap(bmp2, New Rectangle(0, 0, PictureBox2.Width, PictureBox2.Height))
        bmp2.MakeTransparent(BackColor)
        e.Graphics.InterpolationMode = InterpolationMode.High

        Dim bmp_resize2 As Bitmap = New Bitmap(bmp2, bmp2.Width * 1, bmp2.Height * 1)
        e.Graphics.DrawImage(bmp_resize2,
                             0, ratio_hyou_top + (cell_height25 * 6) + gyou_height25,
                             bmp2.Width, bmp2.Height)
        path.StartFigure()
        path.AddRectangle(New Rectangle(0, ratio_hyou_top + (cell_height25 * 6) + gyou_height25,
                                        bmp2.Width, bmp2.Height))

        prf_prn_linepath1.Add(path)

        For Each path_tmp As GraphicsPath In prf_prn_linepath1
            e.Graphics.DrawPath(pen_black_1, path_tmp)
        Next
    End Sub

    Private Sub PDVeloTSI_nom_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PDVeloTSI_nom.PrintPage
        e.Graphics.Clear(Color.White)
        prf_prn_linepath1.Clear()

        Const gyou_height25 = 25
        Const cell_height25 = 25
        Const cell_padding_left = 5
        Const datacell_width = 80

        Dim velohyou_width As Single = datacell_width * 5
        Dim tsihyou_width As Single = datacell_width * 3
        Dim stringSize As SizeF
        Dim string_tmp As String
        Dim title_height As Single
        Dim pen_black_1 As New Pen(Color.Black, 1)
        Dim fnt_14 As New Font("MS UI Gothic", 14)
        Dim fnt_10 As New Font("MS UI Gothic", 10)
        Dim fnt_9 As New Font("MS UI Gothic", 9)

        Dim printbc_brush As Brush = New SolidBrush(frm_PrfForm_bc)
        Dim print_curdata_brush As Brush = New SolidBrush(frm_PrfCurData_color)
        Dim print_olddata_brush As Brush = New SolidBrush(frm_PrfOldData_color)
        Dim print_avgdata_brush As Brush = New SolidBrush(frm_PrfAvgData_color)
        Dim printfc_brush As Brush = New SolidBrush(frm_PrfForm_fc)

        Dim paper_width As Integer = e.MarginBounds.Width
        Dim paper_height As Integer = e.MarginBounds.Height

        '用紙の色（印刷範囲全体）
        If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
            e.Graphics.FillRectangle(printbc_brush,
                                     -Prn_left_margin,
                                     -Prn_top_margin,
                                     paper_width + Prn_left_margin + Prn_right_margin * 2,
                                     paper_height + Prn_top_margin + Prn_btm_margin * 2)
        End If

        string_tmp = My.Application.Info.ProductName & " " & LblPrfTitle.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_14)
        title_height = stringSize.Height

        e.Graphics.DrawString(string_tmp, fnt_14, printfc_brush, 0, 0)

        Dim MeasDataNum_cur As Integer = Val(TxtMeasNumCur.Text)
        If MeasDataNum_cur > 0 Then
            string_tmp = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - stringSize.Width, 0)
        End If

        '測定仕様枠
        Dim prfspec_hyoutop As Single = title_height + gyou_height25
        Dim path As New GraphicsPath
        path.StartFigure()
        path.AddLine(0, prfspec_hyoutop,
                     paper_width, prfspec_hyoutop)
        For i = 1 To 2
            path.StartFigure()
            path.AddLine(0, prfspec_hyoutop + (cell_height25 * i),
                         paper_width, prfspec_hyoutop + (cell_height25 * i))
        Next
        path.StartFigure()
        path.AddLine(0, prfspec_hyoutop,
                     0, prfspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(120, prfspec_hyoutop,
                     120, prfspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(120 + 150, prfspec_hyoutop,
                     120 + 150, prfspec_hyoutop + (cell_height25 * 2))
        If FlgDBF = 1 Then
            path.StartFigure()
            path.AddLine(paper_width - (100 + 100 + 100), prfspec_hyoutop,
                         paper_width - (100 + 100 + 100), prfspec_hyoutop + (cell_height25 * 2))
        End If
        path.StartFigure()
        path.AddLine(paper_width - (100 + 100), prfspec_hyoutop,
                     paper_width - (100 + 100), prfspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(paper_width - 100, prfspec_hyoutop,
                     paper_width - 100, prfspec_hyoutop + (cell_height25 * 2))
        path.StartFigure()
        path.AddLine(paper_width, prfspec_hyoutop,
                     paper_width, prfspec_hyoutop + (cell_height25 * 2))

        '測定仕様　タイトル
        string_tmp = StrMachNo
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              120 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrSampleName
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              120 + 150 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        If FlgDBF = 1 Then
            string_tmp = StrMark
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  paper_width - (100 + 100 + 100) + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        End If
        string_tmp = StrMeasNumber
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - (100 + 100) + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasLot
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - 100 + cell_padding_left,
                              title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasSpec
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_padding_left,
                              title_height + gyou_height25 + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)

        '測定仕様　データ
        'マシーンNo. cur
        string_tmp = TxtMachNoCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              120 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        'サンプル名 cur
        string_tmp = TxtSmplNamCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              120 + 150 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        If FlgDBF = 1 Then
            'マーク cur
            string_tmp = TxtMarkCur.Text
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - (100 + 100 + 100) + cell_padding_left,
                                  prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        End If
        '測定回数 cur
        string_tmp = TxtMeasNumCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              paper_width - 100 - 100 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        '測定ロット数 cur
        string_tmp = TxtMeasLotCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              paper_width - 100 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)

        '------------------------
        'velocity
        Dim velo_hyou_top As Single = prfspec_hyoutop + (cell_height25 * 2) + gyou_height25
        For i = 0 To 5
            path.StartFigure()
            path.AddLine(0, velo_hyou_top + (cell_height25 * i),
                         velohyou_width, velo_hyou_top + (cell_height25 * i))
        Next
        path.StartFigure()
        path.AddLine(0, velo_hyou_top,
                     0, velo_hyou_top + cell_height25 * 5)
        For i = 1 To 4
            path.StartFigure()
            path.AddLine(datacell_width * i, velo_hyou_top + cell_height25,
                         datacell_width * i, velo_hyou_top + cell_height25 * 5)
        Next
        path.StartFigure()
        path.AddLine(velohyou_width, velo_hyou_top,
                     velohyou_width, velo_hyou_top + cell_height25 * 5)

        string_tmp = StrPropVelo_nounit
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              velohyou_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Peak"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 1 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Deep"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 2 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "MD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 3 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "CD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 4 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Max."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 0 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Ave."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 0 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Min."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 0 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)

        For velohyoucol = 1 To 4
            Select Case velohyoucol
                Case 1 : string_tmp = LblVeloPkMax_nom.Text
                Case 2 : string_tmp = LblVeloDpMax_nom.Text
                Case 3 : string_tmp = LblVeloMDMax_nom.Text
                Case 4 : string_tmp = LblVeloCDMax_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  datacell_width * velohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                  velo_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        Next

        For velohyoucol = 1 To 4
            Select Case velohyoucol
                Case 1 : string_tmp = LblVeloPkAvg_nom.Text
                Case 2 : string_tmp = LblVeloDpAvg_nom.Text
                Case 3 : string_tmp = LblVeloMDAvg_nom.Text
                Case 4 : string_tmp = LblVeloCDAvg_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  datacell_width * velohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                  velo_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        Next

        For velohyoucol = 1 To 4
            Select Case velohyoucol
                Case 1 : string_tmp = LblVeloPkMin_nom.Text
                Case 2 : string_tmp = LblVeloDpMin_nom.Text
                Case 3 : string_tmp = LblVeloMDMin_nom.Text
                Case 4 : string_tmp = LblVeloCDMin_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  datacell_width * velohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                  velo_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
        Next

        'グラフを画像として貼り付ける
        Dim bmp1 As New Bitmap(PictureBox3.Width, PictureBox3.Height)
        PictureBox3.DrawToBitmap(bmp1, New Rectangle(0, 0, PictureBox3.Width, PictureBox1.Height))
        bmp1.MakeTransparent(BackColor)
        e.Graphics.InterpolationMode = InterpolationMode.High

        Dim bmp_resize1 As Bitmap = New Bitmap(bmp1, bmp1.Width * 1, bmp1.Height * 1)
        e.Graphics.DrawImage(bmp_resize1,
                             0, velo_hyou_top + (cell_height25 * 5) + gyou_height25,
                             bmp1.Width, bmp1.Height)
        path.StartFigure()
        path.AddRectangle(New Rectangle(0, velo_hyou_top + (cell_height25 * 5) + gyou_height25,
                                        bmp1.Width, bmp1.Height))

        '------------------------
        'tsi
        Dim tsi_hyou_top As Single = velo_hyou_top + (cell_height25 * 5) + gyou_height25 + bmp_resize1.Height + gyou_height25
        For i = 0 To 5
            path.StartFigure()
            path.AddLine(0, tsi_hyou_top + (cell_height25 * i),
                         tsihyou_width, tsi_hyou_top + (cell_height25 * i))
        Next
        path.StartFigure()
        path.AddLine(0, tsi_hyou_top,
                     0, tsi_hyou_top + cell_height25 * 5)
        For i = 1 To 2
            path.StartFigure()
            path.AddLine(datacell_width * i, tsi_hyou_top + cell_height25,
                         datacell_width * i, tsi_hyou_top + cell_height25 * 5)
        Next
        path.StartFigure()
        path.AddLine(tsihyou_width, tsi_hyou_top,
                     tsihyou_width, tsi_hyou_top + cell_height25 * 5)

        string_tmp = "TSI(Km/S)^2"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              tsihyou_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "MD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 1 + datacell_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "CD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 2 + datacell_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Max."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 0 + datacell_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Ave."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 0 + datacell_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Min."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 0 + datacell_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)

        For tsihyoucol = 1 To 2
            Select Case tsihyoucol
                Case 1 : string_tmp = LblTSIMDMax_nom.Text
                Case 2 : string_tmp = LblTSICDMax_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  datacell_width * tsihyoucol + datacell_width / 2 - stringSize.Width / 2,
                                  tsi_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        Next

        For tsihyoucol = 1 To 2
            Select Case tsihyoucol
                Case 1 : string_tmp = LblTSIMDAvg_nom.Text
                Case 2 : string_tmp = LblTSICDAvg_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  datacell_width * tsihyoucol + datacell_width / 2 - stringSize.Width / 2,
                                  tsi_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        Next

        For tsihyoucol = 1 To 2
            Select Case tsihyoucol
                Case 1 : string_tmp = LblTSIMDMin_nom.Text
                Case 2 : string_tmp = LblTSICDMin_nom.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  datacell_width * tsihyoucol + datacell_width / 2 - stringSize.Width / 2,
                                  tsi_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
        Next

        'グラフを画像として貼り付ける
        Dim bmp2 As New Bitmap(PictureBox4.Width, PictureBox4.Height)
        PictureBox4.DrawToBitmap(bmp2, New Rectangle(0, 0, PictureBox4.Width, PictureBox4.Height))
        bmp2.MakeTransparent(BackColor)
        e.Graphics.InterpolationMode = InterpolationMode.High

        Dim bmp_resize2 As Bitmap = New Bitmap(bmp2, PictureBox4.Width * 1, PictureBox4.Height * 1)
        e.Graphics.DrawImage(bmp_resize2,
                             0, tsi_hyou_top + (cell_height25 * 5) + gyou_height25,
                             bmp2.Width, bmp2.Height)
        path.StartFigure()
        path.AddRectangle(New Rectangle(0, tsi_hyou_top + (cell_height25 * 5) + gyou_height25,
                                        bmp2.Width, bmp2.Height))

        prf_prn_linepath1.Add(path)

        For Each path_tmp As GraphicsPath In prf_prn_linepath1
            e.Graphics.DrawPath(pen_black_1, path_tmp)
        Next

        bmp1.Dispose()
        bmp2.Dispose()
        bmp_resize1.Dispose()
        bmp_resize2.Dispose()
        path.Dispose()
        pen_black_1.Dispose()
        fnt_10.Dispose()
        fnt_14.Dispose()
        fnt_9.Dispose()

    End Sub

    Private Sub PDVeloTSI_adm_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PDVeloTSI_adm.PrintPage
        e.Graphics.Clear(Color.White)
        prf_prn_linepath1.Clear()

        Const gyou_height25 = 20
        Const cell_height25 = 25
        Const cell_padding_left = 5
        Const datacell_width = 80

        Dim velohyou_width As Single = datacell_width * 9
        Dim tsihyou_width As Single = datacell_width * 5
        Dim stringSize As SizeF
        Dim string_tmp As String
        Dim title_height As Single
        Dim pen_black_1 As New Pen(Color.Black, 1)
        Dim fnt_14 As New Font("MS UI Gothic", 14)
        Dim fnt_10 As New Font("MS UI Gothic", 10)
        Dim fnt_9 As New Font("MS UI Gothic", 9)

        Dim printbc_brush As Brush = New SolidBrush(frm_PrfForm_bc)
        Dim print_curdata_brush As Brush = New SolidBrush(frm_PrfCurData_color)
        Dim print_olddata_brush As Brush = New SolidBrush(frm_PrfOldData_color)
        Dim print_avgdata_brush As Brush = New SolidBrush(frm_PrfAvgData_color)
        Dim printfc_brush As Brush = New SolidBrush(frm_PrfForm_fc)

        Dim paper_width As Integer = e.MarginBounds.Width
        Dim paper_height As Integer = e.MarginBounds.Height

        '用紙の色（印刷範囲全体）
        If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
            e.Graphics.FillRectangle(printbc_brush,
                                     -Prn_left_margin,
                                     -Prn_top_margin,
                                     paper_width + Prn_left_margin + Prn_right_margin * 2,
                                     paper_height + Prn_top_margin + Prn_btm_margin * 2)
        End If

        string_tmp = My.Application.Info.ProductName & " " & LblPrfTitle.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_14)
        title_height = stringSize.Height

        e.Graphics.DrawString(string_tmp, fnt_14, printfc_brush, 0, 0)

        '測定データの測定日時
        Dim MeasDataNum_cur As Integer = Val(TxtMeasNumCur.Text)
        If MeasDataNum_cur > 0 Then
            string_tmp = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - stringSize.Width, 0)
        End If

        '過去データの測定日時
        'Dim MeasDataNo_bak As Integer = Val(TxtMeasNumBak.Text)
        Dim MeasDataNo_bak As Integer = Val(TxtMeasNumBak.Text)
        If MeasDataNo_bak > 0 Then
            string_tmp = StrPastDataDate & DataDate_bak & StrMeasTime & DataTime_bak
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                  paper_width - stringSize.Width, stringSize.Height + 5)
        End If

        '測定仕様枠
        Dim prfspec_hyoutop As Single = Prn_top_margin + title_height + gyou_height25
        Dim path As New GraphicsPath
        path.StartFigure()
        path.AddLine(0, prfspec_hyoutop,
                     paper_width, prfspec_hyoutop)
        For i = 1 To 3
            path.StartFigure()
            path.AddLine(0, prfspec_hyoutop + (cell_height25 * i),
                         paper_width, prfspec_hyoutop + (cell_height25 * i))
        Next
        path.StartFigure()
        path.AddLine(0, prfspec_hyoutop,
                     0, prfspec_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(120, prfspec_hyoutop,
                     120, prfspec_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(120 + 150, prfspec_hyoutop,
                     120 + 150, prfspec_hyoutop + (cell_height25 * 3))
        If FlgDBF = 1 Then
            path.StartFigure()
            path.AddLine(paper_width - (100 + 100 + 100), prfspec_hyoutop,
                         paper_width - (100 + 100 + 100), prfspec_hyoutop + (cell_height25 * 3))
        End If
        path.StartFigure()
        path.AddLine(paper_width - (100 + 100), prfspec_hyoutop,
                     paper_width - (100 + 100), prfspec_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(paper_width - 100, prfspec_hyoutop,
                     paper_width - 100, prfspec_hyoutop + (cell_height25 * 3))
        path.StartFigure()
        path.AddLine(paper_width, prfspec_hyoutop,
                     paper_width, prfspec_hyoutop + (cell_height25 * 3))

        '測定仕様　タイトル
        string_tmp = StrMachNo
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              120 + cell_padding_left,
                              prfspec_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrSampleName
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              120 + 150 + cell_padding_left,
                              prfspec_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
        If FlgDBF = 1 Then
            string_tmp = StrMark
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  paper_width - (100 + 100 + 100) + cell_padding_left,
                                  prfspec_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
        End If
        string_tmp = StrMeasNumber
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - 100 - 100 + cell_padding_left,
                              prfspec_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasLot
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              paper_width - 100 + cell_padding_left,
                              prfspec_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasSpec
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrPastMeasSpec
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)

        '測定仕様　データ
        'マシーンNo. cur
        string_tmp = TxtMachNoCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              120 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        'サンプル名 cur
        string_tmp = TxtSmplNamCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              120 + 150 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        If FlgDBF = 1 Then
            'マーク cur
            string_tmp = TxtMarkCur.Text
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - (100 + 100 + 100) + cell_padding_left,
                                  prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
        End If
        '測定回数 cur
        string_tmp = TxtMeasNumCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              paper_width - 100 - 100 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
        '測定ロット数 cur
        string_tmp = TxtMeasLotCur.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              paper_width - 100 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)

        'マシーンNo. bak
        string_tmp = TxtMachNoBak.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              120 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
        'サンプル名 bak
        string_tmp = TxtSmplNamBak.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              120 + 150 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
        If FlgDBF = 1 Then
            'マーク bak
            string_tmp = TxtMarkBak.Text
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                  paper_width - (100 + 100 + 100) + cell_padding_left,
                                  prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
        End If
        '測定回数 bak
        'string_tmp = TxtMeasNumBak.Text
        string_tmp = TxtMeasNumBak.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              paper_width - 100 - 100 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
        '測定ロット数 bak
        string_tmp = TxtMeasLotBak.Text
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              paper_width - 100 + cell_padding_left,
                              prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)

        '----------------------------
        'velocity
        Dim velo_hyou_top As Single = prfspec_hyoutop + (cell_height25 * 3) + gyou_height25
        For i = 0 To 6
            If i = 2 Then
                path.StartFigure()
                path.AddLine(datacell_width, velo_hyou_top + (cell_height25 * i),
                             velohyou_width, velo_hyou_top + (cell_height25 * i))
            Else
                path.StartFigure()
                path.AddLine(0, velo_hyou_top + (cell_height25 * i),
                             velohyou_width, velo_hyou_top + (cell_height25 * i))
            End If
        Next
        path.StartFigure()
        path.AddLine(0, velo_hyou_top,
                     0, velo_hyou_top + cell_height25 * 6)
        For i = 1 To 8
            If i Mod 2 = 0 Then
                path.StartFigure()
                path.AddLine(datacell_width * i, velo_hyou_top + cell_height25 * 2,
                             datacell_width * i, velo_hyou_top + cell_height25 * 6)
            Else
                path.StartFigure()
                path.AddLine(datacell_width * i, velo_hyou_top + cell_height25,
                             datacell_width * i, velo_hyou_top + cell_height25 * 6)
            End If
        Next
        path.StartFigure()
        path.AddLine(velohyou_width, velo_hyou_top,
                     velohyou_width, velo_hyou_top + cell_height25 * 6)

        string_tmp = StrPropVelo_nounit
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              velohyou_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Peak"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Deep"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 4 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "MD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 6 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "CD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 8 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasData
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              datacell_width * 1 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              datacell_width * 3 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              datacell_width * 5 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              datacell_width * 7 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrPastData
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              datacell_width * 2 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              datacell_width * 4 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              datacell_width * 6 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              datacell_width * 8 + datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Max."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Ave."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Min."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width / 2 - stringSize.Width / 2,
                              velo_hyou_top + cell_height25 * 5 + cell_height25 / 2 - stringSize.Height / 2)

        For velohyoucol = 1 To 8
            Select Case velohyoucol
                Case 1 : string_tmp = LblVeloPkMaxCur_adm.Text
                Case 2 : string_tmp = LblVeloPkMaxBak_adm.Text
                Case 3 : string_tmp = LblVeloDpMaxCur_adm.Text
                Case 4 : string_tmp = LblVeloDpMaxBak_adm.Text
                Case 5 : string_tmp = LblVeloMDMaxCur_adm.Text
                Case 6 : string_tmp = LblVeloMDMaxBak_adm.Text
                Case 7 : string_tmp = LblVeloCDMaxCur_adm.Text
                Case 8 : string_tmp = LblVeloCDMaxBak_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            If velohyoucol Mod 2 = 0 Then
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      datacell_width * velohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      velo_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
            Else
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      datacell_width * velohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      velo_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
            End If
        Next

        For velohyoucol = 1 To 8
            Select Case velohyoucol
                Case 1 : string_tmp = LblVeloPkAvgCur_adm.Text
                Case 2 : string_tmp = LblVeloPkAvgBak_adm.Text
                Case 3 : string_tmp = LblVeloDpAvgCur_adm.Text
                Case 4 : string_tmp = LblVeloDpAvgBak_adm.Text
                Case 5 : string_tmp = LblVeloMDAvgCur_adm.Text
                Case 6 : string_tmp = LblVeloMDAvgBak_adm.Text
                Case 7 : string_tmp = LblVeloCDAvgCur_adm.Text
                Case 8 : string_tmp = LblVeloCDAvgBak_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            If velohyoucol Mod 2 = 0 Then
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      datacell_width * velohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      velo_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
            Else
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      datacell_width * velohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      velo_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
            End If
        Next

        For velohyoucol = 1 To 8
            Select Case velohyoucol
                Case 1 : string_tmp = LblVeloPkMinCur_adm.Text
                Case 2 : string_tmp = LblVeloPkMinBak_adm.Text
                Case 3 : string_tmp = LblVeloDpMinCur_adm.Text
                Case 4 : string_tmp = LblVeloDpMinBak_adm.Text
                Case 5 : string_tmp = LblVeloMDMinCur_adm.Text
                Case 6 : string_tmp = LblVeloMDMinBak_adm.Text
                Case 7 : string_tmp = LblVeloCDMinCur_adm.Text
                Case 8 : string_tmp = LblVeloCDMinBak_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            If velohyoucol Mod 2 = 0 Then
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      datacell_width * velohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      velo_hyou_top + cell_height25 * 5 + cell_height25 / 2 - stringSize.Height / 2)
            Else
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      datacell_width * velohyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      velo_hyou_top + cell_height25 * 5 + cell_height25 / 2 - stringSize.Height / 2)
            End If
        Next

        'グラフを画像として貼り付ける
        Dim bmp1 As New Bitmap(PictureBox3.Width, PictureBox3.Height)
        PictureBox3.DrawToBitmap(bmp1, New Rectangle(0, 0, PictureBox3.Width, PictureBox3.Height))
        bmp1.MakeTransparent(BackColor)
        e.Graphics.InterpolationMode = InterpolationMode.High

        Dim bmp_resize1 As Bitmap = New Bitmap(bmp1, bmp1.Width * 1, bmp1.Height * 1)
        e.Graphics.DrawImage(bmp_resize1,
                             0, velo_hyou_top + (cell_height25 * 6) + gyou_height25,
                             bmp1.Width, bmp1.Height)
        path.StartFigure()
        path.AddRectangle(New Rectangle(0, velo_hyou_top + (cell_height25 * 6) + gyou_height25,
                                        bmp1.Width, bmp1.Height))

        '----------------------------
        'tsi
        Dim tsi_hyou_top As Single = velo_hyou_top + (cell_height25 * 6) + gyou_height25 + bmp_resize1.Height + gyou_height25
        For i = 0 To 6
            If i = 2 Then
                path.StartFigure()
                path.AddLine(datacell_width, tsi_hyou_top + (cell_height25 * i),
                             tsihyou_width, tsi_hyou_top + (cell_height25 * i))
            Else
                path.StartFigure()
                path.AddLine(0, tsi_hyou_top + (cell_height25 * i),
                             tsihyou_width, tsi_hyou_top + (cell_height25 * i))
            End If
        Next
        path.StartFigure()
        path.AddLine(0, tsi_hyou_top,
                     0, tsi_hyou_top + cell_height25 * 6)
        For i = 1 To 4
            If i Mod 2 = 0 Then
                path.StartFigure()
                path.AddLine(datacell_width * i, tsi_hyou_top + cell_height25 * 2,
                             datacell_width * i, tsi_hyou_top + cell_height25 * 6)
            Else
                path.StartFigure()
                path.AddLine(datacell_width * i, tsi_hyou_top + cell_height25,
                             datacell_width * i, tsi_hyou_top + cell_height25 * 6)
            End If
        Next
        path.StartFigure()
        path.AddLine(tsihyou_width, tsi_hyou_top, tsihyou_width, tsi_hyou_top + cell_height25 * 6)

        string_tmp = "TSI(Km/S)^2"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              tsihyou_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "MD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "CD"
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width * 4 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrMeasData
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              datacell_width * 1 + datacell_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                              datacell_width * 3 + datacell_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = StrPastData
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              datacell_width * 2 + datacell_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                              datacell_width * 4 + datacell_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Max."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Ave."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
        string_tmp = "Min."
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
        e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                              datacell_width / 2 - stringSize.Width / 2,
                              tsi_hyou_top + cell_height25 * 5 + cell_height25 / 2 - stringSize.Height / 2)

        For tsihyoucol = 1 To 4
            Select Case tsihyoucol
                Case 1 : string_tmp = LblRatioPkDpMaxCur_adm.Text
                Case 2 : string_tmp = LblRatioPkDpMaxBak_adm.Text
                Case 3 : string_tmp = LblRatioMDCDMaxCur_adm.Text
                Case 4 : string_tmp = LblRatioMDCDMaxBak_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            If tsihyoucol Mod 2 = 0 Then
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      datacell_width * tsihyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      tsi_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
            Else
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      datacell_width * tsihyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      tsi_hyou_top + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
            End If
        Next

        For tsihyoucol = 1 To 4
            Select Case tsihyoucol
                Case 1 : string_tmp = LblRatioPkDpAvgCur_adm.Text
                Case 2 : string_tmp = LblRatioPkDpAvgBak_adm.Text
                Case 3 : string_tmp = LblRatioMDCDAvgCur_adm.Text
                Case 4 : string_tmp = LblRatioMDCDAvgBak_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            If tsihyoucol Mod 2 = 0 Then
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      datacell_width * tsihyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      tsi_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
            Else
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      datacell_width * tsihyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      tsi_hyou_top + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
            End If
        Next

        For tsihyoucol = 1 To 4
            Select Case tsihyoucol
                Case 1 : string_tmp = LblRatioPkDpMinCur_adm.Text
                Case 2 : string_tmp = LblRatioPkDpMinBak_adm.Text
                Case 3 : string_tmp = LblRatioMDCDMinCur_adm.Text
                Case 4 : string_tmp = LblRatioMDCDMinBak_adm.Text
            End Select
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            If tsihyoucol Mod 2 = 0 Then
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      datacell_width * tsihyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      tsi_hyou_top + cell_height25 * 5 + cell_height25 / 2 - stringSize.Height / 2)
            Else
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      datacell_width * tsihyoucol + datacell_width / 2 - stringSize.Width / 2,
                                      tsi_hyou_top + cell_height25 * 5 + cell_height25 / 2 - stringSize.Height / 2)
            End If
        Next

        'グラフを画像として貼り付ける
        Dim bmp2 As New Bitmap(PictureBox4.Width, PictureBox4.Height)
        PictureBox4.DrawToBitmap(bmp2, New Rectangle(0, 0, PictureBox4.Width, PictureBox4.Height))
        bmp2.MakeTransparent(BackColor)
        e.Graphics.InterpolationMode = InterpolationMode.High

        Dim bmp_resize2 As Bitmap = New Bitmap(bmp2, bmp2.Width * 1, bmp2.Height * 1)
        e.Graphics.DrawImage(bmp_resize2,
                             0, tsi_hyou_top + (cell_height25 * 6) + gyou_height25,
                             bmp2.Width, bmp2.Height)
        path.StartFigure()
        path.AddRectangle(New Rectangle(0, tsi_hyou_top + (cell_height25 * 6) + gyou_height25,
                                        bmp2.Width, bmp2.Height))

        prf_prn_linepath1.Add(path)

        For Each path_tmp As GraphicsPath In prf_prn_linepath1
            e.Graphics.DrawPath(pen_black_1, path_tmp)
        Next

        bmp1.Dispose()
        bmp2.Dispose()
        bmp_resize1.Dispose()
        bmp_resize2.Dispose()
        path.Dispose()
        pen_black_1.Dispose()
        fnt_10.Dispose()
        fnt_14.Dispose()
        fnt_9.Dispose()

    End Sub

    Private Sub PDMeasData_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PDMeasData.PrintPage
        PrintData(PDMeasDataEnum, e)
    End Sub

    Private Sub PDOldData_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PDOldData.PrintPage
        PrintData(PDOldDataEnum, e)
    End Sub

    Private Sub PDAvgData_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PDAvgData.PrintPage
        PrintData(PDAvgDataEnum, e)
    End Sub

    Private Sub PrintData(ByVal select_data As Integer, ByVal e As PrintPageEventArgs)
        e.Graphics.Clear(Color.White)
        prf_prn_linepath1.Clear()
        prf_prn_linepath2.Clear()

        Const gyou_height25 = 25
        Const cell_height25 = 25
        Const dv_col1_width = 50
        Const cell_padding_left = 5

        Dim printbc_brush As Brush = New SolidBrush(frm_PrfForm_bc)
        Dim printgraphbc_brush As Brush = New SolidBrush(frm_PrfGraph_bc)
        Dim print_curdata_brush As Brush = New SolidBrush(frm_PrfCurData_color)
        Dim print_olddata_brush As Brush = New SolidBrush(frm_PrfOldData_color)
        Dim print_avgdata_brush As Brush = New SolidBrush(frm_PrfAvgData_color)
        Dim printfc_brush As Brush = New SolidBrush(frm_PrfForm_fc)

        Dim paper_width As Integer = e.MarginBounds.Width
        Dim paper_height As Integer = e.MarginBounds.Height

        Dim dv_datacol_width As Single = (paper_width - dv_col1_width) / 10

        Dim stringSize As SizeF
        Dim string_tmp As String
        Dim title_height As Single
        Dim title_width As Single
        Dim sub_title As String
        Dim data_sta_row2 As Single
        Dim prfspec_hyoutop As Single
        Dim prfdata_hyoutop As Single
        Dim path As New GraphicsPath
        Dim path2 As New GraphicsPath
        Dim last_row As Single = paper_height - cell_height25
        Dim last_row2 As Single = last_row + (cell_height25 * 3)

        Dim pen_black_1 As New Pen(Color.Black, 1)
        Dim pen_black_2 As New Pen(Color.Black, 1)  '2は太く見えない 一旦1にする
        Dim fnt_14 As New Font("MS UI Gothic", 14)
        Dim fnt_10 As New Font("MS UI Gothic", 10)

        '測定データと過去データのサンプル数は同じである事
        If SampleNo = 0 Then
            If FileDataMax = 0 Then
                '測定後、ファイル読み込み後のみ印刷可能なので
                'この状態にはならないハズ
                Exit Sub
            Else
                targetPrnRow = FileDataMax
            End If
        Else
            targetPrnRow = SampleNo
        End If

        Select Case select_data
            Case PDMeasDataEnum
                sub_title = StrMeasData
            Case PDOldDataEnum
                sub_title = StrPastData
            Case PDAvgDataEnum
                sub_title = StrAvgValue
            Case Else
                sub_title = StrMeasData
        End Select

        string_tmp = My.Application.Info.ProductName & " " & LblPrfTitle.Text & " " & sub_title
        stringSize = e.Graphics.MeasureString(string_tmp, fnt_14)
        title_height = stringSize.Height
        title_width = stringSize.Width
        prfspec_hyoutop = title_height + gyou_height25
        If FlgAdmin = 0 Then
            prfdata_hyoutop = prfspec_hyoutop + (cell_height25 * 2) + gyou_height25
        Else
            prfdata_hyoutop = prfspec_hyoutop + (cell_height25 * 3) + gyou_height25
        End If
        Dim cur_row As Single = prfdata_hyoutop + gyou_height25 + (cell_height25 * 5)

        If curPrnPageNumber = 1 Then
            '用紙の色（印刷範囲全体）
            If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                e.Graphics.FillRectangle(printbc_brush,
                                         -Prn_left_margin,
                                         -Prn_top_margin,
                                         paper_width + Prn_left_margin + Prn_right_margin * 2,
                                         paper_height + Prn_top_margin + Prn_btm_margin * 2)
            End If
            '--------ヘッダー開始--------
            e.Graphics.DrawString(string_tmp, fnt_14, printfc_brush, 0, 0)
            string_tmp = curPrnPageNumber
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(curPrnPageNumber & "Page", fnt_10, printfc_brush, title_width + 5, title_height - stringSize.Height)

            '測定仕様枠
            Dim prfspec_hyourowend As Integer
            If FlgAdmin = 0 Then
                prfspec_hyourowend = 2
            Else
                prfspec_hyourowend = 3
            End If
            For i = 0 To prfspec_hyourowend
                path.StartFigure()
                path.AddLine(0, prfspec_hyoutop + (cell_height25 * i),
                             paper_width, prfspec_hyoutop + (cell_height25 * i))
            Next
            path.StartFigure()
            path.AddLine(0, prfspec_hyoutop,
                         0, prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))
            path.StartFigure()
            path.AddLine(120, prfspec_hyoutop,
                         120, prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))
            path.StartFigure()
            path.AddLine(120 + 150, prfspec_hyoutop,
                         120 + 150, prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))
            If FlgDBF = 1 Then
                path.StartFigure()
                path.AddLine(paper_width - (100 + 100 + 100), prfspec_hyoutop,
                             paper_width - (100 + 100 + 100), prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))
            End If
            path.StartFigure()
            path.AddLine(paper_width - (100 + 100), prfspec_hyoutop,
                         paper_width - (100 + 100), prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))
            path.StartFigure()
            path.AddLine(paper_width - 100, prfspec_hyoutop,
                         paper_width - 100, prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))
            path.StartFigure()
            path.AddLine(paper_width, prfspec_hyoutop,
                         paper_width, prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))

            Dim MeasDataNum_cur As Integer = Val(TxtMeasNumCur.Text)
            If MeasDataNum_cur > 0 Then
                string_tmp = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      paper_width - stringSize.Width, 0)
            End If

            If FlgAdmin <> 0 Then
                Dim MeasDataNum_bak As Integer = Val(TxtMeasNumBak.Text)
                If MeasDataNum_bak > 0 Then
                    string_tmp = StrPastDataDate & DataDate_bak & StrMeasTime & DataTime_bak
                    stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                    e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                          paper_width - stringSize.Width, stringSize.Height + 5)
                End If
            End If

            string_tmp = StrMachNo
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  120 + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = StrSampleName
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  120 + 150 + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
            If FlgDBF = 1 Then
                string_tmp = StrMark
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                      paper_width - (100 + 100 + 100) + cell_padding_left,
                                      title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
            End If
            string_tmp = StrMeasNumber
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  paper_width - (100 + 100) + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = StrMeasLot
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  paper_width - 100 + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = StrMeasSpec
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  cell_padding_left,
                                  title_height + gyou_height25 + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
            If FlgAdmin <> 0 Then
                string_tmp = StrPastMeasSpec
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      cell_padding_left,
                                      title_height + gyou_height25 + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
            End If

            '測定仕様 データ
            'マシーンNo. cur
            string_tmp = TxtMachNoCur.Text
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  120 + cell_padding_left,
                                  prfspec_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            'サンプル名 cur
            string_tmp = TxtSmplNamCur.Text
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  120 + 150 + cell_padding_left,
                                  prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
            If FlgDBF = 1 Then
                'マーク cur
                string_tmp = TxtMarkCur.Text
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - (100 + 100 + 100) + cell_padding_left,
                                  prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
            End If
            '測定回数 cur
            string_tmp = TxtMeasNumCur.Text
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - (100 + 100) + cell_padding_left,
                                  prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
            '測定ロット数 cur
            string_tmp = TxtMeasLotCur.Text
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - 100 + cell_padding_left,
                                  prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)

            If FlgAdmin <> 0 Then
                'マシーンNo. bak
                string_tmp = TxtMachNoBak.Text
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      120 + cell_padding_left,
                                      prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
                'サンプル名 bak
                string_tmp = TxtSmplNamBak.Text
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      120 + 150 + cell_padding_left,
                                      prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
                If FlgDBF = 1 Then
                    'マーク bak
                    string_tmp = TxtMarkBak.Text
                    stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                    e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      paper_width - (100 + 100 + 100) + cell_padding_left,
                                      prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
                End If
                '測定回数 bak
                'string_tmp = TxtMeasNumBak.Text
                string_tmp = TxtMeasNumBak.Text
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      paper_width - (100 + 100) + cell_padding_left,
                                      prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
                '測定ロット数 bak
                string_tmp = TxtMeasLotBak.Text
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      paper_width - 100 + cell_padding_left,
                                      prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
            End If
            '--------ヘッダー終了----------

            '測定データ表
            'Max. Avg. Min.
            For i = 0 To 4
                If i = 1 Then
                    path.StartFigure()
                    path.AddLine(dv_col1_width, prfdata_hyoutop + (cell_height25 * i),
                                 paper_width, prfdata_hyoutop + (cell_height25 * i))
                Else
                    path.StartFigure()
                    path.AddLine(0, prfdata_hyoutop + (cell_height25 * i),
                                 paper_width, prfdata_hyoutop + (cell_height25 * i))
                End If
            Next
            path.StartFigure()
            path.AddLine(0, prfdata_hyoutop,
                         0, prfdata_hyoutop + (cell_height25 * 5))
            path.StartFigure()
            path.AddLine(dv_col1_width, prfdata_hyoutop,
                         dv_col1_width, prfdata_hyoutop + (cell_height25 * 5))
            For i = 1 To 10
                If i Mod 2 = 0 Then
                    path.StartFigure()
                    path.AddLine(dv_col1_width + (dv_datacol_width * i), prfdata_hyoutop,
                                 dv_col1_width + (dv_datacol_width * i), prfdata_hyoutop + (cell_height25 * 5))
                Else
                    path.StartFigure()
                    path.AddLine(dv_col1_width + (dv_datacol_width * i), prfdata_hyoutop + cell_height25,
                                 dv_col1_width + dv_datacol_width * i, prfdata_hyoutop + (cell_height25 * 5))
                End If
            Next
            string_tmp = "No."
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 - stringSize.Height / 2)
            string_tmp = StrOrientAng
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = StrOrientRat
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 2 + dv_datacol_width - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = StrPropVelo_nounit
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 4 + dv_datacol_width - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = StrPropVelo_nounit
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 6 + dv_datacol_width - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "TSI(Km/S)^2"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 8 + dv_datacol_width - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Peak MD+-"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 0 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Deep CD+-"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 1 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "MD/CD"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 2 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Peak/Deep"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 3 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "MD"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 4 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "CD"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 5 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Peak"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 6 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Deep"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 7 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "MD"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 8 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "CD"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 9 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Max."
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Ave."
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Min."
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)

            '測定データ
            Select Case select_data
                Case PDMeasDataEnum
                    For i = 0 To 9
                        Select Case i
                            Case 0 : string_tmp = LblAnglePkMax_TB.Text
                            Case 1 : string_tmp = LblAngleDpMax_TB.Text
                            Case 2 : string_tmp = LblRatioMDCDMax_TB.Text
                            Case 3 : string_tmp = LblRatioPkDpMax_TB.Text
                            Case 4 : string_tmp = LblVeloMDMax_TB.Text
                            Case 5 : string_tmp = LblVeloCDMax_TB.Text
                            Case 6 : string_tmp = LblVeloPkMax_TB.Text
                            Case 7 : string_tmp = LblVeloDpMax_TB.Text
                            Case 8 : string_tmp = LblTSIMDMax_TB.Text
                            Case 9 : string_tmp = LblTSICDMax_TB.Text
                        End Select
                        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                              dv_col1_width + dv_datacol_width * i + dv_datacol_width / 2 - stringSize.Width / 2,
                                              prfdata_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
                    Next

                    For i = 0 To 9
                        Select Case i
                            Case 0 : string_tmp = LblAnglePkAvg_TB.Text
                            Case 1 : string_tmp = LblAngleDpAvg_TB.Text
                            Case 2 : string_tmp = LblRatioMDCDAvg_TB.Text
                            Case 3 : string_tmp = LblRatioPkDpAvg_TB.Text
                            Case 4 : string_tmp = LblVeloMDAvg_TB.Text
                            Case 5 : string_tmp = LblVeloCDAvg_TB.Text
                            Case 6 : string_tmp = LblVeloPkAvg_TB.Text
                            Case 7 : string_tmp = LblVeloDpAvg_TB.Text
                            Case 8 : string_tmp = LblTSIMDAvg_TB.Text
                            Case 9 : string_tmp = LblTSICDAvg_TB.Text
                        End Select
                        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                              dv_col1_width + dv_datacol_width * i + dv_datacol_width / 2 - stringSize.Width / 2,
                                              prfdata_hyoutop + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
                    Next

                    For i = 0 To 9
                        Select Case i
                            Case 0 : string_tmp = LblAnglePkMin_TB.Text
                            Case 1 : string_tmp = LblAngleDpMin_TB.Text
                            Case 2 : string_tmp = LblRatioMDCDMin_TB.Text
                            Case 3 : string_tmp = LblRatioPkDpMin_TB.Text
                            Case 4 : string_tmp = LblVeloMDMin_TB.Text
                            Case 5 : string_tmp = LblVeloCDMin_TB.Text
                            Case 6 : string_tmp = LblVeloPkMin_TB.Text
                            Case 7 : string_tmp = LblVeloDpMin_TB.Text
                            Case 8 : string_tmp = LblTSIMDMin_TB.Text
                            Case 9 : string_tmp = LblTSICDMin_TB.Text
                        End Select
                        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                        e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                              dv_col1_width + dv_datacol_width * i + dv_datacol_width / 2 - stringSize.Width / 2,
                                              prfdata_hyoutop + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
                    Next

                Case PDOldDataEnum
                    For i = 0 To 9
                        Select Case i
                            Case 0 : string_tmp = LblAnglePkMaxOld_TB.Text
                            Case 1 : string_tmp = LblAngleDpMaxOld_TB.Text
                            Case 2 : string_tmp = LblRatioMDCDMaxOld_TB.Text
                            Case 3 : string_tmp = LblRatioPkDpMaxOld_TB.Text
                            Case 4 : string_tmp = LblVeloMDMaxOld_TB.Text
                            Case 5 : string_tmp = LblVeloCDMaxOld_TB.Text
                            Case 6 : string_tmp = LblVeloPkMaxOld_TB.Text
                            Case 7 : string_tmp = LblVeloDpMaxOld_TB.Text
                            Case 8 : string_tmp = LblTSIMDMaxOld_TB.Text
                            Case 9 : string_tmp = LblTSICDMaxOld_TB.Text
                        End Select
                        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                              dv_col1_width + dv_datacol_width * i + dv_datacol_width / 2 - stringSize.Width / 2,
                                              prfdata_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
                    Next

                    For i = 0 To 9
                        Select Case i
                            Case 0 : string_tmp = LblAnglePkAvgOld_TB.Text
                            Case 1 : string_tmp = LblAngleDpAvgOld_TB.Text
                            Case 2 : string_tmp = LblRatioMDCDAvgOld_TB.Text
                            Case 3 : string_tmp = LblRatioPkDpAvgOld_TB.Text
                            Case 4 : string_tmp = LblVeloMDAvgOld_TB.Text
                            Case 5 : string_tmp = LblVeloCDAvgOld_TB.Text
                            Case 6 : string_tmp = LblVeloPkAvgOld_TB.Text
                            Case 7 : string_tmp = LblVeloDpAvgOld_TB.Text
                            Case 8 : string_tmp = LblTSIMDAvgOld_TB.Text
                            Case 9 : string_tmp = LblTSICDAvgOld_TB.Text
                        End Select
                        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                              dv_col1_width + dv_datacol_width * i + dv_datacol_width / 2 - stringSize.Width / 2,
                                              prfdata_hyoutop + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
                    Next

                    For i = 0 To 9
                        Select Case i
                            Case 0 : string_tmp = LblAnglePkMinOld_TB.Text
                            Case 1 : string_tmp = LblAngleDpMinOld_TB.Text
                            Case 2 : string_tmp = LblRatioMDCDMinOld_TB.Text
                            Case 3 : string_tmp = LblRatioPkDpMinOld_TB.Text
                            Case 4 : string_tmp = LblVeloMDMinOld_TB.Text
                            Case 5 : string_tmp = LblVeloCDMinOld_TB.Text
                            Case 6 : string_tmp = LblVeloPkMinOld_TB.Text
                            Case 7 : string_tmp = LblVeloDpMinOld_TB.Text
                            Case 8 : string_tmp = LblTSIMDMinOld_TB.Text
                            Case 9 : string_tmp = LblTSICDMinOld_TB.Text
                        End Select
                        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                        e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                              dv_col1_width + dv_datacol_width * i + dv_datacol_width / 2 - stringSize.Width / 2,
                                              prfdata_hyoutop + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
                    Next

                Case PDAvgDataEnum
                    For i = 0 To 9
                        Select Case i
                            Case 0 : string_tmp = LblAnglePkMaxAvg_TB.Text
                            Case 1 : string_tmp = LblAngleDpMaxAvg_TB.Text
                            Case 2 : string_tmp = LblRatioMDCDMaxAvg_TB.Text
                            Case 3 : string_tmp = LblRatioPkDpMaxAvg_TB.Text
                            Case 4 : string_tmp = LblVeloMDMaxAvg_TB.Text
                            Case 5 : string_tmp = LblVeloCDMaxAvg_TB.Text
                            Case 6 : string_tmp = LblVeloPkMaxAvg_TB.Text
                            Case 7 : string_tmp = LblVeloDpMaxAvg_TB.Text
                            Case 8 : string_tmp = LblTSIMDMaxAvg_TB.Text
                            Case 9 : string_tmp = LblTSICDMaxAvg_TB.Text
                        End Select
                        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                        e.Graphics.DrawString(string_tmp, fnt_10, print_avgdata_brush,
                                              dv_col1_width + dv_datacol_width * i + dv_datacol_width / 2 - stringSize.Width / 2,
                                              prfdata_hyoutop + cell_height25 * 2 + cell_height25 / 2 - stringSize.Height / 2)
                    Next

                    For i = 0 To 9
                        Select Case i
                            Case 0 : string_tmp = LblAnglePkAvgAvg_TB.Text
                            Case 1 : string_tmp = LblAngleDpAvgAvg_TB.Text
                            Case 2 : string_tmp = LblRatioMDCDAvgAvg_TB.Text
                            Case 3 : string_tmp = LblRatioPkDpAvgAvg_TB.Text
                            Case 4 : string_tmp = LblVeloMDAvgAvg_TB.Text
                            Case 5 : string_tmp = LblVeloCDAvgAvg_TB.Text
                            Case 6 : string_tmp = LblVeloPkAvgAvg_TB.Text
                            Case 7 : string_tmp = LblVeloDpAvgAvg_TB.Text
                            Case 8 : string_tmp = LblTSIMDAvgAvg_TB.Text
                            Case 9 : string_tmp = LblTSICDAvgAvg_TB.Text
                        End Select
                        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                        e.Graphics.DrawString(string_tmp, fnt_10, print_avgdata_brush,
                                              dv_col1_width + dv_datacol_width * i + dv_datacol_width / 2 - stringSize.Width / 2,
                                              prfdata_hyoutop + cell_height25 * 3 + cell_height25 / 2 - stringSize.Height / 2)
                    Next

                    For i = 0 To 9
                        Select Case i
                            Case 0 : string_tmp = LblAnglePkMinAvg_TB.Text
                            Case 1 : string_tmp = LblAngleDpMinAvg_TB.Text
                            Case 2 : string_tmp = LblRatioMDCDMinAvg_TB.Text
                            Case 3 : string_tmp = LblRatioPkDpMinAvg_TB.Text
                            Case 4 : string_tmp = LblVeloMDMinAvg_TB.Text
                            Case 5 : string_tmp = LblVeloCDMinAvg_TB.Text
                            Case 6 : string_tmp = LblVeloPkMinAvg_TB.Text
                            Case 7 : string_tmp = LblVeloDpMinAvg_TB.Text
                            Case 8 : string_tmp = LblTSIMDMinAvg_TB.Text
                            Case 9 : string_tmp = LblTSICDMinAvg_TB.Text
                        End Select
                        stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                        e.Graphics.DrawString(string_tmp, fnt_10, print_avgdata_brush,
                                              dv_col1_width + dv_datacol_width * i + dv_datacol_width / 2 - stringSize.Width / 2,
                                              prfdata_hyoutop + cell_height25 * 4 + cell_height25 / 2 - stringSize.Height / 2)
                    Next

            End Select

            curPrnRow = 0
            prf_prn_linepath1.Clear()
            prf_prn_linepath2.Clear()

            While curPrnDataNumber <= targetPrnRow
                If last_row <= cur_row Then
                    e.HasMorePages = True
                    curPrnPageNumber += 1
                    path2.StartFigure()
                    path2.AddLine(0, prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)),
                                  paper_width, prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)))
                    curPrnRow = 0
                    cur_row = Prn_top_margin

                    prf_prn_linepath1.Add(path)
                    prf_prn_linepath2.Add(path2)

                    For Each path_tmp As GraphicsPath In prf_prn_linepath1
                        e.Graphics.DrawPath(pen_black_1, path_tmp)
                    Next

                    For Each path_tmp2 As GraphicsPath In prf_prn_linepath2
                        e.Graphics.DrawPath(pen_black_2, path_tmp2)
                    Next

                    prf_prn_linepath1.Clear()
                    prf_prn_linepath2.Clear()
                    Exit Sub
                Else
                    If curPrnRow = 0 Then
                        path2.StartFigure()
                        path2.AddLine(0, prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)),
                                      paper_width, prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)))
                    Else
                        path.StartFigure()
                        path.AddLine(0, prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)),
                                     paper_width, prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)))
                    End If
                    path2.StartFigure()
                    path2.AddLine(0, prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)),
                                  0, prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)) + cell_height25)

                    For i = 0 To 9
                        path.StartFigure()
                        path.AddLine(dv_col1_width + (dv_datacol_width * i),
                                     prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)), dv_col1_width + (dv_datacol_width * i),
                                     prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)) + cell_height25)
                    Next

                    path2.StartFigure()
                    path2.AddLine(dv_col1_width + (dv_datacol_width * 10),
                                  prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)), dv_col1_width + (dv_datacol_width * 10),
                                  prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)) + cell_height25)

                    string_tmp = curPrnDataNumber
                    stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                    e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                          dv_col1_width / 2 - (stringSize.Width / 2),
                                          prfdata_hyoutop + (cell_height25 * (curPrnDataNumber + 4) + cell_height25 / 2 - stringSize.Height / 2))

                    For i = 1 To 10
                        Select Case select_data
                            Case PDMeasDataEnum
                                string_tmp = DataGridView1.Rows(curPrnDataNumber - 1).Cells(i).Value
                                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                              dv_col1_width + dv_datacol_width * (i - 1) + dv_datacol_width / 2 - stringSize.Width / 2,
                                              prfdata_hyoutop + (cell_height25 * (curPrnDataNumber + 4) + cell_height25 / 2 - stringSize.Height / 2))
                            Case PDOldDataEnum
                                string_tmp = DataGridView2.Rows(curPrnDataNumber - 1).Cells(i).Value
                                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                              dv_col1_width + dv_datacol_width * (i - 1) + dv_datacol_width / 2 - stringSize.Width / 2,
                                              prfdata_hyoutop + (cell_height25 * (curPrnDataNumber + 4) + cell_height25 / 2 - stringSize.Height / 2))
                            Case PDAvgDataEnum
                                string_tmp = DataGridView3.Rows(curPrnDataNumber - 1).Cells(i).Value
                                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                                e.Graphics.DrawString(string_tmp, fnt_10, print_avgdata_brush,
                                              dv_col1_width + dv_datacol_width * (i - 1) + dv_datacol_width / 2 - stringSize.Width / 2,
                                              prfdata_hyoutop + (cell_height25 * (curPrnDataNumber + 4) + cell_height25 / 2 - stringSize.Height / 2))
                        End Select
                    Next

                    curPrnDataNumber += 1
                    curPrnRow += 1
                    cur_row += cell_height25
                End If

            End While
            e.HasMorePages = False
            path2.StartFigure()
            path2.AddLine(0, prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)),
                          paper_width, prfdata_hyoutop + (cell_height25 * (curPrnRow + 5)))
            prf_prn_linepath1.Add(path)
            prf_prn_linepath2.Add(path2)

            For Each path_tmp As GraphicsPath In prf_prn_linepath1
                e.Graphics.DrawPath(pen_black_1, path_tmp)
            Next

            For Each path_tmp2 As GraphicsPath In prf_prn_linepath2
                e.Graphics.DrawPath(pen_black_2, path_tmp2)
            Next
        Else
            '用紙の色（印刷範囲全体）
            If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                e.Graphics.FillRectangle(printbc_brush,
                                         -Prn_left_margin,
                                         -Prn_top_margin,
                                         paper_width + Prn_left_margin + Prn_right_margin * 2,
                                         paper_height + Prn_top_margin + Prn_btm_margin * 2)
            End If
            '-2ページ以降-------ヘッダー開始--------

            data_sta_row2 = prfdata_hyoutop + cell_height25 * 2

            e.Graphics.DrawString(string_tmp, fnt_14, printfc_brush, 0, 0)
            string_tmp = curPrnPageNumber
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(curPrnPageNumber & "Page", fnt_10, printfc_brush, title_width + 5, title_height - stringSize.Height)

            '測定仕様枠
            Dim prfspec_hyourowend As Integer
            If FlgAdmin = 0 Then
                prfspec_hyourowend = 2
            Else
                prfspec_hyourowend = 3
            End If
            For i = 0 To prfspec_hyourowend
                path.StartFigure()
                path.AddLine(0, prfspec_hyoutop + (cell_height25 * i),
                             paper_width, prfspec_hyoutop + (cell_height25 * i))
            Next
            path.StartFigure()
            path.AddLine(0, prfspec_hyoutop,
                         0, prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))
            path.StartFigure()
            path.AddLine(120, prfspec_hyoutop,
                         120, prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))
            path.StartFigure()
            path.AddLine(120 + 150, prfspec_hyoutop,
                         120 + 150, prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))
            If FlgDBF = 1 Then
                path.StartFigure()
                path.AddLine(paper_width - (100 + 100 + 100), prfspec_hyoutop,
                             paper_width - (100 + 100 + 100), prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))
            End If
            path.StartFigure()
            path.AddLine(paper_width - (100 + 100), prfspec_hyoutop,
                         paper_width - (100 + 100), prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))
            path.StartFigure()
            path.AddLine(paper_width - 100, prfspec_hyoutop,
                         paper_width - 100, prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))
            path.StartFigure()
            path.AddLine(paper_width, prfspec_hyoutop,
                         paper_width, prfspec_hyoutop + (cell_height25 * prfspec_hyourowend))

            Dim MeasDataNum_cur As Integer = Val(TxtMeasNumCur.Text)
            If MeasDataNum_cur > 0 Then
                string_tmp = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      paper_width - stringSize.Width, 0)
            End If

            If FlgAdmin <> 0 Then
                'Dim MeasDataNum_bak As Integer = Val(TxtMeasNumBak.Text)
                Dim MeasDataNum_bak As Integer = Val(TxtMeasNumBak.Text)
                If MeasDataNum_bak > 0 Then
                    string_tmp = StrPastDataDate & DataDate_bak & StrMeasTime & DataTime_bak
                    stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                    e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                          paper_width - stringSize.Width, stringSize.Height + 5)
                End If
            End If

            string_tmp = StrMachNo
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  120 + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = StrSampleName
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  120 + 150 + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
            If FlgDBF = 1 Then
                string_tmp = StrMark
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  paper_width - (100 + 100 + 100) + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
            End If
            string_tmp = StrMeasNumber
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  paper_width - (100 + 100) + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = StrMeasLot
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  paper_width - 100 + cell_padding_left,
                                  title_height + gyou_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = StrMeasSpec
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  cell_padding_left,
                                  title_height + gyou_height25 + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
            If FlgAdmin <> 0 Then
                string_tmp = StrPastMeasSpec
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      cell_padding_left,
                                      title_height + gyou_height25 + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
            End If

            '測定仕様 データ
            'マシーンNo. cur
            string_tmp = TxtMachNoCur.Text
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  120 + cell_padding_left,
                                  prfspec_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            'サンプル名 cur
            string_tmp = TxtSmplNamCur.Text
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  120 + 150 + cell_padding_left,
                                  prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
            If FlgDBF = 1 Then
                'マーク cur
                string_tmp = TxtMarkCur.Text
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                      paper_width - (100 + 100 + 100) + cell_padding_left,
                                      prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
            End If
            '測定回数 cur
            string_tmp = TxtMeasNumCur.Text
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - (100 + 100) + cell_padding_left,
                                  prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)
            '測定ロット数 cur
            string_tmp = TxtMeasLotCur.Text
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                  paper_width - 100 + cell_padding_left,
                                  prfspec_hyoutop + (cell_height25 * 1) + cell_height25 / 2 - stringSize.Height / 2)

            If FlgAdmin <> 0 Then
                'マシーンNo. bak
                string_tmp = TxtMachNoBak.Text
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      120 + cell_padding_left,
                                      prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
                'サンプル名 bak
                string_tmp = TxtSmplNamBak.Text
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      120 + 150 + cell_padding_left,
                                      prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
                If FlgDBF = 1 Then
                    'マーク bak
                    string_tmp = TxtMarkBak.Text
                    stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                    e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                          paper_width - (100 + 100 + 100) + cell_padding_left,
                                          prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
                End If
                '測定回数 bak
                'string_tmp = TxtMeasNumBak.Text
                string_tmp = TxtMeasNumBak.Text
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      paper_width - (100 + 100) + cell_padding_left,
                                      prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
                '測定ロット数 bak
                string_tmp = TxtMeasLotBak.Text
                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                      paper_width - 100 + cell_padding_left,
                                      prfspec_hyoutop + (cell_height25 * 2) + cell_height25 / 2 - stringSize.Height / 2)
            End If

            path.StartFigure()
            path.AddLine(0, prfdata_hyoutop + (cell_height25 * 0), paper_width, prfdata_hyoutop + (cell_height25 * 0))
            path.StartFigure()
            path.AddLine(dv_col1_width, prfdata_hyoutop + (cell_height25 * 1), paper_width, prfdata_hyoutop + (cell_height25 * 1))

            path.StartFigure()
            path.AddLine(0, prfdata_hyoutop,
                         0, prfdata_hyoutop + (cell_height25 * 2))
            path.StartFigure()
            path.AddLine(dv_col1_width, prfdata_hyoutop,
                         dv_col1_width, prfdata_hyoutop + (cell_height25 * 2))
            For i = 1 To 10
                If i Mod 2 = 0 Then
                    path.StartFigure()
                    path.AddLine(dv_col1_width + (dv_datacol_width * i), prfdata_hyoutop,
                                 dv_col1_width + (dv_datacol_width * i), prfdata_hyoutop + (cell_height25 * 2))
                Else
                    path.StartFigure()
                    path.AddLine(dv_col1_width + (dv_datacol_width * i), prfdata_hyoutop + cell_height25,
                                 dv_col1_width + dv_datacol_width * i, prfdata_hyoutop + (cell_height25 * 2))
                End If
            Next
            string_tmp = "No."
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 - stringSize.Height / 2)
            string_tmp = StrOrientAng
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = StrOrientRat
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 2 + dv_datacol_width - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = StrPropVelo_nounit
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 4 + dv_datacol_width - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = StrPropVelo_nounit
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 6 + dv_datacol_width - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "TSI(Km/S)^2"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 8 + dv_datacol_width - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Peak MD+-"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 0 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Deep CD+-"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 1 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "MD/CD"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 2 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Peak/Deep"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 3 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "MD"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 4 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "CD"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 5 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Peak"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 6 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "Deep"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 7 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "MD"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 8 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)
            string_tmp = "CD"
            stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
            e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                  dv_col1_width + dv_datacol_width * 9 + dv_datacol_width / 2 - stringSize.Width / 2,
                                  prfdata_hyoutop + cell_height25 + cell_height25 / 2 - stringSize.Height / 2)


            While curPrnDataNumber <= targetPrnRow
                If last_row2 <= cur_row Then
                    e.HasMorePages = True
                    curPrnPageNumber += 1
                    path2.StartFigure()
                    'path2.AddLine(0, Prn_top_margin + (data_sta_row2 + cell_height25 * curPrnRow),
                    path2.AddLine(0, (data_sta_row2 + cell_height25 * curPrnRow),
                                  paper_width, (data_sta_row2 + cell_height25 * curPrnRow))
                    curPrnRow = 0
                    cur_row = Prn_top_margin
                    prf_prn_linepath1.Add(path)
                    prf_prn_linepath2.Add(path2)
                    For Each path_tmp As GraphicsPath In prf_prn_linepath1
                        e.Graphics.DrawPath(pen_black_1, path_tmp)
                    Next

                    For Each path_tmp2 As GraphicsPath In prf_prn_linepath2
                        e.Graphics.DrawPath(pen_black_2, path_tmp2)
                    Next

                    prf_prn_linepath1.Clear()
                    prf_prn_linepath2.Clear()
                    Exit Sub
                Else
                    If curPrnRow = 0 Then
                        path2.StartFigure()
                        path2.AddLine(0, (data_sta_row2 + cell_height25 * curPrnRow), paper_width, (data_sta_row2 + cell_height25 * curPrnRow))
                    Else
                        path.StartFigure()
                        path.AddLine(0, (data_sta_row2 + cell_height25 * curPrnRow), paper_width, (data_sta_row2 + cell_height25 * curPrnRow))
                    End If
                    path2.StartFigure()
                    path2.AddLine(0, (data_sta_row2 + cell_height25 * curPrnRow), 0, (data_sta_row2 + cell_height25 * curPrnRow) + cell_height25)

                    For i = 0 To 9
                        path.StartFigure()
                        path.AddLine(dv_col1_width + (dv_datacol_width * i), (data_sta_row2 + cell_height25 * curPrnRow),
                                     dv_col1_width + (dv_datacol_width * i), (data_sta_row2 + cell_height25 * curPrnRow) + cell_height25)
                    Next
                    path2.StartFigure()
                    path2.AddLine(dv_col1_width + (dv_datacol_width * 10), (data_sta_row2 + cell_height25 * curPrnRow),
                                  dv_col1_width + (dv_datacol_width * 10), (data_sta_row2 + cell_height25 * curPrnRow) + cell_height25)

                    string_tmp = curPrnDataNumber
                    stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                    e.Graphics.DrawString(string_tmp, fnt_10, printfc_brush,
                                          dv_col1_width / 2 - (stringSize.Width / 2), data_sta_row2 + (cell_height25 * curPrnRow) + cell_height25 / 2 - stringSize.Height / 2)

                    For i = 1 To 10
                        Select Case select_data
                            Case PDMeasDataEnum
                                string_tmp = DataGridView1.Rows(curPrnDataNumber - 1).Cells(i).Value
                                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                                e.Graphics.DrawString(string_tmp, fnt_10, print_curdata_brush,
                                              dv_col1_width + dv_datacol_width * (i - 1) + dv_datacol_width / 2 - stringSize.Width / 2,
                                              (data_sta_row2 + cell_height25 * curPrnRow) + cell_height25 / 2 - stringSize.Height / 2)
                            Case PDOldDataEnum
                                string_tmp = DataGridView2.Rows(curPrnDataNumber - 1).Cells(i).Value
                                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                                e.Graphics.DrawString(string_tmp, fnt_10, print_olddata_brush,
                                              dv_col1_width + dv_datacol_width * (i - 1) + dv_datacol_width / 2 - stringSize.Width / 2,
                                              (data_sta_row2 + cell_height25 * curPrnRow) + cell_height25 / 2 - stringSize.Height / 2)
                            Case PDAvgDataEnum
                                string_tmp = DataGridView3.Rows(curPrnDataNumber - 1).Cells(i).Value
                                stringSize = e.Graphics.MeasureString(string_tmp, fnt_10)
                                e.Graphics.DrawString(string_tmp, fnt_10, print_avgdata_brush,
                                              dv_col1_width + dv_datacol_width * (i - 1) + dv_datacol_width / 2 - stringSize.Width / 2,
                                              (data_sta_row2 + cell_height25 * curPrnRow) + cell_height25 / 2 - stringSize.Height / 2)
                        End Select

                    Next

                    curPrnDataNumber += 1
                    curPrnRow += 1
                    cur_row += cell_height25

                End If
            End While
            curPrnPageNumber = 1
            curPrnDataNumber = 1
            e.HasMorePages = False
            path2.StartFigure()
            path2.AddLine(0, (data_sta_row2 + cell_height25 * curPrnRow), paper_width, (data_sta_row2 + cell_height25 * curPrnRow))
            prf_prn_linepath1.Add(path)
            prf_prn_linepath2.Add(path2)

            For Each path_tmp As GraphicsPath In prf_prn_linepath1
                e.Graphics.DrawPath(pen_black_1, path_tmp)
            Next

            For Each path_tmp2 As GraphicsPath In prf_prn_linepath2
                e.Graphics.DrawPath(pen_black_2, path_tmp2)
            Next

        End If

        path.Dispose()
        path2.Dispose()
        pen_black_1.Dispose()
        pen_black_2.Dispose()
        fnt_10.Dispose()
        fnt_14.Dispose()

    End Sub

    Private Sub FrmSST4500_1_0_0E_Profile_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        FlgMainProfile = 90
    End Sub

    Private Sub CmdPrfPrint_Click(sender As Object, e As EventArgs) Handles CmdPrfPrint.Click
        PrintoutPrf()
    End Sub

    Private Sub PrintoutPrf()
        Dim flgprintpreview As Boolean
        flgprintpreview = My.Settings._printpreview

        If FlgAdmin = 0 Then
            '通常モード
            If ChkPrn_AngleRatio.Checked = True Then
                PDAngleRatio_nom.OriginAtMargins = True
                PDAngleRatio_nom.DefaultPageSettings.Margins = New Margins(Prn_left_margin, Prn_right_margin, Prn_top_margin, Prn_btm_margin)
                If FlgPrfAutoPrn = 0 Then
                    '手動印刷
                    If flgprintpreview = True Then
                        PPDAngleRatio_nom.ShowDialog()
                    Else
                        PDAngleRatio_nom.Print()
                    End If
                Else
                    '自動印刷
                    PDAngleRatio_nom.Print()
                End If
            End If

            If ChkPrn_VelocityTSI.Checked = True Then
                PDVeloTSI_nom.OriginAtMargins = True
                PDVeloTSI_nom.DefaultPageSettings.Margins = New Margins(Prn_left_margin, Prn_right_margin, Prn_top_margin, Prn_btm_margin)
                If FlgPrfAutoPrn = 0 Then
                    If flgprintpreview = True Then
                        PPDVeloTSI_nom.ShowDialog()
                    Else
                        PDVeloTSI_nom.Print()
                    End If
                Else
                    PDVeloTSI_nom.Print()
                End If
            End If

            If ChkPrn_MeasData.Checked = True And MeasDataMax > 0 Then
                PDMeasData.OriginAtMargins = True
                PDMeasData.DefaultPageSettings.Margins = New Margins(Prn_left_margin, Prn_right_margin, Prn_top_margin, Prn_btm_margin)

                curPrnPageNumber = 1
                curPrnDataNumber = 1

                If FlgPrfAutoPrn = 0 Then
                    If flgprintpreview = True Then
                        PPDMeasData.ShowDialog()
                    Else
                        PDMeasData.Print()
                    End If
                Else
                    PDMeasData.Print()
                End If
            End If
        Else
            '管理者モード
            If ChkPrn_AngleRatio.Checked = True Then
                PDAngleRatio_adm.OriginAtMargins = True
                PDAngleRatio_adm.DefaultPageSettings.Margins = New Margins(Prn_left_margin, Prn_right_margin, Prn_top_margin, Prn_btm_margin)
                If FlgPrfAutoPrn = 0 Then
                    If flgprintpreview = True Then
                        PPDAngleRatio_adm.ShowDialog()
                    Else
                        PDAngleRatio_adm.Print()
                    End If
                Else
                    PDAngleRatio_adm.Print()
                End If
            End If

            If ChkPrn_VelocityTSI.Checked = True Then
                PDVeloTSI_adm.OriginAtMargins = True
                PDVeloTSI_adm.DefaultPageSettings.Margins = New Margins(Prn_left_margin, Prn_right_margin, Prn_top_margin, Prn_btm_margin)
                If FlgPrfAutoPrn = 0 Then
                    If flgprintpreview = True Then
                        PPDVeloTSI_adm.ShowDialog()
                    Else
                        PDVeloTSI_adm.Print()
                    End If
                Else
                    PDVeloTSI_adm.Print()
                End If
            End If

            If ChkPrn_MeasData.Checked = True And MeasDataMax > 0 Then
                PDMeasData.OriginAtMargins = True
                PDMeasData.DefaultPageSettings.Margins = New Margins(Prn_left_margin, Prn_right_margin, Prn_top_margin, Prn_btm_margin)

                curPrnPageNumber = 1
                curPrnDataNumber = 1

                If FlgPrfAutoPrn = 0 Then
                    If flgprintpreview = True Then
                        PPDMeasData.ShowDialog()
                    Else
                        PDMeasData.Print()
                    End If
                Else
                    PDMeasData.Print()
                End If
            End If

            If ChkPrn_OldData.Checked = True And FileDataMax > 0 Then
                PDOldData.OriginAtMargins = True
                PDOldData.DefaultPageSettings.Margins = New Margins(Prn_left_margin, Prn_right_margin, Prn_top_margin, Prn_btm_margin)

                curPrnPageNumber = 1
                curPrnDataNumber = 1

                If FlgPrfAutoPrn = 0 Then
                    If flgprintpreview = True Then
                        'PPDOldData.Document = PDOldData
                        PPDOldData.ShowDialog()
                    Else
                        PDOldData.Print()
                    End If
                Else
                    PDOldData.Print()
                End If
            End If

            If ChkPrn_AvgData.Checked = True And FlgAvg > 0 Then
                PDAvgData.OriginAtMargins = True
                PDAvgData.DefaultPageSettings.Margins = New Margins(Prn_left_margin, Prn_right_margin, Prn_top_margin, Prn_btm_margin)

                curPrnPageNumber = 1
                curPrnDataNumber = 1

                If FlgPrfAutoPrn = 0 Then
                    If flgprintpreview = True Then
                        PPDAvgData.ShowDialog()
                    Else
                        PDAvgData.Print()
                    End If
                Else
                    PDAvgData.Print()
                End If
            End If

        End If

    End Sub

    Private Sub ChkPrfAutoPrn_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPrfAutoPrn.CheckedChanged
        If ChkPrfAutoPrn.Checked = True Then
            FlgPrfAutoPrn = 1
            If Menu_AutoPrn.Checked = False Then
                Menu_AutoPrn.Checked = True
                'FlgConstChg = True  '変更有の状態にセットする
            End If
        Else
            FlgPrfAutoPrn = 0
            If Menu_AutoPrn.Checked = True Then
                Menu_AutoPrn.Checked = False
                'FlgConstChg = True  '変更有の状態にセットする
            End If
        End If
        If FlgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text2)
        End If
    End Sub

    Public Sub ChkPitchExp_Ena_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPitchExp_Ena.CheckedChanged
        If ChkPitchExp_Ena.Checked = True Then
            RemoveHandler ChkPitchExp_Dis.CheckedChanged, AddressOf ChkPitchExp_Dis_CheckedChanged
            ChkPitchExp_Dis.Checked = False
            AddHandler ChkPitchExp_Dis.CheckedChanged, AddressOf ChkPitchExp_Dis_CheckedChanged
            FlgPitchExp = 1
            TxtLength.Enabled = False
            TxtPitch.Enabled = False
            TxtPoints.Enabled = False

            If FlgInitEnd = 1 Then
                If FlgPitchExp_Load = 0 Then
                    'FrmSST4500_1_0_0E_pitchsetting.Visible = True
                    LoadConstPitch(PchExpSettingFile_FullPath)
                Else
                    TxtLength.Text = PchExp_Length
                    TxtPitch.Text = PchExp_PchData(0)
                    TxtPoints.Text = UBound(PchExp_PchData) + 2

                    If MeasDataNo = 0 And FileDataNo = 0 Then
                        MeasDataNo = 0
                        FileDataNo = 0
                        DrawCalcCurData_init()
                        DrawCalcBakData_init()
                        DrawCalcAvgData_init()
                        DrawTableData_init()
                        GraphInitPrf(UBound(PchExp_PchData) + 2)
                    End If
                End If
            End If
        Else
            RemoveHandler ChkPitchExp_Dis.CheckedChanged, AddressOf ChkPitchExp_Dis_CheckedChanged
            ChkPitchExp_Dis.Checked = True
            AddHandler ChkPitchExp_Dis.CheckedChanged, AddressOf ChkPitchExp_Dis_CheckedChanged
            FlgPitchExp = 0
            TxtLength.Enabled = True
            TxtPitch.Enabled = True
            TxtPoints.Enabled = True
            TxtLength.Text = Length
            TxtPitch.Text = Pitch
            TxtPoints.Text = Points

            If MeasDataNo = 0 And FileDataNo = 0 Then
                MeasDataNo = 0
                FileDataNo = 0
                DrawCalcCurData_init()
                DrawCalcBakData_init()
                DrawCalcAvgData_init()
                DrawTableData_init()
                GraphInitPrf(Points)
            End If
        End If
        If FlgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text2)
        End If
    End Sub

    Public Sub ChkPitchExp_Dis_CheckedChanged(sender As Object, e As EventArgs) Handles ChkPitchExp_Dis.CheckedChanged
        If ChkPitchExp_Dis.Checked = False Then
            RemoveHandler ChkPitchExp_Ena.CheckedChanged, AddressOf ChkPitchExp_Ena_CheckedChanged
            ChkPitchExp_Ena.Checked = True
            AddHandler ChkPitchExp_Ena.CheckedChanged, AddressOf ChkPitchExp_Ena_CheckedChanged
            FlgPitchExp = 1
            TxtLength.Enabled = False
            TxtPitch.Enabled = False
            TxtPoints.Enabled = False

            If FlgInitEnd = 1 Then
                If FlgPitchExp_Load = 0 Then
                    'FrmSST4500_1_0_0E_pitchsetting.Visible = True
                    LoadConstPitch(PchExpSettingFile_FullPath)
                Else
                    TxtLength.Text = PchExp_Length
                    TxtPitch.Text = PchExp_PchData(0)
                    TxtPoints.Text = UBound(PchExp_PchData) + 2

                    If MeasDataNo = 0 And FileDataNo = 0 Then
                        MeasDataNo = 0
                        FileDataNo = 0
                        DrawCalcCurData_init()
                        DrawCalcBakData_init()
                        DrawCalcAvgData_init()
                        DrawTableData_init()
                        GraphInitPrf(UBound(PchExp_PchData) + 2)
                    End If
                End If
            End If
        Else
            RemoveHandler ChkPitchExp_Ena.CheckedChanged, AddressOf ChkPitchExp_Ena_CheckedChanged
            ChkPitchExp_Ena.Checked = False
            AddHandler ChkPitchExp_Ena.CheckedChanged, AddressOf ChkPitchExp_Ena_CheckedChanged
            FlgPitchExp = 0
            TxtLength.Enabled = True
            TxtPitch.Enabled = True
            TxtPoints.Enabled = True
            TxtLength.Text = Length
            TxtPitch.Text = Pitch
            TxtPoints.Text = Points

            If MeasDataNo = 0 And FileDataNo = 0 Then
                MeasDataNo = 0
                FileDataNo = 0
                DrawCalcCurData_init()
                DrawCalcBakData_init()
                DrawCalcAvgData_init()
                DrawTableData_init()
                GraphInitPrf(Points)
            End If
        End If
        If FlgInitEnd = 1 Then
            ConstChangeTrue(Me, title_text2)
        End If
    End Sub

    Private Sub PrfResultSave()
        CmdPrfResultSave.Enabled = False
        CmdPrfResultSave.Text = "Saving"

        Dim Ret As DialogResult
        Dim FilePath As String = ""
        Dim SaveDate As String
        Dim SaveTime As String
        Dim SaveDefFileName As String
        Dim ratio_top_row As Integer
        Dim tsi_top_row As Integer
        Dim bmp As Bitmap
        Dim aa As Single
        Dim i As Integer
        Dim excelApp As New Excel.Application
        Dim excelBooks As Excel.Workbooks = excelApp.Workbooks
        Dim excelBook As Excel.Workbook = excelBooks.Add()
        Dim sheet1 As Excel.Worksheet = excelApp.Worksheets.Add()
        sheet1.Name = "Orientation Angle・Ratio"
        Dim sheet2 As Excel.Worksheet = excelApp.Worksheets.Add(, sheet1, 1, Excel.XlSheetType.xlWorksheet)
        sheet2.Name = "Propagation Velocity・TSI"
        Dim sheet3 As Excel.Worksheet = excelApp.Worksheets.Add(, sheet2, 1, Excel.XlSheetType.xlWorksheet)
        sheet3.Name = "Meas.Data"
        Dim sheet4 As Excel.Worksheet = excelApp.Worksheets.Add(, sheet3, 1, Excel.XlSheetType.xlWorksheet)
        sheet4.Name = "Past Data"
        Dim sheet5 As Excel.Worksheet = excelApp.Worksheets.Add(, sheet4, 1, Excel.XlSheetType.xlWorksheet)
        sheet5.Name = "Ave.Value Data"

        Try
            Using dialog As New SaveFileDialog
                With dialog
                    Select Case FlgProfile
                        Case 1
                            .InitialDirectory = PF_ResultSave_path
                        Case 2
                            .InitialDirectory = CT_ResultSave_path
                        Case 3
                            .InitialDirectory = LG_ResultSave_path
                        Case Else
                            .InitialDirectory = SG_ResultSave_path
                    End Select

                    .Title = StrSaveMeasResult
                    .Filter = "Excel File(*.xlsx)|*.xlsx"

                    SaveDate = Now.ToString("yyyyMMdd")
                    SaveTime = Now.ToString("HHmmss")
                    SaveDefFileName = SaveDate & SaveTime & ".xlsx"

                    .FileName = SaveDefFileName

                    Ret = .ShowDialog

                    If Ret = DialogResult.OK Then
                        FilePath = .FileName

                        Select Case FlgProfile
                            Case 1
                                PF_ResultSave_path = Path.GetDirectoryName(FilePath)
                                My.Settings._pfresultsave_path = PF_ResultSave_path
                            Case 2
                                CT_ResultSave_path = Path.GetDirectoryName(FilePath)
                                My.Settings._ctresultsave_path = CT_ResultSave_path
                            Case 3
                                LG_ResultSave_path = Path.GetDirectoryName(FilePath)
                                My.Settings._lgresultsave_path = LG_ResultSave_path
                            Case Else
                                SG_ResultSave_path = Path.GetDirectoryName(FilePath)
                                My.Settings._sgresultsave_path = SG_ResultSave_path
                        End Select
                        My.Settings.Save()

                        excelApp.Visible = False

                        If FlgAdmin = 0 Then
                            '通常モード時　配向角・配向比
                            With sheet1
                                .Cells.Locked = False
                                If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                                    .Cells.Interior.Color = frm_PrfForm_bc
                                End If

                                .Cells(1, 1) = My.Application.Info.ProductName & " " & LblPrfTitle.Text
                                .Range(.Cells(1, 1), .Cells(1, 1)).Font.Color = frm_PrfForm_fc
                                .Cells(2, 1) = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
                                .Range(.Cells(2, 1), .Cells(2, 1)).Font.Color = frm_PrfCurData_color
                                .Range(.Cells(1, 1), .Cells(2, 1)).Locked = True

                                .Cells(4, 2) = StrMachNo
                                .Cells(4, 3) = StrSampleName
                                If FlgDBF = 1 Then
                                    .Cells(4, 4) = StrMark
                                    .Cells(4, 5) = StrMeasNumber
                                    .Cells(4, 6) = StrMeasLot
                                    .Range(.Cells(4, 2), .Cells(4, 6)).Font.Color = frm_PrfForm_fc
                                Else
                                    .Cells(4, 4) = StrMeasNumber
                                    .Cells(4, 5) = StrMeasLot
                                    .Range(.Cells(4, 2), .Cells(4, 5)).Font.Color = frm_PrfForm_fc
                                End If
                                .Cells(5, 1) = StrMeasSpec
                                .Cells(5, 2) = TxtMachNoCur.Text
                                .Cells(5, 3) = TxtSmplNamCur.Text
                                If FlgDBF = 1 Then
                                    .Cells(5, 4) = TxtMarkCur.Text
                                    .Cells(5, 5) = TxtMeasNumCur.Text
                                    .Cells(5, 6) = TxtMeasLotCur.Text
                                    .Range(.Cells(5, 1), .Cells(5, 6)).Font.Color = frm_PrfCurData_color
                                    .Range(.Cells(4, 1), .Cells(5, 6)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(4, 1), .Cells(5, 6)).Locked = True
                                Else
                                    .Cells(5, 4) = TxtMeasNumCur.Text
                                    .Cells(5, 5) = TxtMeasLotCur.Text
                                    .Range(.Cells(5, 1), .Cells(5, 5)).Font.Color = frm_PrfCurData_color
                                    .Range(.Cells(4, 1), .Cells(5, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(4, 1), .Cells(5, 5)).Locked = True
                                End If
                                'If FlgPrnBc_enable = True Then
                                '.Range(.Cells(4, 1), .Cells(5, 5)).Interior.Color = frm_PrfGraph_bc
                                'End If
                                .Range(.Cells(7, 1), .Cells(7, 3)).MergeCells = True
                                .Cells(7, 1) = StrOrientAng
                                .Cells(8, 2) = "Peak"
                                .Cells(8, 3) = "Deep"
                                .Range(.Cells(7, 1), .Cells(8, 3)).Font.Color = frm_PrfForm_fc
                                .Cells(9, 1) = "Max."
                                .Cells(10, 1) = "Ave."
                                .Cells(11, 1) = "Min."
                                .Range(.Cells(9, 1), .Cells(11, 1)).Font.Color = frm_PrfForm_fc
                                .Cells(9, 2) = LblAnglePkMax_nom.Text
                                .Cells(10, 2) = LblAnglePkAvg_nom.Text
                                .Cells(11, 2) = LblAnglePkMin_nom.Text
                                .Cells(9, 3) = LblAngleDpMax_nom.Text
                                .Cells(10, 3) = LblAngleDpAvg_nom.Text
                                .Cells(11, 3) = LblAngleDpMin_nom.Text
                                .Range(.Cells(9, 2), .Cells(11, 3)).Font.Color = frm_PrfCurData_color
                                .Range(.Cells(7, 1), .Cells(11, 3)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(7, 1), .Cells(11, 3)).Locked = True
                                'If FlgPrnBc_enable = True Then
                                '.Range(.Cells(7, 1), .Cells(11, 3)).Interior.Color = frm_PrfGraph_bc
                                'End If
                                bmp = New Bitmap(PictureBox1.Width, PictureBox1.Height)
                                'bmp.MakeTransparent(BackColor)
                                PictureBox1.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))

                                bmp.Save(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Shapes.AddPicture(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp",
                                                   MsoTriState.msoFalse,
                                                   MsoTriState.msoTrue,
                                                   .Cells(13, 1).left,
                                                   .Cells(13, 1).top,
                                                   bmp.Width * 0.8,
                                                   bmp.Height * 0.8)

                                aa = .Cells(13, 1).top + bmp.Height * 0.8

                                bmp.Dispose()
                                File.Delete(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                i = 1
                                Do While aa > .Cells(13 + i, 1).top
                                    i += 1
                                    Console.WriteLine(.Cells(13 + i, 1).top)
                                Loop
                                ratio_top_row = 13 + i + 1

                                .Range(.Cells(ratio_top_row, 1), .Cells(ratio_top_row, 3)).MergeCells = True
                                .Cells(ratio_top_row, 1) = StrOrientRat
                                .Cells(ratio_top_row + 1, 2) = "Peak/Deep"
                                .Cells(ratio_top_row + 1, 3) = "MD/CD"
                                .Range(.Cells(ratio_top_row, 1), .Cells(ratio_top_row + 1, 3)).Font.Color = frm_PrfForm_fc
                                .Cells(ratio_top_row + 2, 1) = "Max."
                                .Cells(ratio_top_row + 3, 1) = "Ave."
                                .Cells(ratio_top_row + 4, 1) = "Min."
                                .Range(.Cells(ratio_top_row + 2, 1), .Cells(ratio_top_row + 4, 1)).Font.Color = frm_PrfForm_fc
                                .Cells(ratio_top_row + 2, 2) = LblRatioPkDpMax_nom.Text
                                .Cells(ratio_top_row + 3, 2) = LblRatioPkDpAvg_nom.Text
                                .Cells(ratio_top_row + 4, 2) = LblRatioPkDpMin_nom.Text
                                .Cells(ratio_top_row + 2, 3) = LblRatioMDCDMax_nom.Text
                                .Cells(ratio_top_row + 3, 3) = LblRatioMDCDAvg_nom.Text
                                .Cells(ratio_top_row + 4, 3) = LblRatioMDCDMin_nom.Text
                                .Range(.Cells(ratio_top_row + 2, 2), .Cells(ratio_top_row + 4, 3)).Font.Color = frm_PrfCurData_color
                                .Range(.Cells(ratio_top_row, 1), .Cells(ratio_top_row + 4, 3)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(ratio_top_row, 1), .Cells(ratio_top_row + 4, 3)).Locked = True
                                'If FlgPrnBc_enable = True Then
                                '.Range(.Cells(ratio_top_row, 1), .Cells(ratio_top_row + 4, 3)).Interior.Color = frm_PrfGraph_bc
                                'End If
                                bmp = New Bitmap(PictureBox2.Width, PictureBox2.Height)
                                'bmp.MakeTransparent(BackColor)
                                PictureBox2.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))

                                bmp.Save(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Shapes.AddPicture(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp",
                                                   MsoTriState.msoFalse,
                                                   MsoTriState.msoTrue,
                                                   .Cells(ratio_top_row + 6, 1).left,
                                                   .Cells(ratio_top_row + 6, 1).top,
                                                   bmp.Width * 0.8,
                                                   bmp.Height * 0.8)

                                bmp.Dispose()
                                File.Delete(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Protect()

                                '保存する
                                'excelApp.DisplayAlerts = False
                                'excelBook.SaveAs(FilePath)
                                'excelApp.DisplayAlerts = True
                            End With

                            With sheet2
                                .Cells.Locked = False
                                If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                                    .Cells.Interior.Color = frm_PrfForm_bc
                                End If

                                .Cells(1, 1) = My.Application.Info.ProductName & " " & LblPrfTitle.Text
                                .Range(.Cells(1, 1), .Cells(1, 1)).Font.Color = frm_PrfForm_fc
                                .Cells(2, 1) = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
                                .Range(.Cells(2, 1), .Cells(2, 1)).Font.Color = frm_PrfCurData_color
                                .Range(.Cells(1, 1), .Cells(2, 1)).Locked = True

                                .Cells(4, 2) = StrMachNo
                                .Cells(4, 3) = StrSampleName
                                If FlgDBF = 1 Then
                                    .Cells(4, 4) = StrMark
                                    .Cells(4, 5) = StrMeasNumber
                                    .Cells(4, 6) = StrMeasLot
                                    .Range(.Cells(4, 2), .Cells(4, 6)).Font.Color = frm_PrfForm_fc
                                Else
                                    .Cells(4, 4) = StrMeasNumber
                                    .Cells(4, 5) = StrMeasLot
                                    .Range(.Cells(4, 2), .Cells(4, 5)).Font.Color = frm_PrfForm_fc
                                End If
                                .Cells(5, 1) = StrMeasSpec
                                .Cells(5, 2) = TxtMachNoCur.Text
                                .Cells(5, 3) = TxtSmplNamCur.Text
                                If FlgDBF = 1 Then
                                    .Cells(5, 4) = TxtMarkCur.Text
                                    .Cells(5, 5) = TxtMeasNumCur.Text
                                    .Cells(5, 6) = TxtMeasLotCur.Text
                                    .Range(.Cells(5, 1), .Cells(5, 6)).Font.Color = frm_PrfCurData_color
                                    .Range(.Cells(4, 1), .Cells(5, 6)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(4, 1), .Cells(5, 6)).Locked = True
                                Else
                                    .Cells(5, 4) = TxtMeasNumCur.Text
                                    .Cells(5, 5) = TxtMeasLotCur.Text
                                    .Range(.Cells(5, 1), .Cells(5, 5)).Font.Color = frm_PrfCurData_color
                                    .Range(.Cells(4, 1), .Cells(5, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(4, 1), .Cells(5, 5)).Locked = True
                                End If

                                .Range(.Cells(7, 1), .Cells(7, 5)).MergeCells = True
                                .Cells(7, 1) = StrPropVelo
                                .Cells(8, 2) = "Peak"
                                .Cells(8, 3) = "Deep"
                                .Cells(8, 4) = "MD"
                                .Cells(8, 5) = "CD"
                                .Range(.Cells(7, 1), .Cells(8, 5)).Font.Color = frm_PrfForm_fc
                                .Cells(9, 1) = "Max."
                                .Cells(10, 1) = "Ave."
                                .Cells(11, 1) = "Min."
                                .Range(.Cells(9, 1), .Cells(11, 1)).Font.Color = frm_PrfForm_fc
                                .Cells(9, 2) = LblVeloPkMax_nom.Text
                                .Cells(10, 2) = LblVeloPkAvg_nom.Text
                                .Cells(11, 2) = LblVeloPkMin_nom.Text
                                .Cells(9, 3) = LblVeloDpMax_nom.Text
                                .Cells(10, 3) = LblVeloDpAvg_nom.Text
                                .Cells(11, 3) = LblVeloDpMin_nom.Text
                                .Cells(9, 4) = LblVeloMDMax_nom.Text
                                .Cells(10, 4) = LblVeloMDAvg_nom.Text
                                .Cells(11, 4) = LblVeloMDMin_nom.Text
                                .Cells(9, 5) = LblVeloCDMax_nom.Text
                                .Cells(10, 5) = LblVeloCDAvg_nom.Text
                                .Cells(11, 5) = LblVeloCDMin_nom.Text
                                .Range(.Cells(9, 2), .Cells(11, 5)).Font.Color = frm_PrfCurData_color
                                .Range(.Cells(7, 1), .Cells(11, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(7, 1), .Cells(11, 5)).Locked = True
                                'If FlgPrnBc_enable = True Then
                                '.Range(.Cells(7, 1).cells(11, 5)).Interior.Color = frm_PrfGraph_bc
                                'End If
                                bmp = New Bitmap(PictureBox3.Width, PictureBox3.Height)
                                'bmp.MakeTransparent(BackColor)
                                PictureBox3.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))

                                bmp.Save(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Shapes.AddPicture(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp",
                                                   MsoTriState.msoFalse,
                                                   MsoTriState.msoTrue,
                                                   .Cells(13, 1).left,
                                                   .Cells(13, 1).top,
                                                   bmp.Width * 0.8,
                                                   bmp.Height * 0.8)

                                aa = .Cells(13, 1).top + bmp.Height * 0.8

                                bmp.Dispose()
                                File.Delete(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                i = 1
                                Do While aa > .Cells(13 + i, 1).top
                                    i += 1
                                    Console.WriteLine(.Cells(13 + i, 1).top)
                                Loop
                                tsi_top_row = 13 + i + 1

                                .Range(.Cells(tsi_top_row, 1), .Cells(tsi_top_row, 3)).MergeCells = True
                                .Cells(tsi_top_row, 1) = "TSI(Km/S)^2"
                                .Cells(tsi_top_row + 1, 2) = "MD"
                                .Cells(tsi_top_row + 1, 3) = "CD"
                                .Range(.Cells(tsi_top_row, 1), .Cells(tsi_top_row + 1, 3)).Font.Color = frm_PrfForm_fc
                                .Cells(tsi_top_row + 2, 1) = "Max."
                                .Cells(tsi_top_row + 3, 1) = "Ave."
                                .Cells(tsi_top_row + 4, 1) = "Min."
                                .Range(.Cells(tsi_top_row + 2, 1), .Cells(tsi_top_row + 4, 1)).Font.Color = frm_PrfForm_fc
                                .Cells(tsi_top_row + 2, 2) = LblTSIMDMax_nom.Text
                                .Cells(tsi_top_row + 3, 2) = LblTSIMDAvg_nom.Text
                                .Cells(tsi_top_row + 4, 2) = LblTSIMDMin_nom.Text
                                .Cells(tsi_top_row + 2, 3) = LblTSICDMax_nom.Text
                                .Cells(tsi_top_row + 3, 3) = LblTSICDAvg_nom.Text
                                .Cells(tsi_top_row + 4, 3) = LblTSICDMin_nom.Text
                                .Range(.Cells(tsi_top_row + 2, 2), .Cells(tsi_top_row + 4, 3)).Font.Color = frm_PrfCurData_color
                                .Range(.Cells(tsi_top_row, 1), .Cells(tsi_top_row + 4, 3)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(tsi_top_row, 1), .Cells(tsi_top_row + 4, 3)).Locked = True
                                'If FlgPrnBc_enable = True Then
                                '.Range(.Cells(tsi_top_row, 1), .Cells(tsi_top_row + 4, 3)).Interior.Color = frm_PrfGraph_bc
                                'End If
                                bmp = New Bitmap(PictureBox4.Width, PictureBox4.Height)
                                'bmp.MakeTransparent(BackColor)
                                PictureBox4.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))

                                bmp.Save(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Shapes.AddPicture(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp",
                                                   MsoTriState.msoFalse,
                                                   MsoTriState.msoTrue,
                                                   .Cells(tsi_top_row + 6, 1).left,
                                                   .Cells(tsi_top_row + 6, 1).top,
                                                   bmp.Width * 0.8,
                                                   bmp.Height * 0.8)

                                bmp.Dispose()
                                File.Delete(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Protect()
                            End With

                            With sheet3
                                If SampleNo > 0 Then
                                    .Cells.Locked = False
                                    If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                                        .Cells.Interior.Color = frm_PrfForm_bc
                                    End If

                                    .Cells(1, 1) = My.Application.Info.ProductName & " " & LblPrfTitle.Text
                                    .Range(.Cells(1, 1), .Cells(1, 1)).Font.Color = frm_PrfForm_fc
                                    .Cells(2, 1) = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
                                    .Range(.Cells(2, 1), .Cells(2, 1)).Font.Color = frm_PrfCurData_color
                                    .Range(.Cells(1, 1), .Cells(2, 1)).Locked = True

                                    .Cells(4, 2) = StrMachNo
                                    .Cells(4, 3) = StrSampleName
                                    If FlgDBF = 1 Then
                                        .Cells(4, 4) = StrMark
                                        .Cells(4, 5) = StrMeasNumber
                                        .Cells(4, 6) = StrMeasLot
                                        .Range(.Cells(4, 2), .Cells(4, 6)).Font.Color = frm_PrfForm_fc
                                    Else
                                        .Cells(4, 4) = StrMeasNumber
                                        .Cells(4, 5) = StrMeasLot
                                        .Range(.Cells(4, 2), .Cells(4, 5)).Font.Color = frm_PrfForm_fc
                                    End If
                                    .Cells(5, 1) = StrMeasSpec
                                    .Cells(5, 2) = TxtMachNoCur.Text
                                    .Cells(5, 3) = TxtSmplNamCur.Text
                                    .Cells(5, 4) = TxtMarkCur.Text
                                    .Cells(5, 5) = TxtMeasNumCur.Text
                                    .Cells(5, 6) = TxtMeasLotCur.Text
                                    .Range(.Cells(5, 1), .Cells(5, 6)).Font.Color = frm_PrfCurData_color
                                    .Range(.Cells(4, 1), .Cells(5, 6)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(4, 1), .Cells(5, 6)).Locked = True
                                    'If FlgPrnBc_enable = True Then
                                    '.Range(.Cells(4, 1), .Cells(5, 5)).Interior.Color = frm_PrfGraph_bc
                                    'End If
                                    .Range(.Cells(7, 1), .Cells(8, 1)).MergeCells = True
                                    .Cells(7, 1) = "No."
                                    .Range(.Cells(7, 2), .Cells(7, 3)).MergeCells = True
                                    .Cells(7, 2) = StrOrientAng
                                    .Range(.Cells(7, 4), .Cells(7, 5)).MergeCells = True
                                    .Cells(7, 4) = StrOrientRat
                                    .Range(.Cells(7, 6), .Cells(7, 9)).MergeCells = True
                                    .Cells(7, 6) = StrPropVelo
                                    .Range(.Cells(7, 10), .Cells(7, 11)).MergeCells = True
                                    .Cells(7, 10) = "TSI(Km/S)^2"
                                    .Range(.Cells(7, 1), .Cells(7, 10)).Font.Color = frm_PrfCurData_color
                                    .Cells(8, 2) = "Peak MD+-"
                                    .Cells(8, 3) = "Deep CD+-"
                                    .Cells(8, 4) = "MD/CD"
                                    .Cells(8, 5) = "Peak/Deep"
                                    .Cells(8, 6) = "MD"
                                    .Cells(8, 7) = "CD"
                                    .Cells(8, 8) = "Peak"
                                    .Cells(8, 9) = "Deep"
                                    .Cells(8, 10) = "MD"
                                    .Cells(8, 11) = "CD"
                                    .Range(.Cells(8, 2), .Cells(8, 11)).Font.Color = frm_PrfForm_fc
                                    .Cells(9, 1) = "Max."
                                    .Cells(10, 1) = "Ave."
                                    .Cells(11, 1) = "Min."
                                    .Range(.Cells(9, 1), .Cells(11, 1)).Font.Color = frm_PrfForm_fc
                                    .Cells(9, 2) = LblAnglePkMax_TB.Text
                                    .Cells(10, 2) = LblAnglePkAvg_TB.Text
                                    .Cells(11, 2) = LblAnglePkMin_TB.Text
                                    .Cells(9, 3) = LblAngleDpMax_TB.Text
                                    .Cells(10, 3) = LblAngleDpAvg_TB.Text
                                    .Cells(11, 3) = LblAngleDpMin_TB.Text
                                    .Cells(9, 4) = LblRatioMDCDMax_TB.Text
                                    .Cells(10, 4) = LblRatioMDCDAvg_TB.Text
                                    .Cells(11, 4) = LblRatioMDCDMin_TB.Text
                                    .Cells(9, 5) = LblRatioPkDpMax_TB.Text
                                    .Cells(10, 5) = LblRatioPkDpAvg_TB.Text
                                    .Cells(11, 5) = LblRatioPkDpMin_TB.Text
                                    .Cells(9, 6) = LblVeloMDMax_TB.Text
                                    .Cells(10, 6) = LblVeloMDAvg_TB.Text
                                    .Cells(11, 6) = LblVeloMDMin_TB.Text
                                    .Cells(9, 7) = LblVeloCDMax_TB.Text
                                    .Cells(10, 7) = LblVeloCDAvg_TB.Text
                                    .Cells(11, 7) = LblVeloCDMin_TB.Text
                                    .Cells(9, 8) = LblVeloPkMax_TB.Text
                                    .Cells(10, 8) = LblVeloPkAvg_TB.Text
                                    .Cells(11, 8) = LblVeloPkMin_TB.Text
                                    .Cells(9, 9) = LblVeloDpMax_TB.Text
                                    .Cells(10, 9) = LblVeloDpAvg_TB.Text
                                    .Cells(11, 9) = LblVeloDpMin_TB.Text
                                    .Cells(9, 10) = LblTSIMDMax_TB.Text
                                    .Cells(10, 10) = LblTSIMDAvg_TB.Text
                                    .Cells(11, 10) = LblTSIMDMin_TB.Text
                                    .Cells(9, 11) = LblTSICDMax_TB.Text
                                    .Cells(10, 11) = LblTSICDAvg_TB.Text
                                    .Cells(11, 11) = LblTSICDMin_TB.Text
                                    .Range(.Cells(9, 2), .Cells(11, 11)).Font.Color = frm_PrfCurData_color

                                    For s = 1 To SampleNo
                                        For k = 0 To 10
                                            .Cells(11 + s, k + 1) = DataGridView1.Rows(s - 1).Cells(k).Value
                                            .Range(.Cells(11 + s, k + 1), .Cells(11 + s, k + 1)).Font.Color = frm_PrfCurData_color
                                        Next
                                    Next
                                    'If FlgPrnBc_enable = True Then
                                    '.Range(.Cells(7, 1), .Cells(11 + SampleNo, 11)).Interior.Color = frm_PrfGraph_bc
                                    'End If
                                    .Range(.Cells(7, 1), .Cells(11 + SampleNo, 11)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(7, 1), .Cells(11 + SampleNo, 11)).Locked = True

                                Else
                                    .Cells(1, 1) = StrNoData
                                End If

                                .Protect()
                            End With

                            With sheet4
                                If FileDataMax > 0 Then
                                    .Cells.Locked = False
                                    If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                                        .Cells.Interior.Color = frm_PrfForm_bc
                                    End If

                                    .Cells(1, 1) = My.Application.Info.ProductName & " " & LblPrfTitle.Text
                                    .Cells(2, 1) = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
                                    .Range(.Cells(2, 1), .Cells(2, 1)).Font.Color = frm_PrfOldData_color
                                    .Range(.Cells(1, 1), .Cells(2, 1)).Locked = True

                                    .Cells(4, 2) = StrMachNo
                                    .Cells(4, 3) = StrSampleName
                                    If FlgDBF = 1 Then
                                        .Cells(4, 4) = StrMark
                                        .Cells(4, 5) = StrMeasNumber
                                        .Cells(4, 6) = StrMeasLot
                                        .Range(.Cells(4, 2), .Cells(4, 6)).Font.Color = frm_PrfForm_fc
                                    Else
                                        .Cells(4, 4) = StrMeasNumber
                                        .Cells(4, 5) = StrMeasLot
                                        .Range(.Cells(4, 2), .Cells(4, 5)).Font.Color = frm_PrfForm_fc
                                    End If
                                    .Cells(5, 1) = StrMeasSpec
                                    .Cells(5, 2) = TxtMachNoCur.Text
                                    .Cells(5, 3) = TxtSmplNamCur.Text
                                    If FlgDBF = 1 Then
                                        .Cells(5, 4) = TxtMarkCur.Text
                                        .Cells(5, 5) = TxtMeasNumCur.Text
                                        .Cells(5, 6) = TxtMeasLotCur.Text
                                        .Range(.Cells(5, 1), .Cells(5, 6)).Font.Color = frm_PrfOldData_color
                                        .Range(.Cells(4, 1), .Cells(5, 6)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                        .Range(.Cells(4, 1), .Cells(5, 6)).Locked = True
                                    Else
                                        .Cells(5, 4) = TxtMeasNumCur.Text
                                        .Cells(5, 5) = TxtMeasLotCur.Text
                                        .Range(.Cells(5, 1), .Cells(5, 5)).Font.Color = frm_PrfOldData_color
                                        .Range(.Cells(4, 1), .Cells(5, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                        .Range(.Cells(4, 1), .Cells(5, 5)).Locked = True
                                    End If
                                    'If FlgPrnBc_enable = True Then
                                    '.Range(.Cells(4, 1), .Cells(5, 5)).Interior.Color = frm_PrfGraph_bc
                                    'End If
                                    .Range(.Cells(7, 1), .Cells(8, 1)).MergeCells = True
                                    .Cells(7, 1) = "No."
                                    .Range(.Cells(7, 2), .Cells(7, 3)).MergeCells = True
                                    .Cells(7, 2) = StrOrientAng
                                    .Range(.Cells(7, 4), .Cells(7, 5)).MergeCells = True
                                    .Cells(7, 4) = StrOrientRat
                                    .Range(.Cells(7, 6), .Cells(7, 9)).MergeCells = True
                                    .Cells(7, 6) = StrPropVelo
                                    .Range(.Cells(7, 10), .Cells(7, 11)).MergeCells = True
                                    .Cells(7, 10) = "TSI(Km/S)^2"
                                    .Range(.Cells(7, 1), .Cells(7, 10)).Font.Color = frm_PrfOldData_color
                                    .Cells(8, 2) = "Peak MD+-"
                                    .Cells(8, 3) = "Deep CD+-"
                                    .Cells(8, 4) = "MD/CD"
                                    .Cells(8, 5) = "Peak/Deep"
                                    .Cells(8, 6) = "MD"
                                    .Cells(8, 7) = "CD"
                                    .Cells(8, 8) = "Peak"
                                    .Cells(8, 9) = "Deep"
                                    .Cells(8, 10) = "MD"
                                    .Cells(8, 11) = "CD"
                                    .Range(.Cells(8, 2), .Cells(8, 11)).Font.Color = frm_PrfForm_fc
                                    .Cells(9, 1) = "Max."
                                    .Cells(10, 1) = "Ave."
                                    .Cells(11, 1) = "Min."
                                    .Range(.Cells(9, 1), .Cells(11, 1)).Font.Color = frm_PrfForm_fc
                                    .Cells(9, 2) = LblAnglePkMaxOld_TB
                                    .Cells(10, 2) = LblAnglePkAvgOld_TB.Text
                                    .Cells(11, 2) = LblAnglePkMinOld_TB.Text
                                    .Cells(9, 3) = LblAngleDpMaxOld_TB.Text
                                    .Cells(10, 3) = LblAngleDpAvgOld_TB.Text
                                    .Cells(11, 3) = LblAngleDpMinOld_TB.Text
                                    .Cells(9, 4) = LblRatioMDCDMaxOld_TB.Text
                                    .Cells(10, 4) = LblRatioMDCDAvgOld_TB.Text
                                    .Cells(11, 4) = LblRatioMDCDMinOld_TB.Text
                                    .Cells(9, 5) = LblRatioPkDpMaxOld_TB.Text
                                    .Cells(10, 5) = LblRatioPkDpAvgOld_TB.Text
                                    .Cells(11, 5) = LblRatioPkDpMinOld_TB.Text
                                    .Cells(9, 6) = LblVeloMDMaxOld_TB.Text
                                    .Cells(10, 6) = LblVeloMDAvgOld_TB.Text
                                    .Cells(11, 6) = LblVeloMDMinOld_TB.Text
                                    .Cells(9, 7) = LblVeloCDMaxOld_TB.Text
                                    .Cells(10, 7) = LblVeloCDAvgOld_TB.Text
                                    .Cells(11, 7) = LblVeloCDMinOld_TB.Text
                                    .Cells(9, 8) = LblVeloPkMaxOld_TB.Text
                                    .Cells(10, 8) = LblVeloPkAvgOld_TB.Text
                                    .Cells(11, 8) = LblVeloPkMinOld_TB.Text
                                    .Cells(9, 9) = LblVeloDpMaxOld_TB.Text
                                    .Cells(10, 9) = LblVeloDpAvgOld_TB.Text
                                    .Cells(11, 9) = LblVeloDpMinOld_TB.Text
                                    .Cells(9, 10) = LblTSIMDMaxOld_TB.Text
                                    .Cells(10, 10) = LblTSIMDAvgOld_TB.Text
                                    .Cells(11, 10) = LblTSIMDMinOld_TB.Text
                                    .Cells(9, 11) = LblTSICDMaxOld_TB.Text
                                    .Cells(10, 11) = LblTSICDAvgOld_TB.Text
                                    .Cells(11, 11) = LblTSICDMinOld_TB.Text
                                    .Range(.Cells(9, 2), .Cells(11, 11)).Font.Color = frm_PrfOldData_color

                                    For s = 1 To SampleNo
                                        For k = 0 To 10
                                            .Cells(11 + s, k + 1) = DataGridView2.Rows(s - 1).Cells(k).Value
                                            .Range(.Cells(11 + s, k + 1), .Cells(11 + s, k + 1)).Font.Color = frm_PrfOldData_color
                                        Next
                                    Next
                                    'If FlgPrnBc_enable = True Then
                                    '.Range(.Cells(7, 1), .Cells(11 + SampleNo, 11)).Interior.Color = frm_PrfGraph_bc
                                    'End If
                                    .Range(.Cells(7, 1), .Cells(11 + SampleNo, 11)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(7, 1), .Cells(11 + SampleNo, 11)).Locked = True
                                Else
                                    .Cells(1, 1) = StrNoData
                                End If

                                .Protect()
                            End With

                            With sheet5
                                If FlgAvg > 0 Then
                                    .Cells.Locked = False
                                    If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                                        .Cells.Interior.Color = frm_PrfForm_bc
                                    End If

                                    .Cells(1, 1) = My.Application.Info.ProductName & " " & LblPrfTitle.Text
                                    .Cells(2, 1) = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
                                    .Range(.Cells(2, 1), .Cells(2, 1)).Font.Color = frm_PrfAvgData_color
                                    .Range(.Cells(1, 1), .Cells(2, 1)).Locked = True

                                    .Cells(4, 2) = StrMachNo
                                    .Cells(4, 3) = StrSampleName
                                    If FlgDBF = 1 Then
                                        .Cells(4, 4) = StrMark
                                        .Cells(4, 5) = StrMeasNumber
                                        .Cells(4, 6) = StrMeasLot
                                        .Range(.Cells(4, 2), .Cells(4, 6)).Font.Color = frm_PrfForm_fc
                                    Else
                                        .Cells(4, 4) = StrMeasNumber
                                        .Cells(4, 5) = StrMeasLot
                                        .Range(.Cells(4, 2), .Cells(4, 5)).Font.Color = frm_PrfForm_fc
                                    End If
                                    .Cells(5, 1) = StrMeasSpec
                                    .Cells(5, 2) = TxtMachNoCur.Text
                                    .Cells(5, 3) = TxtSmplNamCur.Text
                                    If FlgDBF = 1 Then
                                        .Cells(5, 4) = TxtMarkCur.Text
                                        .Cells(5, 5) = TxtMeasNumCur.Text
                                        .Cells(5, 6) = TxtMeasLotCur.Text
                                        .Range(.Cells(5, 1), .Cells(5, 6)).Font.Color = frm_PrfAvgData_color
                                        .Range(.Cells(4, 1), .Cells(5, 6)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                        .Range(.Cells(4, 1), .Cells(5, 6)).Locked = True
                                    Else
                                        .Cells(5, 4) = TxtMeasNumCur.Text
                                        .Cells(5, 5) = TxtMeasLotCur.Text
                                        .Range(.Cells(5, 1), .Cells(5, 5)).Font.Color = frm_PrfAvgData_color
                                        .Range(.Cells(4, 1), .Cells(5, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                        .Range(.Cells(4, 1), .Cells(5, 5)).Locked = True
                                    End If
                                    'If FlgPrnBc_enable = True Then
                                    '.Range(.Cells(4, 1), .Cells(5, 5)).Interior.Color = frm_PrfGraph_bc
                                    'End If
                                    .Range(.Cells(7, 1), .Cells(8, 1)).MergeCells = True
                                    .Cells(7, 1) = "No."
                                    .Range(.Cells(7, 2), .Cells(7, 3)).MergeCells = True
                                    .Cells(7, 2) = StrOrientAng
                                    .Range(.Cells(7, 4), .Cells(7, 5)).MergeCells = True
                                    .Cells(7, 4) = StrOrientRat
                                    .Range(.Cells(7, 6), .Cells(7, 9)).MergeCells = True
                                    .Cells(7, 6) = StrPropVelo
                                    .Range(.Cells(7, 10), .Cells(7, 11)).MergeCells = True
                                    .Cells(7, 10) = "TSI(Km/S)^2"
                                    .Range(.Cells(7, 1), .Cells(7, 10)).Font.Color = frm_PrfAvgData_color
                                    .Cells(8, 2) = "Peak MD+-"
                                    .Cells(8, 3) = "Deep CD+-"
                                    .Cells(8, 4) = "MD/CD"
                                    .Cells(8, 5) = "Peak/Deep"
                                    .Cells(8, 6) = "MD"
                                    .Cells(8, 7) = "CD"
                                    .Cells(8, 8) = "Peak"
                                    .Cells(8, 9) = "Deep"
                                    .Cells(8, 10) = "MD"
                                    .Cells(8, 11) = "CD"
                                    .Range(.Cells(8, 2), .Cells(8, 11)).Font.Color = frm_PrfForm_fc
                                    .Cells(9, 1) = "Max."
                                    .Cells(10, 1) = "Ave."
                                    .Cells(11, 1) = "Min."
                                    .Range(.Cells(9, 1), .Cells(11, 1)).Font.Color = frm_PrfForm_fc
                                    .Cells(9, 2) = LblAnglePkMaxAvg_TB.Text
                                    .Cells(10, 2) = LblAnglePkAvgAvg_TB.Text
                                    .Cells(11, 2) = LblAnglePkMinAvg_TB.Text
                                    .Cells(9, 3) = LblAngleDpMaxAvg_TB.Text
                                    .Cells(10, 3) = LblAngleDpAvgAvg_TB.Text
                                    .Cells(11, 3) = LblAngleDpMinAvg_TB.Text
                                    .Cells(9, 4) = LblRatioMDCDMaxAvg_TB.Text
                                    .Cells(10, 4) = LblRatioMDCDAvgAvg_TB.Text
                                    .Cells(11, 4) = LblRatioMDCDMinAvg_TB.Text
                                    .Cells(9, 5) = LblRatioPkDpMaxAvg_TB.Text
                                    .Cells(10, 5) = LblRatioPkDpAvgAvg_TB.Text
                                    .Cells(11, 5) = LblRatioPkDpMinAvg_TB.Text
                                    .Cells(9, 6) = LblVeloMDMaxAvg_TB.Text
                                    .Cells(10, 6) = LblVeloMDAvgAvg_TB.Text
                                    .Cells(11, 6) = LblVeloMDMinAvg_TB.Text
                                    .Cells(9, 7) = LblVeloCDMaxAvg_TB.Text
                                    .Cells(10, 7) = LblVeloCDAvgAvg_TB.Text
                                    .Cells(11, 7) = LblVeloCDMinAvg_TB.Text
                                    .Cells(9, 8) = LblVeloPkMaxAvg_TB.Text
                                    .Cells(10, 8) = LblVeloPkAvgAvg_TB.Text
                                    .Cells(11, 8) = LblVeloPkMinAvg_TB.Text
                                    .Cells(9, 9) = LblVeloDpMaxAvg_TB.Text
                                    .Cells(10, 9) = LblVeloDpAvgAvg_TB.Text
                                    .Cells(11, 9) = LblVeloDpMinAvg_TB.Text
                                    .Cells(9, 10) = LblTSIMDMaxAvg_TB.Text
                                    .Cells(10, 10) = LblTSIMDAvgAvg_TB.Text
                                    .Cells(11, 10) = LblTSIMDMinAvg_TB.Text
                                    .Cells(9, 11) = LblTSICDMaxAvg_TB.Text
                                    .Cells(10, 11) = LblTSICDAvgAvg_TB.Text
                                    .Cells(11, 11) = LblTSICDMinAvg_TB.Text
                                    .Range(.Cells(9, 2), .Cells(11, 11)).Font.Color = frm_PrfAvgData_color

                                    For s = 1 To SampleNo
                                        For k = 0 To 10
                                            .Cells(11 + s, k + 1) = DataGridView3.Rows(s - 1).Cells(k).Value
                                            .Range(.Cells(11 + s, k + 1), .Cells(11 + s, k + 1)).Font.Color = frm_PrfAvgData_color
                                        Next
                                    Next
                                    'If FlgPrnBc_enable = True Then
                                    '.Range(.Cells(7, 1), .Cells(11 + SampleNo, 11)).Interior.Color = frm_PrfGraph_bc
                                    'End If
                                    .Range(.Cells(7, 1), .Cells(11 + SampleNo, 11)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(7, 1), .Cells(11 + SampleNo, 11)).Locked = True
                                Else
                                    .Cells(1, 1) = StrNoData
                                End If

                                .Protect()
                            End With
                        Else
                            '管理者モード時
                            With sheet1
                                .Cells.Locked = False
                                If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                                    .Cells.Interior.Color = frm_PrfForm_bc
                                End If

                                .Cells(1, 1) = My.Application.Info.ProductName & " " & LblPrfTitle.Text
                                .Cells(1, 1).Font.Color = frm_PrfForm_fc
                                .Cells(2, 1) = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
                                .Cells(2, 1).Font.Color = frm_PrfCurData_color
                                .Cells(3, 1) = StrPastDataDate & DataDate_bak & StrMeasTime & DataTime_bak
                                .Cells(3, 1).Font.Color = frm_PrfOldData_color
                                .Range(.Cells(1, 1), .Cells(3, 1)).Locked = True

                                .Cells(5, 2) = StrMachNo
                                .Cells(5, 3) = StrSampleName
                                If FlgDBF = 1 Then
                                    .Cells(5, 4) = StrMark
                                    .Cells(5, 5) = StrMeasNumber
                                    .Cells(5, 6) = StrMeasLot
                                    .Range(.Cells(5, 2), .Cells(5, 6)).Font.Color = frm_PrfForm_fc
                                Else
                                    .Cells(5, 4) = StrMeasNumber
                                    .Cells(5, 5) = StrMeasLot
                                    .Range(.Cells(5, 2), .Cells(5, 5)).Font.Color = frm_PrfForm_fc
                                End If
                                .Cells(6, 1) = StrMeasSpec
                                .Cells(6, 2) = TxtMachNoCur.Text
                                .Cells(6, 3) = TxtSmplNamCur.Text
                                If FlgDBF = 1 Then
                                    .Cells(6, 4) = TxtMarkCur.Text
                                    .Cells(6, 5) = TxtMeasNumCur.Text
                                    .Cells(6, 6) = TxtMeasLotCur.Text
                                    .Range(.Cells(6, 1), .Cells(6, 6)).Font.Color = frm_PrfCurData_color
                                Else
                                    .Cells(6, 4) = TxtMeasNumCur.Text
                                    .Cells(6, 5) = TxtMeasLotCur.Text
                                    .Range(.Cells(6, 1), .Cells(6, 5)).Font.Color = frm_PrfCurData_color
                                End If
                                .Cells(7, 1) = StrPastMeasSpec
                                .Cells(7, 2) = TxtMachNoBak.Text
                                .Cells(7, 3) = TxtSmplNamBak.Text
                                If FlgDBF = 1 Then
                                    .Cells(7, 4) = TxtMarkBak.Text
                                    .Cells(7, 5) = TxtMeasNumBak.Text
                                    .Cells(7, 6) = TxtMeasLotBak.Text
                                    .Range(.Cells(7, 1), .Cells(7, 6)).Font.Color = frm_PrfOldData_color
                                    .Range(.Cells(5, 1), .Cells(7, 6)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(5, 1), .Cells(7, 6)).Locked = True
                                Else
                                    .Cells(7, 4) = TxtMeasNumBak.Text
                                    .Cells(7, 5) = TxtMeasLotBak.Text
                                    .Range(.Cells(7, 1), .Cells(7, 5)).Font.Color = frm_PrfOldData_color
                                    .Range(.Cells(5, 1), .Cells(7, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(5, 1), .Cells(7, 5)).Locked = True
                                End If
                                'If FlgPrnBc_enable = True Then
                                '.Range(.Cells(5, 1), .Cells(7, 5)).Interior.Color = frm_PrfGraph_bc
                                'End If
                                '配向角データ グラフ
                                .Range(.Cells(9, 1), .Cells(9, 5)).MergeCells = True
                                .Cells(9, 1) = StrOrientAng
                                .Cells(9, 1).font.color = frm_PrfForm_fc
                                .Range(.Cells(10, 2), .Cells(10, 3)).MergeCells = True
                                .Cells(10, 2) = "Peak"
                                .Cells(10, 2).font.color = frm_PrfForm_fc
                                .Range(.Cells(10, 4), .Cells(10, 5)).MergeCells = True
                                .Cells(10, 4) = "Deep"
                                .Cells(10, 2).font.color = frm_PrfForm_fc
                                .Cells(12, 1) = "Max."
                                .Cells(13, 1) = "Ave."
                                .Cells(14, 1) = "Min."
                                .Range(.Cells(12, 1), .Cells(14, 1)).Font.Color = frm_PrfForm_fc
                                .Cells(11, 2) = StrCurData
                                .Cells(12, 2) = LblAnglePkMaxCur_adm.Text
                                .Cells(13, 2) = LblAnglePkAvgCur_adm.Text
                                .Cells(14, 2) = LblAnglePkMinCur_adm.Text
                                .Range(.Cells(11, 2), .Cells(14, 2)).Font.Color = frm_PrfCurData_color
                                .Cells(11, 3) = StrPastData
                                .Cells(12, 3) = LblAnglePkMaxBak_adm.Text
                                .Cells(13, 3) = LblAnglePkAvgBak_adm.Text
                                .Cells(14, 3) = LblAnglePkMinBak_adm.Text
                                .Range(.Cells(11, 3), .Cells(14, 3)).Font.Color = frm_PrfOldData_color
                                .Cells(11, 4) = StrCurData
                                .Cells(12, 4) = LblAngleDpMaxCur_adm.Text
                                .Cells(13, 4) = LblAngleDpAvgCur_adm.Text
                                .Cells(14, 4) = LblAngleDpMinCur_adm.Text
                                .Range(.Cells(11, 4), .Cells(14, 4)).Font.Color = frm_PrfCurData_color
                                .Cells(11, 5) = StrPastData
                                .Cells(12, 5) = LblAngleDpMaxBak_adm.Text
                                .Cells(13, 5) = LblAngleDpAvgBak_adm.Text
                                .Cells(14, 5) = LblAngleDpMinBak_adm.Text
                                .Range(.Cells(11, 5), .Cells(14, 5)).Font.Color = frm_PrfOldData_color
                                .Range(.Cells(9, 1), .Cells(14, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(9, 1), .Cells(14, 5)).Locked = True
                                'If FlgPrnBc_enable = True Then
                                '.Range(.Cells(9, 1), .Cells(14, 5)).Interior.Color = frm_PrfGraph_bc
                                'End If
                                bmp = New Bitmap(PictureBox1.Width, PictureBox1.Height)
                                'bmp.MakeTransparent(BackColor)
                                PictureBox1.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))

                                bmp.Save(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Shapes.AddPicture(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp",
                                                   MsoTriState.msoFalse,
                                                   MsoTriState.msoTrue,
                                                   .Cells(16, 1).left,
                                                   .Cells(16, 1).top,
                                                   bmp.Width * 0.8,
                                                   bmp.Height * 0.8)

                                aa = .Cells(16, 1).top + bmp.Height * 0.8

                                bmp.Dispose()
                                File.Delete(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                i = 1
                                Do While aa > .Cells(16 + i, 1).top
                                    i += 1
                                Loop
                                ratio_top_row = 16 + i + 1

                                .Range(.Cells(ratio_top_row, 1), .Cells(ratio_top_row, 5)).MergeCells = True
                                .Cells(ratio_top_row, 1) = StrOrientRat
                                .Cells(ratio_top_row, 1).font.color = frm_PrfForm_fc
                                .Range(.Cells(ratio_top_row + 1, 2), .Cells(ratio_top_row + 1, 3)).MergeCells = True
                                .Cells(ratio_top_row + 1, 2) = "Peak/Deep"
                                .Cells(ratio_top_row + 1, 2).font.color = frm_PrfForm_fc
                                .Range(.Cells(ratio_top_row + 1, 4), .Cells(ratio_top_row + 1, 5)).MergeCells = True
                                .Cells(ratio_top_row + 1, 4) = "MD/CD"
                                .Cells(ratio_top_row + 1, 4).font.color = frm_PrfForm_fc
                                .Cells(ratio_top_row + 3, 1) = "Max."
                                .Cells(ratio_top_row + 4, 1) = "Ave."
                                .Cells(ratio_top_row + 5, 1) = "Min."
                                .Range(.Cells(ratio_top_row + 3, 1), .Cells(ratio_top_row + 5, 1)).Font.Color = frm_PrfForm_fc
                                .Cells(ratio_top_row + 2, 2) = StrCurData
                                .Cells(ratio_top_row + 3, 2) = LblRatioPkDpMaxCur_adm.Text
                                .Cells(ratio_top_row + 4, 2) = LblRatioPkDpAvgCur_adm.Text
                                .Cells(ratio_top_row + 5, 2) = LblRatioPkDpMinCur_adm.Text
                                .Range(.Cells(ratio_top_row + 2, 2), .Cells(ratio_top_row + 5, 2)).Font.Color = frm_PrfCurData_color
                                .Cells(ratio_top_row + 2, 3) = StrPastData
                                .Cells(ratio_top_row + 3, 3) = LblRatioPkDpMaxBak_adm.Text
                                .Cells(ratio_top_row + 4, 3) = LblRatioPkDpAvgBak_adm.Text
                                .Cells(ratio_top_row + 5, 3) = LblRatioPkDpMinBak_adm.Text
                                .Range(.Cells(ratio_top_row + 2, 3), .Cells(ratio_top_row + 5, 3)).Font.Color = frm_PrfOldData_color
                                .Cells(ratio_top_row + 2, 4) = StrCurData
                                .Cells(ratio_top_row + 3, 4) = LblRatioMDCDMaxCur_adm.Text
                                .Cells(ratio_top_row + 4, 4) = LblRatioMDCDAvgCur_adm.Text
                                .Cells(ratio_top_row + 5, 4) = LblRatioMDCDMinCur_adm.Text
                                .Range(.Cells(ratio_top_row + 2, 4), .Cells(ratio_top_row + 5, 4)).Font.Color = frm_PrfCurData_color
                                .Cells(ratio_top_row + 2, 5) = StrPastData
                                .Cells(ratio_top_row + 3, 5) = LblRatioMDCDMaxBak_adm.Text
                                .Cells(ratio_top_row + 4, 5) = LblRatioMDCDAvgBak_adm.Text
                                .Cells(ratio_top_row + 5, 5) = LblRatioMDCDMinBak_adm.Text
                                .Range(.Cells(ratio_top_row + 2, 5), .Cells(ratio_top_row + 5, 5)).Font.Color = frm_PrfOldData_color

                                .Range(.Cells(ratio_top_row, 1), .Cells(ratio_top_row + 5, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(ratio_top_row, 1), .Cells(ratio_top_row + 5, 5)).Locked = True
                                'If FlgPrnBc_enable = True Then
                                '.Range(.Cells(ratio_top_row, 1), .Cells(ratio_top_row + 5, 5)).Interior.Color = frm_PrfGraph_bc
                                'End If
                                bmp = New Bitmap(PictureBox2.Width, PictureBox2.Height)
                                'bmp.MakeTransparent(BackColor)
                                PictureBox2.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))

                                bmp.Save(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Shapes.AddPicture(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp",
                                                   MsoTriState.msoFalse,
                                                   MsoTriState.msoTrue,
                                                   .Cells(ratio_top_row + 7, 1).left,
                                                   .Cells(ratio_top_row + 7, 1).top,
                                                   bmp.Width * 0.8,
                                                   bmp.Height * 0.8)

                                bmp.Dispose()
                                File.Delete(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Protect()
                            End With

                            With sheet2
                                .Cells.Locked = False
                                If frm_MeasForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                                    .Cells.Interior.Color = frm_MeasForm_bc
                                End If

                                .Cells(1, 1) = My.Application.Info.ProductName & " " & LblPrfTitle.Text
                                .Cells(1, 1).Font.Color = frm_PrfForm_fc
                                .Cells(2, 1) = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
                                .Cells(2, 1).Font.Color = frm_PrfCurData_color
                                .Cells(3, 1) = StrPastDataDate & DataDate_bak & StrMeasTime & DataTime_bak
                                .Cells(3, 1).Font.Color = frm_PrfOldData_color
                                .Range(.Cells(1, 1), .Cells(3, 1)).Locked = True
                                .Cells(5, 2) = StrMachNo
                                .Cells(5, 3) = StrSampleName
                                If FlgDBF = 1 Then
                                    .Cells(5, 4) = StrMark
                                    .Cells(5, 5) = StrMeasNumber
                                    .Cells(5, 6) = StrMeasLot
                                    .Range(.Cells(5, 2), .Cells(5, 6)).Font.Color = frm_PrfForm_fc
                                Else
                                    .Cells(5, 4) = StrMeasNumber
                                    .Cells(5, 5) = StrMeasLot
                                    .Range(.Cells(5, 2), .Cells(5, 5)).Font.Color = frm_PrfForm_fc
                                End If
                                .Cells(6, 1) = StrMeasSpec
                                .Cells(6, 2) = TxtMachNoCur.Text
                                .Cells(6, 3) = TxtSmplNamCur.Text
                                If FlgDBF = 1 Then
                                    .Cells(6, 4) = TxtMarkCur.Text
                                    .Cells(6, 5) = TxtMeasNumCur.Text
                                    .Cells(6, 6) = TxtMeasLotCur.Text
                                    .Range(.Cells(6, 1), .Cells(6, 6)).Font.Color = frm_PrfCurData_color
                                Else
                                    .Cells(6, 4) = TxtMeasNumCur.Text
                                    .Cells(6, 5) = TxtMeasLotCur.Text
                                    .Range(.Cells(6, 1), .Cells(6, 5)).Font.Color = frm_PrfCurData_color
                                End If
                                .Cells(7, 1) = StrPastMeasSpec
                                .Cells(7, 2) = TxtMachNoBak.Text
                                .Cells(7, 3) = TxtSmplNamBak.Text
                                .Cells(7, 4) = TxtMarkBak.Text
                                .Cells(7, 5) = TxtMeasNumBak.Text
                                .Cells(7, 6) = TxtMeasLotBak.Text
                                .Range(.Cells(7, 1), .Cells(7, 6)).Font.Color = frm_PrfOldData_color
                                .Range(.Cells(5, 1), .Cells(7, 6)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(5, 1), .Cells(7, 6)).Locked = True
                                'If FlgPrnBc_enable = True Then
                                '.Range(.Cells(5, 1), .Cells(7, 5)).Interior.Color = frm_PrfGraph_bc
                                'End If
                                '配向角データ　グラフ
                                .Range(.Cells(9, 1), .Cells(9, 9)).MergeCells = True
                                .Cells(9, 1) = StrPropVelo
                                .Cells(9, 1).font.color = frm_PrfForm_fc
                                .Range(.Cells(10, 2), .Cells(10, 3)).MergeCells = True
                                .Cells(10, 2) = "Peak"
                                .Cells(10, 2).font.color = frm_PrfForm_fc
                                .Range(.Cells(10, 4), .Cells(10, 5)).MergeCells = True
                                .Cells(10, 4) = "Deep"
                                .Cells(10, 4).font.color = frm_PrfForm_fc
                                .Range(.Cells(10, 6), .Cells(10, 7)).MergeCells = True
                                .Cells(10, 6) = "MD"
                                .Cells(10, 6).font.color = frm_PrfForm_fc
                                .Range(.Cells(10, 8), .Cells(10, 9)).MergeCells = True
                                .Cells(10, 8) = "CD"
                                .Cells(10, 8).font.color = frm_PrfForm_fc
                                .Cells(12, 1) = "Max."
                                .Cells(13, 1) = "Ave."
                                .Cells(14, 1) = "Min."
                                .Range(.Cells(12, 1), .Cells(14, 1)).Font.Color = frm_PrfForm_fc
                                .Cells(11, 2) = StrCurData
                                .Cells(12, 2) = LblVeloPkMaxCur_adm.Text
                                .Cells(13, 2) = LblVeloPkAvgCur_adm.Text
                                .Cells(14, 2) = LblVeloPkMinCur_adm.Text
                                .Range(.Cells(11, 2), .Cells(14, 2)).Font.Color = frm_PrfCurData_color
                                .Cells(11, 3) = StrPastData
                                .Cells(12, 3) = LblVeloPkMaxBak_adm.Text
                                .Cells(13, 3) = LblVeloPkAvgBak_adm.Text
                                .Cells(14, 3) = LblVeloPkMinBak_adm.Text
                                .Range(.Cells(11, 3), .Cells(14, 3)).Font.Color = frm_PrfCurData_color
                                .Cells(11, 4) = StrCurData
                                .Cells(12, 4) = LblVeloDpMaxCur_adm.Text
                                .Cells(13, 4) = LblVeloDpAvgCur_adm.Text
                                .Cells(14, 4) = LblVeloDpMinCur_adm.Text
                                .Range(.Cells(11, 4), .Cells(14, 4)).Font.Color = frm_PrfCurData_color
                                .Cells(11, 5) = StrPastData
                                .Cells(12, 5) = LblVeloDpMaxBak_adm.Text
                                .Cells(13, 5) = LblVeloDpAvgBak_adm.Text
                                .Cells(14, 5) = LblVeloDpMinBak_adm.Text
                                .Range(.Cells(11, 5), .Cells(14, 5)).Font.Color = frm_PrfCurData_color
                                .Cells(11, 6) = StrCurData
                                .Cells(12, 6) = LblVeloMDMaxCur_adm.Text
                                .Cells(13, 6) = LblVeloMDAvgCur_adm.Text
                                .Cells(14, 6) = LblVeloMDMinCur_adm.Text
                                .Range(.Cells(11, 6), .Cells(14, 6)).Font.Color = frm_PrfCurData_color
                                .Cells(11, 7) = StrPastData
                                .Cells(12, 7) = LblVeloMDMaxBak_adm.Text
                                .Cells(13, 7) = LblVeloMDAvgBak_adm.Text
                                .Cells(14, 7) = LblVeloMDMinBak_adm.Text
                                .Range(.Cells(11, 7), .Cells(14, 7)).Font.Color = frm_PrfCurData_color
                                .Cells(11, 8) = StrCurData
                                .Cells(12, 8) = LblVeloCDMaxCur_adm.Text
                                .Cells(13, 8) = LblVeloCDAvgCur_adm.Text
                                .Cells(14, 8) = LblVeloCDMinCur_adm.Text
                                .Range(.Cells(11, 8), .Cells(14, 8)).Font.Color = frm_PrfCurData_color
                                .Cells(11, 9) = StrPastData
                                .Cells(12, 9) = LblVeloCDMaxBak_adm.Text
                                .Cells(13, 9) = LblVeloCDAvgBak_adm.Text
                                .Cells(14, 9) = LblVeloCDMinBak_adm.Text
                                .Range(.Cells(11, 9), .Cells(14, 9)).Font.Color = frm_PrfCurData_color

                                .Range(.Cells(9, 1), .Cells(14, 9)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(9, 1), .Cells(14, 9)).Locked = True
                                'If FlgPrnBc_enable = True Then
                                '.Range(.Cells(9, 1), .Cells(14, 9)).Interior.Color = frm_PrfGraph_bc
                                'End If
                                bmp = New Bitmap(PictureBox3.Width, PictureBox3.Height)
                                'bmp.MakeTransparent(BackColor)
                                PictureBox3.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))

                                bmp.Save(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Shapes.AddPicture(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp",
                                                   MsoTriState.msoFalse,
                                                   MsoTriState.msoTrue,
                                                   .Cells(16, 1).Left,
                                                   .Cells(16, 1).Top,
                                                   bmp.Width * 0.8,
                                                   bmp.Height * 0.8)

                                aa = .Cells(16, 1).top + bmp.Height * 0.8

                                bmp.Dispose()
                                File.Delete(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                i = 1
                                Do While aa > .Cells(16 + i, 1).top
                                    i += 1
                                    Console.WriteLine(.Cells(16 + i, 1).top)
                                Loop
                                tsi_top_row = 16 + i + 1

                                .Range(.Cells(tsi_top_row, 1), .Cells(tsi_top_row, 5)).MergeCells = True
                                .Cells(tsi_top_row, 1) = "TSI(Km/S)^2"
                                .Cells(tsi_top_row, 1).font.color = frm_PrfForm_fc
                                .Range(.Cells(tsi_top_row + 1, 2), .Cells(tsi_top_row + 1, 3)).MergeCells = True
                                .Cells(tsi_top_row + 1, 2) = "MD"
                                .Cells(tsi_top_row + 1, 2).font.color = frm_PrfForm_fc
                                .Range(.Cells(tsi_top_row + 1, 4), .Cells(tsi_top_row + 1, 5)).MergeCells = True
                                .Cells(tsi_top_row + 1, 4) = "CD"
                                .Cells(tsi_top_row + 1, 4).font.color = frm_PrfForm_fc
                                .Cells(tsi_top_row + 3, 1) = "Max."
                                .Cells(tsi_top_row + 4, 1) = "Ave."
                                .Cells(tsi_top_row + 5, 1) = "Min."
                                .Range(.Cells(tsi_top_row + 3, 1), .Cells(tsi_top_row + 5, 1)).Font.Color = frm_PrfForm_fc
                                .Cells(tsi_top_row + 2, 2) = StrCurData
                                .Cells(tsi_top_row + 3, 2) = LblTSIMDMaxCur_adm.Text
                                .Cells(tsi_top_row + 4, 2) = LblTSIMDAvgCur_adm.Text
                                .Cells(tsi_top_row + 5, 2) = LblTSIMDMinCur_adm.Text
                                .Range(.Cells(tsi_top_row + 2, 2), .Cells(tsi_top_row + 5, 2)).Font.Color = frm_PrfCurData_color
                                .Cells(tsi_top_row + 2, 3) = StrPastData
                                .Cells(tsi_top_row + 3, 3) = LblTSIMDMaxBak_adm.Text
                                .Cells(tsi_top_row + 4, 3) = LblTSIMDAvgBak_adm.Text
                                .Cells(tsi_top_row + 5, 3) = LblTSIMDAvgBak_adm.Text
                                .Range(.Cells(tsi_top_row + 2, 3), .Cells(tsi_top_row + 5, 3)).Font.Color = frm_PrfOldData_color
                                .Cells(tsi_top_row + 2, 4) = StrCurData
                                .Cells(tsi_top_row + 3, 4) = LblTSICDMaxCur_adm.Text
                                .Cells(tsi_top_row + 4, 4) = LblTSICDAvgCur_adm.Text
                                .Cells(tsi_top_row + 5, 4) = LblTSICDMinCur_adm.Text
                                .Range(.Cells(tsi_top_row + 2, 4), .Cells(tsi_top_row + 5, 4)).Font.Color = frm_PrfCurData_color
                                .Cells(tsi_top_row + 2, 5) = StrPastData
                                .Cells(tsi_top_row + 3, 5) = LblTSICDMaxBak_adm.Text
                                .Cells(tsi_top_row + 4, 5) = LblTSICDAvgBak_adm.Text
                                .Cells(tsi_top_row + 5, 5) = LblTSICDAvgBak_adm.Text
                                .Range(.Cells(tsi_top_row + 2, 5), .Cells(tsi_top_row + 5, 5)).Font.Color = frm_PrfOldData_color

                                .Range(.Cells(tsi_top_row + 1, 1), .Cells(tsi_top_row + 1 + 4, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                .Range(.Cells(tsi_top_row + 1, 1), .Cells(tsi_top_row + 1 + 4, 5)).Locked = True
                                'If FlgPrnBc_enable = True Then
                                '.Range(.Cells(tsi_top_row + 1, 1), .Cells(tsi_top_row + 1 + 4, 5)).Interior.Color = frm_PrfGraph_bc
                                'End If
                                bmp = New Bitmap(PictureBox4.Width, PictureBox4.Height)
                                'bmp.MakeTransparent(BackColor)
                                PictureBox4.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))

                                bmp.Save(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Shapes.AddPicture(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp",
                                                   MsoTriState.msoFalse,
                                                   MsoTriState.msoTrue,
                                                   .Cells(tsi_top_row + 7, 1).left,
                                                   .Cells(tsi_top_row + 7, 1).top,
                                                   bmp.Width * 0.8,
                                                   bmp.Height * 0.8)

                                bmp.Dispose()
                                File.Delete(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                bmp = New Bitmap(PictureBox1.Width, PictureBox1.Height)
                                'bmp.MakeTransparent(BackColor)
                                PictureBox1.DrawToBitmap(bmp, New Rectangle(0, 0, bmp.Width, bmp.Height))

                                bmp.Save(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Shapes.AddPicture(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp",
                                                   MsoTriState.msoFalse,
                                                   MsoTriState.msoTrue,
                                                   .Cells(50, 1).left,
                                                   .Cells(50, 1).top,
                                                   bmp.Width * 0.8,
                                                   bmp.Height * 0.8)

                                aa = .Cells(50, 1).top + bmp.Height * 0.8

                                bmp.Dispose()
                                File.Delete(cur_dir & DEF_RESULT_FILE_FLD & "\xxx.bmp")

                                .Protect()

                            End With

                            With sheet3
                                If SampleNo > 0 Then
                                    .Cells.Locked = False
                                    If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                                        .Cells.Interior.Color = frm_PrfForm_bc
                                    End If

                                    .Cells(1, 1) = My.Application.Info.ProductName & " " & LblPrfTitle.Text
                                    .Cells(1, 1).font.color = frm_PrfForm_fc
                                    .Cells(2, 1) = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
                                    .Cells(2, 1).font.color = frm_PrfCurData_color
                                    .Cells(3, 1) = StrPastDataDate & DataDate_bak & StrMeasTime & DataTime_bak
                                    .Cells(3, 1).font.color = frm_PrfOldData_color
                                    .Range(.Cells(1, 1), .Cells(3, 1)).Locked = True

                                    .Cells(5, 2) = StrMachNo
                                    .Cells(5, 3) = StrSampleName
                                    If FlgDBF = 1 Then
                                        .Cells(5, 4) = StrMark
                                        .Cells(5, 5) = StrMeasNumber
                                        .Cells(5, 6) = StrMeasLot
                                        .Range(.Cells(5, 2), .Cells(5, 6)).Font.Color = frm_PrfForm_fc
                                    Else
                                        .Cells(5, 4) = StrMeasNumber
                                        .Cells(5, 5) = StrMeasLot
                                        .Range(.Cells(5, 2), .Cells(5, 5)).Font.Color = frm_PrfForm_fc
                                    End If
                                    .Cells(6, 1) = StrMeasSpec
                                    .Cells(6, 2) = TxtMachNoCur.Text
                                    .Cells(6, 3) = TxtSmplNamCur.Text
                                    If FlgDBF = 1 Then
                                        .Cells(5, 4) = TxtMarkCur.Text
                                        .Cells(6, 5) = TxtMeasNumCur.Text
                                        .Cells(6, 6) = TxtMeasLotCur.Text
                                        .Range(.Cells(6, 1), .Cells(6, 6)).Font.Color = frm_PrfCurData_color
                                    Else
                                        .Cells(6, 4) = TxtMeasNumCur.Text
                                        .Cells(6, 5) = TxtMeasLotCur.Text
                                        .Range(.Cells(6, 1), .Cells(6, 5)).Font.Color = frm_PrfCurData_color
                                    End If
                                    .Cells(7, 1) = StrPastMeasSpec
                                    .Cells(7, 2) = TxtMachNoBak.Text
                                    .Cells(7, 3) = TxtSmplNamBak.Text
                                    If FlgDBF = 1 Then
                                        .Cells(7, 4) = TxtMarkBak.Text
                                        .Cells(7, 5) = TxtMeasNumBak.Text
                                        .Cells(7, 6) = TxtMeasLotBak.Text
                                        .Range(.Cells(7, 1), .Cells(7, 6)).Font.Color = frm_PrfOldData_color
                                        .Range(.Cells(5, 1), .Cells(7, 6)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                        .Range(.Cells(5, 1), .Cells(7, 6)).Locked = True
                                    Else
                                        .Cells(7, 4) = TxtMeasNumBak.Text
                                        .Cells(7, 5) = TxtMeasLotBak.Text
                                        .Range(.Cells(7, 1), .Cells(7, 5)).Font.Color = frm_PrfOldData_color
                                        .Range(.Cells(5, 1), .Cells(7, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                        .Range(.Cells(5, 1), .Cells(7, 5)).Locked = True
                                    End If
                                    'If FlgPrnBc_enable = True Then
                                    '.Range(.Cells(5, 1), .Cells(7, 5)).Interior.Color = frm_PrfGraph_bc
                                    'End If
                                    .Range(.Cells(9, 1), .Cells(10, 1)).MergeCells = True
                                    .Cells(9, 1) = "No."
                                    .Cells(9, 1).font.color = frm_PrfForm_fc
                                    .Range(.Cells(9, 2), .Cells(9, 3)).MergeCells = True
                                    .Cells(9, 2) = StrOrientAng
                                    .Cells(9, 2).font.color = frm_PrfForm_fc
                                    .Range(.Cells(9, 4), .Cells(9, 5)).MergeCells = True
                                    .Cells(9, 4) = StrOrientRat
                                    .Cells(9, 4).font.color = frm_PrfForm_fc
                                    .Range(.Cells(9, 6), .Cells(9, 9)).MergeCells = True
                                    .Cells(9, 6) = StrPropVelo
                                    .Cells(9, 6).font.color = frm_PrfForm_fc
                                    .Range(.Cells(9, 10), .Cells(9, 11)).MergeCells = True
                                    .Cells(9, 10) = "TSI(Km/S)^2"
                                    .Cells(9, 10).font.color = frm_PrfForm_fc
                                    .Cells(10, 2) = "Peak MD+-"
                                    .Cells(10, 3) = "Deep CD+-"
                                    .Cells(10, 4) = "MD/CD"
                                    .Cells(10, 5) = "Peak/Deep"
                                    .Cells(10, 6) = "MD"
                                    .Cells(10, 7) = "CD"
                                    .Cells(10, 8) = "Peak"
                                    .Cells(10, 9) = "Deep"
                                    .Cells(10, 10) = "MD"
                                    .Cells(10, 11) = "CD"
                                    .Range(.Cells(10, 2), .Cells(10, 11)).Font.Color = frm_PrfForm_fc
                                    .Cells(11, 1) = "Max."
                                    .Cells(12, 1) = "Ave."
                                    .Cells(13, 1) = "Min."
                                    .Range(.Cells(11, 1), .Cells(13, 11)).Font.Color = frm_PrfForm_fc
                                    .Cells(11, 2) = LblAnglePkMax_TB.Text
                                    .Cells(12, 2) = LblAnglePkAvg_TB.Text
                                    .Cells(13, 2) = LblAnglePkMin_TB.Text
                                    .Cells(11, 3) = LblAngleDpMax_TB.Text
                                    .Cells(12, 3) = LblAngleDpAvg_TB.Text
                                    .Cells(13, 3) = LblAngleDpMin_TB.Text
                                    .Cells(11, 4) = LblRatioMDCDMax_TB.Text
                                    .Cells(12, 4) = LblRatioMDCDAvg_TB.Text
                                    .Cells(13, 4) = LblRatioMDCDMin_TB.Text
                                    .Cells(11, 5) = LblRatioPkDpMax_TB.Text
                                    .Cells(12, 5) = LblRatioPkDpAvg_TB.Text
                                    .Cells(13, 5) = LblRatioPkDpMin_TB.Text
                                    .Cells(11, 6) = LblVeloMDMax_TB.Text
                                    .Cells(12, 6) = LblVeloMDAvg_TB.Text
                                    .Cells(13, 6) = LblVeloMDMin_TB.Text
                                    .Cells(11, 7) = LblVeloCDMax_TB.Text
                                    .Cells(12, 7) = LblVeloCDAvg_TB.Text
                                    .Cells(13, 7) = LblVeloCDMin_TB.Text
                                    .Cells(11, 8) = LblVeloPkMax_TB.Text
                                    .Cells(12, 8) = LblVeloPkAvg_TB.Text
                                    .Cells(13, 8) = LblVeloPkMin_TB.Text
                                    .Cells(11, 9) = LblVeloDpMax_TB.Text
                                    .Cells(12, 9) = LblVeloDpAvg_TB.Text
                                    .Cells(13, 9) = LblVeloDpMin_TB.Text
                                    .Cells(11, 10) = LblTSIMDMax_TB.Text
                                    .Cells(12, 10) = LblTSIMDAvg_TB.Text
                                    .Cells(13, 10) = LblTSIMDMin_TB.Text
                                    .Cells(11, 11) = LblTSICDMax_TB.Text
                                    .Cells(12, 11) = LblTSICDAvg_TB.Text
                                    .Cells(13, 11) = LblTSICDMin_TB.Text
                                    .Range(.Cells(11, 2), .Cells(13, 11)).Font.Color = frm_PrfCurData_color

                                    For s = 1 To SampleNo
                                        For k = 0 To 10
                                            .Cells(13 + s, k + 1) = DataGridView1.Rows(s - 1).Cells(k).Value
                                            .Cells(13 + s, k + 1).font.color = frm_PrfCurData_color
                                        Next
                                    Next
                                    .Range(.Cells(9, 1), .Cells(13 + SampleNo, 11)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(9, 1), .Cells(13 + SampleNo, 11)).Locked = True
                                    'If FlgPrnBc_enable = True Then
                                    '.Range(.Cells(9, 1), .Cells(13 + SampleNo, 11)).Interior.Color = frm_PrfGraph_bc
                                    'End If
                                Else
                                    .Cells(1, 1) = StrNoData
                                End If

                                .Protect()
                            End With

                            With sheet4
                                If FileDataMax > 0 Then
                                    .Cells.Locked = False
                                    If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                                        .Cells.Interior.Color = frm_PrfForm_bc
                                    End If

                                    .Cells(1, 1) = My.Application.Info.ProductName & " " & LblPrfTitle.Text
                                    .Cells(1, 1).font.color = frm_PrfForm_fc
                                    .Cells(2, 1) = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
                                    .Cells(2, 1).font.color = frm_PrfCurData_color
                                    .Cells(3, 1) = StrPastDataDate & DataDate_bak & StrMeasTime & DataTime_bak
                                    .Cells(3, 1).font.color = frm_PrfOldData_color
                                    .Range(.Cells(1, 1), .Cells(3, 1)).Locked = True

                                    .Cells(5, 2) = StrMachNo
                                    .Cells(5, 3) = StrSampleName
                                    If FlgDBF = 1 Then
                                        .Cells(5, 4) = StrMark
                                        .Cells(5, 5) = StrMeasNumber
                                        .Cells(5, 6) = StrMeasLot
                                        .Range(.Cells(5, 2), .Cells(5, 6)).Font.Color = frm_PrfForm_fc
                                    Else
                                        .Cells(5, 4) = StrMeasNumber
                                        .Cells(5, 5) = StrMeasLot
                                        .Range(.Cells(5, 2), .Cells(5, 5)).Font.Color = frm_PrfForm_fc
                                    End If
                                    .Cells(6, 1) = StrMeasSpec
                                    .Cells(6, 2) = TxtMachNoCur.Text
                                    .Cells(6, 3) = TxtSmplNamCur.Text
                                    If FlgDBF = 1 Then
                                        .Cells(6, 4) = TxtMarkCur.Text
                                        .Cells(6, 5) = TxtMeasNumCur.Text
                                        .Cells(6, 6) = TxtMeasLotCur.Text
                                        .Range(.Cells(6, 1), .Cells(6, 6)).Font.Color = frm_PrfCurData_color
                                    Else
                                        .Cells(6, 4) = TxtMeasNumCur.Text
                                        .Cells(6, 5) = TxtMeasLotCur.Text
                                        .Range(.Cells(6, 1), .Cells(6, 5)).Font.Color = frm_PrfCurData_color
                                    End If
                                    .Cells(7, 1) = StrPastMeasSpec
                                    .Cells(7, 2) = TxtMachNoBak.Text
                                    .Cells(7, 3) = TxtSmplNamBak.Text
                                    If FlgDBF = 1 Then
                                        .Cells(7, 4) = TxtMarkBak.Text
                                        .Cells(7, 5) = TxtMeasNumBak.Text
                                        .Cells(7, 6) = TxtMeasLotBak.Text
                                        .Range(.Cells(7, 1), .Cells(7, 6)).Font.Color = frm_PrfOldData_color
                                        .Range(.Cells(5, 1), .Cells(7, 6)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                        .Range(.Cells(5, 1), .Cells(7, 6)).Locked = True
                                    Else
                                        .Cells(7, 4) = TxtMeasNumBak.Text
                                        .Cells(7, 5) = TxtMeasLotBak.Text
                                        .Range(.Cells(7, 1), .Cells(7, 5)).Font.Color = frm_PrfOldData_color
                                        .Range(.Cells(5, 1), .Cells(7, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                        .Range(.Cells(5, 1), .Cells(7, 5)).Locked = True
                                    End If
                                    'If FlgPrnBc_enable = True Then
                                    '.Range(.Cells(5, 1), .Cells(7, 5)).Interior.Color = frm_PrfGraph_bc
                                    'End If
                                    .Range(.Cells(9, 1), .Cells(10, 1)).MergeCells = True
                                    .Cells(9, 1) = "No."
                                    .Cells(9, 1).font.color = frm_PrfForm_fc
                                    .Range(.Cells(9, 2), .Cells(9, 3)).MergeCells = True
                                    .Cells(9, 2) = StrOrientAng
                                    .Cells(9, 2).font.color = frm_PrfForm_fc
                                    .Range(.Cells(9, 4), .Cells(9, 5)).MergeCells = True
                                    .Cells(9, 4) = StrOrientRat
                                    .Cells(9, 4).font.color = frm_PrfForm_fc
                                    .Range(.Cells(9, 6), .Cells(9, 9)).MergeCells = True
                                    .Cells(9, 6) = StrPropVelo
                                    .Cells(9, 6).font.color = frm_PrfForm_fc
                                    .Range(.Cells(9, 10), .Cells(9, 11)).MergeCells = True
                                    .Cells(9, 10) = "TSI(Km/S)^2"
                                    .Cells(9, 10).font.color = frm_PrfForm_fc
                                    .Cells(10, 2) = "Peak MD+-"
                                    .Cells(10, 3) = "Deep CD+-"
                                    .Cells(10, 4) = "MD/CD"
                                    .Cells(10, 5) = "Peak/Deep"
                                    .Cells(10, 6) = "MD"
                                    .Cells(10, 7) = "CD"
                                    .Cells(10, 8) = "Peak"
                                    .Cells(10, 9) = "Deep"
                                    .Cells(10, 10) = "MD"
                                    .Cells(10, 11) = "CD"
                                    .Range(.Cells(10, 2), .Cells(10, 11)).Font.Color = frm_PrfForm_fc
                                    .Cells(11, 1) = "Max."
                                    .Cells(12, 1) = "Ave."
                                    .Cells(13, 1) = "Min."
                                    .Range(.Cells(11, 1), .Cells(13, 11)).Font.Color = frm_PrfForm_fc
                                    .Cells(11, 2) = LblAnglePkMaxOld_TB.Text
                                    .Cells(12, 2) = LblAnglePkAvgOld_TB.Text
                                    .Cells(13, 2) = LblAnglePkMinOld_TB.Text
                                    .Cells(11, 3) = LblAngleDpMaxOld_TB.Text
                                    .Cells(12, 3) = LblAngleDpAvgOld_TB.Text
                                    .Cells(13, 3) = LblAngleDpMinOld_TB.Text
                                    .Cells(11, 4) = LblRatioMDCDMaxOld_TB.Text
                                    .Cells(12, 4) = LblRatioMDCDAvgOld_TB.Text
                                    .Cells(13, 4) = LblRatioMDCDMinOld_TB.Text
                                    .Cells(11, 5) = LblRatioPkDpMaxOld_TB.Text
                                    .Cells(12, 5) = LblRatioPkDpAvgOld_TB.Text
                                    .Cells(13, 5) = LblRatioPkDpMinOld_TB.Text
                                    .Cells(11, 6) = LblVeloMDMaxOld_TB.Text
                                    .Cells(12, 6) = LblVeloMDAvgOld_TB.Text
                                    .Cells(13, 6) = LblVeloMDMinOld_TB.Text
                                    .Cells(11, 7) = LblVeloCDMaxOld_TB.Text
                                    .Cells(12, 7) = LblVeloCDAvgOld_TB.Text
                                    .Cells(13, 7) = LblVeloCDMinOld_TB.Text
                                    .Cells(11, 8) = LblVeloPkMaxOld_TB.Text
                                    .Cells(12, 8) = LblVeloPkAvgOld_TB.Text
                                    .Cells(13, 8) = LblVeloPkMinOld_TB.Text
                                    .Cells(11, 9) = LblVeloDpMaxOld_TB.Text
                                    .Cells(12, 9) = LblVeloDpAvgOld_TB.Text
                                    .Cells(13, 9) = LblVeloDpMinOld_TB.Text
                                    .Cells(11, 10) = LblTSIMDMaxOld_TB.Text
                                    .Cells(12, 10) = LblTSIMDAvgOld_TB.Text
                                    .Cells(13, 10) = LblTSIMDMinOld_TB.Text
                                    .Cells(11, 11) = LblTSICDMaxOld_TB.Text
                                    .Cells(12, 11) = LblTSICDAvgOld_TB.Text
                                    .Cells(13, 11) = LblTSICDMinOld_TB.Text
                                    .Range(.Cells(11, 2), .Cells(13, 11)).Font.Color = frm_PrfOldData_color

                                    For s = 1 To FileDataMax
                                        For k = 0 To 10
                                            .Cells(13 + s, k + 1) = DataGridView2.Rows(s - 1).Cells(k).Value
                                            .Cells(13 + s, k + 1).font.color = frm_PrfOldData_color
                                        Next
                                    Next
                                    .Range(.Cells(9, 1), .Cells(13 + SampleNo, 11)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(9, 1), .Cells(13 + SampleNo, 11)).Locked = True
                                    'If FlgPrnBc_enable = True Then
                                    '.Range(.Cells(9, 1), .Cells(13 + SampleNo, 11)).Interior.Color = frm_PrfGraph_bc
                                    'End If
                                Else
                                    .Cells(1, 1) = StrNoData
                                End If

                                .Protect()
                            End With

                            With sheet5
                                If FlgAvg > 0 Then
                                    .Cells.Locked = False
                                    If frm_PrfForm_bc <> SystemColors.Control And FlgPrnBc_enable = True Then
                                        .Cells.Interior.Color = frm_PrfForm_bc
                                    End If

                                    .Cells(1, 1) = My.Application.Info.ProductName & " " & LblPrfTitle.Text
                                    .Cells(1, 1).font.color = frm_PrfForm_fc
                                    .Cells(2, 1) = StrMeasDataDate & DataDate_cur & StrMeasTime & DataTime_cur
                                    .Cells(2, 1).font.color = frm_PrfCurData_color
                                    .Cells(3, 1) = StrPastDataDate & DataDate_bak & StrMeasTime & DataTime_bak
                                    .Cells(3, 1).font.color = frm_PrfOldData_color
                                    .Range(.Cells(1, 1), .Cells(3, 1)).Locked = True
                                    .Cells(5, 2) = StrMachNo
                                    .Cells(5, 3) = StrSampleName
                                    If FlgDBF = 1 Then
                                        .Cells(5, 4) = StrMark
                                        .Cells(5, 5) = StrMeasNumber
                                        .Cells(5, 6) = StrMeasLot
                                        .Range(.Cells(5, 2), .Cells(5, 6)).Font.Color = frm_PrfForm_fc
                                    Else
                                        .Cells(5, 4) = StrMeasNumber
                                        .Cells(5, 5) = StrMeasLot
                                        .Range(.Cells(5, 2), .Cells(5, 5)).Font.Color = frm_PrfForm_fc
                                    End If
                                    .Cells(6, 1) = StrMeasSpec
                                    .Cells(6, 2) = TxtMachNoCur.Text
                                    .Cells(6, 3) = TxtSmplNamCur.Text
                                    If FlgDBF = 1 Then
                                        .Cells(6, 4) = TxtMarkCur.Text
                                        .Cells(6, 5) = TxtMeasNumCur.Text
                                        .Cells(6, 6) = TxtMeasLotCur.Text
                                        .Range(.Cells(6, 1), .Cells(6, 6)).Font.Color = frm_PrfCurData_color
                                    Else
                                        .Cells(6, 4) = TxtMeasNumCur.Text
                                        .Cells(6, 5) = TxtMeasLotCur.Text
                                        .Range(.Cells(6, 1), .Cells(6, 5)).Font.Color = frm_PrfCurData_color
                                    End If
                                    .Cells(7, 1) = StrPastMeasSpec
                                    .Cells(7, 2) = TxtMachNoBak.Text
                                    .Cells(7, 3) = TxtSmplNamBak.Text
                                    If FlgDBF = 1 Then
                                        .Cells(7, 4) = TxtMarkBak.Text
                                        .Cells(7, 5) = TxtMeasNumBak.Text
                                        .Cells(7, 6) = TxtMeasLotBak.Text
                                        .Range(.Cells(7, 1), .Cells(7, 6)).Font.Color = frm_PrfOldData_color
                                        .Range(.Cells(5, 1), .Cells(7, 6)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                        .Range(.Cells(5, 1), .Cells(7, 6)).Locked = True
                                    Else
                                        .Cells(7, 4) = TxtMeasNumBak.Text
                                        .Cells(7, 5) = TxtMeasLotBak.Text
                                        .Range(.Cells(7, 1), .Cells(7, 5)).Font.Color = frm_PrfOldData_color
                                        .Range(.Cells(5, 1), .Cells(7, 5)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                        .Range(.Cells(5, 1), .Cells(7, 5)).Locked = True
                                    End If
                                    'If FlgPrnBc_enable = True Then
                                    '.Range(.Cells(5, 1), .Cells(7, 5)).Interior.Color = frm_PrfGraph_bc
                                    'End If
                                    .Range(.Cells(9, 1), .Cells(10, 1)).MergeCells = True
                                    .Cells(9, 1) = "No."
                                    .Cells(9, 1).font.color = frm_PrfForm_fc
                                    .Range(.Cells(9, 2), .Cells(9, 3)).MergeCells = True
                                    .Cells(9, 2) = StrOrientAng
                                    .Cells(9, 2).font.color = frm_PrfForm_fc
                                    .Range(.Cells(9, 4), .Cells(9, 5)).MergeCells = True
                                    .Cells(9, 4) = StrOrientRat
                                    .Cells(9, 4).font.color = frm_PrfForm_fc
                                    .Range(.Cells(9, 6), .Cells(9, 9)).MergeCells = True
                                    .Cells(9, 6) = StrPropVelo
                                    .Cells(9, 6).font.color = frm_PrfForm_fc
                                    .Range(.Cells(9, 10), .Cells(9, 11)).MergeCells = True
                                    .Cells(9, 10) = "TSI(Km/S)^2"
                                    .Cells(9, 10).font.color = frm_PrfForm_fc
                                    .Cells(10, 2) = "Peak MD+-"
                                    .Cells(10, 3) = "Deep CD+-"
                                    .Cells(10, 4) = "MD/CD"
                                    .Cells(10, 5) = "Peak/Deep"
                                    .Cells(10, 6) = "MD"
                                    .Cells(10, 7) = "CD"
                                    .Cells(10, 8) = "Peak"
                                    .Cells(10, 9) = "Deep"
                                    .Cells(10, 10) = "MD"
                                    .Cells(10, 11) = "CD"
                                    .Range(.Cells(10, 2), .Cells(10, 11)).Font.Color = frm_PrfForm_fc
                                    .Cells(11, 1) = "Max."
                                    .Cells(12, 1) = "Ave."
                                    .Cells(13, 1) = "Min."
                                    .Range(.Cells(11, 1), .Cells(13, 11)).Font.Color = frm_PrfForm_fc
                                    .Cells(11, 2) = LblAnglePkMaxAvg_TB.Text
                                    .Cells(12, 2) = LblAnglePkAvgAvg_TB.Text
                                    .Cells(13, 2) = LblAnglePkMinAvg_TB.Text
                                    .Cells(11, 3) = LblAngleDpMaxAvg_TB.Text
                                    .Cells(12, 3) = LblAngleDpAvgAvg_TB.Text
                                    .Cells(13, 3) = LblAngleDpMinAvg_TB.Text
                                    .Cells(11, 4) = LblRatioMDCDMaxAvg_TB.Text
                                    .Cells(12, 4) = LblRatioMDCDAvgAvg_TB.Text
                                    .Cells(13, 4) = LblRatioMDCDMinAvg_TB.Text
                                    .Cells(11, 5) = LblRatioPkDpMaxAvg_TB.Text
                                    .Cells(12, 5) = LblRatioPkDpAvgAvg_TB.Text
                                    .Cells(13, 5) = LblRatioPkDpMinAvg_TB.Text
                                    .Cells(11, 6) = LblVeloMDMaxAvg_TB.Text
                                    .Cells(12, 6) = LblVeloMDAvgAvg_TB.Text
                                    .Cells(13, 6) = LblVeloMDMinAvg_TB.Text
                                    .Cells(11, 7) = LblVeloCDMaxAvg_TB.Text
                                    .Cells(12, 7) = LblVeloCDAvgAvg_TB.Text
                                    .Cells(13, 7) = LblVeloCDMinAvg_TB.Text
                                    .Cells(11, 8) = LblVeloPkMaxAvg_TB.Text
                                    .Cells(12, 8) = LblVeloPkAvgAvg_TB.Text
                                    .Cells(13, 8) = LblVeloPkMinAvg_TB.Text
                                    .Cells(11, 9) = LblVeloDpMaxAvg_TB.Text
                                    .Cells(12, 9) = LblVeloDpAvgAvg_TB.Text
                                    .Cells(13, 9) = LblVeloDpMinAvg_TB.Text
                                    .Cells(11, 10) = LblTSIMDMaxAvg_TB.Text
                                    .Cells(12, 10) = LblTSIMDAvgAvg_TB.Text
                                    .Cells(13, 10) = LblTSIMDMinAvg_TB.Text
                                    .Cells(11, 11) = LblTSICDMaxAvg_TB.Text
                                    .Cells(12, 11) = LblTSICDAvgAvg_TB.Text
                                    .Cells(13, 11) = LblTSICDMinAvg_TB.Text
                                    .Range(.Cells(11, 2), .Cells(13, 11)).Font.Color = frm_PrfAvgData_color

                                    For s = 1 To SampleNo
                                        For k = 0 To 10
                                            .Cells(13 + s, k + 1) = DataGridView3.Rows(s - 1).Cells(k).Value
                                            .Cells(13 + s, k + 1).font.color = frm_PrfAvgData_color
                                        Next
                                    Next
                                    .Range(.Cells(9, 1), .Cells(13 + SampleNo, 11)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous
                                    .Range(.Cells(9, 1), .Cells(13 + SampleNo, 11)).Locked = True
                                    'If FlgPrnBc_enable = True Then
                                    '.Range(.Cells(9, 1), .Cells(13 + SampleNo, 11)).Interior.Color = frm_PrfGraph_bc
                                    'End If
                                Else
                                    .Cells(1, 1) = StrNoData
                                End If

                                .Protect()
                            End With
                        End If

                        sheet1.Activate()
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
            Marshal.ReleaseComObject(sheet1)
            Marshal.ReleaseComObject(sheet2)
            Marshal.ReleaseComObject(sheet3)
            Marshal.ReleaseComObject(sheet4)
            Marshal.ReleaseComObject(sheet5)
            Marshal.ReleaseComObject(excelBook)
            Marshal.ReleaseComObject(excelApp)

            CmdPrfResultSave.Text = "Save"
            CmdPrfResultSave.Enabled = True
        End Try

    End Sub

    Private Sub CmdPrfResultSave_Click(sender As Object, e As EventArgs) Handles CmdPrfResultSave.Click
        PrfResultSave()
    End Sub

    Private Sub LblAngCenter_Click(sender As Object, e As EventArgs) Handles LblAngCenter.Click
        flgTemp = FlgMainProfile

        FlgMainProfile = 27
    End Sub

    Private Sub MmToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MmToolStripMenuItem.Click
        For Each item As ToolStripMenuItem In groupMenuUnit
            If Object.ReferenceEquals(item, sender) Then
                item.CheckState = CheckState.Indeterminate
                FlgInch = 0
            Else
                item.CheckState = CheckState.Unchecked
                FlgInch = 1
            End If


        Next
        If InchToolStripMenuItem.Checked Then
            OptInch.Checked = True
        Else
            OptMm.Checked = True
        End If
    End Sub

    Private Sub InchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InchToolStripMenuItem.Click
        For Each item As ToolStripMenuItem In groupMenuUnit
            If Object.ReferenceEquals(item, sender) Then
                item.CheckState = CheckState.Indeterminate
                FlgInch = 1
            Else
                item.CheckState = CheckState.Unchecked
                FlgInch = 0
            End If


        Next
        If InchToolStripMenuItem.Checked Then
            OptInch.Checked = True
        Else
            OptMm.Checked = True
        End If
    End Sub

    Private Sub 測定中断ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MeasStopToolStripMenuItem.Click
        MeasStopToolStripMenuItem.Enabled = False
        CmdQuitProfile.Enabled = False
        QuitToolStripMenuItem.Enabled = False
        FlgStop = 1
        If FlgProfile = 3 Then
            FlgLongMeas = 0
        End If
    End Sub

    Private Sub 測定開始ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MeasStartToolStripMenuItem.Click
        MeasRun()
    End Sub

    Private Sub 終了ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        FlgMainProfile = 90
    End Sub

    Private Sub 選択ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectToolStripMenuItem.Click
        Dim result As DialogResult
        Dim fname As String = ""

        result = LoadDefConstName(fname, False)

        If result = DialogResult.OK Then
            StrConstFileName = fname

            LoadConst(Me, title_text2)

            'ClsNoPrf()
            'GraphInitPrf()

            FlgMainProfile = 20
        End If
    End Sub

    Private Sub 保存ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 保存ToolStripMenuItem.Click
        SaveConstPrf()
    End Sub

    Private Sub 読込ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadToolStripMenuItem.Click
        FlgMainProfile = 40
        FlgAvg = 0
    End Sub

    Private Sub グラフ消去ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GraphDelToolStripMenuItem.Click
        DrawCalcCurData_init()
        DrawCalcBakData_init()
        DrawCalcAvgData_init()
        DrawTableData_init()

        ClsNoPrf()
        GraphInitPrf(Points)
    End Sub

    Private Sub 平均値ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AvgToolStripMenuItem.Click
        DataCount = 0
        FlgMainProfile = 45
        FlgAvg = 1
    End Sub

    Private Sub 自動印刷ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutoPrintToolStripMenuItem.Click
        Menu_AutoPrn.Checked = Not Menu_AutoPrn.Checked
        If Menu_AutoPrn.Checked = True Then
            If ChkPrfAutoPrn.Checked = False Then
                ChkPrfAutoPrn.Checked = True
                FlgPrfAutoPrn = 1
            End If
        Else
            If ChkPrfAutoPrn.Checked = True Then
                ChkPrfAutoPrn.Checked = False
                FlgPrfAutoPrn = 0
            End If
        End If
    End Sub

    Private Sub 配向角配向比ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AngRatToolStripMenuItem.Click
        MenuPrn_AngleRatio.Checked = Not MenuPrn_AngleRatio.Checked
        If MenuPrn_AngleRatio.Checked = True Then
            If ChkPrn_AngleRatio.Checked = False Then
                ChkPrn_AngleRatio.Checked = True
                chkPrnAngleRatio = 1
            End If
        Else
            If ChkPrn_AngleRatio.Checked = True Then
                ChkPrn_AngleRatio.Checked = False
                chkPrnAngleRatio = 0
            End If
        End If
        FlgPrfPrint = chkPrnAngleRatio * 1 +
                      chkPrnVelocityTSI * 2 +
                      chkPrnMeasData * 4 +
                      chkPrnOldData * 8 +
                      chkPrnAvgData * 16
    End Sub

    Private Sub 伝播速度TSIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VeloTSIToolStripMenuItem.Click
        MenuPrn_VeloTSI.Checked = Not MenuPrn_VeloTSI.Checked
        If MenuPrn_VeloTSI.Checked = True Then
            If ChkPrn_VelocityTSI.Checked = False Then
                ChkPrn_VelocityTSI.Checked = True
                chkPrnVelocityTSI = 1
            End If
        Else
            If ChkPrn_VelocityTSI.Checked = True Then
                ChkPrn_VelocityTSI.Checked = False
                chkPrnVelocityTSI = 0
            End If
        End If
        FlgPrfPrint = chkPrnAngleRatio * 1 +
                      chkPrnVelocityTSI * 2 +
                      chkPrnMeasData * 4 +
                      chkPrnOldData * 8 +
                      chkPrnAvgData * 16
    End Sub

    Private Sub 測定データ表ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MeasDataTableToolStripMenuItem.Click
        MenuPrn_measData.Checked = Not MenuPrn_measData.Checked
        If MenuPrn_measData.Checked = True Then
            If ChkPrn_MeasData.Checked = False Then
                ChkPrn_MeasData.Checked = True
                chkPrnMeasData = 1
            End If
        Else
            If ChkPrn_MeasData.Checked = True Then
                ChkPrn_MeasData.Checked = False
                chkPrnMeasData = 0
            End If
        End If
        FlgPrfPrint = chkPrnAngleRatio * 1 +
                      chkPrnVelocityTSI * 2 +
                      chkPrnMeasData * 4 +
                      chkPrnOldData * 8 +
                      chkPrnAvgData * 16
    End Sub

    Private Sub 過去データ表ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OldDataTableToolStripMenuItem.Click
        MenuPrn_OldData.Checked = Not MenuPrn_OldData.Checked
        If MenuPrn_OldData.Checked = True Then
            If ChkPrn_OldData.Checked = False Then
                ChkPrn_OldData.Checked = True
                chkPrnOldData = 1
            End If
        Else
            If ChkPrn_OldData.Checked = True Then
                ChkPrn_OldData.Checked = False
                chkPrnOldData = 0
            End If
        End If
        FlgPrfPrint = chkPrnAngleRatio * 1 +
                      chkPrnVelocityTSI * 2 +
                      chkPrnMeasData * 4 +
                      chkPrnOldData * 8 +
                      chkPrnAvgData * 16
    End Sub

    Private Sub 平均値データ表ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AvgDataTableToolStripMenuItem.Click
        MenuPrn_AvgData.Checked = Not MenuPrn_AvgData.Checked
        If MenuPrn_AvgData.Checked = True Then
            If ChkPrn_AvgData.Checked = False Then
                ChkPrn_AvgData.Checked = True
                chkPrnAvgData = 1
            End If
        Else
            If ChkPrn_AvgData.Checked = True Then
                ChkPrn_AvgData.Checked = False
                chkPrnAvgData = 0
            End If
        End If
        FlgPrfPrint = chkPrnAngleRatio * 1 +
                      chkPrnVelocityTSI * 2 +
                      chkPrnMeasData * 4 +
                      chkPrnOldData * 8 +
                      chkPrnAvgData * 16
    End Sub

    Private Sub 手動印刷ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManualPrintToolStripMenuItem.Click
        PrintoutPrf()
    End Sub

    Private Sub 保存ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveExcelToolStripMenuItem1.Click
        PrfResultSave()
    End Sub

    Private Sub 設定ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SettingToolStripMenuItem1.Click
        FrmSST4500_1_0_0E_setting.Visible = True
    End Sub

    Private Sub SetConst_Menu()
        If FlgPrfAutoPrn = 1 Then
            Menu_AutoPrn.Checked = True
        Else
            Menu_AutoPrn.Checked = False
        End If
        If chkPrnAngleRatio = 1 Then
            MenuPrn_AngleRatio.Checked = True
        Else
            MenuPrn_AngleRatio.Checked = False
        End If
        If chkPrnVelocityTSI = 1 Then
            MenuPrn_VeloTSI.Checked = True
        Else
            MenuPrn_VeloTSI.Checked = False
        End If
        If chkPrnMeasData = 1 Then
            MenuPrn_measData.Checked = True
        Else
            MenuPrn_measData.Checked = False
        End If
        If chkPrnOldData = 1 Then
            MenuPrn_OldData.Checked = True
        Else
            MenuPrn_OldData.Checked = False
        End If
        If chkPrnAvgData = 1 Then
            MenuPrn_AvgData.Checked = True
        Else
            MenuPrn_AvgData.Checked = False
        End If
    End Sub

    Private Sub SST4500についてToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SST4500InfoToolStripMenuItem.Click
        FrmSST4500_1_0_0E_helpinfo.ShowDialog()
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        FrmSST4500_1_0_0E_helpinfo.ShowDialog()
    End Sub

    Private Sub prf_dbf_chg(ByVal sw As Integer)
        Select Case sw
            Case 0  '通常
                Label51.Visible = False
                TxtSmplNamCur.Width = TXTSMPWIDTH_1 + TXTSMPWIDTH_add
                TxtMarkCur.Visible = False
                TxtSmplNamBak.Width = TXTSMPWIDTH_1 + TXTSMPWIDTH_add
                TxtMarkBak.Visible = False
            Case 1  '特殊1
                Label51.Visible = True
                TxtSmplNamCur.Width = TXTSMPWIDTH_1
                TxtMarkCur.Visible = True
                TxtSmplNamBak.Width = TXTSMPWIDTH_1
                TxtMarkBak.Visible = True
        End Select
    End Sub

    Private Sub LblPitchExp_Click(sender As Object, e As EventArgs) Handles LblPitchExp.Click

        FrmSST4500_1_0_0E_pitchsetting.Visible = True

    End Sub

End Class