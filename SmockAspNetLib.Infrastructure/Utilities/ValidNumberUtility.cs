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

        private static string constantNumber = "0123456789";
        private static string constantLower = "abcdefghijklmnopqrstuvwxyz";
        private static string constantUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="Length"></param>
        /// <param name="constantType">OnlyNumber = 1,OnlyLower = 2,OnlyUpper = 3,NumberAndLower = 4,LowerAndUpper = 5,NumberAndLowerAndUpper = 6,</param>
        /// <returns></returns>
        public static string GenerateRandomNumber(int Length, int constantType = 6)
        {

            string constantStr = "";

            switch (constantType)
            {
                case 1:
                    constantStr = constantNumber; break;
                case 2:
                    constantStr = constantLower; break;
                case 3:
                    constantStr = constantUpper; break;
                case 4:
                    constantStr = constantNumber + constantLower; break;
                case 5:
                    constantStr = constantLower + constantUpper; break;

                default:
                    constantStr = constantNumber + constantLower + constantUpper;
                    break;
            }

            char[] constant = constantStr.ToCharArray();

            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(constant.Length);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length)]);
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
