using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models.Cart;

namespace WebApplication2.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        //取得目前購物車
        public ActionResult AddToCart(string fProductID, string fLenght, string fWidth,int fQuantity,int fGlassID)
        {
            var currentCart = operation.GetCurrentCart();
            tOrderDetail t = new tOrderDetail()
            {
                fProductID = fProductID,
                fLenght = Convert.ToDecimal(fLenght),
                fWidth = Convert.ToDecimal(fWidth),
                fQuantity =fQuantity,
                fGlassID =fGlassID
            };
            currentCart.AddPorduct(t);
            return PartialView("_CartPartial");
        }

        //從購物車中 刪除一筆資料
        [HttpPost]
        public ActionResult RemoveFromCart(string fProductID, string fLenght, string fWidth)
        {
            var currentCart = operation.GetCurrentCart();
            currentCart.RemoveProduct(fProductID,fLenght,fWidth);

            return PartialView("_CartPartial");
        }

        //清空購物車 並回到購物車頁面
        public ActionResult ClearCart()
        {
            var currentCart = operation.GetCurrentCart();
            currentCart.ClearCart();
            return PartialView("_CartPartial");
        }
    }
}