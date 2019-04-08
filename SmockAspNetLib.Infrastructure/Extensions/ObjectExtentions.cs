//using System;
//using System.Reflection;
//using System.Collections.Generic;

////using SmockAspNetLib.Infrastructure.Reflection;
//using SmockAspNetLib.Infrastructure.Utilities;

//namespace SmockAspNetLib.Infrastructure.Extentions
//{
//    /// <summary>
//    /// 对象扩展类
//    /// </summary>
//    public static class ObjectExtentions
//    {
//        /// <summary>
//        /// 是否为Null
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <returns></returns>
//        public static bool IsNull(this object obj)
//        {
//            return (obj == null || obj == DBNull.Value || (obj is Guid && (Guid)obj == Guid.Empty));
//        }

//        /// <summary>
//        /// 得到实体名
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <returns></returns>
//        public static string GetTypeName(this object obj)
//        {
//            return obj.GetType().Name;
//        }

//        /// <summary>
//        /// 通过属性名称得到值
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public static object GetPropertyValue<T>(this object obj, string name)
//        {
//            return RefectionUtility.GetPropertyValue<T>(obj, name);
//        }

//        /// <summary>
//        /// 得到所有的属性名值对
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <returns></returns>
//        public static Dictionary<string, object> GetPropertyValues(this object obj)
//        {
//            return RefectionUtility.GetPropertyValues(obj);
//        }

//        /// <summary>
//        /// 通过属性名称设置值
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <param name="name"></param>
//        /// <param name="value"></param>
//        public static void SetPropertyValue<T>(this object obj, string name, object value)
//        {
//            RefectionUtility.SetPropertyValue<T>(obj, name, value);
//        }

//        /// <summary>
//        /// 通过属性名称设置部分值
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <param name="keyValues"></param>
//        public static void SetPropertyValues<T>(this object obj, Dictionary<string, object> keyValues)
//        {
//            RefectionUtility.SetPropertyValues<T>(obj, keyValues);
//        }
//    }
//}
