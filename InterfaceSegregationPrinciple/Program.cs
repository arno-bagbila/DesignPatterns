using System;
using static System.Console;

namespace InterfaceSegregationPrinciple
{
   public class Demo
   {

      public class Document
      {
         
      }

      public interface IMachine
      {
         void Print(Document d);
         void Scan(Document d);
         void Fax(Document d);
      }

      public interface IPrinter
      {
         void Print(Document d);
      }

      public interface IScanner
      {
         void Scan(Document d);
      }

      public interface IFax
      {
         void Fax(Document d);
      }

      public interface IMultiFunctionDevice : IScanner, IPrinter //.....
      {
         
      }


      public class MultiFunctionPrinter : IMachine
      {
         public void Fax(Document d)
         {
            //
         }

         public void Print(Document d)
         {
            //
         }

         public void Scan(Document d)
         {
            //;
         }
      }

      //People should not pay for something they don't need. We should have smaller interfaces rather than one big interface which do a lot of thing
      public class GoodOldFashionPrinter : IMachine
      {
         public void Fax(Document d)
         {
            throw new NotImplementedException();
         }

         public void Print(Document d)
         {
            //
         }

         public void Scan(Document d)
         {
            throw new NotImplementedException();
         }
      }

      public class Photocopier : IPrinter, IScanner
      {
         public void Print(Document d)
         {
            //
         }

         public void Scan(Document d)
         {
            //
         }
      }

      public class MultiFunctionMachine : IMultiFunctionDevice
      {
         private IPrinter printer;
         private IScanner scanner;

         public MultiFunctionMachine(IPrinter printer, IScanner scanner)
         {
            this.printer = printer ?? throw new ArgumentNullException(nameof(printer));
            this.scanner = scanner ?? throw new ArgumentNullException(nameof(scanner));
         }

         //delegating the call
         public void Print(Document d)
         {
            printer.Print(d);
         }

         //delegating the call
         public void Scan(Document d)
         {
            scanner.Scan(d);
            //decorator
         }
      }

      static void Main(string[] args)
      {

      }
   }
}
