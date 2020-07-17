using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class CheckInController : Controller
    {
        // GET: CheckIn
        public ActionResult EmpCheckInData()
        {
            try
            {
                dbMSIT126TeamEntities db = new dbMSIT126TeamEntities();
                var q = db.tWorkingTimes.Select(p => p);

                return View(q);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                dbMSIT126TeamEntities db = new dbMSIT126TeamEntities();
                var q = db.tWorkingTimes.Where(p => p.fWorkingID == id).FirstOrDefault();

                return View(q);
            }
            catch
            {
                return View();
            }

        }
    }
}