using System;
using System.Linq;
using System.Runtime.CompilerServices;
using FactoryCodingExercice;
using NUnit.Framework;

namespace FactoryCodingExercice
{

   public class Person
   {
      public int Id { get; set; }

      public string Name { get; set; }

      internal Person(string name)
      {
         Name = name;
      }
   }

   public class PersonFactory
   {
      private int id = 0;
      public Person CreatePerson(string name)
      {
         var person = new Person(name)
         {
            Id = id++
         };

         return person;
      }
   }
   class Program
   {
      static void Main(string[] args)
      {
         var personFactory = new PersonFactory();
         var firstPerson = personFactory.CreatePerson("Susie");
         var secondPerson = personFactory.CreatePerson("Nancy");
         var thirdPerson = personFactory.CreatePerson("Nancy2");
         var fourthPerson = personFactory.CreatePerson("Nancy3");

         Console.WriteLine($"Name: {firstPerson.Name}\nId: {firstPerson.Id}");
         Console.WriteLine($"Name: {secondPerson.Name}\nId: {secondPerson.Id}");
         Console.WriteLine($"Name: {thirdPerson.Name}\nId: {thirdPerson.Id}");
         Console.WriteLine($"Name: {fourthPerson.Name}\nId: {fourthPerson.Id}");
      }
   }
}

namespace Coding.Exercise.UnitTests
{
   [TestFixture]
   public class FirstTestSuite
   {
      [Test]
      public void Test()
      {
         var pf = new PersonFactory();

         var p1 = pf.CreatePerson("Chris");
         Assert.That(p1.Name, Is.EqualTo("Chris"));
         Assert.That(p1.Id, Is.EqualTo(0));

         var p2 = pf.CreatePerson("Sarah");
         Assert.That(p2.Id, Is.EqualTo(1));
      }
   }
}
