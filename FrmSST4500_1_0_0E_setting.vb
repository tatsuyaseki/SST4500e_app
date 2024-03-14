Public Class FrmSST4500_1_0_0E_setting

    Private Sub LblAngPkColor_Click(sender As Object, e As EventArgs) Handles LblAngPkColor.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = angpkgraph_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblAngPkColor.BackColor = dialog.Color
                angpkgraph_color = dialog.Color
                My.Settings._angpkgraph_color = angpkgraph_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblAngDpColor_Click(sender As Object, e As EventArgs) Handles LblAngDpColor.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = angdpgraph_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblAngDpColor.BackColor = dialog.Color
                angdpgraph_color = dialog.Color
                My.Settings._angdpgraph_color = angdpgraph_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub CmdSettingReset_Click(sender As Object, e As EventArgs) Handles CmdSettingReset.Click
        My.Settings.Reset()

        angpkgraph_color = My.Settings._angpkgraph_color
        angdpgraph_color = My.Settings._angdpgraph_color
        ratpkdpgraph_color = My.Settings._ratpkdpgraph_color
        ratmdcdgraph_color = My.Settings._ratmdcdgraph_color
        velopkgraph_color = My.Settings._velopkgraph_color
        velodpgraph_color = My.Settings._velodpgraph_color
        velomdgraph_color = My.Settings._velomdgraph_color
        velocdgraph_color = My.Settings._velocdgraph_color
        tsimdgraph_color = My.Settings._velomdgraph_color
        tsicdgraph_color = My.Settings._velocdgraph_color

        angpkgraph3_color = My.Settings._angpkgraph_color3
        angdpgraph3_color = My.Settings._angdpgraph_color3
        ratpkdpgraph3_color = My.Settings._ratpkdpgraph_color3
        ratmdcdgraph3_color = My.Settings._ratmdcdgraph_color3
        velopkgraph3_color = My.Settings._velopkgraph_color3
        velodpgraph3_color = My.Settings._velodpgraph_color3
        velomdgraph3_color = My.Settings._velomdgraph_color3
        velocdgraph3_color = My.Settings._velocdgraph_color3
        tsimdgraph3_color = My.Settings._tsimdgraph_color3
        tsicdgraph3_color = My.Settings._tsicdgraph_color3

        color_init()
    End Sub

    Private Sub color_init()
        LblAngPkColor.BackColor = angpkgraph_color
        LblAngDpColor.BackColor = angdpgraph_color
        LblRatPkDpColor.BackColor = ratpkdpgraph_color
        LblRatMDCDColor.BackColor = ratmdcdgraph_color
        LblVeloPkColor.BackColor = velopkgraph_color
        LblVeloDpColor.BackColor = velodpgraph_color
        LblVeloMDColor.BackColor = velomdgraph_color
        LblVeloCDColor.BackColor = velocdgraph_color
        LblTSIMDColor.BackColor = tsimdgraph_color
        LblTSICDColor.BackColor = tsicdgraph_color

        LblAngPkColorLG.BackColor = angpkgraph3_color
        LblAngDpColorLG.BackColor = angdpgraph3_color
        LblRatPkDpColorLG.BackColor = ratpkdpgraph3_color
        LblRatMDCDColorLG.BackColor = ratmdcdgraph3_color
        LblVeloPkColorLG.BackColor = velopkgraph3_color
        LblVeloDpColorLG.BackColor = velodpgraph3_color
        LblVeloMDColorLG.BackColor = velomdgraph3_color
        LblVeloCDColorLG.BackColor = velocdgraph3_color
        LblTSIMDColorLG.BackColor = tsimdgraph3_color
        LblTSICDColorLG.BackColor = tsicdgraph3_color

        With FrmSST4500_1_0_0E_Profile
            .PictureBox1.Refresh()
            .PictureBox2.Refresh()
            .PictureBox3.Refresh()
            .PictureBox4.Refresh()
        End With
    End Sub

    Private Sub FrmSST4500_1_0_0E_setting_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            CmdMarginApply.Text = "適用"

            color_init()


            TxtSingleSheetFolder.Text = SG_ResultSave_path
            TxtProfileFolder.Text = PF_ResultSave_path
            TxtCutSheetFolder.Text = CT_ResultSave_path
            TxtMDLongFolder.Text = LG_ResultSave_path

            NupPrnMarginTop.Value = Prn_top_margin
            NupPrnMarginBottom.Value = Prn_btm_margin
            NupPrnMarginLeft.Value = Prn_left_margin
            NupPrnMarginRight.Value = Prn_right_margin

            If My.Settings._printpreview = True Then
                CbxPrintPreview.Checked = True
            Else
                CbxPrintPreview.Checked = False
            End If
        End If
    End Sub

    Private Sub LblRatPkDpColor_Click(sender As Object, e As EventArgs) Handles LblRatPkDpColor.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = ratpkdpgraph_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblRatPkDpColor.BackColor = dialog.Color
                ratpkdpgraph_color = dialog.Color
                My.Settings._ratpkdpgraph_color = ratpkdpgraph_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblRatMDCDColor_Click(sender As Object, e As EventArgs) Handles LblRatMDCDColor.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = ratmdcdgraph_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblRatMDCDColor.BackColor = dialog.Color
                ratmdcdgraph_color = dialog.Color
                My.Settings._ratmdcdgraph_color = ratmdcdgraph_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblVeloPkColor_Click(sender As Object, e As EventArgs) Handles LblVeloPkColor.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = velopkgraph_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblVeloPkColor.BackColor = dialog.Color
                velopkgraph_color = dialog.Color
                My.Settings._velopkgraph_color = velopkgraph_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblVeloDpColor_Click(sender As Object, e As EventArgs) Handles LblVeloDpColor.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = velodpgraph_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblVeloDpColor.BackColor = dialog.Color
                velodpgraph_color = dialog.Color
                My.Settings._velodpgraph_color = velodpgraph_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblVeloMDColor_Click(sender As Object, e As EventArgs) Handles LblVeloMDColor.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = velomdgraph_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblVeloMDColor.BackColor = dialog.Color
                velomdgraph_color = dialog.Color
                My.Settings._velomdgraph_color = velomdgraph_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblVeloCDColor_Click(sender As Object, e As EventArgs) Handles LblVeloCDColor.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = velocdgraph_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblVeloCDColor.BackColor = dialog.Color
                velocdgraph_color = dialog.Color
                My.Settings._velocdgraph_color = velocdgraph_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblTSIMDColor_Click(sender As Object, e As EventArgs) Handles LblTSIMDColor.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = tsimdgraph_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblTSIMDColor.BackColor = dialog.Color
                tsimdgraph_color = dialog.Color
                My.Settings._tsimdgraph_color = tsimdgraph_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblTSICDColor_Click(sender As Object, e As EventArgs) Handles LblTSICDColor.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = tsicdgraph_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblTSICDColor.BackColor = dialog.Color
                tsicdgraph_color = dialog.Color
                My.Settings._tsicdgraph_color = tsicdgraph_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblAngPkColorLG_Click(sender As Object, e As EventArgs) Handles LblAngPkColorLG.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = angpkgraph3_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblAngPkColorLG.BackColor = dialog.Color
                angpkgraph3_color = dialog.Color
                My.Settings._angpkgraph_color3 = angpkgraph3_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblAngDpColorLG_Click(sender As Object, e As EventArgs) Handles LblAngDpColorLG.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = angdpgraph3_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblAngDpColorLG.BackColor = dialog.Color
                angdpgraph3_color = dialog.Color
                My.Settings._angdpgraph_color3 = angdpgraph3_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblRatPkDpColorLG_Click(sender As Object, e As EventArgs) Handles LblRatPkDpColorLG.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = ratpkdpgraph3_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblRatPkDpColorLG.BackColor = dialog.Color
                ratpkdpgraph3_color = dialog.Color
                My.Settings._ratpkdpgraph_color3 = ratpkdpgraph3_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblRatMDCDColorLG_Click(sender As Object, e As EventArgs) Handles LblRatMDCDColorLG.Click
        Using dialog As New ColorDialog
            With dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = ratmdcdgraph3_color
            End With

            If dialog.ShowDialog() = DialogResult.OK Then
                LblRatMDCDColorLG.BackColor = dialog.Color
                ratmdcdgraph3_color = dialog.Color
                My.Settings._ratpkdpgraph_color3 = ratpkdpgraph3_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub CmdSGPath_Click(sender As Object, e As EventArgs) Handles CmdSGPath.Click
        Using dialog As New FolderBrowserDialog
            With dialog
                .Description = "シングルシート測定結果保存フォルダ設定"
                .SelectedPath = SG_ResultSave_path

            End With

            If dialog.ShowDialog = DialogResult.OK Then
                SG_ResultSave_path = dialog.SelectedPath
                TxtSingleSheetFolder.Text = SG_ResultSave_path
                My.Settings._sgresultsave_path = SG_ResultSave_path
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub CmdPFPath_Click(sender As Object, e As EventArgs) Handles CmdPFPath.Click
        Using dialog As New FolderBrowserDialog
            With dialog
                .Description = "プロファイル測定結果保存フォルダ設定"
                .SelectedPath = PF_ResultSave_path

            End With

            If dialog.ShowDialog = DialogResult.OK Then
                PF_ResultSave_path = dialog.SelectedPath
                TxtProfileFolder.Text = PF_ResultSave_path
                My.Settings._pfresultsave_path = PF_ResultSave_path
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub CmdCTPath_Click(sender As Object, e As EventArgs) Handles CmdCTPath.Click
        Using dialog As New FolderBrowserDialog
            With dialog
                .Description = "カットシート測定結果保存フォルダ設定"
                .SelectedPath = CT_ResultSave_path

            End With

            If dialog.ShowDialog = DialogResult.OK Then
                CT_ResultSave_path = dialog.SelectedPath
                TxtCutSheetFolder.Text = CT_ResultSave_path
                My.Settings._ctresultsave_path = CT_ResultSave_path
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub CmdLGPath_Click(sender As Object, e As EventArgs) Handles CmdLGPath.Click
        Using dialog As New FolderBrowserDialog
            With dialog
                .Description = "MD長尺測定結果保存フォルダ設定"
                .SelectedPath = LG_ResultSave_path

            End With

            If dialog.ShowDialog = DialogResult.OK Then
                LG_ResultSave_path = dialog.SelectedPath
                TxtMDLongFolder.Text = LG_ResultSave_path
                My.Settings._lgresultsave_path = LG_ResultSave_path
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub CbxPrintPreview_CheckedChanged(sender As Object, e As EventArgs) Handles CbxPrintPreview.CheckedChanged
        If CbxPrintPreview.Checked = True Then
            My.Settings._printpreview = True
        Else
            My.Settings._printpreview = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub NupPrnMarginTop_ValueChanged(sender As Object, e As EventArgs) Handles NupPrnMarginTop.ValueChanged
        Prn_top_margin = NupPrnMarginTop.Value
        My.Settings._printmargin_top = Prn_top_margin

        If NupPrnMarginTop.Value <> Prn_top_margin Then
            CmdMarginApply.Text = "適用*"
        End If
    End Sub

    Private Sub NupPrnMarginBottom_ValueChanged(sender As Object, e As EventArgs) Handles NupPrnMarginBottom.ValueChanged
        Prn_btm_margin = NupPrnMarginBottom.Value
        My.Settings._printmargin_bottom = Prn_btm_margin

        If NupPrnMarginBottom.Value <> Prn_btm_margin Then
            CmdMarginApply.Text = "適用*"
        End If
    End Sub

    Private Sub NupPrnMarginLeft_ValueChanged(sender As Object, e As EventArgs) Handles NupPrnMarginLeft.ValueChanged
        Prn_left_margin = NupPrnMarginLeft.Value
        My.Settings._printmargin_left = Prn_left_margin

        If NupPrnMarginLeft.Value <> Prn_left_margin Then
            CmdMarginApply.Text = "適用*"
        End If
    End Sub

    Private Sub NupPrnMarginRight_ValueChanged(sender As Object, e As EventArgs) Handles NupPrnMarginRight.ValueChanged
        Prn_right_margin = NupPrnMarginRight.Value
        My.Settings._printmargin_right = Prn_right_margin

        If NupPrnMarginRight.Value <> Prn_right_margin Then
            CmdMarginApply.Text = "適用*"
        End If
    End Sub

    Private Sub CmdMarginApply_Click(sender As Object, e As EventArgs) Handles CmdMarginApply.Click
        My.Settings.Save()

        CmdMarginApply.Text = "適用"
    End Sub

    Private Sub CmdSettingQuit_Click(sender As Object, e As EventArgs) Handles CmdSettingQuit.Click
        Me.Visible = False
    End Sub

    Private Sub FrmSST4500_1_0_0E_setting_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FrmSST4500_1_0_0E_colorsetting.Visible = True
    End Sub
End Class