<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSST4500_1_0_0J_meas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSST4500_1_0_0J_meas))
        Me.LblProductNameMeas = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblMeasSpecCur = New System.Windows.Forms.Label()
        Me.LblMeasSpecBak = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtMachNoCur = New System.Windows.Forms.TextBox()
        Me.TxtMachNoBak = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtSmplNamCur = New System.Windows.Forms.TextBox()
        Me.TxtSmplNamBak = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TblMeasInfo_adm = New System.Windows.Forms.TableLayoutPanel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.LblTSICDBak_adm = New System.Windows.Forms.Label()
        Me.LblTSIMDBak_adm = New System.Windows.Forms.Label()
        Me.LblSpdDeepBak_adm = New System.Windows.Forms.Label()
        Me.LblSpdPeakBak_adm = New System.Windows.Forms.Label()
        Me.LblSpdCDBak_adm = New System.Windows.Forms.Label()
        Me.LblSpdMDBak_adm = New System.Windows.Forms.Label()
        Me.LblratioPKDPBak_adm = New System.Windows.Forms.Label()
        Me.LblratioMDCDBak_adm = New System.Windows.Forms.Label()
        Me.LblAnglDeepBak_adm = New System.Windows.Forms.Label()
        Me.LblAnglPeakBak_adm = New System.Windows.Forms.Label()
        Me.LblMeasNumBak_adm = New System.Windows.Forms.Label()
        Me.LblTSICDCur_adm = New System.Windows.Forms.Label()
        Me.LblTSIMDCur_adm = New System.Windows.Forms.Label()
        Me.LblSpdDeepCur_adm = New System.Windows.Forms.Label()
        Me.LblSpdPeakCur_adm = New System.Windows.Forms.Label()
        Me.LblSpdCDCur_adm = New System.Windows.Forms.Label()
        Me.LblSpdMDCur_adm = New System.Windows.Forms.Label()
        Me.LblratioPKDPCur_adm = New System.Windows.Forms.Label()
        Me.LblratioMDCDCur_adm = New System.Windows.Forms.Label()
        Me.LblAnglDeepCur_adm = New System.Windows.Forms.Label()
        Me.LblAnglPeakCur_adm = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.LblMeasNumCur_adm = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.TimMeas = New System.Windows.Forms.Timer(Me.components)
        Me.CmdMeas = New System.Windows.Forms.Button()
        Me.CmdEtcMeasData = New System.Windows.Forms.Button()
        Me.CmdOldDataLoad = New System.Windows.Forms.Button()
        Me.CmdEtcOldMeasData = New System.Windows.Forms.Button()
        Me.CmdQuitSinglesheet = New System.Windows.Forms.Button()
        Me.TblMeasData_adm = New System.Windows.Forms.TableLayoutPanel()
        Me.LblMeasDatBak1_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak2_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak3_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak4_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak5_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak6_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak7_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak8_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak9_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak10_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak11_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak12_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak13_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak14_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak15_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatBak16_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur1_adm = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.LblMeasDatCur16_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur13_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur12_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur11_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur10_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur9_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur7_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur6_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur5_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur4_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur8_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur3_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur2_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur14_adm = New System.Windows.Forms.Label()
        Me.LblMeasDatCur15_adm = New System.Windows.Forms.Label()
        Me.TblMeasInfo_nom = New System.Windows.Forms.TableLayoutPanel()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.LblTSICDCur_nom = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.LblTSIMDCur_nom = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.LblSpdDeepCur_nom = New System.Windows.Forms.Label()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.LblSpdPeakCur_nom = New System.Windows.Forms.Label()
        Me.LblSpdCDCur_nom = New System.Windows.Forms.Label()
        Me.LblSpdMDCur_nom = New System.Windows.Forms.Label()
        Me.LblratioPKDPCur_nom = New System.Windows.Forms.Label()
        Me.LblratioMDCDCur_nom = New System.Windows.Forms.Label()
        Me.LblAnglDeepCur_nom = New System.Windows.Forms.Label()
        Me.LblAnglPeakCur_nom = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.LblMeasNumCur_nom = New System.Windows.Forms.Label()
        Me.TblMeasData_nom = New System.Windows.Forms.TableLayoutPanel()
        Me.LblMeasDatCur1_nom = New System.Windows.Forms.Label()
        Me.Label203 = New System.Windows.Forms.Label()
        Me.Label204 = New System.Windows.Forms.Label()
        Me.Label205 = New System.Windows.Forms.Label()
        Me.Label207 = New System.Windows.Forms.Label()
        Me.Label208 = New System.Windows.Forms.Label()
        Me.Label209 = New System.Windows.Forms.Label()
        Me.Label210 = New System.Windows.Forms.Label()
        Me.Label211 = New System.Windows.Forms.Label()
        Me.Label212 = New System.Windows.Forms.Label()
        Me.Label213 = New System.Windows.Forms.Label()
        Me.Label214 = New System.Windows.Forms.Label()
        Me.Label215 = New System.Windows.Forms.Label()
        Me.Label216 = New System.Windows.Forms.Label()
        Me.Label217 = New System.Windows.Forms.Label()
        Me.Label218 = New System.Windows.Forms.Label()
        Me.Label219 = New System.Windows.Forms.Label()
        Me.Label220 = New System.Windows.Forms.Label()
        Me.Label221 = New System.Windows.Forms.Label()
        Me.Label222 = New System.Windows.Forms.Label()
        Me.LblMeasDatCur16_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur13_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur12_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur11_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur10_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur9_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur7_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur6_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur5_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur4_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur8_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur3_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur2_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur14_nom = New System.Windows.Forms.Label()
        Me.LblMeasDatCur15_nom = New System.Windows.Forms.Label()
        Me.GbPrint = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CmdMeasResultSave = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CmdMeasPrint = New System.Windows.Forms.Button()
        Me.ChkMeasAutoPrn = New System.Windows.Forms.CheckBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PrintDocument_adm = New System.Drawing.Printing.PrintDocument()
        Me.PPD_amd = New System.Windows.Forms.PrintPreviewDialog()
        Me.PPD_nom = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintDocument_nom = New System.Drawing.Printing.PrintDocument()
        Me.GbMeasSpec = New System.Windows.Forms.GroupBox()
        Me.CmdMeasSpecSel = New System.Windows.Forms.Button()
        Me.CmdMeasSpecSave = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.TxtMeasNumCur = New System.Windows.Forms.Label()
        Me.TxtMeasNumBak = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ファイルToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.測定仕様ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.選択ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.保存ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.過去データToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.読込ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.他の測定データ選択ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.終了ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.測定ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.測定開始ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.他の測定データ選択ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.結果ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.印刷ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.手動印刷ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.保存ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.設定ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.設定ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ヘルプToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SST4500ヘルプToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SST4500についてToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.TxtMarkCur = New System.Windows.Forms.TextBox()
        Me.TxtMarkBak = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmdClsGraph = New System.Windows.Forms.Button()
        Me.TblMeasInfo_adm.SuspendLayout()
        Me.TblMeasData_adm.SuspendLayout()
        Me.TblMeasInfo_nom.SuspendLayout()
        Me.TblMeasData_nom.SuspendLayout()
        Me.GbPrint.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GbMeasSpec.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblProductNameMeas
        '
        Me.LblProductNameMeas.AutoSize = True
        Me.LblProductNameMeas.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblProductNameMeas.Location = New System.Drawing.Point(10, 26)
        Me.LblProductNameMeas.Name = "LblProductNameMeas"
        Me.LblProductNameMeas.Size = New System.Drawing.Size(138, 27)
        Me.LblProductNameMeas.TabIndex = 0
        Me.LblProductNameMeas.Text = "SST-4500"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(156, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(167, 27)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "シングルシート"
        '
        'LblMeasSpecCur
        '
        Me.LblMeasSpecCur.AutoSize = True
        Me.LblMeasSpecCur.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasSpecCur.Location = New System.Drawing.Point(12, 86)
        Me.LblMeasSpecCur.Name = "LblMeasSpecCur"
        Me.LblMeasSpecCur.Size = New System.Drawing.Size(67, 14)
        Me.LblMeasSpecCur.TabIndex = 2
        Me.LblMeasSpecCur.Text = "測定仕様"
        '
        'LblMeasSpecBak
        '
        Me.LblMeasSpecBak.AutoSize = True
        Me.LblMeasSpecBak.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasSpecBak.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasSpecBak.Location = New System.Drawing.Point(12, 112)
        Me.LblMeasSpecBak.Name = "LblMeasSpecBak"
        Me.LblMeasSpecBak.Size = New System.Drawing.Size(79, 14)
        Me.LblMeasSpecBak.TabIndex = 3
        Me.LblMeasSpecBak.Text = "過去の仕様"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(94, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "マシーン No."
        '
        'TxtMachNoCur
        '
        Me.TxtMachNoCur.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtMachNoCur.Location = New System.Drawing.Point(97, 83)
        Me.TxtMachNoCur.Name = "TxtMachNoCur"
        Me.TxtMachNoCur.Size = New System.Drawing.Size(100, 22)
        Me.TxtMachNoCur.TabIndex = 5
        '
        'TxtMachNoBak
        '
        Me.TxtMachNoBak.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtMachNoBak.ForeColor = System.Drawing.Color.Blue
        Me.TxtMachNoBak.Location = New System.Drawing.Point(97, 109)
        Me.TxtMachNoBak.Name = "TxtMachNoBak"
        Me.TxtMachNoBak.Size = New System.Drawing.Size(100, 22)
        Me.TxtMachNoBak.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(201, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 14)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "サンプル名"
        '
        'TxtSmplNamCur
        '
        Me.TxtSmplNamCur.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSmplNamCur.Location = New System.Drawing.Point(201, 83)
        Me.TxtSmplNamCur.Name = "TxtSmplNamCur"
        Me.TxtSmplNamCur.Size = New System.Drawing.Size(260, 22)
        Me.TxtSmplNamCur.TabIndex = 8
        '
        'TxtSmplNamBak
        '
        Me.TxtSmplNamBak.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtSmplNamBak.ForeColor = System.Drawing.Color.Blue
        Me.TxtSmplNamBak.Location = New System.Drawing.Point(201, 109)
        Me.TxtSmplNamBak.Name = "TxtSmplNamBak"
        Me.TxtSmplNamBak.Size = New System.Drawing.Size(260, 22)
        Me.TxtSmplNamBak.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(540, 64)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 14)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "測定回数"
        '
        'TblMeasInfo_adm
        '
        Me.TblMeasInfo_adm.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TblMeasInfo_adm.ColumnCount = 12
        Me.TblMeasInfo_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TblMeasInfo_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TblMeasInfo_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TblMeasInfo_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TblMeasInfo_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68.0!))
        Me.TblMeasInfo_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72.0!))
        Me.TblMeasInfo_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TblMeasInfo_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TblMeasInfo_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TblMeasInfo_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TblMeasInfo_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TblMeasInfo_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblMeasInfo_adm.Controls.Add(Me.Label21, 10, 0)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label17, 8, 0)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label16, 6, 0)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label11, 4, 0)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblTSICDBak_adm, 11, 3)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblTSIMDBak_adm, 10, 3)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblSpdDeepBak_adm, 9, 3)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblSpdPeakBak_adm, 8, 3)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblSpdCDBak_adm, 7, 3)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblSpdMDBak_adm, 6, 3)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblratioPKDPBak_adm, 5, 3)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblratioMDCDBak_adm, 4, 3)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblAnglDeepBak_adm, 3, 3)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblAnglPeakBak_adm, 2, 3)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblMeasNumBak_adm, 1, 3)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblTSICDCur_adm, 11, 2)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblTSIMDCur_adm, 10, 2)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblSpdDeepCur_adm, 9, 2)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblSpdPeakCur_adm, 8, 2)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblSpdCDCur_adm, 7, 2)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblSpdMDCur_adm, 6, 2)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblratioPKDPCur_adm, 5, 2)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblratioMDCDCur_adm, 4, 2)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblAnglDeepCur_adm, 3, 2)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblAnglPeakCur_adm, 2, 2)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label28, 11, 1)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label27, 10, 1)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label26, 9, 1)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label25, 8, 1)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label8, 0, 0)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label9, 1, 0)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label12, 2, 1)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label13, 3, 1)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label14, 4, 1)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label15, 5, 1)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label23, 6, 1)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label24, 7, 1)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label29, 0, 2)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label30, 0, 3)
        Me.TblMeasInfo_adm.Controls.Add(Me.LblMeasNumCur_adm, 1, 2)
        Me.TblMeasInfo_adm.Controls.Add(Me.Label18, 2, 0)
        Me.TblMeasInfo_adm.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TblMeasInfo_adm.Location = New System.Drawing.Point(15, 135)
        Me.TblMeasInfo_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.TblMeasInfo_adm.Name = "TblMeasInfo_adm"
        Me.TblMeasInfo_adm.RowCount = 4
        Me.TblMeasInfo_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TblMeasInfo_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TblMeasInfo_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TblMeasInfo_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TblMeasInfo_adm.Size = New System.Drawing.Size(798, 115)
        Me.TblMeasInfo_adm.TabIndex = 13
        Me.TblMeasInfo_adm.Visible = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.TblMeasInfo_adm.SetColumnSpan(Me.Label21, 2)
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label21.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label21.Location = New System.Drawing.Point(666, 1)
        Me.Label21.Margin = New System.Windows.Forms.Padding(0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(131, 35)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "TSI (Km/S)^2"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.TblMeasInfo_adm.SetColumnSpan(Me.Label17, 2)
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label17.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label17.Location = New System.Drawing.Point(534, 1)
        Me.Label17.Margin = New System.Windows.Forms.Padding(0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(131, 35)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "伝播速度 [Km/S]"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.TblMeasInfo_adm.SetColumnSpan(Me.Label16, 2)
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label16.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label16.Location = New System.Drawing.Point(402, 1)
        Me.Label16.Margin = New System.Windows.Forms.Padding(0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(131, 35)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "伝播速度 [Km/S]"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.TblMeasInfo_adm.SetColumnSpan(Me.Label11, 2)
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label11.Location = New System.Drawing.Point(260, 1)
        Me.Label11.Margin = New System.Windows.Forms.Padding(0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(141, 35)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "配向比"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblTSICDBak_adm
        '
        Me.LblTSICDBak_adm.AutoSize = True
        Me.LblTSICDBak_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblTSICDBak_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblTSICDBak_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTSICDBak_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblTSICDBak_adm.Location = New System.Drawing.Point(732, 89)
        Me.LblTSICDBak_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblTSICDBak_adm.Name = "LblTSICDBak_adm"
        Me.LblTSICDBak_adm.Size = New System.Drawing.Size(65, 25)
        Me.LblTSICDBak_adm.TabIndex = 40
        Me.LblTSICDBak_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblTSIMDBak_adm
        '
        Me.LblTSIMDBak_adm.AutoSize = True
        Me.LblTSIMDBak_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblTSIMDBak_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblTSIMDBak_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTSIMDBak_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblTSIMDBak_adm.Location = New System.Drawing.Point(666, 89)
        Me.LblTSIMDBak_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblTSIMDBak_adm.Name = "LblTSIMDBak_adm"
        Me.LblTSIMDBak_adm.Size = New System.Drawing.Size(65, 25)
        Me.LblTSIMDBak_adm.TabIndex = 39
        Me.LblTSIMDBak_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSpdDeepBak_adm
        '
        Me.LblSpdDeepBak_adm.AutoSize = True
        Me.LblSpdDeepBak_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblSpdDeepBak_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblSpdDeepBak_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpdDeepBak_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblSpdDeepBak_adm.Location = New System.Drawing.Point(600, 89)
        Me.LblSpdDeepBak_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblSpdDeepBak_adm.Name = "LblSpdDeepBak_adm"
        Me.LblSpdDeepBak_adm.Size = New System.Drawing.Size(65, 25)
        Me.LblSpdDeepBak_adm.TabIndex = 38
        Me.LblSpdDeepBak_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSpdPeakBak_adm
        '
        Me.LblSpdPeakBak_adm.AutoSize = True
        Me.LblSpdPeakBak_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblSpdPeakBak_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblSpdPeakBak_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpdPeakBak_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblSpdPeakBak_adm.Location = New System.Drawing.Point(534, 89)
        Me.LblSpdPeakBak_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblSpdPeakBak_adm.Name = "LblSpdPeakBak_adm"
        Me.LblSpdPeakBak_adm.Size = New System.Drawing.Size(65, 25)
        Me.LblSpdPeakBak_adm.TabIndex = 37
        Me.LblSpdPeakBak_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSpdCDBak_adm
        '
        Me.LblSpdCDBak_adm.AutoSize = True
        Me.LblSpdCDBak_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblSpdCDBak_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblSpdCDBak_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpdCDBak_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblSpdCDBak_adm.Location = New System.Drawing.Point(468, 89)
        Me.LblSpdCDBak_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblSpdCDBak_adm.Name = "LblSpdCDBak_adm"
        Me.LblSpdCDBak_adm.Size = New System.Drawing.Size(65, 25)
        Me.LblSpdCDBak_adm.TabIndex = 36
        Me.LblSpdCDBak_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSpdMDBak_adm
        '
        Me.LblSpdMDBak_adm.AutoSize = True
        Me.LblSpdMDBak_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblSpdMDBak_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblSpdMDBak_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpdMDBak_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblSpdMDBak_adm.Location = New System.Drawing.Point(402, 89)
        Me.LblSpdMDBak_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblSpdMDBak_adm.Name = "LblSpdMDBak_adm"
        Me.LblSpdMDBak_adm.Size = New System.Drawing.Size(65, 25)
        Me.LblSpdMDBak_adm.TabIndex = 35
        Me.LblSpdMDBak_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblratioPKDPBak_adm
        '
        Me.LblratioPKDPBak_adm.AutoSize = True
        Me.LblratioPKDPBak_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblratioPKDPBak_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblratioPKDPBak_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblratioPKDPBak_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblratioPKDPBak_adm.Location = New System.Drawing.Point(329, 89)
        Me.LblratioPKDPBak_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblratioPKDPBak_adm.Name = "LblratioPKDPBak_adm"
        Me.LblratioPKDPBak_adm.Size = New System.Drawing.Size(72, 25)
        Me.LblratioPKDPBak_adm.TabIndex = 34
        Me.LblratioPKDPBak_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblratioMDCDBak_adm
        '
        Me.LblratioMDCDBak_adm.AutoSize = True
        Me.LblratioMDCDBak_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblratioMDCDBak_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblratioMDCDBak_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblratioMDCDBak_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblratioMDCDBak_adm.Location = New System.Drawing.Point(260, 89)
        Me.LblratioMDCDBak_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblratioMDCDBak_adm.Name = "LblratioMDCDBak_adm"
        Me.LblratioMDCDBak_adm.Size = New System.Drawing.Size(68, 25)
        Me.LblratioMDCDBak_adm.TabIndex = 33
        Me.LblratioMDCDBak_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblAnglDeepBak_adm
        '
        Me.LblAnglDeepBak_adm.AutoSize = True
        Me.LblAnglDeepBak_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblAnglDeepBak_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblAnglDeepBak_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAnglDeepBak_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblAnglDeepBak_adm.Location = New System.Drawing.Point(189, 89)
        Me.LblAnglDeepBak_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblAnglDeepBak_adm.Name = "LblAnglDeepBak_adm"
        Me.LblAnglDeepBak_adm.Size = New System.Drawing.Size(70, 25)
        Me.LblAnglDeepBak_adm.TabIndex = 32
        Me.LblAnglDeepBak_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblAnglPeakBak_adm
        '
        Me.LblAnglPeakBak_adm.AutoSize = True
        Me.LblAnglPeakBak_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblAnglPeakBak_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblAnglPeakBak_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAnglPeakBak_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblAnglPeakBak_adm.Location = New System.Drawing.Point(118, 89)
        Me.LblAnglPeakBak_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblAnglPeakBak_adm.Name = "LblAnglPeakBak_adm"
        Me.LblAnglPeakBak_adm.Size = New System.Drawing.Size(70, 25)
        Me.LblAnglPeakBak_adm.TabIndex = 31
        Me.LblAnglPeakBak_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasNumBak_adm
        '
        Me.LblMeasNumBak_adm.AutoSize = True
        Me.LblMeasNumBak_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblMeasNumBak_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasNumBak_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasNumBak_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasNumBak_adm.Location = New System.Drawing.Point(72, 89)
        Me.LblMeasNumBak_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasNumBak_adm.Name = "LblMeasNumBak_adm"
        Me.LblMeasNumBak_adm.Size = New System.Drawing.Size(45, 25)
        Me.LblMeasNumBak_adm.TabIndex = 30
        Me.LblMeasNumBak_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblTSICDCur_adm
        '
        Me.LblTSICDCur_adm.AutoSize = True
        Me.LblTSICDCur_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblTSICDCur_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblTSICDCur_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTSICDCur_adm.Location = New System.Drawing.Point(732, 63)
        Me.LblTSICDCur_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblTSICDCur_adm.Name = "LblTSICDCur_adm"
        Me.LblTSICDCur_adm.Size = New System.Drawing.Size(65, 25)
        Me.LblTSICDCur_adm.TabIndex = 29
        Me.LblTSICDCur_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblTSIMDCur_adm
        '
        Me.LblTSIMDCur_adm.AutoSize = True
        Me.LblTSIMDCur_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblTSIMDCur_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblTSIMDCur_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTSIMDCur_adm.Location = New System.Drawing.Point(666, 63)
        Me.LblTSIMDCur_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblTSIMDCur_adm.Name = "LblTSIMDCur_adm"
        Me.LblTSIMDCur_adm.Size = New System.Drawing.Size(65, 25)
        Me.LblTSIMDCur_adm.TabIndex = 28
        Me.LblTSIMDCur_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSpdDeepCur_adm
        '
        Me.LblSpdDeepCur_adm.AutoSize = True
        Me.LblSpdDeepCur_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblSpdDeepCur_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblSpdDeepCur_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpdDeepCur_adm.Location = New System.Drawing.Point(600, 63)
        Me.LblSpdDeepCur_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblSpdDeepCur_adm.Name = "LblSpdDeepCur_adm"
        Me.LblSpdDeepCur_adm.Size = New System.Drawing.Size(65, 25)
        Me.LblSpdDeepCur_adm.TabIndex = 27
        Me.LblSpdDeepCur_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSpdPeakCur_adm
        '
        Me.LblSpdPeakCur_adm.AutoSize = True
        Me.LblSpdPeakCur_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblSpdPeakCur_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblSpdPeakCur_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpdPeakCur_adm.Location = New System.Drawing.Point(534, 63)
        Me.LblSpdPeakCur_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblSpdPeakCur_adm.Name = "LblSpdPeakCur_adm"
        Me.LblSpdPeakCur_adm.Size = New System.Drawing.Size(65, 25)
        Me.LblSpdPeakCur_adm.TabIndex = 26
        Me.LblSpdPeakCur_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSpdCDCur_adm
        '
        Me.LblSpdCDCur_adm.AutoSize = True
        Me.LblSpdCDCur_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblSpdCDCur_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblSpdCDCur_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpdCDCur_adm.Location = New System.Drawing.Point(468, 63)
        Me.LblSpdCDCur_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblSpdCDCur_adm.Name = "LblSpdCDCur_adm"
        Me.LblSpdCDCur_adm.Size = New System.Drawing.Size(65, 25)
        Me.LblSpdCDCur_adm.TabIndex = 25
        Me.LblSpdCDCur_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSpdMDCur_adm
        '
        Me.LblSpdMDCur_adm.AutoSize = True
        Me.LblSpdMDCur_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblSpdMDCur_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblSpdMDCur_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpdMDCur_adm.Location = New System.Drawing.Point(402, 63)
        Me.LblSpdMDCur_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblSpdMDCur_adm.Name = "LblSpdMDCur_adm"
        Me.LblSpdMDCur_adm.Size = New System.Drawing.Size(65, 25)
        Me.LblSpdMDCur_adm.TabIndex = 24
        Me.LblSpdMDCur_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblratioPKDPCur_adm
        '
        Me.LblratioPKDPCur_adm.AutoSize = True
        Me.LblratioPKDPCur_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblratioPKDPCur_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblratioPKDPCur_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblratioPKDPCur_adm.Location = New System.Drawing.Point(329, 63)
        Me.LblratioPKDPCur_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblratioPKDPCur_adm.Name = "LblratioPKDPCur_adm"
        Me.LblratioPKDPCur_adm.Size = New System.Drawing.Size(72, 25)
        Me.LblratioPKDPCur_adm.TabIndex = 23
        Me.LblratioPKDPCur_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblratioMDCDCur_adm
        '
        Me.LblratioMDCDCur_adm.AutoSize = True
        Me.LblratioMDCDCur_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblratioMDCDCur_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblratioMDCDCur_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblratioMDCDCur_adm.Location = New System.Drawing.Point(260, 63)
        Me.LblratioMDCDCur_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblratioMDCDCur_adm.Name = "LblratioMDCDCur_adm"
        Me.LblratioMDCDCur_adm.Size = New System.Drawing.Size(68, 25)
        Me.LblratioMDCDCur_adm.TabIndex = 22
        Me.LblratioMDCDCur_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblAnglDeepCur_adm
        '
        Me.LblAnglDeepCur_adm.AutoSize = True
        Me.LblAnglDeepCur_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblAnglDeepCur_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblAnglDeepCur_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAnglDeepCur_adm.Location = New System.Drawing.Point(189, 63)
        Me.LblAnglDeepCur_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblAnglDeepCur_adm.Name = "LblAnglDeepCur_adm"
        Me.LblAnglDeepCur_adm.Size = New System.Drawing.Size(70, 25)
        Me.LblAnglDeepCur_adm.TabIndex = 21
        Me.LblAnglDeepCur_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblAnglPeakCur_adm
        '
        Me.LblAnglPeakCur_adm.AutoSize = True
        Me.LblAnglPeakCur_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblAnglPeakCur_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblAnglPeakCur_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAnglPeakCur_adm.Location = New System.Drawing.Point(118, 63)
        Me.LblAnglPeakCur_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblAnglPeakCur_adm.Name = "LblAnglPeakCur_adm"
        Me.LblAnglPeakCur_adm.Size = New System.Drawing.Size(70, 25)
        Me.LblAnglPeakCur_adm.TabIndex = 20
        Me.LblAnglPeakCur_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label28.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label28.Location = New System.Drawing.Point(732, 37)
        Me.Label28.Margin = New System.Windows.Forms.Padding(0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(65, 25)
        Me.Label28.TabIndex = 16
        Me.Label28.Text = "CD"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label27.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label27.Location = New System.Drawing.Point(666, 37)
        Me.Label27.Margin = New System.Windows.Forms.Padding(0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(65, 25)
        Me.Label27.TabIndex = 15
        Me.Label27.Text = "MD"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label26.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label26.Location = New System.Drawing.Point(600, 37)
        Me.Label26.Margin = New System.Windows.Forms.Padding(0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(65, 25)
        Me.Label26.TabIndex = 14
        Me.Label26.Text = "Deep"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label25.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label25.Location = New System.Drawing.Point(534, 37)
        Me.Label25.Margin = New System.Windows.Forms.Padding(0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(65, 25)
        Me.Label25.TabIndex = 13
        Me.Label25.Text = "Peak"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(1, 1)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.Name = "Label8"
        Me.TblMeasInfo_adm.SetRowSpan(Me.Label8, 2)
        Me.Label8.Size = New System.Drawing.Size(70, 61)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "データ"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(72, 1)
        Me.Label9.Margin = New System.Windows.Forms.Padding(0)
        Me.Label9.Name = "Label9"
        Me.TblMeasInfo_adm.SetRowSpan(Me.Label9, 2)
        Me.Label9.Size = New System.Drawing.Size(45, 61)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "測定" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "No."
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(118, 37)
        Me.Label12.Margin = New System.Windows.Forms.Padding(0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(70, 25)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Peak"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label13.Location = New System.Drawing.Point(189, 37)
        Me.Label13.Margin = New System.Windows.Forms.Padding(0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(70, 25)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "Deep"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label14.Location = New System.Drawing.Point(260, 37)
        Me.Label14.Margin = New System.Windows.Forms.Padding(0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 25)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "MD/CD"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label15.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label15.Location = New System.Drawing.Point(329, 37)
        Me.Label15.Margin = New System.Windows.Forms.Padding(0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(72, 25)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "Peak/Deep"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label23.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label23.Location = New System.Drawing.Point(402, 37)
        Me.Label23.Margin = New System.Windows.Forms.Padding(0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(65, 25)
        Me.Label23.TabIndex = 11
        Me.Label23.Text = "MD"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label24.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label24.Location = New System.Drawing.Point(468, 37)
        Me.Label24.Margin = New System.Windows.Forms.Padding(0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(65, 25)
        Me.Label24.TabIndex = 12
        Me.Label24.Text = "CD"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label29.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label29.Location = New System.Drawing.Point(1, 63)
        Me.Label29.Margin = New System.Windows.Forms.Padding(0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(70, 25)
        Me.Label29.TabIndex = 17
        Me.Label29.Text = "測定データ"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label30.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Blue
        Me.Label30.Location = New System.Drawing.Point(1, 89)
        Me.Label30.Margin = New System.Windows.Forms.Padding(0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(70, 25)
        Me.Label30.TabIndex = 18
        Me.Label30.Text = "過去データ"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasNumCur_adm
        '
        Me.LblMeasNumCur_adm.AutoSize = True
        Me.LblMeasNumCur_adm.BackColor = System.Drawing.Color.Transparent
        Me.LblMeasNumCur_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasNumCur_adm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasNumCur_adm.Location = New System.Drawing.Point(72, 63)
        Me.LblMeasNumCur_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasNumCur_adm.Name = "LblMeasNumCur_adm"
        Me.LblMeasNumCur_adm.Size = New System.Drawing.Size(45, 25)
        Me.LblMeasNumCur_adm.TabIndex = 19
        Me.LblMeasNumCur_adm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.TblMeasInfo_adm.SetColumnSpan(Me.Label18, 2)
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label18.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label18.Location = New System.Drawing.Point(118, 1)
        Me.Label18.Margin = New System.Windows.Forms.Padding(0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(141, 35)
        Me.Label18.TabIndex = 27
        Me.Label18.Text = "配向角 [deg.]"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TimMeas
        '
        Me.TimMeas.Interval = 5
        '
        'CmdMeas
        '
        Me.CmdMeas.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdMeas.Location = New System.Drawing.Point(847, 243)
        Me.CmdMeas.Name = "CmdMeas"
        Me.CmdMeas.Size = New System.Drawing.Size(120, 35)
        Me.CmdMeas.TabIndex = 16
        Me.CmdMeas.Text = "測定開始"
        Me.CmdMeas.UseVisualStyleBackColor = True
        '
        'CmdEtcMeasData
        '
        Me.CmdEtcMeasData.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdEtcMeasData.Location = New System.Drawing.Point(847, 284)
        Me.CmdEtcMeasData.Name = "CmdEtcMeasData"
        Me.CmdEtcMeasData.Size = New System.Drawing.Size(120, 35)
        Me.CmdEtcMeasData.TabIndex = 17
        Me.CmdEtcMeasData.Text = "他の測定データ"
        Me.CmdEtcMeasData.UseVisualStyleBackColor = True
        '
        'CmdOldDataLoad
        '
        Me.CmdOldDataLoad.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdOldDataLoad.Location = New System.Drawing.Point(8, 16)
        Me.CmdOldDataLoad.Name = "CmdOldDataLoad"
        Me.CmdOldDataLoad.Size = New System.Drawing.Size(120, 35)
        Me.CmdOldDataLoad.TabIndex = 18
        Me.CmdOldDataLoad.Text = "読　込"
        Me.CmdOldDataLoad.UseVisualStyleBackColor = True
        '
        'CmdEtcOldMeasData
        '
        Me.CmdEtcOldMeasData.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdEtcOldMeasData.Location = New System.Drawing.Point(8, 57)
        Me.CmdEtcOldMeasData.Name = "CmdEtcOldMeasData"
        Me.CmdEtcOldMeasData.Size = New System.Drawing.Size(120, 35)
        Me.CmdEtcOldMeasData.TabIndex = 19
        Me.CmdEtcOldMeasData.Text = "他の測定データ"
        Me.CmdEtcOldMeasData.UseVisualStyleBackColor = True
        '
        'CmdQuitSinglesheet
        '
        Me.CmdQuitSinglesheet.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdQuitSinglesheet.Location = New System.Drawing.Point(847, 506)
        Me.CmdQuitSinglesheet.Name = "CmdQuitSinglesheet"
        Me.CmdQuitSinglesheet.Size = New System.Drawing.Size(120, 35)
        Me.CmdQuitSinglesheet.TabIndex = 20
        Me.CmdQuitSinglesheet.Text = "終　了"
        Me.CmdQuitSinglesheet.UseVisualStyleBackColor = True
        '
        'TblMeasData_adm
        '
        Me.TblMeasData_adm.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TblMeasData_adm.ColumnCount = 3
        Me.TblMeasData_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.0!))
        Me.TblMeasData_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TblMeasData_adm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.0!))
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak1_adm, 2, 2)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak2_adm, 2, 3)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak3_adm, 2, 4)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak4_adm, 2, 5)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak5_adm, 2, 6)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak6_adm, 2, 7)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak7_adm, 2, 8)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak8_adm, 2, 9)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak9_adm, 2, 10)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak10_adm, 2, 11)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak11_adm, 2, 12)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak12_adm, 2, 13)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak13_adm, 2, 14)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak14_adm, 2, 15)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak15_adm, 2, 16)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatBak16_adm, 2, 17)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur1_adm, 1, 2)
        Me.TblMeasData_adm.Controls.Add(Me.Label54, 0, 0)
        Me.TblMeasData_adm.Controls.Add(Me.Label55, 1, 0)
        Me.TblMeasData_adm.Controls.Add(Me.Label53, 1, 1)
        Me.TblMeasData_adm.Controls.Add(Me.Label56, 2, 1)
        Me.TblMeasData_adm.Controls.Add(Me.Label57, 0, 2)
        Me.TblMeasData_adm.Controls.Add(Me.Label58, 0, 3)
        Me.TblMeasData_adm.Controls.Add(Me.Label59, 0, 4)
        Me.TblMeasData_adm.Controls.Add(Me.Label64, 0, 9)
        Me.TblMeasData_adm.Controls.Add(Me.Label66, 0, 11)
        Me.TblMeasData_adm.Controls.Add(Me.Label60, 0, 5)
        Me.TblMeasData_adm.Controls.Add(Me.Label61, 0, 6)
        Me.TblMeasData_adm.Controls.Add(Me.Label62, 0, 7)
        Me.TblMeasData_adm.Controls.Add(Me.Label63, 0, 8)
        Me.TblMeasData_adm.Controls.Add(Me.Label65, 0, 10)
        Me.TblMeasData_adm.Controls.Add(Me.Label67, 0, 12)
        Me.TblMeasData_adm.Controls.Add(Me.Label68, 0, 13)
        Me.TblMeasData_adm.Controls.Add(Me.Label69, 0, 14)
        Me.TblMeasData_adm.Controls.Add(Me.Label70, 0, 15)
        Me.TblMeasData_adm.Controls.Add(Me.Label71, 0, 16)
        Me.TblMeasData_adm.Controls.Add(Me.Label72, 0, 17)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur16_adm, 1, 17)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur13_adm, 1, 14)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur12_adm, 1, 13)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur11_adm, 1, 12)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur10_adm, 1, 11)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur9_adm, 1, 10)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur7_adm, 1, 8)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur6_adm, 1, 7)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur5_adm, 1, 6)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur4_adm, 1, 5)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur8_adm, 1, 9)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur3_adm, 1, 4)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur2_adm, 1, 3)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur14_adm, 1, 15)
        Me.TblMeasData_adm.Controls.Add(Me.LblMeasDatCur15_adm, 1, 16)
        Me.TblMeasData_adm.Location = New System.Drawing.Point(505, 271)
        Me.TblMeasData_adm.Name = "TblMeasData_adm"
        Me.TblMeasData_adm.RowCount = 18
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_adm.Size = New System.Drawing.Size(312, 455)
        Me.TblMeasData_adm.TabIndex = 22
        Me.TblMeasData_adm.Visible = False
        '
        'LblMeasDatBak1_adm
        '
        Me.LblMeasDatBak1_adm.AutoSize = True
        Me.LblMeasDatBak1_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak1_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak1_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak1_adm.Location = New System.Drawing.Point(208, 55)
        Me.LblMeasDatBak1_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak1_adm.Name = "LblMeasDatBak1_adm"
        Me.LblMeasDatBak1_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak1_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak1_adm.TabIndex = 37
        Me.LblMeasDatBak1_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak2_adm
        '
        Me.LblMeasDatBak2_adm.AutoSize = True
        Me.LblMeasDatBak2_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak2_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak2_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak2_adm.Location = New System.Drawing.Point(208, 80)
        Me.LblMeasDatBak2_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak2_adm.Name = "LblMeasDatBak2_adm"
        Me.LblMeasDatBak2_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak2_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak2_adm.TabIndex = 52
        Me.LblMeasDatBak2_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak3_adm
        '
        Me.LblMeasDatBak3_adm.AutoSize = True
        Me.LblMeasDatBak3_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak3_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak3_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak3_adm.Location = New System.Drawing.Point(208, 105)
        Me.LblMeasDatBak3_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak3_adm.Name = "LblMeasDatBak3_adm"
        Me.LblMeasDatBak3_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak3_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak3_adm.TabIndex = 49
        Me.LblMeasDatBak3_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak4_adm
        '
        Me.LblMeasDatBak4_adm.AutoSize = True
        Me.LblMeasDatBak4_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak4_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak4_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak4_adm.Location = New System.Drawing.Point(208, 130)
        Me.LblMeasDatBak4_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak4_adm.Name = "LblMeasDatBak4_adm"
        Me.LblMeasDatBak4_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak4_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak4_adm.TabIndex = 48
        Me.LblMeasDatBak4_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak5_adm
        '
        Me.LblMeasDatBak5_adm.AutoSize = True
        Me.LblMeasDatBak5_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak5_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak5_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak5_adm.Location = New System.Drawing.Point(208, 155)
        Me.LblMeasDatBak5_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak5_adm.Name = "LblMeasDatBak5_adm"
        Me.LblMeasDatBak5_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak5_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak5_adm.TabIndex = 47
        Me.LblMeasDatBak5_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak6_adm
        '
        Me.LblMeasDatBak6_adm.AutoSize = True
        Me.LblMeasDatBak6_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak6_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak6_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak6_adm.Location = New System.Drawing.Point(208, 180)
        Me.LblMeasDatBak6_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak6_adm.Name = "LblMeasDatBak6_adm"
        Me.LblMeasDatBak6_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak6_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak6_adm.TabIndex = 46
        Me.LblMeasDatBak6_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak7_adm
        '
        Me.LblMeasDatBak7_adm.AutoSize = True
        Me.LblMeasDatBak7_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak7_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak7_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak7_adm.Location = New System.Drawing.Point(208, 205)
        Me.LblMeasDatBak7_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak7_adm.Name = "LblMeasDatBak7_adm"
        Me.LblMeasDatBak7_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak7_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak7_adm.TabIndex = 45
        Me.LblMeasDatBak7_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak8_adm
        '
        Me.LblMeasDatBak8_adm.AutoSize = True
        Me.LblMeasDatBak8_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak8_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak8_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak8_adm.Location = New System.Drawing.Point(208, 230)
        Me.LblMeasDatBak8_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak8_adm.Name = "LblMeasDatBak8_adm"
        Me.LblMeasDatBak8_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak8_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak8_adm.TabIndex = 43
        Me.LblMeasDatBak8_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak9_adm
        '
        Me.LblMeasDatBak9_adm.AutoSize = True
        Me.LblMeasDatBak9_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak9_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak9_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak9_adm.Location = New System.Drawing.Point(208, 255)
        Me.LblMeasDatBak9_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak9_adm.Name = "LblMeasDatBak9_adm"
        Me.LblMeasDatBak9_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak9_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak9_adm.TabIndex = 42
        Me.LblMeasDatBak9_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak10_adm
        '
        Me.LblMeasDatBak10_adm.AutoSize = True
        Me.LblMeasDatBak10_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak10_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak10_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak10_adm.Location = New System.Drawing.Point(208, 280)
        Me.LblMeasDatBak10_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak10_adm.Name = "LblMeasDatBak10_adm"
        Me.LblMeasDatBak10_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak10_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak10_adm.TabIndex = 41
        Me.LblMeasDatBak10_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak11_adm
        '
        Me.LblMeasDatBak11_adm.AutoSize = True
        Me.LblMeasDatBak11_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak11_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak11_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak11_adm.Location = New System.Drawing.Point(208, 305)
        Me.LblMeasDatBak11_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak11_adm.Name = "LblMeasDatBak11_adm"
        Me.LblMeasDatBak11_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak11_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak11_adm.TabIndex = 40
        Me.LblMeasDatBak11_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak12_adm
        '
        Me.LblMeasDatBak12_adm.AutoSize = True
        Me.LblMeasDatBak12_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak12_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak12_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak12_adm.Location = New System.Drawing.Point(208, 330)
        Me.LblMeasDatBak12_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak12_adm.Name = "LblMeasDatBak12_adm"
        Me.LblMeasDatBak12_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak12_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak12_adm.TabIndex = 44
        Me.LblMeasDatBak12_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak13_adm
        '
        Me.LblMeasDatBak13_adm.AutoSize = True
        Me.LblMeasDatBak13_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak13_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak13_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak13_adm.Location = New System.Drawing.Point(208, 355)
        Me.LblMeasDatBak13_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak13_adm.Name = "LblMeasDatBak13_adm"
        Me.LblMeasDatBak13_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak13_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak13_adm.TabIndex = 39
        Me.LblMeasDatBak13_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak14_adm
        '
        Me.LblMeasDatBak14_adm.AutoSize = True
        Me.LblMeasDatBak14_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak14_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak14_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak14_adm.Location = New System.Drawing.Point(208, 380)
        Me.LblMeasDatBak14_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak14_adm.Name = "LblMeasDatBak14_adm"
        Me.LblMeasDatBak14_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak14_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak14_adm.TabIndex = 38
        Me.LblMeasDatBak14_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak15_adm
        '
        Me.LblMeasDatBak15_adm.AutoSize = True
        Me.LblMeasDatBak15_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak15_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak15_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak15_adm.Location = New System.Drawing.Point(208, 405)
        Me.LblMeasDatBak15_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak15_adm.Name = "LblMeasDatBak15_adm"
        Me.LblMeasDatBak15_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak15_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak15_adm.TabIndex = 50
        Me.LblMeasDatBak15_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatBak16_adm
        '
        Me.LblMeasDatBak16_adm.AutoSize = True
        Me.LblMeasDatBak16_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatBak16_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatBak16_adm.ForeColor = System.Drawing.Color.Blue
        Me.LblMeasDatBak16_adm.Location = New System.Drawing.Point(208, 430)
        Me.LblMeasDatBak16_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatBak16_adm.Name = "LblMeasDatBak16_adm"
        Me.LblMeasDatBak16_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatBak16_adm.Size = New System.Drawing.Size(103, 24)
        Me.LblMeasDatBak16_adm.TabIndex = 51
        Me.LblMeasDatBak16_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur1_adm
        '
        Me.LblMeasDatCur1_adm.AutoSize = True
        Me.LblMeasDatCur1_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur1_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur1_adm.Location = New System.Drawing.Point(106, 55)
        Me.LblMeasDatCur1_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur1_adm.Name = "LblMeasDatCur1_adm"
        Me.LblMeasDatCur1_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur1_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur1_adm.TabIndex = 21
        Me.LblMeasDatCur1_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label54.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label54.Location = New System.Drawing.Point(1, 1)
        Me.Label54.Margin = New System.Windows.Forms.Padding(0)
        Me.Label54.Name = "Label54"
        Me.Label54.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.TblMeasData_adm.SetRowSpan(Me.Label54, 2)
        Me.Label54.Size = New System.Drawing.Size(104, 53)
        Me.Label54.TabIndex = 1
        Me.Label54.Text = "角度  [deg.]"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.TblMeasData_adm.SetColumnSpan(Me.Label55, 2)
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label55.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label55.Location = New System.Drawing.Point(106, 1)
        Me.Label55.Margin = New System.Windows.Forms.Padding(0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(205, 26)
        Me.Label55.TabIndex = 2
        Me.Label55.Text = "伝播速度　[Km/S]"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label53.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label53.Location = New System.Drawing.Point(106, 28)
        Me.Label53.Margin = New System.Windows.Forms.Padding(0)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(101, 26)
        Me.Label53.TabIndex = 3
        Me.Label53.Text = "測定データ"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label56.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label56.ForeColor = System.Drawing.Color.Blue
        Me.Label56.Location = New System.Drawing.Point(208, 28)
        Me.Label56.Margin = New System.Windows.Forms.Padding(0)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(103, 26)
        Me.Label56.TabIndex = 4
        Me.Label56.Text = "過去データ"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label57.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label57.Location = New System.Drawing.Point(1, 55)
        Me.Label57.Margin = New System.Windows.Forms.Padding(0)
        Me.Label57.Name = "Label57"
        Me.Label57.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label57.Size = New System.Drawing.Size(104, 24)
        Me.Label57.TabIndex = 5
        Me.Label57.Text = "0.00"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label58.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label58.Location = New System.Drawing.Point(1, 80)
        Me.Label58.Margin = New System.Windows.Forms.Padding(0)
        Me.Label58.Name = "Label58"
        Me.Label58.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label58.Size = New System.Drawing.Size(104, 24)
        Me.Label58.TabIndex = 6
        Me.Label58.Text = "11.25"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label59.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label59.Location = New System.Drawing.Point(1, 105)
        Me.Label59.Margin = New System.Windows.Forms.Padding(0)
        Me.Label59.Name = "Label59"
        Me.Label59.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label59.Size = New System.Drawing.Size(104, 24)
        Me.Label59.TabIndex = 7
        Me.Label59.Text = "22.50"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label64.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label64.Location = New System.Drawing.Point(1, 230)
        Me.Label64.Margin = New System.Windows.Forms.Padding(0)
        Me.Label64.Name = "Label64"
        Me.Label64.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label64.Size = New System.Drawing.Size(104, 24)
        Me.Label64.TabIndex = 12
        Me.Label64.Text = "78.75"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label66.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label66.Location = New System.Drawing.Point(1, 280)
        Me.Label66.Margin = New System.Windows.Forms.Padding(0)
        Me.Label66.Name = "Label66"
        Me.Label66.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label66.Size = New System.Drawing.Size(104, 24)
        Me.Label66.TabIndex = 14
        Me.Label66.Text = "101.25"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label60.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label60.Location = New System.Drawing.Point(1, 130)
        Me.Label60.Margin = New System.Windows.Forms.Padding(0)
        Me.Label60.Name = "Label60"
        Me.Label60.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label60.Size = New System.Drawing.Size(104, 24)
        Me.Label60.TabIndex = 8
        Me.Label60.Text = "33.75"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label61.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label61.Location = New System.Drawing.Point(1, 155)
        Me.Label61.Margin = New System.Windows.Forms.Padding(0)
        Me.Label61.Name = "Label61"
        Me.Label61.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label61.Size = New System.Drawing.Size(104, 24)
        Me.Label61.TabIndex = 9
        Me.Label61.Text = "45.00"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label62.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label62.Location = New System.Drawing.Point(1, 180)
        Me.Label62.Margin = New System.Windows.Forms.Padding(0)
        Me.Label62.Name = "Label62"
        Me.Label62.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label62.Size = New System.Drawing.Size(104, 24)
        Me.Label62.TabIndex = 10
        Me.Label62.Text = "56.25"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label63.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label63.Location = New System.Drawing.Point(1, 205)
        Me.Label63.Margin = New System.Windows.Forms.Padding(0)
        Me.Label63.Name = "Label63"
        Me.Label63.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label63.Size = New System.Drawing.Size(104, 24)
        Me.Label63.TabIndex = 11
        Me.Label63.Text = "67.50"
        Me.Label63.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label65.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label65.Location = New System.Drawing.Point(1, 255)
        Me.Label65.Margin = New System.Windows.Forms.Padding(0)
        Me.Label65.Name = "Label65"
        Me.Label65.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label65.Size = New System.Drawing.Size(104, 24)
        Me.Label65.TabIndex = 13
        Me.Label65.Text = "90.00"
        Me.Label65.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label67.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label67.Location = New System.Drawing.Point(1, 305)
        Me.Label67.Margin = New System.Windows.Forms.Padding(0)
        Me.Label67.Name = "Label67"
        Me.Label67.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label67.Size = New System.Drawing.Size(104, 24)
        Me.Label67.TabIndex = 15
        Me.Label67.Text = "112.50"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label68.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label68.Location = New System.Drawing.Point(1, 330)
        Me.Label68.Margin = New System.Windows.Forms.Padding(0)
        Me.Label68.Name = "Label68"
        Me.Label68.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label68.Size = New System.Drawing.Size(104, 24)
        Me.Label68.TabIndex = 16
        Me.Label68.Text = "123.75"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label69.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label69.Location = New System.Drawing.Point(1, 355)
        Me.Label69.Margin = New System.Windows.Forms.Padding(0)
        Me.Label69.Name = "Label69"
        Me.Label69.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label69.Size = New System.Drawing.Size(104, 24)
        Me.Label69.TabIndex = 17
        Me.Label69.Text = "135.00"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label70.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label70.Location = New System.Drawing.Point(1, 380)
        Me.Label70.Margin = New System.Windows.Forms.Padding(0)
        Me.Label70.Name = "Label70"
        Me.Label70.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label70.Size = New System.Drawing.Size(104, 24)
        Me.Label70.TabIndex = 18
        Me.Label70.Text = "146.25"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label71.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label71.Location = New System.Drawing.Point(1, 405)
        Me.Label71.Margin = New System.Windows.Forms.Padding(0)
        Me.Label71.Name = "Label71"
        Me.Label71.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label71.Size = New System.Drawing.Size(104, 24)
        Me.Label71.TabIndex = 19
        Me.Label71.Text = "157.50"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label72.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label72.Location = New System.Drawing.Point(1, 430)
        Me.Label72.Margin = New System.Windows.Forms.Padding(0)
        Me.Label72.Name = "Label72"
        Me.Label72.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label72.Size = New System.Drawing.Size(104, 24)
        Me.Label72.TabIndex = 20
        Me.Label72.Text = "168.75"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur16_adm
        '
        Me.LblMeasDatCur16_adm.AutoSize = True
        Me.LblMeasDatCur16_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur16_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur16_adm.Location = New System.Drawing.Point(106, 430)
        Me.LblMeasDatCur16_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur16_adm.Name = "LblMeasDatCur16_adm"
        Me.LblMeasDatCur16_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur16_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur16_adm.TabIndex = 36
        Me.LblMeasDatCur16_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur13_adm
        '
        Me.LblMeasDatCur13_adm.AutoSize = True
        Me.LblMeasDatCur13_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur13_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur13_adm.Location = New System.Drawing.Point(106, 355)
        Me.LblMeasDatCur13_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur13_adm.Name = "LblMeasDatCur13_adm"
        Me.LblMeasDatCur13_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur13_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur13_adm.TabIndex = 33
        Me.LblMeasDatCur13_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur12_adm
        '
        Me.LblMeasDatCur12_adm.AutoSize = True
        Me.LblMeasDatCur12_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur12_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur12_adm.Location = New System.Drawing.Point(106, 330)
        Me.LblMeasDatCur12_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur12_adm.Name = "LblMeasDatCur12_adm"
        Me.LblMeasDatCur12_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur12_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur12_adm.TabIndex = 32
        Me.LblMeasDatCur12_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur11_adm
        '
        Me.LblMeasDatCur11_adm.AutoSize = True
        Me.LblMeasDatCur11_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur11_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur11_adm.Location = New System.Drawing.Point(106, 305)
        Me.LblMeasDatCur11_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur11_adm.Name = "LblMeasDatCur11_adm"
        Me.LblMeasDatCur11_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur11_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur11_adm.TabIndex = 31
        Me.LblMeasDatCur11_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur10_adm
        '
        Me.LblMeasDatCur10_adm.AutoSize = True
        Me.LblMeasDatCur10_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur10_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur10_adm.Location = New System.Drawing.Point(106, 280)
        Me.LblMeasDatCur10_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur10_adm.Name = "LblMeasDatCur10_adm"
        Me.LblMeasDatCur10_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur10_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur10_adm.TabIndex = 30
        Me.LblMeasDatCur10_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur9_adm
        '
        Me.LblMeasDatCur9_adm.AutoSize = True
        Me.LblMeasDatCur9_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur9_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur9_adm.Location = New System.Drawing.Point(106, 255)
        Me.LblMeasDatCur9_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur9_adm.Name = "LblMeasDatCur9_adm"
        Me.LblMeasDatCur9_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur9_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur9_adm.TabIndex = 29
        Me.LblMeasDatCur9_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur7_adm
        '
        Me.LblMeasDatCur7_adm.AutoSize = True
        Me.LblMeasDatCur7_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur7_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur7_adm.Location = New System.Drawing.Point(106, 205)
        Me.LblMeasDatCur7_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur7_adm.Name = "LblMeasDatCur7_adm"
        Me.LblMeasDatCur7_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur7_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur7_adm.TabIndex = 27
        Me.LblMeasDatCur7_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur6_adm
        '
        Me.LblMeasDatCur6_adm.AutoSize = True
        Me.LblMeasDatCur6_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur6_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur6_adm.Location = New System.Drawing.Point(106, 180)
        Me.LblMeasDatCur6_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur6_adm.Name = "LblMeasDatCur6_adm"
        Me.LblMeasDatCur6_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur6_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur6_adm.TabIndex = 26
        Me.LblMeasDatCur6_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur5_adm
        '
        Me.LblMeasDatCur5_adm.AutoSize = True
        Me.LblMeasDatCur5_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur5_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur5_adm.Location = New System.Drawing.Point(106, 155)
        Me.LblMeasDatCur5_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur5_adm.Name = "LblMeasDatCur5_adm"
        Me.LblMeasDatCur5_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur5_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur5_adm.TabIndex = 25
        Me.LblMeasDatCur5_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur4_adm
        '
        Me.LblMeasDatCur4_adm.AutoSize = True
        Me.LblMeasDatCur4_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur4_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur4_adm.Location = New System.Drawing.Point(106, 130)
        Me.LblMeasDatCur4_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur4_adm.Name = "LblMeasDatCur4_adm"
        Me.LblMeasDatCur4_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur4_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur4_adm.TabIndex = 24
        Me.LblMeasDatCur4_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur8_adm
        '
        Me.LblMeasDatCur8_adm.AutoSize = True
        Me.LblMeasDatCur8_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur8_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur8_adm.Location = New System.Drawing.Point(106, 230)
        Me.LblMeasDatCur8_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur8_adm.Name = "LblMeasDatCur8_adm"
        Me.LblMeasDatCur8_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur8_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur8_adm.TabIndex = 28
        Me.LblMeasDatCur8_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur3_adm
        '
        Me.LblMeasDatCur3_adm.AutoSize = True
        Me.LblMeasDatCur3_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur3_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur3_adm.Location = New System.Drawing.Point(106, 105)
        Me.LblMeasDatCur3_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur3_adm.Name = "LblMeasDatCur3_adm"
        Me.LblMeasDatCur3_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur3_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur3_adm.TabIndex = 23
        Me.LblMeasDatCur3_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur2_adm
        '
        Me.LblMeasDatCur2_adm.AutoSize = True
        Me.LblMeasDatCur2_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur2_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur2_adm.Location = New System.Drawing.Point(106, 80)
        Me.LblMeasDatCur2_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur2_adm.Name = "LblMeasDatCur2_adm"
        Me.LblMeasDatCur2_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur2_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur2_adm.TabIndex = 22
        Me.LblMeasDatCur2_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur14_adm
        '
        Me.LblMeasDatCur14_adm.AutoSize = True
        Me.LblMeasDatCur14_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur14_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur14_adm.Location = New System.Drawing.Point(106, 380)
        Me.LblMeasDatCur14_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur14_adm.Name = "LblMeasDatCur14_adm"
        Me.LblMeasDatCur14_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur14_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur14_adm.TabIndex = 34
        Me.LblMeasDatCur14_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur15_adm
        '
        Me.LblMeasDatCur15_adm.AutoSize = True
        Me.LblMeasDatCur15_adm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur15_adm.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur15_adm.Location = New System.Drawing.Point(106, 405)
        Me.LblMeasDatCur15_adm.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur15_adm.Name = "LblMeasDatCur15_adm"
        Me.LblMeasDatCur15_adm.Padding = New System.Windows.Forms.Padding(0, 0, 15, 0)
        Me.LblMeasDatCur15_adm.Size = New System.Drawing.Size(101, 24)
        Me.LblMeasDatCur15_adm.TabIndex = 35
        Me.LblMeasDatCur15_adm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TblMeasInfo_nom
        '
        Me.TblMeasInfo_nom.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TblMeasInfo_nom.ColumnCount = 12
        Me.TblMeasInfo_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TblMeasInfo_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TblMeasInfo_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TblMeasInfo_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TblMeasInfo_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68.0!))
        Me.TblMeasInfo_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72.0!))
        Me.TblMeasInfo_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TblMeasInfo_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TblMeasInfo_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TblMeasInfo_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TblMeasInfo_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65.0!))
        Me.TblMeasInfo_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblMeasInfo_nom.Controls.Add(Me.Label87, 10, 0)
        Me.TblMeasInfo_nom.Controls.Add(Me.LblTSICDCur_nom, 11, 2)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label85, 8, 0)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label83, 6, 0)
        Me.TblMeasInfo_nom.Controls.Add(Me.LblTSIMDCur_nom, 10, 2)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label78, 4, 0)
        Me.TblMeasInfo_nom.Controls.Add(Me.LblSpdDeepCur_nom, 9, 2)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label76, 2, 0)
        Me.TblMeasInfo_nom.Controls.Add(Me.LblSpdPeakCur_nom, 8, 2)
        Me.TblMeasInfo_nom.Controls.Add(Me.LblSpdCDCur_nom, 7, 2)
        Me.TblMeasInfo_nom.Controls.Add(Me.LblSpdMDCur_nom, 6, 2)
        Me.TblMeasInfo_nom.Controls.Add(Me.LblratioPKDPCur_nom, 5, 2)
        Me.TblMeasInfo_nom.Controls.Add(Me.LblratioMDCDCur_nom, 4, 2)
        Me.TblMeasInfo_nom.Controls.Add(Me.LblAnglDeepCur_nom, 3, 2)
        Me.TblMeasInfo_nom.Controls.Add(Me.LblAnglPeakCur_nom, 2, 2)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label50, 11, 1)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label51, 10, 1)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label52, 9, 1)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label73, 8, 1)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label74, 0, 0)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label75, 1, 0)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label79, 2, 1)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label80, 3, 1)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label81, 4, 1)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label82, 5, 1)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label89, 6, 1)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label90, 7, 1)
        Me.TblMeasInfo_nom.Controls.Add(Me.Label91, 0, 2)
        Me.TblMeasInfo_nom.Controls.Add(Me.LblMeasNumCur_nom, 1, 2)
        Me.TblMeasInfo_nom.Location = New System.Drawing.Point(15, 135)
        Me.TblMeasInfo_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.TblMeasInfo_nom.Name = "TblMeasInfo_nom"
        Me.TblMeasInfo_nom.RowCount = 3
        Me.TblMeasInfo_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TblMeasInfo_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TblMeasInfo_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TblMeasInfo_nom.Size = New System.Drawing.Size(798, 89)
        Me.TblMeasInfo_nom.TabIndex = 23
        '
        'Label87
        '
        Me.Label87.AutoSize = True
        Me.TblMeasInfo_nom.SetColumnSpan(Me.Label87, 2)
        Me.Label87.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label87.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label87.Location = New System.Drawing.Point(666, 1)
        Me.Label87.Margin = New System.Windows.Forms.Padding(0)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(131, 35)
        Me.Label87.TabIndex = 0
        Me.Label87.Text = "TSI (Km/S)^2"
        Me.Label87.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblTSICDCur_nom
        '
        Me.LblTSICDCur_nom.AutoSize = True
        Me.LblTSICDCur_nom.BackColor = System.Drawing.SystemColors.Control
        Me.LblTSICDCur_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblTSICDCur_nom.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTSICDCur_nom.Location = New System.Drawing.Point(732, 63)
        Me.LblTSICDCur_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblTSICDCur_nom.Name = "LblTSICDCur_nom"
        Me.LblTSICDCur_nom.Size = New System.Drawing.Size(65, 25)
        Me.LblTSICDCur_nom.TabIndex = 29
        Me.LblTSICDCur_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label85
        '
        Me.Label85.AutoSize = True
        Me.TblMeasInfo_nom.SetColumnSpan(Me.Label85, 2)
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label85.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label85.Location = New System.Drawing.Point(534, 1)
        Me.Label85.Margin = New System.Windows.Forms.Padding(0)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(131, 35)
        Me.Label85.TabIndex = 0
        Me.Label85.Text = "伝播速度 [Km/S]"
        Me.Label85.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.TblMeasInfo_nom.SetColumnSpan(Me.Label83, 2)
        Me.Label83.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label83.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label83.Location = New System.Drawing.Point(402, 1)
        Me.Label83.Margin = New System.Windows.Forms.Padding(0)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(131, 35)
        Me.Label83.TabIndex = 0
        Me.Label83.Text = "伝播速度 [Km/S]"
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblTSIMDCur_nom
        '
        Me.LblTSIMDCur_nom.AutoSize = True
        Me.LblTSIMDCur_nom.BackColor = System.Drawing.Color.Transparent
        Me.LblTSIMDCur_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblTSIMDCur_nom.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTSIMDCur_nom.Location = New System.Drawing.Point(666, 63)
        Me.LblTSIMDCur_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblTSIMDCur_nom.Name = "LblTSIMDCur_nom"
        Me.LblTSIMDCur_nom.Size = New System.Drawing.Size(65, 25)
        Me.LblTSIMDCur_nom.TabIndex = 28
        Me.LblTSIMDCur_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.TblMeasInfo_nom.SetColumnSpan(Me.Label78, 2)
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label78.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label78.Location = New System.Drawing.Point(260, 1)
        Me.Label78.Margin = New System.Windows.Forms.Padding(0)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(141, 35)
        Me.Label78.TabIndex = 1
        Me.Label78.Text = "配向比"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSpdDeepCur_nom
        '
        Me.LblSpdDeepCur_nom.AutoSize = True
        Me.LblSpdDeepCur_nom.BackColor = System.Drawing.Color.Transparent
        Me.LblSpdDeepCur_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblSpdDeepCur_nom.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpdDeepCur_nom.Location = New System.Drawing.Point(600, 63)
        Me.LblSpdDeepCur_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblSpdDeepCur_nom.Name = "LblSpdDeepCur_nom"
        Me.LblSpdDeepCur_nom.Size = New System.Drawing.Size(65, 25)
        Me.LblSpdDeepCur_nom.TabIndex = 27
        Me.LblSpdDeepCur_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.TblMeasInfo_nom.SetColumnSpan(Me.Label76, 2)
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label76.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label76.Location = New System.Drawing.Point(118, 1)
        Me.Label76.Margin = New System.Windows.Forms.Padding(0)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(141, 35)
        Me.Label76.TabIndex = 0
        Me.Label76.Text = "配向角 [deg.]"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSpdPeakCur_nom
        '
        Me.LblSpdPeakCur_nom.AutoSize = True
        Me.LblSpdPeakCur_nom.BackColor = System.Drawing.Color.Transparent
        Me.LblSpdPeakCur_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblSpdPeakCur_nom.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpdPeakCur_nom.Location = New System.Drawing.Point(534, 63)
        Me.LblSpdPeakCur_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblSpdPeakCur_nom.Name = "LblSpdPeakCur_nom"
        Me.LblSpdPeakCur_nom.Size = New System.Drawing.Size(65, 25)
        Me.LblSpdPeakCur_nom.TabIndex = 26
        Me.LblSpdPeakCur_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSpdCDCur_nom
        '
        Me.LblSpdCDCur_nom.AutoSize = True
        Me.LblSpdCDCur_nom.BackColor = System.Drawing.Color.Transparent
        Me.LblSpdCDCur_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblSpdCDCur_nom.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpdCDCur_nom.Location = New System.Drawing.Point(468, 63)
        Me.LblSpdCDCur_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblSpdCDCur_nom.Name = "LblSpdCDCur_nom"
        Me.LblSpdCDCur_nom.Size = New System.Drawing.Size(65, 25)
        Me.LblSpdCDCur_nom.TabIndex = 25
        Me.LblSpdCDCur_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblSpdMDCur_nom
        '
        Me.LblSpdMDCur_nom.AutoSize = True
        Me.LblSpdMDCur_nom.BackColor = System.Drawing.Color.Transparent
        Me.LblSpdMDCur_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblSpdMDCur_nom.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblSpdMDCur_nom.Location = New System.Drawing.Point(402, 63)
        Me.LblSpdMDCur_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblSpdMDCur_nom.Name = "LblSpdMDCur_nom"
        Me.LblSpdMDCur_nom.Size = New System.Drawing.Size(65, 25)
        Me.LblSpdMDCur_nom.TabIndex = 24
        Me.LblSpdMDCur_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblratioPKDPCur_nom
        '
        Me.LblratioPKDPCur_nom.AutoSize = True
        Me.LblratioPKDPCur_nom.BackColor = System.Drawing.Color.Transparent
        Me.LblratioPKDPCur_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblratioPKDPCur_nom.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblratioPKDPCur_nom.Location = New System.Drawing.Point(329, 63)
        Me.LblratioPKDPCur_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblratioPKDPCur_nom.Name = "LblratioPKDPCur_nom"
        Me.LblratioPKDPCur_nom.Size = New System.Drawing.Size(72, 25)
        Me.LblratioPKDPCur_nom.TabIndex = 23
        Me.LblratioPKDPCur_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblratioMDCDCur_nom
        '
        Me.LblratioMDCDCur_nom.AutoSize = True
        Me.LblratioMDCDCur_nom.BackColor = System.Drawing.Color.Transparent
        Me.LblratioMDCDCur_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblratioMDCDCur_nom.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblratioMDCDCur_nom.Location = New System.Drawing.Point(260, 63)
        Me.LblratioMDCDCur_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblratioMDCDCur_nom.Name = "LblratioMDCDCur_nom"
        Me.LblratioMDCDCur_nom.Size = New System.Drawing.Size(68, 25)
        Me.LblratioMDCDCur_nom.TabIndex = 22
        Me.LblratioMDCDCur_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblAnglDeepCur_nom
        '
        Me.LblAnglDeepCur_nom.AutoSize = True
        Me.LblAnglDeepCur_nom.BackColor = System.Drawing.Color.Transparent
        Me.LblAnglDeepCur_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblAnglDeepCur_nom.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAnglDeepCur_nom.Location = New System.Drawing.Point(189, 63)
        Me.LblAnglDeepCur_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblAnglDeepCur_nom.Name = "LblAnglDeepCur_nom"
        Me.LblAnglDeepCur_nom.Size = New System.Drawing.Size(70, 25)
        Me.LblAnglDeepCur_nom.TabIndex = 21
        Me.LblAnglDeepCur_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblAnglPeakCur_nom
        '
        Me.LblAnglPeakCur_nom.AutoSize = True
        Me.LblAnglPeakCur_nom.BackColor = System.Drawing.Color.Transparent
        Me.LblAnglPeakCur_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblAnglPeakCur_nom.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblAnglPeakCur_nom.Location = New System.Drawing.Point(118, 63)
        Me.LblAnglPeakCur_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblAnglPeakCur_nom.Name = "LblAnglPeakCur_nom"
        Me.LblAnglPeakCur_nom.Size = New System.Drawing.Size(70, 25)
        Me.LblAnglPeakCur_nom.TabIndex = 20
        Me.LblAnglPeakCur_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.SystemColors.Control
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label50.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label50.Location = New System.Drawing.Point(732, 37)
        Me.Label50.Margin = New System.Windows.Forms.Padding(0)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(65, 25)
        Me.Label50.TabIndex = 16
        Me.Label50.Text = "CD"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label51.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label51.Location = New System.Drawing.Point(666, 37)
        Me.Label51.Margin = New System.Windows.Forms.Padding(0)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(65, 25)
        Me.Label51.TabIndex = 15
        Me.Label51.Text = "MD"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label52.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label52.Location = New System.Drawing.Point(600, 37)
        Me.Label52.Margin = New System.Windows.Forms.Padding(0)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(65, 25)
        Me.Label52.TabIndex = 14
        Me.Label52.Text = "Deep"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label73.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label73.Location = New System.Drawing.Point(534, 37)
        Me.Label73.Margin = New System.Windows.Forms.Padding(0)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(65, 25)
        Me.Label73.TabIndex = 13
        Me.Label73.Text = "Peak"
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.BackColor = System.Drawing.SystemColors.Control
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label74.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label74.Location = New System.Drawing.Point(1, 1)
        Me.Label74.Margin = New System.Windows.Forms.Padding(0)
        Me.Label74.Name = "Label74"
        Me.TblMeasInfo_nom.SetRowSpan(Me.Label74, 2)
        Me.Label74.Size = New System.Drawing.Size(70, 61)
        Me.Label74.TabIndex = 0
        Me.Label74.Text = "データ"
        Me.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label75.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label75.Location = New System.Drawing.Point(72, 1)
        Me.Label75.Margin = New System.Windows.Forms.Padding(0)
        Me.Label75.Name = "Label75"
        Me.TblMeasInfo_nom.SetRowSpan(Me.Label75, 2)
        Me.Label75.Size = New System.Drawing.Size(45, 61)
        Me.Label75.TabIndex = 1
        Me.Label75.Text = "測定" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "回数"
        Me.Label75.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label79.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label79.Location = New System.Drawing.Point(118, 37)
        Me.Label79.Margin = New System.Windows.Forms.Padding(0)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(70, 25)
        Me.Label79.TabIndex = 4
        Me.Label79.Text = "Peak"
        Me.Label79.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label80.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label80.Location = New System.Drawing.Point(189, 37)
        Me.Label80.Margin = New System.Windows.Forms.Padding(0)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(70, 25)
        Me.Label80.TabIndex = 5
        Me.Label80.Text = "Deep"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label81.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label81.Location = New System.Drawing.Point(260, 37)
        Me.Label81.Margin = New System.Windows.Forms.Padding(0)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(68, 25)
        Me.Label81.TabIndex = 6
        Me.Label81.Text = "MD/CD"
        Me.Label81.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label82.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label82.Location = New System.Drawing.Point(329, 37)
        Me.Label82.Margin = New System.Windows.Forms.Padding(0)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(72, 25)
        Me.Label82.TabIndex = 7
        Me.Label82.Text = "Peak/Deep"
        Me.Label82.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label89
        '
        Me.Label89.AutoSize = True
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label89.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label89.Location = New System.Drawing.Point(402, 37)
        Me.Label89.Margin = New System.Windows.Forms.Padding(0)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(65, 25)
        Me.Label89.TabIndex = 11
        Me.Label89.Text = "MD"
        Me.Label89.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label90
        '
        Me.Label90.AutoSize = True
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label90.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label90.Location = New System.Drawing.Point(468, 37)
        Me.Label90.Margin = New System.Windows.Forms.Padding(0)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(65, 25)
        Me.Label90.TabIndex = 12
        Me.Label90.Text = "CD"
        Me.Label90.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label91
        '
        Me.Label91.AutoSize = True
        Me.Label91.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label91.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label91.Location = New System.Drawing.Point(1, 63)
        Me.Label91.Margin = New System.Windows.Forms.Padding(0)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(70, 25)
        Me.Label91.TabIndex = 17
        Me.Label91.Text = "測定データ"
        Me.Label91.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasNumCur_nom
        '
        Me.LblMeasNumCur_nom.AutoSize = True
        Me.LblMeasNumCur_nom.BackColor = System.Drawing.Color.Transparent
        Me.LblMeasNumCur_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasNumCur_nom.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasNumCur_nom.Location = New System.Drawing.Point(72, 63)
        Me.LblMeasNumCur_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasNumCur_nom.Name = "LblMeasNumCur_nom"
        Me.LblMeasNumCur_nom.Size = New System.Drawing.Size(45, 25)
        Me.LblMeasNumCur_nom.TabIndex = 19
        Me.LblMeasNumCur_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TblMeasData_nom
        '
        Me.TblMeasData_nom.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TblMeasData_nom.ColumnCount = 2
        Me.TblMeasData_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106.0!))
        Me.TblMeasData_nom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur1_nom, 1, 2)
        Me.TblMeasData_nom.Controls.Add(Me.Label203, 0, 0)
        Me.TblMeasData_nom.Controls.Add(Me.Label204, 1, 0)
        Me.TblMeasData_nom.Controls.Add(Me.Label205, 1, 1)
        Me.TblMeasData_nom.Controls.Add(Me.Label207, 0, 2)
        Me.TblMeasData_nom.Controls.Add(Me.Label208, 0, 3)
        Me.TblMeasData_nom.Controls.Add(Me.Label209, 0, 4)
        Me.TblMeasData_nom.Controls.Add(Me.Label210, 0, 9)
        Me.TblMeasData_nom.Controls.Add(Me.Label211, 0, 11)
        Me.TblMeasData_nom.Controls.Add(Me.Label212, 0, 5)
        Me.TblMeasData_nom.Controls.Add(Me.Label213, 0, 6)
        Me.TblMeasData_nom.Controls.Add(Me.Label214, 0, 7)
        Me.TblMeasData_nom.Controls.Add(Me.Label215, 0, 8)
        Me.TblMeasData_nom.Controls.Add(Me.Label216, 0, 10)
        Me.TblMeasData_nom.Controls.Add(Me.Label217, 0, 12)
        Me.TblMeasData_nom.Controls.Add(Me.Label218, 0, 13)
        Me.TblMeasData_nom.Controls.Add(Me.Label219, 0, 14)
        Me.TblMeasData_nom.Controls.Add(Me.Label220, 0, 15)
        Me.TblMeasData_nom.Controls.Add(Me.Label221, 0, 16)
        Me.TblMeasData_nom.Controls.Add(Me.Label222, 0, 17)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur16_nom, 1, 17)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur13_nom, 1, 14)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur12_nom, 1, 13)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur11_nom, 1, 12)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur10_nom, 1, 11)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur9_nom, 1, 10)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur7_nom, 1, 8)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur6_nom, 1, 7)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur5_nom, 1, 6)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur4_nom, 1, 5)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur8_nom, 1, 9)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur3_nom, 1, 4)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur2_nom, 1, 3)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur14_nom, 1, 15)
        Me.TblMeasData_nom.Controls.Add(Me.LblMeasDatCur15_nom, 1, 16)
        Me.TblMeasData_nom.Location = New System.Drawing.Point(505, 271)
        Me.TblMeasData_nom.Name = "TblMeasData_nom"
        Me.TblMeasData_nom.RowCount = 18
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TblMeasData_nom.Size = New System.Drawing.Size(312, 455)
        Me.TblMeasData_nom.TabIndex = 24
        '
        'LblMeasDatCur1_nom
        '
        Me.LblMeasDatCur1_nom.AutoSize = True
        Me.LblMeasDatCur1_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur1_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur1_nom.Location = New System.Drawing.Point(108, 55)
        Me.LblMeasDatCur1_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur1_nom.Name = "LblMeasDatCur1_nom"
        Me.LblMeasDatCur1_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur1_nom.TabIndex = 21
        Me.LblMeasDatCur1_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label203
        '
        Me.Label203.AutoSize = True
        Me.Label203.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label203.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label203.Location = New System.Drawing.Point(1, 1)
        Me.Label203.Margin = New System.Windows.Forms.Padding(0)
        Me.Label203.Name = "Label203"
        Me.Label203.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.TblMeasData_nom.SetRowSpan(Me.Label203, 2)
        Me.Label203.Size = New System.Drawing.Size(106, 53)
        Me.Label203.TabIndex = 1
        Me.Label203.Text = "角度  [deg.]"
        Me.Label203.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label204
        '
        Me.Label204.AutoSize = True
        Me.Label204.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label204.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label204.Location = New System.Drawing.Point(108, 1)
        Me.Label204.Margin = New System.Windows.Forms.Padding(0)
        Me.Label204.Name = "Label204"
        Me.Label204.Size = New System.Drawing.Size(203, 26)
        Me.Label204.TabIndex = 2
        Me.Label204.Text = "伝播速度　[Km/S]"
        Me.Label204.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label205
        '
        Me.Label205.AutoSize = True
        Me.Label205.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label205.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label205.Location = New System.Drawing.Point(108, 28)
        Me.Label205.Margin = New System.Windows.Forms.Padding(0)
        Me.Label205.Name = "Label205"
        Me.Label205.Size = New System.Drawing.Size(203, 26)
        Me.Label205.TabIndex = 3
        Me.Label205.Text = "測定データ"
        Me.Label205.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label207
        '
        Me.Label207.AutoSize = True
        Me.Label207.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label207.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label207.Location = New System.Drawing.Point(1, 55)
        Me.Label207.Margin = New System.Windows.Forms.Padding(0)
        Me.Label207.Name = "Label207"
        Me.Label207.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label207.Size = New System.Drawing.Size(106, 24)
        Me.Label207.TabIndex = 5
        Me.Label207.Text = "0.00"
        Me.Label207.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label208
        '
        Me.Label208.AutoSize = True
        Me.Label208.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label208.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label208.Location = New System.Drawing.Point(1, 80)
        Me.Label208.Margin = New System.Windows.Forms.Padding(0)
        Me.Label208.Name = "Label208"
        Me.Label208.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label208.Size = New System.Drawing.Size(106, 24)
        Me.Label208.TabIndex = 6
        Me.Label208.Text = "11.25"
        Me.Label208.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label209
        '
        Me.Label209.AutoSize = True
        Me.Label209.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label209.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label209.Location = New System.Drawing.Point(1, 105)
        Me.Label209.Margin = New System.Windows.Forms.Padding(0)
        Me.Label209.Name = "Label209"
        Me.Label209.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label209.Size = New System.Drawing.Size(106, 24)
        Me.Label209.TabIndex = 7
        Me.Label209.Text = "22.50"
        Me.Label209.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label210
        '
        Me.Label210.AutoSize = True
        Me.Label210.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label210.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label210.Location = New System.Drawing.Point(1, 230)
        Me.Label210.Margin = New System.Windows.Forms.Padding(0)
        Me.Label210.Name = "Label210"
        Me.Label210.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label210.Size = New System.Drawing.Size(106, 24)
        Me.Label210.TabIndex = 12
        Me.Label210.Text = "78.75"
        Me.Label210.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label211
        '
        Me.Label211.AutoSize = True
        Me.Label211.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label211.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label211.Location = New System.Drawing.Point(1, 280)
        Me.Label211.Margin = New System.Windows.Forms.Padding(0)
        Me.Label211.Name = "Label211"
        Me.Label211.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label211.Size = New System.Drawing.Size(106, 24)
        Me.Label211.TabIndex = 14
        Me.Label211.Text = "101.25"
        Me.Label211.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label212
        '
        Me.Label212.AutoSize = True
        Me.Label212.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label212.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label212.Location = New System.Drawing.Point(1, 130)
        Me.Label212.Margin = New System.Windows.Forms.Padding(0)
        Me.Label212.Name = "Label212"
        Me.Label212.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label212.Size = New System.Drawing.Size(106, 24)
        Me.Label212.TabIndex = 8
        Me.Label212.Text = "33.75"
        Me.Label212.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label213
        '
        Me.Label213.AutoSize = True
        Me.Label213.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label213.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label213.Location = New System.Drawing.Point(1, 155)
        Me.Label213.Margin = New System.Windows.Forms.Padding(0)
        Me.Label213.Name = "Label213"
        Me.Label213.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label213.Size = New System.Drawing.Size(106, 24)
        Me.Label213.TabIndex = 9
        Me.Label213.Text = "45.00"
        Me.Label213.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label214
        '
        Me.Label214.AutoSize = True
        Me.Label214.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label214.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label214.Location = New System.Drawing.Point(1, 180)
        Me.Label214.Margin = New System.Windows.Forms.Padding(0)
        Me.Label214.Name = "Label214"
        Me.Label214.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label214.Size = New System.Drawing.Size(106, 24)
        Me.Label214.TabIndex = 10
        Me.Label214.Text = "56.25"
        Me.Label214.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label215
        '
        Me.Label215.AutoSize = True
        Me.Label215.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label215.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label215.Location = New System.Drawing.Point(1, 205)
        Me.Label215.Margin = New System.Windows.Forms.Padding(0)
        Me.Label215.Name = "Label215"
        Me.Label215.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label215.Size = New System.Drawing.Size(106, 24)
        Me.Label215.TabIndex = 11
        Me.Label215.Text = "67.50"
        Me.Label215.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label216
        '
        Me.Label216.AutoSize = True
        Me.Label216.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label216.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label216.Location = New System.Drawing.Point(1, 255)
        Me.Label216.Margin = New System.Windows.Forms.Padding(0)
        Me.Label216.Name = "Label216"
        Me.Label216.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label216.Size = New System.Drawing.Size(106, 24)
        Me.Label216.TabIndex = 13
        Me.Label216.Text = "90.00"
        Me.Label216.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label217
        '
        Me.Label217.AutoSize = True
        Me.Label217.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label217.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label217.Location = New System.Drawing.Point(1, 305)
        Me.Label217.Margin = New System.Windows.Forms.Padding(0)
        Me.Label217.Name = "Label217"
        Me.Label217.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label217.Size = New System.Drawing.Size(106, 24)
        Me.Label217.TabIndex = 15
        Me.Label217.Text = "112.50"
        Me.Label217.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label218
        '
        Me.Label218.AutoSize = True
        Me.Label218.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label218.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label218.Location = New System.Drawing.Point(1, 330)
        Me.Label218.Margin = New System.Windows.Forms.Padding(0)
        Me.Label218.Name = "Label218"
        Me.Label218.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label218.Size = New System.Drawing.Size(106, 24)
        Me.Label218.TabIndex = 16
        Me.Label218.Text = "123.75"
        Me.Label218.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label219
        '
        Me.Label219.AutoSize = True
        Me.Label219.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label219.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label219.Location = New System.Drawing.Point(1, 355)
        Me.Label219.Margin = New System.Windows.Forms.Padding(0)
        Me.Label219.Name = "Label219"
        Me.Label219.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label219.Size = New System.Drawing.Size(106, 24)
        Me.Label219.TabIndex = 17
        Me.Label219.Text = "135.00"
        Me.Label219.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label220
        '
        Me.Label220.AutoSize = True
        Me.Label220.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label220.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label220.Location = New System.Drawing.Point(1, 380)
        Me.Label220.Margin = New System.Windows.Forms.Padding(0)
        Me.Label220.Name = "Label220"
        Me.Label220.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label220.Size = New System.Drawing.Size(106, 24)
        Me.Label220.TabIndex = 18
        Me.Label220.Text = "146.25"
        Me.Label220.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label221
        '
        Me.Label221.AutoSize = True
        Me.Label221.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label221.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label221.Location = New System.Drawing.Point(1, 405)
        Me.Label221.Margin = New System.Windows.Forms.Padding(0)
        Me.Label221.Name = "Label221"
        Me.Label221.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label221.Size = New System.Drawing.Size(106, 24)
        Me.Label221.TabIndex = 19
        Me.Label221.Text = "157.50"
        Me.Label221.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label222
        '
        Me.Label222.AutoSize = True
        Me.Label222.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label222.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label222.Location = New System.Drawing.Point(1, 430)
        Me.Label222.Margin = New System.Windows.Forms.Padding(0)
        Me.Label222.Name = "Label222"
        Me.Label222.Padding = New System.Windows.Forms.Padding(0, 0, 30, 0)
        Me.Label222.Size = New System.Drawing.Size(106, 24)
        Me.Label222.TabIndex = 20
        Me.Label222.Text = "168.75"
        Me.Label222.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMeasDatCur16_nom
        '
        Me.LblMeasDatCur16_nom.AutoSize = True
        Me.LblMeasDatCur16_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur16_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur16_nom.Location = New System.Drawing.Point(108, 430)
        Me.LblMeasDatCur16_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur16_nom.Name = "LblMeasDatCur16_nom"
        Me.LblMeasDatCur16_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur16_nom.TabIndex = 36
        Me.LblMeasDatCur16_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur13_nom
        '
        Me.LblMeasDatCur13_nom.AutoSize = True
        Me.LblMeasDatCur13_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur13_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur13_nom.Location = New System.Drawing.Point(108, 355)
        Me.LblMeasDatCur13_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur13_nom.Name = "LblMeasDatCur13_nom"
        Me.LblMeasDatCur13_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur13_nom.TabIndex = 33
        Me.LblMeasDatCur13_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur12_nom
        '
        Me.LblMeasDatCur12_nom.AutoSize = True
        Me.LblMeasDatCur12_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur12_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur12_nom.Location = New System.Drawing.Point(108, 330)
        Me.LblMeasDatCur12_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur12_nom.Name = "LblMeasDatCur12_nom"
        Me.LblMeasDatCur12_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur12_nom.TabIndex = 32
        Me.LblMeasDatCur12_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur11_nom
        '
        Me.LblMeasDatCur11_nom.AutoSize = True
        Me.LblMeasDatCur11_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur11_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur11_nom.Location = New System.Drawing.Point(108, 305)
        Me.LblMeasDatCur11_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur11_nom.Name = "LblMeasDatCur11_nom"
        Me.LblMeasDatCur11_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur11_nom.TabIndex = 31
        Me.LblMeasDatCur11_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur10_nom
        '
        Me.LblMeasDatCur10_nom.AutoSize = True
        Me.LblMeasDatCur10_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur10_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur10_nom.Location = New System.Drawing.Point(108, 280)
        Me.LblMeasDatCur10_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur10_nom.Name = "LblMeasDatCur10_nom"
        Me.LblMeasDatCur10_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur10_nom.TabIndex = 30
        Me.LblMeasDatCur10_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur9_nom
        '
        Me.LblMeasDatCur9_nom.AutoSize = True
        Me.LblMeasDatCur9_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur9_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur9_nom.Location = New System.Drawing.Point(108, 255)
        Me.LblMeasDatCur9_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur9_nom.Name = "LblMeasDatCur9_nom"
        Me.LblMeasDatCur9_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur9_nom.TabIndex = 29
        Me.LblMeasDatCur9_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur7_nom
        '
        Me.LblMeasDatCur7_nom.AutoSize = True
        Me.LblMeasDatCur7_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur7_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur7_nom.Location = New System.Drawing.Point(108, 205)
        Me.LblMeasDatCur7_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur7_nom.Name = "LblMeasDatCur7_nom"
        Me.LblMeasDatCur7_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur7_nom.TabIndex = 27
        Me.LblMeasDatCur7_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur6_nom
        '
        Me.LblMeasDatCur6_nom.AutoSize = True
        Me.LblMeasDatCur6_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur6_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur6_nom.Location = New System.Drawing.Point(108, 180)
        Me.LblMeasDatCur6_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur6_nom.Name = "LblMeasDatCur6_nom"
        Me.LblMeasDatCur6_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur6_nom.TabIndex = 26
        Me.LblMeasDatCur6_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur5_nom
        '
        Me.LblMeasDatCur5_nom.AutoSize = True
        Me.LblMeasDatCur5_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur5_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur5_nom.Location = New System.Drawing.Point(108, 155)
        Me.LblMeasDatCur5_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur5_nom.Name = "LblMeasDatCur5_nom"
        Me.LblMeasDatCur5_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur5_nom.TabIndex = 25
        Me.LblMeasDatCur5_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur4_nom
        '
        Me.LblMeasDatCur4_nom.AutoSize = True
        Me.LblMeasDatCur4_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur4_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur4_nom.Location = New System.Drawing.Point(108, 130)
        Me.LblMeasDatCur4_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur4_nom.Name = "LblMeasDatCur4_nom"
        Me.LblMeasDatCur4_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur4_nom.TabIndex = 24
        Me.LblMeasDatCur4_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur8_nom
        '
        Me.LblMeasDatCur8_nom.AutoSize = True
        Me.LblMeasDatCur8_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur8_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur8_nom.Location = New System.Drawing.Point(108, 230)
        Me.LblMeasDatCur8_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur8_nom.Name = "LblMeasDatCur8_nom"
        Me.LblMeasDatCur8_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur8_nom.TabIndex = 28
        Me.LblMeasDatCur8_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur3_nom
        '
        Me.LblMeasDatCur3_nom.AutoSize = True
        Me.LblMeasDatCur3_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur3_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur3_nom.Location = New System.Drawing.Point(108, 105)
        Me.LblMeasDatCur3_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur3_nom.Name = "LblMeasDatCur3_nom"
        Me.LblMeasDatCur3_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur3_nom.TabIndex = 23
        Me.LblMeasDatCur3_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur2_nom
        '
        Me.LblMeasDatCur2_nom.AutoSize = True
        Me.LblMeasDatCur2_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur2_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur2_nom.Location = New System.Drawing.Point(108, 80)
        Me.LblMeasDatCur2_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur2_nom.Name = "LblMeasDatCur2_nom"
        Me.LblMeasDatCur2_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur2_nom.TabIndex = 22
        Me.LblMeasDatCur2_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur14_nom
        '
        Me.LblMeasDatCur14_nom.AutoSize = True
        Me.LblMeasDatCur14_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur14_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur14_nom.Location = New System.Drawing.Point(108, 380)
        Me.LblMeasDatCur14_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur14_nom.Name = "LblMeasDatCur14_nom"
        Me.LblMeasDatCur14_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur14_nom.TabIndex = 34
        Me.LblMeasDatCur14_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMeasDatCur15_nom
        '
        Me.LblMeasDatCur15_nom.AutoSize = True
        Me.LblMeasDatCur15_nom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LblMeasDatCur15_nom.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblMeasDatCur15_nom.Location = New System.Drawing.Point(108, 405)
        Me.LblMeasDatCur15_nom.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMeasDatCur15_nom.Name = "LblMeasDatCur15_nom"
        Me.LblMeasDatCur15_nom.Size = New System.Drawing.Size(203, 24)
        Me.LblMeasDatCur15_nom.TabIndex = 35
        Me.LblMeasDatCur15_nom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GbPrint
        '
        Me.GbPrint.Controls.Add(Me.GroupBox2)
        Me.GbPrint.Controls.Add(Me.GroupBox1)
        Me.GbPrint.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GbPrint.Location = New System.Drawing.Point(852, 563)
        Me.GbPrint.Name = "GbPrint"
        Me.GbPrint.Size = New System.Drawing.Size(121, 160)
        Me.GbPrint.TabIndex = 25
        Me.GbPrint.TabStop = False
        Me.GbPrint.Text = "測定結果"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CmdMeasResultSave)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 99)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(109, 55)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Excel保存"
        '
        'CmdMeasResultSave
        '
        Me.CmdMeasResultSave.Location = New System.Drawing.Point(10, 21)
        Me.CmdMeasResultSave.Name = "CmdMeasResultSave"
        Me.CmdMeasResultSave.Size = New System.Drawing.Size(90, 25)
        Me.CmdMeasResultSave.TabIndex = 28
        Me.CmdMeasResultSave.Text = "保　存"
        Me.CmdMeasResultSave.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CmdMeasPrint)
        Me.GroupBox1.Controls.Add(Me.ChkMeasAutoPrn)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 18)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(109, 75)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "印 刷"
        '
        'CmdMeasPrint
        '
        Me.CmdMeasPrint.Location = New System.Drawing.Point(10, 40)
        Me.CmdMeasPrint.Name = "CmdMeasPrint"
        Me.CmdMeasPrint.Size = New System.Drawing.Size(90, 25)
        Me.CmdMeasPrint.TabIndex = 1
        Me.CmdMeasPrint.Text = "手動印刷"
        Me.CmdMeasPrint.UseVisualStyleBackColor = True
        '
        'ChkMeasAutoPrn
        '
        Me.ChkMeasAutoPrn.AutoSize = True
        Me.ChkMeasAutoPrn.Location = New System.Drawing.Point(12, 18)
        Me.ChkMeasAutoPrn.Name = "ChkMeasAutoPrn"
        Me.ChkMeasAutoPrn.Size = New System.Drawing.Size(59, 16)
        Me.ChkMeasAutoPrn.TabIndex = 0
        Me.ChkMeasAutoPrn.Text = "自　動"
        Me.ChkMeasAutoPrn.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel3, Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel5, Me.ToolStripStatusLabel4})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 740)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(984, 22)
        Me.StatusStrip1.TabIndex = 27
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
        'ToolStripStatusLabel5
        '
        Me.ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Size = New System.Drawing.Size(37, 17)
        Me.ToolStripStatusLabel5.Text = "特殊1"
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel4.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.ToolStripStatusLabel4.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(713, 17)
        Me.ToolStripStatusLabel4.Spring = True
        Me.ToolStripStatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PrintDocument_adm
        '
        '
        'PPD_amd
        '
        Me.PPD_amd.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PPD_amd.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PPD_amd.ClientSize = New System.Drawing.Size(400, 300)
        Me.PPD_amd.Document = Me.PrintDocument_adm
        Me.PPD_amd.Enabled = True
        Me.PPD_amd.Icon = CType(resources.GetObject("PPD_amd.Icon"), System.Drawing.Icon)
        Me.PPD_amd.Name = "PrintPreviewDialog1"
        Me.PPD_amd.Visible = False
        '
        'PPD_nom
        '
        Me.PPD_nom.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PPD_nom.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PPD_nom.ClientSize = New System.Drawing.Size(400, 300)
        Me.PPD_nom.Document = Me.PrintDocument_nom
        Me.PPD_nom.Enabled = True
        Me.PPD_nom.Icon = CType(resources.GetObject("PPD_nom.Icon"), System.Drawing.Icon)
        Me.PPD_nom.Name = "PPD_nom"
        Me.PPD_nom.Visible = False
        '
        'PrintDocument_nom
        '
        '
        'GbMeasSpec
        '
        Me.GbMeasSpec.Controls.Add(Me.CmdMeasSpecSel)
        Me.GbMeasSpec.Controls.Add(Me.CmdMeasSpecSave)
        Me.GbMeasSpec.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GbMeasSpec.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GbMeasSpec.Location = New System.Drawing.Point(839, 135)
        Me.GbMeasSpec.Name = "GbMeasSpec"
        Me.GbMeasSpec.Size = New System.Drawing.Size(134, 100)
        Me.GbMeasSpec.TabIndex = 48
        Me.GbMeasSpec.TabStop = False
        Me.GbMeasSpec.Text = "測定仕様"
        '
        'CmdMeasSpecSel
        '
        Me.CmdMeasSpecSel.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdMeasSpecSel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdMeasSpecSel.Location = New System.Drawing.Point(8, 17)
        Me.CmdMeasSpecSel.Name = "CmdMeasSpecSel"
        Me.CmdMeasSpecSel.Size = New System.Drawing.Size(120, 35)
        Me.CmdMeasSpecSel.TabIndex = 26
        Me.CmdMeasSpecSel.Text = "選　択"
        Me.CmdMeasSpecSel.UseVisualStyleBackColor = True
        '
        'CmdMeasSpecSave
        '
        Me.CmdMeasSpecSave.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdMeasSpecSave.Location = New System.Drawing.Point(8, 58)
        Me.CmdMeasSpecSave.Name = "CmdMeasSpecSave"
        Me.CmdMeasSpecSave.Size = New System.Drawing.Size(120, 35)
        Me.CmdMeasSpecSave.TabIndex = 27
        Me.CmdMeasSpecSave.Text = "保　存"
        Me.CmdMeasSpecSave.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.CmdOldDataLoad)
        Me.GroupBox5.Controls.Add(Me.CmdEtcOldMeasData)
        Me.GroupBox5.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(839, 342)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(134, 100)
        Me.GroupBox5.TabIndex = 50
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "過去データ"
        '
        'TxtMeasNumCur
        '
        Me.TxtMeasNumCur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TxtMeasNumCur.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtMeasNumCur.ForeColor = System.Drawing.Color.Black
        Me.TxtMeasNumCur.Location = New System.Drawing.Point(543, 83)
        Me.TxtMeasNumCur.Margin = New System.Windows.Forms.Padding(0)
        Me.TxtMeasNumCur.Name = "TxtMeasNumCur"
        Me.TxtMeasNumCur.Size = New System.Drawing.Size(55, 22)
        Me.TxtMeasNumCur.TabIndex = 51
        Me.TxtMeasNumCur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtMeasNumBak
        '
        Me.TxtMeasNumBak.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TxtMeasNumBak.Font = New System.Drawing.Font("MS UI Gothic", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TxtMeasNumBak.ForeColor = System.Drawing.Color.Blue
        Me.TxtMeasNumBak.Location = New System.Drawing.Point(543, 109)
        Me.TxtMeasNumBak.Margin = New System.Windows.Forms.Padding(0)
        Me.TxtMeasNumBak.Name = "TxtMeasNumBak"
        Me.TxtMeasNumBak.Size = New System.Drawing.Size(55, 22)
        Me.TxtMeasNumBak.TabIndex = 52
        Me.TxtMeasNumBak.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.MenuStrip1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ファイルToolStripMenuItem, Me.測定ToolStripMenuItem, Me.結果ToolStripMenuItem, Me.設定ToolStripMenuItem, Me.ヘルプToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(984, 24)
        Me.MenuStrip1.TabIndex = 53
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ファイルToolStripMenuItem
        '
        Me.ファイルToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.測定仕様ToolStripMenuItem, Me.過去データToolStripMenuItem, Me.終了ToolStripMenuItem})
        Me.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem"
        Me.ファイルToolStripMenuItem.Size = New System.Drawing.Size(51, 20)
        Me.ファイルToolStripMenuItem.Text = "ファイル"
        '
        '測定仕様ToolStripMenuItem
        '
        Me.測定仕様ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.選択ToolStripMenuItem, Me.保存ToolStripMenuItem})
        Me.測定仕様ToolStripMenuItem.Name = "測定仕様ToolStripMenuItem"
        Me.測定仕様ToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.測定仕様ToolStripMenuItem.Text = "測定仕様"
        '
        '選択ToolStripMenuItem
        '
        Me.選択ToolStripMenuItem.Name = "選択ToolStripMenuItem"
        Me.選択ToolStripMenuItem.Size = New System.Drawing.Size(94, 22)
        Me.選択ToolStripMenuItem.Text = "選択"
        '
        '保存ToolStripMenuItem
        '
        Me.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem"
        Me.保存ToolStripMenuItem.Size = New System.Drawing.Size(94, 22)
        Me.保存ToolStripMenuItem.Text = "保存"
        '
        '過去データToolStripMenuItem
        '
        Me.過去データToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.読込ToolStripMenuItem, Me.他の測定データ選択ToolStripMenuItem})
        Me.過去データToolStripMenuItem.Name = "過去データToolStripMenuItem"
        Me.過去データToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.過去データToolStripMenuItem.Text = "過去データ"
        '
        '読込ToolStripMenuItem
        '
        Me.読込ToolStripMenuItem.Name = "読込ToolStripMenuItem"
        Me.読込ToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.読込ToolStripMenuItem.Text = "読込"
        '
        '他の測定データ選択ToolStripMenuItem
        '
        Me.他の測定データ選択ToolStripMenuItem.Name = "他の測定データ選択ToolStripMenuItem"
        Me.他の測定データ選択ToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.他の測定データ選択ToolStripMenuItem.Text = "他の測定データ選択"
        '
        '終了ToolStripMenuItem
        '
        Me.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem"
        Me.終了ToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.終了ToolStripMenuItem.Text = "終了"
        '
        '測定ToolStripMenuItem
        '
        Me.測定ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.測定開始ToolStripMenuItem, Me.他の測定データ選択ToolStripMenuItem1})
        Me.測定ToolStripMenuItem.Name = "測定ToolStripMenuItem"
        Me.測定ToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.測定ToolStripMenuItem.Text = "測定"
        '
        '測定開始ToolStripMenuItem
        '
        Me.測定開始ToolStripMenuItem.Name = "測定開始ToolStripMenuItem"
        Me.測定開始ToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.測定開始ToolStripMenuItem.Text = "測定開始"
        '
        '他の測定データ選択ToolStripMenuItem1
        '
        Me.他の測定データ選択ToolStripMenuItem1.Name = "他の測定データ選択ToolStripMenuItem1"
        Me.他の測定データ選択ToolStripMenuItem1.Size = New System.Drawing.Size(168, 22)
        Me.他の測定データ選択ToolStripMenuItem1.Text = "他の測定データ選択"
        '
        '結果ToolStripMenuItem
        '
        Me.結果ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.印刷ToolStripMenuItem, Me.手動印刷ToolStripMenuItem, Me.保存ToolStripMenuItem1})
        Me.結果ToolStripMenuItem.Name = "結果ToolStripMenuItem"
        Me.結果ToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.結果ToolStripMenuItem.Text = "結果"
        '
        '印刷ToolStripMenuItem
        '
        Me.印刷ToolStripMenuItem.Checked = True
        Me.印刷ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.印刷ToolStripMenuItem.Name = "印刷ToolStripMenuItem"
        Me.印刷ToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.印刷ToolStripMenuItem.Text = "自動印刷"
        '
        '手動印刷ToolStripMenuItem
        '
        Me.手動印刷ToolStripMenuItem.Name = "手動印刷ToolStripMenuItem"
        Me.手動印刷ToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.手動印刷ToolStripMenuItem.Text = "手動印刷"
        '
        '保存ToolStripMenuItem1
        '
        Me.保存ToolStripMenuItem1.Name = "保存ToolStripMenuItem1"
        Me.保存ToolStripMenuItem1.Size = New System.Drawing.Size(130, 22)
        Me.保存ToolStripMenuItem1.Text = "保存(Excel)"
        '
        '設定ToolStripMenuItem
        '
        Me.設定ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.設定ToolStripMenuItem1})
        Me.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem"
        Me.設定ToolStripMenuItem.Size = New System.Drawing.Size(41, 20)
        Me.設定ToolStripMenuItem.Text = "設定"
        '
        '設定ToolStripMenuItem1
        '
        Me.設定ToolStripMenuItem1.Name = "設定ToolStripMenuItem1"
        Me.設定ToolStripMenuItem1.Size = New System.Drawing.Size(94, 22)
        Me.設定ToolStripMenuItem1.Text = "設定"
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
        Me.SST4500ヘルプToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.SST4500ヘルプToolStripMenuItem.Text = "SST-4500ヘルプ"
        '
        'SST4500についてToolStripMenuItem
        '
        Me.SST4500についてToolStripMenuItem.Name = "SST4500についてToolStripMenuItem"
        Me.SST4500についてToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.SST4500についてToolStripMenuItem.Text = "SST-4500について"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Location = New System.Drawing.Point(17, 271)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(481, 452)
        Me.PictureBox1.TabIndex = 21
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = Global.SST4500_1_0_0J.My.Resources.Resources.nomura_logo1
        Me.PictureBox2.Location = New System.Drawing.Point(824, 25)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(160, 50)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 54
        Me.PictureBox2.TabStop = False
        '
        'TxtMarkCur
        '
        Me.TxtMarkCur.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.TxtMarkCur.Location = New System.Drawing.Point(466, 83)
        Me.TxtMarkCur.Name = "TxtMarkCur"
        Me.TxtMarkCur.Size = New System.Drawing.Size(71, 22)
        Me.TxtMarkCur.TabIndex = 60
        '
        'TxtMarkBak
        '
        Me.TxtMarkBak.Font = New System.Drawing.Font("MS UI Gothic", 11.25!)
        Me.TxtMarkBak.Location = New System.Drawing.Point(466, 109)
        Me.TxtMarkBak.Name = "TxtMarkBak"
        Me.TxtMarkBak.Size = New System.Drawing.Size(71, 22)
        Me.TxtMarkBak.TabIndex = 61
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(463, 66)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 14)
        Me.Label1.TabIndex = 62
        Me.Label1.Text = "マーク"
        '
        'CmdClsGraph
        '
        Me.CmdClsGraph.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmdClsGraph.Location = New System.Drawing.Point(847, 465)
        Me.CmdClsGraph.Name = "CmdClsGraph"
        Me.CmdClsGraph.Size = New System.Drawing.Size(120, 35)
        Me.CmdClsGraph.TabIndex = 63
        Me.CmdClsGraph.Text = "グラフ消去"
        Me.CmdClsGraph.UseVisualStyleBackColor = True
        '
        'FrmSST4500_1_0_0J_meas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(984, 762)
        Me.Controls.Add(Me.CmdClsGraph)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtMarkBak)
        Me.Controls.Add(Me.TxtMarkCur)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.TxtMeasNumBak)
        Me.Controls.Add(Me.TxtMeasNumCur)
        Me.Controls.Add(Me.GbMeasSpec)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.GbPrint)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.CmdQuitSinglesheet)
        Me.Controls.Add(Me.CmdEtcMeasData)
        Me.Controls.Add(Me.CmdMeas)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtSmplNamBak)
        Me.Controls.Add(Me.TxtSmplNamCur)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtMachNoBak)
        Me.Controls.Add(Me.TxtMachNoCur)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LblMeasSpecBak)
        Me.Controls.Add(Me.LblMeasSpecCur)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LblProductNameMeas)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.TblMeasInfo_adm)
        Me.Controls.Add(Me.TblMeasInfo_nom)
        Me.Controls.Add(Me.TblMeasData_nom)
        Me.Controls.Add(Me.TblMeasData_adm)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "FrmSST4500_1_0_0J_meas"
        Me.Text = "SST-4500 Single Sheet"
        Me.TblMeasInfo_adm.ResumeLayout(False)
        Me.TblMeasInfo_adm.PerformLayout()
        Me.TblMeasData_adm.ResumeLayout(False)
        Me.TblMeasData_adm.PerformLayout()
        Me.TblMeasInfo_nom.ResumeLayout(False)
        Me.TblMeasInfo_nom.PerformLayout()
        Me.TblMeasData_nom.ResumeLayout(False)
        Me.TblMeasData_nom.PerformLayout()
        Me.GbPrint.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GbMeasSpec.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LblProductNameMeas As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LblMeasSpecCur As Label
    Friend WithEvents LblMeasSpecBak As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtMachNoCur As TextBox
    Friend WithEvents TxtMachNoBak As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TxtSmplNamCur As TextBox
    Friend WithEvents TxtSmplNamBak As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TblMeasInfo_adm As TableLayoutPanel
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents TimMeas As Timer
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents LblTSICDBak_adm As Label
    Friend WithEvents LblTSIMDBak_adm As Label
    Friend WithEvents LblSpdDeepBak_adm As Label
    Friend WithEvents LblSpdPeakBak_adm As Label
    Friend WithEvents LblSpdCDBak_adm As Label
    Friend WithEvents LblSpdMDBak_adm As Label
    Friend WithEvents LblratioPKDPBak_adm As Label
    Friend WithEvents LblratioMDCDBak_adm As Label
    Friend WithEvents LblAnglDeepBak_adm As Label
    Friend WithEvents LblAnglPeakBak_adm As Label
    Friend WithEvents LblMeasNumBak_adm As Label
    Friend WithEvents LblTSICDCur_adm As Label
    Friend WithEvents LblTSIMDCur_adm As Label
    Friend WithEvents LblSpdDeepCur_adm As Label
    Friend WithEvents LblSpdPeakCur_adm As Label
    Friend WithEvents LblSpdCDCur_adm As Label
    Friend WithEvents LblSpdMDCur_adm As Label
    Friend WithEvents LblratioPKDPCur_adm As Label
    Friend WithEvents LblratioMDCDCur_adm As Label
    Friend WithEvents LblAnglDeepCur_adm As Label
    Friend WithEvents LblAnglPeakCur_adm As Label
    Friend WithEvents Label28 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents LblMeasNumCur_adm As Label
    Friend WithEvents CmdMeas As Button
    Friend WithEvents CmdEtcMeasData As Button
    Friend WithEvents CmdOldDataLoad As Button
    Friend WithEvents CmdEtcOldMeasData As Button
    Friend WithEvents CmdQuitSinglesheet As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TblMeasData_adm As TableLayoutPanel
    Friend WithEvents LblMeasDatBak1_adm As Label
    Friend WithEvents LblMeasDatBak2_adm As Label
    Friend WithEvents LblMeasDatBak3_adm As Label
    Friend WithEvents LblMeasDatBak4_adm As Label
    Friend WithEvents LblMeasDatBak5_adm As Label
    Friend WithEvents LblMeasDatBak6_adm As Label
    Friend WithEvents LblMeasDatBak7_adm As Label
    Friend WithEvents LblMeasDatBak8_adm As Label
    Friend WithEvents LblMeasDatBak9_adm As Label
    Friend WithEvents LblMeasDatBak10_adm As Label
    Friend WithEvents LblMeasDatBak11_adm As Label
    Friend WithEvents LblMeasDatBak12_adm As Label
    Friend WithEvents LblMeasDatBak13_adm As Label
    Friend WithEvents LblMeasDatBak14_adm As Label
    Friend WithEvents LblMeasDatBak15_adm As Label
    Friend WithEvents LblMeasDatBak16_adm As Label
    Friend WithEvents LblMeasDatCur1_adm As Label
    Friend WithEvents Label54 As Label
    Friend WithEvents Label55 As Label
    Friend WithEvents Label53 As Label
    Friend WithEvents Label56 As Label
    Friend WithEvents Label57 As Label
    Friend WithEvents Label58 As Label
    Friend WithEvents Label59 As Label
    Friend WithEvents Label64 As Label
    Friend WithEvents Label66 As Label
    Friend WithEvents Label60 As Label
    Friend WithEvents Label61 As Label
    Friend WithEvents Label62 As Label
    Friend WithEvents Label63 As Label
    Friend WithEvents Label65 As Label
    Friend WithEvents Label67 As Label
    Friend WithEvents Label68 As Label
    Friend WithEvents Label69 As Label
    Friend WithEvents Label70 As Label
    Friend WithEvents Label71 As Label
    Friend WithEvents Label72 As Label
    Friend WithEvents LblMeasDatCur16_adm As Label
    Friend WithEvents LblMeasDatCur13_adm As Label
    Friend WithEvents LblMeasDatCur12_adm As Label
    Friend WithEvents LblMeasDatCur11_adm As Label
    Friend WithEvents LblMeasDatCur10_adm As Label
    Friend WithEvents LblMeasDatCur9_adm As Label
    Friend WithEvents LblMeasDatCur7_adm As Label
    Friend WithEvents LblMeasDatCur6_adm As Label
    Friend WithEvents LblMeasDatCur5_adm As Label
    Friend WithEvents LblMeasDatCur4_adm As Label
    Friend WithEvents LblMeasDatCur8_adm As Label
    Friend WithEvents LblMeasDatCur3_adm As Label
    Friend WithEvents LblMeasDatCur2_adm As Label
    Friend WithEvents LblMeasDatCur14_adm As Label
    Friend WithEvents LblMeasDatCur15_adm As Label
    Friend WithEvents TblMeasInfo_nom As TableLayoutPanel
    Friend WithEvents LblTSICDCur_nom As Label
    Friend WithEvents LblTSIMDCur_nom As Label
    Friend WithEvents LblSpdDeepCur_nom As Label
    Friend WithEvents LblSpdPeakCur_nom As Label
    Friend WithEvents LblSpdCDCur_nom As Label
    Friend WithEvents LblSpdMDCur_nom As Label
    Friend WithEvents LblratioPKDPCur_nom As Label
    Friend WithEvents LblratioMDCDCur_nom As Label
    Friend WithEvents LblAnglDeepCur_nom As Label
    Friend WithEvents LblAnglPeakCur_nom As Label
    Friend WithEvents Label50 As Label
    Friend WithEvents Label51 As Label
    Friend WithEvents Label52 As Label
    Friend WithEvents Label73 As Label
    Friend WithEvents Label74 As Label
    Friend WithEvents Label75 As Label
    Friend WithEvents Label76 As Label
    Friend WithEvents Label78 As Label
    Friend WithEvents Label79 As Label
    Friend WithEvents Label80 As Label
    Friend WithEvents Label81 As Label
    Friend WithEvents Label82 As Label
    Friend WithEvents Label83 As Label
    Friend WithEvents Label85 As Label
    Friend WithEvents Label87 As Label
    Friend WithEvents Label89 As Label
    Friend WithEvents Label90 As Label
    Friend WithEvents Label91 As Label
    Friend WithEvents LblMeasNumCur_nom As Label
    Friend WithEvents TblMeasData_nom As TableLayoutPanel
    Friend WithEvents LblMeasDatCur1_nom As Label
    Friend WithEvents Label203 As Label
    Friend WithEvents Label204 As Label
    Friend WithEvents Label205 As Label
    Friend WithEvents Label207 As Label
    Friend WithEvents Label208 As Label
    Friend WithEvents Label209 As Label
    Friend WithEvents Label210 As Label
    Friend WithEvents Label211 As Label
    Friend WithEvents Label212 As Label
    Friend WithEvents Label213 As Label
    Friend WithEvents Label214 As Label
    Friend WithEvents Label215 As Label
    Friend WithEvents Label216 As Label
    Friend WithEvents Label217 As Label
    Friend WithEvents Label218 As Label
    Friend WithEvents Label219 As Label
    Friend WithEvents Label220 As Label
    Friend WithEvents Label221 As Label
    Friend WithEvents Label222 As Label
    Friend WithEvents LblMeasDatCur16_nom As Label
    Friend WithEvents LblMeasDatCur13_nom As Label
    Friend WithEvents LblMeasDatCur12_nom As Label
    Friend WithEvents LblMeasDatCur11_nom As Label
    Friend WithEvents LblMeasDatCur10_nom As Label
    Friend WithEvents LblMeasDatCur9_nom As Label
    Friend WithEvents LblMeasDatCur7_nom As Label
    Friend WithEvents LblMeasDatCur6_nom As Label
    Friend WithEvents LblMeasDatCur5_nom As Label
    Friend WithEvents LblMeasDatCur4_nom As Label
    Friend WithEvents LblMeasDatCur8_nom As Label
    Friend WithEvents LblMeasDatCur3_nom As Label
    Friend WithEvents LblMeasDatCur2_nom As Label
    Friend WithEvents LblMeasDatCur14_nom As Label
    Friend WithEvents LblMeasDatCur15_nom As Label
    Friend WithEvents GbPrint As GroupBox
    Friend WithEvents CmdMeasPrint As Button
    Friend WithEvents ChkMeasAutoPrn As CheckBox
    Friend WithEvents Label18 As Label
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As ToolStripStatusLabel
    Friend WithEvents PrintDocument_adm As Printing.PrintDocument
    Friend WithEvents PPD_amd As PrintPreviewDialog
    Friend WithEvents PPD_nom As PrintPreviewDialog
    Friend WithEvents PrintDocument_nom As Printing.PrintDocument
    Friend WithEvents CmdMeasResultSave As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GbMeasSpec As GroupBox
    Friend WithEvents CmdMeasSpecSel As Button
    Friend WithEvents CmdMeasSpecSave As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents TxtMeasNumCur As Label
    Friend WithEvents TxtMeasNumBak As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ファイルToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 測定仕様ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 選択ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 保存ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 過去データToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 読込ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 他の測定データ選択ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 終了ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 測定ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 測定開始ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 他の測定データ選択ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents 設定ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 結果ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 印刷ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 手動印刷ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents 保存ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents 設定ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents ヘルプToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SST4500ヘルプToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SST4500についてToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TxtMarkCur As TextBox
    Friend WithEvents TxtMarkBak As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ToolStripStatusLabel5 As ToolStripStatusLabel
    Friend WithEvents CmdClsGraph As Button
End Class
