using System;

namespace PointOfSaleTerminal
{
    // <summary>
    // ItemPrice calculates the total price of an item for a given quantity
    // <summary>
    public class ItemPrice
    {
        public double UnitPrice { get; set; }
        public int BulkQuantity { get; set; }
        public double BulkCost { get; set; }
        public ItemPrice() { }
        public ItemPrice(double unitprice, int bulkquantity = 1, double bulkcost = 0)
        {
            UnitPrice = unitprice;
            BulkQuantity = bulkquantity;
            BulkCost = bulkcost;
        }

        public double CalculatePrice(int scannedquantity)
        {
            if (BulkQuantity > 1)
            {
                // Charge bulk price for eligible quantity and regular price for rest
                int Remainder;
                int Quotient = Math.DivRem(scannedquantity, BulkQuantity, out Remainder);
                return Quotient * BulkCost + Remainder * UnitPrice;
            }
            else
            {
                // No bulk pricing available for this product
                return scannedquantity * UnitPrice;
            }
        }
    }
}
