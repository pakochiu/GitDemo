using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class GlassInfoController : Controller
    {
        // GET: GlassInfo
        public static string getTheGlassName(int GlassID)
        {
            string GlassName = "";

            switch (GlassID)
            {
                case 501:
                    GlassName = "5清";
                    break;
                case 502:
                    GlassName = "5mm銀霞";
                    break;
                case 503:
                    GlassName = "5mm清+強化";
                    break;
                case 504:
                    GlassName = "5mm清T+矽";
                    break;
                case 511:
                    GlassName = "5mm綠";
                    break;
                case 512:
                    GlassName = "5mm綠+矽利康";
                    break;
                case 513:
                    GlassName = "5mm綠+強化";
                    break;
                case 514:
                    GlassName = "5mm綠+矽+強化";
                    break;
                case 801:
                    GlassName = "8mm清T";
                    break;
                case 802:
                    GlassName = "8mm清+矽利康";
                    break;
                case 803:
                    GlassName = "8mm清+強化";
                    break;
                case 804:
                    GlassName = "8mm清+矽+強化";
                    break;
                case 811:
                    GlassName = "8mm綠";
                    break;
                case 812:
                    GlassName = "8mm綠+矽利康";
                    break;
                case 813:
                    GlassName = "8mm綠+強化";
                    break;
                case 814:
                    GlassName = "8mm綠+矽+強化";
                    break;
                case 901:
                    GlassName = "10mm光";
                    break;
                case 902:
                    GlassName = "10mm光T";
                    break;
            }
            return GlassName;
        }
    }
}