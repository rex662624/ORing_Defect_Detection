﻿using CherngerTools.Ini;
using CherngerTools.SKII;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using PLCConnector.PANASONIC;
using SensorTechnology;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserForm;
using CherngerControls;
using UnionAoi;
using System.Collections;
using System.Net.Sockets;
using System.Net;

namespace CherngerUI
{
	public partial class Form1 : Form
	{
		#region 初始參數
		Camera camera = new Camera();

		public static Queue<Mat> AiTestImages = new Queue<Mat>();

		//Mat ReTestSrc = new Mat();
		DefectSettings DefectUI = new DefectSettings();
		ImageProcessing_DefectSetting Image_Processing_Defect_Config = new ImageProcessing_DefectSetting();
		//TimeSpan ProductCounter = new TimeSpan();

		//=========socket===========


		Queue<Mat> ImgAI_1 = new Queue<Mat>();
		Queue<Mat> ImgAI_2 = new Queue<Mat>();
		Queue<Mat> ImgAI_3 = new Queue<Mat>();
		Queue<Mat> ImgAI_4 = new Queue<Mat>();

		Queue<Int64> OutputAI_1 = new Queue<Int64>();
		Queue<Int64> OutputAI_2 = new Queue<Int64>();
		Queue<Int64> OutputAI_3 = new Queue<Int64>();
		Queue<Int64> OutputAI_4 = new Queue<Int64>();
		int TestCount_1 = 0;
		int TestCount_2 = 0;
		int TestCount_3 = 0;
		int TestCount_4 = 0;

		public struct SaveImg
		{
			public string PN;
			public string Name;
			public Mat Img;
		}
		#endregion

		#region 介面

		public Form1()
		{
			ThreadPool.SetMaxThreads(5000, 5000);
			ThreadPool.SetMinThreads(3000, 3000);
			InitializeComponent();

			//圖片按鈕初始
			PictureButtonReset();

			//圖表初始
			ChartReset();

			//歷史照片刪除
			DeleteHistoryUmg(DeleteImgDayBox.SelectedIndex, app.SaveHistoryImgpath);

			//ConsoleBox = new ConsoleTextBox
			//{
			//	Size = new System.Drawing.Size(190, 205),
			//	Location = new System.Drawing.Point(4, 20),
			//	BackColor = Color.Black,
			//	Visible = true,
			//	Enabled = true,
			//	Name = "ConsoleBox1",
			//	TabIndex = 0,
			//	Padding = new Padding(0, 0, 0, 0),
			//};
			//xPanderPanel3.Controls.Add(ConsoleBox);

			//現在時間 初始
			DateTimer_Tick(this, null);
			DateTimer.Interval = 1000;  //每秒觸發一次
			DateTimer.Enabled = true;   //啟動timer
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			#region 權限監控
			LoginOption.onUserChange += onUserChange;
			LoginOption.Logout();
			#endregion

			#region 相機偵測
			if (!camera.Open(this))
			{
				MessageBox.Show("未偵測到攝影機", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
				// Close();
			}
			else
			{
				string CameraName = camera.AddrLink();
				//Console.WriteLine("[CameraName]  "+ CameraName);
				if (CameraName != "")
				{
					MessageBox.Show("攝影機 " + CameraName + " 名稱錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
					//Close();
				}
			}

			#endregion

			#region 相機開啟 初始化
			camera.Setting(1);
			camera.Start();
			for (int i = 0; i < app.MaxCameraCount; i++)
			{
				if (camera.CheckCamera(i))
				{
					CameraSelectBox.Items.Add(i.ToString());
					camera.ExposureClockChange(i, int.Parse(SetupIniIP.IniReadValue("Camera" + i.ToString(), "ExposureClock", app.UISettingpath)));
					camera.GainChange(i, int.Parse(SetupIniIP.IniReadValue("Camera" + i.ToString(), "Gain", app.UISettingpath)));
					ushort s = ushort.Parse(SetupIniIP.IniReadValue("Camera" + i.ToString(), "WhiteBalanceR", app.UISettingpath));
					WhiteBalanceGr.Text = SetupIniIP.IniReadValue("Camera" + CameraSelectBox.Text, "WhiteBalanceGr", app.UISettingpath);
					WhiteBalanceGb.Text = SetupIniIP.IniReadValue("Camera" + CameraSelectBox.Text, "WhiteBalanceGb", app.UISettingpath);
					WhiteBalanceB.Text = SetupIniIP.IniReadValue("Camera" + CameraSelectBox.Text, "WhiteBalanceB", app.UISettingpath);
					camera.SetWhiteBalance(1, i, ushort.Parse(SetupIniIP.IniReadValue("Camera" + i.ToString(), "WhiteBalanceR", app.UISettingpath))
					, ushort.Parse(SetupIniIP.IniReadValue("Camera" + i.ToString(), "WhiteBalanceGr", app.UISettingpath))
					, ushort.Parse(SetupIniIP.IniReadValue("Camera" + i.ToString(), "WhiteBalanceGb", app.UISettingpath))
					, ushort.Parse(SetupIniIP.IniReadValue("Camera" + i.ToString(), "WhiteBalanceB", app.UISettingpath)));

					Console.WriteLine("Camera: " + i.ToString());
					Console.WriteLine(int.Parse(SetupIniIP.IniReadValue("Camera" + i.ToString(), "ExposureClock", app.UISettingpath)).ToString());
					Console.WriteLine(int.Parse(SetupIniIP.IniReadValue("Camera" + i.ToString(), "Gain", app.UISettingpath)).ToString());

				}
			}
			if (camera.Open(this))
			{
				camera.Set_TriggrtSoure(0, 4);
				camera.Set_TriggrtSoure(1, 4);
				camera.Set_TriggrtSoure(2, 4);
			}
			camera.Set_TriggrtSoure(0, 4);
			camera.Set_TriggrtSoure(1, 4);
			camera.Set_TriggrtSoure(2, 4);
			camera.Set_TriggrtSoure(3, 2);
			#endregion

			#region 載入UI資料
			LoadUIData();
			double.TryParse(SetupIniIP.IniReadValue("Gap", "Area", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.GapArea);
			double.TryParse(SetupIniIP.IniReadValue("Gap", "Deep", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.GapDeep);

			double.TryParse(SetupIniIP.IniReadValue("Crack", "Area", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.CrackArea);
			double.TryParse(SetupIniIP.IniReadValue("Crack", "Length", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.CrackLength);

			double.TryParse(SetupIniIP.IniReadValue("CatchUp", "Area", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.CatchUpArea);
			double.TryParse(SetupIniIP.IniReadValue("CatchUp", "Length", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.CatchUpLength);

			double.TryParse(SetupIniIP.IniReadValue("OD", "Standard", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.StandardOD);
			double.TryParse(SetupIniIP.IniReadValue("OD", "LowerBound", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.LowerBoundOD);
			double.TryParse(SetupIniIP.IniReadValue("OD", "UpperBound", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.UpperBoundOD);

			double.TryParse(SetupIniIP.IniReadValue("ID", "Standard", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.StandardID);
			double.TryParse(SetupIniIP.IniReadValue("ID", "LowerBound", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.LowerBoundID);
			double.TryParse(SetupIniIP.IniReadValue("ID", "UpperBound", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.UpperBoundID);

			bool.TryParse(SetupIniIP.IniReadValue("Gap", "Inspect", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.GapApply);
			bool.TryParse(SetupIniIP.IniReadValue("Crack", "Inspect", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.CrackApply);
			bool.TryParse(SetupIniIP.IniReadValue("CatchUp", "Inspect", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.CatchApply);
			bool.TryParse(SetupIniIP.IniReadValue("OD", "Inspect", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.ODApply);
			bool.TryParse(SetupIniIP.IniReadValue("ID", "Inspect", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.IDApply);

			//Image Processing Config
			int.TryParse(SetupIniIP.IniReadValue("Stop1", "inner_circle_radius", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop1_inner_circle_radius);
			int.TryParse(SetupIniIP.IniReadValue("Stop1", "outer_defect_size_min", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_min);
			int.TryParse(SetupIniIP.IniReadValue("Stop1", "outer_defect_size_max", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_max);
			int.TryParse(SetupIniIP.IniReadValue("Stop1", "inner_defect_size_min", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop1_inner_defect_size_min);
			int.TryParse(SetupIniIP.IniReadValue("Stop1", "arclength_area_ratio", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop1_arclength_area_ratio);

			int.TryParse(SetupIniIP.IniReadValue("Stop2", "inner_circle_radius", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop2_inner_circle_radius);
			int.TryParse(SetupIniIP.IniReadValue("Stop2", "outer_defect_size_min", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_min);
			int.TryParse(SetupIniIP.IniReadValue("Stop2", "outer_defect_size_max", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_max);
			int.TryParse(SetupIniIP.IniReadValue("Stop2", "inner_defect_size_min", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop2_inner_defect_size_min);
			int.TryParse(SetupIniIP.IniReadValue("Stop2", "arclength_area_ratio", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop2_arclength_area_ratio);

			int.TryParse(SetupIniIP.IniReadValue("Stop3", "threshold_1phase", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop3_threshold_1phase);
			int.TryParse(SetupIniIP.IniReadValue("Stop3", "threshold_2phase_1", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop3_threshold_2phase_1);
			int.TryParse(SetupIniIP.IniReadValue("Stop3", "threshold_2phase_2", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop3_threshold_2phase_2);


			int.TryParse(SetupIniIP.IniReadValue("Stop4", "black_defect_area_min", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop4_black_defect_area_min);
			int.TryParse(SetupIniIP.IniReadValue("Stop4", "black_defect_area_max", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop4_black_defect_area_max);
			double.TryParse(SetupIniIP.IniReadValue("Stop4", "arclength_area_ratio", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop4_arclength_area_ratio);
			int.TryParse(SetupIniIP.IniReadValue("Stop4", "ignore_radius", CherngerUI.app.Image_ProcssingDefect_Config), out CherngerUI.ImageProcessingDefect_Value.stop4_ignore_radius);

			#endregion

			#region Smart Key
			//SmartKey key = new SmartKey(new byte[] { 0x12, 0x34, 0x56, 0x78 }, new List<byte>() { 0x69, 0x68, 0x5E, 0x52, 0x47, 0x17, 0xE7, 0x96, 0xA3, 0xA3,
			//0x2D, 0xD8, 0x59, 0xDA, 0x8D, 0x0E, 0x05, 0x90, 0x11, 0x45,
			//0x35, 0xA0, 0x63, 0xDC, 0x5C, 0xE4, 0x8D, 0x2D, 0x4D, 0x9B,
			//0xBD, 0x97, 0xC2, 0xBE, 0xD0, 0xD4, 0xB9, 0x6A, 0x84, 0xD4,
			//0x12, 0x92, 0x6C, 0x8E, 0x87, 0x64, 0xE1, 0x95, 0xEA, 0x2B,
			//0x91, 0x00, 0x8A, 0x20, 0xBB, 0xA1, 0xBD, 0x9D, 0x2B, 0x34,
			//0x29, 0x4A, 0x86, 0x52, 0x16, 0xEB, 0xA6, 0xD7, 0x8D, 0x9E,
			//0x6E, 0x5E, 0x72, 0x29, 0xE7, 0x51, 0xBE, 0x90, 0x43, 0x48,
			//0x82, 0xA0, 0x12, 0x90, 0x5B, 0x74, 0x9C, 0x77, 0x1D, 0x0C,
			//0x35, 0x83, 0xD0, 0x20, 0x48, 0x12, 0x6B, 0xD6, 0x3E, 0x39,
			//0x1D, 0xCA, 0xDC, 0xA9, 0xC4  }, KeyRemove);
			#endregion

			#region 計時
			app.RunningSW.Reset();
			app.TotalSW.Reset();
			app.SingleRunningSW.Reset();
			app.SingleItemTime1.Reset();
			app.SingleItemTime2.Reset();
			app.SingleItemTime3.Reset();
			app.SingleItemTime4.Reset();
			app.TotalSW.Start();
			RunTimer.Enabled = true;
			RunTimer.Start();
			#endregion

			PLC1.PLC_On(this);

			#region 各站觸發參數
			app.DelateShoot[0] = 2000;
			app.DelateShoot[1] = 1990;
			app.DelateShoot[2] = 5150;
			app.DelateShoot[3] = 7800;//7800
			app.BlowDelay[0] = 10400;
			app.BlowDelay[1] = 13400;
			#endregion

			#region 各站啟用狀態初始化
			CCD_1_ApplyBox.Checked = app.CCD_Apply[0] = true;
			CCD_2_ApplyBox.Checked = app.CCD_Apply[1] = true;
			CCD_3_ApplyBox.Checked = app.CCD_Apply[2] = true;
			CCD_4_ApplyBox.Checked = app.CCD_Apply[3] = true;
			#endregion

			#region 瑕疵檢測參數初始化

			DefectValue.GapArea = Value.GapArea;                        //缺角面積
			DefectValue.GapDeep = Value.GapDeep;                        //缺角深度
			DefectValue.CrackArea = Value.CrackArea;                    //微裂面積
			DefectValue.CrackLength = Value.CrackLength;                //微裂長度
			DefectValue.CatchUpArea = Value.CatchUpArea;                //髒污面積
			DefectValue.CatchUpLength = Value.CatchUpLength;            //髒污長度

			DefectValue.StandardOD = Value.StandardOD;                  //外徑標準值
			DefectValue.LowerBoundOD = Value.LowerBoundOD;              //外徑下界 
			DefectValue.UpperBoundOD = Value.UpperBoundOD;              //外徑上界

			DefectValue.StandardID = Value.StandardID;                  //內徑標準值
			DefectValue.LowerBoundID = Value.LowerBoundID;              //內徑下界
			DefectValue.UpperBoundID = Value.UpperBoundID;              //內徑上界

			DefectValue.IDRatio = Value.IDRatio;                        //內徑轉換比
			DefectValue.ODRatio = Value.ODRatio;                        //外徑轉換比

			DefectValue.GapApply = Value.GapApply;                      //缺角套用檢測
			DefectValue.CatchApply = Value.CatchApply;                  //髒污套用檢測
			DefectValue.CrackApply = Value.CrackApply;                  //微裂套用檢測
			DefectValue.ODApply = Value.ODApply;                        //外徑套用檢測
			DefectValue.IDApply = Value.IDApply;                        //內徑套用檢測

			#endregion

			app.PlcCounter[0] = 0;
			app.PlcCounter[1] = 0;

			app.NullWarning = 0;
			app.NullWarningTarget = 100;
			NullWarningBox.Text = app.NullWarningTarget.ToString();
			Console.WriteLine("-------");

			#region 存圖執行緒
			//ThreadPool.QueueUserWorkItem(new WaitCallback(SaveImage_1));
			//ThreadPool.QueueUserWorkItem(new WaitCallback(SaveImage_2));
			//ThreadPool.QueueUserWorkItem(new WaitCallback(SaveImage_3));
			//ThreadPool.QueueUserWorkItem(new WaitCallback(SaveImage_4));
			//ThreadPool.QueueUserWorkItem(new WaitCallback(SaveImage_Temp_1));
			//ThreadPool.QueueUserWorkItem(new WaitCallback(SaveImage_Temp_2));
			//ThreadPool.QueueUserWorkItem(new WaitCallback(SaveImage_Temp_3));
			//ThreadPool.QueueUserWorkItem(new WaitCallback(SaveImage_Temp_4));
			#endregion

			OfflinePathBox.Text = app.OfflineImagePath;
			#region 建立資料夾
			My_Create_Folder();
			#endregion

		}

		#region 離線操作區
		private void OfflinePathBox_TextChanged(object sender, EventArgs e)
		{
			app.OfflineImagePath = OfflinePathBox.Text;
			app.OfflineImage = Directory.GetFiles(app.OfflineImagePath);
		}

		private void LastImage_Click(object sender, EventArgs e)
		{
			app.OfflineCount--;
			if (app.OfflineCount <= 0)
			{
				MessageBox.Show("沒有上一張", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				app.OfflineShoot = true;
				app.OfflineShootID = OfflineTestStop.SelectedIndex;
				Mat Src1 = new Mat(app.OfflineImage[app.OfflineCount - 1], ImreadModes.Grayscale);
				Receiver(app.OfflineShootID, Src1);
			}
		}

		private void NextImage_Click(object sender, EventArgs e)
		{
			app.OfflineCount++;
			if (app.OfflineCount > app.OfflineImage.Count())
			{
				MessageBox.Show("沒有下一張", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				app.OfflineShoot = true;
				app.OfflineShootID = OfflineTestStop.SelectedIndex;
				Mat Src1 = new Mat(app.OfflineImage[app.OfflineCount - 1], ImreadModes.Grayscale);
				//UnionAOI AOI = new UnionAOI(Src1, (UnionAOI.Perspective)OfflineTestStop.SelectedIndex);
			}
		}

		//離線讀圖按鈕
		private void OfflineTestBtn_Click(object sender, EventArgs e)
		{
			app.OfflineShoot = true;
			app.OfflineShootID = OfflineTestStop.SelectedIndex;
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.Filter = "(*.jpg;*.bmp)|*.jpg;*.bmp";

			openFileDialog1.ShowDialog();

			if (openFileDialog1.FileName != "")
			{


				//DirectoryInfo dr1 = new DirectoryInfo(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName , ""));
				//app.enumerator = dr1.GetFiles().OrderBy(f => int.Parse(f.Name.Substring(0 , f.Name.Length - 4))).GetEnumerator();
				Mat Src = Cv2.ImRead(openFileDialog1.FileName, ImreadModes.Grayscale);

				if (OfflineTestStop.SelectedIndex >= 0)
				{
					switch (OfflineTestStop.SelectedIndex)
					{
						case 0:
							Stop1_Detector(Src, Src);
							break;
						case 1:
							Stop2_Detector(Src, Src);
							break;
						case 2:
							Stop3_Detector(Src, Src);
							break;
						case 3:
							Stop4_Detector(Src, Src);
							break;
						default:
							MessageBox.Show("錯誤", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
							break;
					}
				}

				//ReTestSrc = Src.Clone();

			}

			//Thread.Sleep(100);
			//OfflineTestBtn.Enabled = false;
		}

		//選擇站號選單
		private void OfflineTestStop_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (OfflineTestStop.SelectedIndex >= 0)
				app.OfflineShootID = OfflineTestStop.SelectedIndex;
		}

		//重新測試按鈕
		private void RetestBtn_Click(object sender, EventArgs e)
		{
			app.OfflineShootID = OfflineTestStop.SelectedIndex;
			//if (ReTestSrc != new Mat())
			//{
			//	app.OfflineShoot = true;
			//	Receiver(OfflineTestStop.SelectedIndex, ReTestSrc);
			//	//Console.WriteLine("重新檢測站號: " + OfflineTestStop.SelectedIndex);
			//	MessageBox.Show("重新檢測成功", "系統", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//}
			//else
			//	MessageBox.Show("目前沒有圖檔！請離線拍照或是讀取圖檔。", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//Console.WriteLine("5");
		}

		//轉盤下一站按鈕
		private void NextStopBtn_Click(object sender, EventArgs e)
		{
			if (OfflineMotorBox.Checked)
			{
				PLC1.setValue(PLC1.PLCContect.OfflineMotor, true);
				Thread.Sleep(100);
				PLC1.setValue(PLC1.PLCContect.Motor, true);
				//GC.Collect();
				Application.DoEvents();
			}
		}
		#endregion

		#region 使用者操作區

		#region 狀態列
		//登入鍵
		private void LogButton_Click(object sender, EventArgs e)
		{
			//Console.WriteLine("LogButtonClick");
			if (!LoginOption.IsLogin())
			{
				LoginOption.Login();
				//LogButton.Text = "登出";
			}
			else
			{
				LoginOption.Logout();
				//LogButton.Text = "登入";
			}
		}

		//帳號管理鍵
		private void adminbutton_Click(object sender, EventArgs e)
		{
			LoginOption.showMangerSetInfo();
		}

		//關閉程式按鈕
		private void Closebutton_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		//開始檢測按鈕
		private void StartButton_Click(object sender, EventArgs e)
		{
			if (StartButton.Text == "開始檢測")
			{
				stop = true;
				app.Run = true;
				PLC1.plcRun();

				#region 介面變化
				Endbutton.Enabled = false;
				StartButton.Text = "暫停檢測";
				Statelabel.Text = "Online";
				Statelabel.BackColor = Color.Green;
				Redlight.Visible = false;
				Yellowlight.Visible = false;
				Greenlight.Visible = true;
				InitialSettingBtn.Enabled = false;
				#endregion

				#region 初始化
				Num.Reset();
				#endregion

				#region 計時
				app.SingleRunningSW.Start();
				app.RunningSW.Start();
				app.UpdateChartSW.Start();
				#endregion

				if (app.Mode == 1 && app.IsAdjust == false)
				{
					Console.WriteLine("調機中");
					PLC1.setValue(PLC1.PLCContect.Green_light, true);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Red_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Yellow_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Motor, true);
					Thread.Sleep(100);
					PLC1.setValue(PLC1.PLCContect.Light, true);
					Thread.Sleep(1000);
					PLC1.setValue(PLC1.PLCContect.Vibrat_Plate, true);
				}
				else if (app.Mode == 1 && app.IsAdjust)
				{
					Console.WriteLine("檢測中");
					PLC1.setValue(PLC1.PLCContect.Green_light, true);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Red_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Yellow_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Motor, true);
					Thread.Sleep(100);
					PLC1.setValue(PLC1.PLCContect.Light, true);
					Thread.Sleep(1000);
					PLC1.setValue(PLC1.PLCContect.Vibrat_Plate, true);
					app.NoneItemWarning = DateTime.Now;
					//ProductCounter = app.RunningSW.Elapsed;
				}
				else if (app.Offline && OfflineMotorBox.Checked)
				{
					Console.WriteLine("離線模式: 轉盤運轉中");
					OfflineMotorBox.Enabled = false;
					PLC1.setValue(PLC1.PLCContect.OfflineMotor, true);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Green_light, true);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Red_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Yellow_light, false);
					Thread.Sleep(100);
					PLC1.setValue(PLC1.PLCContect.Light, true);
					Thread.Sleep(500);
					PLC1.setValue(PLC1.PLCContect.Motor, true);
					Thread.Sleep(50);
					PLC1.setValue(PLC1.PLCContect.OfflineMotor, true);
				}
				else
				{
					PLC1.setValue(PLC1.PLCContect.Green_light, true);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Red_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Yellow_light, false);
					Thread.Sleep(100);
					PLC1.setValue(PLC1.PLCContect.Light, true);
				}


				//GC.Collect();
				Application.DoEvents();
			}
			else if (StartButton.Text == "暫停檢測")
			{
				if (app.Mode == 1)
				{
					PLC1.setValue(PLC1.PLCContect.Vibrat_Plate, false);
					if (app.NullWarning < 100)
					{
						PLC1.setValue(PLC1.PLCContect.Yellow_light, true);
						Thread.Sleep(5);
						PLC1.setValue(PLC1.PLCContect.Red_light, false);
						Thread.Sleep(5);
						PLC1.setValue(PLC1.PLCContect.Green_light, false);
						Thread.Sleep(5);
					}
					else if (app.NullWarning >= 100)
					{
						PLC1.setValue(PLC1.PLCContect.Red_light, true);
						Thread.Sleep(5);
						PLC1.setValue(PLC1.PLCContect.Yellow_light, false);
						Thread.Sleep(5);
						PLC1.setValue(PLC1.PLCContect.Green_light, false);
						Thread.Sleep(5);
					}
				}

				#region 介面變化
				Endbutton.Enabled = true;
				StartButton.Text = "繼續檢測";
				Statelabel.Text = "PAUSE";
				Statelabel.BackColor = Color.Yellow;
				Redlight.Visible = false;
				Yellowlight.Visible = true;
				Greenlight.Visible = false;

				#endregion

				#region 計時
				app.SingleRunningSW.Stop();
				app.RunningSW.Stop();
				app.SingleItemTime1.Stop();
				app.SingleItemTime2.Stop();
				app.SingleItemTime3.Stop();
				app.SingleItemTime4.Stop();
				app.UpdateChartSW.Stop();
				#endregion

				#region 檢測模式
				if (app.Mode == 1)
					ThreadPool.QueueUserWorkItem((o) =>
					{
						BeginInvoke(new Action(() =>
						{
							StartButton.Enabled = false;
							Endbutton.Enabled = false;
							Closebutton.Enabled = false;
						}));
						if (app.SingleItemTime1.Elapsed.TotalSeconds < app.NoneItemTime || app.SingleItemTime2.Elapsed.TotalSeconds < app.NoneItemTime ||
						app.SingleItemTime3.Elapsed.TotalSeconds < app.NoneItemTime || app.SingleItemTime4.Elapsed.TotalSeconds < app.NoneItemTime)
						{
							Thread.Sleep(5000);
							Thread.Sleep(5000);
							Thread.Sleep(5000);
							Thread.Sleep(5000);
						}
						else
						{
							Thread.Sleep(300);
						}

						app.Run = false;
						PLC1.setValue(PLC1.PLCContect.Motor, false);
						if (app.AdjustCounter >= 10)
						{
							app.IsAdjust = true;
						}
						BeginInvoke(new Action(() =>
						{
							StartButton.Enabled = true;
							Endbutton.Enabled = true;
							Closebutton.Enabled = true;
							InitialSettingBtn.Enabled = true;
						}));

						#region 是否校正完成
						if (app.IsAdjust && Num.NULLNum == -1)
						{
							Num.NULLNum = 0;
							BeginInvoke(new Action(() => MessageBox.Show("校正完成\n可以開始檢測", "System", MessageBoxButtons.OK, MessageBoxIcon.Information)));
						}
						#endregion

						#region 重工料斗判定
						if (app.NullWarning >= app.NullWarningTarget)
						{
							PLC1.setValue(PLC1.PLCContect.Red_light, true);
							Thread.Sleep(5);
							while (MessageBox.Show("NULL 料斗需要清空，請盡快處理 !\n請在清空後點選重工歸零按鈕。", "系統", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
							{
								PLC1.setValue(PLC1.PLCContect.Buzzer, true);
								Thread.Sleep(1000);
								PLC1.setValue(PLC1.PLCContect.Buzzer, false);
							}
						}
						#endregion

						#region 空料判定
						if (app.SingleItemTime1.Elapsed.TotalSeconds >= app.NoneItemTime || app.SingleItemTime2.Elapsed.TotalSeconds >= app.NoneItemTime ||
						app.SingleItemTime3.Elapsed.TotalSeconds >= app.NoneItemTime || app.SingleItemTime4.Elapsed.TotalSeconds >= app.NoneItemTime)
						{
							PLC1.setValue(PLC1.PLCContect.Red_light, true);
							Thread.Sleep(5);
							PLC1.setValue(PLC1.PLCContect.Buzzer, true);
							Thread.Sleep(1000);
							PLC1.setValue(PLC1.PLCContect.Buzzer, false);
							while (app.SingleItemTime1.Elapsed.TotalSeconds >= app.NoneItemTime || app.SingleItemTime2.Elapsed.TotalSeconds >= app.NoneItemTime ||
							app.SingleItemTime3.Elapsed.TotalSeconds >= app.NoneItemTime || app.SingleItemTime4.Elapsed.TotalSeconds >= app.NoneItemTime)
							{
								if (MessageBox.Show("目前沒有料件，請確認震動盤內是否為空。", "系統", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
								{
									app.SingleItemTime1.Reset();
									app.SingleItemTime2.Reset();
									app.SingleItemTime3.Reset();
									app.SingleItemTime4.Reset();
									app.NoneItemBuzzer = false;
								}
								else
								{
									PLC1.setValue(PLC1.PLCContect.Buzzer, true);
									Thread.Sleep(1000);
									PLC1.setValue(PLC1.PLCContect.Buzzer, false);
								}

							}
						}
						#endregion

						#region 轉盤潔淨度判定
						if (app.GlassesClean >= 10)
						{
							PLC1.setValue(PLC1.PLCContect.Red_light, true);
							Thread.Sleep(5);
							PLC1.setValue(PLC1.PLCContect.Buzzer, true);
							Thread.Sleep(1000);
							PLC1.setValue(PLC1.PLCContect.Buzzer, false);
							while (app.GlassesClean >= 10)
							{
								if (MessageBox.Show("轉盤玻璃潔淨度警報！請連絡相關人員進行清潔。", "系統", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
								{
									app.GlassesClean = 0;
								}
								else
								{
									PLC1.setValue(PLC1.PLCContect.Buzzer, true);
									Thread.Sleep(1000);
									PLC1.setValue(PLC1.PLCContect.Buzzer, false);
								}

							}
						}
						#endregion

					});
				#endregion

				#region 離線轉盤模式
				else if (app.Offline && OfflineMotorBox.Checked)
				{
					ThreadPool.QueueUserWorkItem((o) =>
					{
						BeginInvoke(new Action(() =>
						{
							StartButton.Enabled = false;
							Endbutton.Enabled = false;
							Closebutton.Enabled = false;
						}));

						Thread.Sleep(500);
						app.Run = false;
						PLC1.setValue(PLC1.PLCContect.Motor, false);
						Thread.Sleep(5);
						PLC1.setValue(PLC1.PLCContect.OfflineMotor, false);

						BeginInvoke(new Action(() =>
						{
							StartButton.Enabled = true;
							Endbutton.Enabled = true;
							Closebutton.Enabled = true;
							InitialSettingBtn.Enabled = true;
							OfflineMotorBox.Enabled = true;
						}));
					});
				}
				#endregion

				//GC.Collect();
				Application.DoEvents();
			}
			else if (StartButton.Text == "繼續檢測")
			{
				stop = true;
				app.Run = true;
				PLC1.plcRun();

				#region 介面變化
				Endbutton.Enabled = false;
				StartButton.Text = "暫停檢測";
				Statelabel.Text = "ONLINE";
				Statelabel.BackColor = Color.Green;
				Redlight.Visible = false;
				Yellowlight.Visible = false;
				Greenlight.Visible = true;
				InitialSettingBtn.Enabled = false;
				#endregion

				#region 計時
				app.SingleRunningSW.Reset();
				app.SingleRunningSW.Start();
				app.RunningSW.Start();

				//app.SingleItemTime1.Reset();
				//app.SingleItemTime1.Start();
				//app.SingleItemTime2.Reset();
				//app.SingleItemTime2.Start();
				//app.SingleItemTime3.Reset();
				//app.SingleItemTime3.Start();
				//app.SingleItemTime4.Reset();
				//app.SingleItemTime4.Start();

				app.UpdateChartSW.Start();
				#endregion

				#region 檢測模式
				if (app.Mode == 1)
				{
					PLC1.setValue(PLC1.PLCContect.Green_light, true);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Red_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Yellow_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Motor, true);
					Thread.Sleep(500);
					PLC1.setValue(PLC1.PLCContect.Vibrat_Plate, true);
				}
				#endregion

				#region 離線轉盤模式
				else if (app.Offline && OfflineMotorBox.Checked)
				{
					OfflineMotorBox.Enabled = false;
					PLC1.setValue(PLC1.PLCContect.OfflineMotor, true);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Green_light, true);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Red_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Yellow_light, false);
					Thread.Sleep(500);
					PLC1.setValue(PLC1.PLCContect.Motor, true);
				}
				#endregion

				#region Live & Offline Live 模式
				else
				{
					PLC1.setValue(PLC1.PLCContect.Green_light, true);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Red_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Yellow_light, false);
					Thread.Sleep(5);
				}
				#endregion


				//GC.Collect();
				Application.DoEvents();
			}
			else if (StartButton.Text == "復歸")
			{
				stop = false;
				app.Run = false;
				app.TakePic = 0;

				#region 介面變化
				Endbutton.Enabled = true;
				StartButton.Text = "繼續檢測";
				Statelabel.Text = "PAUSE";
				Statelabel.BackColor = Color.Yellow;
				Redlight.Visible = false;
				Yellowlight.Visible = true;
				Greenlight.Visible = false;
				InitialSettingBtn.Enabled = false;
				#endregion

				#region 計時
				app.SingleRunningSW.Stop();
				app.RunningSW.Stop();
				app.SingleItemTime1.Stop();
				app.SingleItemTime2.Stop();
				app.SingleItemTime3.Stop();
				app.SingleItemTime4.Stop();

				app.UpdateChartSW.Stop();
				#endregion

				#region 檢測模式
				if (app.Mode == 1)
				{
					PLC1.setValue(PLC1.PLCContect.Red_light, true);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Yellow_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Green_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Light, true);
					Thread.Sleep(50);
					PLC1.setValue(PLC1.PLCContect.Motor, true);
					Thread.Sleep(100);

					ThreadPool.QueueUserWorkItem((o) =>
					{
						Thread.Sleep(5000);
						Thread.Sleep(5000);
						Thread.Sleep(5000);
						Thread.Sleep(5000);

						PLC1.setValue(PLC1.PLCContect.Motor, false);

					});
				}
				#endregion

				#region 離線轉盤模式
				else if (app.Offline && OfflineMotorBox.Checked)
				{
					PLC1.setValue(PLC1.PLCContect.Red_light, true);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Yellow_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Green_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Motor, true);
					Thread.Sleep(100);
					PLC1.setValue(PLC1.PLCContect.OfflineMotor, false);
					Thread.Sleep(5);

					ThreadPool.QueueUserWorkItem((o) =>
					{
						Thread.Sleep(5000);
						Thread.Sleep(5000);
						Thread.Sleep(5000);
						Thread.Sleep(5000);

						PLC1.setValue(PLC1.PLCContect.Motor, false);

					});
				}
				#endregion

				#region Live & Offline Live
				else
				{
					PLC1.setValue(PLC1.PLCContect.Red_light, true);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Yellow_light, false);
					Thread.Sleep(5);
					PLC1.setValue(PLC1.PLCContect.Green_light, false);
					Thread.Sleep(100);
					PLC1.setValue(PLC1.PLCContect.Light, true);
					Thread.Sleep(50);
					PLC1.setValue(PLC1.PLCContect.Motor, false);
					Thread.Sleep(5);
				}
				#endregion

				//GC.Collect();
				Application.DoEvents();
			}
		}

		//結束檢測按鈕
		private void Endbutton_Click(object sender, EventArgs e)
		{
			#region 介面變化
			Endbutton.Enabled = false;
			StartButton.Enabled = true;
			StartButton.Text = "開始檢測";
			Statelabel.Text = "OFFLINE";
			Statelabel.BackColor = Color.Red;
			Redlight.Visible = true;
			Yellowlight.Visible = false;
			Greenlight.Visible = false;
			InitialSettingBtn.Enabled = true;
			OfflineMotorBox.Enabled = false;
			#endregion

			app.Run = false;
			Application.DoEvents();
			PLC1.setValue(PLC1.PLCContect.OfflineMotor, false);
			Thread.Sleep(5);
			PLC1.setValue(PLC1.PLCContect.Red_light, true);
			Thread.Sleep(5);
			PLC1.setValue(PLC1.PLCContect.Yellow_light, false);
			Thread.Sleep(5);
			PLC1.setValue(PLC1.PLCContect.Green_light, false);
			Thread.Sleep(5);
			PLC1.setValue(PLC1.PLCContect.Vibrat_Plate, false);
			Thread.Sleep(50);
			PLC1.setValue(PLC1.PLCContect.Motor, false);
			Thread.Sleep(50);
			PLC1.setValue(PLC1.PLCContect.Light, false);
			//GC.Collect();
		}

		//重工料斗歸零按鈕
		private void NULLWaringBtn_Click(object sender, EventArgs e)
		{
			app.NullWarning = 0;
			BeginInvoke(new Action(() => MessageBox.Show("重工數量已歸零", "系統", MessageBoxButtons.OK, MessageBoxIcon.Information)));
			BeginInvoke(new Action(() => { NULLNumlabel.Text = app.NullWarning.ToString(); }));
		}

		//更換產品按鈕
		private void ChangeItemBtn_Click(object sender, EventArgs e)
		{
			app.IsAdjust = false;
			app.AdjustCounter = 0;
			Num.DefectReset();
		}

		//恢復原廠設定按鈕
		private void InitialSettingBtn_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("確定要恢復原廠設定？\n恢復原廠設定後，您更改過的設定值將無法再復原！", "系統", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
			{
				try
				{
					#region 相機出廠數值

					for (int i = 0; i < app.MaxCameraCount; i++)
					{
						if (camera.CheckCamera(i))
						{
							SetupIniIP.IniWriteValue("Camera" + i.ToString(), "ExposureClock", SetupIniIP.IniReadValue("Camera" + i.ToString(), "ExposureClock", app.UISettingpath_Initial), app.UISettingpath);
							SetupIniIP.IniWriteValue("Camera" + i.ToString(), "Gain", SetupIniIP.IniReadValue("Camera" + i.ToString(), "Gain", app.UISettingpath_Initial), app.UISettingpath);
						}
					}

					#endregion

					#region 瑕疵參數出廠數值

					double.TryParse(SetupIniIP.IniReadValue("Gap", "Area", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.GapArea);
					double.TryParse(SetupIniIP.IniReadValue("Gap", "Deep", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.GapDeep);

					double.TryParse(SetupIniIP.IniReadValue("Crack", "Area", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.CrackArea);
					double.TryParse(SetupIniIP.IniReadValue("Crack", "Length", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.CrackLength);

					double.TryParse(SetupIniIP.IniReadValue("CatchUp", "Area", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.CatchUpArea);
					double.TryParse(SetupIniIP.IniReadValue("CatchUp", "Length", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.CatchUpLength);

					double.TryParse(SetupIniIP.IniReadValue("OD", "Standard", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.StandardOD);
					double.TryParse(SetupIniIP.IniReadValue("OD", "LowerBound", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.LowerBoundOD);
					double.TryParse(SetupIniIP.IniReadValue("OD", "UpperBound", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.UpperBoundOD);

					double.TryParse(SetupIniIP.IniReadValue("ID", "Standard", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.StandardID);
					double.TryParse(SetupIniIP.IniReadValue("ID", "LowerBound", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.LowerBoundID);
					double.TryParse(SetupIniIP.IniReadValue("ID", "UpperBound", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.UpperBoundID);

					bool.TryParse(SetupIniIP.IniReadValue("Gap", "Inspect", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.GapApply);
					bool.TryParse(SetupIniIP.IniReadValue("Crack", "Inspect", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.CrackApply);
					bool.TryParse(SetupIniIP.IniReadValue("CatchUp", "Inspect", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.CatchApply);
					bool.TryParse(SetupIniIP.IniReadValue("OD", "Inspect", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.ODApply);
					bool.TryParse(SetupIniIP.IniReadValue("ID", "Inspect", CherngerUI.app.DefectSettingpath_Initial), out CherngerUI.Value.IDApply);


					SetupIniIP.IniWriteValue("Gap", "Area", Value.GapArea.ToString(), app.DefectSettingpath);
					SetupIniIP.IniWriteValue("Gap", "Deep", Value.GapDeep.ToString(), app.DefectSettingpath);

					SetupIniIP.IniWriteValue("Crack", "Area", Value.CrackArea.ToString(), app.DefectSettingpath);
					SetupIniIP.IniWriteValue("Crack", "Length", Value.CrackLength.ToString(), app.DefectSettingpath);

					SetupIniIP.IniWriteValue("CatchUp", "Area", Value.CatchUpArea.ToString(), app.DefectSettingpath);
					SetupIniIP.IniWriteValue("CatchUp", "Length", Value.CatchUpLength.ToString(), app.DefectSettingpath);

					SetupIniIP.IniWriteValue("OD", "Standard", Value.StandardOD.ToString(), app.DefectSettingpath);
					SetupIniIP.IniWriteValue("OD", "LowerBound", Value.LowerBoundOD.ToString(), app.DefectSettingpath);
					SetupIniIP.IniWriteValue("OD", "UpperBound", Value.UpperBoundOD.ToString(), app.DefectSettingpath);

					SetupIniIP.IniWriteValue("ID", "Standard", Value.StandardID.ToString(), app.DefectSettingpath);
					SetupIniIP.IniWriteValue("ID", "LowerBound", Value.LowerBoundID.ToString(), app.DefectSettingpath);
					SetupIniIP.IniWriteValue("ID", "UpperBound", Value.UpperBoundID.ToString(), app.DefectSettingpath);

					SetupIniIP.IniWriteValue("Gap", "Inspect", Value.GapApply.ToString(), app.DefectSettingpath);
					SetupIniIP.IniWriteValue("Crack", "Inspect", Value.CrackApply.ToString(), app.DefectSettingpath);
					SetupIniIP.IniWriteValue("CatchUp", "Inspect", Value.CatchApply.ToString(), app.DefectSettingpath);
					SetupIniIP.IniWriteValue("OD", "Inspect", Value.ODApply.ToString(), app.DefectSettingpath);
					SetupIniIP.IniWriteValue("ID", "Inspect", Value.IDApply.ToString(), app.DefectSettingpath);

					//Image processing initial value
					int.TryParse(SetupIniIP.IniReadValue("Stop1", "inner_circle_radius", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop1_inner_circle_radius);
					int.TryParse(SetupIniIP.IniReadValue("Stop1", "outer_defect_size_min", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_min);
					int.TryParse(SetupIniIP.IniReadValue("Stop1", "outer_defect_size_max", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_max);
					int.TryParse(SetupIniIP.IniReadValue("Stop1", "inner_defect_size_min", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop1_inner_defect_size_min);
					int.TryParse(SetupIniIP.IniReadValue("Stop1", "arclength_area_ratio", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop1_arclength_area_ratio);

					int.TryParse(SetupIniIP.IniReadValue("Stop2", "inner_circle_radius", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop2_inner_circle_radius);
					int.TryParse(SetupIniIP.IniReadValue("Stop2", "outer_defect_size_min", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_min);
					int.TryParse(SetupIniIP.IniReadValue("Stop2", "outer_defect_size_max", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_max);
					int.TryParse(SetupIniIP.IniReadValue("Stop2", "inner_defect_size_min", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop2_inner_defect_size_min);
					int.TryParse(SetupIniIP.IniReadValue("Stop2", "arclength_area_ratio", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop2_arclength_area_ratio);

					int.TryParse(SetupIniIP.IniReadValue("Stop3", "threshold_1phase", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop3_threshold_1phase);
					int.TryParse(SetupIniIP.IniReadValue("Stop3", "threshold_2phase_1", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop3_threshold_2phase_1);
					int.TryParse(SetupIniIP.IniReadValue("Stop3", "threshold_2phase_2", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop3_threshold_2phase_2);

					int.TryParse(SetupIniIP.IniReadValue("Stop4", "black_defect_area_min", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop4_black_defect_area_min);
					int.TryParse(SetupIniIP.IniReadValue("Stop4", "black_defect_area_max", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop4_black_defect_area_max);
					double.TryParse(SetupIniIP.IniReadValue("Stop4", "arclength_area_ratio", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop4_arclength_area_ratio);
					int.TryParse(SetupIniIP.IniReadValue("Stop4", "ignore_radius", CherngerUI.app.Image_ProcssingDefect_Config_Initial), out CherngerUI.ImageProcessingDefect_Value.stop4_ignore_radius);

					SetupIniIP.IniWriteValue("Stop1", "outer_defect_size_max", CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_max.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop1", "outer_defect_size_min", CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_min.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop1", "inner_circle_radius", CherngerUI.ImageProcessingDefect_Value.stop1_inner_circle_radius.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop1", "inner_defect_size_min", CherngerUI.ImageProcessingDefect_Value.stop1_inner_defect_size_min.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop1", "arclength_area_ratio", CherngerUI.ImageProcessingDefect_Value.stop1_arclength_area_ratio.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);

					SetupIniIP.IniWriteValue("Stop2", "outer_defect_size_max", CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_max.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop2", "outer_defect_size_min", CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_min.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop2", "inner_circle_radius", CherngerUI.ImageProcessingDefect_Value.stop2_inner_circle_radius.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop2", "inner_defect_size_min", CherngerUI.ImageProcessingDefect_Value.stop2_inner_defect_size_min.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop2", "arclength_area_ratio", CherngerUI.ImageProcessingDefect_Value.stop2_arclength_area_ratio.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);

					SetupIniIP.IniWriteValue("Stop3", "threshold_1phase", CherngerUI.ImageProcessingDefect_Value.stop3_threshold_1phase.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop3", "threshold_2phase_1", CherngerUI.ImageProcessingDefect_Value.stop3_threshold_2phase_1.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop3", "threshold_2phase_2", CherngerUI.ImageProcessingDefect_Value.stop3_threshold_2phase_2.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);

					SetupIniIP.IniWriteValue("Stop4", "black_defect_area_min", CherngerUI.ImageProcessingDefect_Value.stop4_black_defect_area_min.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop4", "black_defect_area_max", CherngerUI.ImageProcessingDefect_Value.stop4_black_defect_area_max.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop4", "arclength_area_ratio", CherngerUI.ImageProcessingDefect_Value.stop4_arclength_area_ratio.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);
					SetupIniIP.IniWriteValue("Stop4", "ignore_radius", CherngerUI.ImageProcessingDefect_Value.stop4_ignore_radius.ToString(), CherngerUI.app.Image_ProcssingDefect_Config);

					#endregion

					MessageBox.Show("已成功還原成出廠數值！", "系統", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (System.Exception)
				{
					MessageBox.Show("還原失敗！\n請再試一次。", "系統", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		#endregion

		#region 檢測設定

		//存圖模式選單
		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			app.SavingMode = comboBox2.SelectedItem.ToString();
		}

		//儲存圖檔按鈕
		private void Imgfolderbutton_Click(object sender, EventArgs e)
		{
			string file = @"C:\Windows\explorer.exe";
			System.Diagnostics.Process.Start(file, app.SaveImgpath);
		}

		//離線模式
		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton1.Checked)
			{
				app.Mode = 0;
				app.Offline = true;
				camera.Setting(0);
				OfflineTestBtn.Visible = true;
				OfflineTestStop.Visible = true;
				CameraShootbutton.Visible = true;
				CameraShootbutton.Enabled = true;
				OfflineTestBtn.Enabled = true;
				label17.Visible = true;
				label16.Visible = true;
				cherngerPictureBox5.Visible = true;
				RetestBtn.Visible = true;
				OfflineMotorBox.Visible = true;
				NextStopBtn.Visible = true;
				LastImage.Visible = true;
				NextImage.Visible = true;
				OfflinePathBox.Visible = true;
				camera.Start();
			}

		}

		//檢測模式
		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton2.Checked)
			{
				app.Mode = 1;
				app.Offline = false;
				camera.Setting(1);
				if (camera.Open(this))
				{
					camera.Set_TriggrtSoure(0, 4);
					camera.Set_TriggrtSoure(1, 4);
					camera.Set_TriggrtSoure(2, 4);
				}
				camera.Set_TriggrtSoure(0, 4);
				camera.Set_TriggrtSoure(1, 4);
				camera.Set_TriggrtSoure(2, 4);
				OfflineTestBtn.Visible = false;
				OfflineTestStop.Visible = false;
				CameraShootbutton.Visible = false;
				label17.Visible = false;
				label16.Visible = false;
				cherngerPictureBox5.Visible = false;
				RetestBtn.Visible = false;
				OfflineMotorBox.Visible = false;
				NextStopBtn.Visible = false;
				LastImage.Visible = false;
				NextImage.Visible = false;
				OfflinePathBox.Visible = false;
				camera.Start();
			}

		}

		//Live模式
		private void radioButton6_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton6.Checked)
			{
				app.Mode = 0;
				camera.Setting(0);
				OfflineTestBtn.Visible = false;
				OfflineTestStop.Visible = false;
				CameraShootbutton.Visible = false;
				label17.Visible = false;
				label16.Visible = false;
				cherngerPictureBox5.Visible = false;
				RetestBtn.Visible = false;
				OfflineMotorBox.Visible = false;
				NextStopBtn.Visible = false;
				LastImage.Visible = false;
				NextImage.Visible = false;
				OfflinePathBox.Visible = false;
				camera.Start();
			}

		}

		//調機模式
		private void radioButton7_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton7.Checked)
			{
				app.Mode = 1;
				app.Offline = false;
				camera.Setting(1);
				if (camera.Open(this))
				{
					camera.Set_TriggrtSoure(0, 4);
					camera.Set_TriggrtSoure(1, 4);
					camera.Set_TriggrtSoure(2, 4);
				}
				OfflineTestBtn.Visible = false;
				OfflineTestStop.Visible = false;
				CameraShootbutton.Visible = false;
				label17.Visible = false;
				label16.Visible = false;
				cherngerPictureBox5.Visible = false;
				RetestBtn.Visible = false;
				OfflineMotorBox.Visible = false;
				NextStopBtn.Visible = false;
				LastImage.Visible = false;
				NextImage.Visible = false;
				OfflinePathBox.Visible = false;
				camera.Start();
			}

		}

		//瑕疵設定參數按鈕
		private void InspectSettingBtn_Click(object sender, EventArgs e)
		{
			//原本至右的瑕疵設定
			//DefectUI.ShowDialog();
			Image_Processing_Defect_Config.ShowDialog();
		}

		//重工數量textbox
		private void NullWarningBox_TextChanged(object sender, EventArgs e)
		{
			if (NullWarningBox.Text != string.Empty)
			{
				int.TryParse(NullWarningBox.Text, out app.NullWarningTarget);
			}
		}
		#endregion

		#region 相機設定

		//相機選擇
		private void CameraSelectBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (CameraSelectBox.Text != string.Empty)
			{
				try
				{
					if (camera.CheckCamera(int.Parse(CameraSelectBox.Text)))
					{
						CameraSettingbutton.Enabled = true;
						CameraShootbutton.Enabled = true;
						groupBox1.Enabled = true;
						WhiteBalanceB.Enabled = false;
						WhiteBalanceGb.Enabled = false;
						WhiteBalanceGr.Enabled = false;
						WhiteBalanceR.Enabled = false;
						WhiteBalanceModeselect.Enabled = false;

						#region 導入相機設定
						CameraExposureClock.Text = SetupIniIP.IniReadValue("Camera" + CameraSelectBox.Text, "ExposureClock", app.UISettingpath);
						CameraGain.Text = SetupIniIP.IniReadValue("Camera" + CameraSelectBox.Text, "Gain", app.UISettingpath);
						//WhiteBalanceModeselect.SelectedIndex = int.Parse(SetupIniIP.IniReadValue("Camera" + CameraSelectBox.Text, "WhiteBalanceMode", app.UISettingpath));
						#endregion
					}
					else
					{
						CameraSettingbutton.Enabled = false;
						CameraShootbutton.Enabled = false;
						groupBox1.Enabled = false;
						WhiteBalanceB.Enabled = false;
						WhiteBalanceGb.Enabled = false;
						WhiteBalanceGr.Enabled = false;
						WhiteBalanceR.Enabled = false;
						WhiteBalanceModeselect.Enabled = false;
					}
				}
				catch (System.Exception EX)
				{
					MessageBox.Show(EX.Message);
					CameraSettingbutton.Enabled = false;
					CameraShootbutton.Enabled = false;
					groupBox1.Enabled = false;
					WhiteBalanceB.Enabled = false;
					WhiteBalanceGb.Enabled = false;
					WhiteBalanceGr.Enabled = false;
					WhiteBalanceR.Enabled = false;
					WhiteBalanceModeselect.Enabled = false;
				}
			}
		}

		//拍照按鈕
		private void CameraShootbutton_Click(object sender, EventArgs e)
		{
			Thread.Sleep(10);
			app.OfflineShootID = OfflineTestStop.SelectedIndex;
			app.OfflineShoot = true;
			OfflineTestBtn.Enabled = true;
			if (CameraShootbutton.Text == "拍照")
				CameraShootbutton.Text = "繼續取樣";
			else if (CameraShootbutton.Text == "繼續取樣")
				CameraShootbutton.Text = "拍照";

		}

		//相機設定按鈕
		private void CameraSettingbutton_Click(object sender, EventArgs e)
		{
			camera.ShowSettingDlg(int.Parse(CameraSelectBox.Text));
		}

		//相機曝光
		private void CameraExposureClock_TextChanged(object sender, EventArgs e)
		{
			if (CameraExposureClock.Text != string.Empty)
			{
				camera.ExposureClockChange(int.Parse(CameraSelectBox.Text), int.Parse(CameraExposureClock.Text));
			}
		}

		//相機增益
		private void CameraGain_TextChanged(object sender, EventArgs e)
		{
			if (CameraGain.Text != string.Empty)
			{
				camera.GainChange(int.Parse(CameraSelectBox.Text), int.Parse(CameraGain.Text));
			}
		}


		//白平衡模式選擇
		private void WhiteBalanceModeselect_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (WhiteBalanceModeselect.SelectedIndex == 1)
			{
				WhiteBalanceR.Enabled = true;
				WhiteBalanceGr.Enabled = true;
				WhiteBalanceGb.Enabled = true;
				WhiteBalanceB.Enabled = true;
				WhiteBalanceR.Text = SetupIniIP.IniReadValue("Camera" + CameraSelectBox.Text, "WhiteBalanceR", app.UISettingpath);
				WhiteBalanceGr.Text = SetupIniIP.IniReadValue("Camera" + CameraSelectBox.Text, "WhiteBalanceGr", app.UISettingpath);
				WhiteBalanceGb.Text = SetupIniIP.IniReadValue("Camera" + CameraSelectBox.Text, "WhiteBalanceGb", app.UISettingpath);
				WhiteBalanceB.Text = SetupIniIP.IniReadValue("Camera" + CameraSelectBox.Text, "WhiteBalanceB", app.UISettingpath);
				camera.SetWhiteBalance(WhiteBalanceModeselect.SelectedIndex, int.Parse(CameraSelectBox.Text), ushort.Parse(WhiteBalanceR.Text), ushort.Parse(WhiteBalanceGr.Text), ushort.Parse(WhiteBalanceGb.Text), ushort.Parse(WhiteBalanceB.Text));
			}
			else
			{
				WhiteBalanceR.Enabled = false;
				WhiteBalanceGr.Enabled = false;
				WhiteBalanceGb.Enabled = false;
				WhiteBalanceB.Enabled = false;
				WhiteBalanceR.Text = string.Empty;
				WhiteBalanceGr.Text = string.Empty;
				WhiteBalanceGb.Text = string.Empty;
				WhiteBalanceB.Text = string.Empty;
				camera.SetWhiteBalance(WhiteBalanceModeselect.SelectedIndex, int.Parse(CameraSelectBox.Text), 0, 0, 0, 0);
			}
		}

		//白平衡R
		private void WhiteBalanceR_TextChanged(object sender, EventArgs e)
		{
			if (WhiteBalanceR.Text != string.Empty && WhiteBalanceGr.Text != string.Empty && WhiteBalanceGb.Text != string.Empty && WhiteBalanceB.Text != string.Empty)
				camera.SetWhiteBalance(WhiteBalanceModeselect.SelectedIndex, int.Parse(CameraSelectBox.Text), ushort.Parse(WhiteBalanceR.Text), ushort.Parse(WhiteBalanceGr.Text), ushort.Parse(WhiteBalanceGb.Text), ushort.Parse(WhiteBalanceB.Text));

		}

		//白平衡Gr
		private void WhiteBalanceGr_TextChanged(object sender, EventArgs e)
		{
			if (WhiteBalanceR.Text != string.Empty && WhiteBalanceGr.Text != string.Empty && WhiteBalanceGb.Text != string.Empty && WhiteBalanceB.Text != string.Empty)
				camera.SetWhiteBalance(WhiteBalanceModeselect.SelectedIndex, int.Parse(CameraSelectBox.Text), ushort.Parse(WhiteBalanceR.Text), ushort.Parse(WhiteBalanceGr.Text), ushort.Parse(WhiteBalanceGb.Text), ushort.Parse(WhiteBalanceB.Text));

		}

		//白平衡Gb
		private void WhiteBalanceGb_TextChanged(object sender, EventArgs e)
		{
			if (WhiteBalanceR.Text != string.Empty && WhiteBalanceGr.Text != string.Empty && WhiteBalanceGb.Text != string.Empty && WhiteBalanceB.Text != string.Empty)
				camera.SetWhiteBalance(WhiteBalanceModeselect.SelectedIndex, int.Parse(CameraSelectBox.Text), ushort.Parse(WhiteBalanceR.Text), ushort.Parse(WhiteBalanceGr.Text), ushort.Parse(WhiteBalanceGb.Text), ushort.Parse(WhiteBalanceB.Text));

		}

		//白平衡B
		private void WhiteBalanceB_TextChanged(object sender, EventArgs e)
		{
			if (WhiteBalanceR.Text != string.Empty && WhiteBalanceGr.Text != string.Empty && WhiteBalanceGb.Text != string.Empty && WhiteBalanceB.Text != string.Empty)
				camera.SetWhiteBalance(WhiteBalanceModeselect.SelectedIndex, int.Parse(CameraSelectBox.Text), ushort.Parse(WhiteBalanceR.Text), ushort.Parse(WhiteBalanceGr.Text), ushort.Parse(WhiteBalanceGb.Text), ushort.Parse(WhiteBalanceB.Text));

		}

		//相機設定儲存按鈕
		private void CamerasettingSavebutton_Click(object sender, EventArgs e)
		{
			try
			{
				SetupIniIP.IniWriteValue("Camera" + CameraSelectBox.Text, "ExposureClock", CameraExposureClock.Text, app.UISettingpath);
				SetupIniIP.IniWriteValue("Camera" + CameraSelectBox.Text, "Gain", CameraGain.Text, app.UISettingpath);
				//SetupIniIP.IniWriteValue("Camera" + CameraSelectBox.Text, "WhiteBalanceMode", WhiteBalanceModeselect.SelectedIndex.ToString(), app.UISettingpath);
				//if (WhiteBalanceR.Text != string.Empty && WhiteBalanceGr.Text != string.Empty && WhiteBalanceGb.Text != string.Empty && WhiteBalanceB.Text != string.Empty)
				//{
				//	SetupIniIP.IniWriteValue("Camera" + CameraSelectBox.Text, "WhiteBalanceR", WhiteBalanceR.Text, app.UISettingpath);
				//	SetupIniIP.IniWriteValue("Camera" + CameraSelectBox.Text, "WhiteBalanceGr", WhiteBalanceGr.Text, app.UISettingpath);
				//	SetupIniIP.IniWriteValue("Camera" + CameraSelectBox.Text, "WhiteBalanceGb", WhiteBalanceGb.Text, app.UISettingpath);
				//	SetupIniIP.IniWriteValue("Camera" + CameraSelectBox.Text, "WhiteBalanceB", WhiteBalanceB.Text, app.UISettingpath);
				//}
				BeginInvoke(new Action(() => MessageBox.Show("儲存成功", "系統", MessageBoxButtons.OK, MessageBoxIcon.Information)));
			}
			catch (System.Exception ex)
			{
				BeginInvoke(new Action(() => MessageBox.Show("儲存失敗", "系統", MessageBoxButtons.OK, MessageBoxIcon.Error)));
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region 功能測試

		//PLC點位textbox
		private void PLC1ComBox_TextChanged(object sender, EventArgs e)
		{
			app.Comport1 = PLC1ComBox.Text;
		}

		//PLC點位變更按鈕
		private void PLCcomChangebutton_Click(object sender, EventArgs e)
		{
			try
			{
				SetupIniIP.IniWriteValue("PLC1", "ComPort", PLC1ComBox.Text, app.UISettingpath);
				//SetupIniIP.IniWriteValue("PLC2", "ComPort", PLC2ComBox.Text, app.UISettingpath);
				MessageBox.Show("更改成功");
			}
			catch
			{
				MessageBox.Show("更改失敗");
			}

		}
		//PLC點位開
		private void button2_Click(object sender, EventArgs e)
		{
			int M = comboBox1.SelectedIndex % 8 + (comboBox1.SelectedIndex / 8) * 10;
			PLC1.setValue((PLC1.PLCContect)M, true);
			//PLC1.getValue((PLC1.PLCContect)M, out bool status);
			switch (M)
			{
				case 0:
					label117.BackColor = Color.Lime;
					break;
				case 1:
					label9.BackColor = Color.Lime;
					break;
				case 2:
					label10.BackColor = Color.Lime;
					break;
				case 3:
					label11.BackColor = Color.Lime;
					break;
				case 4:
					label15.BackColor = Color.Lime;
					break;
				case 5:
					label14.BackColor = Color.Lime;
					break;
				case 6:
					label13.BackColor = Color.Lime;
					break;
				case 7:
					label12.BackColor = Color.Lime;
					break;
				case 10:
					label19.BackColor = Color.Lime;
					break;
			}

			//Console.WriteLine("Status: " + status);

		}

		//PLC點位關
		private void button3_Click(object sender, EventArgs e)
		{
			int M = comboBox1.SelectedIndex % 8 + (comboBox1.SelectedIndex / 8) * 10;
			PLC1.setValue((PLC1.PLCContect)M, false);
			//PLC1.getValue((PLC1.PLCContect)M, out bool status);
			switch (M)
			{
				case 0:
					label117.BackColor = Color.Red;
					break;
				case 1:
					label9.BackColor = Color.Red;
					break;
				case 2:
					label10.BackColor = Color.Red;
					break;
				case 3:
					label11.BackColor = Color.Red;
					break;
				case 4:
					label15.BackColor = Color.Red;
					break;
				case 5:
					label14.BackColor = Color.Red;
					break;
				case 6:
					label13.BackColor = Color.Red;
					break;
				case 7:
					label12.BackColor = Color.Red;
					break;
				case 10:
					label19.BackColor = Color.Red;
					break;
			}
			//Console.WriteLine("Status: " + status);
		}
		#endregion

		#region 數據設定

		//讀取量測設定檔
		private void LoadMeasureIniBtn_Click(object sender, EventArgs e)
		{
			if (ProductNameBox.Text != string.Empty)
			{
				double.TryParse(SetupIniIP.IniReadValue("Measurement", "ODRatio", app.SaveProductMeasurepath + ProductNameBox.Text + ".ini"), out Value.ODRatio);
				double.TryParse(SetupIniIP.IniReadValue("Measurement", "IDRatio", app.SaveProductMeasurepath + ProductNameBox.Text + ".ini"), out Value.IDRatio);
				MessageBox.Show("讀取完成\n外徑轉換比例: " + Value.ODRatio.ToString() + "\n內徑轉換比例: " + Value.IDRatio.ToString(), "系統", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("讀取失敗！\n請輸入批號", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		//儲存量測設定檔
		private void SaveMeasureIniBtn_Click(object sender, EventArgs e)
		{
			if (ProductNameBox.Text != string.Empty)
			{
				SetupIniIP.IniWriteValue("Measurement", "ODRatio", Value.ODRatio.ToString(), app.SaveProductMeasurepath + ProductNameBox.Text + ".ini");
				SetupIniIP.IniWriteValue("Measurement", "IDRatio", Value.IDRatio.ToString(), app.SaveProductMeasurepath + ProductNameBox.Text + ".ini");
				MessageBox.Show("儲存完成\n外徑轉換比例: " + Value.ODRatio.ToString() + "\n內徑轉換比例: " + Value.IDRatio.ToString(), "系統", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("讀取失敗！\n請輸入批號", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		//更改天數按鈕
		private void DeleteImgDayButton_Click(object sender, EventArgs e)
		{
			try
			{
				SetupIniIP.IniWriteValue("Image", "DeleteTime", DeleteImgDayBox.Text, app.UISettingpath);
				BeginInvoke(new Action(() => MessageBox.Show("更改成功", "系統", MessageBoxButtons.OK, MessageBoxIcon.Information)));
				MessageBox.Show("更改成功");
			}
			catch
			{
				BeginInvoke(new Action(() => MessageBox.Show("更改失敗", "系統", MessageBoxButtons.OK, MessageBoxIcon.Error)));
				MessageBox.Show("更改失敗");
			}
		}

		//歷史照片按鈕
		private void Historyfolderbutton_Click(object sender, EventArgs e)
		{
			string file = @"C:\Windows\explorer.exe";
			System.Diagnostics.Process.Start(file, app.SaveHistoryImgpath);
		}

		//報表資料夾按鈕
		private void Paperfolderbutton_Click(object sender, EventArgs e)
		{
			string file = @"C:\Windows\explorer.exe";
			System.Diagnostics.Process.Start(file, app.SavePaperpath);
		}

		//趨勢圖時間間隔套用按鈕
		private void ChartTimeIntervalApply_Click(object sender, EventArgs e)
		{
			Num.ChartTime = 0;
			chart1.Series[0] = new System.Windows.Forms.DataVisualization.Charting.Series();
			chart1.ChartAreas[0].AxisX.CustomLabels.Clear();
			switch (ChartTimeInterval.SelectedIndex)
			{
				case 0:
					chart1.ChartAreas[0].AxisX.Maximum = 10;//設定X軸最大值
					break;
				case 1:
					chart1.ChartAreas[0].AxisX.Maximum = 10;//設定X軸最大值
					break;
				case 2:
					chart1.ChartAreas[0].AxisX.Maximum = 10;//設定X軸最大值
					break;
				case 3:
					chart1.ChartAreas[0].AxisX.Maximum = 10;//設定X軸最大值
					break;
				case 4:
					chart1.ChartAreas[0].AxisX.Maximum = 10;//設定X軸最大值
					break;
				case 5:
					chart1.ChartAreas[0].AxisX.Maximum = 10;//設定X軸最大值
					break;
				case 6:
					chart1.ChartAreas[0].AxisX.Maximum = 10;//設定X軸最大值
					break;
			}
			chart1.ChartAreas[0].AxisX.IsLabelAutoFit = true;

			for (int i = 0; i < 10; i++)
			{
				chart1.Series[0].Points.Add();
				//chart1.Series[0].Points[i].SetValueXY(i , 0);
				chart1.ChartAreas[0].AxisX.CustomLabels.Add(i - 0.25, i + 0.25, Num.ChartTime.ToString());
				Num.ChartTime += int.Parse(ChartTimeInterval.SelectedItem.ToString());
				Console.WriteLine("Time: " + Num.ChartTime);
			}
			Num.TimeInterval = int.Parse(ChartTimeInterval.SelectedItem.ToString()) * 60;
			BeginInvoke(new Action(() => MessageBox.Show("已套用圖表設定", "系統", MessageBoxButtons.OK, MessageBoxIcon.Information)));
		}

		//檢測數據歸零按鈕
		private void InspectStatisticReset_Click(object sender, EventArgs e)
		{
			Num.Reset();

			ResetLabelDivision();
			BeginInvoke(new Action(() => { NGNumPercentlabel.Text = Num.NGRate.ToString("P"); }));
			BeginInvoke(new Action(() => { OKNumPercentlabel.Text = Num.OKdRate.ToString("P"); }));
			BeginInvoke(new Action(() => { NULLNumPercentlabel.Text = Num.NULLRate.ToString("P"); }));
			BeginInvoke(new Action(() => { YieldRatelabel.Text = Num.YieldRate.ToString("P"); }));

			BeginInvoke(new Action(() => { NGNumlabel.Text = Num.NGNum.ToString(); }));
			BeginInvoke(new Action(() => { OKNumlabel.Text = Num.OKNum.ToString(); }));
			BeginInvoke(new Action(() => { NULLNumlabel.Text = Num.NULLNum.ToString(); }));
			BeginInvoke(new Action(() => { TotalNumlabel.Text = Num.TotalNum.ToString(); }));
			BeginInvoke(new Action(() => { SuccessNumlabel.Text = Num.TotalSuccessNum.ToString(); }));

			BeginInvoke(new Action(() => MessageBox.Show("檢測數量已歸零", "系統", MessageBoxButtons.OK, MessageBoxIcon.Information)));
		}

		//瑕疵數據歸零按鈕
		private void DefectStatisticReset_Click(object sender, EventArgs e)
		{
			Num.DefectReset();

			BeginInvoke(new Action(() => { CatchUpNumLabel.Text = Num.NG_CatchUp.ToString(); }));
			BeginInvoke(new Action(() => { OIDNumLabel.Text = Num.NG_OID.ToString(); }));
			BeginInvoke(new Action(() => { GapNumLabel.Text = Num.NG_Gap.ToString(); }));
			BeginInvoke(new Action(() => { CrackNumLabel.Text = Num.NG_Crack.ToString(); }));

			BeginInvoke(new Action(() => MessageBox.Show("瑕疵數據已歸零", "系統", MessageBoxButtons.OK, MessageBoxIcon.Information)));
		}

		//報表輸出按鈕
		private void PaperExport_Click(object sender, EventArgs e)
		{
			//Paper.Excelwrite();
			Console.WriteLine(Convert.ToInt64('B').ToString());
			Console.WriteLine(Convert.ToChar(65).ToString());
		}
		#endregion

		#endregion

		#endregion

		#region 各站檢測觸發按鈕
		private void CCD_1_ApplyBox_CheckedChanged(object sender, EventArgs e)
		{
			app.CCD_Apply[0] = CCD_1_ApplyBox.Checked;
		}

		private void CCD_2_ApplyBox_CheckedChanged(object sender, EventArgs e)
		{
			app.CCD_Apply[1] = CCD_2_ApplyBox.Checked;
		}

		private void CCD_3_ApplyBox_CheckedChanged(object sender, EventArgs e)
		{
			app.CCD_Apply[2] = CCD_3_ApplyBox.Checked;
		}

		private void CCD_4_ApplyBox_CheckedChanged(object sender, EventArgs e)
		{
			app.CCD_Apply[3] = CCD_4_ApplyBox.Checked;
		}
		#endregion

		#region 拍照
		bool stop = true;
		public void Blow()
		{
			Thread.Sleep(app.BlowDelay[0] - app.DelateShoot[3] - 1450); //10400-(7800-1450) = 4050
			BeginInvoke(new Action(() => { PLC1.setValue(PLC1.PLCContect.NG_Blow, true); }));
			Thread.Sleep(300);
			BeginInvoke(new Action(() => { PLC1.setValue(PLC1.PLCContect.NG_Blow, false); }));
		}

		public void OK_Blow()
		{
			Thread.Sleep(app.BlowDelay[1] - app.DelateShoot[3] - 1450);
			BeginInvoke(new Action(() => { PLC1.setValue(PLC1.PLCContect.OK_Blow, true); }));
			Thread.Sleep(300);
			BeginInvoke(new Action(() => { PLC1.setValue(PLC1.PLCContect.OK_Blow, false); }));
		}


		public void Work_5()
		{
			//Console.WriteLine("[Work]");
			ThreadPool.QueueUserWorkItem((o) =>
			{
				//Console.WriteLine("[STATE]: WORK START");
				//Console.WriteLine("[Work_5]");
				Thread.Sleep(app.DelateShoot[3] + 1500);
				string s1, s2, s3, s4;
				List<string> Ls1, Ls2, Ls3, Ls4;

				if (app.CCD_Apply[0] == false)
					Value.Result_1.Enqueue("OK");
				if (app.CCD_Apply[1] == false)
					Value.Result_2.Enqueue("OK");
				if (app.CCD_Apply[2] == false)
					Value.Result_3.Enqueue("OK");
				if (app.CCD_Apply[3] == false)
					Value.Result_4.Enqueue("OK");

				if (Value.Result_1.Count > 0 && Value.Result_2.Count > 0 && Value.Result_3.Count > 0 && Value.Result_4.Count > 0 && stop)
				{
					s1 = Value.Result_1.Dequeue();
					s2 = Value.Result_2.Dequeue();
					s3 = Value.Result_3.Dequeue();
					s4 = Value.Result_4.Dequeue();
					if (s1 == "NG" || s2 == "NG" || s3 == "NG" || s4 == "NG")
					{
						try
						{
							ThreadPool.QueueUserWorkItem(w => Blow());
							if (Value.NgType_1.Count > 0 || Value.NgType_2.Count > 0 || Value.NgType_3.Count > 0 || Value.NgType_4.Count > 0)
							{
								if (Value.NgType_1.Count > 0)
									Ls1 = Value.NgType_1.Dequeue();
								else
									Ls1 = new List<string>();

								if (Value.NgType_2.Count > 0)
									Ls2 = Value.NgType_2.Dequeue();
								else
									Ls2 = new List<string>();

								if (Value.NgType_3.Count > 0)
									Ls3 = Value.NgType_3.Dequeue();
								else
									Ls3 = new List<string>();

								if (Value.NgType_4.Count > 0)
									Ls4 = Value.NgType_4.Dequeue();
								else
									Ls4 = new List<string>();

								if (Ls1.Contains("OD") || Ls1.Contains("ID") ||
								Ls2.Contains("OD") || Ls2.Contains("ID") ||
								Ls3.Contains("OD") || Ls3.Contains("ID") ||
								Ls4.Contains("OD") || Ls4.Contains("ID"))
								{
									Num.Bool_OID = true;
								}
								if (Ls1.Contains("Gap") || Ls2.Contains("Gap") || Ls3.Contains("Gap") || Ls4.Contains("Gap"))
								{
									Num.Bool_Gap = true;
								}
								if (Ls1.Contains("Crack") || Ls2.Contains("Crack") || Ls3.Contains("Crack") || Ls4.Contains("Crack"))
								{
									Num.Bool_Crack = true;
								}
								if (Ls1.Contains("CatchUp") || Ls2.Contains("CatchUp") || Ls3.Contains("CatchUp") || Ls4.Contains("CatchUp"))
								{
									Num.Bool_CatchUp = true;
								}

								if (Num.Bool_OID)
								{
									Num.NG_OID++;
									BeginInvoke(new Action(() => { OIDNumLabel.Text = Num.NG_OID.ToString(); }));
									Num.Bool_OID = false;
								}
								if (Num.Bool_Gap)
								{
									Num.NG_Gap++;
									BeginInvoke(new Action(() => { GapNumLabel.Text = Num.NG_Gap.ToString(); }));
									Num.Bool_Gap = false;
								}
								if (Num.Bool_Crack)
								{
									Num.NG_Crack++;
									BeginInvoke(new Action(() => { CrackNumLabel.Text = Num.NG_Crack.ToString(); }));
									Num.Bool_Crack = false;
								}
								if (Num.Bool_CatchUp)
								{
									Num.NG_CatchUp++;
									BeginInvoke(new Action(() => { CatchUpNumLabel.Text = Num.NG_CatchUp.ToString(); }));
									Num.Bool_CatchUp = false;
								}
							}
							//Console.WriteLine("檢測結果: NG");
							BeginInvoke(new Action(() => { UpdateBtn("NG", ref Num.TotalNum_Temp); }));
							BeginInvoke(new Action(() => { OKNumPercentlabel.Text = Num.OKdRate.ToString("P"); }));
							BeginInvoke(new Action(() => { NGNumPercentlabel.Text = Num.NGRate.ToString("P"); }));
							BeginInvoke(new Action(() => { NULLNumPercentlabel.Text = Num.NULLRate.ToString("P"); }));
							BeginInvoke(new Action(() => { OKNumlabel.Text = Num.OKNum.ToString(); }));
							BeginInvoke(new Action(() => { NGNumlabel.Text = Num.NGNum.ToString(); }));
							BeginInvoke(new Action(() => { NULLNumlabel.Text = Num.NULLNum.ToString(); }));
							BeginInvoke(new Action(() => { YieldRatelabel.Text = Num.YieldRate.ToString("P"); }));
							BeginInvoke(new Action(() => { TotalNumlabel.Text = Num.TotalNum.ToString(); }));
							BeginInvoke(new Action(() => { SuccessNumlabel.Text = Num.TotalSuccessNum.ToString(); }));

						}
						catch (System.Exception e)
						{
							MessageBox.Show(e.ToString());
						}
						Thread.Sleep(50);

					}
					else if (s1 == "NULL" || s2 == "NULL" || s3 == "NULL" || s4 == "NULL")
					{
						if (app.IsAdjust)
						{
							BeginInvoke(new Action(() => { UpdateBtn("NULL", ref Num.TotalNum_Temp); }));
							app.NullWarning++;
							BeginInvoke(new Action(() => { OKNumPercentlabel.Text = Num.OKdRate.ToString("P"); }));
							BeginInvoke(new Action(() => { NGNumPercentlabel.Text = Num.NGRate.ToString("P"); }));
							BeginInvoke(new Action(() => { NULLNumPercentlabel.Text = Num.NULLRate.ToString("P"); }));
							BeginInvoke(new Action(() => { OKNumlabel.Text = Num.OKNum.ToString(); }));
							BeginInvoke(new Action(() => { NGNumlabel.Text = Num.NGNum.ToString(); }));
							BeginInvoke(new Action(() => { NULLNumlabel.Text = Num.NULLNum.ToString(); }));
							BeginInvoke(new Action(() => { YieldRatelabel.Text = Num.YieldRate.ToString("P"); }));
							BeginInvoke(new Action(() => { TotalNumlabel.Text = Num.TotalNum.ToString(); }));
							BeginInvoke(new Action(() => { SuccessNumlabel.Text = Num.TotalSuccessNum.ToString(); }));

							if (app.NullWarning >= app.NullWarningTarget)
							{
								lock (StartButton)
								{
									BeginInvoke(new Action(() => { StartButton.PerformClick(); }));
								}
							}
						}
						Thread.Sleep(50);
					}
					else if (s1 == "OK" && s2 == "OK" && s3 == "OK" && s4 == "OK")
					{
						try
						{
							ThreadPool.QueueUserWorkItem(w => OK_Blow());
							//Console.WriteLine("檢測結果: OK");
							BeginInvoke(new Action(() => { UpdateBtn("OK", ref Num.TotalNum_Temp); }));
							BeginInvoke(new Action(() => { OKNumPercentlabel.Text = Num.OKdRate.ToString("P"); }));
							BeginInvoke(new Action(() => { NGNumPercentlabel.Text = Num.NGRate.ToString("P"); }));
							BeginInvoke(new Action(() => { NULLNumPercentlabel.Text = Num.NULLRate.ToString("P"); }));
							BeginInvoke(new Action(() => { OKNumlabel.Text = Num.OKNum.ToString(); }));
							BeginInvoke(new Action(() => { NGNumlabel.Text = Num.NGNum.ToString(); }));
							BeginInvoke(new Action(() => { NULLNumlabel.Text = Num.NULLNum.ToString(); }));
							BeginInvoke(new Action(() => { YieldRatelabel.Text = Num.YieldRate.ToString("P"); }));
							BeginInvoke(new Action(() => { TotalNumlabel.Text = Num.TotalNum.ToString(); }));
							BeginInvoke(new Action(() => { SuccessNumlabel.Text = Num.TotalSuccessNum.ToString(); }));

						}
						catch (System.Exception e)
						{
							MessageBox.Show(e.ToString());
						}
						//Console.WriteLine("TotalNum_Temp: " + Num.TotalNum_Temp);
						Thread.Sleep(50);
					}

					if (app.GlassesClean >= 10)
					{
						lock (StartButton)
						{
							BeginInvoke(new Action(() => { StartButton.PerformClick(); }));
						}
					}
				}
				else
				{
					BeginInvoke(new Action(() => { UpdateBtn("N", ref Num.TotalNum_Temp); }));
				}
				//Console.WriteLine("[STATE]: WORK END");
			});
		}

		private void NGBlow_TextChanged(object sender, EventArgs e)
		{
			app.BlowDelay[0] = int.Parse(NGBlow.Text);
		}

		private void OKBlow_TextChanged(object sender, EventArgs e)
		{
			app.BlowDelay[1] = int.Parse(OKBlow.Text);
		}
		private void ResetLabelDivision()
		{
			BeginInvoke(new Action(() => { NgDivisionLabel_0.Text = Num.NgDivision[0].ToString(); }));
			BeginInvoke(new Action(() => { OkDivisionLabel_0.Text = Num.OkDivision[0].ToString(); }));
			BeginInvoke(new Action(() => { YrDivisionLabel_0.Text = Num.YieldRateDivision[0].ToString("P"); }));
			BeginInvoke(new Action(() => { NgDivisionLabel_1.Text = Num.NgDivision[1].ToString(); }));
			BeginInvoke(new Action(() => { OkDivisionLabel_1.Text = Num.OkDivision[1].ToString(); }));
			BeginInvoke(new Action(() => { YrDivisionLabel_1.Text = Num.YieldRateDivision[1].ToString("P"); }));
			BeginInvoke(new Action(() => { NgDivisionLabel_2.Text = Num.NgDivision[2].ToString(); }));
			BeginInvoke(new Action(() => { OkDivisionLabel_2.Text = Num.OkDivision[2].ToString(); }));
			BeginInvoke(new Action(() => { YrDivisionLabel_2.Text = Num.YieldRateDivision[2].ToString("P"); }));
			BeginInvoke(new Action(() => { NgDivisionLabel_3.Text = Num.NgDivision[3].ToString(); }));
			BeginInvoke(new Action(() => { OkDivisionLabel_3.Text = Num.OkDivision[3].ToString(); }));
			BeginInvoke(new Action(() => { YrDivisionLabel_3.Text = Num.YieldRateDivision[3].ToString("P"); }));
		}

		private void UpdateLabelDivision(string State, int CameraID)
		{
			switch (State)
			{
				case "NG":
					Num.NgDivision[CameraID]++;
					break;
				case "OK":
					Num.OkDivision[CameraID]++;
					break;
			}
			Num.YieldRateDivision[CameraID] = Num.OkDivision[CameraID] * 1.0 / (Num.OkDivision[CameraID] + Num.NgDivision[CameraID]);
			switch (CameraID)
			{
				case 0:
					BeginInvoke(new Action(() => { NgDivisionLabel_0.Text = Num.NgDivision[0].ToString(); }));
					BeginInvoke(new Action(() => { OkDivisionLabel_0.Text = Num.OkDivision[0].ToString(); }));
					BeginInvoke(new Action(() => { YrDivisionLabel_0.Text = Num.YieldRateDivision[0].ToString("P"); }));
					break;
				case 1:
					BeginInvoke(new Action(() => { NgDivisionLabel_1.Text = Num.NgDivision[1].ToString(); }));
					BeginInvoke(new Action(() => { OkDivisionLabel_1.Text = Num.OkDivision[1].ToString(); }));
					BeginInvoke(new Action(() => { YrDivisionLabel_1.Text = Num.YieldRateDivision[1].ToString("P"); }));
					break;
				case 2:
					BeginInvoke(new Action(() => { NgDivisionLabel_2.Text = Num.NgDivision[2].ToString(); }));
					BeginInvoke(new Action(() => { OkDivisionLabel_2.Text = Num.OkDivision[2].ToString(); }));
					BeginInvoke(new Action(() => { YrDivisionLabel_2.Text = Num.YieldRateDivision[2].ToString("P"); }));
					break;
				case 3:
					BeginInvoke(new Action(() => { NgDivisionLabel_3.Text = Num.NgDivision[3].ToString(); }));
					BeginInvoke(new Action(() => { OkDivisionLabel_3.Text = Num.OkDivision[3].ToString(); }));
					BeginInvoke(new Action(() => { YrDivisionLabel_3.Text = Num.YieldRateDivision[3].ToString("P"); }));
					break;
			}
		}
		#endregion

		#region PLC接收器
		public void PLCReceiver(List<int> Data)
		{
			//Console.WriteLine("拍照囉");
			if (app.Run)
			{
				if (Data[0] == 1 && app.Mode == 1 && app.TakePic == 0)
				{
					app.NoneItemWarning = DateTime.Now;
					app.TakePic++;
					app.PlcCounter[0]++;
					//Console.WriteLine("拍照囉");
					Work_5();
					//Console.WriteLine("拍照囉");
				}
				else if (Data[0] == 1 && app.Mode == 1 && app.TakePic == 1)
				{
					app.TakePic = 0;
					app.PlcCounter[1]++;
					//Console.WriteLine("不拍照");
				}
				else if (Data[0] == 2)
				{
					PLC1.reset();
					Value.Result_1.Clear();
					Value.Result_2.Clear();
					Value.Result_3.Clear();
					Value.Result_4.Clear();
					stop = false;
					BeginInvoke(new Action(() => StartButton.Text = "復歸"));
				}
			}

		}
		#endregion

		#region 相機接收器
		public void Receiver(int CameraID, Mat Src)
		{
			//Console.WriteLine("[Mode]: " + app.Mode);

			#region Live + 離線 模式
			if (app.Mode == 0)
			{
				Thread.Sleep(100);
				if (!app.Offline)
				{
					if (CameraID == 0)
					{
						Cv2.Flip(Src, Src, FlipMode.Y);
						BeginInvoke(new Action(() => { cherngerPictureBox1.Image = Src.ToBitmap(); }));
					}
					else if (CameraID == 1)
					{
						BeginInvoke(new Action(() => { cherngerPictureBox2.Image = Src.ToBitmap(); }));
						

					}
					else if (CameraID == 2)
					{
						Cv2.Flip(Src, Src, FlipMode.Y);
						BeginInvoke(new Action(() => { cherngerPictureBox3.Image = Src.ToBitmap(); }));
					}
					else if (CameraID == 3)
					{
						BeginInvoke(new Action(() => { cherngerPictureBox4.Image = Src.ToBitmap(); }));
					}
				}
				else if (app.Offline)
				{
					//Console.WriteLine("off");
					if (!app.OfflineShoot && CameraShootbutton.Text == "拍照")
					{
						if (CameraID == 0)
						{
							Cv2.Flip(Src, Src, FlipMode.Y);
							BeginInvoke(new Action(() => { cherngerPictureBox1.Image = Src.ToBitmap(); }));


						}
						else if (CameraID == 1)
						{
							BeginInvoke(new Action(() => { cherngerPictureBox2.Image = Src.ToBitmap(); }));

						}
						else if (CameraID == 2)
						{
							Cv2.Flip(Src, Src, FlipMode.Y);
							BeginInvoke(new Action(() => { cherngerPictureBox3.Image = Src.ToBitmap(); }));

						}
						else if (CameraID == 3)
						{
							BeginInvoke(new Action(() => { cherngerPictureBox4.Image = Src.ToBitmap(); }));

						}
					}
					else if (app.OfflineShoot || (!app.OfflineShoot && CameraShootbutton.Text == "繼續取樣"))
					{
						//Console.WriteLine("Offline");
						if (CameraID == 0 && app.OfflineShootID == 0)
						{
							//ManualResetEvent[] Waiting = new ManualResetEvent[1];
							//Waiting[0] = new ManualResetEvent(false);
							//Waiting[1] = new ManualResetEvent(false);
							Num.TotalNumSave_1_tmp++;
							Update(CameraID, app.CameraCount);
							Mat Dst = new Mat();
							string Result = string.Empty;
							/*
							ThreadPool.QueueUserWorkItem((o) =>
							{
								DateTime T = DateTime.Now;
								Result = SendToAi(Src, Dst);
								DateTime Now = DateTime.Now;
								string time_consuming = ((TimeSpan)(Now - T)).TotalMilliseconds.ToString("0");
								Console.WriteLine("[CCD 1]: " + time_consuming + " ms");
								//Waiting[0].Set();
							});
							*/

							//WaitHandle.WaitAll(Waiting);
							Console.WriteLine("[Result]: " + Result);

							//Console.WriteLine("Stop1: " + Output.State);
							//BeginInvoke(new Action(() => { cherngerPictureBox1.Image = Dst.ToBitmap(); }));
							//BeginInvoke(new Action(() => { cherngerPictureBox5.Image = Src.ToBitmap(); }));
							//UpdateLabelDivision(AOI.output.State , app.OfflineShootID);

							Num.TotalNumSave_1 = Num.TotalNumSave_1_tmp;
							//if (app.SavingMode == "ALL")
							//DoAoi_1(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);
							//else if (AOI.output.State == app.SavingMode)
							//DoAoi_1(AOI.input.Src1, AOI.output.Dst1, CameraID , app.SavingMode);

							app.OfflineShoot = false;
							My_Save_Image(Src, Src, CameraID, app.SavingMode, stop: 1);

						}
						else if (CameraID == 1 && app.OfflineShootID == 1)
						{
							Num.TotalNumSave_2_tmp++;
							//ReTestSrc = Src.Clone();
							Update(CameraID, app.CameraCount);
							//UnionAOI AOI = new UnionAOI(Src, UnionAOI.Perspective.BackSideBody);
							//AOI.UnionDetection2();
							//Console.WriteLine("[Result]: " + AOI.output.State);
							//BeginInvoke(new Action(() =>
							//{
							//    Result_CCD_2.Text = AOI.output.State;
							//    if (AOI.output.State == "NG")
							//        Result_CCD_2.BackColor = Color.Red;
							//    else if (AOI.output.State == "OK")
							//        Result_CCD_2.BackColor = Color.Green;
							//    else if (AOI.output.State == "NULL")
							//        Result_CCD_2.BackColor = Color.Yellow;
							//}));
							//lock (AOI.output.State)
							//{
							//    Value.Result_2.Enqueue(AOI.output.State);
							//}
							//                     BeginInvoke(new Action(() => { cherngerPictureBox2.Image = AOI.output.Dst1.ToBitmap(); }));
							//BeginInvoke(new Action(() => { cherngerPictureBox5.Image = Src.ToBitmap(); }));
							//UpdateLabelDivision(AOI.output.State, app.OfflineShootID);

							//Num.TotalNumSave_2 = Num.TotalNumSave_2_tmp;
							//if (app.SavingMode == "ALL")
							//	DoAoi_2(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);
							//else if (AOI.output.State == app.SavingMode)
							//                         DoAoi_2(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);

							app.OfflineShoot = false;

							My_Save_Image(Src, Src, CameraID, app.SavingMode, stop: 2);
						}
						else if (CameraID == 2 && app.OfflineShootID == 2)
						{
							Num.TotalNumSave_3_tmp++;
							//ReTestSrc = Src.Clone();
							Update(CameraID, app.CameraCount);
							//UnionAOI AOI = new UnionAOI(Src, UnionAOI.Perspective.InnerSideBody);
							//AOI.UnionDetection3();
							//Console.WriteLine("[Result]: " + AOI.output.State);
							//                     BeginInvoke(new Action(() =>
							//                     {
							//                         Result_CCD_3.Text = AOI.output.State;
							//                         if (AOI.output.State == "NG")
							//                             Result_CCD_3.BackColor = Color.Red;
							//                         else if (AOI.output.State == "OK")
							//                             Result_CCD_3.BackColor = Color.Green;
							//                         else if (AOI.output.State == "NULL")
							//                             Result_CCD_3.BackColor = Color.Yellow;
							//                     }));
							//                     lock (AOI.output.State)
							//                     {
							//                         Value.Result_3.Enqueue(AOI.output.State);
							//                     }
							//                     BeginInvoke(new Action(() => { cherngerPictureBox3.Image = AOI.output.Dst1.ToBitmap(); }));
							//BeginInvoke(new Action(() => { cherngerPictureBox5.Image = Src.ToBitmap(); }));
							//UpdateLabelDivision(AOI.output.State, app.OfflineShootID);

							//Num.TotalNumSave_3 = Num.TotalNumSave_3_tmp;
							//if (app.SavingMode == "ALL")
							//	DoAoi_3(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);
							//else if (AOI.output.State == app.SavingMode)
							//                         DoAoi_3(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);

							//Work_5_AI();
							app.OfflineShoot = false;
							My_Save_Image(Src, Src, CameraID, app.SavingMode,stop: 3);
						}
						else if (CameraID == 3 && app.OfflineShootID == 3)
						{
							Num.TotalNumSave_4_tmp++;
							//ReTestSrc = Src.Clone();
							Update(CameraID, app.CameraCount);
							//UnionAOI AOI = new UnionAOI(Src, UnionAOI.Perspective.OuterSideBody);
							//AOI.UnionDetection4();
							//Console.WriteLine("[Result]: " + AOI.output.State);
							//                     BeginInvoke(new Action(() =>
							//                     {
							//                         Result_CCD_4.Text = AOI.output.State;
							//                         if (AOI.output.State == "NG")
							//                             Result_CCD_4.BackColor = Color.Red;
							//                         else if (AOI.output.State == "OK")
							//                             Result_CCD_4.BackColor = Color.Green;
							//                         else if (AOI.output.State == "NULL")
							//                             Result_CCD_4.BackColor = Color.Yellow;
							//                     }));
							//                     lock (AOI.output.State)
							//                     {
							//                         Value.Result_4.Enqueue("OK");
							//                     }
							//                     BeginInvoke(new Action(() => { cherngerPictureBox4.Image = AOI.output.Dst1.ToBitmap(); }));
							//BeginInvoke(new Action(() => { cherngerPictureBox5.Image = Src.ToBitmap(); }));
							//UpdateLabelDivision(AOI.output.State, app.OfflineShootID);

							//Num.TotalNumSave_4 = Num.TotalNumSave_4_tmp;
							//if (app.SavingMode == "ALL")
							//	DoAoi_4(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);
							//else if (AOI.output.State == app.SavingMode)
							//                         DoAoi_4(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);

							app.OfflineShoot = false;
							//OfflineWork_5();
							My_Save_Image(Src, Src, CameraID, app.SavingMode, stop: 4);
						}
					}
				}
			}
			#endregion

			#region 調機 + 檢測模式
			else if (app.Mode == 1 && app.Offline == false)
			{
				#region 檢測模式
				if (app.IsAdjust == true)
				{
					#region 第一站
					if (CameraID == 0 && app.CCD_Apply[0] == true)
					{
						//if (StartButton.Text == "暫停檢測")
						//{
						//	app.SingleItemTime1.Reset();
						//	app.SingleItemTime1.Start();
						//}
						//else
						//{
						//	app.SingleItemTime1.Stop();
						//	app.SingleItemTime1.Reset();
						//}
						//Console.WriteLine("[AOI]: " + CameraID);
						//Update(CameraID, app.CameraCount);
						//Console.WriteLine("[STATE]: In CCD 1");
						//ManualResetEvent[] Waiting = new ManualResetEvent[1];
						//Waiting[0] = new ManualResetEvent(false);
						//Waiting[1] = new ManualResetEvent(false);

						Mat Dst = new Mat();
						string Result = string.Empty;
						ThreadPool.QueueUserWorkItem((o) =>
						{
							DateTime T = DateTime.Now;
							Stop1_Detector(Src, Dst);
							DateTime Now = DateTime.Now;
							string time_consuming = ((TimeSpan)(Now - T)).TotalMilliseconds.ToString("0");
							Console.WriteLine("[CCD 1]: " + time_consuming + " ms");
							//Waiting[0].Set();
						});

						//WaitHandle.WaitAll(Waiting);
						//Console.WriteLine("[Result]: " + Result);

						#region 控制結果
						//if (NGmodeBox.Checked)
						//{
						//	AOI.output.State = "NG";
						//}
						//else if (OKmodeBox.Checked)
						//{
						//	AOI.output.State = "OK";
						//}
						#endregion

						#region 輸出結果
						//lock (AOI.output.State)
						//{
						//	Value.Result_1.Enqueue(AOI.output.State);
						//}

						//Value.NgType_1.Enqueue(AOI.output.NG_Type);

						//BeginInvoke(new UpdateLabelTextDelegate(UpdateLabelText), Result_CCD_1, AOI.output.State);
						//BeginInvoke(new UpdateLabelBackColorDelegate(UpdateLabelBackColor), Result_CCD_1, AOI.output.State);
						#endregion

						#region 玻璃潔淨度
						//if (Output.GlassCleanArea > 150000)
						//{
						//	app.GlassesClean++;
						//}
						//else
						//{
						//	app.GlassesClean = 0;
						//}	
						#endregion

						//Console.WriteLine("[AOI 1]: " + AOI.output.State);
						//BeginInvoke(new UpdatePictureBoxImageDelegate(UpdatePictureBoxImage), cherngerPictureBox1, AOI.output.Dst1.ToBitmap());
						//UpdateLabelDivision(AOI.output.State, CameraID);

						#region 存圖
						//if (app.SavingMode == "ALL")
						//	DoAoi_1(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);
						//else if (AOI.output.State == app.SavingMode)
						//	DoAoi_1(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);
						#endregion
					}
					#endregion

					#region 第二站
					else if (CameraID == 1 && app.CCD_Apply[1] == true)
					{
						//if (StartButton.Text == "暫停檢測")
						//{
						//	app.SingleItemTime2.Reset();
						//	app.SingleItemTime2.Start();
						//}
						//else
						//{
						//	app.SingleItemTime2.Stop();
						//	app.SingleItemTime2.Reset();
						//}
						//Console.WriteLine("[AOI]: " + CameraID);
						//Update(CameraID, app.CameraCount);
						//Console.WriteLine("[STATE]: In CCD 2");

						Mat Dst = new Mat();
						ThreadPool.QueueUserWorkItem((o) =>
						{
							DateTime T = DateTime.Now;
							Stop2_Detector(Src, Dst);
							DateTime Now = DateTime.Now;
							string time_consuming = ((TimeSpan)(Now - T)).TotalMilliseconds.ToString("0");
							//Console.WriteLine("[CCD 2]: " + time_consuming + " ms");


							#region 輸出結果
							//if (AOI.output.MeasurementState == "NG" || AOI.output.State == "NG")
							//{
							//	AOI.output.State = "NG";
							//}
							//else if (AOI.output.MeasurementState == "OK" && AOI.output.State == "OK")
							//{
							//	AOI.output.State = "OK";
							//}

							//#region 控制結果
							//if (NGmodeBox.Checked)
							//{
							//	AOI.output.State = "NG";
							//}
							//else if (OKmodeBox.Checked)
							//{
							//	AOI.output.State = "OK";
							//}
							//#endregion

							//BeginInvoke(new UpdateLabelTextDelegate(UpdateLabelText), Result_CCD_2, AOI.output.State);
							//BeginInvoke(new UpdateLabelBackColorDelegate(UpdateLabelBackColor), Result_CCD_2, AOI.output.State);

							//lock (AOI.output.State)
							//{
							//	Value.Result_2.Enqueue(AOI.output.State);
							//}

							//Value.NgType_2.Enqueue(AOI.output.NG_Type);
							#endregion
							//Console.WriteLine("[AOI 2]: " + AOI.output.State);
							//BeginInvoke(new UpdatePictureBoxImageDelegate(UpdatePictureBoxImage), cherngerPictureBox2, AOI.output.Dst1.ToBitmap());
							//UpdateLabelDivision(AOI.output.State, CameraID);
							//BeginInvoke(new Action(() => { ODValueLabel.Text = AOI.output.ODCircle.Diameter.ToString("f4"); }));
							//BeginInvoke(new Action(() => { IDValueLabel.Text = AOI.output.IDCircle.Diameter.ToString("f4"); }));

							#region 存圖
							//if (app.SavingMode == "ALL")
							//	DoAoi_2(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);
							//else if (AOI.output.State == app.SavingMode)
							//	DoAoi_2(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);
							#endregion
							//Console.WriteLine("[STATE]: CCD 2 END");
						});
					}
					#endregion

					#region 第三站
					else if (CameraID == 2 && app.CCD_Apply[2] == true)
					{
						//if (StartButton.Text == "暫停檢測")
						//{
						//	app.SingleItemTime3.Reset();
						//	app.SingleItemTime3.Start();
						//}
						//else
						//{
						//	app.SingleItemTime3.Stop();
						//	app.SingleItemTime3.Reset();
						//}
						//Console.WriteLine("[AOI]: " + CameraID);
						//Update(CameraID, app.CameraCount);
						//Console.WriteLine("[STATE]: In CCD 3");

						Mat Dst = new Mat();
						#region 演算法										
						ThreadPool.QueueUserWorkItem((o) =>
						{
							DateTime T = DateTime.Now;
							Stop3_Detector(Src, Dst);
							//UnionAOI AOI = new UnionAOI(Src, UnionAOI.Perspective.InnerSideBody);
							//bool confirm = /*AOI.ClearNoise(AOI.input.Src1, UnionAOI.Perspective.InnerSideBody)*/true;
							//if (confirm)
							//{
							//	AOI.UnionDetection3();
							//	string S3 = ((TimeSpan)(DateTime.Now - T)).TotalMilliseconds.ToString("0");
							//	Console.WriteLine("[Work_3]: " + S3);
							//}
							//DateTime Now = DateTime.Now;
							//string time_consuming = ((TimeSpan)(Now - T)).TotalMilliseconds.ToString("0");
							//Console.WriteLine("[CCD 3]: " + time_consuming + " ms");

							//#region 控制結果
							//if (NGmodeBox.Checked)
							//{
							//	AOI.output.State = "NG";
							//}
							//else if (OKmodeBox.Checked)
							//{
							//	AOI.output.State = "OK";
							//}
							//#endregion

							//#region 輸出結果
							//lock (AOI.output.State)
							//{
							//	Value.Result_3.Enqueue(AOI.output.State);
							//}
							//Value.NgType_3.Enqueue(AOI.output.NG_Type);

							//BeginInvoke(new UpdateLabelTextDelegate(UpdateLabelText), Result_CCD_3, AOI.output.State);
							//BeginInvoke(new UpdateLabelBackColorDelegate(UpdateLabelBackColor), Result_CCD_3, AOI.output.State);
							//#endregion

							////Console.WriteLine("[AOI 3]: " + AOI.output.State);
							//BeginInvoke(new UpdatePictureBoxImageDelegate(UpdatePictureBoxImage), cherngerPictureBox3, AOI.output.Dst1.ToBitmap());
							//UpdateLabelDivision(AOI.output.State, CameraID);

							//#region 存圖
							//if (app.SavingMode == "ALL")
							//	DoAoi_3(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);
							//else if (AOI.output.State == app.SavingMode)
							//	DoAoi_3(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);
							//#endregion
							//Console.WriteLine("[STATE]: CCD 3 END");
						});
						#endregion
					}
					#endregion

					#region 第四站
					else if (CameraID == 3 && app.CCD_Apply[3] == true)
					{
						//if (StartButton.Text == "暫停檢測")
						//{
						//	app.SingleItemTime4.Reset();
						//	app.SingleItemTime4.Start();
						//}
						//else
						//{
						//	app.SingleItemTime4.Stop();
						//	app.SingleItemTime4.Reset();
						//}
						//Console.WriteLine("[AOI]: " + CameraID);
						//Update(CameraID, app.CameraCount);
						//Console.WriteLine("[STATE]: In CCD 4");

						Mat Dst = new Mat();
						ThreadPool.QueueUserWorkItem((o) =>
						{
							DateTime T = DateTime.Now;
							Stop4_Detector(Src, Dst);

							DateTime Now = DateTime.Now;
							string time_consuming = ((TimeSpan)(Now - T)).TotalMilliseconds.ToString("0");
							//Console.WriteLine("[CCD 4]: " + time_consuming + " ms");

							//#region 控制結果
							//if (NGmodeBox.Checked)
							//{
							//	AOI.output.State = "NG";
							//}
							//else if (OKmodeBox.Checked)
							//{
							//	AOI.output.State = "OK";
							//}
							//#endregion

							//#region 輸出結果
							//lock (AOI.output.State)
							//{
							//	Value.Result_4.Enqueue(AOI.output.State);
							//}
							//Value.NgType_4.Enqueue(AOI.output.NG_Type);

							//BeginInvoke(new UpdateLabelTextDelegate(UpdateLabelText), Result_CCD_4, AOI.output.State);
							//BeginInvoke(new UpdateLabelBackColorDelegate(UpdateLabelBackColor), Result_CCD_4, AOI.output.State);
							//#endregion

							////Console.WriteLine("[AOI 4]: " + AOI.output.State);
							//BeginInvoke(new UpdatePictureBoxImageDelegate(UpdatePictureBoxImage), cherngerPictureBox4, AOI.output.Dst1.ToBitmap());
							////UpdateLabelDivision(AOI.output.State, CameraID);

							//#region 存圖
							//if (app.SavingMode == "ALL")
							//	DoAoi_4(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);
							//else if (AOI.output.State == app.SavingMode)
							//	DoAoi_4(AOI.input.Src1, AOI.output.Dst1, CameraID, app.SavingMode);
							//#endregion
							////Console.WriteLine("[STATE]: CCD 4 END");
						});
					}
					#endregion
				}
				#endregion

				#region 調機模式
				else if (app.IsAdjust == false)
				{
					//Console.WriteLine("123456789 [ID]: " + CameraID);
					if (CameraID == 0)
					{
						//Console.WriteLine("[AOI]: Test " + app.AdjustCounter);
						//UnionAOI AOI = new UnionAOI(Src , UnionAOI.Perspective.FrontSideBody);
						////UnionAOI.Output Output = new UnionAOI.Output();
						////Output = AOI.Union_Adjustion(Input);

						//if (AOI.output.State == "OK")
						//{
						//	Value.AdjODRatio += AOI.output.AdjODRatio;
						//	Value.AdjIDRatio += AOI.output.AdjIDRatio;
						//	app.AdjustCounter++;
						//}

						//Value.Result_1.Enqueue("NULL");
						//Value.Result_2.Enqueue("NULL");
						//Value.Result_3.Enqueue("NULL");
						//Value.Result_4.Enqueue("NULL");
						//Console.WriteLine("State: " + AOI.output.State);
						//Console.WriteLine("ADJ: " + app.AdjustCounter);
						//BeginInvoke(new Action(() => { cherngerPictureBox1.Image = AOI.output.Dst1.ToBitmap(); }));
						//BeginInvoke(new Action(() => { ODValueLabel.Text = AOI.output.AdjODRatio.ToString("f4"); }));
						//BeginInvoke(new Action(() => { IDValueLabel.Text = AOI.output.AdjIDRatio.ToString("f4"); }));
						//if (app.AdjustCounter == 10)
						//{
						//	Value.AdjODRatio /= app.AdjustCounter;
						//	Value.AdjIDRatio /= app.AdjustCounter;
						//	Value.ODRatio = Value.AdjODRatio;
						//	Value.IDRatio = Value.AdjIDRatio;
						//	Num.NULLNum = -1;
						//	Console.WriteLine("OD: " + Value.ODRatio);
						//	Console.WriteLine("ID: " + Value.IDRatio);
						//	BeginInvoke(new Action(() => { StartButton.PerformClick(); }));
						//}
					}
				}
				#endregion
			}
			#endregion

			Application.DoEvents();
			//GC.Collect();
		}
		#endregion

		private string ReadResult(Int64 result)
		{
			string R = "NULL";
			if (result < 1)
			{
				R = "OK";
			}
			else
			{
				R = "NG";
			}
			return R;
		}

		#region 演算法

		#region 刷新PictureBox
		private delegate void UpdatePictureBoxImageDelegate(PictureBox picturebox, Bitmap bitmap);
		private void UpdatePictureBoxImage(PictureBox picturebox, Bitmap bitmap)
		{
			picturebox.Image = bitmap;
		}
		#endregion

		#region 刷新Label
		private delegate void UpdateLabelTextDelegate(Label label, String text);
		private void UpdateLabelText(Label label, String text)
		{
			label.Text = text;
		}

		private delegate void UpdateLabelBackColorDelegate(Label label, string state);
		private void UpdateLabelBackColor(Label label, string state)
		{
			switch (state)
			{
				case "OK":
					label.BackColor = Color.Green;
					break;
				case "NG":
					label.BackColor = Color.Red;
					break;
				case "NULL":
					label.BackColor = Color.Yellow;
					break;
				default:
					label.BackColor = Color.Green;
					break;
			}


		}
		#endregion
		// 建立資料夾
		private void My_Create_Folder()
		{
			for(int stop = 1 ; stop <= 4 ; stop++) { 

				if (!Directory.Exists(app.SaveHistoryImgpath + String.Format("CCD-{0}//", stop))) //若不存在新創資料夾
				{ 
					Directory.CreateDirectory(app.SaveHistoryImgpath + String.Format("CCD-{0}//", stop));
					Directory.CreateDirectory(app.Detect_SaveHistoryImgpath + String.Format("CCD-{0}//", stop));
				}
				else //如果存在，原來的也要刪掉
				{

					Directory.Delete(app.SaveHistoryImgpath + String.Format("CCD-{0}//", stop), true);
					Directory.CreateDirectory(app.SaveHistoryImgpath + String.Format("CCD-{0}//", stop));

					Directory.Delete(app.Detect_SaveHistoryImgpath + String.Format("CCD-{0}//", stop), true);
					Directory.CreateDirectory(app.Detect_SaveHistoryImgpath + String.Format("CCD-{0}//", stop));
				}

			}
		}
		//儲存圖 New
		private void My_Save_Image(Mat Src, Mat Dst, int CameraID, string SaveMode, int stop)
		{
			SaveIng(Dst, SavingMode.Decrease, (Num.TotalNumSave_1 % (app.Column * app.Row)).ToString(), app.SaveTmpImgpath + "CCD-1//", Picturetype.jpg);

			if (radioButton4.Checked)
			{
				if (!Directory.Exists(app.SaveHistoryImgpath + String.Format("CCD-{0}//", stop) + SaveMode + "//")) //若不存在新創資料夾
					Directory.CreateDirectory(app.SaveHistoryImgpath + String.Format("CCD-{0}//", stop) + SaveMode + "//");

				//儲存暫時原圖
				SaveIng(Src, SavingMode.Origin, Num.TotalNumSave_1.ToString(), app.SaveHistoryImgpath + String.Format("CCD-{0}//", stop) + SaveMode + "//", Picturetype.jpg);
			}
			else if (radioButton5.Checked)
			{
				if (!Directory.Exists(app.SaveHistoryImgpath + String.Format("CCD-{0}//", stop) + SaveMode + "//")) //若不存在新創資料夾
					Directory.CreateDirectory(app.SaveHistoryImgpath + String.Format("CCD-{0}//", stop) + SaveMode + "//");

				//儲存暫時原圖
				SaveIng(Src, SavingMode.Origin, Num.TotalNumSave_1.ToString(), app.SaveHistoryImgpath + String.Format("CCD-{0}//", stop) + SaveMode + "//", Picturetype.jpg);

				if (!Directory.Exists(app.Detect_SaveHistoryImgpath + String.Format("CCD-{0}//", stop) + SaveMode + "//")) //若不存在新創資料夾
					Directory.CreateDirectory(app.Detect_SaveHistoryImgpath + String.Format("CCD-{0}//", stop) + SaveMode + "//");
				//儲存暫時檢測圖
				SaveIng(Dst, SavingMode.Origin, (Num.TotalNumSave_1).ToString(), app.Detect_SaveHistoryImgpath + String.Format("CCD-{0}//", stop) + SaveMode + "//", Picturetype.jpg);
			}
		}
		#endregion

		#region 方法

		#region 計算各站觸發數量(偵錯用)
		private void Update(int CameraID, int[] CameraCount)
		{
			if (CameraID == 0)
				CameraCount[0]++;
			else if (CameraID == 1)
				CameraCount[1]++;
			else if (CameraID == 2)
				CameraCount[2]++;
			else if (CameraID == 3)
				CameraCount[3]++;
			else if (CameraID == 4)
				CameraCount[4]++;
			else if (CameraID == 5)
				CameraCount[5]++;

		}
		#endregion

		#region 存圖
		public void SaveIng(Mat Src, SavingMode mode, string fileName, string Savepath, Picturetype type)
		{
			switch (mode)
			{
				case SavingMode.Origin:
					{
						switch (type)
						{
							case Picturetype.bmp:
								{
									Cv2.ImWrite(Savepath + fileName + ".bmp", Src);
								}
								break;
							case Picturetype.jpg:
								{
									Cv2.ImWrite(Savepath + fileName + ".jpg", Src);
								}
								break;
						}
					}
					break;
				case SavingMode.Decrease:
					{
						#region 縮圖
						Mat dst = new Mat();
						OpenCvSharp.Size s;
						s.Width = (int)(Src.Width * app.Picpercent);
						s.Height = (int)(Src.Height * app.Picpercent);
						dst = new Mat(s, MatType.CV_8U);
						Cv2.Resize(Src, dst, s, 0, 0, InterpolationFlags.Linear);
						#endregion

						switch (type)
						{
							case Picturetype.bmp:
								{
									Cv2.ImWrite(Savepath + fileName + ".bmp", dst);
								}
								break;
							case Picturetype.jpg:
								{
									Cv2.ImWrite(Savepath + fileName + ".jpg", dst);
								}
								break;
						}
					}
					break;
			}
		}

		public void SaveIng(Mat Src, SavingMode mode, string fileName, string Savepath, Picturetype type, double SizePercent)
		{
			switch (mode)
			{
				case SavingMode.Origin:
					{
						switch (type)
						{
							case Picturetype.bmp:
								{
									Cv2.ImWrite(Savepath + fileName + ".bmp", Src);
								}
								break;
							case Picturetype.jpg:
								{
									Cv2.ImWrite(Savepath + fileName + ".jpg", Src);
								}
								break;
						}
					}
					break;
				case SavingMode.Decrease:
					{
						#region 縮圖
						Mat dst = new Mat();
						OpenCvSharp.Size s;
						s.Width = (int)(Src.Width * SizePercent);
						s.Height = (int)(Src.Height * SizePercent);
						dst = new Mat(s, MatType.CV_8U);
						Cv2.Resize(Src, dst, s, 0, 0, InterpolationFlags.Linear);
						#endregion

						switch (type)
						{
							case Picturetype.bmp:
								{
									Cv2.ImWrite(Savepath + fileName + ".bmp", dst);
								}
								break;
							case Picturetype.jpg:
								{
									Cv2.ImWrite(Savepath + fileName + ".jpg", dst);
								}
								break;
						}
					}
					break;
			}
		}
		#endregion

		#region 智慧卡
		private void KeyRemove(object sender, KeyRemoveArgs e)
		{
			if (e.isRemove)
			{
				MessageBox.Show("未插入智慧卡卡");
				//BeginInvoke(new Action(() => this.Enabled = false));
				Application.Exit();
			}
			else
				BeginInvoke(new Action(() => this.Enabled = true));


		}
		#endregion

		#region 權限
		private void onUserChange(List<ControlAvailable> AvailableItem)
		{
			//Console.WriteLine("OnUserChange");
			foreach (var item in AvailableItem)
			{
				switch (item.Name)
				{
					case "登入":
						LogButton.Text = item.Available ? "登出" : "登入";
						adminbutton.Enabled = item.Available ? true : false;
						StartButton.Enabled = item.Available ? true : false;
						break;
					case "操作員模式":
						//MessageBox.Show("你的權限: " + item.Name , "系統" , MessageBoxButtons.OK , MessageBoxIcon.Information);
						//radioButton1.Enabled = false;
						//checkBox1.Enabled = false;
						//NullWarningBox.Enabled = false;
						//groupBox1.Enabled = false;
						//groupBox2.Enabled = false;
						//CameraSettingbutton.Enabled = false;
						//xPanderPanel4.Enabled = false;
						//groupBox9.Enabled = false;
						//groupBox16.Enabled = false;
						//groupBox17.Enabled = false;
						//InitialSettingBtn.Enabled = false;
						//adminbutton.Enabled = false;
						//OKBlow.Enabled = false;
						//NGBlow.Enabled = false;
						break;
					case "工程師模式":
						//MessageBox.Show("你的權限: " + item.Name, "系統", MessageBoxButtons.OK, MessageBoxIcon.Information);
						radioButton1.Enabled = item.Available ? true : false;
						checkBox1.Enabled = item.Available ? true : false;
						NullWarningBox.Enabled = item.Available ? true : false;
						groupBox1.Enabled = item.Available ? true : false;
						groupBox2.Enabled = item.Available ? true : false;
						CameraSettingbutton.Enabled = item.Available ? true : false;
						CamerasettingSavebutton.Enabled = item.Available ? true : false;
						xPanderPanel4.Enabled = item.Available ? true : false;
						groupBox9.Enabled = item.Available ? true : false;
						groupBox16.Enabled = item.Available ? true : false;
						groupBox17.Enabled = item.Available ? true : false;
						InitialSettingBtn.Enabled = item.Available ? true : false;
						adminbutton.Enabled = item.Available ? true : false;
						OKBlow.Enabled = item.Available ? true : false;
						NGBlow.Enabled = item.Available ? true : false;
						label8.ForeColor = item.Available ? Color.Red : Color.Red;
						button2.ForeColor = item.Available ? Color.Red : Color.Red;
						button3.ForeColor = item.Available ? Color.Red : Color.Red;
						PLCcomChangebutton.ForeColor = item.Available ? Color.Red : Color.Red;
						CameraSettingbutton.ForeColor = item.Available ? Color.Red : Color.Red;
						CamerasettingSavebutton.ForeColor = item.Available ? Color.Red : Color.Red;
						label1.ForeColor = item.Available ? Color.Red : Color.Red;
						label2.ForeColor = item.Available ? Color.Red : Color.Red;
						break;
				}
			}
		}
		#endregion

		#region 照片刪除
		private void DeleteHistoryUmg(int day, string Deletepath)
		{
			//MessageBox.Show("檢查及刪除歷史照片中請稍後");
			//DateTime nowTime = DateTime.Now;
			//string[] directories = Directory.GetDirectories(Deletepath);
			//foreach (string directory in directories)
			//{
			//    DirectoryInfo directoryInfo = new DirectoryInfo(directory);
			//    TimeSpan t = DateTime.Now - directoryInfo.CreationTime;  //当前时间  减去 文件创建时间
			//    int Filedays = t.Days;
			//    if (Filedays > day)   //保存的时间 ；  单位：天
			//    {
			//        Directory.Delete(directory, true);
			//    }
			//}
		}
		#endregion

		#region 導入UI資料
		private void LoadUIData()
		{
			//DeleteImgDayBox.SelectedIndex = int.Parse(SetupIniIP.IniReadValue("Image", "DeleteTime", app.UISettingpath));
			PLC1ComBox.Text = SetupIniIP.IniReadValue("PLC1", "ComPort", app.UISettingpath);
			//PLC2ComBox.Text = SetupIniIP.IniReadValue("PLC2", "ComPort", app.UISettingpath);

		}
		#endregion

		#region 案件觸發事件

		private void btn_Click_1(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			TableLayoutPanel tablepanel = (TableLayoutPanel)btn.Parent;
			TableLayoutPanelCellPosition pos = tablepanel.GetCellPosition(btn);
			ButtoPicture B = new ButtoPicture();
			int Num = app.Column * (pos.Row) + pos.Column;

			if (btn.BackColor == Color.Green)
				B.Data(Num.ToString(), "OK", Num.ToString());
			else if (btn.BackColor == Color.Red)
				B.Data(Num.ToString(), "NG", Num.ToString());
			else if (btn.BackColor == Color.Yellow)
				B.Data(Num.ToString(), "NULL", Num.ToString());
			else
				B.Data(Num.ToString(), "", Num.ToString());
			B.ShowDialog();
		}

		private void UpdateBtn(string Result, ref int Count)
		{

			//Console.WriteLine("btn: " + Count);
			if (Count / 100 > 0)
			{
				Count -= (Count / 100) * 100;
			}
			int i = Count % app.Column;
			int j = Count / app.Column;


			Button btn = (Button)tableLayoutPanel1.GetControlFromPosition(i, j);
			switch (Result)
			{
				case "OK":
					btn.BackColor = Color.Green;
					break;
				case "NG":
					btn.BackColor = Color.Red;
					break;
				case "NULL":
					btn.BackColor = Color.Yellow;
					break;
				case "N":
					btn.BackColor = Color.Black;
					break;


			}
			lock (app.LockGreenRedLight)
			{
				Count++;

				if (Result == "OK")
					Num.OKNum++;
				else if (Result == "NG")
					Num.NGNum++;
				else
					Num.NULLNum++;

				Num.TotalNum = Num.NULLNum + Num.OKNum + Num.NGNum;
				Num.YieldRate = (Num.OKNum * 1.0 / (Num.OKNum + Num.NGNum));
				Num.TotalSuccessNum = Num.OKNum + Num.NGNum;
				Num.OKdRate = Num.OKNum * 1.0 / Num.TotalNum;
				Num.NGRate = Num.NGNum * 1.0 / Num.TotalNum;
				Num.NULLRate = Num.NULLNum * 1.0 / Num.TotalNum;

			}
		}
		#endregion

		#region 圖表初始

		private void ChartReset()
		{
			chart1.ChartAreas[0].AxisY.Minimum = 0;//設定Y軸最小值
												   //chart1.ChartAreas[0].AxisY.Maximum = 100;//設定Y軸最大值
			chart1.ChartAreas[0].AxisX.Minimum = 0;//設定X軸最小值
			chart1.ChartAreas[0].AxisX.Maximum = 10;//設定X軸最大值
			chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
			chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
			chart1.ChartAreas[0].Position.X = 0;
			chart1.ChartAreas[0].Position.Y = 15;
			chart1.ChartAreas[0].Position.Width = 100;
			chart1.ChartAreas[0].Position.Height = 85;
			chart1.ChartAreas[0].AxisX.IsLabelAutoFit = true;
			chart1.Series[0].IsValueShownAsLabel = false;

			ChartTimeInterval.SelectedIndex = 0;
			app.SavingMode = "ALL";

			for (int i = 0; i < 10; i++)
			{
				chart1.Series[0].Points.Add();
				//chart1.Series[0].Points[i].SetValueXY(i, 0);
				chart1.ChartAreas[0].AxisX.CustomLabels.Add(i - 0.25, i + 0.25, Num.ChartTime.ToString());
				Num.ChartTime += int.Parse(ChartTimeInterval.SelectedItem.ToString());
				Console.WriteLine("Time: " + Num.ChartTime);
			}
			Num.TimeInterval = int.Parse(ChartTimeInterval.SelectedItem.ToString()) * 60;
		}

		#endregion

		#region 圖片按鈕初始

		private void PictureButtonReset()
		{
			tableLayoutPanel1.RowCount = app.Row;
			tableLayoutPanel1.ColumnCount = app.Column;
			tableLayoutPanel1.Height = app.Row * (app.RowHeight + 2);
			tableLayoutPanel1.Width = app.Column * (app.ColumeWidth + 2);
			tableLayoutPanel1.RowStyles.Clear();
			tableLayoutPanel1.ColumnStyles.Clear();
			for (int j = 0; j < app.Column; j++)
			{
				tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, app.ColumeWidth));

			}
			for (int i = 0; i < app.Row; i++)
			{

				tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, app.RowHeight));


			}
			for (int j = 0; j < app.Column; j++)
			{

				for (int i = 0; i < app.Row; i++)
				{
					Button btn = new Button();
					btn.Dock = DockStyle.Fill;
					btn.BackColor = Color.Transparent;
					btn.Size = new System.Drawing.Size(0, 0);
					btn.Click += new EventHandler(btn_Click_1);
					btn.Enabled = true;
					tableLayoutPanel1.Controls.Add(btn, j, i);
				}
			}
		}

		#endregion

		#region 更新圖表

		private void UpdateChart()
		{
			Console.WriteLine("更新");
			int Success_N = Num.TotalSuccessNum;
			int Ok_N = Num.OKNum;
			double OKpercent_in_a_minute = 0.0;
			if (Num.ChartCount < 10)
			{
				if (Num.ChartCount == 0) {
					Success_N = Num.TotalSuccessNum;
					Ok_N = Num.OKNum;
				}
				else {
					Success_N -= Num.TotalSuccessChartNum;
					Ok_N -= Num.TotalOKChartNum;
				}
				Num.TotalSuccessChartNum = Num.TotalSuccessNum;
				Num.TotalOKChartNum = Num.OKNum;
				//Console.WriteLine("Chart: " + Num.ChartCount);
				OKpercent_in_a_minute = (double)Num.TotalOKChartNum / Num.TotalSuccessChartNum;
				BeginInvoke(new Action(() => chart1.Series[0].Points[Num.ChartCount - 1].SetValueY(OKpercent_in_a_minute)));
				Num.ChartCount++;
			}
			else
			{
				//Console.WriteLine("Chart>>>: " + Num.ChartCount);
				Num.ChartTime += int.Parse(ChartTimeInterval.SelectedItem.ToString());

				Success_N -= Num.TotalSuccessChartNum;
				Ok_N -= Num.TotalOKChartNum;
				Num.TotalSuccessChartNum = Num.TotalSuccessNum;
				Num.TotalOKChartNum = Num.OKNum;

				Num.ChartTime -= int.Parse(ChartTimeInterval.SelectedItem.ToString()) * 10;
				chart1.ChartAreas[0].AxisX.CustomLabels.Clear();
				for (int i = 0; i < 10; i++)
				{
					chart1.ChartAreas[0].AxisX.CustomLabels.Add(i - 0.25, i + 0.25, Num.ChartTime.ToString());
					Num.ChartTime += int.Parse(ChartTimeInterval.SelectedItem.ToString());
				}
				BeginInvoke(new Action(() => chart1.Series[0].Points[0].SetValueY(chart1.Series[0].Points[1].YValues.First())));
				BeginInvoke(new Action(() => chart1.Series[0].Points[1].SetValueY(chart1.Series[0].Points[2].YValues.First())));
				BeginInvoke(new Action(() => chart1.Series[0].Points[2].SetValueY(chart1.Series[0].Points[3].YValues.First())));
				BeginInvoke(new Action(() => chart1.Series[0].Points[3].SetValueY(chart1.Series[0].Points[4].YValues.First())));
				BeginInvoke(new Action(() => chart1.Series[0].Points[4].SetValueY(chart1.Series[0].Points[5].YValues.First())));
				BeginInvoke(new Action(() => chart1.Series[0].Points[5].SetValueY(chart1.Series[0].Points[6].YValues.First())));
				BeginInvoke(new Action(() => chart1.Series[0].Points[6].SetValueY(chart1.Series[0].Points[7].YValues.First())));
				BeginInvoke(new Action(() => chart1.Series[0].Points[7].SetValueY(chart1.Series[0].Points[8].YValues.First())));
				BeginInvoke(new Action(() => chart1.Series[0].Points[8].SetValueY(chart1.Series[0].Points[9].YValues.First())));

				OKpercent_in_a_minute = (double)Num.TotalOKChartNum / Num.TotalSuccessChartNum;
				BeginInvoke(new Action(() => chart1.Series[0].Points[9].SetValueY(OKpercent_in_a_minute)));
				//BeginInvoke(new Action(() => chart1.Series[0].Points[0].SetValueXY((Num.ChartCount - 9).ToString(), chart1.Series[0].Points[1].YValues.First())));
				//BeginInvoke(new Action(() => chart1.Series[0].Points[1].SetValueXY((Num.ChartCount - 8).ToString(), chart1.Series[0].Points[2].YValues.First())));
				//BeginInvoke(new Action(() => chart1.Series[0].Points[2].SetValueXY((Num.ChartCount - 7).ToString(), chart1.Series[0].Points[3].YValues.First())));
				//BeginInvoke(new Action(() => chart1.Series[0].Points[3].SetValueXY((Num.ChartCount - 6).ToString(), chart1.Series[0].Points[4].YValues.First())));
				//BeginInvoke(new Action(() => chart1.Series[0].Points[4].SetValueXY((Num.ChartCount - 5).ToString(), chart1.Series[0].Points[5].YValues.First())));
				//BeginInvoke(new Action(() => chart1.Series[0].Points[5].SetValueXY((Num.ChartCount - 4).ToString(), chart1.Series[0].Points[6].YValues.First())));
				//BeginInvoke(new Action(() => chart1.Series[0].Points[6].SetValueXY((Num.ChartCount - 3).ToString(), chart1.Series[0].Points[7].YValues.First())));
				//BeginInvoke(new Action(() => chart1.Series[0].Points[7].SetValueXY((Num.ChartCount - 2).ToString(), chart1.Series[0].Points[8].YValues.First())));
				//BeginInvoke(new Action(() => chart1.Series[0].Points[8].SetValueXY((Num.ChartCount - 1).ToString(), chart1.Series[0].Points[9].YValues.First())));

				//BeginInvoke(new Action(() => chart1.Series[0].Points[9].SetValueXY((Num.ChartCount).ToString(), N)));
				Num.ChartCount++;
			}
			BeginInvoke(new Action(() => chart1.ChartAreas[0].RecalculateAxesScale()));
			//Console.WriteLine("[每" + ChartTimeInterval.SelectedItem.ToString() + "分鐘檢測數量]: " + N);
			//if (Num.ChartCount <= 10)
			//{
			//	BeginInvoke(new Action(() => chart1.Series[0].Points[Num.ChartCount].SetValueXY( Num.ChartCount , N)));
			//	Num.ChartCount++;
			//}
			//else
			//{
			//	//ChartReset();
			//	Num.ChartCount = 0;
			//}
			//BeginInvoke(new Action(() => chart1.ChartAreas[0].RecalculateAxesScale()));
		}

		#endregion

		#region 計時顯示
		private void DateTimer_Tick(object sender, EventArgs e)
		{
			DateTime Time = DateTime.Now;
			DatetimeLabel.Text = Time.ToString();
		}
		private void RunTimer_Tick(object sender, EventArgs e)
		{
			WorkTimelabel.Text = String.Format("{0:00}:{1:00}:{2:00}", app.TotalSW.Elapsed.Hours, app.TotalSW.Elapsed.Minutes, app.TotalSW.Elapsed.Seconds);
			TotalRuntimelabel.Text = String.Format("{0:00}:{1:00}:{2:00}", app.RunningSW.Elapsed.Hours, app.RunningSW.Elapsed.Minutes, app.RunningSW.Elapsed.Seconds);
			if (app.CCD_Apply[0])
				Runtimelabel.Text = String.Format("{0:0.00}", app.SingleItemTime1.Elapsed.TotalSeconds);
			else if (app.CCD_Apply[1])
				Runtimelabel.Text = String.Format("{0:0.00}", app.SingleItemTime2.Elapsed.TotalSeconds);
			else if (app.CCD_Apply[2])
				Runtimelabel.Text = String.Format("{0:0.00}", app.SingleItemTime3.Elapsed.TotalSeconds);
			else if (app.CCD_Apply[3])
				Runtimelabel.Text = String.Format("{0:0.00}", app.SingleItemTime4.Elapsed.TotalSeconds);
			label48.Text = app.TakePic.ToString();
			label56.Text = app.PlcCounter[0].ToString();
			label57.Text = app.PlcCounter[1].ToString();
			label52.Text = Value.Result_1.Count.ToString();
			label53.Text = Value.Result_2.Count.ToString();
			label54.Text = Value.Result_3.Count.ToString();
			label55.Text = Value.Result_4.Count.ToString();

			if (app.SingleItemTime1.Elapsed.TotalSeconds >= app.NoneItemTime || app.SingleItemTime2.Elapsed.TotalSeconds >= app.NoneItemTime ||
				app.SingleItemTime3.Elapsed.TotalSeconds >= app.NoneItemTime || app.SingleItemTime4.Elapsed.TotalSeconds >= app.NoneItemTime)
			{
				if (!app.NoneItemBuzzer)
				{
					//app.SingleItemTime.Stop();
					//PLC1.setValue(PLC1.PLCContect.Buzzer, true);
					//Thread.Sleep(500);
					//PLC1.setValue(PLC1.PLCContect.Buzzer, false);
					//Thread.Sleep(5);
					//app.NoneItemBuzzer = true;
					//if (StartButton.Text == "暫停檢測")
					//	BeginInvoke(new Action(() => { StartButton.PerformClick(); }));
				}
			}
			#region 趨勢圖刷新
			//if (app.RunningSW.Elapsed.Seconds % app.ProductEfficient == 0 && app.RunningSW.Elapsed.Seconds >= app.ProductEfficient && app.IsUpdateChart != app.RunningSW.Elapsed.Seconds)
			//{
			//	UpdateChart();
			//	app.IsUpdateChart = app.RunningSW.Elapsed.Seconds;
			//	Console.WriteLine("[******Chart******]: " + app.RunningSW.Elapsed.Seconds);
			//}


			//OK percentage per minute
			if (app.UpdateChartSW.Elapsed.Seconds == 59)
			{
				//Console.WriteLine("[******Chart******]: " + app.UpdateChartSW.Elapsed.Seconds);
				UpdateChart();
				app.UpdateChartSW.Restart();
				//app.IsUpdateChart = app.RunningSW.Elapsed.Seconds;				
			}
			#endregion
		}
		#endregion

		public enum SavingMode
		{
			Origin = 1,
			Decrease = 2
		}

		public enum Picturetype
		{
			bmp = 1,
			jpg = 2
		}
		#endregion

		private void button1_Click(object sender, EventArgs e)
		{
			//Paper.Excelwrite();
			//Console.WriteLine(Convert.ToInt64('B').ToString());
			//Console.WriteLine(Convert.ToChar(65).ToString());
			//app.NullWarning++;
			//BeginInvoke(new Action(() => { NULLNumlabel.Text = app.NullWarning.ToString(); }));
			//Num.YieldRate = 10;
			//Num.OKNum = 80;
			//Num.NGNum = 30;
			//Num.YieldRate = (Num.OKNum * 1.0 / (Num.OKNum + Num.NGNum));
			//BeginInvoke(new Action(() => { NGNumlabel.Text = Num.NGNum.ToString(); }));
			//BeginInvoke(new Action(() => { YieldRatelabel.Text = Num.YieldRate.ToString("P") + "%"; }));

			//app.ProductEfficient;
			Num.TotalSuccessNum += 5;
			UpdateChart();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			app.IsAdjust = true;
			app.AdjustCounter = 10;
		}

		private void SortAsFileName(ref FileInfo[] arrFi)
		{
			Array.Sort(arrFi, delegate (FileInfo x, FileInfo y) { return x.ToString().Length.CompareTo(y.ToString().Length); });
		}

		private string UpdateResult(long ResultPtr)
		{
			if (ResultPtr == 0)
			{
				return "OK";
			}
			else if (ResultPtr == 1)
			{
				return "NG";
			}
			else
			{
				return "NULL";
			}
		}

		#region Chernger/Rex/Algorithm
		//TREX
		#region Stop1 and Stop2 Libraries
		static List<OpenCvSharp.Point[]> Mask_innercicle(ref Mat img)
		{
			Mat img_gaussian = Mat.Zeros(img.Size(), MatType.CV_8UC1);
			Cv2.GaussianBlur(img, img_gaussian, new OpenCvSharp.Size(9, 9), 0, 0);

			Mat thresh1 = img_gaussian.Threshold(180, 255, ThresholdTypes.Binary);

			OpenCvSharp.Point[][] contours;
			HierarchyIndex[] hierarchly;
			Cv2.FindContours(thresh1, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

			// find final circle 
			List<OpenCvSharp.Point[]> contours_final = new List<OpenCvSharp.Point[]>();

			foreach (OpenCvSharp.Point[] contour_now in contours)
			{
				if (Cv2.ContourArea(contour_now) > 1000000 && Cv2.ContourArea(contour_now) < 2500000)
				{
					contours_final.Add(contour_now);
				}

			}
			if (contours_final.Count < 2)
				return contours_final;
			///OpenCvSharp.Point[][] temp = new Point[1][];//for draw on image

			OpenCvSharp.Point[] contours_approx_innercircle;
			var contour_innercircle = contours_final[1];

				//temp[0] = contour_now;

			Point2f center;
			float radius;
			//Cv2.DrawContours(vis_rgb, temp, -1, Scalar.Green, thickness: -1);
			contours_approx_innercircle = Cv2.ApproxPolyDP(contour_innercircle, 0.001, true);//speedup
			Cv2.MinEnclosingCircle(contours_approx_innercircle, out center, out radius);
			//Cv2.Circle(img, (Point)center, (int)radius+ stop1_inner_circle_radius, 255, thickness: -1);
			//Cv2.Circle(vis_rgb, (Point)center, (int)radius, Scalar.White, thickness: -1);

			//==================================================outer contour - inner contour=====================================
			// variable
			OpenCvSharp.Point[][] temp = new OpenCvSharp.Point[1][];


			// inner contour
			Mat inner_contour_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
			//Point[] inner_contour = Cv2.ConvexHull(contours_final[1]);
			temp[0] = contours_final[1];
			//Cv2.DrawContours(inner_contour_img, temp, -1, 255, -1);
			Cv2.DrawContours(inner_contour_img, temp, -1, 255, -1);
			//Cv2.Circle(inner_contour_img, (Point)center, (int)radius + stop1_inner_circle_radius, 255, thickness: -1);

			// outer contour
			Mat outer_contour_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
			Mat outer_contour_img2 = new Mat(img.Size(), MatType.CV_8UC1, new Scalar(255));//initilize Mat with the value 255

			temp[0] = contours_final[0];
			Cv2.DrawContours(outer_contour_img, temp, -1, 255, -1);
			//outer contour2 in order to make mask area = 255
			Cv2.DrawContours(outer_contour_img2, temp, -1, 0, -1);
			//outer - inner
			Mat diff_mask = outer_contour_img - inner_contour_img;
			Mat diff_mask2 = inner_contour_img + outer_contour_img2;

			Mat image = Mat.Zeros(img.Size(), MatType.CV_8UC1);
			img.CopyTo(image, diff_mask);
			//in order to make mask area = 255
			img = image + diff_mask2;


			return contours_final;
		}
		static void FindContour_and_outer_defect(Mat img, List<OpenCvSharp.Point[]> contours_final, ref int nLabels, out int[,] stats, string mode)
		{
			//0: 內圈 ; 1: 外圈
			OpenCvSharp.Point[] contour_now;
			if (mode == "inner")
			{
				contour_now = contours_final[1];
			}
			else
			{
				contour_now = contours_final[0];
			}

			// variable
			OpenCvSharp.Point[][] temp = new OpenCvSharp.Point[1][];


			// ellipsecontour
			var ellipsecontour = Cv2.FitEllipse(contour_now);
			Mat convex_mask_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
			Cv2.Ellipse(convex_mask_img, ellipsecontour, 255, -1);


			// Contour
			temp[0] = contour_now;
			Mat contour_mask_img = Mat.Zeros(img.Size(), MatType.CV_8UC1);
			Cv2.DrawContours(contour_mask_img, temp, -1, 255, -1);

			// Subtraction 
			Mat diff_image = contour_mask_img ^ convex_mask_img;


			//Opening
			Mat kernel = Mat.Ones(5, 5, MatType.CV_8UC1);//改變凹角大小
			diff_image = diff_image.MorphologyEx(MorphTypes.Open, kernel);

			//Connected Component
			var labelMat = new MatOfInt();
			var statsMat = new MatOfInt();// Row: number of labels Column: 5
			var centroidsMat = new MatOfDouble();
			nLabels = Cv2.ConnectedComponentsWithStats(diff_image, labelMat, statsMat, centroidsMat);

			var labels = labelMat.ToRectangularArray();
			stats = statsMat.ToRectangularArray();
			var centroids = centroidsMat.ToRectangularArray();



		}
		static List<OpenCvSharp.Point[][]> canny_test(Mat Src, List<OpenCvSharp.Point[]> contours_final,int Stop)
		{
			int blursize1 = 0, blursize2 = 0, cannysize = 0, closekernel1 = 0, 
				closekernel2 = 0, borderignoresize = 0;
			if (Stop == 1)
			{
				blursize1 = 3;
				blursize2 = 3;
				cannysize = 111;
				closekernel1 = 5;
				closekernel2 = 5;
				borderignoresize = 10;
			}
			else if (Stop == 2)
			{
				blursize1 = 5;
				blursize2 = 7;
				cannysize = 90;
				closekernel1 = 5;
				closekernel2 = 5;
				borderignoresize = 3;
			}

			Mat Canny_Src = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
			//用adaptive threshold 濾出瑕疵
			Cv2.Blur(Src, Canny_Src, new OpenCvSharp.Size(blursize1, blursize2));

			Cv2.Canny(Canny_Src, Canny_Src, cannysize, 0);


			Mat kernel = Mat.Ones(closekernel1, closekernel2, MatType.CV_8UC1);//改變凹角大小
			Canny_Src = Canny_Src.MorphologyEx(MorphTypes.Close, kernel);

			OpenCvSharp.Point[][] temp = new OpenCvSharp.Point[1][];

			temp[0] = contours_final[0];
			Cv2.DrawContours(Canny_Src, temp, -1, 0, borderignoresize);
			temp[0] = contours_final[1];
			Cv2.DrawContours(Canny_Src, temp, -1, 0, borderignoresize);

			//Canny_Src.SaveImage("./result/OK/canny" + fileindex);

			// denoise
			OpenCvSharp.Point[][] contours;
			HierarchyIndex[] hierarchly;
			Cv2.FindContours(Canny_Src, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

			List<OpenCvSharp.Point[][]> final_area = new List<OpenCvSharp.Point[][]>();
			foreach (OpenCvSharp.Point[] contour_now in contours)
			{
				//用bounding rec 濾出white noise
				RotatedRect BoundingRectangle = Cv2.MinAreaRect(contour_now);
				if (BoundingRectangle.Size.Height * BoundingRectangle.Size.Width > 400)
				{
					OpenCvSharp.Point[][] temp_final = new OpenCvSharp.Point[1][];//記得放在裡面宣告
					OpenCvSharp.Point[] approx = Cv2.ApproxPolyDP(contour_now, 0.000, true);
					temp_final[0] = approx;
					final_area.Add(temp_final);
				}

			}


			//img_temp.SaveImage("./result/OK/test" + fileindex);
			return final_area;

		}


		#endregion
		#region AI 1
		private void Stop1_Detector(Mat Src, Mat Dst)
		{
			//lock (app.LockStop1)
			{
				Int64 OK_NG_Flag = 0;
				Mat vis_rgb = Src.CvtColor(ColorConversionCodes.GRAY2RGB);
				Mat Src_original = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
				Src.CopyTo(Src_original);
				//Console.WriteLine(vis_rgb.Size()+"  "+vis_rgb.Channels());

				var watch = new System.Diagnostics.Stopwatch();
				watch.Start();
				//==========================algorithm===============================
				//會把Src的ROI切出來(圓的外面和裡面切掉)存回ROI, return 內外圓輪廓
				List<OpenCvSharp.Point[]> contours_final = Mask_innercicle(ref Src);

				if (contours_final.Count < 2)
					OK_NG_Flag = 2;
				else
				{
					//========================= 找contour ===================================================
					//Find outer defect return 應該要畫的區域
					int nLabels_outer = 0;//number of labels
					int[,] stats_outer = null;
					FindContour_and_outer_defect(Src, contours_final, ref nLabels_outer, out stats_outer, "outer");


					int nLabels_inner = 0;//number of labels
					int[,] stats_inner = null;
					FindContour_and_outer_defect(Src, contours_final, ref nLabels_inner, out stats_inner, "inner");

					//====================Adaptive threshold inner defect==============================================
					List<OpenCvSharp.Point[][]> canny_defect = canny_test(Src, contours_final, Stop: 1);



					foreach (OpenCvSharp.Point[][] temp in canny_defect)
					{

						Cv2.DrawContours(vis_rgb, temp, -1, Scalar.Red, -1);
						OK_NG_Flag = 1;
					}

					for (int i = 0; i < nLabels_outer; i++)
					{

						int area = stats_outer[i, 4];

						if (area < 200000 && area < CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_max && area > CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_min)
						{
							vis_rgb.Rectangle(new Rect(stats_outer[i, 0], stats_outer[i, 1], stats_outer[i, 2], stats_outer[i, 3]), Scalar.Green, 3);
							OK_NG_Flag = 1;
						}
					}
					for (int i = 0; i < nLabels_inner; i++)
					{

						int area = stats_inner[i, 4];

						if (area < 200000 && area < CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_max && area > CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_min)
						{
							vis_rgb.Rectangle(new Rect(stats_inner[i, 0], stats_inner[i, 1], stats_inner[i, 2], stats_inner[i, 3]), Scalar.Green, 3);
							OK_NG_Flag = 1;
						}
					}
				}
				//==================================================================
				++Num.TotalNumSave_1;
				//===============收OK或是NG的數字: 收到0代表OK 收到1代表NG
				//byte[] OK_NG_Flag_Buf = new byte[8];
				ImgAI_1.Enqueue(vis_rgb);
				OutputAI_1.Enqueue(OK_NG_Flag);
				//========================================================
				this.Invoke((EventHandler)delegate
				{
					Mat DST = ImgAI_1.Dequeue();
					app.SavingMode = OK_NG_Flag.ToString();
					//Thread.Sleep(50);
					BeginInvoke(new Action(() => { cherngerPictureBox1.Image = DST.ToBitmap(); }));
					#region 存圖
					My_Save_Image(Src_original, DST, 1, app.SavingMode, stop: 1);
					#endregion

					#region 輸出結果
					lock (OutputAI_1)
					{
						TestCount_1++;
						string Result = UpdateResult(OutputAI_1.Dequeue());
						Value.Result_1.Enqueue(Result);
						BeginInvoke(new UpdateLabelTextDelegate(UpdateLabelText), Result_CCD_1, Result);
						BeginInvoke(new UpdateLabelBackColorDelegate(UpdateLabelBackColor), Result_CCD_1, Result);
						BeginInvoke(new UpdateLabelTextDelegate(UpdateLabelText), label_test_1, TestCount_1.ToString());
						UpdateLabelDivision(Result, 0);
						//Work_5_AI();
					}
					#endregion

				});

			}

		}
		#endregion
		#region AI 2
		private void Stop2_Detector(Mat Src, Mat Dst)
		{
			lock (app.LockStop2)
			{
				Int64 OK_NG_Flag = 0;
				Mat vis_rgb = Src.CvtColor(ColorConversionCodes.GRAY2RGB);
				Mat Src_original = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
				Src.CopyTo(Src_original);
				//Console.WriteLine(vis_rgb.Size()+"  "+vis_rgb.Channels());
				
				//==========================algorithm===============================
				//mask the inner part noise of src
				List<OpenCvSharp.Point[]> contours_final = Mask_innercicle(ref Src);
				//return NULL if not find the contour of circle

				if (contours_final.Count < 2)
					OK_NG_Flag = 2;
				else
				{
					int nLabels_outer = 0;//number of labels
					int[,] stats_outer = null;
					FindContour_and_outer_defect(Src, contours_final, ref nLabels_outer, out stats_outer, "outer");


					int nLabels_inner = 0;//number of labels
					int[,] stats_inner = null;
					FindContour_and_outer_defect(Src, contours_final, ref nLabels_inner, out stats_inner, "inner");

					//====================Adaptive threshold inner defect==============================================
					List<OpenCvSharp.Point[][]> canny_defect = canny_test(Src, contours_final, Stop: 2);



					foreach (OpenCvSharp.Point[][] temp in canny_defect)
					{

						Cv2.DrawContours(vis_rgb, temp, -1, Scalar.Red, 3);
						OK_NG_Flag = 1;
					}

					for (int i = 0; i < nLabels_inner; i++)
					{
						int area = stats_inner[i, 4];
						if (area < 200000
							&& area < CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_max
							&& area > CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_min)
						{

							OK_NG_Flag = 1;
							vis_rgb.Rectangle(new Rect(stats_inner[i, 0], stats_inner[i, 1], stats_inner[i, 2], stats_inner[i, 3]), Scalar.Green, 3);
						}
					}

					for (int i = 0; i < nLabels_outer; i++)
					{
						int area = stats_outer[i, 4];
						if (area < 200000
							&& area < CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_max
							&& area > CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_min)
						{

							OK_NG_Flag = 1;
							vis_rgb.Rectangle(new Rect(stats_outer[i, 0], stats_outer[i, 1], stats_outer[i, 2], stats_outer[i, 3]), Scalar.Green, 3);
						}
					}

					//==================================================================
				}
				
				++Num.TotalNumSave_2;
				//close Stop2 
				//OK_NG_Flag = 0;
				ImgAI_2.Enqueue(vis_rgb);
				OutputAI_2.Enqueue(OK_NG_Flag);

				//========================================================
				this.Invoke((EventHandler)delegate
				{
					Mat DST = ImgAI_2.Dequeue();
					app.SavingMode = OK_NG_Flag.ToString();
					//Thread.Sleep(50);
					BeginInvoke(new Action(() => { cherngerPictureBox2.Image = DST.ToBitmap(); }));
					#region 存圖
					My_Save_Image(Src_original, DST, 1, app.SavingMode, stop: 2);
					#endregion

					#region 輸出結果
					lock (OutputAI_2)
					{
						TestCount_2++;
						string Result = UpdateResult(OutputAI_2.Dequeue());
						Value.Result_2.Enqueue(Result);//Result
						BeginInvoke(new UpdateLabelTextDelegate(UpdateLabelText), Result_CCD_2, Result);
						BeginInvoke(new UpdateLabelBackColorDelegate(UpdateLabelBackColor), Result_CCD_2, Result);
						BeginInvoke(new UpdateLabelTextDelegate(UpdateLabelText), label_test_2, TestCount_2.ToString());

						UpdateLabelDivision(Result, 1);
						//Work_5_AI();
					}
					#endregion

				});
			}

		}
		#endregion
		#region AI 3
		private void Stop3_Detector(Mat Src, Mat Dst)
		{
			//============================threshold===================
			Int64 OK_NG_Flag = 0;
			//=========================================================
			int blur_size = 3;
			int neighbor_degree = 5;
			
			//==========================algorithm====================
			Mat vis_rgb = Src.CvtColor(ColorConversionCodes.GRAY2RGB);
			
			Mat thresh1 = Src.Threshold(70, 255, ThresholdTypes.Binary);

			Mat kernel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(4, 3));
			thresh1 = thresh1.MorphologyEx(MorphTypes.Open, kernel);

			//=========================find contours================
			OpenCvSharp.Point[][] contours;
			HierarchyIndex[] hierarchly;
			Cv2.FindContours(thresh1, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

			// find final circle 
			List<OpenCvSharp.Point[]> contours_final = new List<OpenCvSharp.Point[]>();
			List<int> index = new List<int>();
			int count = 0;//replace for loop index
			foreach (OpenCvSharp.Point[] contour_now in contours)
			{
				//if (np.array(contours[i]).shape[0] > 1500 and cv2.contourArea(contours[i]) < 5000000):
				if (Cv2.ContourArea(contour_now) > 300000 && Cv2.ContourArea(contour_now) < 500000)
				{
					OpenCvSharp.Point[] approx = Cv2.ApproxPolyDP(contour_now, 0.005, true);
					contours_final.Add(approx);
					index.Add(count);
				}
				count++;

			}
			if (contours_final.Count < 1)
			{
				OK_NG_Flag = 2;
			}
			else
			{


				//==========================================find outer circle==============================================
				Point2f center;
				float radius;
				Cv2.MinEnclosingCircle(contours_final[0], out center, out radius);

				Mat mask_img = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
				Cv2.Circle(mask_img, (OpenCvSharp.Point)center, (int)(radius + 7), 255, thickness: -1);

				//outer contour2 in order to make mask area = 255
				Mat mask_img2 = new Mat(Src.Size(), MatType.CV_8UC1, new Scalar(255));//initilize Mat with the value 255
				Cv2.Circle(mask_img2, (OpenCvSharp.Point)center, (int)(radius + 7), 0, thickness: -1);


				Mat image = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
				Src.CopyTo(image, mask_img);

				//in order to make mask area = 255
				image = image + mask_img2;
				//image.SaveImage("./mask.jpg");
				//============================now already find the ROI, start algorithm=====================================
				Cv2.Blur(image, image, ksize: new OpenCvSharp.Size(blur_size, blur_size));
				//image.SaveImage("./mask2.jpg");

				//==========================create 0.5 degree a line===================================================
				double factor = 3.141592653589793 / 180;
				List<OpenCvSharp.Point> outer_index = new List<OpenCvSharp.Point>();
				List<OpenCvSharp.Point> inner_index = new List<OpenCvSharp.Point>();
				double degree_delta = 0.5;
				int cx = (int)center.X;
				int cy = (int)center.Y;
				int r = (int)radius;
				//Console.WriteLine(r + " "+ cx + " "+ cy);

				for (int degree = 0; degree < (360 / degree_delta); degree++)
				{
					double degree_real = degree * degree_delta;
					int now_x = (int)((r + 2) * Math.Sin(degree_real * factor)) + cx;
					int now_y = (int)((r + 2) * Math.Cos(degree_real * factor)) + cy;
					int now_inner_x = (int)((r - 50) * Math.Sin(degree_real * factor)) + cx;
					int now_inner_y = (int)((r - 50) * Math.Cos(degree_real * factor)) + cy;

					inner_index.Add(new OpenCvSharp.Point(now_inner_x, now_inner_y));
					outer_index.Add(new OpenCvSharp.Point(now_x, now_y));
				}
				//==========================shot from center=========================================
				List<int> all_valley_list = new List<int>();
				List<int> all_peak_list = new List<int>();
				List<int> all_diff_list = new List<int>();
				List<int> Candidate_1_phase_index = new List<int>();
				List<byte> value = new List<byte>();
				for (int degree = 0; degree < (360 / degree_delta); degree++)
				{

					LineIterator Line = new LineIterator(image, inner_index[degree], outer_index[degree]);
					foreach (var lip in Line)
					{
						value.Add(lip.GetValue<byte>());
					}

					int peak = 255;
					int valley = 255;
					int peak_index = 0;
					int valley_index = 0;


					int temp_valley = 255;
					int temp_valley_index = 0;
					int max_diff = 0;

					for (int pts_index = 1; pts_index < value.Count - 1; pts_index++)//peak of valley will not at 0 and last element.
					{

						if (max_diff < value[pts_index] - temp_valley)
						{

							max_diff = value[pts_index] - temp_valley;
							peak_index = pts_index;
							//peak = (value[pts_index]+ value[pts_index-1]+ value[pts_index]+1)/3;
							peak = value[pts_index];
							valley = temp_valley;
							valley_index = temp_valley_index;
						}
						if (temp_valley > value[pts_index])
						{
							//temp_valley = (value[pts_index]+ value[pts_index-1]+ value[pts_index + 1])/3;
							temp_valley = (value[pts_index]);
							temp_valley_index = pts_index;

						}
					}

					all_peak_list.Add(peak);
					all_valley_list.Add(valley);
					all_diff_list.Add(peak - valley);
					//phase1
					if (valley > 120 || peak - valley < CherngerUI.ImageProcessingDefect_Value.stop3_threshold_1phase)
					{
						Candidate_1_phase_index.Add(degree);
					}

					//Console.WriteLine("Count:"+Candidate_1_phase_index.Count);
					value.Clear();
				}
				//phase 2
				foreach (var candidate_degree in Candidate_1_phase_index)
				{
					//Console.WriteLine(candidate_degree);
					int now_valley_value = all_valley_list[candidate_degree];
					int prev_valley_value = all_valley_list[(candidate_degree + neighbor_degree + 720) % 720];
					int next_valley_value = all_valley_list[(candidate_degree - neighbor_degree + 720) % 720];

					int now_peak_value = all_peak_list[candidate_degree];
					int prev_peak_value = all_peak_list[(candidate_degree + neighbor_degree + 720) % 720];
					int next_peak_value = all_peak_list[(candidate_degree - neighbor_degree + 720) % 720];

					int now_peak_valley_difference = all_diff_list[candidate_degree];
					int prev_peak_valley_difference = all_diff_list[(candidate_degree + neighbor_degree + 720) % 720];
					int next_peak_valley_difference = all_diff_list[(candidate_degree - neighbor_degree + 720) % 720];

					float value1 = ((float)now_valley_value - (float)prev_valley_value) + ((float)now_valley_value - (float)next_valley_value);
					float value2 = ((float)prev_peak_valley_difference - (float)now_peak_valley_difference) + ((float)next_peak_valley_difference - (float)now_peak_valley_difference);
					if ((((value1 > CherngerUI.ImageProcessingDefect_Value.stop3_threshold_2phase_1)) && (value2 > CherngerUI.ImageProcessingDefect_Value.stop3_threshold_2phase_2)) && (value2 > 0) && (value1 > 0))
					{
						//Console.WriteLine(candidate_degree + " " + now_peak_value + " " + now_valley_value + " " + (now_peak_value - now_valley_value) + " " + value1 + " " + value2);

						Cv2.Circle(vis_rgb, outer_index[candidate_degree], 30, new Scalar(0, 255, 255), thickness: 5);
						OK_NG_Flag = 1;
					}

				}
			}
			//===============收OK或是NG的數字: 收到0代表OK 收到1代表NG
			++Num.TotalNumSave_3;
			//close Stop2 
			OK_NG_Flag = 0;
			ImgAI_3.Enqueue(vis_rgb);
			OutputAI_3.Enqueue(OK_NG_Flag);
			
			//Console.WriteLine("ReceiveByte: " + Recv_Byte + " " + "OK_NG_Flag3: " + OK_NG_Flag + "Stop: " + Stop);
			//========================================================
			this.Invoke((EventHandler)delegate
			{
				Mat DST = ImgAI_3.Dequeue();
				app.SavingMode = OK_NG_Flag.ToString();
				//Thread.Sleep(50);
				BeginInvoke(new Action(() => { cherngerPictureBox3.Image = DST.ToBitmap(); }));
				#region 存圖
				My_Save_Image(Src, DST, 1, app.SavingMode, stop: 3);
				#endregion

				#region 輸出結果
				lock (OutputAI_3)
				{
					TestCount_3++;
					string Result = UpdateResult(OutputAI_3.Dequeue());
					Value.Result_3.Enqueue(Result);
					BeginInvoke(new UpdateLabelTextDelegate(UpdateLabelText), Result_CCD_3, Result);
					BeginInvoke(new UpdateLabelBackColorDelegate(UpdateLabelBackColor), Result_CCD_3, Result);
					BeginInvoke(new UpdateLabelTextDelegate(UpdateLabelText), label_test_3, TestCount_3.ToString());

					UpdateLabelDivision(Result, 2);
					//Work_5_AI();
				}
				#endregion

			});
		}
		#endregion
		#region AI 4
		private void Stop4_Detector(Mat Src, Mat Dst)
		{

			Mat vis_rgb = Src.CvtColor(ColorConversionCodes.GRAY2RGB);
			int OK_NG_Flag = 0;
			//==================================================find real oring===============================================
			OpenCvSharp.Point[][] contours;
			HierarchyIndex[] hierarchly;
			Mat thresh1 = Src.Threshold(240, 255, ThresholdTypes.Binary);
			Cv2.FindContours(thresh1, out contours, out hierarchly, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

			// find final circle 
			List<OpenCvSharp.Point[]> contours_final = new List<OpenCvSharp.Point[]>();
			List<OpenCvSharp.Point[]> approx_list = new List<OpenCvSharp.Point[]>();

			foreach (OpenCvSharp.Point[] contour_now in contours)
			{
				if (Cv2.ContourArea(contour_now) > 300000 && Cv2.ContourArea(contour_now) < 800000)
				{
					contours_final.Add(contour_now);
					OpenCvSharp.Point[] approx = Cv2.ApproxPolyDP(contour_now, 0.5, true);
					approx_list.Add(approx);
				}

			}

			if (approx_list.Count < 2)
			{
				//cannot find the contour exception => return NULL
				OK_NG_Flag = 2;
			}
			else
			{
				//==================================================outer contour - inner contour=====================================
				// variable
				OpenCvSharp.Point[][] temp = new OpenCvSharp.Point[1][];
				// inner contour
				Mat inner_contour_img = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
				OpenCvSharp.Point[] inner_contour = Cv2.ConvexHull(approx_list[1]);
				temp[0] = inner_contour;
				Cv2.DrawContours(inner_contour_img, temp, -1, 255, -1);

				// outer contour
				Mat outer_contour_img = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
				Mat outer_contour_img2 = new Mat(Src.Size(), MatType.CV_8UC1, new Scalar(255));//initilize Mat with the value 255

				temp[0] = approx_list[0];
				Cv2.DrawContours(outer_contour_img, temp, -1, 255, -1);
				//outer contour2 in order to make mask area = 255
				Cv2.DrawContours(outer_contour_img2, temp, -1, 0, -1);
				//outer - inner
				Mat diff_mask = outer_contour_img - inner_contour_img;
				Mat diff_mask2 = inner_contour_img + outer_contour_img2;

				Mat image = Mat.Zeros(Src.Size(), MatType.CV_8UC1);
				Src.CopyTo(image, diff_mask);
				//in order to make mask area = 255
				image = image + diff_mask2;

				//=======================circle ignore=========================
				Point2f center;
				float radius;
				Mat circle_mask = new Mat(Src.Size(), MatType.CV_8UC1, new Scalar(255));
				Cv2.MinEnclosingCircle(approx_list[0], out center, out radius);
				Cv2.Circle(circle_mask, (OpenCvSharp.Point)center, (int)(radius - CherngerUI.ImageProcessingDefect_Value.stop4_ignore_radius), 0, thickness: -1);
				circle_mask.CopyTo(image, circle_mask);
				//================================use threshold to find defect==========================================
				OpenCvSharp.Point[][] contours2;
				HierarchyIndex[] hierarchly2;
				Mat thresh2 = image.Threshold(95, 255, ThresholdTypes.BinaryInv);
				//thresh2.SaveImage("./thresh2.jpg");
				Mat kernel = Mat.Ones(7, 7, MatType.CV_8UC1);//改變凹角大小
				thresh2 = thresh2.MorphologyEx(MorphTypes.Dilate, kernel);
				//thresh2.SaveImage("./thresh2_Dilate.jpg");
				Cv2.FindContours(thresh2, out contours2, out hierarchly2, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

				foreach (OpenCvSharp.Point[] contour_now in contours2)
				{
					if (Cv2.ContourArea(contour_now) > CherngerUI.ImageProcessingDefect_Value.stop4_black_defect_area_min &&
						Cv2.ContourArea(contour_now) < 20000 &&
						Cv2.ContourArea(contour_now) < CherngerUI.ImageProcessingDefect_Value.stop4_black_defect_area_max &&
						(Cv2.ArcLength(contour_now, true) / Cv2.ContourArea(contour_now)) < CherngerUI.ImageProcessingDefect_Value.stop4_arclength_area_ratio)
					{
						//Console.WriteLine("Arc Length: " + (Cv2.ArcLength(contour_now, true) + " Area: " + Cv2.ContourArea(contour_now))+" Length/Area:" +(Cv2.ArcLength(contour_now, true) / Cv2.ContourArea(contour_now)));
						OpenCvSharp.Point[] approx = Cv2.ApproxPolyDP(contour_now, 0.000, true);
						temp[0] = approx;
						Cv2.Polylines(vis_rgb, temp, true, new Scalar(0, 0, 255), 1);
						OK_NG_Flag = 1;
					}

				}
			}
			++Num.TotalNumSave_4;

			//close Stop4
			OK_NG_Flag = 0;
			ImgAI_4.Enqueue(vis_rgb);
			OutputAI_4.Enqueue(OK_NG_Flag);

			//========================================================
			this.Invoke((EventHandler)delegate
			{
				Mat DST = ImgAI_4.Dequeue();
				app.SavingMode = OK_NG_Flag.ToString();
				//Thread.Sleep(50);
				BeginInvoke(new Action(() => { cherngerPictureBox4.Image = DST.ToBitmap(); }));

				#region TREX存圖
				My_Save_Image(Src, DST, 1, app.SavingMode,stop: 4);
				#endregion
				#region 輸出結果


				lock (OutputAI_4)
				{
					TestCount_4++;
					string Result = UpdateResult(OutputAI_4.Dequeue());
					Value.Result_4.Enqueue(Result);
					BeginInvoke(new UpdateLabelTextDelegate(UpdateLabelText), Result_CCD_4, Result);
					BeginInvoke(new UpdateLabelBackColorDelegate(UpdateLabelBackColor), Result_CCD_4, Result);
					BeginInvoke(new UpdateLabelTextDelegate(UpdateLabelText), label_test_4, TestCount_4.ToString());//label_test_4

					UpdateLabelDivision(Result, 3);
					//Work_5_AI();
				}
				#endregion

			});



		}
		#endregion

		#endregion
		
		#region unused
		private void radioButton4_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void radioButton5_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void groupBox26_Enter(object sender, EventArgs e)
		{

		}

		private void panel2_CloseClick(object sender, EventArgs e)
		{

		}

		private void cherngerPictureBox1_Click(object sender, EventArgs e)
		{

		}
		#endregion
	}

	#region 統計參數
	public class Num
	{
		public static double YieldRate = 0.00;
		public static double OKdRate = 0.00;
		public static double NGRate = 0.00;
		public static double NULLRate = 0.00;
		public static int TotalNum = 0;
		public static int TotalNum_Temp = 0;

		public static int TotalSuccessNum = 0;
		public static int TotalSuccessChartNum = 0;
		public static int TotalOKChartNum = 0 ;
		public static int OKNum = 0;
		public static int NGNum = 0;
		public static int NULLNum = 0;

		public static int NG_OID = 0;
		public static int NG_Crack = 0;
		public static int NG_Gap = 0;
		public static int NG_CatchUp = 0;

		public static bool Bool_OID = false;
		public static bool Bool_Crack = false;
		public static bool Bool_Gap = false;
		public static bool Bool_CatchUp = false;

		public static int TotalNumSave_1 = 0;
		public static int TotalNumSave_2 = 0;
		public static int TotalNumSave_3 = 0;
		public static int TotalNumSave_4 = 0;

		public static int TotalNumSave_1_tmp = 0;
		public static int TotalNumSave_2_tmp = 0;
		public static int TotalNumSave_3_tmp = 0;
		public static int TotalNumSave_4_tmp = 0;

		public static int[] NgDivision = new int[] { 0, 0, 0, 0 };
		public static int[] OkDivision = new int[] { 0, 0, 0, 0 };
		public static double[] YieldRateDivision = new double[] { 0.00, 0.00, 0.00, 0.00 };


		public static List<double> ChartRate = new List<double>();
		public static int TimeInterval = 0;
		public static int ChartCount = 0;
		public static int ChartTime = 0;

		public static void DefectReset()
		{
			NG_Crack = 0;
			NG_Gap = 0;
			NG_CatchUp = 0;
			NG_OID = 0;

		}
		public static void Reset()
		{
			TotalNum = 0;
			TotalSuccessNum = 0;
			OKNum = 0;
			NGNum = 0;
			NULLNum = 0;

			YieldRate = 0.00;
			OKdRate = 0.00;
			NGRate = 0.00;
			NULLRate = 0.00;
			ChartRate.Clear();

			NgDivision = new int[] { 0, 0, 0, 0 };
			OkDivision = new int[] { 0, 0, 0, 0 };
			YieldRateDivision = new double[] { 0.00, 0.00, 0.00, 0.00 };
		}
	}
	#endregion

	#region 系統參數
	public class app //global variable在這裡宣告
	{
		//===================DefectImageProcessingConfig===================================
		public static string Image_ProcssingDefect_Config_Initial = System.IO.Directory.GetCurrentDirectory() + @"\config_Initial\Image_ProcssingDefect_Config.ini";//原廠設定
		public static string Image_ProcssingDefect_Config = System.IO.Directory.GetCurrentDirectory() + @"\config\Image_ProcssingDefect_Config.ini";
		//=================================================================================

		public const byte MaxCameraCount = 4;                           // 最大攝影機數量
		public static bool Run = false;                                 // app運作狀態
		public static int TakePic = 0;                                  // 第一張照片拍照狀態
		public static int[] PlcCounter = new int[2];                    // 第一張照片拍照狀態
		public static bool SoftTriggerMode = false;
		public static byte Mode = 1;                                    // Mode 0:攝影模式 1:硬體觸發 2:軟體觸發
		public static bool Offline = false;                             // 離線測試
		public static bool OfflineShoot = false;                        // 離線取樣
		public static bool IsAdjust = true;//false;                            // 調機模式
		public static int AdjustCounter = 0;                            // 校正數量
		public static int OfflineShootID = 5;                           // 離線畫面對應
		public static bool[] CameraLinked = new bool[MaxCameraCount];
		public static int[] CameraCount = new int[4] { 0, 0, 0, 0 };
		public static int DelateImgDay = new int();                     //照片儲存天數
		public static int[] DelateShoot = new int[4];                   //延遲拍照時間
		public static int[] BlowDelay = new int[2];                     //延遲拍照時間
		public static double Picpercent = 0.3;                          //縮圖比例
		public static int NullWarning = 0;                              //NULL 警報 Counter
		public static int NullWarningTarget = 100;                      //NULL 警報 標準
		public static bool[] CCD_Apply = new bool[4] { true, true, true, true };        //各站啟動狀態
		public static DateTime NoneItemWarning;
		public static bool NoneItemBuzzer = false;
		public static int NoneItemTime = 30;
		public static int OfflineStopCounter = 1;                       //離線測試轉盤站號
		public static int GlassesClean = 0;                             //轉盤玻璃潔淨度
		public static int ProductEfficient = 30;                        //趨勢圖更新產量計數
		public static int IsUpdateChart = 0;                            //趨勢圖更新產量判斷
		public static string SavingMode = "OK";                         //趨勢圖更新產量判斷	

		public static int image_number = 1;                             //AI thread 讀圖數量
		public static object LockStop1 = new object();
		public static object LockStop2 = new object();
		public static object LockGreenRedLight = new object();


		public static string SaveImgpath = System.IO.Directory.GetCurrentDirectory() + @"\Image\";
		public static string SaveTmpImgpath = System.IO.Directory.GetCurrentDirectory() + @"\TempImg\";
		public static string SaveHistoryImgpath = System.IO.Directory.GetCurrentDirectory() + @"\Image\";
		public static string Detect_SaveHistoryImgpath = System.IO.Directory.GetCurrentDirectory() + @"\Image_Detected\";
		public static string SavePaperpath = System.IO.Directory.GetCurrentDirectory() + @"\Report\";
		//public static string ColorSettingpath = System.IO.Directory.GetCurrentDirectory() + @"\config\Color.ini";
		public static string UISettingpath = System.IO.Directory.GetCurrentDirectory() + @"\config\UISetting.ini";
		public static string DefectSettingpath = System.IO.Directory.GetCurrentDirectory() + @"\config\DefectSetting.ini";

		public static string UISettingpath_Initial = System.IO.Directory.GetCurrentDirectory() + @"\config_Initial\UISetting.ini";
		public static string DefectSettingpath_Initial = System.IO.Directory.GetCurrentDirectory() + @"\config_Initial\DefectSetting.ini";
		public static string SaveProductMeasurepath = System.IO.Directory.GetCurrentDirectory() + @"\Product\";
		public static string SavePathState = "";
		public static string OfflineImagePath = @"C:\Users\Chernger\Desktop\";
		public static string[] OfflineImage = Directory.GetFiles(OfflineImagePath);
		public static int OfflineCount = 0;

		public static IEnumerator enumerator_1;
		public static IEnumerator enumerator_2;
		public static IEnumerator enumerator_3;
		public static IEnumerator enumerator_4;


		#region PLC COM點
		public static string Comport1 = string.Empty;
		#endregion

		#region 顏色
		public static int[,] ColorValue = new int[,] {
		{   0,  50,  30, 100,  60, 150 },
		{ 150, 180,  30, 100,  60, 150 },
		{  80, 120,   0,  60, 110, 220 },
		{  80, 120,   0,  60, 110, 220 },
		{   0,  50,  30, 100,  60, 150 },
		{ 150, 180,  30, 100,  60, 150 },
		{   0,  50,  30, 100,  60, 150 },
		{ 150, 180,  30, 100,  60, 150 },
		{  80, 120,   0,  60, 110, 220 },
		{  80, 120,   0,  60, 110, 220 },
		{  80, 120,   0,  60, 110, 220 },
		{  80, 120,   0,  60, 110, 220 },
		{   0,  50,  30, 100,  60, 150 },
		{ 150, 180,  30, 100,  60, 150 },
		{   0,  50,  30, 100,  60, 150 },
		{ 150, 180,  30, 100,  60, 150 }};
		#endregion

		#region 計時
		public static Stopwatch RunningSW = new Stopwatch();
		public static Stopwatch TotalSW = new Stopwatch();
		public static Stopwatch SingleRunningSW = new Stopwatch();

		public static Stopwatch SingleItemTime1 = new Stopwatch();
		public static Stopwatch SingleItemTime2 = new Stopwatch();
		public static Stopwatch SingleItemTime3 = new Stopwatch();
		public static Stopwatch SingleItemTime4 = new Stopwatch();

		public static Stopwatch UpdateChartSW = new Stopwatch();
		#endregion

		#region 暫存照片按鈕
		public static int Column = 10;
		public static int Row = 10;
		public static int RowHeight = 37;
		public static int ColumeWidth = 26;
		#endregion
	}
	#endregion
	public class ImageProcessingDefect_Value
	{
		//Stop1
		public static int stop1_inner_circle_radius = 0;
		public static int stop1_out_defect_size_min = 0;
		public static int stop1_out_defect_size_max = 20000;
		public static int stop1_inner_defect_size_min = 500;
		public static int stop1_arclength_area_ratio = 10;

		//Stop2
		public static int stop2_inner_circle_radius = 0;
		public static int stop2_out_defect_size_min = 0;
		public static int stop2_out_defect_size_max = 20000;
		public static int stop2_inner_defect_size_min = 500;
		public static int stop2_arclength_area_ratio = 10;
		//Stop3
		public static int stop3_threshold_1phase = 130;
		public static int stop3_threshold_2phase_1 = 38;
		public static int stop3_threshold_2phase_2 = 22;

		//Stop4
		public static int stop4_black_defect_area_min = 220;
		public static int stop4_black_defect_area_max = 20000;
		public static double stop4_arclength_area_ratio = 0.35;
		public static int stop4_ignore_radius = 5;

	}
	#region 檢測參數
	public class Value
	{
		public static int[] NG_Type = new int[5];                                           //統計各瑕疵數量
		public static string[] NG_TypeName = { "OD", "ID", "Crack", "Gap", "CatchUp" };     //瑕疵種類名稱

		public static Queue<string> Result_1 = new Queue<string>();                         //第一站結果序列
		public static Queue<string> Result_2 = new Queue<string>();                         //第二站結果序列
		public static Queue<string> Result_3 = new Queue<string>();                         //第三站結果序列
		public static Queue<string> Result_4 = new Queue<string>();                         //第四站結果序列

		public static Queue<List<string>> NgType_1 = new Queue<List<string>>();
		public static Queue<List<string>> NgType_2 = new Queue<List<string>>();
		public static Queue<List<string>> NgType_3 = new Queue<List<string>>();
		public static Queue<List<string>> NgType_4 = new Queue<List<string>>();

		public static double GapArea = 0;                       //缺角面積
		public static double GapDeep = 0;                       //缺角深度
		public static double CrackArea = 0;                     //微裂面積
		public static double CrackLength = 0;                   //微裂長度
		public static double CatchUpArea = 0;                   //髒污面積
		public static double CatchUpLength = 0;                 //髒污長度

		public static double StandardOD = 0;                    //外徑標準值
		public static double LowerBoundOD = 0;                  //外徑下界 
		public static double UpperBoundOD = 0;                  //外徑上界

		public static double StandardID = 0;                    //內徑標準值
		public static double LowerBoundID = 0;                  //內徑下界
		public static double UpperBoundID = 0;                  //內徑上界

		public static double ODRatio = 0.017267961763799;       //外徑轉換比
		public static double IDRatio = 0.017267961763799;       //內徑轉換比

		public static bool GapApply = true;                     //缺角套用檢測
		public static bool CrackApply = true;                   //微裂套用檢測
		public static bool CatchApply = true;                   //髒污套用檢測
		public static bool ODApply = true;                      //外徑套用檢測
		public static bool IDApply = true;                      //內徑套用檢測

		public static double AdjODRatio = 0;
		public static double AdjIDRatio = 0;
	}
	#endregion

	#region 控制參數
	public static class PLC1
	{
		#region 列舉
		public enum PLCContect
		{
			/// <summary>
			/// 轉盤
			/// </summary>
			Motor = 0,
			Vibrat_Plate = 1,
			NG_Blow = 2,
			OK_Blow = 3,
			Red_light = 4,
			Yellow_light = 5,
			Green_light = 6,
			Buzzer = 7,
			Light = 10,
			OfflineMotor = 12,
		}
		#endregion
		private static Form1 form1;
		public static void plcRun()
		{
			ThreadPool.QueueUserWorkItem((o) =>
			{
				SerialPort sp = plcconnector.getSerialPort();

				string Buffer = "";
				while (app.Run)
				{
					try
					{
						//var T = Thread.CurrentThread;
						//T.Priority = ThreadPriority.Normal;
						Buffer = sp.ReadExisting();
						List<int> Packet = new List<int>();
						foreach (char item in Buffer)
							Packet.Add(Convert.ToByte(item));

						if (Packet.Count == 1)
						{
							form1.PLCReceiver(Packet);
							//Console.WriteLine("112233");
						}
						//Console.WriteLine("Buffer:" + Buffer);
						Thread.Sleep(5);
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.ToString());
					}
				}
			});
		}

		#region 初始化
		public static void PLC_On(Form1 form)
		{
			form1 = form;
			//plcconnector = new PLCConnect(app.Comport1, 115200, Parity.None, 8, StopBits.One, PLCReciverInner);
			plcconnector = new PLCConnect(app.Comport1, 115200, Parity.None, 8, StopBits.One, null);
			plcconnector.getSerialPort().Encoding = System.Text.Encoding.GetEncoding(28591);
			lastsendtime = DateTime.Now;
			Sensor = new byte[PLC_BYTE_LENGTH] { 0x00, 0x00 };
			onPLCSensorChange += new Action<byte[]>((value) =>
			{
				int period = PLC_TIME_LIMIT - (int)(DateTime.Now - lastsendtime).TotalMilliseconds;
				if (period > 0)
					Thread.Sleep(5);
				//var t = new Thread(() => { Console.WriteLine("in worker thread"); });
				//t.Priority = ThreadPriority.Highest;
				//var T = Thread.CurrentThread;
				//T.Priority = ThreadPriority.Normal;
				var _sp = plcconnector.getSerialPort();
				_sp.Write(value, 0, value.Length);
				foreach (var b in value)
				{
					//Console.Write("{0:X2} ", b);
				}
				//Console.WriteLine("{0}", "");
				lastsendtime = DateTime.Now;
			});


		}
		#endregion

		#region 宣告
		public const int PLC_BYTE_LENGTH = 2;
		public const int PLC_TIME_LIMIT = 50;
		public static PLCDataReceived PLCReciverInner;
		private static DateTime lastsendtime;
		private static Action<byte[]> onPLCSensorChange;
		private static PLCConnect plcconnector;
		private static byte[] sensor;
		private static byte[] Sensor
		{
			get => sensor;
			set
			{
				if (value != sensor)
					onPLCSensorChange?.Invoke(value);
				sensor = value;
			}
		}
		#endregion

		#region 方法
		public static void reset()
		{
			//Sensor = new byte[] { 0x00 };
			Sensor = new byte[PLC_BYTE_LENGTH] { 0x00, 0x00 };
		}
		public static void setValue(PLCContect contect, bool status)
		{

			if (int.TryParse(String.Format("{0:00}", (int)contect).Substring(0, 1), out int d_index)
				&& int.TryParse(String.Format("{0:00}", (int)contect).Substring(1, 1), out int index))
			{
				//d_index = index / 8;
				//index = index % 8;
				//  Console.WriteLine(String.Format("{0:00}", (int)contect));
				//   Console.WriteLine("d_index:" + d_index + " index: " + index);
				byte[] sensor = Sensor.ToArray();
				if (!getBitStatus(Sensor[d_index], (int)index) && status)
					sensor[d_index] += Convert.ToByte(Math.Pow(2, index));
				if (getBitStatus(Sensor[d_index], (int)index) && !status)
					sensor[d_index] -= Convert.ToByte(Math.Pow(2, index));

				Sensor = sensor.ToArray();
				//foreach (var a in sensor)
				//	Console.WriteLine(a);

			}
			else
				throw new System.Exception();
		}

		public static bool getValue(PLCContect contect, out bool status)
		{
			status = false;
			if (int.TryParse(String.Format("{0:00}", (int)contect).Substring(0, 1), out int d_index)
				&& int.TryParse(String.Format("{0:00}", (int)contect).Substring(1, 1), out int index))
			{
				//d_index = index / 8;
				//index = index % 8;
				//	Console.WriteLine(String.Format("{0:00}", (int)contect));
				//	Console.WriteLine("d_index:" + d_index + " index: " + index);
				byte[] sensor = Sensor.ToArray();
				if (!getBitStatus(Sensor[d_index], (int)index))
					status = false;
				if (getBitStatus(Sensor[d_index], (int)index))
					status = true;
				//Sensor = sensor.ToArray();
				Console.WriteLine("{0}", Sensor);
			}
			else
				throw new System.Exception();
			return status;
		}

		public static void Close()
		{
			plcconnector.Close();
			onPLCSensorChange = null;
		}

		private static bool getBitStatus(byte sensor, int index)
		{
			return Convert.ToString(sensor, 2).PadLeft(8, '0').Reverse().ToArray()[index] == '1';
		}
		#endregion
	}
	#endregion
}