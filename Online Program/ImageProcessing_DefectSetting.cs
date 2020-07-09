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
			//Stop1
			Stop1_max_outer_defect.Text = CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_max.ToString();
			Stop1_min_outer_defect.Text = CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_min.ToString();
			Stop1_ignore_inner_circle.Text = CherngerUI.ImageProcessingDefect_Value.stop1_inner_circle_radius.ToString();
			Stop1_min_inner_defect_size.Text = CherngerUI.ImageProcessingDefect_Value.stop1_inner_defect_size_min.ToString();
			Stop1_arclength_area_ratio.Text = CherngerUI.ImageProcessingDefect_Value.stop1_arclength_area_ratio.ToString();


			//Stop2
			Stop2_max_outer_defect.Text = CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_max.ToString();
			Stop2_min_outer_defect.Text = CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_min.ToString();
			Stop2_ignore_inner_circle.Text = CherngerUI.ImageProcessingDefect_Value.stop2_inner_circle_radius.ToString();
			Stop2_min_inner_defect_size.Text = CherngerUI.ImageProcessingDefect_Value.stop2_inner_defect_size_min.ToString();
			Stop2_arclength_area_ratio.Text = CherngerUI.ImageProcessingDefect_Value.stop2_arclength_area_ratio.ToString();

			//Stop4 
			Stop4_max_area.Text = CherngerUI.ImageProcessingDefect_Value.stop4_black_defect_area_max.ToString();
			Stop4_min_area.Text = CherngerUI.ImageProcessingDefect_Value.stop4_black_defect_area_min.ToString();
			Stop4_arclength_area_ratio.Text = CherngerUI.ImageProcessingDefect_Value.stop4_arclength_area_ratio.ToString();
			stop4_ignore_radius.Text = CherngerUI.ImageProcessingDefect_Value.stop4_ignore_radius.ToString();
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
				int.TryParse(Stop4_min_area.Text, out CherngerUI.ImageProcessingDefect_Value.stop4_black_defect_area_min);
			}

			if (Stop4_max_area.Text != string.Empty)
			{
				int.TryParse(Stop4_max_area.Text, out CherngerUI.ImageProcessingDefect_Value.stop4_black_defect_area_max);
			}

			if (Stop4_arclength_area_ratio.Text != string.Empty)
			{
				int.TryParse(Stop4_arclength_area_ratio.Text, out CherngerUI.ImageProcessingDefect_Value.stop4_arclength_area_ratio);
			}

			if (stop4_ignore_radius.Text != string.Empty)
			{
				int.TryParse(stop4_ignore_radius.Text, out CherngerUI.ImageProcessingDefect_Value.stop4_ignore_radius);
			}
		}

		private void save_4_stop_config_Click(object sender, EventArgs e)
		{

			SetupIniIP.IniWriteValue("Stop1", "outer_defect_size_max", Stop1_max_outer_defect.Text, CherngerUI.app.Image_ProcssingDefect_Config);
			SetupIniIP.IniWriteValue("Stop1", "outer_defect_size_min", Stop1_min_outer_defect.Text, CherngerUI.app.Image_ProcssingDefect_Config);
			SetupIniIP.IniWriteValue("Stop1", "inner_circle_radius", Stop1_ignore_inner_circle.Text, CherngerUI.app.Image_ProcssingDefect_Config);
			SetupIniIP.IniWriteValue("Stop1", "inner_defect_size_min", Stop1_min_inner_defect_size.Text, CherngerUI.app.Image_ProcssingDefect_Config);
			SetupIniIP.IniWriteValue("Stop1", "arclength_area_ratio", Stop1_arclength_area_ratio.Text, CherngerUI.app.Image_ProcssingDefect_Config);

			SetupIniIP.IniWriteValue("Stop2", "outer_defect_size_max", Stop2_max_outer_defect.Text, CherngerUI.app.Image_ProcssingDefect_Config);
			SetupIniIP.IniWriteValue("Stop2", "outer_defect_size_min", Stop2_min_outer_defect.Text, CherngerUI.app.Image_ProcssingDefect_Config);
			SetupIniIP.IniWriteValue("Stop2", "inner_circle_radius", Stop2_ignore_inner_circle.Text, CherngerUI.app.Image_ProcssingDefect_Config);
			SetupIniIP.IniWriteValue("Stop2", "inner_defect_size_min", Stop2_min_inner_defect_size.Text, CherngerUI.app.Image_ProcssingDefect_Config);
			SetupIniIP.IniWriteValue("Stop2", "arclength_area_ratio", Stop2_arclength_area_ratio.Text, CherngerUI.app.Image_ProcssingDefect_Config);


			SetupIniIP.IniWriteValue("Stop4", "black_defect_area_min", Stop4_min_area.Text, CherngerUI.app.Image_ProcssingDefect_Config);
			SetupIniIP.IniWriteValue("Stop4", "black_defect_area_max", Stop4_max_area.Text, CherngerUI.app.Image_ProcssingDefect_Config);
			SetupIniIP.IniWriteValue("Stop4", "arclength_area_ratio", Stop4_arclength_area_ratio.Text, CherngerUI.app.Image_ProcssingDefect_Config);
			SetupIniIP.IniWriteValue("Stop4", "ignore_radius", stop4_ignore_radius.Text, CherngerUI.app.Image_ProcssingDefect_Config);
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void textBox3_TextChanged(object sender, EventArgs e)
		{

		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (Stop2_max_outer_defect.Text != string.Empty)
			{
				int.TryParse(Stop2_max_outer_defect.Text, out CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_max);
			}

			if (Stop2_min_outer_defect.Text != string.Empty)
			{
				int.TryParse(Stop2_min_outer_defect.Text, out CherngerUI.ImageProcessingDefect_Value.stop2_out_defect_size_min);
			}

			if (Stop2_ignore_inner_circle.Text != string.Empty)
			{
				int.TryParse(Stop2_ignore_inner_circle.Text, out CherngerUI.ImageProcessingDefect_Value.stop2_inner_circle_radius);
			}

			if (Stop2_min_inner_defect_size.Text != string.Empty)
			{
				int.TryParse(Stop2_min_inner_defect_size.Text, out CherngerUI.ImageProcessingDefect_Value.stop2_inner_defect_size_min);
			}

			if (Stop2_arclength_area_ratio.Text != string.Empty)
			{
				int.TryParse(Stop2_arclength_area_ratio.Text, out CherngerUI.ImageProcessingDefect_Value.stop2_arclength_area_ratio);
			}
		}

		private void Stop1_checkbox_CheckedChanged(object sender, EventArgs e)
		{
			if (Stop1_max_outer_defect.Text != string.Empty)
			{
				int.TryParse(Stop1_max_outer_defect.Text, out CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_max);
			}

			if (Stop1_min_outer_defect.Text != string.Empty)
			{
				int.TryParse(Stop1_min_outer_defect.Text, out CherngerUI.ImageProcessingDefect_Value.stop1_out_defect_size_min);
			}

			if (Stop1_ignore_inner_circle.Text != string.Empty)
			{
				int.TryParse(Stop1_ignore_inner_circle.Text, out CherngerUI.ImageProcessingDefect_Value.stop1_inner_circle_radius);
			}

			if (Stop1_min_inner_defect_size.Text != string.Empty)
			{
				int.TryParse(Stop1_min_inner_defect_size.Text, out CherngerUI.ImageProcessingDefect_Value.stop1_inner_defect_size_min);
			}

			if (Stop1_arclength_area_ratio.Text != string.Empty)
			{
				int.TryParse(Stop1_arclength_area_ratio.Text, out CherngerUI.ImageProcessingDefect_Value.stop1_arclength_area_ratio);
			}
		}
	}
}
