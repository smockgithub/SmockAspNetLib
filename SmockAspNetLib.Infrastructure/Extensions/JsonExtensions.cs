//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;

//namespace SmockAspNetLib.Infrastructure.Extentions
//{
//    public static class JsonExtensions
//    {
//        /// <summary>
//        /// 转化成Json
//        /// </summary>
//        /// <param name="obj">The object.</param>
//        /// <param name="camelCase">if set to <c>true</c> [camel case].</param>
//        /// <param name="ignoreNull">if set to <c>true</c> [ignore null].</param>
//        /// <param name="indented">if set to <c>true</c> [indented].</param>
//        /// <returns>System.String.</returns>
//        public static string ToJsonString(this object obj, bool camelCase = true, bool ignoreNull = true, bool indented = false)
//        {
//            var options = new JsonSerializerSettings();

//            if (camelCase)
//            {
//                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
//            }

//            if (indented)
//            {
//                options.Formatting = Formatting.Indented;
//            }
//            if (ignoreNull)
//            {
//                options.NullValueHandling = NullValueHandling.Ignore;
//            }

//            //日期类型默认格式化处理
//            options.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
//            options.DateFormatString = "yyyy-MM-dd HH:mm:ss";

//            return JsonConvert.SerializeObject(obj, options);
//        }
//    }
//}
