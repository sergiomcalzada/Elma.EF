using System.Data.Entity.Validation;
using System.Web.Mvc;
using Elmah.EF6;

namespace Elmah.EF.AspNet.Mvc
{
    public class DbEntityValidationExceptionHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            var ex = filterContext.Exception as DbEntityValidationException;
            if (ex != null)
            {
                DbEntityValidationExceptionLogger.Log(ex);
            }
        }
    }
}
