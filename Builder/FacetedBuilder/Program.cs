using System;

namespace FacetedBuilder
{

   public class Person
   {
      //address
      public string StreetAddress, Postcode, City;

      //employment
      public string CompanyName, Position;

      public override string ToString()
      {
         return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
      }

      public int AnnualIncome;

   }

   public class PersonBuilder //facade
   {
      //reference!
      protected Person person = new Person();

      public PersonJobBuilder Works => new PersonJobBuilder(person);

      public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

      public static implicit operator Person(PersonBuilder pb)
      {
         return pb.person;
      }
   }

   public class PersonJobBuilder : PersonBuilder
   {
      public PersonJobBuilder(Person person)
      {
         this.person = person;
      }

      public PersonJobBuilder At(string companyName)
      {
         person.CompanyName = companyName;
         return this;
      }

      public PersonJobBuilder ASA(string position)
      {
         person.Position = position;
         return this;
      }

      public PersonJobBuilder Earning(int amount)
      {
         person.AnnualIncome = amount;
         return this;
      }
   }

   public class PersonAddressBuilder : PersonBuilder
   {
      //might not work with value type
      public PersonAddressBuilder(Person person)
      {
         this.person = person;
      }

      public PersonAddressBuilder Street(string street)
      {
         person.StreetAddress = street;
         return this;
      }

      public PersonAddressBuilder WithPostCode(string postcode)
      {
         person.Postcode = postcode;
         return this;
      }

      public PersonAddressBuilder In(string city)
      {
         person.City = city;
         return this;
      }
   }

   public class Demo
   {
      static void Main(string[] args)
      {
         var pb = new PersonBuilder();
         Person person = pb
            .Works
               .At("Freewheel")
               .ASA("Developer")
               .Earning(2)
            .Lives
               .Street("Avarn Road")
               .WithPostCode("swb")
               .In("London");


         Console.WriteLine(person.ToString());
      }
   }
}
