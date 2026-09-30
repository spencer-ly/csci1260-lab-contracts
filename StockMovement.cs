using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class StockMovement
    {
        private int seq;
        private string kind;
        private int count;


        public int Seq
        {
            get
            {
                return seq;
            }
        }

        public string Kind
        {
            get
            {
                return kind;
            }
        }
        public int Count
        {
            get
            {
                return count;
            }
        }



        public StockMovement(int seq, string kind, int count)
        {
            this.seq = seq;
            this.kind = kind;
            this.count = count;
        }

        public string Describe()
        {
            return $"    move {Seq}: {Kind} {Count}";
        }

    }
}
