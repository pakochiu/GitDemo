using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication2.Models;
using WebApplication2.ViewModel;

namespace WebApplication2.Controllers
{
    public class EmployeesController : Controller
    {
        dbMSIT126TeamEntities db = new dbMSIT126TeamEntities();
        // GET: Employees
        public ActionResult Employeeslist()  //觀察會員資料並審核
        {
            if (Session[EmpDitctionary.SK_Login_Deptment] == null)
            {
                return View("CantEnter");
            }
            var table = from e in db.tEmployees
                        where e.fJobTitleID == 0 || e.fDepartmentID ==0
                        select e;
            return View(table);
        }


        public ActionResult Create()//建立員工帳號
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tEmployee e) //建立員工帳號
        {
            e.fJobTitleID = 0;
            e.fDepartmentID = 0;
            e.fUpdateDate = DateTime.Now.ToLocalTime();
            db.tEmployees.Add(e);
            db.SaveChanges();
            return RedirectToAction("thanks");
        }
        public ActionResult Edit(int? id) //錄取員工資訊
        {
            if (Session[EmpDitctionary.SK_Login_Username] == null)
            {
                return View("CantEnter");
            }
            List<tDepartment> depart = db.tDepartments.ToList();
            ViewBag.Deplist = new SelectList(depart, "fDepartmentID", "fDepartmentName");
            List<tJobTitle> jobtitle = db.tJobTitles.ToList();
            ViewBag.joblist = new SelectList(jobtitle, "fJobTitleID", "fTitleName");
            //tEmployee employee = db.tEmployees.First(p => p.fEmployeeID == id);
            tEmployee employee = db.tEmployees.Find(id);
            if (employee == null)
            {
                return RedirectToAction("HRlist");
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(tEmployee e, string PassWord)//錄取員工資訊
        {

            tEmployee employee = db.tEmployees.First(p => p.fEmployeeID == e.fEmployeeID);
            if (employee == null)
            {
                return RedirectToAction("Employeeslist");
            }
            PasswordHasher pw = new PasswordHasher();
            string pwstring = PassWord;
            string bepw = pw.HashPassword(pwstring);
            employee.fLoginPassword = bepw;
            employee.fName = e.fName;
            employee.fUserName = e.fUserName;
            employee.fBirthday = e.fBirthday;
            employee.fEmail = e.fEmail;
            employee.fPhone = e.fPhone;
            employee.fAddress = e.fAddress;
            employee.fDepartmentID = e.fDepartmentID;
            employee.fJobTitleID = e.fJobTitleID;
            employee.fHireDate = e.fHireDate;
            employee.fUpdateDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Employeeslist");
        }
        public ActionResult Delete(int? id)//刪除員工資訊並不錄取
        {
            tEmployee employee = db.tEmployees.First(p => p.fEmployeeID == id);
            if (employee != null)
            {
                db.tEmployees.Remove(employee);
                db.SaveChanges();
                return RedirectToAction("Employeeslist");
            }
            return View("Employeeslist");

        }
        public ActionResult Details(int? id) //詳細資料
        {
            if (Session[EmpDitctionary.SK_Login_Username] == null)
            {
                return View("CantEnter");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tEmployee employee = db.tEmployees.Find(id);
            if (employee == null)
            {
                return RedirectToAction("Employeeslist");
            }
            return View(employee);
        }
        public ActionResult EmpProfile(int? id) //自身員工資訊
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tEmployee employee = db.tEmployees.Find(id);
            if (employee == null)
            {
                return RedirectToAction("FirstView");
            }
            return View(employee);
        }

        public ActionResult myEdit(int? id) //編輯自身資訊
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<tDepartment> depart = db.tDepartments.ToList();
            ViewBag.Deplist = new SelectList(depart, "fDepartmentID", "fDepartmentName");
            List<tJobTitle> jobtitle = db.tJobTitles.ToList();
            ViewBag.joblist = new SelectList(jobtitle, "fJobTitleID", "fTitleName");
            tEmployee employee = db.tEmployees.Find(id);
            if (employee == null)
            {
                return RedirectToAction("EmpProfile");
            }
            return View(employee);
        }
        [HttpPost]
        public ActionResult myEdit(tEmployee e, string PassWord) //編輯自身資訊
        {
            tEmployee employee = db.tEmployees.First(p => p.fEmployeeID == e.fEmployeeID);
            if (employee == null)
            {
                return RedirectToAction("FirstView");
            }
            if (PassWord != "")
            {
                PasswordHasher pw = new PasswordHasher();
                string pwstring = PassWord;
                string bepw = pw.HashPassword(pwstring);
                employee.fLoginPassword = bepw;
            }

            employee.fUpdateDate = DateTime.Now;
            employee.fEmail = e.fEmail;
            employee.fPhone = e.fPhone;
            employee.fAddress = e.fAddress;
            db.SaveChanges();
            return RedirectToAction("FirstView");
        }

        public ActionResult empLogin()//登入員工
        {
            return View();
        }
        [HttpPost]
        public ActionResult empLogin(EmpViewModel p, string password)//登入員工
        {
            PasswordHasher pw = new PasswordHasher();
            if (p.logAccount == "")
            {
                return RedirectToAction("empLogin");
            }
            tEmployee cust = db.tEmployees.First(e => e.fUserName == p.logAccount);

            var pwtrue = pw.VerifyHashedPassword(cust.fLoginPassword, password);
            if (pwtrue.ToString() == "Success")
            {
                Session[EmpDitctionary.SK_Login_Name] = cust.fName;
                Session[EmpDitctionary.SK_Login_Deptment] = cust.fDepartmentID;
                Session[EmpDitctionary.SK_Login_Jobtitel] = cust.fJobTitleID;
                Session[EmpDitctionary.SK_Login_Username] = cust.fUserName;
                Session[EmpDitctionary.SK_Login_ID] = cust.fEmployeeID;
                return RedirectToAction("FirstView");
            }
            return RedirectToAction("empLogin");
        }
        public ActionResult thanks()//感謝畫面
        {
            return View();
        }
        public ActionResult CantEnter()//你沒有權限
        {
            return View();
        }
        public ActionResult FirstView() //後臺首頁
        {
            return View();
        }
        public ActionResult EmpForgotPassword()//忘記密碼
        {

            return View();
        }
        [HttpPost]
        public ActionResult EmpForgotPassword(EmpChangePasswordViewModel e) //忘記密碼
        {
            if (!string.IsNullOrEmpty(e.oldAccount))
            {
                tEmployee cust = db.tEmployees.First(p => p.fUserName == e.oldAccount);
                if (e.OldPassword == "1234")
                {
                    PasswordHasher pw = new PasswordHasher();
                    string pwstring = e.NewPassword;
                    string bepw = pw.HashPassword(pwstring);
                    cust.fLoginPassword = bepw;
                    db.SaveChanges();

                    return RedirectToAction("EmpLogin");
                }
                else
                {
                    return RedirectToAction("EmpForgotPassword");
                }
            }
            else
            {

                return RedirectToAction("EmpForgotPassword") ;

            }
        }
        public ActionResult HRlist()//人事資料
        {
            if(Session[EmpDitctionary.SK_Login_Deptment].ToString()=="2" && Session[EmpDitctionary.SK_Login_Jobtitel].ToString() == "2" ||  Session[EmpDitctionary.SK_Login_Jobtitel].ToString()=="3")
            {
            var table = from e in db.tEmployees
                        where e.fJobTitleID != 0 || e.fDepartmentID !=0
                        select e;
            return View(table);
            }
            else if(Session[EmpDitctionary.SK_Login_Deptment].ToString()=="1")
            {
                var deptment = Session[EmpDitctionary.SK_Login_Deptment];
                var table = from e in db.tEmployees
                            where e.fDepartmentID == 1
                            select e;
                return View(table);
            }
            else if (Session[EmpDitctionary.SK_Login_Deptment].ToString() == "2")
            {
                var deptment = Session[EmpDitctionary.SK_Login_Deptment];
                var table = from e in db.tEmployees
                            where e.fDepartmentID == 2
                            select e;
                return View(table);
            }
            else if (Session[EmpDitctionary.SK_Login_Deptment].ToString() == "3")
            {
                var deptment = Session[EmpDitctionary.SK_Login_Deptment];
                var table = from e in db.tEmployees
                            where e.fDepartmentID == 3
                            select e;
                return View(table);
            }
            else if (Session[EmpDitctionary.SK_Login_Deptment].ToString() == "4")
            {
                var deptment = Session[EmpDitctionary.SK_Login_Deptment];
                var table = from e in db.tEmployees
                            where e.fDepartmentID == 4
                            select e;
                return View(table);
            }
            else if (Session[EmpDitctionary.SK_Login_Deptment].ToString() == "5")
            {
                var deptment = Session[EmpDitctionary.SK_Login_Deptment];
                var table = from e in db.tEmployees
                            where e.fDepartmentID == 5
                            select e;
                return View(table);
            }
            else if (Session[EmpDitctionary.SK_Login_Deptment].ToString() == "6")
            {
                var deptment = Session[EmpDitctionary.SK_Login_Deptment];
                var table = from e in db.tEmployees
                            where e.fDepartmentID == 6
                            select e;
                return View(table);
            }
            return View("CantEnter");
        }

        public ActionResult EmpEdit(int? id) //更改部門或職位
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<tDepartment> depart = db.tDepartments.ToList();
            ViewBag.Deplist = new SelectList(depart, "fDepartmentID", "fDepartmentName");
            List<tJobTitle> jobtitle = db.tJobTitles.ToList();
            ViewBag.joblist = new SelectList(jobtitle, "fJobTitleID", "fTitleName");
            tEmployee employee = db.tEmployees.Find(id);
            if (employee == null)
            {
                return RedirectToAction("EmpProfile");
            }
            return View(employee);
        }
        [HttpPost]
        public ActionResult EmpEdit(tEmployee e) //更改部門或職位
        {
            tEmployee employee = db.tEmployees.First(p => p.fEmployeeID == e.fEmployeeID);
            if (employee == null)
            {
                return RedirectToAction("HRlist");
            }

            employee.fUpdateDate = DateTime.Now;
            employee.fDepartmentID = e.fDepartmentID;
            employee.fJobTitleID = e.fJobTitleID;
            db.SaveChanges();
            return RedirectToAction("HRlist");
        }
        public ActionResult notinlist()//人事資料
        {
            if (Session[EmpDitctionary.SK_Login_Jobtitel].ToString() == "1" || Session[EmpDitctionary.SK_Login_Jobtitel].ToString() == "0")
            {
                return View("CantEnter");
            }
            else
            {
            var table = from e in db.tEmployees
                        where e.fJobTitleID == 0 || e.fDepartmentID == 0
                        select e;
            return View(table);
            }

        }
    }
}