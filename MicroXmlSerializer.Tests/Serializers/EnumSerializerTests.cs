using System;
using System.Reflection;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroXmlSerializer.Serializers;

namespace MicroXmlSerializer.Tests.Serializers
{
    [TestClass]
    public class EnumSerializerTests
    {
        public enum Country
        {
            America = 0,
            [XmlEnum("JP")]
            Japan = 1,
        }

        [TestMethod]
        public void Deserialize()
        {
            var serializer = new EnumSerializer(typeof(Country));

            Assert.AreEqual(Country.America, serializer.Deserialize("America", null));
            Assert.AreEqual(Country.America, serializer.Deserialize("0", null));
            
            Assert.AreEqual(Country.Japan, serializer.Deserialize("JP", null));
            Assert.ThrowsException<FormatException>(() => serializer.Deserialize("Japan", null));
            Assert.ThrowsException<FormatException>(() => serializer.Deserialize("1", null));
        }
    }
}
