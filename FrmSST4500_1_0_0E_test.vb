Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Runtime.CompilerServices

Public Class FrmSST4500_1_0_0E_test
    Const test_graph_x_sta = 50
    Const test_graph_x_end = 698
    Const test_graph_yaxis_max = 370    'picturebox.height - 30
    Const test_graph_yaxis_min = 10
    Dim test_graph_SclY As Single = (test_graph_yaxis_max - test_graph_yaxis_min) / 6
    Dim test_graph_SclX As Single = (test_graph_x_end - test_graph_x_sta) / 24

    Dim test_waku_ypath As New List(Of GraphicsPath)
    Dim test_waku_xpath As New List(Of GraphicsPath)
    Dim test_waku_xlabel(12) As String
    Dim test_waku_ylabel(4) As String
    Dim test_waku_Yaxis_label As String = "Propagation Time (us)"

    Dim graph_path_cur As New List(Of GraphicsPath)
    Dim graph_path_old As New List(Of GraphicsPath)

    Dim strSerialNumber As String
    Dim FlgDspTestData As Integer
    Dim result As DialogResult
    Dim fname As String = ""
    Dim result2 As Integer
    Dim TimeCountT As Integer
    Dim Kt As Long
    Dim Kp As Integer

    Private Sub FrmSST4500_1_0_0E_test_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size

        Me.Text = My.Application.Info.ProductName & " Test (Ver:" & My.Application.Info.Version.ToString & ")"
        Me.Label1.Text = My.Application.Info.ProductName

    End Sub

    Private Sub FrmSST4500_1_0_0E_test_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            UsbOpen()

            GraphWakuInit()

            ClsAngleData()
            ClsDataTest()
            ClsDefData()

            LblTxData.Text = ""
            LblRxData.Text = ""
            LblTime.Text = ""
            LblTT1.Text = ""

            TimTest.Enabled = True
        End If
    End Sub

    Private Sub TimTest_Tick(sender As Object, e As EventArgs) Handles TimTest.Tick
        Dim _flgRx As Integer

        Select Case FlgMainTest
            Case 1
                SampleNo = 0
                FileDataNo = 0
                FileNo = 0
                Kax1 = test_graph_x_sta

                CmdAnotherFileData.Enabled = False

                TxtPitch.Text = 250

                FlgMainTest = 0

            Case 10
                LblTxData.Text = strWdata

                strRxdata = ""
                _flgRx = UsbRead(strRxdata)

                If _flgRx = 0 Then

                    If strRxdata <> "" Then
                        strRxdata = Strings.Left(strRxdata, Len(strRxdata) - 1)
                    End If

                    LblRxData.Text = strRxdata
                    timerCount1 = 0
                    FlgMainTest = 11
                End If

            Case 11
                timerCount1 += 1

                If timerCount1 Mod 10 = 0 Then
                    LblTime.Text = Format(timerCount1 / 50, "0.0")
                End If

                If timerCount1 = 700 Then   ' 700 * 10ms = 7s ≒ 測定時間
                    LblTime.Text = ""
                    timerCount1 = 0
                    FlgMainTest = 12
                End If

            Case 12
                If FlgTest = 1 Then
                    FlgMainTest = 13
                    Exit Sub
                End If

                strRxdata = ""
                _flgRx = UsbRead(strRxdata)

                If _flgRx = 0 Then

                    If strRxdata <> "" Then
                        strRxdata = Strings.Left(strRxdata, Len(strRxdata) - 1)
                    End If

                    LblRxData.Text = strRxdata
                    FlgMainTest = 13
                End If

            Case 13
                SampleNo += 1

                KdData = 1
                ResolveData()
                RemakeData()

                DrawGraph()
                DrawData()
                DrawDef()

                FlgMainTest = 0

            Case 20
                timerCount1 = 0
                FlgMainTest = 21

            Case 21
                timerCount1 += 1
                If timerCount1 > Val(TxtPitch.Text) * 0.02 Then
                    timerCount1 = 0
                    FlgMainTest = 22
                End If

            Case 22
                timerCount1 += 1
                If timerCount1 = 10 Then
                    _flgRx = ReceivedData()

                    If _flgRx = 0 Then
                        FlgMainTest = 0
                    Else
                        timerCount1 = 0
                        FlgMainTest = 22
                    End If
                End If

            Case 30

                If FileNo > 9 Then
                    FlgMainTest = 39
                Else
                    FlgMainTest = 31
                End If

            Case 31
                'ファイルデータ読み込み
                '(過去データ読み込み)
                TimTest.Enabled = False

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
                        FlgMainTest = 0
                    Else
                        'Input a Sample No.へ
                        FlgMainTest = 32
                    End If
                ElseIf result = DialogResult.Cancel Then
                    FlgMainTest = 0
                End If

                TimeCountT = 0
                TimTest.Enabled = True

            Case 32
                TimeCountT += 1
                If TimeCountT = 10 Then
                    FlgMainTest = 33
                End If

            Case 33
                Dim input_ret As String

                Kt = SampleNo

                TimTest.Enabled = False

                input_ret = InputBox(StrInputMeasNo, StrSelMeasNo, Str(FileDataMax))
                '↑valするとキャンセルした時に0が返ってくる
                '  valしないと""になる

                'ここではキャンセルも空データも許さない
                If input_ret = "" Then
                    MessageBox.Show(StrEntMeasNo,
                                    StrInputErr,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                Else
                    If IsNumeric(input_ret) Then

                        If input_ret = 0 Or input_ret > FileDataMax Then
                            MessageBox.Show(StrIncorrectNo,
                                            StrInputErr,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation)
                        Else
                            '過去データ読み込み成功
                            CmdAnotherFileData.Enabled = True

                            MakeDisplayData()

                            KdData = 3
                            RemakeData()
                            DrawGraph()
                            DrawData()
                            DrawDef()

                            FlgMainTest = 0
                        End If
                    Else
                        MessageBox.Show(StrEntNo,
                                        StrInputErr,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                End If

                SampleNo = Kt
                TimTest.Enabled = True

            Case 35
                Dim input_ret As String

                Kt = SampleNo
                Kp = FileDataMax

                TimTest.Enabled = False

                input_ret = InputBox(StrInputMeasNo, StrSelMeasNo, Str(Kp))

                If input_ret = String.Empty Then
                    'たぶんキャンセル
                    'キャンセルなら何もしない
                ElseIf input_ret = "" Then
                    MessageBox.Show(StrEntMeasNo,
                                    StrInputErr,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation)
                Else
                    If IsNumeric(input_ret) = True Then
                        SampleNo = input_ret

                        If SampleNo = 0 Or SampleNo > Kp Then
                            MessageBox.Show(StrIncorrectNo,
                                            StrInputErr,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation)
                        Else
                            TimeCountT = 0

                            FlgMainTest = 36
                            SampleNo = Kt
                            TimTest.Enabled = True
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show(StrEntNo,
                                        StrInputErr,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation)
                    End If
                End If

                SampleNo = Kt
                FlgMainTest = 0

                TimTest.Enabled = True

            Case 36
                TimeCountT += 1
                If TimeCountT = 10 Then
                    FlgMainTest = 37
                End If

            Case 37
                MakeDisplayData()

                KdData = 3
                RemakeData()
                DrawGraph()
                DrawData()
                DrawDef()

                FlgMainTest = 0

            Case 39
                TimTest.Enabled = False

                MessageBox.Show("Can not read more than 10 files",
                                StrErrPastDataRead,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation)

                FlgMainTest = 0

                TimTest.Enabled = True

            Case 90
                '終了ボタン

                ToolStripStatusLabel4.Text = ""
                If FlgTest = 0 Then
                    UsbClose()
                End If

                Me.Visible = False
                timerCount1 = 0
                FlgMainTest = 91

            Case 91
                timerCount1 += 1
                If timerCount1 = 10 Then
                    TimTest.Enabled = False

                    FrmSST4500_1_0_0E_main.Visible = True
                    FlgMainSplash = 11
                    FlgMainTest = 0

                End If

        End Select
    End Sub

    Private Sub GraphWakuInit()

        Dim path1 As New Drawing2D.GraphicsPath
        Dim path2 As New Drawing2D.GraphicsPath

        ClsGraphWaku()

        For i = 0 To 6
            path1.StartFigure()
            path1.AddLine(test_graph_x_sta, test_graph_yaxis_min + i * test_graph_SclY,
                          test_graph_x_end, test_graph_yaxis_min + i * test_graph_SclY)
        Next
        test_waku_ypath.Add(path1)

        For i = 0 To 24
            path2.StartFigure()
            path2.AddLine(test_graph_x_sta + i * test_graph_SclX, test_graph_yaxis_min,
                          test_graph_x_sta + i * test_graph_SclX, test_graph_yaxis_max)
        Next
        test_waku_xpath.Add(path2)

        test_waku_xlabel(0) = "0"
        test_waku_xlabel(1) = "2"
        test_waku_xlabel(2) = "4"
        test_waku_xlabel(3) = "6"
        test_waku_xlabel(4) = "8"
        test_waku_xlabel(5) = "10"
        test_waku_xlabel(6) = "12"
        test_waku_xlabel(7) = "14"
        test_waku_xlabel(8) = "0"
        test_waku_xlabel(9) = "2"
        test_waku_xlabel(10) = "4"
        test_waku_xlabel(11) = "6"
        test_waku_xlabel(12) = "8"

        test_waku_ylabel(0) = "70"
        test_waku_ylabel(1) = "60"
        test_waku_ylabel(2) = "50"
        test_waku_ylabel(3) = "40"
        test_waku_ylabel(4) = "30"

        PictureBox1.Refresh()
    End Sub

    Private Sub ClsGraphWaku()
        test_waku_xpath.Clear()
        test_waku_ypath.Clear()
        For i = 0 To 12
            test_waku_xlabel(i) = ""
        Next
        For i = 0 To 4
            test_waku_ylabel(i) = ""
        Next
        PictureBox1.Refresh()
    End Sub

    Private Sub PictureBox1_Paint(sender As Object, e As PaintEventArgs) Handles PictureBox1.Paint
        Dim pen_black_1_dot2 As New Pen(Color.Black, 1) With {
        .DashStyle = DashStyle.DashDotDot}
        Dim pen_black_1 As New Pen(Color.Black, 1)
        Dim pen_blue_1 As New Pen(Color.Blue, 1)

        For Each path As GraphicsPath In test_waku_ypath
            e.Graphics.DrawPath(pen_black_1_dot2, path)
        Next

        For Each path As GraphicsPath In test_waku_xpath
            e.Graphics.DrawPath(pen_black_1_dot2, path)
        Next

        Draw_test_waku_Xlabel(e)
        Draw_test_waku_Ylabel(e)

        For Each path As GraphicsPath In graph_path_cur
            e.Graphics.DrawPath(pen_black_1, path)
        Next

        For Each path As GraphicsPath In graph_path_old
            e.Graphics.DrawPath(pen_blue_1, path)
        Next

    End Sub

    Private Sub Draw_test_waku_Xlabel(ByVal e As PaintEventArgs)
        Dim fnt As New Font("MS UI Gothic", 10, FontStyle.Bold)
        Dim string_tmp As String = "Channel"
        Dim stringSize As SizeF = e.Graphics.MeasureString(string_tmp, fnt)
        Dim font_y_loc As Integer = test_graph_yaxis_max + 2
        For i = 0 To 4
            e.Graphics.DrawString(test_waku_xlabel(i), fnt, Brushes.Black, test_graph_x_sta + test_graph_SclX * i * 2 - 5, font_y_loc)
            If i = 0 Then
                e.Graphics.DrawString("CD", fnt, Brushes.Black, test_graph_x_sta + test_graph_SclX * i * 2 - 10, font_y_loc + 12)
            ElseIf i = 4 Then
                e.Graphics.DrawString("MD", fnt, Brushes.Black, test_graph_x_sta + test_graph_SclX * i * 2 - 10, font_y_loc + 12)
            End If
        Next

        For i = 5 To 7
            e.Graphics.DrawString(test_waku_xlabel(i), fnt, Brushes.Black, test_graph_x_sta + test_graph_SclX * i * 2 - 8, font_y_loc)
            If i = 6 Then
                e.Graphics.DrawString(string_tmp, fnt, Brushes.Black, test_graph_x_sta + test_graph_SclX * i * 2 - stringSize.Width / 2, font_y_loc + 15)
            End If
        Next

        For i = 8 To 12
            e.Graphics.DrawString(test_waku_xlabel(i), fnt, Brushes.Black, test_graph_x_sta + test_graph_SclX * i * 2 - 5, font_y_loc)
            If i = 8 Then
                e.Graphics.DrawString("CD", fnt, Brushes.Black, test_graph_x_sta + test_graph_SclX * i * 2 - 10, font_y_loc + 12)
            End If
        Next

    End Sub

    Private Sub Draw_test_waku_Ylabel(ByVal e As PaintEventArgs)
        Dim fnt As New Font("MS UI Gothic", 10)
        'e.Graphics.DrawString("Time", fnt, Brushes.Black, 2, 2)
        For i = 0 To 4
            e.Graphics.DrawString(test_waku_ylabel(i), fnt, Brushes.Black, test_graph_x_sta - 18, test_graph_yaxis_min + test_graph_SclY + test_graph_SclY * i - 6)
        Next

        e.Graphics.RotateTransform(-90.0F)
        e.Graphics.DrawString(test_waku_Yaxis_label, fnt, Brushes.Black, -230, 10)
        e.Graphics.RotateTransform(+90.0F)
    End Sub

    Private Sub CmdQuitTest_Click(sender As Object, e As EventArgs) Handles CmdQuitTest.Click
        FlgMainTest = 90
    End Sub

    Private Sub CmdGetSerialNum_Click(sender As Object, e As EventArgs) Handles CmdGetSerialNum.Click
        FT_Close(lngHandle)

        Dim numDevs As Long
        ftStatus = FT_GetNumDevices(numDevs, vbNullChar, FT_LIST_BY_NUMBER_ONLY)

        ftStatus = FT_ListDevices(0, strSerialNumber, FT_LIST_BY_INDEX Or FT_OPEN_BY_SERIAL_NUMBER)
        If ftStatus <> FT_OK Then
            ToolStripStatusLabel4.Text = "FT_Listdevices Failed / status = " & Str(ftStatus)
        Else
            ToolStripStatusLabel4.Text = "Get SerialNumber OK = " & strSerialNumber
        End If

        UsbOpen()
    End Sub

    Private Sub CmdUSBOpen_Click(sender As Object, e As EventArgs) Handles CmdUSBOpen.Click
        UsbOpen()

        If ftStatus <> FT_OK Then
            ToolStripStatusLabel4.Text = "FT_SetBaudRate Failed / status = " & Str(ftStatus)
        Else
            ToolStripStatusLabel4.Text = "FT_Open OK = " & Str(lngHandle)
        End If
    End Sub

    Private Sub CmdReceivedData_Click(sender As Object, e As EventArgs) Handles CmdReceivedData.Click
        ReceivedData()

    End Sub

    Function ReceivedData() As Integer
        Dim KK As Integer
        Dim _flgRx As Integer
        Dim timeout_count As Long

        flFatalError = False

        strTotalReadBuffer = ""
        lngTotalBytesRead = 0
        strRxdata = ""

        KK += 1

        _flgRx = UsbRead(strRxdata)

        timeout_count = 0
        Do While _flgRx <> 0
            strRxdata = ""
            _flgRx = UsbRead(strRxdata)

            If _flgRx = 0 Then

                Exit Do
            ElseIf _flgRx = 1 Then
                If timeout_count > 200 Then     '5ms * 200 = 1s
                    ToolStripStatusLabel4.Text = "FT_Read Timeout / ftstatus = " & strRxdata & " [" & Str(KK) & "]"
                    Return _flgRx
                    Exit Function
                Else
                    timeout_count += 1
                End If
            Else
                ToolStripStatusLabel4.Text = "FT_Read Error / ftstatus = " & ftStatus & " [" & Str(KK) & "]"
                Return _flgRx
                Exit Function
            End If
            System.Threading.Thread.Sleep(5)
        Loop

        If strRxdata <> "" Then
            strRxdata = Strings.Left(strRxdata, Len(strRxdata) - 1)
        End If

        ToolStripStatusLabel4.Text = "Read OK <== " & strRxdata & " / bytes - " & Str(lngTotalBytesRead) & " [" & Str(KK) & "]"
        flFailed = False
        Return _flgRx

    End Function

    Private Sub CmdSendPitch_Click(sender As Object, e As EventArgs) Handles CmdSendPitch.Click
        Dim X As Long
        Dim _flgTx As Integer
        'Dim sw As New System.Diagnostics.Stopwatch

        TxtPitch.Text = InputBox(StrInputPitch, StrInputPitch)

        strWdata = "PCH" & vbCr
        _flgTx = UsbWrite(strWdata)

        If _flgTx = 1 Then
            ToolStripStatusLabel4.Text = "FT_Write Failed / status = " & Str(ftStatus)
            Exit Sub
        Else
            X = 0
            Do
                X += 1

            Loop Until X = 30000

            If Val(TxtPitch.Text) > 6000 Then
                TxtPitch.Text = 6000
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

            _flgTx = UsbWrite(strWdata)

            strWdata = Strings.Left(strWdata, Len(strWdata) - 1)

            If _flgTx = 1 Then
                ToolStripStatusLabel4.Text = "FT_Write Failed / status = " & Str(ftStatus)
                Exit Sub
            Else
                ToolStripStatusLabel4.Text = "FT_Write OK ==> " & strWdata & " / bytes = " & Str(Len(strWdata))
            End If

            ReceivedData()

        End If

        FlgMainTest = 0

    End Sub

    Private Sub CmdUSBClose_Click(sender As Object, e As EventArgs) Handles CmdUSBClose.Click
        FT_Close(lngHandle)
        ToolStripStatusLabel4.Text = "FT_Close = " & Str(lngHandle)
    End Sub

    Private Sub CmdSendFeed_Click(sender As Object, e As EventArgs) Handles CmdSendFeed.Click
        Dim _flgTx As Integer

        strWdata = "FED" & vbCr
        _flgTx = UsbWrite(strWdata)

        strWdata = Strings.Left(strWdata, Len(strWdata) - 1)

        If _flgTx = 1 Then
            ToolStripStatusLabel4.Text = "FT_Write Failed / status = " & Str(ftStatus)
            Exit Sub
        Else
            ToolStripStatusLabel4.Text = "FT_Write OK ==> """ & strWdata & """ / bytes = " & Str(Len(strWdata))
        End If

        ReceivedData()

        FlgMainTest = 20

    End Sub

    Private Sub CmdSendMeas_Click(sender As Object, e As EventArgs) Handles CmdSendMeas.Click
        Dim _flgTx As Integer

        strWdata = "MES" & vbCr
        _flgTx = UsbWrite(strWdata)

        strWdata = Strings.Left(strWdata, Len(strWdata) - 1)

        If _flgTx = 1 Then
            ToolStripStatusLabel4.Text = "FT_Write Failed / status = " & Str(ftStatus)
        Else
            ToolStripStatusLabel4.Text = "FT_Write OK ==> """ & strWdata & """ / bytes = " & Str(Len(strWdata))
        End If

        flgFileLd = 0
        FlgDspTestData = 1

        If FlgTest = 1 Then
            timerCount1 = 650
            FlgMainTest = 11
        Else
            FlgMainTest = 10
        End If

    End Sub

    Private Sub RemakeData()
        Dim M As Integer

        If flgFileLd = 0 Then
            For M = 1 To 18
                DataPrcNum(KdData, SampleNo, M) = Math.Round(Us_Dist / DataPrcNum(KdData, SampleNo, M), 1)
            Next

            DataPrcNum(KdData, SampleNo, 19) = Val(DataPrcStr(KdData, SampleNo, 10))    'Num md/cd
            DataPrcNum(KdData, SampleNo, 20) = Val(DataPrcStr(KdData, SampleNo, 11))    'Num pk/dp

        Else
            For M = 1 To 18
                DataPrcNum(KdData, FileDataNo, M) = Math.Round(Us_Dist / DataPrcNum(KdData, FileDataNo, M), 1)
            Next

            DataPrcNum(KdData, FileDataNo, 19) = Val(DataPrcStr(KdData, FileDataNo, 10))    'Num md/cd
            DataPrcNum(KdData, FileDataNo, 20) = Val(DataPrcStr(KdData, FileDataNo, 11))    'Num pk/dp
        End If
    End Sub

    Private Sub DrawGraph()
        Dim TempNo As Integer
        Dim StepY As Single
        Dim N As Integer
        Dim PosY1 As Single
        Dim PosY2 As Single
        Dim path As New GraphicsPath

        Dim y0us As Single = test_graph_yaxis_max + (test_graph_SclY * 2)

        If flgFileLd = 0 Then
            TempNo = SampleNo
        Else
            TempNo = FileDataNo
        End If

        Select Case FlgDspTestData
            Case 1

        End Select

        StepY = test_graph_SclY / 10

        For N = 12 To 18
            PosY1 = y0us - (DataPrcNum(KdData, TempNo, N - 1) * StepY)
            PosY2 = y0us - (DataPrcNum(KdData, TempNo, N) * StepY)

            path.StartFigure()
            path.AddLine(Kax1, PosY1, Kax1 + test_graph_SclX, PosY2)
            Kax1 += test_graph_SclX
        Next
        PosY1 = y0us - (DataPrcNum(KdData, TempNo, 18) * StepY)
        PosY2 = y0us - (DataPrcNum(KdData, TempNo, 3) * StepY)
        path.StartFigure()
        path.AddLine(Kax1, PosY1, Kax1 + test_graph_SclX, PosY2)
        Kax1 += test_graph_SclX

        For N = 4 To 18
            PosY1 = y0us - (DataPrcNum(KdData, TempNo, N - 1) * StepY)
            PosY2 = y0us - (DataPrcNum(KdData, TempNo, N) * StepY)
            path.StartFigure()
            path.AddLine(Kax1, PosY1, Kax1 + test_graph_SclX, PosY2)
            Kax1 += test_graph_SclX
        Next
        PosY1 = y0us - (DataPrcNum(KdData, TempNo, 18) * StepY)
        PosY2 = y0us - (DataPrcNum(KdData, TempNo, 3) * StepY)
        path.StartFigure()
        path.AddLine(Kax1, PosY1, Kax1 + test_graph_SclX, PosY2)

        Kax1 = test_graph_x_sta

        If KdData = 1 Then
            graph_path_cur.Add(path)
        ElseIf KdData = 3 Then
            graph_path_old.Add(path)
        Else
            graph_path_cur.Add(path)
        End If

        PictureBox1.Refresh()
    End Sub

    Private Sub CmdClsGraph_Click(sender As Object, e As EventArgs) Handles CmdClsGraph.Click
        Dim L As Integer
        Dim M As Integer
        Dim N As Integer

        For L = 0 To 2
            For M = 0 To 20
                For N = 1 To 20
                    DataPrcNum(L, M, N) = 0
                Next
            Next
        Next

        ClsAngleData()
        ClsDataTest()
        ClsDefData()

        graph_path_cur.Clear()
        graph_path_old.Clear()

        PictureBox1.Refresh()

        CmdAnotherFileData.Enabled = False

        SampleNo = 0
        FileNo = 0
        FileDataNo = 0
    End Sub

    Private Sub ClsDefData()

        LblDf0.Text = ""
        LblDf1.Text = ""
        LblDf2.Text = ""
        LblDf3.Text = ""
        LblDf4.Text = ""
        LblDf5.Text = ""
        LblDf6.Text = ""
        LblDf7.Text = ""
        LblDf8.Text = ""
        LblDf9.Text = ""
        LblDf10.Text = ""
        LblDf11.Text = ""
        LblDf12.Text = ""
        LblDf13.Text = ""
        LblDf14.Text = ""
        LblDf15.Text = ""

        LblTT1.Text = ""
        LblDFMax.Text = ""
        LblDFMin.Text = ""

    End Sub

    Private Sub framAx_Click(sender As Object, e As EventArgs) Handles framAx.Click
        If CmdSendCLR.Visible = False Then
            CmdGetSerialNum.Visible = True
            CmdGetSerialNum.Enabled = True
            CmdSendCLR.Visible = True
            CmdSendCLR.Enabled = False      '基板側で機能未実装
            CmdSendTest.Visible = True
            CmdSendTest.Enabled = False     '基板側で機能未実装
            CmdSendReset.Visible = True
            CmdSendReset.Enabled = True
            CmdReceivedData.Visible = True
            CmdReceivedData.Enabled = True
        Else
            CmdGetSerialNum.Visible = False
            CmdGetSerialNum.Enabled = False
            CmdSendCLR.Visible = False
            CmdSendCLR.Enabled = False
            CmdSendTest.Visible = False
            CmdSendTest.Enabled = False
            CmdSendReset.Visible = False
            CmdSendReset.Enabled = False
            CmdReceivedData.Visible = False
            CmdReceivedData.Enabled = False
        End If
    End Sub

    Private Sub ClsDataTest()
        LblMeas0.Text = ""
        LblMeas1.Text = ""
        LblMeas2.Text = ""
        LblMeas3.Text = ""
        LblMeas4.Text = ""
        LblMeas5.Text = ""
        LblMeas6.Text = ""
        LblMeas7.Text = ""
        LblMeas8.Text = ""
        LblMeas9.Text = ""
        LblMeas10.Text = ""
        LblMeas11.Text = ""
        LblMeas12.Text = ""
        LblMeas13.Text = ""
        LblMeas14.Text = ""
        LblMeas15.Text = ""
        LblFile0.Text = ""
        LblFile1.Text = ""
        LblFile2.Text = ""
        LblFile3.Text = ""
        LblFile4.Text = ""
        LblFile5.Text = ""
        LblFile6.Text = ""
        LblFile7.Text = ""
        LblFile8.Text = ""
        LblFile9.Text = ""
        LblFile10.Text = ""
        LblFile11.Text = ""
        LblFile12.Text = ""
        LblFile13.Text = ""
        LblFile14.Text = ""
        LblFile15.Text = ""
    End Sub

    Private Sub DrawData()
        'ClsDataTest()

        If flgFileLd = 1 Then
            '過去
            LblFile0.Text = Strings.Format(DataPrcNum(3, FileDataNo, 11), "00.0")
            LblFile1.Text = Strings.Format(DataPrcNum(3, FileDataNo, 12), "00.0")
            LblFile2.Text = Strings.Format(DataPrcNum(3, FileDataNo, 13), "00.0")
            LblFile3.Text = Strings.Format(DataPrcNum(3, FileDataNo, 14), "00.0")
            LblFile4.Text = Strings.Format(DataPrcNum(3, FileDataNo, 15), "00.0")
            LblFile5.Text = Strings.Format(DataPrcNum(3, FileDataNo, 16), "00.0")
            LblFile6.Text = Strings.Format(DataPrcNum(3, FileDataNo, 17), "00.0")
            LblFile7.Text = Strings.Format(DataPrcNum(3, FileDataNo, 18), "00.0")
            LblFile8.Text = Strings.Format(DataPrcNum(3, FileDataNo, 3), "00.0")
            LblFile9.Text = Strings.Format(DataPrcNum(3, FileDataNo, 4), "00.0")
            LblFile10.Text = Strings.Format(DataPrcNum(3, FileDataNo, 5), "00.0")
            LblFile11.Text = Strings.Format(DataPrcNum(3, FileDataNo, 6), "00.0")
            LblFile12.Text = Strings.Format(DataPrcNum(3, FileDataNo, 7), "00.0")
            LblFile13.Text = Strings.Format(DataPrcNum(3, FileDataNo, 8), "00.0")
            LblFile14.Text = Strings.Format(DataPrcNum(3, FileDataNo, 9), "00.0")
            LblFile15.Text = Strings.Format(DataPrcNum(3, FileDataNo, 10), "00.0")
        Else
            '測定
            LblMeas0.Text = Strings.Format(DataPrcNum(1, SampleNo, 11), "00.0")
            LblMeas1.Text = Strings.Format(DataPrcNum(1, SampleNo, 12), "00.0")
            LblMeas2.Text = Strings.Format(DataPrcNum(1, SampleNo, 13), "00.0")
            LblMeas3.Text = Strings.Format(DataPrcNum(1, SampleNo, 14), "00.0")
            LblMeas4.Text = Strings.Format(DataPrcNum(1, SampleNo, 15), "00.0")
            LblMeas5.Text = Strings.Format(DataPrcNum(1, SampleNo, 16), "00.0")
            LblMeas6.Text = Strings.Format(DataPrcNum(1, SampleNo, 17), "00.0")
            LblMeas7.Text = Strings.Format(DataPrcNum(1, SampleNo, 18), "00.0")
            LblMeas8.Text = Strings.Format(DataPrcNum(1, SampleNo, 3), "00.0")
            LblMeas9.Text = Strings.Format(DataPrcNum(1, SampleNo, 4), "00.0")
            LblMeas10.Text = Strings.Format(DataPrcNum(1, SampleNo, 5), "00.0")
            LblMeas11.Text = Strings.Format(DataPrcNum(1, SampleNo, 6), "00.0")
            LblMeas12.Text = Strings.Format(DataPrcNum(1, SampleNo, 7), "00.0")
            LblMeas13.Text = Strings.Format(DataPrcNum(1, SampleNo, 8), "00.0")
            LblMeas14.Text = Strings.Format(DataPrcNum(1, SampleNo, 9), "00.0")
            LblMeas15.Text = Strings.Format(DataPrcNum(1, SampleNo, 10), "00.0")

            'ClsAngleData()

            LblMD.Text = Format(DataPrcNum(KdData, SampleNo, 19), "0.00")
            LblCD.Text = Format(DataPrcNum(KdData, SampleNo, 20), "0.00")

            DataPrcNum(0, 0, 19) += DataPrcNum(KdData, SampleNo, 19)
            DataPrcNum(0, 0, 20) += DataPrcNum(KdData, SampleNo, 20)

            LblMDAvg.Text = Format(DataPrcNum(0, 0, 19) / SampleNo, "0.00")
            LblCDAvg.Text = Format(DataPrcNum(0, 0, 20) / SampleNo, "0.00")
        End If
    End Sub

    Private Sub ClsAngleData()
        LblMD.Text = ""
        LblMDAvg.Text = ""
        LblCD.Text = ""
        LblCDAvg.Text = ""
    End Sub

    Private Sub DrawDef()
        Dim DFtemp As Single
        Dim DFmax As Single
        Dim DFmin As Single

        DFmax = -99
        DFmin = 99

        LblTT1.Text = "F= " & FileDataNo & "-- " & "S= " & SampleNo

        If FileDataNo = 0 Or SampleNo = 0 Then
            LblDf0.Text = "--"
            LblDf1.Text = "--"
            LblDf2.Text = "--"
            LblDf3.Text = "--"
            LblDf4.Text = "--"
            LblDf5.Text = "--"
            LblDf6.Text = "--"
            LblDf7.Text = "--"
            LblDf8.Text = "--"
            LblDf9.Text = "--"
            LblDf10.Text = "--"
            LblDf11.Text = "--"
            LblDf12.Text = "--"
            LblDf13.Text = "--"
            LblDf14.Text = "--"
            LblDf15.Text = "--"

            DFmax = 0
            DFmin = 0
        Else
            If flgFileLd = 1 Then
                DFtemp = ((DataPrcNum(1, SampleNo, 11) / DataPrcNum(3, FileDataNo, 11)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf0.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 12) / DataPrcNum(3, FileDataNo, 12)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf1.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 13) / DataPrcNum(3, FileDataNo, 13)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf2.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 14) / DataPrcNum(3, FileDataNo, 14)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf3.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 15) / DataPrcNum(3, FileDataNo, 15)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf4.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 16) / DataPrcNum(3, FileDataNo, 16)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf5.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 17) / DataPrcNum(3, FileDataNo, 17)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf6.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 18) / DataPrcNum(3, FileDataNo, 18)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf7.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 3) / DataPrcNum(3, FileDataNo, 3)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf8.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 4) / DataPrcNum(3, FileDataNo, 4)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf9.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 5) / DataPrcNum(3, FileDataNo, 5)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf10.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 6) / DataPrcNum(3, FileDataNo, 6)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf11.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 7) / DataPrcNum(3, FileDataNo, 7)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf12.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 8) / DataPrcNum(3, FileDataNo, 8)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf13.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 9) / DataPrcNum(3, FileDataNo, 9)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf14.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 10) / DataPrcNum(3, FileDataNo, 10)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf15.Text = Format(DFtemp, "0.0")
            Else
                DFtemp = ((DataPrcNum(1, SampleNo, 11) / DataPrcNum(3, FileDataNo, 11)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf0.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 12) / DataPrcNum(3, FileDataNo, 12)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf1.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 13) / DataPrcNum(3, FileDataNo, 13)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf2.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 14) / DataPrcNum(3, FileDataNo, 14)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf3.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 15) / DataPrcNum(3, FileDataNo, 15)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf4.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 16) / DataPrcNum(3, FileDataNo, 16)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf5.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 17) / DataPrcNum(3, FileDataNo, 17)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf6.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 18) / DataPrcNum(3, FileDataNo, 18)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf7.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 3) / DataPrcNum(3, FileDataNo, 3)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf8.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 4) / DataPrcNum(3, FileDataNo, 4)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf9.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 5) / DataPrcNum(3, FileDataNo, 5)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf10.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 6) / DataPrcNum(3, FileDataNo, 6)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf11.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 7) / DataPrcNum(3, FileDataNo, 7)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf12.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 8) / DataPrcNum(3, FileDataNo, 8)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf13.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 9) / DataPrcNum(3, FileDataNo, 9)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf14.Text = Format(DFtemp, "0.0")
                DFtemp = ((DataPrcNum(1, SampleNo, 10) / DataPrcNum(3, FileDataNo, 10)) - 1) * 100
                If DFmax < DFtemp Then DFmax = DFtemp
                If DFmin > DFtemp Then DFmin = DFtemp
                LblDf15.Text = Format(DFtemp, "0.0")
            End If
        End If

        LblDFMax.Text = Format(DFmax, "0.0")
        LblDFMin.Text = Format(DFmin, "0.0")


    End Sub

    Private Sub CmdLoadFileData_Click(sender As Object, e As EventArgs) Handles CmdLoadFileData.Click
        FlgDspTestData = 3
        FlgMainTest = 30

        flgFileLd = 1
    End Sub

    Private Sub CmdAnotherFileData_Click(sender As Object, e As EventArgs) Handles CmdAnotherFileData.Click
        flgFileLd = 1
        FlgMainTest = 35
    End Sub

    Private Sub CmdSendCLR_Click(sender As Object, e As EventArgs) Handles CmdSendCLR.Click
        Dim _flgTx As Integer

        strWdata = "CLR" & vbCr
        _flgTx = UsbWrite(strWdata)

        strWdata = Strings.Left(strWdata, Len(strWdata) - 1)

        If _flgTx = 1 Then
            ToolStripStatusLabel4.Text = "FT_Write Failed / status = " & Str(ftStatus)
            Exit Sub
        Else
            ToolStripStatusLabel4.Text = "FT_Write OK ==> """ & strWdata & """ / bytes = " & Str(Len(strWdata))
        End If

        ReceivedData()

        FlgMainTest = 0

    End Sub

    Private Sub CmdSendTest_Click(sender As Object, e As EventArgs) Handles CmdSendTest.Click
        Dim _flgTx As Integer

        strWdata = "TXT" & vbCr
        _flgTx = UsbWrite(strWdata)

        strWdata = Strings.Left(strWdata, Len(strWdata) - 1)

        If _flgTx = 1 Then
            ToolStripStatusLabel4.Text = "FT_Write Failed / status = " & Str(ftStatus)
            Exit Sub
        Else
            ToolStripStatusLabel4.Text = "FT_Write OK ==> """ & strWdata & """ / bytes = " & Str(Len(strWdata))
        End If

        ReceivedData()

        FlgMainTest = 20


    End Sub

    Private Sub CmdSendReset_Click(sender As Object, e As EventArgs) Handles CmdSendReset.Click
        Dim _flgTx As Integer

        strWdata = "RES" & vbCr
        _flgTx = UsbWrite(strWdata)

        strWdata = Strings.Left(strWdata, Len(strWdata) - 1)

        If _flgTx = 1 Then
            ToolStripStatusLabel4.Text = "FT_Write Failed / status = " & Str(ftStatus)
            Exit Sub
        Else
            ToolStripStatusLabel4.Text = "FT_Write OK ==> """ & strWdata & """ / bytes = " & Str(Len(strWdata))
        End If

        ReceivedData()

        FlgMainTest = 0
    End Sub

    Private Sub CmdSendReady_Click(sender As Object, e As EventArgs) Handles CmdSendReady.Click
        Dim _flgTx As Integer

        strWdata = "RDY" & vbCr
        _flgTx = UsbWrite(strWdata)

        strWdata = Strings.Left(strWdata, Len(strWdata) - 1)

        If _flgTx = 1 Then
            ToolStripStatusLabel4.Text = "FT_Write Failed / status = " & Str(ftStatus)
            Exit Sub
        Else
            ToolStripStatusLabel4.Text = "FT_Write OK ==> """ & strWdata & """ / bytes = " & Str(Len(strWdata))
        End If

        ReceivedData()

        FlgMainTest = 0
    End Sub

    Private Sub FrmSST4500_1_0_0E_test_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        FlgMainTest = 90
    End Sub
End Class