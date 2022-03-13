namespace KursovaPP
{
    class Desert : HeavyFood
    {
        public Desert(string name, int mass, decimal price) : base(name, mass, price)
        {
        }

        public override double GetCalories()
        {
            return Mass * 3;
        }
    }
}
