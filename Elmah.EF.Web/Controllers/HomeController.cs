using System;
using System.Web.Mvc;
using Elmah.EF6;

namespace Elmah.EF.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            this.ViewBag.Title = "Home Page";

            return this.View();
        }

        public ActionResult Error()
        {
            throw new NotImplementedException();
        }
        public ActionResult EntityValidationError()
        {
            using (var ctx = new ElmahContext())
            {
                ctx.ElmahErrors.Add(new ElmahError());
                ctx.SaveChanges();
            }
            return this.View();
        }
    }
}