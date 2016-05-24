using System.Web;
using System.Web.Mvc;
using Elmah.EF.AspNet.Mvc;

namespace Elmah.EF.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new DbEntityValidationExceptionHandleErrorAttribute());
        }
    }
}
