namespace KursovaPP
{
    class Item
    {
        public Item(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
        public decimal Price { get; private set; }
        public string Name { get; private set; }
    }
}
