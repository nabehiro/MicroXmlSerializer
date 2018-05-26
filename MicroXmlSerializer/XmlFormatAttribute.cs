using System;
using System.Collections.Generic;
using System.Text;

namespace MicroXmlSerializer
{
    public class XmlFormatAttribute : Attribute
    {
        public string Format { get; set; }

        public XmlFormatAttribute(string format)
        {
            Format = format;
        }

        public XmlFormatAttribute()
        {
        }
    }
}
