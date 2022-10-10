using System.Web;
using System.Web.Mvc;

namespace FIT5032AssignmentDemo2022S2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
