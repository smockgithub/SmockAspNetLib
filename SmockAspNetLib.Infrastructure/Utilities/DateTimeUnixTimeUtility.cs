//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Text;

//namespace SmockAspNetLib.Infrastructure.Utilities
//{
//    public static class DateTimeUnixTimeUtility
//    {
//        /// <summary>
//        /// 日期转换成unix时间戳，
//        /// </summary>
//        /// <param name="dateTime">dateTime应传入utc的时间</param>
//        /// <returns></returns>
//        public static long DateTimeToUnixTimestamp(DateTime dateTime)
//        {
//            var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
//            return Convert.ToInt64((dateTime - start).TotalSeconds);
//        }

//        /// <summary>
//        /// 获取当前时间的时间戳
//        /// </summary>
//        /// <param name="bflag">true为秒,false为毫秒</param>
//        /// <returns></returns>
//        public static long DateTimeNowToUnixTimestamp(bool bflag)
//        {
//            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
//            if (bflag)
//                return Convert.ToInt64(ts.TotalSeconds);
//            else
//                return Convert.ToInt64(ts.TotalMilliseconds);
//        }

//        /// <summary>
//        /// 获取时间戳
//        /// </summary>
//        /// <param name="dateTime"></param>
//        /// <param name="bflag">true为秒,false为毫秒</param>
//        /// <returns></returns>
//        public static long DateTimeTo13UnixTimestamp(DateTime dateTime, bool bflag)
//        {
//            TimeSpan ts = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
//            if (bflag)
//                return Convert.ToInt64(ts.TotalSeconds);
//            else
//                return Convert.ToInt64(ts.TotalMilliseconds);
//        }

//        /// <summary>
//        /// unix时间戳转换成日期
//        /// </summary>
//        /// <param name="unixTimeStamp">时间戳（秒）</param>
//        /// <returns></returns>
//        public static DateTime UnixTimestampToDateTime(this DateTime target, long timestamp)
//        {
//            var start = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
//            return start.AddSeconds(timestamp);
//        }

//        /// <summary>
//        /// unix时间戳转换成日期（秒/毫秒）
//        /// may be use ToLocalTime() for LocalTime
//        /// </summary>
//        /// <param name="unixTimeStamp">时间戳（秒）</param>
//        /// <param name="isMilliseconds">是否为毫秒，默认false</param>
//        /// <returns></returns>
//        public static DateTime UnixTimestampToUtcDateTime(long timestamp,bool isMilliseconds=false,bool isUtc=true)
//        {
//            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTime.UtcNow.Kind);
//            if(isMilliseconds)
//                return start.AddMilliseconds(timestamp);
//            else
//                return start.AddSeconds(timestamp);
//        }

//        /// <summary>
//        /// String转换日期
//        /// </summary>
//        /// <param name="time">时间字符串</param>
//        /// <param name="format">字符串格式</param>
//        /// <returns></returns>
//        public static DateTime StringFormatToDateTime(string time, string format)
//        {
//            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
//            dtFormat.ShortDatePattern = format;
//            return System.Convert.ToDateTime(time, dtFormat);
//        }
//    }
//}
