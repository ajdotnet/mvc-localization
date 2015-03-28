using AJ.Common;
using System.Web.Mvc;

namespace MyStocks.Mvc
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Guard.AssertNotNull(filters, "filters");

            filters.Add(new HandleErrorAttribute());
        }
    }
}