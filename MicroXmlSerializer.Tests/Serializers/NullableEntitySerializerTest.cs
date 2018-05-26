using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroXmlSerializer.Serializers;

namespace MicroXmlSerializer.Tests.Serializers
{
    [TestClass]
    public class NullableEntitySerializerTest
    {
        [TestMethod]
        public void Deserialize()
        {
            var serializer = new NullableEntitySerializer(new EntitySerializer(typeof(int)));

            Assert.AreEqual(1, serializer.Deserialize("1", null));
            Assert.IsNull(serializer.Deserialize(null, null));
            Assert.ThrowsException<FormatException>(() => serializer.Deserialize("", null));
        }
    }
}
