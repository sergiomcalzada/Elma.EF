using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Elmah.EF6;

namespace Elmah.EF.AspNet.WebApi
{
    public class DbEntityValidationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            this.HandleException(actionExecutedContext);
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            this.HandleException(actionExecutedContext);
            return Task.FromResult(0);
        }

        private void HandleException(HttpActionExecutedContext actionExecutedContext)
        {
            var ex = actionExecutedContext.Exception as DbEntityValidationException;
            if (ex != null) 
            {
              DbEntityValidationExceptionLogger.Log(ex);  
            }
            
        }
    }
}
