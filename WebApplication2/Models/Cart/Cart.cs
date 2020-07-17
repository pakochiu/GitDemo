using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.Cart
{
    [Serializable]
    public class Cart : IEnumerable<CartItem> //IEnumerable<CartItem> 讓他自動實作 購物車類別
    {
        //建構子
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }

        //儲存所有商品
        private List<CartItem> cartItems;

        //取得數量
        public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
        }

        //用 productID 新增一筆訂單
        public bool AddPorduct(string fProductID)
        {
            var findItem = this.cartItems
                .Where(s => s.fProductID == fProductID)
                .Select(s => s)
                .FirstOrDefault();

            //判斷裡面有沒有相同的 fProductID
            if (findItem == default(CartItem))
            {
                //不存在 就新增一筆
                using (dbMSIT126TeamEntities db = new dbMSIT126TeamEntities())
                {
                    var detial = (from p in db.tOrderDetails
                                  where p.fProductID == fProductID
                                  select p).FirstOrDefault();

                    if (detial != default(tOrderDetail))
                    {
                        this.AddPorduct(detial);
                    }
                }
            }
            else
            {
                //如果已經有了 數量+1
                findItem.fQuantity += 1;
            }
            return true;
        }

        //新增一筆 用 tOrderDetail 物件
        public bool AddPorduct(tOrderDetail detial)
        {
            var cartItem = new CartItem();
            {
                cartItem.fProductID = detial.fProductID;
                cartItem.fLenght = detial.fLenght;
                cartItem.fWidth = detial.fWidth;
                cartItem.fQuantity = detial.fQuantity;
                cartItem.fGlassID = detial.fGlassID;
            }

            //加入購物車
            this.cartItems.Add(cartItem);
            return true;
        }

        //刪除一筆資料 使用 fProductID, fLenght, fWidth 當條件搜尋
        public bool RemoveProduct(string fProductID, string fLenght, string fWidth)
        {
            var findItem = this.cartItems
                .Where(s => s.fProductID == fProductID &&
                            s.fLenght.ToString("0.00") == fLenght &&
                            s.fWidth.ToString("0.00") == fWidth)
                .Select(s => s)
                .FirstOrDefault();

            //判斷 CartItem 是否已經有存在購物車內
            if(findItem== default(CartItem))
            {
                //不存在就不做任何作
            }
            else
            {
                //存在就將商品移除
                this.cartItems.Remove(findItem);
            }
            return true;
        }

        //清空購物車
        public bool ClearCart()
        {
            this.cartItems.Clear();
            return true;
        }

        //將購物車裡面的 order detail 轉成 List
        public List<tOrderDetail> TOrderDetailList( int theOrderID, decimal theMaterialPrice, double fDoorFee, double fScreenFee, double fInstallationFee, double fGlassFee, double fWaterwayFee, double fCrackFee )
        {
            var result = new List<tOrderDetail>();
            foreach(var item in this.cartItems)
            {
                // 一平方公尺/10.89 = 一才
                decimal 鋁門窗價格 = (鋁門窗(item.fProductID, item.fLenght, item.fWidth) * (decimal)fDoorFee * theMaterialPrice) + 200; //  五金200/一樘
                decimal 紗門窗價格 = (紗門窗(item.fProductID, item.fLenght, item.fWidth) * (decimal)fScreenFee * theMaterialPrice)+(decimal)(紗窗數量(item.fProductID) * 80) + (decimal)(五金數量(item.fProductID) * 100); //紗窗一片80 五金一片100
                decimal 門窗安裝費 = ((item.fLenght / 100) * (item.fWidth / 100) * 10.89m) * (decimal)fInstallationFee * 20;  // 20元/才
                decimal 玻璃安裝費 = ((item.fLenght / 100) * (item.fWidth / 100) * 10.89m) * (decimal)fGlassFee * 玻璃單價(item.fGlassID);
                decimal 水路安裝費 = (item.fLenght + item.fWidth) * 2 / 100 * (decimal)fWaterwayFee * 45;  //  45元/公尺
                decimal 坎縫安裝費 = (item.fLenght + item.fWidth) * 2 / 100 * (decimal)fCrackFee * 35;     //  35元/公尺
                decimal 小計 = 鋁門窗價格 + 紗門窗價格 + 門窗安裝費 + 玻璃安裝費 + 水路安裝費 + 坎縫安裝費;

                result.Add(new tOrderDetail()
                {
                    fOrderID = theOrderID,
                    fProductID = item.fProductID,
                    fLenght = item.fLenght,
                    fWidth = item.fWidth,
                    fGlassID = item.fGlassID,
                    fDoorPrice = 鋁門窗價格,
                    fScreenPrice = 紗門窗價格,
                    fInstallationPrice = 門窗安裝費,
                    fGlassPrice = 玻璃安裝費,
                    fWaterWayPrice = 水路安裝費,
                    fCrackPrice = 坎縫安裝費,
                    fSubTotal = 小計,
                    fQuantity = item.fQuantity
                });
            }
            return result;
        }


        dbMSIT126TeamEntities db = new dbMSIT126TeamEntities();
        //鋁門窗
        private decimal 鋁門窗(string fProductID,decimal fLenght, decimal fWidth)
        {
            //計算 H, W 用料, 換算成公尺
            decimal HH = fLenght / 100 * AluminumWeight(fProductID, "H");
            decimal WW = fWidth / 100 * AluminumWeight(fProductID, "W");

            decimal 鋁門窗價格 = (HH + WW);

            return 鋁門窗價格;
        }

        //紗門窗
        private decimal 紗門窗(string fProductID, decimal fLenght, decimal fWidth)
        {
            //計算 H, W 用料, 換算成公尺
            decimal HH = fLenght / 100 * AluminumWeight(fProductID, "SH");
            decimal WW = fWidth / 100 * AluminumWeight(fProductID, "SW");

            decimal 紗門窗價格 = HH + WW;
            return 紗門窗價格;
        }

        //鋁單位重量(kg/m) x 單位用料
        private decimal AluminumWeight(string fProductID, string fType)
        {
            decimal weight = db.tProductLists.AsEnumerable()
                .Where(p => p.fProductID == fProductID && p.fType == fType)
                .Sum(p => p.tAluminumMaterialList.AL_UnitWeight * (decimal)p.fQuantity);

            return weight;
        }

        //找到玻璃單價
        private decimal 玻璃單價(int fGlassID)
        {
            var 玻璃單價 = db.tGlasses  .Where(p => p.fGlassID == fGlassID) .Select(p => p.fGlassPrice) .FirstOrDefault();
            return (decimal)玻璃單價;
        }

        //找到五金數量
        private double 五金數量(string fProductID)
        {
            double 五金數量 = db.tProductLists.Where(p => p.fProductID == fProductID && p.fType == "HD").Select(p => p.fQuantity).FirstOrDefault();
            return 五金數量;
        }

        //找到紗窗數量
        private double 紗窗數量(string fProductID)
        {
            double 紗窗數量 = db.tProductLists.Where(p => p.fProductID == fProductID && p.fType == "SC").Select(p => p.fQuantity).FirstOrDefault();
            return 紗窗數量;
        }


        //自動實作的 不要動============================================================
        public IEnumerator<CartItem> GetEnumerator()
        {
            return ((IEnumerable<CartItem>)cartItems).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)cartItems).GetEnumerator();
        }

        


    }
}