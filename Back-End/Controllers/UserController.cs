using Back_End.Database;
using Back_End.Models;
using Back_End.Requests;
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
        public HttpResponseMessage Register(RegisterRequest registerRequest)
        {
            // Create the new User object.
            //TODO: Encrypt the password.

            if (registerRequest.FirstName == "" || registerRequest.LastName == "" || registerRequest.Username == "" || registerRequest.Password == "")
            {
                // If any of the parameters are empty, throw an error.
                // TODO Maybe don't user forbidden status here.
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Registration Failed - Empty Parameters.");
            }

            // Check if there is already a user with this username.
            if(_Database.Users.Any(usr => usr.Username == registerRequest.Username))
            {
                // TODO Maybe don't user forbidden status here.
                System.Diagnostics.Debug.WriteLine("Registration failed!!!!! Username in use.");
                return Request.CreateResponse(HttpStatusCode.Conflict, "Registration Failed - Username in use.");
            }

            User newUser = new User { FirstName = registerRequest.FirstName, LastName = registerRequest.LastName, Username = registerRequest.Username, Password = registerRequest.Password };

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
            if (loginRequest == null || loginRequest.Username == "" || loginRequest.Password == "")
            {
                // If username or password is empty, throw an error.
                System.Diagnostics.Debug.WriteLine("Auth failed!!!!! Empty Auth.");
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Authentication Failed - Empty Username or Password");
            }

            // Using FromBody so the parameters are pulled from the URL request body rather than the query string.
            System.Diagnostics.Debug.WriteLine("Got username " + loginRequest.Username + " and password " + loginRequest.Password);

            // Get the user from the database.
            User user = _Database.Users.Where(usr => usr.Username == loginRequest.Username && usr.Password == loginRequest.Password).FirstOrDefault();

            // Check that the user returned a valid user.
            if (user != default(User))
            {
                // The password is correct, proceed with creating a token.
                // Used information from the following links to build the authentication system without using
                // ASP.Net Identity which seemed too big for this project.
                // http://stackoverflow.com/questions/31511386/owin-cookie-authentication-without-asp-net-identity
                // http://stackoverflow.com/questions/31584506/how-to-implement-custom-authentication-in-asp-net-mvc-5/31585768#31585768

                // Create a claim that can be passed to the authorization manager.
                var claims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, loginRequest.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
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
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Authentication Failed");
            }
        }

        public void Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
        }
    }
}
