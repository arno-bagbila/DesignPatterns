using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderCodingExercice
{


   public class CodeBuilder
   {
      public string ClassName;
      private List<string> elements = new();

      public CodeBuilder(string className)
      {
         ClassName = className;
      }

      public CodeBuilder AddField(string name, string type)
      {
         elements.Add($"  public {type} {name};");
         return this;
      }

      public override string ToString()
      {
         var sb = new StringBuilder();
         sb.AppendLine($"public class {ClassName}");
         sb.AppendLine();
         sb.AppendLine("{");
         foreach (var element in elements)
         {
            sb.AppendLine(element);
         }

         sb.AppendLine();
         sb.Append("}");
         return sb.ToString();
      }
   }

   class Program
   {
      static void Main(string[] args)
      {
         var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
         Console.WriteLine(cb);


      }
   }
}
