//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Reflection;

//using SmockAspNetLib.Infrastructure.Attributes;

//namespace SmockAspNetLib.Infrastructure.Utilities
//{
//    /// <summary>
//    /// 枚举通用类
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public class EnumUtility<T>
//    {
//        private const char Seperator = ',';
//        private const string Space = " ";

//        #region 得到枚举
//        /// <summary>
//        /// 通过数据得到枚举
//        /// </summary>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        public static T Parse(int value)
//        {
//            return (T)Enum.Parse(typeof(T), value.ToString(), true);
//        }

//        /// <summary>
//        /// 通过名称得到枚举
//        /// </summary>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public static T Parse(string name)
//        {
//            return (T)Enum.Parse(typeof(T), name, true);
//        }

//        /// <summary>
//        /// 通过名称得到枚举
//        /// </summary>
//        /// <param name="name"></param>
//        /// <param name="isDisplayName"></param>
//        /// <returns></returns>
//        public static T Parse(string name, bool isDisplayName)
//        {
//            if (!isDisplayName)
//            {
//                return Parse(name);
//            }

//            return (T)Parse(GetValue(name, isDisplayName));
//        }
//        #endregion

//        #region 名称、显示名称
//        /// <summary>
//        /// 得到所有的枚举值及枚举名称
//        /// </summary>
//        /// <returns></returns>
//        public static Dictionary<int, string> GetValueNames()
//        {
//            Dictionary<int, string> reval = new Dictionary<int, string>();

//            foreach (int value in Enum.GetValues(typeof(T)))
//            {
//                reval.Add(value, Parse(value).ToString().Replace(Space, string.Empty));
//            }

//            return reval;
//        }

//        /// <summary>
//        /// 得到所有的枚举值及枚举显示名称
//        /// </summary>
//        /// <returns></returns>
//        public static Dictionary<int, string> GetValueDisplayNames()
//        {
//            Dictionary<int, string> reval = new Dictionary<int, string>();

//            foreach (int value in Enum.GetValues(typeof(T)))
//            {
//                T en = Parse(value);
//                EnumValueAttribute[] atts = GetValueAttribute(en);
//                if (atts != null && atts.Length > 0)
//                {
//                    reval[value] = atts[0].DisplayName;
//                }
//                else
//                {
//                    reval[value] = en.ToString().Replace(Space, string.Empty);
//                }
//            }

//            return reval;
//        }

//        /// <summary>
//        /// 通过枚举值得到显示名称
//        /// </summary>
//        /// <param name="en"></param>
//        /// <returns></returns>
//        public static string GetDisplayName(T en)
//        {
//            EnumValueAttribute[] atts = GetValueAttribute(en);

//            if (atts != null)
//            {
//                string reval = string.Empty;
//                foreach (EnumValueAttribute item in atts)
//                {
//                    if (reval.Length > 0)
//                    {
//                        reval += Seperator;
//                    }
//                    reval += item.DisplayName;
//                }
//                return reval;
//            }

//            return en.ToString().Replace(Space, string.Empty);
//        }

//        /// <summary>
//        /// 通过枚举值得到显示名称
//        /// </summary>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        public static string GetDisplayName(int value)
//        {
//            return GetDisplayName(Parse(value));
//        }


//        /// <summary>
//        /// 通过ID得到显示名称
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public static string GetDisplayName(Guid id)
//        {
//            string strid = id.ToString();
//            Dictionary<int, EnumValueAttribute> atts = GetValueAttributes();
//            foreach (EnumValueAttribute item in atts.Values)
//            {
//                if (string.Compare(item.ID, strid) == 0)
//                {
//                    return item.DisplayName;
//                }
//            }

//            return string.Empty;
//        }

//        /// <summary>
//        /// 通过枚举名称得到显示名称
//        /// </summary>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public static string GetDisplayName(string name)
//        {
//            return GetDisplayName(Parse(name));
//        }

//        /// <summary>
//        /// 得到枚举值
//        /// </summary>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public static int GetValue(string name)
//        {
//            return Convert.ToInt32(Parse(name));
//        }

//        /// <summary>
//        /// 得到枚举值
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public static int GetValue(Guid id)
//        {
//            foreach (KeyValuePair<int, EnumValueAttribute> item in GetValueAttributes())
//            {
//                if (item.Value.ID == id.ToString())
//                {
//                    return item.Key;
//                }
//            }

//            throw new Exception(string.Format("{0} 相对应的枚举值不存在", id));
//        }

//        /// <summary>
//        /// 通过显示名称得到枚举值
//        /// </summary>
//        /// <param name="name"></param>
//        /// <param name="isDisplayName"></param>
//        /// <returns></returns>
//        public static int GetValue(string name, bool isDisplayName)
//        {
//            if (!isDisplayName)
//            {
//                return GetValue(name);
//            }

//            Dictionary<int, string> valueNames = GetValueDisplayNames();
//            string[] names = name.Split(Seperator);
//            int reval = -1;

//            foreach (KeyValuePair<int, string> item in valueNames)
//            {
//                foreach (string str in names)
//                {
//                    if (string.Compare(item.Value, str, true) == 0)
//                    {
//                        if (reval < 0)
//                        {
//                            reval = item.Key;
//                        }
//                        else
//                        {
//                            reval |= item.Key;
//                        }
//                    }
//                }
//            }

//            if (reval < 0)
//            {
//                throw new Exception(string.Format("{0} 相对应的枚举值不存在", name));
//            }

//            return reval;
//        }
//        #endregion

//        #region 自定义属性
//        /// <summary>
//        /// 得到枚举类型自定义属性
//        /// </summary>
//        /// <returns></returns>
//        public static EnumTypeAttribute GetTypeAttribute()
//        {
//            return AttributeUtility.GetEntityAttribute<T,EnumTypeAttribute>();
//        }

//        /// <summary>
//        /// 得到枚举值自定义属性
//        /// </summary>
//        /// <returns></returns>
//        public static Dictionary<int, EnumValueAttribute> GetValueAttributes()
//        {
//            Dictionary<int, EnumValueAttribute> reval = new Dictionary<int, EnumValueAttribute>();

//            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
//            foreach (int value in Enum.GetValues(typeof(T)))
//            {
//                EnumValueAttribute[] att = GetValueAttribute(value);
//                if (att == null)
//                {
//                    continue;
//                }
//                reval.Add(value, att[0]);
//            }

//            return reval;
//        }

//        /// <summary>
//        /// 得到枚举值自定义属性
//        /// </summary>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        public static EnumValueAttribute[] GetValueAttribute(T value)
//        {
//            if (value == null)
//            {
//                throw new ArgumentNullException("value");
//            }

//            List<EnumValueAttribute> reval = new List<EnumValueAttribute>();
//            string fieldName = value.ToString().Replace(Space, string.Empty);
//            EnumValueAttribute attr = typeof(T).GetField(fieldName, BindingFlags.Static | BindingFlags.Public).GetAttribute<EnumValueAttribute>(false);
//            if (attr != null)
//            {
//                reval.Add(attr);
//            }

//            if (reval.Count > 0)
//            {
//                return reval.ToArray();
//            }
//            else
//            {
//                return null;
//            }
//        }

//        /// <summary>
//        /// 得到枚举值自定义属性
//        /// </summary>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        public static EnumValueAttribute[] GetValueAttribute(int value)
//        {
//            return GetValueAttribute(Parse(value));
//        }

//        /// <summary>
//        /// 得到枚举值自定义属性
//        /// </summary>
//        /// <param name="name"></param>
//        /// <returns></returns>
//        public static EnumValueAttribute[] GetValueAttribute(string name)
//        {
//            return GetValueAttribute(Parse(name));
//        }

//        /// <summary>
//        /// 得到枚举值自定义属性
//        /// </summary>
//        /// <param name="name"></param>
//        /// <param name="isDisplayName"></param>
//        /// <returns></returns>
//        public static EnumValueAttribute[] GetValueAttribute(string name, bool isDisplayName)
//        {
//            return GetValueAttribute(Parse(name, isDisplayName));
//        }
//        #endregion

//        public static string GetDisplayName(Enum eum)
//        {
//            var type = eum.GetType();//先获取这个枚举的类型
//            var field = type.GetField(eum.ToString());//通过这个类型获取到值
//            var obj = (DisplayAttribute)field.GetCustomAttribute(typeof(DisplayAttribute));//得到特性
//            return obj.Name ?? "";
//        }

//        #region 属性
//        /// <summary>
//        /// 默认值,未设置时返回-1
//        /// </summary>
//        public static int DefaultValue
//        {
//            get
//            {
//                int defaultValue = -1;

//                Dictionary<int, EnumValueAttribute> valueAtts = GetValueAttributes();
//                EnumTypeAttribute typeAtt = GetTypeAttribute();

//                foreach (KeyValuePair<int, EnumValueAttribute> keyValue in valueAtts)
//                {
//                    if (keyValue.Value.IsDefault)
//                    {
//                        if (defaultValue < 0)
//                        {
//                            defaultValue = 0;
//                        }

//                        defaultValue += keyValue.Key;

//                        if (typeAtt == null || typeAtt.EnumType == Types.EnumType.Sequence)
//                        {
//                            break;
//                        }
//                    }
//                }

//                return defaultValue;
//            }
//        }
//        #endregion

//        #region 列表控件
//        /*
//        /// <summary>
//        /// 绑定到列表控件
//        /// </summary>
//        /// <param name="control"></param>
//        public static void BindingListControl(ListControl control)
//        {
//            BindingListControl(control, DefaultValue);
//        }

//        /// <summary>
//        /// 绑定到列表控件并选中
//        /// </summary>
//        /// <param name="control"></param>
//        /// <param name="selected"></param>
//        public static void BindingListControl(ListControl control, string selected)
//        {
//            BindingListControl(control, Parse(selected));
//        }

//        /// <summary>
//        /// 绑定到列表控件并选中
//        /// </summary>
//        /// <param name="control"></param>
//        /// <param name="selected"></param>
//        public static void BindingListControl(ListControl control, T en)
//        {
//            BindingListControl(control, Convert.ToInt32(en));
//        }

//        /// <summary>
//        /// 绑定到列表控件并选中
//        /// </summary>
//        /// <param name="control"></param>
//        /// <param name="selected"></param>
//        public static void BindingListControl(ListControl control, int selected)
//        {
//            if (control == null)
//            {
//                throw new ArgumentNullException("control");
//            }

//            int value = selected;
//            bool isFlags = false;
//            object[] flags = typeof(T).GetType().GetCustomAttributes(typeof(FlagsAttribute), true);
//            if (flags != null && flags.Length == 1)
//            {
//                isFlags = true;
//            }

//            Dictionary<int, string> valNames = GetValueDisplayNames();

//            foreach (KeyValuePair<int, string> item in valNames)
//            {
//                ListItem li = new ListItem();
//                li.Text = item.Value;
//                li.Value = item.Key.ToString();

//                control.Items.Add(li);

//                if (value < 0)
//                {
//                    continue;
//                }

//                if (isFlags)
//                {
//                    if ((item.Key & value) == item.Key)
//                    {
//                        li.Selected = true;
//                    }
//                }
//                else
//                {
//                    if (item.Key == value)
//                    {
//                        li.Selected = true;
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// 得到选中的值
//        /// </summary>
//        /// <param name="control"></param>
//        /// <returns>返回集合</returns>
//        public static List<T> GetSelectedValues(ListControl control)
//        {
//            if (control == null)
//            {
//                throw new ArgumentNullException("control");
//            }

//            List<T> reval = new List<T>();

//            foreach (ListItem item in control.Items)
//            {
//                if (item.Selected)
//                {
//                    reval.Add(Parse(item.Value.Trim()));
//                }
//            }

//            return reval;
//        }

//        /// <summary>
//        /// 得到选中的值
//        /// </summary>
//        /// <param name="control"></param>
//        /// <returns>返回枚举</returns>
//        public static T GetSelectedValue(ListControl control)
//        {
//            int value = 0;
//            List<T> values = GetSelectedValues(control);
//            foreach (T item in values)
//            {
//                value = value | Convert.ToInt32(item);
//            }

//            return Parse(value);
//        }


//        /// <summary>
//        /// 将ID绑定到列表控件
//        /// </summary>
//        /// <param name="control"></param>
//        public static void BindingListControlGuid(ListControl control)
//        {
//            BindingListControlGuid(control, null);
//        }

//        /// <summary>
//        /// 将ID绑定到列表控件
//        /// </summary>
//        /// <param name="control"></param>
//        /// <param name="id"></param>
//        public static void BindingListControlGuid(ListControl control, Guid id)
//        {
//            BindingListControlGuid(control, new List<Guid>() { id });
//        }

//        /// <summary>
//        /// 将ID绑定到列表控件
//        /// </summary>
//        /// <param name="control"></param>
//        /// <param name="ids"></param>
//        public static void BindingListControlGuid(ListControl control, List<Guid> ids)
//        {
//            if (control == null)
//            {
//                throw new ArgumentNullException("control");
//            }

//            if (ids == null)
//            {
//                ids = new List<Guid>();
//            }

//            foreach (KeyValuePair<int, EnumValueAttribute> item in GetValueAttributes())
//            {
//                ListItem li = new ListItem();
//                li.Text = item.Value.DisplayName;
//                li.Value = item.Value.ID;

//                foreach (Guid id in ids)
//                {
//                    if (string.Compare(item.Value.ID, id.ToString(), true) == 0)
//                    {
//                        li.Selected = true;
//                    }
//                }

//                control.Items.Add(li);
//            }
//        }

//        /// <summary>
//        /// 得到选中的值
//        /// </summary>
//        /// <param name="control"></param>
//        /// <returns></returns>
//        public static List<Guid> GetSelectedGuidValues(ListControl control)
//        {
//            if (control == null)
//            {
//                throw new ArgumentNullException("control");
//            }

//            List<Guid> reval = new List<Guid>();

//            foreach (ListItem item in control.Items)
//            {
//                if (item.Selected)
//                {
//                    reval.Add(new Guid(item.Value));
//                }
//            }

//            return reval;
//        }

//        /// <summary>
//        /// 得到选中的值
//        /// </summary>
//        /// <param name="control"></param>
//        /// <returns></returns>
//        public static Guid GetSelectedGuidValue(ListControl control)
//        {
//            List<Guid> values = GetSelectedGuidValues(control);

//            if (values.Count > 0)
//            {
//                return values[0];
//            }

//            return Guid.Empty;
//        }*/
//        #endregion
//    }
//}
