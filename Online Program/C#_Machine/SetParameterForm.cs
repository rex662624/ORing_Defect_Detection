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

namespace USB
{
    public partial class SetParameterForm : Form
    {
        public SetParameterForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Side_Surrounding.Text != string.Empty && Side_Crash.Text != string.Empty && Side_Window.Text != string.Empty && Side_Thickslice.Text != string.Empty
                && Side_Copper.Text != string.Empty && Side_Glueline.Text != string.Empty && Side_Glueloss.Text != string.Empty && Side_Iron.Text != string.Empty
                && Edge_Surrounding.Text != string.Empty && Edge_Crash.Text != string.Empty && Edge_Copper.Text != string.Empty)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                string path = System.IO.Directory.GetCurrentDirectory() + @"\標準參數\";
                saveFileDialog1.InitialDirectory = path;
                saveFileDialog1.Filter = "晟格設定檔(.cn)|*.cn";
                saveFileDialog1.Title = "儲存設定檔";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    SetupIniIP.IniWriteValue("Side", "Surrounding", Side_Surrounding.Text, saveFileDialog1.FileName);
                    SetupIniIP.IniWriteValue("Side", "Crash", Side_Crash.Text, saveFileDialog1.FileName);
                    SetupIniIP.IniWriteValue("Side", "Window", Side_Window.Text, saveFileDialog1.FileName);
                    SetupIniIP.IniWriteValue("Side", "ThickSlice", Side_Thickslice.Text, saveFileDialog1.FileName);
                    SetupIniIP.IniWriteValue("Side", "Copper", Side_Copper.Text, saveFileDialog1.FileName);
                    SetupIniIP.IniWriteValue("Side", "GlueLine", Side_Glueline.Text, saveFileDialog1.FileName);
                    SetupIniIP.IniWriteValue("Side", "GlueLoss", Side_Glueloss.Text, saveFileDialog1.FileName);
                    SetupIniIP.IniWriteValue("Side", "Iron", Side_Iron.Text, saveFileDialog1.FileName);
                    SetupIniIP.IniWriteValue("Edge", "Surrounding", Edge_Surrounding.Text, saveFileDialog1.FileName);
                    SetupIniIP.IniWriteValue("Edge", "Crash", Edge_Crash.Text, saveFileDialog1.FileName);
                    SetupIniIP.IniWriteValue("Edge", "Copper", Edge_Copper.Text, saveFileDialog1.FileName);
                }

            }
            else
            {
                MessageBox.Show("標準不能為空");
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string path = System.IO.Directory.GetCurrentDirectory() + @"\標準參數\";
            openFileDialog.InitialDirectory = path;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "讀取設定檔";
            openFileDialog.Filter = "晟格設定檔(.cn)|*.cn";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && openFileDialog.FileName != null)
            {
                if (Double.TryParse(SetupIniIP.IniReadValue("Side", "Surrounding", openFileDialog.FileName), out var SIDESURROUNDINGSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "Crash", openFileDialog.FileName), out var SIDECRASHSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "Window", openFileDialog.FileName), out var SIDEWINDOWSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "ThickSlice", openFileDialog.FileName), out var SIDETHICKSLICESIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "Copper", openFileDialog.FileName), out var SIDECOPPERSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "GlueLine", openFileDialog.FileName), out var SIDEGLUELINESIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "GlueLoss", openFileDialog.FileName), out var SIDEGLUELOSSSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "Iron", openFileDialog.FileName), out var SIDEIRONSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Edge", "Surrounding", openFileDialog.FileName), out var EDGESURROUNDINGSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Edge", "Crash", openFileDialog.FileName), out var EDGECRASHSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Edge", "Copper", openFileDialog.FileName), out var EDGECOPPERSIZE))
                {
                    Side_Surrounding.Text = SIDESURROUNDINGSIZE.ToString();
                    Side_Crash.Text = SIDECRASHSIZE.ToString();
                    Side_Window.Text = SIDEWINDOWSIZE.ToString();
                    Side_Thickslice.Text = SIDETHICKSLICESIZE.ToString();
                    Side_Copper.Text = SIDECOPPERSIZE.ToString();
                    Side_Glueline.Text = SIDEGLUELINESIZE.ToString();
                    Side_Glueloss.Text = SIDEGLUELOSSSIZE.ToString();
                    Side_Iron.Text = SIDEIRONSIZE.ToString();
                    Edge_Surrounding.Text = EDGESURROUNDINGSIZE.ToString();
                    Edge_Crash.Text = EDGECRASHSIZE.ToString();
                    Edge_Copper.Text = EDGECOPPERSIZE.ToString();

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string FileName = System.IO.Directory.GetCurrentDirectory() + @"\標準參數\OriginSetting.default";
            if (System.IO.File.Exists(FileName))
            {
                if (Double.TryParse(SetupIniIP.IniReadValue("Side", "Surrounding", FileName), out var SIDESURROUNDINGSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "Crash", FileName), out var SIDECRASHSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "Window", FileName), out var SIDEWINDOWSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "ThickSlice", FileName), out var SIDETHICKSLICESIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "Copper", FileName), out var SIDECOPPERSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "GlueLine", FileName), out var SIDEGLUELINESIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "GlueLoss", FileName), out var SIDEGLUELOSSSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Side", "Iron", FileName), out var SIDEIRONSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Edge", "Surrounding", FileName), out var EDGESURROUNDINGSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Edge", "Crash", FileName), out var EDGECRASHSIZE)
                 && Double.TryParse(SetupIniIP.IniReadValue("Edge", "Copper", FileName), out var EDGECOPPERSIZE))
                {
                    Side_Surrounding.Text = SIDESURROUNDINGSIZE.ToString();
                    Side_Crash.Text = SIDECRASHSIZE.ToString();
                    Side_Window.Text = SIDEWINDOWSIZE.ToString();
                    Side_Thickslice.Text = SIDETHICKSLICESIZE.ToString();
                    Side_Copper.Text = SIDECOPPERSIZE.ToString();
                    Side_Glueline.Text = SIDEGLUELINESIZE.ToString();
                    Side_Glueloss.Text = SIDEGLUELOSSSIZE.ToString();
                    Side_Iron.Text = SIDEIRONSIZE.ToString();
                    Edge_Surrounding.Text = EDGESURROUNDINGSIZE.ToString();
                    Edge_Crash.Text = EDGECRASHSIZE.ToString();
                    Edge_Copper.Text = EDGECOPPERSIZE.ToString();

                }
            }
            else
            {
                MessageBox.Show("預設檔案遺失,請重新設定預設檔案");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Side_Surrounding.Text != string.Empty && Side_Crash.Text != string.Empty && Side_Window.Text != string.Empty && Side_Thickslice.Text != string.Empty
                && Side_Copper.Text != string.Empty && Side_Glueline.Text != string.Empty && Side_Glueloss.Text != string.Empty && Side_Iron.Text != string.Empty
                && Edge_Surrounding.Text != string.Empty && Edge_Crash.Text != string.Empty && Edge_Copper.Text != string.Empty)
            {
                string FileName = System.IO.Directory.GetCurrentDirectory() + @"\標準參數\OriginSetting.default";
                
                    SetupIniIP.IniWriteValue("Side", "Surrounding", Side_Surrounding.Text, FileName);
                    SetupIniIP.IniWriteValue("Side", "Crash", Side_Crash.Text, FileName);
                    SetupIniIP.IniWriteValue("Side", "Window", Side_Window.Text, FileName);
                    SetupIniIP.IniWriteValue("Side", "ThickSlice", Side_Thickslice.Text, FileName);
                    SetupIniIP.IniWriteValue("Side", "Copper", Side_Copper.Text, FileName);
                    SetupIniIP.IniWriteValue("Side", "GlueLine", Side_Glueline.Text, FileName);
                    SetupIniIP.IniWriteValue("Side", "GlueLoss", Side_Glueloss.Text, FileName);
                    SetupIniIP.IniWriteValue("Side", "Iron", Side_Iron.Text, FileName);
                    SetupIniIP.IniWriteValue("Edge", "Surrounding", Edge_Surrounding.Text, FileName);
                    SetupIniIP.IniWriteValue("Edge", "Crash", Edge_Crash.Text, FileName);
                    SetupIniIP.IniWriteValue("Edge", "Copper", Edge_Copper.Text, FileName);
                

            }
            else
            {
                MessageBox.Show("標準不能為空");
            }
        }
    }
}
