using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MicroXmlSerializer
{
    public class XElementDeserializer
    {
        public T Deserialize<T>(string str)
        {
            var root = XElement.Parse(str);
            return default(T);
        }
    }

}
