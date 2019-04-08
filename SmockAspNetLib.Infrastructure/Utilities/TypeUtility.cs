using System;
using System.Reflection;
using System.Collections.Generic;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    /// <summary>
    /// 类型通用类
    /// </summary>
    public static class TypeUtility
    {
        //private static Dictionary<string, List<Type>> DictTypes = new Dictionary<string, List<Type>>();
        /// <summary>
        /// 得到基类的所有子孙
        /// </summary>
        /// <param name="ancestorType"></param>
        /// <returns></returns>
        public static Type[] GetAllDescentdant(Type ancestorType)
        {
            return GetAllDescentdant(null, ancestorType);
        }

        /// <summary>
        /// 得到基类的所有子孙
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="ancestorType"></param>
        /// <returns></returns>
        public static Type[] GetAllDescentdant(Assembly assembly, Type ancestorType)
        {
            if (assembly == null)
            {
                assembly = ancestorType.Assembly;
            }                        

            List<Type> reval = new List<Type>();

            Type[] allTypes = assembly.GetTypes();

            foreach (Type t in allTypes)
            {
                if (IsDescentdantOf(ancestorType, t))
                {
                    reval.Add(t);
                }
            }

            

            return reval.ToArray();
        }

        /// <summary>
        /// 得到所有的子
        /// </summary>
        /// <param name="parentType"></param>
        /// <returns></returns>
        public static Type[] GetAllChildren(Type parentType)
        {
            return GetAllChildren(null, parentType);
        }

        /// <summary>
        /// 得到所有的子
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="parentType"></param>
        /// <returns></returns>
        public static Type[] GetAllChildren(Assembly assembly, Type parentType)
        {
            if (assembly == null)
            {
                assembly = parentType.Assembly;
            }

            List<Type> reval = new List<Type>();

            Type[] allTypes = assembly.GetTypes();

            foreach (Type t in allTypes)
            {
                if (IsChildOf(parentType, t))
                {
                    reval.Add(t);
                }
            }

            return reval.ToArray();
        }
        
        /// <summary>
        /// 是否同一类
        /// </summary>
        /// <param name="type1"></param>
        /// <param name="type2"></param>
        /// <returns></returns>
        public static bool IsSameType(Type type1, Type type2)
        {
            if (type1 == null || type2 == null)
            {
                throw new ArgumentNullException();
            }

            return (type1.FullName == type2.FullName) ? true : false;
        }

        /// <summary>
        /// 是否直接继承自某一类
        /// </summary>
        /// <param name="parentType"></param>
        /// <param name="childType"></param>
        /// <returns></returns>
        public static bool IsChildOf(Type parentType, Type childType)
        {
            if (parentType == null || childType == null)
            {
                throw new ArgumentNullException();
            }

            if (IsSameType(parentType, childType))
            {
                return false;
            }

            if (childType.IsSubclassOf(parentType))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 是否继承自某一类
        /// </summary>
        /// <param name="ancestorType">祖先类</param>
        /// <param name="descentdantType">子孙类</param>
        /// <returns></returns>
        public static bool IsDescentdantOf(Type ancestorType, Type descentdantType)
        {
            if (ancestorType == null || descentdantType == null)
            {
                throw new ArgumentNullException();
            }

            if (descentdantType == null || IsSameType(ancestorType, descentdantType) || descentdantType.BaseType == null)
            {
                return false;
            }

            if (IsSameType(ancestorType, descentdantType.BaseType))
            {
                return true;
            }
            else
            {
                return IsDescentdantOf(ancestorType, descentdantType.BaseType);
            }
        }
    }
}
