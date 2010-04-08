using System.Web.Mvc;

namespace Metsys.Validate.MvcSample.Controllers
{
    public class HomeController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Step1()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Step1(Step1 step1)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return Redirect("/Home/Step2");
        }

        public ViewResult Step2()
        {            
            return View();
        }        
    }
}