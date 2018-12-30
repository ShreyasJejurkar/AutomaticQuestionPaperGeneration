using System.Web.Mvc;

namespace AutomatedQuestionPaper.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/")]
        public ActionResult Index()
        {
            //var directory = string.Format(@"C:\genquest-master");
            //var proc = new Process
            //{
            //    StartInfo =
            //    {
            //        WorkingDirectory = directory,
            //        FileName = "q.bat",
            //        WindowStyle = ProcessWindowStyle.Hidden,
            //        CreateNoWindow = true
            //    }
            //};
            //proc.Start();
            //proc.WaitForExit();
            //proc.Close();


            Session["Username"] = Session["Staff_Name"] = null;
            return View();
        }
    }
}