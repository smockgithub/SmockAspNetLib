using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmockAspNetLib.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToUnix(this DateTime dateTime, bool isMilliseconds = false)
        {
            DateTime timeStamp = new DateTime(1970, 1, 1); //得到1970年的时间戳
            if (isMilliseconds)
            {
                long a = (dateTime.ToUniversalTime().Ticks - timeStamp.Ticks) / 10000; //注意这里有时区问题，用now就要减掉8个小时
                return a;
            }
            else
            {
                long a = (dateTime.ToUniversalTime().Ticks - timeStamp.Ticks) / 10000000; //注意这里有时区问题，用now就要减掉8个小时
                return a;
            }
            
            
        }
    }
}
