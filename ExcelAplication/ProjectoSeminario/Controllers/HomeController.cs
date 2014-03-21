using System.Web.Mvc;
using ProjectoSeminario.Models;

namespace ProjectoSeminario.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View("ExcelTest", RToExcel.GetTableExcel());
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