namespace UnionAoi
{
	partial class ImageProcessing_DefectSetting
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.Button save_4_stop_config;
			this.Stop1 = new System.Windows.Forms.GroupBox();
			this.Stop2 = new System.Windows.Forms.GroupBox();
			this.Stop3 = new System.Windows.Forms.GroupBox();
			this.Stop4 = new System.Windows.Forms.GroupBox();
			this.ODApplyBtn = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Stop4_min_area = new System.Windows.Forms.TextBox();
			this.Stop4_max_area = new System.Windows.Forms.TextBox();
			save_4_stop_config = new System.Windows.Forms.Button();
			this.Stop4.SuspendLayout();
			this.SuspendLayout();
			// 
			// Stop1
			// 
			this.Stop1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.Stop1.Location = new System.Drawing.Point(26, 36);
			this.Stop1.Name = "Stop1";
			this.Stop1.Size = new System.Drawing.Size(539, 249);
			this.Stop1.TabIndex = 1;
			this.Stop1.TabStop = false;
			this.Stop1.Text = "第一站";
			// 
			// Stop2
			// 
			this.Stop2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.Stop2.Location = new System.Drawing.Point(606, 36);
			this.Stop2.Name = "Stop2";
			this.Stop2.Size = new System.Drawing.Size(539, 249);
			this.Stop2.TabIndex = 2;
			this.Stop2.TabStop = false;
			this.Stop2.Text = "第二站";
			this.Stop2.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// Stop3
			// 
			this.Stop3.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.Stop3.Location = new System.Drawing.Point(26, 329);
			this.Stop3.Name = "Stop3";
			this.Stop3.Size = new System.Drawing.Size(539, 249);
			this.Stop3.TabIndex = 3;
			this.Stop3.TabStop = false;
			this.Stop3.Text = "第三站";
			// 
			// Stop4
			// 
			this.Stop4.Controls.Add(this.ODApplyBtn);
			this.Stop4.Controls.Add(this.label1);
			this.Stop4.Controls.Add(this.Stop4_min_area);
			this.Stop4.Controls.Add(this.Stop4_max_area);
			this.Stop4.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.Stop4.Location = new System.Drawing.Point(606, 329);
			this.Stop4.Name = "Stop4";
			this.Stop4.Size = new System.Drawing.Size(539, 249);
			this.Stop4.TabIndex = 4;
			this.Stop4.TabStop = false;
			this.Stop4.Text = "第四站";
			// 
			// ODApplyBtn
			// 
			this.ODApplyBtn.AutoSize = true;
			this.ODApplyBtn.Location = new System.Drawing.Point(369, 0);
			this.ODApplyBtn.Name = "ODApplyBtn";
			this.ODApplyBtn.Size = new System.Drawing.Size(73, 30);
			this.ODApplyBtn.TabIndex = 7;
			this.ODApplyBtn.Text = "套用";
			this.ODApplyBtn.UseVisualStyleBackColor = true;
			this.ODApplyBtn.CheckedChanged += new System.EventHandler(this.ODApplyBtn_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label1.Location = new System.Drawing.Point(125, 62);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 21);
			this.label1.TabIndex = 2;
			this.label1.Text = "> 瑕疵面積 >";
			// 
			// Stop4_min_area
			// 
			this.Stop4_min_area.Location = new System.Drawing.Point(237, 54);
			this.Stop4_min_area.Name = "Stop4_min_area";
			this.Stop4_min_area.Size = new System.Drawing.Size(100, 35);
			this.Stop4_min_area.TabIndex = 1;
			this.Stop4_min_area.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// Stop4_max_area
			// 
			this.Stop4_max_area.Location = new System.Drawing.Point(21, 54);
			this.Stop4_max_area.Name = "Stop4_max_area";
			this.Stop4_max_area.Size = new System.Drawing.Size(100, 35);
			this.Stop4_max_area.TabIndex = 0;
			this.Stop4_max_area.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// save_4_stop_config
			// 
			save_4_stop_config.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			save_4_stop_config.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			save_4_stop_config.Location = new System.Drawing.Point(1151, 419);
			save_4_stop_config.Name = "save_4_stop_config";
			save_4_stop_config.Size = new System.Drawing.Size(53, 159);
			save_4_stop_config.TabIndex = 5;
			save_4_stop_config.Text = "儲存設定";
			save_4_stop_config.UseVisualStyleBackColor = true;
			save_4_stop_config.Click += new System.EventHandler(this.save_4_stop_config_Click);
			// 
			// ImageProcessing_DefectSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(1216, 590);
			this.Controls.Add(save_4_stop_config);
			this.Controls.Add(this.Stop4);
			this.Controls.Add(this.Stop3);
			this.Controls.Add(this.Stop2);
			this.Controls.Add(this.Stop1);
			this.Name = "ImageProcessing_DefectSetting";
			this.Text = "ImageProcessing_DefectSetting";
			this.Load += new System.EventHandler(this.ImageProcessing_DefectSetting_Load);
			this.Stop4.ResumeLayout(false);
			this.Stop4.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox Stop1;
		private System.Windows.Forms.GroupBox Stop2;
		private System.Windows.Forms.GroupBox Stop3;
		private System.Windows.Forms.GroupBox Stop4;
		private System.Windows.Forms.TextBox Stop4_min_area;
		private System.Windows.Forms.TextBox Stop4_max_area;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox ODApplyBtn;
	}
}