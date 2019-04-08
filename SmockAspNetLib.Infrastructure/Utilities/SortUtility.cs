using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    /// <summary>
    /// 排序
    /// </summary>
    public class SortUtility
    {
        /// <summary>
        /// 字典通过Ascii排序
        /// </summary>
        /// <param name="sArray"></param>
        /// <returns></returns>
        public static Dictionary<string, string> AsciiDictionary(Dictionary<string, string> sArray)
        {
            Dictionary<string, string> asciiDic = new Dictionary<string, string>();
            string[] arrKeys = sArray.Keys.ToArray();
            Array.Sort(arrKeys, string.CompareOrdinal);
            foreach (var key in arrKeys)
            {
                string value = sArray[key];
                asciiDic.Add(key, value);
            }
            return asciiDic;
        }
    }
}
