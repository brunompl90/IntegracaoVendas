using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace IntegracaoVendas.Dominio.Utils.Xmls
{
    public static class XmlUtils
    {


        public static String GetTagValue(String xml, string tagName)
        {
            var xElement = XElement.Parse(xml);
            return GetTagValue(xElement, tagName);
        }

        public static String GetTagValue(XElement xmlElement, string tagNameElement)
        {
            return GetElement(xmlElement, tagNameElement).Value;
        }

        public static String GetTagValueOrNull(String xml, string tagName)
        {
            return GetTagValueOrNull(LoadXml(xml), tagName);
        }

        public static String GetTagValueOrNull(XElement xml, string tagName)
        {
            var element = xml.XPathSelectElement(tagName);

            return element?.Value;
        }

        private static XElement GetElement(XElement xml, string tagName)
        {
            var element = xml.XPathSelectElement(tagName);

            if (element == null)
            {
                throw new ArgumentException(string.Format("Tag {0} não foi encontrada no xml passado por parâmetro!", tagName));
            }

            return element;
        }


        public static bool HasTag(XElement xml, string tagName)
        {
            var element = xml.XPathSelectElement(tagName);
            return element != null;
        }

        public static bool HasTag(String xml, string tagName)
        {
            var element = LoadXml(xml).XPathSelectElement(tagName);
            return element != null;
        }

        public static void SetTagValue(ref String xml, String tagName, String value)
        {
            var xElement = XElement.Parse(xml);

            SetTagValue(xElement, tagName, value);

            xml = xElement.ToString(SaveOptions.DisableFormatting);
        }


        public static string ConvertXml(XElement xml)
        {
            return xml.ToString(SaveOptions.DisableFormatting);
        }

        public static void SetTagValue(XElement xml, String tagName, String value)
        {
            value = value ?? String.Empty;

            value = SecurityElement.Escape(value);

            GetElement(xml, tagName).SetValue(value);
        }

        public static XElement LoadXml(String xml)
        {
            return XElement.Parse(xml);
        }

        public static XmlDocument LoadXmlAsDocument(String xml)
        {
            XmlDocument xDocument = new XmlDocument();
            xDocument.XmlResolver = null;
            xDocument.LoadXml(xml);
            return xDocument;
        }

        public static Dictionary<String, String> GetChildTags(String xml, String parentTag = null)
        {
            return GetChildTags(LoadXml(xml), parentTag);
        }

        public static Dictionary<String, String> GetChildTags(XElement xml, String parentTag = null)
        {
            Dictionary<String, String> childs = new Dictionary<string, string>();
            XElement parent = xml;

            if (!String.IsNullOrWhiteSpace(parentTag))
            {
                parent = GetElement(xml, parentTag);
            }

            parent.Descendants().ForEach(x => childs.Add(x.Name.ToString(), x.Value));

            return childs;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> Source, Action<T> action)
        {
            foreach (T item in Source)
            {
                action.Invoke(item);
            }
            return Source;
        }

        public static string PrettyXml(string xml)
        {
            var stringBuilder = new StringBuilder();

            var element = XElement.Parse(xml);

            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }

        public static String ConvertToXml(Object obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(obj.GetType());

            XmlDocument doc = new XmlDocument();
            doc.XmlResolver = null;

            System.IO.StringWriter sww = new System.IO.StringWriter();
            XmlWriter writer = XmlWriter.Create(sww);
            xsSubmit.Serialize(writer, obj);

            return sww.ToString();
        }

    }
}
