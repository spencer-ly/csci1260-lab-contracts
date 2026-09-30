using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class ShelfCount : IEquatable<ShelfCount>, IComparable<ShelfCount>
    {
        private string aisle;
        private int slot;
        private double valueOnHand;

        public string Aisle { get;}
        public int Slot { get; }
        public double ValueOnHand { get; }

        public ShelfCount(string aisle, int slot, double valueOnHand)
        {
            Aisle = aisle;
            Slot = slot;
            ValueOnHand = valueOnHand;
        }

        public bool Equals(ShelfCount other)
        {
            if (Aisle == other.Aisle && Slot == other.Slot)
            {
                return true;
            }
            else return false;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ShelfCount);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Aisle, Slot);
        }

        public int CompareTo(ShelfCount other)
        {
            if (other is null)
            {
                return 1;
            }

            int result = string.Compare(Aisle, other.Aisle, StringComparison.Ordinal);
            if (result != 0)
            {
                return result;
            }

            return Slot.CompareTo(other.Slot);
        }

        public override string ToString()
        {
            return String.Format("{0,-6} #{1} {2,8:N2}", Aisle, Slot, valueOnHand);
        }
    }
}
