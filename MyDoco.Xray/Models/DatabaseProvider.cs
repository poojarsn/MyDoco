using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
namespace MyDoco
{
     public sealed class DatabaseProvider
    {    
        static DatabaseProvider()
        {
            MasterDatabase = Sitecore.Configuration.Factory.GetDatabase("master");
        }

       public static Sitecore.Data.Database MasterDatabase { get; set; }

    }
}