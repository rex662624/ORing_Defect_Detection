using System;
using System.Windows.Forms;
using SensorTechnology;
using OpenCvSharp;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CherngerUI;
using System.Threading;

namespace SensorTechnology
{
	public class Camera
	{
		private static System.IntPtr[] ahCameras = new System.IntPtr[app.MaxCameraCount];
		private static uint[] aCameraID = new uint[app.MaxCameraCount];
		private static string[] astrCameraName = new string[app.MaxCameraCount];
		private static StCamera[] m_Camera = new StCamera[app.MaxCameraCount];
		private static System.IntPtr[] selectCameraHandle = new System.IntPtr[app.MaxCameraCount];
		private static int[] selectDevHandle = new int[app.MaxCameraCount];

		private static int CameraCount = 0;

		private static byte SelectCameraCompany = 0; // 0:StCamera 1:DahuaTech

		private static Form1 form1;

		public bool Open(Form1 form)
		{
			form1 = form;

			CameraCount = StCamera.OpenCameraList(app.MaxCameraCount, ahCameras, aCameraID, astrCameraName);
			if (CameraCount == 0)
			{
				//CameraCount = Enumerator.EnumerateDevices().Count;
				if (CameraCount == 0)
					return false;
				else
					SelectCameraCompany = 1;

				for (int i = 0; i < app.MaxCameraCount; i++)
					selectDevHandle[i] = -1;
			}

			AddrLink();
			return true;
		}

		public void ShowSettingDlg(int CameraID)
		{
			m_Camera[CameraID].ShowSettingDlg();
		}

		public void Set_TriggrtSoure(int CameraID,int i)
		{
			m_Camera[CameraID].TriggerSource = (uint)i;
			m_Camera[CameraID].IOPinPolarity[0] = 1;
		}

		public bool CheckCamera(int CameraID)
		{
			if (m_Camera[CameraID] != null)
				return true;
			else
				return false;
		}

		public void Setting(byte Mode) //Mode 0:Live 1:Hard 2:Soft
		{
			for (byte i = 0; i < app.MaxCameraCount; i++)
			{
				if (SelectCameraCompany == 0 && selectCameraHandle[i] != (IntPtr)0)
				{
					m_Camera[i] = new StCamera(selectCameraHandle[i]);
					m_Camera[i].StopTransfer();

					if (Mode == 0)
						m_Camera[i].TriggerMode = StTrg.STCAM_TRIGGER_MODE_OFF;
					else if (Mode == 1)
					{
						m_Camera[i].TriggerMode = StTrg.STCAM_TRIGGER_MODE_ON;
						m_Camera[i].TriggerSource = StTrg.STCAM_TRIGGER_SOURCE_HARDWARE;
						m_Camera[i].IOPinMode[0] = 16;
						m_Camera[i].IOPinPolarity[0] = 0;
					}
					else if (Mode == 2)
					{
						m_Camera[i].TriggerMode = StTrg.STCAM_TRIGGER_MODE_ON;
						m_Camera[i].TriggerSource = StTrg.STCAM_TRIGGER_SOURCE_SOFTWARE;
					}
					//m_Camera[i].ExposureClock = (uint)CherngerUI.Properties.Settings.Default.ExposureClock;
					//m_Camera[i].Gain = (ushort)CherngerUI.Properties.Settings.Default.Gain;
					m_Camera[i].SetDisplayImageCallback(new StCamera.DisplayImageCallback(Receiver));

					m_Camera[i].StopTransfer();
				}
			}
		}

		public void SetWhiteBalance(int Mode, int i, ushort GainR, ushort GainGr, ushort GainGb, ushort GainB)
		{
			m_Camera[i].WhiteBalanceMode = (byte)Mode;
			if (Mode == 1)
			{
				m_Camera[i].WBBGain = GainB;
				m_Camera[i].WBGbGain = GainGb;
				m_Camera[i].WBGrGain = GainGr;
				m_Camera[i].WBRGain = GainR;
			}

		}

		public void GetWhite()
		{
			Console.WriteLine(m_Camera[0].WhiteBalanceMode);
		}

		public void ExposureClockChange(int ID, int ExposureClock)
		{
			m_Camera[ID].ExposureClock = (ushort)ExposureClock;
			//if (i == 0)
			//    Ini.WriteInt("Type"+type.ToString(), Ini.Key.ExposureClock_1, ExposureClock);
			//else
			//    Ini.WriteInt("Type"+type.ToString(), Ini.Key.ExposureClock_2, ExposureClock);

		}

		public void MirrorSet(int type, int i, int Mode)
		{
			m_Camera[i].MirrorMode = 2;
		}

		public void GainChange(int ID, int Gain)
		{

			m_Camera[ID].Gain = (ushort)Gain;
			//if (i == 0)
			//    Ini.WriteInt("Type" + type.ToString(), Ini.Key.Gain_1, Gain);
			//else
			//    Ini.WriteInt("Type" + type.ToString(), Ini.Key.Gain_2, Gain);

		}

		public void Start()
		{
			for (byte i = 0; i < app.MaxCameraCount; i++)
			{
				if (SelectCameraCompany == 0 && m_Camera[i] != null)
					m_Camera[i].StartTransfer();
			}
		}

		public void Shoot1()
		{
			if (SelectCameraCompany == 0 && m_Camera[0] != null)
				m_Camera[0].TriggerSoftware(StTrg.STCAM_TRIGGER_SELECTOR_FRAME_START);
		}

		public void Shoot2()
		{
			if (SelectCameraCompany == 0 && m_Camera[1] != null)
				m_Camera[1].TriggerSoftware(StTrg.STCAM_TRIGGER_SELECTOR_FRAME_START);
		}

		public void Shoot3()
		{
			if (SelectCameraCompany == 0 && m_Camera[2] != null)
				m_Camera[2].TriggerSoftware(StTrg.STCAM_TRIGGER_SELECTOR_FRAME_START);
		}

		public void Shoot4()
		{
			if (SelectCameraCompany == 0 && m_Camera[3] != null)
				m_Camera[3].TriggerSoftware(StTrg.STCAM_TRIGGER_SELECTOR_FRAME_START);
		}

		public void Shoot(int CameraID)
		{
			if (SelectCameraCompany == 0 && m_Camera[CameraID] != null)
				m_Camera[CameraID].TriggerSoftware(StTrg.STCAM_TRIGGER_SELECTOR_FRAME_START);
		}


		public void Stop()
		{
			//app.Run = false;
			for (byte i = 0; i < app.MaxCameraCount; i++)
			{
				if (SelectCameraCompany == 0 && m_Camera[i] != null)
					m_Camera[i].StopTransfer();
			}
		}

		public void Close()
		{
			for (byte i = 0; i < app.MaxCameraCount; i++)
			{
				if (SelectCameraCompany == 0 && m_Camera[i] != null)
				{
					m_Camera[i].StopTransfer();
					m_Camera[i].Dispose();
					m_Camera[i] = null;
				}
			}
		}

		public string AddrLink()
		{
			string Name = "";
			//Console.WriteLine("[CameraCount]  " + CameraCount);
			for (byte i = 0; i < CameraCount; i++)
			{
				try
				{
					int CameraID = -1;
					if (SelectCameraCompany == 0)
					{
						CameraID = int.Parse(astrCameraName[i].Substring(astrCameraName[i].Length - 2)) - 1;
						//Console.WriteLine("[CameraID]  " + CameraID);
						selectCameraHandle[CameraID] = ahCameras[i];
					}
					app.CameraLinked[CameraID] = true;
				}
				catch
				{
					if (SelectCameraCompany == 0)
						Name += astrCameraName[i] + " ";
				}
			}
			return Name;
		}

		private void Receiver(string CameraUserName, uint dwFrameNo, uint Width, uint Height, uint StPixelFormat, byte[] BGRImage)
		{
			if (app.Run)
			{
				DateTime time_start = DateTime.Now;//計時開始 取得目前時間
				Mat Src = new Mat();
				//Console.WriteLine("[ID]: " + CameraUserName);
				try
				{
					int CameraID = int.Parse(CameraUserName.Substring(CameraUserName.Length - 2)) - 1;
					//Console.WriteLine("[CameraID:] "+ CameraID);
					Src = new Mat(new OpenCvSharp.Size((int)Width, (int)Height), MatType.CV_8UC1);
					Marshal.Copy(BGRImage, 0, Src.Data, Src.Width * Src.Height * 1);
					//using (new CvWindow(WindowMode.FreeRatio, Src)) Cv.WaitKey();
					form1.Receiver(CameraID, Src);
				}
				catch
				{

				}
				#region 計算耗時
				//DateTime time_end = DateTime.Now;//計時結束 取得目前時間
				//string time_consuming = ((TimeSpan)(time_end - time_start)).TotalMilliseconds.ToString("0");

				//form1.UpdateTime(CameraID, time_consuming);
				#endregion

				#region 清理資源
				dwFrameNo = 0;
				Width = 0;
				Height = 0;
				StPixelFormat = 0;
				CameraUserName = null;
				BGRImage = null;
				//time_consuming = null;
				Src = null;
				GC.Collect();
				#endregion
			}


		}

		private void Receiver2(string CameraUserName, uint dwFrameNo, uint Width, uint Height, uint StPixelFormat, byte[] BGRImage)
		{
			if (app.Run)
			{
				DateTime time_start = DateTime.Now;//計時開始 取得目前時間
				Mat Src = new Mat();

				int CameraID = int.Parse(CameraUserName.Substring(CameraUserName.Length - 2)) - 1;
				//Console.WriteLine("[CameraID:] "+ CameraID);


				Src = new Mat(new OpenCvSharp.Size((int)Width , (int)Height) , MatType.CV_8UC3);
				Marshal.Copy(BGRImage, 0, Src.Data, Src.Width * Src.Height * 3);
				//using (new CvWindow(WindowMode.FreeRatio, Src)) Cv.WaitKey();
				form1.Receiver(CameraID, Src);



				#region 計算耗時
				DateTime time_end = DateTime.Now;//計時結束 取得目前時間
				string time_consuming = ((TimeSpan)(time_end - time_start)).TotalMilliseconds.ToString("0");

				//form1.UpdateTime(CameraID, time_consuming);
				#endregion

				#region 清理資源
				dwFrameNo = 0;
				Width = 0;
				Height = 0;
				StPixelFormat = 0;
				CameraID = 0;
				CameraUserName = null;
				BGRImage = null;
				time_consuming = null;
				Src = null;
				GC.Collect();
				#endregion
			}


		}



	}
}
