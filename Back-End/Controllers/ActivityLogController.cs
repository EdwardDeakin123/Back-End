using Back_End.Database;
using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Back_End.Controllers
{
    /// <summary>
    /// API Controller for ActivityLog Model.
    /// </summary>
    // Inheriting from ApiController to implement the API.
    public class ActivityLogController : ApiController
    {
        // Access the ActivityTracker database.
        private ActivityTrackerContext _Database = new ActivityTrackerContext();

        /// <summary>
        /// GET: /ActivityLog/Get
        /// </summary>
        /// <param name="activityLogId">The ID of an ActivityLog entry to get from the database</param>
        /// <returns>The ActivityLog associated with the ID passed.</returns>
        public ActivityLog Get(int activityLogId)
        {
            // Get the element from the database.
            return _Database.ActivityLogs.SingleOrDefault(log => log.ActivityLogId == activityLogId);
        }

        /// <summary>
        /// GET: /ActivityLog/GetByUser
        /// </summary>
        /// <param name="userId">The ID of a User</param>
        /// <returns>A list of activity logs associated with this user.</returns>
        public List<ActivityLog> GetByUser(int userId)
        {
            // Get all elements in the database for this user.
            // Probably won't be used very much if at all. Should instead filter by date.
            return _Database.ActivityLogs.Where(log => log.User.UserId == userId).ToList();
        }
    }
}
