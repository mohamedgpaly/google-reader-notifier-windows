#region Includes

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using CustomUIControls;
#endregion 

namespace GoogleReader
{
	/// <summary>
	/// GoogleReader Notifier system tray application
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		#region Variable Definitions

		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.Timer thetimer;
		private System.ComponentModel.IContainer components;
		private ReaderAPI _api = new ReaderAPI();
		private XmlINI _store;
		TaskbarNotifier taskbarNotifier1;
		private int MaxUnreadDisplay = 10;
		private bool ShowCountTooltip = true;
		private bool DoNotAnimate = false;
		private string Username = "";
		private string Password = "";
		private string FilterLabels = "";
		private int CurrentUnreadCount = 0;
		#endregion

		#region Startup

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			LoadSettings();
		}

		public frmMain()
		{
			InitializeComponent();
			
			this.WindowState = FormWindowState.Minimized;
			this.Hide(); // Make sure window is hidden
			addDefaultItemsToMenu();
			this.Hide();

			taskbarNotifier1=new TaskbarNotifier();
			taskbarNotifier1.SetBackgroundBitmap(new Bitmap(GetType(),"skin.bmp"),Color.FromArgb(255,0,255));
			taskbarNotifier1.SetCloseBitmap(new Bitmap(GetType(),"close.bmp"),Color.FromArgb(255,0,255),new Point(243,7));
			taskbarNotifier1.TitleRectangle=new Rectangle(40,9,70,25);
			taskbarNotifier1.ContentRectangle=new Rectangle(78,15,150,20);
			taskbarNotifier1.Click += new EventHandler(taskbarNotifier1_Click);
			taskbarNotifier1.NormalContentFont = new Font("Arial",12,FontStyle.Bold,GraphicsUnit.Pixel);
			taskbarNotifier1.HoverContentFont = new Font("Arial",12,FontStyle.Bold,GraphicsUnit.Pixel);
			taskbarNotifier1.HoverContentColor = Color.FromArgb(123,150,198);
			
//			taskbarNotifier1.TitleClick+=new EventHandler(TitleClick);
			taskbarNotifier1.ContentClick += new EventHandler(taskbarNotifier1_ContentClick);
			taskbarNotifier1.CloseClick += new EventHandler(taskbarNotifier1_CloseClick);
			taskbarNotifier1.KeepVisibleOnMousOver=true;
			taskbarNotifier1.TopLevel = true;
			_store = new XmlINI();

		}
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}
		#endregion
		
		#region ShutDown
		

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.thetimer = new System.Windows.Forms.Timer(this.components);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenu = this.contextMenu1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "Google Reader Notifier";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
			this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
			// 
			// contextMenu1
			// 
			this.contextMenu1.Popup += new System.EventHandler(this.contextMenu1_Popup);
			// 
			// thetimer
			// 
			this.thetimer.Enabled = true;
			this.thetimer.Interval = 60000;
			this.thetimer.Tick += new System.EventHandler(this.thetimer_Tick);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(70, 62);
			this.ControlBox = false;
			this.Enabled = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(-5000, -5000);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmMain";
			this.Opacity = 0;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Load += new System.EventHandler(this.frmMain_Load);

		}
		#endregion

		#region Application Code

		/// <summary>
		/// Adds the default items to menu.
		/// </summary>
		private void addDefaultItemsToMenu()
		{
			contextMenu1.MenuItems.Add("Go to Reader",new EventHandler(this.mnuGoToReader));
			contextMenu1.MenuItems.Add("Check Now",new EventHandler(this.mnuCheckNow));
			contextMenu1.MenuItems.Add("-");
			contextMenu1.MenuItems.Add("Preferences...",new EventHandler(this.mnuPreferences));
			contextMenu1.MenuItems.Add("Quit Google Reader          ",new EventHandler(this.mnuExitApp));
			this.Hide();
		}


		private void LoadSettings()
		{
			thetimer.Interval = Convert.ToInt32(_store.GetKey("TimerInterval","20"))*60000;
			thetimer.Enabled = true;
			thetimer.Stop(); thetimer.Start();
			MaxUnreadDisplay = Convert.ToInt32(_store.GetKey("MaxUnreadDisplay","10"));
			ShowCountTooltip = Convert.ToBoolean(_store.GetKey("ShowCountTooltip","True"));
			if(!ShowCountTooltip)
			{
				notifyIcon1.Text = "Google Reader Notifier";
			}
			DoNotAnimate = Convert.ToBoolean(_store.GetKey("DoNotAnimate","False"));
			FilterLabels = _store.GetKey("LabelFilter","");
			Username = _store.GetKey("Username","");
			Password = Encryption.Decrypt(_store.GetKey("Password",""));
		}

		/// <summary>
		/// Checks for updates.
		/// </summary>
		private void CheckForUpdates()
		{
			try
			{
				string result = _api.GetDetailedCount(Username,Password,FilterLabels);
			
				// if there was an error, disable the timer for now.
				if(result == "AUTH_ERROR")
				{
					thetimer.Enabled = false;
					return;
				}
				else
				{
					thetimer.Enabled = true;
					thetimer.Start();
				}
				if(_api.totalcount > 0)
				{
					result = _api.totalcount.ToString() + " unread items";

					// if the option is enabled, change the tooltip text
					if(ShowCountTooltip)
					{
						if(result.Length < 64)
						{
							notifyIcon1.Text = result;
						}
					}
					if(CurrentUnreadCount < _api.totalcount)
					{
						// if animation is turned on, show the animation code
						if(!DoNotAnimate)
						{
							taskbarNotifier1.Show("",result,300,5000,500); //Int32.Parse(textBoxDelayShowing.Text),Int32.Parse(textBoxDelayStaying.Text),Int32.Parse(textBoxDelayHiding.Text));
						}
					}
					notifyIcon1.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("GoogleReader.unread.ico"));
					CurrentUnreadCount = _api.totalcount;
				}
				else
				{
					CurrentUnreadCount = 0;
					ResetTrayIcon();
					notifyIcon1.Text = "Google Reader Notifier";
				}
			}
			catch{}

		}

		private void ResetTrayIcon()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
		}

		private void thetimer_Tick(object sender, System.EventArgs e)
		{
			CheckForUpdates();
		}
		private void taskbarNotifier1_Click(object sender, EventArgs e)
		{
			// not implemented - runs before the close click, so somewhat pointless
		}

		private void taskbarNotifier1_CloseClick(object sender, EventArgs e)
		{
			// do nothing
			//ResetTrayIcon();
		}

		private void taskbarNotifier1_ContentClick(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.google.com/reader/view/");
			ResetTrayIcon();
		}

		#endregion

		#region Menu Item Handlers
		/// <summary>
		/// Exits the application
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuExitApp(object sender, System.EventArgs e)
		{
			notifyIcon1.Visible = false;
			Application.Exit();
		}

		/// <summary>
		/// Go to Reader Homepage
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void mnuGoToReader(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.google.com/reader/view/");
			ResetTrayIcon();
		}
	
		/// <summary>
		/// Check For Updates
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void mnuCheckNow(object sender, System.EventArgs e)
		{
			CheckForUpdates();
		}



		/// <summary>
		/// Marks all items as read
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void mnuMarkRead(object sender, System.EventArgs e)
		{
			// not implemented. kinda pointless.			
		}

		/// <summary>
		/// Shows Preferences dialog box
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuPreferences(object sender, EventArgs e)
		{
			optionsForm frm = new optionsForm();
			if(frm.ShowDialog() == DialogResult.OK)
			{
				LoadSettings();
			}
			this.Hide();
		}
		#endregion

		private void notifyIcon1_Click(object sender, System.EventArgs e)
		{
			ResetTrayIcon();
		}

		private void contextMenu1_Popup(object sender, System.EventArgs e)
		{
			ResetTrayIcon();
		}

		private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.google.com/reader/view/");
			ResetTrayIcon();		
		}

	}
}
