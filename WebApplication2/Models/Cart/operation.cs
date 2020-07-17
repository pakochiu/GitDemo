using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebApplication2.ViewModel;

namespace WebApplication2.Models.Cart
{
    //購物車操作
    public class operation
    {
        [WebMethod(EnableSession = true)] //啟用 session
        public static Cart GetCurrentCart()
        {
            if (HttpContext.Current != null) //確認目前 HttpContext.Current 為空
            {
                //如果 Session 不存在"購物車" 就增加一個購物車
                if (HttpContext.Current.Session[CDictionary.購物車內容] == null)
                {
                    var order = new Cart();
                    HttpContext.Current.Session[CDictionary.購物車內容] = order;
                }
                //回傳 Session[CDictionary.購物車內容]
                return (Cart)HttpContext.Current.Session[CDictionary.購物車內容];
            }
            else
            {
                throw new InvalidOperationException("System.Web.HttpContext.Current 為空, 請檢查");
            }
        }
    }
}