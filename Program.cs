using Lab2;

//1
Shop manager = new Shop ("River City Supply");
//2
Console.Write($"Opening catalog: ");
Show(manager);
Console.Write("\n");
Console.Write("Loading five records... \n");
//3

StockItem honey = new PerishableGood("HON01", "Wildflower honey", 8m, 12, 1.5, 2);
StockItem kettle = new DurableGood("KTL11", "Cast iron kettle", 24m, 5, 4.0, 24);
StockItem cheddar = new PerishableGood("CHZ07", "Farm cheddar wedge", 3.5m, 40, 0.5, 9);
StockItem knifeSharpening = new ServiceItem("SRV20", "Knife sharpening", 60m, 2, 2.5);
StockItem giftWrapping = new ServiceItem("SRV21", "Gift wrapping", 15m, 3, 1);
//4

//StockItem bad = new StockItem("X", "Nope", 1m, 1); 
//CS0144: Cannot create instance of abstract type or interface 'StockItem'


honey.Receive(6);       //request 1
//honey.Receive(6);       //request 1
kettle.Release(2);      //request 2
kettle.Release(99);     //request 3
cheddar.Receive(-5);    //request 4
//5

manager.Add(honey);
manager.Add(kettle);
manager.Add(cheddar);
manager.Add(knifeSharpening);
manager.Add(giftWrapping);

List<StockItem> list = new List<StockItem>
{
    honey, kettle, cheddar, knifeSharpening, giftWrapping
};


if (!manager.Add(honey))
    Console.WriteLine("  REJECTED: duplicate SKU HON01");

Console.Write("Recording four movements...\n");

kettle = manager.Find("KTL11");
if (kettle != null && !kettle.Release(99))
    Console.WriteLine("  REJECTED: release of 99 from KTL11");

cheddar = manager.Find("CHZ07");
if (cheddar != null && !cheddar.Receive(-5))
    Console.WriteLine("  REJECTED: release of -5 from CHZ07");

int movementsAccepted = 0;
knifeSharpening = manager.Find("SRV20");
if (knifeSharpening.Sku == "SRV20")
    movementsAccepted++;
giftWrapping = manager.Find("SRV21");
if (giftWrapping.Sku == "SRV21")
    movementsAccepted++;
//6
Console.Write($"Records accepted: {manager.Count}\n");
Console.Write($"Movements accepeted: {movementsAccepted}\n");

static void Show(IReportable r)
{
    Console.Write(r.ReportLine());
}

Console.Write($"Top record: {cheddar}\n");

manager.SortByValue();

manager.PrintReport();

Console.WriteLine("\n");


Console.Write("Contract check\n");
Console.Write($"  Records signing IDiscountable: {manager.SignedCount()} \n");
Console.Write($"  Records signing IDiscountable: {manager.OnSaleCount()} \n");
Console.Write($"  Difference between the two totals: {manager.TotalValue() - manager.SaleValue():N2}\n");

Console.Write("Composition check\n");
Console.Write($"  Movements recorded by HON01: {honey.MoveCount}");
Console.Write($"{honey.MovementLines()} \n");
Console.Write($"  Movements recorded by KTL11: {kettle.MoveCount}");
Console.Write($"{kettle.MovementLines()} \n");
Console.Write($"  Movements recorded by CHZ07: {cheddar.MoveCount}");
Console.Write($"{cheddar.MovementLines()} \n");

