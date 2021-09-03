using System;
using System.Net.Sockets;

namespace Prototype
{
   interface IPrototype<T>
   {
      T DeepCopy();
   }

   public class Person : IPrototype<Person> // : ICloneable
   {
      public string[] Names;
      public Address Address;

      public Person(string[] names, Address address)
      {
         Names = names ?? throw new ArgumentNullException(nameof(names));
         Address = address ?? throw new ArgumentNullException(nameof(address));
      }

      public Person DeepCopy()
      {
         return new Person(Names, Address.DeepCopy());
      }

      public override string ToString()
      {
         return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
      }

      public Person(Person other)
      {
         Names = other.Names;
         Address = new Address(other.Address);
      }

      //public object Clone()
      //{
      //   return new Person(Names, (Address)Address.Clone());
      //}
   }

   public class Address : IPrototype<Address> // :ICloneable
   {
      public string StreetName;
      public int HouseNumber;

      public Address(string streetName, int houseNumber)
      {
         StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
         HouseNumber = houseNumber;
      }

      public Address DeepCopy()
      {
         return new Address(StreetName, HouseNumber);
      }

      public override string ToString()
      {
         return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
      }

      public Address(Address other)
      {
         StreetName = other.StreetName;
         HouseNumber = other.HouseNumber;
      }

      //public object Clone()
      //{
      //   return new Address(StreetName, HouseNumber);
      //}
   }
   class Program
   {
      static void Main(string[] args)
      {
         var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));

         //var jane = john;
         //jane.Names[0] = "Jane";

         //var jane = (Person)john.Clone();
         //jane.Address.HouseNumber = 213;

         //var jane = new Person(john);
         var jane = john.DeepCopy();
         jane.Address.HouseNumber = 213;

         Console.WriteLine(john);
         Console.WriteLine(jane);
      }
   }
}
