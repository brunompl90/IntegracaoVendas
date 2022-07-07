using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace IntegracaoVendas.Dominio.Utils.Xmls
{
    public class DynamicXmlBuilder
    {
        private readonly StringBuilder _Builder = new StringBuilder();
        private String _Preamble;

        public void AppendFile(String path)
        {
            var text = File.ReadAllText(path);

            var hasPreamble = text[1] == '?';
            if (hasPreamble)
            {
                var idxStart = text.IndexOf('\n');
                var lenSignificantPart = text.Length - idxStart;

                if (_Preamble == null)
                    _Preamble = text.Substring(0, idxStart);

                _Builder.Append(text, idxStart, lenSignificantPart);
                _Builder.AppendLine();
            }
            else
                _Builder.Append(text);
        }

        public DynamicXml GetDynamicXml()
        {
            _Builder.Insert(0, _Preamble + "<builded>");
            _Builder.Append("</builded>");

            var content = _Builder.ToString();
            return new DynamicXml(content);
        }
    }

    public class DynamicXml : DynamicObject, IEnumerable
    {
        private static readonly String SUM_NAME = "sumNode";

        private readonly List<XElement> _elements;
        private readonly XDocument _PrimaryRootDocument;

        public String Text { get; private set; }
        public ReadOnlyCollection<XElement> Elements
        {
            get
            {
                return _elements.AsReadOnly();
            }
        }

        public DynamicXml(String text)
            : this(XDocument.Load(text))
        {

        }

        private DynamicXml(XDocument primaryRoot)
        {
            _PrimaryRootDocument = primaryRoot;
            _elements = new List<XElement> { primaryRoot.Root };
            Text = XmlUtils.ConvertXml(primaryRoot.Root);
        }

        protected DynamicXml(XElement element)
        {
            _elements = new List<XElement> { element };
        }

        protected DynamicXml(IEnumerable<XElement> elements)
        {
            _elements = new List<XElement>(elements);
        }

        public override bool TryGetMember(GetMemberBinder binder, out Object result)
        {
            return TryGet(binder.Name, out result);
        }

        public bool TryGet(String binderName, out Object result)
        {
            if (CheckWellKnownNames(binderName, out result))
                return true;

            var attr = _elements[0].Attribute(XName.Get(binderName));
            if (attr != null)
            {
                result = attr.Value;
            }
            else
            {
                var items = _elements
                    .Elements()
                    .Where(_ => _.Name.LocalName.Equals(binderName));

                if (!items.Any())
                    return false;

                result = new DynamicXml(items);
            }

            return true;
        }

        private bool CheckWellKnownNames(String binderName, out object result)
        {
            result = null;

            if (binderName == "XmlValue")
                result = _elements[0].Value;
            else if (binderName == "XmlIdAtribute")
                result = _elements[0].Attribute("id").Value;

            else if (binderName == "XmlCount")
                result = _elements.Count;

            else if (binderName == "XmlName")
                result = _elements[0].Name.LocalName;

            else if (binderName == "XmlElements")
                result = new DynamicXml(_elements.Elements());

            else if (binderName == "XmlString")
                result = _elements[0].ToString(SaveOptions.DisableFormatting);

            else
                return false;

            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var node = indexes[0];

            var filteredElements = _elements.Elements().Where(x => x.Name.LocalName.Equals(node));

            result = filteredElements.Any();

            return true;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            if (!args.Any())
                throw new ArgumentException("args cannot be empty");

            var binderName = args[0] as String;
            if (binderName == null)
                throw new ArgumentException("args[0] must be of type System.String");

            if (_elements[0].Attribute(XName.Get(binderName)) != null)
            {
                result = true;
            }
            else
            {
                var items = _elements
                    .Descendants()
                    .Where(_ => _.Name.LocalName.Equals(binderName));

                result = items.Any();
            }

            return true;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var element in _elements)
                yield return new DynamicXml(element);
        }

        public static DynamicXml LoadFile(String path)
        {
            var doc = XDocument.Load(path, LoadOptions.None);
            return new DynamicXml(doc);
        }

        public static DynamicXml LoadStream(Stream stream)
        {
            if (stream.Position > 0)
                stream.Position = 0;

            var doc = XDocument.Load(stream, LoadOptions.None);
            return new DynamicXml(doc);
        }

        public static DynamicXml operator +(DynamicXml left, DynamicXml right)
        {
            if (left._PrimaryRootDocument == null || right._PrimaryRootDocument == null)
                throw new InvalidOperationException("A operação de soma entre dois objetos dinâmicos de XML só pode ser feita em objetos nascidos diretamente do Parsing de uma String e não filhos de subqueries em objetos antigos.");

            var leftRoot = left._PrimaryRootDocument.Root;
            var leftSumPart = leftRoot.Name.LocalName.Equals(SUM_NAME)
                 ? leftRoot.Elements().ToArray()
                 : new XElement[] { leftRoot };

            var rightRoot = right._PrimaryRootDocument.Root;
            var rightSumPart = rightRoot.Name.LocalName.Equals(SUM_NAME)
                 ? rightRoot.Elements().ToArray()
                 : new XElement[] { rightRoot };

            var newRoot = new XElement(SUM_NAME);
            foreach (var item in leftSumPart.Concat(rightSumPart))
                newRoot.Add(item);

            return new DynamicXml(new XDocument(newRoot));
        }
    }
}
