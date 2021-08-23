using System;
using System.Threading.Tasks;

namespace AsynchronousFactoryMethod
{

   public class Foo
   {
      //// this is not possible as you cannot await in the constructor
      //public Foo()
      //{
      //   await Task.Delay(1000)
      //}

      //fluent- this limitations is that you have to initialize the foo class then called the InitAsync method id not it will not work
      //public async Task<Foo> InitAsync()
      //{
      //   await Task.Delay(1000);
      //   return this;
      //}

      private Foo()
      {
         //
      }

      private async Task<Foo> InitAsync()
      {
         await Task.Delay(1000);
         return this;
      }

      public static Task<Foo> CreateAsync()
      {
         var result = new Foo();
         return result.InitAsync();
      }

   }

   class Demo
   {
      static async Task Main(string[] args)
      {
         //var foo = new Foo();
         //await foo.InitAsync();

         var x = await Foo.CreateAsync();
      }
   }
}
