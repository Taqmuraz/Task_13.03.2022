using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;

namespace KursovaPP
{
    class Order
    {
        public int Table { get; private set; }
        public Item[] Items { get; private set; }

        public Order(int table, Item[] items)
        {
            Table = table;
            Items = items;
        }
    }
    class Program
    {
        delegate void MenuCommand(string[] args);
        static Dictionary<string, MenuCommand> commands = new Dictionary<string, MenuCommand>();
        static List<Item> items = new List<Item>();
        static List<Order> orders = new List<Order>();

        static Program()
        {
            var culture = CultureInfo.InvariantCulture;

            commands.Add("salat", args => items.Add(new Salat(args[1], Convert.ToInt32(args[2]), Convert.ToDecimal(args[3], culture))));
            commands.Add("sup", args => items.Add(new Sup(args[1], Convert.ToInt32(args[2]), Convert.ToDecimal(args[3], culture))));
            commands.Add("primary", args => items.Add(new PrimaryFood(args[1], Convert.ToInt32(args[2]), Convert.ToDecimal(args[3], culture))));
            commands.Add("desert", args => items.Add(new Desert(args[1], Convert.ToInt32(args[2]), Convert.ToDecimal(args[3], culture))));
            commands.Add("drink", args => items.Add(new Drinks(args[1], Convert.ToInt32(args[2]), Convert.ToDecimal(args[3], culture))));
            commands.Add("make_order", args =>
            {
                Item[] orderedItems = args.Skip(1).Select(a => items.First(item => item.Name.Equals(a))).ToArray();
                orders.Add(new Order(Convert.ToInt32(args[0]), orderedItems));
            });
            commands.Add("stats", Stats);
            commands.Add("final", Final);
        }

        static void Stats(string[] args)
        {
            Console.WriteLine($"Tables number : {orders.Select(order => order.Table).Distinct().Count()}");
            Console.WriteLine($"Total orders : {orders.SelectMany(o => o.Items).Count()} -- {orders.Aggregate(0m, (price, order) => price + order.Items.Aggregate(0m, (iPrice, item) => iPrice + item.Price))}");

            Console.WriteLine("Categories :");

            var types = orders.SelectMany(order => order.Items.Select(item => item.GetType()));
            foreach (var type in types.Distinct().OrderBy(item => item.Name))
            {
                Console.WriteLine($"-- {type.Name} : {types.Where(t => t == type).Count()} -- {orders.SelectMany(order => order.Items.Where(i => i.GetType() == type)).Aggregate(0m, (price, item) => price + item.Price)}");
            }
        }
        static void Final(string[] args)
        {
            inProgress = false;
            Stats(args);
        }

        static bool inProgress = true;

        static void Main()
        {
            using (StreamReader sr = new StreamReader("./text.txt"))
            {
                while (inProgress)
                {
                    string[] args = sr.ReadLine().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => new string(s.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray())).ToArray();
                    if (args[0].All(c => char.IsNumber(c)))
                    {
                        commands["make_order"](args);
                    }
                    else
                    {
                        commands[args[0]](args);
                    }
                }
            }
        }
    }
}
