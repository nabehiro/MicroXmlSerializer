using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MicroXmlSerializer
{
    class MicroXmlSerializer
    {
        public MicroXmlSerializer()
        {
        }

        public T Deserialize<T>(string str)
        {
            var deserializer = new XElementDeserializer();
            return deserializer.Deserialize<T>(str);
        }

        

    }
}
