using System;
using System.Collections.Generic;

namespace AbstractFactory
{
   public interface IHotDrink
   {
      void Consume();
   }

   internal class Tea : IHotDrink
   {
      public void Consume()
      {
         Console.WriteLine("This tea is very nice");
      }
   }

   internal class Coffe : IHotDrink
   {
      public void Consume()
      {
         Console.WriteLine("This is a coffe!");
      }
   }

   internal class TeaFactory : IHotDrinkFactory
   {
      public IHotDrink Prepare(int amount)
      {
         Console.WriteLine($"Put in the tea bag, boil water, pour {amount}ml, add lemon and enjoy");
         return new Tea();
      }
   }

   internal class CoffeeFactory : IHotDrinkFactory
   {
      public IHotDrink Prepare(int amount)
      {
         Console.WriteLine($"Gring your coffe and pour {amount} ml of water");
         return new Coffe();
      }
   }

   public interface IHotDrinkFactory
   {
      IHotDrink Prepare(int amount);
   }

   public class HotDrinkMachine
   {
      // We need to get rid of this enum, as it will bring a violation of the Open Close Principle of SOLID
      //public enum AvailableDrink
      //{
      //   Coffee, Tea
      //}

      //private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
      //   new Dictionary<AvailableDrink, IHotDrinkFactory>();

      //public HotDrinkMachine()
      //{
      //   foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
      //   {
      //      var factory = (IHotDrinkFactory)Activator.CreateInstance(
      //         Type.GetType("AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")
      //      );
      //      factories.Add(drink, factory);
      //   }
      //}

      //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
      //{
      //   return factories[drink].Prepare(amount);
      //}

      //better to use dependency injection
      private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();
      public HotDrinkMachine()
      {
         foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
         {
            if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
            {
               factories.Add(Tuple.Create(t.Name.Replace("Factory", string.Empty), (IHotDrinkFactory)Activator.CreateInstance(t)));
            }
         }
      }

      public IHotDrink MakeDrink()
      {
         Console.WriteLine("Available drink:");
         for (var index = 0; index < factories.Count; index ++)
         {
            var tuple = factories[index];
            Console.WriteLine($"{index}: {tuple.Item1}");
         }

         while (true)
         {
            string s;
            if ((s = Console.ReadLine()) != null
                && int.TryParse(s, out int i)
                && i >= 0 && i < factories.Count)
            {
               Console.WriteLine("Specify the amount: ");
               s = Console.ReadLine();
               if (s != null && int.TryParse(s, out int amount) && amount > 0)
               {
                  return factories[i].Item2.Prepare(amount);
               }
            }

            Console.WriteLine("Incorrect, try again!");
         }
      }

   }
   class Program
   {
      static void Main(string[] args)
      {
         var machine = new HotDrinkMachine();
         //var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 100);
         //drink.Consume();
         var drink = machine.MakeDrink();
         drink.Consume();
      }
   }
}
