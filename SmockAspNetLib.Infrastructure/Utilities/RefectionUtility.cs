using System;
using System.Reflection;
using System.Collections.Generic;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    /// <summary>
    /// 反射通用类
    /// </summary>
    public static class RefectionUtility
    {
        static Dictionary<string, Dictionary<string, PropertyInfo>> PropertyInfoDict = new Dictionary<string, Dictionary<string, PropertyInfo>>();

        /// <summary>
        /// 得到属性信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo<T>(string name)
        {
            Dictionary<string, PropertyInfo> properties = GetProperties<T>();
            if (properties.ContainsKey(name))
            {
                return properties[name];
            }

            return null;
        }

        public static Dictionary<string, PropertyInfo> GetProperties<T>()
        {
            var type = typeof(T);
            string fn = type.FullName;
            if (PropertyInfoDict.ContainsKey(fn))
            {
                return PropertyInfoDict[fn];
            }

            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            Dictionary<string, PropertyInfo> re = new Dictionary<string, PropertyInfo>();

            foreach (var item in properties)
            {
                re[item.Name] = item;
            }

            return re;
        }

        /// <summary>
        /// 得到实例的所有属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetPropertyValues(object obj)
        {
            Dictionary<string, object> reval = new Dictionary<string, object>();

            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                reval.Add(pi.Name, pi.GetValue(obj));
            }

            return reval;
        }

        /// <summary>
        /// 得到实例的某一属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetPropertyValue<T>(object obj, string name)
        {
            PropertyInfo pi = GetPropertyInfo<T>(name);

            return pi.GetValue(obj);
        }

        /// <summary>
        /// 设置某一属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue<T>(object obj, string name, object value)
        {
            PropertyInfo prop = GetPropertyInfo<T>(name);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(obj, value);
            }
        }

        /// <summary>
        /// 设置部分属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetPropertyValues<T>(object obj, Dictionary<string, object> keyValues)
        {
            foreach (KeyValuePair<string, object> keyValue in keyValues)
            {
                SetPropertyValue<T>(obj, keyValue.Key, keyValue.Value);
            }
        }
    }
}
