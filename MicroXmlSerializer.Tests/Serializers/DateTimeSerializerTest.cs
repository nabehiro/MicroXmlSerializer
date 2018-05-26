using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicroXmlSerializer.Serializers;

namespace MicroXmlSerializer.Tests.Serializers
{
    [TestClass]
    public class DateTimeSerializerTest
    {
        [TestMethod]
        public void Deserialize()
        {
            var serializer = new DateTimeSerializer();

            Assert.AreEqual(new DateTime(2000, 1, 1), serializer.Deserialize("2000/1/1 00:00:00", null));
            Assert.ThrowsException<FormatException>(() => serializer.Deserialize("20000101", null));
            Assert.AreEqual(new DateTime(2000, 1, 1), serializer.Deserialize("20000101", "yyyyMMdd"));
        }
    }
}
