using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace BuilderPatterns
{

   public class HtmlElement
   {
      public string Name, Text;
      public List<HtmlElement> Elements = new List<HtmlElement>();
      private const int indentSize = 2;

      public HtmlElement()
      {
            
      }

      public HtmlElement(string name, string text)
      {
         Name = name ?? throw new ArgumentNullException(nameof(name));
         Text = text ?? throw new ArgumentNullException(nameof(text));
      }

      private string toStringImpl(int indent)
      {
         var sb = new StringBuilder();
         var i = new string(' ', indentSize * indent);
         sb.Append($"{i}<{Name}>\n");

         if (!string.IsNullOrWhiteSpace(Text))
         {
            sb.Append(new string(' ', indentSize * (indent + 1)));
            sb.Append(Text);
            sb.Append("\n");
         }

         foreach (var element in Elements)
         {
            sb.Append(element.toStringImpl(indent + 1));
         }

         sb.AppendLine($"{i}</{Name}>");

         return sb.ToString();
      }

      public override string ToString()
      {
         return toStringImpl(0);
      }
   }

   //this is a builder
   public class HtmlBuilder
   {
      private readonly string _rootName;
      private HtmlElement root = new HtmlElement();

      public HtmlBuilder(string rootName)
      {
         _rootName = rootName;
         root.Name = rootName;
      }

      //Thi is a fluent builder
      public HtmlBuilder AddChild(string childName, string childText)
      {
         var e = new HtmlElement(childName, childText);
         root.Elements.Add(e);
         return this;
      }

      public override string ToString()
      {
         return root.ToString();
      }

      public void Clear()
      {
         root = new HtmlElement { Name = _rootName };
      }
   }
   public class Demo
   {
      static void Main(string[] args)
      {
         var hello = "hello";
         var sb = new StringBuilder();
         sb.Append("<p>");
         sb.Append(hello);
         sb.Append("<p>");
         WriteLine(sb);

         var words = new[] { "hello", "world" };
         sb.Clear();
         sb.Append("<ul>");
         foreach (var word in words)
         {
            sb.AppendFormat("<li>{0}</li>", word);
         }

         sb.Append("</ul>");

         WriteLine(sb);

         var builder = new HtmlBuilder("ul");
         builder.AddChild("li", "hello")
            .AddChild("li", "world");

         WriteLine(builder.ToString());
      }
   }
}
