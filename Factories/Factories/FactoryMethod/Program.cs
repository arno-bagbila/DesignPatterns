using System;
using System.Runtime.CompilerServices;

namespace FactoryMethod
{
   public enum CoordinateSystem
   {
      Cartesian,
      Polar
   }
   public class Point
   {
      private double x, y;

      /// <summary>
      /// explain how to usu the constructor, but not a lot of people will read this. This is to show that constructors have their limitations. The Factory try to resolve this issue
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <param name="system"></param>
      //public Point(double a, double b, CoordinateSystem system = CoordinateSystem.Cartesian)
      //{
      //   switch (system)
      //   {
      //      case CoordinateSystem.Cartesian:
      //         x = a;
      //         y = b; // we don't even if a should be x and b should be y...
      //         break;
      //      case CoordinateSystem.Polar:
      //         x = a * Math.Cos(b);
      //         x = a * Math.Sin(b); // once again we don't have any indication on how tha data should be allocated
      //         break;
      //      default:
      //         throw new ArgumentOutOfRangeException(nameof(system), system, null);
      //   }
      //}

      ////cannot create constructor with name data type even if it is for different properties
      //public Point(double rho, double theta)
      //{

      //}
      private Point(double x, double y)
      {
         this.x = x;
         this.y = y;
      }
      // this is a factory method
      public static Point NewCartesianPoint(double x, double y)
      {
         return new Point(x, y);
      }

      //this is another factory point that you can exposed.
      public static Point NewPolarPoint(double rho, double theta)
      {

         return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
      }

      public override string ToString()
      {
         return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
      }
   }
   public class Demo
   {
      static void Main(string[] args)
      {
         var point = Point.NewPolarPoint(1.0, Math.PI / 2);
         Console.WriteLine(point);

         // Advantage of Factory: you get to have an overload with the same argument dataType - Also the name of the factory method are also unique, not have to be the class name
      }
   }
}
