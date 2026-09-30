Press play and the project will run. I had an issue with formatting for things outside PrintReport()

1) PhysicalGood shouldn't run on its own for a few reasons. Category() and HandlingFee() aren't being used by PhysicalGood (used in StockItem, Perishable, Durable).
   It has real code in it, but needs the category and handling fee. If trying to create a new PhysicalGood, it would throw an error for not including these.
2) Shop has a relationship with StockItem but can live without each other. StockItem stores the list of items, while shop adds them to manager (in my case).
   StockItem to Stockmovement is composition that can't live without each other. There is no stock movement without an item.
   if Add lived in Shop, StockItem wouldn't be needed and would move logic into Shop (solid diamond would change).
3) Rental would implement StockItem similar to PhysicalGood. Shop doesn't need to change since you'd add a new Category() and HandlingFee() for rentals
   which would satisfy the abstract requirement. If IsOnSale were declared on StockItem it'd have to turn false for each item rather than just passing it to some that
   require the discount sale.
