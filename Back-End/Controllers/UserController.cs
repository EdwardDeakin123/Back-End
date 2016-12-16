using Back_End.Database;
using Back_End.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace Back_End.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        // Access the ActivityTracker database.
        private ActivityTrackerContext _Database = new ActivityTrackerContext();

        public List<User> GetAll()
        {
            //TODO Remove this function as it is obviously a massive security risk. Used only for testing.
            return _Database.Users.ToList();
        }

        // POST: /User/Register
        [HttpPost]
        [AllowAnonymous]
        public void Register(string firstname, string lastname, string username, string password)
        {
            // Create the new User object.
            //TODO: Encrypt the password.
            //TODO: Add a salt to the password.
            //TODO: Check for a user with this username before adding it to the database.
            //TODO Use Any to get a boolean from the database.
            User newUser = new User { FirstName = firstname, LastName = lastname, Username = username, Password = password };

            System.Diagnostics.Debug.WriteLine("Adding a new user...");

            // Add the user to the database.
            _Database.Users.Add(newUser);
            _Database.SaveChanges();
        }

        [HttpPost]
        [AllowAnonymous]
        public void Login(string username, string password)
        {
            //TODO Check for null strings.
            //TODO Maybe create a UserManager to handle the validation.
            var user = _Database.Users.SingleOrDefault(usr => usr.Username == username);

            if(user.Password == password)
            {
                // The password is correct, proceed with creating a token.
                // Used information from the following links to build the authentication system without using
                // ASP.Net Identity which seemed too big for this project.
                // http://stackoverflow.com/questions/31511386/owin-cookie-authentication-without-asp-net-identity
                // http://stackoverflow.com/questions/31584506/how-to-implement-custom-authentication-in-asp-net-mvc-5/31585768#31585768

                // Create a claim that can be passed to the authorization manager.
                var claims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                },
                DefaultAuthenticationTypes.ApplicationCookie);

                // Pass the claim Owin which will create a Token to authenticate the user.
                Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, claims);
                System.Diagnostics.Debug.WriteLine("Auth Success!!!!! Might be logged in now...");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Auth failed!!!!!");
            }
        }

        public void Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
        }
    }
}
