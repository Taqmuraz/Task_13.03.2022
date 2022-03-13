namespace KursovaPP
{
    class LightFood : Item
    {
        public LightFood(string name, int mass, decimal price) : base(name, price)
        {
            Mass = mass;
        }
        public int Mass { get; private set; }
    }
}
