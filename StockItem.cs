using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public abstract class StockItem : IReportable
    {
        private string sku;
        private string name;
        private decimal unitPrice;
        private int quantityOnHand;
        private List<StockMovement> history;
        private int nextSeq;

        public string Sku 
        {
            get
            {
                return sku;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
        public decimal UnitPrice 
        {
            get
            {
                return unitPrice;
            }
        }
        public int QuantityOnHand
        {
            get
            {
                return quantityOnHand;
            }
        }
        public int MoveCount
        {
            get
            {
                return history.Count;
            }
        }

        protected StockItem(string sku, string name, decimal unitPrice, int quantityOnHand)
        {

            this.sku = sku;
            this.name = name;

            if (UnitPrice < 0)
            {
                this.unitPrice = 0;
            }
            else
            {
                this.unitPrice = unitPrice;
            }

            if (QuantityOnHand < 0)
            {
                this.quantityOnHand = 0;
            }
            else
            {
                this.quantityOnHand = quantityOnHand;
            }

            history = new List<StockMovement>();
            nextSeq = 1;

        }

        public abstract string Category();

        public abstract decimal HandlingFee();
        

        public decimal ExtendedValue()
        {
            return (UnitPrice + HandlingFee()) * quantityOnHand;
        }

        public bool Receive(int count)
        {
            if (count <= 0)
            { 
                return false; 
            }
            else 
            { 
                quantityOnHand += count;
                history.Add(new StockMovement(nextSeq, "Received", count));
                nextSeq++;
                return true;
            }
        }

        public bool Release(int count)
        { 
            if (count <= 0 || count > quantityOnHand)
            {
                return false;
            }
            else
            {
                quantityOnHand -= count;
                history.Add(new StockMovement(nextSeq, "Released", count));
                nextSeq++;
                return true;
            }

        }

        public string MovementLines()
        {
            List<string> noNewline = new List<string>();

            foreach (StockMovement movement in history)
            {
                noNewline.Add(movement.Describe());
            }
            return Environment.NewLine + string.Join(Environment.NewLine, noNewline);
        }

        public virtual string Describe()
        {
            return String.Format($"{Sku} {Name} {Category()}");
        }


        //sku, name, category, qty, value
        public string ReportLine()
        {
            return $"   {Sku,-7} {Name,-21} {Category(),-10} {quantityOnHand,4} ${ExtendedValue(),11:N2}";
        }

        public override string ToString()
        {
            return Describe();
        }

    }
}
