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
        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Step2()
        {            
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Step2(Step2 step2)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return Redirect("/Home/Step3");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Step3()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Step3(Step3 step3)
        {
            return View();            
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Step4()
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Step4(Step4 step4)
        {
            return View();
        }       
    }
}