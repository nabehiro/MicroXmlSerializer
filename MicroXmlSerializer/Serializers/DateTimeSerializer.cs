using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MicroXmlSerializer.Serializers
{
    class DateTimeSerializer : IEntitySerializer
    {
        public object Deserialize(string str, string format)
        {
            return string.IsNullOrEmpty(format)
                ? DateTime.Parse(str, CultureInfo.InvariantCulture)
                : DateTime.ParseExact(str, format, CultureInfo.InvariantCulture);
        }

        public string Serialize(object obj, string format)
        {
            throw new NotImplementedException();
        }
    }
}
