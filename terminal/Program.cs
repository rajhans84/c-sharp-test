using System.Collections.Generic;
using System.Linq;
using System;
using PointOfSaleTerminal;

namespace Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Point Of Sale Terminal Service!");
            var terminal = new PointOfSaleTerminalService();

            terminal.SetPricing('A', 1.25, 3, 3.0);
            terminal.SetPricing('B', 4.25);
            terminal.SetPricing('C', 1.00, 6, 5.0);
            terminal.SetPricing('D', 0.75);

            Console.WriteLine("Please enter a product from following list: (A, B, C, D).");
            Console.WriteLine("Enter ! to finish");
            var availableProducts = new List<char>() { 'A', 'B', 'C', 'D' };
            while (true)
            {
                char item = (char)Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (item == '!')
                {
                    break;
                }
                if (availableProducts.Contains(item))
                {

                    terminal.ScanProduct(item);
                }
                else
                {
                    Console.WriteLine("You entered wrong product.");
                    Console.WriteLine("Enter ! to finish");
                    continue;
                }

            }

            double result = (double)terminal.CalculateTotal();

            Console.WriteLine("Total price of items scanned is: {0:C2}", result);
        }
    }
}
