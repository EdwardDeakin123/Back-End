﻿using Back_End.Database;
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
        /// <param name="userId">The ID of a User</param>
        /// <returns>A list of activity logs associated with this user.</returns>
        public List<ActivityLog> GetByUser(int userId)
        {
            // Get all elements in the database for this user.
            // Probably won't be used very much if at all. Should instead filter by date.
            return _Database.ActivityLogs.Where(log => log.User.UserId == userId).ToList();
        }

        /// <summary>
        /// POST: /ActivityLog/Add
        /// </summary>
        [HttpPost]
        public void Add(int userId, int activityId, DateTime startTime, DateTime endTime)
        {
            //TODO Verify that a valid activity and valid user have been passed.
            User user = (User) _Database.Users.SingleOrDefault(usr => usr.UserId == userId);
            Activity activity = (Activity)_Database.Activities.SingleOrDefault(act => act.ActivityId == activityId);

            // Create a new ActivityLog entry in the database.
            ActivityLog newLog = new ActivityLog { Activity = activity, User = user, StartTime = startTime, EndTime = endTime };

            System.Diagnostics.Debug.WriteLine("Adding a new ActivityLog...");

            // Add the ActivityLog to the database.
            _Database.ActivityLogs.Add(newLog);
            _Database.SaveChanges();
        }
    }
}
