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
    public class UserController : ApiController
    {
        // Access the ActivityTracker database.
        private ActivityTrackerContext _Database = new ActivityTrackerContext();

        // POST: /User/Register
        [HttpPost]
        public void Register(string firstname, string lastname, string username, string password)
        {
            // Create the new User object.
            //TODO: Encrypt the password.
            //TODO: Add a salt to the password.
            //TODO: Check for a user with this username before adding it to the database.
            User newUser = new User { FirstName = firstname, LastName = lastname, Username = username, Password = password };

            // Add the user to the database.
            _Database.Users.Add(newUser);
        }
    }
}
