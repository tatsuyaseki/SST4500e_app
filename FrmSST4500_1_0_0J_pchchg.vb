Public Class FrmSST4500_1_0_0J_pchchg
    Private Sub FrmSST4500_1_0_0J_pchchg_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size

        If FlgPchExp_Visible = 1 Then
            Rb_Enable.Checked = True
        Else
            Rb_Disable.Checked = True
        End If

    End Sub

    Private Sub CmdOK_Click(sender As Object, e As EventArgs) Handles CmdOK.Click
        If Rb_Disable.Checked = True Then
            FlgPchExp_Visible = 0
        ElseIf Rb_Enable.Checked = True Then
            FlgPchExp_Visible = 1
        End If

        My.Settings._flg_pchexp_visible = FlgPchExp_Visible
        My.Settings.Save()

        With FrmSST4500_1_0_0J_Profile
            If FlgPchExp_Visible = 1 Then
                .ChkPitchExp_Ena.Visible = True
                .LblPitch.Visible = True
            Else
                .ChkPitchExp_Ena.Visible = False
                .LblPitch.Visible = False
                FlgPitchExp = 0     '無効時は強制的にピッチ拡張OFF
            End If
        End With
        Me.Visible = False
    End Sub

    Private Sub CmdCancel_Click(sender As Object, e As EventArgs) Handles CmdCancel.Click
        Me.Visible = False
    End Sub
End Class