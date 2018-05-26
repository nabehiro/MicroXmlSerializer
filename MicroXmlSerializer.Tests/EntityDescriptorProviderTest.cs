using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroXmlSerializer.Serializers;

namespace MicroXmlSerializer.Tests
{
    [TestClass]
    public class EntityDescriptorProviderTest
    { 
        public class Animal
        {
            public string Pub_Ani_Str { get; set; }
            public int Pub_Ani_Int { get; set; }
            public int? Pub_Ani_NInt { get; set; }
            public Model Pub_Ani_Model { get; set; }
        }
        [XmlRoot("ROOT_CAT")]
        public class Cat : Animal
        {
            public string Pub_Str { get; set; }
            [XmlElement("ELEMENT")]
            [XmlFormat("FORMAT")]
            public int Pub_Int { get; set; }
            [XmlElement("ELEMENT")]
            [XmlAttribute("ATTRIBUTE")]
            public int? Pub_NInt { get; set; }
            public Model Pub_Model { get; set; }

            public List<Model> ModelList { get; set; }

            [XmlArray("MODEL_ARRAY")]
            [XmlArrayItem("MODEL_ITEM")]
            public Model[] ModelArray { get; set; }
        }
        public class Model { }


        [TestMethod]
        public void GetSerializableDescriptor()
        {
            var provider = new EntityDescriptorProvider();

            // Pub_Int
            var prop = typeof(Cat).GetProperty("Pub_Int");
            var desc = (SerializableEntityDescriptor)provider.GetDescriptor(prop);

            Assert.AreEqual(typeof(int), desc.EntityType);
            Assert.IsInstanceOfType(desc.Serializer, typeof(EntitySerializer));
            Assert.AreEqual("FORMAT", desc.Format);
            Assert.IsNull(desc.XmlAttributeName);
            Assert.AreEqual("ELEMENT", desc.XmlElementName);

            // Pub_NInt
            prop = typeof(Cat).GetProperty("Pub_NInt");
            desc = (SerializableEntityDescriptor)provider.GetDescriptor(prop);

            Assert.AreEqual(typeof(int?), desc.EntityType);
            Assert.IsInstanceOfType(desc.Serializer, typeof(NullableEntitySerializer));
            Assert.IsNull(desc.XmlElementName);
            Assert.AreEqual("ATTRIBUTE", desc.XmlAttributeName);

            // Pub_Str
            prop = typeof(Cat).GetProperty("Pub_Str");
            desc = (SerializableEntityDescriptor)provider.GetDescriptor(prop);

            Assert.AreEqual("Pub_Str", desc.XmlElementName);
        }

        [TestMethod]
        public void GetCompositeDescriptor()
        {
            var provider = new EntityDescriptorProvider();

            // Cat
            var desc = (CompositeEntityDescriptor)provider.GetDescriptor(typeof(Cat));

            Assert.AreEqual(typeof(Cat), desc.EntityType);
            Assert.AreEqual("ROOT_CAT", desc.XmlElementName);

            // Pub_Model
            desc = (CompositeEntityDescriptor)provider.GetDescriptor(typeof(Cat).GetProperty("Pub_Model"));
            Assert.AreEqual(typeof(Model), desc.EntityType);
            Assert.AreEqual("Pub_Model", desc.XmlElementName);
        }

        [TestMethod]
        public void GetCollectionDescriptor()
        {
            var provider = new EntityDescriptorProvider();

            // ModelList
            var desc = (CollectionEntityDescriptor)provider.GetDescriptor(typeof(Cat).GetProperty("ModelList"));

            Assert.AreEqual(typeof(List<>).MakeGenericType(new[] { typeof(Model) }), desc.EntityType);
            Assert.AreEqual("ModelList", desc.XmlElementName);
            Assert.AreEqual("Model", desc.XmlItemElementName);
            Assert.AreEqual(typeof(Model), desc.ItemType);

            // ModelArray
            desc = (CollectionEntityDescriptor)provider.GetDescriptor(typeof(Cat).GetProperty("ModelArray"));
            Assert.AreEqual(typeof(Model).MakeArrayType(), desc.EntityType);
            Assert.AreEqual("MODEL_ARRAY", desc.XmlElementName);
            Assert.AreEqual("MODEL_ITEM", desc.XmlItemElementName);
        }

        //[Ignore]
        [TestMethod]
        public void GetCollectionDescriptor_Type()
        {
            var provider = new EntityDescriptorProvider();

            // TODO: Unexpected name !!
            // int[]
            var desc = (CollectionEntityDescriptor)provider.GetDescriptor(typeof(int).MakeArrayType());
            Assert.AreEqual("Int32[]", desc.XmlElementName);
            Assert.AreEqual("Int32", desc.XmlItemElementName);
            Assert.AreEqual(typeof(int), desc.ItemType);
        }

    }
}
