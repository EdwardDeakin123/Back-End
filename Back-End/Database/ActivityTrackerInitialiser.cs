using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Back_End.Models;

namespace Back_End.Database
{
    // Creates the default entries in the database.
    // Primarily Activities at this stage but may add more.
    // Used the following guide to assist in building this class:
    // https://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application
    public class ActivityTrackerInitialiser : DropCreateDatabaseIfModelChanges<ActivityTrackerContext>
    {
        protected override void Seed(ActivityTrackerContext context)
        {
            // Add some default activities.
            var activities = new List<Activity>
            {
                new Activity { ActivityId=1, ActivityName="Running"},
                new Activity { ActivityId=2, ActivityName="Sleeping" }
            };

            // Save these entries to the database.
            activities.ForEach(s => context.Activities.Add(s));

            // Commit the changes.
            context.SaveChanges();
        }
    }
}