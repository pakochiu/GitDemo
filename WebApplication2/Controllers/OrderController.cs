using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Cart;

namespace WebApplication2.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(tOrder theOrder)
        {
            //取得購物車
            var currentCart = operation.GetCurrentCart();

            using (dbMSIT126TeamEntities db=new dbMSIT126TeamEntities())
            {
                //建立 tOrder 物件
                var order = new tOrder()
                {
                    fCustomerID = theOrder.fCustomerID,
                    fDeliverDate1 = theOrder.fDeliverDate1,
                    fDeliverDate2 = theOrder.fDeliverDate1.AddDays(30),
                    fDeliverDate3 = theOrder.fDeliverDate1.AddDays(60),
                    fDeliverDate4 = theOrder.fDeliverDate1.AddDays(180),
                    fOrderDate = theOrder.fOrderDate,
                    fEmployeeID = 18,        //預設18號
                    fMaterialPrice = 140,    //鋁料價格寫死 140
                    fDoorFee = 1.3,          //預設 1.3
                    fScreenFee = 1.3,        //預設 1.3
                    fInstallationFee = 1.3,  //預設 1.3
                    fGlassFee = 1,           //預設 1
                    fWaterwayFee = 1,        //預設 1
                    fCrackFee = 1,           //預設 1
                    fOrderConfirm = "0"      //預設 0 (未確認)
                };

                //儲存面更
                db.tOrders.Add(order);
                db.SaveChanges();

                //取得購物車裡面的 order detail
                var orderDetail = currentCart.TOrderDetailList
                    (
                        order.fOrderID,
                        (decimal)order.fMaterialPrice,
                        (double)order.fDoorFee,
                        (double)order.fScreenFee,
                        (double)order.fInstallationFee,
                        (double)order.fGlassFee,
                        (double)order.fWaterwayFee,
                        (double)order.fCrackFee
                    );

                //加入 order detail 資料後儲存
                db.tOrderDetails.AddRange(orderDetail);
                db.SaveChanges();

                //把訂單總額寫進 tOrder
                decimal OrderTotal = (decimal)db.tOrderDetails.Where(p => p.fOrderID == order.fOrderID).Select(p => p.fQuantity * p.fSubTotal).Sum(p => p)*1.05m; //含稅5%
                order.fGetMoneyBack1 = OrderTotal * 0.4m;
                order.fGetMoneyBack2 = OrderTotal * 0.4m;
                order.fGetMoneyBack3 = OrderTotal * 0.1m;
                order.fGetMoneyBack4 = OrderTotal * 0.1m;
                db.SaveChanges();
            }


            //return Content("訂購成功");
            return RedirectToAction("Index","Home");
        }

        //GlassID 轉成 Glass Name
        public static string getTheGlassName(int GlassID)
        {
            dbMSIT126TeamEntities db = new dbMSIT126TeamEntities();

            string innerText = (from p in db.tGlasses
                     where p.fGlassID == GlassID
                     select p.fGlassName).FirstOrDefault();

            return innerText;
        }
    }
}