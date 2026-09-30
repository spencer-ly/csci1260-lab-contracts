using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class ServiceItem : StockItem, IDiscountable
    {
        private double laborHours;

        public double LaborHours
        {
            get
            {
                return laborHours;
            }
        }
        // sale for more than 2 hours of labor performed
        public bool IsOnSale
        {
            get
            {
                if (LaborHours >= 2.0)
                    return true;
                else
                    return false;
            }
        }


        public ServiceItem(string sku, string name, decimal unitPrice, int quantityOnHand, double laborHours)
            : base (sku, name, unitPrice, quantityOnHand)
        {
            if (laborHours < 0)
            {
                this.laborHours = 0;
            }
            else
            { 
                this.laborHours = laborHours;
            }
        }

        public override string Category()
        {
            return "Service";
        }

        public override decimal HandlingFee()
        {
            return 0m;
        }

        public decimal SalePrice()
        {
            if (IsOnSale)
            {
                return UnitPrice * .85m;
            }
            else
            {
                return UnitPrice;
            }
        }

        public override string Describe()
        {
            return base.Describe() + String.Format(", {0:N1} labor hours", LaborHours);
        }


    }
}
