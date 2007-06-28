using System;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using GoogleReaderNotifier.ReaderAPI.Data;

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
		
		#endregion 
		
		public bool LoggedIn
        {
            get { return _loggedIn; }
        }

		#region Application Code

        // https://www.google.com/reader/atom/user/-/state/com.google/reading-list // get full feed of items
        
		public bool Login(string username, string password, string errorMessage)
		{
            bool result;

            errorMessage = "";

			HttpWebRequest req = CreateRequest("https://www.google.com/accounts/ServiceLoginAuth");
			
            PostLoginForm(req, String.Format("Email={0}&Passwd={1}&service=reader&continue=https://www.google.com/reader&nui=1", username, password));
			
            result = GetResponseString(req).IndexOf("http://www.google.com/reader/atom/user/") != -1;
            
            _loggedIn = result;

            if(!result)
              errorMessage = "AUTH_ERROR";

            return result;
        }

        private delegate string LocateUnreadItemIdentifierDelegate(string identifierSource);

        private string LocateUnreadFeedIdentifier(string identifierSource)
        {
            return identifierSource.Substring(identifierSource.LastIndexOf("/http") + 1);
        }

        private string LocateUnreadTagIdentifier(string identifierSource)
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

                    CollectUnreadItemsBySelectedNodes(xdoc, "//object/string[contains(.,'feed/http')]", unreadFeeds, LocateUnreadFeedIdentifier, null);
                }


                // Collect tag information
                if (unreadTags != null)
                {
                    unreadTags.Clear();

                    CollectUnreadItemsBySelectedNodes(xdoc, "//object/string[contains(.,'/label/') and contains(.,'user/')]", unreadTags, LocateUnreadTagIdentifier, identifierFilterList);
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
			string theXml = GetResponseString(CreateRequest(url));

			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(theXml);

			return xdoc;
		}

        #endregion 

		#region HTTP Functions

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
			catch (Exception ex)
			{
				string exc = ex.ToString();
			}
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
			//WebProxy defaultProxy = WebProxy.GetDefaultProxy();
            IWebProxy defaultProxy = HttpWebRequest.DefaultWebProxy;
			
			req.Proxy = defaultProxy; // if we wanted to disable proxying, can be done by setting to null I think
			//req.UserAgent = _user.UserAgent;
			req.CookieContainer = _cookiesContainer;
			//req.Referer = _currentURL; // set the referring url properly to appear as a regular browser
			//_currentURL = url; // set the current url so the next request will have the right referring url ( might not work for sub pages )
			return req;
		}

		#endregion 

	}
}