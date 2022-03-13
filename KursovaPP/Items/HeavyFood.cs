namespace KursovaPP
{
    abstract class HeavyFood : LightFood, ICaloriable
    {
        public HeavyFood(string name, int mass, decimal price) : base(name, mass, price)
        {
        }
        public abstract double GetCalories();
    }
}
