using Lab2;
using System.Reflection.Emit;
using System.Threading.Channels;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"=== RIVER CITY SUPPLY ===");
        Console.WriteLine();

        ShelfCount dairy = new ShelfCount("DAIRY", 3, 21.50);
        ShelfCount dry = new ShelfCount("DRY", 1, 19.00);
        ShelfCount dairy2 = new ShelfCount("DAIRY", 1, 15.00);
        ShelfCount dry2 = new ShelfCount("DRY", 1, 19.00);
        ShelfCount dairy3 = new ShelfCount("DAIRY", 3, 18.75);
        ShelfCount frozen = new ShelfCount("FROZEN", 2, 30.00);
        ShelfCount dry3 = new ShelfCount("DRY", 4, 12.50);

        int position = 1;

        // table order
        List<ShelfCount> records = new List<ShelfCount>
{
    dairy, dry, dairy2, dry2, dairy3, frozen, dry3
};

        Console.Write("Seven records created, in this order:\n");
        foreach (ShelfCount record in records)
        {
            string list = String.Format(" {0,2} {1}", position, record);
            Console.WriteLine(list);
            position++;
        }
        Console.WriteLine();
        // Contract 1
        Console.WriteLine("Contract 1: Equals and GetHashCode");

        // hashset
        HashSet<ShelfCount> hashSet = new HashSet<ShelfCount>
{
    dairy, dry, dairy2, dry2, dairy3, frozen, dry3
};

        string record1 = String.Format(" {0,-50}{1,6}", "Record 1 equals record 5 (same key, new value)?", records[0].Equals(records[4]));
        string record2 = String.Format(" {0,-50}{1,6}", "Record 2 equals record 4 (identical)?", records[1].Equals(records[3]));
        string record3 = String.Format(" {0,-50}{1,6}", "Record 1 equals record 3?", records[0].Equals(records[2]));
        string record4 = String.Format(" {0,-50}{1,6}", "Record 2 and record 4 are the same object?", ReferenceEquals(records[1], records[3]));
        string record5 = String.Format(" {0,-50}{1,6}", "Equal records report equal hash codes?", records[0].GetHashCode() == records[4].GetHashCode());
        string recordsCreated = String.Format(" {0,-50}{1,6}", "Records created: ", records.Count);
        string recordsDistinct = String.Format(" {0,-50}{1,6}", "Distinct records in set:", hashSet.Count);

        Console.WriteLine(record1);
        Console.WriteLine(record2);
        Console.WriteLine(record3);
        Console.WriteLine(record4);
        Console.WriteLine(record5);
        Console.WriteLine(recordsCreated);
        Console.WriteLine(recordsDistinct);

        Console.WriteLine();
        // Contract 2
        List<ShelfCount> distinct = new List<ShelfCount>(hashSet); //hashset copied to List<ShelfCount> for rest of program
        distinct.Sort();
        Console.WriteLine("Contract 2: CompareTo, the natural order");
        foreach (ShelfCount record in distinct)
        {
            string list = String.Format("  {0}", record);
            Console.WriteLine(list);
        }

        Console.WriteLine();

        //Contract 3
        distinct.Sort(new HighestValueFirst());

        Console.WriteLine("Contract 3: a comparer, chosen at the call site");
        Console.WriteLine("  HighestValueFirst");

        foreach (ShelfCount record in distinct)
        {
            string list = string.Format("    {0}", record);
            Console.WriteLine(list);
        }

        distinct.Sort(new GroupedByKey());
        Console.WriteLine("  GroupedByKey");
        foreach (ShelfCount record in distinct)
        {
            string list = string.Format("    {0}", record);
            Console.WriteLine(list);
        }

        Console.WriteLine();

        // Contract 4
        distinct.Sort(new HighestValueFirst());
        Console.WriteLine("Contract 4: cleanup that runs even when the code throws");
        CountLog log = null;
        try
        {
            using (log = new CountLog("count-log.txt"))
            {
                for (int i = 0; i < 3; i++)
                    log.Write(distinct[i]);

                throw new InvalidOperationException("scanner fault after 3 writes");
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("  Caught: " + ex.Message);
        }

        bool safeTwice = true;
        try { log.Dispose(); } catch { safeTwice = false; }

        string recordLog = String.Format("    {0,-50}{1,6}", "The log closed itself?", log.IsClosed);
        string recordLog2 = String.Format("    {0,-50}{1,6}", "Closing it a second time was safe?", safeTwice);
        string recordLog3 = String.Format("    {0,-50}{1,6}", "Lines the log wrote before the fault:", log.Count);

        string[] file = File.ReadAllLines("count-log.txt");

        Console.WriteLine(recordLog);
        Console.WriteLine(recordLog2);
        Console.WriteLine(recordLog3);

        Console.WriteLine("   count-log.txt now says:");
        foreach (string line in file)
        {
            string lines = String.Format("    {0}", line);
            Console.WriteLine(lines);
        }
    }
}