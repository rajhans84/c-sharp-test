using System.Collections;
using System;


namespace PointOfSaleTerminal
{
    public class PointOfSaleTerminalService
    {

        // Store prices of the products alongwith the bulk price deals
        public Hashtable PricingTable = new Hashtable();
        // Store scanned products and their count
        public Hashtable ProductBasket = new Hashtable();
        /// <summary>
        /// Setup pricing structure of the products available for scan
        /// If there is no bulk pricing then no need to enter it
        /// </summary>
        /// <param name="item">Product Item</param>
        /// <param name="unitprice">Unit price of Item without deal</param>
        /// <param name="bulkquantity">Bulk quantity eligible for deal e.g. 6 for $5.00 where 6 is bulkquantity 
        /// and $5.00 is accumulativeprice</param>
        /// <param name="accumulativeprice">total price for bulk quantity deal</param>
        /// <returns></returns>
        public void SetPricing(char item, double unitprice, int bulkquantity = 1, double accumulativeprice = 0)
        {
            PricingTable.Add(item, new ItemPrice(unitprice, bulkquantity, accumulativeprice));
        }

        public Hashtable GetPricingTable()
        {
            return PricingTable;
        }
        /// <summary>
        /// Scan items into the basket 
        /// </summary>
        /// <param name="item">Product Item</param>
        public void ScanProduct(char item)
        {
            if (PricingTable.ContainsKey(item))
            {
                if (ProductBasket.ContainsKey(item))
                {
                    int cnt = (int)ProductBasket[item];
                    ProductBasket[item] = cnt + 1;
                }
                else
                {
                    ProductBasket.Add(item, 1);
                }
            }
            else
            {

                throw new ArgumentException("Product is not available in the pricing catalog");
            }
        }
        /// <summary>
        /// Get product basket Hashmap table with keys representing items and values representing
        /// number of times item scanned in a given basket
        /// </summary>
        /// <returns>Product basket with their count</returns>
        public Hashtable GetProductBasket()
        {
            return ProductBasket;
        }
        /// <summary>
        /// Calculate total price of the scanned items
        /// </summary>
        /// <returns>total price of scanned items </returns>
        public double CalculateTotal()
        {
            double total = 0;
            ICollection products = ProductBasket.Keys;
            foreach (var item in products)
            {
                ItemPrice itemPrice = (ItemPrice)PricingTable[item];
                int numberOfItemsInBasket = (int)ProductBasket[item];
                double consolidatedPrice = itemPrice.CalculatePrice(numberOfItemsInBasket);
                total += consolidatedPrice;
            }
            return total;
        }
    }
}
