using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Back_End.Startup))]
namespace Back_End
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Using token based authentication to manage access to the API.
            // Tokens will be implemented using OWIN.
            // Based this code off information on this website:
            // http://www.developerhandbook.com/c-sharp/create-restful-api-authentication-using-web-api-jwt/
            ConfigureAuth(app);
        }
    }
}
