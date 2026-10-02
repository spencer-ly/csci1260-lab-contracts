using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class HighestValueFirst : IComparer<ShelfCount>
    {
        // if b greater than 'a' it returns as if a is null (1), if equal, they compare
        public int Compare(ShelfCount a, ShelfCount b) 
        { 
            // if compared is null return 1
            if (a == null) 
            {
                return 1;
            }
            //if differ
            if (b.ValueOnHand > a.ValueOnHand)
            {
                return 1;
            }
            // if equal return CompareTo
            else if (a == b)
            {
                return a.CompareTo(b);
            }
            else
            {
                return -1;
            }
        }




    }
}
