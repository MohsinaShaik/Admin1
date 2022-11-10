using System.Web;
using System.Web.Mvc;

namespace WebApiCRUD_ADO.NET_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
