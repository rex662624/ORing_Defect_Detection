using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SensorTechnology
{
	/// <summary>
	/// SetCameraIDForm
	/// </summary>
	public class SetCameraIDForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtCameraNo;
		private System.Windows.Forms.TextBox txtCameraName;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblCameraNo;
		private System.Windows.Forms.Label lblCameraName;
		/// <summary>
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SetCameraIDForm()
		{
			//
			//
			InitializeComponent();

			//
			//
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

		#region Windows
		/// <summary>
		/// </summary>
		private void InitializeComponent()
		{
            this.txtCameraNo = new System.Windows.Forms.TextBox();
            this.txtCameraName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCameraNo = new System.Windows.Forms.Label();
            this.lblCameraName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCameraNo
            // 
            this.txtCameraNo.Location = new System.Drawing.Point(104, 60);
            this.txtCameraNo.MaxLength = 10;
            this.txtCameraNo.Name = "txtCameraNo";
            this.txtCameraNo.Size = new System.Drawing.Size(72, 22);
            this.txtCameraNo.TabIndex = 0;
            this.txtCameraNo.Text = "0";
            this.txtCameraNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCameraName
            // 
            this.txtCameraName.Location = new System.Drawing.Point(104, 20);
            this.txtCameraName.MaxLength = 125;
            this.txtCameraName.Name = "txtCameraName";
            this.txtCameraName.Size = new System.Drawing.Size(216, 22);
            this.txtCameraName.TabIndex = 1;
            this.txtCameraName.Text = "STC-XXXXXUSB";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(152, 100);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(240, 100);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            // 
            // lblCameraNo
            // 
            this.lblCameraNo.AutoSize = true;
            this.lblCameraNo.Location = new System.Drawing.Point(16, 60);
            this.lblCameraNo.Name = "lblCameraNo";
            this.lblCameraNo.Size = new System.Drawing.Size(58, 12);
            this.lblCameraNo.TabIndex = 4;
            this.lblCameraNo.Text = "Camera No";
            // 
            // lblCameraName
            // 
            this.lblCameraName.AutoSize = true;
            this.lblCameraName.Location = new System.Drawing.Point(16, 20);
            this.lblCameraName.Name = "lblCameraName";
            this.lblCameraName.Size = new System.Drawing.Size(71, 12);
            this.lblCameraName.TabIndex = 5;
            this.lblCameraName.Text = "Camera Name";
            // 
            // SetCameraIDForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
            this.ClientSize = new System.Drawing.Size(330, 140);
            this.Controls.Add(this.lblCameraName);
            this.Controls.Add(this.lblCameraNo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtCameraName);
            this.Controls.Add(this.txtCameraNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SetCameraIDForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Camera ID";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public string CameraName
		{
			get
			{
				return(txtCameraName.Text);
			}
			set
			{
				txtCameraName.Text = value;
			}
		}
		public uint CameraNo
		{
			get
			{
				uint cameraNo = 0;
				try
				{
					cameraNo = uint.Parse(txtCameraNo.Text);
				}
				catch(Exception)
				{

				}
				return(cameraNo);
			}
			set
			{
				txtCameraNo.Text = value.ToString();
			}
		}
	}
}
