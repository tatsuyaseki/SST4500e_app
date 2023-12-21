﻿Public Class FrmSST4500_1_1_0J_dbfchg
    Private Sub FrmSST4500_1_1_0J_dbfchg_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size


        If FlgDBF = 1 Then
            Rb_custum1.Checked = True
        Else
            Rb_default.Checked = True
        End If

    End Sub

    Private Sub Rb_default_CheckedChanged(sender As Object, e As EventArgs) Handles Rb_default.CheckedChanged
        If Rb_default.Checked = True Then
            FlgDBF = 0
        ElseIf Rb_custum1.Checked = True Then
            FlgDBF = 1
        End If
    End Sub

    Private Sub Rb_custum1_CheckedChanged(sender As Object, e As EventArgs) Handles Rb_custum1.CheckedChanged
        If Rb_default.Checked = True Then
            FlgDBF = 0
        ElseIf Rb_custum1.Checked = True Then
            FlgDBF = 1
        End If
    End Sub

    Private Sub CmdOK_Click(sender As Object, e As EventArgs) Handles CmdOK.Click
        My.Settings._flg_dbf = FlgDBF
        My.Settings.Save()
        If FlgDBF = 1 Then
            FrmSST4500_1_0_0J_main.ToolStripStatusLabel4.Text = "特殊1"
        Else
            FrmSST4500_1_0_0J_main.ToolStripStatusLabel4.Text = ""
        End If
        Me.Visible = False
        'Console.WriteLine(FlgDBF)
    End Sub

    Private Sub CmdCancel_Click(sender As Object, e As EventArgs) Handles CmdCancel.Click
        Me.Visible = False
        'Console.WriteLine(FlgDBF)
    End Sub
End Class