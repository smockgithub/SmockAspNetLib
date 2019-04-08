//using System;
//using System.IO;
//using System.Configuration;
//using System.Collections.Generic;
//using SmockAspNetLib.Infrastructure.Config;
//using SmockAspNetLib.Infrastructure.Extentions;

//namespace SmockAspNetLib.Infrastructure.Utilities
//{
//    /// <summary>
//    /// 配置文件通用类
//    /// </summary>
//    public static class ConfigUtility
//    {
//        #region 常量
//        private const string _dynamicConfigFileName = "Dynamic";
//        private const string _databaseConfigFileName = "Database";
//        private const string _webConfigFileName = "Web";
//        private const string _appConfigFileName = "App";
//        #endregion

//        #region 得到配置（先系统后扩展）
//        /// <summary>
//        /// 得到配置文件中AppSetting值并转化成Bool类型（先系统后扩展）
//        /// </summary>
//        /// <param name="appSettingKey"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static bool GetConfigBoolValue(string appSettingKey, string message = "true/false", bool canEmpty = false)
//        {
//            return GetConfigValue(appSettingKey, message, canEmpty).ToUpper().Trim().ParseBool();
//        }

//        /// <summary>
//        /// 得到配置文件中AppSetting值并转化成Guid类型（先系统后扩展）
//        /// </summary>
//        /// <param name="appSettingKey"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static Guid GetConfigGuidValue(string appSettingKey, string message = "Guid", bool canEmpty = false)
//        {
//            return GetConfigValue(appSettingKey, message, canEmpty).ToUpper().Trim().ParseGuid();
//        }

//        /// <summary>
//        /// 得到配置文件中AppSetting值并转化成Int类型（先系统后扩展）
//        /// </summary>
//        /// <param name="appSettingKey"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static int GetConfigIntValue(string appSettingKey, string message = "数字", bool canEmpty = false)
//        {
//            return GetConfigValue(appSettingKey, message, canEmpty).ToUpper().Trim().ParseInt();
//        }

//        /// <summary>
//        /// 得到配置文件中AppSetting值（先系统后扩展）
//        /// </summary>
//        /// <param name="appSettingKey">The application setting key.</param>
//        /// <param name="message">The message.</param>
//        /// <param name="canEmpty">if set to <c>true</c> [can empty].</param>
//        /// <returns>System.String.</returns>
//        /// <exception cref="Exception"></exception>
//        public static string GetConfigValue(string appSettingKey, string message, bool canEmpty = false)
//        {
//            string srcString = ConfigurationManager.AppSettings[appSettingKey];
//            try
//            {
//                if (DataUtility.IsInvalid(srcString))
//                {
//                    srcString = GetCustomConfigValue(_dynamicConfigFileName, appSettingKey, message, canEmpty);

//                }

//                if (DataUtility.IsInvalid(srcString))
//                {
//                    srcString = GetCustomConfigValue(_databaseConfigFileName, appSettingKey, message, canEmpty);
//                }
//            }
//            catch
//            {
//            }

//            if (!canEmpty && DataUtility.IsInvalid(srcString))
//            {
//                string configName = GetSystemConfigName();

//                throw new Exception(string.Format("请在{2}.config或Dynamic.config或Database.config中的configuration/appSettings段下配置Key={0}的节点，Value允许的值为：{1}.", appSettingKey, message, configName));
//            }

//            return srcString ?? (srcString = string.Empty);
//        }
//        #endregion

//        #region 得到系统配置
//        /// <summary>
//        /// 得到系统配置文件中AppSetting值转化成Bool类型
//        /// </summary>
//        /// <param name="appSettingKey"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static bool GetSystemConfigBoolValue(string appSettingKey, string message = "true/false", bool canEmpty = false)
//        {
//            return GetSystemConfigValue(appSettingKey, message, canEmpty).ToUpper().Trim().ParseBool();
//        }

//        /// <summary>
//        /// 得到系统配置文件中AppSetting值转化成Guid类型
//        /// </summary>
//        /// <param name="appSettingKey"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static Guid GetSystemConfigGuidValue(string appSettingKey, string message = "Guid", bool canEmpty = false)
//        {
//            return GetSystemConfigValue(appSettingKey, message, canEmpty).ToUpper().Trim().ParseGuid();
//        }

//        /// <summary>
//        /// 得到系统配置文件中AppSetting值转化成Int类型
//        /// </summary>
//        /// <param name="appSettingKey"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static int GetSystemConfigIntValue(string appSettingKey, string message = "数字", bool canEmpty = false)
//        {
//            return GetSystemConfigValue(appSettingKey, message, canEmpty).ToUpper().Trim().ParseInt();
//        }

//        /// <summary>
//        /// 得到系统配置文件中AppSetting值
//        /// </summary>
//        /// <param name="appSettingKey"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static string GetSystemConfigValue(string appSettingKey, string message, bool canEmpty = false)
//        {
//            string srcString = ConfigurationManager.AppSettings[appSettingKey];
//            if (!canEmpty && DataUtility.IsInvalid(srcString))
//            {
//                string configName = GetSystemConfigName();

//                throw new Exception(string.Format("请在{2}.config中的configuration/appSettings段下配置Key={0}的节点，Value允许的值为：{1}.", appSettingKey, message, configName));
//            }
//            if (srcString == null)
//            {
//                srcString = string.Empty;
//            }
//            return srcString;
//        }
//        #endregion

//        #region 得到扩展配置

//        /// <summary>
//        /// 得到自定义配置文件中AppSetting值转化成Bool类型
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="appSettingKey"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static bool GetCustomConfigBoolValue(string fileName, string appSettingKey, string message = "true/false", bool canEmpty = false)
//        {
//            return GetCustomConfigValue(fileName, appSettingKey, message, canEmpty).ToUpper().Trim().ParseBool();
//        }

//        /// <summary>
//        /// 得到自定义配置文件中AppSetting值转化成Guid类型
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="appSettingKey"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static Guid GetCustomConfigGuidValue(string fileName, string appSettingKey, string message = "Guid", bool canEmpty = false)
//        {
//            return GetCustomConfigValue(fileName, appSettingKey, message, canEmpty).ToUpper().Trim().ParseGuid();
//        }

//        /// <summary>
//        /// 得到自定义配置文件中AppSetting值转化成Int类型
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="appSettingKey"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static int GetCustomConfigIntValue(string fileName, string appSettingKey, string message = "数字", bool canEmpty = false)
//        {
//            return GetCustomConfigValue(fileName, appSettingKey, message, canEmpty).ToUpper().Trim().ParseInt();
//        }

//        /// <summary>
//        /// 得到自定义配置文件中AppSetting值
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="appSettingKey"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static string GetCustomConfigValue(string fileName, string appSettingKey, string message, bool canEmpty = false)
//        {
//            try
//            {
//                Configuration config = ConfigurationManager.OpenMappedMachineConfiguration(GetConfigurationFileMap(fileName));
//                string appSettings = config.AppSettings.Settings[appSettingKey].Value;
//                if (!canEmpty && DataUtility.IsInvalid(appSettings))
//                {
//                    throw new Exception($"请在{fileName}.config中的configuration/appSettings段下配置Key={appSettingKey}的节点，Value允许的值为：{message}.");
//                }

//                if (appSettings == null)
//                {
//                    appSettings = string.Empty;
//                }

//                return appSettings;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        #endregion

//        #region 得到自定义节点配置

//        /// <summary>
//        /// 得到自定义配置值
//        /// </summary>
//        /// <param name="sectionName"></param>
//        /// <param name="key"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static string GetCustomSettingValue(string sectionName, string key, string message, bool canEmpty = false)
//        {
//            return GetCustomSettingValue(string.Empty, sectionName, string.Empty, key, message, canEmpty);
//        }

//        /// <summary>
//        /// 得到自定义配置值
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="sectionName"></param>
//        /// <param name="key"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static string GetCustomSettingValue(string fileName, string sectionName, string key, string message, bool canEmpty = false)
//        {
//            return GetCustomSettingValue(fileName, sectionName, string.Empty, key, message, canEmpty);
//        }

//        /// <summary>
//        /// 得到自定义配置值
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="sectionName"></param>
//        /// <param name="settingName"></param>
//        /// <param name="key"></param>
//        /// <param name="message"></param>
//        /// <param name="canEmpty"></param>
//        /// <returns></returns>
//        public static string GetCustomSettingValue(string fileName, string sectionName, string settingName, string key, string message, bool canEmpty = false)
//        {
//            Dictionary<string, string> settings = GetCustomSetting(fileName, sectionName, sectionName);
//            if (settings.ContainsKey(key))
//            {
//                return settings[key];
//            }
//            else
//            {
//                if (canEmpty)
//                {
//                    return string.Empty;
//                }
//                else
//                {
//                    throw new Exception(string.Format("请在{0}文件中{1}下配置Key={2}的节点，Value允许的值为：{3}.", fileName, sectionName, key, message));
//                }
//            }
//        }

//        /// <summary>
//        /// 得到自定义Section下配置值
//        /// </summary>
//        /// <param name="sectionName"></param>
//        /// <returns></returns>
//        public static Dictionary<string, string> GetCustomSetting(string sectionName)
//        {
//            return GetCustomSetting(string.Empty, sectionName, string.Empty);
//        }

//        /// <summary>
//        /// 得到自定义Section下配置值
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="sectionName"></param>
//        /// <returns></returns>
//        public static Dictionary<string, string> GetCustomSetting(string fileName, string sectionName)
//        {

//            return GetCustomSetting(fileName, sectionName, string.Empty);
//        }

//        /// <summary>
//        /// 得到自定义Section下配置值
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <param name="sectionName"></param>
//        /// <param name="settingName"></param>
//        /// <returns></returns>
//        public static Dictionary<string, string> GetCustomSetting(string fileName, string sectionName, string settingName)
//        {
//            try
//            {
//                Dictionary<string, string> reval = new Dictionary<string, string>();

//                Configuration config;

//                if (fileName == string.Empty)
//                {
//                    config = ConfigUtility.GetSystemConfig();
//                }
//                else
//                {
//                    config = ConfigurationManager.OpenMappedMachineConfiguration(ConfigUtility.GetConfigurationFileMap(fileName));
//                }

//                UnifySettingsSection section = (UnifySettingsSection)config.GetSection(sectionName);

//                if (section == null || sectionName == string.Empty)
//                {
//                    foreach (ConfigurationSection tmp in config.Sections)
//                    {
//                        if (tmp is UnifySettingsSection)
//                        {
//                            section = (UnifySettingsSection)tmp;
//                            break;
//                        }
//                    }
//                }

//                if (settingName == string.Empty)
//                {
//                    settingName = section.DefaultSetting;
//                }

//                UnifySetting customSetting = section.CustomSettings[settingName];
//                UnifySettingItems items = customSetting.CustomItems;

//                int count = items.Count;
//                for (int i = 0; i < count; i++)
//                {
//                    UnifySettingItem item = items[i];
//                    if (item.Key.Length < 1)
//                    {
//                        continue;
//                    }

//                    reval[item.Key] = item.Value;
//                }


//                return reval;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        #endregion

//        #region 公共方法
//        /// <summary>
//        /// 配置文件地址
//        /// </summary>
//        /// <param name="fileName"></param>
//        /// <returns></returns>
//        public static ConfigurationFileMap GetConfigurationFileMap(string fileName)
//        {
//            ConfigurationFileMap fileMap = new ConfigurationFileMap();
//            fileMap.MachineConfigFilename = GetConfigFileFullName(fileName);

//            return fileMap;
//        }

//        /// <summary>
//        /// 得到系统配置文件
//        /// </summary>
//        /// <returns></returns>
//        public static Configuration GetSystemConfig()
//        {
//            Configuration config;
//            string webConfigPath = ConfigUtility.GetWebConfigFileFullName();
//            if (webConfigPath.Length > 0)
//            {
//                config = ConfigurationManager.OpenMappedMachineConfiguration(new ConfigurationFileMap(webConfigPath));
//            }
//            else
//            {
//                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
//            }

//            return config;
//        }

//        /// <summary>
//        /// 得到配置文件路径
//        /// </summary>
//        /// <returns></returns>
//        public static string GetWebOrAppConfigPath()
//        {
//            return SystemUtility.CurrentPath;
//        }

//        /// <summary>
//        /// 得到Web配置文件完整路径
//        /// </summary>
//        /// <returns></returns>
//        public static string GetWebConfigFileFullName()
//        {
//            try
//            {
//                return GetConfigFileFullName(_webConfigFileName);
//            }
//            catch
//            {
//                return "";
//            }
//        }

//        /// <summary>
//        /// 得到系统配置文件名
//        /// </summary>
//        /// <returns></returns>
//        public static string GetSystemConfigName()
//        {
//            if (!IsWebApp())
//            {
//                return _appConfigFileName;
//            }

//            return _webConfigFileName;
//        }

//        /// <summary>
//        /// 当前程序是否Web应用程序
//        /// </summary>
//        /// <returns></returns>
//        public static bool IsWebApp()
//        {
//            try
//            {
//                GetConfigFileFullName(_webConfigFileName);
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// 得到配置文件的完整路径
//        /// </summary>
//        /// <param name="name">The name.</param>
//        /// <returns>System.String.</returns>
//        /// <exception cref="Exception"></exception>
//        public static string GetConfigFileFullName(string name)
//        {
//            string fileName = Path.Combine(GetWebOrAppConfigPath(), $"{name}.config");

//            //TODO: 是否存在还与用户权限相关
//            if (!File.Exists(fileName))
//            {
//                throw new Exception($"文件不存在：{fileName}");
//            }
//            return fileName;
//        }

//        /// <summary>
//        /// 得到系统配置文件完整路径
//        /// </summary>
//        /// <returns></returns>
//        public static string GetConfigFileFullName()
//        {
//            return GetConfigFileFullName(GetSystemConfigName());
//        }

//        /// <summary>
//        /// 设置AppSettings配置文件
//        /// </summary>
//        /// <param name="key">键</param>
//        /// <param name="value">值</param>
//        public static void SetAppSetting(string key, string value)
//        {
//            Configuration config = GetSystemConfig();

//            config.AppSettings.Settings.Remove(key);
//            config.AppSettings.Settings.Add(key, value);
//            config.Save(ConfigurationSaveMode.Minimal);
//            ConfigurationManager.RefreshSection("appSettings");
//        }
//        #endregion

//        #region 属性
//        /// <summary>
//        /// 框架配置文件名
//        /// </summary>
//        public static string DynamicConfigFileName
//        {
//            get
//            {
//                return _dynamicConfigFileName;
//            }
//        }

//        /// <summary>
//        /// 数据数据库配置文件
//        /// </summary>
//        public static string DatabaseConfigFileName
//        {
//            get
//            {
//                return _databaseConfigFileName;
//            }
//        }

//        /// <summary>
//        /// 当前运行程序路径
//        /// </summary>
//        public static string CurrentPath
//        {
//            get
//            {
//                return SystemUtility.CurrentPath;
//            }
//        }
//        #endregion
//    }
//}
