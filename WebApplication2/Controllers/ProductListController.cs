using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class ProductListController : Controller
    {
        // GET: ProductList
        dbMSIT126TeamEntities db = new dbMSIT126TeamEntities();
        public ActionResult ProductList(string fGroup)
        {
            //玻璃清單
            SelectList getGlassList = new SelectList(this.getGlassList(), "fGlassID", "fGlassName");
            ViewBag.theGlassList = getGlassList;

            //group 過後的 product 清單
            SelectList getGroupProductList = new SelectList(this.getGroupProductList(fGroup), "fProductID", "fProductID");
            ViewBag.theProductList = getGroupProductList;

            var theList = from p in db.tProducts
                          where p.fGroup == fGroup
                          select p;

            return View(theList);
        }

        //彈出視窗的放大圖
        public string thePopupImage(string theKey)
        {
            string img = $"<img src='/Image/ProductImages/{theKey}.jpg'/>";
            return img;
        }

        //取得玻璃清單
        private IEnumerable getGlassList()
        {
            var theList = from p in db.tGlasses
                          select p;

            return theList;
        }

        //取得 group 過後的 product 清單
        private IEnumerable getGroupProductList(string productKey)
        {
            var theList= from p in db.tProducts
                         where p.fGroup==productKey
                         select p;

            return theList;
        }
    }
}