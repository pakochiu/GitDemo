using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Newtonsoft.Json;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication2.KeyHolder;
using WebApplication2.ViewModel;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class salesController : Controller
    {
      
        dbMSIT126TeamEntities  db = new dbMSIT126TeamEntities();
        // GET: sales

        public ActionResult sales(string sortOrder, string currentFilter, string searchString, int page = 1)//業務主畫面
        {
            //排序資料
            //ViewBag.CurrentSort = sortOrder;
            //ViewBag.orderidSortParm = String.IsNullOrEmpty(sortOrder) ? "orderid_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "OrderDate" ? "OrderDate_desc" : "OrderDate";
            //搜尋
            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            //ViewBag.CurrentFilter = searchString;

            var table = from s in db.tOrders
                        where s.fOrderConfirm == "0"
                        //orderby s.fOrderID
                        select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    table = table.Where(s => s.fOrderID.ToString().Contains(searchString)
            //                           || s.fEmployeeID.ToString().Contains(searchString)
            //                           || s.fOrderDate.ToString().Contains(searchString)
            //                           || s.tEmployee.fName.ToString().Contains(searchString)
            //                           || s.tCustomer.fCompanyName.ToString().Contains(searchString));
            //}
            //switch (sortOrder)
            //{
            //    case "orderid_desc":
            //        table = table.OrderByDescending(s => s.fOrderID);
            //        break;
            //    case "OrderDate":
            //        table = table.OrderBy(s => s.fOrderDate);
            //        break;
            //    case "OrderDate_desc":
            //        table = table.OrderBy(s => s.fOrderDate);
            //        break;
            //    default:
            //        table = table.OrderBy(s => s.fOrderID);
            //        break;
            //}
            //分頁
            //int pageSize = 3;
            //int pageNumber = page < 1 ? 1 : page;
            //return View(table.ToPagedList(pageNumber, pageSize));
            return View(table);
        }


        public ActionResult List()//業務主畫面
        {
            var table = from p in db.tOrders
                        where p.fOrderConfirm=="0"
                        orderby p.fOrderID
                        select p;
            return View(table);
        }
        public ActionResult history() //歷史訂單
        {
            var table = from s in db.tOrders
                        where s.fOrderConfirm == "1"
                        //orderby s.fOrderID
                        select s;
            return View(table);
        }
        public ActionResult ErrorOrder() //錯誤訂單
        {
            var table = from s in db.tOrders
                        where s.fOrderConfirm == "2"
                        //orderby s.fOrderID
                        select s;
            return View(table);
        }
        public ActionResult New() //新增訂單
        {
            return View();
        }
        [HttpPost]
        public ActionResult New(tOrder p)
        {
            db.tOrders.Add(p);
            db.SaveChanges();
            return RedirectToAction("sales");
        }
        public ActionResult Edit(int id) //修改訂單
        {
            //撈回empID
            SelectList getempList = new SelectList(this.getempList(), "fEmployeeID", "fName");
            ViewBag.theempList = getempList;
            //撈回customerID
            SelectList getcustList = new SelectList(this.getcustList(), "fCustomerID", "fCompanyName");
            ViewBag.thecustList = getcustList;

            tOrder order = db.tOrders.First(p => p.fOrderID == id);
            //customerlist(order.fCustomerID.Value);
            if (order == null)
                return RedirectToAction("sales");
            return View(order);
        }
        [HttpPost]
        public ActionResult Edit(tOrder p, string ODConfirm)//修改訂單儲存
        {
            
            //dbMSIT126Team02Entities3 db = new dbMSIT126Team02Entities3();
            tOrder order = db.tOrders.First(m => m.fOrderID == p.fOrderID);
            if (order == null)
                return RedirectToAction("sales");
            order.fCrackFee = p.fCrackFee;
            order.fCustomerID = p.fCustomerID;
            order.fDeliverDate1 = p.fDeliverDate1;
            order.fDeliverDate2 = p.fDeliverDate2;
            order.fDeliverDate3 = p.fDeliverDate3;
            order.fDeliverDate4 = p.fDeliverDate4;
            order.fDoorFee = p.fDoorFee;
            order.fEmployeeID = p.fEmployeeID;
            order.fGetMoneyBack1 = p.fGetMoneyBack1;
            order.fGetMoneyBack2 = p.fGetMoneyBack2;
            order.fGetMoneyBack3 = p.fGetMoneyBack3;
            order.fGetMoneyBack4 = p.fGetMoneyBack4;
            order.fGlassFee = p.fGlassFee;
            order.fMaterialPrice = p.fMaterialPrice;
            order.fOrderDate = p.fOrderDate;
            order.fScreenFee = p.fScreenFee;
            order.fWaterwayFee = p.fWaterwayFee;          
            //order.fOrderConfirm = p.fOrderConfirm;
            if (ODConfirm == "0")
                order.fOrderConfirm = "0";
            else
                order.fOrderConfirm = "1";
            db.SaveChanges();
            return RedirectToAction("sales");
        }

        private IEnumerable getempList()//撈回empID
        {
            var theList = from p in db.tEmployees
                          select p;

            return theList;
        }

        private IEnumerable getcustList() //撈回customerID
        {
            var theList = from p in db.tCustomers
                          select p;

            return theList;
        }
        public ActionResult Back(int? id)//將錯誤訂單還原
        {

            // dbMSIT126Team02Entities3 db = new dbMSIT126Team02Entities3();
            tOrder order = db.tOrders.First(p => p.fOrderID == id);
            if (order != null)
            {
                order.fOrderConfirm = "0";
                db.SaveChanges();
            }
            return RedirectToAction("sales");
        }
        public ActionResult Delete(int? id)//將不需要的訂單設為2(錯誤訂單)
        {

           // dbMSIT126Team02Entities3 db = new dbMSIT126Team02Entities3();
            tOrder order = db.tOrders.First(p => p.fOrderID == id);
            if (order != null)
            {
                order.fOrderConfirm = "2";
                db.SaveChanges();     
            }
            return RedirectToAction("sales");
        }

        static int orderID;
      

        public ActionResult Comment(int id)//訂單備註
        {
            ViewBag.thecomment = id;

            var table = ((from p in db.tOrderComments
                         where p.fOrderID == id
                         select p).ToList());
            
            orderID = id;
            return View(table);
        }

        public ActionResult NewComment()//新增訂單備註
        {
            ViewBag.newcomment = orderID;
            ViewBag.empName = (Session[EmpDitctionary.SK_Login_Name]);
            ViewBag.empid = (Session[EmpDitctionary.SK_Login_ID]);
            return View();
        }
        [HttpPost]
        public ActionResult NewComment(tOrderComment p)
        {
            
            p.fOrderID = orderID;
            //p.fCustomerID =;
            //p.fEmployeeID =;
            tOrderComment m = new tOrderComment();
            m.fEmployeeID = Convert.ToInt32(Session[EmpDitctionary.SK_Login_ID]);
            m.fOrderID = orderID;
            m.fComment = p.fComment;
            m.fUpdateDate = p.fUpdateDate;
            db.tOrderComments.Add(m);
            db.SaveChanges();
            string com = "Comment/" + orderID;
            return RedirectToAction(com);
        }


        public ActionResult EditComment(int id) //修改訂單
        {
             SelectList getempList = new SelectList(this.getempList(), "fEmployeeID", "fName");
            ViewBag.theempList = getempList;

            tOrderComment comment = db.tOrderComments.First(p => p.fID == id);
            //customerlist(order.fCustomerID.Value);
            if (comment == null)
                return RedirectToAction("sales");
            return View(comment);
        }
        [HttpPost]
        public ActionResult EditComment(tOrderComment p)//修改訂單儲存
        {
            //dbMSIT126Team02Entities3 db = new dbMSIT126Team02Entities3();
            tOrderComment EC = db.tOrderComments.First(m => m.fID == p.fID);
            if (EC == null)
                return RedirectToAction("sales");
            EC.fEmployeeID = p.fEmployeeID;
            EC.fOrderID = orderID;
            EC.fComment = p.fComment;
            EC.fUpdateDate = p.fUpdateDate;
            db.SaveChanges();
            string com = "Comment/" + orderID;
            return RedirectToAction(com);
        }


        public ActionResult orderDetail(int id)//訂單詳細資料
        {

            //tOrderDetail detail = (new dbMSIT126TeamEntities()).tOrderDetails.First(p => p.fOrderID == id);
            //if (detail == null)
            //    return RedirectToAction("sales");
            //return View(detail);
            var q = (from p in db.tOrderDetails
                     where p.fOrderID == id
                     select p).ToList();
            return View(q);
        }
        public ActionResult customer(int id)//廠商資料
        {
            tCustomer customer = db.tCustomers.First(p => p.fCustomerID == id);
            if (customer == null)
                return RedirectToAction("sales");
            return View(customer);
        }

        // public ActionResult employee(int id)//業務資料
        //{
        //    tEmployee employee = db.tEmployees.First(p => p.fEmployeeID == id);
        //    if (employee == null)
        //        return RedirectToAction("sales");
        //    return View(employee);
        //}
        public JsonResult emp(int id)//業務資料
        {
            //tEmployee employee = db.tEmployees.First(p => p.fEmployeeID == id);

            var q = from e in db.tEmployees
                    where e.fEmployeeID == id
                    select new
                    {
                        e.fGender,
                        e.fName,
                        e.fPhone
                    };
        
            return Json(q);
        }
        public JsonResult cust(int id)//業務資料
        {
            //tEmployee employee = db.tEmployees.First(p => p.fEmployeeID == id);

            var q = from e in db.tCustomers
                    where e.fCustomerID == id
                    select new
                    {
                        e.fCompanyName,
                        e.fContactName,
                        e.fTelephone,
                        e.fPhone,
                        e.fCity,
                        e.fAddress,
                        e.fUserName
                    };

            return Json(q);
        }
        public ActionResult check(int id)//進度回報
        {
            var table = ((from p in db.tChecks
                          where  p.fOderID== id
                          select p).ToList());

            return View(table);
        }
    }
}