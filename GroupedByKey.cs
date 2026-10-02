using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    // Aisle ascending (a,b) Value descending (b,a) Slot ascending (a,b)
    public class GroupedByKey : IComparer<ShelfCount>
    {
        public int Compare(ShelfCount a, ShelfCount b)
        {
            int byAisle = string.CompareOrdinal(a.Aisle, b.Aisle);

            if (byAisle != 0) return byAisle;

            int byValue = b.ValueOnHand.CompareTo(a.ValueOnHand);

            if (byValue != 0) return byValue;

            return a.Slot.CompareTo(b.Slot);
        }
    }
}
