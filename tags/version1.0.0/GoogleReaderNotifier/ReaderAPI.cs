#region Includes
using System;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
#endregion

namespace GoogleReader
{
	/// <summary>
	/// Summary description for ReaderAPI.
	/// </summary>
	public class ReaderAPI
	{
		#region Includes

		private CookieCollection _Cookies = new CookieCollection();
		private CookieContainer _cookiesContainer = new CookieContainer();
		private bool LoggedIn = false;
		public int totalcount = 0;
		public string tagcount = "";
		#endregion 

		#region Startup

		public ReaderAPI()
		{
	
		}
		#endregion 

		#region Application Code

		// https://www.google.com/reader/atom/user/-/state/com.google/reading-list // get full feed of items
		public string GetDetailedCount(string username, string password,string filters)
		{
			if(!LoggedIn)
			{
				HttpWebRequest req = CreateRequest("https://www.google.com/accounts/ServiceLoginAuth");
				PostLoginForm(req, String.Format("Email={0}&Passwd={1}&service=reader&continue=https://www.google.com/reader&nui=1", username, password));
				if(GetResponseString(req).IndexOf("http://www.google.com/reader/atom/user/") != -1)
				{
					LoggedIn = true;
				}
				else
				{
					return "AUTH_ERROR";
				}
			}

			string url = "https://www.google.com/reader/api/0/unread-count?all=true";
			string thexml = GetResponseString(CreateRequest(url));

			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(thexml);
			int thecount = 0;
			string detailedcount = "";

			// if filters are set, then don't check the regular unread items
			if(filters.Trim().Length == 0)
			{
				foreach(XmlNode node in xdoc.SelectNodes("//object/string[contains(.,'feed/http')]"))
				{
					int thenumber = Convert.ToInt32(node.ParentNode.SelectSingleNode("number").InnerText);
					thecount += thenumber;
				}
				this.totalcount = thecount;
			}
			else
			{
				// filters have been set, check here for just the tagged items.
				string[] filterlist = filters.Split(" ".ToCharArray());
				thecount = 0;
				foreach(XmlNode node in xdoc.SelectNodes("//object/string[contains(.,'/label/') and contains(.,'user/')]"))
				{
					string thelabel = node.InnerText.Substring(node.InnerText.LastIndexOf("/")+1);  //user/10477630455154158284/label/food

					// this section of code isn't so good, but I don't feel like refactoring yet.
					if(filters.Trim().Length > 0)
					{
						foreach(string thefilter in filterlist)
						{
							if(thefilter == thelabel)
							{
								int thenumber = Convert.ToInt32(node.ParentNode.SelectSingleNode("number").InnerText);
								if(thenumber > 0)
								{
									detailedcount += thenumber.ToString() + " in " + thelabel + Environment.NewLine;
									thecount += thenumber;
								}
							}
						}
					}
					else
					{
						int thenumber = Convert.ToInt32(node.ParentNode.SelectSingleNode("number").InnerText);
						if(thenumber > 0)
						{
							detailedcount += thenumber.ToString() + " in " + thelabel + Environment.NewLine;
							thecount += thenumber;
						}
					}
				}
				this.totalcount = thecount;
			}
			
			detailedcount = this.totalcount + " unread items" + Environment.NewLine + detailedcount;
			this.tagcount = detailedcount;

			return detailedcount;
		}

		#endregion 

		#region HTTP Functions

		private string GetResponseString(HttpWebRequest req)
		{
			HttpWebResponse res = (HttpWebResponse)req.GetResponse();
			res.Cookies = req.CookieContainer.GetCookies(req.RequestUri);
			_Cookies.Add(res.Cookies);
			string responseString = null;
			using (StreamReader read = new StreamReader(res.GetResponseStream()))
			{
				responseString = read.ReadToEnd();
			}
			res.Close();
			//_currentHTML = responseString;
			return responseString;
		}

		private void PostLoginForm(HttpWebRequest req, string p)
		{
			req.ContentType = "application/x-www-form-urlencoded";
			req.Method = "POST";
			//req.Referer = _currentURL;
			byte[] b = Encoding.UTF8.GetBytes(p);
			req.ContentLength = b.Length;
			using (Stream s = req.GetRequestStream())
			{
				s.Write(b, 0, b.Length);
				s.Close();
			}
		}

		private HttpWebRequest CreateRequest(string url)
		{
			HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
			WebProxy defaultProxy = WebProxy.GetDefaultProxy();
			
			req.Proxy = defaultProxy; 
			//req.UserAgent = _user.UserAgent;
			req.CookieContainer = _cookiesContainer;
			//req.Referer = _currentURL; // set the referring url properly to appear as a regular browser
			//_currentURL = url; // set the current url so the next request will have the right referring url ( might not work for sub pages )
			return req;
		}

		#endregion 

	}
}
