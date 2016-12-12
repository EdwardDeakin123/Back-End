using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Back_End.Models;

namespace Back_End.Controllers
{
    // API Controller for Activity Model.
    // Inheriting from ApiController to implement the API.
    public class ActivityController : ApiController
    {
        // GET: /Activity/GetAll
        public List<Activity> GetAll()
        {
            Activity testAct = new Activity();
            testAct.ActivityId = 0;
            testAct.ActivityName = "Running";

            List<Activity> activityList = new List<Activity>();
            activityList.Add(testAct);

            return activityList;
        }

        // GET: /Activity/Get
        public Activity Get(int activityId)
        {
            Activity testAct = new Activity();
            testAct.ActivityId = 0;
            testAct.ActivityName = "Running";

            return testAct;
        }
    }
}
