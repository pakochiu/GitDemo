using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace WebApplication2.Controllers
{
    public class AccountingController : Controller
    {
        //dbContext 變數
        dbMSIT126TeamEntities dbChecks = new dbMSIT126TeamEntities();

        //帳務清單
        public ActionResult allCheckBook(string search, string currentFilter, string sortOrder, int page = 1)
        {
            //存下排序/查詢訊息
            ViewBag.sortData = sortOrder;

            //欄位變數
            ViewBag.sortCustomerName = sortOrder == "_customerName" ? "_customerNameDesc" : "_customerName";
            ViewBag.sortOrderID = sortOrder == "_orderID" ? "_orderIDDesc" : "_orderID";
            ViewBag.sortCheckNumber = sortOrder == "_checkNum" ? "_checkNumDesc" : "_checkNum";
            ViewBag.sortCheckMoney = sortOrder == "_money" ? "_moneyDesc" : "_money";
            ViewBag.sortPaymentDay = sortOrder == "_payment" ? "_paymentDesc" : "_payment";
            ViewBag.sortTaxID = sortOrder == "_tax" ? "_taxDesc" : "_tax";
            ViewBag.sortTicktedDay = sortOrder == "_ticketed" ? "_ticketedDesc" : "_ticketed";
            ViewBag.sortPaymentStatus = sortOrder == "_payStatus" ? "_payStatusDesc" : "_payStatus";
            ViewBag.sortPayByCash = sortOrder == "_payWay" ? "_payWayDesc" : "_payWay";
            ViewBag.sortPaymentPeriod = sortOrder == "_payPeriod" ? "_payPeriodDesc" : "_payPeriod";

            //判斷是否有查詢條件
            if (search != null)
            {
                page = 1;
            }
            else if (search == null)
            {
                search = currentFilter;
            }
            ViewBag.allCheck = search;

            //Linq 查詢不在資源回收桶內的資料
            var allCheck = from q in dbChecks.tChecks
                           where q.fDeleteStatus == false
                           orderby q.fTicktedDay
                           select q;
            //查詢條件判斷
            if (!String.IsNullOrEmpty(search))
            {
                allCheck = from q in dbChecks.tChecks
                           where
                           q.tCustomer.fTaxID.Contains(search) ||
                           q.fCheckNumber.Contains(search) ||
                           q.tCustomer.fCompanyName.ToString().Contains(search) ||
                           q.fPaymentStatus.ToString().Contains(search) ||
                           q.fPaymentPeriod.ToString().Contains(search)
                           orderby q.fTicktedDay
                           select q;
            }

            //點選欄位以排序
            switch (sortOrder)
            {
                case "_customerName":
                    allCheck = allCheck.OrderBy(q => q.tCustomer.fCompanyName);
                    break;
                case "_customerNameDesc":
                    allCheck = allCheck.OrderByDescending(q => q.tCustomer.fCompanyName);
                    break;
                case "_orderID":
                    allCheck = allCheck.OrderBy(q => q.fOderID);
                    break;
                case "_orderIDDesc":
                    allCheck = allCheck.OrderByDescending(q => q.fOderID);
                    break;
                case "_checkNum":
                    allCheck = allCheck.OrderBy(q => q.fCheckNumber);
                    break;
                case "_checkNumDesc":
                    allCheck = allCheck.OrderByDescending(q => q.fCheckNumber);
                    break;
                case "_money":
                    allCheck = allCheck.OrderBy(q => q.fCheckMoney);
                    break;
                case "_moneyDesc":
                    allCheck = allCheck.OrderByDescending(q => q.fCheckMoney);
                    break;
                case "_payment":
                    allCheck = allCheck.OrderBy(q => q.fPaymentDay);
                    break;
                case "_paymentDesc":
                    allCheck = allCheck.OrderByDescending(q => q.fPaymentDay);
                    break;
                case "_tax":
                    allCheck = allCheck.OrderBy(q => q.tCustomer.fTaxID);
                    break;
                case "_taxDesc":
                    allCheck = allCheck.OrderByDescending(q => q.tCustomer.fTaxID);
                    break;
                case "_ticketed":
                    allCheck = allCheck.OrderBy(q => q.fTicktedDay);
                    break;
                case "_ticketedDesc":
                    allCheck = allCheck.OrderByDescending(q => q.fTicktedDay);
                    break;
                case "_payStatus":
                    allCheck = allCheck.OrderBy(q => q.fPaymentStatus);
                    break;
                case "_payStatusDesc":
                    allCheck = allCheck.OrderByDescending(q => q.fPaymentStatus);
                    break;
                case "_payWay":
                    allCheck = allCheck.OrderBy(q => q.fPayByCash);
                    break;
                case "_payWayDesc":
                    allCheck = allCheck.OrderByDescending(q => q.fPayByCash);
                    break;
                case "_payPeriod":
                    allCheck = allCheck.OrderBy(q => q.fPaymentPeriod);
                    break;
                case "_payPeriodDesc":
                    allCheck = allCheck.OrderByDescending(q => q.fPaymentPeriod);
                    break;
            }

            //加入分頁
            int pageSize = 15;
            int pageNum = page < 1 ? 1 : page;
            return View(allCheck.ToPagedList(pageNum, pageSize));
        }

        //帳務清單彈跳式視窗
        public JsonResult customer(int id)
        {
            var customer = from q in dbChecks.tCustomers
                           where q.fCustomerID == id
                           select new
                           {
                               q.fCompanyName,
                               q.fContactName,
                               q.fTelephone,
                               q.fPhone,
                               q.fCity,
                               q.fAddress,
                               q.fUserName
                           };

            return Json(customer);
        }

        //新增收款資料
        public ActionResult addCheck()
        {
            //Linq 查詢客戶名單
            var lstCustomer = from q in dbChecks.tCustomers select q;

            //將客戶名單做成集合，在前端以 DropDownList 呈現
            List<SelectListItem> itemsCN = new List<SelectListItem>();
            foreach (var companyName in lstCustomer)
            {
                itemsCN.Add(new SelectListItem()
                {
                    Text = companyName.fCompanyName,
                    Value = companyName.fCustomerID.ToString()
                });
            }
            ViewBag.lstCompanyName = itemsCN;

            //Linq 查詢訂單
            var lstOrder = from q in dbChecks.tOrders where q.fOrderConfirm == "0" select q;

            //將訂單做成集合，在前端以 DropDownList 呈現
            List<SelectListItem> itemsO = new List<SelectListItem>();
            foreach (var order in lstOrder)
            {
                itemsO.Add(new SelectListItem()
                {
                    Text = order.fOrderID.ToString(),
                    Value = order.fOrderID.ToString()
                });
            }
            ViewBag.lstOrders = itemsO;

            return View();
        }
        //新增收款資料POST
        [HttpPost]
        public ActionResult addCheck(tCheck tC, bool fPayByCash)
        {
            dbChecks.tChecks.Add(tC);
            dbChecks.SaveChanges();
            return RedirectToAction("allCheckBook");
        }

        //編輯收款資料
        public ActionResult editCheck(int id)
        {
            tCheck check = dbChecks.tChecks.First(q => q.fID == id);
            if (check == null)
            {
                return RedirectToAction("allCheckBook");
            }

            return View(check);
        }
        //編輯收款資料POST
        [HttpPost]
        public ActionResult editCheck(tCheck tC)
        {
            tCheck c = dbChecks.tChecks.First(q => q.fID == tC.fID);
            if (c == null)
            {
                return RedirectToAction("allCheckBook");
            }
            c.fCustomerID = tC.fCustomerID;
            c.fCheckNumber = tC.fCheckNumber;
            c.fCheckMoney = tC.fCheckMoney;
            c.fPaymentDay = tC.fPaymentDay;
            c.fTicktedDay = tC.fTicktedDay;
            c.fPaymentStatus = tC.fPaymentStatus;
            dbChecks.SaveChanges();
            return RedirectToAction("allCheckBook");
        }

        //將收款資料刪除(移入資源回收桶)
        public ActionResult deleteCheck(int id)
        {
            //更改 fDeleteStatus 來判斷是否將收款資料移入資源回收桶
            tCheck check = dbChecks.tChecks.First(q => q.fID == id);
            if (check != null)
            {
                check.fDeleteStatus = true;
                dbChecks.SaveChanges();
            }
            return RedirectToAction("allCheckBook");
        }

        //資源回收桶(VIEW)
        public ActionResult realDeleteCheck()
        {
            //以 fDeleteStatus 欄位判斷是否會顯示在資源回收桶內
            var cCB = from a in dbChecks.tChecks where a.fDeleteStatus == true orderby a.fTicktedDay select a;
            return View(cCB);
        }
        //將收款資料自資料表移除
        public ActionResult realDelete(int id)
        {
            tCheck check = dbChecks.tChecks.First(q => q.fID == id);
            if (check != null)
            {
                dbChecks.tChecks.Remove(check);
                dbChecks.SaveChanges();
            }
            return RedirectToAction("realDeleteCheck");
        }
        //將收款資料自資源回收桶還原
        public ActionResult restore(int id)
        {
            tCheck check = dbChecks.tChecks.First(q => q.fID == id);
            if (check != null)
            {
                check.fDeleteStatus = false;
                dbChecks.SaveChanges();
            }
            return RedirectToAction("realDeleteCheck");
        }
    }
}