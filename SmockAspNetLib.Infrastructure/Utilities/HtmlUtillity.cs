//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace SmockAspNetLib.Infrastructure.Utilities
//{
//   public static class HtmlUtillity
//    {
//        public static IHtmlString trimString(object str, int len)
//        {
//            if (str == null)
//            {
//                return new HtmlString("");
//            }
//            string objectString = str.ToString();
//            if (objectString.Length > len)
//            {
//                return new HtmlString(objectString.Substring(0, len) + "...");
//            }
//            return new HtmlString(objectString);
//        }
//    }
//}
