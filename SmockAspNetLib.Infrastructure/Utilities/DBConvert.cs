//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;

//namespace SmockAspNetLib.Infrastructure.Utilities
//{
//    public static class DBConvert
//    {
//        /// <summary>
//        /// 将数据库中的日期字段转换为DateTime,如果dbValue为DBNull.Value,则返回DateTime.MinValue;
//        /// </summary>
//        /// <param name="dbValue">要转换的数据库字段值</param>
//        /// <returns>日期</returns>
//        public static DateTime ToDateTime(object dbValue)
//        {
//            if (dbValue == null || dbValue == DBNull.Value)
//            {
//                return DateTime.MinValue;
//            }
//            return Convert.ToDateTime(dbValue);
//        }

//        /// <summary>
//        /// 转换bool类型
//        /// </summary>
//        /// <param name="dbValue">要转换的数据库字段值</param>
//        /// <returns></returns>
//        public static bool ToBool(object dbValue)
//        {
//            bool result = false;
//            if (dbValue == null || dbValue == DBNull.Value)
//            {
//                return result;
//            }

//            bool.TryParse(dbValue.ToString(), out result);
//            return result;
//        }

//        /// <summary>
//        /// 获取字符串
//        /// </summary>
//        /// <param name="dbValue">待转换的object</param>
//        /// <returns></returns>
//        public static string ToSafeString(object dbValue)
//        {
//            if (dbValue == null)
//                return string.Empty;
//            return dbValue.ToString();
//        }

//        /// <summary>
//        /// 转换为ToDecimal
//        /// </summary>
//        /// <param name="dbValue">待转换的object</param>
//        /// <returns></returns>
//        public static decimal ToDecimal(object dbValue)
//        {
//            decimal result = decimal.Zero;
//            if (dbValue == null || dbValue == DBNull.Value)
//            {
//                return result;
//            }
//            decimal.TryParse(dbValue.ToString(), out result);
//            return result;
//        }

//        /// <summary>
//        /// 转换时间
//        /// </summary>
//        /// <param name="dbValue">待转换的object</param>
//        /// <returns></returns>
//        public static DateTime? ToDateTimeNullable(object dbValue)
//        {
//            if (dbValue == null || dbValue == DBNull.Value)
//            {
//                return null;
//            }
//            return Convert.ToDateTime(dbValue);
//        }

//        /// <summary>
//        /// 将日期型转换成数据库字段值，如果time值为time.MinValue，则转换为DBNull.Value
//        /// </summary>
//        /// <param name="time">要转换的日期</param>
//        /// <returns></returns>
//        public static object ToDBValue(DateTime time)
//        {
//            if (time == DateTime.MinValue)
//                return DBNull.Value;

//            return time;
//        }

//        /// <summary>
//        /// 将日期型转换成数据库字段值，如果time值为null，则转换为DBNull.Value
//        /// </summary>
//        /// <param name="time">要转换的日期</param>
//        /// <returns></returns>
//        public static object ToDBValue(DateTime? time)
//        {
//            if (!time.HasValue)
//                return DBNull.Value;

//            return ToDBValue((DateTime)time);
//        }
//        public static object ToDBValue(string value)
//        {
//            if (string.IsNullOrEmpty(value))
//            {
//                return DBNull.Value;
//            }
//            return value;
//        }

//        public static object ToDBValue(int? d)
//        {

//            if (!d.HasValue)
//                return DBNull.Value;
//            return ToDBValue((int)d);
//        }

//        public static object ToDBValue(int d)
//        {

//            if (d == int.MinValue)
//                return DBNull.Value;
//            return d;
//        }

//        public static object ToDBValue(decimal? d)
//        {

//            if (!d.HasValue)
//                return DBNull.Value;
//            return ToDBValue((decimal)d);
//        }

//        public static object ToDBValue(decimal d)
//        {

//            if (d == decimal.MinValue)
//                return DBNull.Value;
//            return d;
//        }


//        /// <summary>
//        /// 将整型的数据库字段值转换为System.Int32，如果值为DBNull.Value,则转换为 -1
//        /// </summary>
//        /// <param name="dbValue">整型的数据库值</param>
//        /// <returns></returns>
//        public static int ToInt32(object dbValue)
//        {
//            if (dbValue == null || dbValue == DBNull.Value)
//            {
//                return int.MinValue;
//            }
//            return Convert.ToInt32(dbValue);
//        }

//        public static int? ToInt32Nullable(object dbValue)
//        {
//            if (dbValue == null || dbValue == DBNull.Value)
//            {
//                return null;
//            }
//            return Convert.ToInt32(dbValue);
//        }

//        /// <summary>
//        ///  将整型的数据库字段值转换为System.Int64，如果值为DBNull.Value,则转换为 -1
//        /// </summary>
//        /// <param name="dbValue">整型的数据库值</param>
//        /// <returns></returns>
//        public static Int64 ToInt64(object dbValue)
//        {
//            if (dbValue == null || dbValue == DBNull.Value)
//            {
//                return Int64.MinValue;
//            }
//            return Convert.ToInt64(dbValue);
//        }

//        /// <summary>
//        /// 直接从dataReader中读取第i列的日期值，如果值为空，则返回DateTime.MinValue
//        /// </summary>
//        /// <param name="dataReader">要从中读取数据的dataReader</param>
//        /// <param name="i">dataReader中的列索引</param>
//        /// <returns></returns>
//        public static DateTime GetDateTime(System.Data.IDataReader dataReader, int i)
//        {
//            if (dataReader.IsDBNull(i))
//            {
//                return DateTime.MinValue;
//            }

//            return dataReader.GetDateTime(i);
//        }

//        /// <summary>
//        /// 直接从dataReader中读取第i列的日期值，如果值为空，则返回DateTime.MinValue
//        /// </summary>
//        /// <param name="dataReader">要从中读取数据的dataReader</param>
//        /// <param name="i">dataReader中的列索引</param>
//        /// <returns></returns>
//        public static DateTime? GetDateTimeNullable(System.Data.IDataReader dataReader, int i)
//        {
//            if (dataReader.IsDBNull(i))
//            {
//                return null;
//            }

//            return dataReader.GetDateTime(i);
//        }


//        /// <summary>
//        /// 直接从dataReader中读取第i列的值,如果值为空，则返回Int.MinValue
//        /// </summary>
//        /// <param name="dataReader">要读取数据的dataReader</param>
//        /// <param name="i">dataReader中的列索引</param>
//        /// <returns></returns>
//        public static int? GetInt32Nullable(System.Data.IDataReader dataReader, int i)
//        {
//            if (dataReader.IsDBNull(i))
//            {
//                return null;
//            }
//            return dataReader.GetInt32(i);
//        }

//        /// <summary>
//        /// 直接从dataReader中读取第i列的值,如果值为空，则返回Int.MinValue
//        /// </summary>
//        /// <param name="dataReader">要读取数据的dataReader</param>
//        /// <param name="i">dataReader中的列索引</param>
//        /// <returns></returns>
//        public static int GetInt32(System.Data.IDataReader dataReader, int i)
//        {
//            if (dataReader.IsDBNull(i))
//            {
//                return int.MinValue;
//            }
//            return dataReader.GetInt32(i);
//        }

//        public static bool? GetBoolNullable(System.Data.IDataReader dataReader, int i)
//        {
//            if (dataReader.IsDBNull(i))
//            {
//                return null;
//            }
//            return dataReader.GetBoolean(i);
//        }

//        public static bool GetBool(System.Data.IDataReader dataReader, int i)
//        {
//            if (dataReader.IsDBNull(i))
//            {
//                return false;
//            }

//            return dataReader.GetBoolean(i);
//        }

//        /// <summary>
//        /// 直接从dataReader中读取第i列的值,如果值为空，则返回Decimal.MinValue
//        /// </summary>
//        /// <param name="dataReader">要读取数据的dataReader</param>
//        /// <param name="i">dataReader中的列索引</param>
//        /// <returns></returns>
//        public static decimal? GetDecimalNullable(System.Data.IDataReader dataReader, int i)
//        {
//            if (dataReader.IsDBNull(i))
//            {
//                return null;
//            }
//            return dataReader.GetDecimal(i);
//        }

//        /// <summary>
//        /// 直接从dataReader中读取第i列的值,如果值为空，则返回Decimal.MinValue
//        /// </summary>
//        /// <param name="dataReader">要读取数据的dataReader</param>
//        /// <param name="i">dataReader中的列索引</param>
//        /// <returns></returns>
//        public static decimal GetDecimal(System.Data.IDataReader dataReader, int i)
//        {
//            if (dataReader.IsDBNull(i))
//            {
//                return Decimal.MinValue;
//            }
//            return dataReader.GetDecimal(i);
//        }

//        /// <summary>
//        /// 直接从dataReader中读取第i列的值,如果值为空，则返回空串
//        /// </summary>
//        /// <param name="dataReader">要读取数据的dataReader</param>
//        /// <param name="i">dataReader中的列索引</param>
//        /// <returns></returns>
//        public static string GetString(System.Data.IDataReader dataReader, int i)
//        {
//            if (dataReader.IsDBNull(i))
//                return "";

//            //return i.ToString();
//            return dataReader.GetString(i);
//        }

//        /// <summary>
//        /// 直接从dataReader中读取第i列的值
//        /// </summary>
//        /// <param name="dataReader">要读取数据的dataReader</param>
//        /// <param name="i">dataReader中的列索引</param>
//        /// <returns></returns>
//        public static Guid GetGuid(System.Data.IDataReader dataReader, int i)
//        {
//            return dataReader.GetGuid(i);
//        }

//        /// <summary>
//        /// 直接从dataReader中读取第i列的值
//        /// </summary>
//        /// <param name="dataReader">要读取数据的dataReader</param>
//        /// <param name="i">dataReader中的列索引</param>
//        /// <returns></returns>
//        public static Guid? GetGuidNullable(System.Data.IDataReader dataReader, int i)
//        {
//            if (dataReader.IsDBNull(i))
//            {
//                return null;
//            }
//            return dataReader.GetGuid(i);
//        }
//    }

//    public static class ExcelReaderConvert
//    {
//        public static string ToString(object obj)
//        {
//            string resultStr = string.Empty;
//            if (obj == null || obj.ToString().Trim() == string.Empty)
//            {
//                return null;
//            }
//            resultStr = Convert.ToString(obj).Trim();

//            string regExp = @"<(?:[^><]|""[^""]""|'[^']')*>";
//            if (resultStr.Contains("<") && resultStr.Contains(">"))
//                resultStr = Regex.Replace(resultStr, regExp, "").Replace(" ", "").Replace("\r", "").Replace("\n", "");
//            else
//            {
//                //string regNormalString = @"^[A-Za-z0-9_,/&-—－()（）_\s\u4e00-\u9fa5]*$";
//                //增加全角半角符号
//                //string regNormalString = @"^[A-Za-z0-9_,/&-—－()（）_\s\u4e00-\u9fa5\u0000-\u00FF\uFF00-\uFFFF]*$";
//                //增加 。 ；  ， ： “ ”（ ） 、 ？ 《 》 这些中文标点符号
//                string regNormalString = @"^[A-Za-z0-9_,/&-—－()（）_\s\u4e00-\u9fa5\u3002\uff1b\uff0c\uff1a\u201c\u201d\uff08\uff09\u3001\uff1f\u300a\u300b]*$";
//                if (!Regex.IsMatch(resultStr, regNormalString))
//                    throw new BusinessException(string.Format("Excel中数据[" + resultStr + "]存在非法字符,请确认。"));
//            }
//            return resultStr;
//        }

//        public static DateTime ToDateTime(object obj)
//        {
//            if (obj == null)
//            {
//                return DateTime.MinValue;
//            }
//            return Convert.ToDateTime(obj);
//        }

//        public static DateTime? ToDateTimeNullable(object obj)
//        {
//            if (obj == null)
//                return null;
//            DateTime result;
//            if (DateTime.TryParse(obj.ToString(), out result))
//            {
//                return result;
//            }

//            return null;
//        }

//        public static int ToInt32(object obj)
//        {
//            if (obj == null)
//            {
//                return int.MinValue;
//            }
//            return Convert.ToInt32(obj);
//        }

//        public static int? ToInt32Nullable(object obj)
//        {
//            if (obj == null || string.IsNullOrEmpty(obj.ToString().Trim()))
//            {
//                return (int?)null;
//            }
//            return Convert.ToInt32(obj);
//        }

//        public static decimal? ToDecimalNullable(object obj)
//        {
//            if (obj == null || string.IsNullOrEmpty(obj.ToString().Trim()))
//            {
//                return (decimal?)null;
//            }
//            return Convert.ToDecimal(obj);
//        }
//    }
//}
