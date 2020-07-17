using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class delController : Controller
    {
        // GET: del
        public void del()
        {
            dbMSIT126TeamEntities db = new dbMSIT126TeamEntities();
            var x = db.tCustomers.Where(p => p.fUserName == "iamcustomer@gmail.com").FirstOrDefault();
            var y = db.AspNetUsers.Where(p => p.UserName == "iamcustomer@gmail.com").FirstOrDefault();
            if (x != null)
            {
                db.tCustomers.Remove(x);
                db.SaveChanges();
            }
            if (y != null)
            {
                db.AspNetUsers.Remove(y);
                db.SaveChanges();
            }
        }
    }
}