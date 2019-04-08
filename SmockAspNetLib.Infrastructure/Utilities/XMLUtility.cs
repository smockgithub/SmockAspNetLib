using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace SmockAspNetLib.Infrastructure.Utilities
{
    /// <summary>
    /// XML公共类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class XMLUtility<T>
    {
        /// <summary>
        /// 将对象序列化为XML
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static XmlDocument Serializer(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            StringBuilder sb = new StringBuilder();
            XmlWriter xw = XmlWriter.Create(sb);
            ns.Add(string.Empty, string.Empty);
            serializer.Serialize(xw, obj, ns);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(sb.ToString());

            return xmldoc;
        }

        /// <summary>
        /// 将XML反序列化为对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize(string xml)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);

            return Deserialize(xmldoc);
        }
        
        /// <summary>
        /// 将XML反序列化为对象
        /// </summary>
        /// <param name="xmldoc"></param>
        /// <returns></returns>
        public static T Deserialize(XmlDocument xmldoc)
        {
            XmlNode xn = xmldoc.DocumentElement.ParentNode;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T obj = (T)serializer.Deserialize(new XmlNodeReader(xn));

            return obj;
        }
    }
}
