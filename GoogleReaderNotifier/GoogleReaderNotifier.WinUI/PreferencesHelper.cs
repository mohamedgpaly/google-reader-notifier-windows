using System;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GoogleReaderNotifier.WinUI
{
	/// <summary>
	/// stores and loads configuration information from an xml file.
	/// </summary>
	public class PreferencesHelper
	{
		static PreferencesHelper()
		{
			//create the file if it does not already exist
			if(!System.IO.File.Exists(PreferencesFileName))
			{
				UserPreferences prefs = new UserPreferences();
				PreferencesHelper.SavePreferences(prefs);
			}
		}


		public static UserPreferences RetrievePreferences()
		{
			UserPreferences prefs;
			XmlSerializer xs = new XmlSerializer(typeof(UserPreferences));
			using (TextReader tr = new StreamReader( PreferencesFileName ))
			{
				prefs = (UserPreferences)xs.Deserialize(tr);
			}
			return prefs;
		}

		public static void SavePreferences(UserPreferences prefs)
		{
			XmlSerializer xs = new XmlSerializer(typeof(UserPreferences));
			using (TextWriter tw = new StreamWriter( PreferencesFileName ))
			{
				xs.Serialize(tw, prefs);
			}
		}
		

//		public static string GetKey(string key,string defaultValue)
//		{
//			XmlDocument xdoc = new XmlDocument();
//			xdoc.Load(PreferencesFileName);
//			XmlNode node = xdoc.SelectSingleNode("//"+key);
//			if(node != null)
//			{
//				string result = node.InnerText;
//				if(result != null && result.Length > 0)
//				{
//					return result;
//				}
//				else
//				{
//					return defaultValue;
//				}
//			}
//			else
//			{
//				return defaultValue;
//			}
//		}
//
//		public static string GetEncryptedKey(string key,string defaultValue)
//		{
//			string encryptedValue = GetKey(key, defaultValue);
//			return Encryption.Decrypt(encryptedValue);
//		}
//
//		public static bool SaveKey(string key,string newvalue)
//		{
//			XmlDocument xdoc = new XmlDocument();
//			xdoc.Load(PreferencesFileName);
//			XmlNode node = xdoc.SelectSingleNode("//"+key);
//			if(node != null)
//			{
//				node.InnerText = newvalue;
//				xdoc.Save(PreferencesFileName);
//				return true;
//			}
//			else
//			{
//				XmlElement newNode = xdoc.CreateElement(key);
//				newNode.InnerText = newvalue;
//				xdoc.DocumentElement.AppendChild(newNode);
//				xdoc.Save(PreferencesFileName);
//				return true;
//			}
//			
//		}

		private static string PreferencesFileName
		{
			get{ return ApplicationPath + ConfigurationManager.AppSettings["PreferencesFileName"];} 
		}

        private static string ApplicationPath
        {
            get { return Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + Path.DirectorySeparatorChar; }
        }

	}
}
