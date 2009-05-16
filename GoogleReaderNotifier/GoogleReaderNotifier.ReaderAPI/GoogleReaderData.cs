using System;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace GoogleReaderNotifier.ReaderAPI.Data
{
    /// <summary>
    /// UnreadItemCollection contains UnreadItem objectsiders unread, as defined
    /// by the data returned from the https://www.google.com/reader/api/0/unread-count?all=true URL
    /// </summary>
    public class UnreadItemCollection : List<UnreadItem>
    {
        #region Public Methods

        public int TotalUnreadItemArticles()
        {
            int result = 0;

            foreach (UnreadItem item in this)
            {
                result += item.ArticleCount;
            }

            return result;
        }

        public void SortByIdentifier()
        {
            Sort(delegate(UnreadItem item1, UnreadItem item2)
                { return item1.Identifier.CompareTo(item2.Identifier); });
        }

        public UnreadItem UnreadItemByIdentifier(string identifier)
        {
            // To Do : .Find is iterative, and thus could eventually become a performance issue
            return Find(delegate(UnreadItem item)
                { return item.Identifier == identifier; });
        }

        public bool ExistsByIdentifier(string identifier)
        {
            UnreadItem item = UnreadItemByIdentifier(identifier);

            return item != null;
        }

        #endregion
    }

    
    /// <summary>
	/// UnreadItem represents an element returned from the google reader api call
    /// https://www.google.com/reader/api/0/unread-count?all=true. An unread item is
    /// either a Feed or Label(tag) that has a name and a count.
	/// </summary>
	public class UnreadItem
	{
		#region Private variables

		private string _name = "";
        private int _arcticleCount = 0;
	
		#endregion 
		
        #region Properties

        public string Identifier
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public int ArticleCount
		{
			get{return _arcticleCount;}
			set{_arcticleCount = value;}
        }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            System.Diagnostics.Debug.Assert(obj is UnreadItem, "obj must be of type UnreadItem");

            bool result;

            try
            {
                result = obj != null;

                if (result)
                {
                    result = (Identifier == ((UnreadItem)obj).Identifier) && (ArticleCount == ((UnreadItem)obj).ArticleCount);
                }
            }
            catch
            {
                // Equals() should not raise exceptions, so return false instead
                result = false;
            }

            return result;
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode() ^ ArticleCount.GetHashCode();
        }

        #endregion
    }


    /// <summary>
    /// UnreadItemIdentifierComparer can be used to sort/search a list of UnreadItem by UnreadItem.Identifier.
    /// </summary>
    public class UnreadItemIdentifierComparer : IComparer<UnreadItem>
    {
        public int Compare(UnreadItem x, UnreadItem y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the identifiers
                    //
                    return x.Identifier.CompareTo(y.Identifier);                    
                }
            }
        }
    }
}
