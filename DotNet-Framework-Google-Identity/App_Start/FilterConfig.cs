﻿using System.Web;
using System.Web.Mvc;

namespace DotNet_Framework_Google_Identity
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
