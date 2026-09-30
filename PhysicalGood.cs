using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public abstract class PhysicalGood : StockItem
    {
        private double weightPounds;

        public const decimal HandlingRate = 0.60m;
        public double WeightPounds
        {
            get
            {
                return weightPounds;
            }
        }

        protected PhysicalGood(string sku, string name, decimal unitPrice, int quantityOnHand, double weightPounds)
            : base(sku, name, unitPrice, quantityOnHand)
        {
            
            if (weightPounds < 0)
            {
                this.weightPounds = 0;
            }
            else
            {
                this.weightPounds = weightPounds;
            }
        }

        public decimal ShippingCost()
        {
            return (decimal)WeightPounds * HandlingRate;
        }

        public override string Describe()
        {
            return base.Describe() + String.Format(", {0:N1}", WeightPounds);
        }

    }
}
