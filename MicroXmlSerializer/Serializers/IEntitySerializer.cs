using System;
using System.Collections.Generic;
using System.Text;

namespace MicroXmlSerializer.Serializers
{
    interface IEntitySerializer
    {
        string Serialize(object obj, string format);
        object Deserialize(string str, string format);
    }
}
