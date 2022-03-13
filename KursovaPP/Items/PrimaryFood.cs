namespace KursovaPP
{
    class PrimaryFood : HeavyFood
    {
        public PrimaryFood(string name, int mass, decimal price) : base(name, mass, price)
        {
        }

        public override double GetCalories()
        {
            return Mass;
        }
    }
}
