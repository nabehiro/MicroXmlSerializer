using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MicroXmlSerializer.Serializers
{
    class EntitySerializer : IEntitySerializer
    {
        public Type TargetType { get; set; }

        public EntitySerializer(Type type)
        {
            TargetType = type;
        }
        public object Deserialize(string str, string format)
        {
            return Convert.ChangeType(str, TargetType, CultureInfo.InvariantCulture);
        }

        public string Serialize(object obj, string format)
        {
            throw new NotImplementedException();
        }
    }
}
