using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace CopyThroughSerialization  
{
   public static class ExtensionMethods
   {
      public static T DeepCopy<T>(this T self)
      {
         var stream = new MemoryStream();
         var formatter = new BinaryFormatter();
         formatter.Serialize(stream, self);

         stream.Seek(0, SeekOrigin.Begin);
         object copy = formatter.Deserialize(stream);
         stream.Close();
         return (T)copy;
      }

      public static T DeepCopyXml<T>(this T self)
      {
         using var ms = new MemoryStream();
         var s = new XmlSerializer(typeof(T));
         s.Serialize(ms, self);
         ms.Position = 0;
         return (T)s.Deserialize(ms);
      }
   }

   //[Serializable] - Required by BinaryFormatter
   public class Person 
   {
      public string[] Names;
      public Address Address;

      public Person()
      {
         
      }

      public Person(string[] names, Address address)
      {
         Names = names ?? throw new ArgumentNullException(nameof(names));
         Address = address ?? throw new ArgumentNullException(nameof(address));
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

   }

   //[Serializable]
   public class Address  // :ICloneable
   {
      public string StreetName;
      public int HouseNumber;

      //empty constructor is requited when using xml serializer
      public Address()
      {
         
      }

      public Address(string streetName, int houseNumber)
      {
         StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
         HouseNumber = houseNumber;
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
         var jane = john.DeepCopyXml();
         jane.Names[0] = "JaneTwo";
         jane.Address.HouseNumber = 213;

         Console.WriteLine(john);
         Console.WriteLine(jane);
      }
   }
}