using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace GoogleReaderNotifier.WinUI
{
	/// <summary>
	/// Summary description for UserPreferences.
	/// </summary>
	[XmlRoot("userPreferences")]
    public class UserPreferences
	{
		public UserPreferences()
		{
            _notificationAudioFilePath = "";
		}

        private int _timerMinutes;
		public int TimerMinutes
		{
			get{return (_timerMinutes == 0 ? 20 : _timerMinutes); }
			set{_timerMinutes = value;}
		}

		private string _filterLabels;
		public string FilterLabels
		{
			get{return (_filterLabels == null ? string.Empty : _filterLabels);}
			set{_filterLabels = value;}
		}

		private bool _showCountTooltip = true;
		public bool ShowCountTooltip
		{
			get{return _showCountTooltip;}
			set{_showCountTooltip = value;}
		}
		
		private bool _animatePopup = true;
		public bool AnimatePopup
		{
			get{return _animatePopup;}
			set{_animatePopup = value;}
		}

		private bool _startAtWindowsStartup = true;
		public bool StartAtWindowsStartup
		{
			get{return _startAtWindowsStartup;}	
			set{_startAtWindowsStartup = value;}
		}

		private string _username;
		public string Username
		{
			get{return (_username == null ? string.Empty : _username);}
			set{_username = value;}
		}

		private string _password;
		[XmlIgnore()]
		public string Password
		{
			get
			{
				return Encryption.Decrypt((_password == null ? string.Empty : _password) );
			}
			set
			{
				_password = Encryption.Encrypt(value);
			}
		}

		public string EncryptedPassword
		{
			get{return _password;}
			set{_password = value;}
		}

        [OptionalField(VersionAdded = 2)]
        private string _notificationAudioFilePath;        
        public string NotificationAudioFilePath
        {
            get { return _notificationAudioFilePath; }
            set { _notificationAudioFilePath = value; }
        }

        [XmlIgnore()]
        public bool HasNotificationAudioFilePath
        {
            get { return _notificationAudioFilePath != string.Empty; }
        }
	}
}
