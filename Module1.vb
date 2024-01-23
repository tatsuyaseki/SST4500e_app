Imports System.Data.Common
Imports System.IO
Imports System.Linq.Expressions
Imports System.Management.Instrumentation
Imports System.Net.Http.Headers
Imports System.Net.Mail
Imports System.Net.Security
Imports System.Runtime.CompilerServices
Imports System.Runtime.Remoting.Messaging
Imports System.Security.Authentication
Imports System.Text
Imports Microsoft.Office.Core
Imports Microsoft.Office.Interop.Excel

Module Module1
    Public Declare Function FT_ListDevices Lib "FTD2XX.DLL" (ByVal arg1 As Integer, ByVal arg2 As String, ByVal dwFlags As Integer) As Integer
    Public Declare Function FT_GetNumDevices Lib "FTD2XX.DLL" Alias "FT_ListDevices" (ByVal arg1 As Integer, ByVal arg2 As String, ByVal dwFlags As Integer) As Integer

    Public Declare Function FT_Open Lib "FTD2XX.DLL" (ByVal intDeviceNumber As Short, ByRef lngHandle As Integer) As Integer
    Public Declare Function FT_OpenEx Lib "FTD2XX.DLL" (ByVal arg1 As String, ByVal arg2 As Integer, ByRef lngHandle As Long) As Integer
    Public Declare Function FT_Close Lib "FTD2XX.DLL" (ByVal lngHandle As Integer) As Integer
    'Public Declare Function FT_OpenBySerialNumber Lib "FTD2XX.DLL" Alias "FT_OpenEx" (ByVal SerialNumber As String, ByVal lngFlags As Integer, ByRef lngHandle As Integer) As Integer
    'Public Declare Function FT_OpenByDescription Lib "FTD2XX.DLL" Alias "FT_OpenEx" (ByVal Description As String, ByVal lngFlags As Integer, ByRef lngHandle As Integer) As Integer

    Public Declare Function FT_SetBaudRate Lib "FTD2XX.DLL" (ByVal lngHandle As Integer, ByVal lngBaudRate As Integer) As Integer
    Public Declare Function FT_SetBitMode Lib "FTD2XX.DLL" (ByVal lngHandle As Integer, ByVal intMask As Byte, ByVal intMode As Byte) As Integer
    Public Declare Function FT_GetBitMode Lib "FTD2XX.DLL" (ByVal lngHandle As Integer, ByRef intData As Integer) As Integer
    Public Declare Function FT_SetDataCharacteristics Lib "FTD2XX.DLL" (ByVal lngHandle As Integer, ByVal byWordLength As Byte, ByVal byStopBits As Byte, ByVal byParity As Byte) As Integer
    Public Declare Function FT_SetFlowControl Lib "FTD2XX.DLL" (ByVal lngHandle As Integer, ByVal intFlowControl As Integer, ByVal byXonChar As Byte, ByVal byXoffChar As Byte) As Integer
    Public Declare Function FT_SetTimeouts Lib "FTD2XX.DLL" (ByVal lngHandle As Integer, ByVal lngReadTimeout As Integer, ByVal lngWriteTimeout As Integer) As Integer

    Public Declare Function FT_ResetDevice Lib "FTD2XX.DLL" (ByVal lngHandle As Integer) As Integer
    Public Declare Function FT_SetLatencyTimer Lib "FTD2XX.DLL" (ByVal lngHandle As Integer, ByVal ucTimer As Byte) As Integer
    Public Declare Function FT_Purge Lib "FTD2XX.DLL" (ByVal lngHandle As Integer, ByVal lngMask As Integer) As Integer

    Public Declare Function FT_Read Lib "FTD2XX.DLL" (ByVal lngHandle As Integer, ByRef lpszBuffer As Byte, ByVal lngBufferSize As Integer, ByRef lngBytesReturned As Integer) As Integer
    Public Declare Function FT_Write Lib "FTD2XX.DLL" (ByVal lngHandle As Integer, ByRef lpszBuffer As Byte, ByVal lngBufferSize As Integer, ByRef lngBytesWritten As Integer) As Integer
    Public Declare Function FT_Read_String Lib "FTD2XX.DLL" Alias "FT_Read" (ByVal lngHandle As Integer, ByVal lpvBuffer As String, ByVal lngBufferSize As Integer, ByRef lngBytesReturned As Integer) As Integer
    Public Declare Function FT_Write_String Lib "FTD2XX.DLL" Alias "FT_Write" (ByVal lngHandle As Integer, ByVal lpvBuffer As String, ByVal lngBufferSize As Integer, ByRef lngBytesWritten As Integer) As Integer
    Public Declare Function FT_Read_Bytes Lib "FTD2XX.DLL" Alias "FT_Read" (ByVal lngHandle As Integer, ByRef lpvBuffer As Byte, ByVal lngBufferSize As Integer, ByRef lngBytesReturned As Integer) As Integer
    Public Declare Function FT_Write_Bytes Lib "FTD2XX.DLL" Alias "FT_Write" (ByVal lngHandle As Integer, ByRef lpvBuffer As Byte, ByVal lngBufferSize As Integer, ByRef lngBytesWritten As Integer) As Integer
    Public Declare Function FT_GetQueueStatus Lib "FTD2XX.DLL" (ByVal lngHandle As Integer, ByRef lngRxBytes As Integer) As Integer

    Public Const FT_OK = 0
    Public Const FT_INVALID_HANDLE = 1
    Public Const FT_DEVICE_NOT_FOUND = 2
    Public Const FT_DEVICE_NOT_OPENED = 3
    Public Const FT_IO_ERROR = 4
    Public Const FT_INSUFFICIENT_RESOURCES = 5
    Public Const FT_INVALID_PARAMETER = 6
    Public Const FT_INVALID_BAUD_ratio = 7
    Public Const FT_DEVICE_NOT_OPENED_FOR_ERASE = 8
    Public Const FT_DEVICE_NOT_OPENED_FOR_WRITE = 9
    Public Const FT_FAILED_TO_WRITE_DEVICE = 10
    Public Const FT_EEPROM_READ_FAILED = 11
    Public Const FT_EEPROM_WRITE_FAILED = 12
    Public Const FT_EEPROM_ERASE_FAILED = 13
    Public Const FT_EEPROM_NOT_PRESENT = 14
    Public Const FT_EEPROM_NOT_PROGRAMMED = 15
    Public Const FT_INVALID_ARGS = 16
    Public Const FT_OTHER_ERROR = 17

    Public Const FT_PURGE_RX = 1
    Public Const FT_PURGE_TX = 2

    Public Const FT_BITMODE_RESET = &H0
    Public Const FT_BITMODE_ASYNC_BITBANG = &H1
    Public Const FT_BITMODE_SYNC_BITBANG = &H4

    Public Const FT_LIST_BY_NUMBER_ONLY = &H80000000
    Public Const FT_LIST_BY_INDEX = &H40000000
    Public Const FT_LIST_ALL = &H20000000

    Public Const FT_OPEN_BY_SERIAL_NUMBER As Short = 1
    Public Const FT_OPEN_BY_DESCRIPTION As Short = 2

    Public Const FT_BAUD_9600 = 9600
    Public Const FT_DATA_BITS_7 = 7
    Public Const FT_DATA_BITS_8 = 8
    Public Const FT_STOP_BITS_1 = 0
    Public Const FT_PARITY_NONE = 0
    Public Const FT_FLOW_NONE = &H0
    Public Const FT_FLOW_RTS_CTS = &H100
    Public Const FT_FLOW_DTR_DSR = &H200
    Public Const FT_FLOW_XON_XOFF = &H400

    Public Const DEF_CONST_FILE_NAME_SG = "SG_フリー測定モード.cns"
    Public Const DEF_CONST_FILE_NAME_PF = "PF_フリー測定モード.cns"
    Public Const DEF_CONST_FILE_NAME_CT = "CT_フリー測定モード.cns"
    Public Const DEF_CONST_FILE_NAME_LG = "LG_フリー測定モード.cns"
    Public Const DEF_CONST_FILE_FLD = "\Const"
    Public Const DEF_RESULT_FILE_FLD = "\Result"
    Public Const DEF_DATA_FILE_FLD = "\Data\"

    Public Const PDMeasDataEnum = 0
    Public Const PDOldDataEnum = 1
    Public Const PDAvgDataEnum = 2

    Public Const Main_Enum = 0
    Public Const Meas_Enum = 1
    Public Const Profile_Enum = 2

    Public Const Us_Dist = 120

    Public Const TXTSMPWIDTH_0 = 260
    Public Const TXTSMPWIDTH_1 = 310
    Public Const TXTSMPWIDTH_add = 75

    Public Const LnCmp = 420    '両端補正値
    Public Const min_Pitch = 10 '最小ピッチ(mm)
    Public Const min_Points = 2
    Public Const max_Pitch = 9999

    Public lngHandle As Long

    Public passwd_adm As String
    Public passwd_adm2 As String
    Public passwd_adm2_chg As String        'PASSWD ADM2のパスワード変更用
    Public passwd_dbfsetting As String      'Data Backup Format Setting
    Public passwd_dbfsetting_chg As String  'PASSWD DBFSETTINGのパスワード変更用
    Public passwd_pchexpsetting As String       'Pitch Exp Setting Visible
    Public passwd_pchexpsetting_chg As String   'Pitch exp Setting Visibleのパスワード変更用1
    Public FlgDBF As Integer
    Public Const passwd_key = "MpX7BmKM"    '暗号化キー
    Public FlgPasswdChg As Integer          '1:adm, 2:adm2

    Public MachineNo As String
    Public Sample As String
    Public Mark As String
    Public BW As String
    Public DataDate As String
    Public DataTime As String
    Public DataDate_cur As String
    Public DataDate_bak As String
    Public DataTime_cur As String
    Public DataTime_bak As String
    Public FlgProfile As Integer
    Public flgTemp As Integer
    Public Length As Single
    Public Pitch As Single
    Public Points As Single
    Public LengthOld As Single
    Public PitchOld As Single
    Public PointsOld As Single
    Public Length_tmp As Single
    Public Pitch_tmp As Single
    Public Points_tmp As Single
    Public MeasNo As Integer
    Public FlgInch As Integer
    Public FlgPrfDisplay As Integer
    Public FlgMeasAutoPrn As Integer
    Public FlgPrfAutoPrn As Integer
    Public FlgPrfPrint As Integer
    Public FlgAlternate As Integer
    Public FlgVelocityRange As Integer
    Public FlgAngleRange As Integer
    Public FlgTSIRange As Integer
    Public FlgPkCenterAngle As Integer
    Public FlgDpCenterAngle As Integer
    Public PkAngCent As Single
    Public FileDate As String
    Public FileTime As String

    Public SampleNo As Long
    Public MeasDataNo As Long
    Public MeasDataMax As Long
    Public FileDataNo As Long
    Public FileDataMax As Long

    Public FlgStop As Integer

    Public DataCount As Integer

    Public KdData As Integer
    Public FlgPkcd As Single
    Public FlgDpmd As Single
    Public FlgAvg As Integer
    Public FlgLongMeas As Integer

    Public DataReceive(27) As String
    Public DataPrcStr(3, 30000, 11) As String
    Public DataPrcNum(3, 30000, 20) As Single
    '(0,M,N)=Avg.data, (1,M,N)=Meas.data, (2,M,N)=File data, (3,M,N)=Temp.data for Display
    'Max. Sample length = 1Km as Pitch=10mm
    Public DataFileStr(10, 1000, 11) As String
    Public DataFileNum(10, 1000, 20) As Single

    Public DataMogiStr(15, 11) As String
    Public DataMogiNum(15, 20) As Single

    'Public PosX(3) As Long
    Public PosX1(3) As Single
    Public PosX2(3) As Single

    Public StepX As Single
    Public StepScale As Single
    Public SclX As Single
    Public HsbHold As Single
    Public ShiftXNum As Integer
    Public Kax1 As Long
    Public DspPointx As Long

    Public DataMax1TSI(3) As Single
    Public DataMin1TSI(3) As Single
    Public DataInt1TSI(3) As Double
    Public DataMax2TSI(3) As Single
    Public DataMin2TSI(3) As Single
    Public DataInt2TSI(3) As Double
    Public DataMax1Angle(3) As Single
    Public DataMin1Angle(3) As Single
    Public DataInt1Angle(3) As Double
    Public DataMax2Angle(3) As Single
    Public DataMin2Angle(3) As Single
    Public DataInt2Angle(3) As Double
    Public DataMax1VelocityM(3) As Single
    Public DataMin1VelocityM(3) As Single
    Public DataInt1VelocityM(3) As Double
    Public DataMax2VelocityM(3) As Single
    Public DataMin2VelocityM(3) As Single
    Public DataInt2VelocityM(3) As Double
    Public DataMax1VelocityP(3) As Single
    Public DataMin1VelocityP(3) As Single
    Public DataInt1VelocityP(3) As Double
    Public DataMax2VelocityP(3) As Single
    Public DataMin2VelocityP(3) As Single
    Public DataInt2VelocityP(3) As Double
    Public DataMax1RatioM(3) As Single
    Public DataMin1RatioM(3) As Single
    Public DataInt1RatioM(3) As Double
    Public DataMax1RatioP(3) As Single
    Public DataMin1RatioP(3) As Single
    Public DataInt1RatioP(3) As Double

    Public flgFileLd As Integer
    Public FileNo As Integer

    Public lngBytesWritten As Long
    Public lngBytesRead As Long
    Public strTotalReadBuffer As String
    Public lngTotalBytesRead As Long
    Public numDevs As Integer
    Public TimerCountS As Integer
    Public FlgInitSplash As Integer
    Public FlgMainSplash As Integer
    Public FlgMainMeas As Integer
    Public FlgHoldMeas As Integer
    Public FlgMainProfile As Integer
    Public FlgMainTest As Integer
    Public FlgFeeder As Integer
    Public FlgAdmin As Integer
    Public FlgLine As Integer
    Public FlgScroll As Integer
    Public FlgTest As Integer
    Public ftStatus As Short
    Public strTemp As String
    Public passResult As Integer
    Public strWdata As String
    Public strRxdata As String
    Public flTimedout As Boolean
    Public flFatalError As Boolean
    Public flFailed As Boolean
    Public FT_RxQ_Bytes As Integer

    Public timerCount1 As Integer
    Public timerCount2 As Integer

    Public FileNumConst As Integer
    Public FileNumData As Integer

    Public StrConstFileName As String
    Public StrConstFilePath As String
    Public FlgConstChg As Boolean
    Public StrDataFileName As String
    Public StrFileName As String

    Public meas_waku_path1 As New List(Of Drawing2D.GraphicsPath)
    Public meas_waku_path2 As New List(Of Drawing2D.GraphicsPath)
    Public axis_path_cur As New List(Of Drawing2D.GraphicsPath)
    Public axis_path_bak As New List(Of Drawing2D.GraphicsPath)
    Public square_path1 As New List(Of Drawing2D.GraphicsPath)
    Public square_path2 As New List(Of Drawing2D.GraphicsPath)
    Public triangle_path1 As New List(Of Drawing2D.GraphicsPath)
    Public triangle_path2 As New List(Of Drawing2D.GraphicsPath)
    Public ytop_label As String
    Public ybtm_label As String
    Public xleft_label As String
    Public xright1_label As String
    Public xright2_label As String

    Public prn_meas_waku_path2 As New List(Of Drawing2D.GraphicsPath)
    Public prn_axis_path_cur As New List(Of Drawing2D.GraphicsPath)
    Public prn_axis_path_bak As New List(Of Drawing2D.GraphicsPath)
    Public prn_square_path1 As New List(Of Drawing2D.GraphicsPath)
    Public prn_square_path2 As New List(Of Drawing2D.GraphicsPath)
    Public prn_triangle_path1 As New List(Of Drawing2D.GraphicsPath)
    Public prn_triangle_path2 As New List(Of Drawing2D.GraphicsPath)

    Public prf_waku_Xlabel(7) As String
    Public prf_waku_Xlabel_name As String

    Public angle_peak_cur_path As New List(Of Drawing2D.GraphicsPath)
    Public angle_deep_cur_path As New List(Of Drawing2D.GraphicsPath)
    Public ratio_pkdp_cur_path As New List(Of Drawing2D.GraphicsPath)
    Public ratio_mdcd_cur_path As New List(Of Drawing2D.GraphicsPath)
    Public velo_md_cur_path As New List(Of Drawing2D.GraphicsPath)
    Public velo_cd_cur_path As New List(Of Drawing2D.GraphicsPath)
    Public velo_peak_cur_path As New List(Of Drawing2D.GraphicsPath)
    Public velo_deep_cur_path As New List(Of Drawing2D.GraphicsPath)
    Public tsi_md_cur_path As New List(Of Drawing2D.GraphicsPath)
    Public tsi_cd_cur_path As New List(Of Drawing2D.GraphicsPath)
    Public angle_peak_old_path As New List(Of Drawing2D.GraphicsPath)
    Public angle_deep_old_path As New List(Of Drawing2D.GraphicsPath)
    Public ratio_pkdp_old_path As New List(Of Drawing2D.GraphicsPath)
    Public ratio_mdcd_old_path As New List(Of Drawing2D.GraphicsPath)
    Public velo_md_old_path As New List(Of Drawing2D.GraphicsPath)
    Public velo_cd_old_path As New List(Of Drawing2D.GraphicsPath)
    Public velo_peak_old_path As New List(Of Drawing2D.GraphicsPath)
    Public velo_deep_old_path As New List(Of Drawing2D.GraphicsPath)
    Public tsi_md_old_path As New List(Of Drawing2D.GraphicsPath)
    Public tsi_cd_old_path As New List(Of Drawing2D.GraphicsPath)
    Public angle_peak_avg_path As New List(Of Drawing2D.GraphicsPath)
    Public angle_deep_avg_path As New List(Of Drawing2D.GraphicsPath)
    Public ratio_pkdp_avg_path As New List(Of Drawing2D.GraphicsPath)
    Public ratio_mdcd_avg_path As New List(Of Drawing2D.GraphicsPath)
    Public velo_md_avg_path As New List(Of Drawing2D.GraphicsPath)
    Public velo_cd_avg_path As New List(Of Drawing2D.GraphicsPath)
    Public velo_peak_avg_path As New List(Of Drawing2D.GraphicsPath)
    Public velo_deep_avg_path As New List(Of Drawing2D.GraphicsPath)
    Public tsi_md_avg_path As New List(Of Drawing2D.GraphicsPath)
    Public tsi_cd_avg_path As New List(Of Drawing2D.GraphicsPath)

    Public prf_waku_angle_Ypath1 As New List(Of Drawing2D.GraphicsPath)
    Public prf_waku_angle_Ypath2 As New List(Of Drawing2D.GraphicsPath)
    Public prf_waku_angle_Xpath As New List(Of Drawing2D.GraphicsPath)
    Public prf_waku_angle_Ylabel(8) As String
    Public prf_waku_angle_Pklabel_name As String
    Public prf_waku_angle_Dplabel_name As String
    Public prf_waku_angle_Yaxis_label As String

    Public prf_waku_ratio_Ypath As New List(Of Drawing2D.GraphicsPath)
    Public prf_waku_ratio_Xpath As New List(Of Drawing2D.GraphicsPath)
    Public prf_waku_ratio_Ylabel(3) As String
    Public prf_waku_ratio_MDCDlabel_name As String
    Public prf_waku_ratio_PkDplabel_name As String
    Public prf_waku_ratio_Yaxis_label As String

    Public prf_waku_velo_Ypath As New List(Of Drawing2D.GraphicsPath)
    Public prf_waku_velo_Xpath As New List(Of Drawing2D.GraphicsPath)
    Public prf_waku_velo_Ylabel(3) As String
    Public prf_waku_velo_VMDlabel_name As String
    Public prf_waku_velo_VCDlabel_name As String
    Public prf_waku_velo_VPklabel_name As String
    Public prf_waku_velo_VDplabel_name As String
    Public prf_waku_velo_Yaxis_label As String

    Public prf_waku_tsi_Ypath As New List(Of Drawing2D.GraphicsPath)
    Public prf_waku_tsi_Xpath As New List(Of Drawing2D.GraphicsPath)
    Public prf_waku_tsi_Ylabel(3) As String
    Public prf_waku_tsi_MDlabel_name As String
    Public prf_waku_tsi_CDlabel_name As String
    Public prf_waku_tsi_Yaxis_label As String

    Public meas_prn_linepath1 As New List(Of Drawing2D.GraphicsPath)
    Public prf_prn_linepath1 As New List(Of Drawing2D.GraphicsPath)
    Public prf_prn_linepath2 As New List(Of Drawing2D.GraphicsPath)

    Public Prn_top_margin As Integer    '30/100 inch = 7.62mm
    Public Prn_btm_margin As Integer
    Public Prn_left_margin As Integer
    Public Prn_right_margin As Integer
    Public FlgPrnBc_enable As Boolean

    Public FlgFTDLLerr As Integer

    Public chkPrnAngleRatio As Integer
    Public chkPrnVelocityTSI As Integer
    Public chkPrnMeasData As Integer
    Public chkPrnOldData As Integer
    Public chkPrnAvgData As Integer

    Public SG_ResultSave_path As String
    Public PF_ResultSave_path As String
    Public CT_ResultSave_path As String
    Public LG_ResultSave_path As String

    Public cur_dir As String

    Public angdpgraph_color As Color
    Public angpkgraph_color As Color
    Public angdpgraph3_color As Color
    Public angpkgraph3_color As Color
    Public ratpkdpgraph_color As Color
    Public ratmdcdgraph_color As Color
    Public ratpkdpgraph3_color As Color
    Public ratmdcdgraph3_color As Color
    Public velomdgraph_color As Color
    Public velocdgraph_color As Color
    Public velopkgraph_color As Color
    Public velodpgraph_color As Color
    Public velomdgraph3_color As Color
    Public velocdgraph3_color As Color
    Public velopkgraph3_color As Color
    Public velodpgraph3_color As Color
    Public tsimdgraph_color As Color
    Public tsicdgraph_color As Color
    Public tsimdgraph3_color As Color
    Public tsicdgraph3_color As Color

    Public test_count1 As Integer      'テストモード時の加圧時間
    Public test_count2 As Integer      'テストモード時の測定時間
    Public test_count1_prf As Integer
    Public test_count2_prf As Integer
    Public test_count1_md As Integer
    Public test_count2_md As Integer
    Public test_count3_prf As Integer
    Public test_count3_md As Integer
    Public timeout_time As Integer
    Public cmd_timeout As Integer
    Public feed_timeout As Integer

    Public frm_MainForm_bc As Color
    Public frm_MainMenu_bc As Color
    Public frm_MainStatus_bc As Color
    Public frm_MainButton_bc As Color
    Public frm_MainButton_fc As Color
    Public frm_MainForm_fc As Color
    Public frm_MainMenu_fc As Color
    Public frm_MainStatus_fc As Color
    Public frm_MainLine_color As Color

    Public frm_MeasForm_bc As Color
    Public frm_MeasMenu_bc As Color
    Public frm_MeasStatus_bc As Color
    Public frm_MeasGraph_bc As Color
    Public frm_MeasButton_bc As Color
    Public frm_MeasButton_fc As Color
    Public frm_MeasForm_fc As Color
    Public frm_MeasCurData_color As Color
    Public frm_MeasOldData_color As Color
    Public frm_MeasMenu_fc As Color
    Public frm_MeasStatus_fc As Color
    Public frm_MeasuringButton_bc As Color
    Public frm_MeasuringButton_fc As Color
    Public frm_MeasTextBox_bc As Color
    Public frm_MeasGraphWaku_color As Color
    Public frm_MeasMeasButton_bc As Color
    Public frm_MeasMeasButton_fc As Color

    Public frm_PrfForm_bc As Color
    Public frm_PrfMenu_bc As Color
    Public frm_PrfStatus_bc As Color
    Public frm_PrfGraph_bc As Color
    Public frm_PrfButton_bc As Color
    Public frm_PrfButton_fc As Color
    Public frm_PrfForm_fc As Color
    Public frm_PrfCurData_color As Color
    Public frm_PrfOldData_color As Color
    Public frm_PrfAvgData_color As Color
    Public frm_PrfMenu_fc As Color
    Public frm_PrfStatus_fc As Color
    Public frm_PrfMeasuringButton_bc As Color
    Public frm_PrfMeasuringButton_fc As Color
    Public frm_PrfTextBox_bc As Color
    Public frm_PrfGraphWaku_color As Color
    Public frm_PrfMeasButton_bc As Color
    Public frm_PrfMeasButton_fc As Color

    Public frm_MainStatusBorder_stl As Border3DStyle
    Public frm_MeasStatusBorder_stl As Border3DStyle
    Public frm_PrfStatusBorder_stl As Border3DStyle

    Public Const _rdy = 0
    Public Const _mes = 1

    Public FlgPitchExp As Integer   '0=ピッチ拡張無効, 1=ピッチ拡張有効
    Public FlgPitchExp_Load As Integer  '0=ピッチ拡張未ロード, 1=ピッチ拡張ロード済み
    Public PchExp_PchData(0) As Single
    Public PchExp_Length As Single
    Public Const StrConstFileName_PchExp = ".pitch"
    Public FlgPchExpMes As Integer  '0=ピッチ拡張無効で測定, 1=ピッチ拡張有効で測定
    Public FlgPchExpMes_old As Integer  '過去データのピッチ拡張有効無効測定フラグ
    Public FlgPchExp_Visible As Integer 'ピッチ拡張表示非表示 0=非表示(強制的に無効), 1=表示
    Public PchExpSettingFile As String
    Public PchExpSettingFile_FullPath As String

    Public Const Dbf_add_filename = "_adddata"

    Public Sub CmdMeasButton_set(ByVal meas_status As Integer)
        If FlgProfile = 0 Then
            'シングルモード
            If meas_status = _rdy Then
                With FrmSST4500_1_0_0J_meas
                    .CmdMeas.BackColor = frm_MeasMeasButton_bc
                    .CmdMeas.ForeColor = frm_MeasMeasButton_fc
                    .CmdMeas.FlatStyle = FlatStyle.Standard
                    If .CmdMeas.BackColor = SystemColors.Control Then
                        .CmdMeas.UseVisualStyleBackColor = True
                    End If
                End With
            ElseIf meas_status = _mes Then
                With FrmSST4500_1_0_0J_meas
                    .CmdMeas.BackColor = frm_MeasuringButton_bc
                    .CmdMeas.ForeColor = frm_MeasuringButton_fc
                End With
            End If
        Else
            'プロファイルモード
            If meas_status = _rdy Then
                With FrmSST4500_1_0_0J_Profile
                    .CmdMeas.BackColor = frm_PrfMeasButton_bc
                    .CmdMeas.ForeColor = frm_PrfMeasButton_fc
                    .CmdMeas.FlatStyle = FlatStyle.Standard
                    If .CmdMeas.BackColor = SystemColors.Control Then
                        .CmdMeas.UseVisualStyleBackColor = True
                    End If
                End With
            ElseIf meas_status = _mes Then
                With FrmSST4500_1_0_0J_Profile
                    .CmdMeas.BackColor = frm_PrfMeasuringButton_bc
                    .CmdMeas.ForeColor = frm_PrfMeasuringButton_fc
                End With
            End If
        End If
    End Sub

    Public Sub mainform_color_setting_load()
        With My.Settings
            frm_MainForm_bc = ._frm_MainForm_bc
            frm_MainMenu_bc = ._frm_MainMenu_bc
            frm_MainStatus_bc = ._frm_MainStatus_bc
            frm_MainButton_bc = ._frm_MainButton_bc

            frm_MainForm_fc = ._frm_MainForm_fc
            frm_MainMenu_fc = ._frm_MainMenu_fc
            frm_MainStatus_fc = ._frm_MainStatus_fc
            frm_MainButton_fc = ._frm_MainButton_fc
            frm_MainLine_color = ._frm_MainLine_color

            frm_MainStatusBorder_stl = ._frm_MainStatusBorder_stl
        End With
    End Sub

    Public Sub measform_color_setting_load()
        With My.Settings
            frm_MeasForm_bc = ._frm_MeasForm_bc
            frm_MeasMenu_bc = ._frm_MeasMenu_bc
            frm_MeasStatus_bc = ._frm_MeasStatus_bc
            frm_MeasButton_bc = ._frm_MeasButton_bc
            frm_MeasMeasButton_bc = ._frm_MeasMeasButton_bc
            frm_MeasuringButton_bc = ._frm_MeasuringButton_bc
            frm_MeasGraph_bc = ._frm_MeasGraph_bc
            frm_MeasTextBox_bc = ._frm_MeasTextBox_bc

            frm_MeasOldData_color = ._frm_MeasOldData_color
            frm_MeasCurData_color = ._frm_MeasCurData_color
            frm_MeasGraphWaku_color = ._frm_MeasGraphWaku_color

            frm_MeasForm_fc = ._frm_MeasForm_fc
            frm_MeasMenu_fc = ._frm_MeasMenu_fc
            frm_MeasStatus_fc = ._frm_MeasStatus_fc
            frm_MeasButton_fc = ._frm_MeasButton_fc
            frm_MeasMeasButton_fc = ._frm_MeasMeasButton_fc
            frm_MeasuringButton_fc = ._frm_MeasuringButton_fc

            frm_MeasStatusBorder_stl = ._frm_MeasStatusBorder_stl
        End With
    End Sub

    Public Sub prfform_color_setting_load()
        With My.Settings
            frm_PrfForm_bc = ._frm_PrfForm_bc
            frm_PrfMenu_bc = ._frm_PrfMenu_bc
            frm_PrfStatus_bc = ._frm_PrfStatus_bc
            frm_PrfButton_bc = ._frm_PrfButton_bc
            frm_PrfMeasButton_bc = ._frm_PrfMeasButton_bc
            frm_PrfMeasuringButton_bc = ._frm_PrfMeasuringButton_bc
            frm_PrfGraph_bc = ._frm_PrfGraph_bc
            frm_PrfTextBox_bc = ._frm_PrfTextBox_bc

            frm_PrfOldData_color = ._frm_PrfOldData_color
            frm_PrfCurData_color = ._frm_PrfCurData_color
            frm_PrfAvgData_color = ._frm_PrfAvgData_color
            frm_PrfGraphWaku_color = ._frm_PrfGraphWaku_color

            frm_PrfForm_fc = ._frm_PrfForm_fc
            frm_PrfMenu_fc = ._frm_PrfMenu_fc
            frm_PrfStatus_fc = ._frm_PrfStatus_fc
            frm_PrfButton_fc = ._frm_PrfButton_fc
            frm_PrfMeasButton_fc = ._frm_PrfMeasButton_fc
            frm_PrfMeasuringButton_fc = ._frm_PrfMeasuringButton_fc

            frm_PrfStatusBorder_stl = ._frm_PrfStatusBorder_stl

        End With
    End Sub

    Public Sub colorsetting_label_init(ByVal sel As Integer)
        Select Case sel
            Case Main_Enum
                With FrmSST4500_1_0_0J_colorsetting
                    .LblFrmMainFormBC.BackColor = frm_MainForm_bc
                    .LblFrmMainMenuBC.BackColor = frm_MainMenu_bc
                    .LblFrmMainStatusBC.BackColor = frm_MainStatus_bc
                    .LblFrmMainButtonBC.BackColor = frm_MainButton_bc

                    .LblFrmMainFormFC.BackColor = frm_MainForm_fc
                    .LblFrmMainMenuFC.BackColor = frm_MainMenu_fc
                    .LblFrmMainStatusFC.BackColor = frm_MainStatus_fc
                    .LblFrmMainButtonFC.BackColor = frm_MainButton_fc
                    .LblFrmMainLineColor.BackColor = frm_MainLine_color
                End With
                mainform_borderstyle_init()

            Case Meas_Enum
                With FrmSST4500_1_0_0J_colorsetting
                    .LblFrmMeasFormBC.BackColor = frm_MeasForm_bc
                    .LblFrmMeasMenuBC.BackColor = frm_MeasMenu_bc
                    .LblFrmMeasStatusBC.BackColor = frm_MeasStatus_bc
                    .LblFrmMeasButtonBC.BackColor = frm_MeasButton_bc
                    .LblFrmMeasMeasButtonBC.BackColor = frm_MeasMeasButton_bc
                    .LblFrmMeasuringButtonBC.BackColor = frm_MeasuringButton_bc
                    .LblFrmMeasGraphBC.BackColor = frm_MeasGraph_bc
                    .LblFrmMeasTextBoxBC.BackColor = frm_MeasTextBox_bc

                    .LblFrmMeasFormFC.BackColor = frm_MeasForm_fc
                    .LblFrmMeasMenuFC.BackColor = frm_MeasMenu_fc
                    .LblFrmMeasStatusFC.BackColor = frm_MeasStatus_fc
                    .LblFrmMeasButtonFC.BackColor = frm_MeasButton_fc
                    .LblFrmMeasMeasButtonFC.BackColor = frm_MeasMeasButton_fc
                    .LblFrmMeasuringButtonFC.BackColor = frm_MeasuringButton_fc
                    .LblFrmMeasOldDataColor.BackColor = frm_MeasOldData_color
                    .LblFrmMeasCurDataColor.BackColor = frm_MeasCurData_color
                    .LblFrmMeasGraphWakuColor.BackColor = frm_MeasGraphWaku_color
                End With
                measform_borderstyle_init()

            Case Profile_Enum
                With FrmSST4500_1_0_0J_colorsetting
                    .LblFrmPrfFormBC.BackColor = frm_PrfForm_bc
                    .LblFrmPrfMenuBC.BackColor = frm_PrfMenu_bc
                    .LblFrmPrfStatusBC.BackColor = frm_PrfStatus_bc
                    .LblFrmPrfButtonBC.BackColor = frm_PrfButton_bc
                    .LblFrmPrfMeasButtonBC.BackColor = frm_PrfMeasButton_bc
                    .LblFrmPrfMeasuringButtonBC.BackColor = frm_PrfMeasuringButton_bc
                    .LblFrmPrfGraphBC.BackColor = frm_PrfGraph_bc
                    .LblFrmPrfTextBoxBC.BackColor = frm_PrfTextBox_bc

                    .LblFrmPrfFormFC.BackColor = frm_PrfForm_fc
                    .LblFrmPrfMenuFC.BackColor = frm_PrfMenu_fc
                    .LblFrmPrfStatusFC.BackColor = frm_PrfStatus_fc
                    .LblFrmPrfButtonFC.BackColor = frm_PrfButton_fc
                    .LblFrmPrfMeasButtonFC.BackColor = frm_PrfMeasButton_fc
                    .LblFrmPrfMeasuringButtonFC.BackColor = frm_PrfMeasuringButton_fc
                    .LblFrmPrfOldDataColor.BackColor = frm_PrfOldData_color
                    .LblFrmPrfCurDataColor.BackColor = frm_PrfCurData_color
                    .LblFrmPrfAvgDataColor.BackColor = frm_PrfAvgData_color
                    .LblFrmPrfGraphWakuColor.BackColor = frm_PrfGraphWaku_color
                End With
                prfform_borderstyle_init()
        End Select
    End Sub

    Public Sub prfform_color_init()
        With FrmSST4500_1_0_0J_Profile
            set_prfformbc()
            .MenuStrip1.BackColor = frm_PrfMenu_bc
            .StatusStrip1.BackColor = frm_PrfStatus_bc
            set_prfgraphbc()
            set_prftextboxbc()
            set_prfcmdbc()
            set_prfcmdfc()

            If frm_PrfButton_bc = SystemColors.Control Then
                .CmdMeas.UseVisualStyleBackColor = True
                .CmdMeasSpecSel.UseVisualStyleBackColor = True
                .CmdMeasSpecSave.UseVisualStyleBackColor = True
                .CmdOldDataLoad.UseVisualStyleBackColor = True
                .CmdClsGraph.UseVisualStyleBackColor = True
                .CmdAvg.UseVisualStyleBackColor = True
                .CmdQuitProfile.UseVisualStyleBackColor = True
                .CmdPrfPrint.UseVisualStyleBackColor = True
                .CmdPrfResultSave.UseVisualStyleBackColor = True
                .CmdAngleRange.UseVisualStyleBackColor = True
                .CmdVeloRange.UseVisualStyleBackColor = True
                .CmdTSIRange.UseVisualStyleBackColor = True
            End If

            set_prfformfc()
            set_prfolddatacolor()
            set_prfcurdatacolor()
            set_prfavgdatacolor()
            set_prfmenufc()

            .ToolStripStatusLabel1.ForeColor = frm_PrfStatus_fc
            .ToolStripStatusLabel2.ForeColor = frm_PrfStatus_fc
            .ToolStripStatusLabel3.ForeColor = frm_PrfStatus_fc
        End With
    End Sub

    Public Sub measform_color_init()
        'colorsetting_label_init(Meas_Enum)

        With FrmSST4500_1_0_0J_meas

            set_measformbc()

            'MeasMenuBC
            .MenuStrip1.BackColor = frm_MeasMenu_bc

            'MeasStatusBC
            .StatusStrip1.BackColor = frm_MeasStatus_bc

            'MeasPicturebox1BC
            .PictureBox1.BackColor = frm_MeasGraph_bc

            set_meastextboxbc()

            set_meascmdbc()
            set_meascmdfc()

            If frm_MeasButton_bc = SystemColors.Control Then
                .CmdMeas.UseVisualStyleBackColor = True
                .CmdEtcMeasData.UseVisualStyleBackColor = True
                .CmdMeasSpecSel.UseVisualStyleBackColor = True
                .CmdMeasSpecSave.UseVisualStyleBackColor = True
                .CmdOldDataLoad.UseVisualStyleBackColor = True
                .CmdEtcOldMeasData.UseVisualStyleBackColor = True
                .CmdQuitSinglesheet.UseVisualStyleBackColor = True
                .CmdMeasPrint.UseVisualStyleBackColor = True
                .CmdMeasResultSave.UseVisualStyleBackColor = True
            End If

            set_measformfc()
            set_measolddatacolor()
            set_meascurdatacolor()
            set_measmenufc()

            .ToolStripStatusLabel1.ForeColor = frm_MeasStatus_fc
            .ToolStripStatusLabel2.ForeColor = frm_MeasStatus_fc
            .ToolStripStatusLabel3.ForeColor = frm_MeasStatus_fc
        End With
    End Sub

    Public Sub set_prfgraphbc()
        With FrmSST4500_1_0_0J_Profile
            .PictureBox1.BackColor = frm_PrfGraph_bc
            .PictureBox2.BackColor = frm_PrfGraph_bc
            .PictureBox3.BackColor = frm_PrfGraph_bc
            .PictureBox4.BackColor = frm_PrfGraph_bc
            .LblAngCenter.BackColor = frm_PrfGraph_bc
            '.TabAngleratio.BackColor = frm_PrfGraph_bc
            '.TabVeloTsi.BackColor = frm_PrfGraph_bc
            '.TabMeasDataView.BackColor = frm_PrfGraph_bc
            '.TabPage1.BackColor = frm_PrfGraph_bc
            '.TabPage2.BackColor = frm_PrfGraph_bc
            '.TabPage3.BackColor = frm_PrfGraph_bc
            '.Label57.BackColor = frm_PrfGraph_bc
            '.Label70.BackColor = frm_PrfGraph_bc
            '.Label73.BackColor = frm_PrfGraph_bc
            '.Label82.BackColor = frm_PrfGraph_bc
            '.Label88.BackColor = frm_PrfGraph_bc
            '.Label110.BackColor = frm_PrfGraph_bc
            '.Label111.BackColor = frm_PrfGraph_bc
            '.Label114.BackColor = frm_PrfGraph_bc
            '.Label117.BackColor = frm_PrfGraph_bc
            '.Label123.BackColor = frm_PrfGraph_bc
            '.Label129.BackColor = frm_PrfGraph_bc
            '.Label130.BackColor = frm_PrfGraph_bc
            '.Label133.BackColor = frm_PrfGraph_bc
            '.Label136.BackColor = frm_PrfGraph_bc
            '.Label142.BackColor = frm_PrfGraph_bc
            '.DataGridView1.DefaultCellStyle.BackColor = frm_PrfGraph_bc
            '.DataGridView1.DefaultCellStyle.SelectionBackColor = frm_PrfGraph_bc
            '.DataGridView2.DefaultCellStyle.BackColor = frm_PrfGraph_bc
            '.DataGridView2.DefaultCellStyle.SelectionBackColor = frm_PrfGraph_bc
            '.DataGridView3.DefaultCellStyle.BackColor = frm_PrfGraph_bc
            '.DataGridView3.DefaultCellStyle.SelectionBackColor = frm_PrfGraph_bc
            '.Label76.BackColor = frm_PrfGraph_bc        '配向角 [deg.]
            '.Label79.BackColor = frm_PrfGraph_bc        'Max.
            '.Label81.BackColor = frm_PrfGraph_bc        'Avg.
            '.Label80.BackColor = frm_PrfGraph_bc        'Min.
            '.Label19.BackColor = frm_PrfGraph_bc        '配向比
            '.Label22.BackColor = frm_PrfGraph_bc        'Max.
            '.Label23.BackColor = frm_PrfGraph_bc        'Avg.
            '.Label24.BackColor = frm_PrfGraph_bc        'Min.
            '.Label64.BackColor = frm_PrfGraph_bc
            '.Label67.BackColor = frm_PrfGraph_bc
            '.Label69.BackColor = frm_PrfGraph_bc
            '.Label68.BackColor = frm_PrfGraph_bc
            '.LblAnglePkMax_nom.BackColor = frm_PrfGraph_bc
            '.LblAnglePkAvg_nom.BackColor = frm_PrfGraph_bc
            '.LblAnglePkMin_nom.BackColor = frm_PrfGraph_bc
            '.LblAngleDpMax_nom.BackColor = frm_PrfGraph_bc
            '.LblAngleDpAvg_nom.BackColor = frm_PrfGraph_bc
            '.LblAngleDpMin_nom.BackColor = frm_PrfGraph_bc
            '.Label39.BackColor = frm_PrfGraph_bc
            '.Label42.BackColor = frm_PrfGraph_bc
            '.Label44.BackColor = frm_PrfGraph_bc
            '.Label43.BackColor = frm_PrfGraph_bc
            '.LblRatioPkDpMax_nom.BackColor = frm_PrfGraph_bc
            '.LblRatioPkDpAvg_nom.BackColor = frm_PrfGraph_bc
            '.LblRatioPkDpMin_nom.BackColor = frm_PrfGraph_bc
            '.LblRatioMDCDMax_nom.BackColor = frm_PrfGraph_bc
            '.LblRatioMDCDAvg_nom.BackColor = frm_PrfGraph_bc
            '.LblRatioMDCDMin_nom.BackColor = frm_PrfGraph_bc
            '.Label4.BackColor = frm_PrfGraph_bc
            '.Label10.BackColor = frm_PrfGraph_bc
            '.Label11.BackColor = frm_PrfGraph_bc
            '.Label12.BackColor = frm_PrfGraph_bc
            '.Label13.BackColor = frm_PrfGraph_bc
            '.Label16.BackColor = frm_PrfGraph_bc
            '.Label17.BackColor = frm_PrfGraph_bc
            '.Label18.BackColor = frm_PrfGraph_bc
            '.Label58.BackColor = frm_PrfGraph_bc
            '.Label61.BackColor = frm_PrfGraph_bc
            '.Label63.BackColor = frm_PrfGraph_bc
            '.Label62.BackColor = frm_PrfGraph_bc
            '.Label29.BackColor = frm_PrfGraph_bc
            '.Label32.BackColor = frm_PrfGraph_bc
            '.Label34.BackColor = frm_PrfGraph_bc
            '.Label33.BackColor = frm_PrfGraph_bc
            '.LblVeloPkMax_nom.BackColor = frm_PrfGraph_bc
            '.LblVeloPkAvg_nom.BackColor = frm_PrfGraph_bc
            '.LblVeloPkMin_nom.BackColor = frm_PrfGraph_bc
            '.LblVeloDpMax_nom.BackColor = frm_PrfGraph_bc
            '.LblVeloDpAvg_nom.BackColor = frm_PrfGraph_bc
            '.LblVeloDpMin_nom.BackColor = frm_PrfGraph_bc
            '.Label45.BackColor = frm_PrfGraph_bc
            '.Label48.BackColor = frm_PrfGraph_bc
            '.Label50.BackColor = frm_PrfGraph_bc
            '.Label49.BackColor = frm_PrfGraph_bc
            '.LblVeloMDMax_nom.BackColor = frm_PrfGraph_bc
            '.LblVeloMDAvg_nom.BackColor = frm_PrfGraph_bc
            '.LblVeloMDMin_nom.BackColor = frm_PrfGraph_bc
            '.LblVeloCDMax_nom.BackColor = frm_PrfGraph_bc
            '.LblVeloCDAvg_nom.BackColor = frm_PrfGraph_bc
            '.LblVeloCDMin_nom.BackColor = frm_PrfGraph_bc
            '.Label35.BackColor = frm_PrfGraph_bc
            '.Label38.BackColor = frm_PrfGraph_bc
            '.Label56.BackColor = frm_PrfGraph_bc
            '.Label55.BackColor = frm_PrfGraph_bc
            '.LblTSIMDMax_nom.BackColor = frm_PrfGraph_bc
            '.LblTSIMDAvg_nom.BackColor = frm_PrfGraph_bc
            '.LblTSIMDMin_nom.BackColor = frm_PrfGraph_bc
            '.LblTSICDMax_nom.BackColor = frm_PrfGraph_bc
            '.LblTSICDAvg_nom.BackColor = frm_PrfGraph_bc
            '.LblTSICDMin_nom.BackColor = frm_PrfGraph_bc
        End With
    End Sub

    Public Sub set_measformbc()
        With FrmSST4500_1_0_0J_meas
            'MeasFormBC
            .BackColor = frm_MeasForm_bc
            .Label8.BackColor = frm_MeasForm_bc
            .Label74.BackColor = frm_MeasForm_bc
            .Label50.BackColor = frm_MeasForm_bc
            .LblTSICDCur_nom.BackColor = frm_MeasForm_bc
            .GbMeasSpec.BackColor = frm_MeasForm_bc
            .GroupBox5.BackColor = frm_MeasForm_bc
            .GbPrint.BackColor = frm_MeasForm_bc
            .GroupBox1.BackColor = frm_MeasForm_bc
            .GroupBox2.BackColor = frm_MeasForm_bc
        End With
    End Sub

    Public Sub set_prfformbc()
        With FrmSST4500_1_0_0J_Profile
            .BackColor = frm_PrfForm_bc
            .GbPrfSpec.BackColor = frm_PrfForm_bc
            .GroupBox1.BackColor = frm_PrfForm_bc
            .GbPrint.BackColor = frm_PrfForm_bc
            .GroupBox3.BackColor = frm_PrfForm_bc
            .GroupBox2.BackColor = frm_PrfForm_bc
            .TabAngleratio.BackColor = frm_PrfForm_bc
            .TabVeloTsi.BackColor = frm_PrfForm_bc
            .TabMeasDataView.BackColor = frm_PrfForm_bc
            .TabPage1.BackColor = frm_PrfForm_bc
            .TabPage2.BackColor = frm_PrfForm_bc
            .TabPage3.BackColor = frm_PrfForm_bc
            .Label57.BackColor = frm_PrfForm_bc
            .Label70.BackColor = frm_PrfForm_bc
            .Label73.BackColor = frm_PrfForm_bc
            .Label82.BackColor = frm_PrfForm_bc
            .Label88.BackColor = frm_PrfForm_bc
            .Label110.BackColor = frm_PrfForm_bc
            .Label111.BackColor = frm_PrfForm_bc
            .Label114.BackColor = frm_PrfForm_bc
            .Label117.BackColor = frm_PrfForm_bc
            .Label123.BackColor = frm_PrfForm_bc
            .Label129.BackColor = frm_PrfForm_bc
            .Label130.BackColor = frm_PrfForm_bc
            .Label133.BackColor = frm_PrfForm_bc
            .Label136.BackColor = frm_PrfForm_bc
            .Label142.BackColor = frm_PrfForm_bc
            .DataGridView1.DefaultCellStyle.BackColor = frm_PrfForm_bc
            .DataGridView1.DefaultCellStyle.SelectionBackColor = frm_PrfForm_bc
            .DataGridView2.DefaultCellStyle.BackColor = frm_PrfForm_bc
            .DataGridView2.DefaultCellStyle.SelectionBackColor = frm_PrfForm_bc
            .DataGridView3.DefaultCellStyle.BackColor = frm_PrfForm_bc
            .DataGridView3.DefaultCellStyle.SelectionBackColor = frm_PrfForm_bc
            .Label76.BackColor = frm_PrfForm_bc        '配向角 [deg.]
            .Label79.BackColor = frm_PrfForm_bc        'Max.
            .Label81.BackColor = frm_PrfForm_bc        'Avg.
            .Label80.BackColor = frm_PrfForm_bc        'Min.
            .Label19.BackColor = frm_PrfForm_bc        '配向比
            .Label22.BackColor = frm_PrfForm_bc        'Max.
            .Label23.BackColor = frm_PrfForm_bc        'Avg.
            .Label24.BackColor = frm_PrfForm_bc        'Min.
            .Label64.BackColor = frm_PrfForm_bc
            .Label67.BackColor = frm_PrfForm_bc
            .Label69.BackColor = frm_PrfForm_bc
            .Label68.BackColor = frm_PrfForm_bc
            .LblAnglePkMax_nom.BackColor = frm_PrfForm_bc
            .LblAnglePkAvg_nom.BackColor = frm_PrfForm_bc
            .LblAnglePkMin_nom.BackColor = frm_PrfForm_bc
            .LblAngleDpMax_nom.BackColor = frm_PrfForm_bc
            .LblAngleDpAvg_nom.BackColor = frm_PrfForm_bc
            .LblAngleDpMin_nom.BackColor = frm_PrfForm_bc
            .Label39.BackColor = frm_PrfForm_bc
            .Label42.BackColor = frm_PrfForm_bc
            .Label44.BackColor = frm_PrfForm_bc
            .Label43.BackColor = frm_PrfForm_bc
            .LblRatioPkDpMax_nom.BackColor = frm_PrfForm_bc
            .LblRatioPkDpAvg_nom.BackColor = frm_PrfForm_bc
            .LblRatioPkDpMin_nom.BackColor = frm_PrfForm_bc
            .LblRatioMDCDMax_nom.BackColor = frm_PrfForm_bc
            .LblRatioMDCDAvg_nom.BackColor = frm_PrfForm_bc
            .LblRatioMDCDMin_nom.BackColor = frm_PrfForm_bc
            .Label4.BackColor = frm_PrfForm_bc
            .Label10.BackColor = frm_PrfForm_bc
            .Label11.BackColor = frm_PrfForm_bc
            .Label12.BackColor = frm_PrfForm_bc
            .Label13.BackColor = frm_PrfForm_bc
            .Label16.BackColor = frm_PrfForm_bc
            .Label17.BackColor = frm_PrfForm_bc
            .Label18.BackColor = frm_PrfForm_bc
            .Label58.BackColor = frm_PrfForm_bc
            .Label61.BackColor = frm_PrfForm_bc
            .Label63.BackColor = frm_PrfForm_bc
            .Label62.BackColor = frm_PrfForm_bc
            .Label29.BackColor = frm_PrfForm_bc
            .Label32.BackColor = frm_PrfForm_bc
            .Label34.BackColor = frm_PrfForm_bc
            .Label33.BackColor = frm_PrfForm_bc
            .LblVeloPkMax_nom.BackColor = frm_PrfForm_bc
            .LblVeloPkAvg_nom.BackColor = frm_PrfForm_bc
            .LblVeloPkMin_nom.BackColor = frm_PrfForm_bc
            .LblVeloDpMax_nom.BackColor = frm_PrfForm_bc
            .LblVeloDpAvg_nom.BackColor = frm_PrfForm_bc
            .LblVeloDpMin_nom.BackColor = frm_PrfForm_bc
            .Label45.BackColor = frm_PrfForm_bc
            .Label48.BackColor = frm_PrfForm_bc
            .Label50.BackColor = frm_PrfForm_bc
            .Label49.BackColor = frm_PrfForm_bc
            .LblVeloMDMax_nom.BackColor = frm_PrfForm_bc
            .LblVeloMDAvg_nom.BackColor = frm_PrfForm_bc
            .LblVeloMDMin_nom.BackColor = frm_PrfForm_bc
            .LblVeloCDMax_nom.BackColor = frm_PrfForm_bc
            .LblVeloCDAvg_nom.BackColor = frm_PrfForm_bc
            .LblVeloCDMin_nom.BackColor = frm_PrfForm_bc
            .Label35.BackColor = frm_PrfForm_bc
            .Label38.BackColor = frm_PrfForm_bc
            .Label56.BackColor = frm_PrfForm_bc
            .Label55.BackColor = frm_PrfForm_bc
            .LblTSIMDMax_nom.BackColor = frm_PrfForm_bc
            .LblTSIMDAvg_nom.BackColor = frm_PrfForm_bc
            .LblTSIMDMin_nom.BackColor = frm_PrfForm_bc
            .LblTSICDMax_nom.BackColor = frm_PrfForm_bc
            .LblTSICDAvg_nom.BackColor = frm_PrfForm_bc
            .LblTSICDMin_nom.BackColor = frm_PrfForm_bc
        End With
    End Sub

    Public Sub set_meastextboxbc()
        With FrmSST4500_1_0_0J_meas
            'Meas TextBoxBC
            .TxtMachNoCur.BackColor = frm_MeasTextBox_bc
            .TxtMachNoBak.BackColor = frm_MeasTextBox_bc
            .TxtSmplNamCur.BackColor = frm_MeasTextBox_bc
            .TxtSmplNamBak.BackColor = frm_MeasTextBox_bc
            .TxtMarkCur.BackColor = frm_MeasTextBox_bc
            .TxtMarkBak.BackColor = frm_MeasTextBox_bc
            '.TxtMeasNumCur.BackColor = frm_MeasTextBox_bc
            '.TxtMeasNumBak.BackColor = frm_MeasTextBox_bc
        End With
    End Sub

    Public Sub set_prftextboxbc()
        With FrmSST4500_1_0_0J_Profile
            'Meas TextBoxBC
            .TxtMachNoCur.BackColor = frm_PrfTextBox_bc
            .TxtMachNoBak.BackColor = frm_PrfTextBox_bc
            .TxtSmplNamCur.BackColor = frm_PrfTextBox_bc
            .TxtSmplNamBak.BackColor = frm_PrfTextBox_bc
            .TxtMarkCur.BackColor = frm_PrfTextBox_bc
            .TxtMarkBak.BackColor = frm_PrfTextBox_bc
            '.TxtMeasNumCur.BackColor = frm_PrfTextBox_bc
            '.TxtMeasNumBak.BackColor = frm_PrfTextBox_bc
            '.TxtMeasLotCur.BackColor = frm_PrfTextBox_bc
            '.TxtMeasLotBak.BackColor = frm_PrfTextBox_bc
            .TxtLength.BackColor = frm_PrfTextBox_bc
            .TxtPitch.BackColor = frm_PrfTextBox_bc
            .TxtPoints.BackColor = frm_PrfTextBox_bc
        End With
    End Sub

    Public Sub set_meascmdbc()
        With FrmSST4500_1_0_0J_meas
            '.CmdMeas.BackColor = frm_MeasButton_bc
            .CmdMeas.BackColor = frm_MeasMeasButton_bc
            .CmdEtcMeasData.BackColor = frm_MeasButton_bc
            .CmdMeasSpecSel.BackColor = frm_MeasButton_bc
            .CmdMeasSpecSave.BackColor = frm_MeasButton_bc
            .CmdOldDataLoad.BackColor = frm_MeasButton_bc
            .CmdEtcOldMeasData.BackColor = frm_MeasButton_bc
            .CmdQuitSinglesheet.BackColor = frm_MeasButton_bc
            .CmdMeasPrint.BackColor = frm_MeasButton_bc
            .CmdMeasResultSave.BackColor = frm_MeasButton_bc
        End With
    End Sub

    Public Sub set_prfcmdbc()
        With FrmSST4500_1_0_0J_Profile
            '.CmdMeas.BackColor = frm_PrfButton_bc
            .CmdMeas.BackColor = frm_PrfMeasButton_bc
            .CmdMeasSpecSel.BackColor = frm_PrfButton_bc
            .CmdMeasSpecSave.BackColor = frm_PrfButton_bc
            .CmdOldDataLoad.BackColor = frm_PrfButton_bc
            .CmdClsGraph.BackColor = frm_PrfButton_bc
            .CmdAvg.BackColor = frm_PrfButton_bc
            .CmdQuitProfile.BackColor = frm_PrfButton_bc
            .CmdPrfPrint.BackColor = frm_PrfButton_bc
            .CmdPrfResultSave.BackColor = frm_PrfButton_bc
            .CmdAngleRange.BackColor = frm_PrfButton_bc
            .CmdVeloRange.BackColor = frm_PrfButton_bc
            .CmdTSIRange.BackColor = frm_PrfButton_bc
        End With
    End Sub

    Public Sub set_prfcmdfc()
        With FrmSST4500_1_0_0J_Profile
            '.CmdMeas.ForeColor = frm_PrfButton_fc
            .CmdMeas.ForeColor = frm_PrfMeasButton_fc
            .CmdMeasSpecSel.ForeColor = frm_PrfButton_fc
            .CmdMeasSpecSave.ForeColor = frm_PrfButton_fc
            .CmdOldDataLoad.ForeColor = frm_PrfButton_fc
            .CmdClsGraph.ForeColor = frm_PrfButton_fc
            .CmdAvg.ForeColor = frm_PrfButton_fc
            .CmdQuitProfile.ForeColor = frm_PrfButton_fc
            .CmdPrfPrint.ForeColor = frm_PrfButton_fc
            .CmdPrfResultSave.ForeColor = frm_PrfButton_fc
            .CmdAngleRange.ForeColor = frm_PrfButton_fc
            .CmdVeloRange.ForeColor = frm_PrfButton_fc
            .CmdTSIRange.ForeColor = frm_PrfButton_fc
        End With
    End Sub

    Public Sub set_meascmdfc()
        With FrmSST4500_1_0_0J_meas
            '.CmdMeas.ForeColor = frm_MeasButton_fc
            .CmdMeas.ForeColor = frm_MeasMeasButton_fc
            .CmdEtcMeasData.ForeColor = frm_MeasButton_fc
            .CmdMeasSpecSel.ForeColor = frm_MeasButton_fc
            .CmdMeasSpecSave.ForeColor = frm_MeasButton_fc
            .CmdOldDataLoad.ForeColor = frm_MeasButton_fc
            .CmdEtcOldMeasData.ForeColor = frm_MeasButton_fc
            .CmdQuitSinglesheet.ForeColor = frm_MeasButton_fc
            .CmdMeasPrint.ForeColor = frm_MeasButton_fc
            .CmdMeasResultSave.ForeColor = frm_MeasButton_fc
        End With
    End Sub

    Public Sub set_measolddatacolor()
        With FrmSST4500_1_0_0J_meas
            '過去データ色
            .LblMeasSpecBak.ForeColor = frm_MeasOldData_color
            .TxtMachNoBak.ForeColor = frm_MeasOldData_color
            .TxtSmplNamBak.ForeColor = frm_MeasOldData_color
            .TxtMarkBak.ForeColor = frm_MeasOldData_color
            .TxtMeasNumBak.ForeColor = frm_MeasOldData_color

            .Label30.ForeColor = frm_MeasOldData_color          '過去データ
            .LblMeasNumBak_adm.ForeColor = frm_MeasOldData_color
            .LblAnglPeakBak_adm.ForeColor = frm_MeasOldData_color
            .LblAnglDeepBak_adm.ForeColor = frm_MeasOldData_color
            .LblratioMDCDBak_adm.ForeColor = frm_MeasOldData_color
            .LblratioPKDPBak_adm.ForeColor = frm_MeasOldData_color
            .LblSpdMDBak_adm.ForeColor = frm_MeasOldData_color
            .LblSpdCDBak_adm.ForeColor = frm_MeasOldData_color
            .LblSpdPeakBak_adm.ForeColor = frm_MeasOldData_color
            .LblSpdDeepBak_adm.ForeColor = frm_MeasOldData_color
            .LblTSIMDBak_adm.ForeColor = frm_MeasOldData_color
            .LblTSICDBak_adm.ForeColor = frm_MeasOldData_color
            .Label56.ForeColor = frm_MeasOldData_color               '過去データ
            .LblMeasDatBak1_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak2_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak3_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak4_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak5_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak6_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak7_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak8_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak9_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak10_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak11_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak12_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak13_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak14_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak15_adm.ForeColor = frm_MeasOldData_color
            .LblMeasDatBak16_adm.ForeColor = frm_MeasOldData_color
        End With
    End Sub

    Public Sub set_prfcurdatacolor()
        With FrmSST4500_1_0_0J_Profile
            .LblMeasSpecCur.ForeColor = frm_PrfCurData_color
            .LblMeasSpecCur2.ForeColor = frm_PrfCurData_color
            .TxtMachNoCur.ForeColor = frm_PrfCurData_color
            .TxtSmplNamCur.ForeColor = frm_PrfCurData_color
            .TxtMarkCur.ForeColor = frm_PrfCurData_color
            .TxtMeasNumCur.ForeColor = frm_PrfCurData_color
            .TxtMeasLotCur.ForeColor = frm_PrfCurData_color
            .TxtLength.ForeColor = frm_PrfCurData_color
            .TxtPitch.ForeColor = frm_PrfCurData_color
            .TxtPoints.ForeColor = frm_PrfCurData_color
            .LblAnglePkMax_nom.ForeColor = frm_PrfCurData_color
            .LblAnglePkMaxCur_adm.ForeColor = frm_PrfCurData_color
            .LblAnglePkMax_TB.ForeColor = frm_PrfCurData_color
            .LblAnglePkAvg_nom.ForeColor = frm_PrfCurData_color
            .LblAnglePkAvgCur_adm.ForeColor = frm_PrfCurData_color
            .LblAnglePkAvg_TB.ForeColor = frm_PrfCurData_color
            .LblAnglePkMin_nom.ForeColor = frm_PrfCurData_color
            .LblAnglePkMinCur_adm.ForeColor = frm_PrfCurData_color
            .LblAnglePkMin_TB.ForeColor = frm_PrfCurData_color
            .LblAngleDpMax_nom.ForeColor = frm_PrfCurData_color
            .LblAngleDpMaxCur_adm.ForeColor = frm_PrfCurData_color
            .LblAngleDpMax_TB.ForeColor = frm_PrfCurData_color
            .LblAngleDpAvg_nom.ForeColor = frm_PrfCurData_color
            .LblAngleDpAvgCur_adm.ForeColor = frm_PrfCurData_color
            .LblAngleDpAvg_TB.ForeColor = frm_PrfCurData_color
            .LblAngleDpMin_nom.ForeColor = frm_PrfCurData_color
            .LblAngleDpMinCur_adm.ForeColor = frm_PrfCurData_color
            .LblAngleDpMin_TB.ForeColor = frm_PrfCurData_color
            .LblRatioPkDpMax_nom.ForeColor = frm_PrfCurData_color
            .LblRatioPkDpMaxCur_adm.ForeColor = frm_PrfCurData_color
            .LblRatioPkDpMax_TB.ForeColor = frm_PrfCurData_color
            .LblRatioPkDpAvg_nom.ForeColor = frm_PrfCurData_color
            .LblRatioPkDpAvgCur_adm.ForeColor = frm_PrfCurData_color
            .LblRatioPkDpAvg_TB.ForeColor = frm_PrfCurData_color
            .LblRatioPkDpMin_nom.ForeColor = frm_PrfCurData_color
            .LblRatioPkDpMinCur_adm.ForeColor = frm_PrfCurData_color
            .LblRatioPkDpMin_TB.ForeColor = frm_PrfCurData_color
            .LblRatioMDCDMax_nom.ForeColor = frm_PrfCurData_color
            .LblRatioMDCDMaxCur_adm.ForeColor = frm_PrfCurData_color
            .LblRatioMDCDMax_TB.ForeColor = frm_PrfCurData_color
            .LblRatioMDCDAvg_nom.ForeColor = frm_PrfCurData_color
            .LblRatioMDCDAvgCur_adm.ForeColor = frm_PrfCurData_color
            .LblRatioMDCDAvg_TB.ForeColor = frm_PrfCurData_color
            .LblRatioMDCDMin_nom.ForeColor = frm_PrfCurData_color
            .LblRatioMDCDMinCur_adm.ForeColor = frm_PrfCurData_color
            .LblRatioMDCDMin_TB.ForeColor = frm_PrfCurData_color
            .LblVeloPkMax_nom.ForeColor = frm_PrfCurData_color
            .LblVeloPkMaxCur_adm.ForeColor = frm_PrfCurData_color
            .LblVeloPkMax_TB.ForeColor = frm_PrfCurData_color
            .LblVeloPkAvg_nom.ForeColor = frm_PrfCurData_color
            .LblVeloPkAvgCur_adm.ForeColor = frm_PrfCurData_color
            .LblVeloPkAvg_TB.ForeColor = frm_PrfCurData_color
            .LblVeloPkMin_nom.ForeColor = frm_PrfCurData_color
            .LblVeloPkMinCur_adm.ForeColor = frm_PrfCurData_color
            .LblVeloPkMin_TB.ForeColor = frm_PrfCurData_color
            .LblVeloDpMax_nom.ForeColor = frm_PrfCurData_color
            .LblVeloDpMaxCur_adm.ForeColor = frm_PrfCurData_color
            .LblVeloDpMax_TB.ForeColor = frm_PrfCurData_color
            .LblVeloDpAvg_nom.ForeColor = frm_PrfCurData_color
            .LblVeloDpAvgCur_adm.ForeColor = frm_PrfCurData_color
            .LblVeloDpAvg_TB.ForeColor = frm_PrfCurData_color
            .LblVeloDpMin_nom.ForeColor = frm_PrfCurData_color
            .LblVeloDpMinCur_adm.ForeColor = frm_PrfCurData_color
            .LblVeloDpMin_TB.ForeColor = frm_PrfCurData_color
            .LblVeloMDMax_nom.ForeColor = frm_PrfCurData_color
            .LblVeloMDMaxCur_adm.ForeColor = frm_PrfCurData_color
            .LblVeloMDMax_TB.ForeColor = frm_PrfCurData_color
            .LblVeloMDAvg_nom.ForeColor = frm_PrfCurData_color
            .LblVeloMDAvgCur_adm.ForeColor = frm_PrfCurData_color
            .LblVeloMDAvg_TB.ForeColor = frm_PrfCurData_color
            .LblVeloMDMin_nom.ForeColor = frm_PrfCurData_color
            .LblVeloMDMinCur_adm.ForeColor = frm_PrfCurData_color
            .LblVeloMDMin_TB.ForeColor = frm_PrfCurData_color
            .LblVeloCDMax_nom.ForeColor = frm_PrfCurData_color
            .LblVeloCDMaxCur_adm.ForeColor = frm_PrfCurData_color
            .LblVeloCDMax_TB.ForeColor = frm_PrfCurData_color
            .LblVeloCDAvg_nom.ForeColor = frm_PrfCurData_color
            .LblVeloCDAvgCur_adm.ForeColor = frm_PrfCurData_color
            .LblVeloCDAvg_TB.ForeColor = frm_PrfCurData_color
            .LblVeloCDMin_nom.ForeColor = frm_PrfCurData_color
            .LblVeloCDMinCur_adm.ForeColor = frm_PrfCurData_color
            .LblVeloCDMin_TB.ForeColor = frm_PrfCurData_color
            .LblTSIMDMax_nom.ForeColor = frm_PrfCurData_color
            .LblTSIMDMaxCur_adm.ForeColor = frm_PrfCurData_color
            .LblTSIMDMax_TB.ForeColor = frm_PrfCurData_color
            .LblTSIMDAvg_nom.ForeColor = frm_PrfCurData_color
            .LblTSIMDAvgCur_adm.ForeColor = frm_PrfCurData_color
            .LblTSIMDAvg_TB.ForeColor = frm_PrfCurData_color
            .LblTSIMDMin_nom.ForeColor = frm_PrfCurData_color
            .LblTSIMDMinCur_adm.ForeColor = frm_PrfCurData_color
            .LblTSIMDMin_TB.ForeColor = frm_PrfCurData_color
            .LblTSICDMax_nom.ForeColor = frm_PrfCurData_color
            .LblTSICDMaxCur_adm.ForeColor = frm_PrfCurData_color
            .LblTSICDMax_TB.ForeColor = frm_PrfCurData_color
            .LblTSICDAvg_nom.ForeColor = frm_PrfCurData_color
            .LblTSICDAvgCur_adm.ForeColor = frm_PrfCurData_color
            .LblTSICDAvg_TB.ForeColor = frm_PrfCurData_color
            .LblTSICDMin_nom.ForeColor = frm_PrfCurData_color
            .LblTSICDMinCur_adm.ForeColor = frm_PrfCurData_color
            .LblTSICDMin_TB.ForeColor = frm_PrfCurData_color

            .Label1.ForeColor = frm_PrfCurData_color
            .Label28.ForeColor = frm_PrfCurData_color

            With FrmSST4500_1_0_0J_Profile.DataGridView1
                For i = 1 To 10
                    .Columns(i).DefaultCellStyle.ForeColor = frm_PrfCurData_color
                Next
                .DefaultCellStyle.SelectionForeColor = frm_PrfCurData_color
            End With
        End With
    End Sub

    Public Sub set_prfolddatacolor()
        With FrmSST4500_1_0_0J_Profile
            .LblMeasSpecBak.ForeColor = frm_PrfOldData_color
            .LblMeasSpecBak2.ForeColor = frm_PrfOldData_color
            .TxtMachNoBak.ForeColor = frm_PrfOldData_color
            .TxtSmplNamBak.ForeColor = frm_PrfOldData_color
            .TxtMarkBak.ForeColor = frm_PrfOldData_color
            .TxtMeasNumBak.ForeColor = frm_PrfOldData_color
            .TxtMeasLotBak.ForeColor = frm_PrfOldData_color
            .TxtLengthOld.ForeColor = frm_PrfOldData_color
            .TxtPitchOld.ForeColor = frm_PrfOldData_color
            .TxtPointsOld.ForeColor = frm_PrfOldData_color
            .LblAnglePkMaxBak_adm.ForeColor = frm_PrfOldData_color
            .LblAnglePkMaxOld_TB.ForeColor = frm_PrfOldData_color
            .LblAnglePkAvgBak_adm.ForeColor = frm_PrfOldData_color
            .LblAnglePkAvgOld_TB.ForeColor = frm_PrfOldData_color
            .LblAnglePkMinBak_adm.ForeColor = frm_PrfOldData_color
            .LblAnglePkMinOld_TB.ForeColor = frm_PrfOldData_color
            .LblAngleDpMaxBak_adm.ForeColor = frm_PrfOldData_color
            .LblAngleDpMaxOld_TB.ForeColor = frm_PrfOldData_color
            .LblAngleDpAvgBak_adm.ForeColor = frm_PrfOldData_color
            .LblAngleDpAvgOld_TB.ForeColor = frm_PrfOldData_color
            .LblAngleDpMinBak_adm.ForeColor = frm_PrfOldData_color
            .LblAngleDpMinOld_TB.ForeColor = frm_PrfOldData_color
            .LblRatioPkDpMaxBak_adm.ForeColor = frm_PrfOldData_color
            .LblRatioPkDpMaxOld_TB.ForeColor = frm_PrfOldData_color
            .LblRatioPkDpAvgBak_adm.ForeColor = frm_PrfOldData_color
            .LblRatioPkDpAvgOld_TB.ForeColor = frm_PrfOldData_color
            .LblRatioPkDpMinBak_adm.ForeColor = frm_PrfOldData_color
            .LblRatioPkDpMinOld_TB.ForeColor = frm_PrfOldData_color
            .LblRatioMDCDMaxBak_adm.ForeColor = frm_PrfOldData_color
            .LblRatioMDCDMaxOld_TB.ForeColor = frm_PrfOldData_color
            .LblRatioMDCDAvgBak_adm.ForeColor = frm_PrfOldData_color
            .LblRatioMDCDAvgOld_TB.ForeColor = frm_PrfOldData_color
            .LblRatioMDCDMinBak_adm.ForeColor = frm_PrfOldData_color
            .LblRatioMDCDMinOld_TB.ForeColor = frm_PrfOldData_color
            .LblVeloPkMaxBak_adm.ForeColor = frm_PrfOldData_color
            .LblVeloPkMaxOld_TB.ForeColor = frm_PrfOldData_color
            .LblVeloPkAvgBak_adm.ForeColor = frm_PrfOldData_color
            .LblVeloPkAvgOld_TB.ForeColor = frm_PrfOldData_color
            .LblVeloPkMinBak_adm.ForeColor = frm_PrfOldData_color
            .LblVeloPkMinOld_TB.ForeColor = frm_PrfOldData_color
            .LblVeloDpMaxBak_adm.ForeColor = frm_PrfOldData_color
            .LblVeloDpMaxOld_TB.ForeColor = frm_PrfOldData_color
            .LblVeloDpAvgBak_adm.ForeColor = frm_PrfOldData_color
            .LblVeloDpAvgOld_TB.ForeColor = frm_PrfOldData_color
            .LblVeloDpMinBak_adm.ForeColor = frm_PrfOldData_color
            .LblVeloDpMinOld_TB.ForeColor = frm_PrfOldData_color
            .LblVeloMDMaxBak_adm.ForeColor = frm_PrfOldData_color
            .LblVeloMDMaxOld_TB.ForeColor = frm_PrfOldData_color
            .LblVeloMDAvgBak_adm.ForeColor = frm_PrfOldData_color
            .LblVeloMDAvgOld_TB.ForeColor = frm_PrfOldData_color
            .LblVeloMDMinBak_adm.ForeColor = frm_PrfOldData_color
            .LblVeloMDMinOld_TB.ForeColor = frm_PrfOldData_color
            .LblVeloCDMaxBak_adm.ForeColor = frm_PrfOldData_color
            .LblVeloCDMaxOld_TB.ForeColor = frm_PrfOldData_color
            .LblVeloCDAvgBak_adm.ForeColor = frm_PrfOldData_color
            .LblVeloCDAvgOld_TB.ForeColor = frm_PrfOldData_color
            .LblVeloCDMinBak_adm.ForeColor = frm_PrfOldData_color
            .LblVeloCDMinOld_TB.ForeColor = frm_PrfOldData_color
            .LblTSIMDMaxBak_adm.ForeColor = frm_PrfOldData_color
            .LblTSIMDMaxOld_TB.ForeColor = frm_PrfOldData_color
            .LblTSIMDAvgBak_adm.ForeColor = frm_PrfOldData_color
            .LblTSIMDAvgOld_TB.ForeColor = frm_PrfOldData_color
            .LblTSIMDMinBak_adm.ForeColor = frm_PrfOldData_color
            .LblTSIMDMinOld_TB.ForeColor = frm_PrfOldData_color
            .LblTSICDMaxBak_adm.ForeColor = frm_PrfOldData_color
            .LblTSICDMaxOld_TB.ForeColor = frm_PrfOldData_color
            .LblTSICDAvgBak_adm.ForeColor = frm_PrfOldData_color
            .LblTSICDAvgOld_TB.ForeColor = frm_PrfOldData_color
            .LblTSICDMinBak_adm.ForeColor = frm_PrfOldData_color
            .LblTSICDMinOld_TB.ForeColor = frm_PrfOldData_color

            .Label27.ForeColor = frm_PrfOldData_color
            .Label2.ForeColor = frm_PrfOldData_color

            With FrmSST4500_1_0_0J_Profile.DataGridView2
                For i = 1 To 10
                    .Columns(i).DefaultCellStyle.ForeColor = frm_PrfOldData_color
                Next
                .DefaultCellStyle.SelectionForeColor = frm_PrfOldData_color
            End With
        End With
    End Sub

    Public Sub set_prfavgdatacolor()
        With FrmSST4500_1_0_0J_Profile
            .LblAnglePkMaxAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblAnglePkMaxAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblAnglePkAvgAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblAnglePkAvgAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblAnglePkMinAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblAnglePkMinAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblAngleDpMaxAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblAngleDpMaxAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblAngleDpAvgAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblAngleDpAvgAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblAngleDpMinAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblAngleDpMinAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblRatioPkDpMaxAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblRatioPkDpMaxAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblRatioPkDpAvgAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblRatioPkDpAvgAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblRatioPkDpMinAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblRatioPkDpMinAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblRatioMDCDMaxAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblRatioMDCDMaxAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblRatioMDCDAvgAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblRatioMDCDAvgAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblRatioMDCDMinAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblRatioMDCDMinAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblVeloPkMaxAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblVeloPkMaxAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblVeloPkAvgAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblVeloPkAvgAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblVeloPkMinAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblVeloPkMinAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblVeloDpMaxAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblVeloDpMaxAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblVeloDpAvgAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblVeloDpAvgAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblVeloDpMinAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblVeloDpMinAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblVeloMDMaxAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblVeloMDMaxAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblVeloMDAvgAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblVeloMDAvgAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblVeloMDMinAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblVeloMDMinAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblVeloCDMaxAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblVeloCDMaxAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblVeloCDAvgAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblVeloCDAvgAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblVeloCDMinAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblVeloCDMinAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblTSIMDMaxAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblTSIMDMaxAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblTSIMDAvgAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblTSIMDAvgAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblTSIMDMinAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblTSIMDMinAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblTSICDMaxAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblTSICDMaxAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblTSICDAvgAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblTSICDAvgAvg_TB.ForeColor = frm_PrfAvgData_color
            .LblTSICDMinAvg_adm.ForeColor = frm_PrfAvgData_color
            .LblTSICDMinAvg_TB.ForeColor = frm_PrfAvgData_color

            .Label25.ForeColor = frm_PrfAvgData_color
            .Label26.ForeColor = frm_PrfAvgData_color

            With FrmSST4500_1_0_0J_Profile.DataGridView3
                For i = 1 To 10
                    .Columns(i).DefaultCellStyle.ForeColor = frm_PrfAvgData_color
                Next
                .DefaultCellStyle.SelectionForeColor = frm_PrfAvgData_color
            End With
        End With
    End Sub

    Public Sub set_meascurdatacolor()
        With FrmSST4500_1_0_0J_meas
            .LblMeasSpecCur.ForeColor = frm_MeasCurData_color
            .TxtMachNoCur.ForeColor = frm_MeasCurData_color
            .TxtSmplNamCur.ForeColor = frm_MeasCurData_color
            .TxtMarkCur.ForeColor = frm_MeasCurData_color
            .TxtMeasNumCur.ForeColor = frm_MeasCurData_color

            .Label91.ForeColor = frm_MeasCurData_color
            .LblMeasNumCur_nom.ForeColor = frm_MeasCurData_color
            .LblMeasNumCur_adm.ForeColor = frm_MeasCurData_color
            .LblAnglPeakCur_nom.ForeColor = frm_MeasCurData_color
            .LblAnglPeakCur_adm.ForeColor = frm_MeasCurData_color
            .LblAnglDeepCur_nom.ForeColor = frm_MeasCurData_color
            .LblAnglDeepCur_adm.ForeColor = frm_MeasCurData_color
            .LblratioMDCDCur_nom.ForeColor = frm_MeasCurData_color
            .LblratioMDCDCur_adm.ForeColor = frm_MeasCurData_color
            .LblratioPKDPCur_nom.ForeColor = frm_MeasCurData_color
            .LblratioPKDPCur_adm.ForeColor = frm_MeasCurData_color
            .LblSpdMDCur_nom.ForeColor = frm_MeasCurData_color
            .LblSpdMDCur_adm.ForeColor = frm_MeasCurData_color
            .LblSpdCDCur_nom.ForeColor = frm_MeasCurData_color
            .LblSpdCDCur_adm.ForeColor = frm_MeasCurData_color
            .LblSpdPeakCur_nom.ForeColor = frm_MeasCurData_color
            .LblSpdPeakCur_adm.ForeColor = frm_MeasCurData_color
            .LblSpdDeepCur_nom.ForeColor = frm_MeasCurData_color
            .LblSpdDeepCur_adm.ForeColor = frm_MeasCurData_color
            .LblTSIMDCur_nom.ForeColor = frm_MeasCurData_color
            .LblTSIMDCur_adm.ForeColor = frm_MeasCurData_color
            .LblTSICDCur_nom.ForeColor = frm_MeasCurData_color
            .LblTSICDCur_adm.ForeColor = frm_MeasCurData_color

            .Label53.ForeColor = frm_MeasCurData_color
            .Label205.ForeColor = frm_MeasCurData_color
            .Label29.ForeColor = frm_MeasCurData_color

            .LblMeasDatCur1_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur2_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur3_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur4_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur5_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur6_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur7_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur8_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur9_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur10_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur11_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur12_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur13_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur14_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur15_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur16_nom.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur1_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur2_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur3_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur4_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur5_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur6_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur7_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur8_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur9_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur10_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur11_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur12_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur13_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur14_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur15_adm.ForeColor = frm_MeasCurData_color
            .LblMeasDatCur16_adm.ForeColor = frm_MeasCurData_color

        End With
    End Sub

    Public Sub set_prfformfc()
        With FrmSST4500_1_0_0J_Profile
            .GbPrfSpec.ForeColor = frm_PrfForm_fc
            .GroupBox1.ForeColor = frm_PrfForm_fc
            .GbPrint.ForeColor = frm_PrfForm_fc
            .GroupBox3.ForeColor = frm_PrfForm_fc
            .GroupBox2.ForeColor = frm_PrfForm_fc

            .LblProductNamePrf.ForeColor = frm_PrfForm_fc
            .LblPrfTitle.ForeColor = frm_PrfForm_fc
            .Label5.ForeColor = frm_PrfForm_fc
            .Label6.ForeColor = frm_PrfForm_fc
            .Label7.ForeColor = frm_PrfForm_fc
            .Label3.ForeColor = frm_PrfForm_fc
            .LblSmp_len.ForeColor = frm_PrfForm_fc
            .LblPitch_num.ForeColor = frm_PrfForm_fc
            .LblAllMeas_num.ForeColor = frm_PrfForm_fc
            .OptMm.ForeColor = frm_PrfForm_fc
            .OptInch.ForeColor = frm_PrfForm_fc

        End With
    End Sub

    Public Sub set_measformfc()
        With FrmSST4500_1_0_0J_meas

            .GbMeasSpec.ForeColor = frm_MeasForm_fc
            .GroupBox5.ForeColor = frm_MeasForm_fc
            .GbPrint.ForeColor = frm_MeasForm_fc
            .GroupBox1.ForeColor = frm_MeasForm_fc
            .GroupBox2.ForeColor = frm_MeasForm_fc
            .ChkMeasAutoPrn.ForeColor = frm_MeasForm_fc

            .LblProductNameMeas.ForeColor = frm_MeasForm_fc
            .Label2.ForeColor = frm_MeasForm_fc                 'シングルシート
            .Label5.ForeColor = frm_MeasForm_fc                 'マシーンNo.
            .Label6.ForeColor = frm_MeasForm_fc                 'サンプル名
            .Label7.ForeColor = frm_MeasForm_fc                 '測定回数
            .Label73.ForeColor = frm_MeasForm_fc
            .Label74.ForeColor = frm_MeasForm_fc                'データ
            .Label75.ForeColor = frm_MeasForm_fc                '測定回数
            .Label76.ForeColor = frm_MeasForm_fc                '配向角[deg.]
            .Label78.ForeColor = frm_MeasForm_fc                '配向比
            .Label79.ForeColor = frm_MeasForm_fc                'Peak
            .Label80.ForeColor = frm_MeasForm_fc                'Deep
            .Label81.ForeColor = frm_MeasForm_fc                'MD/CD
            .Label82.ForeColor = frm_MeasForm_fc                'Peak/Deep
            .Label69.ForeColor = frm_MeasForm_fc
            .Label83.ForeColor = frm_MeasForm_fc
            .Label85.ForeColor = frm_MeasForm_fc
            .Label90.ForeColor = frm_MeasForm_fc
            .Label52.ForeColor = frm_MeasForm_fc
            .Label87.ForeColor = frm_MeasForm_fc
            .Label52.ForeColor = frm_MeasForm_fc
            .Label51.ForeColor = frm_MeasForm_fc
            .Label50.ForeColor = frm_MeasForm_fc
            .Label8.ForeColor = frm_MeasForm_fc
            .Label9.ForeColor = frm_MeasForm_fc
            .Label18.ForeColor = frm_MeasForm_fc
            .Label12.ForeColor = frm_MeasForm_fc
            .Label13.ForeColor = frm_MeasForm_fc
            .Label11.ForeColor = frm_MeasForm_fc
            .Label14.ForeColor = frm_MeasForm_fc
            .Label15.ForeColor = frm_MeasForm_fc
            .Label16.ForeColor = frm_MeasForm_fc
            .Label23.ForeColor = frm_MeasForm_fc
            .Label24.ForeColor = frm_MeasForm_fc
            .Label17.ForeColor = frm_MeasForm_fc
            .Label25.ForeColor = frm_MeasForm_fc
            .Label26.ForeColor = frm_MeasForm_fc
            .Label21.ForeColor = frm_MeasForm_fc
            .Label27.ForeColor = frm_MeasForm_fc
            .Label28.ForeColor = frm_MeasForm_fc

            .Label54.ForeColor = frm_MeasForm_fc
            .Label55.ForeColor = frm_MeasForm_fc
            .Label57.ForeColor = frm_MeasForm_fc
            .Label58.ForeColor = frm_MeasForm_fc
            .Label59.ForeColor = frm_MeasForm_fc
            .Label60.ForeColor = frm_MeasForm_fc
            .Label61.ForeColor = frm_MeasForm_fc
            .Label62.ForeColor = frm_MeasForm_fc
            .Label63.ForeColor = frm_MeasForm_fc
            .Label64.ForeColor = frm_MeasForm_fc
            .Label65.ForeColor = frm_MeasForm_fc
            .Label66.ForeColor = frm_MeasForm_fc
            .Label67.ForeColor = frm_MeasForm_fc
            .Label68.ForeColor = frm_MeasForm_fc
            .Label69.ForeColor = frm_MeasForm_fc
            .Label70.ForeColor = frm_MeasForm_fc
            .Label71.ForeColor = frm_MeasForm_fc
            .Label72.ForeColor = frm_MeasForm_fc
            .Label203.ForeColor = frm_MeasForm_fc
            .Label204.ForeColor = frm_MeasForm_fc
            .Label207.ForeColor = frm_MeasForm_fc
            .Label208.ForeColor = frm_MeasForm_fc
            .Label209.ForeColor = frm_MeasForm_fc
            .Label212.ForeColor = frm_MeasForm_fc
            .Label213.ForeColor = frm_MeasForm_fc
            .Label214.ForeColor = frm_MeasForm_fc
            .Label215.ForeColor = frm_MeasForm_fc
            .Label210.ForeColor = frm_MeasForm_fc
            .Label216.ForeColor = frm_MeasForm_fc
            .Label211.ForeColor = frm_MeasForm_fc
            .Label217.ForeColor = frm_MeasForm_fc
            .Label218.ForeColor = frm_MeasForm_fc
            .Label219.ForeColor = frm_MeasForm_fc
            .Label220.ForeColor = frm_MeasForm_fc
            .Label221.ForeColor = frm_MeasForm_fc
            .Label222.ForeColor = frm_MeasForm_fc
        End With
    End Sub

    Public Sub mainform_color_init()

        With FrmSST4500_1_0_0J_main
            .BackColor = frm_MainForm_bc

            .MenuStrip1.BackColor = frm_MainMenu_bc
            .StatusStrip1.BackColor = frm_MainStatus_bc

            set_maincmdbc()
            set_maincmdfc()

            If frm_MainButton_bc = SystemColors.Control Then
                .CmdSinglesheet.UseVisualStyleBackColor = True
                .CmdCutSheetProfile.UseVisualStyleBackColor = True
                .CmdProfile.UseVisualStyleBackColor = True
                .CmdAdmin.UseVisualStyleBackColor = True
                .CmdMDlong.UseVisualStyleBackColor = True
                .CmdTest.UseVisualStyleBackColor = True
                .CmdQuitSplash.UseVisualStyleBackColor = True
            End If

            .LblProductNameMenu.ForeColor = frm_MainForm_fc

            set_mainmenufc()

            .ToolStripStatusLabel1.ForeColor = frm_MainStatus_fc
            .ToolStripStatusLabel2.ForeColor = frm_MainStatus_fc
            .ToolStripStatusLabel3.ForeColor = frm_MainStatus_fc

        End With
    End Sub

    Public Sub set_maincmdbc()
        With FrmSST4500_1_0_0J_main
            .CmdSinglesheet.BackColor = frm_MainButton_bc
            .CmdCutSheetProfile.BackColor = frm_MainButton_bc
            .CmdProfile.BackColor = frm_MainButton_bc
            .CmdAdmin.BackColor = frm_MainButton_bc
            .CmdMDlong.BackColor = frm_MainButton_bc
            .CmdTest.BackColor = frm_MainButton_bc
            .CmdQuitSplash.BackColor = frm_MainButton_bc
        End With
    End Sub

    Public Sub set_maincmdfc()
        With FrmSST4500_1_0_0J_main
            .CmdSinglesheet.ForeColor = frm_MainButton_fc
            .CmdCutSheetProfile.ForeColor = frm_MainButton_fc
            .CmdProfile.ForeColor = frm_MainButton_fc
            .CmdAdmin.ForeColor = frm_MainButton_fc
            .CmdMDlong.ForeColor = frm_MainButton_fc
            .CmdTest.ForeColor = frm_MainButton_fc
            .CmdQuitSplash.ForeColor = frm_MainButton_fc
        End With
    End Sub

    Public Sub set_measmenufc()
        With FrmSST4500_1_0_0J_meas
            .MenuStrip1.ForeColor = frm_MeasMenu_fc
            .測定仕様ToolStripMenuItem.ForeColor = frm_MeasMenu_fc
            .選択ToolStripMenuItem.ForeColor = frm_MeasMenu_fc
            .保存ToolStripMenuItem.ForeColor = frm_MeasMenu_fc
            .過去データToolStripMenuItem.ForeColor = frm_MeasMenu_fc
            .読込ToolStripMenuItem.ForeColor = frm_MeasMenu_fc
            .他の測定データ選択ToolStripMenuItem.ForeColor = frm_MeasMenu_fc
            .終了ToolStripMenuItem.ForeColor = frm_MeasMenu_fc
            .測定開始ToolStripMenuItem.ForeColor = frm_MeasMenu_fc
            .他の測定データ選択ToolStripMenuItem1.ForeColor = frm_MeasMenu_fc
            .印刷ToolStripMenuItem.ForeColor = frm_MeasMenu_fc
            .手動印刷ToolStripMenuItem.ForeColor = frm_MeasMenu_fc
            .保存ToolStripMenuItem1.ForeColor = frm_MeasMenu_fc
            .設定ToolStripMenuItem1.ForeColor = frm_MeasMenu_fc
        End With
    End Sub

    Public Sub set_prfmenufc()
        With FrmSST4500_1_0_0J_Profile
            .MenuStrip1.ForeColor = frm_PrfMenu_fc
            .測定仕様ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .選択ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .保存ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .過去データToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .読込ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .終了ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .測定開始ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .測定中断ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .グラフ消去ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .平均値ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .自動印刷ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .印刷項目ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .配向角配向比ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .伝播速度TSIToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .測定データ表ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .過去データ表ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .平均値データ表ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .手動印刷ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .保存ToolStripMenuItem1.ForeColor = frm_PrfMenu_fc
            .設定ToolStripMenuItem1.ForeColor = frm_PrfMenu_fc
            .単位ToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .MmToolStripMenuItem.ForeColor = frm_PrfMenu_fc
            .InchToolStripMenuItem.ForeColor = frm_PrfMenu_fc
        End With
    End Sub

    Public Sub set_mainmenufc()
        With FrmSST4500_1_0_0J_main
            .MenuStrip1.ForeColor = frm_MainMenu_fc
            .シングルシートToolStripMenuItem.ForeColor = frm_MainMenu_fc
            .カットシートToolStripMenuItem.ForeColor = frm_MainMenu_fc
            .プロファイルToolStripMenuItem.ForeColor = frm_MainMenu_fc
            .終了ToolStripMenuItem.ForeColor = frm_MainMenu_fc
            .管理者ログインToolStripMenuItem.ForeColor = frm_MainMenu_fc
            .設定ToolStripMenuItem1.ForeColor = frm_MainMenu_fc
            .MD長尺測定ToolStripMenuItem1.ForeColor = frm_MainMenu_fc
            .試験調整ToolStripMenuItem.ForeColor = frm_MainMenu_fc
        End With
    End Sub

    Public Sub mainform_borderstyle_init()

        With FrmSST4500_1_0_0J_colorsetting
            With .CbxFrmMainStatusBoderStyle
                'テキストボックス部分を編集不可にする
                .DropDownStyle = ComboBoxStyle.DropDownList

                .Items.Clear()

                .Items.Add("Adjust")
                .Items.Add("Bump")
                .Items.Add("Etched")
                .Items.Add("Flat")
                .Items.Add("Raised")
                .Items.Add("RaisedInner")
                .Items.Add("RaisedOuter")
                .Items.Add("Sunken")
                .Items.Add("SunkenInner")
                .Items.Add("SunkenOuter")
            End With

            Select Case frm_MainStatusBorder_stl
                Case Border3DStyle.Adjust
                    .CbxFrmMainStatusBoderStyle.SelectedIndex = 0
                Case Border3DStyle.Bump
                    .CbxFrmMainStatusBoderStyle.SelectedIndex = 1
                Case Border3DStyle.Etched
                    .CbxFrmMainStatusBoderStyle.SelectedIndex = 2
                Case Border3DStyle.Flat
                    .CbxFrmMainStatusBoderStyle.SelectedIndex = 3
                Case Border3DStyle.Raised
                    .CbxFrmMainStatusBoderStyle.SelectedIndex = 4
                Case Border3DStyle.RaisedInner
                    .CbxFrmMainStatusBoderStyle.SelectedIndex = 5
                Case Border3DStyle.RaisedOuter
                    .CbxFrmMainStatusBoderStyle.SelectedIndex = 6
                Case Border3DStyle.Sunken
                    .CbxFrmMainStatusBoderStyle.SelectedIndex = 7
                Case Border3DStyle.SunkenInner
                    .CbxFrmMainStatusBoderStyle.SelectedIndex = 8
                Case Border3DStyle.SunkenOuter
                    .CbxFrmMainStatusBoderStyle.SelectedIndex = 9
            End Select
        End With

        With FrmSST4500_1_0_0J_main
            .ToolStripStatusLabel1.BorderStyle = frm_MainStatusBorder_stl
            .ToolStripStatusLabel2.BorderStyle = frm_MainStatusBorder_stl
            .ToolStripStatusLabel3.BorderStyle = frm_MainStatusBorder_stl
        End With
    End Sub

    Public Sub measform_borderstyle_init()

        With FrmSST4500_1_0_0J_colorsetting
            With .CbxFrmMeasStatusBoderStyle
                'テキストボックス部分を編集不可にする
                .DropDownStyle = ComboBoxStyle.DropDownList

                .Items.Clear()

                .Items.Add("Adjust")
                .Items.Add("Bump")
                .Items.Add("Etched")
                .Items.Add("Flat")
                .Items.Add("Raised")
                .Items.Add("RaisedInner")
                .Items.Add("RaisedOuter")
                .Items.Add("Sunken")
                .Items.Add("SunkenInner")
                .Items.Add("SunkenOuter")
            End With

            Select Case frm_MeasStatusBorder_stl
                Case Border3DStyle.Adjust
                    .CbxFrmMeasStatusBoderStyle.SelectedIndex = 0
                Case Border3DStyle.Bump
                    .CbxFrmMeasStatusBoderStyle.SelectedIndex = 1
                Case Border3DStyle.Etched
                    .CbxFrmMeasStatusBoderStyle.SelectedIndex = 2
                Case Border3DStyle.Flat
                    .CbxFrmMeasStatusBoderStyle.SelectedIndex = 3
                Case Border3DStyle.Raised
                    .CbxFrmMeasStatusBoderStyle.SelectedIndex = 4
                Case Border3DStyle.RaisedInner
                    .CbxFrmMeasStatusBoderStyle.SelectedIndex = 5
                Case Border3DStyle.RaisedOuter
                    .CbxFrmMeasStatusBoderStyle.SelectedIndex = 6
                Case Border3DStyle.Sunken
                    .CbxFrmMeasStatusBoderStyle.SelectedIndex = 7
                Case Border3DStyle.SunkenInner
                    .CbxFrmMeasStatusBoderStyle.SelectedIndex = 8
                Case Border3DStyle.SunkenOuter
                    .CbxFrmMeasStatusBoderStyle.SelectedIndex = 9
            End Select
        End With

        With FrmSST4500_1_0_0J_meas
            .ToolStripStatusLabel1.BorderStyle = frm_MeasStatusBorder_stl
            .ToolStripStatusLabel2.BorderStyle = frm_MeasStatusBorder_stl
            .ToolStripStatusLabel3.BorderStyle = frm_MeasStatusBorder_stl
        End With
    End Sub

    Public Sub prfform_borderstyle_init()

        With FrmSST4500_1_0_0J_colorsetting
            With .CbxFrmPrfStatusBoderStyle
                'テキストボックス部分を編集不可にする
                .DropDownStyle = ComboBoxStyle.DropDownList

                .Items.Clear()

                .Items.Add("Adjust")
                .Items.Add("Bump")
                .Items.Add("Etched")
                .Items.Add("Flat")
                .Items.Add("Raised")
                .Items.Add("RaisedInner")
                .Items.Add("RaisedOuter")
                .Items.Add("Sunken")
                .Items.Add("SunkenInner")
                .Items.Add("SunkenOuter")
            End With

            Select Case frm_PrfStatusBorder_stl
                Case Border3DStyle.Adjust
                    .CbxFrmPrfStatusBoderStyle.SelectedIndex = 0
                Case Border3DStyle.Bump
                    .CbxFrmPrfStatusBoderStyle.SelectedIndex = 1
                Case Border3DStyle.Etched
                    .CbxFrmPrfStatusBoderStyle.SelectedIndex = 2
                Case Border3DStyle.Flat
                    .CbxFrmPrfStatusBoderStyle.SelectedIndex = 3
                Case Border3DStyle.Raised
                    .CbxFrmPrfStatusBoderStyle.SelectedIndex = 4
                Case Border3DStyle.RaisedInner
                    .CbxFrmPrfStatusBoderStyle.SelectedIndex = 5
                Case Border3DStyle.RaisedOuter
                    .CbxFrmPrfStatusBoderStyle.SelectedIndex = 6
                Case Border3DStyle.Sunken
                    .CbxFrmPrfStatusBoderStyle.SelectedIndex = 7
                Case Border3DStyle.SunkenInner
                    .CbxFrmPrfStatusBoderStyle.SelectedIndex = 8
                Case Border3DStyle.SunkenOuter
                    .CbxFrmPrfStatusBoderStyle.SelectedIndex = 9
            End Select
        End With

        With FrmSST4500_1_0_0J_Profile
            .ToolStripStatusLabel1.BorderStyle = frm_PrfStatusBorder_stl
            .ToolStripStatusLabel2.BorderStyle = frm_PrfStatusBorder_stl
            .ToolStripStatusLabel3.BorderStyle = frm_PrfStatusBorder_stl
        End With
    End Sub

    Public Sub UsbClose()
        Try
            FT_Close(lngHandle)
        Catch ex As System.DllNotFoundException
            FlgFTDLLerr = 1
            MessageBox.Show("FTD2XX.DLLが読み込めませんでした。" & vbCrLf &
                            "D2XX Driverがインストールされているか確認して下さい。" & vbCrLf &
                            "TESTモードで起動します。",
                            "USBエラー",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function UsbOpen() As Integer
        UsbClose()

        If FlgFTDLLerr = 0 Then
            '互換性のため"SST4000a"とする
            'ftStatus = FT_OpenEx("SST4000a", FT_OPEN_BY_DESCRIPTION, lngHandle)
            ftStatus = FT_OpenEx("SST4000a", FT_OPEN_BY_SERIAL_NUMBER, lngHandle)
            If ftStatus <> FT_OK Then
                Return 0
            End If

            ftStatus = FT_SetBaudRate(lngHandle, FT_BAUD_9600)
            If ftStatus <> FT_OK Then
                Return 0
            End If

            ftStatus = FT_SetDataCharacteristics(lngHandle, FT_DATA_BITS_7, FT_STOP_BITS_1, FT_PARITY_NONE)
            If ftStatus <> FT_OK Then
                Return 0
            End If

            ftStatus = FT_SetFlowControl(lngHandle, FT_FLOW_NONE, 0, 0)
            If ftStatus <> FT_OK Then
                Return 0
            End If

            ftStatus = FT_Purge(lngHandle, FT_PURGE_RX)
            If ftStatus <> FT_OK Then
                Return 0
            End If

            ftStatus = FT_Purge(lngHandle, FT_PURGE_TX)
            If ftStatus <> FT_OK Then
                Return 0
            End If

            ftStatus = FT_SetTimeouts(lngHandle, 300, 30)
            If ftStatus <> FT_OK Then
                Return 0
            End If

            Return 1
        Else
            Return -1
        End If

    End Function

    Public Function UsbWrite(strWdata) As Integer
        Dim Wln As Long
        Dim Ln As Long
        Wln = Len(strWdata)
        ftStatus = FT_Write_String(lngHandle, strWdata, Wln, Ln)
        If ftStatus <> FT_OK Then
            UsbWrite = 1
        Else
            UsbWrite = 0
        End If
    End Function

    Public Function UsbRead(ByRef strRxdata As String) As Integer
        Dim TempStringData As String

        ftStatus = FT_GetQueueStatus(lngHandle, FT_RxQ_Bytes)
        If ftStatus <> FT_OK Then
            Return 2
        End If

        flTimedout = False
        flFatalError = False
        strTotalReadBuffer = ""
        lngTotalBytesRead = 0
        TempStringData = Space(FT_RxQ_Bytes + 1)
        Do
            lngBytesRead = 0
            'ftStatus = FT_Read_String(lngHandle, strTotalReadBuffer, 5, lngBytesRead)
            ftStatus = FT_Read_String(lngHandle, TempStringData, FT_RxQ_Bytes, lngBytesRead)
            'ftStatus = FT_Read_String(lngHandle, strTotalReadBuffer, FT_RxQ_Bytes, lngBytesRead)
            If (ftStatus = FT_OK) Or (ftStatus = FT_IO_ERROR) Then
                If lngBytesRead > 0 Then
                    'strTotalReadBuffer = strTotalReadBuffer & Left(strTotalReadBuffer, lngBytesRead)
                    strTotalReadBuffer &= Left(TempStringData, lngBytesRead)
                    lngTotalBytesRead += +lngBytesRead
                Else
                    flTimedout = True
                End If
            Else
                flFatalError = True
            End If
        Loop Until (Right(strTotalReadBuffer, 1) = vbCr) Or (flTimedout = True) Or (flFatalError = True)

        If (flTimedout = False) And (flFatalError = False) Then
            strRxdata = Left(strTotalReadBuffer, lngTotalBytesRead)
            Return 0
            flFailed = False
        ElseIf flTimedout = True Then
            Return 1
        Else
            Return 2
        End If
    End Function

    Function LoadDefConstName(ByRef fname As String, ByVal deffname As Boolean) As DialogResult
        'ソフト起動時に実行
        'Dim curdir As String
        'CurDir = Directory.GetCurrentDirectory

        '測定仕様ファイルの選択
        Using Dialog As New OpenFileDialog
            With Dialog
                .InitialDirectory = cur_dir & DEF_CONST_FILE_FLD
                .Title = "測定仕様ファイルの選択"
                .CheckFileExists = True

                Select Case FlgProfile
                    Case 0
                        .Filter = "Constant File(SG*.cns)|SG*.cns"
                        If deffname = True Then
                            .FileName = DEF_CONST_FILE_NAME_SG 'デフォルト表示ファイル名 ※全てが表示されない　スクロールされた状態???
                        Else
                            .FileName = ""
                        End If
                    Case 1
                        .Filter = "Constant File(PF*.cns)|PF*.cns"
                        If deffname = True Then
                            .FileName = DEF_CONST_FILE_NAME_PF
                        Else
                            .FileName = ""
                        End If
                    Case 2
                        .Filter = "Constant File(CT*.cns)|CT*.cns"
                        If deffname = True Then
                            .FileName = DEF_CONST_FILE_NAME_CT
                        Else
                            .FileName = ""
                        End If
                    Case 3
                        .Filter = "Constant File(LG*.cns)|LG*.cns"
                        If deffname = True Then
                            .FileName = DEF_CONST_FILE_NAME_LG
                        Else
                            .FileName = ""
                        End If
                    Case 4
                        .Filter = "Constant File(PF*.cns)|PF*.cns"
                        If deffname = True Then
                            .FileName = DEF_CONST_FILE_NAME_PF
                        Else
                            .FileName = ""
                        End If
                End Select

                LoadDefConstName = .ShowDialog

                fname = .FileName
                StrConstFilePath = fname
            End With
        End Using

    End Function

    Function LoadOldDataName(ByRef fname As String) As DialogResult
        'ソフト起動時に実行
        'カレントディレクトリ取得
        'Dim curdir As String
        'CurDir = Directory.GetCurrentDirectory

        Using dialog As New OpenFileDialog
            With dialog
                .InitialDirectory = cur_dir & DEF_DATA_FILE_FLD

                .Title = "過去の測定データ選択"
                .CheckFileExists = True

                If FlgDBF = 0 Then
                    Select Case FlgProfile
                        Case 0
                            .Filter = "Meas Data Old File(SG*.csv)|SG*.csv"
                        Case 1
                            .Filter = "Meas Data Old File(PF*.csv)|PF*.csv"
                        Case 2
                            .Filter = "Meas Data Old File(CT*.csv)|CT*.csv"
                        Case 3
                            .Filter = "Meas Data Old File(LG*.csv)|LG*.csv"
                    End Select
                Else
                    Select Case FlgProfile
                        'プロファイル、MD長尺はCで統一
                        Case 0
                            .Filter = "Meas Data Old File(*_S_*.csv)|*_S_*.csv"
                        Case 1
                            .Filter = "Meas Data Old File(*_C_*.csv)|*_C_*.csv"
                        Case 2
                            .Filter = "Meas Data Old File(*_C_*.csv)|*_C_*.csv"
                        Case 3
                            .Filter = "Meas Data Old File(*_C_*.csv)|*_C_*.csv"
                    End Select
                End If

                LoadOldDataName = .ShowDialog

                fname = .FileName

            End With
        End Using
    End Function

    Function LoadData() As Integer
        Dim M As Integer
        Dim N As Integer
        Dim Ds(14) As String
        Dim Dn(25) As Single
        Dim i As Integer
        Dim data_len As Integer
        Dim data_len_tmp As Integer

        M = 0

        Dim txtParser As FileIO.TextFieldParser =
            New FileIO.TextFieldParser(StrFileName, Encoding.GetEncoding("Shift_jis"))

        txtParser.TextFieldType = FileIO.FieldType.Delimited

        txtParser.SetDelimiters(",")

        Dim splittedResult As String()

        'タイトル行は読み捨てる
        splittedResult = txtParser.ReadFields()

        While Not txtParser.EndOfData
            'タイトル行の最初の要素に"Machine No."の文字列があるかないか
            If M = 0 Then
                If splittedResult(0) <> "Machine No." And splittedResult(0) <> "Sample Name" Then
                    Return -2
                    Exit Function
                End If
            End If

            M += 1
            splittedResult = txtParser.ReadFields()

            If FlgDBF = 0 Then
                data_len = 29   '30個または32個
            Else
                data_len = 26   '従来データ(SST2500)も読み込み出来るようにする為
            End If

            data_len_tmp = UBound(splittedResult)   'UBoundは最終要素Indexを示す為データ数-1
            If data_len_tmp < data_len Then
                'データ数が揃っていない場合は破損データとして扱う
                '新データにサンプル長とピッチ数が追加される
                If M = 1 Then
                    Return -1
                    Exit Function
                End If

            Else
                If FlgDBF = 0 Then
                    '測定データフォーマット通常
                    For i = 0 To 6
                        'Ds(1) = マシーンNo.
                        'Ds(2) = Sample Name(サンプル名)
                        'Ds(3) = マーク
                        'Ds(4) = BW
                        'Ds(5) = No.(測定回数)
                        'Ds(6) = Date(測定日)
                        'Ds(7) = Time(測定時間)
                        Ds(i + 1) = splittedResult(i)
                    Next
                    Ds(8) = splittedResult(10)      'Angle-Deep
                    Ds(9) = splittedResult(11)      'Angle-Peak
                    Dn(1) = Val(splittedResult(7))  'Points
                    Dn(2) = Val(splittedResult(8))  'MD/CD
                    Dn(3) = Val(splittedResult(9))  'Peak/Deep

                    Dn(6) = Val(splittedResult(12)) 'Deep
                    Dn(7) = Val(splittedResult(13)) 'Peak
                    For i = 0 To 15
                        'Dn(8)～Dn(23) = 0deg～168.75deg
                        Dn(i + 8) = Val(splittedResult(i + 14))
                    Next

                    For N = 1 To 9
                        DataFileStr(FileNo, M, N) = Ds(N)
                    Next
                    DataDate = Ds(6)
                    DataDate_bak = DataDate
                    DataTime = Ds(7)
                    DataTime_bak = DataTime

                    DataFileNum(FileNo, M, 19) = Dn(2)
                    DataFileStr(FileNo, M, 10) = Str(Dn(2))
                    DataFileNum(FileNo, M, 20) = Dn(3)
                    DataFileStr(FileNo, M, 11) = Str(Dn(3))

                    For N = 1 To 18
                        DataFileNum(FileNo, M, N) = Dn(N + 5)
                    Next
                    FileDataNo = M
                    FileDataMax = M

                    If data_len_tmp > data_len Then
                        LengthOld = splittedResult(30)
                        PitchOld = splittedResult(31)
                        FlgPchExpMes_old = splittedResult(32)
                    Else
                        LengthOld = 0
                        PitchOld = 0
                        FlgPchExpMes_old = 0
                    End If
                Else
                    '測定データフォーマット特殊
                    'Ds(1) = splittedResult(29)  'Machine No.
                    Ds(2) = splittedResult(0)   'Sample Name
                    Ds(3) = splittedResult(1)   'Mark
                    'Ds(4) = splittedResult(30)  'BW
                    Ds(5) = Trim(Strings.Right(splittedResult(2), Len(splittedResult(2)) - 2))   'No.
                    Ds(6) = splittedResult(3)   'Date
                    Ds(7) = splittedResult(4)   'Time

                    Ds(8) = splittedResult(6)   'Angle-Deep = Or.Angle CD
                    Ds(9) = splittedResult(5)   'Angle-Peak = Or.Angle MD
                    'Dn(1) = splittedResult(31)  'Points
                    Dn(2) = Val(splittedResult(7))  'MD/CD
                    Dn(3) = Val(splittedResult(8))  'Peak/Deep

                    Dn(6) = Val(splittedResult(10)) 'Deep
                    Dn(7) = Val(splittedResult(9))  'Peak
                    For i = 0 To 15
                        'Dn(8)～Dn(23) = 0deg～168.75deg
                        Dn(i + 8) = Val(splittedResult(i + 11))
                    Next

                    For N = 1 To 9
                        DataFileStr(FileNo, M, N) = Ds(N)
                    Next
                    DataDate = Ds(6)
                    DataDate_bak = DataDate
                    DataTime = Ds(7)
                    DataTime_bak = DataTime

                    DataFileNum(FileNo, M, 19) = Dn(2)
                    DataFileStr(FileNo, M, 10) = Str(Dn(2))
                    DataFileNum(FileNo, M, 20) = Dn(3)
                    DataFileStr(FileNo, M, 11) = Str(Dn(3))

                    For N = 1 To 18
                        DataFileNum(FileNo, M, N) = Dn(N + 5)
                    Next
                    FileDataNo = M
                    FileDataMax = M

                    'If data_len_tmp > data_len Then
                    'LengthOld = splittedResult(27)
                    'PitchOld = splittedResult(28)
                    'Ds(1) = splittedResult(29)
                    'Ds(4) = splittedResult(30)
                    'Dn(1) = splittedResult(31)
                    'FlgPchExpMes_old = splittedResult(32)
                    'Else
                    'LengthOld = 0
                    'PitchOld = 0
                    'FlgPchExpMes_old = 0
                    'End If
                    'LengthOld = 0
                    'PitchOld = 0
                    'FlgPchExpMes_old = 0
                    LoadData_tokusyu()
                End If
            End If
        End While

        LoadData = M

    End Function

    Public Sub LoadData_tokusyu()
        Dim _add_filename As String
        Dim _StrFileName_tmp As String
        Dim splittedResult As String()
        Dim ret As Boolean

        _StrFileName_tmp = Path.GetFileNameWithoutExtension(StrFileName)

        _add_filename = cur_dir & DEF_DATA_FILE_FLD & _StrFileName_tmp & ".add"
        ret = File.Exists(_add_filename)

        If ret = True Then
            Dim txtParser As FileIO.TextFieldParser =
            New FileIO.TextFieldParser(_add_filename, Encoding.GetEncoding("Shift_jis"))

            txtParser.TextFieldType = FileIO.FieldType.Delimited
            txtParser.SetDelimiters(",")

            splittedResult = txtParser.ReadFields()
            Dim i As Integer
            i = 0

            While Not txtParser.EndOfData
                i += 1
                splittedResult = txtParser.ReadFields()
                LengthOld = splittedResult(2)
                PitchOld = splittedResult(3)
                FlgPchExpMes_old = splittedResult(4)
            End While

        Else
            LengthOld = 0
            PitchOld = 0
            FlgPchExpMes_old = 0
        End If
    End Sub

    Public Sub SaveConst(ByVal fpath As String)
        Using sw As New StreamWriter(fpath, False, Encoding.UTF8)
            If FlgProfile = 1 Then
                sw.WriteLine(MachineNo & "," & Sample & "," &
                             Mark & "," & BW & "," &
                             DataDate & "," & DataTime & "," &
                             FlgProfile & "," & Length & "," &
                             Pitch & "," & Points & "," &
                             FlgInch & "," & FlgPrfDisplay & "," &
                             FlgMeasAutoPrn & "," & FlgPrfAutoPrn & "," &
                             FlgPrfPrint & "," & FlgAlternate & "," &
                             FlgVelocityRange & "," & FlgAngleRange & "," &
                             FlgPkCenterAngle & "," & FlgDpCenterAngle & "," &
                             FlgPitchExp & "," & PchExpSettingFile_FullPath)
            Else
                sw.WriteLine(MachineNo & "," & Sample & "," &
                             Mark & "," & BW & "," &
                             DataDate & "," & DataTime & "," &
                             FlgProfile & "," & Length & "," &
                             Pitch & "," & Points & "," &
                             FlgInch & "," & FlgPrfDisplay & "," &
                             FlgMeasAutoPrn & "," & FlgPrfAutoPrn & "," &
                             FlgPrfPrint & "," & FlgAlternate & "," &
                             FlgVelocityRange & "," & FlgAngleRange & "," &
                             FlgPkCenterAngle & "," & FlgDpCenterAngle & "," &
                             FlgPitchExp)
            End If
        End Using
    End Sub

    Public Sub MakeDisplayData()
        Dim M As Integer
        Dim N As Integer

        For M = 1 To FileDataMax
            For N = 1 To 11
                DataPrcStr(3, M, N) = DataFileStr(FileNo, M, N)
            Next

            For N = 1 To 20
                DataPrcNum(3, M, N) = DataFileNum(FileNo, M, N)
            Next
        Next
    End Sub

    Public Sub ConstChangeTrue(ByVal this_form As Form, ByVal title_text As String)
        Dim _filename2 As String

        FlgConstChg = True
        '変更があった事を示すためにタイトルに"*"をつける
        _filename2 = Path.GetFileNameWithoutExtension(StrConstFileName)
        this_form.Text = title_text & " (" & _filename2 & " *)"

    End Sub

    Public Sub LoadConst(ByVal this_form As Form, ByVal title_text As String)
        Dim _filename2 As String
        _filename2 = Path.GetFileNameWithoutExtension(StrConstFileName)
        this_form.Text = title_text & " (" & _filename2 & ")"
        FlgConstChg = False '変更無し状態に初期化

        Dim txtParser As FileIO.TextFieldParser =
            New FileIO.TextFieldParser(StrConstFileName, Encoding.GetEncoding("Shift_jis"))

        txtParser.TextFieldType = FileIO.FieldType.Delimited

        txtParser.SetDelimiters(",")

        Dim splittedResult As String() = txtParser.ReadFields()
        Dim const_len As Integer = UBound(splittedResult)

        MachineNo = splittedResult(0)
        Sample = splittedResult(1)
        Mark = splittedResult(2)
        BW = splittedResult(3)
        DataDate = splittedResult(4)
        DataTime = splittedResult(5)    '既存はHH:mm NEWはHH:mm:ss
        FlgProfile = splittedResult(6)
        Length = splittedResult(7)
        Pitch = splittedResult(8)
        Points = splittedResult(9)
        FlgInch = splittedResult(10)
        FlgPrfDisplay = splittedResult(11)
        FlgMeasAutoPrn = splittedResult(12)
        FlgPrfAutoPrn = splittedResult(13)
        FlgPrfPrint = splittedResult(14)
        FlgAlternate = splittedResult(15)
        FlgVelocityRange = splittedResult(16)
        FlgAngleRange = splittedResult(17)
        FlgPkCenterAngle = splittedResult(18)
        FlgDpCenterAngle = splittedResult(19)
        If const_len >= 20 Then
            If FlgPchExp_Visible = 1 Then
                FlgPitchExp = splittedResult(20)
            Else
                FlgPitchExp = 0
            End If

            If const_len = 20 Then
                PchExpSettingFile_FullPath = ""
                PchExpSettingFile = ""
            ElseIf const_len = 21 Then
                PchExpSettingFile_FullPath = splittedResult(21)
                PchExpSettingFile = Path.GetFileName(PchExpSettingFile_FullPath)
            End If
        Else
            FlgPitchExp = 0
            PchExpSettingFile_FullPath = ""
            PchExpSettingFile = ""
        End If
        SetConst()
    End Sub

    Public Sub LoadConstPitch(ByVal _filepath As String)
        'ファイルの有無を調べてなければ保存を実行したときに新規に作成する
        'constファイルのファイル名+ "_pitch"とする
        Dim _filename_const As String
        Dim _pathname_const As String
        Dim _filename_pchexp_full As String
        Dim ret As Boolean
        Dim txtParser As FileIO.TextFieldParser
        Dim splittedResult As String()
        Dim result_tmp As DialogResult

        If _filepath = "" Then
            'ファイル名が空欄だったら
            '古い形式のconsファイルの可能性でconsファイル名.pitchの場合が
            'あるため、それで調べる
            _filename_const = Path.GetFileNameWithoutExtension(StrConstFileName)
            _pathname_const = Path.GetDirectoryName(StrConstFileName)
            _filename_pchexp_full = _pathname_const & "\" & _filename_const & StrConstFileName_PchExp
            PchExpSettingFile = Path.GetFileName(_filename_pchexp_full)
            PchExpSettingFile_FullPath = _filename_pchexp_full
            ret = File.Exists(_filename_pchexp_full)

        Else
            'ファイル名が空欄でなければ
            _filename_pchexp_full = _filepath
            ret = File.Exists(_filename_pchexp_full)

        End If

        If ret = True Then
            'ファイルが存在するときのみ読み込みを実行する
            txtParser = New FileIO.TextFieldParser(_filename_pchexp_full, Encoding.GetEncoding("Shift_jis"))
            txtParser.TextFieldType = FileIO.FieldType.Delimited
            txtParser.SetDelimiters(",")
            splittedResult = txtParser.ReadFields()

            For i = 0 To UBound(splittedResult)
                If i = 0 Then
                    '1つ目はサンプル長
                    PchExp_Length = Val(splittedResult(i))
                ElseIf i = 1 Then
                    ReDim PchExp_PchData(i - 1)
                    PchExp_PchData(i - 1) = Val(splittedResult(i))
                Else
                    ReDim Preserve PchExp_PchData(UBound(PchExp_PchData) + 1)
                    PchExp_PchData(i - 1) = Val(splittedResult(i))
                End If
            Next
            FlgPitchExp_Load = 1
        Else
            'ファイルがない場合はピッチ拡張設定画面を開く
            '設定を保存せずにファイル作成をキャンセルした場合は、
            '拡張のチェックボックスるをfalseにする
            'ピッチ拡張を無効にする
            FlgPitchExp_Load = 0
            result_tmp = MessageBox.Show("ピッチ拡張設定ファイルが未作成の様です。" & vbCrLf &
                                         "作成して、ピッチ拡張設定を有効にしますか？" & vbCrLf &
                                         "はい : ピッチ拡張設定画面を開く" & vbCrLf &
                                         "いいえ : ピッチ拡張設定を無効にする",
                                         "確認",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information)
            If result_tmp = vbYes Then
                'FlgPitchExp = 1
                FrmSST4500_1_0_0J_pitchsetting.Visible = True
            Else
                FlgPitchExp = 0
                FrmSST4500_1_0_0J_Profile.ChkPitchExp.Checked = False
            End If
        End If
    End Sub

    Public Sub SetConst()
        If FlgProfile = 0 Then
            With FrmSST4500_1_0_0J_meas
                .TxtMachNoCur.Text = MachineNo

                If FlgDBF = 0 Then
                    If BW = "" Then
                        '新フォーマットファイルはSampleのみ表示
                        .TxtSmplNamCur.Text = Sample
                    Else
                        '従来のフォーマットファイルはカンマ区切りで表示
                        .TxtSmplNamCur.Text = Sample & "," & BW
                    End If
                    .TxtMarkCur.Text = Mark
                Else
                    .TxtSmplNamCur.Text = Sample
                    .TxtMarkCur.Text = Mark
                    'BWは無視
                End If

                .TxtMeasNumCur.Text = "0"
                If FlgMeasAutoPrn = 0 Then
                    .ChkMeasAutoPrn.Checked = False
                Else
                    .ChkMeasAutoPrn.Checked = True
                End If
            End With
        Else
            PkAngCent = FlgPkCenterAngle

            With FrmSST4500_1_0_0J_Profile
                .TxtMachNoCur.Text = MachineNo

                If FlgDBF = 0 Then
                    If BW = "" Then
                        '新フォーマットファイルはSampleのみ表示
                        .TxtSmplNamCur.Text = Sample
                    Else
                        '従来のフォーマットファイルはカンマ区切りで表示
                        .TxtSmplNamCur.Text = Sample & "," & BW
                    End If
                    .TxtMarkCur.Text = Mark
                Else
                    .TxtSmplNamCur.Text = Sample
                    .TxtMarkCur.Text = Mark
                End If

                .TxtMeasNumCur.Text = "0"
                If FlgPrfAutoPrn = 0 Then
                    .ChkPrfAutoPrn.Checked = False
                Else
                    .ChkPrfAutoPrn.Checked = True
                End If

                Select Case FlgProfile
                    Case 1  'プロファイル
                        .OptMm.Visible = True
                        .OptInch.Visible = True
                        If FlgInch = 1 Then
                            .OptMm.Checked = False
                            .OptInch.Checked = True
                        Else
                            .OptMm.Checked = True
                            .OptInch.Checked = False
                        End If
                        .LblSmp_len.Visible = True
                        .TxtLength.Visible = True
                        .TxtLength.Enabled = True
                        .LblPitch_num.Visible = True
                        .TxtPitch.Visible = True
                        If FlgPitchExp = 1 Then
                            .ChkPitchExp.Checked = True
                            .TxtPitch.Enabled = False
                            .TxtPoints.Enabled = False
                            .TxtLength.Enabled = False
                            .OptMm.Enabled = False
                            .OptInch.Enabled = False
                            .単位ToolStripMenuItem.Enabled = False
                        Else
                            .ChkPitchExp.Checked = False
                            .TxtPitch.Enabled = True
                            .TxtPoints.Enabled = True
                            .TxtLength.Enabled = True
                            .OptMm.Enabled = True
                            .OptInch.Enabled = True
                            .単位ToolStripMenuItem.Enabled = True
                        End If
                        .LblAllMeas_num.Visible = True
                        .TxtPoints.Visible = True
                        '.TxtPoints.Enabled = True
                        .TxtLength.Text = Length
                        .TxtPitch.Text = Pitch
                        .TxtPoints.Text = Points
                        If FlgPchExp_Visible = 1 Then
                            .ChkPitchExp.Visible = True
                            .LblPitchExp.Visible = True
                        Else
                            .ChkPitchExp.Visible = False
                            .LblPitchExp.Visible = False
                        End If
                        If FlgAdmin <> 0 Then
                            .TxtLengthOld.Visible = True
                            .TxtPitchOld.Visible = True
                            .TxtPointsOld.Visible = True
                        End If
                    Case 2  'カットシートプロファイル
                        .OptMm.Visible = False
                        .OptInch.Visible = False
                        .OptMm.Checked = False
                        .OptInch.Checked = False
                        .LblSmp_len.Visible = False
                        .TxtLength.Visible = False
                        .TxtLengthOld.Visible = False
                        .LblPitch_num.Visible = False
                        .TxtPitch.Visible = False
                        .TxtPitchOld.Visible = False
                        .LblAllMeas_num.Visible = True
                        .TxtPoints.Visible = True
                        .TxtPoints.Visible = True
                        .TxtPoints.Text = Points
                        .ChkPitchExp.Visible = False
                        .LblPitchExp.Visible = False
                        FlgInch = 0
                    Case 3  'MD長尺サンプル
                        .OptMm.Visible = True
                        .OptInch.Visible = True
                        If FlgInch = 1 Then
                            .OptMm.Checked = False
                            .OptInch.Checked = True
                        Else
                            .OptMm.Checked = True
                            .OptInch.Checked = False
                        End If
                        .LblSmp_len.Visible = False
                        .TxtLength.Visible = False
                        .TxtLengthOld.Visible = False
                        .LblPitch_num.Visible = True
                        .TxtPitch.Visible = True
                        .LblAllMeas_num.Visible = False
                        .TxtPoints.Visible = False
                        .TxtPointsOld.Visible = False
                        .TxtPitch.Text = Pitch
                        .ChkPitchExp.Visible = False
                        .LblPitchExp.Visible = False
                        If FlgAdmin <> 0 Then
                            .TxtPitchOld.Visible = True
                        End If
                End Select
            End With
        End If

        SampleNo = 0
    End Sub

    Public Sub ClsData()
        Dim N As Integer
        Dim M As Long

        For KdData = 1 To 3
            For M = 1 To 200
                For N = 1 To 11
                    DataPrcStr(KdData, M, N) = ""
                Next
                For N = 1 To 20
                    DataPrcNum(KdData, M, N) = 0
                Next
            Next
        Next
    End Sub

    Public Sub OpenDataFile()
        'ソフト起動時に実行済み
        'Dim curdir As String
        'CurDir = Directory.GetCurrentDirectory

        Dim Sa As String = ""
        'Dim Mark As String = ""
        'Dim f_chk As Boolean
        'Dim StrDataFileName_tmp As String
        'Dim file_count As Integer

        If FlgDBF = 0 Then
            Select Case FlgProfile
                Case 0
                    Sa = "SG_0_"
                Case 1
                    Sa = "PF_" & Trim(Str(Points)) & "_"
                Case 2
                    Sa = "CT_" & Trim(Str(Points)) & "_"
                Case 3
                    Sa = "LG_X_"
            End Select
            StrDataFileName = Sa & Trim(MachineNo) & "_" & Trim(Sample) & "_" & FileDate & "_" & FileTime & ".csv"
        Else
            Select Case FlgProfile
                'プロファイル、MD長尺の認識なしすべてC
                Case 0  'シングルモード
                    Sa = "S"
                Case 1  'プロファイルモード
                    Sa = "C"
                Case 2  'カットシートモード
                    Sa = "C"
                Case 3  'MD長尺モード
                    Sa = "C"
            End Select
            StrDataFileName = Trim(Sample) & "_" & Mark & "_" & Sa & "_" & FileDate & "_" & FileTime & ".csv"
        End If

        '同名ファイルが作成されるタイミングでは操作しないとのこと。
        '最悪は追記されるだけなので動作が止まることはない。
        ''同名のファイルが存在するかチェックする
        'f_chk = File.Exists(cur_dir & DEF_DATA_FILE_FLD & StrDataFileName)
        'Console.WriteLine(f_chk)
        'file_count = 0
        'StrDataFileName_tmp = IO.Path.GetFileNameWithoutExtension(StrDataFileName)
        'While f_chk = True
        'file_count += 1
        'StrDataFileName = StrDataFileName_tmp & "(" & file_count & ")" & ".csv"
        'f_chk = File.Exists(cur_dir & DEF_DATA_FILE_FLD & StrDataFileName)
        'End While
        '空のファイルを作成する
        Using sw As New StreamWriter(cur_dir & DEF_DATA_FILE_FLD & StrDataFileName, True, Encoding.UTF8)

        End Using

        If FlgDBF = 1 Then
            Using sw As New StreamWriter(cur_dir & DEF_CONST_FILE_FLD & StrDataFileName & Dbf_add_filename, True, Encoding.UTF8)

            End Using
        End If
    End Sub

    Public Sub DataFileRename(ByVal _flgProfile As String,
                              ByVal _cur_dir As String,
                              ByVal _points As Long,
                              ByVal _machineno As String,
                              ByVal _sample As String,
                              ByVal _filedate As String,
                              ByVal _filetime As String)
        Dim Sa As String = ""
        Dim NewStrDataFileName As String

        'FlgDBF = 1の場合は、ファイル名に測定回数が含まれないため何もせず抜ける
        If FlgDBF = 1 Then
            Exit Sub
        End If

        Select Case _flgProfile
            Case 0
                Sa = "SG_1_"
            Case 1
                Sa = "PF_" & _points & "_"
            Case 2
                Sa = "CT_" & _points & "_"
            Case 3
                Sa = "LG_" & _points & "_"
        End Select

        NewStrDataFileName = Sa & _machineno & "_" & Sample & "_" & _filedate & "_" & _filetime & ".csv"

        Debug.Print("DataFileRename Run...")
        Debug.Print("CurFileNmae: " & StrDataFileName)
        Debug.Print("NewFileName: " & NewStrDataFileName)

        If StrDataFileName <> NewStrDataFileName Then
            My.Computer.FileSystem.RenameFile(_cur_dir & DEF_DATA_FILE_FLD & StrDataFileName, NewStrDataFileName)
            StrDataFileName = NewStrDataFileName
        End If
    End Sub

    Public Sub SaveDataTitle()
        'ソフト起動時に実行済み
        'OpenDataFile()実行後にStrDataFileNameが確定していること
        'Dim curdir As String
        'CurDir = Directory.GetCurrentDirectory

        Using sw As New StreamWriter(cur_dir & DEF_DATA_FILE_FLD & StrDataFileName, True, Encoding.UTF8)
            If FlgDBF = 0 Then
                '標準データ仕様
                sw.WriteLine("Machine No.," &   '0 Ds(1)
                             "Sample Name," &   '1 Ds(2)
                             "Mark," &          '2 Ds(3)
                             "B/W," &           '3 Ds(4)
                             "No.," &           '4 Ds(5)
                             "Date," &          '5 Ds(6)
                             "Time," &          '6 Ds(7)
                             "Points," &        '7 Dn(1)
                             "MD/CD," &         '8 Dn(2)
                             "Peak/Deep," &     '9 Dn(3)
                             "Angle-Deep," &    '10 Ds(8)
                             "Angle-Peak," &    '11 Ds(9)
                             "Deep," &          '12 Dn(6)
                             "Peak," &          '13 Dn(7)
                             "0," &             '14 Dn(8)
                             "11.25," &         '15 Dn(9)
                             "22.5," &          '16 Dn(10)
                             "33.75," &         '17 Dn(11)
                             "45," &            '18 Dn(12)
                             "56.25," &         '19 Dn(13)
                             "67.5," &          '20 Dn(14)
                             "78.75," &         '21 Dn(15)
                             "90," &            '22 Dn(16)
                             "101.25," &        '23 Dn(17)
                             "112.5," &         '24 Dn(18)
                             "123.75," &        '25 Dn(19)
                             "135," &           '26 Dn(20)
                             "146.25," &        '27 Dn(21)
                             "157.5," &         '28 Dn(22)
                             "168.75," &        '29 Dn(23)
                             "Length," &        '30 Dn(24)
                             "Pitch," &         '31 Dn(25)
                             "PchExp")          '32 Dn(26)   
            Else
                '特定顧客向けデータ仕様(特殊1)
                sw.WriteLine("Sample Name," &       '0 Ds(2)
                             "Mark," &              '1 Ds(3)
                             "No.," &               '2 Ds(5)
                             "Date," &              '3 Ds(6)
                             "Time," &              '4 Ds(7)
                             "Or.Angle MD," &       '5 Ds(9)
                             "Or.Angle CD," &       '6 Ds(8)
                             "MD/CD Ratio," &       '7 Dn(2)
                             "Peak/Deep Ratio," &   '8 Dn(3)
                             "Peak," &              '9 Dn(7)
                             "Deep," &              '10 Dn(6)
                             "0 deg.," &            '11 Dn(8)
                             "11.25 deg.," &        '12 Dn(9)
                             "22.50 deg.," &        '13 Dn(10)
                             "33.75 deg.," &        '14 Dn(11)
                             "45.00 deg.," &        '15 Dn(12)
                             "56.25 deg.," &        '16 Dn(13)
                             "67.50 deg.," &        '17 Dn(14)
                             "78.75 deg.," &        '18 Dn(15)
                             "90.00 deg.," &        '19 Dn(16)
                             "101.25 deg.," &       '20 Dn(17)
                             "112.50 deg.," &       '21 Dn(18)
                             "123.75 deg.," &       '22 Dn(19)
                             "135.00 deg.," &       '23 Dn(20)
                             "146.25 deg.," &       '24 Dn(21)
                             "157.50 deg.," &       '25 Dn(22)
                             "166.75 deg.")         '26 Dn(22)
                '"168.75 deg.," &       '26 Dn(23)
                '"Length," &            '27 Dn(24)
                '"Pitch," &             '28 Dn(25)
                '"Machine No.," &       '29 Ds(1)
                '"B/W," &               '30 Dn(4)
                '"Points," &            '31 Dn(1)
                '"PchExp")              '32 Dn(26)
            End If
        End Using

        If FlgDBF = 1 Then
            '測定データフォーマット特殊仕様時の未格納データの追加保存
            Using sw As New StreamWriter(cur_dir & DEF_DATA_FILE_FLD & Path.GetFileNameWithoutExtension(StrDataFileName) & ".add", True, Encoding.UTF8)
                sw.WriteLine("Sample Name," &
                             "Mark," &
                             "Length," &
                             "Pitch," &
                             "PchExp")
            End Using
        End If

    End Sub

    Public Sub SaveData()
        'ソフト起動時に実行済み
        'OpenDataFile(),SaveDataTitle()実行後
        'Dim curdir As String
        'CurDir = Directory.GetCurrentDirectory
        Dim Sa As String = ""

        Dim N As Integer
        Dim Ds(13) As String
        Dim Dn(28) As Single

        For N = 1 To 5
            Ds(N) = DataPrcStr(1, SampleNo, N)
        Next

        Ds(6) = DataDate
        Ds(7) = DataTime
        Ds(8) = DataPrcStr(1, SampleNo, 8)  'angle CD
        Ds(9) = DataPrcStr(1, SampleNo, 9)  'angle MD
        If FlgDBF = 1 Then
            Ds(8) = Single.Parse(Ds(8).Substring(2)).ToString
            Ds(9) = Single.Parse(Ds(9).Substring(2)).ToString
        End If

        Dn(1) = Points
        Dn(2) = DataPrcNum(1, SampleNo, 3) / DataPrcNum(1, SampleNo, 11)    'm/c ratio
        Dn(3) = DataPrcNum(1, SampleNo, 2) / DataPrcNum(1, SampleNo, 1)     'p/d ratio

        For N = 1 To 18
            Dn(N + 5) = DataPrcNum(1, SampleNo, N)
        Next

        If FlgInch = 1 Then
            Dn(24) = Math.Round(Length / 25.4)
            Dn(25) = Math.Round(Pitch / 25.4)
        Else
            Dn(24) = Length
            Dn(25) = Pitch
        End If

        'ピッチ拡張有無　測定フラグ
        Dn(26) = FlgPchExpMes

        Using sw As New StreamWriter(cur_dir & DEF_DATA_FILE_FLD & StrDataFileName, True, Encoding.UTF8)
            If FlgDBF = 0 Then
                sw.WriteLine(Ds(1) & "," &              'Machine No.
                             Ds(2) & "," &              'Sample Name
                             Ds(3) & "," &              'Mark
                             Ds(4) & "," &              'B/W
                             Ds(5) & "," &              'No.
                             Ds(6) & "," &              'Date
                             Ds(7) & "," &              'Time
                             Dn(1).ToString & "," &     'Points
                             Dn(2).ToString & "," &     'MD/CD
                             Dn(3).ToString & "," &     'Peak/Deep
                             Ds(8) & "," &              'Angle-Deep
                             Ds(9) & "," &              'Angle-Peak
                             Dn(6).ToString & "," &     'Deep
                             Dn(7).ToString & "," &     'Peak
                             Dn(8).ToString & "," &     '0
                             Dn(9).ToString & "," &     '11.25
                             Dn(10).ToString & "," &    '22.5
                             Dn(11).ToString & "," &    '33.75
                             Dn(12).ToString & "," &    '45
                             Dn(13).ToString & "," &    '56.25
                             Dn(14).ToString & "," &    '67.5
                             Dn(15).ToString & "," &    '78.75
                             Dn(16).ToString & "," &    '90
                             Dn(17).ToString & "," &    '101.25
                             Dn(18).ToString & "," &    '112.5
                             Dn(19).ToString & "," &    '123.75
                             Dn(20).ToString & "," &    '135
                             Dn(21).ToString & "," &    '146.25
                             Dn(22).ToString & "," &    '157.5
                             Dn(23).ToString & "," &    '168.75
                             Dn(24).ToString & "," &    'Length
                             Dn(25).ToString & "," &    'Pitch
                             Dn(26).ToString)           'PchExp
            Else
                Select Case FlgProfile
                    Case 0
                        Sa = "S_"
                    Case 1
                        Sa = "P_"
                    Case 2
                        Sa = "C_"
                    Case 3
                        Sa = "L_"
                End Select
                sw.WriteLine(Ds(2) & "," &              'Sample Name
                             Ds(3) & "," &              'Mark
                             Sa & Ds(5) & "," &         'No.
                             Ds(6) & "," &              'Date
                             Ds(7) & "," &              'Time
                             Ds(9) & "," &              'Angle-Peak
                             Ds(8) & "," &              'Angle-Deep
                             Dn(2).ToString & "," &     'MD/CD
                             Dn(3).ToString & "," &     'Peak/Deep
                             Dn(7).ToString & "," &     'Peak
                             Dn(6).ToString & "," &     'Deep
                             Dn(8).ToString & "," &     '0
                             Dn(9).ToString & "," &     '11.25
                             Dn(10).ToString & "," &    '22.5
                             Dn(11).ToString & "," &    '33.75
                             Dn(12).ToString & "," &    '45
                             Dn(13).ToString & "," &    '56.25
                             Dn(14).ToString & "," &    '67.5
                             Dn(15).ToString & "," &    '78.75
                             Dn(16).ToString & "," &    '90
                             Dn(17).ToString & "," &    '101.25
                             Dn(18).ToString & "," &    '112.5
                             Dn(19).ToString & "," &    '123.75
                             Dn(20).ToString & "," &    '135
                             Dn(21).ToString & "," &    '146.25
                             Dn(22).ToString & "," &    '157.5
                             Dn(23).ToString)           '168.75
                'Dn(23).ToString & "," &    '168.75
                'Dn(24).ToString & "," &    'Length
                'Dn(25).ToString & "," &    'Pitch
                'Ds(1) & "," &              'Machine No.
                'Ds(4) & "," &              'B/W
                'Dn(1).ToString & "," &     'Points
                'Dn(26).ToString)           'PchExp
            End If
        End Using

        If FlgDBF = 1 Then
            Using sw As New StreamWriter(cur_dir & DEF_DATA_FILE_FLD &
                                         Path.GetFileNameWithoutExtension(StrDataFileName) &
                                         ".add", True, Encoding.UTF8)
                sw.WriteLine(Ds(2) & "," &
                             Ds(3) & "," &
                             Dn(24) & "," &
                             Dn(25) & "," &
                             Dn(26))
            End Using
        End If
    End Sub

    Public Sub S_MogiData()
        Dim M As Integer

        '==== 模擬データセット ====
        '
        'DataMogiStr(M, N): 1=MachineNo, 2=SampleName, 3=Mark, 4=B/W, 5=S.No, 6=Data, 7=Time, 8=Angle-Deep(Km/S), 9=Angle-Peak(Km/S)
        'DataMogiNum(m, N): 1=md/cd, 2=Pk/Dp, 3=Deep(Km/S), 4=Peak(Km/S), 5=Meas-Data(0°=MD), 6...12, 13=Meas-Data(90°=CD), 14...20
        '※1:Meas Data=[Km/S] ※2:Str(8)=Angle-Deep(Km/S)=Angle-Peak(uS), Str(9)=Angle-Peak(Km/S)=Angle-Deep(uS)

        M = 1
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD+ 1.14" : DataMogiStr(M, 9) = "MD- 1.68"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.6 : DataMogiNum(M, 3) = 2.073 : DataMogiNum(M, 4) = 3.209
        DataMogiNum(M, 5) = 3.209 : DataMogiNum(M, 6) = 3.141 : DataMogiNum(M, 7) = 2.913 : DataMogiNum(M, 8) = 2.626
        DataMogiNum(M, 9) = 2.353 : DataMogiNum(M, 10) = 2.178 : DataMogiNum(M, 11) = 2.076 : DataMogiNum(M, 12) = 2.0#
        DataMogiNum(M, 13) = 2.024 : DataMogiNum(M, 14) = 2.062 : DataMogiNum(M, 15) = 2.162 : DataMogiNum(M, 16) = 2.303
        DataMogiNum(M, 17) = 2.542 : DataMogiNum(M, 18) = 2.752 : DataMogiNum(M, 19) = 2.993 : DataMogiNum(M, 20) = 3.175

        M = 2
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD- 1.52" : DataMogiStr(M, 9) = "MD- 1.33"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.6 : DataMogiNum(M, 3) = 2.01 : DataMogiNum(M, 4) = 3.217
        DataMogiNum(M, 5) = 3.2 : DataMogiNum(M, 6) = 3.166 : DataMogiNum(M, 7) = 2.927 : DataMogiNum(M, 8) = 2.643
        DataMogiNum(M, 9) = 2.376 : DataMogiNum(M, 10) = 2.198 : DataMogiNum(M, 11) = 2.083 : DataMogiNum(M, 12) = 2.0#
        DataMogiNum(M, 13) = 2.017 : DataMogiNum(M, 14) = 2.058 : DataMogiNum(M, 15) = 2.162 : DataMogiNum(M, 16) = 2.286
        DataMogiNum(M, 17) = 2.516 : DataMogiNum(M, 18) = 2.721 : DataMogiNum(M, 19) = 2.963 : DataMogiNum(M, 20) = 3.183

        M = 3
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD- 0.20" : DataMogiStr(M, 9) = "MD- 0.74"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.6 : DataMogiNum(M, 3) = 2.007 : DataMogiNum(M, 4) = 3.217
        DataMogiNum(M, 5) = 3.2 : DataMogiNum(M, 6) = 3.175 : DataMogiNum(M, 7) = 2.956 : DataMogiNum(M, 8) = 2.667
        DataMogiNum(M, 9) = 2.39 : DataMogiNum(M, 10) = 2.21 : DataMogiNum(M, 11) = 2.091 : DataMogiNum(M, 12) = 2.0#
        DataMogiNum(M, 13) = 2.01 : DataMogiNum(M, 14) = 2.094 : DataMogiNum(M, 15) = 2.143 : DataMogiNum(M, 16) = 2.268
        DataMogiNum(M, 17) = 2.495 : DataMogiNum(M, 18) = 2.697 : DataMogiNum(M, 19) = 2.948 : DataMogiNum(M, 20) = 3.175

        M = 4
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD- 1.47" : DataMogiStr(M, 9) = "MD+ 0.52"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.6 : DataMogiNum(M, 3) = 2.007 : DataMogiNum(M, 4) = 3.217
        DataMogiNum(M, 5) = 3.226 : DataMogiNum(M, 6) = 3.209 : DataMogiNum(M, 7) = 2.963 : DataMogiNum(M, 8) = 2.691
        DataMogiNum(M, 9) = 2.414 : DataMogiNum(M, 10) = 2.239 : DataMogiNum(M, 11) = 2.109 : DataMogiNum(M, 12) = 2.003
        DataMogiNum(M, 13) = 2.01 : DataMogiNum(M, 14) = 2.041 : DataMogiNum(M, 15) = 2.135 : DataMogiNum(M, 16) = 2.256
        DataMogiNum(M, 17) = 2.464 : DataMogiNum(M, 18) = 2.673 : DataMogiNum(M, 19) = 2.927 : DataMogiNum(M, 20) = 3.166

        M = 5
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD+ 0.17" : DataMogiStr(M, 9) = "MD+ 0.86"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.7 : DataMogiNum(M, 3) = 2.003 : DataMogiNum(M, 4) = 3.306
        DataMogiNum(M, 5) = 3.261 : DataMogiNum(M, 6) = 3.235 : DataMogiNum(M, 7) = 3.046 : DataMogiNum(M, 8) = 2.721
        DataMogiNum(M, 9) = 2.449 : DataMogiNum(M, 10) = 2.256 : DataMogiNum(M, 11) = 2.116 : DataMogiNum(M, 12) = 2.02
        DataMogiNum(M, 13) = 2.003 : DataMogiNum(M, 14) = 2.03 : DataMogiNum(M, 15) = 2.12 : DataMogiNum(M, 16) = 2.239
        DataMogiNum(M, 17) = 2.459 : DataMogiNum(M, 18) = 2.665 : DataMogiNum(M, 19) = 2.913 : DataMogiNum(M, 20) = 3.217

        M = 6
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD+ 1.48" : DataMogiStr(M, 9) = "MD+ 1.00"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.7 : DataMogiNum(M, 3) = 2.01 : DataMogiNum(M, 4) = 3.343
        DataMogiNum(M, 5) = 3.261 : DataMogiNum(M, 6) = 3.252 : DataMogiNum(M, 7) = 3.038 : DataMogiNum(M, 8) = 2.74
        DataMogiNum(M, 9) = 2.474 : DataMogiNum(M, 10) = 2.273 : DataMogiNum(M, 11) = 2.128 : DataMogiNum(M, 12) = 2.027
        DataMogiNum(M, 13) = 2.01 : DataMogiNum(M, 14) = 2.03 : DataMogiNum(M, 15) = 2.105 : DataMogiNum(M, 16) = 2.214
        DataMogiNum(M, 17) = 2.429 : DataMogiNum(M, 18) = 2.632 : DataMogiNum(M, 19) = 2.892 : DataMogiNum(M, 20) = 3.2

        M = 7
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD+ 1.49" : DataMogiStr(M, 9) = "MD+ 1.25"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.6 : DataMogiNum(M, 3) = 2.01 : DataMogiNum(M, 4) = 3.297
        DataMogiNum(M, 5) = 3.252 : DataMogiNum(M, 6) = 3.252 : DataMogiNum(M, 7) = 2.993 : DataMogiNum(M, 8) = 2.733
        DataMogiNum(M, 9) = 2.469 : DataMogiNum(M, 10) = 2.273 : DataMogiNum(M, 11) = 2.124 : DataMogiNum(M, 12) = 2.027
        DataMogiNum(M, 13) = 2.01 : DataMogiNum(M, 14) = 2.03 : DataMogiNum(M, 15) = 2.105 : DataMogiNum(M, 16) = 2.214
        DataMogiNum(M, 17) = 2.424 : DataMogiNum(M, 18) = 2.626 : DataMogiNum(M, 19) = 2.899 : DataMogiNum(M, 20) = 3.175

        M = 8
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD+ 0.97" : DataMogiStr(M, 9) = "MD+ 1.40"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.6 : DataMogiNum(M, 3) = 2.01 : DataMogiNum(M, 4) = 3.279
        DataMogiNum(M, 5) = 3.261 : DataMogiNum(M, 6) = 3.235 : DataMogiNum(M, 7) = 3.015 : DataMogiNum(M, 8) = 2.733
        DataMogiNum(M, 9) = 2.464 : DataMogiNum(M, 10) = 2.268 : DataMogiNum(M, 11) = 2.124 : DataMogiNum(M, 12) = 2.024
        DataMogiNum(M, 13) = 2.007 : DataMogiNum(M, 14) = 2.034 : DataMogiNum(M, 15) = 2.109 : DataMogiNum(M, 16) = 2.226
        DataMogiNum(M, 17) = 2.439 : DataMogiNum(M, 18) = 2.643 : DataMogiNum(M, 19) = 2.906 : DataMogiNum(M, 20) = 3.2

        M = 9
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD+ 1.62" : DataMogiStr(M, 9) = "MD+ 1.38"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.6 : DataMogiNum(M, 3) = 2.013 : DataMogiNum(M, 4) = 3.306
        DataMogiNum(M, 5) = 3.27 : DataMogiNum(M, 6) = 3.27 : DataMogiNum(M, 7) = 3.038 : DataMogiNum(M, 8) = 2.74
        DataMogiNum(M, 9) = 2.474 : DataMogiNum(M, 10) = 2.273 : DataMogiNum(M, 11) = 2.128 : DataMogiNum(M, 12) = 2.03
        DataMogiNum(M, 13) = 2.007 : DataMogiNum(M, 14) = 2.034 : DataMogiNum(M, 15) = 2.105 : DataMogiNum(M, 16) = 2.21
        DataMogiNum(M, 17) = 2.424 : DataMogiNum(M, 18) = 2.626 : DataMogiNum(M, 19) = 2.892 : DataMogiNum(M, 20) = 3.175

        M = 10
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD+ 1.40" : DataMogiStr(M, 9) = "MD+ 1.79"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.7 : DataMogiNum(M, 3) = 2.01 : DataMogiNum(M, 4) = 3.324
        DataMogiNum(M, 5) = 3.352 : DataMogiNum(M, 6) = 3.315 : DataMogiNum(M, 7) = 3.038 : DataMogiNum(M, 8) = 2.74
        DataMogiNum(M, 9) = 2.469 : DataMogiNum(M, 10) = 2.268 : DataMogiNum(M, 11) = 2.128 : DataMogiNum(M, 12) = 2.027
        DataMogiNum(M, 13) = 2.007 : DataMogiNum(M, 14) = 2.034 : DataMogiNum(M, 15) = 2.105 : DataMogiNum(M, 16) = 2.214
        DataMogiNum(M, 17) = 2.424 : DataMogiNum(M, 18) = 2.626 : DataMogiNum(M, 19) = 2.892 : DataMogiNum(M, 20) = 3.243

        M = 11
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD+ 1.55" : DataMogiStr(M, 9) = "MD+ 1.67"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.7 : DataMogiNum(M, 3) = 2.01 : DataMogiNum(M, 4) = 3.399
        DataMogiNum(M, 5) = 3.343 : DataMogiNum(M, 6) = 3.324 : DataMogiNum(M, 7) = 3.141 : DataMogiNum(M, 8) = 2.765
        DataMogiNum(M, 9) = 2.495 : DataMogiNum(M, 10) = 2.294 : DataMogiNum(M, 11) = 2.143 : DataMogiNum(M, 12) = 2.034
        DataMogiNum(M, 13) = 2.017 : DataMogiNum(M, 14) = 2.02 : DataMogiNum(M, 15) = 2.098 : DataMogiNum(M, 16) = 2.206
        DataMogiNum(M, 17) = 2.41 : DataMogiNum(M, 18) = 2.609 : DataMogiNum(M, 19) = 2.92 : DataMogiNum(M, 20) = 3.235

        M = 12
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD+ 1.49" : DataMogiStr(M, 9) = "MD+ 1.87"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.6 : DataMogiNum(M, 3) = 2.01 : DataMogiNum(M, 4) = 3.27
        DataMogiNum(M, 5) = 3.243 : DataMogiNum(M, 6) = 3.261 : DataMogiNum(M, 7) = 3.093 : DataMogiNum(M, 8) = 2.784
        DataMogiNum(M, 9) = 2.51 : DataMogiNum(M, 10) = 2.308 : DataMogiNum(M, 11) = 2.151 : DataMogiNum(M, 12) = 2.037
        DataMogiNum(M, 13) = 2.013 : DataMogiNum(M, 14) = 2.03 : DataMogiNum(M, 15) = 2.105 : DataMogiNum(M, 16) = 2.198
        DataMogiNum(M, 17) = 2.395 : DataMogiNum(M, 18) = 2.581 : DataMogiNum(M, 19) = 2.85 : DataMogiNum(M, 20) = 3.117

        M = 13
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD+ 1.72" : DataMogiStr(M, 9) = "MD+ 2.01"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.6 : DataMogiNum(M, 3) = 2.01 : DataMogiNum(M, 4) = 3.279
        DataMogiNum(M, 5) = 3.217 : DataMogiNum(M, 6) = 3.235 : DataMogiNum(M, 7) = 3.101 : DataMogiNum(M, 8) = 2.81
        DataMogiNum(M, 9) = 2.532 : DataMogiNum(M, 10) = 2.326 : DataMogiNum(M, 11) = 2.166 : DataMogiNum(M, 12) = 2.048
        DataMogiNum(M, 13) = 2.007 : DataMogiNum(M, 14) = 2.017 : DataMogiNum(M, 15) = 2.087 : DataMogiNum(M, 16) = 2.182
        DataMogiNum(M, 17) = 2.372 : DataMogiNum(M, 18) = 2.559 : DataMogiNum(M, 19) = 2.824 : DataMogiNum(M, 20) = 3.085

        M = 14
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD+ 1.89" : DataMogiStr(M, 9) = "MD+ 2.19"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.7 : DataMogiNum(M, 3) = 2.01 : DataMogiNum(M, 4) = 3.333
        DataMogiNum(M, 5) = 3.252 : DataMogiNum(M, 6) = 3.288 : DataMogiNum(M, 7) = 3.2 : DataMogiNum(M, 8) = 2.837
        DataMogiNum(M, 9) = 2.559 : DataMogiNum(M, 10) = 2.344 : DataMogiNum(M, 11) = 2.178 : DataMogiNum(M, 12) = 2.055
        DataMogiNum(M, 13) = 2.01 : DataMogiNum(M, 14) = 2.007 : DataMogiNum(M, 15) = 2.073 : DataMogiNum(M, 16) = 2.162
        DataMogiNum(M, 17) = 2.344 : DataMogiNum(M, 18) = 2.526 : DataMogiNum(M, 19) = 2.804 : DataMogiNum(M, 20) = 3.061

        M = 0
        DataMogiStr(M, 1) = "" : DataMogiStr(M, 2) = "" : DataMogiStr(M, 3) = ""
        DataMogiStr(M, 4) = "" : DataMogiStr(M, 5) = Str(M) : DataMogiStr(M, 8) = "CD+ 2.10" : DataMogiStr(M, 9) = "MD+ 2.26"
        DataMogiNum(M, 1) = 1.6 : DataMogiNum(M, 2) = 1.7 : DataMogiNum(M, 3) = 1.99 : DataMogiNum(M, 4) = 3.343
        DataMogiNum(M, 5) = 3.243 : DataMogiNum(M, 6) = 3.279 : DataMogiNum(M, 7) = 3.166 : DataMogiNum(M, 8) = 2.871
        DataMogiNum(M, 9) = 2.586 : DataMogiNum(M, 10) = 2.353 : DataMogiNum(M, 11) = 2.186 : DataMogiNum(M, 12) = 2.055
        DataMogiNum(M, 13) = 1.997 : DataMogiNum(M, 14) = 1.99 : DataMogiNum(M, 15) = 2.044 : DataMogiNum(M, 16) = 2.139
        DataMogiNum(M, 17) = 2.321 : DataMogiNum(M, 18) = 2.51 : DataMogiNum(M, 19) = 2.784 : DataMogiNum(M, 20) = 3.038

    End Sub

    Public Sub ResolveData()
        Dim K As Integer
        Dim M As Integer
        Dim N As Integer
        Dim StrSepa As String

        If FlgTest <> 0 Then
            SetMogi()
        Else
            N = Val(Mid(strRxdata, 12, 2))
            StrSepa = ""
            M = 1

            For K = 1 To Len(strRxdata)
                If Mid(strRxdata, K, 1) <> "," Then
                    StrSepa += Mid(strRxdata, K, 1)
                Else
                    DataReceive(M) = StrSepa
                    StrSepa = ""
                    M += 1
                End If
            Next

            DataPrcStr(1, SampleNo, 1) = MachineNo
            DataPrcStr(1, SampleNo, 2) = Sample
            DataPrcStr(1, SampleNo, 3) = Mark
            DataPrcStr(1, SampleNo, 4) = BW
            DataPrcStr(1, SampleNo, 5) = Str(SampleNo)
            DataPrcStr(1, SampleNo, 6) = Now.ToString("yy/MM/dd")
            If FlgDBF = 0 Then
                DataPrcStr(1, SampleNo, 7) = Now.ToString("HH:mm:ss")
            Else
                DataPrcStr(1, SampleNo, 7) = Now.ToString("HH:mm")
            End If

            DataPrcStr(1, SampleNo, 8) = DataReceive(1)     'angle-peak(us)
            DataPrcStr(1, SampleNo, 9) = DataReceive(2)     'angle-deep(us)

            If FlgProfile = 3 Then
                DataPrcStr(1, SampleNo, 10) = Str(Val(DataReceive(7)) / Val(DataReceive(15)))
            Else
                DataPrcStr(1, SampleNo, 10) = Str(Val(DataReceive(15)) / Val(DataReceive(7)))
            End If

            DataPrcStr(1, SampleNo, 11) = Str(Val(DataReceive(5)) / Val(DataReceive(6)))

            DataPrcNum(1, SampleNo, 19) = Val(DataPrcStr(1, SampleNo, 10))
            DataPrcNum(1, SampleNo, 20) = Val(DataPrcStr(1, SampleNo, 11))

            For M = 1 To 18
                If Val(DataReceive(M + 4)) = 0 Then
                    DataPrcNum(1, SampleNo, M) = 0
                Else
                    DataPrcNum(1, SampleNo, M) = Us_Dist / Val(DataReceive(M + 4))
                End If
            Next
        End If

        RearrangeData()
    End Sub

    Public Sub TxtSetMogi()
        Dim M As Integer
        Dim K As Integer

        M = SampleNo
        K = (SampleNo + (MeasNo - 1) * 1) Mod 15

        Select Case FlgProfile
            Case 0
                FrmSST4500_1_0_0J_meas.TxtMachNoCur.Text = DataMogiStr(1, 1)
                FrmSST4500_1_0_0J_meas.TxtSmplNamCur.Text = DataMogiStr(1, 2)
                FrmSST4500_1_0_0J_meas.TxtMarkCur.Text = DataMogiStr(1, 3)
            Case Else
                FrmSST4500_1_0_0J_Profile.TxtMachNoCur.Text = DataMogiStr(1, 1)
                FrmSST4500_1_0_0J_Profile.TxtSmplNamCur.Text = DataMogiStr(1, 2)
                FrmSST4500_1_0_0J_Profile.TxtMarkCur.Text = DataMogiStr(1, 3)
        End Select

    End Sub

    Public Sub SetMogi()
        Dim M As Integer
        Dim N As Integer
        Dim K As Integer

        M = SampleNo
        K = (SampleNo + (MeasNo - 1) * 1) Mod 15

        'For N = 1 To 9
        'DataPrcStr(1, M, N) = DataMogiStr(K, N)
        'Next
        DataPrcStr(1, M, 1) = MachineNo
        DataPrcStr(1, M, 2) = Sample
        DataPrcStr(1, M, 3) = Mark
        DataPrcStr(1, M, 4) = BW
        DataPrcStr(1, M, 5) = Str(SampleNo)
        DataPrcStr(1, M, 6) = Now.ToString("yy/MM/dd")
        If FlgDBF = 0 Then
            DataPrcStr(1, M, 7) = Now.ToString("HH:mm:ss")
        Else
            DataPrcStr(1, M, 7) = Now.ToString("HH:mm")
        End If

        DataPrcStr(1, M, 8) = DataMogiStr(K, 8)
        DataPrcStr(1, M, 9) = DataMogiStr(K, 9)

        If FlgProfile = 3 Then
            DataPrcStr(1, M, 10) = Str(DataMogiNum(K, 5) / DataMogiNum(K, 13))
        Else
            DataPrcStr(1, M, 10) = Str(DataMogiNum(K, 13) / DataMogiNum(K, 5))
        End If

        DataPrcStr(1, M, 11) = Str(DataMogiNum(K, 4) / DataMogiNum(K, 3))

        DataPrcNum(1, M, 19) = Val(DataPrcStr(1, M, 10))
        DataPrcNum(1, M, 20) = Val(DataPrcStr(1, M, 11))

        For N = 1 To 18
            DataPrcNum(1, M, N) = DataMogiNum(K, N + 2)
        Next
    End Sub

    Public Sub RearrangeData()
        Dim Kst As String
        Dim Ms As Single
        'Dim Ns As Single
        'Dim M As Integer

        If FlgProfile = 3 Then
            Kst = Strings.Left(DataPrcStr(1, SampleNo, 8), 2)
            If Kst = "CD" Then
                If FlgDpmd = 0 Then
                    Kst = Strings.Left(DataPrcStr(1, SampleNo, 8), 3)
                    If Kst = "CD+" Then
                        FlgDpmd = 1
                    ElseIf Kst = "CD-" Then
                        FlgDpmd = 2
                    End If
                End If

                Kst = DataPrcStr(1, SampleNo, 8)
                Ms = Val(Strings.Right(Kst, Len(Kst) - 2)) - 90
                If FlgDpmd = 2 Then
                    Ms += 180
                End If

            Else
                Kst = DataPrcStr(1, SampleNo, 8)
                Ms = Val(Strings.Right(Kst, Len(Kst) - 2))
            End If

            If Ms < -1 Then
                Kst = "CD-" + Trim(Str(-Ms))
            ElseIf Ms < 0 Then
                Kst = "CD-0" + Trim(Str(-Ms))
            ElseIf Ms = 0 Then
                Kst = "CD+0.00"
            ElseIf Ms < 1 Then
                Kst = "CD+0" + Trim(Str(Ms))
            Else
                Kst = "CD+" + Trim(Str(Ms))
            End If

            DataPrcStr(1, SampleNo, 8) = Kst

            Kst = Strings.Left(DataPrcStr(1, SampleNo, 9), 2)
            If Kst = "MD" Then
                If FlgPkcd = 0 Then
                    Kst = Strings.Left(DataPrcStr(1, SampleNo, 9), 3)
                    If Kst = "MD+" Then
                        FlgPkcd = 1
                    ElseIf Kst = "MD-" Then
                        FlgPkcd = 2
                    End If
                End If

                Kst = DataPrcStr(1, SampleNo, 9)
                Ms = Val(Strings.Right(Kst, Len(Kst) - 2)) + 90
                If FlgPkcd = 1 Then
                    Ms -= 180
                End If

            Else
                Kst = DataPrcStr(1, SampleNo, 9)
                Ms = Val(Strings.Right(Kst, Len(Kst) - 2))
            End If

            If Ms < -1 Then
                Kst = "MD-" + Trim(Str(-Ms))
            ElseIf Ms < 0 Then
                Kst = "MD-0" + Trim(Str(-Ms))
            ElseIf Ms = 0 Then
                Kst = "MD+0.00"
            ElseIf Ms < 1 Then
                Kst = "MD+0" + Trim(Str(Ms))
            Else
                Kst = "MD+" + Trim(Str(Ms))
            End If

            DataPrcStr(1, SampleNo, 9) = Kst

            For M = 3 To 10
                DataPrcNum(0, SampleNo, M) = DataPrcNum(1, SampleNo, M)
                DataPrcNum(1, SampleNo, M) = DataPrcNum(1, SampleNo, M + 8)
                DataPrcNum(1, SampleNo, M + 8) = DataPrcNum(0, SampleNo, M)
            Next

        Else
            Kst = Left(DataPrcStr(1, SampleNo, 8), 2)
            If Kst = "MD" Then
                If FlgDpmd = 0 Then
                    Kst = Left(DataPrcStr(1, SampleNo, 8), 3)
                    If Kst = "MD+" Then
                        FlgDpmd = 1
                    ElseIf Kst = "MD-" Then
                        FlgDpmd = 2
                    End If
                End If
                Kst = DataPrcStr(1, SampleNo, 8)
                Ms = Val(Right(Kst, Len(Kst) - 2)) - 90
                If FlgDpmd = 2 Then
                    Ms += 180
                End If
            Else
                Kst = DataPrcStr(1, SampleNo, 8)
                Ms = Val(Right(Kst, Len(Kst) - 2))
            End If

            If Ms < -1 Then
                Kst = "CD-" + Trim(Str(-Ms))
            ElseIf Ms < 0 Then
                Kst = "CD-0" + Trim(Str(-Ms))
            ElseIf Ms = 0 Then
                Kst = "CD+0.00"
            ElseIf Ms < 1 Then
                Kst = "CD+0" + Trim(Str(Ms))
            Else
                Kst = "CD+" + Trim(Str(Ms))
            End If
            DataPrcStr(1, SampleNo, 8) = Kst

            Kst = Left(DataPrcStr(1, SampleNo, 9), 2)
            If Kst = "CD" Then
                If FlgPkcd = 0 Then
                    Kst = Left(DataPrcStr(1, SampleNo, 9), 3)
                    If Kst = "CD+" Then
                        FlgPkcd = 1
                    ElseIf Kst = "CD-" Then
                        FlgPkcd = 2
                    End If
                End If
                Kst = DataPrcStr(1, SampleNo, 9)
                Ms = Val(Right(Kst, Len(Kst) - 2)) + 90
                If FlgPkcd = 1 Then
                    Ms -= 180
                End If
            Else
                Kst = DataPrcStr(1, SampleNo, 9)
                Ms = Val(Right(Kst, Len(Kst) - 2))
            End If

            If Ms < -1 Then
                Kst = "MD-" + Trim(Str(-Ms))
            ElseIf Ms < 0 Then
                Kst = "MD-0" + Trim(Str(-Ms))
            ElseIf Ms = 0 Then
                Kst = "MD+0.00"
            ElseIf Ms < 1 Then
                Kst = "MD+0" + Trim(Str(Ms))
            Else
                Kst = "MD+" + Trim(Str(Ms))
            End If

            DataPrcStr(1, SampleNo, 9) = Kst
        End If
    End Sub

    Public Sub SaveConst_PchExp(ByRef pch_data() As Single, ByVal length As Integer, ByVal _filepath As String)
        'Dim _filename_const As String
        'Dim _pathname_const As String
        'Dim _filename_pchexp_full As String
        Dim _writedata As String = ""

        '_filename_const = Path.GetFileNameWithoutExtension(StrConstFileName)
        '_pathname_const = Path.GetDirectoryName(StrConstFileName)
        '_filename_pchexp_full = _pathname_const & "\" & _filename_const & StrConstFileName_PchExp

        Dim datalen As Integer = UBound(pch_data)

        _writedata = length.ToString & ","
        For i = 0 To datalen - 1
            _writedata &= pch_data(i).ToString & ","
        Next
        _writedata &= pch_data(datalen).ToString

        'Using sw As New StreamWriter(_filename_pchexp_full, False, Encoding.UTF8)
        Using sw As New StreamWriter(_filepath, False, Encoding.UTF8)

            sw.WriteLine(_writedata)
        End Using

        LoadConstPitch(_filepath)
    End Sub
End Module
