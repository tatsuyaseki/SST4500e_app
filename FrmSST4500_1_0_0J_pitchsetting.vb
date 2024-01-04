Imports System.IO
Imports System.Text
Imports Microsoft.Office.Interop.Excel

Public Class FrmSST4500_1_0_0J_pitchsetting
    Dim _flg_init As Integer
    Dim changed_row As Integer
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

    Private Sub FrmSST4500_1_0_0J_pitchsetting_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        Dim _pitch_sum As Integer

        _flg_init = 0
        If Me.Visible = True Then
            Label5.Text = "※サンプル長 - 両端補正値(" & LnCmp & "mm)以下になる" & vbCrLf &
                          "　様に設定して下さい。"

            TxtLength.Text = Length

            'ピッチ拡張設定の有効無効は関係ない
            If FlgPitchExp_Load = 1 Then
                'ロード済みの場合セットする
                SetConstPitch()
            Else
                '未ロードの場合新規作成状態
                TxtPitchNum.Text = 0
                TxtPoints.Text = 0
                'MessageBox.Show("ピッチ拡張設定がされていません。" & vbCrLf &
                '                "ピッチ拡張設定を有効にするためには、" & vbCrLf &
                '                "ピッチ拡張設定を行って下さい。",
                '                "確認",
                'MessageBoxButtons.OK,
                'MessageBoxIcon.Information)
            End If
            _pitch_sum = Data_sum()
            TxtLengthSum.Text = _pitch_sum
            Data_chk()
            cmd_enadis()
        End If
        _flg_init = 1
    End Sub

    Private Sub SetConstPitch()
        Dim _pitchnum As Integer
        Dim _pitch_sum As Integer

        _pitchnum = UBound(PchExp_PchData) + 1
        TxtPitchNum.Text = _pitchnum
        TxtPoints.Text = _pitchnum + 1

        For Each _pitch_sum_tmp In PchExp_PchData
            _pitch_sum += _pitch_sum_tmp
        Next
        TxtLengthSum.Text = _pitch_sum

        DataGridView1.Rows.Clear()
        For i = 0 To _pitchnum - 1
            DataGridView1.Rows.Add()
            DataGridView1.Rows(i).Cells(0).Value = i + 1
            DataGridView1.Rows(i).Cells(1).Value = PchExp_PchData(i)
        Next

        data_chk()
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Console.WriteLine("CellEndEdit : ")
        Dim _sel_row As Integer
        Dim _pitch_sum As Integer
        _sel_row = DataGridView1.SelectedCells(0).RowIndex
        Console.WriteLine("SelectedRowsIndex : " & _sel_row)
        _pitch_sum = Data_sum()
        TxtLengthSum.Text = _pitch_sum

        data_chk()
    End Sub

    Function Data_sum() As Integer
        Dim _rows_count As Integer
        Dim _pitch_sum As Integer

        _rows_count = DataGridView1.Rows.Count
        For i = 0 To _rows_count - 2
            DataGridView1.Rows(i).Cells(0).Value = i + 1
            _pitch_sum += DataGridView1.Rows(i).Cells(1).Value
        Next

        Return _pitch_sum
    End Function

    Private Sub Data_chk()
        Dim _length As Integer
        Dim _pitch_sum As Integer
        Dim _rows_count As Integer

        _length = TxtLength.Text
        _pitch_sum = TxtLengthSum.Text
        _rows_count = DataGridView1.Rows.Count

        If _pitch_sum = 0 Then
            'NG
            LblResult.Text = "NG"
            LblResult.ForeColor = Color.Red
            CmdSave.Enabled = False
        Else
            If _pitch_sum > _length - LnCmp Then
                'NG
                LblResult.Text = "NG"
                LblResult.ForeColor = Color.Red
                CmdSave.Enabled = False
            Else
                'OK
                LblResult.Text = "OK"
                LblResult.ForeColor = Color.Green
                CmdSave.Enabled = True
            End If
        End If
    End Sub

    Private Sub CmdRowsDel_Click(sender As Object, e As EventArgs) Handles CmdRowsDel.Click
        Dim _sel_row As Integer
        Dim _rows_count As Integer
        Dim _pitch_sum As Integer
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
            FrmSST4500_1_0_0J_Profile.ChkPitchExp.Checked = False
            Me.Visible = False
        Else
            'ロード済みの場合
            _result = MessageBox.Show("変更済みで未保存の場合、変更内容が破棄されますが、" & vbCrLf &
                                      "閉じてよろしいですか？",
                                      "確認",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning)
            If _result = vbYes Then
                Me.Visible = False

            End If
        End If
    End Sub

    Private Sub CmdAllRowsDel_Click(sender As Object, e As EventArgs) Handles CmdAllRowsDel.Click
        Dim result_tmp As DialogResult
        Dim _rows_count As Integer
        Dim _pitch_sum As Integer

        result_tmp = MessageBox.Show("削除してよろしいですか？",
                                     "削除確認",
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
        Dim _data_array(0) As Integer
        Dim result_tmp As DialogResult

        If FlgPitchExp_Load = 1 Then
            result_tmp = MessageBox.Show("上書きされますがよろしいですか？",
                                         "保存確認",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning)
        Else
            '新規作成の場合は強制的に保存する
            result_tmp = vbYes
        End If
        If result_tmp = vbYes Then
            _rows_count = DataGridView1.Rows.Count
            For i = 0 To _rows_count - 2
                If i = 0 Then
                    _data_array(i) = DataGridView1.Rows(i).Cells(1).Value
                Else
                    ReDim Preserve _data_array(i)
                    _data_array(i) = DataGridView1.Rows(i).Cells(1).Value
                End If
            Next

            SaveConst_PchExp(_data_array)
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
            MessageBox.Show("数値を入力して下さい。",
                            "入力 値エラー",
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
End Class