#region Includes

using System;
using System.Xml;
#endregion 

namespace GoogleReader
{
	/// <summary>
	/// XmlINI stores and loads configuration information from an xml file.
	/// </summary>
	public class XmlINI
	{
		#region Object Constructor

		public XmlINI()
		{
			if(!System.IO.File.Exists("GoogleReader.xml"))
			{
				XmlDocument xdoc = new XmlDocument();
				
				xdoc.InnerXml = "<Settings></Settings>";
				xdoc.Save("GoogleReader.xml");
			}
		}
		#endregion 

		#region Application Code

		public string GetKey(string key,string defaultValue)
		{
			// TODO: create functionality to automatically create INI file if it doesn't exist

			XmlDocument xdoc = new XmlDocument();
			xdoc.Load("GoogleReader.xml");
			XmlNode node = xdoc.SelectSingleNode("//"+key);
			if(node != null)
			{
				string result = node.InnerText;
				if(result != null && result.Length > 0)
				{
					return result;
				}
				else
				{
					return defaultValue;
				}
			}
			else
			{
				return defaultValue;
			}
		}
		public bool SaveKey(string key,string newvalue)
		{
			XmlDocument xdoc = new XmlDocument();
			xdoc.Load("GoogleReader.xml");
			XmlNode node = xdoc.SelectSingleNode("//"+key);
			if(node != null)
			{
				node.InnerText = newvalue;
				xdoc.Save("GoogleReader.xml");
				return true;
			}
			else
			{
				XmlElement newNode = xdoc.CreateElement(key);
				newNode.InnerText = newvalue;
				xdoc.DocumentElement.AppendChild(newNode);
				xdoc.Save("GoogleReader.xml");
				return true;
			}
			
		}
		#endregion 

	}
}
