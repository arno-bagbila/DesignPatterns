using System;
using static System.Console;

namespace LiskovSubstitutionPrinciple
{
   public class Rectangle
   {
      //public int Width { get; set; }
      public virtual int  Width { get; set; }
      //public int Height { get; set; }
      public virtual int Height { get; set; }

      public Rectangle()
      {

      }

      public Rectangle(int width, int height)
      {
         Width = width;
         Height = height;
      }

      public override string ToString()
      {
         return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
      }
   }

   public class Square : Rectangle
   {
      public  override int Width
      {
         set
         {
            base.Width = base.Height = value;
         }
      }

      public  override int Height
      {
         set
         {
            base.Height = base.Width = value;
         }
      }
   }
   public class Demo
   {
      public static int Area(Rectangle rc) => rc.Width * rc.Height;

      static void Main(string[] args)
      {
         Rectangle rc = new Rectangle(2, 3);
         WriteLine($"{rc} has an area {Area(rc)}");

         //you should always be able to upcast to your base type
         Rectangle sq = new Square();
         sq.Width = 4;
         WriteLine($"{sq} has area: {Area(sq)}");

      }
   }
}
