using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Back_End.Controllers
{
    public class HomeController : Controller
    {
       private ActivityTrackerContext _Database = new ActivityTrackerContext();

       
        
        public List<ActivityLog> GetAll2()
        {
            return _Database.ActivityLogs.ToList();
        }

        public ActionResult Index()
        {
            return View(GetAll2());
        }

        public void ExportClientsListToCSV()
        {

            StringWriter sw = new StringWriter();
            

            sw.WriteLine("\"Start Time\",\"End Time\",\"User\",\"Activity\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Exported_Users.csv");
            Response.ContentType = "text/csv";

            foreach (var line in GetAll2())
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                                           line.StartTime,
                                           line.EndTime,
                                           line.User.Username,
                                           line.Activity.ActivityName));
            }

            Response.Write(sw.ToString());

            Response.End();

        }
    }
}
