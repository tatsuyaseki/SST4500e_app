﻿Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports Microsoft.Office.Interop.Excel

Public Class FrmSST4500_1_0_0E_pitchsetting
    Dim _flg_init As Integer
    Dim changed_row As Integer
    Dim _flg_ng As Integer  '2=OKで保存済み、1=OKで未保存、0=NG

    Private Sub CmdRowsAdd_Click(sender As Object, e As EventArgs) Handles CmdRowsAdd.Click
        '選択行の下に追加する

        Dim _sel_row As Integer
        Dim _rows_count As Integer
        _sel_row = DataGridView1.SelectedCells(0).RowIndex
        Console.WriteLine("SelectedRowsIndex : " & _sel_row)
        _rows_count = DataGridView1.Rows.Count
        Console.WriteLine("RowsCount : " & _rows_count)

        If _rows_count = 1 Then
            '1行しかない場合
            DataGridView1.Rows.Add()
        Else
            '2行以上ある場合
            If _sel_row + 1 = _rows_count Then
                '選択行が最終行の場合
                DataGridView1.Rows.Add()
            Else
                '選択行が最終行ではない場合
                DataGridView1.Rows.Insert(_sel_row + 1)
            End If
        End If

        _rows_count = DataGridView1.Rows.Count
        For i = 0 To _rows_count - 2
            DataGridView1.Rows(i).Cells(0).Value = i + 1
        Next
        If _rows_count > 1 Then
            TxtPitchNum.Text = _rows_count - 1
            TxtPoints.Text = _rows_count
        Else
            TxtPitchNum.Text = 0
            TxtPoints.Text = 0
        End If
        cmd_enadis()
    End Sub

    Private Sub FrmSST4500_1_0_0E_pitchsetting_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        TabControl1.SelectedIndex = 0   '現在地タブ選択

        _flg_init = 0
        If Me.Visible = True Then

            Label5.Text = "*Total pitch should be less than" & vbCrLf &
                          "  Sample length - 420mm" & vbCrLf &
                          "  (both edge length correction)"

            _flg_ng = 1 '一旦OKにする
            'If FlgPitchExp_Load = 1 Then
            'ロード済みの場合セットする
            'SetConstPitch()

            'TxtPchExpLoadedFile.Text = PchExpSettingFile
            'Else
            '未ロードの場合新規作成状態
            'TxtLength.Text = Length 'とりあえず測定画面のサンプル長をセットする
            'TxtPitchNum.Text = 0
            'TxtPoints.Text = 0
            'DataGridView1.Rows.Clear()
            'PchExpSettingFile = ""
            'PchExpSettingFile_FullPath = ""
            'End If

            'TxtLengthSum.Text = Data_sum()
            SetConstPitch()

            Data_chk()
            cmd_enadis()

            'data_backup(TabControl1.SelectedIndex)
        End If

        _flg_init = 1   '初期化完了
    End Sub

    Private Sub SetConstPitch()
        Dim _pitchnum As Integer
        Dim _points As Single

        If FlgPitchExp_Load = 1 Then
            _pitchnum = UBound(PchExp_PchData) + 1
            TxtPitchNum.Text = _pitchnum
            TxtPoints.Text = _pitchnum + 1
            TxtLength.Text = PchExp_Length

            With DataGridView1
                .Rows.Clear()
                For i = 0 To _pitchnum - 1
                    .Rows.Add()
                    .Rows(i).Cells(0).Value = i + 1
                    .Rows(i).Cells(1).Value = PchExp_PchData(i)
                Next
            End With

            'プロファイル測定画面にも適用する
            If FlgPitchExp = 1 Then
                With FrmSST4500_1_0_0E_Profile
                    .TxtLength.Text = PchExp_Length
                    .TxtPitch.Text = PchExp_PchData(0)
                    _points = _pitchnum + 1
                    .TxtPoints.Text = _points

                    If SampleNo = 0 And FileDataMax = 0 Then
                        .DrawCalcCurData_init()
                        .DrawCalcBakData_init()
                        .DrawCalcAvgData_init()
                        .DrawTableData_init()
                        .GraphInitPrf(_points)
                    End If
                End With
            End If
        Else
            '未ロードの場合新規作成状態
            TxtLength.Text = Length 'とりあえず測定画面のサンプル長をセットする
            TxtPitchNum.Text = 0
            TxtPoints.Text = 0
            DataGridView1.Rows.Clear()
            PchExpSettingFile = ""
            PchExpSettingFile_FullPath = ""

        End If

        TxtPchExpLoadedFile.Text = PchExpSettingFile
        TxtLengthSum.Text = Data_sum()
    End Sub

    Private Sub SetPchExpOld()
        Dim _pitchnum As Integer
        Dim _pitch_sum As Single

        If FlgPitchExp_Load_old = 1 Then
            _pitchnum = UBound(PchExp_PchData_old) + 1
            TxtPitchNum.Text = _pitchnum
            TxtPoints.Text = _pitchnum + 1
            TxtLength.Text = PchExp_Length_old

            For Each _pitch_sum_tmp In PchExp_PchData_old
                _pitch_sum += _pitch_sum_tmp
            Next
            TxtLengthSum.Text = _pitch_sum

            TxtPchExpLoadedFile.Text = Path.GetFileName(PchExpSettingFile_FullPath_old)

            With DataGridView2
                .Rows.Clear()
                For i = 0 To _pitchnum - 1
                    .Rows.Add()
                    .Rows(i).Cells(0).Value = i + 1
                    .Rows(i).Cells(1).Value = PchExp_PchData_old(i)
                Next
            End With

        Else
            TxtLength.Text = 0
            TxtPitchNum.Text = 0
            TxtPoints.Text = 0
            TxtLengthSum.Text = 0
            DataGridView2.Rows.Clear()
            If FlgPitchExp_Load_old = 2 Then
                TxtPchExpLoadedFile.Text = "Unable to Open Pitch Setting File"
            Else
                TxtPchExpLoadedFile.Text = ""
            End If
        End If

        LblResult.Text = ""
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Console.WriteLine("CellEndEdit : ")
        Dim _sel_row As Integer
        Dim _pitch_sum As Single
        Dim _rows_count As Integer

        _sel_row = DataGridView1.SelectedCells(0).RowIndex
        Console.WriteLine("SelectedRowsIndex : " & _sel_row)
        _pitch_sum = Data_sum()
        TxtLengthSum.Text = _pitch_sum
        _rows_count = DataGridView1.Rows.Count
        If _rows_count > 1 Then
            TxtPitchNum.Text = _rows_count - 1
            TxtPoints.Text = _rows_count
        Else
            TxtPitchNum.Text = 0
            TxtPoints.Text = 0
        End If

        Data_chk()
    End Sub

    Function Data_sum() As Single
        Dim _rows_count As Integer
        Dim _pitch_sum As Single

        _rows_count = DataGridView1.Rows.Count
        For i = 0 To _rows_count - 2
            DataGridView1.Rows(i).Cells(0).Value = i + 1
            _pitch_sum += DataGridView1.Rows(i).Cells(1).Value
        Next

        Return _pitch_sum
    End Function

    Private Sub Data_chk()
        Dim _length As Single
        Dim _pitch_sum As Single
        Dim _rows_count As Integer

        _length = TxtLength.Text
        _pitch_sum = TxtLengthSum.Text
        _rows_count = DataGridView1.Rows.Count

        If _pitch_sum = 0 Then
            'NG
            LblResult.Text = "NG"
            LblResult.ForeColor = Color.Red
            CmdSave.Enabled = False
            _flg_ng = 0
        Else
            If _pitch_sum > _length - LnCmp Then
                'NG
                LblResult.Text = "NG"
                LblResult.ForeColor = Color.Red
                CmdSave.Enabled = False
                _flg_ng = 0
            Else
                'OK
                LblResult.Text = "OK"
                LblResult.ForeColor = Color.Green
                CmdSave.Enabled = True
                If _flg_ng = 0 Then
                    _flg_ng = 1
                Else
                    _flg_ng = 2
                End If
            End If
        End If
    End Sub

    Private Sub CmdRowsDel_Click(sender As Object, e As EventArgs) Handles CmdRowsDel.Click
        Dim _sel_row As Integer
        Dim _rows_count As Integer
        Dim _pitch_sum As Single
        _sel_row = DataGridView1.SelectedCells(0).RowIndex
        _rows_count = DataGridView1.Rows.Count

        'コマンドが実行できるのは消せる条件がそろっているから
        RemoveHandler DataGridView1.CellEnter, AddressOf DataGridView1_CellEnter
        DataGridView1.Rows.RemoveAt(_sel_row)
        AddHandler DataGridView1.CellEnter, AddressOf DataGridView1_CellEnter

        _pitch_sum = Data_sum()
        TxtLengthSum.Text = _pitch_sum
        _rows_count = DataGridView1.Rows.Count
        If _rows_count > 1 Then
            TxtPitchNum.Text = _rows_count - 1
            TxtPoints.Text = _rows_count
        Else
            TxtPitchNum.Text = 0
            TxtPoints.Text = 0
        End If
        Data_chk()
        cmd_enadis()
    End Sub

    Private Sub cmd_enadis()
        Dim _rows_count As Integer
        Dim _sel_row As Integer
        Dim _sel_rows_count As Integer
        _rows_count = DataGridView1.Rows.Count
        _sel_rows_count = DataGridView1.SelectedCells.Count
        If _sel_rows_count > 0 Then
            _sel_row = DataGridView1.SelectedCells(0).RowIndex
        End If
        Console.WriteLine("sel : " & _sel_row & ", rows_count : " & _rows_count)

        If TabControl1.SelectedIndex = 0 Then
            TxtLength.Enabled = True
            CmdRowsAdd.Enabled = True
            CmdLoad.Enabled = True
            If _rows_count = 1 Then
                '1行のみの場合
                CmdRowsDel.Enabled = False
                CmdRowsMvUp.Enabled = False
                CmdRowsMvDn.Enabled = False
                CmdAllRowsDel.Enabled = False
            ElseIf _rows_count = 2 Then
                '2行の場合
                If _sel_row = 0 Then
                    '1行目選択時はOK
                    CmdRowsDel.Enabled = True
                Else
                    '2行目(最終行)選択時はNG
                    CmdRowsDel.Enabled = False
                End If
                CmdRowsMvUp.Enabled = False
                CmdRowsMvDn.Enabled = False
                CmdAllRowsDel.Enabled = True
            Else
                CmdAllRowsDel.Enabled = True
                If _sel_row = 0 Then
                    CmdRowsDel.Enabled = True
                    CmdRowsMvUp.Enabled = False
                    CmdRowsMvDn.Enabled = True
                ElseIf _sel_row = _rows_count - 1 Then
                    CmdRowsDel.Enabled = False
                    CmdRowsMvUp.Enabled = False
                    CmdRowsMvDn.Enabled = False
                ElseIf _sel_row = _rows_count - 2 Then
                    CmdRowsDel.Enabled = True
                    CmdRowsMvUp.Enabled = True
                    CmdRowsMvDn.Enabled = False
                Else
                    CmdRowsDel.Enabled = True
                    CmdRowsMvUp.Enabled = True
                    CmdRowsMvDn.Enabled = True
                End If

            End If
        Else
            TxtLength.Enabled = False
            CmdRowsAdd.Enabled = False
            CmdRowsDel.Enabled = False
            CmdRowsMvUp.Enabled = False
            CmdRowsMvDn.Enabled = False
            CmdAllRowsDel.Enabled = False
            CmdLoad.Enabled = False
            CmdSave.Enabled = False
        End If
    End Sub

    Private Sub DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
        'Dim _rows_count As Integer
        'Dim _sel_row As Integer
        '_rows_count = DataGridView1.Rows.Count

        If _flg_init = 1 Then
            '_sel_row = DataGridView1.SelectedCells(0).RowIndex

            cmd_enadis()
        End If
    End Sub

    Private Sub CmdClose_Click(sender As Object, e As EventArgs) Handles CmdClose.Click
        'ロード済みかそうでないかで処理を帰る
        'ロード済み出ない場合は、ピッチ拡張設定を解除する
        'ロード済みの場合は、念のため変更内容が破棄されるアナウンスをする
        Dim _result As DialogResult

        If FlgPitchExp_Load = 0 Then
            '未ロードの場合
            With FrmSST4500_1_0_0E_Profile
                RemoveHandler .ChkPitchExp_Ena.CheckedChanged, AddressOf .ChkPitchExp_Ena_CheckedChanged
                .ChkPitchExp_Ena.Checked = False
                AddHandler .ChkPitchExp_Ena.CheckedChanged, AddressOf .ChkPitchExp_Ena_CheckedChanged

                .ChkPitchExp_Dis.Checked = True
            End With
            LoadConstPitch_FileErr_Run = 0
            Me.Visible = False
        Else
            If _flg_ng = 0 Then
                MessageBox.Show("Total pitch should be less than" & vbCrLf &
                                "Sample length - " & LnCmp & "mm (both edge length correction)",
                                "Total length Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error)
            ElseIf _flg_ng = 1 Then
                MessageBox.Show("Save the total length after correction",
                                "Unsaved",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning)
            Else
                'ロード済みの場合
                _result = MessageBox.Show("Unsaved changes will be discarded," & vbCrLf &
                                          "are you sure to close?",
                                          "Confirm",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning)
                If _result = vbYes Then
                    LoadConstPitch_FileErr_Run = 0
                    Me.Visible = False

                End If
            End If
        End If
    End Sub

    Private Sub CmdAllRowsDel_Click(sender As Object, e As EventArgs) Handles CmdAllRowsDel.Click
        Dim result_tmp As DialogResult
        Dim _rows_count As Integer
        Dim _pitch_sum As Single

        result_tmp = MessageBox.Show("Are you sure you want to delete?",
                                     "Confirm Delete",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Warning)
        If result_tmp = vbYes Then
            DataGridView1.Rows.Clear()
            _rows_count = DataGridView1.Rows.Count
            If _rows_count > 1 Then
                TxtPitchNum.Text = _rows_count - 1
                TxtPoints.Text = _rows_count
            Else
                TxtPitchNum.Text = 0
                TxtPoints.Text = 0
            End If
        End If
        _pitch_sum = Data_sum()
        TxtLengthSum.Text = _pitch_sum
        cmd_enadis()
    End Sub

    Private Sub CmdRowsMvUp_Click(sender As Object, e As EventArgs) Handles CmdRowsMvUp.Click
        Dim _sel_row As Integer
        Dim _sel_col As Integer
        Dim _prv_cellvalue As Integer
        Dim _cur_cellvalue As Integer

        With DataGridView1
            _sel_row = .CurrentCell.RowIndex
            _sel_col = .CurrentCell.ColumnIndex
            '1つ上のセルの値
            _prv_cellvalue = .Rows(_sel_row - 1).Cells(1).Value
            '現在のセルの値
            _cur_cellvalue = .Rows(_sel_row).Cells(1).Value
            '現在のセルの値を1つ上のセルにコピーする
            .Rows(_sel_row - 1).Cells(1).Value = _cur_cellvalue
            '1つ上のセルの値を現在のセルにコピーする
            .Rows(_sel_row).Cells(1).Value = _prv_cellvalue
            .CurrentCell = .Item(_sel_col, _sel_row - 1)
        End With
    End Sub

    Private Sub CmdRowsMvDn_Click(sender As Object, e As EventArgs) Handles CmdRowsMvDn.Click
        Dim _sel_row As Integer
        Dim _sel_col As Integer
        Dim _nxt_cellvalue As Integer
        Dim _cur_cellvalue As Integer

        With DataGridView1
            _sel_row = .CurrentCell.RowIndex
            _sel_col = .CurrentCell.ColumnIndex
            '1つ下のセルの値
            _nxt_cellvalue = .Rows(_sel_row + 1).Cells(1).Value
            '現在のセルの値
            _cur_cellvalue = .Rows(_sel_row).Cells(1).Value
            '現在のセルの値を1つ下のセルにコピーする
            .Rows(_sel_row + 1).Cells(1).Value = _cur_cellvalue
            '1下のセルの値を現在のセルにコピーする
            .Rows(_sel_row).Cells(1).Value = _nxt_cellvalue
            .CurrentCell = .Item(_sel_col, _sel_row + 1)
        End With
    End Sub

    Private Sub CmdSave_Click(sender As Object, e As EventArgs) Handles CmdSave.Click
        Dim _rows_count As Integer
        Dim _data_array(0) As Single
        Dim result_tmp As DialogResult
        Dim _filename_const As String
        Dim _filepath As String
        Dim pitchfile_bak As String
        Dim chk_filename As String
        Dim chk_filehead As String
        Dim _points As Single

        chk_filehead = "PF"

        _filename_const = Path.GetFileNameWithoutExtension(StrConstFileName)
        pitchfile_bak = PchExpSettingFile_FullPath

        Using dialog As New SaveFileDialog
            With dialog
                .InitialDirectory = cur_dir & DEF_CONST_FILE_FLD
                .Title = StrSavePchSetFile
                .Filter = "Pitch Exp File(PF*.pitch)|PF*.pitch"
                If pitchfile_bak = "" Then
                    .FileName = _filename_const & StrConstFileName_PchExp
                Else
                    .FileName = Path.GetFileNameWithoutExtension(pitchfile_bak) & StrConstFileName_PchExp
                End If

                result_tmp = .ShowDialog

                If result_tmp = DialogResult.OK Then
                    _filepath = .FileName

                    chk_filename = Strings.Left(Path.GetFileName(_filepath), 2)
                    If chk_filename <> chk_filehead Then
                        MessageBox.Show("File Name must start with """ & chk_filehead & """" & vbCrLf &
                                        "Cancel the save process",
                                        StrFileNameErr,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    _rows_count = DataGridView1.Rows.Count
                    For i = 0 To _rows_count - 2
                        If i = 0 Then
                            _data_array(i) = DataGridView1.Rows(i).Cells(1).Value
                        Else
                            ReDim Preserve _data_array(i)
                            _data_array(i) = DataGridView1.Rows(i).Cells(1).Value
                        End If
                    Next

                    SaveConst_PchExp(_data_array, Val(TxtLength.Text), _filepath)
                    PchExpSettingFile_FullPath = _filepath
                    PchExpSettingFile = Path.GetFileName(_filepath)
                    TxtPchExpLoadedFile.Text = PchExpSettingFile

                    If FlgPitchExp = 1 Then
                        With FrmSST4500_1_0_0E_Profile
                            .TxtLength.Text = PchExp_Length
                            .TxtPitch.Text = _data_array(0)
                            _points = _rows_count
                            .TxtPoints.Text = _points

                            .DrawCalcCurData_init()
                            .DrawCalcBakData_init()
                            .DrawCalcAvgData_init()
                            .DrawTableData_init()
                            .GraphInitPrf(_points)
                        End With
                    End If

                    _flg_ng = 2
                End If

            End With
        End Using

        If pitchfile_bak <> PchExpSettingFile_FullPath Then
            ConstChangeTrue(FrmSST4500_1_0_0E_Profile, title_text2)
        End If

    End Sub

    Private Sub DataGridView1_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        Console.WriteLine("CellValidated : ")
        'If e.RowIndex = dgv.NewRowIndex OrElse Not dgv.IsCurrentCellDirty Then
        'Exit Sub
        'End If
        'Console.WriteLine("CellValidated1 : ")

    End Sub

    Private Sub DataGridView1_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        If e.RowIndex = dgv.NewRowIndex OrElse Not dgv.IsCurrentCellDirty Then
            Exit Sub
        End If

        changed_row = e.RowIndex
        Console.WriteLine("changed_row : " & e.RowIndex)
        Dim oldValue = dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        Console.WriteLine("oldValue : " & oldValue)

        Dim newValue As String = e.FormattedValue.ToString
        Console.WriteLine("newValue : " & newValue)

        If IsNumeric(newValue) = False Then
            MessageBox.Show(StrInputPitch,
                            StrInputErr,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)

            dgv.CancelEdit()

            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub DataGridView1_CellParsing(sender As Object, e As DataGridViewCellParsingEventArgs) Handles DataGridView1.CellParsing
        Dim dgv As DataGridView = DirectCast(sender, DataGridView)
        Console.WriteLine("CellParsing RowIndex : " & e.RowIndex)

        Dim curValue As Integer = e.Value
        Console.WriteLine("CellParsing e.Value : " & e.Value)
        If curValue <= min_Pitch Then
            e.Value = min_Pitch
            e.ParsingApplied = True
        ElseIf curValue > max_Pitch Then
            e.Value = max_Pitch
            e.ParsingApplied = True
        End If
    End Sub

    Private Sub TxtLength_Validated(sender As Object, e As EventArgs) Handles TxtLength.Validated
        Console.WriteLine("Pitch Ext TexLenght.Validated")

        '特に何もしない
    End Sub

    Private Sub TxtLength_Validating(sender As Object, e As CancelEventArgs) Handles TxtLength.Validating
        Console.WriteLine("Pitch Ext TextLength.Validating")
        If IsNumeric(TxtLength.Text) = False Then
            MessageBox.Show(StrInputPitch,
                            StrInputErr,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
            TxtLength.Text = Length
        Else
            Length_tmp = Math.Truncate(Val(TxtLength.Text))
            TxtLength.Text = Length_tmp

        End If

        If Length_tmp < LnCmp + min_Pitch Then
            MessageBox.Show("Input More Than " & LnCmp + min_Pitch & "mm",
                            StrInputErr,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation)
            TxtLength.Text = Length
        End If

    End Sub

    Private Sub TxtLength_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtLength.KeyDown
        If e.KeyCode = Keys.Enter Then
            Console.WriteLine("TxtLength.KeyDown Enter")
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        End If
    End Sub

    Private Sub CmdLoad_Click(sender As Object, e As EventArgs) Handles CmdLoad.Click
        Dim result_tmp As DialogResult
        Dim pitchfile_bak As String

        pitchfile_bak = PchExpSettingFile_FullPath

        Using dialog As New OpenFileDialog
            With dialog
                .InitialDirectory = cur_dir & DEF_CONST_FILE_FLD
                .Title = StrLoadPchSetFile
                .CheckFileExists = True
                .Filter = "Pitch Exp File(PF*.pitch)|PF*.pitch"
                .FileName = PchExpSettingFile

                result_tmp = .ShowDialog

                If result_tmp = DialogResult.OK Then
                    LoadConstPitch(.FileName)
                    PchExpSettingFile_FullPath = .FileName
                    PchExpSettingFile = Path.GetFileName(.FileName)
                    TxtPchExpLoadedFile.Text = PchExpSettingFile

                    SetConstPitch()

                    Data_chk()
                    cmd_enadis()

                    _flg_ng = 2
                End If

            End With
        End Using

        If pitchfile_bak <> PchExpSettingFile_FullPath Then
            ConstChangeTrue(FrmSST4500_1_0_0E_Profile, title_text2)
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        cmd_enadis()

        Select Case TabControl1.SelectedIndex
            Case 0
                'data_backup(1)
                SetConstPitch()
                Data_chk()
            Case 1
                'data_backup(0)
                SetPchExpOld()
        End Select
        'restore_backup(TabControl1.SelectedIndex)

    End Sub

End Class