using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroXmlSerializer.Serializers;

namespace MicroXmlSerializer.Tests.Serializers
{
    [TestClass]
    public class EntitySerializerTest
    {
        [TestMethod]
        public void Deserialize()
        {
            var serializer = new EntitySerializer(typeof(int));

            Assert.AreEqual(1, serializer.Deserialize("1", null));

            Assert.ThrowsException<InvalidCastException>(() => serializer.Deserialize(null, null));
            Assert.ThrowsException<FormatException>(() => serializer.Deserialize("", null));
        }
    }
}
