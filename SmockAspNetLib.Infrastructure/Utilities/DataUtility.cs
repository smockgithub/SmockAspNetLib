using System;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    /// <summary>
    /// 数据通用类
    /// </summary>
    public class DataUtility
    {
        /// <summary>
        /// 是否无效数据
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsInvalid(object src)
        {
            return (((src == DBNull.Value) || (src == null)) || (src.ToString().Trim() == string.Empty) || (src.ToString().Trim() == Guid.Empty.ToString()));
        }

        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIntNumber(string str)
        {
            return RegexUtility.IsValidate(str, RegexUtility.IntNumber);
        }

        /// <summary>
        /// 是否小数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDecimalNumber(string str)
        {
            return RegexUtility.IsValidate(str, RegexUtility.DecimalNumber);
        }
    }
}
