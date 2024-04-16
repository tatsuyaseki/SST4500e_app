<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSST4500_1_0_0E_main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSST4500_1_0_0E_main))
        Me.TimSplash = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
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
        Me.SingleSheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CutSheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdmLoginToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdmModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MDLongToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SST4500HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SST4500InfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel3, Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel4})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 540)
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
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(130, 17)
        Me.ToolStripStatusLabel1.Text = "USB Connection Status"
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.ToolStripStatusLabel3.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(103, 17)
        Me.ToolStripStatusLabel3.Text = "Connection Status"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.ToolStripStatusLabel2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(84, 17)
        Me.ToolStripStatusLabel2.Text = "Operator Mode"
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(52, 17)
        Me.ToolStripStatusLabel4.Text = "Special 1"
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
        Me.CmdAdmin.Text = "Admin. Login"
        Me.CmdAdmin.UseVisualStyleBackColor = True
        '
        'CmdMDlong
        '
        Me.CmdMDlong.Font = New System.Drawing.Font("MS UI Gothic", 20.0!)
        Me.CmdMDlong.Location = New System.Drawing.Point(270, 323)
        Me.CmdMDlong.Name = "CmdMDlong"
        Me.CmdMDlong.Size = New System.Drawing.Size(240, 130)
        Me.CmdMDlong.TabIndex = 7
        Me.CmdMDlong.Text = "Meas.MD Long Sample"
        Me.CmdMDlong.UseVisualStyleBackColor = True
        '
        'CmdQuitSplash
        '
        Me.CmdQuitSplash.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdQuitSplash.Location = New System.Drawing.Point(638, 489)
        Me.CmdQuitSplash.Name = "CmdQuitSplash"
        Me.CmdQuitSplash.Size = New System.Drawing.Size(120, 35)
        Me.CmdQuitSplash.TabIndex = 8
        Me.CmdQuitSplash.Text = "Close"
        Me.CmdQuitSplash.UseVisualStyleBackColor = True
        '
        'CmdTest
        '
        Me.CmdTest.Font = New System.Drawing.Font("MS UI Gothic", 20.0!)
        Me.CmdTest.Location = New System.Drawing.Point(518, 323)
        Me.CmdTest.Name = "CmdTest"
        Me.CmdTest.Size = New System.Drawing.Size(240, 130)
        Me.CmdTest.TabIndex = 9
        Me.CmdTest.Text = "Test・Adjust"
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
        Me.CmdCutSheetProfile.Text = "Cut Sheet"
        Me.CmdCutSheetProfile.UseVisualStyleBackColor = True
        '
        'CmdProfile
        '
        Me.CmdProfile.Font = New System.Drawing.Font("MS UI Gothic", 20.0!)
        Me.CmdProfile.Location = New System.Drawing.Point(518, 130)
        Me.CmdProfile.Name = "CmdProfile"
        Me.CmdProfile.Size = New System.Drawing.Size(240, 130)
        Me.CmdProfile.TabIndex = 5
        Me.CmdProfile.Text = "Profile"
        Me.CmdProfile.UseVisualStyleBackColor = True
        '
        'CmdSinglesheet
        '
        Me.CmdSinglesheet.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdSinglesheet.Location = New System.Drawing.Point(22, 130)
        Me.CmdSinglesheet.Name = "CmdSinglesheet"
        Me.CmdSinglesheet.Size = New System.Drawing.Size(240, 130)
        Me.CmdSinglesheet.TabIndex = 3
        Me.CmdSinglesheet.Text = "Single Sheet"
        Me.CmdSinglesheet.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.SettingToolStripMenuItem, Me.AdmModeToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(784, 24)
        Me.MenuStrip1.TabIndex = 11
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SingleSheetToolStripMenuItem, Me.CutSheetToolStripMenuItem, Me.ProfileToolStripMenuItem, Me.QuitToolStripMenuItem})
        Me.ToolStripMenuItem1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(46, 20)
        Me.ToolStripMenuItem1.Text = "Meas."
        '
        'SingleSheetToolStripMenuItem
        '
        Me.SingleSheetToolStripMenuItem.Name = "SingleSheetToolStripMenuItem"
        Me.SingleSheetToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.SingleSheetToolStripMenuItem.Text = "Single Sheet"
        '
        'CutSheetToolStripMenuItem
        '
        Me.CutSheetToolStripMenuItem.Name = "CutSheetToolStripMenuItem"
        Me.CutSheetToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.CutSheetToolStripMenuItem.Text = "Cut Sheet"
        '
        'ProfileToolStripMenuItem
        '
        Me.ProfileToolStripMenuItem.Name = "ProfileToolStripMenuItem"
        Me.ProfileToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.ProfileToolStripMenuItem.Text = "Profile"
        '
        'QuitToolStripMenuItem
        '
        Me.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem"
        Me.QuitToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.QuitToolStripMenuItem.Text = "Close"
        '
        'SettingToolStripMenuItem
        '
        Me.SettingToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AdmLoginToolStripMenuItem, Me.SettingToolStripMenuItem1})
        Me.SettingToolStripMenuItem.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.SettingToolStripMenuItem.Name = "SettingToolStripMenuItem"
        Me.SettingToolStripMenuItem.Size = New System.Drawing.Size(53, 20)
        Me.SettingToolStripMenuItem.Text = "Setting"
        '
        'AdmLoginToolStripMenuItem
        '
        Me.AdmLoginToolStripMenuItem.Name = "AdmLoginToolStripMenuItem"
        Me.AdmLoginToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.AdmLoginToolStripMenuItem.Text = "Admin. Login"
        '
        'SettingToolStripMenuItem1
        '
        Me.SettingToolStripMenuItem1.Name = "SettingToolStripMenuItem1"
        Me.SettingToolStripMenuItem1.Size = New System.Drawing.Size(135, 22)
        Me.SettingToolStripMenuItem1.Text = "Setting"
        '
        'AdmModeToolStripMenuItem
        '
        Me.AdmModeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MDLongToolStripMenuItem1, Me.TestToolStripMenuItem})
        Me.AdmModeToolStripMenuItem.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.AdmModeToolStripMenuItem.Name = "AdmModeToolStripMenuItem"
        Me.AdmModeToolStripMenuItem.Size = New System.Drawing.Size(82, 20)
        Me.AdmModeToolStripMenuItem.Text = "Admin.Mode"
        '
        'MDLongToolStripMenuItem1
        '
        Me.MDLongToolStripMenuItem1.Name = "MDLongToolStripMenuItem1"
        Me.MDLongToolStripMenuItem1.Size = New System.Drawing.Size(189, 22)
        Me.MDLongToolStripMenuItem1.Text = "Meas.MD Long Sample"
        '
        'TestToolStripMenuItem
        '
        Me.TestToolStripMenuItem.Name = "TestToolStripMenuItem"
        Me.TestToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.TestToolStripMenuItem.Text = "Test・Adjust"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SST4500HelpToolStripMenuItem, Me.SST4500InfoToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'SST4500HelpToolStripMenuItem
        '
        Me.SST4500HelpToolStripMenuItem.Enabled = False
        Me.SST4500HelpToolStripMenuItem.Name = "SST4500HelpToolStripMenuItem"
        Me.SST4500HelpToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.SST4500HelpToolStripMenuItem.Text = "SST-4500 Help"
        '
        'SST4500InfoToolStripMenuItem
        '
        Me.SST4500InfoToolStripMenuItem.Name = "SST4500InfoToolStripMenuItem"
        Me.SST4500InfoToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.SST4500InfoToolStripMenuItem.Text = "About SST-4500"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.SST4500_1_0_0E.My.Resources.Resources.nomura_logo1e
        Me.PictureBox1.Location = New System.Drawing.Point(621, 25)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(160, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 12
        Me.PictureBox1.TabStop = False
        '
        'FrmSST4500_1_0_0E_main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(784, 562)
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
        Me.Name = "FrmSST4500_1_0_0E_main"
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
    Friend WithEvents SingleSheetToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CutSheetToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProfileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents QuitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AdmLoginToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents AdmModeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MDLongToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents TestToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SST4500HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SST4500InfoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel4 As ToolStripStatusLabel
End Class
