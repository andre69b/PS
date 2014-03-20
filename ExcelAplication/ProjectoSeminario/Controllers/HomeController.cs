using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RDotNet;

namespace ProjectoSeminario.Controllers
{
    public class HomeController : Controller
    {
        public static void SetupPath()
        {
            var oldPath = System.Environment.GetEnvironmentVariable("PATH");
            var rPath = System.Environment.Is64BitProcess ? @"C:\Program Files\R\R-3.0.3\bin\x64" : @"C:\Program Files\R\R-3.0.3\bin\i386";

            if (Directory.Exists(rPath) == false)
                throw new DirectoryNotFoundException(string.Format("Could not found the specified path to the directory containing R.dll: {0}", rPath));
            var newPath = string.Format("{0}{1}{2}", rPath, System.IO.Path.PathSeparator, oldPath);
            System.Environment.SetEnvironmentVariable("PATH", newPath);

        }
        public ActionResult Index()
        {
            SetupPath();
            REngine engine = REngine.CreateInstance("RDotNet");
            
                engine.Initialize();
                CharacterVector charVec = engine.CreateCharacterVector(new[] { 
                     "Hello, R world!, .NET speaking" });
                engine.SetSymbol("greetings", charVec);
                engine.Evaluate("str(greetings)");
                string[] a = engine.Evaluate("'Hi there .NET, from the R engine'").AsCharacter().ToArray();
                //Console.WriteLine("R answered: '{0}'", a[0]);
                //Console.WriteLine("Press any kzey to exit the program");
                //Console.ReadKey();
            

            return View("ExcelTest", a[0]);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}