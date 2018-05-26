using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroXmlSerializer.Serializers;

namespace MicroXmlSerializer.Tests
{
    [TestClass]
    public class EntitySerializerProviderTest
    {
        [TestMethod]
        public void AddSerializer()
        {
            var provider = new EntitySerializerProvider();

            Assert.IsFalse(provider.IsSerializable(typeof(object)));

            var serializer = new EntitySerializer(typeof(object));
            provider.AddSerializer(typeof(object), serializer);

            Assert.AreEqual(serializer, provider.GetSerizliser(typeof(object)));
            Assert.IsTrue(provider.IsSerializable(typeof(object)));
        }

        [TestMethod]
        public void IsSerializable()
        {
            var provider = new EntitySerializerProvider();

            Assert.IsTrue(provider.IsSerializable(typeof(int)));
            Assert.IsTrue(provider.IsSerializable(typeof(int?)));
            Assert.IsTrue(provider.IsSerializable(typeof(string)));
            Assert.IsTrue(provider.IsSerializable(typeof(DateTime)));
            Assert.IsTrue(provider.IsSerializable(typeof(DateTime?)));

            Assert.IsTrue(provider.IsSerializable(typeof(BindingFlags)));
            Assert.IsTrue(provider.IsSerializable(typeof(BindingFlags?)));

            Assert.IsFalse(provider.IsSerializable(typeof(Nullable<>)));
            Assert.IsFalse(provider.IsSerializable(typeof(object)));
        }

        [TestMethod]
        public void GetSerializer()
        {
            var provider = new EntitySerializerProvider();
            
            var serializer1 = (EntitySerializer)provider.GetSerizliser(typeof(float));
            Assert.AreEqual(typeof(float), serializer1.TargetType);

            var serializer2 = (NullableEntitySerializer)provider.GetSerizliser(typeof(float?));
            Assert.AreEqual(typeof(float), (serializer2.BaseSerializer as EntitySerializer).TargetType);

            var serializer3 = (EnumSerializer)provider.GetSerizliser(typeof(BindingFlags));
            Assert.AreEqual(typeof(BindingFlags), serializer3.TargetType);

            var serializer4 = (NullableEntitySerializer)provider.GetSerizliser(typeof(BindingFlags?));
            Assert.AreEqual(typeof(BindingFlags), (serializer4.BaseSerializer as EnumSerializer).TargetType);

            Assert.IsNull(provider.GetSerizliser(typeof(object)));
        }
    }
}
