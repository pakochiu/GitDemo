using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.KeyHolder;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class CustomerManagementPageController : Controller
    {
        dbMSIT126TeamEntities db = new dbMSIT126TeamEntities();

        // GET: CustomerManagementPage
        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult testPartialNews()
        {
            try
            {
                var customer = User.Identity.GetUserName();
                var news = (from p in db.tNews
                            where p.tCustomer.fUserName == customer
                            select p).Take(5).ToList();
                return View(news);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ContactSales()
        {
            try
            {
                tCustomer contact = GetUserName();
                return View(contact);
            }

            catch
            {
                return View();
            }

        }

        private tCustomer GetUserName()
        {            
            var customer = User.Identity.GetUserName();
            var contact = db.tCustomers.Where(c => c.fUserName == customer).FirstOrDefault();
            return contact;
        }

        [HttpPost]
        public string SendMailToSales(string heading, string content)
        {
            string h = heading;
            string c = content;
            var customer = GetUserName();

            if (h != null && c != null)
            {

                tCase a = new tCase();
                a.fheading = h;
                a.fcontent = c;
                a.fCustomerID = customer.fCustomerID;
                db.tCases.Add(a);
                db.SaveChanges();
                return "<span style=\"font-weight: bold\">--------訊息已發送成功，請耐心等待業務員的回覆，謝謝!!-------- </span>";



            }
            else
            {
                return "<span style=\"font-weight: bold\">--------請檢查輸入是否正確，謝謝!!-------- </span>";
            }
                
        }


        public ActionResult progress()
        {
            try
            {
                var q = from p in db.tOrders
                        select p;
                return View(q);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Invoice()
        {
            return View();
        }

        public ActionResult charts()
        {
            return View();
        }

        public ActionResult OrderList()
        {
            try
            {
                var customer = User.Identity.GetUserName();
                if (customer != null)
                {
                    var q = (from p in db.tOrders
                             where p.tCustomer.fUserName == customer
                             select p).OrderBy(p => p.fOrderID).ToList();
                    return View(q);
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

     

        public ActionResult OrderDetailTable(int id)
        {
            try
            {
                var q = (from p in db.tOrderDetails
                         where p.fOrderID == id
                         select p).OrderBy(p => p.fOrderID).ToList();
                return View(q);
            }
            catch
            {
                return View();
            }
        }


        public ActionResult News()
        {
            try
            {
                var customer = User.Identity.GetUserName();
                var q = (from p in db.tNews
                         where p.tCustomer.fUserName == customer
                         select p).ToList();
                return View(q);
            }
            catch
            {
                return View();
            }
        }


        public string NewsUpdate()
        {
            try
            {
                var customer = User.Identity.GetUserName();
                var q = (from p in db.tNews
                         where p.tCustomer.fUserName == customer
                         select new { p.fData, p.fSubject, p.fContent }).ToList();

                string str = JsonConvert.SerializeObject(q);
                return str;
            }
            catch
            {
                string str1 = "";
                return str1;
            }
        }
    }
}