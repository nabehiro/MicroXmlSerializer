using System;
using System.Collections.Generic;
using System.Text;

namespace MicroXmlSerializer.Serializers
{
    class NullableEntitySerializer : IEntitySerializer
    {
        public IEntitySerializer BaseSerializer { get; }

        public NullableEntitySerializer(IEntitySerializer baseSerializer)
        {
            BaseSerializer = baseSerializer;
        }

        public object Deserialize(string str, string format)
        {
            return str == null ? null : BaseSerializer.Deserialize(str, format);
        }

        public string Serialize(object obj, string format)
        {
            throw new NotImplementedException();
        }
    }
}
