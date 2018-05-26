using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroXmlSerializer.Tests
{
    [TestClass]
    public class EntityCollectionsTest
    {
        [TestMethod]
        public void IsCollection()
        {
            var ec = new EntityCollections();

            var arrayType = new int[0].GetType();
            Assert.IsTrue(ec.IsCollection(arrayType));

            arrayType = typeof(object).MakeArrayType();
            Assert.IsTrue(ec.IsCollection(arrayType));

            var collectionType = typeof(IList<>);
            Assert.IsTrue(ec.IsCollection(collectionType));

            collectionType = typeof(IList<>).MakeGenericType(typeof(object));
            Assert.IsTrue(ec.IsCollection(collectionType));
        }

        [TestMethod]
        public void GetItemType()
        {
            var ec = new EntityCollections();

            var array = new int?[0];
            Assert.AreEqual(typeof(int?), ec.GetItemType(array.GetType()));

            var collection = new Collection<object>();
            Assert.AreEqual(typeof(object), ec.GetItemType(collection.GetType()));

            Assert.ThrowsException<ArgumentException>(() => ec.GetItemType(typeof(string)));
        }

        [TestMethod]
        public void CreateCollection()
        {
            var ec = new EntityCollections();

            var items = new int[] { 1, 2, 3 };

            var arrayType = typeof(int).MakeArrayType();
            var array = ec.CreateCollection(arrayType, items);
            Assert.IsInstanceOfType(array, typeof(int[]));
            Assert.AreEqual(3, ((int[])array).Length);

            var listType = typeof(ICollection<>).MakeGenericType(new[] { typeof(int) });
            var list = ec.CreateCollection(listType, items);
            Assert.IsInstanceOfType(list, typeof(List<>).MakeGenericType(new[] { typeof(int) }));
            Assert.AreEqual(3, ((List<int>)list).Count);
        }
    }
}
