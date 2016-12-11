using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Back_End.Models;

namespace Back_End.Controllers
{
    public class ActivityController : ApiController
    {
        public List<Activity> GetAll()
        {
            Activity testAct = new Activity();
            testAct.ActivityId = 0;
            testAct.ActivityName = "Running";

            List<Activity> activityList = new List<Activity>();
            activityList.Add(testAct);

            return activityList;
        }
    }
}
