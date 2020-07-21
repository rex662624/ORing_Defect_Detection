using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CherngerUI
{
    public partial class ButtoPicture : Form
    {
        public ButtoPicture()
        {
            InitializeComponent();
        }

        string Num = string.Empty;
        string Result = string.Empty;
        string File = string.Empty;

        public void Data(string N, string R, string F)
        {
            Num = N;
            Result = R;
            File = F;
        }

        private void ButtoPicture_Load(object sender, EventArgs e)
        {
            label1.Text = Num;
            label2.Text = Result;
			switch (Result)
			{
				case "OK":
					label2.ForeColor = Color.Green;
					break;
				case "NG":
					label2.ForeColor = Color.Red;
					break;
				default:
					label2.ForeColor = Color.Yellow;
					break;
			}

            string a = app.SaveTmpImgpath + "CCD-1\\" + Num.ToString() + ".jpg";
			
			if (System.IO.File.Exists(app.SaveTmpImgpath + "CCD-1\\" + Num.ToString() + ".jpg"))
				cherngerPictureBox2.Image = new Mat(app.SaveTmpImgpath + "CCD-1\\" + Num.ToString() + ".jpg" , ImreadModes.Color).ToBitmap();
			if (System.IO.File.Exists(app.SaveTmpImgpath + "CCD-2\\" + Num.ToString() + ".jpg"))
				cherngerPictureBox1.Image = new Mat(app.SaveTmpImgpath + "CCD-2\\" + Num.ToString() + ".jpg", ImreadModes.Color).ToBitmap();
			if (System.IO.File.Exists(app.SaveTmpImgpath + "CCD-3\\" + Num.ToString() + ".jpg"))
				cherngerPictureBox4.Image = new Mat(app.SaveTmpImgpath + "CCD-3\\" + Num.ToString() + ".jpg", ImreadModes.Color).ToBitmap();
			if (System.IO.File.Exists(app.SaveTmpImgpath + "CCD-4\\" + Num.ToString() + ".jpg"))
				cherngerPictureBox3.Image = new Mat(app.SaveTmpImgpath + "CCD-4\\" + Num.ToString() + ".jpg", ImreadModes.Color).ToBitmap();
		}
    }
}
