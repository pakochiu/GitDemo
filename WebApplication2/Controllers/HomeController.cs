using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Product()
        {
            using (dbMSIT126TeamEntities db = new dbMSIT126TeamEntities())
            {
                var result = from p in db.tProducts
                             select p;

                return View(result.ToList());
            }
        }

    }
}