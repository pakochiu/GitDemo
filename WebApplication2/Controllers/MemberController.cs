using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member

        public ActionResult Registeration()
        {
            return View();
        }

        public ActionResult Forgot()
        {
            return View();
        }

        dbMSIT126TeamEntities db = new dbMSIT126TeamEntities();
        [HttpPost]
        public string Forgot(AspNetUser c)
        {
            var q = db.AspNetUsers.Any(n => n.UserName == c.UserName);
            if (q)
                return "電郵已發送至你的信箱";
            else
                return "無此帳號";
        }

        [HttpPost]
        public ActionResult Registeration(tCustomer p)
        {
            try
            {
                if (p != null)
                {
                    dbMSIT126TeamEntities db = new dbMSIT126TeamEntities();
                    p.fUserName = Session[CDictionary.SK_Logon_UserName].ToString();                    
                    db.tCustomers.Add(p);
                    db.SaveChanges();
                    var q = (from c in db.tCustomers
                             where c.fUserName == p.fUserName
                             select c.fCustomerID).FirstOrDefault();
                    Session[CDictionary.SK_Logon_ID] = q.ToString();
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch
            {
                p.fUserName = User.Identity.Name;
                db.tCustomers.Add(p);
                db.SaveChanges();
                var q = (from c in db.tCustomers
                         where c.fUserName == p.fUserName
                         select c.fCustomerID).FirstOrDefault();
                Session[CDictionary.SK_Logon_ID] = q.ToString();
                return RedirectToAction("Index", "Manage");
            }

        }

        public ActionResult Newsupdate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Newsupdate(tNew news, string CustomerName)
        {
            try
            {
                var q = db.tCustomers.Where(p => p.fCompanyName == CustomerName).Select(p => p.fCustomerID).FirstOrDefault();
                news.fCustomerID = q;
                db.tNews.Add(news);
                db.SaveChanges();
                return View();
            }
            catch
            {
                return View();
            }
        }


        public string CustomerList()
        {
            var q = (db.tCustomers.Select(p => p.fCompanyName)).ToList();
            var json = JsonConvert.SerializeObject(q);
            return json;
        }
    }


}