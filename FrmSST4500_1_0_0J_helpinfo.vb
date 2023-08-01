Public Class FrmSST4500_1_0_0J_helpinfo
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("mailto:nomurashoji@nomurashoji.com")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("http://www.nomurashoji.com")
    End Sub

    Private Sub FrmSST4500_1_0_0J_helpinfo_Load(sender As Object, e As EventArgs) Handles Me.Load
        MinimumSize = Size
        MaximumSize = Size

        Label4.Text = My.Application.Info.ProductName & " Ver:" & My.Application.Info.Version.ToString
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class