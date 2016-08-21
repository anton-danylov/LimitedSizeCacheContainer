using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LimitedSizeCacheContainer;
using System.Linq;

namespace LimitedSizeCacheContainer.Test
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestLimit()
        {
            // Arrange
            LimitedSizeCacheContainer<string> cache = new LimitedSizeCacheContainer<string>(10);

            // Act
            Enumerable.Range(0, 20).ToList().ForEach(i => cache.AddItem(i.ToString(), i.ToString()));
            
            // Assert
            Assert.AreEqual(cache.Count, 10);
        }


        [TestMethod]
        public void TestAdding()
        {
            // Arrange
            LimitedSizeCacheContainer<Tuple<long, string>> cache = new LimitedSizeCacheContainer<Tuple<long, string>>(5);

            // Act
            cache.AddItem("1", new Tuple<long, string>(111, "qqqqqqqqq"));
            cache.AddItem("2", new Tuple<long, string>(222, "wwwwwww"));
            var tuple = cache.RetrieveItem("1");

            // Assert
            Assert.AreEqual(tuple.Item1, 111);
        }

        [TestMethod]
        public void TestRemoval()
        {
            // Arrange
            LimitedSizeCacheContainer<Tuple<long, string>> cache = new LimitedSizeCacheContainer<Tuple<long, string>>(5);

            // Act
            cache.AddItem("1", new Tuple<long, string>(111, "qqqqqqqqq"));
            cache.AddItem("2", new Tuple<long, string>(222, "wwwwwww"));
            cache.RemoveItem("1");

            // Assert
            Assert.IsTrue(!cache.IsItemInCache("1") && cache.RetrieveItem("2").Item1 == 222);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestDoubleAdd()
        {
            // Arrange
            LimitedSizeCacheContainer<string> cache = new LimitedSizeCacheContainer<string>(10);

            // Act
            cache.AddItem("q", "qqqqq");
            cache.AddItem("q", "qqqqq");

            // Assert
            Assert.IsTrue(false);
        }
    }
}
