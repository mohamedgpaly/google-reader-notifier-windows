using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;
using System.Data;

namespace GoogleReaderNotifier.WinUI
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class PreferencesForm : BaseClasses.BaseForm
	{
        private GoogleReaderNotifier.ReaderAPI.GoogleReader reader = null;

		private System.Windows.Forms.Label _label1;
        private System.Windows.Forms.Label _label2;
		private System.Windows.Forms.GroupBox _groupBox1;
		private System.Windows.Forms.GroupBox _groupBox2;
		private System.Windows.Forms.Label _label7;
		private System.Windows.Forms.Label _label6;
		private System.Windows.Forms.Label _label8;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.LinkLabel _helpLink;
        private System.Windows.Forms.ComboBox _timerMinutes;
		private System.Windows.Forms.CheckBox _startWithWindows;
		private System.Windows.Forms.CheckBox _animatePopup;
		private System.Windows.Forms.CheckBox _showCountTooltip;
		private System.Windows.Forms.TextBox _password;
		private System.Windows.Forms.TextBox _userName;
        private IContainer components;
        private GroupBox _noficationAudoFilePathGroupBox;
        private Button _selectNotificationAudioFilePathButton;
        private TextBox _notificationAudioFilePath;
        private GroupBox tagsGroupBox;
        private CheckedListBox tagsListBox;
        private Button refreshTagsButton;
        private ContextMenuStrip tagListBoxContextMenuStrip;
        private ToolStripMenuItem checkAllToolStripMenuItem;
        private ToolStripMenuItem checkNoneToolStripMenuItem;
		private System.Windows.Forms.ErrorProvider _errorProvider;
		//private System.ComponentModel.IContainer components;

		public PreferencesForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		#region Dispose

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
                //if (components != null) 
                //{
                //    components.Dispose();
                //}
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesForm));
            this._label1 = new System.Windows.Forms.Label();
            this._timerMinutes = new System.Windows.Forms.ComboBox();
            this._label2 = new System.Windows.Forms.Label();
            this._groupBox1 = new System.Windows.Forms.GroupBox();
            this._startWithWindows = new System.Windows.Forms.CheckBox();
            this._animatePopup = new System.Windows.Forms.CheckBox();
            this._showCountTooltip = new System.Windows.Forms.CheckBox();
            this._groupBox2 = new System.Windows.Forms.GroupBox();
            this._password = new System.Windows.Forms.TextBox();
            this._userName = new System.Windows.Forms.TextBox();
            this._label7 = new System.Windows.Forms.Label();
            this._label6 = new System.Windows.Forms.Label();
            this._label8 = new System.Windows.Forms.Label();
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._helpLink = new System.Windows.Forms.LinkLabel();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._noficationAudoFilePathGroupBox = new System.Windows.Forms.GroupBox();
            this._selectNotificationAudioFilePathButton = new System.Windows.Forms.Button();
            this._notificationAudioFilePath = new System.Windows.Forms.TextBox();
            this.tagsGroupBox = new System.Windows.Forms.GroupBox();
            this.refreshTagsButton = new System.Windows.Forms.Button();
            this.tagsListBox = new System.Windows.Forms.CheckedListBox();
            this.tagListBoxContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._groupBox1.SuspendLayout();
            this._groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this._noficationAudoFilePathGroupBox.SuspendLayout();
            this.tagsGroupBox.SuspendLayout();
            this.tagListBoxContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _label1
            // 
            this._label1.BackColor = System.Drawing.Color.Transparent;
            this._label1.Location = new System.Drawing.Point(16, 40);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(144, 23);
            this._label1.TabIndex = 1;
            this._label1.Text = "Check for new items every";
            // 
            // _timerMinutes
            // 
            this._timerMinutes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._timerMinutes.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "30",
            "60",
            "120"});
            this._timerMinutes.Location = new System.Drawing.Point(152, 40);
            this._timerMinutes.Name = "_timerMinutes";
            this._timerMinutes.Size = new System.Drawing.Size(56, 21);
            this._timerMinutes.TabIndex = 2;
            // 
            // _label2
            // 
            this._label2.BackColor = System.Drawing.Color.Transparent;
            this._label2.Location = new System.Drawing.Point(216, 40);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(56, 23);
            this._label2.TabIndex = 3;
            this._label2.Text = "minutes";
            // 
            // _groupBox1
            // 
            this._groupBox1.BackColor = System.Drawing.Color.Transparent;
            this._groupBox1.Controls.Add(this._startWithWindows);
            this._groupBox1.Controls.Add(this._animatePopup);
            this._groupBox1.Controls.Add(this._showCountTooltip);
            this._groupBox1.Location = new System.Drawing.Point(16, 204);
            this._groupBox1.Name = "_groupBox1";
            this._groupBox1.Size = new System.Drawing.Size(422, 87);
            this._groupBox1.TabIndex = 10;
            this._groupBox1.TabStop = false;
            // 
            // _startWithWindows
            // 
            this._startWithWindows.Location = new System.Drawing.Point(16, 55);
            this._startWithWindows.Name = "_startWithWindows";
            this._startWithWindows.Size = new System.Drawing.Size(248, 24);
            this._startWithWindows.TabIndex = 2;
            this._startWithWindows.Text = "start notifier when Windows starts";
            // 
            // _animatePopup
            // 
            this._animatePopup.Location = new System.Drawing.Point(16, 37);
            this._animatePopup.Name = "_animatePopup";
            this._animatePopup.Size = new System.Drawing.Size(248, 21);
            this._animatePopup.TabIndex = 1;
            this._animatePopup.Text = "animate popup";
            // 
            // _showCountTooltip
            // 
            this._showCountTooltip.Location = new System.Drawing.Point(16, 16);
            this._showCountTooltip.Name = "_showCountTooltip";
            this._showCountTooltip.Size = new System.Drawing.Size(248, 21);
            this._showCountTooltip.TabIndex = 0;
            this._showCountTooltip.Text = "show count in tooltip";
            // 
            // _groupBox2
            // 
            this._groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._groupBox2.BackColor = System.Drawing.Color.Transparent;
            this._groupBox2.Controls.Add(this._password);
            this._groupBox2.Controls.Add(this._userName);
            this._groupBox2.Controls.Add(this._label7);
            this._groupBox2.Controls.Add(this._label6);
            this._groupBox2.Location = new System.Drawing.Point(16, 353);
            this._groupBox2.Name = "_groupBox2";
            this._groupBox2.Size = new System.Drawing.Size(424, 76);
            this._groupBox2.TabIndex = 11;
            this._groupBox2.TabStop = false;
            this._groupBox2.Text = "Google Account Authentication";
            // 
            // _password
            // 
            this._errorProvider.SetError(this._password, "Required!");
            this._password.Location = new System.Drawing.Point(88, 48);
            this._password.Name = "_password";
            this._password.PasswordChar = '*';
            this._password.Size = new System.Drawing.Size(232, 20);
            this._password.TabIndex = 3;
            // 
            // _userName
            // 
            this._errorProvider.SetError(this._userName, "Required!");
            this._userName.Location = new System.Drawing.Point(88, 24);
            this._userName.Name = "_userName";
            this._userName.Size = new System.Drawing.Size(232, 20);
            this._userName.TabIndex = 2;
            // 
            // _label7
            // 
            this._label7.Location = new System.Drawing.Point(16, 48);
            this._label7.Name = "_label7";
            this._label7.Size = new System.Drawing.Size(56, 23);
            this._label7.TabIndex = 1;
            this._label7.Text = "Password";
            // 
            // _label6
            // 
            this._label6.Location = new System.Drawing.Point(16, 24);
            this._label6.Name = "_label6";
            this._label6.Size = new System.Drawing.Size(64, 23);
            this._label6.TabIndex = 0;
            this._label6.Text = "Username";
            // 
            // _label8
            // 
            this._label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._label8.BackColor = System.Drawing.Color.Transparent;
            this._label8.Location = new System.Drawing.Point(16, 432);
            this._label8.Name = "_label8";
            this._label8.Size = new System.Drawing.Size(272, 48);
            this._label8.TabIndex = 12;
            this._label8.Text = "Google Reader Notifier is a private open source project utilizing the unofficial " +
                "Google Reader API, and is in no way related to the fine people at Google Corp.";
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._okButton.Location = new System.Drawing.Point(290, 456);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(72, 24);
            this._okButton.TabIndex = 13;
            this._okButton.Text = "OK";
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.CausesValidation = false;
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._cancelButton.Location = new System.Drawing.Point(368, 456);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(72, 24);
            this._cancelButton.TabIndex = 14;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _helpLink
            // 
            this._helpLink.ActiveLinkColor = System.Drawing.Color.Black;
            this._helpLink.BackColor = System.Drawing.Color.Transparent;
            this._helpLink.LinkColor = System.Drawing.Color.Black;
            this._helpLink.Location = new System.Drawing.Point(408, 32);
            this._helpLink.Name = "_helpLink";
            this._helpLink.Size = new System.Drawing.Size(32, 16);
            this._helpLink.TabIndex = 15;
            this._helpLink.TabStop = true;
            this._helpLink.Text = "Help";
            this._helpLink.VisitedLinkColor = System.Drawing.Color.Black;
            this._helpLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HandleHelpLinkClicked);
            // 
            // _errorProvider
            // 
            this._errorProvider.ContainerControl = this;
            // 
            // _noficationAudoFilePathGroupBox
            // 
            this._noficationAudoFilePathGroupBox.BackColor = System.Drawing.Color.Transparent;
            this._noficationAudoFilePathGroupBox.Controls.Add(this._selectNotificationAudioFilePathButton);
            this._noficationAudoFilePathGroupBox.Controls.Add(this._notificationAudioFilePath);
            this._noficationAudoFilePathGroupBox.Location = new System.Drawing.Point(16, 297);
            this._noficationAudoFilePathGroupBox.Name = "_noficationAudoFilePathGroupBox";
            this._noficationAudoFilePathGroupBox.Size = new System.Drawing.Size(422, 50);
            this._noficationAudoFilePathGroupBox.TabIndex = 16;
            this._noficationAudoFilePathGroupBox.TabStop = false;
            this._noficationAudoFilePathGroupBox.Text = "Notification Audo File";
            // 
            // _selectNotificationAudioFilePathButton
            // 
            this._selectNotificationAudioFilePathButton.BackColor = System.Drawing.SystemColors.Control;
            this._selectNotificationAudioFilePathButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._selectNotificationAudioFilePathButton.Location = new System.Drawing.Point(386, 15);
            this._selectNotificationAudioFilePathButton.Name = "_selectNotificationAudioFilePathButton";
            this._selectNotificationAudioFilePathButton.Size = new System.Drawing.Size(25, 20);
            this._selectNotificationAudioFilePathButton.TabIndex = 6;
            this._selectNotificationAudioFilePathButton.Text = "...";
            this._selectNotificationAudioFilePathButton.UseVisualStyleBackColor = false;
            this._selectNotificationAudioFilePathButton.Click += new System.EventHandler(this._selectNotificationAudioFilePathButton_Click);
            // 
            // _notificationAudioFilePath
            // 
            this._notificationAudioFilePath.Location = new System.Drawing.Point(11, 15);
            this._notificationAudioFilePath.Name = "_notificationAudioFilePath";
            this._notificationAudioFilePath.Size = new System.Drawing.Size(371, 20);
            this._notificationAudioFilePath.TabIndex = 5;
            // 
            // tagsGroupBox
            // 
            this.tagsGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.tagsGroupBox.Controls.Add(this.refreshTagsButton);
            this.tagsGroupBox.Controls.Add(this.tagsListBox);
            this.tagsGroupBox.Location = new System.Drawing.Point(16, 67);
            this.tagsGroupBox.Name = "tagsGroupBox";
            this.tagsGroupBox.Size = new System.Drawing.Size(421, 131);
            this.tagsGroupBox.TabIndex = 17;
            this.tagsGroupBox.TabStop = false;
            this.tagsGroupBox.Text = "Only notify for items tagged as...";
            // 
            // refreshTagsButton
            // 
            this.refreshTagsButton.BackColor = System.Drawing.SystemColors.Control;
            this.refreshTagsButton.Location = new System.Drawing.Point(328, 19);
            this.refreshTagsButton.Name = "refreshTagsButton";
            this.refreshTagsButton.Size = new System.Drawing.Size(87, 23);
            this.refreshTagsButton.TabIndex = 1;
            this.refreshTagsButton.Text = "Refresh";
            this.refreshTagsButton.UseVisualStyleBackColor = false;
            this.refreshTagsButton.Click += new System.EventHandler(this.refreshTagsButton_Click);
            // 
            // tagsListBox
            // 
            this.tagsListBox.CheckOnClick = true;
            this.tagsListBox.ContextMenuStrip = this.tagListBoxContextMenuStrip;
            this.tagsListBox.FormattingEnabled = true;
            this.tagsListBox.Location = new System.Drawing.Point(14, 17);
            this.tagsListBox.Name = "tagsListBox";
            this.tagsListBox.Size = new System.Drawing.Size(308, 109);
            this.tagsListBox.TabIndex = 0;
            // 
            // tagListBoxContextMenuStrip
            // 
            this.tagListBoxContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllToolStripMenuItem,
            this.checkNoneToolStripMenuItem});
            this.tagListBoxContextMenuStrip.Name = "tagListBoxContextMenuStrip";
            this.tagListBoxContextMenuStrip.Size = new System.Drawing.Size(153, 70);
            // 
            // checkAllToolStripMenuItem
            // 
            this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
            this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.checkAllToolStripMenuItem.Text = "Select All";
            this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click);
            // 
            // checkNoneToolStripMenuItem
            // 
            this.checkNoneToolStripMenuItem.Name = "checkNoneToolStripMenuItem";
            this.checkNoneToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.checkNoneToolStripMenuItem.Text = "Select None";
            this.checkNoneToolStripMenuItem.Click += new System.EventHandler(this.checkNoneToolStripMenuItem_Click);
            // 
            // PreferencesForm
            // 
            this.ClientSize = new System.Drawing.Size(456, 500);
            this.Controls.Add(this.tagsGroupBox);
            this.Controls.Add(this._noficationAudoFilePathGroupBox);
            this.Controls.Add(this._helpLink);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._label8);
            this.Controls.Add(this._groupBox2);
            this.Controls.Add(this._groupBox1);
            this.Controls.Add(this._label2);
            this.Controls.Add(this._timerMinutes);
            this.Controls.Add(this._label1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsResizable = false;
            this.Name = "PreferencesForm";
            this.Text = "Google Reader Notifier Preferences";
            this.Load += new System.EventHandler(this.PreferencesForm_Load);
            this._groupBox1.ResumeLayout(false);
            this._groupBox2.ResumeLayout(false);
            this._groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this._noficationAudoFilePathGroupBox.ResumeLayout(false);
            this._noficationAudoFilePathGroupBox.PerformLayout();
            this.tagsGroupBox.ResumeLayout(false);
            this.tagListBoxContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void PreferencesForm_Load(object sender, System.EventArgs e)
		{
            Cursor savedCursor = this.Cursor;

            reader = new GoogleReaderNotifier.ReaderAPI.GoogleReader();

            this.Cursor = Cursors.WaitCursor;
            try
            {
                this.SynchroniseTags();

			    this.LoadSettings();

                this.ValidateForm();
            }
            finally
            {
                this.Cursor = savedCursor;
            }
		}

		private void _okButton_Click(object sender, System.EventArgs e)
		{
            Cursor savedCursor = this.Cursor;

            this.Cursor = Cursors.WaitCursor;
            try
            {
			    if(this.ValidateForm())
			    {
				    this.SaveSettings();
				    this.DialogResult = DialogResult.OK;
				    this.Close();
			    }
            }
            finally
            {
                this.Cursor = savedCursor;
            }
		}

		private void _cancelButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void HandleHelpLinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.braindotty.com/google-reader-notifier/help/?from=client");
		}

        private void SynchroniseTags()
        {
            List<string> tags = new List<string>();
            List<string> currentCheckedTags = new List<string>();

            Cursor savedCursor = this.Cursor;

            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (reader.LoggedIn)
                {
                    reader.CollectTags(tags);

                    for (int i = 0; i < tagsListBox.CheckedItems.Count; i++)
                    {
                        currentCheckedTags.Add(tagsListBox.CheckedItems[i].ToString());
                    }

                    tagsListBox.Items.Clear();

                    foreach (string tag in tags)
                    {
                        tagsListBox.Items.Add(tag, currentCheckedTags.Contains(tag));
                    }
                }
            }
            finally
            {
                this.Cursor = savedCursor;
            }
        }

		private void LoadSettings()
		{
			UserPreferences prefs = PreferencesHelper.RetrievePreferences();

			_timerMinutes.SelectedIndex = _timerMinutes.Items.IndexOf(prefs.TimerMinutes.ToString());
			_showCountTooltip.Checked = prefs.ShowCountTooltip;
			_animatePopup.Checked = prefs.AnimatePopup;
			_startWithWindows.Checked = prefs.StartAtWindowsStartup;
			_userName.Text = prefs.Username;
			_password.Text = prefs.Password;
            _notificationAudioFilePath.Text = prefs.NotificationAudioFilePath;

            foreach (string tag in prefs.FilterTags)
            {
                tagsListBox.Items.Add(tag, true);
            }
		}

		private void SaveSettings()
		{
			UserPreferences prefs = new UserPreferences();
			
			prefs.TimerMinutes = int.Parse(_timerMinutes.SelectedItem.ToString());
			prefs.ShowCountTooltip = _showCountTooltip.Checked;
			prefs.AnimatePopup = _animatePopup.Checked;
			prefs.StartAtWindowsStartup = _startWithWindows.Checked;
			prefs.Username = _userName.Text;
			prefs.Password = _password.Text;
            prefs.NotificationAudioFilePath = _notificationAudioFilePath.Text;

            if (prefs.FilterTags == null)
            {
                prefs.FilterTags = new List<string>();
            }

            prefs.FilterTags.Clear();

            for (int i = 0; i < tagsListBox.CheckedItems.Count; i++)
            {
                prefs.FilterTags.Add(tagsListBox.CheckedItems[i].ToString());
            }

			PreferencesHelper.SavePreferences(prefs);
		}

		private bool ValidateForm()
		{
			bool isValid = true;

			string userError = null;
			if(_userName.Text.Trim().Length == 0)
			{
				userError = "Required!";
				isValid = false;
			}
			_errorProvider.SetError(_userName, userError);

			string passwordError = null;
			if(_password.Text.Trim().Length == 0)
			{
				passwordError = "Required!";
				isValid = false;
			}
			_errorProvider.SetError(_password, passwordError);

            string notificationAudioFilePathError = "";
            if ((_notificationAudioFilePath.Text != string.Empty) && !System.IO.File.Exists(_notificationAudioFilePath.Text))
            {
                notificationAudioFilePathError = "File does not exist!";
                isValid = false;
            }
            _errorProvider.SetError(_notificationAudioFilePath, notificationAudioFilePathError);

			if(isValid)
			{
				isValid = this.VerifyLogin();
			}

			return isValid;
		}

		private bool VerifyLogin()
		{
			string errorMessage = "";
            bool result = reader.Login(_userName.Text, _password.Text, errorMessage);

            if (!result)
            {
                _errorProvider.SetError(_userName, "Login to Google Failed!");
            }
            
            return result;
		}

        private void _selectNotificationAudioFilePathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Multiselect = false;
            dialog.Filter = "WAV files (*.wav)|*.wav";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _notificationAudioFilePath.Text = dialog.FileName;
            }
        }

        private void refreshTagsButton_Click(object sender, EventArgs e)
        {
            SynchroniseTags();
        }

        private void checkNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tagsListBox.Items.Count; i++)
            {
                tagsListBox.SetItemChecked(i, false);
            }
        }

        private void checkAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tagsListBox.Items.Count; i++)
            {
                tagsListBox.SetItemChecked(i, true);
            }
        }
	}
}
