using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using PointOfSaleTerminal;

namespace Test_PointOfSaleTerminal
{
    [TestClass]
    public class ItemPriceTest
    {
        private ItemPrice _itemPrice;

        [TestInitialize]
        public void Initialize_Class()
        {
            _itemPrice = new ItemPrice(1.25, 3, 3.00);

        }

        [TestMethod]
        public void Test_Instance_Type()
        {
            Assert.IsInstanceOfType(_itemPrice, typeof(ItemPrice));
        }

        [TestMethod]
        public void Test_Properties()
        {
            Assert.IsTrue(_itemPrice.UnitPrice == 1.25);
            Assert.IsTrue(_itemPrice.BulkQuantity == 3);
            Assert.IsTrue(_itemPrice.BulkCost == 3);
        }

        [TestMethod]
        public void Test_CalculatePrice_Quantity_LessThan_Bulk()
        {
            Assert.IsTrue(_itemPrice.CalculatePrice(2) == 2.50);
        }

        [TestMethod]
        public void Test_CalculatePrice_Quantity_Equals_Bulk()
        {
            Assert.IsTrue(_itemPrice.CalculatePrice(3) == 3.00);
        }

        [TestMethod]
        public void Test_CalculatePrice_Quantity_GreaterThan_Bulk()
        {
            Assert.IsTrue(_itemPrice.CalculatePrice(4) == 4.25);
        }

        [TestMethod]
        public void Test_Class_With_List()
        {
            var list1 = new List<ItemPrice>()
            {
                new ItemPrice(1.25, 3, 3.00),
                new ItemPrice(4.25),
                new ItemPrice(1.00, 6, 5.00),
                new ItemPrice(0.75)
            };
            Assert.IsTrue(list1[0].CalculatePrice(3) == 3.00);
            Assert.IsTrue(list1[1].CalculatePrice(4) == 17.00);
            Assert.IsTrue(list1[2].CalculatePrice(6) == 5.00);
            Assert.IsTrue(list1[3].CalculatePrice(8) == 6.00);
        }
    }
}
