using System.Web;
using System.Web.Mvc;

namespace basic.ntier.architecture.auth
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}