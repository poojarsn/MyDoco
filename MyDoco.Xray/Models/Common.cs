using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Sites;
using Sitecore.Web;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MyDoco
{

    public class Common {

		private static CategoryCollection collection;

		private Common() {
		}

		/// <summary>
		/// Wraper around local data cache to retrieve just root categories from the collection
		/// </summary>
		/// <returns>CategoryCollection containing just categories with a parentId of "root"</returns>
		public static CategoryCollection GetRootCategories() {

            CategoryCollection rootCategories = new CategoryCollection();
            var siteContext = GetSiteContext(HttpContext.Current.Request.Url);
		    Item item;

            if (siteContext != null)
		    {
                 item = DatabaseProvider.MasterDatabase.GetItem(siteContext.RootPath + siteContext.StartItem);
            }
		    else
		    {
                 item = DatabaseProvider.MasterDatabase.GetItem(Sitecore.Context.Site.ContentStartPath);
            }

            //var item = DatabaseProvider.MasterDatabase.GetItem(Sitecore.Context.Site.StartPath);
		   
            foreach (Category category in GetCategoryData()) {                
               
                if (category.ParentId == item.ID.ToString())
					rootCategories.Add(category);
			}
			return rootCategories;
		}
        public static SiteContext GetSiteContext(Uri requestUrl)
        {
            Assert.ArgumentNotNull(requestUrl, "requestUrl");
            string requestHostName = requestUrl.Host;
            foreach (SiteInfo siteInfo in Factory.GetSiteInfoList())
            {
                if (IsMatch(requestHostName, siteInfo.HostName) || IsMatch(requestHostName, siteInfo.TargetHostName))
                    return new SiteContext(siteInfo);
            }
            return SiteContext.GetSite("website");
        }

        private static bool IsMatch(string input, string wildcardPattern)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            if (string.IsNullOrWhiteSpace(wildcardPattern))
                return false;
            string regexPattern = WildcardToRegex(wildcardPattern);
            return Regex.IsMatch(input, regexPattern, RegexOptions.IgnoreCase);
        }

        private static string WildcardToRegex(string pattern)
        {
            return "^" + Regex.Escape(pattern).Replace("*", ".*").Replace("?", ".") + "$";
        }

        public static Item GetCurrentItem()
        {
            var siteContext = GetSiteContext(HttpContext.Current.Request.Url);
            Item item;
            if (siteContext != null)
            {
                item = DatabaseProvider.MasterDatabase.GetItem(siteContext.RootPath + siteContext.StartItem);
            }
            else
            {
                item = DatabaseProvider.MasterDatabase.GetItem(Sitecore.Context.Site.ContentStartPath);
            }
            return item;
        }

        public static string GetRootPath()
        {
            var siteContext = GetSiteContext(HttpContext.Current.Request.Url);
            string rootPath;
            if (siteContext != null)
            {
                rootPath = siteContext.RootPath + siteContext.StartItem;
            }
            else
            {
                rootPath = Sitecore.Context.Site.ContentStartPath;
            }
            return rootPath;
        }

        /// <summary>
        /// Method to generate sample data for examples
        /// </summary>
        /// <returns>CategoryCollection containing computer store related categories.</returns>
        public static CategoryCollection GetCategoryData() {

			// Simulate going to the database or pulling from a local cache
			if (collection == null) {

				collection = new CategoryCollection();
                var siteContext = GetSiteContext(HttpContext.Current.Request.Url);
                Item item;
                if (siteContext != null)
                {
                    item = DatabaseProvider.MasterDatabase.GetItem(siteContext.RootPath + siteContext.StartItem);
                }
                else
                {
                    item = DatabaseProvider.MasterDatabase.GetItem(Sitecore.Context.Site.ContentStartPath);
                }
                var childrens = item.GetChildren();
               
                var str = new StringBuilder();
                foreach (Item obj in childrens)
                {
                    collection.Add(new Category(obj.ID.ToString(), item.ID.ToString(),obj.Name));
                    foreach (Item objInner in obj.GetChildren())
                    {
                        collection.Add(new Category(objInner.ID.ToString(), obj.ID.ToString(), objInner.Name));
                    }
                }             
			}
			return collection;
		}
	}
}