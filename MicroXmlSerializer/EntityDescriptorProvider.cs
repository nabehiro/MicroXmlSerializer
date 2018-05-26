using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using System.Linq;

namespace MicroXmlSerializer
{
    class EntityDescriptorProvider
    {
        private readonly EntitySerializerProvider _serializerProvider;
        private readonly EntityCollections _entityCollections;

        public EntityDescriptorProvider(EntitySerializerProvider serializerProvider, EntityCollections entityCollections)
        {
            _serializerProvider = serializerProvider;
            _entityCollections = entityCollections;
        }

        public EntityDescriptorProvider() : this(new EntitySerializerProvider(), new EntityCollections())
        { }

        public EntityDescriptor GetDescriptor(Type type)
        {
            return CreateDescriptor(type, type.GetCustomAttributes(), type.Name, null);
        }
        public EntityDescriptor GetDescriptor(PropertyInfo prop)
        {
            return CreateDescriptor(prop.PropertyType, prop.GetCustomAttributes(), null, prop.Name);
        }

        public EntityDescriptor CreateDescriptor(Type type, IEnumerable<Attribute> attributes, string typeName, string propName)
        {
            if (_serializerProvider.IsSerializable(type))
                return CreateSerializableDescriptor(type, attributes, propName ?? typeName);
            else if (_entityCollections.IsCollection(type))
                return CreateCollectionDescriptor(type, attributes, propName ?? "collection");
            else
                return CreateCompositeDescriptor(type, attributes, propName ?? typeName);
        }

        public SerializableEntityDescriptor CreateSerializableDescriptor(Type type, IEnumerable<Attribute> attributes, string defaultName)
        {
            var desc = new SerializableEntityDescriptor
            {
                EntityType = type,
                Serializer = _serializerProvider.GetSerizliser(type),
                Format = attributes.Find<XmlFormatAttribute>()?.Format,
                XmlAttributeName = attributes.Find<XmlAttributeAttribute>()?.AttributeName
            };

            if (desc.XmlAttributeName == null)
                desc.XmlElementName = GetElementName(attributes, defaultName);

            return desc;
        }
        
        public CollectionEntityDescriptor CreateCollectionDescriptor(Type type, IEnumerable<Attribute> attributes, string defaultName)
        {
            var desc = new CollectionEntityDescriptor
            {
                EntityType = type,
                XmlElementName = GetElementName(attributes, defaultName),
                XmlItemElementName = attributes.Find<XmlArrayItemAttribute>()?.ElementName,
                ItemType = _entityCollections.GetItemType(type)
            };

            if (desc.XmlItemElementName == null)
                desc.XmlItemElementName = GetElementName(desc.ItemType.GetCustomAttributes(), desc.ItemType.Name);

            return desc;
        }

        public CompositeEntityDescriptor CreateCompositeDescriptor(Type type, IEnumerable<Attribute> attributes, string defaultName)
        {
            var desc = new CompositeEntityDescriptor
            {
                EntityType = type,
                XmlElementName = GetElementName(attributes, defaultName)
            };

            return desc;
        }


        public string GetElementName(IEnumerable<Attribute> attributes, string defaultName)
        {
            return attributes.Find<XmlArrayAttribute>()?.ElementName ??
                attributes.Find<XmlElementAttribute>()?.ElementName ??
                attributes.Find<XmlRootAttribute>()?.ElementName ??
                defaultName;
        }

        public IEnumerable<EntityDescriptor> GetPropertyDescriptors(Type type)
        {
            return type.GetProperties()
                .Where(p => p.GetCustomAttribute<XmlIgnoreAttribute>() == null)
                .Select(p => GetDescriptor(p))
                .ToList();
        }
    }
}
