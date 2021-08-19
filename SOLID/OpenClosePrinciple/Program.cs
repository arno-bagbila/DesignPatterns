using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

namespace OpenClosePrinciple
{
   public enum Flavour
   {
      Wheat, Hops, Corn
   }
   
   public enum Color
   {
      Red, Blond, Brune
   }

   public class Beer
   {
      public string Name;
      public Color Color;
      public Flavour Flavour;

      public Beer(string name, Color color, Flavour flavour)      
      {
         if (name == null)
         {
            throw new ArgumentNullException(paramName: nameof(name));
         }

         Name = name;
         Color = color;
         Flavour = flavour;
      }
   }

   public class BeerFilter
   {
      public static IEnumerable<Beer> FilterByFlavour(IEnumerable<Beer> beers, Flavour flavour)
      {
         foreach (var beer in beers)
         {
            if (beer.Flavour == flavour)
               yield return beer;
         }
      }

      public IEnumerable<Beer> FilterByColor(IEnumerable<Beer> beers, Color color)
      {
         foreach (var beer in beers)
         {
            if (beer.Color == color)
               yield return beer;
         }
      }

      public IEnumerable<Beer> FilterByColorAndFlavour(IEnumerable<Beer> beers, Color color, Flavour flavour)
      {
         foreach (var beer in beers)
         {
            if (beer.Color == color && beer.Flavour == flavour)
               yield return beer;
         }
      }
   }

   public interface ISpecification<T>
   {
      bool isSatisfied(T t);
   }

   public interface IFilter<T>
   {
      IEnumerable<T> Filter(IEnumerable<T> item, ISpecification<T> spec);
   }

   public class ColorSpecification : ISpecification<Beer>
   {
      private Color _color;

      public ColorSpecification(Color color)
      {
         _color = color;
      }
      public bool isSatisfied(Beer beer)
      {
         return beer.Color == _color;
      }
   }

   public class FlavourSpecification : ISpecification<Beer>
   {
      private Flavour _flavour;

      public FlavourSpecification(Flavour flavour)
      {
         _flavour = flavour;
      }
      public bool isSatisfied(Beer t)
      {
         return t.Flavour == _flavour;
      }
   }

   public class AndSpecification<T> : ISpecification<T>
   {
      private ISpecification<T> _first, _second;


      public AndSpecification(ISpecification<T> first, ISpecification<T> second)
      {
         _first = first ?? throw new ArgumentNullException(nameof(first));
         _second = second ?? throw new ArgumentNullException(nameof(second));
      }

      public bool isSatisfied(T t)
      {
         return _first.isSatisfied(t) && _second.isSatisfied(t);
      }
   }

   //No need to add another method when we want to filter by another specification, so it is close to modification, but thanks to the interface and generics we can extend to take more specification
   public class BetterBeerFilter : IFilter<Beer>
   {
      public IEnumerable<Beer> Filter(IEnumerable<Beer> beers, ISpecification<Beer> spec)
      {
         foreach (var beer in beers)
         {
            if (spec.isSatisfied(beer))
               yield return beer;
         }
      }
   }

   public class Demo
   {
      static void Main(string[] args)
      {
         var leffe = new Beer("Leffe", Color.Blond, Flavour.Corn);
         var stella = new Beer("Stella Artois", Color.Blond, Flavour.Hops);
         var guinness = new Beer("Guinness", Color.Red, Flavour.Hops);

         Beer[] beers = { leffe, stella, guinness };
         var bf = new BeerFilter();
         WriteLine("Blond beers(old):");
         foreach (var beer in bf.FilterByColor(beers, Color.Blond))
         {
            WriteLine($" - {beer.Name} is Blond");
         }

         var bbf = new BetterBeerFilter();
         var ff = bbf.Filter(beers, new ColorSpecification(Color.Blond));
         WriteLine("Blond beers (new):");
         foreach (var beer in ff)
         {
            WriteLine($" - {beer.Name} is Blond");
         }

         WriteLine("RED AND HOPPY");
         var beerFlavAndCol = bbf.Filter(beers, new AndSpecification<Beer>(new FlavourSpecification(Flavour.Hops), new ColorSpecification(Color.Red)));
         foreach (var beer in beerFlavAndCol)
         {
            WriteLine($"{beer.Name} is Red and Hops");
         }

      }
   }
}
