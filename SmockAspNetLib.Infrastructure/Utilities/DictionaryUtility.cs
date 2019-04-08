using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    public class DictionaryUtility
    {
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

        public static string GetDictionaryString(Dictionary<string, string> sArray)
        {
            var result = "";
            foreach (var item in sArray)
            {
                result += item.Key + "=" + item.Value;
                if (item.Key != sArray.Last().Key)
                {
                    result+="&";
                }
            }
            return result;
        }
    }
}
