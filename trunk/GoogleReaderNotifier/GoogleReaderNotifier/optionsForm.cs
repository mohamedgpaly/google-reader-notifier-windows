#region Includes

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text;
#endregion 

namespace GoogleReader
{
	/// <summary>
	/// optionsForm controls editing and saving configurations
	/// </summary>
	public class optionsForm : GoogleTalkForm
	{
		#region Variables
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox comboTimer;
		private System.Windows.Forms.ComboBox comboMaxUnread;
		private System.Windows.Forms.TextBox txtFilterLabels;
		private System.Windows.Forms.CheckBox chkShowCountTooltip;
		private System.Windows.Forms.CheckBox chkDoNotAnimate;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
				private XmlINI _store;
		#endregion
		private System.Windows.Forms.LinkLabel linkHelp;
		private System.Windows.Forms.CheckBox chkStartWithWindows;


		#region Startup
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private void optionsForm_Load(object sender, System.EventArgs e)
		{
			LoadSettings();
		}

		public optionsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			_store = new XmlINI();
		}

		/// <summary>
		/// Clean up any resources being used.
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

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(optionsForm));
			this.label1 = new System.Windows.Forms.Label();
			this.comboTimer = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboMaxUnread = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtFilterLabels = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkDoNotAnimate = new System.Windows.Forms.CheckBox();
			this.chkShowCountTooltip = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.cmdOK = new System.Windows.Forms.Button();
			this.linkHelp = new System.Windows.Forms.LinkLabel();
			this.chkStartWithWindows = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Check for new items every";
			// 
			// comboTimer
			// 
			this.comboTimer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboTimer.Items.AddRange(new object[] {
															"5",
															"10",
															"15",
															"20",
															"30",
															"60",
															"120"});
			this.comboTimer.Location = new System.Drawing.Point(160, 32);
			this.comboTimer.Name = "comboTimer";
			this.comboTimer.Size = new System.Drawing.Size(56, 21);
			this.comboTimer.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(224, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "minutes";
			// 
			// comboMaxUnread
			// 
			this.comboMaxUnread.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboMaxUnread.Items.AddRange(new object[] {
																"10",
																"20",
																"30",
																"40",
																"50"});
			this.comboMaxUnread.Location = new System.Drawing.Point(928, 24);
			this.comboMaxUnread.Name = "comboMaxUnread";
			this.comboMaxUnread.Size = new System.Drawing.Size(56, 21);
			this.comboMaxUnread.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(680, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(240, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "Maximum number of unread items to display";
			// 
			// comboBox3
			// 
			this.comboBox3.Location = new System.Drawing.Point(752, 152);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(40, 21);
			this.comboBox3.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(472, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(240, 23);
			this.label4.TabIndex = 5;
			this.label4.Text = "Maximum number of simultaneous notifications";
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Location = new System.Drawing.Point(16, 64);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(208, 23);
			this.label5.TabIndex = 7;
			this.label5.Text = "Only display items with label (optional)";
			// 
			// txtFilterLabels
			// 
			this.txtFilterLabels.Location = new System.Drawing.Point(232, 64);
			this.txtFilterLabels.Name = "txtFilterLabels";
			this.txtFilterLabels.Size = new System.Drawing.Size(208, 20);
			this.txtFilterLabels.TabIndex = 8;
			this.txtFilterLabels.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.Transparent;
			this.groupBox1.Controls.Add(this.chkStartWithWindows);
			this.groupBox1.Controls.Add(this.chkDoNotAnimate);
			this.groupBox1.Controls.Add(this.chkShowCountTooltip);
			this.groupBox1.Location = new System.Drawing.Point(16, 96);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(424, 96);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			// 
			// chkDoNotAnimate
			// 
			this.chkDoNotAnimate.Location = new System.Drawing.Point(16, 40);
			this.chkDoNotAnimate.Name = "chkDoNotAnimate";
			this.chkDoNotAnimate.Size = new System.Drawing.Size(248, 24);
			this.chkDoNotAnimate.TabIndex = 1;
			this.chkDoNotAnimate.Text = "don\'t show popup animation";
			// 
			// chkShowCountTooltip
			// 
			this.chkShowCountTooltip.Location = new System.Drawing.Point(16, 16);
			this.chkShowCountTooltip.Name = "chkShowCountTooltip";
			this.chkShowCountTooltip.Size = new System.Drawing.Size(248, 24);
			this.chkShowCountTooltip.TabIndex = 0;
			this.chkShowCountTooltip.Text = "show count in tooltip";
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(560, 192);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(248, 24);
			this.checkBox3.TabIndex = 2;
			this.checkBox3.Text = "don\'t alert on new version";
			// 
			// checkBox5
			// 
			this.checkBox5.Location = new System.Drawing.Point(552, 224);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(248, 24);
			this.checkBox5.TabIndex = 4;
			this.checkBox5.Text = "always use https (secure connection)";
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.Transparent;
			this.groupBox2.Controls.Add(this.txtPassword);
			this.groupBox2.Controls.Add(this.txtUsername);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Location = new System.Drawing.Point(16, 208);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(424, 88);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Google Account Authentication";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(88, 48);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(232, 20);
			this.txtPassword.TabIndex = 3;
			this.txtPassword.Text = "";
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(88, 24);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(232, 20);
			this.txtUsername.TabIndex = 2;
			this.txtUsername.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 48);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 23);
			this.label7.TabIndex = 1;
			this.label7.Text = "Password";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 24);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 23);
			this.label6.TabIndex = 0;
			this.label6.Text = "Username";
			// 
			// cmdCancel
			// 
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(376, 344);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(72, 24);
			this.cmdCancel.TabIndex = 4;
			this.cmdCancel.Text = "Cancel";
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Location = new System.Drawing.Point(16, 312);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(272, 64);
			this.label8.TabIndex = 11;
			this.label8.Text = "Google Reader Notifier is a private open source project utilizing the unofficial " +
				"Google Reader API, and is in no way related to the fine people at Google Corp.";
			// 
			// cmdOK
			// 
			this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.cmdOK.Location = new System.Drawing.Point(296, 344);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(72, 24);
			this.cmdOK.TabIndex = 12;
			this.cmdOK.Text = "OK";
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// linkHelp
			// 
			this.linkHelp.ActiveLinkColor = System.Drawing.Color.Black;
			this.linkHelp.BackColor = System.Drawing.Color.Transparent;
			this.linkHelp.LinkColor = System.Drawing.Color.Black;
			this.linkHelp.Location = new System.Drawing.Point(424, 32);
			this.linkHelp.Name = "linkHelp";
			this.linkHelp.Size = new System.Drawing.Size(32, 16);
			this.linkHelp.TabIndex = 13;
			this.linkHelp.TabStop = true;
			this.linkHelp.Text = "Help";
			this.linkHelp.VisitedLinkColor = System.Drawing.Color.Black;
			this.linkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkHelp_LinkClicked);
			// 
			// chkStartWithWindows
			// 
			this.chkStartWithWindows.Location = new System.Drawing.Point(16, 64);
			this.chkStartWithWindows.Name = "chkStartWithWindows";
			this.chkStartWithWindows.Size = new System.Drawing.Size(248, 24);
			this.chkStartWithWindows.TabIndex = 2;
			this.chkStartWithWindows.Text = "start notifier when Windows starts";
			// 
			// optionsForm
			// 
			this.AcceptButton = this.cmdOK;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(458, 384);
			this.Controls.Add(this.linkHelp);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.txtFilterLabels);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.comboBox3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboMaxUnread);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboTimer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBox3);
			this.Controls.Add(this.checkBox5);
			this.Controls.Add(this.cmdCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsResizable = false;
			this.IsWindowSnappable = false;
			this.MaximizeBox = false;
			this.Name = "optionsForm";
			this.Text = "Reader Notifier Preferences";
			this.Load += new System.EventHandler(this.optionsForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Application Code
		private void LoadSettings()
		{
			comboTimer.SelectedIndex = comboTimer.Items.IndexOf(_store.GetKey("TimerInterval","20"));
			comboMaxUnread.SelectedIndex = comboMaxUnread.Items.IndexOf(_store.GetKey("MaxUnreadDisplay","10"));
			txtFilterLabels.Text = _store.GetKey("LabelFilter","");
			chkShowCountTooltip.Checked = Convert.ToBoolean(_store.GetKey("ShowCountTooltip","True"));
			chkDoNotAnimate.Checked = Convert.ToBoolean(_store.GetKey("DoNotAnimate","False"));
			chkStartWithWindows.Checked = StartupHelper.AppStartsWithWindows();
			txtUsername.Text = _store.GetKey("Username","");
			txtPassword.Text = Encryption.Decrypt(_store.GetKey("Password",""));
		}

		#endregion

		#region Menu And Application Events
		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			// handle saves here
			_store.SaveKey("TimerInterval",comboTimer.SelectedItem.ToString());
			_store.SaveKey("MaxUnreadDisplay",comboMaxUnread.SelectedItem.ToString());
			_store.SaveKey("LabelFilter",txtFilterLabels.Text);
			_store.SaveKey("ShowCountTooltip",chkShowCountTooltip.Checked.ToString());
			_store.SaveKey("DoNotAnimate",chkDoNotAnimate.Checked.ToString());
			_store.SaveKey("Username",txtUsername.Text);
			_store.SaveKey("Password",Encryption.Encrypt(txtPassword.Text)); // need to encrypt this

			StartupHelper.StartWithWindows(chkStartWithWindows.Checked);

			this.DialogResult = DialogResult.OK;
			this.Hide();
			this.Close();		
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Hide();
			this.Close();
		}
		#endregion

		private void linkHelp_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.braindotty.com/google-reader-notifier/help/?from=client");
		}

	}
}
