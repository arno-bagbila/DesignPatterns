using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;

namespace Solid.SingleResponsibility
{

   public class Beer
   {
      private readonly List<string> comments = new List<string>();
      private static int count = 0;

      public int AddComment(string comment)
      {
         comments.Add($"{++count}: {comment}");
         return count; //memento pattern
      }

      public void RemoveComment(int index)
      {
         comments.RemoveAt(index);
      }

      public override string ToString()
      {
         return string.Join(Environment.NewLine, comments);
      }

      //breaks single responsibility principle
      public void Save(string filename, bool overwrite = false)
      {
         File.WriteAllText(filename, ToString());
      }

      public void Load(string filename)
      {

      }

      public void Load(Uri uri)
      {

      }
   }

   public class BeerPersistence
   {
      public void SaveToFile(Beer beer, string filename, bool overwrite = false)
      {
         if (overwrite || !File.Exists(filename))
            File.WriteAllText(filename, beer.ToString());
      }
   }

   public class Demo
   {
      static void Main(string[] args)
      {
         var beer = new Beer();
         beer.AddComment("first comment");
         beer.AddComment("second comment");
         WriteLine(beer);

         var beerPersistence = new BeerPersistence();
         var filename = @"c:\temp\Beer.txt";
         beerPersistence.SaveToFile(beer, filename, true);
         Process p = new Process();
         p.StartInfo.UseShellExecute = true;
         p.StartInfo.FileName = filename;
         p.Start();
      }
   }
}
