using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Resources;
using System.Windows.Forms;
using System.Collections.Generic;
using Snarl;
using GoogleReaderNotifier.ReaderAPI.Data;
using GoogleReaderNotifier.ReaderAPI;

namespace GoogleReaderNotifier.WinUI
{
	/// <summary>
	/// Summary description for MainForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
        private const int DATETIME_MINUTE_ONE = 60000;

		private System.Windows.Forms.NotifyIcon _notifyIcon;
        private System.Windows.Forms.ContextMenu _contextMenu;
		private System.Windows.Forms.MenuItem _goToReaderMenuItem;
		private System.Windows.Forms.MenuItem _checkNowMenuItem;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem _preferencesMenuItem;
		private System.Windows.Forms.MenuItem _exitMenuItem;
		private System.ComponentModel.IContainer components;

		private bool _showCountTooltip = false;
		private List<string> _tagFilter = null;
		private bool _animatePopup = true;
		private string _browserPath = string.Empty;
		private string _username = string.Empty;
		private string _password = string.Empty;
        private string _unreadIcon = string.Empty;

		private UnreadItemCollection _currentUnreadItems = null;
		private TaskbarNotifier _trayNotifier;
        private System.Timers.Timer _checkForUpdatesTimer;

        private static IntPtr snarlMsgWindowHandle = IntPtr.Zero;
        private int SNARL_GLOBAL_MESSAGE;
        private string iconPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\App.ico";
		

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            if (snarlMsgWindowHandle == IntPtr.Zero)
            {
                snarlMsgWindowHandle = this.Handle;
            }
            this.SNARL_GLOBAL_MESSAGE = Snarl.SnarlConnector.GetGlobalMsg();


            SnarlConnector.RegisterConfig(snarlMsgWindowHandle, "GRaiN", Snarl.WindowsMessage.WM_USER + 16, iconPath);
            SnarlConnector.RegisterAlert("GRaiN", "New items");


			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			InitializeTrayNotifier();
            _checkForUpdatesTimer = new System.Timers.Timer();
            _checkForUpdatesTimer.Elapsed += new System.Timers.ElapsedEventHandler(_checkForUpdatesTimer_Elapsed);
            // SynchronizingObject is required so that timer event occurs within the main forms thread. If
            // it isn't done in this thread, the popup doesn't display properly unless the display event
            // was manually triggered from the menu option, which causes it to fire in the forms thread.
            _checkForUpdatesTimer.SynchronizingObject = this;
		}

         ~MainForm()
        {
            SnarlConnector.RevokeConfig(snarlMsgWindowHandle);
        }

		#region Dispose()

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this._contextMenu = new System.Windows.Forms.ContextMenu();
            this._goToReaderMenuItem = new System.Windows.Forms.MenuItem();
            this._checkNowMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this._preferencesMenuItem = new System.Windows.Forms.MenuItem();
            this._exitMenuItem = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // _notifyIcon
            // 
            this._notifyIcon.ContextMenu = this._contextMenu;
            this._notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("_notifyIcon.Icon")));
            this._notifyIcon.Text = "GRaiN - Google Reader Notifier";
            this._notifyIcon.Visible = true;
            this._notifyIcon.DoubleClick += new System.EventHandler(this.HandleNotifyIconDoubleClicked);
            // 
            // _contextMenu
            // 
            this._contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._goToReaderMenuItem,
            this._checkNowMenuItem,
            this.menuItem3,
            this._preferencesMenuItem,
            this._exitMenuItem});
            // 
            // _goToReaderMenuItem
            // 
            this._goToReaderMenuItem.DefaultItem = true;
            this._goToReaderMenuItem.Index = 0;
            this._goToReaderMenuItem.Text = "Go to Reader...";
            this._goToReaderMenuItem.Click += new System.EventHandler(this.HandleGoToReaderMenuItemClicked);
            // 
            // _checkNowMenuItem
            // 
            this._checkNowMenuItem.Index = 1;
            this._checkNowMenuItem.Text = "Check Now";
            this._checkNowMenuItem.Click += new System.EventHandler(this.HandleCheckNowMenuItemClicked);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "-";
            // 
            // _preferencesMenuItem
            // 
            this._preferencesMenuItem.Index = 3;
            this._preferencesMenuItem.Text = "Preferences...";
            this._preferencesMenuItem.Click += new System.EventHandler(this.HandlePreferencesMenuItemClicked);
            // 
            // _exitMenuItem
            // 
            this._exitMenuItem.Index = 4;
            this._exitMenuItem.Text = "Exit";
            this._exitMenuItem.Click += new System.EventHandler(this.HandleExitMenuItemClicked);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(248, 120);
            this.ControlBox = false;
            this.Enabled = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

		}
		#endregion

		#region Menu Click Handlers

		private void HandleGoToReaderMenuItemClicked(object sender, System.EventArgs e)
		{
			this.GoToReader();
		}

		private void HandleCheckNowMenuItemClicked(object sender, System.EventArgs e)
		{
			this.CheckForUpdates();
		}

		private void HandlePreferencesMenuItemClicked(object sender, System.EventArgs e)
		{
			this.DisplayPreferences();
		}

		private void HandleExitMenuItemClicked(object sender, System.EventArgs e)
		{
			this.ExitApplication();
		}

		#endregion

 		private void MainForm_Load(object sender, System.EventArgs e)
		{
            // set form visibility to false so that application doesn't show when using Alt+Tab
            this.Visible = false;

            if (RequiredFilesExist())
            {
                this.LoadSettings();
                this.CheckForUpdates();
            }
            else
            {
                Close();
            }
		}

        private bool RequiredFilesExist()
        {
            bool result;
            string executablePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + System.IO.Path.DirectorySeparatorChar;
            string configFile = executablePath + "GoogleReaderNotifier.exe.config";
            string googleReaderAPIFile = executablePath + "GoogleReaderNotifier.ReaderAPI.dll";

            result = System.IO.File.Exists(configFile);

            if (!result)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("The required file {0} is missing. Your installation may have become corrupted.", configFile));
            }
            else
            {
                result = System.IO.File.Exists(googleReaderAPIFile);

                if (!result)
                {
                    System.Windows.Forms.MessageBox.Show(String.Format("The required file {0} is missing. Your installation may have become corrupted.", googleReaderAPIFile));
                }
            }

            return result;
        }

		#region TrayNotification 

        private void InitializeTrayNotifier()
        {
            _trayNotifier = new TaskbarNotifier();
            _trayNotifier.SetBackgroundBitmap(new Bitmap(GetType(), "Images.skin.bmp"), Color.FromArgb(255, 0, 255));
            _trayNotifier.SetCloseBitmap(new Bitmap(GetType(), "Images.close.bmp"), Color.FromArgb(255, 0, 255), new Point(287, 7));
            _trayNotifier.TitleRectangle = new Rectangle(40, 9, 70, 25);
            _trayNotifier.ContentRectangle = new Rectangle(70, 15, 200, 20);
            _trayNotifier.NormalContentFont = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
            _trayNotifier.HoverContentFont = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
            _trayNotifier.HoverContentColor = Color.FromArgb(123, 150, 198);
            _trayNotifier.KeepVisibleOnMousOver = true;
            _trayNotifier.TopLevel = true;

            _trayNotifier.ContentClick += new EventHandler(HandleTrayNotifierContentClicked);
        }

		private void HandleTrayNotifierContentClicked(object sender, System.EventArgs e)
		{
			if (_browserPath == "")
				System.Diagnostics.Process.Start(ConfigurationSettings.AppSettings["GoogleReaderUrl"]);
			else
				System.Diagnostics.Process.Start(_browserPath, ConfigurationSettings.AppSettings["GoogleReaderUrl"]);
			ResetTrayIcon();
		}

		private void HandleNotifyIconDoubleClicked(object sender, System.EventArgs e)
		{
			this.GoToReader();
		}

		private void ShowTrayNotification(string displayText)
		{
			_trayNotifier.Show("", displayText, 300, 5000, 500);
		}

		private void ResetTrayIcon()
		{
			ResourceManager resources = new ResourceManager(typeof(MainForm));
			_notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("_notifyIcon.Icon")));
		}

		#endregion

		#region Private methods

		private void CheckForUpdates()
		{
            bool collectionResult;
            UnreadItemCollection unreadItems = new UnreadItemCollection();

            try
            {
                // if there are no errors, this will be restarted.
                //_checkForUpdatesTimer.Stop();//but what if not? it should never stop.

				//verify the user has entered credentials
                if (this.VerifyLoginEntered())
                {
                    //call the Google API
                    GoogleReader reader = new GoogleReader();
                    
                    reader.Login(_username, _password);
                    
                    if (HasFilterTags())
                    {
                        collectionResult = reader.CollectUnreadTags(unreadItems, _tagFilter);
                    }
                    else
                    {
                        collectionResult = reader.CollectUnreadFeeds(unreadItems);
                    }

                    if (collectionResult)
                    {
                        this.DisplayUnreadItems(unreadItems);

                        _checkForUpdatesTimer.Start();
                    }
                }
            }
            catch{}
		}

		private bool VerifyLoginEntered()
		{
			DialogResult result = DialogResult.None;

			//if not user/password, prompt user
			if ((_username.Trim().Length == 0) || (_password.Trim().Length == 0))
			{
				result = this.DisplayPreferences();

				return (result == DialogResult.OK);
			}
			else
			{
				return true;
			}
			
		}
                
        private void DisplayUnreadItems(UnreadItemCollection unreadItems)
        {
            int newUnreadCount;
            int oldUnreadCount = 0;
            int newSinceLastUpdateCount = 0;
            string displayInformation = "";
            string tooltipInformation = "";
            bool showNotification;
            
            if (unreadItems.Count > 0)
            {
                newUnreadCount = unreadItems.TotalUnreadItemArticles();                

                if (_currentUnreadItems != null)
                {
                    oldUnreadCount = _currentUnreadItems.TotalUnreadItemArticles();
                    newSinceLastUpdateCount = CountNewSinceLastUpdate(unreadItems);
                }

                // if the there were previously no unread items, or if the overall unread items 
                // has increased, or if there are new ones since the last check, then notify the user.
                showNotification = (_currentUnreadItems == null) || 
                    (newUnreadCount > oldUnreadCount) || (newSinceLastUpdateCount > 0);

                tooltipInformation = String.Format("{0} unread items", newUnreadCount);
                displayInformation = tooltipInformation;

                if (newSinceLastUpdateCount > 0)
                {
                    displayInformation = String.Format("{0} unread items ({1} new)", newUnreadCount, newSinceLastUpdateCount);
                }

                // if the option is enabled, change the tooltip text
                if (_showCountTooltip)
                {
                    if (tooltipInformation.Length < 64)
                    {
                        _notifyIcon.Text = tooltipInformation;
                    }
                }

                // if we need to notify user of changes and the animation is turned on, show the animation code
                if (showNotification)
                {
                    // play notification sound if configured
                    UserPreferences prefs = PreferencesHelper.RetrievePreferences();

                    if (prefs.HasNotificationAudioFilePath)
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

                        player.SoundLocation = prefs.NotificationAudioFilePath;
						try
						{
							player.Play();
						}
						catch { }
                    }

                    if (_animatePopup)
                    {
                        if (SnarlConnector.GetSnarlWindow() != IntPtr.Zero)
                        {
                            Snarl.SnarlConnector.ShowMessageEx("New items", "New items", displayInformation, 15, iconPath, snarlMsgWindowHandle, Snarl.WindowsMessage.WM_USER + 17, "");
                        }
                        else
                        {
                            this.ShowTrayNotification(displayInformation);
                        }
                    }
                }

                _notifyIcon.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("GoogleReaderNotifier.WinUI.Images.unread"+_unreadIcon+".ico"));
                _currentUnreadItems = unreadItems;
            }
            else
            {
                _currentUnreadItems = null;
                ResetTrayIcon();
				_notifyIcon.Text = "0 unread items";
            }
        }

        private int CountNewSinceLastUpdate(UnreadItemCollection newUnreadItems)
        {
            int result = 0;
            UnreadItem oldUnreadItem;

            if (_currentUnreadItems == null)
            {
                result = newUnreadItems.TotalUnreadItemArticles();
            }
            else
            {
                foreach (UnreadItem newItem in newUnreadItems)
                {
                    oldUnreadItem = _currentUnreadItems.UnreadItemByIdentifier(newItem.Identifier);

                    if (oldUnreadItem == null)
                    {
                        result += newItem.ArticleCount;
                    }
                    else
                    {
                        if (newItem.ArticleCount > oldUnreadItem.ArticleCount)
                        {
                            result += newItem.ArticleCount - oldUnreadItem.ArticleCount;
                        }
                    }
                }
            }

            return result;
        }

        private bool HasFilterTags()
        {
            return (_tagFilter != null) && (_tagFilter.Count > 0);
        }

		private DialogResult DisplayPreferences()
		{
			PreferencesForm frm = new PreferencesForm();
			DialogResult result = frm.ShowDialog();

			if(result == DialogResult.OK)
			{
				LoadSettings();
				CheckForUpdates();
			}

			return result;
		}

		private void ExitApplication()
		{
			//reset the "start with windows" setting
			UserPreferences prefs = PreferencesHelper.RetrievePreferences();
			StartupHelper.StartWithWindows(prefs.StartAtWindowsStartup);

			_notifyIcon.Visible = false;
            SnarlConnector.RevokeConfig(snarlMsgWindowHandle);
			Application.Exit();
		}

        protected override void WndProc(ref Message m)
        {
            int newFeedItemsMessage = (int)Snarl.WindowsMessage.WM_USER;
            newFeedItemsMessage += 17;
            // we gonna take care here if Snarl is (re)started while GRaiN has been running already
            if (m.Msg == this.SNARL_GLOBAL_MESSAGE)
            {
                if ((int)m.WParam == Snarl.SnarlConnector.SNARL_LAUNCHED)
                {
                    Snarl.SnarlConnector.GetSnarlWindow(true);

                    SnarlConnector.RegisterConfig(snarlMsgWindowHandle, "GRaiN", Snarl.WindowsMessage.WM_USER + 58, iconPath);
                    SnarlConnector.RegisterAlert("GRaiN", "New items");

                }
            }

            // here we can react on clicks the user makes on SNarl popups
            if (m.Msg == newFeedItemsMessage)
            {
                if ((int)m.WParam == Snarl.SnarlConnector.SNARL_NOTIFICATION_CLICKED)
                {
                    // clicked with right mouse button
                    GoToReader();
                }

                if ((int)m.WParam == Snarl.SnarlConnector.SNARL_NOTIFICATION_ACK)
                {
                    // clicked with left mousebutton
                    GoToReader();
                }
            }
            base.WndProc(ref m);
        }

		private void GoToReader()
		{
			if (_browserPath == "")
				System.Diagnostics.Process.Start(ConfigurationSettings.AppSettings["GoogleReaderUrl"]);
			else
				System.Diagnostics.Process.Start(_browserPath, ConfigurationSettings.AppSettings["GoogleReaderUrl"]);
			ResetTrayIcon();
		}

		private void LoadSettings()
		{
			_checkForUpdatesTimer.Stop();

			UserPreferences prefs = PreferencesHelper.RetrievePreferences();
            _checkForUpdatesTimer.Interval = prefs.TimerMinutes * DATETIME_MINUTE_ONE;
			_showCountTooltip = prefs.ShowCountTooltip;
			_animatePopup = prefs.AnimatePopup;
			_tagFilter = prefs.FilterTags;
			_browserPath = prefs.BrowserPath;
			_username = prefs.Username;
			_password = prefs.Password;
            _unreadIcon = (prefs.UnreadIcon == 0) ? string.Empty : prefs.UnreadIcon.ToString();


			StartupHelper.StartWithWindows(prefs.StartAtWindowsStartup);

			if (!_showCountTooltip)
			{
                _notifyIcon.Text = ConfigurationSettings.AppSettings["NotifyIconTitle"];
			}
			
			if ((_username.Trim().Length == 0) || (_password.Trim().Length == 0))
			{
				this.DisplayPreferences();
			}

			_checkForUpdatesTimer.Start();
		}

        private void _checkForUpdatesTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CheckForUpdates();
        }

        #endregion
	}
}
