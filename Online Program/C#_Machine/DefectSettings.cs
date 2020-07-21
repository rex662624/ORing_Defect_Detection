using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CherngerTools.Ini;

namespace UnionAoi
{
    public partial class DefectSettings : Form
    {
        public DefectSettings()
        {
            InitializeComponent();
        }

        private void DefectSettings_Load(object sender, EventArgs e)
        {
			GapAreaTextBox.Text = SetupIniIP.IniReadValue("Gap", "Area", CherngerUI.app.DefectSettingpath);
			GapDeepTextBox.Text = SetupIniIP.IniReadValue("Gap", "Deep", CherngerUI.app.DefectSettingpath);
			CrackAreaTextBox.Text = SetupIniIP.IniReadValue("Crack", "Area", CherngerUI.app.DefectSettingpath);
			CrackLengthTextBox.Text = SetupIniIP.IniReadValue("Crack", "Length", CherngerUI.app.DefectSettingpath);
			CatchUpAreaTextBox.Text = SetupIniIP.IniReadValue("CatchUp", "Area", CherngerUI.app.DefectSettingpath);
			CatchUpLengthTextBox.Text = SetupIniIP.IniReadValue("CatchUp", "Length", CherngerUI.app.DefectSettingpath);
			ODLowerBoundTextBox.Text = SetupIniIP.IniReadValue("OD", "LowerBound", CherngerUI.app.DefectSettingpath);
			ODStandardTextBox.Text = SetupIniIP.IniReadValue("OD", "Standard", CherngerUI.app.DefectSettingpath);
			ODUpperBoundTextBox.Text = SetupIniIP.IniReadValue("OD", "UpperBound", CherngerUI.app.DefectSettingpath);
			IDLowerBoundTextBox.Text = SetupIniIP.IniReadValue("ID", "LowerBound", CherngerUI.app.DefectSettingpath);
			IDStandardTextBox.Text = SetupIniIP.IniReadValue("ID", "Standard", CherngerUI.app.DefectSettingpath);
			IDUpperBoundTextBox.Text = SetupIniIP.IniReadValue("ID", "UpperBound", CherngerUI.app.DefectSettingpath);
			
			double.TryParse(GapAreaTextBox.Text, out CherngerUI.Value.GapArea);
			double.TryParse(GapDeepTextBox.Text, out CherngerUI.Value.GapDeep);
			
			double.TryParse(CrackAreaTextBox.Text, out CherngerUI.Value.CrackArea);
			double.TryParse(CrackLengthTextBox.Text, out CherngerUI.Value.CrackLength);
			
			double.TryParse(CatchUpAreaTextBox.Text, out CherngerUI.Value.CatchUpArea);
			double.TryParse(CatchUpLengthTextBox.Text, out CherngerUI.Value.CatchUpLength);
			
			double.TryParse(ODStandardTextBox.Text, out CherngerUI.Value.StandardOD);
			double.TryParse(ODLowerBoundTextBox.Text, out CherngerUI.Value.LowerBoundOD);
			double.TryParse(ODUpperBoundTextBox.Text, out CherngerUI.Value.UpperBoundOD);

			double.TryParse(IDStandardTextBox.Text, out CherngerUI.Value.StandardID);
			double.TryParse(IDLowerBoundTextBox.Text, out CherngerUI.Value.LowerBoundID);
			double.TryParse(IDUpperBoundTextBox.Text, out CherngerUI.Value.UpperBoundID);

			bool.TryParse(SetupIniIP.IniReadValue("Gap", "Inspect", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.GapApply);
			bool.TryParse(SetupIniIP.IniReadValue("Crack", "Inspect", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.CrackApply);
			bool.TryParse(SetupIniIP.IniReadValue("CatchUp", "Inspect", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.CatchApply);
			bool.TryParse(SetupIniIP.IniReadValue("OD", "Inspect", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.ODApply);
			bool.TryParse(SetupIniIP.IniReadValue("ID", "Inspect", CherngerUI.app.DefectSettingpath), out CherngerUI.Value.IDApply);

			GapApplyBtn.Checked = CherngerUI.Value.GapApply;
			CrackApplyBtn.Checked = CherngerUI.Value.CrackApply;
			CatchUpApplyBtn.Checked = CherngerUI.Value.CatchApply;
			ODApplyBtn.Checked = CherngerUI.Value.ODApply;
			IDApplyBtn.Checked = CherngerUI.Value.IDApply;
		}

		private void SaveBtn_Click(object sender, EventArgs e)
		{
			SetupIniIP.IniWriteValue("Gap", "Area", GapAreaTextBox.Text, CherngerUI.app.DefectSettingpath);
			SetupIniIP.IniWriteValue("Gap", "Deep", GapDeepTextBox.Text, CherngerUI.app.DefectSettingpath);
			double.TryParse(GapAreaTextBox.Text, out CherngerUI.Value.GapArea);
			double.TryParse(GapDeepTextBox.Text, out CherngerUI.Value.GapDeep);

			SetupIniIP.IniWriteValue("Crack", "Area", CrackAreaTextBox.Text, CherngerUI.app.DefectSettingpath);
			SetupIniIP.IniWriteValue("Crack", "Length", CrackLengthTextBox.Text, CherngerUI.app.DefectSettingpath);
			double.TryParse(CrackAreaTextBox.Text, out CherngerUI.Value.CrackArea);
			double.TryParse(CrackLengthTextBox.Text, out CherngerUI.Value.CrackLength);

			SetupIniIP.IniWriteValue("CatchUp", "Area", CatchUpAreaTextBox.Text, CherngerUI.app.DefectSettingpath);
			SetupIniIP.IniWriteValue("CatchUp", "Length", CatchUpLengthTextBox.Text, CherngerUI.app.DefectSettingpath);
			double.TryParse(CatchUpAreaTextBox.Text, out CherngerUI.Value.CatchUpArea);
			double.TryParse(CatchUpLengthTextBox.Text, out CherngerUI.Value.CatchUpLength);

			SetupIniIP.IniWriteValue("OD", "Standard", ODStandardTextBox.Text , CherngerUI.app.DefectSettingpath);
			SetupIniIP.IniWriteValue("OD", "LowerBound", ODLowerBoundTextBox.Text, CherngerUI.app.DefectSettingpath);
			SetupIniIP.IniWriteValue("OD", "UpperBound", ODUpperBoundTextBox.Text, CherngerUI.app.DefectSettingpath);
			double.TryParse(ODStandardTextBox.Text, out CherngerUI.Value.StandardOD);
			double.TryParse(ODLowerBoundTextBox.Text, out CherngerUI.Value.LowerBoundOD);
			double.TryParse(ODUpperBoundTextBox.Text, out CherngerUI.Value.UpperBoundOD);

			SetupIniIP.IniWriteValue("ID", "Standard", IDStandardTextBox.Text, CherngerUI.app.DefectSettingpath);
			SetupIniIP.IniWriteValue("ID", "LowerBound", IDLowerBoundTextBox.Text, CherngerUI.app.DefectSettingpath);
			SetupIniIP.IniWriteValue("ID", "UpperBound", IDUpperBoundTextBox.Text, CherngerUI.app.DefectSettingpath);
			double.TryParse(IDStandardTextBox.Text, out CherngerUI.Value.StandardID);
			double.TryParse(IDLowerBoundTextBox.Text, out CherngerUI.Value.LowerBoundID);
			double.TryParse(IDUpperBoundTextBox.Text, out CherngerUI.Value.UpperBoundID);

			SetupIniIP.IniWriteValue("Gap", "Inspect" , GapApplyBtn.Checked.ToString() , CherngerUI.app.DefectSettingpath);
			SetupIniIP.IniWriteValue("Crack", "Inspect", CrackApplyBtn.Checked.ToString(), CherngerUI.app.DefectSettingpath);
			SetupIniIP.IniWriteValue("CatchUp", "Inspect", CatchUpApplyBtn.Checked.ToString(), CherngerUI.app.DefectSettingpath);
			SetupIniIP.IniWriteValue("OD", "Inspect", ODApplyBtn.Checked.ToString(), CherngerUI.app.DefectSettingpath);
			SetupIniIP.IniWriteValue("ID", "Inspect", IDApplyBtn.Checked.ToString(), CherngerUI.app.DefectSettingpath);

			CherngerUI.Value.GapApply = GapApplyBtn.Checked;
			CherngerUI.Value.CrackApply = CrackApplyBtn.Checked;
			CherngerUI.Value.CatchApply = CatchUpApplyBtn.Checked;
			CherngerUI.Value.ODApply = ODApplyBtn.Checked;
			CherngerUI.Value.IDApply = IDApplyBtn.Checked;

            CherngerUI.DefectValue.GapArea = CherngerUI.Value.GapArea;                        //缺角面積
            CherngerUI.DefectValue.GapDeep = CherngerUI.Value.GapDeep;                        //缺角深度
            CherngerUI.DefectValue.CrackArea = CherngerUI.Value.CrackArea;                    //微裂面積
            CherngerUI.DefectValue.CrackLength = CherngerUI.Value.CrackLength;                //微裂長度
            CherngerUI.DefectValue.CatchUpArea = CherngerUI.Value.CatchUpArea;                //髒污面積
            CherngerUI.DefectValue.CatchUpLength = CherngerUI.Value.CatchUpLength;            //髒污長度

            CherngerUI.DefectValue.StandardOD = CherngerUI.Value.StandardOD;                  //外徑標準值
            CherngerUI.DefectValue.LowerBoundOD = CherngerUI.Value.LowerBoundOD;              //外徑下界 
            CherngerUI.DefectValue.UpperBoundOD = CherngerUI.Value.UpperBoundOD;              //外徑上界

            CherngerUI.DefectValue.StandardID = CherngerUI.Value.StandardID;                  //內徑標準值
            CherngerUI.DefectValue.LowerBoundID = CherngerUI.Value.LowerBoundID;              //內徑下界
            CherngerUI.DefectValue.UpperBoundID = CherngerUI.Value.UpperBoundID;              //內徑上界

            CherngerUI.DefectValue.IDRatio = CherngerUI.Value.IDRatio;                        //內徑轉換比
            CherngerUI.DefectValue.ODRatio = CherngerUI.Value.ODRatio;                        //外徑轉換比

            CherngerUI.DefectValue.GapApply = CherngerUI.Value.GapApply;                      //缺角套用檢測
            CherngerUI.DefectValue.CatchApply = CherngerUI.Value.CatchApply;                  //髒污套用檢測
            CherngerUI.DefectValue.CrackApply = CherngerUI.Value.CrackApply;                  //微裂套用檢測
            CherngerUI.DefectValue.ODApply = CherngerUI.Value.ODApply;                        //外徑套用檢測
            CherngerUI.DefectValue.IDApply = CherngerUI.Value.IDApply;                        //內徑套用檢測

            BeginInvoke(new Action(() => MessageBox.Show("儲存完成", "System", MessageBoxButtons.OK, MessageBoxIcon.Information)));
		}

		private void IDLowerBoundTextBox_TextChanged(object sender, EventArgs e)
		{

		}

		private void IDUpperBoundTextBox_TextChanged(object sender, EventArgs e)
		{

		}

		private void CrackApplyBtn_CheckedChanged(object sender, EventArgs e)
		{

		}
	}
}
