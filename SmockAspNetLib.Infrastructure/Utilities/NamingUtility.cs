//using System;
//using SmockAspNetLib.Infrastructure.Extentions;

//namespace SmockAspNetLib.Infrastructure.Utilities
//{
//    /// <summary>
//    /// 命名通用类
//    /// </summary>
//    public class NamingUtility
//    {
//        private static string EntityPrefix = string.Empty;
//        private static string entitySuffix = string.Empty;
//        private static string DefaultEntitySuffix = "Entity";
//        private static string FieldSuffix = "Field";

//        /// <summary>
//        /// 得到Entity类名称
//        /// </summary>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public static string GetEntityCalssName(string name)
//        {
//            if (EntitySuffix.Length > 0)
//            {
//                return GetClassName(name) + EntitySuffix;
//            }
//            else
//            {
//                return GetClassName(name) + DefaultEntitySuffix;
//            }
//        }

//        /// <summary>
//        /// 得到类名称
//        /// </summary>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public static string GetClassName(string name)
//        {
//            return name.Substring(0, 1).ToUpper() + name.Substring(1);
//        }

//        /// <summary>
//        /// 得到属性名称
//        /// </summary>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public static string GetProertyName(string name)
//        {
//            if (name.Length < 1)
//            {
//                return name;
//            }
//            return name.Substring(0, 1).ToUpper() + name.Substring(1);
//        }

//        /// <summary>
//        /// 得到Field名称
//        /// </summary>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public static string GetFieldName(string name)
//        {
//            return name.ToCamelCaseName()+ FieldSuffix;
//        }

//        /// <summary>
//        /// 实体后缀（如果不设置或为空有默认设置）
//        /// </summary>
//        public static string EntitySuffix
//        {
//            get
//            {
//                return entitySuffix;
//            }
//            set
//            {
//                entitySuffix = value;
//            }
//        }
//    }
//}
