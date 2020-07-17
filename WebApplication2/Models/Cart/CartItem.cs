using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;

namespace WebApplication2.Models.Cart
{
    //購物車 Item
    [Serializable]
    public class CartItem
    {
        public int fID { get; set; }
        public int fOrderID { get; set; }
        public string fProductID { get; set; }
        public decimal fLenght { get; set; }
        public decimal fWidth { get; set; }
        public int fQuantity { get; set; }
        public int fGlassID { get; set; }     
    }
}