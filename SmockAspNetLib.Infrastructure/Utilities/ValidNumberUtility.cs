using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    public class ValidNumberUtility
    {
        private const int ExpireTime = 5;//过期时间5分钟
        private static object _VerValidNumberLock = new object();
        private static Dictionary<string, string> _DictVerValidNumber = new Dictionary<string, string>();
        private static Dictionary<string, DateTime> _DictVerTime = new Dictionary<string, DateTime>();

        private static char[] constant =
        {
        '0','1','2','3','4','5','6','7','8','9',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
        };
        public static string GenerateRandomNumber(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
        public static string GetValidNumber(string strLoginName)
        {
            string strKey = System.Convert.ToString(GenerateRandomNumber(4));
            lock (_VerValidNumberLock)
            {
                _DictVerValidNumber[strLoginName] = strKey;
                _DictVerTime[strLoginName] = DateTime.Now;
            }
            return strKey;
        }

        public static bool HasValidNumber(string strLoginName)
        {
            lock (_VerValidNumberLock)
            {
                if (_DictVerValidNumber.ContainsKey(strLoginName) &&
                        _DictVerTime.ContainsKey(strLoginName) &&
                        _DictVerTime[strLoginName].AddMinutes(ExpireTime) >= DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CompareValidNumber(string strLoginName, string strValidNumber)
        {
            lock (_VerValidNumberLock)
            {
                if (_DictVerValidNumber.ContainsKey(strLoginName) &&
                        _DictVerTime.ContainsKey(strLoginName) &&
                        _DictVerTime[strLoginName].AddMinutes(ExpireTime) >= DateTime.Now &&
                        _DictVerValidNumber[strLoginName] == strValidNumber)
                {
                    _DictVerValidNumber.Remove(strLoginName);
                    _DictVerTime.Remove(strLoginName);
                    return true;
                }
            }
            return false;
        }
    }
}
