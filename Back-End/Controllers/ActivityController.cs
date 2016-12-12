using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Back_End.Models;
using Back_End.Database;

namespace Back_End.Controllers
{
    // API Controller for Activity Model.
    // Inheriting from ApiController to implement the API.
    public class ActivityController : ApiController
    {
        private ActivityTrackerContext _Database = new ActivityTrackerContext();

        // GET: /Activity/GetAll
        public List<Activity> GetAll()
        {
            Activity testAct = new Activity();
            testAct.ActivityId = 0;
            testAct.ActivityName = "Running";

            List<Activity> activityList = new List<Activity>();
            activityList.Add(testAct);

            return _Database.Activities.ToList();
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
