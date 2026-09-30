using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Lab2
{
    public class Shop : IReportable
    {
        private string name;
        private List<StockItem> items;


        public string Name 
        {
            get
            {
                return name;
            }
        }
        public int Count
        {
            get 
            { 
                return items.Count; 
            }
        }


        public Shop(string name)
        {
            this.name = name;

            this.items = new List<StockItem>();

        }

        public bool Add(StockItem item)
        {
            if (item == null)
            {
                return false;
            }
            if (Find(item.Sku) != null)
            {
                return false;
            }
            items.Add(item);
            return true;
        }

        public StockItem Find(string sku)
        {
            foreach (StockItem itemFind in items)
            {
                if (itemFind.Sku == sku)
                {
                    return itemFind;
                }
            }
            return null;
        }

        public decimal TotalValue()
        {
            decimal total = 0;

            foreach (StockItem item in items)
            {
                total += item.ExtendedValue();
            }
            
            return total;
        }

        // calculates if item is on sale
        public decimal SaleValue()
        {
            decimal total = 0;
            foreach (StockItem item in items)
            {
                if (item is IDiscountable d && d.IsOnSale)
                {
                    total += (d.SalePrice() + item.HandlingFee()) * item.QuantityOnHand;
                }
                else
                {
                    total += item.ExtendedValue();
                }
            }
            return (decimal)total;
        }

        public int SignedCount()
        {
            int count = 0;
            foreach (StockItem item in items)
            {
                if (item is IDiscountable)
                {
                    count++;
                }
            }

            return count;
        }

        public int OnSaleCount()
        {
            int count = 0;
            foreach (StockItem item in items)
            {
                if (item is IDiscountable d && d.IsOnSale)
                {
                    count++;
                }
            }
            return count;
        }

        // Mat's beautiful double for loop
        public void SortByValue()
        {
            for (int i = 0; i < items.Count -1; i++)
            {
                int best = i;
                for (int j = i + 1; j < items.Count; j++)
                {
                    if (Beats(items[j], items[best]))
                    {
                        best = j;
                    }
                }

                if (best != i)
                {
                    StockItem hold = items[i];
                    items[i] = items[best];
                    items[best] = hold;
                }
            }
        }

        private static bool Beats(StockItem a, StockItem b)
        {
            if (a.ExtendedValue() != b.ExtendedValue())
                return a.ExtendedValue() > b.ExtendedValue();

            return string.Compare(a.Name, b.Name, StringComparison.Ordinal) < 0;
        }


        public string ReportLine()
        {
            return String.Format("{0}: {1} items, ${2:N2} on hand", Name, Count, SaleValue());
        }

        public void PrintReport()
        {
            Console.Write("============================================================\n");
            Console.Write($"  {name.ToUpper()} : INVENTORY REPORT\n");
            Console.Write("============================================================\n");
            Console.Write(string.Format("{0,-7} {1,-21} {2,-10} {3,4} {4,11}\n", "SKU", "ITEM", "CATEGORY", "QTY", "VALUE"));
            Console.Write("------------------------------------------------------------\n");

            foreach (StockItem item in items)
            {
                Console.Write($"{item.ReportLine()}\n");
            }

            Console.Write("------------------------------------------------------------\n");
            Console.Write($"Records on file: {items.Count}\n");
            Console.Write($"Total value on hand: {TotalValue():N2}\n");
            Console.Write($"Value if every sale price were taken: {SaleValue():N2}\n");
            Console.Write("============================================================");
        }

 
    }
}
