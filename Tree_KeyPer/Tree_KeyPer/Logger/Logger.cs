namespace PK3_project;
using System;
using System.IO;

public class Logger : LogBase
{
   private string CurrentDirectory { get; set; }
   
   private string FileName { get; set; }
   
   private string FilePath { get; set; }

   public Logger()
   {
      this.CurrentDirectory = Directory.GetCurrentDirectory();
      this.FileName = "Log.txt";
      this.FilePath = this.CurrentDirectory + "/" + this.FileName;
   }
   
   public override void Log(string Message)
   {

      using (System.IO.StreamWriter w = System.IO.File.AppendText(this.FilePath))
      {
         w.Write("\r\nLog Entry : ");
         w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
         w.WriteLine("  :{0}", Message);
         w.WriteLine("-----------------------------------------------");
         Console.WriteLine($"Exception message writen in {FilePath}");
      }
   }
}