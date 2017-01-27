using Back_End.Database;
using Back_End.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Back_End.Requests;

namespace Back_End.Controllers
{
    /// <summary>
    /// API Controller for ActivityLog Model.
    /// </summary>
    // Inheriting from ApiController to implement the API.
    // Mark this class as requiring authentication.
    [Authorize]
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
        /// <returns>A list of activity logs associated with the currently logged in user.</returns>
        public List<ActivityLog> GetByUser()
        {
            // Get all elements in the database for this user.
            // Probably won't be used very much if at all. Should instead filter by date.
            int userId = int.Parse(User.Identity.GetUserId());

            return _Database.ActivityLogs.Where(log => log.User.UserId == userId).ToList();
        }

        /// <summary>
        /// GET: /ActivityLog/GetByDate
        /// </summary>
        /// <returns>A list of activity logs associated with the currently logged in user on a particular date.</returns>
        public List<ActivityLog> GetByDate(DateTime startDate, DateTime endDate)
        {
            // Get all elements in the database for this user.
            // Probably won't be used very much if at all. Should instead filter by date.
            int userId = int.Parse(User.Identity.GetUserId());

            // We only really deal with the entire day and ignore the times, so adjust start and end so they go from 12:00am - 11:59pm
            DateTime actStart = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
            DateTime actEnd = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

            // Get all activity logs on this date.
            return _Database.ActivityLogs.Where(log => log.User.UserId == userId && log.StartTime >= actStart && log.EndTime <= actEnd).ToList();
        }

        /// <summary>
        /// POST: /ActivityLog/Add
        /// </summary>
        /// <param name="userId">ID of the user for the activity log.</param>
        /// <param name="activityId">ID of the activity for the activity log</param>
        /// <param name="startTime">Start Time for this activity log.</param>
        /// <param name="endTime">End Time for this activity log.</param>
        [HttpPost]
        public HttpResponseMessage Add([FromBody] ActivityLogRequest activityLogRequest)
        {
            // Get the UserId of the logged in user.
            int userId = int.Parse(User.Identity.GetUserId());

            //TODO Verify that a valid activity and valid user have been passed.
            User user = (User) _Database.Users.SingleOrDefault(usr => usr.UserId == userId);
            Activity activity = (Activity)_Database.Activities.SingleOrDefault(act => act.ActivityId == activityLogRequest.ActivityId);

            // Create a new ActivityLog entry in the database.
            ActivityLog newLog = new ActivityLog { Activity = activity, User = user, StartTime = activityLogRequest.StartTime, EndTime = activityLogRequest.EndTime };

            System.Diagnostics.Debug.WriteLine("Adding a new ActivityLog...");

            // Add the ActivityLog to the database.
            _Database.ActivityLogs.Add(newLog);
            _Database.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public HttpResponseMessage Update([FromBody] ActivityLogUpdateRequest activityLogUpdateRequest)
        {
            // Get the UserId of the logged in user.
            int userId = int.Parse(User.Identity.GetUserId());

            // Get the ActivityLog from the database.
            ActivityLog activityLog = (ActivityLog)_Database.ActivityLogs.SingleOrDefault(actLog => actLog.ActivityLogId == activityLogUpdateRequest.ActivityLogId);

            // Verify that the current user is the same one associated with this activityLog.
            if(activityLog.User.UserId != userId)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "This log doesn't belong to the current user.");
            }

            // Update the start and end times.
            activityLog.StartTime = activityLogUpdateRequest.StartTime;
            activityLog.EndTime = activityLogUpdateRequest.EndTime;

            // Commit the changes to the database.
            _Database.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int activityLogId)
        {
            // Get the UserId of the logged in user.
            int userId = int.Parse(User.Identity.GetUserId());

            // Get the ActivityLog from the database.
            ActivityLog activityLog = (ActivityLog)_Database.ActivityLogs.SingleOrDefault(actLog => actLog.ActivityLogId == activityLogId);

            // Verify that the current user is the same one associated with this activityLog.
            if (activityLog.User.UserId != userId)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "This log doesn't belong to the current user.");
            }

            _Database.ActivityLogs.Remove(activityLog);

            // Commit the changes to the database.
            _Database.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
