//using System;
//using System.Configuration;
//using System.Collections.Generic;

////using SmockAspNetLib.Infrastructure.Types;

//namespace SmockAspNetLib.Infrastructure.Utilities
//{
//    /// <summary>
//    /// 日期时间通用类
//    /// </summary>
//    public static class DateTimeUtility
//    {
//        private readonly static string NumericDateTimeFormat = "yyyyMMddHHmmss";
//        private readonly static string NumericDateFormat = "yyyyMMdd";
//        private readonly static string NumericTimeFormat = "HHmmss";

//        #region Year
//        /// <summary>
//        /// 得到年的第一天零时
//        /// 例：0001-01-01 00:00:00
//        /// </summary>
//        /// <returns></returns>
//        public static DateTime GetYearFloor()
//        {
//            return GetYearFloor(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到年的第一天零时
//        /// 例：0001-01-01 00:00:00
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime GetYearFloor(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return new DateTime(dt.Year, 1, 1);
//        }

//        /// <summary>
//        /// 得到年的最后一天
//        /// 例：9999-12-31 23:59:59
//        /// </summary>
//        /// <returns></returns>
//        public static DateTime GetYearCeiling()
//        {
//            return GetYearCeiling(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到年的最后一天
//        /// 例：9999-12-31 23:59:59
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime GetYearCeiling(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return GetDayCeiling(GetYearFloor(dt).AddYears(1).AddDays(-1));
//        }
//        #endregion

//        #region Quarter
//        /// <summary>
//        /// 得到季度的第一天
//        /// </summary>
//        /// <returns></returns>
//        public static DateTime GetQuarterFirstDay()
//        {
//            return GetQuarterFirstDay(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到季度的第一天
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime GetQuarterFirstDay(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return GetMonthFirstDay(dt.AddMonths(-(dt.Month - 1) % 3));
//        }

//        /// <summary>
//        /// 得到季度的第一天
//        /// </summary>
//        /// <param name="quarterOffset"></param>
//        /// <returns></returns>
//        public static DateTime GetQuarterFirstDay(int quarterOffset)
//        {
//            return GetQuarterFirstDay(DateTime.Now, quarterOffset);
//        }

//        /// <summary>
//        /// 得到季度的第一天
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <param name="quarterOffset"></param>
//        /// <returns></returns>
//        public static DateTime GetQuarterFirstDay(DateTime dt, int quarterOffset)
//        {
//            return GetQuarterFirstDay(dt).AddMonths(quarterOffset * 3);
//        }

//        /// <summary>
//        /// 得到季度的最后一天
//        /// </summary>
//        /// <returns></returns>
//        public static DateTime GetQuarterLastDay()
//        {
//            return GetQuarterLastDay(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到季度的最后一天
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime GetQuarterLastDay(DateTime dt)
//        {
//            return GetMonthLastDay(dt.AddMonths((2 - (dt.Month - 1) % 3)));
//        }

//        /// <summary>
//        /// 得到季度的最后一天
//        /// </summary>
//        /// <param name="quarterOffset"></param>
//        /// <returns></returns>
//        public static DateTime GetQuarterLastDay(int quarterOffset)
//        {
//            return GetQuarterLastDay(DateTime.Now, quarterOffset);
//        }

//        /// <summary>
//        /// 得到季度的最后一天
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <param name="quarterOffset"></param>
//        /// <returns></returns>
//        public static DateTime GetQuarterLastDay(DateTime dt, int quarterOffset)
//        {
//            return GetQuarterLastDay(dt).AddMonths(quarterOffset * 3);
//        }

//        /// <summary>
//        /// 得到一年中的第几季度
//        /// </summary>
//        /// <returns></returns>
//        public static QuarterOfYear GetQuarterOfYear()
//        {
//            return GetQuarterOfYear(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到一年中的第几季度
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static QuarterOfYear GetQuarterOfYear(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            QuarterOfYear quarter = QuarterOfYear.FirstQuarter;
//            switch (dt.Month)
//            {
//                case 1:
//                case 2:
//                case 3:
//                    quarter = QuarterOfYear.FirstQuarter;
//                    break;
//                case 4:
//                case 5:
//                case 6:
//                    quarter = QuarterOfYear.SecondQuarter;
//                    break;
//                case 7:
//                case 8:
//                case 9:
//                    quarter = QuarterOfYear.ThirdQuarter;
//                    break;
//                case 10:
//                case 11:
//                case 12:
//                    quarter = QuarterOfYear.FourthQuarter;
//                    break;
//            }

//            return quarter;
//        }
//        #endregion

//        #region Month
//        /// <summary>
//        /// 得到月的第一天
//        /// </summary>
//        /// <returns></returns>
//        public static DateTime GetMonthFirstDay()
//        {
//            return GetMonthFirstDay(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到月的第一天
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime GetMonthFirstDay(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return dt.AddDays(-dt.Day).AddDays(1);
//        }

//        /// <summary>
//        /// 得到月的第一天
//        /// </summary>
//        /// <param name="monthOffset"></param>
//        /// <returns></returns>
//        public static DateTime GetMonthFirstDay(int monthOffset)
//        {
//            return GetMonthFirstDay(DateTime.Now, monthOffset);
//        }

//        /// <summary>
//        /// 得到月的第一天
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <param name="monthOffset"></param>
//        /// <returns></returns>
//        public static DateTime GetMonthFirstDay(DateTime dt, int monthOffset)
//        {
//            return GetMonthFirstDay(dt).AddMonths(monthOffset);
//        }

//        /// <summary>
//        /// 得到月的最后一天
//        /// </summary>
//        /// <returns></returns>
//        public static DateTime GetMonthLastDay()
//        {
//            return GetMonthLastDay(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到月的最后一天
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime GetMonthLastDay(DateTime dt)
//        {
//            return GetMonthFirstDay(dt).AddMonths(1).AddDays(-1);
//        }

//        /// <summary>
//        /// 得到月的最后一天
//        /// </summary>
//        /// <param name="monthOffset"></param>
//        /// <returns></returns>
//        public static DateTime GetMonthLastDay(int monthOffset)
//        {
//            return GetMonthLastDay(DateTime.Now, monthOffset);
//        }

//        /// <summary>
//        /// 得到月的最后一天
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <param name="monthOffset"></param>
//        /// <returns></returns>
//        public static DateTime GetMonthLastDay(DateTime dt, int monthOffset)
//        {
//            return GetMonthFirstDay(dt, monthOffset + 1).AddDays(-1);
//        }
//        #endregion

//        #region Week
//        /// <summary>
//        /// 添加周
//        /// </summary>
//        /// <param name="weekOffset"></param>
//        /// <returns></returns>
//        public static DateTime AddWeeks(int weekOffset)
//        {
//            return AddWeeks(DateTime.Now, weekOffset);
//        }

//        /// <summary>
//        /// 添加周
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <param name="weekOffset"></param>
//        /// <returns></returns>
//        public static DateTime AddWeeks(DateTime dt, int weekOffset)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return dt.AddDays(weekOffset * 7);
//        }

//        /// <summary>
//        /// 得到周的第一天
//        /// 注：周日为第一天
//        /// </summary>
//        /// <returns></returns>
//        public static DateTime GetWeekFirstDay()
//        {
//            return GetWeekFirstDay(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到周的第一天
//        /// 注：周日为第一天
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime GetWeekFirstDay(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return dt.AddDays(-(int)dt.DayOfWeek);
//        }

//        /// <summary>
//        /// 得到周的第一天
//        /// 注：周日为第一天
//        /// </summary>
//        /// <param name="weekOffset"></param>
//        /// <returns></returns>
//        public static DateTime GetWeekFirstDay(int weekOffset)
//        {
//            return GetWeekFirstDay(DateTime.Now, weekOffset);
//        }

//        /// <summary>
//        /// 得到周的第一天
//        /// 注：周日为第一天
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <param name="weekOffset"></param>
//        /// <returns></returns>
//        public static DateTime GetWeekFirstDay(DateTime dt, int weekOffset)
//        {
//            return AddWeeks(GetWeekFirstDay(dt), weekOffset);
//        }

//        /// <summary>
//        /// 得到周的最后一天
//        /// 注：周六为最后一天
//        /// </summary>
//        /// <returns></returns>
//        public static DateTime GetWeekLastDay()
//        {
//            return GetWeekLastDay(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到周的最后一天
//        /// 注：周六为最后一天
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime GetWeekLastDay(DateTime dt)
//        {
//            return GetWeekFirstDay(dt).AddDays((int)DayOfWeek.Saturday);
//        }

//        /// <summary>
//        /// 得到周的最后一天
//        /// 注：周六为最后一天
//        /// </summary>
//        /// <param name="weekOffset"></param>
//        /// <returns></returns>
//        public static DateTime GetWeekLastDay(int weekOffset)
//        {
//            return GetWeekLastDay(DateTime.Now, weekOffset);
//        }

//        /// <summary>
//        /// 得到周的最后一天
//        /// 注：周六为最后一天
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <param name="weekOffset"></param>
//        /// <returns></returns>
//        public static DateTime GetWeekLastDay(DateTime dt, int weekOffset)
//        {
//            return AddWeeks(GetWeekLastDay(dt), weekOffset);
//        }
//        #endregion

//        #region Day
//        /// <summary>
//        /// 得到天的第一刻
//        /// 例：9999-12-31 00：00：00
//        /// </summary>
//        /// <returns></returns>
//        public static DateTime GetDayFloor()
//        {
//            return GetDayFloor(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到天的第一刻
//        /// 例：9999-12-31 00：00：00
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime GetDayFloor(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
//        }

//        /// <summary>
//        /// 得到天的最后一刻
//        /// 例：9999-12-31 23：59：59
//        /// </summary>
//        /// <returns></returns>
//        public static DateTime GetDayCeiling()
//        {
//            return GetDayCeiling(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到天的最后一刻
//        /// 例：9999-12-31 23：59：59
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime GetDayCeiling(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }
//            return GetDayFloor(dt).AddDays(1).AddSeconds(-1);
//        }
//        #endregion

//        #region Date
//        /// <summary>
//        /// 星期中文
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static string GetDayOfWeekzh(DateTime dt)
//        {
//            return  EnumUtility<DayOfWeekType>.GetDisplayName(dt.DayOfWeek.ToString());
//        }

//        /// <summary>
//        /// 一个月的天数
//        /// </summary>
//        /// <param name="year"></param>
//        /// <param name="month"></param>
//        /// <returns></returns>
//        public static int GetDaysOfMonth(int year, int month)
//        {
//            switch (month)
//            {
//                case 1:
//                case 3:
//                case 5:
//                case 7:
//                case 8:
//                case 10:
//                case 12:
//                    return 0x1f;

//                case 2:
//                    if (!IsRunYear(year))
//                    {
//                        return 0x1c;
//                    }
//                    return 0x1d;

//                case 4:
//                case 6:
//                case 9:
//                case 11:
//                    return 30;
//            }
//            return -1;
//        }

//        /// <summary>
//        /// 是否润年
//        /// </summary>
//        /// <param name="year"></param>
//        /// <returns></returns>
//        public static bool IsRunYear(int year)
//        {
//            return ((((year % 4) == 0) && ((year % 100) != 0)) || ((year % 400) == 0));
//        }

//        /// <summary>
//        /// 得到日期时间
//        /// </summary>
//        /// <returns></returns>
//        public static string GetDateTime()
//        {
//            return GetDateTime(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到日期时间
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static string GetDateTime(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return dt.ToString(DefaultDateTimeFormat);
//        }

//        /// <summary>
//        /// 得到日期
//        /// </summary>
//        /// <returns></returns>
//        public static string GetDate()
//        {
//            return GetDate(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到日期
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static string GetDate(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return dt.ToString(DefaultDateFormat);
//        }
//        #endregion

//        #region Numeric
//        /// <summary>
//        /// 得到日期数字
//        /// 例：9999-12-31（99991231）
//        /// </summary>
//        /// <returns></returns>
//        public static int GetNumericDate()
//        {
//            return GetNumericDate(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到日期数字
//        /// 例：9999-12-31（99991231）
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static int GetNumericDate(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return Convert.ToInt32(dt.ToString(NumericDateFormat));
//        }

//        /// <summary>
//        /// 得到时间数字
//        /// 例：23:59:59（235959）
//        /// </summary>
//        /// <returns></returns>
//        public static int GetNumericTime()
//        {
//            return GetNumericTime(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到时间数字
//        /// 例：23:59:59（235959）
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static int GetNumericTime(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return Convert.ToInt32(dt.ToString(NumericTimeFormat));
//        }

//        /// <summary>
//        /// 得到日期时间数字
//        /// 例：9999-12-31 23:59:59（99991231235959）
//        /// </summary>
//        /// <returns></returns>
//        public static long GetNumericDateTime()
//        {
//            return GetNumericDateTime(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到日期时间数字
//        /// 例：9999-12-31 23:59:59（99991231235959）
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static long GetNumericDateTime(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return Convert.ToInt64(dt.ToString(NumericDateTimeFormat));
//        }
//        #endregion

//        #region Property
//        /// <summary>
//        /// 日期时间默认格式
//        /// 优先配置文件：DefaultDateTimeFormat
//        /// 默认：yyyy-MM-dd HH:mm:ss
//        /// </summary>
//        public static string DefaultDateTimeFormat
//        {
//            get
//            {
//                string format = ConfigUtility.GetConfigValue("DefaultDateTimeFormat", "日期时间默认格式", true);
//                if (!DataUtility.IsInvalid(format))
//                {
//                    return format;
//                }
//                return "yyyy-MM-dd HH:mm:ss";
//            }
//        }
//        /// <summary>
//        /// 日期默认格式
//        /// 优先配置文件：DefaultDateFormat
//        /// 默认格式：yyyy-MM-dd
//        /// </summary>
//        public static string DefaultDateFormat
//        {
//            get
//            {
//                string format = ConfigUtility.GetConfigValue("DefaultDateFormat", "日期默认格式", true);
//                if (!DataUtility.IsInvalid(format))
//                {
//                    return format;
//                }
//                return "yyyy-MM-dd";
//            }
//        }

//        /// <summary>
//        /// 时间默认格式
//        /// 优先配置文件：DefaultTimeFormat
//        /// 默认格式：HH:mm:ss
//        /// </summary>
//        public static string DefaultTimeFormat
//        {
//            get
//            {
//                string format = ConfigUtility.GetConfigValue("DefaultTimeFormat", "时间默认格式", true);
//                if (!DataUtility.IsInvalid(format))
//                {
//                    return format;
//                }
//                return "HH:mm:ss";
//            }
//        }

//        /// <summary>
//        /// 一秒的毫秒数
//        /// </summary>
//        public static int ASecondMillSeconds
//        {
//            get
//            {
//                return 0x3E8;
//            }
//        }

//        /// <summary>
//        /// 一分钟毫秒数
//        /// </summary>
//        public static int AMinuteMillSeconds
//        {
//            get
//            {
//                return 0xea60;
//            }
//        }

//        /// <summary>
//        /// 一小时毫秒数
//        /// </summary>
//        public static int AnHourMillSeconds
//        {
//            get
//            {
//                return 0x36ee80;
//            }
//        }

//        /// <summary>
//        /// 一分钟的秒数
//        /// </summary>
//        public static int AMinuteSeconds
//        {
//            get
//            {
//                return 0x3c;
//            }
//        }

//        /// <summary>
//        /// 一小时的秒数
//        /// </summary>
//        public static int AHourSeconds
//        {
//            get
//            {
//                return 0xe10;
//            }
//        }

//        /// <summary>
//        /// 一天的秒数
//        /// </summary>
//        public static int ADaySeconds
//        {
//            get
//            {
//                return 0x15180;
//            }
//        }
//        #endregion

//        #region Wrap
//        /// <summary>
//        /// 添加时间
//        /// </summary>
//        /// <param name="type"></param>
//        /// <param name="offset"></param>
//        /// <returns></returns>
//        public static DateTime AddOffset(TimeUnit type, int offset)
//        {
//            return AddOffset(type, offset, DateTime.Now);
//        }

//        /// <summary>
//        /// 添加时间
//        /// </summary>
//        /// <param name="type"></param>
//        /// <param name="offset"></param>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime AddOffset(TimeUnit type, int offset, DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException("dt");
//            }

//            DateTime reval = dt;

//            switch (type)
//            {
//                case TimeUnit.Millisecond:
//                    reval = dt.AddMilliseconds(offset);
//                    break;
//                case TimeUnit.Second:
//                    reval = dt.AddSeconds(offset);
//                    break;
//                case TimeUnit.Minute:
//                    reval = dt.AddMinutes(offset);
//                    break;
//                case TimeUnit.Hour:
//                    reval = dt.AddHours(offset);
//                    break;
//                case TimeUnit.Day:
//                    reval = dt.AddDays(offset);
//                    break;
//                case TimeUnit.Week:
//                    reval = AddWeeks(dt, offset);
//                    break;
//                case TimeUnit.Month:
//                    reval = dt.AddMonths(offset);
//                    break;
//                case TimeUnit.Quarter:
//                    reval = dt.AddMonths(3 * offset);
//                    break;
//                case TimeUnit.Year:
//                    reval = dt.AddYears(offset);
//                    break;
//            }

//            return reval;
//        }

//        /// <summary>
//        /// 得到第一时间
//        /// </summary>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        public static DateTime GetFirstDateTime(TimeUnit type)
//        {
//            return GetFirstDateTime(type, 0);
//        }

//        /// <summary>
//        /// 得到第一时间
//        /// </summary>
//        /// <param name="type"></param>
//        /// <param name="offset"></param>
//        /// <returns></returns>
//        public static DateTime GetFirstDateTime(TimeUnit type, int offset)
//        {
//            return GetFirstDateTime(type, offset, DateTime.Now);
//        }

//        /// <summary>
//        /// 得到第一时间
//        /// </summary>
//        /// <param name="type"></param>
//        /// <param name="offset"></param>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime GetFirstDateTime(TimeUnit type, int offset, DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException("dt");
//            }

//            DateTime reval = dt;

//            switch (type)
//            {
//                case TimeUnit.Millisecond:
//                    reval = dt.AddMilliseconds(offset);
//                    break;
//                case TimeUnit.Second:
//                    reval = dt.AddSeconds(offset);
//                    break;
//                case TimeUnit.Minute:
//                    reval = dt.AddMinutes(offset);
//                    reval = reval.AddSeconds(-reval.Second);
//                    break;
//                case TimeUnit.Hour:
//                    reval = dt.AddHours(offset);
//                    reval = reval.AddSeconds(-reval.Minute);
//                    reval = reval.AddSeconds(-reval.Second);
//                    break;
//                case TimeUnit.Day:
//                    reval = GetDayFloor(dt).AddDays(offset);
//                    break;
//                case TimeUnit.Week:
//                    reval = GetWeekFirstDay(dt, offset);
//                    break;
//                case TimeUnit.Month:
//                    reval = GetMonthFirstDay(dt, offset);
//                    break;
//                case TimeUnit.Quarter:
//                    reval = GetQuarterFirstDay(dt, offset);
//                    break;
//                case TimeUnit.Year:
//                    reval = GetYearFloor(dt);
//                    break;
//            }

//            return reval;
//        }

//        /// <summary>
//        /// 得到最后时间
//        /// </summary>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        public static DateTime GetLastDateTime(TimeUnit type)
//        {
//            return GetLastDateTime(type, 0);
//        }

//        /// <summary>
//        /// 得到最后时间
//        /// </summary>
//        /// <param name="type"></param>
//        /// <param name="offset"></param>
//        /// <returns></returns>
//        public static DateTime GetLastDateTime(TimeUnit type, int offset)
//        {
//            return GetLastDateTime(type, offset, DateTime.Now);
//        }

//        /// <summary>
//        /// 得到最后时间
//        /// </summary>
//        /// <param name="type"></param>
//        /// <param name="offset"></param>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static DateTime GetLastDateTime(TimeUnit type, int offset, DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException("dt");
//            }

//            DateTime reval = dt;

//            switch (type)
//            {
//                case TimeUnit.Millisecond:
//                    reval = dt.AddMilliseconds(offset);
//                    break;
//                case TimeUnit.Second:
//                    reval = dt.AddSeconds(offset);
//                    break;
//                case TimeUnit.Minute:
//                    reval = dt.AddMinutes(offset);
//                    reval = reval.AddSeconds(59 - reval.Second);
//                    break;
//                case TimeUnit.Hour:
//                    reval = dt.AddHours(offset);
//                    reval = reval.AddSeconds(59 - reval.Minute);
//                    reval = reval.AddSeconds(59 - reval.Second);
//                    break;
//                case TimeUnit.Day:
//                    reval = GetDayCeiling(dt).AddDays(offset);
//                    break;
//                case TimeUnit.Week:
//                    reval = GetWeekLastDay(dt, offset);
//                    break;
//                case TimeUnit.Month:
//                    reval = GetMonthLastDay(dt, offset);
//                    break;
//                case TimeUnit.Quarter:
//                    reval = GetQuarterLastDay(dt, offset);
//                    break;
//                case TimeUnit.Year:
//                    reval = GetYearCeiling(dt);
//                    break;
//            }

//            return reval;
//        }
//        #endregion

//        #region Time
//        /// <summary>
//        /// 得到时间
//        /// </summary>
//        /// <returns></returns>
//        public static string GetTime()
//        {
//            return GetTime(DateTime.Now);
//        }

//        /// <summary>
//        /// 得到时间
//        /// </summary>
//        /// <param name="dt"></param>
//        /// <returns></returns>
//        public static string GetTime(DateTime dt)
//        {
//            if (dt == null)
//            {
//                throw new ArgumentNullException();
//            }

//            return dt.ToString(DefaultTimeFormat);
//        }
//        #endregion
//    }
//}
