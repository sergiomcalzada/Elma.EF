using System;
using System.Web.Mvc;

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
    }
}