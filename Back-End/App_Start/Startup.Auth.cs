using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Back_End
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // This method configures the authentication in the app.
            // Sets it to use Token based cookie authentication.
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });
        }
    }
}