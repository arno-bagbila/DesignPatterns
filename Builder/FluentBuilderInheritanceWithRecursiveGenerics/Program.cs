using System;

namespace FluentBuilderInheritanceWithRecursiveGenerics
{

   public class Person
   {
      public string Name;
      public string Position;

      public class Builder : PersonJobBuilder<Builder>
      {
         
      }

      public static Builder New => new Builder();

      public override string ToString()
      {
         return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
      }
   }

   public class PersonInfoBuilder<SELF> 
      : PersonBuilder 
      where  SELF : PersonInfoBuilder<SELF>
   {

      public SELF Called(string name)
      {
         person.Name = name;
         return (SELF)this;
      }
   }



   public abstract class PersonBuilder
   {
      protected Person person = new Person();

      public Person Build()
      {
         return person;
      }
   }

   public class PersonJobBuilder<SELF> 
      : PersonInfoBuilder<PersonJobBuilder<SELF>> 
      where SELF : PersonJobBuilder<SELF>
   {
      //fluent builder
      public SELF workAsA(string position)
      {
         person.Position = position;
         return (SELF) this;
      }
   }

   internal class Program
   {
      static void Main(string[] args)
      {
         //var builder = new PersonJobBuilder();
         //builder
         //   .Called("Arnaud")
         //   .WorkAsA(); //cannot access the WorkAsA method in the PersonJobBuider because the method Called return a PersonInfoBuilder which knows nothing about PersonJobBuilder

         var me = Person
            .New
            .Called("Arnaud")
            .workAsA("quant").Build();

         Console.WriteLine(me.ToString());
      }
   }
}
