using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using MoreLinq;
using NUnit.Framework;

namespace Singleton
{
  public interface IDatabase
   {
      int GetPopulation(string name);
   }

   public class SingletonDatabase : IDatabase
   {
      private Dictionary<string, int> capitals;
      private static int instanceCount; //0 
      public static int Count => instanceCount;

      private SingletonDatabase()
      {
         instanceCount++;
         Console.WriteLine("Initializing database");

         capitals = File.ReadAllLines("capitals.txt")
            .Batch(2)
            .ToDictionary(
               list => list.ElementAt(0).Trim(),
               list => int.Parse(list.ElementAt(1)));
      }
      public int GetPopulation(string name)
      {
         return capitals[name];
      }

      private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

      public static SingletonDatabase Instance => instance.Value;
   }

   public class OrdinaryDatabase : IDatabase
   {
      private Dictionary<string, int> capitals;

      public OrdinaryDatabase()
      {
         Console.WriteLine("Initializing database");

         capitals = File.ReadAllLines("capitals.txt")
            .Batch(2)
            .ToDictionary(
               list => list.ElementAt(0).Trim(),
               list => int.Parse(list.ElementAt(1)));
      }


      public int GetPopulation(string name)
      {
         return capitals[name];
      }
   }

   public class SingletonRecordFinder
   {
      public int GetTotalPopulation(IEnumerable<string> names)
      {
         int result = 0;
         foreach (var name in names)
         {
            result += SingletonDatabase.Instance.GetPopulation(name); // depending on a live database, bad idea
         }

         return result;
      }
   }

   public class ConfigurableRecordFinder
   {
      private IDatabase database;

      public ConfigurableRecordFinder(IDatabase database)
      {
         this.database = database ?? throw new ArgumentNullException(nameof(database));
      }

      public int GetTotalPopulation(IEnumerable<string> names)
      {
         int result = 0;
         foreach (var name in names)
         {
            result += database.GetPopulation(name);
         }

         return result;
      }
   }

   public class DummyDatabase: IDatabase
   {
      public int GetPopulation(string name)
      {
         return new Dictionary<string, int>
         {
            ["alpha"] = 1,
            ["beta"] = 2,
            ["gamma"] = 3
         }[name];
      }
   }

   [TestFixture]
   public class SingletonTests
   {
      [Test]
      public void IsSingletonTest()
      {
         var db = SingletonDatabase.Instance;
         var db2 = SingletonDatabase.Instance;
         Assert.That(db, Is.SameAs(db2));
         Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
      }

      [Test]
      public void SingletonTotalPopulation()
      {
         var rf = new SingletonRecordFinder();
         var names = new[] { "Seoul", "Mexico City" };
         var tp = rf.GetTotalPopulation(names);
         Assert.That(tp, Is.EqualTo(8));
      }

      [Test]
      public void ConfigurablePopulationTest()
      {
         var rf = new ConfigurableRecordFinder(new DummyDatabase());
         var names = new[] { "alpha", "beta" };
         int tp = rf.GetTotalPopulation(names);
         Assert.That(tp, Is.EqualTo(3));
      }

      public void DIPopulationTest()
      {
         var cb = new ContainerBuilder();
         cb.RegisterType<OrdinaryDatabase>()
            .As<IDatabase>()
            .SingleInstance();

         cb.RegisterType<ConfigurableRecordFinder>();

         using (var c = cb.Build())
         {
            var rf = c.Resolve<ConfigurableRecordFinder>();
         }
      }

   }

   class Program
   {
      private static void Main(string[] args)
      {
         var db = SingletonDatabase.Instance;
         var city = "Tokyo";
         Console.WriteLine($"{city} has population {db.GetPopulation(city)}");
      }
   }
}
