using System;
using System.Reflection;

//using SmockAspNetLib.Infrastructure.Reflection;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    /// <summary>
    /// 工厂通用类
    /// </summary>
    public static class FactoryUtility<T>
    {
        /// <summary>
        /// 通过名称创建实例(T)为基类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetInstanceByName(string name)
        {
            Type type = typeof(T);
            return GetInstanceByName(Assembly.GetAssembly(type), type, name);
        }   
        /// <summary>
        /// 通过类名创建实例
        /// </summary>
        /// <param name="baseType">基类类型</param>
        /// <param name="name">子类名称</param>
        /// <returns></returns>
        public static object GetInstanceByName(Type baseType, string name)
        {
            return GetInstanceByName(Assembly.GetAssembly(baseType), baseType, name);
        }

        /// <summary>
        /// 通过名称创建实例
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="baseType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetInstanceByName(Assembly assembly, Type baseType, string name)
        {
            Type[] types = TypeUtility.GetAllDescentdant(assembly,baseType);

            foreach (Type t in types)
            {
                if (t.Name == name)
                {
                    return Activator.CreateInstance(t);
                }
            }

            //TODO：优化
            throw new ArgumentException(string.Format("创建实例失败！{0}不是{1}的子类", name, baseType.Name));
        }
    }
}
