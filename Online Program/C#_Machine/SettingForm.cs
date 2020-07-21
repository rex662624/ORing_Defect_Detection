using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using StCtlLib;

namespace SensorTechnology
{
	/// <summary>
	/// SettingForm
	/// </summary>
	public class SettingForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.Panel panelButton;
		/// <summary>
		/// 
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblExposureValue;
		private System.Windows.Forms.TrackBar trackBarExposure;
		private System.Windows.Forms.Label lblExposure;

		private StCamera m_objStCamera;

		private TrackBar[] m_TrackBarList;
		private TextBox[] m_TextBoxList;
		private Label[] m_LabelList;
		private StCtlLib.StComboBox[] m_ComboBoxList;
		private TextBox[] m_ReadOnlyTextBoxList;
		private Button[] m_ButtonList;
		private CheckBox[] m_CheckBoxList;

		private System.Windows.Forms.Label lblGainValue;
		private System.Windows.Forms.TrackBar trackBarGain;
		private System.Windows.Forms.Label lblGain;
		private System.Windows.Forms.Label lblDigitalGainValue;
		private System.Windows.Forms.TrackBar trackBarDigitalGain;
		private System.Windows.Forms.Label lblDigitalGain;
		private System.Windows.Forms.Label lblWBMode;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Label lblWBRGainValue;
		private System.Windows.Forms.TrackBar trackBarWBRGain;
		private System.Windows.Forms.Label lblWBRGain;
		private System.Windows.Forms.Label lblWBGrGainValue;
		private System.Windows.Forms.TrackBar trackBarWBGrGain;
		private System.Windows.Forms.Label lblWBGrGain;
		private System.Windows.Forms.Label lblWBGbGainValue;
		private System.Windows.Forms.TrackBar trackBarWBGbGain;
		private System.Windows.Forms.Label lblWBGbGain;
		private System.Windows.Forms.Label lblWBBGainValue;
		private System.Windows.Forms.TrackBar trackBarWBBGain;
		private System.Windows.Forms.Label lblWBBGain;
		private System.Windows.Forms.TabPage tabPageShutterGain;
		private System.Windows.Forms.TabPage tabPageWB;
		private System.Windows.Forms.TabPage tabPageTriggerMode;
		private System.Windows.Forms.TabPage tabPageTriggerTiming;
		private System.Windows.Forms.TabPage tabPageIO;
		private System.Windows.Forms.TabPage tabPageOther;
		private System.Windows.Forms.Label lblTriggerMode;
		private System.Windows.Forms.Label lblTriggerSource;
        private System.Windows.Forms.Label lblNoiseReduction;
		private System.Windows.Forms.Label lblExposureEnd;
		private System.Windows.Forms.Label lblExposureWaitHD;
		private System.Windows.Forms.Label lblExposureWaitReadOut;
		private System.Windows.Forms.Label lblExposureMode;
		private System.Windows.Forms.Label lblScanMode;
		private System.Windows.Forms.Label lblClockMode;
		private System.Windows.Forms.Label lblColorInterpolation;
		private System.Windows.Forms.Button btnFrameStartTrigger;
		private System.Windows.Forms.Button btnSensorReadOutStartTrigger;
		private System.Windows.Forms.Button btnResetFrameNo;
		private System.Windows.Forms.Label lblIO0;
		private System.Windows.Forms.Label lblIOInOut;
		private System.Windows.Forms.Label lblIOMode;
		private System.Windows.Forms.Label lblIOStatus;
		private System.Windows.Forms.Label lblIO1;
		private System.Windows.Forms.Label lblIO2;
		private System.Windows.Forms.Label lblIO3;
		private System.Windows.Forms.Label lblIOPolarity;
        private System.Windows.Forms.Label lblSW;
		private System.Windows.Forms.TrackBar trackBarStrobeStartDelay;
		private System.Windows.Forms.Label lblStrobeStartDelay;
		private System.Windows.Forms.Label lblStrobeEndDelayValue;
		private System.Windows.Forms.TrackBar trackBarStrobeEndDelay;
		private System.Windows.Forms.Label lblReadOutDelayValue;
		private System.Windows.Forms.TrackBar trackBarReadOutDelay;
		private System.Windows.Forms.Label lblReadOutDelay;
		private System.Windows.Forms.Button btnExposureEndTrigger;
		private System.Windows.Forms.Label lblStrobeEndDelay;
        private System.Windows.Forms.Label lbltrackBarStrobeStartDelayValue;
		private System.Windows.Forms.Label lblSaturationValue;
		private System.Windows.Forms.TrackBar trackBarSaturation;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblHueValue;
		private System.Windows.Forms.TrackBar trackBarHue;
		private System.Windows.Forms.Label lblHue;
		private System.Windows.Forms.Label lblHueSaturationMode;
		private System.Windows.Forms.TextBox txtFirmVersion;
		private System.Windows.Forms.TextBox txtFPGAVersion;
		private System.Windows.Forms.TextBox txtCameraType;
		private System.Windows.Forms.Label lblFirmVersion;
		private System.Windows.Forms.Label lblFPGAVersion;
		private System.Windows.Forms.Label lblCameraType;
		private System.Windows.Forms.Label lblMirror;
		private System.Windows.Forms.Label lblRotation;
		private System.Windows.Forms.Label lblLEDGreen;
		private System.Windows.Forms.Label lblRed;
		private System.Windows.Forms.Label lblCameraMemory;
		private System.Windows.Forms.TextBox txtSWStatus0;
		private System.Windows.Forms.TextBox txtSWStatus1;
		private System.Windows.Forms.TextBox txtSWStatus2;
		private System.Windows.Forms.TextBox txtSWStatus3;
		private System.Windows.Forms.Label lblAutoTriggerTime;
		private System.Windows.Forms.TrackBar trackBarAutoTriggerTime;
		private System.Windows.Forms.CheckBox chkAutoTrigger;
		private System.Windows.Forms.Label lblOutputPulseDuration;
		private System.Windows.Forms.Label lblOutputPulseDelayValue;
		private System.Windows.Forms.TrackBar trackBarOutputPulseDelay;
		private System.Windows.Forms.Label lblOutputPulseDelay;
		private System.Windows.Forms.Label lblOutputPulseDurationValue;
		private StCtlLib.StTextBox txtDigitalGain;
		private StCtlLib.StTextBox txtGain;
		private StCtlLib.StTextBox txtExposure;
		private StCtlLib.StComboBox cmbLEDRed;
		private StCtlLib.StComboBox cmbLEDGreen;
		private StCtlLib.StComboBox cmbIOStatus3;
		private StCtlLib.StComboBox cmbIOStatus2;
		private StCtlLib.StComboBox cmbIOStatus1;
		private StCtlLib.StComboBox cmbIOStatus0;
		private StCtlLib.StComboBox cmbIOPolarity3;
		private StCtlLib.StComboBox cmbIOPolarity2;
		private StCtlLib.StComboBox cmbIOPolarity1;
		private StCtlLib.StComboBox cmbIOPolarity0;
		private StCtlLib.StComboBox cmbIOMode3;
		private StCtlLib.StComboBox cmbIOMode2;
		private StCtlLib.StComboBox cmbIOMode1;
		private StCtlLib.StComboBox cmbIOMode0;
		private StCtlLib.StComboBox cmbIOInOut3;
		private StCtlLib.StComboBox cmbIOInOut2;
		private StCtlLib.StComboBox cmbIOInOut1;
		private StCtlLib.StComboBox cmbIOInOut0;
		private StCtlLib.StComboBox cmbCameraMemory;
        private StCtlLib.StComboBox cmbExposureEnd;
		private StCtlLib.StComboBox cmbExposureWaitReadOut;
		private StCtlLib.StComboBox cmbExposureWaitHD;
		private StCtlLib.StComboBox cmbNoiseReduction;
		private StCtlLib.StComboBox cmbExposureMode;
		private StCtlLib.StComboBox cmbTriggerSource;
		private StCtlLib.StComboBox cmbTriggerMode;
		private StCtlLib.StTextBox txtAutoTriggerTime;
		private StCtlLib.StComboBox cmbHueSaturationMode;
		private StCtlLib.StComboBox cmbWBMode;
		private StCtlLib.StTextBox txtSaturation;
		private StCtlLib.StTextBox txtHue;
		private StCtlLib.StTextBox txtWBBGain;
		private StCtlLib.StTextBox txtWBGbGain;
		private StCtlLib.StTextBox txtWBGrGain;
		private StCtlLib.StTextBox txtWBRGain;
		private StCtlLib.StTextBox txtReadOutDelay;
		private StCtlLib.StTextBox txtOutputPulseDuration;
		private StCtlLib.StTextBox txtOutputPulseDelay;
		private StCtlLib.StTextBox txtStrobeEndDelay;
        private StCtlLib.StTextBox txtStrobeStartDelay;
		private StCtlLib.StComboBox cmbRotation;
		private StCtlLib.StComboBox cmbMirror;
		private StCtlLib.StComboBox cmbColorInterpolation;
		private StCtlLib.StComboBox cmbClockMode;
		private StCtlLib.StComboBox cmbScanMode;
        private TabPage tabPageColorGamma;
        private StTextBox txtRGamma;
        private TrackBar trackBarRGamma;
        private Label label4;
        private StComboBox cmbRGammaMode;
        private Label label5;
        private StTextBox txtBGamma;
        private TrackBar trackBarBGamma;
        private Label label10;
        private StComboBox cmbBGammaMode;
        private Label label11;
        private StTextBox txtGBGamma;
        private TrackBar trackBarGBGamma;
        private Label label8;
        private StComboBox cmbGBGammaMode;
        private Label label9;
        private StTextBox txtGRGamma;
        private TrackBar trackBarGRGamma;
        private Label label6;
        private StComboBox cmbGRGammaMode;
        private Label label7;
        private TextBox txtSDKVersioin;
        private Label lblSDKVersion;
		private StComboBox cmbTransferBitsPerPixel;
		private Label lblTransferBitsPerPixel;
		private StComboBox cmbTriggerOverlap;
		private Label lblTriggerOverlap;
		private StTextBox stTextBoxImageHeight;
		private StTextBox stTextBoxImageWidth;
		private TrackBar trackBarImageHeight;
		private Label label14;
		private TrackBar trackBarImageWidth;
		private Label label15;
		private StTextBox stTextBoxImageOffsetY;
		private StTextBox stTextBoxImageOffsetX;
		private TrackBar trackBarImageOffsetY;
		private Label label12;
		private TrackBar trackBarImageOffsetX;
		private Label label13;
		private StComboBox stComboBoxVBinningSkipping;
		private Label label17;
		private StComboBox stComboBoxHBinningSkipping;
		private Label label16;
		private Label labelOutputFPS;
		private StTextBox stTextBoxVBlankForFPS;
		private TrackBar trackBarVBlankForFPS;
		private Label label18;
		private StComboBox cmbSensorShutterMode;
		private Label label19;
		private StTextBox txtAGCMaxGain;
		private Label lblAGCMaxGain;
		private TrackBar trackBarAGCMaxGain;
		private Label label25;
		private StTextBox txtAGCMinGain;
		private Label lblAGCMinGain;
		private TrackBar trackBarAGCMinGain;
		private Label label27;
		private StTextBox txtAEMaxExposure;
		private Label lblAEMaxExposure;
		private TrackBar trackBarAEMaxExposure;
		private Label label24;
		private StTextBox txtAEMinExposure;
		private Label lblAEMinExposure;
		private TrackBar trackBarAEMinExposure;
		private Label label23;
		private StTextBox txtALCTarget;
		private TrackBar trackBarALCTarget;
		private Label label21;
		private StComboBox cmbALCMode;
		private Label label20;
		private TabPage tabPageEEPROM;
		private Button btnWriteCameraSettingDPP;
		private Button btnReadCameraSettingDPP;
		private Button btnInitCameraSetting;
		private Button btnWriteCameraSetting;
		private Button btnReadCameraSetting;
		private TabPage tabPageDefectPixelCorrection;
		private StComboBox cmbDefectPixelCorrectionMode;
		private Label label26;
		private Button btnDetectDefectPixel;
		private NumericUpDown numericUpDownDefectPixelThreshold;
		private Label label22;
		private DefectPixelSetting defectPixelSetting1;
		private Label label28;
		private StComboBox cmbHBinningSumMode;
		private TabPage tabPageY;
		private TrackBar trackBarCameraGamma;
		private Label label29;
		private StTextBox txtCameraGamma;
		private TrackBar trackBarYGamma;
		private Label label3;
		private Label label1;
		private TrackBar trackBarSharpnessCoring;
		private Label lblSharpnessCoring;
		private TrackBar trackBarSharpnessGain;
		private Label lblSharpnessGain;
		private Label lblSharpnessMode;
		private StTextBox txtYGamma;
		private StComboBox cmbYGammaMode;
		private StComboBox cmbSharpnessMode;
		private StTextBox txtSharpnessCoring;
		private StTextBox txtSharpnessGain;
		private TrackBar trackBarDigitalClamp;
		private Label label30;
		private StTextBox stTextBoxDigitalClamp;
		private Label label31;
		private StComboBox cmbVBinningSumMode;
        private Label lblRegionMode;
        private StComboBox cmbRegionMode;
        private Label lblCurrentRegion;
        private StComboBox cmbCurrentRegion;
        private StTextBox txtChromaSuppresionSuppressionLevel;
        private StTextBox txtChromaSuppresionStartLevel;
        private TrackBar trackBarChromaSuppresionSuppressionLevel;
        private Label label32;
        private TrackBar trackBarChromaSuppresionStartLevel;
        private Label label33;
        private Label label35;
        private StComboBox cmbShadingCorrectionMode;
        private TrackBar trackBarShadingCorrectionTarget;
        private Label label34;
        private StTextBox txtShadingCorrectionTarget;
        private Label label36;
        private StComboBox cmbResetSwitch;
        private TrackBar trackBarAnalogBlackLevel;
        private Label label37;
        private StTextBox txtAnalogBlackLevel;
        private StTextBox txtTemperature;
        private Label labelTemperature;
        private StComboBox cmbAdjustmentDigitalGain;
        private Label labelAdjustmentDigitalGain;
        private TabPage tabPageHDR_CMOSIS4M;
        private TrackBar trackBarHDR_CMOSIS4M_Vlow3;
        private Label label42;
        private StTextBox txtHDR_CMOSIS4M_Vlow3;
        private TrackBar trackBarHDR_CMOSIS4M_Knee2;
        private Label label43;
        private StTextBox txtHDR_CMOSIS4M_Knee2;
        private TrackBar trackBarHDR_CMOSIS4M_Vlow2;
        private Label label41;
        private StTextBox txtHDR_CMOSIS4M_Vlow2;
        private TrackBar trackBarHDR_CMOSIS4M_Knee1;
        private Label label40;
        private StTextBox txtHDR_CMOSIS4M_Knee1;
        private Label label39;
        private StComboBox cmbHDR_CMOSIS4M_SlopeNum;
        private Label label38;
        private StComboBox cmbHDR_CMOSIS4M_Mode;
        private Label labelDisplayPixelFormat;
        private StComboBox cmbDisplayPixelFormat;
        private StTextBox txtLowChromaSuppresionSuppressionLevel;
        private TrackBar trackBarLowChromaSuppresionSuppressionLevel;
        private Label label45;
        private StTextBox txtLowChromaSuppresionStartLevel;
        private TrackBar trackBarLowChromaSuppresionStartLevel;
        private Label label44;
        private Button btnExposureStartTrigger;
        private StComboBox cmbTriggerSelector;
        private Label label46;
        private Label lblTriggerDelay;
        private Label label47;
        private TrackBar trackBarTriggerDelay;
        private StTextBox txtTriggerDelay;
        private StTextBox txtLineDebounceTime;
        private Label lblLineDebounceTimeValue;
        private TrackBar trackBarLineDebounceTime;
        private Label lblLineDebounceTime;
		private System.Windows.Forms.TrackBar trackBarOutputPulseDuration;

		public SettingForm()
		{
			//
			// Windows
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent
			//
			m_objStCamera = null;

		}

		/// <summary>
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		public StCamera Camera
		{
			set
			{
				m_objStCamera = value;

				
			}
		}
		private void UpdateControlList()
		{	
			m_TrackBarList = null;
			m_TextBoxList = null;
			m_LabelList = null;
			m_ComboBoxList = null;
			m_ReadOnlyTextBoxList = null;
			m_ButtonList = null;
			m_CheckBoxList = null;
			if(tabControl.SelectedTab.Equals(tabPageShutterGain))
			{
				m_TrackBarList = new TrackBar[] { trackBarExposure, trackBarGain, trackBarDigitalGain, trackBarALCTarget, trackBarAGCMaxGain, trackBarAGCMinGain, trackBarAEMaxExposure, trackBarAEMinExposure };
				m_TextBoxList = new TextBox[] { txtExposure, txtGain, txtDigitalGain, txtALCTarget, txtAGCMaxGain, txtAGCMinGain, txtAEMaxExposure, txtAEMinExposure };
				m_LabelList = new Label[] { lblExposureValue, lblGainValue, lblDigitalGainValue, null, lblAGCMaxGain, lblAGCMinGain, lblAEMaxExposure, lblAEMinExposure };
                m_ComboBoxList = new StComboBox[] { cmbALCMode, cmbAdjustmentDigitalGain };
			}
			else if(tabControl.SelectedTab.Equals(tabPageWB))
            {
                m_TrackBarList = new TrackBar[] { trackBarWBRGain, trackBarWBGrGain, trackBarWBGbGain, trackBarWBBGain, trackBarHue, trackBarSaturation, trackBarChromaSuppresionStartLevel, trackBarChromaSuppresionSuppressionLevel, trackBarLowChromaSuppresionStartLevel, trackBarLowChromaSuppresionSuppressionLevel };
                m_TextBoxList = new TextBox[] { txtWBRGain, txtWBGrGain, txtWBGbGain, txtWBBGain, txtHue, txtSaturation, txtChromaSuppresionStartLevel, txtChromaSuppresionSuppressionLevel, txtLowChromaSuppresionStartLevel, txtLowChromaSuppresionSuppressionLevel };
                m_LabelList = new Label[] { lblWBRGainValue, lblWBGrGainValue, lblWBGbGainValue, lblWBBGainValue, lblHueValue, lblSaturationValue, null, null, null, null };
				m_ComboBoxList = new StComboBox[]{cmbWBMode, cmbHueSaturationMode};
            }
            else if (tabControl.SelectedTab.Equals(tabPageHDR_CMOSIS4M))
            {

                m_TrackBarList = new TrackBar[] { trackBarHDR_CMOSIS4M_Knee1, trackBarHDR_CMOSIS4M_Vlow2, trackBarHDR_CMOSIS4M_Knee2, trackBarHDR_CMOSIS4M_Vlow3 };
                m_TextBoxList = new TextBox[] { txtHDR_CMOSIS4M_Knee1, txtHDR_CMOSIS4M_Vlow2, txtHDR_CMOSIS4M_Knee2, txtHDR_CMOSIS4M_Vlow3 };
                m_LabelList = new Label[] { null, null, null, null };
                m_ComboBoxList = new StComboBox[] { cmbHDR_CMOSIS4M_Mode, cmbHDR_CMOSIS4M_SlopeNum };
            }
			else if(tabControl.SelectedTab.Equals(tabPageTriggerMode))
			{
                m_TrackBarList = new TrackBar[] { trackBarTriggerDelay, trackBarAutoTriggerTime };
                m_TextBoxList = new TextBox[] { txtTriggerDelay, txtAutoTriggerTime };
                m_LabelList = new Label[] { lblTriggerDelay, lblAutoTriggerTime };
                m_ComboBoxList = new StComboBox[]{cmbExposureMode, cmbTriggerSelector, cmbTriggerMode, cmbTriggerSource, cmbNoiseReduction, 
													 cmbExposureWaitHD, cmbExposureWaitReadOut, cmbExposureEnd, cmbCameraMemory, cmbTriggerOverlap, cmbSensorShutterMode};

                m_ButtonList = new Button[] { btnFrameStartTrigger, btnExposureStartTrigger, btnExposureEndTrigger, btnSensorReadOutStartTrigger, btnResetFrameNo };
				m_CheckBoxList = new CheckBox[]{chkAutoTrigger};

			}
			else if(tabControl.SelectedTab.Equals(tabPageTriggerTiming))
			{
				m_TrackBarList = new TrackBar[]{trackBarStrobeStartDelay, trackBarStrobeEndDelay, 
										trackBarOutputPulseDelay, trackBarOutputPulseDuration, trackBarReadOutDelay, trackBarLineDebounceTime};


				m_LabelList = new Label[]{lbltrackBarStrobeStartDelayValue, lblStrobeEndDelayValue, 
										lblOutputPulseDelayValue, lblOutputPulseDurationValue, lblReadOutDelayValue, lblLineDebounceTimeValue};



				m_TextBoxList = new TextBox[]{txtStrobeStartDelay, txtStrobeEndDelay, 
										txtOutputPulseDelay, txtOutputPulseDuration, txtReadOutDelay, txtLineDebounceTime};

			}
			else if(tabControl.SelectedTab.Equals(tabPageIO))
			{
				m_ComboBoxList = new StComboBox[]{cmbIOInOut0, cmbIOInOut1, cmbIOInOut2, cmbIOInOut3,
													 cmbIOMode0, cmbIOMode1, cmbIOMode2, cmbIOMode3,
													 cmbIOPolarity0, cmbIOPolarity1, cmbIOPolarity2, cmbIOPolarity3,
													 cmbIOStatus0, cmbIOStatus1, cmbIOStatus2, cmbIOStatus3,
													 cmbLEDGreen, cmbLEDRed,
                                                    cmbResetSwitch};
                m_ReadOnlyTextBoxList = new TextBox[] { txtSWStatus0, txtSWStatus1, txtSWStatus2, txtSWStatus3, txtCameraType, txtFPGAVersion, txtFirmVersion, txtSDKVersioin, txtTemperature };
			}
			else if (tabControl.SelectedTab.Equals(tabPageY))
            {
                m_ComboBoxList = new StComboBox[] { cmbSharpnessMode, cmbYGammaMode, cmbShadingCorrectionMode };
                m_TrackBarList = new TrackBar[] { trackBarSharpnessGain, trackBarSharpnessCoring, trackBarYGamma, trackBarCameraGamma, trackBarDigitalClamp, trackBarShadingCorrectionTarget, trackBarAnalogBlackLevel };
                m_TextBoxList = new TextBox[] { txtSharpnessGain, txtSharpnessCoring, txtYGamma, txtCameraGamma, stTextBoxDigitalClamp, txtShadingCorrectionTarget, txtAnalogBlackLevel };
                m_LabelList = new Label[] { null, null, null, null, null, null, null };
			}
			else if (tabControl.SelectedTab.Equals(tabPageOther))
			{
                m_ComboBoxList = new StComboBox[] { cmbScanMode, cmbClockMode, cmbColorInterpolation, cmbMirror, cmbRotation, cmbTransferBitsPerPixel, stComboBoxHBinningSkipping, stComboBoxVBinningSkipping, cmbHBinningSumMode, cmbVBinningSumMode, cmbCurrentRegion, cmbRegionMode, cmbDisplayPixelFormat };
				m_TrackBarList = new TrackBar[] { trackBarImageOffsetX, trackBarImageOffsetY, trackBarImageWidth, trackBarImageHeight, trackBarVBlankForFPS };
				m_TextBoxList = new TextBox[] {  stTextBoxImageOffsetX, stTextBoxImageOffsetY, stTextBoxImageWidth, stTextBoxImageHeight, stTextBoxVBlankForFPS };
				m_LabelList = new Label[] { null, null, null, null, labelOutputFPS };
			}
			else if (tabControl.SelectedTab.Equals(tabPageColorGamma))
			{
				m_ComboBoxList = new StComboBox[] { cmbRGammaMode, cmbGRGammaMode, cmbGBGammaMode, cmbBGammaMode };
				m_TrackBarList = new TrackBar[] { trackBarRGamma, trackBarGRGamma, trackBarGBGamma, trackBarBGamma };
				m_TextBoxList = new TextBox[] { txtRGamma, txtGRGamma, txtGBGamma, txtBGamma };
			}
			else if (tabControl.SelectedTab.Equals(tabPageDefectPixelCorrection))
			{
				m_ComboBoxList = new StComboBox[] { cmbDefectPixelCorrectionMode };
				defectPixelSetting1.GetTrackBarCtrl(ref m_TrackBarList);
				defectPixelSetting1.GetTextBoxCtrl(ref m_TextBoxList);
			}
		}
		private void UpdateDisplay()
		{
			#region UpdateDisplay : TrackBar , TextBox, Label
			//tabControl.
			if(null != m_TrackBarList)
			{
				//TrackBar, TextBox, Label
				for(int i = 0; i < m_TrackBarList.Length; i++)
				{
					TrackBar trackBar = m_TrackBarList[i];
					TextBox textBox = m_TextBoxList[i];
					Label label = null;
					if(null != m_LabelList)
					{
						label = m_LabelList[i];
					}
					int MinValue = 0;
					int MaxValue = 0;
					int CurValue = 0;
					int LargeChange = 10;
					string strValue = "";
					bool enabled = true;

					if (trackBar.Equals(trackBarExposure))
					{
						//Shutter / Gain
						MaxValue = (int)m_objStCamera.MaxExposureClock;
						LargeChange = MaxValue / 10;
						CurValue = (int)m_objStCamera.ExposureClock;
						strValue = m_objStCamera.ExposureClockText;
						enabled = !m_objStCamera.IsAEOn;
                    }
                    else if (trackBar.Equals(trackBarGain))
                    {
                        if (m_objStCamera.HasAnalogGain)
                        {
                            MaxValue = m_objStCamera.MaxGain;
                            LargeChange = 16;
                            CurValue = (int)m_objStCamera.Gain;
                            strValue = String.Format("{0:N2} dB", m_objStCamera.CurrrentGainDB);
                            if (!m_objStCamera.IsDigitalGainCtrl)
                            {
                                enabled = !m_objStCamera.IsAGCOn;
                            }
                        }
                        else
                        {
                            enabled = false;
                        }

                    }
                    else if (trackBar.Equals(trackBarDigitalGain))
                    {
                        if (m_objStCamera.HasDigitalGainFunction())
                        {
                            MinValue = (int)m_objStCamera.DigitalGainOffValue;
                            MaxValue = (int)m_objStCamera.MaxDigitalGain;
                            LargeChange = 16;
                            CurValue = (int)m_objStCamera.DigitalGain;
                            strValue = String.Format("x {0:N2}", m_objStCamera.DigitalGainTimes);
                            if (m_objStCamera.IsDigitalGainCtrl)
                            {
                                enabled = !m_objStCamera.IsAGCOn;
                            }
                        }
                        else
                        {
                            enabled = false;
                        }
                    }
					else if (trackBar.Equals(trackBarALCTarget))
					{
						MaxValue = 255;
						LargeChange = 10;
						CurValue = (int)m_objStCamera.ALCTargetLevel;

						enabled = m_objStCamera.ALCMode != StTrg.STCAM_ALCMODE_OFF;
                    }
                    else if (trackBar.Equals(trackBarAGCMaxGain))
                    {
                        if (m_objStCamera.IsDigitalGainCtrl)
                        {
                            MaxValue = (int)m_objStCamera.MaxDigitalGain;
                            strValue = String.Format("x {0:N2}", m_objStCamera.AGCMaxGainTimes);
                        }
                        else
                        {
                            MaxValue = (int)m_objStCamera.MaxGain;
                            strValue = String.Format("{0:N2} dB", m_objStCamera.AGCMaxGainDB);
                        }
                        LargeChange = MaxValue / 10;
                        CurValue = (int)m_objStCamera.AGCMaxGain;

                        enabled = m_objStCamera.IsAGCOn;
                    }
                    else if (trackBar.Equals(trackBarAGCMinGain))
                    {
                        if (m_objStCamera.IsDigitalGainCtrl)
                        {
                            MaxValue = (int)m_objStCamera.MaxDigitalGain;
                            strValue = String.Format("x {0:N2}", m_objStCamera.AGCMinGainTimes);
                        }
                        else
                        {
                            MaxValue = (int)m_objStCamera.MaxGain;
                            strValue = String.Format("{0:N2} dB", m_objStCamera.AGCMinGainDB);
                        }
                        LargeChange = MaxValue / 10;
                        CurValue = (int)m_objStCamera.AGCMinGain;

                        enabled = m_objStCamera.IsAGCOn;
                    }
					else if (trackBar.Equals(trackBarAEMaxExposure))
					{
						MaxValue = (int)m_objStCamera.MaxExposureClock;
						LargeChange = MaxValue / 10;
						CurValue = (int)m_objStCamera.AEMaxExposureClock;
						strValue = m_objStCamera.AEMaxExposureClockText;
						enabled = m_objStCamera.IsAEOn;
					}
					else if (trackBar.Equals(trackBarAEMinExposure))
					{
						MaxValue = (int)m_objStCamera.MaxExposureClock;
						LargeChange = MaxValue / 10;
						CurValue = (int)m_objStCamera.AEMinExposureClock;
						strValue = m_objStCamera.AEMinExposureClockText;
						enabled = m_objStCamera.IsAEOn;
                    }
                    else if (trackBar.Equals(trackBarHDR_CMOSIS4M_Knee1))
                    {
                        MinValue = 0;
                        MaxValue = 255;
                        LargeChange = 5;
                        CurValue = (int)m_objStCamera.HDR_CMOSIS4M_Knee1;
                        enabled = ((0 < m_objStCamera.HDR_CMOSIS4M_Mode) && (2 <= m_objStCamera.HDR_CMOSIS4M_SlopeNum));
                    }
                    else if (trackBar.Equals(trackBarHDR_CMOSIS4M_Vlow2))
                    {
                        MinValue = 0;
                        MaxValue = 64;
                        LargeChange = 5;
                        CurValue = (int)m_objStCamera.HDR_CMOSIS4M_Vlow2;
                        enabled = ((0 < m_objStCamera.HDR_CMOSIS4M_Mode) && (2 <= m_objStCamera.HDR_CMOSIS4M_SlopeNum));
                    }
                    else if (trackBar.Equals(trackBarHDR_CMOSIS4M_Knee2))
                    {
                        MinValue = 0;
                        MaxValue = 255;
                        LargeChange = 5;
                        CurValue = (int)m_objStCamera.HDR_CMOSIS4M_Knee2;
                        enabled = ((0 < m_objStCamera.HDR_CMOSIS4M_Mode) && (3 <= m_objStCamera.HDR_CMOSIS4M_SlopeNum));
                    }
                    else if (trackBar.Equals(trackBarHDR_CMOSIS4M_Vlow3))
                    {
                        MinValue = 0;
                        MaxValue = 64;
                        LargeChange = 5;
                        CurValue = (int)m_objStCamera.HDR_CMOSIS4M_Vlow3;
                        enabled = ((0 < m_objStCamera.HDR_CMOSIS4M_Mode) && (3 <= m_objStCamera.HDR_CMOSIS4M_SlopeNum));
                    }
					else if (trackBar.Equals(trackBarImageOffsetX))
					{
						MaxValue = (int)m_objStCamera.MaximumImageWidth - 4;
						LargeChange = 128;
						CurValue = (int)m_objStCamera.ImageOffsetX;
						strValue = m_objStCamera.ImageOffsetX.ToString();
						enabled = m_objStCamera.EnableImageOffsetX;
					}
					else if (trackBar.Equals(trackBarImageOffsetY))
					{
						MaxValue = (int)m_objStCamera.MaximumImageHeight - 4;
						LargeChange = 128;
						CurValue = (int)m_objStCamera.ImageOffsetY;
						strValue = m_objStCamera.ImageOffsetY.ToString();
						enabled = m_objStCamera.EnableImageOffsetY;
					}
					else if (trackBar.Equals(trackBarImageWidth))
					{
						MaxValue = (int)m_objStCamera.MaximumImageWidth;
						LargeChange = 128;
						CurValue = (int)m_objStCamera.ImageWidth;
						strValue = m_objStCamera.ImageWidth.ToString();
						enabled = m_objStCamera.EnableImageWidth;
					}
					else if (trackBar.Equals(trackBarImageHeight))
					{
						MaxValue = (int)m_objStCamera.MaximumImageHeight;
						LargeChange = 128;
						CurValue = (int)m_objStCamera.ImageHeight;
						strValue = m_objStCamera.ImageHeight.ToString();
						enabled = m_objStCamera.EnableImageHeight;
					}
					else if (trackBar.Equals(trackBarVBlankForFPS))
					{
						MaxValue = (int)m_objStCamera.MaxVBlankForFPS;
						LargeChange = 64;
						CurValue = (int)m_objStCamera.VBlankForFPS;
						strValue = m_objStCamera.OutputFPS.ToString("F2") + "FPS";
						enabled = m_objStCamera.HasVBlankForFPS();
					}
					else if(trackBar.Equals(trackBarSharpnessGain))
					{
						MinValue = 0;
						MaxValue = 500;
						LargeChange = 50;
						CurValue = (int)m_objStCamera.SharpnessGain;
						if(StTrg.STCAM_SHARPNESS_OFF == m_objStCamera.SharpnessMode)
						{
							enabled = false;
						}
					}
					else if(trackBar.Equals(trackBarSharpnessCoring))
					{
						MinValue = 0;
						MaxValue = 255;
						LargeChange = 16;
						CurValue = (int)m_objStCamera.SharpnessCoring;
						if(StTrg.STCAM_SHARPNESS_OFF == m_objStCamera.SharpnessMode)
						{
							enabled = false;
						}
                    }
                    else if (trackBar.Equals(trackBarYGamma))
                    {
                        MinValue = 1;
                        MaxValue = 500;
                        LargeChange = 10;
                        CurValue = (int)m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_Y];
                        if (StTrg.STCAM_GAMMA_OFF == m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_Y])
                        {
                            enabled = false;
                        }
                    }
                    else if (trackBar.Equals(trackBarRGamma))
                    {
                        MinValue = 1;
                        MaxValue = 500;
                        LargeChange = 10;
                        CurValue = (int)m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_R];
                        if (StTrg.STCAM_GAMMA_OFF == m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_R])
                        {
                            enabled = false;
                        }
                    }
                    else if (trackBar.Equals(trackBarGRGamma))
                    {
                        MinValue = 1;
                        MaxValue = 500;
                        LargeChange = 10;
                        CurValue = (int)m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_GR];
                        if (StTrg.STCAM_GAMMA_OFF == m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_GR])
                        {
                            enabled = false;
                        }
                    }
                    else if (trackBar.Equals(trackBarGBGamma))
                    {
                        MinValue = 1;
                        MaxValue = 500;
                        LargeChange = 10;
                        CurValue = (int)m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_GB];
                        if (StTrg.STCAM_GAMMA_OFF == m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_GB])
                        {
                            enabled = false;
                        }
                    }
                    else if (trackBar.Equals(trackBarBGamma))
                    {
                        MinValue = 1;
                        MaxValue = 500;
                        LargeChange = 10;
                        CurValue = (int)m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_B];
                        if (StTrg.STCAM_GAMMA_OFF == m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_B])
                        {
                            enabled = false;
                        }
                    }

					else if (trackBar.Equals(trackBarWBRGain))
					{
						if (m_objStCamera.ColorArray != StTrg.STCAM_COLOR_ARRAY_MONO)
						{
							MaxValue = 511;
							LargeChange = 16;
							CurValue = (int)m_objStCamera.WBRGain;
							strValue = String.Format("x {0:N2}", CurValue / 128.0);
						}
						else
						{
							enabled = false;
						}
					}
					else if (trackBar.Equals(trackBarWBGrGain))
					{
						if (m_objStCamera.ColorArray != StTrg.STCAM_COLOR_ARRAY_MONO)
						{
							MaxValue = 511;
							LargeChange = 16;
							CurValue = (int)m_objStCamera.WBGrGain;
							strValue = String.Format("x {0:N2}", CurValue / 128.0);
						}
						else
						{
							enabled = false;
						}
					}
					else if (trackBar.Equals(trackBarWBGbGain))
					{
						if (m_objStCamera.ColorArray != StTrg.STCAM_COLOR_ARRAY_MONO)
						{
							MaxValue = 511;
							LargeChange = 16;
							CurValue = (int)m_objStCamera.WBGbGain;
							strValue = String.Format("x {0:N2}", CurValue / 128.0);
						}
						else
						{
							enabled = false;
						}
					}
					else if (trackBar.Equals(trackBarWBBGain))
					{
						if (m_objStCamera.ColorArray != StTrg.STCAM_COLOR_ARRAY_MONO)
						{
							MaxValue = 511;
							LargeChange = 16;
							CurValue = (int)m_objStCamera.WBBGain;
							strValue = String.Format("x {0:N2}", CurValue / 128.0);
						}
						else
						{
							enabled = false;
						}
					}
                    else if (trackBar.Equals(trackBarHue))
                    {
                        if (
                            (StTrg.STCAM_COLOR_ARRAY_MONO != m_objStCamera.ColorArray) &&
                            (StTrg.STCAM_HUE_SATURATION_OFF != m_objStCamera.HueSaturationMode)
                            )
                        {
                            MinValue = -1800;
                            MaxValue = 1800;
                            LargeChange = 10;
                            CurValue = (int)m_objStCamera.Hue;
                            strValue = String.Format("{0:N1}", CurValue / 10.0);
                        }
                        else
                        {
                            enabled = false;
                        }
                    }
                    else if (trackBar.Equals(trackBarSaturation))
                    {
                        if (
                            (StTrg.STCAM_COLOR_ARRAY_MONO != m_objStCamera.ColorArray) &&
                            (StTrg.STCAM_HUE_SATURATION_OFF != m_objStCamera.HueSaturationMode)
                            )
                        {
                            MaxValue = 200;
                            LargeChange = 10;
                            CurValue = (int)m_objStCamera.Saturation;
                            strValue = String.Format("{0:N2}", CurValue / 100.0);
                        }
                        else
                        {
                            enabled = false;
                        }
                    }
                    else if (trackBar.Equals(trackBarChromaSuppresionStartLevel))
                    {
                        if (StTrg.STCAM_COLOR_ARRAY_MONO != m_objStCamera.ColorArray)
                        {
                            MaxValue = 255;
                            LargeChange = 10;
                            CurValue = (int)m_objStCamera.HighChromaSuppressionStartLevel;
                        }
                        else
                        {
                            enabled = false;
                        }
                    }
                    else if (trackBar.Equals(trackBarChromaSuppresionSuppressionLevel))
                    {
                        if (
                            (StTrg.STCAM_COLOR_ARRAY_MONO != m_objStCamera.ColorArray) &&
                            (m_objStCamera.HighChromaSuppressionStartLevel < 255)
                            )
                        {
                            MaxValue = 255;
                            LargeChange = 10;
                            CurValue = (int)m_objStCamera.HighChromaSuppressionSuppressionLevel;
                        }
                        else
                        {
                            enabled = false;
                        }
                    }
                    else if (trackBar.Equals(trackBarLowChromaSuppresionStartLevel))
                    {
                        if (StTrg.STCAM_COLOR_ARRAY_MONO != m_objStCamera.ColorArray)
                        {
                            MaxValue = 255;
                            LargeChange = 10;
                            CurValue = (int)m_objStCamera.LowChromaSuppressionStartLevel;
                        }
                        else
                        {
                            enabled = false;
                        }
                    }
                    else if (trackBar.Equals(trackBarLowChromaSuppresionSuppressionLevel))
                    {
                        if (
                            (StTrg.STCAM_COLOR_ARRAY_MONO != m_objStCamera.ColorArray) &&
                            (0 < m_objStCamera.LowChromaSuppressionStartLevel)
                            )
                        {
                            MaxValue = 255;
                            LargeChange = 10;
                            CurValue = (int)m_objStCamera.LowChromaSuppressionSuppressionLevel;
                        }
                        else
                        {
                            enabled = false;
                        }
                    }
                    else if (trackBar.Equals(trackBarAutoTriggerTime))
                    {
                        if (
                            m_objStCamera.IsEnableTriggerSoftware(StTrg.STCAM_TRIGGER_SELECTOR_EXPOSURE_START) &&
                            (m_objStCamera.ExposureMode == StTrg.STCAM_EXPOSURE_MODE_TRIGGER_CONTROLLED) && 
                            (m_objStCamera.AutoTrigger)
                            )
                        {
                            MinValue = 500;
                            MaxValue = 600000;
                            LargeChange = 10;
                            CurValue = (int)m_objStCamera.AutoTriggerDueTime;
                            int ms = CurValue;
                            int min = ms / 60000;
                            strValue = "";
                            if (0 != min)
                            {
                                ms -= min * 60000;
                                strValue = min + "min ";
                            }
                            int s = ms / 1000;
                            if (0 != s)
                            {
                                ms -= s * 1000;
                                strValue = strValue + s + "s ";
                            }
                            if (0 != ms)
                            {
                                strValue = strValue + ms + "ms";
                            }
                        }
                        else
                        {
                            enabled = false;
                        }
                    }
                    else if (trackBar.Equals(trackBarTriggerDelay))
                    {
                        if (m_objStCamera.HasTriggerFunction())
                        {
                            if (m_objStCamera.IsIOUnitUs())
                            {
                                MaxValue = 0x3FFFF;
                            }
                            else
                            {
                                MaxValue = 4094;
                            }
                            strValue = m_objStCamera.TriggerDelayString;
                            LargeChange = 256;
                            CurValue = (int)m_objStCamera.TriggerDelay;
                            enabled = (m_objStCamera.TriggerMode == StTrg.STCAM_TRIGGER_MODE_ON);
                        }
                        else
                        {
                            enabled = false;
                        }
                    }


					else if (trackBar.Equals(trackBarStrobeStartDelay))
					{
						if (m_objStCamera.IsIOUnitUs())
						{
							MaxValue = 0x3FFFF;
						}
						else
						{
							MaxValue = 4094;
						}
						LargeChange = 256;
						CurValue = (int)m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_STROBE_START_DELAY];
						strValue = m_objStCamera.TriggerTimingText[StTrg.STCAM_TRIGGER_TIMING_STROBE_START_DELAY];
					}
					else if (trackBar.Equals(trackBarStrobeEndDelay))
					{
						if (m_objStCamera.IsIOUnitUs())
						{
							MaxValue = 0x3FFFF;
						}
						else
						{
							MaxValue = 4094;
						}
						LargeChange = 256;
						CurValue = (int)m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_STROBE_END_DELAY];
						strValue = m_objStCamera.TriggerTimingText[StTrg.STCAM_TRIGGER_TIMING_STROBE_END_DELAY];
					}
					else if (trackBar.Equals(trackBarOutputPulseDelay))
					{
						if (m_objStCamera.IsIOUnitUs())
						{
							MaxValue = 0x3FFFF;
						}
						else
						{
							MaxValue = 4094;
						}
						LargeChange = 256;
						CurValue = (int)m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_OUTPUT_PULSE_DELAY];
						strValue = m_objStCamera.TriggerTimingText[StTrg.STCAM_TRIGGER_TIMING_OUTPUT_PULSE_DELAY];
					}
					else if (trackBar.Equals(trackBarOutputPulseDuration))
					{
						if (m_objStCamera.IsIOUnitUs())
						{
							MaxValue = 0x3FFFF;
						}
						else
						{
							MaxValue = 4094;
						}
						LargeChange = 256;
						CurValue = (int)m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_OUTPUT_PULSE_DURATION];
						strValue = m_objStCamera.TriggerTimingText[StTrg.STCAM_TRIGGER_TIMING_OUTPUT_PULSE_DURATION];
					}
					else if (trackBar.Equals(trackBarReadOutDelay))
					{
						MaxValue = 65535;
						LargeChange = 256;
						CurValue = (int)m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_READOUT_DELAY];
						strValue = m_objStCamera.TriggerTimingText[StTrg.STCAM_TRIGGER_TIMING_READOUT_DELAY];
						enabled = m_objStCamera.HasReadOut();
                    }
                    else if (trackBar.Equals(trackBarLineDebounceTime))
                    {
                        MaxValue = 10000;
                        LargeChange = 100;
                        CurValue = (int)m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_LINE_DEBOUNCE_TIME];
                        strValue = m_objStCamera.TriggerTimingText[StTrg.STCAM_TRIGGER_TIMING_LINE_DEBOUNCE_TIME];
                        enabled = m_objStCamera.HasLineDebounceTime();
                    }
					else if (tabControl.SelectedTab.Equals(tabPageDefectPixelCorrection))
					{
						ushort nDefectPixelIndex = (ushort)(i / 2);
						uint x = 0;
						uint y = 0;
						m_objStCamera.GetDefectPixelCorrectionPosition(nDefectPixelIndex, out x, out y);


						MaxValue = 2047;
						LargeChange = 64;
						CurValue = (int)(((i % 2) == 0) ? x : y);
					}
					else if (trackBar.Equals(trackBarCameraGamma))
					{

						MinValue = 0;
						MaxValue = 40;
						LargeChange = 10;
						CurValue = (int)m_objStCamera.CameraGamma;
						enabled = m_objStCamera.HasCameraGamma();
					}
					else if (trackBar.Equals(trackBarDigitalClamp))
					{
                        MinValue = 0;
                        MaxValue = (int)m_objStCamera.MaxDigitalClamp;
						LargeChange = 8;
						CurValue = (int)m_objStCamera.DigitalClamp;
						enabled = m_objStCamera.HasDigitalClamp();
                    }
                    else if (trackBar.Equals(trackBarAnalogBlackLevel))
                    {
                        MinValue = 0;
                        MaxValue = (int)m_objStCamera.MaxAnalogBlackLevel;
                        LargeChange = 8;
                        CurValue = (int)m_objStCamera.AnalogBlackLevel;
                        enabled = m_objStCamera.HasAnalogBlackLevel();
                    }
                    else if (trackBar.Equals(trackBarShadingCorrectionTarget))
                    {
                        MinValue = 0;
                        MaxValue = 255;
                        LargeChange = 8;
                        CurValue = (int)m_objStCamera.ShadingCorrectionTarget;
                        enabled = (m_objStCamera.ShadingCorrectionMode == StTrg.STCAM_SHADING_CORRECTION_MODE_OFF);
                    }

						
					if(CurValue < MinValue)	MinValue = CurValue;
					if(MaxValue < CurValue) MaxValue = CurValue;
					trackBar.SetRange(MinValue, MaxValue);
					trackBar.Value = CurValue;
					trackBar.LargeChange = LargeChange;
					textBox.Text = CurValue.ToString();
					if(null != label)
					{
						label.Text = strValue;
						label.Enabled = enabled;
					}

					trackBar.Enabled = enabled;
					textBox.Enabled = enabled;
				}

			}
			#endregion
			#region ComboBox
			//Combo Box
			if(null != m_ComboBoxList)
			{
				for(int i = 0; i < m_ComboBoxList.Length; i++)
				{
					StComboBox comboBox = m_ComboBoxList[i];
					while(0 < comboBox.Items.Count)comboBox.Items.RemoveAt(0);

					StComboBoxItem[] list = null;
					uint Value = 0;
					bool forceDisabled = false;

					if (comboBox.Equals(cmbALCMode))
					{
						if (m_objStCamera.HasAE() && m_objStCamera.HasAGC())
						{
							list = new StComboBoxItem[]{
														   new StComboBoxItem("OFF", StTrg.STCAM_ALCMODE_OFF),
														   new StComboBoxItem("AE ON", StTrg.STCAM_ALCMODE_CAMERA_AE_ON),
														   new StComboBoxItem("AGC ON", StTrg.STCAM_ALCMODE_CAMERA_AGC_ON),
														   new StComboBoxItem("AE/AGC ON", StTrg.STCAM_ALCMODE_CAMERA_AE_AGC_ON)
													   };
							Value = m_objStCamera.ALCMode;
						}
						else if (m_objStCamera.HasAE())
						{
							list = new StComboBoxItem[]{
														   new StComboBoxItem("OFF", StTrg.STCAM_ALCMODE_OFF),
														   new StComboBoxItem("AE ON", StTrg.STCAM_ALCMODE_CAMERA_AE_ON)
													   };
							Value = m_objStCamera.ALCMode;
						}
						else if (m_objStCamera.HasAGC())
						{
							list = new StComboBoxItem[]{
														   new StComboBoxItem("OFF", StTrg.STCAM_ALCMODE_OFF),
														   new StComboBoxItem("AGC ON", StTrg.STCAM_ALCMODE_CAMERA_AGC_ON)
													   };
							Value = m_objStCamera.ALCMode;
						}
						else
						{
							list = new StComboBoxItem[]{
								   new StComboBoxItem("OFF", StTrg.STCAM_ALCMODE_OFF),
								   new StComboBoxItem("AE/AGC ON", StTrg.STCAM_ALCMODE_PC_AE_AGC_ON),
									new StComboBoxItem("AE ON", StTrg.STCAM_ALCMODE_PC_AE_ON),
									new StComboBoxItem("AGC ON", StTrg.STCAM_ALCMODE_PC_AGC_ON),
									new StComboBoxItem("AE/AGC OneShot", StTrg.STCAM_ALCMODE_PC_AE_AGC_ONESHOT),
									new StComboBoxItem("AE OneShot", StTrg.STCAM_ALCMODE_PC_AE_ONESHOT),
									new StComboBoxItem("AGC OneShot", StTrg.STCAM_ALCMODE_PC_AGC_ONESHOT)
							   };
							Value = m_objStCamera.ALCMode;
						}
                    }
                    else if (comboBox.Equals(cmbAdjustmentDigitalGain))
                    {
                        bool hasAdjustmentDigitalGain = m_objStCamera.HasAdjustmentModeDigitalGain();
                        comboBox.Visible = labelAdjustmentDigitalGain.Visible = hasAdjustmentDigitalGain;
                        if (hasAdjustmentDigitalGain)
                        {
                            list = new StComboBoxItem[]{
														   new StComboBoxItem("OFF", 0),
														   new StComboBoxItem("ON", 1)
													   };
                            Value = (uint)(m_objStCamera.EnableAdjustmentDigitalGain ? 1 : 0);
                        }
                    }
                    else if (comboBox.Equals(cmbHDR_CMOSIS4M_Mode))
                    {
                        if (m_objStCamera.HDRType == StTrg.STCAM_HDR_TYPE_CMOSIS_4M)
                        {
                            list = new StComboBoxItem[]{
														   new StComboBoxItem("OFF", 0),
														   new StComboBoxItem("ON", 1)
													   };
                            Value = (uint)m_objStCamera.HDR_CMOSIS4M_Mode;
                        }
                    }
                    else if (comboBox.Equals(cmbHDR_CMOSIS4M_SlopeNum))
                    {
                        if (m_objStCamera.HDRType == StTrg.STCAM_HDR_TYPE_CMOSIS_4M)
                        {
                            list = new StComboBoxItem[]{
														   new StComboBoxItem("1", 1),
														   new StComboBoxItem("2", 2),
														   new StComboBoxItem("3", 3)
													   };
                            Value = (uint)m_objStCamera.HDR_CMOSIS4M_SlopeNum;
                            forceDisabled = (m_objStCamera.HDR_CMOSIS4M_Mode == 0);
                        }
                    }
					else if (comboBox.Equals(cmbWBMode))
					{
						if(m_objStCamera.ColorArray != StTrg.STCAM_COLOR_ARRAY_MONO)
						{
							list = new StComboBoxItem[]{
														   new StComboBoxItem("OFF", StTrg.STCAM_WB_OFF),
														   new StComboBoxItem("Manual", StTrg.STCAM_WB_MANUAL),
														   new StComboBoxItem("One Shot", StTrg.STCAM_WB_ONESHOT),
														   new StComboBoxItem("Full Auto", StTrg.STCAM_WB_FULLAUTO)
													   };
							Value = m_objStCamera.WhiteBalanceMode;
						}
					}
					else if(comboBox.Equals(cmbHueSaturationMode))
					{
						if(m_objStCamera.ColorArray != StTrg.STCAM_COLOR_ARRAY_MONO)
						{
							list = new StComboBoxItem[]{
														   new StComboBoxItem("OFF", StTrg.STCAM_HUE_SATURATION_OFF),
														   new StComboBoxItem("ON", StTrg.STCAM_HUE_SATURATION_ON)
													   };
							Value = m_objStCamera.HueSaturationMode;
						}

					}
                    else if (comboBox.Equals(cmbTriggerMode))
                    {
                        if (m_objStCamera.HasTriggerFunction())
                        {
                            int count = 2;
                            list = new StComboBoxItem[count];

                            list[0] = new StComboBoxItem("OFF", StTrg.STCAM_TRIGGER_MODE_OFF);
                            list[1] = new StComboBoxItem("ON", StTrg.STCAM_TRIGGER_MODE_ON);
                            Value = m_objStCamera.TriggerMode;
                        }
                    }
                    else if (comboBox.Equals(cmbTriggerSource))
                    {
                        if (m_objStCamera.HasTriggerFunction())
                        {
                            int count = 2;
                            bool isGenICamIO = m_objStCamera.HasGenICamIO();

                            bool isFrameBurst = (m_objStCamera.TriggerSelector == StTrg.STCAM_TRIGGER_SELECTOR_FRAME_BURST_START);
                            if (isFrameBurst)
                            {
                                isGenICamIO = true;
                            }
                            bool[] pIsInputPin = new bool[4];
                            if (isGenICamIO)
                            {
                                for (int pin = 0; pin < pIsInputPin.GetLength(0); pin++)
                                {
                                    pIsInputPin[pin] = false;
                                    if (m_objStCamera.IOPinExistence[(byte)pin])
                                    {
                                        if (m_objStCamera.IOPinInOut[(byte)pin] == 0)
                                        {
                                            pIsInputPin[pin] = true;
                                            count++;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                count = 3;
                            }

                            list = new StComboBoxItem[count];

                            list[0] = new StComboBoxItem("Disabled", StTrg.STCAM_TRIGGER_SOURCE_DISABLED);
                            list[1] = new StComboBoxItem("Software", StTrg.STCAM_TRIGGER_SOURCE_SOFTWARE);

                            Value = m_objStCamera.TriggerSource;
                            if (isGenICamIO)
                            {
                                int nIndex = 2;
                                for (int pin = 0; pin < pIsInputPin.GetLength(0); pin++)
                                {
                                    if (pIsInputPin[pin])
                                    {
                                        list[nIndex] = new StComboBoxItem("Line" + pin.ToString(), StTrg.STCAM_TRIGGER_SOURCE_LINE0 + pin);
                                        nIndex++;
                                    }
                                }
                            }
                            else
                            {
                                list[2] = new StComboBoxItem("Hardware", StTrg.STCAM_TRIGGER_SOURCE_HARDWARE);
                                if (2 < Value) Value = 2;
                            }
                            forceDisabled = (m_objStCamera.TriggerMode != StTrg.STCAM_TRIGGER_MODE_ON);

                        }
                    }

                    else if (comboBox.Equals(cmbExposureMode))
                    {
                        if (m_objStCamera.HasTriggerFunction())
                        {
                            int count = 2;
                            bool hasTriggerControlled = m_objStCamera.HasExposureModeTriggerControlled();
                            bool hasTriggerWidth = m_objStCamera.HasExposureModeTriggerWidth();
                            if (hasTriggerControlled) count++;
                            if (hasTriggerWidth) count++;
                            list = new StComboBoxItem[count];

                            int index = 0;
                            list[index++] = new StComboBoxItem("OFF", StTrg.STCAM_EXPOSURE_MODE_OFF);
                            list[index++] = new StComboBoxItem("Timed", StTrg.STCAM_EXPOSURE_MODE_TIMED);
                            if (hasTriggerWidth) list[index++] = new StComboBoxItem("Trigger Width", StTrg.STCAM_EXPOSURE_MODE_TRIGGER_WIDTH);
                            if (hasTriggerControlled) list[index++] = new StComboBoxItem("Trigger Controlled", StTrg.STCAM_EXPOSURE_MODE_TRIGGER_CONTROLLED);
                            Value = m_objStCamera.ExposureMode;
                        }
                    }
                    else if (comboBox.Equals(cmbTriggerSelector))
                    {
                        if (m_objStCamera.HasTriggerFunction())
                        {
                            uint[] aSelector = new uint[]{
                                    StTrg.STCAM_TRIGGER_SELECTOR_FRAME_START, 
                                    StTrg.STCAM_TRIGGER_SELECTOR_FRAME_BURST_START, 
                                    StTrg.STCAM_TRIGGER_SELECTOR_EXPOSURE_START, 
                                    StTrg.STCAM_TRIGGER_SELECTOR_EXPOSURE_END,
                                    StTrg.STCAM_TRIGGER_SELECTOR_SENSOR_READ_OUT_START
                                };
                            string[] aText = new string[]
                                {
                                    "FrameStart", 
                                    "FrameBurstStart", 
                                    "ExposureStart", 
                                    "ExposureEnd", 
                                    "SensorReadOutStart"
                                };
                            bool[] isSupported = new bool[aSelector.GetLength(0)];

                            int count = 0;
                            for (int j = 0; j < aSelector.GetLength(0); j++)
                            {
                                isSupported[j] = m_objStCamera.IsTriggerSelectorSupported(aSelector[j]);
                                if (isSupported[j])
                                {
                                    count++;
                                }
                            }
                            if (0 < count)
                            {
                                list = new StComboBoxItem[count];
                                int index = 0;
                                for (int j = 0; j < aSelector.GetLength(0); j++)
                                {
                                    if (isSupported[j])
                                    {
                                        list[index] = new StComboBoxItem(aText[j], aSelector[j]);
                                        index++;
                                    }
                                }

                                Value = m_objStCamera.TriggerSelector;
                            }
                        }
                    }

					else if(comboBox.Equals(cmbNoiseReduction))
					{
						if(m_objStCamera.HasTriggerFunction())
                        {
							list = new StComboBoxItem[]{
														   new StComboBoxItem("OFF", StTrg.STCAM_NR_OFF),
														   new StComboBoxItem("Easy", StTrg.STCAM_NR_EASY),
														   new StComboBoxItem("Calibration", StTrg.STCAM_NR_DARK_CL),
														   new StComboBoxItem("Complex", StTrg.STCAM_NR_COMPREX)
													   };
							Value = m_objStCamera.NoiseRedutionMode;
						}
					}

	

					else if(comboBox.Equals(cmbExposureEnd))
					{
						if(m_objStCamera.HasTriggerFunction())
						{
							list = new StComboBoxItem[]{
														   new StComboBoxItem("Disable", StTrg.STCAM_TRIGGER_MODE_EXPEND_DISABLE),
														   new StComboBoxItem("Enable", StTrg.STCAM_TRIGGER_MODE_EXPEND_ENABLE)
													   };
							Value = m_objStCamera.ExposureEnd;
						}
					}
					else if(comboBox.Equals(cmbCameraMemory))
					{
						if(m_objStCamera.HasCameraMemoryFunction())
						{
							list = new StComboBoxItem[]{
														   new StComboBoxItem("Type B", StTrg.STCAM_TRIGGER_MODE_CAMERA_MEMORY_TYPE_B),
														   new StComboBoxItem("Type A", StTrg.STCAM_TRIGGER_MODE_CAMERA_MEMORY_TYPE_A),
														   new StComboBoxItem("OFF", StTrg.STCAM_TRIGGER_MODE_CAMERA_MEMORY_OFF)
													   };
							Value = m_objStCamera.CameraMemory;
						}
					}
					else if (comboBox.Equals(cmbTriggerOverlap))
					{
                        if (m_objStCamera.HasTriggerFunction())
                        {
                            if (m_objStCamera.HasTriggerOverlapOffPreviousFunction())
		    				{

                                list = new StComboBoxItem[]{
                                           new StComboBoxItem("OFF", StTrg.STCAM_TRIGGER_OVERLAP_OFF),
                                           new StComboBoxItem("Previous Frame", StTrg.STCAM_TRIGGER_OVERLAP_PREVIOUS_FRAME)
													   };
                                Value = m_objStCamera.TriggerOverlap;
							}
						}
					}
					else if (comboBox.Equals(cmbSensorShutterMode))
					{
                        UInt32 dwSupportedSensorShuttertType = m_objStCamera.GetSupportedSensorShutterMode();
                        if (0 < dwSupportedSensorShuttertType)
						{
							int count = 1;
                            if (0 < (dwSupportedSensorShuttertType & 1)) count++;
                            if (0 < (dwSupportedSensorShuttertType & 2)) count++;
							list = new StComboBoxItem[count];

							int index = 0;
                            list[index++] = new StComboBoxItem("Rolling", StTrg.STCAM_SENSOR_SHUTTER_MODE_ROLLING);
                            if (0 < (dwSupportedSensorShuttertType & 1))
							{
                                list[index++] = new StComboBoxItem("Global Reset", StTrg.STCAM_SENSOR_SHUTTER_MODE_GLOBAL_RESET);
							}
                            if (0 < (dwSupportedSensorShuttertType & 2))
							{
                                list[index++] = new StComboBoxItem("Global", StTrg.STCAM_SENSOR_SHUTTER_MODE_GLOBAL);
							}
                            Value = m_objStCamera.SensorShutterMode;
						}
					}
					else if(comboBox.Equals(cmbScanMode))
					{
						ushort wEnableScanMode = m_objStCamera.EnableScanMode;
						int nScanModeCount = 1;
						ushort wMask = 1;
						do
						{
							if ((wEnableScanMode & wMask) != 0)
							{
								nScanModeCount++;
							}
							wMask <<= 1;
						} while (wMask != 0);

						list = new StComboBoxItem[nScanModeCount];
						list[0] = new StComboBoxItem("Normal", StTrg.STCAM_SCAN_MODE_NORMAL);
						int nIndex = 1;
						wMask = 1;
						do
						{
							if ((wEnableScanMode & wMask) != 0)
							{
								switch (wMask)
								{
									case (StTrg.STCAM_SCAN_MODE_PARTIAL_1):
										list[nIndex] = new StComboBoxItem("Partial_1", wMask);
										break;
									case (StTrg.STCAM_SCAN_MODE_PARTIAL_2):
										list[nIndex] = new StComboBoxItem("Partial_2", wMask);
										break;
									case (StTrg.STCAM_SCAN_MODE_PARTIAL_4):
										list[nIndex] = new StComboBoxItem("Partial_4", wMask);
										break;
									case (StTrg.STCAM_SCAN_MODE_BINNING):
										list[nIndex] = new StComboBoxItem("Binning", wMask);
										break;
									case (StTrg.STCAM_SCAN_MODE_BINNING_PARTIAL_1):
										list[nIndex] = new StComboBoxItem("Binning Partial_1", wMask);
										break;
									case (StTrg.STCAM_SCAN_MODE_BINNING_PARTIAL_2):
										list[nIndex] = new StComboBoxItem("Binning Partial_2", wMask);
										break;
									case (StTrg.STCAM_SCAN_MODE_BINNING_PARTIAL_4):
										list[nIndex] = new StComboBoxItem("Binning Partial_4", wMask);
										break;
									case (StTrg.STCAM_SCAN_MODE_ROI):
										list[nIndex] = new StComboBoxItem("ROI", wMask);
										break;
									case (StTrg.STCAM_SCAN_MODE_VARIABLE_PARTIAL):
										list[nIndex] = new StComboBoxItem("Variable Partial", wMask);
										break;
									case (StTrg.STCAM_SCAN_MODE_BINNING_VARIABLE_PARTIAL):
										list[nIndex] = new StComboBoxItem("Binning Variable Partial", wMask);
										break;
								}

								nIndex++;
							}
							wMask <<= 1;
						} while (wMask != 0);
						Value = m_objStCamera.ScanMode;
					}

					else if(comboBox.Equals(cmbClockMode))
					{
						uint dwEnableClockMode = m_objStCamera.EnableClockMode;
						int nCount = 1;
						uint dwMask = 1;
						do
						{
							if ((dwEnableClockMode & dwMask) != 0)
							{
								nCount++;
							}
							dwMask <<= 1;
						} while (dwMask != 0);

						list = new StComboBoxItem[nCount];
						list[0] = new StComboBoxItem("Normal", StTrg.STCAM_CLOCK_MODE_NORMAL);
						int nIndex = 1;
						dwMask = 1;
						do
						{
							if ((dwEnableClockMode & dwMask) != 0)
							{
								switch (dwMask)
								{
									case (StTrg.STCAM_CLOCK_MODE_DIV_2):
										list[nIndex] = new StComboBoxItem("1/2", dwMask);
										break;
									case (StTrg.STCAM_CLOCK_MODE_DIV_4):
										list[nIndex] = new StComboBoxItem("1/4", dwMask);
										break;
									case (StTrg.STCAM_CLOCK_MODE_VGA_90FPS):
										list[nIndex] = new StComboBoxItem("90FPS", dwMask);
										break;
								}

								nIndex++;
							}
							dwMask <<= 1;
						} while (dwMask != 0);
						Value = m_objStCamera.ClockMode;
						forceDisabled = nCount < 2;
					}
					else if(comboBox.Equals(cmbSharpnessMode))
					{
						list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", StTrg.STCAM_SHARPNESS_OFF),
													   new StComboBoxItem("ON", StTrg.STCAM_SHARPNESS_ON)
												   };
						Value = m_objStCamera.SharpnessMode;
                    }
                    else if (comboBox.Equals(cmbShadingCorrectionMode))
                    {
                        list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", StTrg.STCAM_SHADING_CORRECTION_MODE_OFF),
													   new StComboBoxItem("Calibration(Multiplication)", StTrg.STCAM_SHADING_CORRECTION_MODE_CALIBRATION_MULTIPLICATION),
													   new StComboBoxItem("ON(Multiplication)", StTrg.STCAM_SHADING_CORRECTION_MODE_ON_MULTIPLICATION),
													   new StComboBoxItem("Calibration(Addition)", StTrg.STCAM_SHADING_CORRECTION_MODE_CALIBRATION_ADDITION),
													   new StComboBoxItem("ON(Addition)", StTrg.STCAM_SHADING_CORRECTION_MODE_ON_ADDITION)
												   };
                        Value = m_objStCamera.ShadingCorrectionMode;
                    }
                    else if (comboBox.Equals(cmbYGammaMode))
                    {
                        list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", StTrg.STCAM_GAMMA_OFF),
													   new StComboBoxItem("ON", StTrg.STCAM_GAMMA_ON),
													   new StComboBoxItem("REVERSE", StTrg.STCAM_GAMMA_REVERSE)
												   };
                        Value = m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_Y];
                    }
                    else if (comboBox.Equals(cmbRGammaMode))
                    {
                        list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", StTrg.STCAM_GAMMA_OFF),
													   new StComboBoxItem("ON", StTrg.STCAM_GAMMA_ON),
													   new StComboBoxItem("REVERSE", StTrg.STCAM_GAMMA_REVERSE)
												   };
                        Value = m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_R];
                    }
                    else if (comboBox.Equals(cmbGRGammaMode))
                    {
                        list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", StTrg.STCAM_GAMMA_OFF),
													   new StComboBoxItem("ON", StTrg.STCAM_GAMMA_ON),
													   new StComboBoxItem("REVERSE", StTrg.STCAM_GAMMA_REVERSE)
												   };
                        Value = m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_GR];
                    }
                    else if (comboBox.Equals(cmbGBGammaMode))
                    {
                        list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", StTrg.STCAM_GAMMA_OFF),
													   new StComboBoxItem("ON", StTrg.STCAM_GAMMA_ON),
													   new StComboBoxItem("REVERSE", StTrg.STCAM_GAMMA_REVERSE)
												   };
                        Value = m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_GB];
                    }
                    else if (comboBox.Equals(cmbBGammaMode))
                    {
                        list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", StTrg.STCAM_GAMMA_OFF),
													   new StComboBoxItem("ON", StTrg.STCAM_GAMMA_ON),
													   new StComboBoxItem("REVERSE", StTrg.STCAM_GAMMA_REVERSE)
												   };
                        Value = m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_B];
                    }
                    else if (comboBox.Equals(cmbColorInterpolation))
                    {
                        if (m_objStCamera.ColorArray != StTrg.STCAM_COLOR_ARRAY_MONO)
                        {
                            list = new StComboBoxItem[]{
														   new StComboBoxItem("None(MONO)", StTrg.STCAM_COLOR_INTERPOLATION_NONE_MONO),
														   new StComboBoxItem("None(Color)", StTrg.STCAM_COLOR_INTERPOLATION_NONE_COLOR),
														   new StComboBoxItem("Nearest Neighbor", StTrg.STCAM_COLOR_INTERPOLATION_NEAREST_NEIGHBOR),
														   new StComboBoxItem("Bilinear", StTrg.STCAM_COLOR_INTERPOLATION_BILINEAR),
														   new StComboBoxItem("Bilinear(False color reduction)", StTrg.STCAM_COLOR_INTERPOLATION_BILINEAR_FALSE_COLOR_REDUCTION),
														   new StComboBoxItem("Bicubic", StTrg.STCAM_COLOR_INTERPOLATION_BICUBIC)
													   };

                            Value = m_objStCamera.ColorInterpolationMode;
                        }
                    }

					else if (comboBox.Equals(cmbMirror))
					{
						bool hasMirrorH = m_objStCamera.HasMirrorHorizontal();
						bool hasMirrorV = m_objStCamera.HasMirrorVertical();
						if (hasMirrorH && hasMirrorV)
						{
							list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", StTrg.STCAM_MIRROR_OFF),
													   new StComboBoxItem("Horizontal", StTrg.STCAM_MIRROR_HORIZONTAL),
													   new StComboBoxItem("Vertical", StTrg.STCAM_MIRROR_VERTICAL),
													   new StComboBoxItem("Horizontal/Vertical", StTrg.STCAM_MIRROR_HORIZONTAL_VERTICAL),
														new StComboBoxItem("Horizontal[Camera]", StTrg.STCAM_MIRROR_HORIZONTAL_CAMERA),
														new StComboBoxItem("Vertical[Camera]", StTrg.STCAM_MIRROR_VERTICAL_CAMERA),
														new StComboBoxItem("Horizontal/Vertical[Camera]", StTrg.STCAM_MIRROR_HORIZONTAL_CAMERA | StTrg.STCAM_MIRROR_VERTICAL_CAMERA),
												   };
						}
						else if (hasMirrorH)
						{
							list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", StTrg.STCAM_MIRROR_OFF),
													   new StComboBoxItem("Horizontal", StTrg.STCAM_MIRROR_HORIZONTAL),
													   new StComboBoxItem("Vertical", StTrg.STCAM_MIRROR_VERTICAL),
													   new StComboBoxItem("Horizontal/Vertical", StTrg.STCAM_MIRROR_HORIZONTAL_VERTICAL),
														new StComboBoxItem("Horizontal[Camera]", StTrg.STCAM_MIRROR_HORIZONTAL_CAMERA),
												   };
						}
						else
						{
							list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", StTrg.STCAM_MIRROR_OFF),
													   new StComboBoxItem("Horizontal", StTrg.STCAM_MIRROR_HORIZONTAL),
													   new StComboBoxItem("Vertical", StTrg.STCAM_MIRROR_VERTICAL),
													   new StComboBoxItem("Horizontal/Vertical", StTrg.STCAM_MIRROR_HORIZONTAL_VERTICAL)
												   };
						}
						Value = m_objStCamera.MirrorMode;
					}
                    else if (comboBox.Equals(cmbRotation))
                    {
                        list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", StTrg.STCAM_ROTATION_OFF),
													   new StComboBoxItem("CLOCKWISE_90", StTrg.STCAM_ROTATION_CLOCKWISE_90),
													   new StComboBoxItem("COUNTERCLOCKWISE_90", StTrg.STCAM_ROTATION_COUNTERCLOCKWISE_90)
												   };

                        Value = m_objStCamera.RotationMode;
                    }
                    else if (
                        comboBox.Equals(cmbIOInOut0) ||
                        comboBox.Equals(cmbIOInOut1) ||
                        comboBox.Equals(cmbIOInOut2) ||
                        comboBox.Equals(cmbIOInOut3)
                        )
                    {
                        byte ioNo = 0;
                        if (comboBox.Equals(cmbIOInOut1)) ioNo = 1;
                        else if (comboBox.Equals(cmbIOInOut2)) ioNo = 2;
                        else if (comboBox.Equals(cmbIOInOut3)) ioNo = 3;
                        list = new StComboBoxItem[]{
													   new StComboBoxItem("Input", 0),
													   new StComboBoxItem("Output", 1)
												   };
                        Value = m_objStCamera.IOPinInOut[ioNo];
						forceDisabled = !m_objStCamera.HasChangeIOFunction();
						forceDisabled |= !m_objStCamera.IOPinExistence[ioNo];
                    }
                    else if (
                        comboBox.Equals(cmbIOMode0) ||
                        comboBox.Equals(cmbIOMode1) ||
                        comboBox.Equals(cmbIOMode2) ||
                        comboBox.Equals(cmbIOMode3)
                        )
                    {
                        byte ioNo = 0;
                        if (comboBox.Equals(cmbIOMode1)) ioNo = 1;
                        else if (comboBox.Equals(cmbIOMode2)) ioNo = 2;
                        else if (comboBox.Equals(cmbIOMode3)) ioNo = 3;

                        if (1 == m_objStCamera.IOPinInOut[ioNo])
						{
							//Output
							int listCount = 7;
							if (m_objStCamera.HasTriggerThroughFunction())
							{
								listCount++;
							}
							if (m_objStCamera.HasTriggerValidOutput())
							{
								listCount++;
                            }
                            if (m_objStCamera.HasTransferEndOutput())
                            {
                                listCount++;
                            }
							list = new StComboBoxItem[listCount];

							int listPos = 0;
							list[listPos++] = new StComboBoxItem("Disable", StTrg.STCAM_OUT_PIN_MODE_DISABLE);
							list[listPos++] = new StComboBoxItem("General Output", StTrg.STCAM_OUT_PIN_MODE_GENERAL_OUTPUT);
							list[listPos++] = new StComboBoxItem("Trigger Output Programmable", StTrg.STCAM_OUT_PIN_MODE_TRIGGER_OUTPUT_PROGRAMMABLE);
							if (m_objStCamera.HasTriggerThroughFunction())
							{
								list[listPos++] = new StComboBoxItem("Trigger Output Loop Through", StTrg.STCAM_OUT_PIN_MODE_TRIGGER_OUTPUT_LOOP_THROUGH);
							}
							list[listPos++] = new StComboBoxItem("Exposure End", StTrg.STCAM_OUT_PIN_MODE_EXPOSURE_END);
							list[listPos++] = new StComboBoxItem("CCD Read End Output", StTrg.STCAM_OUT_PIN_MODE_CCD_READ_END_OUTPUT);
							list[listPos++] = new StComboBoxItem("Strobe Output Programmable", StTrg.STCAM_OUT_PIN_MODE_STROBE_OUTPUT_PROGRAMMABLE);
							list[listPos++] = new StComboBoxItem("Strobe Output Exposure", StTrg.STCAM_OUT_PIN_MODE_STROBE_OUTPUT_EXPOSURE);
							if (m_objStCamera.HasTriggerValidOutput())
							{
								list[listPos++] = new StComboBoxItem("Trigger Valid Out", StTrg.STCAM_OUT_PIN_MODE_TRIGGER_VALID_OUT);
                            }
                            if (m_objStCamera.HasTransferEndOutput())
                            {
                                list[listPos++] = new StComboBoxItem("Transfer End Out", StTrg.STCAM_OUT_PIN_MODE_TRANSFER_END);
                            }

                        }
                        else
                        {
                            //Input
                            if (m_objStCamera.HasGenICamIO())
                            {

                            }
                            else
                            {
                                int listCount = 3;
                                bool hasReadOut = m_objStCamera.HasReadOut();
                                bool hasStartStopExpHardwareTrigger = m_objStCamera.HasExposureEndHardwareTrigger();
                                if (hasReadOut)
                                {
                                    listCount++;
                                }
                                if (hasStartStopExpHardwareTrigger)
                                {
                                    listCount++;
                                }
                                list = new StComboBoxItem[listCount];

                                int listPos = 0;
                                list[listPos++] = new StComboBoxItem("Disable", StTrg.STCAM_IN_PIN_MODE_DISABLE);
                                list[listPos++] = new StComboBoxItem("General Input", StTrg.STCAM_IN_PIN_MODE_GENERAL_INPUT);
                                list[listPos++] = new StComboBoxItem("Frame/ExposureStart Input", StTrg.STCAM_IN_PIN_MODE_TRIGGER_INPUT);
                                if (hasReadOut)
                                {
                                    list[listPos++] = new StComboBoxItem("Readout Input", StTrg.STCAM_IN_PIN_MODE_READOUT_INPUT);
                                }
                                if (hasStartStopExpHardwareTrigger)
                                {
                                    list[listPos++] = new StComboBoxItem("ExposureEnd Trigger Input", StTrg.STCAM_IN_PIN_MODE_SUB_TRIGGER_INPUT);
                                }
                            }
                        }
						Value = m_objStCamera.IOPinMode[ioNo];
						forceDisabled = !m_objStCamera.IOPinExistence[ioNo];
                    }

                    else if (
                        comboBox.Equals(cmbIOPolarity0) ||
                        comboBox.Equals(cmbIOPolarity1) ||
                        comboBox.Equals(cmbIOPolarity2) ||
                        comboBox.Equals(cmbIOPolarity3)
                        )
                    {

                        byte ioNo = 0;
                        if (comboBox.Equals(cmbIOPolarity1)) ioNo = 1;
                        else if (comboBox.Equals(cmbIOPolarity2)) ioNo = 2;
                        else if (comboBox.Equals(cmbIOPolarity3)) ioNo = 3;
                        list = new StComboBoxItem[]{
                                                   new StComboBoxItem("Positive", 0),
                                                   new StComboBoxItem("Negative", 1)
                                               };
                        Value = m_objStCamera.IOPinPolarity[ioNo];

                        uint ioMode = m_objStCamera.IOPinMode[ioNo];
                        if (m_objStCamera.HasGenICamIO())
                        {

                        }
                        else
                        {
                            if (
                                (StTrg.STCAM_IN_PIN_MODE_DISABLE == ioMode) ||
                                (StTrg.STCAM_IN_PIN_MODE_GENERAL_INPUT == ioMode)
                                )
                            {
                                forceDisabled = true;
                            }
                        }
                        forceDisabled |= !m_objStCamera.IOPinExistence[ioNo];
                    }
                    else if (
                        comboBox.Equals(cmbIOStatus0) ||
                        comboBox.Equals(cmbIOStatus1) ||
                        comboBox.Equals(cmbIOStatus2) ||
                        comboBox.Equals(cmbIOStatus3)
                        )
                    {
                        byte ioNo = 0;
                        if (comboBox.Equals(cmbIOStatus1)) ioNo = 1;
                        else if (comboBox.Equals(cmbIOStatus2)) ioNo = 2;
                        else if (comboBox.Equals(cmbIOStatus3)) ioNo = 3;
                        list = new StComboBoxItem[]{
													   new StComboBoxItem("Low", 0),
													   new StComboBoxItem("High", 1)
												   };
                        Value = m_objStCamera.IOPinStatus[ioNo];
                        if (
                            (0 == m_objStCamera.IOPinInOut[ioNo]) ||
                            (StTrg.STCAM_OUT_PIN_MODE_GENERAL_OUTPUT != m_objStCamera.IOPinMode[ioNo])
                            )
                        {
                            forceDisabled = true;
						}
						forceDisabled |= !m_objStCamera.IOPinExistence[ioNo];
                    }
                    else if (comboBox.Equals(cmbResetSwitch))
                    {
                        list = new StComboBoxItem[]{
													   new StComboBoxItem("Disabled", 0),
													   new StComboBoxItem("Enabled", 1),
												   };
                        Value = (uint)m_objStCamera.ResetSwitchEnabled;
                        if (!m_objStCamera.HasResetSwitchDisabledFunction())
                        {
                            forceDisabled = true;
                        }
                    }
                    else if (comboBox.Equals(cmbLEDGreen))
                    {
                        list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", 0),
													   new StComboBoxItem("ON", StTrg.STCAM_LED_GREEN_ON)
												   };
                        Value = m_objStCamera.LEDGreen;
                        if (!m_objStCamera.HasLEDFunction(0))
                        {
                            forceDisabled = true;
                        }
                    }
                    else if (comboBox.Equals(cmbLEDRed))
                    {
                        list = new StComboBoxItem[]{
													   new StComboBoxItem("OFF", 0),
													   new StComboBoxItem("ON", StTrg.STCAM_LED_RED_ON)
												   };
                        Value = m_objStCamera.LEDRed;
                        if (!m_objStCamera.HasLEDFunction(1))
                        {
                            forceDisabled = true;
                        }
                    }
					else if (comboBox.Equals(stComboBoxHBinningSkipping))
					{
						if (m_objStCamera.ScanMode != StTrg.STCAM_SCAN_MODE_ROI)
						{
							forceDisabled = true;
						}
						switch (m_objStCamera.USBPID)
						{
							case (StTrg.STCAM_USBPID_STC_MBA5MUSB3):
							case (StTrg.STCAM_USBPID_STC_MCA5MUSB3):
								list = new StComboBoxItem[]{
									new StComboBoxItem("1/1", 0x0000),
									new StComboBoxItem("1/2", 0x0001),
									new StComboBoxItem("2/2", 0x0101),
									new StComboBoxItem("1/3", 0x0002),
									new StComboBoxItem("1/4", 0x0003),
									new StComboBoxItem("2/4", 0x0103),
									new StComboBoxItem("4/4", 0x0203),
									new StComboBoxItem("1/5", 0x0004),
									new StComboBoxItem("1/6", 0x0005),
									new StComboBoxItem("2/6", 0x0105),
									new StComboBoxItem("1/7", 0x0006),
								};
								break;
							case (StTrg.STCAM_USBPID_STC_MBE132U3V):
							case (StTrg.STCAM_USBPID_STC_MCE132U3V):
								list = new StComboBoxItem[]{
									new StComboBoxItem("1/1", 0x0101),
									new StComboBoxItem("1/2", 0x0102),
									new StComboBoxItem("1/4", 0x0104),
									new StComboBoxItem("2/1", 0x0201),
									new StComboBoxItem("2/2", 0x0202),
									new StComboBoxItem("2/4", 0x0204),
								};
                                break;
                            case (StTrg.STCAM_USBPID_STC_MBCM401U3V):
                            case (StTrg.STCAM_USBPID_STC_MBCM200U3V):
                                list = new StComboBoxItem[]{
                                    new StComboBoxItem("1/1", 0x0101),
                                    new StComboBoxItem("1/2", 0x0102),
                                    new StComboBoxItem("1/4", 0x0104),
                                    new StComboBoxItem("2/1", 0x0201),
                                    new StComboBoxItem("4/1", 0x0401),
                                };
                                break;
                            case (StTrg.STCAM_USBPID_STC_MCCM401U3V):
                            case (StTrg.STCAM_USBPID_STC_MCCM200U3V):
                                list = new StComboBoxItem[]{
                                    new StComboBoxItem("1/1", 0x0101)
                                };
                                break;
                            case (StTrg.STCAM_USBPID_STC_MBS241U3V):
                            case (StTrg.STCAM_USBPID_STC_MBS510U3V):
                            case (StTrg.STCAM_USBPID_STC_MBS322U3V):
                                list = new StComboBoxItem[]{
											new StComboBoxItem("1/1", 0x0101),
											new StComboBoxItem("1/2", 0x0102),
											new StComboBoxItem("2/1", 0x0201),
										};
                                break;
                            case (StTrg.STCAM_USBPID_STC_MCS241U3V):
                            case (StTrg.STCAM_USBPID_STC_MCS510U3V):
                            case (StTrg.STCAM_USBPID_STC_MCS322U3V):
                                list = new StComboBoxItem[]{
											new StComboBoxItem("1/1", 0x0101),
											new StComboBoxItem("1/2", 0x0102),
										};
                                break;
						}
						Value = m_objStCamera.HBinningSkipping;
					}
					else if (comboBox.Equals(stComboBoxVBinningSkipping))
					{
						if (m_objStCamera.ScanMode != StTrg.STCAM_SCAN_MODE_ROI)
						{
							forceDisabled = true;
						}
						switch (m_objStCamera.USBPID)
						{
							case (StTrg.STCAM_USBPID_STC_MBA5MUSB3):
							case (StTrg.STCAM_USBPID_STC_MCA5MUSB3):
								list = new StComboBoxItem[]{
											new StComboBoxItem("1/1", 0x0000),
											new StComboBoxItem("1/2", 0x0001),
											new StComboBoxItem("2/2", 0x0101),
											new StComboBoxItem("1/3", 0x0002),
											new StComboBoxItem("1/4", 0x0003),
											new StComboBoxItem("2/4", 0x0103),
											new StComboBoxItem("4/4", 0x0203),
											new StComboBoxItem("1/5", 0x0004),
											new StComboBoxItem("1/6", 0x0005),
											new StComboBoxItem("2/6", 0x0105),
											new StComboBoxItem("1/7", 0x0006),
											new StComboBoxItem("1/8", 0x0007),
											new StComboBoxItem("2/8", 0x0107),
											new StComboBoxItem("4/8", 0x0207),
										};
								break;
							case (StTrg.STCAM_USBPID_STC_MBE132U3V):
							case (StTrg.STCAM_USBPID_STC_MCE132U3V):
								list = new StComboBoxItem[]{
											new StComboBoxItem("1/1", 0x0101),
											new StComboBoxItem("1/2", 0x0102),
											new StComboBoxItem("1/4", 0x0104),
											new StComboBoxItem("2/1", 0x0201),
											new StComboBoxItem("2/2", 0x0202),
											new StComboBoxItem("2/4", 0x0204),
										};
                                break;
                            case (StTrg.STCAM_USBPID_STC_MBCM200U3V):
                            case (StTrg.STCAM_USBPID_STC_MBCM401U3V):
                            list = new StComboBoxItem[]{
                                            new StComboBoxItem("1/1", 0x0101),
                                            new StComboBoxItem("1/2", 0x0102),
                                            new StComboBoxItem("1/4", 0x0104),
                                            new StComboBoxItem("2/1", 0x0201),
                                            new StComboBoxItem("4/1", 0x0401),
                                        };
                            break;
                        case (StTrg.STCAM_USBPID_STC_MCCM200U3V):
                            case (StTrg.STCAM_USBPID_STC_MCCM401U3V):
                            list = new StComboBoxItem[]{
                                            new StComboBoxItem("1/1", 0x0101),
                                            new StComboBoxItem("1/2", 0x0102),
                                            new StComboBoxItem("1/4", 0x0104),
                                        };
                            break;
                        case (StTrg.STCAM_USBPID_STC_MBS241U3V):
                        case (StTrg.STCAM_USBPID_STC_MBS510U3V):
                        case (StTrg.STCAM_USBPID_STC_MBS322U3V):
                            list = new StComboBoxItem[]{
											new StComboBoxItem("1/1", 0x0101),
											new StComboBoxItem("1/2", 0x0102),
											new StComboBoxItem("2/1", 0x0201),
										};
                            break;
                        case (StTrg.STCAM_USBPID_STC_MCS241U3V):
                        case (StTrg.STCAM_USBPID_STC_MCS510U3V):
                        case (StTrg.STCAM_USBPID_STC_MCS322U3V):
                            list = new StComboBoxItem[]{
											new StComboBoxItem("1/1", 0x0101),
											new StComboBoxItem("1/2", 0x0102),
										};
                            break;
						}
						Value = m_objStCamera.VBinningSkipping;
					}
					else if(comboBox.Equals(cmbTransferBitsPerPixel))
					{
						uint dwEnableFormat = m_objStCamera.EnableTransferBitsPerPixel;
						int nCount = 0;
						uint dwMask = 1;
						do
						{
							if ((dwEnableFormat & dwMask) != 0)
							{
								nCount++;
							}
							dwMask <<= 1;
						} while (dwMask != 0);

						list = new StComboBoxItem[nCount];
						int nIndex = 0;
						dwMask = 1;
						do
						{
							if ((dwEnableFormat & dwMask) != 0)
							{
								switch (dwMask)
								{
									case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_RAW_08):
										list[nIndex] = new StComboBoxItem("RAW_8bits", dwMask);
										break;
									case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_RAW_10):
										list[nIndex] = new StComboBoxItem("RAW_10bits", dwMask);
                                        break;
                                    case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_RAW_10P):
                                        list[nIndex] = new StComboBoxItem("RAW_10bits[Packed]", dwMask);
                                        break;
									case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_RAW_12):
										list[nIndex] = new StComboBoxItem("RAW_12bits", dwMask);
										break;
                                    case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_RAW_12P):
                                        list[nIndex] = new StComboBoxItem("RAW_12bits[Packed]", dwMask);
                                        break;
									case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_RAW_14):
										list[nIndex] = new StComboBoxItem("RAW_14bits", dwMask);
										break;
									case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_RAW_16):
										list[nIndex] = new StComboBoxItem("RAW_16bits", dwMask);
										break;
									case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_MONO_08):
										list[nIndex] = new StComboBoxItem("MONO_8bits", dwMask);
										break;
									case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_MONO_10):
										list[nIndex] = new StComboBoxItem("MONO_10bits", dwMask);
										break;
                                    case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_MONO_10P):
                                        list[nIndex] = new StComboBoxItem("MONO_10bits[Packed]", dwMask);
                                        break;
									case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_MONO_12):
										list[nIndex] = new StComboBoxItem("MONO_12bits", dwMask);
										break;
                                    case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_MONO_12P):
                                        list[nIndex] = new StComboBoxItem("MONO_12bits[Packed]", dwMask);
                                        break;
									case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_MONO_14):
										list[nIndex] = new StComboBoxItem("MONO_14bits", dwMask);
										break;
									case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_MONO_16):
										list[nIndex] = new StComboBoxItem("MONO_16bits", dwMask);
										break;
									case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_BGR_08):
										list[nIndex] = new StComboBoxItem("BGR_8bits", dwMask);
										break;
									case (StTrg.STCAM_TRANSFER_BITS_PER_PIXEL_BGR_10):
										list[nIndex] = new StComboBoxItem("BGR_10bits", dwMask);
										break;
								}
								nIndex++;
							}
							dwMask <<= 1;
						} while (dwMask != 0);
						if (nCount < 2)
						{
							forceDisabled = true;
						}

						Value = m_objStCamera.TransferBitsPerPixel;
                    }
                    else if (comboBox.Equals(cmbDisplayPixelFormat))
                    {
                        if (m_objStCamera.ColorArray != StTrg.STCAM_COLOR_ARRAY_MONO)
                        {
                            list = new StComboBoxItem[]{
							    new StComboBoxItem("BGR24", StTrg.STCAM_PIXEL_FORMAT_24_BGR),
							    new StComboBoxItem("BGR32", StTrg.STCAM_PIXEL_FORMAT_32_BGR)
						    };
                        }
                        else
                        {
                            list = new StComboBoxItem[]{
							    new StComboBoxItem("Mono8", StTrg.STCAM_PIXEL_FORMAT_08_MONO_OR_RAW),
							    new StComboBoxItem("BGR24", StTrg.STCAM_PIXEL_FORMAT_24_BGR),
							    new StComboBoxItem("BGR32", StTrg.STCAM_PIXEL_FORMAT_32_BGR)
						    };
                        }
                        Value = m_objStCamera.StPixelFormat;
                    }
					else if (comboBox.Equals(cmbDefectPixelCorrectionMode))
					{
						list = new StComboBoxItem[]{
							new StComboBoxItem("OFF", StTrg.STCAM_DEFECT_PIXEL_CORRECTION_OFF),
							new StComboBoxItem("ON", StTrg.STCAM_DEFECT_PIXEL_CORRECTION_ON)
						};
						Value = m_objStCamera.DefectPixelCorrectionMode;
					}
					else if (comboBox.Equals(cmbHBinningSumMode))
					{
						forceDisabled = (!m_objStCamera.HasHBinningSum()) || (stComboBoxHBinningSkipping.SelectedIndex == 0);
						if (!forceDisabled)
						{
							list = new StComboBoxItem[]{
									new StComboBoxItem("OFF", StTrg.STCAM_BINNING_SUM_MODE_OFF),
									new StComboBoxItem("ON", StTrg.STCAM_BINNING_SUM_MODE_H),
								};
							Value = (uint)(m_objStCamera.BinningSumMode & 0x00FF);
						}
					}
					else if (comboBox.Equals(cmbVBinningSumMode))
					{
						forceDisabled = (!m_objStCamera.HasVBinningSum()) || (stComboBoxVBinningSkipping.SelectedIndex == 0);
						if (!forceDisabled)
						{
							list = new StComboBoxItem[]{
									new StComboBoxItem("OFF", StTrg.STCAM_BINNING_SUM_MODE_OFF),
									new StComboBoxItem("ON", StTrg.STCAM_BINNING_SUM_MODE_V),
								};
							Value = (uint)(m_objStCamera.BinningSumMode & 0xFF00);
						}
                    }
                    else if (comboBox.Equals(cmbCurrentRegion))
                    {
                        UInt32 count = m_objStCamera.MaxROICount;
                        list = new StComboBoxItem[count];
                        for (UInt32 nRegion = 0; nRegion < count; nRegion++)
                        {
                            list[nRegion] = new StComboBoxItem("Region" + nRegion.ToString(), nRegion);
                        }
                        Value = m_objStCamera.CurrentRegion;
                        forceDisabled = (m_objStCamera.ScanMode != StTrg.STCAM_SCAN_MODE_ROI);
                    }
                    else if (comboBox.Equals(cmbRegionMode))
                    {
                        list = new StComboBoxItem[2];
                        list[0] = new StComboBoxItem("Disabled", 0);
                        list[1] = new StComboBoxItem("Enabled", 1);
                        Value = (uint)(m_objStCamera.RegionMode ? 1 : 0);
                        forceDisabled = (m_objStCamera.CurrentRegion == 0);
                    }
					else if (comboBox.Equals(cmbExposureWaitHD))
                    {
                        if (m_objStCamera.IsAnyTriggerModeON())
                        {
							if (m_objStCamera.HasExposureStartWaitHD())
							{
								list = new StComboBoxItem[]{
															   new StComboBoxItem("OFF", StTrg.STCAM_TRIGGER_MODE_EXPOSURE_WAIT_HD_OFF),
															   new StComboBoxItem("ON", StTrg.STCAM_TRIGGER_MODE_EXPOSURE_WAIT_HD_ON)
														   };
								Value = m_objStCamera.ExposureWaitHD;
							}
						}
						else
						{
							forceDisabled = true;
						}
					}
					else if (comboBox.Equals(cmbExposureWaitReadOut))
                    {
                        if (m_objStCamera.IsAnyTriggerModeON())
                        {
							if (m_objStCamera.HasExposureStartWaitReadOut())
							{
								list = new StComboBoxItem[]{
															   new StComboBoxItem("OFF", StTrg.STCAM_TRIGGER_MODE_EXPOSURE_WAIT_READOUT_OFF),
															   new StComboBoxItem("ON", StTrg.STCAM_TRIGGER_MODE_EXPOSURE_WAIT_READOUT_ON)
														   };
								Value = m_objStCamera.ExposureWaitReadOut;
							}
						}
						else
						{
							forceDisabled = true;
						}
					}

					if(null != list)
					{
						comboBox.Items.AddRange(list);
						comboBox.StValue = Value;
						comboBox.Enabled = true && (!forceDisabled);
					}
					else
					{
						comboBox.Enabled = false;
					}
				}
			}
			#endregion
			#region ReadOnlyTextBox
			if(null != m_ReadOnlyTextBoxList)
			{
				//
				for(int i = 0; i < m_ReadOnlyTextBoxList.Length; i++)
				{
					TextBox textBox = m_ReadOnlyTextBoxList[i];

					if(textBox.Equals(txtCameraType))
					{
						textBox.Text = m_objStCamera.CameraType;
					}
					else if(textBox.Equals(txtFPGAVersion))
					{
						textBox.Text = string.Format("{0:X4}", m_objStCamera.FPGAVersion);
					}
					else if(textBox.Equals(txtFirmVersion))
					{
						textBox.Text = string.Format("{0:X4}", m_objStCamera.FirmwareVersion);
                    }
                    else if (textBox.Equals(txtSDKVersioin))
                    {
                        textBox.Text = m_objStCamera.SDKVersion;
                    }
                    else if (textBox.Equals(txtTemperature))
                    {
                        bool hasTemperature = m_objStCamera.HasDeviceTemperatureMainBoard();
                        textBox.Visible = labelTemperature.Visible = hasTemperature;
                        if (hasTemperature)
                        {
                            textBox.Text = m_objStCamera.DeviceTemperatureMainBoard.ToString();
                        }
                    }
					else if(textBox.Equals(txtSWStatus0) || textBox.Equals(txtSWStatus1) || textBox.Equals(txtSWStatus2) || textBox.Equals(txtSWStatus3))
					{
						byte swNo = 0;
						if(textBox.Equals(txtSWStatus1))	swNo = 1;
						else if(textBox.Equals(txtSWStatus2))	swNo = 2;
						else if(textBox.Equals(txtSWStatus3))	swNo = 3;
						if(m_objStCamera.SwStatus[swNo] == 1)
						{
							textBox.Text = "ON";
						}
						else
						{
							textBox.Text = "OFF";
						}
					}
				}
			}
			#endregion
			#region Button
			if(null != m_ButtonList)
			{
				foreach(Button button in m_ButtonList)
				{
                    bool enabled = false;
                    if (
                        button.Equals(btnFrameStartTrigger) ||
                        button.Equals(btnExposureStartTrigger) ||
                        button.Equals(btnExposureEndTrigger) ||
                        button.Equals(btnSensorReadOutStartTrigger)
                        )
                    {
                        enabled = m_objStCamera.IsEnableTriggerSoftware(uint.Parse(button.Tag.ToString()));
                    }
					else if(button.Equals(btnResetFrameNo))
					{
						enabled = true;
					}
					button.Enabled = enabled;
				}
			}
			#endregion

			#region CheckBox
			if(null != m_CheckBoxList)
			{
				foreach(CheckBox checkbox in m_CheckBoxList)
				{
					bool enabled = false;
					if(checkbox.Equals(chkAutoTrigger))
					{
						if(m_objStCamera.HasTriggerFunction())
                        {
                            if (
                                m_objStCamera.IsEnableTriggerSoftware(StTrg.STCAM_TRIGGER_SELECTOR_EXPOSURE_START) &&
                                (m_objStCamera.ExposureMode == StTrg.STCAM_EXPOSURE_MODE_TRIGGER_CONTROLLED)
                                )
							{
								enabled = true;
								checkbox.Checked = m_objStCamera.AutoTrigger;
							}
						}
						checkbox.Enabled = enabled;
		
					}
				}
			}
			#endregion
			

		}


		


		#region InitializeComponent
		/// <summary>
		/// </summary>
		private void InitializeComponent()
		{
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageShutterGain = new System.Windows.Forms.TabPage();
			this.cmbAdjustmentDigitalGain = new StCtlLib.StComboBox();
			this.labelAdjustmentDigitalGain = new System.Windows.Forms.Label();
			this.txtAGCMaxGain = new StCtlLib.StTextBox();
			this.lblAGCMaxGain = new System.Windows.Forms.Label();
			this.trackBarAGCMaxGain = new System.Windows.Forms.TrackBar();
			this.label25 = new System.Windows.Forms.Label();
			this.txtAGCMinGain = new StCtlLib.StTextBox();
			this.lblAGCMinGain = new System.Windows.Forms.Label();
			this.trackBarAGCMinGain = new System.Windows.Forms.TrackBar();
			this.label27 = new System.Windows.Forms.Label();
			this.txtAEMaxExposure = new StCtlLib.StTextBox();
			this.lblAEMaxExposure = new System.Windows.Forms.Label();
			this.trackBarAEMaxExposure = new System.Windows.Forms.TrackBar();
			this.label24 = new System.Windows.Forms.Label();
			this.txtAEMinExposure = new StCtlLib.StTextBox();
			this.lblAEMinExposure = new System.Windows.Forms.Label();
			this.trackBarAEMinExposure = new System.Windows.Forms.TrackBar();
			this.label23 = new System.Windows.Forms.Label();
			this.txtALCTarget = new StCtlLib.StTextBox();
			this.trackBarALCTarget = new System.Windows.Forms.TrackBar();
			this.label21 = new System.Windows.Forms.Label();
			this.cmbALCMode = new StCtlLib.StComboBox();
			this.label20 = new System.Windows.Forms.Label();
			this.txtDigitalGain = new StCtlLib.StTextBox();
			this.txtGain = new StCtlLib.StTextBox();
			this.txtExposure = new StCtlLib.StTextBox();
			this.lblDigitalGainValue = new System.Windows.Forms.Label();
			this.trackBarDigitalGain = new System.Windows.Forms.TrackBar();
			this.lblDigitalGain = new System.Windows.Forms.Label();
			this.lblGainValue = new System.Windows.Forms.Label();
			this.trackBarGain = new System.Windows.Forms.TrackBar();
			this.lblGain = new System.Windows.Forms.Label();
			this.lblExposureValue = new System.Windows.Forms.Label();
			this.trackBarExposure = new System.Windows.Forms.TrackBar();
			this.lblExposure = new System.Windows.Forms.Label();
			this.tabPageWB = new System.Windows.Forms.TabPage();
			this.txtLowChromaSuppresionSuppressionLevel = new StCtlLib.StTextBox();
			this.trackBarLowChromaSuppresionSuppressionLevel = new System.Windows.Forms.TrackBar();
			this.label45 = new System.Windows.Forms.Label();
			this.txtLowChromaSuppresionStartLevel = new StCtlLib.StTextBox();
			this.trackBarLowChromaSuppresionStartLevel = new System.Windows.Forms.TrackBar();
			this.label44 = new System.Windows.Forms.Label();
			this.txtChromaSuppresionSuppressionLevel = new StCtlLib.StTextBox();
			this.txtChromaSuppresionStartLevel = new StCtlLib.StTextBox();
			this.trackBarChromaSuppresionSuppressionLevel = new System.Windows.Forms.TrackBar();
			this.label32 = new System.Windows.Forms.Label();
			this.trackBarChromaSuppresionStartLevel = new System.Windows.Forms.TrackBar();
			this.label33 = new System.Windows.Forms.Label();
			this.cmbHueSaturationMode = new StCtlLib.StComboBox();
			this.cmbWBMode = new StCtlLib.StComboBox();
			this.txtSaturation = new StCtlLib.StTextBox();
			this.txtHue = new StCtlLib.StTextBox();
			this.txtWBBGain = new StCtlLib.StTextBox();
			this.txtWBGbGain = new StCtlLib.StTextBox();
			this.txtWBGrGain = new StCtlLib.StTextBox();
			this.txtWBRGain = new StCtlLib.StTextBox();
			this.lblSaturationValue = new System.Windows.Forms.Label();
			this.trackBarSaturation = new System.Windows.Forms.TrackBar();
			this.label2 = new System.Windows.Forms.Label();
			this.lblHueValue = new System.Windows.Forms.Label();
			this.trackBarHue = new System.Windows.Forms.TrackBar();
			this.lblHue = new System.Windows.Forms.Label();
			this.lblHueSaturationMode = new System.Windows.Forms.Label();
			this.lblWBBGainValue = new System.Windows.Forms.Label();
			this.trackBarWBBGain = new System.Windows.Forms.TrackBar();
			this.lblWBBGain = new System.Windows.Forms.Label();
			this.lblWBGbGainValue = new System.Windows.Forms.Label();
			this.trackBarWBGbGain = new System.Windows.Forms.TrackBar();
			this.lblWBGbGain = new System.Windows.Forms.Label();
			this.lblWBGrGainValue = new System.Windows.Forms.Label();
			this.trackBarWBGrGain = new System.Windows.Forms.TrackBar();
			this.lblWBGrGain = new System.Windows.Forms.Label();
			this.lblWBRGainValue = new System.Windows.Forms.Label();
			this.trackBarWBRGain = new System.Windows.Forms.TrackBar();
			this.lblWBRGain = new System.Windows.Forms.Label();
			this.lblWBMode = new System.Windows.Forms.Label();
			this.tabPageY = new System.Windows.Forms.TabPage();
			this.trackBarAnalogBlackLevel = new System.Windows.Forms.TrackBar();
			this.label37 = new System.Windows.Forms.Label();
			this.txtAnalogBlackLevel = new StCtlLib.StTextBox();
			this.label35 = new System.Windows.Forms.Label();
			this.cmbShadingCorrectionMode = new StCtlLib.StComboBox();
			this.trackBarShadingCorrectionTarget = new System.Windows.Forms.TrackBar();
			this.label34 = new System.Windows.Forms.Label();
			this.txtShadingCorrectionTarget = new StCtlLib.StTextBox();
			this.trackBarDigitalClamp = new System.Windows.Forms.TrackBar();
			this.label30 = new System.Windows.Forms.Label();
			this.stTextBoxDigitalClamp = new StCtlLib.StTextBox();
			this.trackBarCameraGamma = new System.Windows.Forms.TrackBar();
			this.label29 = new System.Windows.Forms.Label();
			this.txtCameraGamma = new StCtlLib.StTextBox();
			this.trackBarYGamma = new System.Windows.Forms.TrackBar();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.trackBarSharpnessCoring = new System.Windows.Forms.TrackBar();
			this.lblSharpnessCoring = new System.Windows.Forms.Label();
			this.trackBarSharpnessGain = new System.Windows.Forms.TrackBar();
			this.lblSharpnessGain = new System.Windows.Forms.Label();
			this.lblSharpnessMode = new System.Windows.Forms.Label();
			this.txtYGamma = new StCtlLib.StTextBox();
			this.cmbYGammaMode = new StCtlLib.StComboBox();
			this.cmbSharpnessMode = new StCtlLib.StComboBox();
			this.txtSharpnessCoring = new StCtlLib.StTextBox();
			this.txtSharpnessGain = new StCtlLib.StTextBox();
			this.tabPageHDR_CMOSIS4M = new System.Windows.Forms.TabPage();
			this.trackBarHDR_CMOSIS4M_Vlow3 = new System.Windows.Forms.TrackBar();
			this.label42 = new System.Windows.Forms.Label();
			this.txtHDR_CMOSIS4M_Vlow3 = new StCtlLib.StTextBox();
			this.trackBarHDR_CMOSIS4M_Knee2 = new System.Windows.Forms.TrackBar();
			this.label43 = new System.Windows.Forms.Label();
			this.txtHDR_CMOSIS4M_Knee2 = new StCtlLib.StTextBox();
			this.trackBarHDR_CMOSIS4M_Vlow2 = new System.Windows.Forms.TrackBar();
			this.label41 = new System.Windows.Forms.Label();
			this.txtHDR_CMOSIS4M_Vlow2 = new StCtlLib.StTextBox();
			this.trackBarHDR_CMOSIS4M_Knee1 = new System.Windows.Forms.TrackBar();
			this.label40 = new System.Windows.Forms.Label();
			this.txtHDR_CMOSIS4M_Knee1 = new StCtlLib.StTextBox();
			this.label39 = new System.Windows.Forms.Label();
			this.cmbHDR_CMOSIS4M_SlopeNum = new StCtlLib.StComboBox();
			this.label38 = new System.Windows.Forms.Label();
			this.cmbHDR_CMOSIS4M_Mode = new StCtlLib.StComboBox();
			this.tabPageColorGamma = new System.Windows.Forms.TabPage();
			this.txtBGamma = new StCtlLib.StTextBox();
			this.trackBarBGamma = new System.Windows.Forms.TrackBar();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.trackBarGBGamma = new System.Windows.Forms.TrackBar();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.trackBarGRGamma = new System.Windows.Forms.TrackBar();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.trackBarRGamma = new System.Windows.Forms.TrackBar();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.cmbBGammaMode = new StCtlLib.StComboBox();
			this.txtGBGamma = new StCtlLib.StTextBox();
			this.cmbGBGammaMode = new StCtlLib.StComboBox();
			this.txtGRGamma = new StCtlLib.StTextBox();
			this.cmbGRGammaMode = new StCtlLib.StComboBox();
			this.txtRGamma = new StCtlLib.StTextBox();
			this.cmbRGammaMode = new StCtlLib.StComboBox();
			this.tabPageTriggerMode = new System.Windows.Forms.TabPage();
			this.txtTriggerDelay = new StCtlLib.StTextBox();
			this.lblTriggerDelay = new System.Windows.Forms.Label();
			this.label47 = new System.Windows.Forms.Label();
			this.trackBarTriggerDelay = new System.Windows.Forms.TrackBar();
			this.cmbTriggerSelector = new StCtlLib.StComboBox();
			this.label46 = new System.Windows.Forms.Label();
			this.btnExposureStartTrigger = new System.Windows.Forms.Button();
			this.cmbSensorShutterMode = new StCtlLib.StComboBox();
			this.label19 = new System.Windows.Forms.Label();
			this.lblTriggerOverlap = new System.Windows.Forms.Label();
			this.chkAutoTrigger = new System.Windows.Forms.CheckBox();
			this.lblAutoTriggerTime = new System.Windows.Forms.Label();
			this.trackBarAutoTriggerTime = new System.Windows.Forms.TrackBar();
			this.lblCameraMemory = new System.Windows.Forms.Label();
			this.btnExposureEndTrigger = new System.Windows.Forms.Button();
			this.btnResetFrameNo = new System.Windows.Forms.Button();
			this.btnSensorReadOutStartTrigger = new System.Windows.Forms.Button();
			this.btnFrameStartTrigger = new System.Windows.Forms.Button();
			this.lblExposureEnd = new System.Windows.Forms.Label();
			this.lblExposureWaitReadOut = new System.Windows.Forms.Label();
			this.lblExposureWaitHD = new System.Windows.Forms.Label();
			this.lblNoiseReduction = new System.Windows.Forms.Label();
			this.lblExposureMode = new System.Windows.Forms.Label();
			this.lblTriggerSource = new System.Windows.Forms.Label();
			this.lblTriggerMode = new System.Windows.Forms.Label();
			this.cmbTriggerOverlap = new StCtlLib.StComboBox();
			this.cmbCameraMemory = new StCtlLib.StComboBox();
			this.cmbExposureEnd = new StCtlLib.StComboBox();
			this.cmbExposureWaitReadOut = new StCtlLib.StComboBox();
			this.cmbExposureWaitHD = new StCtlLib.StComboBox();
			this.cmbNoiseReduction = new StCtlLib.StComboBox();
			this.cmbExposureMode = new StCtlLib.StComboBox();
			this.cmbTriggerSource = new StCtlLib.StComboBox();
			this.cmbTriggerMode = new StCtlLib.StComboBox();
			this.txtAutoTriggerTime = new StCtlLib.StTextBox();
			this.tabPageIO = new System.Windows.Forms.TabPage();
			this.txtTemperature = new StCtlLib.StTextBox();
			this.labelTemperature = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.cmbResetSwitch = new StCtlLib.StComboBox();
			this.txtSDKVersioin = new System.Windows.Forms.TextBox();
			this.lblSDKVersion = new System.Windows.Forms.Label();
			this.txtSWStatus3 = new System.Windows.Forms.TextBox();
			this.txtSWStatus2 = new System.Windows.Forms.TextBox();
			this.txtSWStatus1 = new System.Windows.Forms.TextBox();
			this.txtSWStatus0 = new System.Windows.Forms.TextBox();
			this.lblRed = new System.Windows.Forms.Label();
			this.lblLEDGreen = new System.Windows.Forms.Label();
			this.txtFirmVersion = new System.Windows.Forms.TextBox();
			this.txtFPGAVersion = new System.Windows.Forms.TextBox();
			this.txtCameraType = new System.Windows.Forms.TextBox();
			this.lblFirmVersion = new System.Windows.Forms.Label();
			this.lblFPGAVersion = new System.Windows.Forms.Label();
			this.lblCameraType = new System.Windows.Forms.Label();
			this.lblSW = new System.Windows.Forms.Label();
			this.lblIO3 = new System.Windows.Forms.Label();
			this.lblIO2 = new System.Windows.Forms.Label();
			this.lblIO1 = new System.Windows.Forms.Label();
			this.lblIOStatus = new System.Windows.Forms.Label();
			this.lblIOPolarity = new System.Windows.Forms.Label();
			this.lblIOMode = new System.Windows.Forms.Label();
			this.lblIOInOut = new System.Windows.Forms.Label();
			this.lblIO0 = new System.Windows.Forms.Label();
			this.cmbLEDRed = new StCtlLib.StComboBox();
			this.cmbLEDGreen = new StCtlLib.StComboBox();
			this.cmbIOStatus3 = new StCtlLib.StComboBox();
			this.cmbIOStatus2 = new StCtlLib.StComboBox();
			this.cmbIOStatus1 = new StCtlLib.StComboBox();
			this.cmbIOStatus0 = new StCtlLib.StComboBox();
			this.cmbIOPolarity3 = new StCtlLib.StComboBox();
			this.cmbIOPolarity2 = new StCtlLib.StComboBox();
			this.cmbIOPolarity1 = new StCtlLib.StComboBox();
			this.cmbIOPolarity0 = new StCtlLib.StComboBox();
			this.cmbIOMode3 = new StCtlLib.StComboBox();
			this.cmbIOMode2 = new StCtlLib.StComboBox();
			this.cmbIOMode1 = new StCtlLib.StComboBox();
			this.cmbIOMode0 = new StCtlLib.StComboBox();
			this.cmbIOInOut3 = new StCtlLib.StComboBox();
			this.cmbIOInOut2 = new StCtlLib.StComboBox();
			this.cmbIOInOut1 = new StCtlLib.StComboBox();
			this.cmbIOInOut0 = new StCtlLib.StComboBox();
			this.tabPageTriggerTiming = new System.Windows.Forms.TabPage();
			this.txtLineDebounceTime = new StCtlLib.StTextBox();
			this.lblLineDebounceTimeValue = new System.Windows.Forms.Label();
			this.trackBarLineDebounceTime = new System.Windows.Forms.TrackBar();
			this.lblLineDebounceTime = new System.Windows.Forms.Label();
			this.txtReadOutDelay = new StCtlLib.StTextBox();
			this.txtOutputPulseDuration = new StCtlLib.StTextBox();
			this.txtOutputPulseDelay = new StCtlLib.StTextBox();
			this.txtStrobeEndDelay = new StCtlLib.StTextBox();
			this.txtStrobeStartDelay = new StCtlLib.StTextBox();
			this.lblReadOutDelayValue = new System.Windows.Forms.Label();
			this.trackBarReadOutDelay = new System.Windows.Forms.TrackBar();
			this.lblReadOutDelay = new System.Windows.Forms.Label();
			this.lblOutputPulseDurationValue = new System.Windows.Forms.Label();
			this.trackBarOutputPulseDuration = new System.Windows.Forms.TrackBar();
			this.lblOutputPulseDuration = new System.Windows.Forms.Label();
			this.lblOutputPulseDelayValue = new System.Windows.Forms.Label();
			this.trackBarOutputPulseDelay = new System.Windows.Forms.TrackBar();
			this.lblOutputPulseDelay = new System.Windows.Forms.Label();
			this.lblStrobeEndDelayValue = new System.Windows.Forms.Label();
			this.trackBarStrobeEndDelay = new System.Windows.Forms.TrackBar();
			this.lblStrobeEndDelay = new System.Windows.Forms.Label();
			this.lbltrackBarStrobeStartDelayValue = new System.Windows.Forms.Label();
			this.trackBarStrobeStartDelay = new System.Windows.Forms.TrackBar();
			this.lblStrobeStartDelay = new System.Windows.Forms.Label();
			this.tabPageDefectPixelCorrection = new System.Windows.Forms.TabPage();
			this.defectPixelSetting1 = new StCtlLib.DefectPixelSetting();
			this.cmbDefectPixelCorrectionMode = new StCtlLib.StComboBox();
			this.label26 = new System.Windows.Forms.Label();
			this.btnDetectDefectPixel = new System.Windows.Forms.Button();
			this.numericUpDownDefectPixelThreshold = new System.Windows.Forms.NumericUpDown();
			this.label22 = new System.Windows.Forms.Label();
			this.tabPageEEPROM = new System.Windows.Forms.TabPage();
			this.btnWriteCameraSettingDPP = new System.Windows.Forms.Button();
			this.btnReadCameraSettingDPP = new System.Windows.Forms.Button();
			this.btnInitCameraSetting = new System.Windows.Forms.Button();
			this.btnWriteCameraSetting = new System.Windows.Forms.Button();
			this.btnReadCameraSetting = new System.Windows.Forms.Button();
			this.tabPageOther = new System.Windows.Forms.TabPage();
			this.labelDisplayPixelFormat = new System.Windows.Forms.Label();
			this.cmbDisplayPixelFormat = new StCtlLib.StComboBox();
			this.lblRegionMode = new System.Windows.Forms.Label();
			this.cmbRegionMode = new StCtlLib.StComboBox();
			this.lblCurrentRegion = new System.Windows.Forms.Label();
			this.cmbCurrentRegion = new StCtlLib.StComboBox();
			this.cmbVBinningSumMode = new StCtlLib.StComboBox();
			this.label31 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.cmbHBinningSumMode = new StCtlLib.StComboBox();
			this.labelOutputFPS = new System.Windows.Forms.Label();
			this.trackBarVBlankForFPS = new System.Windows.Forms.TrackBar();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.trackBarImageHeight = new System.Windows.Forms.TrackBar();
			this.label14 = new System.Windows.Forms.Label();
			this.trackBarImageWidth = new System.Windows.Forms.TrackBar();
			this.label15 = new System.Windows.Forms.Label();
			this.trackBarImageOffsetY = new System.Windows.Forms.TrackBar();
			this.label12 = new System.Windows.Forms.Label();
			this.trackBarImageOffsetX = new System.Windows.Forms.TrackBar();
			this.label13 = new System.Windows.Forms.Label();
			this.lblTransferBitsPerPixel = new System.Windows.Forms.Label();
			this.lblRotation = new System.Windows.Forms.Label();
			this.lblMirror = new System.Windows.Forms.Label();
			this.lblColorInterpolation = new System.Windows.Forms.Label();
			this.lblClockMode = new System.Windows.Forms.Label();
			this.lblScanMode = new System.Windows.Forms.Label();
			this.stTextBoxVBlankForFPS = new StCtlLib.StTextBox();
			this.stComboBoxVBinningSkipping = new StCtlLib.StComboBox();
			this.stComboBoxHBinningSkipping = new StCtlLib.StComboBox();
			this.stTextBoxImageHeight = new StCtlLib.StTextBox();
			this.stTextBoxImageWidth = new StCtlLib.StTextBox();
			this.stTextBoxImageOffsetY = new StCtlLib.StTextBox();
			this.stTextBoxImageOffsetX = new StCtlLib.StTextBox();
			this.cmbTransferBitsPerPixel = new StCtlLib.StComboBox();
			this.cmbRotation = new StCtlLib.StComboBox();
			this.cmbMirror = new StCtlLib.StComboBox();
			this.cmbColorInterpolation = new StCtlLib.StComboBox();
			this.cmbClockMode = new StCtlLib.StComboBox();
			this.cmbScanMode = new StCtlLib.StComboBox();
			this.panelButton = new System.Windows.Forms.Panel();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.tabPageShutterGain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarAGCMaxGain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarAGCMinGain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarAEMaxExposure)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarAEMinExposure)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarALCTarget)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarDigitalGain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarGain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarExposure)).BeginInit();
			this.tabPageWB.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarLowChromaSuppresionSuppressionLevel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarLowChromaSuppresionStartLevel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarChromaSuppresionSuppressionLevel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarChromaSuppresionStartLevel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSaturation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarHue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarWBBGain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarWBGbGain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarWBGrGain)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarWBRGain)).BeginInit();
			this.tabPageY.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarAnalogBlackLevel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarShadingCorrectionTarget)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarDigitalClamp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarCameraGamma)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarYGamma)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSharpnessCoring)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSharpnessGain)).BeginInit();
			this.tabPageHDR_CMOSIS4M.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarHDR_CMOSIS4M_Vlow3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarHDR_CMOSIS4M_Knee2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarHDR_CMOSIS4M_Vlow2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarHDR_CMOSIS4M_Knee1)).BeginInit();
			this.tabPageColorGamma.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarBGamma)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarGBGamma)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarGRGamma)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarRGamma)).BeginInit();
			this.tabPageTriggerMode.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarTriggerDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarAutoTriggerTime)).BeginInit();
			this.tabPageIO.SuspendLayout();
			this.tabPageTriggerTiming.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarLineDebounceTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarReadOutDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarOutputPulseDuration)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarOutputPulseDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarStrobeEndDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarStrobeStartDelay)).BeginInit();
			this.tabPageDefectPixelCorrection.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDefectPixelThreshold)).BeginInit();
			this.tabPageEEPROM.SuspendLayout();
			this.tabPageOther.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarVBlankForFPS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarImageHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarImageWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarImageOffsetY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarImageOffsetX)).BeginInit();
			this.panelButton.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageShutterGain);
			this.tabControl.Controls.Add(this.tabPageWB);
			this.tabControl.Controls.Add(this.tabPageY);
			this.tabControl.Controls.Add(this.tabPageHDR_CMOSIS4M);
			this.tabControl.Controls.Add(this.tabPageColorGamma);
			this.tabControl.Controls.Add(this.tabPageTriggerMode);
			this.tabControl.Controls.Add(this.tabPageIO);
			this.tabControl.Controls.Add(this.tabPageTriggerTiming);
			this.tabControl.Controls.Add(this.tabPageDefectPixelCorrection);
			this.tabControl.Controls.Add(this.tabPageEEPROM);
			this.tabControl.Controls.Add(this.tabPageOther);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(480, 545);
			this.tabControl.TabIndex = 0;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// tabPageShutterGain
			// 
			this.tabPageShutterGain.Controls.Add(this.cmbAdjustmentDigitalGain);
			this.tabPageShutterGain.Controls.Add(this.labelAdjustmentDigitalGain);
			this.tabPageShutterGain.Controls.Add(this.txtAGCMaxGain);
			this.tabPageShutterGain.Controls.Add(this.lblAGCMaxGain);
			this.tabPageShutterGain.Controls.Add(this.trackBarAGCMaxGain);
			this.tabPageShutterGain.Controls.Add(this.label25);
			this.tabPageShutterGain.Controls.Add(this.txtAGCMinGain);
			this.tabPageShutterGain.Controls.Add(this.lblAGCMinGain);
			this.tabPageShutterGain.Controls.Add(this.trackBarAGCMinGain);
			this.tabPageShutterGain.Controls.Add(this.label27);
			this.tabPageShutterGain.Controls.Add(this.txtAEMaxExposure);
			this.tabPageShutterGain.Controls.Add(this.lblAEMaxExposure);
			this.tabPageShutterGain.Controls.Add(this.trackBarAEMaxExposure);
			this.tabPageShutterGain.Controls.Add(this.label24);
			this.tabPageShutterGain.Controls.Add(this.txtAEMinExposure);
			this.tabPageShutterGain.Controls.Add(this.lblAEMinExposure);
			this.tabPageShutterGain.Controls.Add(this.trackBarAEMinExposure);
			this.tabPageShutterGain.Controls.Add(this.label23);
			this.tabPageShutterGain.Controls.Add(this.txtALCTarget);
			this.tabPageShutterGain.Controls.Add(this.trackBarALCTarget);
			this.tabPageShutterGain.Controls.Add(this.label21);
			this.tabPageShutterGain.Controls.Add(this.cmbALCMode);
			this.tabPageShutterGain.Controls.Add(this.label20);
			this.tabPageShutterGain.Controls.Add(this.txtDigitalGain);
			this.tabPageShutterGain.Controls.Add(this.txtGain);
			this.tabPageShutterGain.Controls.Add(this.txtExposure);
			this.tabPageShutterGain.Controls.Add(this.lblDigitalGainValue);
			this.tabPageShutterGain.Controls.Add(this.trackBarDigitalGain);
			this.tabPageShutterGain.Controls.Add(this.lblDigitalGain);
			this.tabPageShutterGain.Controls.Add(this.lblGainValue);
			this.tabPageShutterGain.Controls.Add(this.trackBarGain);
			this.tabPageShutterGain.Controls.Add(this.lblGain);
			this.tabPageShutterGain.Controls.Add(this.lblExposureValue);
			this.tabPageShutterGain.Controls.Add(this.trackBarExposure);
			this.tabPageShutterGain.Controls.Add(this.lblExposure);
			this.tabPageShutterGain.Location = new System.Drawing.Point(4, 22);
			this.tabPageShutterGain.Name = "tabPageShutterGain";
			this.tabPageShutterGain.Size = new System.Drawing.Size(472, 519);
			this.tabPageShutterGain.TabIndex = 0;
			this.tabPageShutterGain.Text = "Shutter/Gain";
			this.tabPageShutterGain.UseVisualStyleBackColor = true;
			// 
			// cmbAdjustmentDigitalGain
			// 
			this.cmbAdjustmentDigitalGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbAdjustmentDigitalGain.Location = new System.Drawing.Point(80, 518);
			this.cmbAdjustmentDigitalGain.Name = "cmbAdjustmentDigitalGain";
			this.cmbAdjustmentDigitalGain.Size = new System.Drawing.Size(224, 20);
			this.cmbAdjustmentDigitalGain.StValue = ((long)(0));
			this.cmbAdjustmentDigitalGain.TabIndex = 37;
			this.cmbAdjustmentDigitalGain.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// labelAdjustmentDigitalGain
			// 
			this.labelAdjustmentDigitalGain.AutoSize = true;
			this.labelAdjustmentDigitalGain.Location = new System.Drawing.Point(8, 499);
			this.labelAdjustmentDigitalGain.Name = "labelAdjustmentDigitalGain";
			this.labelAdjustmentDigitalGain.Size = new System.Drawing.Size(117, 12);
			this.labelAdjustmentDigitalGain.TabIndex = 36;
			this.labelAdjustmentDigitalGain.Text = "Adjustment Digital Gain";
			// 
			// txtAGCMaxGain
			// 
			this.txtAGCMaxGain.Location = new System.Drawing.Point(392, 451);
			this.txtAGCMaxGain.Name = "txtAGCMaxGain";
			this.txtAGCMaxGain.Size = new System.Drawing.Size(76, 22);
			this.txtAGCMaxGain.TabIndex = 33;
			this.txtAGCMaxGain.Text = "stTextBox3";
			this.txtAGCMaxGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtAGCMaxGain.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// lblAGCMaxGain
			// 
			this.lblAGCMaxGain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblAGCMaxGain.Location = new System.Drawing.Point(296, 451);
			this.lblAGCMaxGain.Name = "lblAGCMaxGain";
			this.lblAGCMaxGain.Size = new System.Drawing.Size(88, 30);
			this.lblAGCMaxGain.TabIndex = 35;
			this.lblAGCMaxGain.Text = "s";
			this.lblAGCMaxGain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarAGCMaxGain
			// 
			this.trackBarAGCMaxGain.AutoSize = false;
			this.trackBarAGCMaxGain.LargeChange = 1000;
			this.trackBarAGCMaxGain.Location = new System.Drawing.Point(80, 451);
			this.trackBarAGCMaxGain.Name = "trackBarAGCMaxGain";
			this.trackBarAGCMaxGain.Size = new System.Drawing.Size(216, 30);
			this.trackBarAGCMaxGain.TabIndex = 32;
			this.trackBarAGCMaxGain.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarAGCMaxGain.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(8, 432);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(78, 12);
			this.label25.TabIndex = 34;
			this.label25.Text = "AGC Max Gain";
			// 
			// txtAGCMinGain
			// 
			this.txtAGCMinGain.Location = new System.Drawing.Point(392, 385);
			this.txtAGCMinGain.Name = "txtAGCMinGain";
			this.txtAGCMinGain.Size = new System.Drawing.Size(76, 22);
			this.txtAGCMinGain.TabIndex = 29;
			this.txtAGCMinGain.Text = "stTextBox3";
			this.txtAGCMinGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtAGCMinGain.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// lblAGCMinGain
			// 
			this.lblAGCMinGain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblAGCMinGain.Location = new System.Drawing.Point(296, 385);
			this.lblAGCMinGain.Name = "lblAGCMinGain";
			this.lblAGCMinGain.Size = new System.Drawing.Size(88, 30);
			this.lblAGCMinGain.TabIndex = 31;
			this.lblAGCMinGain.Text = "s";
			this.lblAGCMinGain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarAGCMinGain
			// 
			this.trackBarAGCMinGain.AutoSize = false;
			this.trackBarAGCMinGain.LargeChange = 1000;
			this.trackBarAGCMinGain.Location = new System.Drawing.Point(80, 385);
			this.trackBarAGCMinGain.Name = "trackBarAGCMinGain";
			this.trackBarAGCMinGain.Size = new System.Drawing.Size(216, 30);
			this.trackBarAGCMinGain.TabIndex = 28;
			this.trackBarAGCMinGain.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarAGCMinGain.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(8, 366);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(76, 12);
			this.label27.TabIndex = 30;
			this.label27.Text = "AGC Min Gain";
			// 
			// txtAEMaxExposure
			// 
			this.txtAEMaxExposure.Location = new System.Drawing.Point(392, 319);
			this.txtAEMaxExposure.Name = "txtAEMaxExposure";
			this.txtAEMaxExposure.Size = new System.Drawing.Size(76, 22);
			this.txtAEMaxExposure.TabIndex = 25;
			this.txtAEMaxExposure.Text = "stTextBox3";
			this.txtAEMaxExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtAEMaxExposure.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// lblAEMaxExposure
			// 
			this.lblAEMaxExposure.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblAEMaxExposure.Location = new System.Drawing.Point(296, 319);
			this.lblAEMaxExposure.Name = "lblAEMaxExposure";
			this.lblAEMaxExposure.Size = new System.Drawing.Size(88, 30);
			this.lblAEMaxExposure.TabIndex = 27;
			this.lblAEMaxExposure.Text = "s";
			this.lblAEMaxExposure.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarAEMaxExposure
			// 
			this.trackBarAEMaxExposure.AutoSize = false;
			this.trackBarAEMaxExposure.LargeChange = 1000;
			this.trackBarAEMaxExposure.Location = new System.Drawing.Point(80, 319);
			this.trackBarAEMaxExposure.Name = "trackBarAEMaxExposure";
			this.trackBarAEMaxExposure.Size = new System.Drawing.Size(216, 30);
			this.trackBarAEMaxExposure.TabIndex = 24;
			this.trackBarAEMaxExposure.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarAEMaxExposure.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(8, 289);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(91, 12);
			this.label24.TabIndex = 26;
			this.label24.Text = "AE Max Exposure";
			// 
			// txtAEMinExposure
			// 
			this.txtAEMinExposure.Location = new System.Drawing.Point(392, 241);
			this.txtAEMinExposure.Name = "txtAEMinExposure";
			this.txtAEMinExposure.Size = new System.Drawing.Size(76, 22);
			this.txtAEMinExposure.TabIndex = 21;
			this.txtAEMinExposure.Text = "stTextBox3";
			this.txtAEMinExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtAEMinExposure.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// lblAEMinExposure
			// 
			this.lblAEMinExposure.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblAEMinExposure.Location = new System.Drawing.Point(296, 241);
			this.lblAEMinExposure.Name = "lblAEMinExposure";
			this.lblAEMinExposure.Size = new System.Drawing.Size(88, 30);
			this.lblAEMinExposure.TabIndex = 23;
			this.lblAEMinExposure.Text = "s";
			this.lblAEMinExposure.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarAEMinExposure
			// 
			this.trackBarAEMinExposure.AutoSize = false;
			this.trackBarAEMinExposure.LargeChange = 1000;
			this.trackBarAEMinExposure.Location = new System.Drawing.Point(80, 241);
			this.trackBarAEMinExposure.Name = "trackBarAEMinExposure";
			this.trackBarAEMinExposure.Size = new System.Drawing.Size(216, 30);
			this.trackBarAEMinExposure.TabIndex = 20;
			this.trackBarAEMinExposure.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarAEMinExposure.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(8, 222);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(89, 12);
			this.label23.TabIndex = 22;
			this.label23.Text = "AE Min Exposure";
			// 
			// txtALCTarget
			// 
			this.txtALCTarget.Location = new System.Drawing.Point(392, 175);
			this.txtALCTarget.Name = "txtALCTarget";
			this.txtALCTarget.Size = new System.Drawing.Size(76, 22);
			this.txtALCTarget.TabIndex = 18;
			this.txtALCTarget.Text = "stTextBox3";
			this.txtALCTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtALCTarget.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// trackBarALCTarget
			// 
			this.trackBarALCTarget.AutoSize = false;
			this.trackBarALCTarget.LargeChange = 1000;
			this.trackBarALCTarget.Location = new System.Drawing.Point(80, 175);
			this.trackBarALCTarget.Name = "trackBarALCTarget";
			this.trackBarALCTarget.Size = new System.Drawing.Size(216, 30);
			this.trackBarALCTarget.TabIndex = 17;
			this.trackBarALCTarget.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarALCTarget.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(8, 185);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(61, 12);
			this.label21.TabIndex = 19;
			this.label21.Text = "ALC Target";
			// 
			// cmbALCMode
			// 
			this.cmbALCMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbALCMode.Location = new System.Drawing.Point(80, 142);
			this.cmbALCMode.Name = "cmbALCMode";
			this.cmbALCMode.Size = new System.Drawing.Size(224, 20);
			this.cmbALCMode.StValue = ((long)(0));
			this.cmbALCMode.TabIndex = 16;
			this.cmbALCMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(8, 146);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(58, 12);
			this.label20.TabIndex = 15;
			this.label20.Text = "ALC Mode";
			// 
			// txtDigitalGain
			// 
			this.txtDigitalGain.Location = new System.Drawing.Point(392, 90);
			this.txtDigitalGain.Name = "txtDigitalGain";
			this.txtDigitalGain.Size = new System.Drawing.Size(76, 22);
			this.txtDigitalGain.TabIndex = 5;
			this.txtDigitalGain.Text = "stTextBox3";
			this.txtDigitalGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtDigitalGain.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtGain
			// 
			this.txtGain.Location = new System.Drawing.Point(392, 50);
			this.txtGain.Name = "txtGain";
			this.txtGain.Size = new System.Drawing.Size(76, 22);
			this.txtGain.TabIndex = 3;
			this.txtGain.Text = "stTextBox2";
			this.txtGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtGain.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtExposure
			// 
			this.txtExposure.Location = new System.Drawing.Point(392, 10);
			this.txtExposure.Name = "txtExposure";
			this.txtExposure.Size = new System.Drawing.Size(76, 22);
			this.txtExposure.TabIndex = 1;
			this.txtExposure.Text = "stTextBox1";
			this.txtExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtExposure.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// lblDigitalGainValue
			// 
			this.lblDigitalGainValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblDigitalGainValue.Location = new System.Drawing.Point(296, 90);
			this.lblDigitalGainValue.Name = "lblDigitalGainValue";
			this.lblDigitalGainValue.Size = new System.Drawing.Size(88, 30);
			this.lblDigitalGainValue.TabIndex = 14;
			this.lblDigitalGainValue.Text = "s";
			this.lblDigitalGainValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarDigitalGain
			// 
			this.trackBarDigitalGain.AutoSize = false;
			this.trackBarDigitalGain.LargeChange = 1000;
			this.trackBarDigitalGain.Location = new System.Drawing.Point(80, 90);
			this.trackBarDigitalGain.Name = "trackBarDigitalGain";
			this.trackBarDigitalGain.Size = new System.Drawing.Size(216, 30);
			this.trackBarDigitalGain.TabIndex = 4;
			this.trackBarDigitalGain.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarDigitalGain.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblDigitalGain
			// 
			this.lblDigitalGain.AutoSize = true;
			this.lblDigitalGain.Location = new System.Drawing.Point(8, 100);
			this.lblDigitalGain.Name = "lblDigitalGain";
			this.lblDigitalGain.Size = new System.Drawing.Size(58, 12);
			this.lblDigitalGain.TabIndex = 12;
			this.lblDigitalGain.Text = "DigitalGain";
			// 
			// lblGainValue
			// 
			this.lblGainValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblGainValue.Location = new System.Drawing.Point(296, 50);
			this.lblGainValue.Name = "lblGainValue";
			this.lblGainValue.Size = new System.Drawing.Size(88, 30);
			this.lblGainValue.TabIndex = 10;
			this.lblGainValue.Text = "s";
			this.lblGainValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarGain
			// 
			this.trackBarGain.AutoSize = false;
			this.trackBarGain.LargeChange = 1000;
			this.trackBarGain.Location = new System.Drawing.Point(80, 50);
			this.trackBarGain.Name = "trackBarGain";
			this.trackBarGain.Size = new System.Drawing.Size(216, 30);
			this.trackBarGain.TabIndex = 2;
			this.trackBarGain.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarGain.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblGain
			// 
			this.lblGain.AutoSize = true;
			this.lblGain.Location = new System.Drawing.Point(8, 60);
			this.lblGain.Name = "lblGain";
			this.lblGain.Size = new System.Drawing.Size(27, 12);
			this.lblGain.TabIndex = 8;
			this.lblGain.Text = "Gain";
			// 
			// lblExposureValue
			// 
			this.lblExposureValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblExposureValue.Location = new System.Drawing.Point(296, 10);
			this.lblExposureValue.Name = "lblExposureValue";
			this.lblExposureValue.Size = new System.Drawing.Size(88, 30);
			this.lblExposureValue.TabIndex = 6;
			this.lblExposureValue.Text = "s";
			this.lblExposureValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarExposure
			// 
			this.trackBarExposure.AutoSize = false;
			this.trackBarExposure.LargeChange = 1000;
			this.trackBarExposure.Location = new System.Drawing.Point(80, 10);
			this.trackBarExposure.Name = "trackBarExposure";
			this.trackBarExposure.Size = new System.Drawing.Size(216, 30);
			this.trackBarExposure.TabIndex = 0;
			this.trackBarExposure.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarExposure.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblExposure
			// 
			this.lblExposure.AutoSize = true;
			this.lblExposure.Location = new System.Drawing.Point(8, 20);
			this.lblExposure.Name = "lblExposure";
			this.lblExposure.Size = new System.Drawing.Size(49, 12);
			this.lblExposure.TabIndex = 4;
			this.lblExposure.Text = "Exposure";
			// 
			// tabPageWB
			// 
			this.tabPageWB.Controls.Add(this.txtLowChromaSuppresionSuppressionLevel);
			this.tabPageWB.Controls.Add(this.trackBarLowChromaSuppresionSuppressionLevel);
			this.tabPageWB.Controls.Add(this.label45);
			this.tabPageWB.Controls.Add(this.txtLowChromaSuppresionStartLevel);
			this.tabPageWB.Controls.Add(this.trackBarLowChromaSuppresionStartLevel);
			this.tabPageWB.Controls.Add(this.label44);
			this.tabPageWB.Controls.Add(this.txtChromaSuppresionSuppressionLevel);
			this.tabPageWB.Controls.Add(this.txtChromaSuppresionStartLevel);
			this.tabPageWB.Controls.Add(this.trackBarChromaSuppresionSuppressionLevel);
			this.tabPageWB.Controls.Add(this.label32);
			this.tabPageWB.Controls.Add(this.trackBarChromaSuppresionStartLevel);
			this.tabPageWB.Controls.Add(this.label33);
			this.tabPageWB.Controls.Add(this.cmbHueSaturationMode);
			this.tabPageWB.Controls.Add(this.cmbWBMode);
			this.tabPageWB.Controls.Add(this.txtSaturation);
			this.tabPageWB.Controls.Add(this.txtHue);
			this.tabPageWB.Controls.Add(this.txtWBBGain);
			this.tabPageWB.Controls.Add(this.txtWBGbGain);
			this.tabPageWB.Controls.Add(this.txtWBGrGain);
			this.tabPageWB.Controls.Add(this.txtWBRGain);
			this.tabPageWB.Controls.Add(this.lblSaturationValue);
			this.tabPageWB.Controls.Add(this.trackBarSaturation);
			this.tabPageWB.Controls.Add(this.label2);
			this.tabPageWB.Controls.Add(this.lblHueValue);
			this.tabPageWB.Controls.Add(this.trackBarHue);
			this.tabPageWB.Controls.Add(this.lblHue);
			this.tabPageWB.Controls.Add(this.lblHueSaturationMode);
			this.tabPageWB.Controls.Add(this.lblWBBGainValue);
			this.tabPageWB.Controls.Add(this.trackBarWBBGain);
			this.tabPageWB.Controls.Add(this.lblWBBGain);
			this.tabPageWB.Controls.Add(this.lblWBGbGainValue);
			this.tabPageWB.Controls.Add(this.trackBarWBGbGain);
			this.tabPageWB.Controls.Add(this.lblWBGbGain);
			this.tabPageWB.Controls.Add(this.lblWBGrGainValue);
			this.tabPageWB.Controls.Add(this.trackBarWBGrGain);
			this.tabPageWB.Controls.Add(this.lblWBGrGain);
			this.tabPageWB.Controls.Add(this.lblWBRGainValue);
			this.tabPageWB.Controls.Add(this.trackBarWBRGain);
			this.tabPageWB.Controls.Add(this.lblWBRGain);
			this.tabPageWB.Controls.Add(this.lblWBMode);
			this.tabPageWB.Location = new System.Drawing.Point(4, 22);
			this.tabPageWB.Name = "tabPageWB";
			this.tabPageWB.Size = new System.Drawing.Size(472, 519);
			this.tabPageWB.TabIndex = 1;
			this.tabPageWB.Text = "White Balance";
			this.tabPageWB.UseVisualStyleBackColor = true;
			// 
			// txtLowChromaSuppresionSuppressionLevel
			// 
			this.txtLowChromaSuppresionSuppressionLevel.Location = new System.Drawing.Point(400, 520);
			this.txtLowChromaSuppresionSuppressionLevel.Name = "txtLowChromaSuppresionSuppressionLevel";
			this.txtLowChromaSuppresionSuppressionLevel.Size = new System.Drawing.Size(64, 22);
			this.txtLowChromaSuppresionSuppressionLevel.TabIndex = 44;
			this.txtLowChromaSuppresionSuppressionLevel.Text = "stTextBox10";
			this.txtLowChromaSuppresionSuppressionLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtLowChromaSuppresionSuppressionLevel.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// trackBarLowChromaSuppresionSuppressionLevel
			// 
			this.trackBarLowChromaSuppresionSuppressionLevel.AutoSize = false;
			this.trackBarLowChromaSuppresionSuppressionLevel.LargeChange = 1000;
			this.trackBarLowChromaSuppresionSuppressionLevel.Location = new System.Drawing.Point(96, 520);
			this.trackBarLowChromaSuppresionSuppressionLevel.Name = "trackBarLowChromaSuppresionSuppressionLevel";
			this.trackBarLowChromaSuppresionSuppressionLevel.Size = new System.Drawing.Size(224, 30);
			this.trackBarLowChromaSuppresionSuppressionLevel.TabIndex = 43;
			this.trackBarLowChromaSuppresionSuppressionLevel.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarLowChromaSuppresionSuppressionLevel.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label45
			// 
			this.label45.AutoSize = true;
			this.label45.Location = new System.Drawing.Point(8, 501);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(214, 12);
			this.label45.TabIndex = 42;
			this.label45.Text = "Low Chroma Suppression Suppression Level";
			// 
			// txtLowChromaSuppresionStartLevel
			// 
			this.txtLowChromaSuppresionStartLevel.Location = new System.Drawing.Point(400, 468);
			this.txtLowChromaSuppresionStartLevel.Name = "txtLowChromaSuppresionStartLevel";
			this.txtLowChromaSuppresionStartLevel.Size = new System.Drawing.Size(64, 22);
			this.txtLowChromaSuppresionStartLevel.TabIndex = 41;
			this.txtLowChromaSuppresionStartLevel.Text = "stTextBox9";
			this.txtLowChromaSuppresionStartLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtLowChromaSuppresionStartLevel.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// trackBarLowChromaSuppresionStartLevel
			// 
			this.trackBarLowChromaSuppresionStartLevel.AutoSize = false;
			this.trackBarLowChromaSuppresionStartLevel.LargeChange = 1000;
			this.trackBarLowChromaSuppresionStartLevel.Location = new System.Drawing.Point(96, 468);
			this.trackBarLowChromaSuppresionStartLevel.Name = "trackBarLowChromaSuppresionStartLevel";
			this.trackBarLowChromaSuppresionStartLevel.Size = new System.Drawing.Size(224, 30);
			this.trackBarLowChromaSuppresionStartLevel.TabIndex = 40;
			this.trackBarLowChromaSuppresionStartLevel.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarLowChromaSuppresionStartLevel.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label44
			// 
			this.label44.AutoSize = true;
			this.label44.Location = new System.Drawing.Point(8, 449);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(179, 12);
			this.label44.TabIndex = 39;
			this.label44.Text = "Low Chroma Suppression Start Level";
			// 
			// txtChromaSuppresionSuppressionLevel
			// 
			this.txtChromaSuppresionSuppressionLevel.Location = new System.Drawing.Point(400, 415);
			this.txtChromaSuppresionSuppressionLevel.Name = "txtChromaSuppresionSuppressionLevel";
			this.txtChromaSuppresionSuppressionLevel.Size = new System.Drawing.Size(64, 22);
			this.txtChromaSuppresionSuppressionLevel.TabIndex = 36;
			this.txtChromaSuppresionSuppressionLevel.Text = "stTextBox10";
			this.txtChromaSuppresionSuppressionLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtChromaSuppresionSuppressionLevel.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtChromaSuppresionStartLevel
			// 
			this.txtChromaSuppresionStartLevel.Location = new System.Drawing.Point(400, 362);
			this.txtChromaSuppresionStartLevel.Name = "txtChromaSuppresionStartLevel";
			this.txtChromaSuppresionStartLevel.Size = new System.Drawing.Size(64, 22);
			this.txtChromaSuppresionStartLevel.TabIndex = 34;
			this.txtChromaSuppresionStartLevel.Text = "stTextBox9";
			this.txtChromaSuppresionStartLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtChromaSuppresionStartLevel.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// trackBarChromaSuppresionSuppressionLevel
			// 
			this.trackBarChromaSuppresionSuppressionLevel.AutoSize = false;
			this.trackBarChromaSuppresionSuppressionLevel.LargeChange = 1000;
			this.trackBarChromaSuppresionSuppressionLevel.Location = new System.Drawing.Point(96, 415);
			this.trackBarChromaSuppresionSuppressionLevel.Name = "trackBarChromaSuppresionSuppressionLevel";
			this.trackBarChromaSuppresionSuppressionLevel.Size = new System.Drawing.Size(224, 30);
			this.trackBarChromaSuppresionSuppressionLevel.TabIndex = 35;
			this.trackBarChromaSuppresionSuppressionLevel.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarChromaSuppresionSuppressionLevel.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label32
			// 
			this.label32.AutoSize = true;
			this.label32.Location = new System.Drawing.Point(8, 396);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(216, 12);
			this.label32.TabIndex = 38;
			this.label32.Text = "High Chroma Suppression Suppression Level";
			// 
			// trackBarChromaSuppresionStartLevel
			// 
			this.trackBarChromaSuppresionStartLevel.AutoSize = false;
			this.trackBarChromaSuppresionStartLevel.LargeChange = 1000;
			this.trackBarChromaSuppresionStartLevel.Location = new System.Drawing.Point(96, 362);
			this.trackBarChromaSuppresionStartLevel.Name = "trackBarChromaSuppresionStartLevel";
			this.trackBarChromaSuppresionStartLevel.Size = new System.Drawing.Size(224, 30);
			this.trackBarChromaSuppresionStartLevel.TabIndex = 33;
			this.trackBarChromaSuppresionStartLevel.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarChromaSuppresionStartLevel.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label33
			// 
			this.label33.AutoSize = true;
			this.label33.Location = new System.Drawing.Point(8, 344);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(181, 12);
			this.label33.TabIndex = 37;
			this.label33.Text = "High Chroma Suppression Start Level";
			// 
			// cmbHueSaturationMode
			// 
			this.cmbHueSaturationMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbHueSaturationMode.Location = new System.Drawing.Point(104, 230);
			this.cmbHueSaturationMode.Name = "cmbHueSaturationMode";
			this.cmbHueSaturationMode.Size = new System.Drawing.Size(224, 20);
			this.cmbHueSaturationMode.StValue = ((long)(0));
			this.cmbHueSaturationMode.TabIndex = 15;
			this.cmbHueSaturationMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbWBMode
			// 
			this.cmbWBMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbWBMode.Location = new System.Drawing.Point(104, 20);
			this.cmbWBMode.Name = "cmbWBMode";
			this.cmbWBMode.Size = new System.Drawing.Size(224, 20);
			this.cmbWBMode.StValue = ((long)(0));
			this.cmbWBMode.TabIndex = 6;
			this.cmbWBMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// txtSaturation
			// 
			this.txtSaturation.Location = new System.Drawing.Point(400, 310);
			this.txtSaturation.Name = "txtSaturation";
			this.txtSaturation.Size = new System.Drawing.Size(64, 22);
			this.txtSaturation.TabIndex = 19;
			this.txtSaturation.Text = "stTextBox10";
			this.txtSaturation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSaturation.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtHue
			// 
			this.txtHue.Location = new System.Drawing.Point(400, 270);
			this.txtHue.Name = "txtHue";
			this.txtHue.Size = new System.Drawing.Size(64, 22);
			this.txtHue.TabIndex = 17;
			this.txtHue.Text = "stTextBox9";
			this.txtHue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtHue.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtWBBGain
			// 
			this.txtWBBGain.Location = new System.Drawing.Point(400, 180);
			this.txtWBBGain.Name = "txtWBBGain";
			this.txtWBBGain.Size = new System.Drawing.Size(64, 22);
			this.txtWBBGain.TabIndex = 14;
			this.txtWBBGain.Text = "stTextBox8";
			this.txtWBBGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtWBBGain.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtWBGbGain
			// 
			this.txtWBGbGain.Location = new System.Drawing.Point(400, 140);
			this.txtWBGbGain.Name = "txtWBGbGain";
			this.txtWBGbGain.Size = new System.Drawing.Size(64, 22);
			this.txtWBGbGain.TabIndex = 12;
			this.txtWBGbGain.Text = "stTextBox7";
			this.txtWBGbGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtWBGbGain.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtWBGrGain
			// 
			this.txtWBGrGain.Location = new System.Drawing.Point(400, 100);
			this.txtWBGrGain.Name = "txtWBGrGain";
			this.txtWBGrGain.Size = new System.Drawing.Size(64, 22);
			this.txtWBGrGain.TabIndex = 10;
			this.txtWBGrGain.Text = "stTextBox6";
			this.txtWBGrGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtWBGrGain.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtWBRGain
			// 
			this.txtWBRGain.Location = new System.Drawing.Point(400, 60);
			this.txtWBRGain.Name = "txtWBRGain";
			this.txtWBRGain.Size = new System.Drawing.Size(64, 22);
			this.txtWBRGain.TabIndex = 8;
			this.txtWBRGain.Text = "stTextBox5";
			this.txtWBRGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtWBRGain.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// lblSaturationValue
			// 
			this.lblSaturationValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblSaturationValue.Location = new System.Drawing.Point(320, 310);
			this.lblSaturationValue.Name = "lblSaturationValue";
			this.lblSaturationValue.Size = new System.Drawing.Size(72, 30);
			this.lblSaturationValue.TabIndex = 32;
			this.lblSaturationValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarSaturation
			// 
			this.trackBarSaturation.AutoSize = false;
			this.trackBarSaturation.LargeChange = 1000;
			this.trackBarSaturation.Location = new System.Drawing.Point(96, 310);
			this.trackBarSaturation.Name = "trackBarSaturation";
			this.trackBarSaturation.Size = new System.Drawing.Size(224, 30);
			this.trackBarSaturation.TabIndex = 18;
			this.trackBarSaturation.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarSaturation.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 320);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 12);
			this.label2.TabIndex = 30;
			this.label2.Text = "Saturation";
			// 
			// lblHueValue
			// 
			this.lblHueValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblHueValue.Location = new System.Drawing.Point(320, 270);
			this.lblHueValue.Name = "lblHueValue";
			this.lblHueValue.Size = new System.Drawing.Size(72, 30);
			this.lblHueValue.TabIndex = 28;
			this.lblHueValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarHue
			// 
			this.trackBarHue.AutoSize = false;
			this.trackBarHue.LargeChange = 1000;
			this.trackBarHue.Location = new System.Drawing.Point(96, 270);
			this.trackBarHue.Name = "trackBarHue";
			this.trackBarHue.Size = new System.Drawing.Size(224, 30);
			this.trackBarHue.TabIndex = 16;
			this.trackBarHue.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarHue.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblHue
			// 
			this.lblHue.AutoSize = true;
			this.lblHue.Location = new System.Drawing.Point(8, 280);
			this.lblHue.Name = "lblHue";
			this.lblHue.Size = new System.Drawing.Size(24, 12);
			this.lblHue.TabIndex = 26;
			this.lblHue.Text = "Hue";
			// 
			// lblHueSaturationMode
			// 
			this.lblHueSaturationMode.AutoSize = true;
			this.lblHueSaturationMode.Location = new System.Drawing.Point(8, 230);
			this.lblHueSaturationMode.Name = "lblHueSaturationMode";
			this.lblHueSaturationMode.Size = new System.Drawing.Size(74, 12);
			this.lblHueSaturationMode.TabIndex = 24;
			this.lblHueSaturationMode.Text = "Hue/Saturation";
			// 
			// lblWBBGainValue
			// 
			this.lblWBBGainValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblWBBGainValue.Location = new System.Drawing.Point(320, 180);
			this.lblWBBGainValue.Name = "lblWBBGainValue";
			this.lblWBBGainValue.Size = new System.Drawing.Size(72, 30);
			this.lblWBBGainValue.TabIndex = 22;
			this.lblWBBGainValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarWBBGain
			// 
			this.trackBarWBBGain.AutoSize = false;
			this.trackBarWBBGain.LargeChange = 1000;
			this.trackBarWBBGain.Location = new System.Drawing.Point(96, 180);
			this.trackBarWBBGain.Name = "trackBarWBBGain";
			this.trackBarWBBGain.Size = new System.Drawing.Size(224, 30);
			this.trackBarWBBGain.TabIndex = 13;
			this.trackBarWBBGain.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarWBBGain.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblWBBGain
			// 
			this.lblWBBGain.AutoSize = true;
			this.lblWBBGain.Location = new System.Drawing.Point(8, 190);
			this.lblWBBGain.Name = "lblWBBGain";
			this.lblWBBGain.Size = new System.Drawing.Size(38, 12);
			this.lblWBBGain.TabIndex = 20;
			this.lblWBBGain.Text = "B Gain";
			// 
			// lblWBGbGainValue
			// 
			this.lblWBGbGainValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblWBGbGainValue.Location = new System.Drawing.Point(320, 140);
			this.lblWBGbGainValue.Name = "lblWBGbGainValue";
			this.lblWBGbGainValue.Size = new System.Drawing.Size(72, 30);
			this.lblWBGbGainValue.TabIndex = 18;
			this.lblWBGbGainValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarWBGbGain
			// 
			this.trackBarWBGbGain.AutoSize = false;
			this.trackBarWBGbGain.LargeChange = 1000;
			this.trackBarWBGbGain.Location = new System.Drawing.Point(96, 140);
			this.trackBarWBGbGain.Name = "trackBarWBGbGain";
			this.trackBarWBGbGain.Size = new System.Drawing.Size(224, 30);
			this.trackBarWBGbGain.TabIndex = 11;
			this.trackBarWBGbGain.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarWBGbGain.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblWBGbGain
			// 
			this.lblWBGbGain.AutoSize = true;
			this.lblWBGbGain.Location = new System.Drawing.Point(8, 150);
			this.lblWBGbGain.Name = "lblWBGbGain";
			this.lblWBGbGain.Size = new System.Drawing.Size(44, 12);
			this.lblWBGbGain.TabIndex = 16;
			this.lblWBGbGain.Text = "Gb Gain";
			// 
			// lblWBGrGainValue
			// 
			this.lblWBGrGainValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblWBGrGainValue.Location = new System.Drawing.Point(320, 100);
			this.lblWBGrGainValue.Name = "lblWBGrGainValue";
			this.lblWBGrGainValue.Size = new System.Drawing.Size(72, 30);
			this.lblWBGrGainValue.TabIndex = 14;
			this.lblWBGrGainValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarWBGrGain
			// 
			this.trackBarWBGrGain.AutoSize = false;
			this.trackBarWBGrGain.LargeChange = 1000;
			this.trackBarWBGrGain.Location = new System.Drawing.Point(96, 100);
			this.trackBarWBGrGain.Name = "trackBarWBGrGain";
			this.trackBarWBGrGain.Size = new System.Drawing.Size(224, 30);
			this.trackBarWBGrGain.TabIndex = 9;
			this.trackBarWBGrGain.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarWBGrGain.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblWBGrGain
			// 
			this.lblWBGrGain.AutoSize = true;
			this.lblWBGrGain.Location = new System.Drawing.Point(8, 110);
			this.lblWBGrGain.Name = "lblWBGrGain";
			this.lblWBGrGain.Size = new System.Drawing.Size(42, 12);
			this.lblWBGrGain.TabIndex = 12;
			this.lblWBGrGain.Text = "Gr Gain";
			// 
			// lblWBRGainValue
			// 
			this.lblWBRGainValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblWBRGainValue.Location = new System.Drawing.Point(320, 60);
			this.lblWBRGainValue.Name = "lblWBRGainValue";
			this.lblWBRGainValue.Size = new System.Drawing.Size(72, 30);
			this.lblWBRGainValue.TabIndex = 10;
			this.lblWBRGainValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarWBRGain
			// 
			this.trackBarWBRGain.AutoSize = false;
			this.trackBarWBRGain.LargeChange = 1000;
			this.trackBarWBRGain.Location = new System.Drawing.Point(96, 60);
			this.trackBarWBRGain.Name = "trackBarWBRGain";
			this.trackBarWBRGain.Size = new System.Drawing.Size(224, 30);
			this.trackBarWBRGain.TabIndex = 7;
			this.trackBarWBRGain.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarWBRGain.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblWBRGain
			// 
			this.lblWBRGain.AutoSize = true;
			this.lblWBRGain.Location = new System.Drawing.Point(8, 70);
			this.lblWBRGain.Name = "lblWBRGain";
			this.lblWBRGain.Size = new System.Drawing.Size(38, 12);
			this.lblWBRGain.TabIndex = 8;
			this.lblWBRGain.Text = "R Gain";
			// 
			// lblWBMode
			// 
			this.lblWBMode.AutoSize = true;
			this.lblWBMode.Location = new System.Drawing.Point(8, 20);
			this.lblWBMode.Name = "lblWBMode";
			this.lblWBMode.Size = new System.Drawing.Size(54, 12);
			this.lblWBMode.TabIndex = 5;
			this.lblWBMode.Text = "WB Mode";
			// 
			// tabPageY
			// 
			this.tabPageY.Controls.Add(this.trackBarAnalogBlackLevel);
			this.tabPageY.Controls.Add(this.label37);
			this.tabPageY.Controls.Add(this.txtAnalogBlackLevel);
			this.tabPageY.Controls.Add(this.label35);
			this.tabPageY.Controls.Add(this.cmbShadingCorrectionMode);
			this.tabPageY.Controls.Add(this.trackBarShadingCorrectionTarget);
			this.tabPageY.Controls.Add(this.label34);
			this.tabPageY.Controls.Add(this.txtShadingCorrectionTarget);
			this.tabPageY.Controls.Add(this.trackBarDigitalClamp);
			this.tabPageY.Controls.Add(this.label30);
			this.tabPageY.Controls.Add(this.stTextBoxDigitalClamp);
			this.tabPageY.Controls.Add(this.trackBarCameraGamma);
			this.tabPageY.Controls.Add(this.label29);
			this.tabPageY.Controls.Add(this.txtCameraGamma);
			this.tabPageY.Controls.Add(this.trackBarYGamma);
			this.tabPageY.Controls.Add(this.label3);
			this.tabPageY.Controls.Add(this.label1);
			this.tabPageY.Controls.Add(this.trackBarSharpnessCoring);
			this.tabPageY.Controls.Add(this.lblSharpnessCoring);
			this.tabPageY.Controls.Add(this.trackBarSharpnessGain);
			this.tabPageY.Controls.Add(this.lblSharpnessGain);
			this.tabPageY.Controls.Add(this.lblSharpnessMode);
			this.tabPageY.Controls.Add(this.txtYGamma);
			this.tabPageY.Controls.Add(this.cmbYGammaMode);
			this.tabPageY.Controls.Add(this.cmbSharpnessMode);
			this.tabPageY.Controls.Add(this.txtSharpnessCoring);
			this.tabPageY.Controls.Add(this.txtSharpnessGain);
			this.tabPageY.Location = new System.Drawing.Point(4, 22);
			this.tabPageY.Name = "tabPageY";
			this.tabPageY.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageY.Size = new System.Drawing.Size(472, 519);
			this.tabPageY.TabIndex = 9;
			this.tabPageY.Text = "Y";
			this.tabPageY.UseVisualStyleBackColor = true;
			// 
			// trackBarAnalogBlackLevel
			// 
			this.trackBarAnalogBlackLevel.AutoSize = false;
			this.trackBarAnalogBlackLevel.LargeChange = 1000;
			this.trackBarAnalogBlackLevel.Location = new System.Drawing.Point(108, 288);
			this.trackBarAnalogBlackLevel.Name = "trackBarAnalogBlackLevel";
			this.trackBarAnalogBlackLevel.Size = new System.Drawing.Size(224, 30);
			this.trackBarAnalogBlackLevel.TabIndex = 25;
			this.trackBarAnalogBlackLevel.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarAnalogBlackLevel.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label37
			// 
			this.label37.AutoSize = true;
			this.label37.Location = new System.Drawing.Point(4, 298);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(98, 12);
			this.label37.TabIndex = 24;
			this.label37.Text = "Analog Black Level";
			// 
			// txtAnalogBlackLevel
			// 
			this.txtAnalogBlackLevel.Location = new System.Drawing.Point(340, 288);
			this.txtAnalogBlackLevel.Name = "txtAnalogBlackLevel";
			this.txtAnalogBlackLevel.Size = new System.Drawing.Size(64, 22);
			this.txtAnalogBlackLevel.TabIndex = 26;
			this.txtAnalogBlackLevel.Text = "stTextBox18";
			this.txtAnalogBlackLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtAnalogBlackLevel.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// label35
			// 
			this.label35.AutoSize = true;
			this.label35.Location = new System.Drawing.Point(4, 411);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(126, 12);
			this.label35.TabIndex = 22;
			this.label35.Text = "Shading Correction Mode";
			// 
			// cmbShadingCorrectionMode
			// 
			this.cmbShadingCorrectionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbShadingCorrectionMode.Location = new System.Drawing.Point(116, 441);
			this.cmbShadingCorrectionMode.Name = "cmbShadingCorrectionMode";
			this.cmbShadingCorrectionMode.Size = new System.Drawing.Size(224, 20);
			this.cmbShadingCorrectionMode.StValue = ((long)(0));
			this.cmbShadingCorrectionMode.TabIndex = 23;
			this.cmbShadingCorrectionMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// trackBarShadingCorrectionTarget
			// 
			this.trackBarShadingCorrectionTarget.AutoSize = false;
			this.trackBarShadingCorrectionTarget.LargeChange = 1000;
			this.trackBarShadingCorrectionTarget.Location = new System.Drawing.Point(108, 374);
			this.trackBarShadingCorrectionTarget.Name = "trackBarShadingCorrectionTarget";
			this.trackBarShadingCorrectionTarget.Size = new System.Drawing.Size(224, 30);
			this.trackBarShadingCorrectionTarget.TabIndex = 20;
			this.trackBarShadingCorrectionTarget.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarShadingCorrectionTarget.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label34
			// 
			this.label34.AutoSize = true;
			this.label34.Location = new System.Drawing.Point(4, 355);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(129, 12);
			this.label34.TabIndex = 19;
			this.label34.Text = "Shading Correction Target";
			// 
			// txtShadingCorrectionTarget
			// 
			this.txtShadingCorrectionTarget.Location = new System.Drawing.Point(340, 374);
			this.txtShadingCorrectionTarget.Name = "txtShadingCorrectionTarget";
			this.txtShadingCorrectionTarget.Size = new System.Drawing.Size(64, 22);
			this.txtShadingCorrectionTarget.TabIndex = 21;
			this.txtShadingCorrectionTarget.Text = "stTextBox18";
			this.txtShadingCorrectionTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtShadingCorrectionTarget.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// trackBarDigitalClamp
			// 
			this.trackBarDigitalClamp.AutoSize = false;
			this.trackBarDigitalClamp.LargeChange = 1000;
			this.trackBarDigitalClamp.Location = new System.Drawing.Point(108, 250);
			this.trackBarDigitalClamp.Name = "trackBarDigitalClamp";
			this.trackBarDigitalClamp.Size = new System.Drawing.Size(224, 30);
			this.trackBarDigitalClamp.TabIndex = 17;
			this.trackBarDigitalClamp.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarDigitalClamp.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(4, 260);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(70, 12);
			this.label30.TabIndex = 16;
			this.label30.Text = "Digital Clamp";
			// 
			// stTextBoxDigitalClamp
			// 
			this.stTextBoxDigitalClamp.Location = new System.Drawing.Point(340, 250);
			this.stTextBoxDigitalClamp.Name = "stTextBoxDigitalClamp";
			this.stTextBoxDigitalClamp.Size = new System.Drawing.Size(64, 22);
			this.stTextBoxDigitalClamp.TabIndex = 18;
			this.stTextBoxDigitalClamp.Text = "stTextBox18";
			this.stTextBoxDigitalClamp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.stTextBoxDigitalClamp.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// trackBarCameraGamma
			// 
			this.trackBarCameraGamma.AutoSize = false;
			this.trackBarCameraGamma.LargeChange = 1000;
			this.trackBarCameraGamma.Location = new System.Drawing.Point(108, 82);
			this.trackBarCameraGamma.Name = "trackBarCameraGamma";
			this.trackBarCameraGamma.Size = new System.Drawing.Size(224, 30);
			this.trackBarCameraGamma.TabIndex = 6;
			this.trackBarCameraGamma.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarCameraGamma.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label29
			// 
			this.label29.AutoSize = true;
			this.label29.Location = new System.Drawing.Point(4, 92);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(85, 12);
			this.label29.TabIndex = 5;
			this.label29.Text = "Gamma[Camera]";
			// 
			// txtCameraGamma
			// 
			this.txtCameraGamma.Location = new System.Drawing.Point(340, 82);
			this.txtCameraGamma.Name = "txtCameraGamma";
			this.txtCameraGamma.Size = new System.Drawing.Size(64, 22);
			this.txtCameraGamma.TabIndex = 7;
			this.txtCameraGamma.Text = "stTextBox17";
			this.txtCameraGamma.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtCameraGamma.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// trackBarYGamma
			// 
			this.trackBarYGamma.AutoSize = false;
			this.trackBarYGamma.LargeChange = 1000;
			this.trackBarYGamma.Location = new System.Drawing.Point(108, 45);
			this.trackBarYGamma.Name = "trackBarYGamma";
			this.trackBarYGamma.Size = new System.Drawing.Size(224, 30);
			this.trackBarYGamma.TabIndex = 3;
			this.trackBarYGamma.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarYGamma.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 55);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "Gamma";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "Gamma Mode";
			// 
			// trackBarSharpnessCoring
			// 
			this.trackBarSharpnessCoring.AutoSize = false;
			this.trackBarSharpnessCoring.LargeChange = 1000;
			this.trackBarSharpnessCoring.Location = new System.Drawing.Point(108, 200);
			this.trackBarSharpnessCoring.Name = "trackBarSharpnessCoring";
			this.trackBarSharpnessCoring.Size = new System.Drawing.Size(224, 30);
			this.trackBarSharpnessCoring.TabIndex = 14;
			this.trackBarSharpnessCoring.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarSharpnessCoring.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblSharpnessCoring
			// 
			this.lblSharpnessCoring.AutoSize = true;
			this.lblSharpnessCoring.Location = new System.Drawing.Point(4, 210);
			this.lblSharpnessCoring.Name = "lblSharpnessCoring";
			this.lblSharpnessCoring.Size = new System.Drawing.Size(87, 12);
			this.lblSharpnessCoring.TabIndex = 13;
			this.lblSharpnessCoring.Text = "Sharpness Coring";
			// 
			// trackBarSharpnessGain
			// 
			this.trackBarSharpnessGain.AutoSize = false;
			this.trackBarSharpnessGain.LargeChange = 1000;
			this.trackBarSharpnessGain.Location = new System.Drawing.Point(108, 160);
			this.trackBarSharpnessGain.Name = "trackBarSharpnessGain";
			this.trackBarSharpnessGain.Size = new System.Drawing.Size(224, 30);
			this.trackBarSharpnessGain.TabIndex = 11;
			this.trackBarSharpnessGain.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarSharpnessGain.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblSharpnessGain
			// 
			this.lblSharpnessGain.AutoSize = true;
			this.lblSharpnessGain.Location = new System.Drawing.Point(4, 170);
			this.lblSharpnessGain.Name = "lblSharpnessGain";
			this.lblSharpnessGain.Size = new System.Drawing.Size(76, 12);
			this.lblSharpnessGain.TabIndex = 10;
			this.lblSharpnessGain.Text = "Sharpness Gain";
			// 
			// lblSharpnessMode
			// 
			this.lblSharpnessMode.AutoSize = true;
			this.lblSharpnessMode.Location = new System.Drawing.Point(4, 120);
			this.lblSharpnessMode.Name = "lblSharpnessMode";
			this.lblSharpnessMode.Size = new System.Drawing.Size(81, 12);
			this.lblSharpnessMode.TabIndex = 8;
			this.lblSharpnessMode.Text = "Sharpness Mode";
			// 
			// txtYGamma
			// 
			this.txtYGamma.Location = new System.Drawing.Point(340, 45);
			this.txtYGamma.Name = "txtYGamma";
			this.txtYGamma.Size = new System.Drawing.Size(64, 22);
			this.txtYGamma.TabIndex = 4;
			this.txtYGamma.Text = "stTextBox17";
			this.txtYGamma.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtYGamma.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// cmbYGammaMode
			// 
			this.cmbYGammaMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbYGammaMode.Location = new System.Drawing.Point(116, 8);
			this.cmbYGammaMode.Name = "cmbYGammaMode";
			this.cmbYGammaMode.Size = new System.Drawing.Size(224, 20);
			this.cmbYGammaMode.StValue = ((long)(0));
			this.cmbYGammaMode.TabIndex = 1;
			this.cmbYGammaMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbSharpnessMode
			// 
			this.cmbSharpnessMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSharpnessMode.Location = new System.Drawing.Point(116, 120);
			this.cmbSharpnessMode.Name = "cmbSharpnessMode";
			this.cmbSharpnessMode.Size = new System.Drawing.Size(224, 20);
			this.cmbSharpnessMode.StValue = ((long)(0));
			this.cmbSharpnessMode.TabIndex = 9;
			this.cmbSharpnessMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// txtSharpnessCoring
			// 
			this.txtSharpnessCoring.Location = new System.Drawing.Point(340, 200);
			this.txtSharpnessCoring.Name = "txtSharpnessCoring";
			this.txtSharpnessCoring.Size = new System.Drawing.Size(64, 22);
			this.txtSharpnessCoring.TabIndex = 15;
			this.txtSharpnessCoring.Text = "stTextBox18";
			this.txtSharpnessCoring.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSharpnessCoring.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtSharpnessGain
			// 
			this.txtSharpnessGain.Location = new System.Drawing.Point(340, 160);
			this.txtSharpnessGain.Name = "txtSharpnessGain";
			this.txtSharpnessGain.Size = new System.Drawing.Size(64, 22);
			this.txtSharpnessGain.TabIndex = 12;
			this.txtSharpnessGain.Text = "stTextBox17";
			this.txtSharpnessGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtSharpnessGain.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// tabPageHDR_CMOSIS4M
			// 
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.trackBarHDR_CMOSIS4M_Vlow3);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.label42);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.txtHDR_CMOSIS4M_Vlow3);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.trackBarHDR_CMOSIS4M_Knee2);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.label43);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.txtHDR_CMOSIS4M_Knee2);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.trackBarHDR_CMOSIS4M_Vlow2);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.label41);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.txtHDR_CMOSIS4M_Vlow2);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.trackBarHDR_CMOSIS4M_Knee1);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.label40);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.txtHDR_CMOSIS4M_Knee1);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.label39);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.cmbHDR_CMOSIS4M_SlopeNum);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.label38);
			this.tabPageHDR_CMOSIS4M.Controls.Add(this.cmbHDR_CMOSIS4M_Mode);
			this.tabPageHDR_CMOSIS4M.Location = new System.Drawing.Point(4, 22);
			this.tabPageHDR_CMOSIS4M.Name = "tabPageHDR_CMOSIS4M";
			this.tabPageHDR_CMOSIS4M.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageHDR_CMOSIS4M.Size = new System.Drawing.Size(472, 519);
			this.tabPageHDR_CMOSIS4M.TabIndex = 10;
			this.tabPageHDR_CMOSIS4M.Text = "HDR";
			this.tabPageHDR_CMOSIS4M.UseVisualStyleBackColor = true;
			// 
			// trackBarHDR_CMOSIS4M_Vlow3
			// 
			this.trackBarHDR_CMOSIS4M_Vlow3.AutoSize = false;
			this.trackBarHDR_CMOSIS4M_Vlow3.LargeChange = 1000;
			this.trackBarHDR_CMOSIS4M_Vlow3.Location = new System.Drawing.Point(121, 194);
			this.trackBarHDR_CMOSIS4M_Vlow3.Name = "trackBarHDR_CMOSIS4M_Vlow3";
			this.trackBarHDR_CMOSIS4M_Vlow3.Size = new System.Drawing.Size(224, 30);
			this.trackBarHDR_CMOSIS4M_Vlow3.TabIndex = 16;
			this.trackBarHDR_CMOSIS4M_Vlow3.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarHDR_CMOSIS4M_Vlow3.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label42
			// 
			this.label42.AutoSize = true;
			this.label42.Location = new System.Drawing.Point(8, 198);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(36, 12);
			this.label42.TabIndex = 15;
			this.label42.Text = "Vlow3";
			// 
			// txtHDR_CMOSIS4M_Vlow3
			// 
			this.txtHDR_CMOSIS4M_Vlow3.Location = new System.Drawing.Point(353, 194);
			this.txtHDR_CMOSIS4M_Vlow3.Name = "txtHDR_CMOSIS4M_Vlow3";
			this.txtHDR_CMOSIS4M_Vlow3.Size = new System.Drawing.Size(64, 22);
			this.txtHDR_CMOSIS4M_Vlow3.TabIndex = 17;
			this.txtHDR_CMOSIS4M_Vlow3.Text = "stTextBox17";
			this.txtHDR_CMOSIS4M_Vlow3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtHDR_CMOSIS4M_Vlow3.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// trackBarHDR_CMOSIS4M_Knee2
			// 
			this.trackBarHDR_CMOSIS4M_Knee2.AutoSize = false;
			this.trackBarHDR_CMOSIS4M_Knee2.LargeChange = 1000;
			this.trackBarHDR_CMOSIS4M_Knee2.Location = new System.Drawing.Point(121, 156);
			this.trackBarHDR_CMOSIS4M_Knee2.Name = "trackBarHDR_CMOSIS4M_Knee2";
			this.trackBarHDR_CMOSIS4M_Knee2.Size = new System.Drawing.Size(224, 30);
			this.trackBarHDR_CMOSIS4M_Knee2.TabIndex = 13;
			this.trackBarHDR_CMOSIS4M_Knee2.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarHDR_CMOSIS4M_Knee2.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label43
			// 
			this.label43.AutoSize = true;
			this.label43.Location = new System.Drawing.Point(8, 160);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(35, 12);
			this.label43.TabIndex = 12;
			this.label43.Text = "Knee2";
			// 
			// txtHDR_CMOSIS4M_Knee2
			// 
			this.txtHDR_CMOSIS4M_Knee2.Location = new System.Drawing.Point(353, 156);
			this.txtHDR_CMOSIS4M_Knee2.Name = "txtHDR_CMOSIS4M_Knee2";
			this.txtHDR_CMOSIS4M_Knee2.Size = new System.Drawing.Size(64, 22);
			this.txtHDR_CMOSIS4M_Knee2.TabIndex = 14;
			this.txtHDR_CMOSIS4M_Knee2.Text = "stTextBox17";
			this.txtHDR_CMOSIS4M_Knee2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtHDR_CMOSIS4M_Knee2.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// trackBarHDR_CMOSIS4M_Vlow2
			// 
			this.trackBarHDR_CMOSIS4M_Vlow2.AutoSize = false;
			this.trackBarHDR_CMOSIS4M_Vlow2.LargeChange = 1000;
			this.trackBarHDR_CMOSIS4M_Vlow2.Location = new System.Drawing.Point(121, 119);
			this.trackBarHDR_CMOSIS4M_Vlow2.Name = "trackBarHDR_CMOSIS4M_Vlow2";
			this.trackBarHDR_CMOSIS4M_Vlow2.Size = new System.Drawing.Size(224, 30);
			this.trackBarHDR_CMOSIS4M_Vlow2.TabIndex = 10;
			this.trackBarHDR_CMOSIS4M_Vlow2.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarHDR_CMOSIS4M_Vlow2.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label41
			// 
			this.label41.AutoSize = true;
			this.label41.Location = new System.Drawing.Point(8, 122);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(36, 12);
			this.label41.TabIndex = 9;
			this.label41.Text = "Vlow2";
			// 
			// txtHDR_CMOSIS4M_Vlow2
			// 
			this.txtHDR_CMOSIS4M_Vlow2.Location = new System.Drawing.Point(353, 119);
			this.txtHDR_CMOSIS4M_Vlow2.Name = "txtHDR_CMOSIS4M_Vlow2";
			this.txtHDR_CMOSIS4M_Vlow2.Size = new System.Drawing.Size(64, 22);
			this.txtHDR_CMOSIS4M_Vlow2.TabIndex = 11;
			this.txtHDR_CMOSIS4M_Vlow2.Text = "stTextBox17";
			this.txtHDR_CMOSIS4M_Vlow2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtHDR_CMOSIS4M_Vlow2.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// trackBarHDR_CMOSIS4M_Knee1
			// 
			this.trackBarHDR_CMOSIS4M_Knee1.AutoSize = false;
			this.trackBarHDR_CMOSIS4M_Knee1.LargeChange = 1000;
			this.trackBarHDR_CMOSIS4M_Knee1.Location = new System.Drawing.Point(121, 81);
			this.trackBarHDR_CMOSIS4M_Knee1.Name = "trackBarHDR_CMOSIS4M_Knee1";
			this.trackBarHDR_CMOSIS4M_Knee1.Size = new System.Drawing.Size(224, 30);
			this.trackBarHDR_CMOSIS4M_Knee1.TabIndex = 7;
			this.trackBarHDR_CMOSIS4M_Knee1.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarHDR_CMOSIS4M_Knee1.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label40
			// 
			this.label40.AutoSize = true;
			this.label40.Location = new System.Drawing.Point(8, 85);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(35, 12);
			this.label40.TabIndex = 6;
			this.label40.Text = "Knee1";
			// 
			// txtHDR_CMOSIS4M_Knee1
			// 
			this.txtHDR_CMOSIS4M_Knee1.Location = new System.Drawing.Point(353, 81);
			this.txtHDR_CMOSIS4M_Knee1.Name = "txtHDR_CMOSIS4M_Knee1";
			this.txtHDR_CMOSIS4M_Knee1.Size = new System.Drawing.Size(64, 22);
			this.txtHDR_CMOSIS4M_Knee1.TabIndex = 8;
			this.txtHDR_CMOSIS4M_Knee1.Text = "stTextBox17";
			this.txtHDR_CMOSIS4M_Knee1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtHDR_CMOSIS4M_Knee1.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// label39
			// 
			this.label39.AutoSize = true;
			this.label39.Location = new System.Drawing.Point(8, 52);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(84, 12);
			this.label39.TabIndex = 4;
			this.label39.Text = "HDR Slope Num";
			// 
			// cmbHDR_CMOSIS4M_SlopeNum
			// 
			this.cmbHDR_CMOSIS4M_SlopeNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbHDR_CMOSIS4M_SlopeNum.Location = new System.Drawing.Point(121, 49);
			this.cmbHDR_CMOSIS4M_SlopeNum.Name = "cmbHDR_CMOSIS4M_SlopeNum";
			this.cmbHDR_CMOSIS4M_SlopeNum.Size = new System.Drawing.Size(224, 20);
			this.cmbHDR_CMOSIS4M_SlopeNum.StValue = ((long)(0));
			this.cmbHDR_CMOSIS4M_SlopeNum.TabIndex = 5;
			this.cmbHDR_CMOSIS4M_SlopeNum.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// label38
			// 
			this.label38.AutoSize = true;
			this.label38.Location = new System.Drawing.Point(8, 20);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(59, 12);
			this.label38.TabIndex = 2;
			this.label38.Text = "HDR Mode";
			// 
			// cmbHDR_CMOSIS4M_Mode
			// 
			this.cmbHDR_CMOSIS4M_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbHDR_CMOSIS4M_Mode.Location = new System.Drawing.Point(121, 16);
			this.cmbHDR_CMOSIS4M_Mode.Name = "cmbHDR_CMOSIS4M_Mode";
			this.cmbHDR_CMOSIS4M_Mode.Size = new System.Drawing.Size(224, 20);
			this.cmbHDR_CMOSIS4M_Mode.StValue = ((long)(0));
			this.cmbHDR_CMOSIS4M_Mode.TabIndex = 3;
			this.cmbHDR_CMOSIS4M_Mode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// tabPageColorGamma
			// 
			this.tabPageColorGamma.Controls.Add(this.txtBGamma);
			this.tabPageColorGamma.Controls.Add(this.trackBarBGamma);
			this.tabPageColorGamma.Controls.Add(this.label10);
			this.tabPageColorGamma.Controls.Add(this.label11);
			this.tabPageColorGamma.Controls.Add(this.trackBarGBGamma);
			this.tabPageColorGamma.Controls.Add(this.label8);
			this.tabPageColorGamma.Controls.Add(this.label9);
			this.tabPageColorGamma.Controls.Add(this.trackBarGRGamma);
			this.tabPageColorGamma.Controls.Add(this.label6);
			this.tabPageColorGamma.Controls.Add(this.label7);
			this.tabPageColorGamma.Controls.Add(this.trackBarRGamma);
			this.tabPageColorGamma.Controls.Add(this.label4);
			this.tabPageColorGamma.Controls.Add(this.label5);
			this.tabPageColorGamma.Controls.Add(this.cmbBGammaMode);
			this.tabPageColorGamma.Controls.Add(this.txtGBGamma);
			this.tabPageColorGamma.Controls.Add(this.cmbGBGammaMode);
			this.tabPageColorGamma.Controls.Add(this.txtGRGamma);
			this.tabPageColorGamma.Controls.Add(this.cmbGRGammaMode);
			this.tabPageColorGamma.Controls.Add(this.txtRGamma);
			this.tabPageColorGamma.Controls.Add(this.cmbRGammaMode);
			this.tabPageColorGamma.Location = new System.Drawing.Point(4, 22);
			this.tabPageColorGamma.Name = "tabPageColorGamma";
			this.tabPageColorGamma.Size = new System.Drawing.Size(472, 519);
			this.tabPageColorGamma.TabIndex = 6;
			this.tabPageColorGamma.Text = "Color Gamma";
			this.tabPageColorGamma.UseVisualStyleBackColor = true;
			// 
			// txtBGamma
			// 
			this.txtBGamma.Location = new System.Drawing.Point(344, 278);
			this.txtBGamma.Name = "txtBGamma";
			this.txtBGamma.Size = new System.Drawing.Size(64, 22);
			this.txtBGamma.TabIndex = 100;
			this.txtBGamma.Text = "stTextBox17";
			this.txtBGamma.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtBGamma.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// trackBarBGamma
			// 
			this.trackBarBGamma.AutoSize = false;
			this.trackBarBGamma.LargeChange = 1000;
			this.trackBarBGamma.Location = new System.Drawing.Point(112, 278);
			this.trackBarBGamma.Name = "trackBarBGamma";
			this.trackBarBGamma.Size = new System.Drawing.Size(224, 30);
			this.trackBarBGamma.TabIndex = 99;
			this.trackBarBGamma.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarBGamma.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(8, 288);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(52, 12);
			this.label10.TabIndex = 98;
			this.label10.Text = "B Gamma";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(8, 250);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(82, 12);
			this.label11.TabIndex = 96;
			this.label11.Text = "B Gamma Mode";
			// 
			// trackBarGBGamma
			// 
			this.trackBarGBGamma.AutoSize = false;
			this.trackBarGBGamma.LargeChange = 1000;
			this.trackBarGBGamma.Location = new System.Drawing.Point(112, 202);
			this.trackBarGBGamma.Name = "trackBarGBGamma";
			this.trackBarGBGamma.Size = new System.Drawing.Size(224, 30);
			this.trackBarGBGamma.TabIndex = 94;
			this.trackBarGBGamma.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarGBGamma.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(8, 212);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(60, 12);
			this.label8.TabIndex = 93;
			this.label8.Text = "GB Gamma";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(8, 175);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(90, 12);
			this.label9.TabIndex = 91;
			this.label9.Text = "GB Gamma Mode";
			// 
			// trackBarGRGamma
			// 
			this.trackBarGRGamma.AutoSize = false;
			this.trackBarGRGamma.LargeChange = 1000;
			this.trackBarGRGamma.Location = new System.Drawing.Point(112, 128);
			this.trackBarGRGamma.Name = "trackBarGRGamma";
			this.trackBarGRGamma.Size = new System.Drawing.Size(224, 30);
			this.trackBarGRGamma.TabIndex = 89;
			this.trackBarGRGamma.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarGRGamma.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 138);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(60, 12);
			this.label6.TabIndex = 88;
			this.label6.Text = "GR Gamma";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 100);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(90, 12);
			this.label7.TabIndex = 86;
			this.label7.Text = "GR Gamma Mode";
			// 
			// trackBarRGamma
			// 
			this.trackBarRGamma.AutoSize = false;
			this.trackBarRGamma.LargeChange = 1000;
			this.trackBarRGamma.Location = new System.Drawing.Point(112, 52);
			this.trackBarRGamma.Name = "trackBarRGamma";
			this.trackBarRGamma.Size = new System.Drawing.Size(224, 30);
			this.trackBarRGamma.TabIndex = 84;
			this.trackBarRGamma.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarRGamma.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 62);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(52, 12);
			this.label4.TabIndex = 83;
			this.label4.Text = "R Gamma";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 25);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(82, 12);
			this.label5.TabIndex = 81;
			this.label5.Text = "R Gamma Mode";
			// 
			// cmbBGammaMode
			// 
			this.cmbBGammaMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBGammaMode.Location = new System.Drawing.Point(120, 240);
			this.cmbBGammaMode.Name = "cmbBGammaMode";
			this.cmbBGammaMode.Size = new System.Drawing.Size(224, 20);
			this.cmbBGammaMode.StValue = ((long)(0));
			this.cmbBGammaMode.TabIndex = 97;
			this.cmbBGammaMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// txtGBGamma
			// 
			this.txtGBGamma.Location = new System.Drawing.Point(344, 202);
			this.txtGBGamma.Name = "txtGBGamma";
			this.txtGBGamma.Size = new System.Drawing.Size(64, 22);
			this.txtGBGamma.TabIndex = 95;
			this.txtGBGamma.Text = "stTextBox17";
			this.txtGBGamma.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtGBGamma.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// cmbGBGammaMode
			// 
			this.cmbGBGammaMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbGBGammaMode.Location = new System.Drawing.Point(120, 165);
			this.cmbGBGammaMode.Name = "cmbGBGammaMode";
			this.cmbGBGammaMode.Size = new System.Drawing.Size(224, 20);
			this.cmbGBGammaMode.StValue = ((long)(0));
			this.cmbGBGammaMode.TabIndex = 92;
			this.cmbGBGammaMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// txtGRGamma
			// 
			this.txtGRGamma.Location = new System.Drawing.Point(344, 128);
			this.txtGRGamma.Name = "txtGRGamma";
			this.txtGRGamma.Size = new System.Drawing.Size(64, 22);
			this.txtGRGamma.TabIndex = 90;
			this.txtGRGamma.Text = "stTextBox17";
			this.txtGRGamma.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtGRGamma.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// cmbGRGammaMode
			// 
			this.cmbGRGammaMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbGRGammaMode.Location = new System.Drawing.Point(120, 90);
			this.cmbGRGammaMode.Name = "cmbGRGammaMode";
			this.cmbGRGammaMode.Size = new System.Drawing.Size(224, 20);
			this.cmbGRGammaMode.StValue = ((long)(0));
			this.cmbGRGammaMode.TabIndex = 87;
			this.cmbGRGammaMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// txtRGamma
			// 
			this.txtRGamma.Location = new System.Drawing.Point(344, 52);
			this.txtRGamma.Name = "txtRGamma";
			this.txtRGamma.Size = new System.Drawing.Size(64, 22);
			this.txtRGamma.TabIndex = 85;
			this.txtRGamma.Text = "stTextBox17";
			this.txtRGamma.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtRGamma.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// cmbRGammaMode
			// 
			this.cmbRGammaMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbRGammaMode.Location = new System.Drawing.Point(120, 15);
			this.cmbRGammaMode.Name = "cmbRGammaMode";
			this.cmbRGammaMode.Size = new System.Drawing.Size(224, 20);
			this.cmbRGammaMode.StValue = ((long)(0));
			this.cmbRGammaMode.TabIndex = 82;
			this.cmbRGammaMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// tabPageTriggerMode
			// 
			this.tabPageTriggerMode.Controls.Add(this.txtTriggerDelay);
			this.tabPageTriggerMode.Controls.Add(this.lblTriggerDelay);
			this.tabPageTriggerMode.Controls.Add(this.label47);
			this.tabPageTriggerMode.Controls.Add(this.trackBarTriggerDelay);
			this.tabPageTriggerMode.Controls.Add(this.cmbTriggerSelector);
			this.tabPageTriggerMode.Controls.Add(this.label46);
			this.tabPageTriggerMode.Controls.Add(this.btnExposureStartTrigger);
			this.tabPageTriggerMode.Controls.Add(this.cmbSensorShutterMode);
			this.tabPageTriggerMode.Controls.Add(this.label19);
			this.tabPageTriggerMode.Controls.Add(this.lblTriggerOverlap);
			this.tabPageTriggerMode.Controls.Add(this.chkAutoTrigger);
			this.tabPageTriggerMode.Controls.Add(this.lblAutoTriggerTime);
			this.tabPageTriggerMode.Controls.Add(this.trackBarAutoTriggerTime);
			this.tabPageTriggerMode.Controls.Add(this.lblCameraMemory);
			this.tabPageTriggerMode.Controls.Add(this.btnExposureEndTrigger);
			this.tabPageTriggerMode.Controls.Add(this.btnResetFrameNo);
			this.tabPageTriggerMode.Controls.Add(this.btnSensorReadOutStartTrigger);
			this.tabPageTriggerMode.Controls.Add(this.btnFrameStartTrigger);
			this.tabPageTriggerMode.Controls.Add(this.lblExposureEnd);
			this.tabPageTriggerMode.Controls.Add(this.lblExposureWaitReadOut);
			this.tabPageTriggerMode.Controls.Add(this.lblExposureWaitHD);
			this.tabPageTriggerMode.Controls.Add(this.lblNoiseReduction);
			this.tabPageTriggerMode.Controls.Add(this.lblExposureMode);
			this.tabPageTriggerMode.Controls.Add(this.lblTriggerSource);
			this.tabPageTriggerMode.Controls.Add(this.lblTriggerMode);
			this.tabPageTriggerMode.Controls.Add(this.cmbTriggerOverlap);
			this.tabPageTriggerMode.Controls.Add(this.cmbCameraMemory);
			this.tabPageTriggerMode.Controls.Add(this.cmbExposureEnd);
			this.tabPageTriggerMode.Controls.Add(this.cmbExposureWaitReadOut);
			this.tabPageTriggerMode.Controls.Add(this.cmbExposureWaitHD);
			this.tabPageTriggerMode.Controls.Add(this.cmbNoiseReduction);
			this.tabPageTriggerMode.Controls.Add(this.cmbExposureMode);
			this.tabPageTriggerMode.Controls.Add(this.cmbTriggerSource);
			this.tabPageTriggerMode.Controls.Add(this.cmbTriggerMode);
			this.tabPageTriggerMode.Controls.Add(this.txtAutoTriggerTime);
			this.tabPageTriggerMode.Location = new System.Drawing.Point(4, 22);
			this.tabPageTriggerMode.Name = "tabPageTriggerMode";
			this.tabPageTriggerMode.Size = new System.Drawing.Size(472, 519);
			this.tabPageTriggerMode.TabIndex = 2;
			this.tabPageTriggerMode.Text = "Trigger Mode";
			this.tabPageTriggerMode.UseVisualStyleBackColor = true;
			// 
			// txtTriggerDelay
			// 
			this.txtTriggerDelay.Location = new System.Drawing.Point(384, 184);
			this.txtTriggerDelay.Name = "txtTriggerDelay";
			this.txtTriggerDelay.Size = new System.Drawing.Size(72, 22);
			this.txtTriggerDelay.TabIndex = 11;
			this.txtTriggerDelay.Text = "stTextBox4";
			this.txtTriggerDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtTriggerDelay.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// lblTriggerDelay
			// 
			this.lblTriggerDelay.Location = new System.Drawing.Point(356, 152);
			this.lblTriggerDelay.Name = "lblTriggerDelay";
			this.lblTriggerDelay.Size = new System.Drawing.Size(100, 29);
			this.lblTriggerDelay.TabIndex = 9;
			this.lblTriggerDelay.Text = "0";
			this.lblTriggerDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label47
			// 
			this.label47.AutoSize = true;
			this.label47.Location = new System.Drawing.Point(102, 159);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(70, 12);
			this.label47.TabIndex = 8;
			this.label47.Text = "Trigger Delay";
			// 
			// trackBarTriggerDelay
			// 
			this.trackBarTriggerDelay.AutoSize = false;
			this.trackBarTriggerDelay.LargeChange = 1000;
			this.trackBarTriggerDelay.Location = new System.Drawing.Point(198, 184);
			this.trackBarTriggerDelay.Name = "trackBarTriggerDelay";
			this.trackBarTriggerDelay.Size = new System.Drawing.Size(180, 30);
			this.trackBarTriggerDelay.TabIndex = 10;
			this.trackBarTriggerDelay.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarTriggerDelay.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// cmbTriggerSelector
			// 
			this.cmbTriggerSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTriggerSelector.Location = new System.Drawing.Point(104, 49);
			this.cmbTriggerSelector.Name = "cmbTriggerSelector";
			this.cmbTriggerSelector.Size = new System.Drawing.Size(224, 20);
			this.cmbTriggerSelector.StValue = ((long)(0));
			this.cmbTriggerSelector.TabIndex = 3;
			this.cmbTriggerSelector.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// label46
			// 
			this.label46.AutoSize = true;
			this.label46.Location = new System.Drawing.Point(8, 52);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(80, 12);
			this.label46.TabIndex = 2;
			this.label46.Text = "Trigger Selector";
			// 
			// btnExposureStartTrigger
			// 
			this.btnExposureStartTrigger.Location = new System.Drawing.Point(246, 255);
			this.btnExposureStartTrigger.Name = "btnExposureStartTrigger";
			this.btnExposureStartTrigger.Size = new System.Drawing.Size(136, 29);
			this.btnExposureStartTrigger.TabIndex = 15;
			this.btnExposureStartTrigger.Tag = "2";
			this.btnExposureStartTrigger.Text = "ExposureStart Trigger";
			this.btnExposureStartTrigger.UseVisualStyleBackColor = true;
			this.btnExposureStartTrigger.Click += new System.EventHandler(this.btnTrigger_Click);
			// 
			// cmbSensorShutterMode
			// 
			this.cmbSensorShutterMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSensorShutterMode.Location = new System.Drawing.Point(102, 590);
			this.cmbSensorShutterMode.Name = "cmbSensorShutterMode";
			this.cmbSensorShutterMode.Size = new System.Drawing.Size(224, 20);
			this.cmbSensorShutterMode.StValue = ((long)(0));
			this.cmbSensorShutterMode.TabIndex = 33;
			this.cmbSensorShutterMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(8, 571);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(102, 12);
			this.label19.TabIndex = 32;
			this.label19.Text = "Sensor Shutter Mode";
			// 
			// lblTriggerOverlap
			// 
			this.lblTriggerOverlap.AutoSize = true;
			this.lblTriggerOverlap.Location = new System.Drawing.Point(102, 225);
			this.lblTriggerOverlap.Name = "lblTriggerOverlap";
			this.lblTriggerOverlap.Size = new System.Drawing.Size(80, 12);
			this.lblTriggerOverlap.TabIndex = 12;
			this.lblTriggerOverlap.Text = "Trigger Overlap";
			// 
			// chkAutoTrigger
			// 
			this.chkAutoTrigger.Location = new System.Drawing.Point(104, 329);
			this.chkAutoTrigger.Name = "chkAutoTrigger";
			this.chkAutoTrigger.Size = new System.Drawing.Size(120, 30);
			this.chkAutoTrigger.TabIndex = 18;
			this.chkAutoTrigger.Text = "Auto ExposureEnd Trigger";
			this.chkAutoTrigger.CheckedChanged += new System.EventHandler(this.chkAutoTrigger_CheckedChanged);
			// 
			// lblAutoTriggerTime
			// 
			this.lblAutoTriggerTime.Location = new System.Drawing.Point(256, 329);
			this.lblAutoTriggerTime.Name = "lblAutoTriggerTime";
			this.lblAutoTriggerTime.Size = new System.Drawing.Size(120, 30);
			this.lblAutoTriggerTime.TabIndex = 19;
			this.lblAutoTriggerTime.Text = "s";
			this.lblAutoTriggerTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarAutoTriggerTime
			// 
			this.trackBarAutoTriggerTime.AutoSize = false;
			this.trackBarAutoTriggerTime.LargeChange = 1000;
			this.trackBarAutoTriggerTime.Location = new System.Drawing.Point(104, 359);
			this.trackBarAutoTriggerTime.Name = "trackBarAutoTriggerTime";
			this.trackBarAutoTriggerTime.Size = new System.Drawing.Size(280, 30);
			this.trackBarAutoTriggerTime.TabIndex = 20;
			this.trackBarAutoTriggerTime.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarAutoTriggerTime.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblCameraMemory
			// 
			this.lblCameraMemory.AutoSize = true;
			this.lblCameraMemory.Location = new System.Drawing.Point(8, 546);
			this.lblCameraMemory.Name = "lblCameraMemory";
			this.lblCameraMemory.Size = new System.Drawing.Size(84, 12);
			this.lblCameraMemory.TabIndex = 30;
			this.lblCameraMemory.Text = "Camera Memory";
			// 
			// btnExposureEndTrigger
			// 
			this.btnExposureEndTrigger.Location = new System.Drawing.Point(104, 291);
			this.btnExposureEndTrigger.Name = "btnExposureEndTrigger";
			this.btnExposureEndTrigger.Size = new System.Drawing.Size(136, 30);
			this.btnExposureEndTrigger.TabIndex = 16;
			this.btnExposureEndTrigger.Tag = "3";
			this.btnExposureEndTrigger.Text = "ExposureEnd Trigger";
			this.btnExposureEndTrigger.Click += new System.EventHandler(this.btnTrigger_Click);
			// 
			// btnResetFrameNo
			// 
			this.btnResetFrameNo.Location = new System.Drawing.Point(102, 622);
			this.btnResetFrameNo.Name = "btnResetFrameNo";
			this.btnResetFrameNo.Size = new System.Drawing.Size(104, 30);
			this.btnResetFrameNo.TabIndex = 34;
			this.btnResetFrameNo.Text = "Reset Frame No";
			this.btnResetFrameNo.Click += new System.EventHandler(this.btnResetFrameNo_Click);
			// 
			// btnSensorReadOutStartTrigger
			// 
			this.btnSensorReadOutStartTrigger.Location = new System.Drawing.Point(246, 291);
			this.btnSensorReadOutStartTrigger.Name = "btnSensorReadOutStartTrigger";
			this.btnSensorReadOutStartTrigger.Size = new System.Drawing.Size(136, 30);
			this.btnSensorReadOutStartTrigger.TabIndex = 17;
			this.btnSensorReadOutStartTrigger.Tag = "4";
			this.btnSensorReadOutStartTrigger.Text = "SensorReadOutStart Trigger";
			this.btnSensorReadOutStartTrigger.Click += new System.EventHandler(this.btnTrigger_Click);
			// 
			// btnFrameStartTrigger
			// 
			this.btnFrameStartTrigger.Location = new System.Drawing.Point(104, 254);
			this.btnFrameStartTrigger.Name = "btnFrameStartTrigger";
			this.btnFrameStartTrigger.Size = new System.Drawing.Size(136, 30);
			this.btnFrameStartTrigger.TabIndex = 14;
			this.btnFrameStartTrigger.Tag = "0";
			this.btnFrameStartTrigger.Text = "FrameStart Trigger";
			this.btnFrameStartTrigger.Click += new System.EventHandler(this.btnTrigger_Click);
			// 
			// lblExposureEnd
			// 
			this.lblExposureEnd.AutoSize = true;
			this.lblExposureEnd.Location = new System.Drawing.Point(8, 491);
			this.lblExposureEnd.Name = "lblExposureEnd";
			this.lblExposureEnd.Size = new System.Drawing.Size(101, 12);
			this.lblExposureEnd.TabIndex = 28;
			this.lblExposureEnd.Text = "Exposure End Event";
			// 
			// lblExposureWaitReadOut
			// 
			this.lblExposureWaitReadOut.AutoSize = true;
			this.lblExposureWaitReadOut.Location = new System.Drawing.Point(8, 466);
			this.lblExposureWaitReadOut.Name = "lblExposureWaitReadOut";
			this.lblExposureWaitReadOut.Size = new System.Drawing.Size(74, 12);
			this.lblExposureWaitReadOut.TabIndex = 26;
			this.lblExposureWaitReadOut.Text = "Wait Read Out";
			// 
			// lblExposureWaitHD
			// 
			this.lblExposureWaitHD.AutoSize = true;
			this.lblExposureWaitHD.Location = new System.Drawing.Point(8, 436);
			this.lblExposureWaitHD.Name = "lblExposureWaitHD";
			this.lblExposureWaitHD.Size = new System.Drawing.Size(46, 12);
			this.lblExposureWaitHD.TabIndex = 24;
			this.lblExposureWaitHD.Text = "Wait HD";
			// 
			// lblNoiseReduction
			// 
			this.lblNoiseReduction.AutoSize = true;
			this.lblNoiseReduction.Location = new System.Drawing.Point(8, 404);
			this.lblNoiseReduction.Name = "lblNoiseReduction";
			this.lblNoiseReduction.Size = new System.Drawing.Size(82, 12);
			this.lblNoiseReduction.TabIndex = 22;
			this.lblNoiseReduction.Text = "Noise Reduction";
			// 
			// lblExposureMode
			// 
			this.lblExposureMode.AutoSize = true;
			this.lblExposureMode.Location = new System.Drawing.Point(8, 20);
			this.lblExposureMode.Name = "lblExposureMode";
			this.lblExposureMode.Size = new System.Drawing.Size(79, 12);
			this.lblExposureMode.TabIndex = 0;
			this.lblExposureMode.Text = "Exposure Mode";
			// 
			// lblTriggerSource
			// 
			this.lblTriggerSource.AutoSize = true;
			this.lblTriggerSource.Location = new System.Drawing.Point(102, 122);
			this.lblTriggerSource.Name = "lblTriggerSource";
			this.lblTriggerSource.Size = new System.Drawing.Size(75, 12);
			this.lblTriggerSource.TabIndex = 6;
			this.lblTriggerSource.Text = "Trigger Source";
			// 
			// lblTriggerMode
			// 
			this.lblTriggerMode.AutoSize = true;
			this.lblTriggerMode.Location = new System.Drawing.Point(102, 90);
			this.lblTriggerMode.Name = "lblTriggerMode";
			this.lblTriggerMode.Size = new System.Drawing.Size(70, 12);
			this.lblTriggerMode.TabIndex = 4;
			this.lblTriggerMode.Text = "Trigger Mode";
			// 
			// cmbTriggerOverlap
			// 
			this.cmbTriggerOverlap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTriggerOverlap.Location = new System.Drawing.Point(198, 221);
			this.cmbTriggerOverlap.Name = "cmbTriggerOverlap";
			this.cmbTriggerOverlap.Size = new System.Drawing.Size(224, 20);
			this.cmbTriggerOverlap.StValue = ((long)(0));
			this.cmbTriggerOverlap.TabIndex = 13;
			this.cmbTriggerOverlap.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbCameraMemory
			// 
			this.cmbCameraMemory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCameraMemory.Location = new System.Drawing.Point(102, 542);
			this.cmbCameraMemory.Name = "cmbCameraMemory";
			this.cmbCameraMemory.Size = new System.Drawing.Size(224, 20);
			this.cmbCameraMemory.StValue = ((long)(0));
			this.cmbCameraMemory.TabIndex = 31;
			this.cmbCameraMemory.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbExposureEnd
			// 
			this.cmbExposureEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbExposureEnd.Location = new System.Drawing.Point(104, 510);
			this.cmbExposureEnd.Name = "cmbExposureEnd";
			this.cmbExposureEnd.Size = new System.Drawing.Size(224, 20);
			this.cmbExposureEnd.StValue = ((long)(0));
			this.cmbExposureEnd.TabIndex = 29;
			this.cmbExposureEnd.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbExposureWaitReadOut
			// 
			this.cmbExposureWaitReadOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbExposureWaitReadOut.Location = new System.Drawing.Point(104, 462);
			this.cmbExposureWaitReadOut.Name = "cmbExposureWaitReadOut";
			this.cmbExposureWaitReadOut.Size = new System.Drawing.Size(224, 20);
			this.cmbExposureWaitReadOut.StValue = ((long)(0));
			this.cmbExposureWaitReadOut.TabIndex = 27;
			this.cmbExposureWaitReadOut.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbExposureWaitHD
			// 
			this.cmbExposureWaitHD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbExposureWaitHD.Location = new System.Drawing.Point(104, 432);
			this.cmbExposureWaitHD.Name = "cmbExposureWaitHD";
			this.cmbExposureWaitHD.Size = new System.Drawing.Size(224, 20);
			this.cmbExposureWaitHD.StValue = ((long)(0));
			this.cmbExposureWaitHD.TabIndex = 25;
			this.cmbExposureWaitHD.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbNoiseReduction
			// 
			this.cmbNoiseReduction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbNoiseReduction.Location = new System.Drawing.Point(104, 400);
			this.cmbNoiseReduction.Name = "cmbNoiseReduction";
			this.cmbNoiseReduction.Size = new System.Drawing.Size(224, 20);
			this.cmbNoiseReduction.StValue = ((long)(0));
			this.cmbNoiseReduction.TabIndex = 23;
			this.cmbNoiseReduction.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbExposureMode
			// 
			this.cmbExposureMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbExposureMode.Location = new System.Drawing.Point(104, 16);
			this.cmbExposureMode.Name = "cmbExposureMode";
			this.cmbExposureMode.Size = new System.Drawing.Size(224, 20);
			this.cmbExposureMode.StValue = ((long)(0));
			this.cmbExposureMode.TabIndex = 1;
			this.cmbExposureMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbTriggerSource
			// 
			this.cmbTriggerSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTriggerSource.Location = new System.Drawing.Point(198, 119);
			this.cmbTriggerSource.Name = "cmbTriggerSource";
			this.cmbTriggerSource.Size = new System.Drawing.Size(224, 20);
			this.cmbTriggerSource.StValue = ((long)(0));
			this.cmbTriggerSource.TabIndex = 7;
			this.cmbTriggerSource.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbTriggerMode
			// 
			this.cmbTriggerMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTriggerMode.Location = new System.Drawing.Point(198, 86);
			this.cmbTriggerMode.Name = "cmbTriggerMode";
			this.cmbTriggerMode.Size = new System.Drawing.Size(224, 20);
			this.cmbTriggerMode.StValue = ((long)(0));
			this.cmbTriggerMode.TabIndex = 5;
			this.cmbTriggerMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// txtAutoTriggerTime
			// 
			this.txtAutoTriggerTime.Location = new System.Drawing.Point(384, 359);
			this.txtAutoTriggerTime.Name = "txtAutoTriggerTime";
			this.txtAutoTriggerTime.Size = new System.Drawing.Size(72, 22);
			this.txtAutoTriggerTime.TabIndex = 21;
			this.txtAutoTriggerTime.Text = "stTextBox4";
			this.txtAutoTriggerTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtAutoTriggerTime.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// tabPageIO
			// 
			this.tabPageIO.Controls.Add(this.txtTemperature);
			this.tabPageIO.Controls.Add(this.labelTemperature);
			this.tabPageIO.Controls.Add(this.label36);
			this.tabPageIO.Controls.Add(this.cmbResetSwitch);
			this.tabPageIO.Controls.Add(this.txtSDKVersioin);
			this.tabPageIO.Controls.Add(this.lblSDKVersion);
			this.tabPageIO.Controls.Add(this.txtSWStatus3);
			this.tabPageIO.Controls.Add(this.txtSWStatus2);
			this.tabPageIO.Controls.Add(this.txtSWStatus1);
			this.tabPageIO.Controls.Add(this.txtSWStatus0);
			this.tabPageIO.Controls.Add(this.lblRed);
			this.tabPageIO.Controls.Add(this.lblLEDGreen);
			this.tabPageIO.Controls.Add(this.txtFirmVersion);
			this.tabPageIO.Controls.Add(this.txtFPGAVersion);
			this.tabPageIO.Controls.Add(this.txtCameraType);
			this.tabPageIO.Controls.Add(this.lblFirmVersion);
			this.tabPageIO.Controls.Add(this.lblFPGAVersion);
			this.tabPageIO.Controls.Add(this.lblCameraType);
			this.tabPageIO.Controls.Add(this.lblSW);
			this.tabPageIO.Controls.Add(this.lblIO3);
			this.tabPageIO.Controls.Add(this.lblIO2);
			this.tabPageIO.Controls.Add(this.lblIO1);
			this.tabPageIO.Controls.Add(this.lblIOStatus);
			this.tabPageIO.Controls.Add(this.lblIOPolarity);
			this.tabPageIO.Controls.Add(this.lblIOMode);
			this.tabPageIO.Controls.Add(this.lblIOInOut);
			this.tabPageIO.Controls.Add(this.lblIO0);
			this.tabPageIO.Controls.Add(this.cmbLEDRed);
			this.tabPageIO.Controls.Add(this.cmbLEDGreen);
			this.tabPageIO.Controls.Add(this.cmbIOStatus3);
			this.tabPageIO.Controls.Add(this.cmbIOStatus2);
			this.tabPageIO.Controls.Add(this.cmbIOStatus1);
			this.tabPageIO.Controls.Add(this.cmbIOStatus0);
			this.tabPageIO.Controls.Add(this.cmbIOPolarity3);
			this.tabPageIO.Controls.Add(this.cmbIOPolarity2);
			this.tabPageIO.Controls.Add(this.cmbIOPolarity1);
			this.tabPageIO.Controls.Add(this.cmbIOPolarity0);
			this.tabPageIO.Controls.Add(this.cmbIOMode3);
			this.tabPageIO.Controls.Add(this.cmbIOMode2);
			this.tabPageIO.Controls.Add(this.cmbIOMode1);
			this.tabPageIO.Controls.Add(this.cmbIOMode0);
			this.tabPageIO.Controls.Add(this.cmbIOInOut3);
			this.tabPageIO.Controls.Add(this.cmbIOInOut2);
			this.tabPageIO.Controls.Add(this.cmbIOInOut1);
			this.tabPageIO.Controls.Add(this.cmbIOInOut0);
			this.tabPageIO.Location = new System.Drawing.Point(4, 22);
			this.tabPageIO.Name = "tabPageIO";
			this.tabPageIO.Size = new System.Drawing.Size(472, 519);
			this.tabPageIO.TabIndex = 4;
			this.tabPageIO.Text = "IO";
			this.tabPageIO.UseVisualStyleBackColor = true;
			// 
			// txtTemperature
			// 
			this.txtTemperature.Location = new System.Drawing.Point(120, 524);
			this.txtTemperature.Name = "txtTemperature";
			this.txtTemperature.ReadOnly = true;
			this.txtTemperature.Size = new System.Drawing.Size(150, 22);
			this.txtTemperature.TabIndex = 43;
			this.txtTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// labelTemperature
			// 
			this.labelTemperature.AutoSize = true;
			this.labelTemperature.Location = new System.Drawing.Point(16, 524);
			this.labelTemperature.Name = "labelTemperature";
			this.labelTemperature.Size = new System.Drawing.Size(64, 12);
			this.labelTemperature.TabIndex = 42;
			this.labelTemperature.Text = "Temperature";
			// 
			// label36
			// 
			this.label36.AutoSize = true;
			this.label36.Location = new System.Drawing.Point(16, 192);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(64, 12);
			this.label36.TabIndex = 24;
			this.label36.Text = "Reset Switch";
			// 
			// cmbResetSwitch
			// 
			this.cmbResetSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbResetSwitch.Location = new System.Drawing.Point(120, 192);
			this.cmbResetSwitch.Name = "cmbResetSwitch";
			this.cmbResetSwitch.Size = new System.Drawing.Size(200, 20);
			this.cmbResetSwitch.StValue = ((long)(0));
			this.cmbResetSwitch.TabIndex = 25;
			this.cmbResetSwitch.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// txtSDKVersioin
			// 
			this.txtSDKVersioin.Location = new System.Drawing.Point(122, 492);
			this.txtSDKVersioin.Name = "txtSDKVersioin";
			this.txtSDKVersioin.ReadOnly = true;
			this.txtSDKVersioin.Size = new System.Drawing.Size(150, 22);
			this.txtSDKVersioin.TabIndex = 41;
			this.txtSDKVersioin.Text = "0000";
			this.txtSDKVersioin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lblSDKVersion
			// 
			this.lblSDKVersion.AutoSize = true;
			this.lblSDKVersion.Location = new System.Drawing.Point(18, 492);
			this.lblSDKVersion.Name = "lblSDKVersion";
			this.lblSDKVersion.Size = new System.Drawing.Size(66, 12);
			this.lblSDKVersion.TabIndex = 40;
			this.lblSDKVersion.Text = "SDK Version";
			// 
			// txtSWStatus3
			// 
			this.txtSWStatus3.Location = new System.Drawing.Point(202, 289);
			this.txtSWStatus3.Name = "txtSWStatus3";
			this.txtSWStatus3.ReadOnly = true;
			this.txtSWStatus3.Size = new System.Drawing.Size(40, 22);
			this.txtSWStatus3.TabIndex = 29;
			this.txtSWStatus3.Text = "OFF";
			this.txtSWStatus3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtSWStatus2
			// 
			this.txtSWStatus2.Location = new System.Drawing.Point(154, 289);
			this.txtSWStatus2.Name = "txtSWStatus2";
			this.txtSWStatus2.ReadOnly = true;
			this.txtSWStatus2.Size = new System.Drawing.Size(40, 22);
			this.txtSWStatus2.TabIndex = 28;
			this.txtSWStatus2.Text = "OFF";
			this.txtSWStatus2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtSWStatus1
			// 
			this.txtSWStatus1.Location = new System.Drawing.Point(106, 289);
			this.txtSWStatus1.Name = "txtSWStatus1";
			this.txtSWStatus1.ReadOnly = true;
			this.txtSWStatus1.Size = new System.Drawing.Size(40, 22);
			this.txtSWStatus1.TabIndex = 27;
			this.txtSWStatus1.Text = "OFF";
			this.txtSWStatus1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtSWStatus0
			// 
			this.txtSWStatus0.Location = new System.Drawing.Point(58, 289);
			this.txtSWStatus0.Name = "txtSWStatus0";
			this.txtSWStatus0.ReadOnly = true;
			this.txtSWStatus0.Size = new System.Drawing.Size(40, 22);
			this.txtSWStatus0.TabIndex = 26;
			this.txtSWStatus0.Text = "OFF";
			this.txtSWStatus0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lblRed
			// 
			this.lblRed.AutoSize = true;
			this.lblRed.Location = new System.Drawing.Point(18, 369);
			this.lblRed.Name = "lblRed";
			this.lblRed.Size = new System.Drawing.Size(49, 12);
			this.lblRed.TabIndex = 32;
			this.lblRed.Text = "LED Red";
			// 
			// lblLEDGreen
			// 
			this.lblLEDGreen.AutoSize = true;
			this.lblLEDGreen.Location = new System.Drawing.Point(18, 339);
			this.lblLEDGreen.Name = "lblLEDGreen";
			this.lblLEDGreen.Size = new System.Drawing.Size(58, 12);
			this.lblLEDGreen.TabIndex = 30;
			this.lblLEDGreen.Text = "LED Green";
			// 
			// txtFirmVersion
			// 
			this.txtFirmVersion.Location = new System.Drawing.Point(122, 461);
			this.txtFirmVersion.Name = "txtFirmVersion";
			this.txtFirmVersion.ReadOnly = true;
			this.txtFirmVersion.Size = new System.Drawing.Size(150, 22);
			this.txtFirmVersion.TabIndex = 39;
			this.txtFirmVersion.Text = "0000";
			this.txtFirmVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtFPGAVersion
			// 
			this.txtFPGAVersion.Location = new System.Drawing.Point(122, 431);
			this.txtFPGAVersion.Name = "txtFPGAVersion";
			this.txtFPGAVersion.ReadOnly = true;
			this.txtFPGAVersion.Size = new System.Drawing.Size(150, 22);
			this.txtFPGAVersion.TabIndex = 37;
			this.txtFPGAVersion.Text = "0000";
			this.txtFPGAVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtCameraType
			// 
			this.txtCameraType.Location = new System.Drawing.Point(122, 401);
			this.txtCameraType.Name = "txtCameraType";
			this.txtCameraType.ReadOnly = true;
			this.txtCameraType.Size = new System.Drawing.Size(150, 22);
			this.txtCameraType.TabIndex = 35;
			this.txtCameraType.Text = "STC-XXXXXUSB";
			this.txtCameraType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lblFirmVersion
			// 
			this.lblFirmVersion.AutoSize = true;
			this.lblFirmVersion.Location = new System.Drawing.Point(18, 461);
			this.lblFirmVersion.Name = "lblFirmVersion";
			this.lblFirmVersion.Size = new System.Drawing.Size(66, 12);
			this.lblFirmVersion.TabIndex = 38;
			this.lblFirmVersion.Text = "Firm Version";
			// 
			// lblFPGAVersion
			// 
			this.lblFPGAVersion.AutoSize = true;
			this.lblFPGAVersion.Location = new System.Drawing.Point(18, 431);
			this.lblFPGAVersion.Name = "lblFPGAVersion";
			this.lblFPGAVersion.Size = new System.Drawing.Size(72, 12);
			this.lblFPGAVersion.TabIndex = 36;
			this.lblFPGAVersion.Text = "FPGA Version";
			// 
			// lblCameraType
			// 
			this.lblCameraType.AutoSize = true;
			this.lblCameraType.Location = new System.Drawing.Point(18, 401);
			this.lblCameraType.Name = "lblCameraType";
			this.lblCameraType.Size = new System.Drawing.Size(68, 12);
			this.lblCameraType.TabIndex = 34;
			this.lblCameraType.Text = "Camera Type";
			// 
			// lblSW
			// 
			this.lblSW.AutoSize = true;
			this.lblSW.Location = new System.Drawing.Point(18, 289);
			this.lblSW.Name = "lblSW";
			this.lblSW.Size = new System.Drawing.Size(22, 12);
			this.lblSW.TabIndex = 25;
			this.lblSW.Text = "SW";
			// 
			// lblIO3
			// 
			this.lblIO3.AutoSize = true;
			this.lblIO3.Location = new System.Drawing.Point(16, 160);
			this.lblIO3.Name = "lblIO3";
			this.lblIO3.Size = new System.Drawing.Size(23, 12);
			this.lblIO3.TabIndex = 19;
			this.lblIO3.Text = "IO3";
			// 
			// lblIO2
			// 
			this.lblIO2.AutoSize = true;
			this.lblIO2.Location = new System.Drawing.Point(16, 120);
			this.lblIO2.Name = "lblIO2";
			this.lblIO2.Size = new System.Drawing.Size(23, 12);
			this.lblIO2.TabIndex = 14;
			this.lblIO2.Text = "IO2";
			// 
			// lblIO1
			// 
			this.lblIO1.AutoSize = true;
			this.lblIO1.Location = new System.Drawing.Point(16, 80);
			this.lblIO1.Name = "lblIO1";
			this.lblIO1.Size = new System.Drawing.Size(23, 12);
			this.lblIO1.TabIndex = 9;
			this.lblIO1.Text = "IO1";
			// 
			// lblIOStatus
			// 
			this.lblIOStatus.AutoSize = true;
			this.lblIOStatus.Location = new System.Drawing.Point(408, 10);
			this.lblIOStatus.Name = "lblIOStatus";
			this.lblIOStatus.Size = new System.Drawing.Size(32, 12);
			this.lblIOStatus.TabIndex = 3;
			this.lblIOStatus.Text = "Status";
			// 
			// lblIOPolarity
			// 
			this.lblIOPolarity.AutoSize = true;
			this.lblIOPolarity.Location = new System.Drawing.Point(328, 10);
			this.lblIOPolarity.Name = "lblIOPolarity";
			this.lblIOPolarity.Size = new System.Drawing.Size(63, 12);
			this.lblIOPolarity.TabIndex = 2;
			this.lblIOPolarity.Text = "NEGA/POSI";
			// 
			// lblIOMode
			// 
			this.lblIOMode.AutoSize = true;
			this.lblIOMode.Location = new System.Drawing.Point(136, 10);
			this.lblIOMode.Name = "lblIOMode";
			this.lblIOMode.Size = new System.Drawing.Size(44, 12);
			this.lblIOMode.TabIndex = 1;
			this.lblIOMode.Text = "IOMode";
			// 
			// lblIOInOut
			// 
			this.lblIOInOut.AutoSize = true;
			this.lblIOInOut.Location = new System.Drawing.Point(56, 10);
			this.lblIOInOut.Name = "lblIOInOut";
			this.lblIOInOut.Size = new System.Drawing.Size(43, 12);
			this.lblIOInOut.TabIndex = 0;
			this.lblIOInOut.Text = "IN/OUT";
			// 
			// lblIO0
			// 
			this.lblIO0.AutoSize = true;
			this.lblIO0.Location = new System.Drawing.Point(16, 40);
			this.lblIO0.Name = "lblIO0";
			this.lblIO0.Size = new System.Drawing.Size(23, 12);
			this.lblIO0.TabIndex = 4;
			this.lblIO0.Text = "IO0";
			// 
			// cmbLEDRed
			// 
			this.cmbLEDRed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbLEDRed.Location = new System.Drawing.Point(98, 369);
			this.cmbLEDRed.Name = "cmbLEDRed";
			this.cmbLEDRed.Size = new System.Drawing.Size(72, 20);
			this.cmbLEDRed.StValue = ((long)(0));
			this.cmbLEDRed.TabIndex = 33;
			this.cmbLEDRed.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbLEDGreen
			// 
			this.cmbLEDGreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbLEDGreen.Location = new System.Drawing.Point(98, 339);
			this.cmbLEDGreen.Name = "cmbLEDGreen";
			this.cmbLEDGreen.Size = new System.Drawing.Size(72, 20);
			this.cmbLEDGreen.StValue = ((long)(0));
			this.cmbLEDGreen.TabIndex = 31;
			this.cmbLEDGreen.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOStatus3
			// 
			this.cmbIOStatus3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOStatus3.Location = new System.Drawing.Point(392, 160);
			this.cmbIOStatus3.Name = "cmbIOStatus3";
			this.cmbIOStatus3.Size = new System.Drawing.Size(72, 20);
			this.cmbIOStatus3.StValue = ((long)(0));
			this.cmbIOStatus3.TabIndex = 23;
			this.cmbIOStatus3.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOStatus2
			// 
			this.cmbIOStatus2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOStatus2.Location = new System.Drawing.Point(392, 120);
			this.cmbIOStatus2.Name = "cmbIOStatus2";
			this.cmbIOStatus2.Size = new System.Drawing.Size(72, 20);
			this.cmbIOStatus2.StValue = ((long)(0));
			this.cmbIOStatus2.TabIndex = 18;
			this.cmbIOStatus2.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOStatus1
			// 
			this.cmbIOStatus1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOStatus1.Location = new System.Drawing.Point(392, 80);
			this.cmbIOStatus1.Name = "cmbIOStatus1";
			this.cmbIOStatus1.Size = new System.Drawing.Size(72, 20);
			this.cmbIOStatus1.StValue = ((long)(0));
			this.cmbIOStatus1.TabIndex = 13;
			this.cmbIOStatus1.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOStatus0
			// 
			this.cmbIOStatus0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOStatus0.Location = new System.Drawing.Point(392, 40);
			this.cmbIOStatus0.Name = "cmbIOStatus0";
			this.cmbIOStatus0.Size = new System.Drawing.Size(72, 20);
			this.cmbIOStatus0.StValue = ((long)(0));
			this.cmbIOStatus0.TabIndex = 8;
			this.cmbIOStatus0.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOPolarity3
			// 
			this.cmbIOPolarity3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOPolarity3.Location = new System.Drawing.Point(320, 160);
			this.cmbIOPolarity3.Name = "cmbIOPolarity3";
			this.cmbIOPolarity3.Size = new System.Drawing.Size(72, 20);
			this.cmbIOPolarity3.StValue = ((long)(0));
			this.cmbIOPolarity3.TabIndex = 22;
			this.cmbIOPolarity3.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOPolarity2
			// 
			this.cmbIOPolarity2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOPolarity2.Location = new System.Drawing.Point(320, 120);
			this.cmbIOPolarity2.Name = "cmbIOPolarity2";
			this.cmbIOPolarity2.Size = new System.Drawing.Size(72, 20);
			this.cmbIOPolarity2.StValue = ((long)(0));
			this.cmbIOPolarity2.TabIndex = 17;
			this.cmbIOPolarity2.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOPolarity1
			// 
			this.cmbIOPolarity1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOPolarity1.Location = new System.Drawing.Point(320, 80);
			this.cmbIOPolarity1.Name = "cmbIOPolarity1";
			this.cmbIOPolarity1.Size = new System.Drawing.Size(72, 20);
			this.cmbIOPolarity1.StValue = ((long)(0));
			this.cmbIOPolarity1.TabIndex = 12;
			this.cmbIOPolarity1.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOPolarity0
			// 
			this.cmbIOPolarity0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOPolarity0.Location = new System.Drawing.Point(320, 40);
			this.cmbIOPolarity0.Name = "cmbIOPolarity0";
			this.cmbIOPolarity0.Size = new System.Drawing.Size(72, 20);
			this.cmbIOPolarity0.StValue = ((long)(0));
			this.cmbIOPolarity0.TabIndex = 7;
			this.cmbIOPolarity0.SelectedIndexChanged += new System.EventHandler(this.cmbIOPolarity0_SelectedIndexChanged);
			this.cmbIOPolarity0.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOMode3
			// 
			this.cmbIOMode3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOMode3.Location = new System.Drawing.Point(120, 160);
			this.cmbIOMode3.Name = "cmbIOMode3";
			this.cmbIOMode3.Size = new System.Drawing.Size(200, 20);
			this.cmbIOMode3.StValue = ((long)(0));
			this.cmbIOMode3.TabIndex = 21;
			this.cmbIOMode3.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOMode2
			// 
			this.cmbIOMode2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOMode2.Location = new System.Drawing.Point(120, 120);
			this.cmbIOMode2.Name = "cmbIOMode2";
			this.cmbIOMode2.Size = new System.Drawing.Size(200, 20);
			this.cmbIOMode2.StValue = ((long)(0));
			this.cmbIOMode2.TabIndex = 16;
			this.cmbIOMode2.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOMode1
			// 
			this.cmbIOMode1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOMode1.Location = new System.Drawing.Point(120, 80);
			this.cmbIOMode1.Name = "cmbIOMode1";
			this.cmbIOMode1.Size = new System.Drawing.Size(200, 20);
			this.cmbIOMode1.StValue = ((long)(0));
			this.cmbIOMode1.TabIndex = 11;
			this.cmbIOMode1.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOMode0
			// 
			this.cmbIOMode0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOMode0.Location = new System.Drawing.Point(120, 40);
			this.cmbIOMode0.Name = "cmbIOMode0";
			this.cmbIOMode0.Size = new System.Drawing.Size(200, 20);
			this.cmbIOMode0.StValue = ((long)(0));
			this.cmbIOMode0.TabIndex = 6;
			this.cmbIOMode0.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOInOut3
			// 
			this.cmbIOInOut3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOInOut3.Location = new System.Drawing.Point(48, 160);
			this.cmbIOInOut3.Name = "cmbIOInOut3";
			this.cmbIOInOut3.Size = new System.Drawing.Size(72, 20);
			this.cmbIOInOut3.StValue = ((long)(0));
			this.cmbIOInOut3.TabIndex = 20;
			this.cmbIOInOut3.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOInOut2
			// 
			this.cmbIOInOut2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOInOut2.Location = new System.Drawing.Point(48, 120);
			this.cmbIOInOut2.Name = "cmbIOInOut2";
			this.cmbIOInOut2.Size = new System.Drawing.Size(72, 20);
			this.cmbIOInOut2.StValue = ((long)(0));
			this.cmbIOInOut2.TabIndex = 15;
			this.cmbIOInOut2.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOInOut1
			// 
			this.cmbIOInOut1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOInOut1.Location = new System.Drawing.Point(48, 80);
			this.cmbIOInOut1.Name = "cmbIOInOut1";
			this.cmbIOInOut1.Size = new System.Drawing.Size(72, 20);
			this.cmbIOInOut1.StValue = ((long)(0));
			this.cmbIOInOut1.TabIndex = 10;
			this.cmbIOInOut1.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbIOInOut0
			// 
			this.cmbIOInOut0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbIOInOut0.Location = new System.Drawing.Point(48, 40);
			this.cmbIOInOut0.Name = "cmbIOInOut0";
			this.cmbIOInOut0.Size = new System.Drawing.Size(72, 20);
			this.cmbIOInOut0.StValue = ((long)(0));
			this.cmbIOInOut0.TabIndex = 5;
			this.cmbIOInOut0.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// tabPageTriggerTiming
			// 
			this.tabPageTriggerTiming.Controls.Add(this.txtLineDebounceTime);
			this.tabPageTriggerTiming.Controls.Add(this.lblLineDebounceTimeValue);
			this.tabPageTriggerTiming.Controls.Add(this.trackBarLineDebounceTime);
			this.tabPageTriggerTiming.Controls.Add(this.lblLineDebounceTime);
			this.tabPageTriggerTiming.Controls.Add(this.txtReadOutDelay);
			this.tabPageTriggerTiming.Controls.Add(this.txtOutputPulseDuration);
			this.tabPageTriggerTiming.Controls.Add(this.txtOutputPulseDelay);
			this.tabPageTriggerTiming.Controls.Add(this.txtStrobeEndDelay);
			this.tabPageTriggerTiming.Controls.Add(this.txtStrobeStartDelay);
			this.tabPageTriggerTiming.Controls.Add(this.lblReadOutDelayValue);
			this.tabPageTriggerTiming.Controls.Add(this.trackBarReadOutDelay);
			this.tabPageTriggerTiming.Controls.Add(this.lblReadOutDelay);
			this.tabPageTriggerTiming.Controls.Add(this.lblOutputPulseDurationValue);
			this.tabPageTriggerTiming.Controls.Add(this.trackBarOutputPulseDuration);
			this.tabPageTriggerTiming.Controls.Add(this.lblOutputPulseDuration);
			this.tabPageTriggerTiming.Controls.Add(this.lblOutputPulseDelayValue);
			this.tabPageTriggerTiming.Controls.Add(this.trackBarOutputPulseDelay);
			this.tabPageTriggerTiming.Controls.Add(this.lblOutputPulseDelay);
			this.tabPageTriggerTiming.Controls.Add(this.lblStrobeEndDelayValue);
			this.tabPageTriggerTiming.Controls.Add(this.trackBarStrobeEndDelay);
			this.tabPageTriggerTiming.Controls.Add(this.lblStrobeEndDelay);
			this.tabPageTriggerTiming.Controls.Add(this.lbltrackBarStrobeStartDelayValue);
			this.tabPageTriggerTiming.Controls.Add(this.trackBarStrobeStartDelay);
			this.tabPageTriggerTiming.Controls.Add(this.lblStrobeStartDelay);
			this.tabPageTriggerTiming.Location = new System.Drawing.Point(4, 22);
			this.tabPageTriggerTiming.Name = "tabPageTriggerTiming";
			this.tabPageTriggerTiming.Size = new System.Drawing.Size(472, 519);
			this.tabPageTriggerTiming.TabIndex = 3;
			this.tabPageTriggerTiming.Text = "Trigger Timing";
			this.tabPageTriggerTiming.UseVisualStyleBackColor = true;
			// 
			// txtLineDebounceTime
			// 
			this.txtLineDebounceTime.Location = new System.Drawing.Point(408, 210);
			this.txtLineDebounceTime.Name = "txtLineDebounceTime";
			this.txtLineDebounceTime.Size = new System.Drawing.Size(56, 22);
			this.txtLineDebounceTime.TabIndex = 51;
			this.txtLineDebounceTime.Text = "stTextBox16";
			this.txtLineDebounceTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtLineDebounceTime.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// lblLineDebounceTimeValue
			// 
			this.lblLineDebounceTimeValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLineDebounceTimeValue.Location = new System.Drawing.Point(336, 210);
			this.lblLineDebounceTimeValue.Name = "lblLineDebounceTimeValue";
			this.lblLineDebounceTimeValue.Size = new System.Drawing.Size(64, 30);
			this.lblLineDebounceTimeValue.TabIndex = 49;
			this.lblLineDebounceTimeValue.Text = "s";
			this.lblLineDebounceTimeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarLineDebounceTime
			// 
			this.trackBarLineDebounceTime.AutoSize = false;
			this.trackBarLineDebounceTime.LargeChange = 1000;
			this.trackBarLineDebounceTime.Location = new System.Drawing.Point(128, 210);
			this.trackBarLineDebounceTime.Name = "trackBarLineDebounceTime";
			this.trackBarLineDebounceTime.Size = new System.Drawing.Size(200, 30);
			this.trackBarLineDebounceTime.TabIndex = 50;
			this.trackBarLineDebounceTime.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarLineDebounceTime.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblLineDebounceTime
			// 
			this.lblLineDebounceTime.AutoSize = true;
			this.lblLineDebounceTime.Location = new System.Drawing.Point(8, 220);
			this.lblLineDebounceTime.Name = "lblLineDebounceTime";
			this.lblLineDebounceTime.Size = new System.Drawing.Size(103, 12);
			this.lblLineDebounceTime.TabIndex = 48;
			this.lblLineDebounceTime.Text = "Line Debounce Time";
			// 
			// txtReadOutDelay
			// 
			this.txtReadOutDelay.Location = new System.Drawing.Point(408, 172);
			this.txtReadOutDelay.Name = "txtReadOutDelay";
			this.txtReadOutDelay.Size = new System.Drawing.Size(56, 22);
			this.txtReadOutDelay.TabIndex = 47;
			this.txtReadOutDelay.Text = "stTextBox16";
			this.txtReadOutDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtReadOutDelay.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtOutputPulseDuration
			// 
			this.txtOutputPulseDuration.Location = new System.Drawing.Point(408, 132);
			this.txtOutputPulseDuration.Name = "txtOutputPulseDuration";
			this.txtOutputPulseDuration.Size = new System.Drawing.Size(56, 22);
			this.txtOutputPulseDuration.TabIndex = 45;
			this.txtOutputPulseDuration.Text = "stTextBox15";
			this.txtOutputPulseDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtOutputPulseDuration.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtOutputPulseDelay
			// 
			this.txtOutputPulseDelay.Location = new System.Drawing.Point(408, 92);
			this.txtOutputPulseDelay.Name = "txtOutputPulseDelay";
			this.txtOutputPulseDelay.Size = new System.Drawing.Size(56, 22);
			this.txtOutputPulseDelay.TabIndex = 43;
			this.txtOutputPulseDelay.Text = "stTextBox14";
			this.txtOutputPulseDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtOutputPulseDelay.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtStrobeEndDelay
			// 
			this.txtStrobeEndDelay.Location = new System.Drawing.Point(408, 52);
			this.txtStrobeEndDelay.Name = "txtStrobeEndDelay";
			this.txtStrobeEndDelay.Size = new System.Drawing.Size(56, 22);
			this.txtStrobeEndDelay.TabIndex = 41;
			this.txtStrobeEndDelay.Text = "stTextBox13";
			this.txtStrobeEndDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtStrobeEndDelay.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// txtStrobeStartDelay
			// 
			this.txtStrobeStartDelay.Location = new System.Drawing.Point(408, 12);
			this.txtStrobeStartDelay.Name = "txtStrobeStartDelay";
			this.txtStrobeStartDelay.Size = new System.Drawing.Size(56, 22);
			this.txtStrobeStartDelay.TabIndex = 39;
			this.txtStrobeStartDelay.Text = "stTextBox12";
			this.txtStrobeStartDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtStrobeStartDelay.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// lblReadOutDelayValue
			// 
			this.lblReadOutDelayValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblReadOutDelayValue.Location = new System.Drawing.Point(336, 172);
			this.lblReadOutDelayValue.Name = "lblReadOutDelayValue";
			this.lblReadOutDelayValue.Size = new System.Drawing.Size(64, 30);
			this.lblReadOutDelayValue.TabIndex = 30;
			this.lblReadOutDelayValue.Text = "s";
			this.lblReadOutDelayValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarReadOutDelay
			// 
			this.trackBarReadOutDelay.AutoSize = false;
			this.trackBarReadOutDelay.LargeChange = 1000;
			this.trackBarReadOutDelay.Location = new System.Drawing.Point(128, 172);
			this.trackBarReadOutDelay.Name = "trackBarReadOutDelay";
			this.trackBarReadOutDelay.Size = new System.Drawing.Size(200, 30);
			this.trackBarReadOutDelay.TabIndex = 46;
			this.trackBarReadOutDelay.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarReadOutDelay.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblReadOutDelay
			// 
			this.lblReadOutDelay.AutoSize = true;
			this.lblReadOutDelay.Location = new System.Drawing.Point(8, 182);
			this.lblReadOutDelay.Name = "lblReadOutDelay";
			this.lblReadOutDelay.Size = new System.Drawing.Size(79, 12);
			this.lblReadOutDelay.TabIndex = 28;
			this.lblReadOutDelay.Text = "Read Out Delay";
			// 
			// lblOutputPulseDurationValue
			// 
			this.lblOutputPulseDurationValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblOutputPulseDurationValue.Location = new System.Drawing.Point(336, 132);
			this.lblOutputPulseDurationValue.Name = "lblOutputPulseDurationValue";
			this.lblOutputPulseDurationValue.Size = new System.Drawing.Size(64, 30);
			this.lblOutputPulseDurationValue.TabIndex = 26;
			this.lblOutputPulseDurationValue.Text = "s";
			this.lblOutputPulseDurationValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarOutputPulseDuration
			// 
			this.trackBarOutputPulseDuration.AutoSize = false;
			this.trackBarOutputPulseDuration.LargeChange = 1000;
			this.trackBarOutputPulseDuration.Location = new System.Drawing.Point(128, 132);
			this.trackBarOutputPulseDuration.Name = "trackBarOutputPulseDuration";
			this.trackBarOutputPulseDuration.Size = new System.Drawing.Size(200, 30);
			this.trackBarOutputPulseDuration.TabIndex = 44;
			this.trackBarOutputPulseDuration.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarOutputPulseDuration.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblOutputPulseDuration
			// 
			this.lblOutputPulseDuration.AutoSize = true;
			this.lblOutputPulseDuration.Location = new System.Drawing.Point(8, 142);
			this.lblOutputPulseDuration.Name = "lblOutputPulseDuration";
			this.lblOutputPulseDuration.Size = new System.Drawing.Size(108, 12);
			this.lblOutputPulseDuration.TabIndex = 24;
			this.lblOutputPulseDuration.Text = "Output Pulse Duration";
			// 
			// lblOutputPulseDelayValue
			// 
			this.lblOutputPulseDelayValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblOutputPulseDelayValue.Location = new System.Drawing.Point(336, 92);
			this.lblOutputPulseDelayValue.Name = "lblOutputPulseDelayValue";
			this.lblOutputPulseDelayValue.Size = new System.Drawing.Size(64, 30);
			this.lblOutputPulseDelayValue.TabIndex = 22;
			this.lblOutputPulseDelayValue.Text = "s";
			this.lblOutputPulseDelayValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarOutputPulseDelay
			// 
			this.trackBarOutputPulseDelay.AutoSize = false;
			this.trackBarOutputPulseDelay.LargeChange = 1000;
			this.trackBarOutputPulseDelay.Location = new System.Drawing.Point(128, 92);
			this.trackBarOutputPulseDelay.Name = "trackBarOutputPulseDelay";
			this.trackBarOutputPulseDelay.Size = new System.Drawing.Size(200, 30);
			this.trackBarOutputPulseDelay.TabIndex = 42;
			this.trackBarOutputPulseDelay.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarOutputPulseDelay.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblOutputPulseDelay
			// 
			this.lblOutputPulseDelay.AutoSize = true;
			this.lblOutputPulseDelay.Location = new System.Drawing.Point(8, 102);
			this.lblOutputPulseDelay.Name = "lblOutputPulseDelay";
			this.lblOutputPulseDelay.Size = new System.Drawing.Size(94, 12);
			this.lblOutputPulseDelay.TabIndex = 20;
			this.lblOutputPulseDelay.Text = "Output Pulse Delay";
			// 
			// lblStrobeEndDelayValue
			// 
			this.lblStrobeEndDelayValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblStrobeEndDelayValue.Location = new System.Drawing.Point(336, 52);
			this.lblStrobeEndDelayValue.Name = "lblStrobeEndDelayValue";
			this.lblStrobeEndDelayValue.Size = new System.Drawing.Size(64, 30);
			this.lblStrobeEndDelayValue.TabIndex = 18;
			this.lblStrobeEndDelayValue.Text = "s";
			this.lblStrobeEndDelayValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarStrobeEndDelay
			// 
			this.trackBarStrobeEndDelay.AutoSize = false;
			this.trackBarStrobeEndDelay.LargeChange = 1000;
			this.trackBarStrobeEndDelay.Location = new System.Drawing.Point(128, 52);
			this.trackBarStrobeEndDelay.Name = "trackBarStrobeEndDelay";
			this.trackBarStrobeEndDelay.Size = new System.Drawing.Size(200, 30);
			this.trackBarStrobeEndDelay.TabIndex = 40;
			this.trackBarStrobeEndDelay.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarStrobeEndDelay.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblStrobeEndDelay
			// 
			this.lblStrobeEndDelay.AutoSize = true;
			this.lblStrobeEndDelay.Location = new System.Drawing.Point(8, 62);
			this.lblStrobeEndDelay.Name = "lblStrobeEndDelay";
			this.lblStrobeEndDelay.Size = new System.Drawing.Size(87, 12);
			this.lblStrobeEndDelay.TabIndex = 16;
			this.lblStrobeEndDelay.Text = "Strobe End Delay";
			// 
			// lbltrackBarStrobeStartDelayValue
			// 
			this.lbltrackBarStrobeStartDelayValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbltrackBarStrobeStartDelayValue.Location = new System.Drawing.Point(336, 12);
			this.lbltrackBarStrobeStartDelayValue.Name = "lbltrackBarStrobeStartDelayValue";
			this.lbltrackBarStrobeStartDelayValue.Size = new System.Drawing.Size(64, 30);
			this.lbltrackBarStrobeStartDelayValue.TabIndex = 14;
			this.lbltrackBarStrobeStartDelayValue.Text = "s";
			this.lbltrackBarStrobeStartDelayValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarStrobeStartDelay
			// 
			this.trackBarStrobeStartDelay.AutoSize = false;
			this.trackBarStrobeStartDelay.LargeChange = 1000;
			this.trackBarStrobeStartDelay.Location = new System.Drawing.Point(128, 12);
			this.trackBarStrobeStartDelay.Name = "trackBarStrobeStartDelay";
			this.trackBarStrobeStartDelay.Size = new System.Drawing.Size(200, 30);
			this.trackBarStrobeStartDelay.TabIndex = 38;
			this.trackBarStrobeStartDelay.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarStrobeStartDelay.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// lblStrobeStartDelay
			// 
			this.lblStrobeStartDelay.AutoSize = true;
			this.lblStrobeStartDelay.Location = new System.Drawing.Point(8, 22);
			this.lblStrobeStartDelay.Name = "lblStrobeStartDelay";
			this.lblStrobeStartDelay.Size = new System.Drawing.Size(89, 12);
			this.lblStrobeStartDelay.TabIndex = 12;
			this.lblStrobeStartDelay.Text = "Strobe Start Delay";
			// 
			// tabPageDefectPixelCorrection
			// 
			this.tabPageDefectPixelCorrection.AutoScroll = true;
			this.tabPageDefectPixelCorrection.Controls.Add(this.defectPixelSetting1);
			this.tabPageDefectPixelCorrection.Controls.Add(this.cmbDefectPixelCorrectionMode);
			this.tabPageDefectPixelCorrection.Controls.Add(this.label26);
			this.tabPageDefectPixelCorrection.Controls.Add(this.btnDetectDefectPixel);
			this.tabPageDefectPixelCorrection.Controls.Add(this.numericUpDownDefectPixelThreshold);
			this.tabPageDefectPixelCorrection.Controls.Add(this.label22);
			this.tabPageDefectPixelCorrection.Location = new System.Drawing.Point(4, 22);
			this.tabPageDefectPixelCorrection.Name = "tabPageDefectPixelCorrection";
			this.tabPageDefectPixelCorrection.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDefectPixelCorrection.Size = new System.Drawing.Size(472, 519);
			this.tabPageDefectPixelCorrection.TabIndex = 8;
			this.tabPageDefectPixelCorrection.Text = "Defect Pixel Correction";
			this.tabPageDefectPixelCorrection.UseVisualStyleBackColor = true;
			// 
			// defectPixelSetting1
			// 
			this.defectPixelSetting1.AutoScroll = true;
			this.defectPixelSetting1.AutoSize = true;
			this.defectPixelSetting1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.defectPixelSetting1.Count = 64;
			this.defectPixelSetting1.Location = new System.Drawing.Point(0, 86);
			this.defectPixelSetting1.Name = "defectPixelSetting1";
			this.defectPixelSetting1.Size = new System.Drawing.Size(421, 3779);
			this.defectPixelSetting1.TabIndex = 5;
			this.defectPixelSetting1.TrackBarScroll += new System.EventHandler(this.trackBar_Scroll);
			this.defectPixelSetting1.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// cmbDefectPixelCorrectionMode
			// 
			this.cmbDefectPixelCorrectionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDefectPixelCorrectionMode.FormattingEnabled = true;
			this.cmbDefectPixelCorrectionMode.Location = new System.Drawing.Point(188, 49);
			this.cmbDefectPixelCorrectionMode.Name = "cmbDefectPixelCorrectionMode";
			this.cmbDefectPixelCorrectionMode.Size = new System.Drawing.Size(121, 20);
			this.cmbDefectPixelCorrectionMode.StValue = ((long)(0));
			this.cmbDefectPixelCorrectionMode.TabIndex = 4;
			this.cmbDefectPixelCorrectionMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(8, 52);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(140, 12);
			this.label26.TabIndex = 3;
			this.label26.Text = "Defect pixel correction mode";
			// 
			// btnDetectDefectPixel
			// 
			this.btnDetectDefectPixel.Location = new System.Drawing.Point(188, 15);
			this.btnDetectDefectPixel.Name = "btnDetectDefectPixel";
			this.btnDetectDefectPixel.Size = new System.Drawing.Size(75, 29);
			this.btnDetectDefectPixel.TabIndex = 2;
			this.btnDetectDefectPixel.Text = "Detect";
			this.btnDetectDefectPixel.UseVisualStyleBackColor = true;
			this.btnDetectDefectPixel.Click += new System.EventHandler(this.btnDetectDefectPixel_Click);
			// 
			// numericUpDownDefectPixelThreshold
			// 
			this.numericUpDownDefectPixelThreshold.Location = new System.Drawing.Point(96, 19);
			this.numericUpDownDefectPixelThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.numericUpDownDefectPixelThreshold.Name = "numericUpDownDefectPixelThreshold";
			this.numericUpDownDefectPixelThreshold.Size = new System.Drawing.Size(65, 22);
			this.numericUpDownDefectPixelThreshold.TabIndex = 1;
			this.numericUpDownDefectPixelThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDownDefectPixelThreshold.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(8, 21);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(52, 12);
			this.label22.TabIndex = 0;
			this.label22.Text = "Threshold";
			// 
			// tabPageEEPROM
			// 
			this.tabPageEEPROM.Controls.Add(this.btnWriteCameraSettingDPP);
			this.tabPageEEPROM.Controls.Add(this.btnReadCameraSettingDPP);
			this.tabPageEEPROM.Controls.Add(this.btnInitCameraSetting);
			this.tabPageEEPROM.Controls.Add(this.btnWriteCameraSetting);
			this.tabPageEEPROM.Controls.Add(this.btnReadCameraSetting);
			this.tabPageEEPROM.Location = new System.Drawing.Point(4, 22);
			this.tabPageEEPROM.Name = "tabPageEEPROM";
			this.tabPageEEPROM.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageEEPROM.Size = new System.Drawing.Size(472, 519);
			this.tabPageEEPROM.TabIndex = 7;
			this.tabPageEEPROM.Text = "EEPROM";
			this.tabPageEEPROM.UseVisualStyleBackColor = true;
			// 
			// btnWriteCameraSettingDPP
			// 
			this.btnWriteCameraSettingDPP.Location = new System.Drawing.Point(9, 154);
			this.btnWriteCameraSettingDPP.Name = "btnWriteCameraSettingDPP";
			this.btnWriteCameraSettingDPP.Size = new System.Drawing.Size(267, 28);
			this.btnWriteCameraSettingDPP.TabIndex = 4;
			this.btnWriteCameraSettingDPP.Tag = "2400";
			this.btnWriteCameraSettingDPP.Text = "Save[DefectPixelPosition]";
			this.btnWriteCameraSettingDPP.UseVisualStyleBackColor = true;
			this.btnWriteCameraSettingDPP.Click += new System.EventHandler(this.btnCameraSetting_Click);
			// 
			// btnReadCameraSettingDPP
			// 
			this.btnReadCameraSettingDPP.Location = new System.Drawing.Point(9, 118);
			this.btnReadCameraSettingDPP.Name = "btnReadCameraSettingDPP";
			this.btnReadCameraSettingDPP.Size = new System.Drawing.Size(267, 28);
			this.btnReadCameraSettingDPP.TabIndex = 3;
			this.btnReadCameraSettingDPP.Tag = "1400";
			this.btnReadCameraSettingDPP.Text = "Load[DefectPixelPosition]";
			this.btnReadCameraSettingDPP.UseVisualStyleBackColor = true;
			this.btnReadCameraSettingDPP.Click += new System.EventHandler(this.btnCameraSetting_Click);
			// 
			// btnInitCameraSetting
			// 
			this.btnInitCameraSetting.Location = new System.Drawing.Point(9, 81);
			this.btnInitCameraSetting.Name = "btnInitCameraSetting";
			this.btnInitCameraSetting.Size = new System.Drawing.Size(267, 29);
			this.btnInitCameraSetting.TabIndex = 2;
			this.btnInitCameraSetting.Tag = "8000";
			this.btnInitCameraSetting.Text = "Reset Factory Default";
			this.btnInitCameraSetting.UseVisualStyleBackColor = true;
			this.btnInitCameraSetting.Click += new System.EventHandler(this.btnCameraSetting_Click);
			// 
			// btnWriteCameraSetting
			// 
			this.btnWriteCameraSetting.Location = new System.Drawing.Point(9, 45);
			this.btnWriteCameraSetting.Name = "btnWriteCameraSetting";
			this.btnWriteCameraSetting.Size = new System.Drawing.Size(267, 29);
			this.btnWriteCameraSetting.TabIndex = 1;
			this.btnWriteCameraSetting.Tag = "2800";
			this.btnWriteCameraSetting.Text = "Save[Standard]";
			this.btnWriteCameraSetting.UseVisualStyleBackColor = true;
			this.btnWriteCameraSetting.Click += new System.EventHandler(this.btnCameraSetting_Click);
			// 
			// btnReadCameraSetting
			// 
			this.btnReadCameraSetting.Location = new System.Drawing.Point(9, 9);
			this.btnReadCameraSetting.Name = "btnReadCameraSetting";
			this.btnReadCameraSetting.Size = new System.Drawing.Size(267, 29);
			this.btnReadCameraSetting.TabIndex = 0;
			this.btnReadCameraSetting.Tag = "1800";
			this.btnReadCameraSetting.Text = "Load[Standard]";
			this.btnReadCameraSetting.UseVisualStyleBackColor = true;
			this.btnReadCameraSetting.Click += new System.EventHandler(this.btnCameraSetting_Click);
			// 
			// tabPageOther
			// 
			this.tabPageOther.Controls.Add(this.labelDisplayPixelFormat);
			this.tabPageOther.Controls.Add(this.cmbDisplayPixelFormat);
			this.tabPageOther.Controls.Add(this.lblRegionMode);
			this.tabPageOther.Controls.Add(this.cmbRegionMode);
			this.tabPageOther.Controls.Add(this.lblCurrentRegion);
			this.tabPageOther.Controls.Add(this.cmbCurrentRegion);
			this.tabPageOther.Controls.Add(this.cmbVBinningSumMode);
			this.tabPageOther.Controls.Add(this.label31);
			this.tabPageOther.Controls.Add(this.label28);
			this.tabPageOther.Controls.Add(this.cmbHBinningSumMode);
			this.tabPageOther.Controls.Add(this.labelOutputFPS);
			this.tabPageOther.Controls.Add(this.trackBarVBlankForFPS);
			this.tabPageOther.Controls.Add(this.label18);
			this.tabPageOther.Controls.Add(this.label17);
			this.tabPageOther.Controls.Add(this.label16);
			this.tabPageOther.Controls.Add(this.trackBarImageHeight);
			this.tabPageOther.Controls.Add(this.label14);
			this.tabPageOther.Controls.Add(this.trackBarImageWidth);
			this.tabPageOther.Controls.Add(this.label15);
			this.tabPageOther.Controls.Add(this.trackBarImageOffsetY);
			this.tabPageOther.Controls.Add(this.label12);
			this.tabPageOther.Controls.Add(this.trackBarImageOffsetX);
			this.tabPageOther.Controls.Add(this.label13);
			this.tabPageOther.Controls.Add(this.lblTransferBitsPerPixel);
			this.tabPageOther.Controls.Add(this.lblRotation);
			this.tabPageOther.Controls.Add(this.lblMirror);
			this.tabPageOther.Controls.Add(this.lblColorInterpolation);
			this.tabPageOther.Controls.Add(this.lblClockMode);
			this.tabPageOther.Controls.Add(this.lblScanMode);
			this.tabPageOther.Controls.Add(this.stTextBoxVBlankForFPS);
			this.tabPageOther.Controls.Add(this.stComboBoxVBinningSkipping);
			this.tabPageOther.Controls.Add(this.stComboBoxHBinningSkipping);
			this.tabPageOther.Controls.Add(this.stTextBoxImageHeight);
			this.tabPageOther.Controls.Add(this.stTextBoxImageWidth);
			this.tabPageOther.Controls.Add(this.stTextBoxImageOffsetY);
			this.tabPageOther.Controls.Add(this.stTextBoxImageOffsetX);
			this.tabPageOther.Controls.Add(this.cmbTransferBitsPerPixel);
			this.tabPageOther.Controls.Add(this.cmbRotation);
			this.tabPageOther.Controls.Add(this.cmbMirror);
			this.tabPageOther.Controls.Add(this.cmbColorInterpolation);
			this.tabPageOther.Controls.Add(this.cmbClockMode);
			this.tabPageOther.Controls.Add(this.cmbScanMode);
			this.tabPageOther.Location = new System.Drawing.Point(4, 22);
			this.tabPageOther.Name = "tabPageOther";
			this.tabPageOther.Size = new System.Drawing.Size(472, 519);
			this.tabPageOther.TabIndex = 5;
			this.tabPageOther.Text = "Other";
			this.tabPageOther.UseVisualStyleBackColor = true;
			// 
			// labelDisplayPixelFormat
			// 
			this.labelDisplayPixelFormat.AutoSize = true;
			this.labelDisplayPixelFormat.Location = new System.Drawing.Point(8, 339);
			this.labelDisplayPixelFormat.Name = "labelDisplayPixelFormat";
			this.labelDisplayPixelFormat.Size = new System.Drawing.Size(102, 12);
			this.labelDisplayPixelFormat.TabIndex = 28;
			this.labelDisplayPixelFormat.Text = "Display Pixel Format";
			// 
			// cmbDisplayPixelFormat
			// 
			this.cmbDisplayPixelFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDisplayPixelFormat.Location = new System.Drawing.Point(120, 335);
			this.cmbDisplayPixelFormat.Name = "cmbDisplayPixelFormat";
			this.cmbDisplayPixelFormat.Size = new System.Drawing.Size(224, 20);
			this.cmbDisplayPixelFormat.StValue = ((long)(0));
			this.cmbDisplayPixelFormat.TabIndex = 29;
			this.cmbDisplayPixelFormat.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// lblRegionMode
			// 
			this.lblRegionMode.AutoSize = true;
			this.lblRegionMode.Location = new System.Drawing.Point(240, 116);
			this.lblRegionMode.Name = "lblRegionMode";
			this.lblRegionMode.Size = new System.Drawing.Size(69, 12);
			this.lblRegionMode.TabIndex = 12;
			this.lblRegionMode.Text = "Region Mode";
			// 
			// cmbRegionMode
			// 
			this.cmbRegionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbRegionMode.Location = new System.Drawing.Point(352, 106);
			this.cmbRegionMode.Name = "cmbRegionMode";
			this.cmbRegionMode.Size = new System.Drawing.Size(104, 20);
			this.cmbRegionMode.StValue = ((long)(0));
			this.cmbRegionMode.TabIndex = 13;
			this.cmbRegionMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// lblCurrentRegion
			// 
			this.lblCurrentRegion.AutoSize = true;
			this.lblCurrentRegion.Location = new System.Drawing.Point(8, 119);
			this.lblCurrentRegion.Name = "lblCurrentRegion";
			this.lblCurrentRegion.Size = new System.Drawing.Size(78, 12);
			this.lblCurrentRegion.TabIndex = 10;
			this.lblCurrentRegion.Text = "Current Region";
			// 
			// cmbCurrentRegion
			// 
			this.cmbCurrentRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCurrentRegion.Location = new System.Drawing.Point(120, 109);
			this.cmbCurrentRegion.Name = "cmbCurrentRegion";
			this.cmbCurrentRegion.Size = new System.Drawing.Size(104, 20);
			this.cmbCurrentRegion.StValue = ((long)(0));
			this.cmbCurrentRegion.TabIndex = 11;
			this.cmbCurrentRegion.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbVBinningSumMode
			// 
			this.cmbVBinningSumMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbVBinningSumMode.Location = new System.Drawing.Point(352, 74);
			this.cmbVBinningSumMode.Name = "cmbVBinningSumMode";
			this.cmbVBinningSumMode.Size = new System.Drawing.Size(104, 20);
			this.cmbVBinningSumMode.StValue = ((long)(0));
			this.cmbVBinningSumMode.TabIndex = 9;
			this.cmbVBinningSumMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// label31
			// 
			this.label31.AutoSize = true;
			this.label31.Location = new System.Drawing.Point(240, 78);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(78, 12);
			this.label31.TabIndex = 8;
			this.label31.Text = "V Binning Sum";
			// 
			// label28
			// 
			this.label28.AutoSize = true;
			this.label28.Location = new System.Drawing.Point(8, 78);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(78, 12);
			this.label28.TabIndex = 6;
			this.label28.Text = "H Binning Sum";
			// 
			// cmbHBinningSumMode
			// 
			this.cmbHBinningSumMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbHBinningSumMode.Location = new System.Drawing.Point(120, 74);
			this.cmbHBinningSumMode.Name = "cmbHBinningSumMode";
			this.cmbHBinningSumMode.Size = new System.Drawing.Size(104, 20);
			this.cmbHBinningSumMode.StValue = ((long)(0));
			this.cmbHBinningSumMode.TabIndex = 7;
			this.cmbHBinningSumMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// labelOutputFPS
			// 
			this.labelOutputFPS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelOutputFPS.Location = new System.Drawing.Point(352, 421);
			this.labelOutputFPS.Name = "labelOutputFPS";
			this.labelOutputFPS.Size = new System.Drawing.Size(72, 30);
			this.labelOutputFPS.TabIndex = 35;
			this.labelOutputFPS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarVBlankForFPS
			// 
			this.trackBarVBlankForFPS.AutoSize = false;
			this.trackBarVBlankForFPS.LargeChange = 1000;
			this.trackBarVBlankForFPS.Location = new System.Drawing.Point(112, 376);
			this.trackBarVBlankForFPS.Name = "trackBarVBlankForFPS";
			this.trackBarVBlankForFPS.Size = new System.Drawing.Size(224, 30);
			this.trackBarVBlankForFPS.TabIndex = 31;
			this.trackBarVBlankForFPS.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarVBlankForFPS.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(8, 386);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(82, 12);
			this.label18.TabIndex = 30;
			this.label18.Text = "V Blank for FPS";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(240, 45);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(99, 12);
			this.label17.TabIndex = 4;
			this.label17.Text = "V Binning Skipping";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(8, 45);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(99, 12);
			this.label16.TabIndex = 2;
			this.label16.Text = "H Binning Skipping";
			// 
			// trackBarImageHeight
			// 
			this.trackBarImageHeight.AutoSize = false;
			this.trackBarImageHeight.LargeChange = 1000;
			this.trackBarImageHeight.Location = new System.Drawing.Point(112, 262);
			this.trackBarImageHeight.Name = "trackBarImageHeight";
			this.trackBarImageHeight.Size = new System.Drawing.Size(224, 30);
			this.trackBarImageHeight.TabIndex = 24;
			this.trackBarImageHeight.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarImageHeight.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(8, 272);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(36, 12);
			this.label14.TabIndex = 23;
			this.label14.Text = "Height";
			// 
			// trackBarImageWidth
			// 
			this.trackBarImageWidth.AutoSize = false;
			this.trackBarImageWidth.LargeChange = 1000;
			this.trackBarImageWidth.Location = new System.Drawing.Point(112, 222);
			this.trackBarImageWidth.Name = "trackBarImageWidth";
			this.trackBarImageWidth.Size = new System.Drawing.Size(224, 30);
			this.trackBarImageWidth.TabIndex = 21;
			this.trackBarImageWidth.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarImageWidth.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(8, 232);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(34, 12);
			this.label15.TabIndex = 20;
			this.label15.Text = "Width";
			// 
			// trackBarImageOffsetY
			// 
			this.trackBarImageOffsetY.AutoSize = false;
			this.trackBarImageOffsetY.LargeChange = 1000;
			this.trackBarImageOffsetY.Location = new System.Drawing.Point(112, 185);
			this.trackBarImageOffsetY.Name = "trackBarImageOffsetY";
			this.trackBarImageOffsetY.Size = new System.Drawing.Size(224, 30);
			this.trackBarImageOffsetY.TabIndex = 18;
			this.trackBarImageOffsetY.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarImageOffsetY.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(8, 195);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(41, 12);
			this.label12.TabIndex = 17;
			this.label12.Text = "OffsetY";
			// 
			// trackBarImageOffsetX
			// 
			this.trackBarImageOffsetX.AutoSize = false;
			this.trackBarImageOffsetX.LargeChange = 1000;
			this.trackBarImageOffsetX.Location = new System.Drawing.Point(112, 145);
			this.trackBarImageOffsetX.Name = "trackBarImageOffsetX";
			this.trackBarImageOffsetX.Size = new System.Drawing.Size(224, 30);
			this.trackBarImageOffsetX.TabIndex = 15;
			this.trackBarImageOffsetX.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackBarImageOffsetX.Scroll += new System.EventHandler(this.trackBar_Scroll);
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(8, 155);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(41, 12);
			this.label13.TabIndex = 14;
			this.label13.Text = "OffsetX";
			// 
			// lblTransferBitsPerPixel
			// 
			this.lblTransferBitsPerPixel.AutoSize = true;
			this.lblTransferBitsPerPixel.Location = new System.Drawing.Point(8, 306);
			this.lblTransferBitsPerPixel.Name = "lblTransferBitsPerPixel";
			this.lblTransferBitsPerPixel.Size = new System.Drawing.Size(91, 12);
			this.lblTransferBitsPerPixel.TabIndex = 26;
			this.lblTransferBitsPerPixel.Text = "Transfer Bits/Pixel";
			// 
			// lblRotation
			// 
			this.lblRotation.AutoSize = true;
			this.lblRotation.Location = new System.Drawing.Point(8, 546);
			this.lblRotation.Name = "lblRotation";
			this.lblRotation.Size = new System.Drawing.Size(45, 12);
			this.lblRotation.TabIndex = 40;
			this.lblRotation.Text = "Rotation";
			// 
			// lblMirror
			// 
			this.lblMirror.AutoSize = true;
			this.lblMirror.Location = new System.Drawing.Point(8, 516);
			this.lblMirror.Name = "lblMirror";
			this.lblMirror.Size = new System.Drawing.Size(36, 12);
			this.lblMirror.TabIndex = 38;
			this.lblMirror.Text = "Mirror";
			// 
			// lblColorInterpolation
			// 
			this.lblColorInterpolation.AutoSize = true;
			this.lblColorInterpolation.Location = new System.Drawing.Point(8, 476);
			this.lblColorInterpolation.Name = "lblColorInterpolation";
			this.lblColorInterpolation.Size = new System.Drawing.Size(95, 12);
			this.lblColorInterpolation.TabIndex = 36;
			this.lblColorInterpolation.Text = "Color Interpolation";
			// 
			// lblClockMode
			// 
			this.lblClockMode.AutoSize = true;
			this.lblClockMode.Location = new System.Drawing.Point(8, 436);
			this.lblClockMode.Name = "lblClockMode";
			this.lblClockMode.Size = new System.Drawing.Size(33, 12);
			this.lblClockMode.TabIndex = 33;
			this.lblClockMode.Text = "Clock";
			// 
			// lblScanMode
			// 
			this.lblScanMode.AutoSize = true;
			this.lblScanMode.Location = new System.Drawing.Point(8, 20);
			this.lblScanMode.Name = "lblScanMode";
			this.lblScanMode.Size = new System.Drawing.Size(57, 12);
			this.lblScanMode.TabIndex = 0;
			this.lblScanMode.Text = "Scan Mode";
			// 
			// stTextBoxVBlankForFPS
			// 
			this.stTextBoxVBlankForFPS.Location = new System.Drawing.Point(344, 376);
			this.stTextBoxVBlankForFPS.Name = "stTextBoxVBlankForFPS";
			this.stTextBoxVBlankForFPS.Size = new System.Drawing.Size(64, 22);
			this.stTextBoxVBlankForFPS.TabIndex = 32;
			this.stTextBoxVBlankForFPS.Text = "stTextBox17";
			this.stTextBoxVBlankForFPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.stTextBoxVBlankForFPS.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// stComboBoxVBinningSkipping
			// 
			this.stComboBoxVBinningSkipping.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stComboBoxVBinningSkipping.Location = new System.Drawing.Point(352, 41);
			this.stComboBoxVBinningSkipping.Name = "stComboBoxVBinningSkipping";
			this.stComboBoxVBinningSkipping.Size = new System.Drawing.Size(104, 20);
			this.stComboBoxVBinningSkipping.StValue = ((long)(0));
			this.stComboBoxVBinningSkipping.TabIndex = 5;
			this.stComboBoxVBinningSkipping.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// stComboBoxHBinningSkipping
			// 
			this.stComboBoxHBinningSkipping.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.stComboBoxHBinningSkipping.Location = new System.Drawing.Point(120, 41);
			this.stComboBoxHBinningSkipping.Name = "stComboBoxHBinningSkipping";
			this.stComboBoxHBinningSkipping.Size = new System.Drawing.Size(104, 20);
			this.stComboBoxHBinningSkipping.StValue = ((long)(0));
			this.stComboBoxHBinningSkipping.TabIndex = 3;
			this.stComboBoxHBinningSkipping.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// stTextBoxImageHeight
			// 
			this.stTextBoxImageHeight.Location = new System.Drawing.Point(344, 262);
			this.stTextBoxImageHeight.Name = "stTextBoxImageHeight";
			this.stTextBoxImageHeight.Size = new System.Drawing.Size(64, 22);
			this.stTextBoxImageHeight.TabIndex = 25;
			this.stTextBoxImageHeight.Text = "stTextBox18";
			this.stTextBoxImageHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.stTextBoxImageHeight.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// stTextBoxImageWidth
			// 
			this.stTextBoxImageWidth.Location = new System.Drawing.Point(344, 222);
			this.stTextBoxImageWidth.Name = "stTextBoxImageWidth";
			this.stTextBoxImageWidth.Size = new System.Drawing.Size(64, 22);
			this.stTextBoxImageWidth.TabIndex = 22;
			this.stTextBoxImageWidth.Text = "stTextBox17";
			this.stTextBoxImageWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.stTextBoxImageWidth.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// stTextBoxImageOffsetY
			// 
			this.stTextBoxImageOffsetY.Location = new System.Drawing.Point(344, 185);
			this.stTextBoxImageOffsetY.Name = "stTextBoxImageOffsetY";
			this.stTextBoxImageOffsetY.Size = new System.Drawing.Size(64, 22);
			this.stTextBoxImageOffsetY.TabIndex = 19;
			this.stTextBoxImageOffsetY.Text = "stTextBox18";
			this.stTextBoxImageOffsetY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.stTextBoxImageOffsetY.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// stTextBoxImageOffsetX
			// 
			this.stTextBoxImageOffsetX.Location = new System.Drawing.Point(344, 145);
			this.stTextBoxImageOffsetX.Name = "stTextBoxImageOffsetX";
			this.stTextBoxImageOffsetX.Size = new System.Drawing.Size(64, 22);
			this.stTextBoxImageOffsetX.TabIndex = 16;
			this.stTextBoxImageOffsetX.Text = "stTextBox17";
			this.stTextBoxImageOffsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.stTextBoxImageOffsetX.TextEntered += new StCtlLib.TextEnteredEventHandler(this.stTextBox_TextEntered);
			// 
			// cmbTransferBitsPerPixel
			// 
			this.cmbTransferBitsPerPixel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTransferBitsPerPixel.Location = new System.Drawing.Point(120, 302);
			this.cmbTransferBitsPerPixel.Name = "cmbTransferBitsPerPixel";
			this.cmbTransferBitsPerPixel.Size = new System.Drawing.Size(224, 20);
			this.cmbTransferBitsPerPixel.StValue = ((long)(0));
			this.cmbTransferBitsPerPixel.TabIndex = 27;
			this.cmbTransferBitsPerPixel.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbRotation
			// 
			this.cmbRotation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbRotation.Location = new System.Drawing.Point(120, 546);
			this.cmbRotation.Name = "cmbRotation";
			this.cmbRotation.Size = new System.Drawing.Size(224, 20);
			this.cmbRotation.StValue = ((long)(0));
			this.cmbRotation.TabIndex = 41;
			this.cmbRotation.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbMirror
			// 
			this.cmbMirror.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbMirror.Location = new System.Drawing.Point(120, 506);
			this.cmbMirror.Name = "cmbMirror";
			this.cmbMirror.Size = new System.Drawing.Size(224, 20);
			this.cmbMirror.StValue = ((long)(0));
			this.cmbMirror.TabIndex = 39;
			this.cmbMirror.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbColorInterpolation
			// 
			this.cmbColorInterpolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbColorInterpolation.Location = new System.Drawing.Point(120, 466);
			this.cmbColorInterpolation.Name = "cmbColorInterpolation";
			this.cmbColorInterpolation.Size = new System.Drawing.Size(224, 20);
			this.cmbColorInterpolation.StValue = ((long)(0));
			this.cmbColorInterpolation.TabIndex = 37;
			this.cmbColorInterpolation.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbClockMode
			// 
			this.cmbClockMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbClockMode.Location = new System.Drawing.Point(120, 426);
			this.cmbClockMode.Name = "cmbClockMode";
			this.cmbClockMode.Size = new System.Drawing.Size(224, 20);
			this.cmbClockMode.StValue = ((long)(0));
			this.cmbClockMode.TabIndex = 34;
			this.cmbClockMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// cmbScanMode
			// 
			this.cmbScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbScanMode.Location = new System.Drawing.Point(120, 10);
			this.cmbScanMode.Name = "cmbScanMode";
			this.cmbScanMode.Size = new System.Drawing.Size(224, 20);
			this.cmbScanMode.StValue = ((long)(0));
			this.cmbScanMode.TabIndex = 1;
			this.cmbScanMode.SelectionChangeCommitted += new System.EventHandler(this.comboBox_SelectionChangeCommitted);
			// 
			// panelButton
			// 
			this.panelButton.Controls.Add(this.btnRefresh);
			this.panelButton.Controls.Add(this.btnLoad);
			this.panelButton.Controls.Add(this.btnSave);
			this.panelButton.Controls.Add(this.btnOK);
			this.panelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelButton.Location = new System.Drawing.Point(0, 545);
			this.panelButton.Name = "panelButton";
			this.panelButton.Size = new System.Drawing.Size(480, 50);
			this.panelButton.TabIndex = 1;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Location = new System.Drawing.Point(384, 10);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(88, 30);
			this.btnRefresh.TabIndex = 3;
			this.btnRefresh.Text = "&Refresh";
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(288, 10);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(88, 30);
			this.btnLoad.TabIndex = 2;
			this.btnLoad.Text = "&Load...";
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(192, 10);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(88, 30);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "&Save...";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(96, 10);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(88, 30);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// SettingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
			this.ClientSize = new System.Drawing.Size(480, 595);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.panelButton);
			this.Name = "SettingForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Setting";
			this.Load += new System.EventHandler(this.SettingForm_Load);
			this.tabControl.ResumeLayout(false);
			this.tabPageShutterGain.ResumeLayout(false);
			this.tabPageShutterGain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarAGCMaxGain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarAGCMinGain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarAEMaxExposure)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarAEMinExposure)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarALCTarget)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarDigitalGain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarGain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarExposure)).EndInit();
			this.tabPageWB.ResumeLayout(false);
			this.tabPageWB.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarLowChromaSuppresionSuppressionLevel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarLowChromaSuppresionStartLevel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarChromaSuppresionSuppressionLevel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarChromaSuppresionStartLevel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSaturation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarHue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarWBBGain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarWBGbGain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarWBGrGain)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarWBRGain)).EndInit();
			this.tabPageY.ResumeLayout(false);
			this.tabPageY.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarAnalogBlackLevel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarShadingCorrectionTarget)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarDigitalClamp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarCameraGamma)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarYGamma)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSharpnessCoring)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarSharpnessGain)).EndInit();
			this.tabPageHDR_CMOSIS4M.ResumeLayout(false);
			this.tabPageHDR_CMOSIS4M.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarHDR_CMOSIS4M_Vlow3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarHDR_CMOSIS4M_Knee2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarHDR_CMOSIS4M_Vlow2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarHDR_CMOSIS4M_Knee1)).EndInit();
			this.tabPageColorGamma.ResumeLayout(false);
			this.tabPageColorGamma.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarBGamma)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarGBGamma)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarGRGamma)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarRGamma)).EndInit();
			this.tabPageTriggerMode.ResumeLayout(false);
			this.tabPageTriggerMode.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarTriggerDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarAutoTriggerTime)).EndInit();
			this.tabPageIO.ResumeLayout(false);
			this.tabPageIO.PerformLayout();
			this.tabPageTriggerTiming.ResumeLayout(false);
			this.tabPageTriggerTiming.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarLineDebounceTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarReadOutDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarOutputPulseDuration)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarOutputPulseDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarStrobeEndDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarStrobeStartDelay)).EndInit();
			this.tabPageDefectPixelCorrection.ResumeLayout(false);
			this.tabPageDefectPixelCorrection.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDefectPixelThreshold)).EndInit();
			this.tabPageEEPROM.ResumeLayout(false);
			this.tabPageOther.ResumeLayout(false);
			this.tabPageOther.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarVBlankForFPS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarImageHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarImageWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarImageOffsetY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarImageOffsetX)).EndInit();
			this.panelButton.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void trackBar_Scroll(object sender, System.EventArgs e)
		{
			TrackBar trackBar = (TrackBar)sender;
			if(sender.Equals(trackBarExposure))
			{
				m_objStCamera.ExposureClock = (uint)trackBar.Value;
			}
			else if(sender.Equals(trackBarGain))
			{
				m_objStCamera.Gain = (ushort)trackBar.Value;
			}
			else if(sender.Equals(trackBarDigitalGain))
			{
				m_objStCamera.DigitalGain = (ushort)trackBar.Value;
			}
			else if (sender.Equals(trackBarALCTarget))
			{
				m_objStCamera.ALCTargetLevel = (ushort)trackBar.Value;
			}
			else if (sender.Equals(trackBarAGCMaxGain))
			{
				m_objStCamera.AGCMaxGain = (ushort)trackBar.Value;
			}
			else if (sender.Equals(trackBarAGCMinGain))
			{
				m_objStCamera.AGCMinGain = (ushort)trackBar.Value;
			}
			else if (sender.Equals(trackBarAEMaxExposure))
			{
				m_objStCamera.AEMaxExposureClock = (uint)trackBar.Value;
			}

			else if (sender.Equals(trackBarAEMinExposure))
			{
				m_objStCamera.AEMinExposureClock = (uint)trackBar.Value;
            }
            else if (trackBar.Equals(trackBarHDR_CMOSIS4M_Knee1))
            {
                m_objStCamera.HDR_CMOSIS4M_Knee1 = (byte)trackBar.Value;
            }
            else if (trackBar.Equals(trackBarHDR_CMOSIS4M_Vlow2))
            {
                m_objStCamera.HDR_CMOSIS4M_Vlow2 = (byte)trackBar.Value;
            }
            else if (trackBar.Equals(trackBarHDR_CMOSIS4M_Knee2))
            {
                m_objStCamera.HDR_CMOSIS4M_Knee2 = (byte)trackBar.Value;
            }
            else if (trackBar.Equals(trackBarHDR_CMOSIS4M_Vlow3))
            {
                m_objStCamera.HDR_CMOSIS4M_Vlow3 = (byte)trackBar.Value;
            }
			else if (sender.Equals(trackBarImageOffsetX))
			{
				uint value = (uint)trackBar.Value;
				value &= 0xFFFFFFFE;
				m_objStCamera.ImageOffsetX = value;
			}
			else if (sender.Equals(trackBarImageOffsetY))
			{
				uint value = (uint)trackBar.Value;
				value &= 0xFFFFFFFE;
				m_objStCamera.ImageOffsetY = value;
			}
			else if (sender.Equals(trackBarImageWidth))
			{
				uint value = (uint)trackBar.Value;
				value &= 0xFFFFFFFC;
				m_objStCamera.ImageWidth = value;
			}
			else if (sender.Equals(trackBarImageHeight))
			{
				uint value = (uint)trackBar.Value;
				value &= 0xFFFFFFFC;
				m_objStCamera.ImageHeight = value;
			}
			else if (sender.Equals(trackBarVBlankForFPS))
			{
				uint value = (uint)trackBar.Value;
				m_objStCamera.VBlankForFPS = value;
			}
			else if(sender.Equals(trackBarSharpnessGain))
			{
				m_objStCamera.SharpnessGain = (ushort)trackBar.Value;
			}
			else if(sender.Equals(trackBarSharpnessCoring))
			{
				m_objStCamera.SharpnessCoring = (byte)trackBar.Value;
            }
            else if (sender.Equals(trackBarYGamma))
            {
                m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_Y] = (ushort)trackBar.Value;
            }
            else if (sender.Equals(trackBarRGamma))
            {
                m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_R] = (ushort)trackBar.Value;
            }
            else if (sender.Equals(trackBarGRGamma))
            {
                m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_GR] = (ushort)trackBar.Value;
            }
            else if (sender.Equals(trackBarGBGamma))
            {
                m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_GB] = (ushort)trackBar.Value;
            }
            else if (sender.Equals(trackBarBGamma))
            {
                m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_B] = (ushort)trackBar.Value;
            }
			else if(sender.Equals(trackBarWBRGain))
			{
				m_objStCamera.WBRGain = (ushort)trackBar.Value;
			}
			else if(sender.Equals(trackBarWBGrGain))
			{
				m_objStCamera.WBGrGain = (ushort)trackBar.Value;
			}
			else if(sender.Equals(trackBarWBGbGain))
			{
				m_objStCamera.WBGbGain = (ushort)trackBar.Value;
			}
			else if(sender.Equals(trackBarWBBGain))
			{
				m_objStCamera.WBBGain = (ushort)trackBar.Value;
			}
			else if(sender.Equals(trackBarHue))
			{
				m_objStCamera.Hue = (short)trackBar.Value;
			}
			else if(sender.Equals(trackBarSaturation))
			{
				m_objStCamera.Saturation = (ushort)trackBar.Value;
            }
            else if (sender.Equals(trackBarChromaSuppresionStartLevel))
            {
                m_objStCamera.HighChromaSuppressionStartLevel = (ushort)trackBar.Value;
            }
            else if (sender.Equals(trackBarChromaSuppresionSuppressionLevel))
            {
                m_objStCamera.HighChromaSuppressionSuppressionLevel = (ushort)trackBar.Value;
            }
            else if (sender.Equals(trackBarLowChromaSuppresionStartLevel))
            {
                m_objStCamera.LowChromaSuppressionStartLevel = (ushort)trackBar.Value;
            }
            else if (sender.Equals(trackBarLowChromaSuppresionSuppressionLevel))
            {
                m_objStCamera.LowChromaSuppressionSuppressionLevel = (ushort)trackBar.Value;
            }
			else if(sender.Equals(trackBarAutoTriggerTime))
			{
				m_objStCamera.AutoTriggerDueTime = trackBar.Value;
            }
            else if (sender.Equals(trackBarTriggerDelay))
            {
                m_objStCamera.TriggerDelay = (uint)trackBar.Value;
            }

			else if(sender.Equals(trackBarStrobeStartDelay))
			{
				m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_STROBE_START_DELAY] = (uint)trackBar.Value;
			}
			else if(sender.Equals(trackBarStrobeEndDelay))
			{
				m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_STROBE_END_DELAY] = (uint)trackBar.Value;
			}
			else if(sender.Equals(trackBarOutputPulseDelay))
			{
				m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_OUTPUT_PULSE_DELAY] = (uint)trackBar.Value;
			}
			else if(sender.Equals(trackBarOutputPulseDuration))
			{
				m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_OUTPUT_PULSE_DURATION] = (uint)trackBar.Value;
			}
			else if(sender.Equals(trackBarReadOutDelay))
			{
				m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_READOUT_DELAY] = (uint)trackBar.Value;
            }
            else if (sender.Equals(trackBarLineDebounceTime))
            {
                m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_LINE_DEBOUNCE_TIME] = (uint)trackBar.Value;
            }
			else if (tabControl.SelectedTab.Equals(tabPageDefectPixelCorrection))
			{
				for (int i = 0; i < m_TrackBarList.Length; i++)
				{
					if (sender.Equals(m_TrackBarList[i]))
					{
						ushort index = (ushort)(i / 2);
						uint x = 0;
						uint y = 0;
						m_objStCamera.GetDefectPixelCorrectionPosition(index, out x, out y);
						if ((i % 2) == 0) x = (uint)trackBar.Value;
						else y = (uint)trackBar.Value;
						m_objStCamera.SetDefectPixelCorrectionPosition(index, x, y);
					}
				}
			}
			else if (sender.Equals(trackBarCameraGamma))
			{
				m_objStCamera.CameraGamma = (ushort)trackBar.Value;
			}
			else if (sender.Equals(trackBarDigitalClamp))
			{
				m_objStCamera.DigitalClamp = (ushort)trackBar.Value;
            }
            else if (sender.Equals(trackBarAnalogBlackLevel))
            {
                m_objStCamera.AnalogBlackLevel = (ushort)trackBar.Value;
            }
            else if (sender.Equals(trackBarShadingCorrectionTarget))
            {
                m_objStCamera.ShadingCorrectionTarget = (ushort)trackBar.Value;
            }


			UpdateDisplay();
		}
		private void comboBox_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			StComboBox comboBox = (StComboBox)sender;

			if (sender.Equals(cmbALCMode))
			{
				m_objStCamera.ALCMode = (byte)comboBox.StValue;
            }
            else if (comboBox.Equals(cmbAdjustmentDigitalGain))
            {
                m_objStCamera.EnableAdjustmentDigitalGain = (0 != comboBox.StValue);
            }
            else if (comboBox.Equals(cmbHDR_CMOSIS4M_Mode))
            {
                m_objStCamera.HDR_CMOSIS4M_Mode = (byte)comboBox.StValue;
            }
            else if (comboBox.Equals(cmbHDR_CMOSIS4M_SlopeNum))
            {
                m_objStCamera.HDR_CMOSIS4M_SlopeNum = (byte)comboBox.StValue;
            }

			else if (sender.Equals(cmbWBMode))
			{
				m_objStCamera.WhiteBalanceMode = (byte)comboBox.StValue;
			}
			else if(sender.Equals(cmbHueSaturationMode))
			{
				m_objStCamera.HueSaturationMode = (byte)comboBox.StValue;
			}
			else if(sender.Equals(cmbTriggerMode))
            {
                m_objStCamera.TriggerMode = (UInt32)comboBox.StValue;
			}
			else if(sender.Equals(cmbTriggerSource))
            {
                m_objStCamera.TriggerSource = (UInt32)comboBox.StValue;
			}
			else if(sender.Equals(cmbExposureMode))
            {
                m_objStCamera.ExposureMode = (UInt32)comboBox.StValue;
            }
            else if (sender.Equals(cmbTriggerSelector))
            {
                m_objStCamera.TriggerSelector = (UInt32)comboBox.StValue;
            }
			else if(sender.Equals(cmbNoiseReduction))
			{
				m_objStCamera.NoiseRedutionMode = (uint)comboBox.StValue;
			}
			else if(sender.Equals(cmbExposureWaitHD))
			{
				m_objStCamera.ExposureWaitHD = (uint)comboBox.StValue;
			}
			else if(sender.Equals(cmbExposureWaitReadOut))
			{
				m_objStCamera.ExposureWaitReadOut = (uint)comboBox.StValue;
			}

			else if(sender.Equals(cmbExposureEnd))
			{
				m_objStCamera.ExposureEnd = (uint)comboBox.StValue;
			}
			else if(sender.Equals(cmbCameraMemory))
			{
				m_objStCamera.CameraMemory = (uint)comboBox.StValue;
			}
			else if (sender.Equals(cmbTriggerOverlap))
            {
                m_objStCamera.TriggerOverlap = (uint)comboBox.StValue;
			}
			else if (sender.Equals(cmbSensorShutterMode))
			{
                m_objStCamera.SensorShutterMode = (uint)comboBox.StValue;
			}
			else if(sender.Equals(cmbScanMode))
			{
				m_objStCamera.ScanMode = (ushort)comboBox.StValue;
			}
			else if(sender.Equals(cmbClockMode))
			{
				m_objStCamera.ClockMode = (uint)comboBox.StValue;
			}
			else if(sender.Equals(cmbSharpnessMode))
			{
				m_objStCamera.SharpnessMode = (byte)comboBox.StValue;
            }
            else if (sender.Equals(cmbShadingCorrectionMode))
            {
                m_objStCamera.ShadingCorrectionMode = (uint)comboBox.StValue;
            }
            else if (sender.Equals(cmbYGammaMode))
            {
                m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_Y] = (byte)comboBox.StValue;
            }
            else if (sender.Equals(cmbRGammaMode))
            {
                m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_R] = (byte)comboBox.StValue;
            }
            else if (sender.Equals(cmbGRGammaMode))
            {
                m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_GR] = (byte)comboBox.StValue;
            }
            else if (sender.Equals(cmbGBGammaMode))
            {
                m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_GB] = (byte)comboBox.StValue;
            }
            else if (sender.Equals(cmbBGammaMode))
            {
                m_objStCamera.GammaMode[StTrg.STCAM_GAMMA_TARGET_B] = (byte)comboBox.StValue;
            }
            else if (sender.Equals(cmbColorInterpolation))
            {
                m_objStCamera.ColorInterpolationMode = (byte)comboBox.StValue;
            }
            else if (sender.Equals(cmbMirror))
            {
                m_objStCamera.MirrorMode = (byte)comboBox.StValue;
            }
            else if (sender.Equals(cmbRotation))
            {
                m_objStCamera.RotationMode = (byte)comboBox.StValue;
            }
            else if (
                sender.Equals(cmbIOInOut0) ||
                sender.Equals(cmbIOInOut1) ||
                sender.Equals(cmbIOInOut2) ||
                sender.Equals(cmbIOInOut3)
                )
            {
                byte ioNo = 0;
                if (sender.Equals(cmbIOInOut1)) ioNo = 1;
                else if (sender.Equals(cmbIOInOut2)) ioNo = 2;
                else if (sender.Equals(cmbIOInOut3)) ioNo = 3;
                m_objStCamera.IOPinInOut[ioNo] = (uint)comboBox.StValue;
            }
            else if (
                sender.Equals(cmbIOMode0) ||
                sender.Equals(cmbIOMode1) ||
                sender.Equals(cmbIOMode2) ||
                sender.Equals(cmbIOMode3)
                )
            {
                byte ioNo = 0;
                if (sender.Equals(cmbIOMode1)) ioNo = 1;
                else if (sender.Equals(cmbIOMode2)) ioNo = 2;
                else if (sender.Equals(cmbIOMode3)) ioNo = 3;
                m_objStCamera.IOPinMode[ioNo] = (uint)comboBox.StValue;
            }
            else if (
                sender.Equals(cmbIOPolarity0) ||
                sender.Equals(cmbIOPolarity1) ||
                sender.Equals(cmbIOPolarity2) ||
                sender.Equals(cmbIOPolarity3)
                )
            {
                byte ioNo = 0;
                if (sender.Equals(cmbIOPolarity1)) ioNo = 1;
                else if (sender.Equals(cmbIOPolarity2)) ioNo = 2;
                else if (sender.Equals(cmbIOPolarity3)) ioNo = 3;
                m_objStCamera.IOPinPolarity[ioNo] = (uint)comboBox.StValue;
            }
            else if (comboBox.Equals(cmbResetSwitch))
            {
                m_objStCamera.ResetSwitchEnabled = comboBox.StValue;
            }
            else if (sender.Equals(cmbLEDGreen))
            {
                m_objStCamera.LEDGreen = (uint)comboBox.StValue;
            }
            else if (sender.Equals(cmbLEDRed))
            {
                m_objStCamera.LEDRed = (uint)comboBox.StValue;
            }
            else if (
                sender.Equals(cmbIOStatus0) ||
                sender.Equals(cmbIOStatus1) ||
                sender.Equals(cmbIOStatus2) ||
                sender.Equals(cmbIOStatus3)
                )
            {
                byte ioNo = 0;
                if (sender.Equals(cmbIOStatus1)) ioNo = 1;
                else if (sender.Equals(cmbIOStatus2)) ioNo = 2;
                else if (sender.Equals(cmbIOStatus3)) ioNo = 3;
                m_objStCamera.IOPinStatus[ioNo] = (uint)comboBox.StValue;
			}
			else if (comboBox.Equals(stComboBoxHBinningSkipping))
			{
				m_objStCamera.HBinningSkipping = (ushort)comboBox.StValue;
			}
			else if (comboBox.Equals(stComboBoxVBinningSkipping))
			{
				m_objStCamera.VBinningSkipping = (ushort)comboBox.StValue;
			}
			else if (sender.Equals(cmbTransferBitsPerPixel))
			{
				m_objStCamera.TransferBitsPerPixel = (uint)comboBox.StValue;
            }
            else if (sender.Equals(cmbDisplayPixelFormat))
            {
                m_objStCamera.StPixelFormat = (uint)comboBox.StValue;
            }
			else if (comboBox.Equals(cmbDefectPixelCorrectionMode))
			{
				m_objStCamera.DefectPixelCorrectionMode = (ushort)comboBox.StValue;
			}

			else if (comboBox.Equals(cmbHBinningSumMode))
			{
				int value = m_objStCamera.BinningSumMode & 0xFF00;
				value |= (ushort)comboBox.StValue;
				m_objStCamera.BinningSumMode = (ushort)value;
			}
			else if (comboBox.Equals(cmbVBinningSumMode))
			{
				int value = m_objStCamera.BinningSumMode & 0x00FF;
				value |= (ushort)comboBox.StValue;
				m_objStCamera.BinningSumMode = (ushort)value;
            }
            else if (sender.Equals(cmbCurrentRegion))
            {
                m_objStCamera.CurrentRegion = (uint)comboBox.StValue;
            }
            else if (sender.Equals(cmbRegionMode))
            {
                m_objStCamera.RegionMode = (comboBox.StValue != 0);
            }


			UpdateDisplay();
		
		}

		private void tabControl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//System.Console.WriteLine("Tab = " + ((TabControl)sender).SelectedIndex);
			UpdateControlList();
			UpdateDisplay();
		}

		private void SettingForm_Load(object sender, System.EventArgs e)
		{
			if(!m_objStCamera.HasTriggerFunction())
			{
				if(tabControl.TabPages.Contains(tabPageTriggerMode))
				{
					tabControl.TabPages.Remove(tabPageTriggerMode);
				}
				if(tabControl.TabPages.Contains(tabPageTriggerTiming))
				{
					tabControl.TabPages.Remove(tabPageTriggerTiming);
				}
				if(tabControl.TabPages.Contains(tabPageIO))
				{
					tabControl.TabPages.Remove(tabPageIO);
				}
			}
			if(m_objStCamera.ColorArray == StTrg.STCAM_COLOR_ARRAY_MONO)
			{
                if (tabControl.TabPages.Contains(tabPageColorGamma))
				{
                    tabControl.TabPages.Remove(tabPageColorGamma);
				}

                if (tabControl.TabPages.Contains(tabPageWB))
                {
                    tabControl.TabPages.Remove(tabPageWB);
                }
            }

            if (m_objStCamera.HDRType != StTrg.STCAM_HDR_TYPE_CMOSIS_4M)
            {
                if (tabControl.TabPages.Contains(tabPageHDR_CMOSIS4M))
                {
                    tabControl.TabPages.Remove(tabPageHDR_CMOSIS4M);
                }
            }
			if (!m_objStCamera.HasStoreCameraSetting())
			{
				if (tabControl.TabPages.Contains(tabPageEEPROM))
				{
					tabControl.TabPages.Remove(tabPageEEPROM);
				}
			}
			if (m_objStCamera.EnableDefectPixelCorrectionCount < 1)
			{
				if (tabControl.TabPages.Contains(tabPageDefectPixelCorrection))
				{
					tabControl.TabPages.Remove(tabPageDefectPixelCorrection);
				}
			}
			else
			{
				defectPixelSetting1.Count = m_objStCamera.EnableDefectPixelCorrectionCount;
			}
			UpdateControlList();
			UpdateDisplay();		
		}

		private void stTextBox_TextEntered(object sender, StCtlLib.TextEnteredEventArgs e)
		{
			try
			{
				if(sender.Equals(txtExposure))
				{
					uint Value = uint.Parse(e.Text);
					m_objStCamera.ExposureClock = Value;
				}
				else if(sender.Equals(txtGain))
				{
					ushort Value = ushort.Parse(e.Text);
					m_objStCamera.Gain = Value;
				}
				else if(sender.Equals(txtDigitalGain))
				{
					ushort Value = ushort.Parse(e.Text);
					m_objStCamera.DigitalGain = Value;
				}
				else if (sender.Equals(txtALCTarget))
				{
					ushort Value = ushort.Parse(e.Text);
					m_objStCamera.ALCTargetLevel = Value;
				}
				else if (sender.Equals(txtAGCMaxGain))
				{
					ushort Value = ushort.Parse(e.Text);
					m_objStCamera.AGCMaxGain = Value;
				}
				else if (sender.Equals(txtAGCMinGain))
				{
					ushort Value = ushort.Parse(e.Text);
					m_objStCamera.AGCMinGain = Value;
				}
				else if (sender.Equals(txtAEMaxExposure))
				{
					uint Value = uint.Parse(e.Text);
					m_objStCamera.AEMaxExposureClock = Value;
				}
				else if (sender.Equals(txtAEMinExposure))
				{
					uint Value = uint.Parse(e.Text);
					m_objStCamera.AEMinExposureClock = Value;
                }
                else if (sender.Equals(txtHDR_CMOSIS4M_Knee1))
                {
                    byte Value = byte.Parse(e.Text);
                    m_objStCamera.HDR_CMOSIS4M_Knee1 = Value;
                }
                else if (sender.Equals(txtHDR_CMOSIS4M_Vlow2))
                {
                    byte Value = byte.Parse(e.Text);
                    m_objStCamera.HDR_CMOSIS4M_Vlow2 = Value;
                }
                else if (sender.Equals(txtHDR_CMOSIS4M_Knee2))
                {
                    byte Value = byte.Parse(e.Text);
                    m_objStCamera.HDR_CMOSIS4M_Knee2 = Value;
                }
                else if (sender.Equals(txtHDR_CMOSIS4M_Vlow3))
                {
                    byte Value = byte.Parse(e.Text);
                    m_objStCamera.HDR_CMOSIS4M_Vlow3 = Value;
                }
				else if (sender.Equals(stTextBoxImageOffsetX))
				{
					uint value = uint.Parse(e.Text);
					value &= 0xFFFFFFFE;
					m_objStCamera.ImageOffsetX = value;
				}
				else if (sender.Equals(stTextBoxImageOffsetY))
				{
					uint value = uint.Parse(e.Text);
					value &= 0xFFFFFFFE;
					m_objStCamera.ImageOffsetY = value;
				}
				else if (sender.Equals(stTextBoxImageWidth))
				{
					uint value = uint.Parse(e.Text);
					value &= 0xFFFFFFFC;
					m_objStCamera.ImageWidth = value;
				}
				else if (sender.Equals(stTextBoxImageHeight))
				{
					uint value = uint.Parse(e.Text);
					value &= 0xFFFFFFFC;
					m_objStCamera.ImageHeight = value;
				}
				else if (sender.Equals(stTextBoxVBlankForFPS))
				{
					uint value = uint.Parse(e.Text);
					m_objStCamera.VBlankForFPS = value;
				}
				else if(sender.Equals(txtSharpnessGain))
				{
					ushort Value = ushort.Parse(e.Text);
					m_objStCamera.SharpnessGain = Value;
				}
				else if(sender.Equals(txtSharpnessCoring))
				{
					byte Value = byte.Parse(e.Text);
					m_objStCamera.SharpnessCoring = Value;
				}
                else if (sender.Equals(txtYGamma))
                {
                    byte Value = byte.Parse(e.Text);
                    m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_Y] = Value;
                }
                else if (sender.Equals(txtRGamma))
                {
                    byte Value = byte.Parse(e.Text);
                    m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_R] = Value;
                }
                else if (sender.Equals(txtGRGamma))
                {
                    byte Value = byte.Parse(e.Text);
                    m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_GR] = Value;
                }
                else if (sender.Equals(txtGBGamma))
                {
                    byte Value = byte.Parse(e.Text);
                    m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_GB] = Value;
                }
                else if (sender.Equals(txtBGamma))
                {
                    byte Value = byte.Parse(e.Text);
                    m_objStCamera.GammaValue[StTrg.STCAM_GAMMA_TARGET_B] = Value;
                }
                else if (sender.Equals(txtWBRGain))
                {
                    ushort Value = ushort.Parse(e.Text);
                    m_objStCamera.WBRGain = Value;
                }
                else if (sender.Equals(txtWBGrGain))
                {
                    ushort Value = ushort.Parse(e.Text);
                    m_objStCamera.WBGrGain = Value;
                }
                else if (sender.Equals(txtWBGbGain))
                {
                    ushort Value = ushort.Parse(e.Text);
                    m_objStCamera.WBGbGain = Value;
                }
                else if (sender.Equals(txtWBBGain))
                {
                    ushort Value = ushort.Parse(e.Text);
                    m_objStCamera.WBBGain = Value;
                }
                else if (sender.Equals(txtHue))
                {
                    short Value = short.Parse(e.Text);
                    m_objStCamera.Hue = Value;
                }
                else if (sender.Equals(txtSaturation))
                {
                    ushort Value = ushort.Parse(e.Text);
                    m_objStCamera.Saturation = Value;
                }
                else if (sender.Equals(txtChromaSuppresionStartLevel))
                {
                    ushort Value = ushort.Parse(e.Text);
                    m_objStCamera.HighChromaSuppressionStartLevel = Value;
                }
                else if (sender.Equals(txtChromaSuppresionSuppressionLevel))
                {
                    ushort Value = ushort.Parse(e.Text);
                    m_objStCamera.HighChromaSuppressionSuppressionLevel = Value;
                }
                else if (sender.Equals(txtLowChromaSuppresionStartLevel))
                {
                    ushort Value = ushort.Parse(e.Text);
                    m_objStCamera.LowChromaSuppressionStartLevel = Value;
                }
                else if (sender.Equals(txtLowChromaSuppresionSuppressionLevel))
                {
                    ushort Value = ushort.Parse(e.Text);
                    m_objStCamera.LowChromaSuppressionSuppressionLevel = Value;
                }
                else if (sender.Equals(txtAutoTriggerTime))
                {
                    int Value = int.Parse(e.Text);
                    m_objStCamera.AutoTriggerDueTime = Value;
                }
                else if (sender.Equals(txtTriggerDelay))
                {
                    uint Value = uint.Parse(e.Text);
                    m_objStCamera.TriggerDelay = Value;
                }


                else if (sender.Equals(txtStrobeStartDelay))
                {
                    uint Value = uint.Parse(e.Text);
                    m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_STROBE_START_DELAY] = Value;
                }
                else if (sender.Equals(txtStrobeEndDelay))
                {
                    uint Value = uint.Parse(e.Text);
                    m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_STROBE_END_DELAY] = Value;
                }
                else if (sender.Equals(txtOutputPulseDelay))
                {
                    uint Value = uint.Parse(e.Text);
                    m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_OUTPUT_PULSE_DELAY] = Value;
                }
                else if (sender.Equals(txtOutputPulseDuration))
                {
                    uint Value = uint.Parse(e.Text);
                    m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_OUTPUT_PULSE_DURATION] = Value;
                }
                else if (sender.Equals(txtReadOutDelay))
                {
                    uint Value = uint.Parse(e.Text);
                    m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_READOUT_DELAY] = Value;
                }
                else if (sender.Equals(txtLineDebounceTime))
                {
                    uint Value = uint.Parse(e.Text);
                    m_objStCamera.TriggerTiming[StTrg.STCAM_TRIGGER_TIMING_LINE_DEBOUNCE_TIME] = Value;
                }
				else if (tabControl.SelectedTab.Equals(tabPageDefectPixelCorrection))
				{
					for (int i = 0; i < m_TextBoxList.Length; i++)
					{
						if (sender.Equals(m_TextBoxList[i]))
						{
							uint value = uint.Parse(e.Text);
							ushort index = (ushort)(i / 2);
							uint x = 0;
							uint y = 0;
							m_objStCamera.GetDefectPixelCorrectionPosition(index, out x, out y);
							if ((i % 2) == 0) x = value;
							else y = value;
							m_objStCamera.SetDefectPixelCorrectionPosition(index, x, y);
						}
					}
				}
				else if (sender.Equals(txtCameraGamma))
				{
					ushort Value = ushort.Parse(e.Text);
					m_objStCamera.CameraGamma = Value;
				}
				else if (sender.Equals(stTextBoxDigitalClamp))
				{
					ushort Value = ushort.Parse(e.Text);
					m_objStCamera.DigitalClamp = Value;
                }
                else if (sender.Equals(txtAnalogBlackLevel))
                {
                    ushort Value = ushort.Parse(e.Text);
                    m_objStCamera.AnalogBlackLevel = Value;
                }
                else if (sender.Equals(txtShadingCorrectionTarget))
                {
                    ushort Value = ushort.Parse(e.Text);
                    m_objStCamera.ShadingCorrectionTarget = Value;
                }


			}
			catch(Exception)
			{

			}
			UpdateDisplay();		
		}

		private void btnTrigger_Click(object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            m_objStCamera.TriggerSoftware(uint.Parse(btn.Tag.ToString()));
		}



		private void btnResetFrameNo_Click(object sender, System.EventArgs e)
		{
			m_objStCamera.ResetCounter();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			using (SaveFileDialog dlg = new SaveFileDialog())
			{
				dlg.Filter = "CFG File(*.cfg)|*.cfg|" +
					"All Files(*.*)|*.*";
				dlg.DefaultExt = "cfg";
				dlg.FileName = m_objStCamera.CameraUserName;

				if (DialogResult.OK == dlg.ShowDialog())
				{
					m_objStCamera.WriteSettingFile(dlg.FileName);
				}
			}
		}

		private void btnLoad_Click(object sender, System.EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.Filter = "CFG File(*.cfg)|*.cfg|" +
					"All Files(*.*)|*.*";
				dlg.DefaultExt = "cfg";


				if (DialogResult.OK == dlg.ShowDialog())
				{
					m_objStCamera.ReadSettingFile(dlg.FileName);

				}
			}
			
			UpdateDisplay();
		}

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			UpdateDisplay();
		}



		private void chkAutoTrigger_CheckedChanged(object sender, System.EventArgs e)
		{
			m_objStCamera.AutoTrigger = chkAutoTrigger.Checked;
			UpdateDisplay();
		}

		private void btnCameraSetting_Click(object sender, EventArgs e)
		{
			Button btn = sender as Button;

			ushort wMode = Convert.ToUInt16(btn.Tag.ToString(), 16);
			m_objStCamera.CameraSetting(wMode);
			UpdateDisplay();
		}

		private void btnDetectDefectPixel_Click(object sender, EventArgs e)
		{
			bool result = true;

			do
			{
				result = m_objStCamera.DetectDefectPixel((ushort)numericUpDownDefectPixelThreshold.Value);
				if (!result) break;

				UpdateDisplay();
			} while (false);
		}

		private void cmbIOPolarity0_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
