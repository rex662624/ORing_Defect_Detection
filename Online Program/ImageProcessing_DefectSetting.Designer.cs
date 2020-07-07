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
			this.label4 = new System.Windows.Forms.Label();
			this.Stop1_checkbox = new System.Windows.Forms.CheckBox();
			this.Stop1_ignore_inner_circle = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.Stop1_max_outer_defect = new System.Windows.Forms.TextBox();
			this.Stop1_min_outer_defect = new System.Windows.Forms.TextBox();
			this.Stop2 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.Stop2_ignore_inner_circle = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.Stop2_min_outer_defect = new System.Windows.Forms.TextBox();
			this.Stop2_checkbox = new System.Windows.Forms.CheckBox();
			this.Stop2_max_outer_defect = new System.Windows.Forms.TextBox();
			this.Stop4 = new System.Windows.Forms.GroupBox();
			this.Stop4_checkbox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Stop4_min_area = new System.Windows.Forms.TextBox();
			this.Stop4_max_area = new System.Windows.Forms.TextBox();
			save_4_stop_config = new System.Windows.Forms.Button();
			this.Stop1.SuspendLayout();
			this.Stop2.SuspendLayout();
			this.Stop4.SuspendLayout();
			this.SuspendLayout();
			// 
			// save_4_stop_config
			// 
			save_4_stop_config.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			save_4_stop_config.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			save_4_stop_config.Location = new System.Drawing.Point(429, 886);
			save_4_stop_config.Name = "save_4_stop_config";
			save_4_stop_config.Size = new System.Drawing.Size(150, 51);
			save_4_stop_config.TabIndex = 5;
			save_4_stop_config.Text = "儲存設定";
			save_4_stop_config.UseVisualStyleBackColor = true;
			save_4_stop_config.Click += new System.EventHandler(this.save_4_stop_config_Click);
			// 
			// Stop1
			// 
			this.Stop1.Controls.Add(this.label4);
			this.Stop1.Controls.Add(this.Stop1_checkbox);
			this.Stop1.Controls.Add(this.Stop1_ignore_inner_circle);
			this.Stop1.Controls.Add(this.label5);
			this.Stop1.Controls.Add(this.Stop1_max_outer_defect);
			this.Stop1.Controls.Add(this.Stop1_min_outer_defect);
			this.Stop1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.Stop1.Location = new System.Drawing.Point(26, 36);
			this.Stop1.Name = "Stop1";
			this.Stop1.Size = new System.Drawing.Size(539, 249);
			this.Stop1.TabIndex = 1;
			this.Stop1.TabStop = false;
			this.Stop1.Text = "第一站";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label4.Location = new System.Drawing.Point(219, 119);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(114, 21);
			this.label4.TabIndex = 17;
			this.label4.Text = "忽略內圈面積: ";
			// 
			// Stop1_checkbox
			// 
			this.Stop1_checkbox.AutoSize = true;
			this.Stop1_checkbox.Location = new System.Drawing.Point(351, 0);
			this.Stop1_checkbox.Name = "Stop1_checkbox";
			this.Stop1_checkbox.Size = new System.Drawing.Size(73, 30);
			this.Stop1_checkbox.TabIndex = 8;
			this.Stop1_checkbox.Text = "套用";
			this.Stop1_checkbox.UseVisualStyleBackColor = true;
			this.Stop1_checkbox.CheckedChanged += new System.EventHandler(this.Stop1_checkbox_CheckedChanged);
			// 
			// Stop1_ignore_inner_circle
			// 
			this.Stop1_ignore_inner_circle.Location = new System.Drawing.Point(339, 111);
			this.Stop1_ignore_inner_circle.Name = "Stop1_ignore_inner_circle";
			this.Stop1_ignore_inner_circle.Size = new System.Drawing.Size(100, 35);
			this.Stop1_ignore_inner_circle.TabIndex = 16;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label5.Location = new System.Drawing.Point(195, 53);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(138, 21);
			this.label5.TabIndex = 15;
			this.label5.Text = "> 外部瑕疵面積 >";
			// 
			// Stop1_max_outer_defect
			// 
			this.Stop1_max_outer_defect.Location = new System.Drawing.Point(91, 45);
			this.Stop1_max_outer_defect.Name = "Stop1_max_outer_defect";
			this.Stop1_max_outer_defect.Size = new System.Drawing.Size(100, 35);
			this.Stop1_max_outer_defect.TabIndex = 13;
			// 
			// Stop1_min_outer_defect
			// 
			this.Stop1_min_outer_defect.Location = new System.Drawing.Point(339, 45);
			this.Stop1_min_outer_defect.Name = "Stop1_min_outer_defect";
			this.Stop1_min_outer_defect.Size = new System.Drawing.Size(100, 35);
			this.Stop1_min_outer_defect.TabIndex = 14;
			// 
			// Stop2
			// 
			this.Stop2.Controls.Add(this.label3);
			this.Stop2.Controls.Add(this.Stop2_ignore_inner_circle);
			this.Stop2.Controls.Add(this.label2);
			this.Stop2.Controls.Add(this.Stop2_min_outer_defect);
			this.Stop2.Controls.Add(this.Stop2_checkbox);
			this.Stop2.Controls.Add(this.Stop2_max_outer_defect);
			this.Stop2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.Stop2.Location = new System.Drawing.Point(26, 335);
			this.Stop2.Name = "Stop2";
			this.Stop2.Size = new System.Drawing.Size(539, 249);
			this.Stop2.TabIndex = 2;
			this.Stop2.TabStop = false;
			this.Stop2.Text = "第二站";
			this.Stop2.Enter += new System.EventHandler(this.groupBox1_Enter);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label3.Location = new System.Drawing.Point(233, 119);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(114, 21);
			this.label3.TabIndex = 12;
			this.label3.Text = "忽略內圈面積: ";
			this.label3.Click += new System.EventHandler(this.label3_Click);
			// 
			// Stop2_ignore_inner_circle
			// 
			this.Stop2_ignore_inner_circle.Location = new System.Drawing.Point(353, 111);
			this.Stop2_ignore_inner_circle.Name = "Stop2_ignore_inner_circle";
			this.Stop2_ignore_inner_circle.Size = new System.Drawing.Size(100, 35);
			this.Stop2_ignore_inner_circle.TabIndex = 11;
			this.Stop2_ignore_inner_circle.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label2.Location = new System.Drawing.Point(209, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(138, 21);
			this.label2.TabIndex = 10;
			this.label2.Text = "> 外部瑕疵面積 >";
			// 
			// Stop2_min_outer_defect
			// 
			this.Stop2_min_outer_defect.Location = new System.Drawing.Point(353, 45);
			this.Stop2_min_outer_defect.Name = "Stop2_min_outer_defect";
			this.Stop2_min_outer_defect.Size = new System.Drawing.Size(100, 35);
			this.Stop2_min_outer_defect.TabIndex = 9;
			// 
			// Stop2_checkbox
			// 
			this.Stop2_checkbox.AutoSize = true;
			this.Stop2_checkbox.Location = new System.Drawing.Point(369, 0);
			this.Stop2_checkbox.Name = "Stop2_checkbox";
			this.Stop2_checkbox.Size = new System.Drawing.Size(73, 30);
			this.Stop2_checkbox.TabIndex = 8;
			this.Stop2_checkbox.Text = "套用";
			this.Stop2_checkbox.UseVisualStyleBackColor = true;
			this.Stop2_checkbox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// Stop2_max_outer_defect
			// 
			this.Stop2_max_outer_defect.Location = new System.Drawing.Point(105, 45);
			this.Stop2_max_outer_defect.Name = "Stop2_max_outer_defect";
			this.Stop2_max_outer_defect.Size = new System.Drawing.Size(100, 35);
			this.Stop2_max_outer_defect.TabIndex = 8;
			// 
			// Stop4
			// 
			this.Stop4.Controls.Add(this.Stop4_checkbox);
			this.Stop4.Controls.Add(this.label1);
			this.Stop4.Controls.Add(this.Stop4_min_area);
			this.Stop4.Controls.Add(this.Stop4_max_area);
			this.Stop4.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.Stop4.Location = new System.Drawing.Point(26, 622);
			this.Stop4.Name = "Stop4";
			this.Stop4.Size = new System.Drawing.Size(539, 249);
			this.Stop4.TabIndex = 4;
			this.Stop4.TabStop = false;
			this.Stop4.Text = "第四站";
			// 
			// Stop4_checkbox
			// 
			this.Stop4_checkbox.AutoSize = true;
			this.Stop4_checkbox.Location = new System.Drawing.Point(369, 0);
			this.Stop4_checkbox.Name = "Stop4_checkbox";
			this.Stop4_checkbox.Size = new System.Drawing.Size(73, 30);
			this.Stop4_checkbox.TabIndex = 7;
			this.Stop4_checkbox.Text = "套用";
			this.Stop4_checkbox.UseVisualStyleBackColor = true;
			this.Stop4_checkbox.CheckedChanged += new System.EventHandler(this.ODApplyBtn_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label1.Location = new System.Drawing.Point(209, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 21);
			this.label1.TabIndex = 2;
			this.label1.Text = "> 瑕疵面積 >";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// Stop4_min_area
			// 
			this.Stop4_min_area.Location = new System.Drawing.Point(321, 56);
			this.Stop4_min_area.Name = "Stop4_min_area";
			this.Stop4_min_area.Size = new System.Drawing.Size(100, 35);
			this.Stop4_min_area.TabIndex = 1;
			this.Stop4_min_area.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// Stop4_max_area
			// 
			this.Stop4_max_area.Location = new System.Drawing.Point(105, 56);
			this.Stop4_max_area.Name = "Stop4_max_area";
			this.Stop4_max_area.Size = new System.Drawing.Size(100, 35);
			this.Stop4_max_area.TabIndex = 0;
			this.Stop4_max_area.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// ImageProcessing_DefectSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(605, 953);
			this.Controls.Add(save_4_stop_config);
			this.Controls.Add(this.Stop4);
			this.Controls.Add(this.Stop2);
			this.Controls.Add(this.Stop1);
			this.Name = "ImageProcessing_DefectSetting";
			this.Text = "ImageProcessing_DefectSetting";
			this.Load += new System.EventHandler(this.ImageProcessing_DefectSetting_Load);
			this.Stop1.ResumeLayout(false);
			this.Stop1.PerformLayout();
			this.Stop2.ResumeLayout(false);
			this.Stop2.PerformLayout();
			this.Stop4.ResumeLayout(false);
			this.Stop4.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox Stop1;
		private System.Windows.Forms.GroupBox Stop2;
		private System.Windows.Forms.GroupBox Stop4;
		private System.Windows.Forms.TextBox Stop4_min_area;
		private System.Windows.Forms.TextBox Stop4_max_area;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox Stop4_checkbox;
		private System.Windows.Forms.CheckBox Stop1_checkbox;
		private System.Windows.Forms.CheckBox Stop2_checkbox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox Stop2_ignore_inner_circle;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox Stop2_min_outer_defect;
		private System.Windows.Forms.TextBox Stop2_max_outer_defect;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox Stop1_ignore_inner_circle;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox Stop1_max_outer_defect;
		private System.Windows.Forms.TextBox Stop1_min_outer_defect;
	}
}