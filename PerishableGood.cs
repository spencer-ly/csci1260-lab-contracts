using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class PerishableGood : PhysicalGood, IDiscountable
    {
        private int shelfLifeDays;

        public const decimal SurchargeFee = 0.40m;

        public int ShelfLifeDays
        {
            get
            {
                return shelfLifeDays;
            }
        }

        // item has sale if shelf life is less than 3 days
        public bool IsOnSale
        {
            get
            {
                return ShelfLifeDays <= 3;
            }
        }

        public PerishableGood(string sku, string name, decimal unitPrice, int quantityOnHand, double weightPounds, int shelfLifeDays)
            : base(sku, name, unitPrice, quantityOnHand, weightPounds)
        {
            if (shelfLifeDays < 0)
            {
                this.shelfLifeDays = 0;
            }
            else
            {
                this.shelfLifeDays = shelfLifeDays;
            }
        }

        public override string Category()
        {
            return "Perishable";
        }

        public override decimal HandlingFee()
        {
            return ShippingCost() + SurchargeFee;
        }

        public decimal SalePrice()
        {
            if (IsOnSale)
            {
                return UnitPrice * 0.70m;
            }
            else
            {
                return UnitPrice;
            }
        }

        public override string Describe()
        {
            return base.Describe() + String.Format(", {0} days left", ShelfLifeDays);
        }

    }
}
