using System;

namespace StepwiseBuilder
{

   public enum CarType
   {
      Sedan,
      Crossover
   }

   public class Car
   {
      public CarType Type;
      public int WheelSize;

      public override string ToString()
      {
         return $"This car is of type {Type} with {WheelSize} wheelsize";
      }
   }

   public interface ISpecifyCarType
   {
      ISpecifyWheelSize WithCarType(CarType carType);
   }

   public interface ISpecifyWheelSize
   {
      IBuildCar WithWheels(int size);
   }

   public class CarBuilder
   {
      public static ISpecifyCarType Create()
      {
         return new Impl();
      }

   }

   class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
   {
      private Car car = new Car();

      public Car Build()
      {
         return car;
      }

      public ISpecifyWheelSize WithCarType(CarType carType)
      {
         car.Type = carType;
         return this;
      }

      public IBuildCar WithWheels(int size)
      {
         switch (car.Type)
         {
            case CarType.Crossover when size < 17 || size > 20:
            case CarType.Sedan when size < 15 || size > 17:
               throw new ArgumentException($"Wrong size of wheel for {car.Type}");
         }

         car.WheelSize = size;
         return this;
      }

   }

   public interface IBuildCar
   {
      public Car Build();
   }

   public class Demo
   {
      static void Main(string[] args)
      {
         var car = CarBuilder.Create()
            .WithCarType(CarType.Crossover)
            .WithWheels(18)
            .Build();

         Console.WriteLine(car.ToString());
      }
   }
}
