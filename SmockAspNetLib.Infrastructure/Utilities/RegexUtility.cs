using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using SmockAspNetLib.Infrastructure;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    /// <summary>
    /// 正则通用类
    /// </summary>
    public class RegexUtility
    {
        /// <summary>
        /// 是否匹配（部分）
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(string source, string pattern)
        {
            return IsMatch(source, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否匹配（部分）
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool IsMatch(string source, string pattern, RegexOptions options)
        {
            Regex reg = new Regex(pattern, options);

            return reg.IsMatch(source);
        }

        /// <summary>
        /// 是否完全匹配
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsValidate(string source, string pattern)
        {
            return IsValidate(source, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否完全匹配
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool IsValidate(string source, string pattern, RegexOptions options)
        {
            bool reval = false;
            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = reg.Match(source);
            reval = m.Success;

            if (string.Compare(source, m.Value) != 0)
            {
                reval = false;
            }

            return reval;
        }

        /// <summary>
        /// 得到匹配的值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="index">1是得到第1个括号内匹配的值</param>
        /// <returns></returns>
        public static string GetValidateValue(string source, string pattern, int index)
        {
            Regex reg = new Regex(pattern);
            MatchCollection mc = reg.Matches(source);

            foreach (Match m in mc)
            {
                if (m.Groups[index] != null)
                {
                    return m.Groups[index].Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 得到匹配的值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pattern"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValidateValue(string source, string pattern, string key)
        {
            Regex reg = new Regex(pattern);
            MatchCollection mc = reg.Matches(source);

            foreach (Match m in mc)
            {
                if (m.Groups[key] != null)
                {
                    return m.Groups[key].Value;
                }
            }

            return string.Empty;
        }

        #region 常用正则
        /// <summary>
        /// 中国身份证
        /// </summary>
        public const string ChineseIdentityCard = "^([1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3})|([1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[A-Z]))$";

        /// <summary>
        /// 中国电话号码：区号支持3到4位，电话支持7到8位
        /// </summary>
        public const string ChinesePhoneNumber = "^\\d{3,4}[-]?\\d{7,8}$";

        /// <summary>
        /// 手机号码
        /// </summary>
        public const string Mobile = "^(13[0-9]|15[0|3|6|7|8|9]|18[6|8|9])\\d{8}$";

        /// <summary>
        /// 中国移动手机号码
        /// </summary>
        public const string ChinaMobile = "^1(3[4-9]|5[012789]|8[78])\\d{8}$";

        /// <summary>
        /// 中国邮政编码
        /// </summary>
        public const string ChineseZipCode = "^[1-9]\\d{5}(?!\\d)$";

        /// <summary>
        /// 小数
        /// </summary>
        public const string DecimalNumber = "^[+-]?(?:\\d+\\.?\\d*|\\d*\\.?\\d+)$";

        /// <summary>
        /// 整数
        /// </summary>
        public const string IntNumber = "^[+-]?\\d+$";

        /// <summary>
        /// 中文字符
        /// </summary>
        public const string ChineseCharacter = "[\u4e00-\u9fa5]";

        /// <summary>
        /// 双字节字符
        /// </summary>
        public const string DubleByteCharacter = "[^x00-xff]";

        /// <summary>
        /// URL
        /// </summary>
        public const string URL = "^(https?|ftp)://([-A-Z0-9.]+)(/[-A-Z0-9+&@#/%=~_|!:,.;]*)?(\\?[-A-Z0-9+&@#/%=~_|!:,.;]*)?$";

        /// <summary>
        /// 电子邮件
        /// </summary>
        public const string Email = "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";

        /// <summary>
        /// GUID
        /// </summary>
        public const string GUID = "^[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-Z0-9]{12}$";

        /// <summary>
        /// Windows路径
        /// </summary>
        public const string WindowsPath = "^([a-z]:|\\\\[a-z0-9]+)\\[^/:*?\"<>|\r\n]*$";

        /// <summary>
        /// IP地址
        /// </summary>
        public const string IPAddr = "^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        /// <summary>
        /// 日期（yyyy-MM-dd,yyyy/MM/dd,yyyy.MM.dd）
        /// </summary>
        public const string Date = "^(((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))/0?2/29)|(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})/(((1[02]|0?[13578])/(3[01]|[12][0-9]|0?[1-9]))|((0?[469]|11)/(30|[12][0-9]|0?[1-9]))|(0?2/(2[0-8]|[1][0-9]|0?[1-9])))))|(((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-0?2-29)|(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((1[02]|0?[13578])-(3[01]|[12][0-9]|0?[1-9]))|((0?[469]|11)-(30|[12][0-9]|0?[1-9]))|(0?2-(2[0-8]|[1][0-9]|0?[1-9])))))|(((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))\\.0?2\\.29)|(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})\\.(((1[02]|0?[13578])\\.(3[01]|[12][0-9]|0?[1-9]))|((0?[469]|11)\\.(30|[12][0-9]|0?[1-9]))|(0?2\\.(2[0-8]|[1][0-9]|0?[1-9])))))$";

        /// <summary>
        /// 时间（hh:mm:ss)
        /// </summary>
        public const string Time = "^([0-1]?[0-9]|2[0-3]):([1-5][0-9]|0?[1-9]):([1-5][0-9]|0?[1-9])$";

        /// <summary>
        /// 时间（hh:mm)
        /// </summary>
        public const string TimeShort = "^([0-1]?[0-9]|2[0-3]):([1-5][0-9]|0?[1-9])$";

        /// <summary>
        /// 日期时间（yyyy-MM-dd hh:mm:ss）日期与时间之间是空格
        /// </summary>
        public const string DateTime = "^(((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-0?2-29)|(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((1[02]|0?[13578])-(3[01]|[12][0-9]|0?[1-9]))|((0?[469]|11)-(30|[12][0-9]|0?[1-9]))|(0?2-(2[0-8]|[1][0-9]|0?[1-9])))))\\s([0-1]?[0-9]|2[0-3]):([1-5][0-9]|0?[1-9]):([1-5][0-9]|0?[1-9])$";

        /// <summary>
        /// 日期时间（yyyy-MM-dd hh:mm）日期与时间之间是空格
        /// </summary>
        public const string DateTimeShort = "^(((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-0?2-29)|(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((1[02]|0?[13578])-(3[01]|[12][0-9]|0?[1-9]))|((0?[469]|11)-(30|[12][0-9]|0?[1-9]))|(0?2-(2[0-8]|[1][0-9]|0?[1-9])))))\\s([0-1]?[0-9]|2[0-3]):([1-5][0-9]|0?[1-9])$";
        #endregion

        ///// <summary>
        ///// 通过传入的字符格式得到日期时间正则yyyy(年),MM(月),dd（日）,hh（小时),mm(分),ss(秒)
        ///// </summary>
        ///// <param name="fromat"></param>
        ///// <returns></returns>
        //public static string DateTimeRegex(string fromat)
        //{
        //    string reval = CommonConst.DateTime;

        //    return reval;
        //}
    }
}
