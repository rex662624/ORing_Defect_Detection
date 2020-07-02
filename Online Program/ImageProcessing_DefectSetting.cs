using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CherngerTools.Ini;

namespace UnionAoi
{
	public partial class ImageProcessing_DefectSetting : Form
	{
		public ImageProcessing_DefectSetting()
		{
			InitializeComponent();
		}

		private void ImageProcessing_DefectSetting_Load(object sender, EventArgs e)
		{
			Stop4_max_area.Text = CherngerUI.ImageProcessingDefect_Value.black_defect_area_max.ToString();
			Stop4_min_area.Text = CherngerUI.ImageProcessingDefect_Value.black_defect_area_min.ToString();
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			

		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			
		}

		private void ODApplyBtn_CheckedChanged(object sender, EventArgs e)
		{
			if (Stop4_min_area.Text != string.Empty)
			{
				int.TryParse(Stop4_min_area.Text, out CherngerUI.ImageProcessingDefect_Value.black_defect_area_min);
			}

			if (Stop4_max_area.Text != string.Empty)
			{
				int.TryParse(Stop4_max_area.Text, out CherngerUI.ImageProcessingDefect_Value.black_defect_area_max);
			}

		}

		private void save_4_stop_config_Click(object sender, EventArgs e)
		{
			SetupIniIP.IniWriteValue("Stop4", "black_defect_area_min", Stop4_min_area.Text, CherngerUI.app.Image_ProcssingDefect_Config);
			SetupIniIP.IniWriteValue("Stop4", "black_defect_area_max", Stop4_max_area.Text, CherngerUI.app.Image_ProcssingDefect_Config);
		}
	}
}
