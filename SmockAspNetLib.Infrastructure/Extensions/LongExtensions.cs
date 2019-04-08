using System;
using System.Collections.Generic;
using System.Text;

namespace SmockAspNetLib.Infrastructure.Extensions
{
    public static class LongExtensions
    {
        /// <summary>
        /// unix时间戳转换成日期（秒/毫秒）
        /// may be use ToLocalTime() for LocalTime
        /// </summary>
        /// <param name="timestamp">时间戳（秒/毫秒）</param>
        /// <param name="isMilliseconds">是否为毫秒，默认false</param>
        /// <param name="isUtc">默认为UTC</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timestamp, bool isMilliseconds = false, bool isUtc = true)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, isUtc ? DateTime.UtcNow.Kind : DateTime.Now.Kind);
            if (isMilliseconds)
                return start.AddMilliseconds(timestamp);
            else
                return start.AddSeconds(timestamp);
        }
    }
}
