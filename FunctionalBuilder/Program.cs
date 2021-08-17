using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalBuilder
{

   public class Beer
   {
      public string Name, Category;

      public override string ToString()
      {
         return $"Beer name: {Name}, Category: {Category}";
      }
   }

   public abstract class FunctionalBuilder<TSubject, TSelf>
      where TSelf : FunctionalBuilder<TSubject, TSelf>
      where TSubject : new()
   {
      private readonly List<Func<Beer, Beer>> actions = new List<Func<Beer, Beer>>();

      public TSelf Do(Action<Beer> action) => AddAction(action);

      public Beer Build() => actions.Aggregate(new Beer(), (b, f) => f(b));

      private TSelf AddAction(Action<Beer> action)
      {
         actions.Add(b =>
         {
            action(b);
            return b;
         });

         return (TSelf)this;
      }
   }

   public sealed class BeerBuilder 
      : FunctionalBuilder<Beer, BeerBuilder>
   {
      public BeerBuilder Called(string name) => Do(b => b.Name = name);
   }

   ////we can make the whole sealed class a generic class
   //public sealed class BeerBuilder
   //{
   //   private readonly List<Func<Beer, Beer>> actions = new List<Func<Beer, Beer>>();

   //   public BeerBuilder Called(string name) => Do(b => b.Name = name);

   //   public BeerBuilder Do(Action<Beer> action) => AddAction(action);

   //   public Beer Build() => actions.Aggregate(new Beer(), (b, f) => f(b));

   //   private BeerBuilder AddAction(Action<Beer> action)
   //   {
   //      actions.Add(b => { action(b);
   //         return b;
   //      });

   //      return this;
   //   }
   //}

   public static class BeerBuilderExtensions
   {
      public static BeerBuilder IsOfCategory(this BeerBuilder builder, string category) =>
         builder.Do(b => b.Category = category);
   }

   class Demo
   {
      static void Main(string[] args)
      {
         var beer = new BeerBuilder()
            .Called("Leffe")
            .IsOfCategory("Abbey")
            .Build();

         Console.WriteLine(beer.ToString());
      }
   }
}
