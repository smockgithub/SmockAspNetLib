using System;
using System.Collections.Generic;
using System.Text;

namespace SmockAspNetLib.Infrastructure.Extensions
{
    public static class LongExtensions
    {
        /// <summary>
        /// unix时间戳转换成日期（秒/毫秒）
        /// </summary>
        /// <param name="timestamp">时间戳（秒/毫秒）</param>
        /// <param name="isMilliseconds">是否为毫秒，默认false</param>
        /// <param name="isUtc">默认为LocalTime，true=UtcTime</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timestamp, bool isMilliseconds = false, bool isUtc = false)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTime.UtcNow.Kind);
            if (isMilliseconds)
                start = start.AddMilliseconds(timestamp);
            else
                start = start.AddSeconds(timestamp);

            if (isUtc)
                return start;
            else
                return start.ToLocalTime();
        }
    }
}
