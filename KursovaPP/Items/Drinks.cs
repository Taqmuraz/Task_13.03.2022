namespace KursovaPP
{
    class Drinks : Item, ICaloriable
    {
        public Drinks(string name, int volume, decimal price) : base(name, price)
        {
            Volume = volume;
        }
        public int Volume { get; private set; }

        public double GetCalories()
        {
            return Volume * 1.5d;
        }
    }
}
