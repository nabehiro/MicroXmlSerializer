using MicroXmlSerializer.Serializers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MicroXmlSerializer
{
    abstract class EntityDescriptor
    {
        public Type EntityType { get; set; }
    }

    class SerializableEntityDescriptor : EntityDescriptor
    {
        public string XmlElementName { get; set; }
        public string XmlAttributeName { get; set; }
        public string Format { get; set; }
        public IEntitySerializer Serializer { get; set; }
    }

    class CollectionEntityDescriptor : EntityDescriptor
    {
        public string XmlElementName { get; set; }
        public string XmlItemElementName { get; set; }
        public Type ItemType { get; set; }
    }

    class CompositeEntityDescriptor : EntityDescriptor
    {
        public string XmlElementName { get; set; }

    }
}
