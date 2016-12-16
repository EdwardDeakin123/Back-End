using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Back_End.Models;
using Back_End.Database;

namespace Back_End.Controllers
{
    /// <summary>
    /// API Controller for Activity Model.
    /// </summary>
    // Inheriting from ApiController to implement the API.
    public class ActivityController : ApiController
    {
        // Access the ActivityTracker database.
        private ActivityTrackerContext _Database = new ActivityTrackerContext();

        /// <summary>
        /// GET: /Activity/GetAll
        /// </summary>
        /// <returns>A List containing all the activities stored in the database.</returns>
        [Authorize]
        public List<Activity> GetAll()
        {
            return _Database.Activities.ToList();
        }

        /// <summary>
        /// GET: /Activity/Get
        /// </summary>
        /// <param name="activityId">The ID of an activity to get from the database</param>
        /// <returns>The Activity associated with the ID passed.</returns>
        public Activity Get(int activityId)
        {
            // Get the element from the database.
            return _Database.Activities.SingleOrDefault(act => act.ActivityId == activityId);
        }
    }
}
