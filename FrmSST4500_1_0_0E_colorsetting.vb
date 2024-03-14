Public Class FrmSST4500_1_0_0E_colorsetting
    Private Sub CmdColorSettingReset_Click(sender As Object, e As EventArgs) Handles CmdColorSettingReset.Click
        My.Settings.Reset()

        mainform_color_setting_load()
        measform_color_setting_load()
        prfform_color_setting_load()

        mainform_color_init()
        colorsetting_label_init(Main_Enum)
        'mainform_borderstyle_init()

        measform_color_init()
        colorsetting_label_init(Meas_Enum)
        'measform_borderstyle_init()

        prfform_color_init()
        colorsetting_label_init(Profile_Enum)
        'prfform_borderstyle_init()

        Me.CbPrintBc.Checked = My.Settings._printbc
        FlgPrnBc_enable = Me.CbPrintBc.Checked

        FrmSST4500_1_0_0E_main.Refresh()
    End Sub

    Private Sub LblFrmMainFormBC_Click(sender As Object, e As EventArgs) Handles LblFrmMainFormBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MainForm_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMainFormBC.BackColor = Dialog.Color
                frm_MainForm_bc = Dialog.Color
                FrmSST4500_1_0_0E_main.BackColor = frm_MainForm_bc
                My.Settings._frm_MainForm_bc = frm_MainForm_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMainMenuBC_Click(sender As Object, e As EventArgs) Handles LblFrmMainMenuBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MainMenu_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMainMenuBC.BackColor = Dialog.Color
                frm_MainMenu_bc = Dialog.Color
                FrmSST4500_1_0_0E_main.MenuStrip1.BackColor = frm_MainMenu_bc
                My.Settings._frm_MainMenu_bc = frm_MainMenu_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMainStatusBC_Click(sender As Object, e As EventArgs) Handles LblFrmMainStatusBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MainStatus_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMainStatusBC.BackColor = Dialog.Color
                frm_MainStatus_bc = Dialog.Color
                FrmSST4500_1_0_0E_main.StatusStrip1.BackColor = frm_MainStatus_bc
                My.Settings._frm_MainStatus_bc = frm_MainStatus_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub FrmSST4500_1_0_0E_colorsetting_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged

    End Sub

    Private Sub FrmSST4500_1_0_0E_colorsetting_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.MinimumSize = Me.Size

        colorsetting_label_init(Main_Enum)
        colorsetting_label_init(Meas_Enum)
        colorsetting_label_init(Profile_Enum)

        Me.CbPrintBc.Checked = My.Settings._printbc
        FlgPrnBc_enable = Me.CbPrintBc.Checked
    End Sub

    Private Sub CbxFrmMainStatusBoderStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbxFrmMainStatusBoderStyle.SelectedIndexChanged
        Dim idx As Integer = Me.CbxFrmMainStatusBoderStyle.SelectedIndex
        Select Case idx
            Case 0
                frm_MainStatusBorder_stl = Border3DStyle.Adjust
            Case 1
                frm_MainStatusBorder_stl = Border3DStyle.Bump
            Case 2
                frm_MainStatusBorder_stl = Border3DStyle.Etched
            Case 3
                frm_MainStatusBorder_stl = Border3DStyle.Flat
            Case 4
                frm_MainStatusBorder_stl = Border3DStyle.Raised
            Case 5
                frm_MainStatusBorder_stl = Border3DStyle.RaisedInner
            Case 6
                frm_MainStatusBorder_stl = Border3DStyle.RaisedOuter
            Case 7
                frm_MainStatusBorder_stl = Border3DStyle.Sunken
            Case 8
                frm_MainStatusBorder_stl = Border3DStyle.SunkenInner
            Case 9
                frm_MainStatusBorder_stl = Border3DStyle.SunkenOuter
        End Select

        With FrmSST4500_1_0_0E_main
            .ToolStripStatusLabel1.BorderStyle = frm_MainStatusBorder_stl
            .ToolStripStatusLabel2.BorderStyle = frm_MainStatusBorder_stl
            .ToolStripStatusLabel3.BorderStyle = frm_MainStatusBorder_stl
        End With

        My.Settings._frm_MainStatusBorder_stl = frm_MainStatusBorder_stl
        My.Settings.Save()

    End Sub

    Private Sub LblFrmMainButtonBC_Click(sender As Object, e As EventArgs) Handles LblFrmMainButtonBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MainButton_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMainButtonBC.BackColor = Dialog.Color
                frm_MainButton_bc = Dialog.Color

                set_maincmdbc()

                My.Settings._frm_MainButton_bc = frm_MainButton_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMainButtonFC_Click(sender As Object, e As EventArgs) Handles LblFrmMainButtonFC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MainButton_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMainButtonFC.BackColor = Dialog.Color
                frm_MainButton_fc = Dialog.Color

                set_maincmdfc()

                My.Settings._frm_MainButton_fc = frm_MainButton_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMainMenuFC_Click(sender As Object, e As EventArgs) Handles LblFrmMainMenuFC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MainMenu_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMainMenuFC.BackColor = Dialog.Color
                frm_MainMenu_fc = Dialog.Color

                set_mainmenufc()

                My.Settings._frm_MainMenu_fc = frm_MainMenu_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMainFormFC_Click(sender As Object, e As EventArgs) Handles LblFrmMainFormFC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MainForm_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMainFormFC.BackColor = Dialog.Color
                frm_MainForm_fc = Dialog.Color
                With FrmSST4500_1_0_0E_main
                    .LblProductNameMenu.ForeColor = frm_MainForm_fc

                    .Refresh()
                End With
                My.Settings._frm_MainForm_fc = frm_MainForm_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMainStatusFC_Click(sender As Object, e As EventArgs) Handles LblFrmMainStatusFC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MainStatus_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMainStatusFC.BackColor = Dialog.Color
                frm_MainStatus_fc = Dialog.Color
                With FrmSST4500_1_0_0E_main
                    .ToolStripStatusLabel1.ForeColor = frm_MainStatus_fc
                    .ToolStripStatusLabel2.ForeColor = frm_MainStatus_fc
                    .ToolStripStatusLabel3.ForeColor = frm_MainStatus_fc
                End With
                My.Settings._frm_MainStatus_fc = frm_MainStatus_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMainLineColor_Click(sender As Object, e As EventArgs) Handles LblFrmMainLineColor.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MainLine_color
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMainLineColor.BackColor = Dialog.Color
                frm_MainLine_color = Dialog.Color
                With FrmSST4500_1_0_0E_main
                    .Refresh()
                End With
                My.Settings._frm_MainLine_color = frm_MainLine_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasFormBC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasFormBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasForm_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasFormBC.BackColor = Dialog.Color
                frm_MeasForm_bc = Dialog.Color

                set_measformbc()

                My.Settings._frm_MeasForm_bc = frm_MeasForm_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasMenuBC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasMenuBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasMenu_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasMenuBC.BackColor = Dialog.Color
                frm_MeasMenu_bc = Dialog.Color
                With FrmSST4500_1_0_0E_meas
                    .MenuStrip1.BackColor = frm_MeasMenu_bc
                End With
                My.Settings._frm_MeasMenu_bc = frm_MeasMenu_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasStatusBC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasStatusBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasStatus_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasStatusBC.BackColor = Dialog.Color
                frm_MeasStatus_bc = Dialog.Color
                With FrmSST4500_1_0_0E_meas
                    .StatusStrip1.BackColor = frm_MeasStatus_bc
                End With
                My.Settings._frm_MeasStatus_bc = frm_MeasStatus_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasGraphBC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasGraphBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasGraph_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasGraphBC.BackColor = Dialog.Color
                frm_MeasGraph_bc = Dialog.Color
                With FrmSST4500_1_0_0E_meas
                    .PictureBox1.BackColor = frm_MeasGraph_bc
                End With
                My.Settings._frm_MeasGraph_bc = frm_MeasGraph_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub CbxFrmMeasStatusBoderStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbxFrmMeasStatusBoderStyle.SelectedIndexChanged
        Dim idx As Integer = Me.CbxFrmMeasStatusBoderStyle.SelectedIndex
        Select Case idx
            Case 0
                frm_MeasStatusBorder_stl = Border3DStyle.Adjust
            Case 1
                frm_MeasStatusBorder_stl = Border3DStyle.Bump
            Case 2
                frm_MeasStatusBorder_stl = Border3DStyle.Etched
            Case 3
                frm_MeasStatusBorder_stl = Border3DStyle.Flat
            Case 4
                frm_MeasStatusBorder_stl = Border3DStyle.Raised
            Case 5
                frm_MeasStatusBorder_stl = Border3DStyle.RaisedInner
            Case 6
                frm_MeasStatusBorder_stl = Border3DStyle.RaisedOuter
            Case 7
                frm_MeasStatusBorder_stl = Border3DStyle.Sunken
            Case 8
                frm_MeasStatusBorder_stl = Border3DStyle.SunkenInner
            Case 9
                frm_MeasStatusBorder_stl = Border3DStyle.SunkenOuter
        End Select

        With FrmSST4500_1_0_0E_meas
            .ToolStripStatusLabel1.BorderStyle = frm_MeasStatusBorder_stl
            .ToolStripStatusLabel2.BorderStyle = frm_MeasStatusBorder_stl
            .ToolStripStatusLabel3.BorderStyle = frm_MeasStatusBorder_stl
        End With

        My.Settings._frm_MeasStatusBorder_stl = frm_MeasStatusBorder_stl
        My.Settings.Save()
    End Sub

    Private Sub LblFrmMeasFormFC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasFormFC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasForm_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasFormFC.BackColor = Dialog.Color
                frm_MeasForm_fc = Dialog.Color

                set_measformfc()

                My.Settings._frm_MeasForm_fc = frm_MeasForm_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasOldDataColor_Click(sender As Object, e As EventArgs) Handles LblFrmMeasOldDataColor.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasOldData_color
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasOldDataColor.BackColor = Dialog.Color
                frm_MeasOldData_color = Dialog.Color

                set_measolddatacolor()

                FrmSST4500_1_0_0E_meas.PictureBox1.Refresh()
                My.Settings._frm_MeasOldData_color = frm_MeasOldData_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasMenuFC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasMenuFC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasMenu_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasMenuFC.BackColor = Dialog.Color
                frm_MeasMenu_fc = Dialog.Color

                set_measmenufc()

                My.Settings._frm_MeasMenu_fc = frm_MeasMenu_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasStatusFC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasStatusFC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasStatus_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasStatusFC.BackColor = Dialog.Color
                frm_MeasStatus_fc = Dialog.Color

                With FrmSST4500_1_0_0E_meas
                    .ToolStripStatusLabel1.ForeColor = frm_MainStatus_fc
                    .ToolStripStatusLabel2.ForeColor = frm_MainStatus_fc
                    .ToolStripStatusLabel3.ForeColor = frm_MainStatus_fc
                End With

                My.Settings._frm_MeasStatus_fc = frm_MeasStatus_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasButtonBC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasButtonBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasButton_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasButtonBC.BackColor = Dialog.Color
                frm_MeasButton_bc = Dialog.Color

                set_meascmdbc()

                My.Settings._frm_MeasButton_bc = frm_MeasButton_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasButtonFC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasButtonFC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasButton_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasButtonFC.BackColor = Dialog.Color
                frm_MeasButton_fc = Dialog.Color

                set_meascmdfc()

                My.Settings._frm_MeasButton_fc = frm_MeasButton_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasuringButtonBC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasuringButtonBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasuringButton_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasuringButtonBC.BackColor = Dialog.Color
                frm_MeasuringButton_bc = Dialog.Color

                My.Settings._frm_MeasuringButton_bc = frm_MeasuringButton_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasTextBoxBC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasTextBoxBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasTextBox_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasTextBoxBC.BackColor = Dialog.Color
                frm_MeasTextBox_bc = Dialog.Color

                set_meastextboxbc()

                My.Settings._frm_MeasTextBox_bc = frm_MeasTextBox_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasCurDataColor_Click(sender As Object, e As EventArgs) Handles LblFrmMeasCurDataColor.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasCurData_color
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasCurDataColor.BackColor = Dialog.Color
                frm_MeasCurData_color = Dialog.Color

                set_meascurdatacolor()

                FrmSST4500_1_0_0E_meas.PictureBox1.Refresh()

                My.Settings._frm_MeasCurData_color = frm_MeasCurData_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasGraphWakuColor_Click(sender As Object, e As EventArgs) Handles LblFrmMeasGraphWakuColor.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasGraphWaku_color
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasGraphWakuColor.BackColor = Dialog.Color
                frm_MeasGraphWaku_color = Dialog.Color

                FrmSST4500_1_0_0E_meas.PictureBox1.Refresh()

                My.Settings._frm_MeasGraphWaku_color = frm_MeasGraphWaku_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfFormBC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfFormBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfForm_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfFormBC.BackColor = Dialog.Color
                frm_PrfForm_bc = Dialog.Color

                set_prfformbc()

                My.Settings._frm_PrfForm_bc = frm_PrfForm_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfFormFC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfFormFC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfForm_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfFormFC.BackColor = Dialog.Color
                frm_PrfForm_fc = Dialog.Color

                set_prfformfc()

                My.Settings._frm_PrfForm_fc = frm_PrfForm_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfMenuBC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfMenuBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfMenu_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfMenuBC.BackColor = Dialog.Color
                frm_PrfMenu_bc = Dialog.Color
                With FrmSST4500_1_0_0E_Profile
                    .MenuStrip1.BackColor = frm_PrfMenu_bc
                End With
                My.Settings._frm_PrfMenu_bc = frm_PrfMenu_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfMenuFC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfMenuFC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfMenu_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfMenuFC.BackColor = Dialog.Color
                frm_PrfMenu_fc = Dialog.Color

                set_prfmenufc()

                My.Settings._frm_PrfMenu_fc = frm_PrfMenu_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfStatusBC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfStatusBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfStatus_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfStatusBC.BackColor = Dialog.Color
                frm_PrfStatus_bc = Dialog.Color
                With FrmSST4500_1_0_0E_Profile
                    .StatusStrip1.BackColor = frm_PrfStatus_bc
                End With
                My.Settings._frm_PrfStatus_bc = frm_PrfStatus_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfStatusFC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfStatusFC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfStatus_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfStatusFC.BackColor = Dialog.Color
                frm_PrfStatus_fc = Dialog.Color

                With FrmSST4500_1_0_0E_Profile
                    .ToolStripStatusLabel1.ForeColor = frm_PrfStatus_fc
                    .ToolStripStatusLabel2.ForeColor = frm_PrfStatus_fc
                    .ToolStripStatusLabel3.ForeColor = frm_PrfStatus_fc
                End With

                My.Settings._frm_PrfStatus_fc = frm_PrfStatus_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub CbxFrmPrfStatusBoderStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CbxFrmPrfStatusBoderStyle.SelectedIndexChanged
        Dim idx As Integer = Me.CbxFrmPrfStatusBoderStyle.SelectedIndex
        Select Case idx
            Case 0
                frm_PrfStatusBorder_stl = Border3DStyle.Adjust
            Case 1
                frm_PrfStatusBorder_stl = Border3DStyle.Bump
            Case 2
                frm_PrfStatusBorder_stl = Border3DStyle.Etched
            Case 3
                frm_PrfStatusBorder_stl = Border3DStyle.Flat
            Case 4
                frm_PrfStatusBorder_stl = Border3DStyle.Raised
            Case 5
                frm_PrfStatusBorder_stl = Border3DStyle.RaisedInner
            Case 6
                frm_PrfStatusBorder_stl = Border3DStyle.RaisedOuter
            Case 7
                frm_PrfStatusBorder_stl = Border3DStyle.Sunken
            Case 8
                frm_PrfStatusBorder_stl = Border3DStyle.SunkenInner
            Case 9
                frm_PrfStatusBorder_stl = Border3DStyle.SunkenOuter
        End Select

        With FrmSST4500_1_0_0E_Profile
            .ToolStripStatusLabel1.BorderStyle = frm_PrfStatusBorder_stl
            .ToolStripStatusLabel2.BorderStyle = frm_PrfStatusBorder_stl
            .ToolStripStatusLabel3.BorderStyle = frm_PrfStatusBorder_stl
        End With

        My.Settings._frm_PrfStatusBorder_stl = frm_PrfStatusBorder_stl
        My.Settings.Save()
    End Sub

    Private Sub LblFrmPrfButtonBC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfButtonBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfButton_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfButtonBC.BackColor = Dialog.Color
                frm_PrfButton_bc = Dialog.Color

                set_prfcmdbc()

                My.Settings._frm_PrfButton_bc = frm_PrfButton_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfButtonFC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfButtonFC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfButton_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfButtonFC.BackColor = Dialog.Color
                frm_PrfButton_fc = Dialog.Color

                set_prfcmdfc()

                My.Settings._frm_PrfButton_fc = frm_PrfButton_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfMeasuringButtonBC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfMeasuringButtonBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfMeasuringButton_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfMeasuringButtonBC.BackColor = Dialog.Color
                frm_PrfMeasuringButton_bc = Dialog.Color

                My.Settings._frm_PrfMeasuringButton_bc = frm_PrfMeasuringButton_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfTextBoxBC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfTextBoxBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfTextBox_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfTextBoxBC.BackColor = Dialog.Color
                frm_PrfTextBox_bc = Dialog.Color

                set_prftextboxbc()

                My.Settings._frm_PrfTextBox_bc = frm_PrfTextBox_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfGraphBC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfGraphBC.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfGraph_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfGraphBC.BackColor = Dialog.Color
                frm_PrfGraph_bc = Dialog.Color

                set_prfgraphbc()

                My.Settings._frm_PrfGraph_bc = frm_PrfGraph_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfGraphWakuColor_Click(sender As Object, e As EventArgs) Handles LblFrmPrfGraphWakuColor.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfGraphWaku_color
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfGraphWakuColor.BackColor = Dialog.Color
                frm_PrfGraphWaku_color = Dialog.Color

                With FrmSST4500_1_0_0E_Profile
                    .PictureBox1.Refresh()
                    .PictureBox2.Refresh()
                    .PictureBox3.Refresh()
                    .PictureBox4.Refresh()
                End With

                My.Settings._frm_PrfGraphWaku_color = frm_PrfGraphWaku_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfCurDataColor_Click(sender As Object, e As EventArgs) Handles LblFrmPrfCurDataColor.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfCurData_color
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfCurDataColor.BackColor = Dialog.Color
                frm_PrfCurData_color = Dialog.Color

                set_prfcurdatacolor()



                My.Settings._frm_PrfCurData_color = frm_PrfCurData_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfOldDataColor_Click(sender As Object, e As EventArgs) Handles LblFrmPrfOldDataColor.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfOldData_color
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfOldDataColor.BackColor = Dialog.Color
                frm_PrfOldData_color = Dialog.Color

                set_prfolddatacolor()

                My.Settings._frm_PrfOldData_color = frm_PrfOldData_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfAvgDataColor_Click(sender As Object, e As EventArgs) Handles LblFrmPrfAvgDataColor.Click
        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfAvgData_color
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfAvgDataColor.BackColor = Dialog.Color
                frm_PrfAvgData_color = Dialog.Color

                set_prfavgdatacolor()

                My.Settings._frm_PrfAvgData_color = frm_PrfAvgData_color
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub CmdColorSettingQuit_Click(sender As Object, e As EventArgs) Handles CmdColorSettingQuit.Click
        Me.Visible = False
    End Sub

    Private Sub LblMeasMeasButtonBC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasMeasButtonBC.Click
        Dim a As Integer

        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasMeasButton_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasMeasButtonBC.BackColor = Dialog.Color
                frm_MeasMeasButton_bc = Dialog.Color

                a = FlgProfile

                FlgProfile = 0
                CmdMeasButton_set(_rdy)

                FlgProfile = a

                My.Settings._frm_MeasMeasButton_bc = frm_MeasMeasButton_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmMeasMeasButtonFC_Click(sender As Object, e As EventArgs) Handles LblFrmMeasMeasButtonFC.Click
        Dim a As Integer

        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_MeasMeasButton_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmMeasMeasButtonFC.BackColor = Dialog.Color
                frm_MeasMeasButton_fc = Dialog.Color

                a = FlgProfile

                FlgProfile = 0
                CmdMeasButton_set(_rdy)

                FlgProfile = a

                My.Settings._frm_MeasMeasButton_fc = frm_MeasMeasButton_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfMeasButtonBC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfMeasButtonBC.Click
        Dim a As Integer

        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfMeasButton_bc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfMeasButtonBC.BackColor = Dialog.Color
                frm_PrfMeasButton_bc = Dialog.Color

                a = FlgProfile

                FlgProfile = 1
                CmdMeasButton_set(_rdy)

                FlgProfile = a

                My.Settings._frm_PrfMeasButton_bc = frm_PrfMeasButton_bc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub LblFrmPrfMeasButtonFC_Click(sender As Object, e As EventArgs) Handles LblFrmPrfMeasButtonFC.Click
        Dim a As Integer

        Using Dialog As New ColorDialog
            With Dialog
                .AllowFullOpen = True
                .FullOpen = True
                .Color = frm_PrfMeasButton_fc
            End With

            If Dialog.ShowDialog = DialogResult.OK Then
                LblFrmPrfMeasButtonFC.BackColor = Dialog.Color
                frm_PrfMeasButton_fc = Dialog.Color

                a = FlgProfile

                FlgProfile = 1
                CmdMeasButton_set(_rdy)

                FlgProfile = a

                My.Settings._frm_PrfMeasButton_fc = frm_PrfMeasButton_fc
                My.Settings.Save()
            End If
        End Using
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CbPrintBc.CheckedChanged
        FlgPrnBc_enable = Me.CbPrintBc.Checked
        My.Settings._printbc = FlgPrnBc_enable
        My.Settings.Save()
    End Sub
End Class