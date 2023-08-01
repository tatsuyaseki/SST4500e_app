<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSST4500_1_0_0J_main
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSST4500_1_0_0J_main))
        Me.TimSplash = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LblProductNameMenu = New System.Windows.Forms.Label()
        Me.CmdAdmin = New System.Windows.Forms.Button()
        Me.CmdMDlong = New System.Windows.Forms.Button()
        Me.CmdQuitSplash = New System.Windows.Forms.Button()
        Me.CmdTest = New System.Windows.Forms.Button()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.CmdCutSheetProfile = New System.Windows.Forms.Button()
        Me.CmdProfile = New System.Windows.Forms.Button()
        Me.CmdSinglesheet = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.シングルシートToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.カットシートToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.プロファイルToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.終了ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.設定ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.管理者ログインToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.設定ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.管理者モードToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MD長尺測定ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.試験調整ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ヘルプToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SST4500ヘルプToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SST4500についてToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TimSplash
        '
        Me.TimSplash.Interval = 10
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel3, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 539)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(784, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.ToolStripStatusLabel1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(80, 17)
        Me.ToolStripStatusLabel1.Text = "USB接続状態"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.ToolStripStatusLabel3.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(78, 17)
        Me.ToolStripStatusLabel3.Text = "SST接続状態"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.ToolStripStatusLabel2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(61, 17)
        Me.ToolStripStatusLabel2.Text = "通常モード"
        '
        'LblProductNameMenu
        '
        Me.LblProductNameMenu.AutoSize = True
        Me.LblProductNameMenu.Font = New System.Drawing.Font("MS UI Gothic", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblProductNameMenu.Location = New System.Drawing.Point(35, 35)
        Me.LblProductNameMenu.Name = "LblProductNameMenu"
        Me.LblProductNameMenu.Size = New System.Drawing.Size(197, 40)
        Me.LblProductNameMenu.TabIndex = 1
        Me.LblProductNameMenu.Text = "SST-4500"
        '
        'CmdAdmin
        '
        Me.CmdAdmin.Font = New System.Drawing.Font("MS UI Gothic", 20.0!)
        Me.CmdAdmin.Location = New System.Drawing.Point(22, 323)
        Me.CmdAdmin.Name = "CmdAdmin"
        Me.CmdAdmin.Size = New System.Drawing.Size(240, 130)
        Me.CmdAdmin.TabIndex = 6
        Me.CmdAdmin.Text = "管理者ログイン"
        Me.CmdAdmin.UseVisualStyleBackColor = True
        '
        'CmdMDlong
        '
        Me.CmdMDlong.Font = New System.Drawing.Font("MS UI Gothic", 20.0!)
        Me.CmdMDlong.Location = New System.Drawing.Point(270, 323)
        Me.CmdMDlong.Name = "CmdMDlong"
        Me.CmdMDlong.Size = New System.Drawing.Size(240, 130)
        Me.CmdMDlong.TabIndex = 7
        Me.CmdMDlong.Text = "MD長尺測定"
        Me.CmdMDlong.UseVisualStyleBackColor = True
        '
        'CmdQuitSplash
        '
        Me.CmdQuitSplash.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdQuitSplash.Location = New System.Drawing.Point(638, 489)
        Me.CmdQuitSplash.Name = "CmdQuitSplash"
        Me.CmdQuitSplash.Size = New System.Drawing.Size(120, 35)
        Me.CmdQuitSplash.TabIndex = 8
        Me.CmdQuitSplash.Text = "終 了"
        Me.CmdQuitSplash.UseVisualStyleBackColor = True
        '
        'CmdTest
        '
        Me.CmdTest.Font = New System.Drawing.Font("MS UI Gothic", 20.0!)
        Me.CmdTest.Location = New System.Drawing.Point(518, 323)
        Me.CmdTest.Name = "CmdTest"
        Me.CmdTest.Size = New System.Drawing.Size(240, 130)
        Me.CmdTest.TabIndex = 9
        Me.CmdTest.Text = "試験・調整"
        Me.CmdTest.UseVisualStyleBackColor = True
        '
        'PrintDocument1
        '
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'CmdCutSheetProfile
        '
        Me.CmdCutSheetProfile.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdCutSheetProfile.Location = New System.Drawing.Point(270, 130)
        Me.CmdCutSheetProfile.Name = "CmdCutSheetProfile"
        Me.CmdCutSheetProfile.Size = New System.Drawing.Size(240, 130)
        Me.CmdCutSheetProfile.TabIndex = 4
        Me.CmdCutSheetProfile.Text = "カットシート" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.CmdCutSheetProfile.UseVisualStyleBackColor = True
        '
        'CmdProfile
        '
        Me.CmdProfile.Enabled = False
        Me.CmdProfile.Font = New System.Drawing.Font("MS UI Gothic", 20.0!)
        Me.CmdProfile.Location = New System.Drawing.Point(518, 130)
        Me.CmdProfile.Name = "CmdProfile"
        Me.CmdProfile.Size = New System.Drawing.Size(240, 130)
        Me.CmdProfile.TabIndex = 5
        Me.CmdProfile.Text = "プロファイル" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.CmdProfile.UseVisualStyleBackColor = True
        '
        'CmdSinglesheet
        '
        Me.CmdSinglesheet.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdSinglesheet.Location = New System.Drawing.Point(22, 130)
        Me.CmdSinglesheet.Name = "CmdSinglesheet"
        Me.CmdSinglesheet.Size = New System.Drawing.Size(240, 130)
        Me.CmdSinglesheet.TabIndex = 3
        Me.CmdSinglesheet.Text = "シングルシート" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.CmdSinglesheet.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.設定ToolStripMenuItem, Me.管理者モードToolStripMenuItem, Me.ヘルプToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(784, 24)
        Me.MenuStrip1.TabIndex = 11
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.シングルシートToolStripMenuItem, Me.カットシートToolStripMenuItem, Me.プロファイルToolStripMenuItem, Me.終了ToolStripMenuItem})
        Me.ToolStripMenuItem1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(41, 20)
        Me.ToolStripMenuItem1.Text = "測定"
        '
        'シングルシートToolStripMenuItem
        '
        Me.シングルシートToolStripMenuItem.Name = "シングルシートToolStripMenuItem"
        Me.シングルシートToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.シングルシートToolStripMenuItem.Text = "シングルシート"
        '
        'カットシートToolStripMenuItem
        '
        Me.カットシートToolStripMenuItem.Name = "カットシートToolStripMenuItem"
        Me.カットシートToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.カットシートToolStripMenuItem.Text = "カットシート"
        '
        'プロファイルToolStripMenuItem
        '
        Me.プロファイルToolStripMenuItem.Name = "プロファイルToolStripMenuItem"
        Me.プロファイルToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.プロファイルToolStripMenuItem.Text = "プロファイル"
        '
        '終了ToolStripMenuItem
        '
        Me.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem"
        Me.終了ToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.終了ToolStripMenuItem.Text = "終了"
        '
        '設定ToolStripMenuItem
        '
        Me.設定ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.管理者ログインToolStripMenuItem, Me.設定ToolStripMenuItem1})
        Me.設定ToolStripMenuItem.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem"
        Me.設定ToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.設定ToolStripMenuItem.Text = "設定"
        '
        '管理者ログインToolStripMenuItem
        '
        Me.管理者ログインToolStripMenuItem.Name = "管理者ログインToolStripMenuItem"
        Me.管理者ログインToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.管理者ログインToolStripMenuItem.Text = "管理者ログイン"
        '
        '設定ToolStripMenuItem1
        '
        Me.設定ToolStripMenuItem1.Name = "設定ToolStripMenuItem1"
        Me.設定ToolStripMenuItem1.Size = New System.Drawing.Size(142, 22)
        Me.設定ToolStripMenuItem1.Text = "設定"
        '
        '管理者モードToolStripMenuItem
        '
        Me.管理者モードToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MD長尺測定ToolStripMenuItem1, Me.試験調整ToolStripMenuItem})
        Me.管理者モードToolStripMenuItem.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.管理者モードToolStripMenuItem.Name = "管理者モードToolStripMenuItem"
        Me.管理者モードToolStripMenuItem.Size = New System.Drawing.Size(81, 20)
        Me.管理者モードToolStripMenuItem.Text = "管理者モード"
        '
        'MD長尺測定ToolStripMenuItem1
        '
        Me.MD長尺測定ToolStripMenuItem1.Name = "MD長尺測定ToolStripMenuItem1"
        Me.MD長尺測定ToolStripMenuItem1.Size = New System.Drawing.Size(135, 22)
        Me.MD長尺測定ToolStripMenuItem1.Text = "MD長尺測定"
        '
        '試験調整ToolStripMenuItem
        '
        Me.試験調整ToolStripMenuItem.Name = "試験調整ToolStripMenuItem"
        Me.試験調整ToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.試験調整ToolStripMenuItem.Text = "試験・調整"
        '
        'ヘルプToolStripMenuItem
        '
        Me.ヘルプToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SST4500ヘルプToolStripMenuItem, Me.SST4500についてToolStripMenuItem})
        Me.ヘルプToolStripMenuItem.Name = "ヘルプToolStripMenuItem"
        Me.ヘルプToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ヘルプToolStripMenuItem.Text = "ヘルプ"
        '
        'SST4500ヘルプToolStripMenuItem
        '
        Me.SST4500ヘルプToolStripMenuItem.Enabled = False
        Me.SST4500ヘルプToolStripMenuItem.Name = "SST4500ヘルプToolStripMenuItem"
        Me.SST4500ヘルプToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SST4500ヘルプToolStripMenuItem.Text = "SST-4500ヘルプ"
        '
        'SST4500についてToolStripMenuItem
        '
        Me.SST4500についてToolStripMenuItem.Name = "SST4500についてToolStripMenuItem"
        Me.SST4500についてToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SST4500についてToolStripMenuItem.Text = "SST-4500について"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.SST4500_1_0_0J.My.Resources.Resources.nomura_logo1
        Me.PictureBox1.Location = New System.Drawing.Point(621, 24)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(160, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = False
        '
        'FrmSST4500_1_0_0J_main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.CmdCutSheetProfile)
        Me.Controls.Add(Me.CmdSinglesheet)
        Me.Controls.Add(Me.CmdProfile)
        Me.Controls.Add(Me.CmdTest)
        Me.Controls.Add(Me.CmdQuitSplash)
        Me.Controls.Add(Me.CmdMDlong)
        Me.Controls.Add(Me.CmdAdmin)
        Me.Controls.Add(Me.LblProductNameMenu)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FrmSST4500_1_0_0J_main"
        Me.Text = "SST-4500 Menu"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TimSplash As Timer
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents LblProductNameMenu As Label
    Friend WithEvents CmdAdmin As Button
    Friend WithEvents CmdMDlong As Button
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents CmdQuitSplash As Button
    Friend WithEvents CmdTest As Button
    Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents CmdCutSheetProfile As Button
    Friend WithEvents CmdProfile As Button
    Friend WithEvents CmdSinglesheet As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents シングルシートToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents カットシートToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents プロファイルToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 終了ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 設定ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 管理者ログインToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 設定ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents 管理者モードToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MD長尺測定ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents 試験調整ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ヘルプToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SST4500ヘルプToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SST4500についてToolStripMenuItem As ToolStripMenuItem
End Class
