using System;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using GoogleReaderNotifier.ReaderAPI.Data;
using System.Reflection;

namespace GoogleReaderNotifier.ReaderAPI
{
	/// <summary>
	/// Summary description for GoogleReader.
	/// </summary>
	public class GoogleReader
	{
		#region Private variables

		private CookieCollection _Cookies = new CookieCollection();
		private CookieContainer _cookiesContainer = new CookieContainer();
		private bool _loggedIn = false;
		private string[] _loginAuth;

		#endregion

		#region Public variables

		public string LoginError = "";

		#endregion

		public bool LoggedIn
        {
            get { return _loggedIn; }
        }

		#region Application Code

        // https://www.google.com/reader/atom/user/-/state/com.google/reading-list // get full feed of items
        
		public string Login(string username, string password)
		{
			_loggedIn = false;
			LoginError = "";
            
			HttpWebRequest req = CreateRequest("https://www.google.com/accounts/ClientLogin");

			string exc = PostLoginForm(req, String.Format("accountType=GOOGLE&Email={0}&Passwd={1}&service=reader&source=GRaiN-Notifier-" + Assembly.GetExecutingAssembly().GetName().Version, Uri.EscapeDataString(username), Uri.EscapeDataString(password)));

			if (exc.IndexOf("System.Net.WebException") != -1) return "CONNECT_ERROR";

			try
			{
				string _helper = GetResponseString(req);
                _loggedIn = (_helper.IndexOf("error", StringComparison.OrdinalIgnoreCase) == -1) && (_helper.IndexOf("auth", StringComparison.OrdinalIgnoreCase) != -1);
				if (_loggedIn == true)
				{
					_loginAuth = _helper.Split('\n');
					foreach (string st in _loginAuth)
					{
						if (st != string.Empty)
						{
							string[] coo = st.Split('=');
							_cookiesContainer.Add(new Cookie(coo[0], coo[1], "/", ".google.com"));
						}
					}

				}
				else
				{
					LoginError += _helper + "\r\n";
				}
			}
			catch (Exception ex)
			{
				_loggedIn = false;
				string _exc = ex.ToString();
				LoginError += "Exception in GetResponseString. \r\n" + _exc + "\r\n";
			}

			if (!_loggedIn)
				return "AUTH_ERROR";
			else
				LoginError = string.Empty;
				return string.Empty;
        }

        private delegate string LocateUnreadItemIdentifierDelegate(string identifierSource);

        private string LocateFeedIdentifier(string identifierSource)
        {
            return identifierSource.Substring(identifierSource.LastIndexOf("/http") + 1);
        }

        private string LocateTagIdentifier(string identifierSource)
        {
            return identifierSource.Substring(identifierSource.LastIndexOf("/label/") + "/label/".Length);
        }

        public bool CollectUnreadFeeds(UnreadItemCollection unreadFeeds)
        {
            System.Diagnostics.Debug.Assert(unreadFeeds != null, "unreadFeeds must be assigned.");
            
            return CollectUnreadItems(unreadFeeds, null, null);
        }
		
        public bool CollectUnreadTags(UnreadItemCollection unreadTags, List<string> tagFilterList)
        {
            System.Diagnostics.Debug.Assert(unreadTags != null, "unreadTags must be assigned.");
            
            return CollectUnreadItems(null, unreadTags, tagFilterList);
        }

        public void CollectTags(List<String> tags)
        {
            System.Diagnostics.Debug.Assert(tags != null, "tags must be assigned.");

            if (!LoggedIn)
            {
                throw new Exception("Must be logged in to collect tags");
            }

            XmlDocument xdoc;
            string identifier;

            xdoc = this.GetTagListXMLDocument();

            tags.Clear();

            foreach (XmlNode node in xdoc.SelectNodes("//object/string[contains(.,'/label/') and contains(.,'user/')]"))
            {
                identifier = LocateTagIdentifier(node.InnerText);

                tags.Add(identifier);
            }
        }

        public bool CollectUnreadItems(UnreadItemCollection unreadFeeds, UnreadItemCollection unreadTags, List<string> identifierFilterList)
        {
            bool result = true;
            //int unreadCount;
            XmlDocument xdoc;

            result = LoggedIn;
            
            if (result)
            {
                xdoc = this.GetAllUnreadCountsXMLDocument();

                // Collect feed information
                if (unreadFeeds != null)
                {
                    unreadFeeds.Clear();

                    CollectUnreadItemsBySelectedNodes(xdoc, "//object/string[contains(.,'feed/http')]", unreadFeeds, LocateFeedIdentifier, null);
                }


                // Collect tag information
                if (unreadTags != null)
                {
                    unreadTags.Clear();

                    CollectUnreadItemsBySelectedNodes(xdoc, "//object/string[contains(.,'/label/') and contains(.,'user/')]", unreadTags, LocateTagIdentifier, identifierFilterList);
                }
            }

            return result;
        }

        private void CollectUnreadItemsBySelectedNodes(XmlDocument xdoc, string nodeSelection, UnreadItemCollection unreadItems, LocateUnreadItemIdentifierDelegate locateIdentifierDelegate, List<string> identifierFilterList)
        {
            int unreadCount;
            UnreadItem unreadItem;
            string identifier;
            bool addToList;

            foreach (XmlNode node in xdoc.SelectNodes(nodeSelection))
            {
                unreadCount = Convert.ToInt32(node.ParentNode.SelectSingleNode("number").InnerText);
                identifier = locateIdentifierDelegate(node.InnerText);

                addToList = (unreadCount > 0) && ((identifierFilterList == null) || identifierFilterList.Contains(identifier));
                
                if (addToList)
                {
                    unreadItem = new UnreadItem();

                    unreadItem.Identifier = identifier;
                    unreadItem.ArticleCount = unreadCount;

                    
                    unreadItems.Add(unreadItem);
                }
            }
        }        

        private XmlDocument GetAllUnreadCountsXMLDocument()
		{
			string url = "https://www.google.com/reader/api/0/unread-count?all=true";
            string theXml = GetResponseString(CreateRequest(url))/*.Replace("&nbsp;", "&#160;")*/;

			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(theXml);

			return xdoc;
		}

		private XmlDocument GetTagListXMLDocument()
		{
			string url = "https://www.google.com/reader/api/0/tag/list";
            string theXml = GetResponseString(CreateRequest(url))/*.Replace("&nbsp;", "&#160;")*/;

			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(theXml);

			return xdoc;
		}

		#endregion 

		#region HTTP Functions

		public string GetResponseStrEx(String url)
		{
			string xstr = GetResponseString(CreateRequest(url));

			return xstr;
		}

		private string GetResponseString(HttpWebRequest req)
		{
			string responseString = null;
			try
			{
				HttpWebResponse res = (HttpWebResponse)req.GetResponse();
				res.Cookies = req.CookieContainer.GetCookies(req.RequestUri);
				_Cookies.Add(res.Cookies);
				
				using (StreamReader read = new StreamReader(res.GetResponseStream()))
				{
					responseString = read.ReadToEnd();
				}
				res.Close();
				//_currentHTML = responseString;
				
			}
			catch (WebException ex)
			{
				using (StreamReader read = new StreamReader(ex.Response.GetResponseStream()))
				{
					responseString = read.ReadToEnd();
				}
				//res.Close();
				string exc = ex.ToString();
				LoginError += responseString+"\r\n" +exc + "\r\n";
			}
			catch (Exception ex)
			{
				string exc = ex.ToString();
				LoginError += exc + "\r\n";
			}
			return responseString;
		}

		private string PostLoginForm(HttpWebRequest req, string p)
		{
			string exc = string.Empty;
			req.ContentType = "application/x-www-form-urlencoded";
			req.Method = "POST";
			//req.Referer = _currentURL;
			byte[] b = Encoding.UTF8.GetBytes(p);
			req.ContentLength = b.Length;
			try
			{
				using (Stream s = req.GetRequestStream())
				{
					s.Write(b, 0, b.Length);
					s.Close();
				}
			}
			catch (Exception ex)
			{
				exc = ex.ToString();
			}
			return exc;
		}

		private HttpWebRequest CreateRequest(string url)
		{
			HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
			//WebProxy defaultProxy = WebProxy.GetDefaultProxy();
            IWebProxy defaultProxy = HttpWebRequest.DefaultWebProxy;
			
			req.Proxy = defaultProxy; // if we wanted to disable proxying, can be done by setting to null I think
			//req.UserAgent = _user.UserAgent;
			req.UserAgent = "GRaiN/" + Assembly.GetExecutingAssembly().GetName().Version + "(Google Reader Notifier for Windows)";
			req.CookieContainer = _cookiesContainer;
			//req.Referer = _currentURL; // set the referring url properly to appear as a regular browser
			//_currentURL = url; // set the current url so the next request will have the right referring url ( might not work for sub pages )
			return req;
		}

		#endregion 

	}
}
