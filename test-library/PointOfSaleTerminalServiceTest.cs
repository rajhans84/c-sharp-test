using System.Collections;
using System.Reflection;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using PointOfSaleTerminal;

namespace Test_PointOfSaleTerminal
{
    [TestClass]
    public class PointOfSaleTerminalServiceTest
    {
        private PointOfSaleTerminalService _posTermService;

        [TestInitialize]
        public void Initialize_Class()
        {
            _posTermService = new PointOfSaleTerminalService();
        }

        [TestMethod]
        public void Test_SetPricing_Stores_Item()
        {
            _posTermService.SetPricing('B', 4.25);
            _posTermService.SetPricing('A', 1.25, 3, 3.0);
            _posTermService.SetPricing('C', 1.00, 6, 5.0);
            _posTermService.SetPricing('D', 0.75);
            Hashtable table = _posTermService.GetPricingTable();
            Assert.IsTrue(table.ContainsKey('B') && table.ContainsKey('D') && table.ContainsKey('A') && table.ContainsKey('C'));
        }

        [TestMethod]
        public void Test_SetPricing_Stores_Returns_Correct_Pricing()
        {
            _posTermService.SetPricing('A', 1.25, 3, 3.0);
            _posTermService.SetPricing('B', 4.25);
            _posTermService.SetPricing('C', 1.00, 6, 5.0);
            _posTermService.SetPricing('D', 0.75);
            Hashtable table = _posTermService.GetPricingTable();
            ItemPrice itemPriceC = (ItemPrice)table['C'];
            Assert.IsInstanceOfType(itemPriceC, typeof(ItemPrice));
            Assert.IsTrue(itemPriceC.CalculatePrice(6) == 5.0);
        }
        [TestMethod]
        public void Test_ScanProduct_Scans_Products()
        {
            _posTermService.SetPricing('A', 1.25, 3, 3.0);
            _posTermService.SetPricing('B', 4.25);
            _posTermService.SetPricing('C', 1.00, 6, 5.0);
            _posTermService.SetPricing('D', 0.75);

            _posTermService.ScanProduct('A');
            _posTermService.ScanProduct('B');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('D');
            _posTermService.ScanProduct('A');
            _posTermService.ScanProduct('B');
            _posTermService.ScanProduct('A');
            Hashtable basket = _posTermService.GetProductBasket();
            int countA = (int)basket['A'];
            int countB = (int)basket['B'];
            int countC = (int)basket['C'];
            int countD = (int)basket['D'];
            Assert.IsTrue(
                countA == 3 &&
                countB == 2 &&
                countC == 1 &&
                countD == 1
                            );
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Product is not available in the pricing catalog")]
        public void Test_ScanProduct_Throws_Error_If_Pricing_Not_Found()
        {
            _posTermService.ScanProduct('X');
        }

        [TestMethod]
        public void Test_CalculateTotal_For_Scan_ABCDABA()
        {
            // Set pricing for A,B,C,D
            _posTermService.SetPricing('A', 1.25, 3, 3.0);
            _posTermService.SetPricing('B', 4.25);
            _posTermService.SetPricing('C', 1.00, 6, 5.0);
            _posTermService.SetPricing('D', 0.75);

            // Scan products in ABCDABA order
            _posTermService.ScanProduct('A');
            _posTermService.ScanProduct('B');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('D');
            _posTermService.ScanProduct('A');
            _posTermService.ScanProduct('B');
            _posTermService.ScanProduct('A');
            double total = _posTermService.CalculateTotal();
            Console.WriteLine(total);
            Assert.IsTrue(total == 13.25);
        }
        [TestMethod]
        public void Test_CalculateTotal_For_Scan_CCCCCCC()
        {
            // Set pricing for A,B,C,D
            _posTermService.SetPricing('A', 1.25, 3, 3.0);
            _posTermService.SetPricing('B', 4.25);
            _posTermService.SetPricing('C', 1.00, 6, 5.0);
            _posTermService.SetPricing('D', 0.75);

            // Scan products in CCCCCCC order
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('C');
            double total = _posTermService.CalculateTotal();
            Assert.IsTrue(total == 6.00);
        }
        [TestMethod]
        public void Test_CalculateTotal_For_Scan_ABCD()
        {
            // Set pricing for A,B,C,D
            _posTermService.SetPricing('A', 1.25, 3, 3.0);
            _posTermService.SetPricing('B', 4.25);
            _posTermService.SetPricing('C', 1.00, 6, 5.0);
            _posTermService.SetPricing('D', 0.75);

            // Scan products in ABCD order
            _posTermService.ScanProduct('A');
            _posTermService.ScanProduct('B');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('D');
            double total = _posTermService.CalculateTotal();
            Assert.IsTrue(total == 7.25);
        }
        [TestMethod]
        public void Test_CalculateTotal_For_Scan_ABCDABBAACCCCC()
        {
            // Set pricing for A,B,C,D
            _posTermService.SetPricing('A', 1.25, 3, 3.0);
            _posTermService.SetPricing('B', 4.25);
            _posTermService.SetPricing('C', 1.00, 6, 5.0);
            _posTermService.SetPricing('D', 0.75);

            // Scan products in ABCD order
            _posTermService.ScanProduct('A');
            _posTermService.ScanProduct('B');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('D');
            _posTermService.ScanProduct('A');
            _posTermService.ScanProduct('B');
            _posTermService.ScanProduct('B');
            _posTermService.ScanProduct('A');
            _posTermService.ScanProduct('A');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('C');
            _posTermService.ScanProduct('C');
            double total = _posTermService.CalculateTotal();
            Assert.IsTrue(total == 22.75);
        }
    }
}
