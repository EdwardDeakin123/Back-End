using Back_End.Database;
using Back_End.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Net;

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
        public HttpResponseMessage Register(string firstname, string lastname, string username, string password)
        {
            // Create the new User object.
            //TODO: Encrypt the password.

            if (firstname == "" || lastname == "" || username == "" || password == "")
            {
                // If any of the parameters are empty, throw an error.
                // TODO Maybe don't user forbidden status here.
                System.Diagnostics.Debug.WriteLine("Registration failed!!!!! Empty Parameters.");
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Registration Failed - Empty Parameters.");
            }

            // Check if there is already a user with this username.
            if(_Database.Users.Any(usr => usr.Username == username))
            {
                // TODO Maybe don't user forbidden status here.
                System.Diagnostics.Debug.WriteLine("Registration failed!!!!! Username in use.");
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Registration Failed - Username in use.");
            }

            User newUser = new User { FirstName = firstname, LastName = lastname, Username = username, Password = password };

            System.Diagnostics.Debug.WriteLine("Adding a new user...");

            // Add the user to the database.
            _Database.Users.Add(newUser);
            _Database.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Login([FromBody] LoginRequest loginRequest)
        {
            // Using FromBody so the parameters are pulled from the URL request body rather than the query string.
            System.Diagnostics.Debug.WriteLine("Got username " + loginRequest.Username + " and password " + loginRequest.Password);

            if(loginRequest.Username == "" || loginRequest.Password == "")
            {
                // If username or password is empty, throw an error.
                System.Diagnostics.Debug.WriteLine("Auth failed!!!!! Empty Auth.");
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Authentication Failed - Empty Username or Password");
            }

            // Check if there is a user in the database with this username and password.
            if (_Database.Users.Any(usr => usr.Username == loginRequest.Username && usr.Password == loginRequest.Password))
            {
                // The password is correct, proceed with creating a token.
                // Used information from the following links to build the authentication system without using
                // ASP.Net Identity which seemed too big for this project.
                // http://stackoverflow.com/questions/31511386/owin-cookie-authentication-without-asp-net-identity
                // http://stackoverflow.com/questions/31584506/how-to-implement-custom-authentication-in-asp-net-mvc-5/31585768#31585768

                // Create a claim that can be passed to the authorization manager.
                var claims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, loginRequest.Username)
                },
                DefaultAuthenticationTypes.ApplicationCookie);

                // Pass the claim Owin which will create a Token to authenticate the user.
                Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, claims);

                // Authentication succeeded.
                System.Diagnostics.Debug.WriteLine("Auth Success!!!!!");
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                // Send the Forbidden status code for failed authentication.
                System.Diagnostics.Debug.WriteLine("Auth failed!!!!!");
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Authentication Failed");
            }
        }

        public void Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
        }
    }
}
