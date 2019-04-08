using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Linq.Expressions;
//using SmockAspNetLib.Infrastructure.Reflection;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    /// <summary>
    /// 自定义属性通用类
    /// </summary>
    public static class AttributeUtility
    {
        static Dictionary<string, object> EntityAttrDict = new Dictionary<string, object>();
        static Dictionary<string, Dictionary<string, object>> ProptertyAttrDict = new Dictionary<string, Dictionary<string, object>>();


        /// <summary>
        /// 得到类的DisplayName
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.String.</returns>
        public static string GetEntityDisplayName<T>()
        {
            string name = "";

            DisplayNameAttribute attr = GetEntityDisplayNameAttribute<T>();

            if (attr != null)
            {
                name = attr.DisplayName;
            }

            return name;
        }
        /// <summary>
        /// 得到DisplayNameAttribute
        /// </summary>
        /// <param name="customAttributes"></param>
        /// <returns></returns>
        public static DisplayNameAttribute GetEntityDisplayNameAttribute<T>()
        {
            var type = typeof(T);
            string typeName = type.FullName;
            if (EntityAttrDict.ContainsKey(typeName))
            {
                return (DisplayNameAttribute)EntityAttrDict[typeName];
            }

            DisplayNameAttribute attr = typeof(T).GetCustomAttribute<DisplayNameAttribute>();


            return attr;
        }

        public static TAttribute GetEntityAttribute<T, TAttribute>() where TAttribute : Attribute
        {
            var type = typeof(T);
            string typeName = type.FullName;
            if (EntityAttrDict.ContainsKey(typeName))
            {
                return (TAttribute)EntityAttrDict[typeName];
            }

            TAttribute attr = type.GetCustomAttribute<TAttribute>();
            if (attr != null)
            {
                EntityAttrDict[typeName] = attr;
            }

            return attr;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the t attribute.</typeparam>
        /// <param name="member">The member.</param>
        /// <param name="isRequired">if set to <c>true</c> [is required].</param>
        /// <returns>TAttribute.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo member, bool isRequired) where TAttribute : Attribute
        {
            var type = typeof(TAttribute);
            var attribute = member.GetCustomAttributes(type, false).SingleOrDefault();

            if (attribute == null && isRequired)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture, "The {0} attribute must be defined on member {1}",
                        type.Name, member.Name));
            }

            return (TAttribute)attribute;
        }

        /// <summary>
        /// Gets the get property attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TAttribute">The type of the t attribute.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>TAttribute.</returns>
        /// <exception cref="System.ArgumentException">No property reference expression was found.;propertyExpression</exception>
        public static TAttribute GetGetPropertyAttribute<T, TAttribute>(Expression<Func<T, object>> propertyExpression) where TAttribute : Attribute
        {
            var memberInfo = GetPropertyInformation(propertyExpression.Body);
            if (memberInfo == null)
            {
                throw new ArgumentException(
                    "No property reference expression was found.",
                    "propertyExpression");
            }

            var attr = memberInfo.GetAttribute<TAttribute>(false);


            return attr;
        }

        /// <summary>
        /// Gets the display name of the property.
        /// </summary>
        /// <typeparam name="T">The type of the t attribute.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentException">No property reference expression was found.;propertyExpression</exception>
        public static string GetPropertyDisplayName<T>(Expression<Func<T, object>> propertyExpression)
        {
            var attr= GetGetPropertyAttribute<T, DisplayNameAttribute>(propertyExpression);

            if (attr != null)
            {
                return attr.DisplayName;
            }

            return "";
        }

        /// <summary>
        /// Gets the property information.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>MemberInfo.</returns>
        public static MemberInfo GetPropertyInformation(Expression propertyExpression)
        {
            MemberExpression memberExpr = propertyExpression as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyExpression as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                {
                    memberExpr = unaryExpr.Operand as MemberExpression;
                }
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
            {
                return memberExpr.Member;
            }

            return null;
        }

        ///// <summary>
        ///// 是否可以序列化
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public static bool IsSerializable(Type type)
        //{
        //    return (GetSerializableAttribute(type.GetCustomAttributes(true)) == null) ? false : true;
        //}

        ///// <summary>
        ///// 是否DataContractAttribute
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //public static bool IsDataContract(Type type)
        //{
        //    return (GetDataContractAttribute(type.GetCustomAttributes(true)) == null) ? false : true;
        //}

        ///// <summary>
        ///// 是否DataMemberAttribute
        ///// </summary>
        ///// <param name="pi"></param>
        ///// <returns></returns>
        //public static bool IsDataMember(PropertyInfo pi)
        //{
        //    return (GetDataMemberAttribute(pi.GetCustomAttributes(true)) == null) ? false : true;
        //}

        ///// <summary>
        ///// 得到SerializableAttribute
        ///// </summary>
        ///// <param name="customAttributes"></param>
        ///// <returns></returns>
        //public static SerializableAttribute GetSerializableAttribute(object[] customAttributes)
        //{
        //    return (SerializableAttribute)GetCustomAttribute(customAttributes, typeof(SerializableAttribute));
        //}
        ///// <summary>
        ///// 得到DataContractAttribute
        ///// </summary>
        ///// <param name="customAttributes"></param>
        ///// <returns></returns>
        //public static DataContractAttribute GetDataContractAttribute(object[] customAttributes)
        //{
        //    return (DataContractAttribute)GetCustomAttribute(customAttributes, typeof(DataContractAttribute));
        //}

        ///// <summary>
        ///// 得到DataMemberAttribute
        ///// </summary>
        ///// <param name="customAttributes"></param>
        ///// <returns></returns>
        //public static DataMemberAttribute GetDataMemberAttribute(object[] customAttributes)
        //{
        //    return (DataMemberAttribute)GetCustomAttribute(customAttributes, typeof(DataMemberAttribute));
        //}

        ///// <summary>
        ///// 得到自定义属性
        ///// </summary>
        ///// <param name="pi"></param>
        ///// <param name="attributeType"></param>
        ///// <returns></returns>
        //public static object GetCustomAttribute(PropertyInfo pi, Type attributeType)
        //{
        //    return GetCustomAttribute(pi.GetCustomAttributes(true), attributeType);
        //}

        ///// <summary>
        ///// 得到自定义属性值
        ///// </summary>
        ///// <param name="pi"></param>
        ///// <param name="attributeType"></param>
        ///// <param name="propertyName"></param>
        ///// <returns></returns>
        //public static object GetCustomAttributeValue(PropertyInfo pi, Type attributeType, string propertyName)
        //{
        //    return GetCustomAttributeValue(pi.GetCustomAttributes(attributeType, true), attributeType, propertyName);
        //}


        ///// <summary>
        ///// 得到自定义属性
        ///// </summary>
        ///// <param name="customAttributes"></param>
        ///// <param name="attributeType"></param>
        ///// <returns></returns>
        //public static object GetCustomAttribute(object[] customAttributes, Type attributeType)
        //{
        //    object re = null;

        //    if (customAttributes == null || customAttributes.Length < 1)
        //    {
        //        return null;
        //    }

        //    foreach (var item in customAttributes)
        //    {
        //        if (item.GetType().Name == attributeType.Name)
        //        {
        //            re = item;
        //        }
        //    }

        //    return re;
        //}

        ///// <summary>
        ///// 得到自定义属性值
        ///// </summary>
        ///// <param name="customAttributes"></param>
        ///// <param name="attributeType"></param>
        ///// <param name="propertyName"></param>
        ///// <returns></returns>
        //public static object GetCustomAttributeValue(object[] customAttributes, Type attributeType, string propertyName)
        //{
        //    object reval = GetCustomAttribute(customAttributes, attributeType);
        //    if (reval == null)
        //    {
        //        return null;
        //    }

        //    PropertyInfo[] properties = reval.GetType().GetProperties();

        //    foreach (PropertyInfo item in properties)
        //    {
        //        if (item.Name == propertyName)
        //        {
        //            return item.GetValue(reval);
        //        }
        //    }

        //    return null;
        //}
        //TODO:通过自定义属性得到所有属性，得到某个属性
    }
}
