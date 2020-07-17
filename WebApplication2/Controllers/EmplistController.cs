using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class EmplistController : Controller
    {
        //dbMSIT126Team02Entities_Accounting db = new dbMSIT126Team02Entities_Accounting();
        // GET: Emplist
        public ActionResult index()
        {
            
            return View();
        }
        //員工清單檢視用
        public ActionResult showEmp() 
        {
            
            var table = from e in (new dbMSIT126TeamEntities()).tEmployees
                        select e;
            return View(table);
        }

        ////---------------Pako紅線註解-----------------------------------------------
        //public ActionResult New()
        //{
        //    List<tDepartment> departments = db.tDepartments.ToList();
        //    ViewBag.Deplist = new SelectList(departments, "fDepartmentID", "fDepartmentName");
        //    List<tJobTitle> jobtitle = db.tJobTitles.ToList();
        //    ViewBag.joblist = new SelectList(jobtitle, "fJobTitleID", "fTitleName");
        //    //SelectList Department = new SelectList(this.getdepartment(), "fDepartmentID", "fDepartmentName");
        //    //ViewBag.DepartList = Department;
        //    //SelectList JobTitle = new SelectList(this.getJobtitle(), "fJobTitleID", "fTitleName");
        //    //ViewBag.JobList = JobTitle;
        //    return View();
        //}
        ////---------------Pako紅線註解-----------------------------------------------

        //private IEnumerable<tJobTitle> getJobtitle()
        //{
        //    dbMSIT126Team02Entities1 db = new dbMSIT126Team02Entities1();

        //   var jobtitle = db.tJobTitle.OrderBy(y => y.fJobTitleID);
        //        return jobtitle.ToList();

        //    //throw new NotImplementedException();
        //}

        //private IEnumerable<tDepartment> getdepartment()
        //{
        //   dbMSIT126Team02Entities1 db = new dbMSIT126Team02Entities1();

        //    var department = db.tDepartment.OrderBy(x => x.fDepartmentID);
        //        return department.ToList();

        //    throw new NotImplementedException();
        //}

        ////---------------Pako紅線註解-----------------------------------------------
        //[HttpPost]
        //public ActionResult New(tEmployee e ,string fpassword)//員工建立
        //{
        //    string pwstring = fpassword;
        //    byte[] bytesPssword = Encoding.Unicode.GetBytes(pwstring) ;
        //    SHA256Managed Algoritm = new SHA256Managed();
        //    var pw = Algoritm.ComputeHash(bytesPssword);
        //    e.fDepartmentID = 0;
        //    e.fJobTitleID = 0;
        //    e.fLoginPassword = pw;
        //    e.fUpdateDate = DateTime.Now.ToLocalTime();
        //    db.tEmployees.Add(e);
        //    db.SaveChanges();
        //    return RedirectToAction("showEmp");
        //}
        ////---------------Pako紅線註解-----------------------------------------------

    }
}