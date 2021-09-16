using System;

namespace MonoState
{
   static class Program
   {
      // The singleton approach is to prevent people calling the constructor
      public class CEO
      {
         private static string name;
         private static int age;

         public string Name
         {
            get => name;
            set => name = value;
         }

         public int Age
         {
            get => age;
            set => age = value;
         }

         public override string ToString()
         {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
         }
      }
      static void Main(string[] args)
      {
         var ceo = new CEO();
         ceo.Name = "Cedric";
         ceo.Age = 30;

         var ceo2 = new CEO();
         ceo2.Age = 48;

         Console.WriteLine(ceo);
      }
   }
}
