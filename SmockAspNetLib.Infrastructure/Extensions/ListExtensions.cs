using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmockAspNetLib.Infrastructure.Extensions
{
    /// <summary>
    /// List的扩展。
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// 切片，.NET3.0请使用新特性Rang
        /// https://www.infoq.cn/article/1eM2A9mfINflb58qa9gs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Obsolete(".NET CORE 3.0 的新特性是Rang")]
        public static List<List<T>> ToRang<T>(this List<T> list, int pageSize)
        {
            List<List<T>> result = new List<List<T>>();

            int count = list.Count % pageSize == 0 ? list.Count / pageSize : list.Count / pageSize + 1;

            for (int i = 0; i < count; i++)
            {
                int index = i * pageSize;
                var subary = list.Skip(index).Take(pageSize).ToList();
                result.Add(subary);
            }

            return result;
        }
    }
}
